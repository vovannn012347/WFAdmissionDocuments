using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.DocumentSets
{
    public partial class SelectTemplatesForm : Form
    {
        public class CustomItem
        {
            public string DisplayText { get; set; }
            public string Tag { get; set; }

            public CustomItem(string displayText, string tag)
            {
                DisplayText = displayText;
                Tag = tag;
            }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        public List<string> PreSelectedElements { get; set; }

        public Dictionary<string, PdfTemplatePropertiesAbriged> SelectionValues { get; set; }

        public List<string> SelectedElements { get; set; } = new List<string>();


        public SelectTemplatesForm()
        {
            InitializeComponent();
        }

        private void SelectTemplatesForm_Load(object sender, EventArgs e)
        {
            checkedListBoxElements.Items.Clear();
            foreach (var value in SelectionValues.Values)
            {
                if (value != null && !PreSelectedElements.Contains(value.Name))
                {
                    var name = string.IsNullOrWhiteSpace(value.Settings.VisibleName) ? value.Name : value.Settings.VisibleName;

                    checkedListBoxElements.Items.Add(new CustomItem(name, value.Name));
                }
            }
            LocalizeForm();
        }
        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.SelectTemplatesForm_Title;

            buttonOk.Text = Resources.Ok_Text;
        }

        private void checkedListBoxElements_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //CustomItem item = checkedListBoxElements.Items[e.Index] as CustomItem;
            //SelectedElement = item.Tag;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            foreach(CustomItem it in checkedListBoxElements.CheckedItems)
            {
                SelectedElements.Add(it.Tag);
            }
            DialogResult = DialogResult.OK;

        }
    }
}
