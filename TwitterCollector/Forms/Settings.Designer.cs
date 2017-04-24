namespace TwitterCollector.Forms
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startOnStartup = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.supervisiorTS = new JCS.ToggleSwitch();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.supervisiorIntervalsL = new System.Windows.Forms.Label();
            this.threadDGV = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThreadName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThreadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThreadDesirableState = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ThreadProcessID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.settingsIcon = new System.Windows.Forms.ToolStripButton();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadDGV)).BeginInit();
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
            this.topPanel.TabIndex = 4;
            // 
            // tiltleL
            // 
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
            this.panel1.Controls.Add(this.startOnStartup);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.supervisiorTS);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.supervisiorIntervalsL);
            this.panel1.Controls.Add(this.threadDGV);
            this.panel1.Location = new System.Drawing.Point(13, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 585);
            this.panel1.TabIndex = 5;
            // 
            // startOnStartup
            // 
            this.startOnStartup.AutoSize = true;
            this.startOnStartup.Checked = true;
            this.startOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startOnStartup.Location = new System.Drawing.Point(19, 92);
            this.startOnStartup.Name = "startOnStartup";
            this.startOnStartup.Size = new System.Drawing.Size(205, 21);
            this.startOnStartup.TabIndex = 5;
            this.startOnStartup.Text = "Start Supervisor On Startup";
            this.startOnStartup.UseVisualStyleBackColor = true;
            this.startOnStartup.CheckedChanged += new System.EventHandler(this.startOnStartup_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Supervisior State:";
            // 
            // supervisiorTS
            // 
            this.supervisiorTS.Location = new System.Drawing.Point(201, 36);
            this.supervisiorTS.Name = "supervisiorTS";
            this.supervisiorTS.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.supervisiorTS.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.supervisiorTS.Size = new System.Drawing.Size(65, 22);
            this.supervisiorTS.Style = JCS.ToggleSwitch.ToggleSwitchStyle.IOS5;
            this.supervisiorTS.TabIndex = 3;
            this.supervisiorTS.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.supervisiorTS_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(201, 64);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 22);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // supervisiorIntervalsL
            // 
            this.supervisiorIntervalsL.AutoSize = true;
            this.supervisiorIntervalsL.Location = new System.Drawing.Point(16, 66);
            this.supervisiorIntervalsL.Name = "supervisiorIntervalsL";
            this.supervisiorIntervalsL.Size = new System.Drawing.Size(176, 17);
            this.supervisiorIntervalsL.TabIndex = 1;
            this.supervisiorIntervalsL.Text = "Supervisior Intervals (sec):";
            // 
            // threadDGV
            // 
            this.threadDGV.AllowUserToAddRows = false;
            this.threadDGV.AllowUserToDeleteRows = false;
            this.threadDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.threadDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.threadDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.threadDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ThreadName,
            this.SubjectName,
            this.ThreadState,
            this.ThreadDesirableState,
            this.ThreadProcessID,
            this.MachineName});
            this.threadDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.threadDGV.Location = new System.Drawing.Point(3, 123);
            this.threadDGV.Name = "threadDGV";
            this.threadDGV.RowTemplate.Height = 24;
            this.threadDGV.Size = new System.Drawing.Size(986, 459);
            this.threadDGV.TabIndex = 0;
            this.threadDGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvThreads_CurrentCellDirtyStateChanged);
            this.threadDGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.threadDGV_DataError);
            // 
            // ID
            // 
            this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 46;
            // 
            // ThreadName
            // 
            this.ThreadName.HeaderText = "Thread Name";
            this.ThreadName.Name = "ThreadName";
            this.ThreadName.ReadOnly = true;
            // 
            // SubjectName
            // 
            this.SubjectName.HeaderText = "Subject Name";
            this.SubjectName.Name = "SubjectName";
            this.SubjectName.ReadOnly = true;
            // 
            // ThreadState
            // 
            this.ThreadState.HeaderText = "Thread State";
            this.ThreadState.Name = "ThreadState";
            this.ThreadState.ReadOnly = true;
            // 
            // ThreadDesirableState
            // 
            this.ThreadDesirableState.HeaderText = "Thread Desirable State";
            this.ThreadDesirableState.Items.AddRange(new object[] {
            "Start",
            "Stop"});
            this.ThreadDesirableState.Name = "ThreadDesirableState";
            // 
            // ThreadProcessID
            // 
            this.ThreadProcessID.HeaderText = "Thread Process ID";
            this.ThreadProcessID.Name = "ThreadProcessID";
            this.ThreadProcessID.ReadOnly = true;
            this.ThreadProcessID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ThreadProcessID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MachineName
            // 
            this.MachineName.HeaderText = "Machine Name";
            this.MachineName.Name = "MachineName";
            this.MachineName.ReadOnly = true;
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
            this.dictionaryIcon,
            this.settingsIcon});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1017, 27);
            this.toolStrip1.TabIndex = 6;
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
            // settingsIcon
            // 
            this.settingsIcon.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsIcon.Image = ((System.Drawing.Image)(resources.GetObject("settingsIcon.Image")));
            this.settingsIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsIcon.Name = "settingsIcon";
            this.settingsIcon.Size = new System.Drawing.Size(23, 24);
            this.settingsIcon.Text = "Settings";
            this.settingsIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 688);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Twitter Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onExit_Click);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threadDGV)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label tiltleL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mainIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton subjectIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton statisticsIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dictionaryIcon;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label supervisiorIntervalsL;
        private System.Windows.Forms.Label label1;
        private JCS.ToggleSwitch supervisiorTS;
        private System.Windows.Forms.ToolStripButton settingsIcon;
        private System.Windows.Forms.CheckBox startOnStartup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadState;
        private System.Windows.Forms.DataGridViewComboBoxColumn ThreadDesirableState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThreadProcessID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineName;
        private System.Windows.Forms.DataGridView threadDGV;
    }
}