namespace WFAdmissionDocuments
{
    partial class DocumentSetSettingsForm
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxTemplateName = new System.Windows.Forms.TextBox();
            this.labelDocumentSetName = new System.Windows.Forms.Label();
            this.labelExcelPath = new System.Windows.Forms.Label();
            this.excelPathSelectButton = new System.Windows.Forms.Button();
            this.textBoxExcelPath = new System.Windows.Forms.TextBox();
            this.ExportToExcelFileCheckBox = new System.Windows.Forms.CheckBox();
            this.PrePrintedPageCheckbox = new System.Windows.Forms.CheckBox();
            this.DisplayBorderBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.EmbedFontsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            // labelDocumentSetName
            // 
            this.labelDocumentSetName.AutoSize = true;
            this.labelDocumentSetName.Location = new System.Drawing.Point(12, 9);
            this.labelDocumentSetName.Name = "labelDocumentSetName";
            this.labelDocumentSetName.Size = new System.Drawing.Size(106, 13);
            this.labelDocumentSetName.TabIndex = 6;
            this.labelDocumentSetName.Text = "Document Set Name";
            // 
            // labelExcelPath
            // 
            this.labelExcelPath.AutoSize = true;
            this.labelExcelPath.Location = new System.Drawing.Point(12, 76);
            this.labelExcelPath.Name = "labelExcelPath";
            this.labelExcelPath.Size = new System.Drawing.Size(58, 13);
            this.labelExcelPath.TabIndex = 10;
            this.labelExcelPath.Text = "Excel Path";
            // 
            // excelPathSelectButton
            // 
            this.excelPathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.excelPathSelectButton.Location = new System.Drawing.Point(566, 92);
            this.excelPathSelectButton.Name = "excelPathSelectButton";
            this.excelPathSelectButton.Size = new System.Drawing.Size(53, 20);
            this.excelPathSelectButton.TabIndex = 9;
            this.excelPathSelectButton.Text = ">>";
            this.excelPathSelectButton.UseVisualStyleBackColor = true;
            this.excelPathSelectButton.Click += new System.EventHandler(this.excelPathSelectButton_Click);
            // 
            // textBoxExcelPath
            // 
            this.textBoxExcelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExcelPath.Enabled = false;
            this.textBoxExcelPath.Location = new System.Drawing.Point(11, 92);
            this.textBoxExcelPath.Name = "textBoxExcelPath";
            this.textBoxExcelPath.Size = new System.Drawing.Size(548, 20);
            this.textBoxExcelPath.TabIndex = 8;
            // 
            // ExportToExcelFileCheckBox
            // 
            this.ExportToExcelFileCheckBox.AutoSize = true;
            this.ExportToExcelFileCheckBox.Location = new System.Drawing.Point(12, 52);
            this.ExportToExcelFileCheckBox.Name = "ExportToExcelFileCheckBox";
            this.ExportToExcelFileCheckBox.Size = new System.Drawing.Size(101, 17);
            this.ExportToExcelFileCheckBox.TabIndex = 7;
            this.ExportToExcelFileCheckBox.Text = "Export To Excel";
            this.ExportToExcelFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // PrePrintedPageCheckbox
            // 
            this.PrePrintedPageCheckbox.AutoSize = true;
            this.PrePrintedPageCheckbox.Location = new System.Drawing.Point(11, 118);
            this.PrePrintedPageCheckbox.Name = "PrePrintedPageCheckbox";
            this.PrePrintedPageCheckbox.Size = new System.Drawing.Size(148, 17);
            this.PrePrintedPageCheckbox.TabIndex = 11;
            this.PrePrintedPageCheckbox.Text = "Produce preprinted pages";
            this.PrePrintedPageCheckbox.UseVisualStyleBackColor = true;
            // 
            // DisplayBorderBoxesCheckBox
            // 
            this.DisplayBorderBoxesCheckBox.AutoSize = true;
            this.DisplayBorderBoxesCheckBox.Location = new System.Drawing.Point(11, 141);
            this.DisplayBorderBoxesCheckBox.Name = "DisplayBorderBoxesCheckBox";
            this.DisplayBorderBoxesCheckBox.Size = new System.Drawing.Size(191, 17);
            this.DisplayBorderBoxesCheckBox.TabIndex = 14;
            this.DisplayBorderBoxesCheckBox.Text = "Display border boxes for keyed text";
            this.DisplayBorderBoxesCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmbedFontsCheckBox
            // 
            this.EmbedFontsCheckBox.AutoSize = true;
            this.EmbedFontsCheckBox.Location = new System.Drawing.Point(11, 164);
            this.EmbedFontsCheckBox.Name = "EmbedFontsCheckBox";
            this.EmbedFontsCheckBox.Size = new System.Drawing.Size(155, 17);
            this.EmbedFontsCheckBox.TabIndex = 15;
            this.EmbedFontsCheckBox.Text = "Embed fonts into document";
            this.EmbedFontsCheckBox.UseVisualStyleBackColor = true;
            // 
            // DocumentSetSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 213);
            this.Controls.Add(this.EmbedFontsCheckBox);
            this.Controls.Add(this.DisplayBorderBoxesCheckBox);
            this.Controls.Add(this.PrePrintedPageCheckbox);
            this.Controls.Add(this.labelExcelPath);
            this.Controls.Add(this.excelPathSelectButton);
            this.Controls.Add(this.textBoxExcelPath);
            this.Controls.Add(this.ExportToExcelFileCheckBox);
            this.Controls.Add(this.labelDocumentSetName);
            this.Controls.Add(this.textBoxTemplateName);
            this.Controls.Add(this.buttonSave);
            this.Name = "DocumentSetSettingsForm";
            this.Text = "Document Set Settings";
            this.Load += new System.EventHandler(this.DocumentSetSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxTemplateName;
        private System.Windows.Forms.Label labelDocumentSetName;
        private System.Windows.Forms.Label labelExcelPath;
        private System.Windows.Forms.Button excelPathSelectButton;
        private System.Windows.Forms.TextBox textBoxExcelPath;
        private System.Windows.Forms.CheckBox ExportToExcelFileCheckBox;
        private System.Windows.Forms.CheckBox PrePrintedPageCheckbox;
        private System.Windows.Forms.CheckBox DisplayBorderBoxesCheckBox;
        private System.Windows.Forms.CheckBox EmbedFontsCheckBox;
    }
}

