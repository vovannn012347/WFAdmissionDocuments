using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Newtonsoft.Json;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.DocumentSets
{
    public partial class GeneralSettingsForm : Form
    {
        class LanguageSelection
        {
            public SettingsLanguage Value { get; set; }
            public string Description { get; set; }
        }

        private String FileSave => Constants.GeneralSettingsFile;
        private GeneralSettings Settings;
        private FontConverter fontConverter = new FontConverter();

        private List<LanguageSelection> LangSource = new List<LanguageSelection>();
        public GeneralSettingsForm()
        {
            InitializeComponent();
        }

        private void GeneralSettingsForm_Load(object sender, EventArgs evt)
        {
            if (File.Exists(Constants.GeneralSettingsFile))
            {
                string fileData = File.ReadAllText(Constants.GeneralSettingsFile);

                try
                {
                    Settings = JsonConvert.DeserializeObject<GeneralSettings>(fileData);
                }
                catch (Exception ex)
                {
                    Settings = new GeneralSettings();
                }
            }
            else
            {
                Settings = new GeneralSettings();
            }

            textBoxFont.Text = fontConverter.ConvertToString(Settings.DefaultFont);
            numericUpDownBorderThickness.Value = 0 + (decimal)(Settings.PdfElementBorderWidth ?? 0.3f);
            numericUpDownRegexHistory.Value = 0 + Settings.RegexHistoryCount ?? 40;

            //comboBoxLanguage.DataSource = Enum.GetValues(typeof(SettingsLanguage)).Cast<Enum>().Select(e => new
            //{
            //    Value = e,
            //    Description = Enums.GetEnumDescription(e)
            //}).ToList();
            LocalizeForm();
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxLanguage.SelectedIndex != -1)
            {
                Settings.Language = (SettingsLanguage)comboBoxLanguage.SelectedValue;
                Constants.Settings.Language = (SettingsLanguage)comboBoxLanguage.SelectedValue;
                LocalizeForm();
            }
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.GeneralSettingsForm_Title;

            labelDefaultFont.Text = Resources.GeneralSettingsForm_DefaultFont_Text;
            labelPdfBorderThickness.Text = Resources.GeneralSettingsForm_PdfBorderThickness_Text;
            labelRegexHistoryCount.Text = Resources.GeneralSettingsForm_RegexHistoryCount_Text;
            labelLanguage.Text = Resources.GeneralSettingsForm_Language_Text;

            comboBoxLanguage.SelectedIndexChanged -= comboBoxLanguage_SelectedIndexChanged;
            LangSource = Enum.GetValues(typeof(SettingsLanguage))
                .Cast<SettingsLanguage>().Select(e => new
            LanguageSelection
            {
                Value = e,
                Description = Enums.GetEnumDescription(e)
            }).ToList();
            comboBoxLanguage.DataSource = LangSource;
            comboBoxLanguage.SelectedIndex = LangSource.FindIndex(i => i.Value == Constants.Settings.Language);

            comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;

            buttonOk.Text = Resources.Ok_Text;

        }
        private void fontSelectButton_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.DefaultFont = fontDialog.Font;
                    Settings.DefaultFontSize = Settings.DefaultFont.Size;

                    textBoxFont.Text = fontConverter.ConvertToString(Settings.DefaultFont);
                }
            }
        }

        private void numericUpDownBorderThickness_ValueChanged(object sender, EventArgs e)
        {
            Settings.PdfElementBorderWidth = (float)numericUpDownBorderThickness.Value;
        }

        private void numericUpDownRegexHistory_ValueChanged(object sender, EventArgs e)
        {
            Settings.RegexHistoryCount = (int)numericUpDownRegexHistory.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Constants.Settings = Settings;

            var dataString = JsonConvert.SerializeObject(Settings);
            File.WriteAllText(Constants.GeneralSettingsFile, dataString);

            DialogResult = DialogResult.OK;
            comboBoxLanguage.SelectedIndexChanged -= comboBoxLanguage_SelectedIndexChanged;
            this.Close();
        }

        private void GeneralSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            comboBoxLanguage.SelectedIndexChanged -= comboBoxLanguage_SelectedIndexChanged;
        }
    }
}
