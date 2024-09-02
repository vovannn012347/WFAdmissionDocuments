using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using Newtonsoft.Json;

using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.Templates
{
    public partial class RegexTesterForm : Form
    {
        public bool SetClose { get; set; }
        private List<string> RegexHistory = new List<string>();
        private Match matches;

        public RegexTesterForm(Form parent)
        {
            Owner = parent;

            InitializeComponent();
        }

        private void RegexTesterForm_Load(object sender, EventArgs e)
        {
            LoadRegex();
            LocalizeForm();
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.RegexTesterForm_Title;

            labelText.Text = Resources.RegexTesterForm_Text_Text;
            labelRegularExpression.Text = Resources.RegexTesterForm_RegularExpression_Text;
            labelCaptureGroup.Text = Resources.RegexTesterForm_CaptureGroup_Text;
            labelResult.Text = Resources.RegexTesterForm_Result_Text;
            labelRegexHistory.Text = Resources.RegexTesterForm_RegexHistory_Text;

            buttonRunRegex.Text = Resources.RegexTesterForm_RunRegex_Text;
            buttonRegexHelp.Text = Resources.RegexTesterForm_RegexHelp_Text;
        }

        private void LoadRegex()
        {
            if (File.Exists(Constants.RegexHistoryFile))
            {
                string regexHistoryFileData = File.ReadAllText(Constants.RegexHistoryFile);

                try
                {
                    RegexHistory = JsonConvert.DeserializeObject<List<string>>(regexHistoryFileData);
                }
                catch (Exception ex)
                {

                }
            }

            if (Constants.RegexHistoryCount > 1)
                while (RegexHistory.Count > Constants.RegexHistoryCount)
                {
                    RegexHistory.RemoveAt(RegexHistory.Count - 1);
                }
        }

        private void SaveRegex()
        {
            var regexHistoryString = JsonConvert.SerializeObject(RegexHistory);
            File.WriteAllText(Constants.RegexHistoryFile, regexHistoryString);
        }

        private void captureGroupNumber_ValueChanged(object sender, EventArgs e)
        {
            if (matches != null && matches.Success)
            {
                if (matches.Groups.Count <= captureGroupNumber.Value)
                {
                    textBoxResult.Text = matches.Groups[(int)captureGroupNumber.Value - 1].Value;
                }
                else
                {
                    textBoxResult.Text = string.Empty;
                }
            }
        }

        private void buttonRunRegex_Click(object sender, EventArgs e)
        {
            var regex = new Regex(textBoxRegex.Text);

            matches = null;
            try
            {
                matches = regex.Match(textBoxText.Text);
            }
            catch(Exception ex)
            {
                textBoxResult.Text = ex.ToString();
                return;
            }

            if(matches != null && matches.Success)
            {
                int regexIndex = RegexHistory.IndexOf(textBoxRegex.Text);
                if (regexIndex > 0)
                {
                    RegexHistory.RemoveAt(regexIndex);
                    regexHistoryListBox.Items.RemoveAt(regexIndex);
                }

                if (regexIndex != 0)
                {
                    RegexHistory.Insert(0, textBoxRegex.Text);
                    regexHistoryListBox.Items.Insert(0, textBoxRegex.Text);
                }

                if (captureGroupNumber.Value <= matches.Groups.Count)
                {
                    textBoxResult.Text = matches.Groups[(int)captureGroupNumber.Value - 1].Value;
                }
                else
                {
                    textBoxResult.Text = string.Empty;
                }

                if (Constants.RegexHistoryCount > 1)
                    while (RegexHistory.Count > Constants.RegexHistoryCount)
                    {
                        regexHistoryListBox.Items.RemoveAt(RegexHistory.Count - 1);
                        RegexHistory.RemoveAt(RegexHistory.Count - 1);
                    }
            }
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            matches = null;
            textBoxResult.Text = string.Empty;
        }

        private void regexHistoryListBox_DoubleClick(object sender, EventArgs e)
        {
            var selectedIndex = regexHistoryListBox.SelectedIndex;
            if (selectedIndex < RegexHistory.Count)
            {
                textBoxRegex.Text = RegexHistory[selectedIndex];
            }
        }

        private void RegexTesterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SetClose)
            {
                e.Cancel = true;
                this.Hide();
            }

            SaveRegex();
        }
    }
}
