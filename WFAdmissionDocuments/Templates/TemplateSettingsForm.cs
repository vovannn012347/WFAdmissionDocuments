using System.Threading;
using System.Windows.Forms;
using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments
{
    public partial class TemplateSettingsForm : Form
    {
        SettingsData _data;

        public TemplateSettingsForm()
        {
            InitializeComponent();
        }

        public void SetSettingsData(SettingsData data)
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

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void textBoxExcelPath_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void ExportToExcelFileCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void TemplateSettingsForm_Load(object sender, System.EventArgs e)
        {
            LocalizeForm();
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.TemplateSettingsForm_Title;// = "Template Settings"

            this.ExportToExcelFileCheckBox.Text = Resources.ExportToExcelFile_Text;// = "Export To Excel"

            this.labelExcelPath.Text = Resources.ExcelPath_Text;// = "Excel Path"
            this.buttonSave.Text = Resources.Save_Text;// = "Save"
            this.labelTemplateName.Text = Resources.TemplateName_Text;// = "Template Name"
            this.PrePrintedPageCheckbox.Text = Resources.TemplateSettingsForm_PrePrintedPage_Text;// = "Produce preprinted pages (overriden by document set setting)"
            this.DisplayBorderBoxesCheckBox.Text = Resources.TemplateSettingsForm_DisplayBorderBoxes_Text;// = "Display border boxes for keyed text (overriden by document set setting)"
            this.EmbedFontsCheckBox.Text = Resources.TemplateSettingsForm_EmbedFonts_Text;// "Embed fonts into document (overriden by document set setting)"
        }
    }
}
