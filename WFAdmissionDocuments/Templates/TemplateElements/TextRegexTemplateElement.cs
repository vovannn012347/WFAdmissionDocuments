using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.Code.TemplateStuff.Properties;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;

namespace WFAdmissionDocuments.Templates.TemplateElements
{
    //template text to enter in document create mode
    public class TextRegexTemplateElement : Label, ITextTemplateElement, ITemplateKeyElement
    {
        private Timer longClickTimer;
        private bool isLongClick;
        private MouseEventArgs LongPressArgs;

        public EventHandler<MouseEventArgs> LongClick { get => _LongClick; set { _LongClick = value; } }
        public EventHandler<MouseEventArgs> LongPress { get => _LongPress; set { _LongPress = value; } }

        public EventHandler ResizeStart { get => _ResizeStart; set { _ResizeStart = value; } }
        public EventHandler ResizeEnd { get => _ResizeEnd; set { _ResizeEnd = value; } }

        public event EventHandler<MouseEventArgs> _LongClick;
        public event EventHandler<MouseEventArgs> _LongPress;

        public event EventHandler _ResizeStart;
        public event EventHandler _ResizeEnd;

        public ITemplateData GetSaveData()
        {
            return new KeyedRegexTextTemplate();
        }

        private static List<ElementPropertyProperties> PopertyDefs =
            new List<ElementPropertyProperties>{
                PropertyDictionary.Get(typeof(TextTextBoxProperty)),
                PropertyDictionary.Get(typeof(TemplateKeyProperty)),
                PropertyDictionary.Get(typeof(TemplateKeyFriendlyNameProperty)),
                PropertyDictionary.Get(typeof(UseTextAsFallbackTextBoxProperty)),

                PropertyDictionary.Get(typeof(RegexPatternProperty)),
                PropertyDictionary.Get(typeof(CaptureGroupProperty)),

                PropertyDictionary.Get(typeof(FontTextBoxProperty)),
                PropertyDictionary.Get(typeof(FontColorTextBoxProperty)),
                PropertyDictionary.Get(typeof(DownScaleTextTextBoxProperty)),
                PropertyDictionary.Get(typeof(MultilineTextBoxProperty)),
                PropertyDictionary.Get(typeof(TextAlignTextBoxProperty)),

                PropertyDictionary.Get(typeof(PositionProperty)),
                PropertyDictionary.Get(typeof(SizeProperty)),
            };
        

        private bool _selected = false;
        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                Invalidate();
            }
        }
        
        private bool _resizing = false;
        public bool Resizing
        {
            get => _resizing;
            set
            {
                _resizing = value;
                Invalidate();
            }
        }

        //public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = value; }

        private bool _multiline = false;
        [Description("Gets or sets value whether this is a multiline control")]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                OnResize(null);
                //Invalidate();
            }
        }

        //public override Size Size

        //todo: set template key flag
        [Browsable(true)]
        [Description("Key that tells where to put text that is entered as input at this key when doing document generation")]
        public string TemplateKey { get; set; }

        [Browsable(true)]
        [Description("Friendly name to display as template key name as variable")]
        public string TemplateKeyName { get; set; }

        [Browsable(true)]
        [Description("Downscale text on pdf generation to fit container (No design-time effect)")]
        public bool DownScaleText { get; set; }

        [Browsable(true)]
        [Description("Display fallback text in pdf")]
        public bool UseTextAsFallback { get; set; }


        [Browsable(true)]
        [Description("Regular expression that selects text (one capture group required)")]
        public string RegexPattern { get; set; }

        [Browsable(true)]
        [Description("Capture group to select as text")]
        public int CaptureGroup { get; set; }

        public IEnumerable<ElementPropertyProperties> Properties => PopertyDefs;

        public TextRegexTemplateElement()
        {
            this.MouseEnter += TextTemplateElement_MouseEnter;
            this.MouseLeave += TextTemplateElement_MouseLeave;
            this.Click += TextTemplateElement_Click;
            
            this.MouseDown += TextTemplateElement_MouseDown;
            this.MouseUp += TextTemplateElement_MouseUp;

            this.MouseMove += TextTemplateElement_MouseMove;

            longClickTimer = new Timer();
            longClickTimer.Interval = Constants.LongClickDuration;
            longClickTimer.Tick += LongClickTimer_Tick;

            this.AutoSize = false;

            var backColor = this.BackColor;
            //this.ReadOnly = true;
            this.BackColor = backColor;
        }

        private INotifySelected ParentContainer;
        public void SetContainerForm(INotifySelected parentForm)
        {
            ParentContainer = parentForm;
        }
        private void TextTemplateElement_Click(object sender, System.EventArgs e)
        {
            this.Selected = true;
            ParentContainer?.NotifySelected(this);
        }

        public void InitDefaults()
        {
            this.Size = new System.Drawing.Size(100, 20);
            this.Font = Constants.DefaultFont;
        }

        public bool MouseHoverBool = false;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (this.Selected)
                DrawUtils.PaintBorder(this, pe.Graphics, Constants.SelectionColor);

            if (this.MouseHoverBool)
                DrawUtils.PaintBorder(this, pe.Graphics, Constants.HoverColor);
        }

        protected override void OnResize(EventArgs e)
        {
            if(!_multiline) Height = PreferredHeight;

            base.OnResize(e);
        }

        private void TextTemplateElement_MouseLeave(object sender, System.EventArgs e)
        {
            this.MouseHoverBool = false;
            this.Invalidate();

            longClickTimer.Stop();
        }

        private void TextTemplateElement_MouseEnter(object sender, System.EventArgs e)
        {
            this.MouseHoverBool = true;
            this.Invalidate();
        }

        private void TextTemplateElement_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
            e.X >= this.Width - Constants.ResizeHandleSize &&
            e.Y >= this.Height - Constants.ResizeHandleSize)
            {
                Resizing = true;
                ResizeStart?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                isLongClick = false;
                LongPressArgs = e;
                longClickTimer.Start();
            }
        }

        private void TextTemplateElement_MouseUp(object sender, MouseEventArgs e)
        {
            if (Resizing)
            {
                Resizing = false;
                ResizeEnd?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                longClickTimer.Stop();

                if (isLongClick)
                {
                    isLongClick = false;
                    _LongClick?.Invoke(this, e);
                }
            }
        }

        private void TextTemplateElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= this.Width - Constants.ResizeHandleSize &&
               e.Y >= this.Height - Constants.ResizeHandleSize)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }

            if (Resizing)
            {
                int newWidth = e.Location.X; //this.Width + (e.X - lastMousePosition.X);
                int newHeight = e.Location.Y; //this.Height + (e.Y - lastMousePosition.Y);

                // Ensure the control doesn't get too small
                if (newWidth >= Constants.ResizeHandleSize && newHeight >= Constants.ResizeHandleSize)
                {
                    this.Size = new Size(newWidth, newHeight);
                }
            }
        }

        private void LongClickTimer_Tick(object sender, EventArgs e)
        {
            isLongClick = true;
            longClickTimer.Stop();

            _LongPress?.Invoke(this, LongPressArgs);
            LongPressArgs = null;
        }
    }
}
