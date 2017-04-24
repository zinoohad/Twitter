namespace TwitterCollector.Forms
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.topPanel = new System.Windows.Forms.Panel();
            this.tiltleL = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lastSubject = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.agePieChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.agePieChart)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.topPanel.Controls.Add(this.tiltleL);
            this.topPanel.Location = new System.Drawing.Point(0, 28);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1016, 49);
            this.topPanel.TabIndex = 1;
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
            this.panel1.Controls.Add(this.lastSubject);
            this.panel1.Controls.Add(this.chart2);
            this.panel1.Controls.Add(this.agePieChart);
            this.panel1.Location = new System.Drawing.Point(13, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 585);
            this.panel1.TabIndex = 2;
            // 
            // lastSubject
            // 
            this.lastSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lastSubject.Location = new System.Drawing.Point(3, 28);
            this.lastSubject.Name = "lastSubject";
            this.lastSubject.Size = new System.Drawing.Size(986, 58);
            this.lastSubject.TabIndex = 11;
            this.lastSubject.Text = "Last Subject Division";
            this.lastSubject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart2
            // 
            this.chart2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(505, 159);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(484, 406);
            this.chart2.TabIndex = 10;
            this.chart2.Text = "chart2";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            title1.Name = "Title";
            title1.Text = "Gender Division";
            this.chart2.Titles.Add(title1);
            // 
            // agePieChart
            // 
            this.agePieChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.agePieChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.agePieChart.Legends.Add(legend2);
            this.agePieChart.Location = new System.Drawing.Point(3, 159);
            this.agePieChart.Name = "agePieChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.agePieChart.Series.Add(series2);
            this.agePieChart.Size = new System.Drawing.Size(496, 398);
            this.agePieChart.TabIndex = 9;
            this.agePieChart.Text = "chart1v";
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            title2.Name = "Age Split";
            title2.Text = "Age Division";
            this.agePieChart.Titles.Add(title2);
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
            this.toolStrip1.TabIndex = 3;
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1017, 688);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twitter Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onExit_Click);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.agePieChart)).EndInit();
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
        private System.Windows.Forms.ToolStripButton subjectIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton statisticsIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton dictionaryIcon;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart agePieChart;
        private System.Windows.Forms.Label lastSubject;
        private System.Windows.Forms.ToolStripButton mainIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton settingsIcon;
    }
}