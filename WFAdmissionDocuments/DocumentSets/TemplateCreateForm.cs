using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using UnidecodeSharpFork;
using WFAdmissionDocuments.Code;

using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.DocumentSets
{
    public partial class TemplateCreateForm : Form
    {
        public string FileNameAddGuid = Guid.NewGuid().ToString();
        public bool NewFileName = true;

        public string FileName { get; set; }
        public string FriendlyName { get; set; }

        public TemplateCreateForm()
        {
            InitializeComponent();
        }

        private void TemplateCreateForm_Load(object sender, EventArgs e)
        {
            textBoxFileName.Text = FileName;

            textBoxFriendlyName.Text = FriendlyName;

            if (!NewFileName)
            {
                textBoxFileName.Enabled = false;
            }

            textBoxFriendlyName.Focus();
            LocalizeForm();
        }
        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.TemplateCreateForm_Title;

            labelTemplateName.Text = Resources.TemplateName_Text;
            labelFileName.Text = Resources.FileName_Text;

            buttonOk.Text = Resources.Ok_Text;
        }

        private void textBoxFriendlyName_TextChanged(object sender, EventArgs e)
        {
            if (NewFileName)
            {
                string unidecoded = (textBoxFriendlyName.Text.Trim() + " ").Replace(' ', '_').Unidecode();

                textBoxFileName.Text = unidecoded + FileNameAddGuid;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileName = textBoxFileName.Text;
            FriendlyName = textBoxFriendlyName.Text;

            if (string.IsNullOrWhiteSpace(FileName))
            {
                MessageBox.Show("File name cannnot be empty");
                return;
            }

            var templatesDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);

            //fix ukrainian stuff
            FileName = FileName.Unidecode();

            //remove invalid chars
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            FileName = string.Concat(FileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            //limit length
            int maxLength = 100 - FileNameAddGuid.Length - Constants.TemplatesExtension.Length;
            FileName = FileName.Length <= maxLength ? FileName : FileName.Substring(0, maxLength);

            //zet extension
            FileName = Path.ChangeExtension(FileName, Constants.TemplatesExtension);

            var fileNamePath = Path.Combine(templatesDirectory, FileName);

            if (File.Exists(fileNamePath))
            {
                DialogResult mb = MessageBox.Show("File exists, overwrite?", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (mb != DialogResult.OK) return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
