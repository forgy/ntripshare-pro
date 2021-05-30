namespace NtripShare.Frm
{
    partial class FrmStatus
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStatus));
            this.layoutSpacerItem3 = new DevComponents.DotNetBar.Layout.LayoutSpacerItem();
            this.layoutSpacerItem4 = new DevComponents.DotNetBar.Layout.LayoutSpacerItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartPool = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartNtrip = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTCP = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNtrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTCP)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutSpacerItem3
            // 
            this.layoutSpacerItem3.Height = 32;
            this.layoutSpacerItem3.Name = "layoutSpacerItem3";
            this.layoutSpacerItem3.Width = 32;
            // 
            // layoutSpacerItem4
            // 
            this.layoutSpacerItem4.Height = 32;
            this.layoutSpacerItem4.Name = "layoutSpacerItem4";
            this.layoutSpacerItem4.Width = 32;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartPool);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1178, 738);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 0;
            // 
            // chartPool
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPool.ChartAreas.Add(chartArea1);
            this.chartPool.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartPool.Legends.Add(legend1);
            this.chartPool.Location = new System.Drawing.Point(0, 0);
            this.chartPool.Name = "chartPool";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPool.Series.Add(series1);
            this.chartPool.Size = new System.Drawing.Size(1178, 392);
            this.chartPool.TabIndex = 0;
            this.chartPool.Text = "chart1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartNtrip);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartTCP);
            this.splitContainer2.Size = new System.Drawing.Size(1178, 342);
            this.splitContainer2.SplitterDistance = 595;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartNtrip
            // 
            chartArea2.Name = "ChartArea1";
            this.chartNtrip.ChartAreas.Add(chartArea2);
            this.chartNtrip.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartNtrip.Legends.Add(legend2);
            this.chartNtrip.Location = new System.Drawing.Point(0, 0);
            this.chartNtrip.Name = "chartNtrip";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartNtrip.Series.Add(series2);
            this.chartNtrip.Size = new System.Drawing.Size(595, 342);
            this.chartNtrip.TabIndex = 0;
            this.chartNtrip.Text = "chart1";
            // 
            // chartTCP
            // 
            chartArea3.Name = "ChartArea1";
            this.chartTCP.ChartAreas.Add(chartArea3);
            this.chartTCP.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartTCP.Legends.Add(legend3);
            this.chartTCP.Location = new System.Drawing.Point(0, 0);
            this.chartTCP.Name = "chartTCP";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartTCP.Series.Add(series3);
            this.chartTCP.Size = new System.Drawing.Size(579, 342);
            this.chartTCP.TabIndex = 0;
            this.chartTCP.Text = "chart1";
            // 
            // FrmStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 738);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStatus";
            this.Text = "系统数据流监控";
            this.Load += new System.EventHandler(this.FrmStatus_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPool)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartNtrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTCP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Layout.LayoutSpacerItem layoutSpacerItem3;
        private DevComponents.DotNetBar.Layout.LayoutSpacerItem layoutSpacerItem4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPool;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNtrip;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTCP;
    }
}