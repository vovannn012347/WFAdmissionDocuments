using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using UnidecodeSharpFork;

using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.DocumentSets
{
    public partial class DocumentSetCreateForm : Form
    {
        public string FileNameAddGuid = Guid.NewGuid().ToString();
        public string FileName { get; set; }
        public string FriendlyName { get; set; }

        public DocumentSetCreateForm()
        {
            InitializeComponent();
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

            var documentSetsDirectory = Path.Combine(Directory.GetCurrentDirectory(), Constants.DocumentSetsDirectory);

            //fix ukrainian stuff
            FileName = FileName.Unidecode();

            //remove invalid chars
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            FileName = string.Concat(FileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            //limit length
            int maxLength = 100 - FileNameAddGuid.Length - Constants.DocumentSetExtension.Length;
            FileName = FileName.Length <= maxLength ? FileName : FileName.Substring(0, maxLength);

            //zet extension
            FileName = Path.ChangeExtension(FileName, Constants.DocumentSetExtension);

            var fileNamePath = Path.ChangeExtension(Path.Combine(documentSetsDirectory, FileName), Constants.DocumentSetExtension);

            if (File.Exists(fileNamePath))
            {
                DialogResult mb = MessageBox.Show("File exists, overwrite?", "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (mb != DialogResult.OK) return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void DocumentSetCreateForm_Load(object sender, EventArgs e)
        {
            textBoxFriendlyName.Focus();
            LocalizeForm();
        }
        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.DocumentSetCreateForm_Title;

            labelDocumentSetName.Text = Resources.DocumentSetName_Text;
            labelFileName.Text = Resources.FileName_Text;

            buttonOk.Text = Resources.Ok_Text;
        }

        private void textBoxFriendlyName_TextChanged(object sender, EventArgs e)
        {
            var unidecoded = (textBoxFriendlyName.Text.Trim() + " ").Replace(' ', '_').Unidecode();

            textBoxFileName.Text = unidecoded + FileNameAddGuid;
        }
    }
}
