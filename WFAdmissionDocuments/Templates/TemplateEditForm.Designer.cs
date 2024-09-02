namespace WFAdmissionDocuments
{
    partial class TemplateEditForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bringToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemplateFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regExTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palleteGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPalletePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PImageRadioButton = new System.Windows.Forms.CheckBox();
            this.PTextRadioButton = new System.Windows.Forms.CheckBox();
            this.PTextTemplateRadioButton = new System.Windows.Forms.CheckBox();
            this.PTextMultilineRadioButton = new System.Windows.Forms.CheckBox();
            this.PTextRegexRadioButton = new System.Windows.Forms.CheckBox();
            this.PdfContainerPanel = new System.Windows.Forms.Panel();
            this.InterceptPanel = new WFAdmissionDocuments.Templates.TemplateElements.TransparentPanelElement();
            this.PdfSourcePanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.palleteGroupBox.SuspendLayout();
            this.flowLayoutPalletePanel.SuspendLayout();
            this.PdfContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItemEdit,
            this.settingsToolStripMenuItem,
            this.stashToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(519, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.printToolStripMenuItem.Tag = "";
            this.printToolStripMenuItem.Text = "To pdf";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.bringToFrontToolStripMenuItem,
            this.sendToBackToolStripMenuItem});
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItemEdit.Text = "Edit";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // bringToFrontToolStripMenuItem
            // 
            this.bringToFrontToolStripMenuItem.Name = "bringToFrontToolStripMenuItem";
            this.bringToFrontToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + ↑";
            this.bringToFrontToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.bringToFrontToolStripMenuItem.Text = "Bring To Front";
            this.bringToFrontToolStripMenuItem.Click += new System.EventHandler(this.bringToFrontToolStripMenuItem_Click);
            // 
            // sendToBackToolStripMenuItem
            // 
            this.sendToBackToolStripMenuItem.Name = "sendToBackToolStripMenuItem";
            this.sendToBackToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + ↓";
            this.sendToBackToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.sendToBackToolStripMenuItem.Text = "Send To Back";
            this.sendToBackToolStripMenuItem.Click += new System.EventHandler(this.sendToBackToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // stashToolStripMenuItem
            // 
            this.stashToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.openTemplateFileToolStripMenuItem});
            this.stashToolStripMenuItem.Name = "stashToolStripMenuItem";
            this.stashToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.stashToolStripMenuItem.Text = "Stash";
            this.stashToolStripMenuItem.Visible = false;
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // openTemplateFileToolStripMenuItem
            // 
            this.openTemplateFileToolStripMenuItem.Name = "openTemplateFileToolStripMenuItem";
            this.openTemplateFileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.openTemplateFileToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openTemplateFileToolStripMenuItem.Text = "Open Template File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.palleteToolStripMenuItem,
            this.regExTestToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeyDisplayString = "Alt+P";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // palleteToolStripMenuItem
            // 
            this.palleteToolStripMenuItem.Name = "palleteToolStripMenuItem";
            this.palleteToolStripMenuItem.ShortcutKeyDisplayString = "Alt+L";
            this.palleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.palleteToolStripMenuItem.Text = "Pallete";
            this.palleteToolStripMenuItem.Click += new System.EventHandler(this.palleteToolStripMenuItem_Click);
            // 
            // regExTestToolStripMenuItem
            // 
            this.regExTestToolStripMenuItem.Name = "regExTestToolStripMenuItem";
            this.regExTestToolStripMenuItem.ShortcutKeyDisplayString = "Alt+R";
            this.regExTestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.regExTestToolStripMenuItem.Text = "RegExTest";
            this.regExTestToolStripMenuItem.Click += new System.EventHandler(this.reExTestToolStripMenuItem_Click);
            // 
            // palleteGroupBox
            // 
            this.palleteGroupBox.Controls.Add(this.flowLayoutPalletePanel);
            this.palleteGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.palleteGroupBox.Location = new System.Drawing.Point(0, 335);
            this.palleteGroupBox.Name = "palleteGroupBox";
            this.palleteGroupBox.Size = new System.Drawing.Size(519, 47);
            this.palleteGroupBox.TabIndex = 1;
            this.palleteGroupBox.TabStop = false;
            this.palleteGroupBox.Text = "Pallete";
            this.palleteGroupBox.Visible = false;
            // 
            // flowLayoutPalletePanel
            // 
            this.flowLayoutPalletePanel.AutoScroll = true;
            this.flowLayoutPalletePanel.Controls.Add(this.PImageRadioButton);
            this.flowLayoutPalletePanel.Controls.Add(this.PTextRadioButton);
            this.flowLayoutPalletePanel.Controls.Add(this.PTextTemplateRadioButton);
            this.flowLayoutPalletePanel.Controls.Add(this.PTextMultilineRadioButton);
            this.flowLayoutPalletePanel.Controls.Add(this.PTextRegexRadioButton);
            this.flowLayoutPalletePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPalletePanel.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPalletePanel.Name = "flowLayoutPalletePanel";
            this.flowLayoutPalletePanel.Size = new System.Drawing.Size(513, 28);
            this.flowLayoutPalletePanel.TabIndex = 4;
            // 
            // PImageRadioButton
            // 
            this.PImageRadioButton.AutoSize = true;
            this.PImageRadioButton.Location = new System.Drawing.Point(3, 3);
            this.PImageRadioButton.Name = "PImageRadioButton";
            this.PImageRadioButton.Size = new System.Drawing.Size(55, 17);
            this.PImageRadioButton.TabIndex = 0;
            this.PImageRadioButton.Text = "Image";
            this.PImageRadioButton.UseVisualStyleBackColor = true;
            this.PImageRadioButton.CheckedChanged += new System.EventHandler(this.PImageRadioButton_CheckedChanged);
            // 
            // PTextRadioButton
            // 
            this.PTextRadioButton.AutoSize = true;
            this.PTextRadioButton.Location = new System.Drawing.Point(64, 3);
            this.PTextRadioButton.Name = "PTextRadioButton";
            this.PTextRadioButton.Size = new System.Drawing.Size(47, 17);
            this.PTextRadioButton.TabIndex = 1;
            this.PTextRadioButton.Text = "Text";
            this.PTextRadioButton.UseVisualStyleBackColor = true;
            this.PTextRadioButton.CheckedChanged += new System.EventHandler(this.PTextRadioButton_CheckedChanged);
            // 
            // PTextTemplateRadioButton
            // 
            this.PTextTemplateRadioButton.AutoSize = true;
            this.PTextTemplateRadioButton.Location = new System.Drawing.Point(117, 3);
            this.PTextTemplateRadioButton.Name = "PTextTemplateRadioButton";
            this.PTextTemplateRadioButton.Size = new System.Drawing.Size(74, 17);
            this.PTextTemplateRadioButton.TabIndex = 2;
            this.PTextTemplateRadioButton.Text = "Text Input";
            this.PTextTemplateRadioButton.UseVisualStyleBackColor = true;
            this.PTextTemplateRadioButton.CheckedChanged += new System.EventHandler(this.PTextTemplateRadioButton_CheckedChanged);
            // 
            // PTextMultilineRadioButton
            // 
            this.PTextMultilineRadioButton.AutoSize = true;
            this.PTextMultilineRadioButton.Location = new System.Drawing.Point(197, 3);
            this.PTextMultilineRadioButton.Name = "PTextMultilineRadioButton";
            this.PTextMultilineRadioButton.Size = new System.Drawing.Size(113, 17);
            this.PTextMultilineRadioButton.TabIndex = 3;
            this.PTextMultilineRadioButton.Text = "Text input multiline";
            this.PTextMultilineRadioButton.UseVisualStyleBackColor = true;
            this.PTextMultilineRadioButton.CheckedChanged += new System.EventHandler(this.PTextMultilineRadioButton_CheckedChanged);
            // 
            // PTextRegexRadioButton
            // 
            this.PTextRegexRadioButton.AutoSize = true;
            this.PTextRegexRadioButton.Location = new System.Drawing.Point(316, 3);
            this.PTextRegexRadioButton.Name = "PTextRegexRadioButton";
            this.PTextRegexRadioButton.Size = new System.Drawing.Size(111, 17);
            this.PTextRegexRadioButton.TabIndex = 4;
            this.PTextRegexRadioButton.Text = "Text input(RegEx)";
            this.PTextRegexRadioButton.UseVisualStyleBackColor = true;
            this.PTextRegexRadioButton.CheckedChanged += new System.EventHandler(this.PTextRegexRadioButton_CheckedChanged);
            // 
            // PdfContainerPanel
            // 
            this.PdfContainerPanel.AutoScroll = true;
            this.PdfContainerPanel.Controls.Add(this.InterceptPanel);
            this.PdfContainerPanel.Controls.Add(this.PdfSourcePanel);
            this.PdfContainerPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.PdfContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PdfContainerPanel.Location = new System.Drawing.Point(0, 24);
            this.PdfContainerPanel.Name = "PdfContainerPanel";
            this.PdfContainerPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 25);
            this.PdfContainerPanel.Size = new System.Drawing.Size(519, 311);
            this.PdfContainerPanel.TabIndex = 2;
            // 
            // InterceptPanel
            // 
            this.InterceptPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.InterceptPanel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.InterceptPanel.Location = new System.Drawing.Point(0, 0);
            this.InterceptPanel.Name = "InterceptPanel";
            this.InterceptPanel.Size = new System.Drawing.Size(200, 100);
            this.InterceptPanel.TabIndex = 0;
            this.InterceptPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InterceptPanel_MouseClick);
            this.InterceptPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InterceptPanel_MouseDown);
            this.InterceptPanel.MouseLeave += new System.EventHandler(this.InterceptPanel_MouseLeave);
            this.InterceptPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InterceptPanel_MouseMove);
            this.InterceptPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InterceptPanel_MouseUp);
            // 
            // PdfSourcePanel
            // 
            this.PdfSourcePanel.BackColor = System.Drawing.Color.White;
            this.PdfSourcePanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.PdfSourcePanel.Location = new System.Drawing.Point(0, 0);
            this.PdfSourcePanel.Margin = new System.Windows.Forms.Padding(0);
            this.PdfSourcePanel.Name = "PdfSourcePanel";
            this.PdfSourcePanel.Size = new System.Drawing.Size(448, 266);
            this.PdfSourcePanel.TabIndex = 0;
            // 
            // TemplateEditForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 382);
            this.Controls.Add(this.PdfContainerPanel);
            this.Controls.Add(this.palleteGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TemplateEditForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Edit Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplateEditForm_FormClosing);
            this.Load += new System.EventHandler(this.AddEditTemplateForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.palleteGroupBox.ResumeLayout(false);
            this.flowLayoutPalletePanel.ResumeLayout(false);
            this.flowLayoutPalletePanel.PerformLayout();
            this.PdfContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stashToolStripMenuItem;
        private System.Windows.Forms.GroupBox palleteGroupBox;
        private System.Windows.Forms.CheckBox PTextRadioButton;
        private System.Windows.Forms.CheckBox PImageRadioButton;
        private System.Windows.Forms.CheckBox PTextTemplateRadioButton;
        private System.Windows.Forms.Panel PdfContainerPanel;
        private System.Windows.Forms.Panel PdfSourcePanel;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem palleteToolStripMenuItem;
        private Templates.TemplateElements.TransparentPanelElement InterceptPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPalletePanel;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplateFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bringToFrontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToBackToolStripMenuItem;
        private System.Windows.Forms.CheckBox PTextMultilineRadioButton;
        private System.Windows.Forms.CheckBox PTextRegexRadioButton;
        private System.Windows.Forms.ToolStripMenuItem regExTestToolStripMenuItem;
    }
}