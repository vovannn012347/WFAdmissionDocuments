using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.DocumentSets;

namespace WFAdmissionDocuments.Templates
{
    public partial class InputKeyPropertiesForm : Form
    {
        enum ElementColumnIndex
        {
            BtnUp = 0,
            BtnDown = 1,
            KeyName,
            KeyNameLocations,
            PresetValue,
            InputType,
            AdditionalData,
            ExcelCheckbox
        }


        public Dictionary<string, KeyVariablesNameData> TemplateKeyDatas { get; set; } = new Dictionary<string, KeyVariablesNameData>();
        public Dictionary<string, string> TemplateKeys { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, PdfTemplatePropertiesAbriged> Elements { get; set; } = new Dictionary<string, PdfTemplatePropertiesAbriged>();
        public Dictionary<string, KeyVariablesInputProperties> TemplateKeyInputProps { get; set; } = new Dictionary<string, KeyVariablesInputProperties>();
        public List<string> KeysOrdered { get; set; } = new List<string>();
        public List<string> ExcelKeys { get; set; } = new List<string>();

        public Graphics measureGrapics;

        //private Label label;

        public InputKeyPropertiesForm()
        {
            InitializeComponent();
            measureGrapics = this.CreateGraphics();
            this.FormClosed += TemplateKeyedVariablesForm_FormClosed;
            this.FormClosing += TemplateKeyedVariablesForm_FormClosing;
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

        private void TemplateKeyedVariablesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            measureGrapics.Dispose();
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
            keyElementsTableLayout.ColumnCount = (int)ElementColumnIndex.ExcelCheckbox + 1;
            keyElementsTableLayout.RowCount = TemplateKeyDatas.Keys.Count + 2;
            keyElementsTableLayout.RowStyles.Clear();

            keyElementsTableLayout.SuspendLayout();

            SetupHeaderLabel(GetElement<Label>(), "⇧", (int)ElementColumnIndex.BtnUp);
            SetupHeaderLabel(GetElement<Label>(), "⇩", (int)ElementColumnIndex.BtnDown);
            SetupHeaderLabel(GetElement<Label>(), "Keys", (int)ElementColumnIndex.KeyName);
            SetupHeaderLabel(GetElement<Label>(), "Names and locations", (int)ElementColumnIndex.KeyNameLocations);
            SetupHeaderLabel(GetElement<Label>(), "Pre-set value", (int)ElementColumnIndex.PresetValue);
            SetupHeaderLabel(GetElement<Label>(), "Input type", (int)ElementColumnIndex.InputType);
            SetupHeaderLabel(GetElement<Label>(), "⚙️", (int)ElementColumnIndex.AdditionalData);

            this.keyElementsTableLayout.Controls.Add(new PictureBox()
            {
                Dock = DockStyle.Top,
                Image = WFAdmissionDocuments.Properties.Resources.excel48,
                MaximumSize = new Size(24, 24),
                MinimumSize = new Size(24, 24),
                SizeMode = PictureBoxSizeMode.StretchImage
            }, (int)ElementColumnIndex.ExcelCheckbox, 0);
            keyElementsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());

            //sync keys Ordered content

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
            Size minSize = new Size(23, 23);
            int i = 1;
            foreach (var key in KeysOrdered)
            {
                keyElementsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());

                #region Move Key Buttons
                //additional data values

                Button btnUp = GetElement<Button>();
                btnUp.Tag = key;
                btnUp.Text = "⇧";
                //btnUp.MinimumSize = minSize;
                btnUp.MaximumSize = minSize;
                btnUp.Dock = DockStyle.Top;
                btnUp.TextAlign = ContentAlignment.MiddleCenter;
                btnUp.Click += MoveKeyUp_Click;
                AddEventRemovalCleanFunc(btnUp, () =>
                {
                    btnUp.Click -= MoveKeyUp_Click;
                });
                this.keyElementsTableLayout.Controls.Add(btnUp, (int)ElementColumnIndex.BtnUp, i);

                Button btnDown = GetElement<Button>();
                btnDown.Tag = key;
                btnDown.Text = "⇩";
                //btnDown.MinimumSize = minSize;
                btnDown.MaximumSize = minSize;
                btnDown.Dock = DockStyle.Top;
                btnDown.TextAlign = ContentAlignment.MiddleCenter;
                btnDown.Click += MoveKeyDown_Click;
                //btnDown.Dock = DockStyle.Top;
                AddEventRemovalCleanFunc(btnDown, () =>
                {
                    btnDown.Click -= MoveKeyDown_Click;
                });
                this.keyElementsTableLayout.Controls.Add(btnDown, (int)ElementColumnIndex.BtnDown, i);
                #endregion

                #region Keys
                var keyData = TemplateKeyDatas[key];

                var keyValue = string.Empty;
                TemplateKeys.TryGetValue(key, out keyValue);
                 
                var keyLabel = GetElement<Label>();
                keyLabel.Text = key;
                keyLabel.Dock = DockStyle.Top;
                keyLabel.AutoSize = true;
                keyLabel.Padding = new Padding(0, 0, 0, 5);

                string keySourceString = key;
                foreach (var keySource in keyData.KeySources)
                {
                    if (Elements.ContainsKey(keySource))
                    {
                        var el = Elements[keySource];
                        var name = !string.IsNullOrEmpty(el.Settings.VisibleName) ? el.Settings.VisibleName : el.Name;
                        keySourceString += "\n >" + name;
                    }
                    else
                    {
                        keySourceString += "\n >" + keySource;
                    }
                }
                keyLabel.Text = keySourceString;

                this.keyElementsTableLayout.Controls.Add(keyLabel, (int)ElementColumnIndex.KeyName, i);
                #endregion

                #region Name
                var friendlyNameLabel = GetElement<Label>();
                friendlyNameLabel.Dock = DockStyle.Top;
                friendlyNameLabel.AutoSize = true;
                friendlyNameLabel.Padding = new Padding(0, 0, 0, 5);

                var friendlyNameStringBuilder = new StringBuilder();
                for (int j = 0; j < keyData.FriendlyNames.Count; ++j)
                {
                    friendlyNameStringBuilder.Append(keyData.FriendlyNames[j]);
                    foreach (var nameSource in keyData.FriendlyNameSources[j])
                    {
                        if (Elements.ContainsKey(nameSource))
                        {
                            var el = Elements[nameSource];
                            var name = !string.IsNullOrEmpty(el.Settings.VisibleName) ? el.Settings.VisibleName : el.Name;
                            friendlyNameStringBuilder.Append("\n >" + name);
                        }
                        else
                        {
                            friendlyNameStringBuilder.Append("\n >" + nameSource);
                        }
                    }
                    friendlyNameStringBuilder.Append("\n");

                }
                friendlyNameLabel.Text = friendlyNameStringBuilder.ToString();

                this.keyElementsTableLayout.Controls.Add(friendlyNameLabel, (int)ElementColumnIndex.KeyNameLocations, i);
                #endregion

                #region Preset Values
                //pre-set values
                var textBox = GetElement<TextBox>();
                textBox.Tag = key;
                textBox.Dock = DockStyle.Top;
                textBox.Multiline = true;
                textBox.TextChanged += MultilineTextBox_TextChanged;
                AddEventRemovalCleanFunc(textBox, () =>
                {
                    textBox.TextChanged -= MultilineTextBox_TextChanged;
                });

                if (TemplateKeys.TryGetValue(key, out var keyTextValue))
                {
                    textBox.Text = keyTextValue;
                }
                adjustHeightAfterLayout.Add(textBox);

                this.keyElementsTableLayout.Controls.Add(textBox, (int)ElementColumnIndex.PresetValue, i);
                #endregion

                #region Input Type
                //value entrance type combobox
                
                ComboBox combobox = GetElement<ComboBox>();
                combobox.Tag = key;
                combobox.DropDownStyle = ComboBoxStyle.DropDownList;
                combobox.Dock = DockStyle.Top;

                var comboboxValues = (Enum.GetValues(typeof(VariableInputType)) as VariableInputType[]);
                combobox.DataSource = comboboxValues;

                AddEventRemovalCleanFunc(combobox, () =>
                {
                    combobox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                });

                this.keyElementsTableLayout.Controls.Add(combobox, (int)ElementColumnIndex.InputType, i);

                combobox.BeginInvoke(new Action(() => { 
                    if (TemplateKeyInputProps.TryGetValue(key, out var inputProps))
                    {
                        combobox.Invoke(new Action(() =>
                        {
                            combobox.SelectedIndex = Array.IndexOf(comboboxValues, inputProps.InputType);
                        }));
                    }
                    else
                    {
                        combobox.Invoke(new Action(() =>
                        {
                            combobox.SelectedIndex = 0;
                        }));
                    }

                    combobox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                }));

                #endregion

                #region additional data values
                //additional data values
                Button OpenAdditionalValuesBtn = GetElement<Button>();
                OpenAdditionalValuesBtn.Tag = key;
                OpenAdditionalValuesBtn.Text = "⚙️";
                OpenAdditionalValuesBtn.Dock = DockStyle.Top;
                //OpenAdditionalValuesBtn.MinimumSize = minSize;
                OpenAdditionalValuesBtn.MaximumSize = minSize;
                OpenAdditionalValuesBtn.TextAlign = ContentAlignment.MiddleCenter;
                OpenAdditionalValuesBtn.Click += OpenAdditionalValuesBtn_Click;
                AddEventRemovalCleanFunc(OpenAdditionalValuesBtn, () =>
                {
                    OpenAdditionalValuesBtn.Click -= OpenAdditionalValuesBtn_Click;
                });

                this.keyElementsTableLayout.Controls.Add(OpenAdditionalValuesBtn, (int)ElementColumnIndex.AdditionalData, i);
                #endregion

                #region excel checkboxes
                //additional data values
                CheckBox excelCheckBox = GetElement<CheckBox>();

                excelCheckBox.Tag = key;
                //excelCheckBox.MinimumSize = minSize;
                excelCheckBox.Checked = ExcelKeys.Contains(key);

                this.keyElementsTableLayout.Controls.Add(excelCheckBox, (int)ElementColumnIndex.ExcelCheckbox, i);
                #endregion

                ++i;
            }

            this.keyElementsTableLayout.Controls.Add(new Label(), 2, i);

            keyElementsTableLayout.ResumeLayout();

            foreach(var textbox in adjustHeightAfterLayout)
            {
                AdjustTextBoxHeight(textbox);
            }
        }

        private void MoveKeyDown_Click(object sender, EventArgs e)
        {
            var position = keyElementsTableLayout.GetRow(sender as Control);
            if ((position + 1) > KeysOrdered.Count) return;

            SwapRows(keyElementsTableLayout, position, position + 1);

            SwapKeyOrders(position - 1, position);
        }

        private void MoveKeyUp_Click(object sender, EventArgs e)
        {
            var position = keyElementsTableLayout.GetRow(sender as Control);
            if ((position - 1) < 1) return;

            SwapRows(keyElementsTableLayout, position - 1, position);

            SwapKeyOrders(position - 2, position - 1);
        }
        private void SwapKeyOrders(int keyindex1, int keyindex2)
        {
            var prevKey = KeysOrdered[keyindex1];
            KeysOrdered[keyindex1] = KeysOrdered[keyindex2];
            KeysOrdered[keyindex2] = prevKey;
        }

        private void SwapRows(TableLayoutPanel tableLayoutPanel, int row1, int row2)
        {
            if (row1 >= tableLayoutPanel.RowCount || row2 >= tableLayoutPanel.RowCount || row1 < 0 || row2 < 0 || row1 == row2)
            {
                throw new ArgumentException("Invalid row indices");
            }

            // Temporary storage for controls in the rows to be swapped
            Control[] row1Controls = new Control[tableLayoutPanel.ColumnCount];
            Control[] row2Controls = new Control[tableLayoutPanel.ColumnCount];

            // Store controls from row1
            for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
            {
                row1Controls[col] = tableLayoutPanel.GetControlFromPosition(col, row1);
            }

            // Store controls from row2
            for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
            {
                row2Controls[col] = tableLayoutPanel.GetControlFromPosition(col, row2);
            }

            tableLayoutPanel.SuspendLayout();
            // Swap controls
            for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
            {
                tableLayoutPanel.SetCellPosition(row1Controls[col], new TableLayoutPanelCellPosition(col, row2));
                tableLayoutPanel.SetCellPosition(row2Controls[col], new TableLayoutPanelCellPosition(col, row1));
            }
            tableLayoutPanel.ResumeLayout();

            // Refresh the layout
            tableLayoutPanel.PerformLayout();
        }

        private void OpenAdditionalValuesBtn_Click(object sender, EventArgs e)
        {
            Button sBtn = sender as Button;
            string key = sBtn.Tag as string;

            LaunchAdditionalKeyVariableForm(key);
        }

        private void LaunchAdditionalKeyVariableForm(string key)
        {
            KeyVariablesInputProperties props;
            if (!TemplateKeyInputProps.ContainsKey(key))
                {
                props = new KeyVariablesInputProperties()
                {
                    InputDatas = new string[0] { }
                };
                props.InputType = VariableInputType.TextBox;
                TemplateKeyInputProps[key] = props;
            }
            else
            {
                props = TemplateKeyInputProps[key];
            }

            using (InputKeyPropertiesAdditionalDataForm additionalDataForm = new InputKeyPropertiesAdditionalDataForm())
            {
                additionalDataForm.KeyAdditionalDatas = props.InputDatas.ToList();

                if(additionalDataForm.ShowDialog() == DialogResult.OK)
                {
                    props.InputDatas = additionalDataForm.KeyAdditionalDatas.ToArray();
                }
            }
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox sBox = sender as ComboBox;
            string key = sBox.Tag as string;

            if (sBox.SelectedItem is VariableInputType selectedType)
            {
                if (!TemplateKeyInputProps.ContainsKey(key))
                {
                    var props = new KeyVariablesInputProperties()
                    {
                        InputDatas = new string[0] { }
                    };
                    props.InputType = selectedType;
                    TemplateKeyInputProps[key] = props;
                }
                else
                {
                    var props = TemplateKeyInputProps[key];
                    props.InputType = selectedType;
                }

                //update 
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            TemplateKeys.Clear();
            ExcelKeys.Clear();

            for (int i = 1; i < keyElementsTableLayout.RowCount; ++i)
            {
                if(this.keyElementsTableLayout.GetControlFromPosition((int)ElementColumnIndex.PresetValue, i) is TextBox textbox)
                {
                    TemplateKeys[textbox.Tag as string] = textbox.Text;
                }

                if (this.keyElementsTableLayout.GetControlFromPosition((int)ElementColumnIndex.ExcelCheckbox, i) is CheckBox checkBox)
                {
                    if (checkBox.Checked)
                        ExcelKeys.Add(checkBox.Tag as string);
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void KeyedVariablesDataForm_Resize(object sender, EventArgs e)
        {

        }
    }
}
