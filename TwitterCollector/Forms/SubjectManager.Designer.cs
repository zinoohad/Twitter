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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectManager));
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addKeywordB = new System.Windows.Forms.Button();
            this.dgvKeyword = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.langK = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.languagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.twitterDataSet = new TwitterCollector.TwitterDataSet();
            this.deleteKey = new System.Windows.Forms.DataGridViewImageColumn();
            this.addKeywordL = new System.Windows.Forms.Label();
            this.addKeywordTB = new System.Windows.Forms.TextBox();
            this.addSubjectB = new System.Windows.Forms.Button();
            this.dgvSubject = new System.Windows.Forms.DataGridView();
            this.subecjtName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lang = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.addSubjectL = new System.Windows.Forms.Label();
            this.addSubjectTB = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mainIcon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.subjectIcon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statisticsIcon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolIcon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dictionaryIcon = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.languagesTableAdapter = new TwitterCollector.TwitterDataSetTableAdapters.LanguagesTableAdapter();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.twitterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubject)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.langK,
            this.deleteKey});
            this.dgvKeyword.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvKeyword.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvKeyword.Location = new System.Drawing.Point(531, 22);
            this.dgvKeyword.Name = "dgvKeyword";
            this.dgvKeyword.RowTemplate.Height = 24;
            this.dgvKeyword.Size = new System.Drawing.Size(443, 462);
            this.dgvKeyword.TabIndex = 6;
            this.dgvKeyword.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.keywordDGV_CellClick);
            this.dgvKeyword.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvKeyword_CurrentCellDirtyStateChanged);
            this.dgvKeyword.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSubject_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Keyword";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // langK
            // 
            this.langK.DataSource = this.languagesBindingSource;
            this.langK.HeaderText = "Language";
            this.langK.Name = "langK";
            this.langK.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.langK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.langK.ValueMember = "NAME";
            this.langK.Width = 97;
            // 
            // languagesBindingSource
            // 
            this.languagesBindingSource.DataMember = "Languages";
            this.languagesBindingSource.DataSource = this.twitterDataSet;
            this.languagesBindingSource.Sort = "name";
            // 
            // twitterDataSet
            // 
            this.twitterDataSet.DataSetName = "TwitterDataSet";
            this.twitterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // deleteKey
            // 
            this.deleteKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.deleteKey.HeaderText = "";
            this.deleteKey.Image = global::TwitterCollector.Properties.Resources.X;
            this.deleteKey.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.deleteKey.Name = "deleteKey";
            this.deleteKey.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.deleteKey.Width = 19;
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
            this.lang,
            this.delete});
            this.dgvSubject.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvSubject.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSubject.Location = new System.Drawing.Point(19, 22);
            this.dgvSubject.Name = "dgvSubject";
            this.dgvSubject.RowTemplate.Height = 24;
            this.dgvSubject.Size = new System.Drawing.Size(443, 462);
            this.dgvSubject.TabIndex = 2;
            this.dgvSubject.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.subjectDGV_CellClick);
            this.dgvSubject.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvSubject_CurrentCellDirtyStateChanged);
            this.dgvSubject.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSubject_DataError);
            // 
            // subecjtName
            // 
            this.subecjtName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.subecjtName.HeaderText = "Subject Name";
            this.subecjtName.Name = "subecjtName";
            // 
            // lang
            // 
            this.lang.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lang.DataSource = this.languagesBindingSource;
            this.lang.DisplayMember = "NAME";
            this.lang.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.lang.HeaderText = "Language";
            this.lang.Name = "lang";
            this.lang.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.lang.ValueMember = "NAME";
            this.lang.Width = 97;
            // 
            // delete
            // 
            this.delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.delete.HeaderText = "";
            this.delete.Image = global::TwitterCollector.Properties.Resources.X;
            this.delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.delete.Name = "delete";
            this.delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.delete.Width = 19;
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainIcon,
            this.toolStripSeparator4,
            this.subjectIcon,
            this.toolStripSeparator1,
            this.statisticsIcon,
            this.toolStripSeparator2,
            this.toolIcon,
            this.toolStripSeparator3,
            this.dictionaryIcon});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1017, 27);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mainIcon
            // 
            this.mainIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mainIcon.Image = global::TwitterCollector.Properties.Resources.home;
            this.mainIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mainIcon.Name = "mainIcon";
            this.mainIcon.Size = new System.Drawing.Size(23, 24);
            this.mainIcon.Text = "toolStripButton1";
            this.mainIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // subjectIcon
            // 
            this.subjectIcon.Image = global::TwitterCollector.Properties.Resources.blue_pencil;
            this.subjectIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.subjectIcon.Name = "subjectIcon";
            this.subjectIcon.Size = new System.Drawing.Size(78, 24);
            this.subjectIcon.Text = "Subject";
            this.subjectIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // statisticsIcon
            // 
            this.statisticsIcon.Image = global::TwitterCollector.Properties.Resources.pie_chart;
            this.statisticsIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statisticsIcon.Name = "statisticsIcon";
            this.statisticsIcon.Size = new System.Drawing.Size(87, 24);
            this.statisticsIcon.Text = "Statistics";
            this.statisticsIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolIcon
            // 
            this.toolIcon.Image = global::TwitterCollector.Properties.Resources.twitter;
            this.toolIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolIcon.Name = "toolIcon";
            this.toolIcon.Size = new System.Drawing.Size(155, 24);
            this.toolIcon.Text = "Twitter Search Tool";
            this.toolIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // dictionaryIcon
            // 
            this.dictionaryIcon.Image = global::TwitterCollector.Properties.Resources.dictionary;
            this.dictionaryIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dictionaryIcon.Name = "dictionaryIcon";
            this.dictionaryIcon.Size = new System.Drawing.Size(97, 24);
            this.dictionaryIcon.Text = "Dictionary";
            this.dictionaryIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "Delete";
            this.dataGridViewImageColumn1.Image = global::TwitterCollector.Properties.Resources.X;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // 
            // languagesTableAdapter
            // 
            this.languagesTableAdapter.ClearBeforeFill = true;
            // 
            // SubjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 688);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubjectManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Twitter Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onExit_Click);
            this.Load += new System.EventHandler(this.SubjectManager_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.twitterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubject)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton subjectIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton statisticsIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dictionaryIcon;
        private System.Windows.Forms.ToolStripButton mainIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private TwitterDataSet twitterDataSet;
        private TwitterDataSetTableAdapters.LanguagesTableAdapter languagesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn langK;
        private System.Windows.Forms.DataGridViewImageColumn deleteKey;
        public System.Windows.Forms.BindingSource languagesBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn subecjtName;
        private System.Windows.Forms.DataGridViewComboBoxColumn lang;
        private System.Windows.Forms.DataGridViewImageColumn delete;

    }
}