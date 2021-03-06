﻿namespace TwitterCollector.Forms
{
    partial class SubjectResult
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectResult));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.startTimeTB = new System.Windows.Forms.TextBox();
            this.startTimeL = new System.Windows.Forms.Label();
            this.onOffS = new JCS.ToggleSwitch();
            this.totalUsersUD = new System.Windows.Forms.NumericUpDown();
            this.totalUsersL = new System.Windows.Forms.Label();
            this.totalTweetsUD = new System.Windows.Forms.NumericUpDown();
            this.tweetBelongL = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.keyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tweetContainsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subjectCB = new System.Windows.Forms.ComboBox();
            this.subjectL = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
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
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalUsersUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTweetsUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.topPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.startTimeTB);
            this.panel1.Controls.Add(this.startTimeL);
            this.panel1.Controls.Add(this.onOffS);
            this.panel1.Controls.Add(this.totalUsersUD);
            this.panel1.Controls.Add(this.totalUsersL);
            this.panel1.Controls.Add(this.totalTweetsUD);
            this.panel1.Controls.Add(this.tweetBelongL);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.subjectCB);
            this.panel1.Controls.Add(this.subjectL);
            this.panel1.Location = new System.Drawing.Point(13, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 586);
            this.panel1.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 32);
            this.button2.TabIndex = 13;
            this.button2.Text = "Close Diagrams";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CloseXmapp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "View Diagrams";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.diagrms_button_click);
            // 
            // startTimeTB
            // 
            this.startTimeTB.Location = new System.Drawing.Point(115, 112);
            this.startTimeTB.Name = "startTimeTB";
            this.startTimeTB.ReadOnly = true;
            this.startTimeTB.Size = new System.Drawing.Size(275, 22);
            this.startTimeTB.TabIndex = 11;
            this.startTimeTB.Text = "dd/MM/yyyy hh:mm:ss";
            // 
            // startTimeL
            // 
            this.startTimeL.AutoSize = true;
            this.startTimeL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startTimeL.Location = new System.Drawing.Point(15, 111);
            this.startTimeL.Name = "startTimeL";
            this.startTimeL.Size = new System.Drawing.Size(92, 20);
            this.startTimeL.TabIndex = 10;
            this.startTimeL.Text = "Start Time:";
            // 
            // onOffS
            // 
            this.onOffS.Location = new System.Drawing.Point(341, 17);
            this.onOffS.Name = "onOffS";
            this.onOffS.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.onOffS.OffForeColor = System.Drawing.Color.Red;
            this.onOffS.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.onOffS.OnForeColor = System.Drawing.Color.Green;
            this.onOffS.Size = new System.Drawing.Size(50, 23);
            this.onOffS.Style = JCS.ToggleSwitch.ToggleSwitchStyle.BrushedMetal;
            this.onOffS.TabIndex = 9;
            this.onOffS.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.onOffS_CheckedChanged);
            // 
            // totalUsersUD
            // 
            this.totalUsersUD.Location = new System.Drawing.Point(205, 81);
            this.totalUsersUD.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.totalUsersUD.Name = "totalUsersUD";
            this.totalUsersUD.ReadOnly = true;
            this.totalUsersUD.Size = new System.Drawing.Size(186, 22);
            this.totalUsersUD.TabIndex = 6;
            // 
            // totalUsersL
            // 
            this.totalUsersL.AutoSize = true;
            this.totalUsersL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.totalUsersL.Location = new System.Drawing.Point(15, 82);
            this.totalUsersL.Name = "totalUsersL";
            this.totalUsersL.Size = new System.Drawing.Size(167, 20);
            this.totalUsersL.TabIndex = 5;
            this.totalUsersL.Text = "Total Users Belongs:";
            // 
            // totalTweetsUD
            // 
            this.totalTweetsUD.Location = new System.Drawing.Point(204, 53);
            this.totalTweetsUD.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.totalTweetsUD.Name = "totalTweetsUD";
            this.totalTweetsUD.ReadOnly = true;
            this.totalTweetsUD.Size = new System.Drawing.Size(186, 22);
            this.totalTweetsUD.TabIndex = 4;
            // 
            // tweetBelongL
            // 
            this.tweetBelongL.AutoSize = true;
            this.tweetBelongL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tweetBelongL.Location = new System.Drawing.Point(14, 54);
            this.tweetBelongL.Name = "tweetBelongL";
            this.tweetBelongL.Size = new System.Drawing.Size(176, 20);
            this.tweetBelongL.TabIndex = 3;
            this.tweetBelongL.Text = "Total Tweets Belongs:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyword,
            this.tweetContainsCount});
            this.dataGridView1.Location = new System.Drawing.Point(25, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(365, 419);
            this.dataGridView1.TabIndex = 2;
            // 
            // keyword
            // 
            this.keyword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.keyword.HeaderText = "Keyword";
            this.keyword.Name = "keyword";
            this.keyword.ReadOnly = true;
            // 
            // tweetContainsCount
            // 
            this.tweetContainsCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tweetContainsCount.DefaultCellStyle = dataGridViewCellStyle1;
            this.tweetContainsCount.HeaderText = "Tweet Contains Count";
            this.tweetContainsCount.Name = "tweetContainsCount";
            this.tweetContainsCount.ReadOnly = true;
            this.tweetContainsCount.Width = 156;
            // 
            // subjectCB
            // 
            this.subjectCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subjectCB.FormattingEnabled = true;
            this.subjectCB.Location = new System.Drawing.Point(90, 17);
            this.subjectCB.Name = "subjectCB";
            this.subjectCB.Size = new System.Drawing.Size(245, 24);
            this.subjectCB.TabIndex = 1;
            this.subjectCB.SelectedIndexChanged += new System.EventHandler(this.subjectCB_SelectedIndexChanged);
            // 
            // subjectL
            // 
            this.subjectL.AutoSize = true;
            this.subjectL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.subjectL.Location = new System.Drawing.Point(14, 21);
            this.subjectL.Name = "subjectL";
            this.subjectL.Size = new System.Drawing.Size(70, 20);
            this.subjectL.TabIndex = 0;
            this.subjectL.Text = "Subject:";
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.topPanel.Controls.Add(this.tiltleL);
            this.topPanel.Location = new System.Drawing.Point(0, 29);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1016, 49);
            this.topPanel.TabIndex = 4;
            // 
            // tiltleL
            // 
            this.tiltleL.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tiltleL.AutoSize = true;
            this.tiltleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tiltleL.ForeColor = System.Drawing.Color.White;
            this.tiltleL.Location = new System.Drawing.Point(436, 6);
            this.tiltleL.Name = "tiltleL";
            this.tiltleL.Size = new System.Drawing.Size(126, 39);
            this.tiltleL.TabIndex = 0;
            this.tiltleL.Text = "Twitter";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.mainIcon.Size = new System.Drawing.Size(24, 24);
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
            this.subjectIcon.Size = new System.Drawing.Size(82, 24);
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
            this.statisticsIcon.Size = new System.Drawing.Size(91, 24);
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
            this.toolIcon.Size = new System.Drawing.Size(159, 24);
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
            this.dictionaryIcon.Size = new System.Drawing.Size(101, 24);
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
            this.settingsIcon.Size = new System.Drawing.Size(24, 24);
            this.settingsIcon.Text = "Settings";
            this.settingsIcon.Click += new System.EventHandler(this.toolStripAction_Click);
            // 
            // SubjectResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 688);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubjectResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Twitter Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onExit_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.totalUsersUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTweetsUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label tiltleL;
        private System.Windows.Forms.ComboBox subjectCB;
        private System.Windows.Forms.Label subjectL;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword;
        private System.Windows.Forms.DataGridViewTextBoxColumn tweetContainsCount;
        private System.Windows.Forms.NumericUpDown totalTweetsUD;
        private System.Windows.Forms.Label tweetBelongL;
        private System.Windows.Forms.NumericUpDown totalUsersUD;
        private System.Windows.Forms.Label totalUsersL;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton subjectIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton statisticsIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dictionaryIcon;
        private JCS.ToggleSwitch onOffS;
        private System.Windows.Forms.ToolStripButton mainIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TextBox startTimeTB;
        private System.Windows.Forms.Label startTimeL;
        private System.Windows.Forms.ToolStripButton settingsIcon;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}