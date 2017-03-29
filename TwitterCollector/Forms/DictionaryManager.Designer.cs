namespace TwitterCollector.Forms
{
    partial class DictionaryManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictionaryManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveB = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ageGB = new System.Windows.Forms.GroupBox();
            this.ageUD = new System.Windows.Forms.NumericUpDown();
            this.olderL = new System.Windows.Forms.Label();
            this.youngerL = new System.Windows.Forms.Label();
            this.ageTB = new System.Windows.Forms.TrackBar();
            this.posNegGB = new System.Windows.Forms.GroupBox();
            this.neutralL = new System.Windows.Forms.Label();
            this.posL = new System.Windows.Forms.Label();
            this.negL = new System.Windows.Forms.Label();
            this.posNegTB = new System.Windows.Forms.TrackBar();
            this.genderGB = new System.Windows.Forms.GroupBox();
            this.genderUD = new System.Windows.Forms.NumericUpDown();
            this.womanL = new System.Windows.Forms.Label();
            this.menL = new System.Windows.Forms.Label();
            this.genderTrackB = new System.Windows.Forms.TrackBar();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.women = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.man = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posNeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleL = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mainIcon = new System.Windows.Forms.ToolStripButton();
            this.subjectIcon = new System.Windows.Forms.ToolStripButton();
            this.statisticsIcon = new System.Windows.Forms.ToolStripButton();
            this.toolIcon = new System.Windows.Forms.ToolStripButton();
            this.dictionaryIcon = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.ageGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageTB)).BeginInit();
            this.posNegGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.posNegTB)).BeginInit();
            this.genderGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genderUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderTrackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
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
            this.panel1.Controls.Add(this.saveB);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.ageGB);
            this.panel1.Controls.Add(this.posNegGB);
            this.panel1.Controls.Add(this.genderGB);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.titleL);
            this.panel1.Location = new System.Drawing.Point(13, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 585);
            this.panel1.TabIndex = 5;
            // 
            // saveB
            // 
            this.saveB.Location = new System.Drawing.Point(875, 534);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(94, 28);
            this.saveB.TabIndex = 10;
            this.saveB.Text = "Save";
            this.saveB.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBox1.Location = new System.Drawing.Point(467, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(508, 64);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "Word To Explore";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ageGB
            // 
            this.ageGB.Controls.Add(this.ageUD);
            this.ageGB.Controls.Add(this.olderL);
            this.ageGB.Controls.Add(this.youngerL);
            this.ageGB.Controls.Add(this.ageTB);
            this.ageGB.Location = new System.Drawing.Point(467, 342);
            this.ageGB.Name = "ageGB";
            this.ageGB.Size = new System.Drawing.Size(508, 72);
            this.ageGB.TabIndex = 8;
            this.ageGB.TabStop = false;
            this.ageGB.Text = "Age";
            // 
            // ageUD
            // 
            this.ageUD.DecimalPlaces = 5;
            this.ageUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ageUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.ageUD.Location = new System.Drawing.Point(14, 27);
            this.ageUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ageUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ageUD.Name = "ageUD";
            this.ageUD.Size = new System.Drawing.Size(108, 26);
            this.ageUD.TabIndex = 7;
            this.ageUD.Leave += new System.EventHandler(this.ageUD_Leave);
            // 
            // olderL
            // 
            this.olderL.AutoSize = true;
            this.olderL.Location = new System.Drawing.Point(459, 49);
            this.olderL.Name = "olderL";
            this.olderL.Size = new System.Drawing.Size(43, 17);
            this.olderL.TabIndex = 7;
            this.olderL.Text = "Older";
            // 
            // youngerL
            // 
            this.youngerL.AutoSize = true;
            this.youngerL.Location = new System.Drawing.Point(132, 49);
            this.youngerL.Name = "youngerL";
            this.youngerL.Size = new System.Drawing.Size(62, 17);
            this.youngerL.TabIndex = 3;
            this.youngerL.Text = "Younger";
            // 
            // ageTB
            // 
            this.ageTB.Location = new System.Drawing.Point(135, 10);
            this.ageTB.Maximum = 10000;
            this.ageTB.Minimum = -10000;
            this.ageTB.Name = "ageTB";
            this.ageTB.Size = new System.Drawing.Size(367, 56);
            this.ageTB.TabIndex = 1;
            this.ageTB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.ageTB.Scroll += new System.EventHandler(this.ageTB_Scroll);
            // 
            // posNegGB
            // 
            this.posNegGB.Controls.Add(this.neutralL);
            this.posNegGB.Controls.Add(this.posL);
            this.posNegGB.Controls.Add(this.negL);
            this.posNegGB.Controls.Add(this.posNegTB);
            this.posNegGB.Location = new System.Drawing.Point(467, 251);
            this.posNegGB.Name = "posNegGB";
            this.posNegGB.Size = new System.Drawing.Size(508, 72);
            this.posNegGB.TabIndex = 7;
            this.posNegGB.TabStop = false;
            this.posNegGB.Text = "Positive / Negative";
            // 
            // neutralL
            // 
            this.neutralL.AutoSize = true;
            this.neutralL.Location = new System.Drawing.Point(293, 49);
            this.neutralL.Name = "neutralL";
            this.neutralL.Size = new System.Drawing.Size(54, 17);
            this.neutralL.TabIndex = 8;
            this.neutralL.Text = "Neutral";
            // 
            // posL
            // 
            this.posL.AutoSize = true;
            this.posL.Location = new System.Drawing.Point(446, 49);
            this.posL.Name = "posL";
            this.posL.Size = new System.Drawing.Size(57, 17);
            this.posL.TabIndex = 7;
            this.posL.Text = "Positive";
            // 
            // negL
            // 
            this.negL.AutoSize = true;
            this.negL.Location = new System.Drawing.Point(132, 49);
            this.negL.Name = "negL";
            this.negL.Size = new System.Drawing.Size(64, 17);
            this.negL.TabIndex = 3;
            this.negL.Text = "Negative";
            // 
            // posNegTB
            // 
            this.posNegTB.Location = new System.Drawing.Point(135, 10);
            this.posNegTB.Maximum = 1;
            this.posNegTB.Minimum = -1;
            this.posNegTB.Name = "posNegTB";
            this.posNegTB.Size = new System.Drawing.Size(367, 56);
            this.posNegTB.TabIndex = 1;
            // 
            // genderGB
            // 
            this.genderGB.Controls.Add(this.genderUD);
            this.genderGB.Controls.Add(this.womanL);
            this.genderGB.Controls.Add(this.menL);
            this.genderGB.Controls.Add(this.genderTrackB);
            this.genderGB.Location = new System.Drawing.Point(467, 161);
            this.genderGB.Name = "genderGB";
            this.genderGB.Size = new System.Drawing.Size(508, 72);
            this.genderGB.TabIndex = 6;
            this.genderGB.TabStop = false;
            this.genderGB.Text = "Gender";
            // 
            // genderUD
            // 
            this.genderUD.DecimalPlaces = 5;
            this.genderUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.genderUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.genderUD.Location = new System.Drawing.Point(14, 27);
            this.genderUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.genderUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.genderUD.Name = "genderUD";
            this.genderUD.Size = new System.Drawing.Size(108, 26);
            this.genderUD.TabIndex = 7;
            this.genderUD.Leave += new System.EventHandler(this.genderUD_Leave);
            // 
            // womanL
            // 
            this.womanL.AutoSize = true;
            this.womanL.Location = new System.Drawing.Point(446, 49);
            this.womanL.Name = "womanL";
            this.womanL.Size = new System.Drawing.Size(56, 17);
            this.womanL.TabIndex = 7;
            this.womanL.Text = "Women";
            // 
            // menL
            // 
            this.menL.AutoSize = true;
            this.menL.Location = new System.Drawing.Point(132, 49);
            this.menL.Name = "menL";
            this.menL.Size = new System.Drawing.Size(35, 17);
            this.menL.TabIndex = 3;
            this.menL.Text = "Men";
            // 
            // genderTrackB
            // 
            this.genderTrackB.Location = new System.Drawing.Point(135, 10);
            this.genderTrackB.Maximum = 10000;
            this.genderTrackB.Minimum = -10000;
            this.genderTrackB.Name = "genderTrackB";
            this.genderTrackB.Size = new System.Drawing.Size(367, 56);
            this.genderTrackB.TabIndex = 1;
            this.genderTrackB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.genderTrackB.Scroll += new System.EventHandler(this.genderTrackB_Scroll);
            // 
            // dgv
            // 
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.word,
            this.women,
            this.man,
            this.posNeg,
            this.age});
            this.dgv.Location = new System.Drawing.Point(23, 89);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(423, 473);
            this.dgv.TabIndex = 2;
            // 
            // word
            // 
            this.word.HeaderText = "Word";
            this.word.Name = "word";
            this.word.Width = 67;
            // 
            // women
            // 
            this.women.HeaderText = "Women";
            this.women.Name = "women";
            this.women.Width = 81;
            // 
            // man
            // 
            this.man.HeaderText = "Man";
            this.man.Name = "man";
            this.man.Width = 60;
            // 
            // posNeg
            // 
            this.posNeg.HeaderText = "Pos / Neg word";
            this.posNeg.Name = "posNeg";
            this.posNeg.Width = 118;
            // 
            // age
            // 
            this.age.HeaderText = "Age Group";
            this.age.Name = "age";
            this.age.Width = 94;
            // 
            // titleL
            // 
            this.titleL.AutoSize = true;
            this.titleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.titleL.Location = new System.Drawing.Point(334, 17);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(311, 39);
            this.titleL.TabIndex = 0;
            this.titleL.Text = "Learning Dictionary";
            // 
            // topPanel
            // 
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
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
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
            // subjectIcon
            // 
            this.subjectIcon.Image = global::TwitterCollector.Properties.Resources.blue_pencil;
            this.subjectIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.subjectIcon.Name = "subjectIcon";
            this.subjectIcon.Size = new System.Drawing.Size(78, 24);
            this.subjectIcon.Text = "Subject";
            this.subjectIcon.Click += new System.EventHandler(this.toolStripAction_Click);
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
            // toolIcon
            // 
            this.toolIcon.Image = global::TwitterCollector.Properties.Resources.twitter;
            this.toolIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolIcon.Name = "toolIcon";
            this.toolIcon.Size = new System.Drawing.Size(155, 24);
            this.toolIcon.Text = "Twitter Search Tool";
            this.toolIcon.Click += new System.EventHandler(this.toolStripAction_Click);
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
            // DictionaryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 688);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DictionaryManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DictionaryManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onExit_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ageGB.ResumeLayout(false);
            this.ageGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ageUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ageTB)).EndInit();
            this.posNegGB.ResumeLayout(false);
            this.posNegGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.posNegTB)).EndInit();
            this.genderGB.ResumeLayout(false);
            this.genderGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genderUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.genderTrackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton subjectIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton statisticsIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dictionaryIcon;
        private System.Windows.Forms.Label titleL;
        private System.Windows.Forms.TrackBar genderTrackB;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn word;
        private System.Windows.Forms.DataGridViewTextBoxColumn women;
        private System.Windows.Forms.DataGridViewTextBoxColumn man;
        private System.Windows.Forms.GroupBox genderGB;
        private System.Windows.Forms.Label womanL;
        private System.Windows.Forms.Label menL;
        private System.Windows.Forms.NumericUpDown genderUD;
        private System.Windows.Forms.GroupBox posNegGB;
        private System.Windows.Forms.Label posL;
        private System.Windows.Forms.Label negL;
        private System.Windows.Forms.TrackBar posNegTB;
        private System.Windows.Forms.GroupBox ageGB;
        private System.Windows.Forms.NumericUpDown ageUD;
        private System.Windows.Forms.Label olderL;
        private System.Windows.Forms.Label youngerL;
        private System.Windows.Forms.TrackBar ageTB;
        private System.Windows.Forms.Label neutralL;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton mainIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataGridViewTextBoxColumn posNeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.Button saveB;
    }
}