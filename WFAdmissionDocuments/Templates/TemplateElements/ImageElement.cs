using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.Code.TemplateStuff.Properties;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.Templates.TemplateElements
{
    public class ImageElement : PictureBox, ITemplateElement, IPreprintHideableElement
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

        private static List<ElementPropertyProperties> PopertyDefs = 
            new List<ElementPropertyProperties>{
                PropertyDictionary.Get(typeof(PositionProperty)),
                PropertyDictionary.Get(typeof(SizeProperty)),
                PropertyDictionary.Get(typeof(ImageSourceProperty)),
                PropertyDictionary.Get(typeof(IsVanishableProperty))
            };
        public IEnumerable<ElementPropertyProperties> Properties => PopertyDefs;


        private bool _selected = false;
        public bool Selected {
            get => _selected;
            set {
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

        private string imagePath;

        [Browsable(false)]
        public string ImagePath
        {
            get => imagePath;
            set => imagePath = value;
        }
        

        [Editor(typeof(ImageFileEditor), typeof(UITypeEditor))]
        public new Image Image
        {
            get => base.Image;
            set {

                var prevImage = base.Image;
                base.Image = value;

                if(prevImage != null)
                    prevImage.Dispose();
            }
        }

        [Browsable(true)]
        [Description("When true - does not display this picture in pdf for print when Pre Printed Page setting is turned on")]
        public bool IsVanishable { get; set; }

        public ImageElement()
        {
            this.MouseEnter += ImageElement_MouseEnter;
            this.MouseLeave += ImageElement_MouseLeave;
            this.Click += this.ImageElement_Click;


            this.MouseDown += ImageElement_MouseDown;
            this.MouseUp += ImageElement_MouseUp;

            this.MouseMove += ImageElement_MouseMove;

            longClickTimer = new Timer();
            longClickTimer.Interval = Constants.LongClickDuration;
            longClickTimer.Tick += LongClickTimer_Tick;
        }

        public void InitDefaults()
        {
            this.Size = new System.Drawing.Size(100, 100);
            this.Image = Resources.imagePlaceholder;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private INotifySelected ParentContainer;
        public void SetContainerForm(INotifySelected parentForm)
        {
            ParentContainer = parentForm;
        }
        private void ImageElement_Click(object sender, System.EventArgs e)
        {
            this.Selected = true;
            ParentContainer?.NotifySelected(this);
        }

        public bool MouseHoverBool = false;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if(this.Selected)
                DrawUtils.PaintBorder(this, pe.Graphics, Constants.SelectionColor);

            if (this.MouseHoverBool)
                DrawUtils.PaintBorder(this, pe.Graphics, Constants.HoverColor);
        }

        private void ImageElement_MouseEnter(object sender, System.EventArgs e)
        {
            this.MouseHoverBool = true;
            this.Invalidate();
        }

        private void ImageElement_MouseLeave(object sender, System.EventArgs e)
        {
            this.MouseHoverBool = false;
            this.Invalidate();

            longClickTimer.Stop();
        }

        private void ImageElement_MouseDown(object sender, MouseEventArgs e)
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

        private void ImageElement_MouseUp(object sender, MouseEventArgs e)
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

        private void ImageElement_MouseMove(object sender, MouseEventArgs e)
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

        public ITemplateData GetSaveData()
        {
            return new ImageTemplate();
        }
    }


    public class ImageFileEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var instance = context?.Instance;
            if (context?.Instance is TypeDescriptorWrapper descriptor)
                if (descriptor.Descriptor.Instance is ImageElement pictureBox)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            using (Stream stream = File.OpenRead(filePath))
                            {
                                Image image = System.Drawing.Image.FromStream(stream);
                                pictureBox.Image = image;
                                pictureBox.ImagePath = filePath; // Update the ImagePath property
                            }


                        }
                    }
                }
            return value;
        }
    }
}
