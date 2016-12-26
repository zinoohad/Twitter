namespace TwitterCollector.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubjectResult));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vIEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.twitterTestUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectL = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.keyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tweetContainsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tweetBelongL = new System.Windows.Forms.Label();
            this.totalTweetsUD = new System.Windows.Forms.NumericUpDown();
            this.totalUsersUD = new System.Windows.Forms.NumericUpDown();
            this.totalUsersL = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTweetsUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalUsersUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.chart2);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Controls.Add(this.totalUsersUD);
            this.panel1.Controls.Add(this.totalUsersL);
            this.panel1.Controls.Add(this.totalTweetsUD);
            this.panel1.Controls.Add(this.tweetBelongL);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.subjectL);
            this.panel1.Location = new System.Drawing.Point(13, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 585);
            this.panel1.TabIndex = 5;
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
            this.menuStrip1.TabIndex = 3;
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
            this.topPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.topPanel.Controls.Add(this.tiltleL);
            this.topPanel.Location = new System.Drawing.Point(0, 29);
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
            // twitterTestUIToolStripMenuItem
            // 
            this.twitterTestUIToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("twitterTestUIToolStripMenuItem.Image")));
            this.twitterTestUIToolStripMenuItem.Name = "twitterTestUIToolStripMenuItem";
            this.twitterTestUIToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.twitterTestUIToolStripMenuItem.Text = "Twitter REST API";
            this.twitterTestUIToolStripMenuItem.Click += new System.EventHandler(this.twitterTestUIToolStripMenuItem_Click);
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
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(90, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(301, 24);
            this.comboBox1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyword,
            this.tweetContainsCount});
            this.dataGridView1.Location = new System.Drawing.Point(25, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(365, 301);
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
            this.tweetContainsCount.Width = 123;
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
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(534, 48);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(377, 266);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(529, 340);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(381, 223);
            this.chart2.TabIndex = 8;
            this.chart2.Text = "chart2";
            // 
            // SubjectResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 754);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubjectResult";
            this.Text = "Twitter Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalTweetsUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalUsersUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eDITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vIEWToolStripMenuItem;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label tiltleL;
        private System.Windows.Forms.ToolStripMenuItem twitterTestUIToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label subjectL;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyword;
        private System.Windows.Forms.DataGridViewTextBoxColumn tweetContainsCount;
        private System.Windows.Forms.NumericUpDown totalTweetsUD;
        private System.Windows.Forms.Label tweetBelongL;
        private System.Windows.Forms.NumericUpDown totalUsersUD;
        private System.Windows.Forms.Label totalUsersL;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}