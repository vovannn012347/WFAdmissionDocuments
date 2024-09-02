namespace WFAdmissionDocuments.Templates
{
    partial class RegexTesterForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxRegex = new System.Windows.Forms.TextBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.regexHistoryListBox = new System.Windows.Forms.ListBox();
            this.labelRegularExpression = new System.Windows.Forms.Label();
            this.labelCaptureGroup = new System.Windows.Forms.Label();
            this.captureGroupNumber = new System.Windows.Forms.NumericUpDown();
            this.labelText = new System.Windows.Forms.Label();
            this.buttonRunRegex = new System.Windows.Forms.Button();
            this.labelRegexHistory = new System.Windows.Forms.Label();
            this.buttonRegexHelp = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.labelResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.captureGroupNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(424, 0);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBoxRegex
            // 
            this.textBoxRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegex.Location = new System.Drawing.Point(12, 90);
            this.textBoxRegex.Name = "textBoxRegex";
            this.textBoxRegex.Size = new System.Drawing.Size(400, 20);
            this.textBoxRegex.TabIndex = 1;
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(12, 25);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(400, 46);
            this.textBoxText.TabIndex = 2;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // regexHistoryListBox
            // 
            this.regexHistoryListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regexHistoryListBox.FormattingEnabled = true;
            this.regexHistoryListBox.Location = new System.Drawing.Point(12, 236);
            this.regexHistoryListBox.Name = "regexHistoryListBox";
            this.regexHistoryListBox.Size = new System.Drawing.Size(400, 108);
            this.regexHistoryListBox.TabIndex = 4;
            this.regexHistoryListBox.DoubleClick += new System.EventHandler(this.regexHistoryListBox_DoubleClick);
            // 
            // labelRegularExpression
            // 
            this.labelRegularExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRegularExpression.AutoSize = true;
            this.labelRegularExpression.Location = new System.Drawing.Point(12, 74);
            this.labelRegularExpression.Name = "labelRegularExpression";
            this.labelRegularExpression.Size = new System.Drawing.Size(236, 13);
            this.labelRegularExpression.TabIndex = 5;
            this.labelRegularExpression.Text = "Regular expression (capture groups are required)";
            // 
            // labelCaptureGroup
            // 
            this.labelCaptureGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCaptureGroup.AutoSize = true;
            this.labelCaptureGroup.Location = new System.Drawing.Point(12, 113);
            this.labelCaptureGroup.Name = "labelCaptureGroup";
            this.labelCaptureGroup.Size = new System.Drawing.Size(74, 13);
            this.labelCaptureGroup.TabIndex = 6;
            this.labelCaptureGroup.Text = "Capture group";
            // 
            // captureGroupNumber
            // 
            this.captureGroupNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.captureGroupNumber.Location = new System.Drawing.Point(12, 129);
            this.captureGroupNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.captureGroupNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.captureGroupNumber.Name = "captureGroupNumber";
            this.captureGroupNumber.Size = new System.Drawing.Size(97, 20);
            this.captureGroupNumber.TabIndex = 7;
            this.captureGroupNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.captureGroupNumber.ValueChanged += new System.EventHandler(this.captureGroupNumber_ValueChanged);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(12, 9);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(48, 13);
            this.labelText.TabIndex = 8;
            this.labelText.Text = "Text test";
            // 
            // buttonRunRegex
            // 
            this.buttonRunRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRunRegex.Location = new System.Drawing.Point(115, 126);
            this.buttonRunRegex.Name = "buttonRunRegex";
            this.buttonRunRegex.Size = new System.Drawing.Size(75, 23);
            this.buttonRunRegex.TabIndex = 9;
            this.buttonRunRegex.Text = "Run regex";
            this.buttonRunRegex.UseVisualStyleBackColor = true;
            this.buttonRunRegex.Click += new System.EventHandler(this.buttonRunRegex_Click);
            // 
            // labelRegexHistory
            // 
            this.labelRegexHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRegexHistory.AutoSize = true;
            this.labelRegexHistory.Location = new System.Drawing.Point(12, 220);
            this.labelRegexHistory.Name = "labelRegexHistory";
            this.labelRegexHistory.Size = new System.Drawing.Size(71, 13);
            this.labelRegexHistory.TabIndex = 10;
            this.labelRegexHistory.Text = "Regex history";
            // 
            // buttonRegexHelp
            // 
            this.buttonRegexHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRegexHelp.Location = new System.Drawing.Point(337, 126);
            this.buttonRegexHelp.Name = "buttonRegexHelp";
            this.buttonRegexHelp.Size = new System.Drawing.Size(75, 23);
            this.buttonRegexHelp.TabIndex = 11;
            this.buttonRegexHelp.Text = "Regex help";
            this.buttonRegexHelp.UseVisualStyleBackColor = true;
            this.buttonRegexHelp.Visible = false;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResult.Location = new System.Drawing.Point(12, 168);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(400, 49);
            this.textBoxResult.TabIndex = 12;
            // 
            // labelResult
            // 
            this.labelResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 152);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(37, 13);
            this.labelResult.TabIndex = 13;
            this.labelResult.Text = "Result";
            // 
            // RegexTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 356);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonRegexHelp);
            this.Controls.Add(this.labelRegexHistory);
            this.Controls.Add(this.buttonRunRegex);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.captureGroupNumber);
            this.Controls.Add(this.labelCaptureGroup);
            this.Controls.Add(this.labelRegularExpression);
            this.Controls.Add(this.regexHistoryListBox);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.textBoxRegex);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 320);
            this.Name = "RegexTesterForm";
            this.Text = "Regex Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegexTesterForm_FormClosing);
            this.Load += new System.EventHandler(this.RegexTesterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.captureGroupNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxRegex;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.ListBox regexHistoryListBox;
        private System.Windows.Forms.Label labelRegularExpression;
        private System.Windows.Forms.Label labelCaptureGroup;
        private System.Windows.Forms.NumericUpDown captureGroupNumber;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button buttonRunRegex;
        private System.Windows.Forms.Label labelRegexHistory;
        private System.Windows.Forms.Button buttonRegexHelp;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label labelResult;
    }
}