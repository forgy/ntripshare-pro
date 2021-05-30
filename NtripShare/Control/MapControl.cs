using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graphic;
using NtripShare.Model;
using NtripShare.Util;
//using NtripClient.Model;
//using ZHD.CoordLib;
//using NtripClient.Util;

namespace NtripShare
{
    public partial class MapControl : UserControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public List<NtripAccount> NtripAccounts { get; set; } = new List<NtripAccount>();
        public List<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();

        public bool ShowJiZhan { get; set; } = true;
        public bool ShowJianCe { get; set; } = true;
        public bool ShowResultPos { get; set; } = true;
        public bool ShowAllResult { get; set; } = false;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonXPan = new DevComponents.DotNetBar.ButtonX();
            this.buttonXGWang = new DevComponents.DotNetBar.ButtonX();
            this.buttonXFullExtend = new DevComponents.DotNetBar.ButtonX();
            this.buttonXZoomOut = new DevComponents.DotNetBar.ButtonX();
            this.buttonXZoomIn = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.map1 = new Graphic.MapViewer();
            this.buttonXCur = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonXPan
            // 
            this.buttonXPan.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXPan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXPan.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXPan.Image = global::NtripShare.Properties.Resources.pan1;
            this.buttonXPan.Location = new System.Drawing.Point(417, 2);
            this.buttonXPan.Name = "buttonXPan";
            this.buttonXPan.Size = new System.Drawing.Size(30, 30);
            this.buttonXPan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXPan.TabIndex = 17;
            this.buttonXPan.Click += new System.EventHandler(this.buttonXPan_Click);
            // 
            // buttonXGWang
            // 
            this.buttonXGWang.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXGWang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXGWang.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXGWang.Image = global::NtripShare.Properties.Resources.grid;
            this.buttonXGWang.Location = new System.Drawing.Point(607, 2);
            this.buttonXGWang.Name = "buttonXGWang";
            this.buttonXGWang.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonXGWang.Size = new System.Drawing.Size(30, 30);
            this.buttonXGWang.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXGWang.TabIndex = 16;
            this.buttonXGWang.Click += new System.EventHandler(this.buttonXGWang_Click);
            // 
            // buttonXFullExtend
            // 
            this.buttonXFullExtend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXFullExtend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXFullExtend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXFullExtend.Image = global::NtripShare.Properties.Resources.resize;
            this.buttonXFullExtend.Location = new System.Drawing.Point(569, 2);
            this.buttonXFullExtend.Name = "buttonXFullExtend";
            this.buttonXFullExtend.Size = new System.Drawing.Size(30, 30);
            this.buttonXFullExtend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXFullExtend.TabIndex = 15;
            this.buttonXFullExtend.Click += new System.EventHandler(this.buttonXFullExtend_Click);
            // 
            // buttonXZoomOut
            // 
            this.buttonXZoomOut.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXZoomOut.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXZoomOut.Image = global::NtripShare.Properties.Resources.zoomout;
            this.buttonXZoomOut.Location = new System.Drawing.Point(493, 2);
            this.buttonXZoomOut.Name = "buttonXZoomOut";
            this.buttonXZoomOut.Size = new System.Drawing.Size(30, 30);
            this.buttonXZoomOut.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXZoomOut.TabIndex = 14;
            this.buttonXZoomOut.Click += new System.EventHandler(this.buttonXZoomOut_Click);
            // 
            // buttonXZoomIn
            // 
            this.buttonXZoomIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXZoomIn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXZoomIn.Image = global::NtripShare.Properties.Resources.zoomin;
            this.buttonXZoomIn.Location = new System.Drawing.Point(455, 2);
            this.buttonXZoomIn.Name = "buttonXZoomIn";
            this.buttonXZoomIn.Size = new System.Drawing.Size(30, 30);
            this.buttonXZoomIn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXZoomIn.TabIndex = 13;
            this.buttonXZoomIn.Click += new System.EventHandler(this.buttonXZoomIn_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = global::NtripShare.Properties.Resources.selec;
            this.buttonX1.Location = new System.Drawing.Point(531, 2);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(30, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 18;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // map1
            // 
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.Location = new System.Drawing.Point(0, 0);
            this.map1.Name = "map1";
            this.map1.Size = new System.Drawing.Size(648, 480);
            this.map1.TabIndex = 0;
            this.map1.toolMode = Graphic.ToolModes.Pan;
            // 
            // buttonXCur
            // 
            this.buttonXCur.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCur.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCur.Image = global::NtripShare.Properties.Resources.cursor;
            this.buttonXCur.Location = new System.Drawing.Point(379, 2);
            this.buttonXCur.Name = "buttonXCur";
            this.buttonXCur.Size = new System.Drawing.Size(30, 30);
            this.buttonXCur.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonXCur.TabIndex = 12;
            this.buttonXCur.Click += new System.EventHandler(this.buttonXCur_Click);
            // 
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.buttonXPan);
            this.Controls.Add(this.buttonXGWang);
            this.Controls.Add(this.buttonXFullExtend);
            this.Controls.Add(this.buttonXZoomOut);
            this.Controls.Add(this.buttonXZoomIn);
            this.Controls.Add(this.buttonXCur);
            this.Controls.Add(this.map1);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(648, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private Graphic.MapViewer map1;
        private DevComponents.DotNetBar.ButtonX buttonXPan;
        private DevComponents.DotNetBar.ButtonX buttonXGWang;
        private DevComponents.DotNetBar.ButtonX buttonXFullExtend;
        private DevComponents.DotNetBar.ButtonX buttonXZoomOut;
        private DevComponents.DotNetBar.ButtonX buttonXZoomIn;
        private DevComponents.DotNetBar.ButtonX buttonXCur;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private int minX = 0;
        private int minY = 0;
        private int maxX = 200;
        private int maxY = 200;
        public MapControl()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.mapControl1_Resize);
            map1.IsSimpleMode = true;
            map1.PaintMapCE = mapDraw;
            map1.toolMode = ToolModes.Pan;
            map1.MouseWheel += onMouseWheel;
        }

        private void mapControl1_Resize(object sender, EventArgs e)
        {
            map1.ResizeMap(this.Width, this.Height);
        }

        /// <summary>
        /// 地图绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="convertor"></param>
        public void mapDraw(Graphics g, Convertor convertor)
        {
            double _minX = 99999999999;
            double _minY = 99999999999;
            double _maxX = -99999999999;
            double _maxY = -99999999999;
            if (NtripAccounts != null) {
           
            for (int i = 0; i < NtripAccounts.Count; i++)
            {
                Color color = Color.Green;
                Pen pen = new Pen(color, 5);
                double[] coord = CoordUtil.LonLat2Mercator(NtripAccounts[i].CurrentLon, NtripAccounts[i].CurrentLat);

                if (coord[0] < _minX)
                {
                    _minX = coord[0];
                }
                if (coord[0] > _maxX)
                {
                    _maxX = coord[0];
                }
                if (coord[1] < _minY)
                {
                    _minY = coord[1];
                }
                if (coord[1] > _maxY)
                {
                    _maxY = coord[1];
                }
                Point point = convertor.Grid2Screen(coord[0], coord[1]);
                try
                {
                    g.DrawRectangle(pen, point.X - 5, point.Y - 5, 5, 5);
                    Brush greenBrush = new SolidBrush(color);
                    g.FillRectangle(greenBrush, point.X - 5, point.Y - 5, 5, 5);
                    g.DrawString("基准站：" + NtripAccounts[i].Name, new Font("宋体", 10), new SolidBrush(color), point.X + 5, point.Y - 5);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }

            for (int i = 0; i < UserAccounts.Count; i++)
            {
                Color color = Color.Green;
                Pen pen = new Pen(color, 5);

                double[] coord = CoordUtil.LonLat2Mercator(UserAccounts[i].CurrentLon, UserAccounts[i].CurrentLat);

                if (coord[0] < _minX)
                {
                    _minX = coord[0];
                }
                if (coord[0] > _maxX)
                {
                    _maxX = coord[0];
                }
                if (coord[1] < _minY)
                {
                    _minY = coord[1];
                }
                if (coord[1] > _maxY)
                {
                    _maxY = coord[1];
                }
                Point point = convertor.Grid2Screen(coord[0], coord[1]);

                if (UserAccounts[i].ConnectStaion != null && UserAccounts[i].ConnectStaion != "")
                {
                    for (int m = 0; m < NtripAccounts.Count; m++)
                    {

                        if (UserAccounts[i].ConnectStaion == NtripAccounts[m].Name)
                        {
                            double[] coord1 = CoordUtil.LonLat2Mercator(NtripAccounts[m].CurrentLon, NtripAccounts[m].CurrentLat);
                            Point point2 = convertor.Grid2Screen(coord1[0], coord1[1]);
                            Pen pen1 = new Pen(Color.Gray, 1);
                            try
                            {
                                g.DrawLines(pen1, new PointF[] { point, point2 });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                    }
                }
                try
                {
                    g.DrawEllipse(pen, point.X, point.Y, 1, 1);
                    Brush greenBrush = new SolidBrush(color);
                    g.FillRectangle(greenBrush, point.X - 5, point.Y - 5, 5, 5);
                    g.DrawString("流动站：" + UserAccounts[i].Username, new Font("宋体", 10), new SolidBrush(color), point.X + 5, point.Y - 5);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            if ((_maxX - _minX) < 1)
            {
                _maxX += 0.5;
                _minX -= 0.5;
            }
            if ((_maxY - _minY) < 1)
            {
                _minY += 0.5;
                _maxY -= 0.5;
            }
            minX = (int)_minX;
            minY = (int)_minY;
            maxX = (int)_maxX;
            maxY = (int)_maxY;
            }
        }

        private void onMouseWheel(object o, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                map1.ZoomIn();

            }
            else
            {
                map1.ZoomOut();
            }

        }


        private void buttonXCur_Click(object sender, EventArgs e)
        {
            map1.toolMode = ToolModes.Pan;
            map1.Focus();
        }

        private void buttonXPan_Click(object sender, EventArgs e)
        {
            map1.toolMode = ToolModes.Pan;
            map1.Focus();
        }

        private void buttonXZoomIn_Click(object sender, EventArgs e)
        {
            map1.ZoomIn();
            map1.Focus();
        }

        private void buttonXZoomOut_Click(object sender, EventArgs e)
        {
            map1.ZoomOut();
            map1.Focus();
        }

        private void buttonXFullExtend_Click(object sender, EventArgs e)
        {
            map1.AutoAdjustView(minX, maxX, minY, maxY);
            map1.Focus();
        }

        public void AutoAdjustView()
        {
            map1.AutoAdjustView(minX, maxX, minY, maxY);
            map1.Focus();
        }

        private void buttonXGWang_Click(object sender, EventArgs e)
        {
            if (map1.IsDrawGrid)
            {
                map1.IsDrawGrid = false;
            }
            else
            {
                map1.IsDrawGrid = true;
            }
            map1.Refresh();
            map1.Focus();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            map1.toolMode = ToolModes.RegionZoomIn;
            map1.Focus();
        }

    }
}
