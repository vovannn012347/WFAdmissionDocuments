using System.Threading;
using System.Windows.Forms;
using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.DocumentSet.Code;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments
{
    public partial class DocumentSetSettingsForm : Form
    {
        PdfDocumentSetSettingsData _data;

        public DocumentSetSettingsForm()
        {
            InitializeComponent();
        }

        public void SetSettingsData(PdfDocumentSetSettingsData data)
        {
            _data = data;

            ExportToExcelFileCheckBox.Checked = _data.SaveToExcel;
            textBoxExcelPath.Text = _data.ExcelFileSaveLocation;
            textBoxTemplateName.Text = _data.VisibleName;
            PrePrintedPageCheckbox.Checked = _data.PrePrintedPage;
            DisplayBorderBoxesCheckBox.Checked = _data.DisplayBorders;
            EmbedFontsCheckBox.Checked = _data.EmbedFonts;
        }

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            _data.SaveToExcel = ExportToExcelFileCheckBox.Checked;
            _data.ExcelFileSaveLocation = textBoxExcelPath.Text;
            _data.VisibleName = textBoxTemplateName.Text;
            _data.PrePrintedPage = PrePrintedPageCheckbox.Checked;
            _data.DisplayBorders = DisplayBorderBoxesCheckBox.Checked;
            _data.EmbedFonts = EmbedFontsCheckBox.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void excelPathSelectButton_Click(object sender, System.EventArgs e)
        {
            string file;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxExcelPath.Text = ExcelUtils.GetRelativePath(saveFileDialog.FileName);
                    file = saveFileDialog.FileName;
                }
                else return;
            }
        }

        private void DocumentSetSettingsForm_Load(object sender, System.EventArgs e)
        {
            LocalizeForm();
        }
        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.DocumentSetSettingsForm_Title;// = "Template Settings"

            this.ExportToExcelFileCheckBox.Text = Resources.ExportToExcelFile_Text;// = "Export To Excel"

            this.labelDocumentSetName.Text = Resources.DocumentSetName_Text;// = "Document Set Name"
            this.labelExcelPath.Text = Resources.ExcelPath_Text;// = "Excel Path"
            this.buttonSave.Text = Resources.Save_Text;// = "Save"

            this.PrePrintedPageCheckbox.Text = Resources.DocumentSetSettingsForm_PrePrintedPage_Text;// = "Produce preprinted pages"
            this.DisplayBorderBoxesCheckBox.Text = Resources.DocumentSetSettingsForm_DisplayBorderBoxes_Text;// = "Display border boxes for keyed text"
            this.EmbedFontsCheckBox.Text = Resources.DocumentSetSettingsForm_EmbedFonts_Text;// "Embed fonts into document"
        }
    }
}
