namespace WFAdmissionDocuments.DocumentSets
{
    partial class GeneralSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxFont = new System.Windows.Forms.TextBox();
            this.fontSelectButton = new System.Windows.Forms.Button();
            this.labelDefaultFont = new System.Windows.Forms.Label();
            this.labelPdfBorderThickness = new System.Windows.Forms.Label();
            this.labelRegexHistoryCount = new System.Windows.Forms.Label();
            this.numericUpDownRegexHistory = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBorderThickness = new System.Windows.Forms.NumericUpDown();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegexHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(265, 167);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxFont
            // 
            this.textBoxFont.Location = new System.Drawing.Point(12, 27);
            this.textBoxFont.Name = "textBoxFont";
            this.textBoxFont.ReadOnly = true;
            this.textBoxFont.Size = new System.Drawing.Size(289, 20);
            this.textBoxFont.TabIndex = 5;
            // 
            // fontSelectButton
            // 
            this.fontSelectButton.Location = new System.Drawing.Point(307, 27);
            this.fontSelectButton.Name = "fontSelectButton";
            this.fontSelectButton.Size = new System.Drawing.Size(33, 20);
            this.fontSelectButton.TabIndex = 6;
            this.fontSelectButton.Text = "...";
            this.fontSelectButton.UseVisualStyleBackColor = true;
            this.fontSelectButton.Click += new System.EventHandler(this.fontSelectButton_Click);
            // 
            // labelDefaultFont
            // 
            this.labelDefaultFont.AutoSize = true;
            this.labelDefaultFont.Location = new System.Drawing.Point(13, 11);
            this.labelDefaultFont.Name = "labelDefaultFont";
            this.labelDefaultFont.Size = new System.Drawing.Size(65, 13);
            this.labelDefaultFont.TabIndex = 7;
            this.labelDefaultFont.Text = "Default font:";
            // 
            // labelPdfBorderThickness
            // 
            this.labelPdfBorderThickness.AutoSize = true;
            this.labelPdfBorderThickness.Location = new System.Drawing.Point(13, 50);
            this.labelPdfBorderThickness.Name = "labelPdfBorderThickness";
            this.labelPdfBorderThickness.Size = new System.Drawing.Size(124, 13);
            this.labelPdfBorderThickness.TabIndex = 8;
            this.labelPdfBorderThickness.Text = "Pdf border box thickness";
            // 
            // labelRegexHistoryCount
            // 
            this.labelRegexHistoryCount.AutoSize = true;
            this.labelRegexHistoryCount.Location = new System.Drawing.Point(13, 89);
            this.labelRegexHistoryCount.Name = "labelRegexHistoryCount";
            this.labelRegexHistoryCount.Size = new System.Drawing.Size(101, 13);
            this.labelRegexHistoryCount.TabIndex = 9;
            this.labelRegexHistoryCount.Text = "Regex history count";
            // 
            // numericUpDownRegexHistory
            // 
            this.numericUpDownRegexHistory.Location = new System.Drawing.Point(12, 106);
            this.numericUpDownRegexHistory.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownRegexHistory.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRegexHistory.Name = "numericUpDownRegexHistory";
            this.numericUpDownRegexHistory.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRegexHistory.TabIndex = 11;
            this.numericUpDownRegexHistory.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRegexHistory.ValueChanged += new System.EventHandler(this.numericUpDownRegexHistory_ValueChanged);
            // 
            // numericUpDownBorderThickness
            // 
            this.numericUpDownBorderThickness.DecimalPlaces = 1;
            this.numericUpDownBorderThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownBorderThickness.Location = new System.Drawing.Point(12, 66);
            this.numericUpDownBorderThickness.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownBorderThickness.Name = "numericUpDownBorderThickness";
            this.numericUpDownBorderThickness.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBorderThickness.TabIndex = 12;
            this.numericUpDownBorderThickness.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.numericUpDownBorderThickness.ValueChanged += new System.EventHandler(this.numericUpDownBorderThickness_ValueChanged);
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(13, 129);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(64, 13);
            this.labelLanguage.TabIndex = 13;
            this.labelLanguage.Text = "Ui language";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DisplayMember = "Description";
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(12, 145);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(179, 21);
            this.comboBoxLanguage.TabIndex = 14;
            this.comboBoxLanguage.ValueMember = "Value";
            // 
            // GeneralSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 202);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.numericUpDownBorderThickness);
            this.Controls.Add(this.numericUpDownRegexHistory);
            this.Controls.Add(this.labelRegexHistoryCount);
            this.Controls.Add(this.labelPdfBorderThickness);
            this.Controls.Add(this.labelDefaultFont);
            this.Controls.Add(this.fontSelectButton);
            this.Controls.Add(this.textBoxFont);
            this.Controls.Add(this.buttonOk);
            this.Name = "GeneralSettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "New Template Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneralSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.GeneralSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRegexHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderThickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxFont;
        private System.Windows.Forms.Button fontSelectButton;
        private System.Windows.Forms.Label labelDefaultFont;
        private System.Windows.Forms.Label labelPdfBorderThickness;
        private System.Windows.Forms.Label labelRegexHistoryCount;
        private System.Windows.Forms.NumericUpDown numericUpDownRegexHistory;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderThickness;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}