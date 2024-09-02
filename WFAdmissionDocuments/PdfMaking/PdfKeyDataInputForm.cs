﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;

namespace WFAdmissionDocuments.Templates
{
    public partial class PdfKeyDataInputForm : Form
    {
        public Dictionary<string, KeyVariablesNameData> TemplateKeyDatas { get; set; } = new Dictionary<string, KeyVariablesNameData>();

        public Dictionary<string, string> TemplateKeys { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, KeyVariablesInputProperties> TemplateKeyProps { get; set; } = new Dictionary<string, KeyVariablesInputProperties>();
        public List<string> KeysOrdered { get; set; } = new List<string>();

        public Graphics measureGrapics;


        public PdfKeyDataInputForm()
        {
            InitializeComponent();
            measureGrapics = this.CreateGraphics();
            this.FormClosed += TemplateKeyedVariablesForm_FormClosed;
            this.FormClosing += TemplateKeyedVariablesForm_FormClosing;
        }

        private void TemplateKeyedVariablesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            measureGrapics.Dispose();
        }
        private void TemplateKeyedVariablesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyElementsTableLayout.SuspendLayout();
            for (int i = this.keyElementsTableLayout.Controls.Count - 1; i >= 0; --i)
            {
                var control = this.keyElementsTableLayout.Controls[i];

                if (control is PictureBox)
                {
                    continue;
                }

                FormElementsCacheSingleton.Instance.ReturnElement(control);
                this.Controls.Remove(control);
                this.keyElementsTableLayout.Controls.RemoveAt(i);
            }
        }

        private void SetupHeaderLabel(Label label, string text, int column)
        {
            label.Text = text;
            label.AutoSize = true;
            label.Dock = DockStyle.Top;
            this.keyElementsTableLayout.Controls.Add(label, column, 0);
        }
        T GetElement<T>() where T : Control
        {
            return FormElementsCacheSingleton.Instance.GetElement<T>();
        }

        private void AddEventRemovalCleanFunc<T>(T cleanedObject, Action action) where T : Control
        {
            FormElementsCacheSingleton.Instance.AddCleanupDelegate<T>(cleanedObject, action);
        }

        private void TemplateKeyedVariablesForm_Load(object sender, EventArgs e)
        {
            keyElementsTableLayout.ColumnCount = 2;
            keyElementsTableLayout.RowCount = TemplateKeyDatas.Keys.Count + 1;
            keyElementsTableLayout.RowStyles.Clear();

            keyElementsTableLayout.SuspendLayout();

            SetupHeaderLabel(GetElement<Label>(), "Keys", 0);
            SetupHeaderLabel(GetElement<Label>(), "Values", 1);

            keyElementsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());


            for (int i_k = KeysOrdered.Count - 1; i_k >= 0; --i_k)
            {
                var key = KeysOrdered[i_k];
                if (!TemplateKeyDatas.ContainsKey(key))
                {
                    KeysOrdered.RemoveAt(i_k);
                }
            }

            foreach (var key in TemplateKeyDatas.Keys.OrderBy(item => item))
            {
                if (!KeysOrdered.Contains(key)) KeysOrdered.Add(key);
            }

            List<TextBox> adjustHeightAfterLayout = new List<TextBox>();
            int i = 1;
            foreach (var key in KeysOrdered)
            {
                string keyName = key;
                if(TemplateKeyDatas.TryGetValue(key, out var value))
                {
                    keyName = value.FriendlyNames.FirstOrDefault() ?? key;
                }

                var label = GetElement<Label>();
                label.Text = keyName;
                label.Dock = DockStyle.Top;
                label.TextAlign = ContentAlignment.TopLeft;
                label.AutoSize = true;
                label.Padding = new Padding(0, 0, 0, 5);

                this.keyElementsTableLayout.Controls.Add(label, 0, i);

                if(TemplateKeyProps.TryGetValue(key, out var keyProps))
                {
                    if(keyProps.InputType == VariableInputType.TextBox)
                    {
                        adjustHeightAfterLayout.Add(AppendTextBox(key, i));
                    }
                    else
                    if(keyProps.InputType == VariableInputType.ComboBox)
                    {
                        AppendComboBox(key, i, keyProps);
                    }
                }
                else
                {
                    adjustHeightAfterLayout.Add(AppendTextBox(key, i));
                }

                keyElementsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());

                ++i;
            }

            keyElementsTableLayout.ResumeLayout();

            foreach (var textbox in adjustHeightAfterLayout)
            {
                AdjustTextBoxHeight(textbox);
            }
        }

        private void AppendComboBox(string key, int i, KeyVariablesInputProperties keyProps)
        {
            var comboBox = GetElement<ComboBox>();
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox.Tag = key;
            comboBox.Dock = DockStyle.Top;
            comboBox.AutoSize = true;
            comboBox.Padding = new Padding(0, 0, 0, 5);

            this.keyElementsTableLayout.Controls.Add(comboBox, 1, i);
            comboBox.DataSource = keyProps.InputDatas;

            if (TemplateKeys.TryGetValue(key, out var keyTextValue))
            {
                comboBox.Text = keyTextValue;
            }
        }

        private TextBox AppendTextBox(string key, int i)
        {
            var textBox = GetElement<TextBox>();
            textBox.Tag = key;
            textBox.Dock = DockStyle.Top;
            textBox.AutoSize = true;
            textBox.Multiline = true;
            textBox.Padding = new Padding(0, 0, 0, 5);

            this.keyElementsTableLayout.Controls.Add(textBox, 1, i);
            textBox.TextChanged += MultilineTextBox_TextChanged;

            AddEventRemovalCleanFunc(textBox, () =>
            {
                textBox.TextChanged -= MultilineTextBox_TextChanged;
            });

            if (TemplateKeys.TryGetValue(key, out var keyTextValue))
            {
                textBox.Text = keyTextValue;
            }

            return textBox;
        }

        private void MultilineTextBox_TextChanged(object sender, EventArgs e)
        {
            AdjustTextBoxHeight(sender as TextBox);
        }

        private void AdjustTextBoxHeight(TextBox textBox)
        {
            if (textBox == null) return;
            string measuredText = "test";
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                measuredText = textBox.Text;
            }
            if (measuredText.TrimEnd(' ').EndsWith("\n")) measuredText += "t";

            SizeF size = measureGrapics.MeasureString(measuredText, textBox.Font, textBox.Width);
            int newHeight = (int)Math.Ceiling(size.Height) + textBox.Margin.Vertical;

            if (textBox.Height != newHeight)
            {
                textBox.Height = newHeight;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for(int i = 1; i < keyElementsTableLayout.RowCount; ++i)
            {
                var control = this.keyElementsTableLayout.GetControlFromPosition(1, i) as Control;
                TemplateKeys[control.Tag as string] = control.Text;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
