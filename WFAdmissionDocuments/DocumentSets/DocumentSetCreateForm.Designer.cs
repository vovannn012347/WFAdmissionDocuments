namespace WFAdmissionDocuments.DocumentSets
{
    partial class DocumentSetCreateForm
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.textBoxFriendlyName = new System.Windows.Forms.TextBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.labelDocumentSetName = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxFileName.Location = new System.Drawing.Point(12, 26);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(328, 20);
            this.textBoxFileName.TabIndex = 1;
            // 
            // textBoxFriendlyName
            // 
            this.textBoxFriendlyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFriendlyName.Location = new System.Drawing.Point(12, 65);
            this.textBoxFriendlyName.Name = "textBoxFriendlyName";
            this.textBoxFriendlyName.Size = new System.Drawing.Size(328, 20);
            this.textBoxFriendlyName.TabIndex = 0;
            this.textBoxFriendlyName.TextChanged += new System.EventHandler(this.textBoxFriendlyName_TextChanged);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(12, 9);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(52, 13);
            this.labelFileName.TabIndex = 2;
            this.labelFileName.Text = "File name";
            // 
            // labelDocumentSetName
            // 
            this.labelDocumentSetName.AutoSize = true;
            this.labelDocumentSetName.Location = new System.Drawing.Point(12, 49);
            this.labelDocumentSetName.Name = "labelDocumentSetName";
            this.labelDocumentSetName.Size = new System.Drawing.Size(102, 13);
            this.labelDocumentSetName.TabIndex = 3;
            this.labelDocumentSetName.Text = "Document set name";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(265, 97);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // DocumentSetCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 132);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDocumentSetName);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.textBoxFriendlyName);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "DocumentSetCreateForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "New Document Set";
            this.Load += new System.EventHandler(this.DocumentSetCreateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.TextBox textBoxFriendlyName;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label labelDocumentSetName;
        private System.Windows.Forms.Button buttonOk;
    }
}