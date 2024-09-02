using iText.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.Properties;
using WFAdmissionDocuments.Templates;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments
{
    public partial class TemplateEditForm : Form, INotifySelected
    {
        public bool FileEdit { get; set; } = false;
        public bool FileSaved { get; set; } = false;
        public string FileToOpen { get; set; }

        #region Edit Part
        public event EventHandler ElementSelected;
        public TemplateElementPropertiesForm PropertiesForm;
        public RegexTesterForm RegexTestForm;
        protected Type _templateAddType;
        protected TemplateEditMode _templateEditMode = TemplateEditMode.EditElement;
        protected TemplateEditMode TemplateEditMode {
            get => _templateEditMode;
            set
            {
                if (value != _templateEditMode)
                {
                    _templateEditMode = value;
                    UpdateEditMode();
                }
            }
        }
        protected ITemplateElement _selectedElement;
        public ITemplateElement SelectedElement
        {
            get => _selectedElement; set
            {
                if (value != _selectedElement)
                {
                    _selectedElement = value;
                    ElementSelected?.Invoke(_selectedElement, null);
                }
            }
        }
        public Point SelectedElementMousePosition { get; private set; }

        public TemplateEditForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            
            this.KeyDown += GlobalKeyListenerForm_KeyDown;
        }

        private void GlobalKeyListenerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.N && newToolStripMenuItem.Visible)
                {
                    InitializeEmpty();
                }

                if (e.KeyCode == Keys.O && openToolStripMenuItem.Visible)
                {
                    OpenFileCLick();
                }

                if (e.KeyCode == Keys.S && saveToolStripMenuItem.Visible)
                {
                    Save();
                }
                
                if (e.KeyCode == Keys.S && saveToolStripMenuItem.Visible)
                {
                    Save();
                }

                if (e.KeyCode == Keys.P && printToolStripMenuItem.Visible)
                {
                    PrintFileClick();
                }


                if (e.KeyCode == Keys.Delete && deleteToolStripMenuItem.Visible)
                {
                    DeleteElement();
                }

                if (e.KeyCode == Keys.Up)
                {
                    BringElementFront();
                }

                if (e.KeyCode == Keys.Down)
                {
                    BringElementToBack();
                }
            }


            if (e.Alt)
            {
                if (e.KeyCode == Keys.P)
                {
                    SwitchPropertiesFormVisible(!PropertiesForm.Visible);
                }

                if (e.KeyCode == Keys.L)
                {
                    SwitchPalleteVisible();
                }

                if (e.Alt && e.KeyCode == Keys.F4)
                {
                    Close();
                }

                if (e.KeyCode == Keys.R)
                {
                    SwitchRegexVisible(!RegexTestForm.Visible);
                }
            }
        }

        private void AddEditTemplateForm_Load(object sender, EventArgs e)
        {
            //pdfGenerator.PageWidth = 210; // in millimeters
            //pdfGenerator.PageHeight = 297; // in millimeters

            this.SuspendLayout();
            var size = new Size
            {
                Width = SizeUtils.MillimetersToPixels(210),
                Height = SizeUtils.MillimetersToPixels(297),
            };

            PdfSourcePanel.Size = size;
            InterceptPanel.Size = size;

            PropertiesForm = new TemplateElementPropertiesForm(this);

            PropertiesForm.VisibleChanged += PropertiesForm_VisibleChanged;
            PropertiesForm.PropertyChanged += PropertiesForm_PropertyChanged;
            FileSaved = false;

            RegexTestForm = new RegexTesterForm(this);
            RegexTestForm.VisibleChanged += RegexTestForm_VisibleChanged;

            InitializeEmpty();

            if (FileEdit && !string.IsNullOrWhiteSpace(FileToOpen) && File.Exists(FileToOpen))
            {
                newToolStripMenuItem.Visible = false;
                openToolStripMenuItem.Visible = false;

                OpenFile(FileToOpen);
            }

            this.ResumeLayout();
            LocalizeForm();
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.TemplateEditForm_Title;
            //this.Text = "Edit Template";

            this.fileToolStripMenuItem.Text = Resources.TemplateEditForm_FileToolStrip_Text;// = "File";
            this.newToolStripMenuItem.Text = Resources.TemplateEditForm_NewToolStrip_Text;// = "New";
            this.openToolStripMenuItem.Text = Resources.TemplateEditForm_OpenToolStrip_Text;// = "Open";
            this.saveToolStripMenuItem.Text = Resources.Save_Text;// = "Save";
            this.printToolStripMenuItem.Text = Resources.TemplateEditForm_PrintToolStrip_Text;// = "To pdf";
            this.closeToolStripMenuItem.Text = Resources.TemplateEditForm_CloseToolStrip_Text;// = "Close";
            
            this.toolStripMenuItemEdit.Text = Resources.TemplateEditForm_EditToolStrip_Text;// = "Edit";
            this.deleteToolStripMenuItem.Text = Resources.TemplateEditForm_DeleteToolStrip_Text;// = "Delete";
            this.bringToFrontToolStripMenuItem.Text = Resources.TemplateEditForm_BringToFrontToolStrip_Text;// = "Bring To Front";
            this.sendToBackToolStripMenuItem.Text = Resources.TemplateEditForm_SendToBackToolStrip_Text;// = "Send To Back";
            
            this.settingsToolStripMenuItem.Text = Resources.TemplateEditForm_SettingsToolStrip_Text;// = "Settings";
            
            this.stashToolStripMenuItem.Text = Resources.TemplateEditForm_StashToolStrip_Text;// = "Stash";
            this.saveAsToolStripMenuItem.Text = Resources.TemplateEditForm_SaveAsToolStrip_Text;// = "Save As";
            this.openTemplateFileToolStripMenuItem.Text = Resources.TemplateEditForm_OpenTemplateFileToolStrip_Text;// = "Open Template File";
            
            this.viewToolStripMenuItem.Text = Resources.TemplateEditForm_ViewToolStrip_Text;// = "View";
            this.propertiesToolStripMenuItem.Text = Resources.TemplateEditForm_PropertiesToolStrip_Text;// = "Properties";
            this.palleteToolStripMenuItem.Text = Resources.TemplateEditForm_palleteToolStrip_Text;// = "Pallete";
            this.regExTestToolStripMenuItem.Text = Resources.TemplateEditForm_regExTestToolStrip_Text;// = "RegExTest";
            
            this.palleteGroupBox.Text = Resources.TemplateEditForm_palleteGroupBox_Text;// = "Pallete";

            this.PImageRadioButton.Text = Resources.TemplateEditForm_PalleteImage_Text;// = "Image";
            this.PTextRadioButton.Text = Resources.TemplateEditForm_PalleteText_Text;// = "Text";
            this.PTextTemplateRadioButton.Text = Resources.TemplateEditForm_PalleteTextTemplate_Text;// = "Text Input";
            this.PTextMultilineRadioButton.Text = Resources.TemplateEditForm_PalleteTextMultiline_Text;// = "Text input multiline";
            this.PTextRegexRadioButton.Text = Resources.TemplateEditForm_PalleteTextRegex_Text;// = "Text input(RegEx)";
        }

        private void PropertiesForm_PropertyChanged(object sender, EventArgs e)
        {
            FileSaved = false;
        }

        private void PropertiesForm_VisibleChanged(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.Checked = PropertiesForm.Visible;
        }

        private void RegexTestForm_VisibleChanged(object sender, EventArgs e)
        {
            regExTestToolStripMenuItem.Checked = RegexTestForm.Visible;
        }

        private void InitializeEmpty()
        {
            PTextTemplateRadioButton.Checked = false;
            PTextRadioButton.Checked = false;
            PImageRadioButton.Checked = false;

            TemplateEditMode = TemplateEditMode.EditElement;
            SelectedElement = null;
            TemplateProperties = null;
            PdfSourcePanel.Controls.Clear();

            InterceptPanel.SendToBack();
            InterceptPanel.BackgroundImage = null;
            InterceptPanel.Capture = false;
        }
        
        //TODO: should do it recursively, fix later
        private Bitmap DrawControlsToBitmap(Control parentControl)
        {
            Bitmap bitmap = new Bitmap(parentControl.Width, parentControl.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                parentControl.DrawToBitmap(bitmap, new Rectangle(0, 0, PdfSourcePanel.Width, PdfSourcePanel.Height));

                for (int i = parentControl.Controls.Count - 1; i >= 0; i--)
                {
                    Control control = parentControl.Controls[i];
                    
                    control.DrawToBitmap(bitmap, control.Bounds);
                }
            }
            return bitmap;
        }

        private void UpdateEditMode()
        {
            if (TemplateEditMode == TemplateEditMode.AddElement)
            {
                Bitmap bmp = DrawControlsToBitmap(PdfSourcePanel); //new Bitmap(PdfSourcePanel.Width, PdfSourcePanel.Height);

                //Draw the control onto the Bitmap
                //PdfSourcePanel.DrawToBitmap(bmp, new Rectangle(0, 0, PdfSourcePanel.Width, PdfSourcePanel.Height));

                InterceptPanel.BackgroundImage = bmp;
                InterceptPanel.BringToFront();
                InterceptPanel.Capture = true;
                InterceptPanel.Cursor = Cursors.Cross;
            }
            else
            if (TemplateEditMode == TemplateEditMode.EditElement)
            {
                InterceptPanel.SendToBack();
                InterceptPanel.BackgroundImage.Dispose();
                InterceptPanel.BackgroundImage = null;
                InterceptPanel.Capture = false;

                var checkBoxType = typeof(CheckBox);
                foreach (var child in flowLayoutPalletePanel.Controls)
                {
                    if (checkBoxType.IsAssignableFrom(child.GetType())) {
                        (child as CheckBox).Checked = false;
                    }
                }
            }
            else
            if (TemplateEditMode == TemplateEditMode.MoveElement)
            {
                Bitmap bmp = DrawControlsToBitmap(PdfSourcePanel);
                //Bitmap bmp = new Bitmap(PdfSourcePanel.Width, PdfSourcePanel.Height);
                //PdfSourcePanel.DrawToBitmap(bmp, new Rectangle(0, 0, PdfSourcePanel.Width, PdfSourcePanel.Height));

                InterceptPanel.BackgroundImage = bmp;
                InterceptPanel.BringToFront();
                InterceptPanel.Capture = true;
                InterceptPanel.Cursor = Cursors.SizeAll;
            }
            else
            if (TemplateEditMode == TemplateEditMode.ResizeElement)
            {
                Bitmap bmp = DrawControlsToBitmap(PdfSourcePanel);
                //Bitmap bmp = new Bitmap(PdfSourcePanel.Width, PdfSourcePanel.Height);
                //PdfSourcePanel.DrawToBitmap(bmp, new Rectangle(0, 0, PdfSourcePanel.Width, PdfSourcePanel.Height));

                InterceptPanel.BackgroundImage = bmp;
                InterceptPanel.BringToFront();
                InterceptPanel.Capture = true;
                InterceptPanel.Cursor = Cursors.SizeNWSE;
            }
        }

        private void InterceptPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Point position = e.Location;
            if (_templateAddType != null)
            {
                Control newElement = (Control)Activator.CreateInstance(_templateAddType);

                newElement.Location = position;

                InitElement(newElement as ITemplateElement);

                _templateAddType = null;
                TemplateEditMode = TemplateEditMode.EditElement;
            }
        }

        private void InitElement(ITemplateElement newElement)
        {
            (newElement as ITemplateElement).InitDefaults();
            (newElement as ITemplateElement).SetContainerForm(this);
            (newElement as ITemplateElement).LongPress += Element_LongPress;
            (newElement as ITemplateElement).ResizeStart += Element_ResizeStart;
            (newElement as Control).DoubleClick += Element_DoubleClick;

            PdfSourcePanel.Controls.Add(newElement as Control);
            PdfSourcePanel.Controls.SetChildIndex(newElement as Control, 0);
        }

        private void SetAddElementSelected(object element, Type type)
        {
            if ((element as CheckBox).Checked)
            {
                TemplateEditMode = TemplateEditMode.AddElement;
                _templateAddType = type;
                foreach (var checkBox in flowLayoutPalletePanel.Controls)
                {
                    if (checkBox is CheckBox cb && cb != element)
                    {
                        cb.Checked = false;
                    }
                }
            }
            else
            {
                _templateAddType = null;
                TemplateEditMode = TemplateEditMode.EditElement;
            }
        }

        private void PImageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetAddElementSelected(sender, typeof(ImageElement));
        }

        private void PTextRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetAddElementSelected(sender, typeof(TextStaticElement));
        }

        private void PTextTemplateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetAddElementSelected(sender, typeof(TextTemplateElement));
        }

        private void PTextMultilineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetAddElementSelected(sender, typeof(TextMultipartTemplateElement));
        }

        private void PTextRegexRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetAddElementSelected(sender, typeof(TextRegexTemplateElement));
        }

        private void SwitchPropertiesFormVisible(bool setVisible)
        {
            if (setVisible)
            {
                PropertiesForm.Show();
                propertiesToolStripMenuItem.Checked = true;
            }
            else
            {
                PropertiesForm.Hide();
                propertiesToolStripMenuItem.Checked = false;
            }
            this.Focus();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchPropertiesFormVisible(!PropertiesForm.Visible);
        }

        public void NotifySelected(ITemplateElement element)
        {
            foreach(var control in PdfSourcePanel.Controls)
            {
                if(control is ITemplateElement el)
                {
                    el.Selected = false;
                }
            }
            element.Selected = true;
            SelectedElement = element;
        }

        private void SwitchPalleteVisible()
        {
            if (palleteGroupBox.Visible)
            {
                palleteGroupBox.Hide();
                palleteToolStripMenuItem.Checked = false;
            }
            else
            {
                palleteGroupBox.Show();
                palleteToolStripMenuItem.Checked = true;
            }
        }
        private void palleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchPalleteVisible();
        }

        private void SwitchRegexVisible(bool setVisible)
        {
            if (setVisible)
            {
                RegexTestForm.Show();
                regExTestToolStripMenuItem.Checked = true;
            }
            else
            {
                RegexTestForm.Hide();
                regExTestToolStripMenuItem.Checked = false;
            }
            this.Focus();
        }
        private void reExTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchRegexVisible(!RegexTestForm.Visible);
        }

        private void InterceptPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (_templateEditMode == TemplateEditMode.MoveElement)
            {
                var drawPoint = new Point(e.Location.X - SelectedElementMousePosition.X, e.Location.Y - SelectedElementMousePosition.Y);

                InterceptPanel.SetDrawSquare(e.Location, SelectedElement.Size);
            }
            else
            if (_templateEditMode == TemplateEditMode.ResizeElement)
            {
                InterceptPanel.SetDrawSquare(SelectedElement.Location, e.Location);
            }
        }


        private void InterceptPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_templateEditMode == TemplateEditMode.MoveElement)
            {
                var drawPoint = new Point(e.Location.X - SelectedElementMousePosition.X, e.Location.Y - SelectedElementMousePosition.Y);

                if (drawPoint.X < 0 || drawPoint.X > InterceptPanel.Size.Width ||
                    drawPoint.Y < 0 || drawPoint.Y > InterceptPanel.Size.Height)
                {
                    if (drawPoint.X < 0) drawPoint.X = 0;
                    if (drawPoint.X > InterceptPanel.Size.Width) drawPoint.X = InterceptPanel.Size.Width;
                    if (drawPoint.Y < 0) drawPoint.Y = 0;
                    if (drawPoint.Y > InterceptPanel.Size.Height) drawPoint.Y = InterceptPanel.Size.Height;

                    InterceptPanel.SetDrawSquare(drawPoint, SelectedElement.Size);
                }
                else
                {
                    InterceptPanel.SetDrawSquare(drawPoint, SelectedElement.Size);
                }
            }
            else
            if (_templateEditMode == TemplateEditMode.ResizeElement)
            {
                int x = e.Location.X, y = e.Location.Y;

                if (e.Location.X <= 0 || e.Location.X >= InterceptPanel.Size.Width ||
                    e.Location.Y <= 0 || e.Location.Y >= InterceptPanel.Size.Height)
                {
                    if (e.Location.X <= 0) x = 0;
                    if (e.Location.X >= InterceptPanel.Size.Width) x = InterceptPanel.Size.Width;
                    if (e.Location.Y <= 0) y = 0;
                    if (e.Location.Y >= InterceptPanel.Size.Height) y = InterceptPanel.Size.Height;
                }

                var width = x - SelectedElement.Location.X;
                var height = y - SelectedElement.Location.Y;

                width = width < Constants.ResizeHandleSize ? Constants.ResizeHandleSize : width;
                height = height < Constants.ResizeHandleSize ? Constants.ResizeHandleSize : height;

                SelectedElement.Size = new Size(width, height);

                InterceptPanel.SetDrawSquare(SelectedElement.Location, SelectedElement.Size);
            }
        }

        private void InterceptPanel_MouseLeave(object sender, EventArgs e)
        {
            StopInterceptMouseMove();
        }

        private void InterceptPanel_MouseUp(object sender, MouseEventArgs e)
        {
            StopInterceptMouseMove();
        }

        private void StopInterceptMouseMove()
        {
            if (_templateEditMode == TemplateEditMode.MoveElement)
            {
                SelectedElement.Location = InterceptPanel.ResultPosition;
                FileSaved = false;
            }
            else
            if (_templateEditMode == TemplateEditMode.ResizeElement)
            {
                SelectedElement.Size = InterceptPanel.ResultSize;
                SelectedElement.Resizing = false;
                FileSaved = false;
            }

            TemplateEditMode = TemplateEditMode.EditElement;
            InterceptPanel.StopDraw();
        }


        private void Element_DoubleClick(object sender, EventArgs e)
        {
            if (!PropertiesForm.Visible)
            {
                SwitchPropertiesFormVisible(true);

                NotifySelected(sender as ITemplateElement);
            }
        }

        private void Element_LongPress(object sender, MouseEventArgs e)
        {
            InterceptPanel.Invoke(new Action(() =>
            {
                SelectedElement = sender as ITemplateElement;
                SelectedElementMousePosition = e.Location;
                TemplateEditMode = TemplateEditMode.MoveElement;

                var drawPoint = new Point(SelectedElement.Location.X - SelectedElementMousePosition.X, SelectedElement.Location.Y - SelectedElementMousePosition.Y);

                InterceptPanel.SetDrawSquare(drawPoint, SelectedElement.Size);
            }));
        }

        private void Element_ResizeStart(object sender, EventArgs e)
        {
            InterceptPanel.Invoke(new Action(() =>
            {
                SelectedElement = sender as ITemplateElement;
                TemplateEditMode = TemplateEditMode.ResizeElement;
                InterceptPanel.SetDrawSquare(SelectedElement.Location, SelectedElement.Size);
            }));
        }

        private void DeleteElement()
        {
            var element = SelectedElement as Control;
            if (element != null)
            {
                PdfSourcePanel.Controls.Remove(element);
                FileSaved = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteElement();
        }

        private void BringElementFront()
        {
            if (SelectedElement != null)
            {
                FileSaved = false;
                PdfSourcePanel.Controls.SetChildIndex(SelectedElement as Control, 0);
            }
        }
        private void BringElementToBack()
        {
            if (SelectedElement != null)
            {
                FileSaved = false;
                PdfSourcePanel.Controls.SetChildIndex(SelectedElement as Control, PdfSourcePanel.Controls.Count - 1);
            }
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringElementFront();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringElementToBack();
        }
        #endregion

        #region Template Part
        public PdfTemplatePropertiesAbriged TemplateProperties { get; set;}
        public Dictionary<string, string> TemplateKeyDatas { get; set; } = new Dictionary<string, string>();

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeEmpty();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }



        #region Save
        private bool Save()
        {
            string name;
            string path;

            if (TemplateProperties == null || !TemplateProperties.IsSaved)
            {
                var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
                if (!Directory.Exists(templatesPath)) Directory.CreateDirectory(templatesPath);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Template json files (*.tmjson)|*.tmjson";
                    saveFileDialog.FileName = "defaultTemplate.tmjson";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.InitialDirectory = templatesPath;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        name = Path.GetFileName(saveFileDialog.FileName);
                        path = Path.GetDirectoryName(saveFileDialog.FileName);
                    }
                    else return false;

                }

                TemplateProperties = TemplateLibrary.CreateNewTemplate(name);
                TemplateProperties.RootFolderPath = path;
                TemplateProperties.PageSize = Constants.TemplateSizes.A4;

                TemplateProperties.TemplateWithElements = new PdfTemplateProperties();
                TemplateProperties.TemplateWithElements.GetDataFromAbriged(TemplateProperties);
            }
            else
            {
                name = TemplateProperties.Name;
                path = TemplateProperties.RootFolderPath;
            }

            if (TemplateProperties.PageSize == SizeF.Empty)
            {
                TemplateProperties.PageSize = Constants.TemplateSizes.A4;
            }

            try
            {
                PopulateTemplateElements(TemplateProperties.TemplateWithElements);
                //UpdateImageElementPaths(TemplateProperties);
                SaveTemplateFile(TemplateProperties);
                UpdateImageElementPaths(TemplateProperties);
            }
            catch (Exception ex)
            {
                return false;
            }

            FileSaved = true;
            return true;
        }

        private void UpdateImageElementPaths(PdfTemplatePropertiesAbriged templateObject)
        {
            var templateFolderPath = templateObject.RootFolderPath;
            //string destImageFolderPath = Path.Combine(templateFolderPath, Constants.ImagesDirectory);

            foreach (var element in templateObject.TemplateWithElements.Elements)
            {
                if (element is ITemplateImage el && el.Parent != null && el.Parent is ImageElement imEl)
                {
                    imEl.ImagePath = Path.Combine(templateFolderPath, el.ImagePath);
                }
            }
        }

        private void PopulateTemplateElements(PdfTemplateProperties templateProperties)
        {
            templateProperties.Elements.Clear();
            foreach (var control in PdfSourcePanel.Controls)
            {
                var templateItem = control as ITemplateElement;
                var data = templateItem.GetSaveData();
                data.PopulateFromElement(templateItem);

                templateProperties.Elements.Add(data);
            }
        }
        
        private void UpdateImages(PdfTemplatePropertiesAbriged templateObject, string newTemplateDirectory)
        {
            //move stuff to templates directory

            var templateFolderPath = templateObject.RootFolderPath;
            //update image elements
            //should contain image paths to template`s Images directory
            List<string> imagePathsOld = templateObject.ImagesPathOld;

            List<string> imagePathsNew = new List<string>();
            foreach (var element in templateObject.TemplateWithElements.Elements)
            {
                if (element is ITemplateImage el)
                {
                    imagePathsNew.Add(el.ImagePath);
                }
            }
            //delete old images
            var imagePathNotExist = imagePathsOld.Where(i => !imagePathsNew.Contains(i));
            foreach (var imagePath in imagePathNotExist)
            {
                if (File.Exists(imagePath)) File.Delete(imagePath);
            }

            //move images
            string destImageFolderPath = Path.Combine(newTemplateDirectory, Constants.ImagesDirectory);
            if (!Directory.Exists(destImageFolderPath)) Directory.CreateDirectory(destImageFolderPath);

            imagePathsOld.Clear();
            foreach (var element in templateObject.TemplateWithElements.Elements)
            {
                if (element is ITemplateImage el)
                {
                    var imagepath = el.ImagePath;
                    if (!Path.IsPathRooted(imagepath))
                        imagepath = Path.Combine(templateFolderPath, imagepath);
                    if (!string.IsNullOrEmpty(imagepath) && !imagepath.StartsWith(destImageFolderPath))
                    {
                        var destFilePath = Path.Combine(destImageFolderPath, Guid.NewGuid() + Path.GetExtension(imagepath));

                        File.Copy(imagepath, destFilePath, true);

                        templateObject.ImagesPathOld.Add(destFilePath);
                        el.ImagePath = destFilePath.Remove(0, newTemplateDirectory.Length).Trim('\\', '/');
                    }
                    else
                    if(imagepath.StartsWith(destImageFolderPath))
                    {
                        el.ImagePath = imagepath.Remove(0, newTemplateDirectory.Length).Trim('\\', '/');
                    }
                }
            }
        }

        private void SaveTemplateFile(PdfTemplatePropertiesAbriged templateObject)
        {
            var templateFolderPath = templateObject.RootFolderPath;
            var templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            var templatesFile = Path.ChangeExtension(Path.Combine(templateDirectory, TemplateProperties.Name), ".tmjson");

            if (!Directory.Exists(templateDirectory))
            {
                Directory.CreateDirectory(templateDirectory);
            }
            
            if (!File.Exists(templatesFile))
            {
                File.Create(templatesFile);
            }

            UpdateImages(templateObject, templateDirectory);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string serializedData = JsonConvert.SerializeObject(templateObject.TemplateWithElements, settings);
            File.WriteAllText(templatesFile, serializedData, System.Text.Encoding.Unicode);

            templateObject.RootFolderPath = templateDirectory;
            templateObject.IsSaved = true;
        }
        #endregion
        
        private void OpenFile(string file)
        {
            string data = File.ReadAllText(file, System.Text.Encoding.Unicode);

            var settingsIgnore = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            var templateFolderPath = Path.GetDirectoryName(file);


            PdfTemplatePropertiesAbriged serializedData = null;
            try
            {
                serializedData = JsonConvert.DeserializeObject<PdfTemplatePropertiesAbriged>(data, settingsIgnore);
            }
            catch (Exception)
            {
                serializedData = TemplateLibrary.CreateNewTemplate(Path.GetFileName(file));
            }

            try
            {
                serializedData.TemplateWithElements = JsonConvert.DeserializeObject<PdfTemplateProperties>(data, settings);
            }
            catch (Exception)
            {
                serializedData.TemplateWithElements = new PdfTemplateProperties();
            }

            serializedData.TemplateWithElements.GetDataFromAbriged(serializedData);



            serializedData.RootFolderPath = templateFolderPath;
            serializedData.IsSaved = true;
            FileSaved = true;

            PdfSourcePanel.Controls.Clear();
            serializedData.TemplateWithElements.Elements.Reverse();
            foreach (var element in serializedData.TemplateWithElements.Elements)
            {
                var control = element.GetRawElement();
                InitElement(control);

                element.PopulateToElement(control);
                //PdfSourcePanel.Controls.Add(control as Control);

                if (control is ImageElement el)
                {
                    el.ImagePath = Path.Combine(templateFolderPath, el.ImagePath);

                    serializedData.ImagesPathOld.Add(el.ImagePath);
                    if (File.Exists(el.ImagePath))
                    {
                        using (Stream stream = File.OpenRead(el.ImagePath))
                        {
                            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                            el.Image = image;
                        }
                    }

                }
            }

            TemplateProperties = serializedData;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileCLick();
        }

        private void OpenFileCLick()
        {
            var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            if (!Directory.Exists(templatesPath)) Directory.CreateDirectory(templatesPath);

            string file;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Template json files (*.tmjson)|*.tmjson";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.InitialDirectory = templatesPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = openFileDialog.FileName;
                }
                else return;
            }

            OpenFile(file);
        }

        private void PrintFileClick()
        {
            string file;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.OverwritePrompt = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = saveFileDialog.FileName;
                }
                else return;
            }
            /*
            To set the size and position of elements in iTextSharp using millimeters, you need to convert millimeters to points because iTextSharp uses points as the measurement unit. The conversion factor is that 1 millimeter equals 2.83465 points.
            */

            if (TemplateProperties == null)
            {
                TemplateProperties = TemplateLibrary.CreateNewTemplate(string.Empty);
            }
            if (!TemplateProperties.IsSaved)
            {
                PopulateTemplateElements(TemplateProperties.TemplateWithElements);
            }

            PdfUtils.PrintPdf(TemplateProperties.TemplateWithElements, file, TemplateKeyDatas);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintFileClick();
        }

        
        #endregion

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(TemplateProperties == null)
            {
                TemplateProperties = TemplateLibrary.CreateNewTemplate(string.Empty);
            }

            TemplateSettingsForm settingsForm = new TemplateSettingsForm();
            settingsForm.SetSettingsData(TemplateProperties.Settings);

            settingsForm.ShowDialog();

            TemplateProperties.TemplateWithElements.Settings = TemplateProperties.Settings;
        }

        private void TemplateEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FileSaved)
            {
                var message = "Do you want to save changes?";
                var title = "Save Changes";

                DialogResult result = MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (!Save())
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case DialogResult.No:
                        FileSaved = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }

            PropertiesForm.SetClose = true;
            PropertiesForm.Close();

            RegexTestForm.SetClose = true;
            RegexTestForm.Close();

            if (FileSaved) DialogResult = DialogResult.OK;
        }
    }
}
