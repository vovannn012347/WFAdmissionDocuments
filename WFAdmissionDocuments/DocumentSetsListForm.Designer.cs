using System;
using System.Windows.Forms;

namespace WFAdmissionDocuments
{
    partial class DocumentSetsListForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Узел7");
            this.documentSetTreeView = new System.Windows.Forms.TreeView();
            this.buttonNewDocumentSet = new System.Windows.Forms.Button();
            this.buttonNewPageTemplate = new System.Windows.Forms.Button();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.buttonRemoveUniversal = new System.Windows.Forms.Button();
            this.buttonItemSettingsUniversal = new System.Windows.Forms.Button();
            this.buttonEditTemplate = new System.Windows.Forms.Button();
            this.buttonAddPageDocumentSet = new System.Windows.Forms.Button();
            this.buttonToPfdUniversal = new System.Windows.Forms.Button();
            this.panelUpDown = new System.Windows.Forms.Panel();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonEditValues = new System.Windows.Forms.Button();
            this.groupBoxUniversalOps = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxTemplateOps = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxDocumentSetOps = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSettingsGeneral = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.groupBoxUniversalOps.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxTemplateOps.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBoxDocumentSetOps.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // documentSetTreeView
            // 
            this.documentSetTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentSetTreeView.HideSelection = false;
            this.documentSetTreeView.Location = new System.Drawing.Point(12, 12);
            this.documentSetTreeView.Name = "documentSetTreeView";
            treeNode1.Name = "Узел7";
            treeNode1.Text = "Узел7";
            this.documentSetTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.documentSetTreeView.Size = new System.Drawing.Size(468, 441);
            this.documentSetTreeView.TabIndex = 0;
            this.documentSetTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.documentSetTreeView_AfterSelect);
            // 
            // buttonNewDocumentSet
            // 
            this.buttonNewDocumentSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewDocumentSet.AutoSize = true;
            this.buttonNewDocumentSet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNewDocumentSet.Location = new System.Drawing.Point(3, 3);
            this.buttonNewDocumentSet.Name = "buttonNewDocumentSet";
            this.buttonNewDocumentSet.Size = new System.Drawing.Size(110, 23);
            this.buttonNewDocumentSet.TabIndex = 1;
            this.buttonNewDocumentSet.Text = "New document set";
            this.buttonNewDocumentSet.UseVisualStyleBackColor = true;
            this.buttonNewDocumentSet.Click += new System.EventHandler(this.buttonNewDocumentSet_Click);
            // 
            // buttonNewPageTemplate
            // 
            this.buttonNewPageTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewPageTemplate.AutoSize = true;
            this.buttonNewPageTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNewPageTemplate.Location = new System.Drawing.Point(3, 3);
            this.buttonNewPageTemplate.Name = "buttonNewPageTemplate";
            this.buttonNewPageTemplate.Size = new System.Drawing.Size(66, 23);
            this.buttonNewPageTemplate.TabIndex = 2;
            this.buttonNewPageTemplate.Text = "New page";
            this.buttonNewPageTemplate.UseVisualStyleBackColor = true;
            this.buttonNewPageTemplate.Click += new System.EventHandler(this.buttonNewTemplatePage_Click);
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadTemplate.AutoSize = true;
            this.buttonLoadTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLoadTemplate.Location = new System.Drawing.Point(3, 61);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(66, 23);
            this.buttonLoadTemplate.TabIndex = 3;
            this.buttonLoadTemplate.Text = "Load";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.buttonLoadPage_Click);
            // 
            // buttonRemoveUniversal
            // 
            this.buttonRemoveUniversal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveUniversal.AutoSize = true;
            this.buttonRemoveUniversal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRemoveUniversal.Location = new System.Drawing.Point(3, 3);
            this.buttonRemoveUniversal.Name = "buttonRemoveUniversal";
            this.buttonRemoveUniversal.Size = new System.Drawing.Size(76, 23);
            this.buttonRemoveUniversal.TabIndex = 5;
            this.buttonRemoveUniversal.Text = "Remove";
            this.buttonRemoveUniversal.UseVisualStyleBackColor = true;
            this.buttonRemoveUniversal.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonItemSettingsUniversal
            // 
            this.buttonItemSettingsUniversal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonItemSettingsUniversal.AutoSize = true;
            this.buttonItemSettingsUniversal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonItemSettingsUniversal.Location = new System.Drawing.Point(3, 32);
            this.buttonItemSettingsUniversal.Name = "buttonItemSettingsUniversal";
            this.buttonItemSettingsUniversal.Size = new System.Drawing.Size(76, 23);
            this.buttonItemSettingsUniversal.TabIndex = 6;
            this.buttonItemSettingsUniversal.Text = "Item settings";
            this.buttonItemSettingsUniversal.UseVisualStyleBackColor = true;
            this.buttonItemSettingsUniversal.Click += new System.EventHandler(this.ButtonSettings_Click);
            // 
            // buttonEditTemplate
            // 
            this.buttonEditTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditTemplate.AutoSize = true;
            this.buttonEditTemplate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonEditTemplate.Location = new System.Drawing.Point(3, 32);
            this.buttonEditTemplate.Name = "buttonEditTemplate";
            this.buttonEditTemplate.Size = new System.Drawing.Size(66, 23);
            this.buttonEditTemplate.TabIndex = 7;
            this.buttonEditTemplate.Text = "Edit";
            this.buttonEditTemplate.UseVisualStyleBackColor = true;
            this.buttonEditTemplate.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAddPageDocumentSet
            // 
            this.buttonAddPageDocumentSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPageDocumentSet.AutoSize = true;
            this.buttonAddPageDocumentSet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddPageDocumentSet.Location = new System.Drawing.Point(3, 61);
            this.buttonAddPageDocumentSet.Name = "buttonAddPageDocumentSet";
            this.buttonAddPageDocumentSet.Size = new System.Drawing.Size(110, 23);
            this.buttonAddPageDocumentSet.TabIndex = 8;
            this.buttonAddPageDocumentSet.Text = "Add page(s)";
            this.buttonAddPageDocumentSet.UseVisualStyleBackColor = true;
            this.buttonAddPageDocumentSet.Click += new System.EventHandler(this.buttonAddPage_Click);
            // 
            // buttonToPfdUniversal
            // 
            this.buttonToPfdUniversal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToPfdUniversal.AutoSize = true;
            this.buttonToPfdUniversal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonToPfdUniversal.Location = new System.Drawing.Point(3, 61);
            this.buttonToPfdUniversal.Name = "buttonToPfdUniversal";
            this.buttonToPfdUniversal.Size = new System.Drawing.Size(76, 23);
            this.buttonToPfdUniversal.TabIndex = 9;
            this.buttonToPfdUniversal.Text = "To pdf";
            this.buttonToPfdUniversal.UseVisualStyleBackColor = true;
            this.buttonToPfdUniversal.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // panelUpDown
            // 
            this.panelUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUpDown.AutoSize = true;
            this.panelUpDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelUpDown.Location = new System.Drawing.Point(3, 362);
            this.panelUpDown.Name = "panelUpDown";
            this.panelUpDown.Size = new System.Drawing.Size(122, 0);
            this.panelUpDown.TabIndex = 11;
            this.panelUpDown.Visible = false;
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReload.AutoSize = true;
            this.buttonReload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonReload.Location = new System.Drawing.Point(3, 415);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(122, 23);
            this.buttonReload.TabIndex = 12;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Visible = false;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonEditValues
            // 
            this.buttonEditValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditValues.AutoSize = true;
            this.buttonEditValues.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonEditValues.Location = new System.Drawing.Point(3, 32);
            this.buttonEditValues.Name = "buttonEditValues";
            this.buttonEditValues.Size = new System.Drawing.Size(110, 23);
            this.buttonEditValues.TabIndex = 13;
            this.buttonEditValues.Text = "Set template values";
            this.buttonEditValues.UseVisualStyleBackColor = true;
            this.buttonEditValues.Click += new System.EventHandler(this.EditTemplateValuesButton_Click);
            // 
            // groupBoxUniversalOps
            // 
            this.groupBoxUniversalOps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUniversalOps.AutoSize = true;
            this.groupBoxUniversalOps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxUniversalOps.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxUniversalOps.Location = new System.Drawing.Point(3, 115);
            this.groupBoxUniversalOps.Name = "groupBoxUniversalOps";
            this.groupBoxUniversalOps.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxUniversalOps.Size = new System.Drawing.Size(122, 100);
            this.groupBoxUniversalOps.TabIndex = 14;
            this.groupBoxUniversalOps.TabStop = false;
            this.groupBoxUniversalOps.Text = "Universal";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.buttonRemoveUniversal);
            this.flowLayoutPanel1.Controls.Add(this.buttonItemSettingsUniversal);
            this.flowLayoutPanel1.Controls.Add(this.buttonToPfdUniversal);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 13);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(122, 87);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // groupBoxTemplateOps
            // 
            this.groupBoxTemplateOps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTemplateOps.AutoSize = true;
            this.groupBoxTemplateOps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxTemplateOps.Controls.Add(this.flowLayoutPanel2);
            this.groupBoxTemplateOps.Location = new System.Drawing.Point(3, 221);
            this.groupBoxTemplateOps.Name = "groupBoxTemplateOps";
            this.groupBoxTemplateOps.Size = new System.Drawing.Size(122, 106);
            this.groupBoxTemplateOps.TabIndex = 15;
            this.groupBoxTemplateOps.TabStop = false;
            this.groupBoxTemplateOps.Text = "Template";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.buttonNewPageTemplate);
            this.flowLayoutPanel2.Controls.Add(this.buttonEditTemplate);
            this.flowLayoutPanel2.Controls.Add(this.buttonLoadTemplate);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(116, 87);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // groupBoxDocumentSetOps
            // 
            this.groupBoxDocumentSetOps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDocumentSetOps.AutoSize = true;
            this.groupBoxDocumentSetOps.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxDocumentSetOps.Controls.Add(this.flowLayoutPanel3);
            this.groupBoxDocumentSetOps.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDocumentSetOps.Name = "groupBoxDocumentSetOps";
            this.groupBoxDocumentSetOps.Size = new System.Drawing.Size(122, 106);
            this.groupBoxDocumentSetOps.TabIndex = 16;
            this.groupBoxDocumentSetOps.TabStop = false;
            this.groupBoxDocumentSetOps.Text = "Document set";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.buttonNewDocumentSet);
            this.flowLayoutPanel3.Controls.Add(this.buttonEditValues);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddPageDocumentSet);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(116, 87);
            this.flowLayoutPanel3.TabIndex = 17;
            // 
            // buttonSettingsGeneral
            // 
            this.buttonSettingsGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettingsGeneral.AutoSize = true;
            this.buttonSettingsGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSettingsGeneral.Location = new System.Drawing.Point(3, 333);
            this.buttonSettingsGeneral.Name = "buttonSettingsGeneral";
            this.buttonSettingsGeneral.Size = new System.Drawing.Size(122, 23);
            this.buttonSettingsGeneral.TabIndex = 13;
            this.buttonSettingsGeneral.Text = "Settings";
            this.buttonSettingsGeneral.UseVisualStyleBackColor = true;
            this.buttonSettingsGeneral.Click += new System.EventHandler(this.buttonSettings_Click_1);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.groupBoxDocumentSetOps);
            this.flowLayoutPanel4.Controls.Add(this.groupBoxUniversalOps);
            this.flowLayoutPanel4.Controls.Add(this.groupBoxTemplateOps);
            this.flowLayoutPanel4.Controls.Add(this.buttonSettingsGeneral);
            this.flowLayoutPanel4.Controls.Add(this.panelUpDown);
            this.flowLayoutPanel4.Controls.Add(this.flowLayoutPanel5);
            this.flowLayoutPanel4.Controls.Add(this.buttonReload);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(489, 12);
            this.flowLayoutPanel4.MinimumSize = new System.Drawing.Size(129, 441);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(129, 441);
            this.flowLayoutPanel4.TabIndex = 17;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.buttonUp);
            this.flowLayoutPanel5.Controls.Add(this.buttonDown);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 368);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(78, 41);
            this.flowLayoutPanel5.TabIndex = 17;
            this.flowLayoutPanel5.Visible = false;
            // 
            // buttonUp
            // 
            this.buttonUp.AutoSize = true;
            this.buttonUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonUp.Location = new System.Drawing.Point(3, 3);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(33, 35);
            this.buttonUp.TabIndex = 11;
            this.buttonUp.Text = "⇧";
            this.buttonUp.UseVisualStyleBackColor = true;
            // 
            // buttonDown
            // 
            this.buttonDown.AutoSize = true;
            this.buttonDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDown.Location = new System.Drawing.Point(42, 3);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(33, 35);
            this.buttonDown.TabIndex = 12;
            this.buttonDown.Text = "⇩";
            this.buttonDown.UseVisualStyleBackColor = true;
            // 
            // DocumentSetsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 465);
            this.Controls.Add(this.flowLayoutPanel4);
            this.Controls.Add(this.documentSetTreeView);
            this.MinimumSize = new System.Drawing.Size(643, 504);
            this.Name = "DocumentSetsListForm";
            this.Text = "DocumentSetsListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentSetsListForm_FormClosing);
            this.Load += new System.EventHandler(this.DocumentSetsListForm_Load);
            this.groupBoxUniversalOps.ResumeLayout(false);
            this.groupBoxUniversalOps.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxTemplateOps.ResumeLayout(false);
            this.groupBoxTemplateOps.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBoxDocumentSetOps.ResumeLayout(false);
            this.groupBoxDocumentSetOps.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView documentSetTreeView;
        private System.Windows.Forms.Button buttonNewDocumentSet;
        private System.Windows.Forms.Button buttonNewPageTemplate;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Button buttonRemoveUniversal;
        private System.Windows.Forms.Button buttonItemSettingsUniversal;
        private System.Windows.Forms.Button buttonEditTemplate;
        private System.Windows.Forms.Button buttonAddPageDocumentSet;
        private System.Windows.Forms.Button buttonToPfdUniversal;
        private Panel panelUpDown;
        private Button buttonReload;
        private Button buttonEditValues;
        private GroupBox groupBoxUniversalOps;
        private GroupBox groupBoxTemplateOps;
        private GroupBox groupBoxDocumentSetOps;
        private Button buttonSettingsGeneral;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel5;
        private Button buttonUp;
        private Button buttonDown;
    }
}