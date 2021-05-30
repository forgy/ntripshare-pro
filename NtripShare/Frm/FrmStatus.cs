using NtripShare.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NtripShare.Frm
{
    public partial class FrmStatus : Form
    {
        public Dictionary<string, Queue<double>> PoolDataDic = new Dictionary<string, Queue<double>>();
        public Dictionary<string, Queue<double>> NtripDataDic = new Dictionary<string, Queue<double>>();
        public Dictionary<string, Queue<double>> TCPDataDic = new Dictionary<string, Queue<double>>();

        private FrmStatus()
        {
            InitializeComponent();
        }

        private static FrmStatus defaultInstance;

        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static FrmStatus Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new FrmStatus();
                }
                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }

        /// <summary>
        /// 添加账号池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        public void addPoolData(string name, int size) {
            if (!PoolDataDic.ContainsKey(name)) {
                PoolDataDic.Add(name, new Queue<double>());
            }
            PoolDataDic[name].Enqueue(size);
            if (PoolDataDic[name].Count > 50)
            {
                PoolDataDic[name].Dequeue();
            }
        }

        /// <summary>
        /// 添加Ntrip
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        public void addNtripData(string name, int size)
        {
            if (!NtripDataDic.ContainsKey(name))
            {
                NtripDataDic.Add(name, new Queue<double>());
            }
            NtripDataDic[name].Enqueue(size);
            if (NtripDataDic[name].Count > 50)
            {
                NtripDataDic[name].Dequeue();
            }
        }

        /// <summary>
        /// 添加TCP数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        public void addTCPData(string name, int size)
        {
            if (!TCPDataDic.ContainsKey(name))
            {
                TCPDataDic.Add(name, new Queue<double>());
            }
            TCPDataDic[name].Enqueue(size);
            if (TCPDataDic[name].Count > 50) {
                TCPDataDic[name].Dequeue();
            }
        }

        private void FrmStatus_Load(object sender, EventArgs e)
        {
            InitChart();
            this.timer1.Start();
        }


        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            foreach (string key in PoolDataDic.Keys)
            {
                Series series1 = this.chartPool.Series.FindByName(key);
                if (series1 == null) {
                    series1 = new Series(key);
                    series1.ChartArea = "Pool";
                    series1.Color = ColorUtil.GetRandomColor();
                    series1.ChartType = SeriesChartType.Line;
                    series1.Points.Clear();
                    this.chartPool.Series.Add(series1);
                }
                series1.Points.Clear();
                for (int i = 0; i < PoolDataDic[key].Count; i++) {
                    series1.Points.AddXY((i + 1), PoolDataDic[key].ElementAt(i));
                }
              
            }
            foreach (string key in TCPDataDic.Keys)
            {
                Series series1 = this.chartTCP.Series.FindByName(key);
                if (series1 == null)
                {
                    series1 = new Series(key);
                    series1.ChartArea = chartTCP.ChartAreas[0].Name;
                    series1.Color = ColorUtil.GetRandomColor();
                    series1.ChartType = SeriesChartType.Line;
                    series1.Points.Clear();
                    this.chartTCP.Series.Add(series1);
                }
                series1.Points.Clear();
                for (int i = 0; i < TCPDataDic[key].Count; i++)
                {
                    series1.Points.AddXY((i + 1), TCPDataDic[key].ElementAt(i));
                }

            }

            foreach (string key in NtripDataDic.Keys)
            {
                Series series1 = this.chartNtrip.Series.FindByName(key);
                if (series1 == null)
                {
                    series1 = new Series(key);
                    series1.ChartArea = "Ntrip";
                    series1.Color = ColorUtil.GetRandomColor();
                    series1.ChartType = SeriesChartType.Line;
                    series1.Points.Clear();
                    this.chartNtrip.Series.Add(series1);
                }
                series1.Points.Clear();
                for (int i = 0; i < NtripDataDic[key].Count; i++)
                {
                    series1.Points.AddXY((i + 1), NtripDataDic[key].ElementAt(i));
                }

            }
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //定义图表区域
            this.chartPool.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("Pool");
            this.chartPool.ChartAreas.Add(chartArea1);
            //设置图表显示样式
            this.chartPool.ChartAreas[0].AxisY.Minimum = 0;
            this.chartPool.ChartAreas[0].AxisY.Maximum = 1500;
            this.chartPool.ChartAreas[0].AxisY.Interval = 100;
            this.chartPool.ChartAreas[0].AxisX.Interval = 5;
            this.chartPool.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chartPool.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //设置标题
            this.chartPool.Titles.Clear();
            this.chartPool.Titles.Add("S01");
            this.chartPool.Titles[0].ForeColor = Color.RoyalBlue;
            this.chartPool.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chartPool.Titles[0].Text = string.Format("Ntrip账号池数据状态");
            //定义存储和显示点的容器
            this.chartPool.Series.Clear();

            //定义图表区域
            this.chartTCP.ChartAreas.Clear();
            ChartArea chartArea2 = new ChartArea("TCP");
            this.chartTCP.ChartAreas.Add(chartArea2);
            //设置图表显示样式
            this.chartTCP.ChartAreas[0].AxisY.Minimum = 0;
            this.chartTCP.ChartAreas[0].AxisY.Maximum = 1500;
            this.chartTCP.ChartAreas[0].AxisY.Interval = 100;
            this.chartTCP.ChartAreas[0].AxisX.Interval = 5;
            this.chartTCP.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chartTCP.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //设置标题
            this.chartTCP.Titles.Clear();
            this.chartTCP.Titles.Add("S01");
            this.chartTCP.Titles[0].ForeColor = Color.RoyalBlue;
            this.chartTCP.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chartTCP.Titles[0].Text = string.Format("TCP数据流状态");
            //定义存储和显示点的容器
            this.chartTCP.Series.Clear();

            //定义图表区域
            this.chartNtrip.ChartAreas.Clear();
            ChartArea chartArea3 = new ChartArea("Ntrip");
            this.chartNtrip.ChartAreas.Add(chartArea3);
            //设置图表显示样式
            this.chartNtrip.ChartAreas[0].AxisY.Minimum = 0;
            this.chartNtrip.ChartAreas[0].AxisY.Maximum = 1500;
            this.chartNtrip.ChartAreas[0].AxisY.Interval = 100;
            this.chartNtrip.ChartAreas[0].AxisX.Interval = 5;
            this.chartNtrip.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chartNtrip.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //设置标题
            this.chartNtrip.Titles.Clear();
            this.chartNtrip.Titles.Add("S01");
            this.chartNtrip.Titles[0].ForeColor = Color.RoyalBlue;
            this.chartNtrip.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chartNtrip.Titles[0].Text = string.Format("Ntrip数据流状态");
            //定义存储和显示点的容器
            this.chartNtrip.Series.Clear();
        }
    }
}
