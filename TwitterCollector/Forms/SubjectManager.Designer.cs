namespace TwitterCollector.Forms
{
    partial class SubjectManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectManager));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vIEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addKeywordB = new System.Windows.Forms.Button();
            this.dgvKeyword = new System.Windows.Forms.DataGridView();
            this.addKeywordL = new System.Windows.Forms.Label();
            this.addKeywordTB = new System.Windows.Forms.TextBox();
            this.addSubjectB = new System.Windows.Forms.Button();
            this.dgvSubject = new System.Windows.Forms.DataGridView();
            this.addSubjectL = new System.Windows.Forms.Label();
            this.addSubjectTB = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.twitterTestUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subecjtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteKey = new System.Windows.Forms.DataGridViewImageColumn();
            this.menuStrip1.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubject)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.eDITToolStripMenuItem,
            this.vIEWToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1017, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.fileToolStripMenuItem.Text = "FILE";
            // 
            // eDITToolStripMenuItem
            // 
            this.eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            this.eDITToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.eDITToolStripMenuItem.Text = "EDIT";
            // 
            // vIEWToolStripMenuItem
            // 
            this.vIEWToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.twitterTestUIToolStripMenuItem});
            this.vIEWToolStripMenuItem.Name = "vIEWToolStripMenuItem";
            this.vIEWToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.vIEWToolStripMenuItem.Text = "TOOLS";
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.topPanel.Controls.Add(this.tiltleL);
            this.topPanel.Location = new System.Drawing.Point(0, 28);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1016, 49);
            this.topPanel.TabIndex = 3;
            // 
            // tiltleL
            // 
            this.tiltleL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tiltleL.AutoSize = true;
            this.tiltleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tiltleL.ForeColor = System.Drawing.Color.White;
            this.tiltleL.Location = new System.Drawing.Point(436, 6);
            this.tiltleL.Name = "tiltleL";
            this.tiltleL.Size = new System.Drawing.Size(126, 39);
            this.tiltleL.TabIndex = 0;
            this.tiltleL.Text = "Twitter";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.addKeywordB);
            this.panel1.Controls.Add(this.dgvKeyword);
            this.panel1.Controls.Add(this.addKeywordL);
            this.panel1.Controls.Add(this.addKeywordTB);
            this.panel1.Controls.Add(this.addSubjectB);
            this.panel1.Controls.Add(this.dgvSubject);
            this.panel1.Controls.Add(this.addSubjectL);
            this.panel1.Controls.Add(this.addSubjectTB);
            this.panel1.Location = new System.Drawing.Point(13, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 585);
            this.panel1.TabIndex = 4;
            // 
            // addKeywordB
            // 
            this.addKeywordB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addKeywordB.Location = new System.Drawing.Point(890, 507);
            this.addKeywordB.Name = "addKeywordB";
            this.addKeywordB.Size = new System.Drawing.Size(76, 23);
            this.addKeywordB.TabIndex = 7;
            this.addKeywordB.Text = "Add";
            this.addKeywordB.UseVisualStyleBackColor = true;
            this.addKeywordB.Click += new System.EventHandler(this.addKeywordB_Click);
            // 
            // dgvKeyword
            // 
            this.dgvKeyword.AllowUserToAddRows = false;
            this.dgvKeyword.AllowUserToDeleteRows = false;
            this.dgvKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKeyword.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvKeyword.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeyword.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.deleteKey});
            this.dgvKeyword.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvKeyword.Location = new System.Drawing.Point(531, 22);
            this.dgvKeyword.Name = "dgvKeyword";
            this.dgvKeyword.RowTemplate.Height = 24;
            this.dgvKeyword.Size = new System.Drawing.Size(443, 462);
            this.dgvKeyword.TabIndex = 6;
            this.dgvKeyword.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.keywordDGV_CellClick);
            // 
            // addKeywordL
            // 
            this.addKeywordL.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addKeywordL.AutoSize = true;
            this.addKeywordL.Location = new System.Drawing.Point(534, 510);
            this.addKeywordL.Name = "addKeywordL";
            this.addKeywordL.Size = new System.Drawing.Size(95, 17);
            this.addKeywordL.TabIndex = 5;
            this.addKeywordL.Text = "Add Keyword:";
            // 
            // addKeywordTB
            // 
            this.addKeywordTB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addKeywordTB.Location = new System.Drawing.Point(632, 507);
            this.addKeywordTB.Name = "addKeywordTB";
            this.addKeywordTB.Size = new System.Drawing.Size(247, 22);
            this.addKeywordTB.TabIndex = 4;
            this.addKeywordTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keyword_OnKeyDownHandler);
            // 
            // addSubjectB
            // 
            this.addSubjectB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addSubjectB.Location = new System.Drawing.Point(376, 507);
            this.addSubjectB.Name = "addSubjectB";
            this.addSubjectB.Size = new System.Drawing.Size(76, 23);
            this.addSubjectB.TabIndex = 3;
            this.addSubjectB.Text = "Add";
            this.addSubjectB.UseVisualStyleBackColor = true;
            this.addSubjectB.Click += new System.EventHandler(this.addSubjectB_Click);
            // 
            // dgvSubject
            // 
            this.dgvSubject.AllowUserToAddRows = false;
            this.dgvSubject.AllowUserToDeleteRows = false;
            this.dgvSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvSubject.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSubject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subecjtName,
            this.delete});
            this.dgvSubject.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvSubject.Location = new System.Drawing.Point(19, 22);
            this.dgvSubject.Name = "dgvSubject";
            this.dgvSubject.RowTemplate.Height = 24;
            this.dgvSubject.Size = new System.Drawing.Size(443, 462);
            this.dgvSubject.TabIndex = 2;
            this.dgvSubject.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.subjectDGV_CellClick);
            // 
            // addSubjectL
            // 
            this.addSubjectL.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addSubjectL.AutoSize = true;
            this.addSubjectL.Location = new System.Drawing.Point(22, 510);
            this.addSubjectL.Name = "addSubjectL";
            this.addSubjectL.Size = new System.Drawing.Size(88, 17);
            this.addSubjectL.TabIndex = 1;
            this.addSubjectL.Text = "Add Subject:";
            // 
            // addSubjectTB
            // 
            this.addSubjectTB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addSubjectTB.Location = new System.Drawing.Point(118, 507);
            this.addSubjectTB.Name = "addSubjectTB";
            this.addSubjectTB.Size = new System.Drawing.Size(247, 22);
            this.addSubjectTB.TabIndex = 0;
            this.addSubjectTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Subject_OnKeyDownHandler);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Delete";
            this.dataGridViewImageColumn1.Image = global::TwitterCollector.Properties.Resources.X;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 74;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn2.HeaderText = "Delete";
            this.dataGridViewImageColumn2.Image = global::TwitterCollector.Properties.Resources.X;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 74;
            // 
            // twitterTestUIToolStripMenuItem
            // 
            this.twitterTestUIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("twitterTestUIToolStripMenuItem.Image")));
            this.twitterTestUIToolStripMenuItem.Name = "twitterTestUIToolStripMenuItem";
            this.twitterTestUIToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.twitterTestUIToolStripMenuItem.Text = "Twitter REST API";
            this.twitterTestUIToolStripMenuItem.Click += new System.EventHandler(this.twitterTestUIToolStripMenuItem_Click);
            // 
            // subecjtName
            // 
            this.subecjtName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.subecjtName.HeaderText = "Subject Name";
            this.subecjtName.Name = "subecjtName";
            this.subecjtName.ReadOnly = true;
            // 
            // delete
            // 
            this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.delete.HeaderText = "Delete";
            this.delete.Image = global::TwitterCollector.Properties.Resources.X;
            this.delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.delete.Name = "delete";
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delete.Width = 74;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Keyword";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // deleteKey
            // 
            this.deleteKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.deleteKey.HeaderText = "Delete";
            this.deleteKey.Image = global::TwitterCollector.Properties.Resources.X;
            this.deleteKey.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.deleteKey.Name = "deleteKey";
            this.deleteKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.deleteKey.Width = 74;
            // 
            // SubjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 754);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubjectManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twitter Tool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubject)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eDITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vIEWToolStripMenuItem;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label tiltleL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label addSubjectL;
        private System.Windows.Forms.TextBox addSubjectTB;
        private System.Windows.Forms.Button addKeywordB;
        private System.Windows.Forms.DataGridView dgvKeyword;
        private System.Windows.Forms.Label addKeywordL;
        private System.Windows.Forms.TextBox addKeywordTB;
        private System.Windows.Forms.Button addSubjectB;
        private System.Windows.Forms.DataGridView dgvSubject;
        private System.Windows.Forms.ToolStripMenuItem twitterTestUIToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn deleteKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn subecjtName;
        private System.Windows.Forms.DataGridViewImageColumn delete;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;

    }
}