namespace WFAdmissionDocuments
{
    partial class TemplateSettingsForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ExportToExcelFileCheckBox = new System.Windows.Forms.CheckBox();
            this.textBoxExcelPath = new System.Windows.Forms.TextBox();
            this.excelPathSelectButton = new System.Windows.Forms.Button();
            this.labelExcelPath = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxTemplateName = new System.Windows.Forms.TextBox();
            this.labelTemplateName = new System.Windows.Forms.Label();
            this.PrePrintedPageCheckbox = new System.Windows.Forms.CheckBox();
            this.DisplayBorderBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.EmbedFontsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ExportToExcelFileCheckBox
            // 
            this.ExportToExcelFileCheckBox.AutoSize = true;
            this.ExportToExcelFileCheckBox.Location = new System.Drawing.Point(12, 52);
            this.ExportToExcelFileCheckBox.Name = "ExportToExcelFileCheckBox";
            this.ExportToExcelFileCheckBox.Size = new System.Drawing.Size(101, 17);
            this.ExportToExcelFileCheckBox.TabIndex = 0;
            this.ExportToExcelFileCheckBox.Text = "Export To Excel";
            this.ExportToExcelFileCheckBox.UseVisualStyleBackColor = true;
            this.ExportToExcelFileCheckBox.CheckedChanged += new System.EventHandler(this.ExportToExcelFileCheckBox_CheckedChanged);
            // 
            // textBoxExcelPath
            // 
            this.textBoxExcelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExcelPath.Enabled = false;
            this.textBoxExcelPath.Location = new System.Drawing.Point(11, 92);
            this.textBoxExcelPath.Name = "textBoxExcelPath";
            this.textBoxExcelPath.Size = new System.Drawing.Size(548, 20);
            this.textBoxExcelPath.TabIndex = 1;
            this.textBoxExcelPath.TextChanged += new System.EventHandler(this.textBoxExcelPath_TextChanged);
            // 
            // excelPathSelectButton
            // 
            this.excelPathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.excelPathSelectButton.Location = new System.Drawing.Point(566, 92);
            this.excelPathSelectButton.Name = "excelPathSelectButton";
            this.excelPathSelectButton.Size = new System.Drawing.Size(53, 20);
            this.excelPathSelectButton.TabIndex = 2;
            this.excelPathSelectButton.Text = ">>";
            this.excelPathSelectButton.UseVisualStyleBackColor = true;
            this.excelPathSelectButton.Click += new System.EventHandler(this.excelPathSelectButton_Click);
            // 
            // labelExcelPath
            // 
            this.labelExcelPath.AutoSize = true;
            this.labelExcelPath.Location = new System.Drawing.Point(12, 76);
            this.labelExcelPath.Name = "labelExcelPath";
            this.labelExcelPath.Size = new System.Drawing.Size(58, 13);
            this.labelExcelPath.TabIndex = 3;
            this.labelExcelPath.Text = "Excel Path";
            this.labelExcelPath.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(545, 178);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxTemplateName
            // 
            this.textBoxTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateName.Location = new System.Drawing.Point(11, 26);
            this.textBoxTemplateName.Name = "textBoxTemplateName";
            this.textBoxTemplateName.Size = new System.Drawing.Size(608, 20);
            this.textBoxTemplateName.TabIndex = 5;
            // 
            // labelTemplateName
            // 
            this.labelTemplateName.AutoSize = true;
            this.labelTemplateName.Location = new System.Drawing.Point(12, 9);
            this.labelTemplateName.Name = "labelTemplateName";
            this.labelTemplateName.Size = new System.Drawing.Size(82, 13);
            this.labelTemplateName.TabIndex = 6;
            this.labelTemplateName.Text = "Template Name";
            // 
            // PrePrintedPageCheckbox
            // 
            this.PrePrintedPageCheckbox.AutoSize = true;
            this.PrePrintedPageCheckbox.Location = new System.Drawing.Point(11, 118);
            this.PrePrintedPageCheckbox.Name = "PrePrintedPageCheckbox";
            this.PrePrintedPageCheckbox.Size = new System.Drawing.Size(316, 17);
            this.PrePrintedPageCheckbox.TabIndex = 12;
            this.PrePrintedPageCheckbox.Text = "Produce preprinted pages (overriden by document set setting)";
            this.PrePrintedPageCheckbox.UseVisualStyleBackColor = true;
            // 
            // DisplayBorderBoxesCheckBox
            // 
            this.DisplayBorderBoxesCheckBox.AutoSize = true;
            this.DisplayBorderBoxesCheckBox.Location = new System.Drawing.Point(11, 141);
            this.DisplayBorderBoxesCheckBox.Name = "DisplayBorderBoxesCheckBox";
            this.DisplayBorderBoxesCheckBox.Size = new System.Drawing.Size(359, 17);
            this.DisplayBorderBoxesCheckBox.TabIndex = 13;
            this.DisplayBorderBoxesCheckBox.Text = "Display border boxes for keyed text (overriden by document set setting)";
            this.DisplayBorderBoxesCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmbedFontsCheckBox
            // 
            this.EmbedFontsCheckBox.AutoSize = true;
            this.EmbedFontsCheckBox.Location = new System.Drawing.Point(11, 164);
            this.EmbedFontsCheckBox.Name = "EmbedFontsCheckBox";
            this.EmbedFontsCheckBox.Size = new System.Drawing.Size(323, 17);
            this.EmbedFontsCheckBox.TabIndex = 14;
            this.EmbedFontsCheckBox.Text = "Embed fonts into document (overriden by document set setting)";
            this.EmbedFontsCheckBox.UseVisualStyleBackColor = true;
            // 
            // TemplateSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 213);
            this.Controls.Add(this.EmbedFontsCheckBox);
            this.Controls.Add(this.DisplayBorderBoxesCheckBox);
            this.Controls.Add(this.PrePrintedPageCheckbox);
            this.Controls.Add(this.labelTemplateName);
            this.Controls.Add(this.textBoxTemplateName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelExcelPath);
            this.Controls.Add(this.excelPathSelectButton);
            this.Controls.Add(this.textBoxExcelPath);
            this.Controls.Add(this.ExportToExcelFileCheckBox);
            this.Name = "TemplateSettingsForm";
            this.Text = "Template Settings";
            this.Load += new System.EventHandler(this.TemplateSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ExportToExcelFileCheckBox;
        private System.Windows.Forms.TextBox textBoxExcelPath;
        private System.Windows.Forms.Button excelPathSelectButton;
        private System.Windows.Forms.Label labelExcelPath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxTemplateName;
        private System.Windows.Forms.Label labelTemplateName;
        private System.Windows.Forms.CheckBox PrePrintedPageCheckbox;
        private System.Windows.Forms.CheckBox DisplayBorderBoxesCheckBox;
        private System.Windows.Forms.CheckBox EmbedFontsCheckBox;
    }
}

