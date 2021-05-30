using DevComponents.DotNetBar.SuperGrid;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using DevComponents.DotNetBar.SuperGrid.Style;
using System.Runtime.Serialization.Formatters.Binary;
using NtripShare.NTRIP;
using System.Reflection;
using System.Diagnostics;
using NtripShare.Model;
using NtripShare.Util;
using NtripShare.Frm;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.ComponentModel;
using GMapUtil;
using System.Drawing;
using NtripClient.Util;

namespace NtripShare
{
    public partial class MainForm : Form
    {

        String RTCM30 = "STR;##;##;RTCM 3.0;1004(1),1012(1),1005(10);2;GNSS;EagleGnss;CHN;0.00;0.00;1;0;NtripShare2020;none;B;N;9600;";
        String RTCM32 = "STR;##;##;RTCM 3.2;1074(1),1084(1),1124(1),1005(5),1007(5),1033(5);2;GNSS;EagleGnss;CHN;0.00;0.00;1;1;NtripShare2020;none;B;N;19200;";
        LogUtil logUtil = new LogUtil();
        private List<NtripShare.NTRIP.NTRIPClient> poolNtripLists = new List<NtripShare.NTRIP.NTRIPClient>();
        private List<NtripShare.NTRIP.NTRIPClient> staticNtripLists = new List<NtripShare.NTRIP.NTRIPClient>();
        private List<string> NtripAccountInUseList = new List<string>();
        private List<string> NtripAccountErrorList = new List<string>();
        private List<TCPServer> tCPServers = new List<TCPServer>();

        // POI overlay
        private GMapOverlay poiOverlay = new GMapOverlay("poiOverlay");
        private GMapOverlay regionOverlay = new GMapOverlay("region");
        // China boundry
        public GMapChinaRegion.Country China;

        private Thread WebServerThread;
        ManageServer manageServer;

        private static MainForm defaultInstance;

        private DateTime PoolAlarmTime;
        private DateTime ConnectionAlartmTime;

        public bool isTcpOpen()
        {
            return switchButtonTCP.Value;
        }
        public bool isWebOpen()
        {
            return switchButtonWeb.Value;
        }


        public bool isServerOpen()
        {
            return switchButtonServer.Value;
        }

        public bool isNtripOpen()
        {
            return switchButtonNtrip.Value;
        }



        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static MainForm Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new MainForm();
                    defaultInstance.FormClosed += new FormClosedEventHandler(defaultInstance_FormClosed);
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }


        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        private MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加基站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBase_Click(object sender, EventArgs e)
        {
            Frm.FrmAccount frmAccount = new Frm.FrmAccount();
            frmAccount.StartPosition = FormStartPosition.CenterScreen;
            if (frmAccount.ShowDialog() == DialogResult.OK)
            {
                updateNtripAccount();
            }
        }

        /// <summary>
        /// 更新基准站
        /// </summary>
        public void updateNtripAccount()
        {
            sideBarBase.Groups.Clear();
            sideBarBase.AddGroup("账号池");
            sideBarBase.AddGroup("固定站");
            sideBarBase.ImageList = imageListListIcon;
            foreach (NtripAccount ntripAccount in DocumentSetting.Default.NtripAccounts.Values)
            {
                if (!ntripAccount.UseCoord)
                {
                    SideBar.SbItem item = new SideBar.SbItem(ntripAccount.Name, 0);
                    item.ImageIndex = 0;
                    item.Tag = ntripAccount.Name;
                    sideBarBase.Groups[0].Items.Add(item);
                }
                else
                {
                    SideBar.SbItem item = new SideBar.SbItem(ntripAccount.Name, 0);
                    item.ImageIndex = 0;
                    item.Tag = ntripAccount.Name;
                    sideBarBase.Groups[1].Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 更新TCP
        /// </summary>
        public void updateTCP()
        {
            sideBarTCP.Groups.Clear();
            sideBarTCP.AddGroup("TCP");
            sideBarTCP.ImageList = imageListListIcon;
            foreach (TCP tCP in DocumentSetting.Default.Tcps.Values)
            {
                SideBar.SbItem item = new SideBar.SbItem(tCP.Name, 0);
                item.ImageIndex = 0;
                item.Tag = tCP.Name;
                sideBarTCP.Groups[0].Items.Add(item);
            }
        }

        /// <summary>
        /// 添加监测站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            FrmTCP frmTCP = new FrmTCP();
            frmTCP.StartPosition = FormStartPosition.CenterScreen;
            if (frmTCP.ShowDialog() == DialogResult.OK)
            {
                updateTCP();
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sideNavItem7_Click(object sender, EventArgs e)
        {
            tabControlStatus.Visible = false;
            tabControl1.Visible = false;
        }

        /// <summary>
        /// 启动基站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchButton1_ValueChanged_1(object sender, EventArgs e)
        {
            lock (staticNtripLists)
            {
                if (switchButtonNtrip.Value)
                {
                    if (!DocumentSetting.Default.StartGuDingDynamic)
                    {
                        foreach (NtripAccount ntripAccount in DocumentSetting.Default.NtripAccounts.Values)
                        {
                            if (ntripAccount.UseCoord)
                            {
                                try
                                {
                                    LogEvent("正在启动固定站Ntrip服务，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.MountPoint);
                                    NTRIPClient client = new NTRIPClient(ntripAccount);
                                    client.StartNTRIP();
                                    staticNtripLists.Add(client);
                                    LogEvent("启动固定站Ntrip服务成功，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.MountPoint);
                                }
                                catch (Exception ex)
                                {
                                    LogEvent("启动Ntrip服务失败，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.MountPoint);
                                    //NtripAccountErrorList.Add(ntripAccount.Name);
                                }
                            }

                        }
                    }

                    addNtrip.Enabled = false;
                    removeNtrip.Enabled = false;
                }
                else
                {
                    foreach (NTRIPClient client in staticNtripLists)
                    {
                        client.StopNTRIP();
                    }
                    staticNtripLists.Clear();
                    FrmStatus.Default.NtripDataDic.Clear();
                    addNtrip.Enabled = true;
                    removeNtrip.Enabled = true;
                }
            }

        }



        private void sideNavItem2_Click_2(object sender, EventArgs e)
        {
            tabControlStatus.Visible = true;
            tabControl1.Visible = false;

        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sideNavItem5_Click(object sender, EventArgs e)
        {
            FrmSetting about = new FrmSetting();
            about.StartPosition = FormStartPosition.CenterScreen;
            about.ShowDialog();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sideNavItem6_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.StartPosition = FormStartPosition.CenterScreen;
            about.ShowDialog();
        }

        /// <summary>
        /// 删除基站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem16_Click(object sender, EventArgs e)
        {
            if (switchButtonNtrip.Value || switchButtonTCP.Value || switchButtonServer.Value)
            {
                MessageBox.Show("当前正在接收数据，请先关闭！");
                return;
            }
            for (int i = 0; i < sideBarBase.VisibleGroup.Items.Count; i++)
            {
                if (sideBarBase.VisibleGroup.Items[i].Pushed)
                {
                    if (MessageBox.Show("您确定要删除该账号吗？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string key = sideBarBase.VisibleGroup.Items[i].Tag.ToString();
                        DocumentSetting.Default.removeNtripAccount(key);

                        MessageBox.Show("删除Ntrip账号成功");
                        updateNtripAccount();
                        return;
                    }
                }
                MessageBox.Show("请先选中数据！");
            }
        }


        /// <summary>
        /// 删除监测站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem20_Click(object sender, EventArgs e)
        {
            if (switchButtonNtrip.Value || switchButtonTCP.Value || switchButtonServer.Value)
            {
                MessageBox.Show("当前正在接收数据，请先关闭！");
                return;
            }
            for (int i = 0; i < sideBarTCP.Groups[0].Items.Count; i++)
            {
                if (sideBarTCP.Groups[0].Items[i].Pushed)
                {
                    if (MessageBox.Show("您确定要删除该站点吗？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string name = sideBarTCP.Groups[0].Items[i].Tag.ToString();
                        DocumentSetting.Default.removeTcp(name);
                        updateTCP();
                        return;
                    }
                }
            }
            MessageBox.Show("请先选中数据！");
        }

        /// <summary>
        /// 发送报警邮件
        /// </summary>
        /// <param name="alarm"></param>
        private void sendAlarmMail(string alarmMsg)
        {
            try
            {

                EmailUtil email = new EmailUtil(DocumentSetting.Default.SmtpServer, DocumentSetting.Default.SmtpServerPort, DocumentSetting.Default.HostEmail, DocumentSetting.Default.HostEmailPassword);
                email.SendEmail(DocumentSetting.Default.AlarmEmail, "NtripShare 2020 监测报警", alarmMsg, "", "");
            }
            catch (Exception e)
            {
                LogEvent("发送报警邮件出错，" + e.Message);
            }
        }

        /// <summary>
        /// 编辑基站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sideBarBase.Groups[0].Items.Count; i++)
            {
                if (sideBarBase.Groups[0].Items[i].Pushed)
                {
                    return;
                }
            }

            MessageBox.Show("请先选中数据！");

        }

        /// <summary>
        /// 编辑监测站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sideBarTCP.Groups[0].Items.Count; i++)
            {
                if (sideBarTCP.Groups[0].Items[i].Pushed)
                {


                    return;
                }
            }
            MessageBox.Show("请先选中数据！");

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitChinaRegion();
            loadConfig();
            InitMap();
            updateNtripAccount();
            updateTCP();
            timerUpdate.Start();
            huiFu();
        }

        private void startWebServer()
        {

            manageServer = new ManageServer(DocumentSetting.Default.WebPort);
            manageServer.Start();
        }

        private void huiFu()
        {
            if (DocumentSetting.Default.ErrorClose)
            {
                if (DocumentSetting.Default.IsTcpOpen)
                {
                    switchButtonTCP.Value = true;
                }
                if (DocumentSetting.Default.IsNtripOpen)
                {
                    switchButtonNtrip.Value = true;
                }
                if (DocumentSetting.Default.IsServerOpen)
                {
                    switchButtonServer.Value = true;
                }
                if (DocumentSetting.Default.IsWebOpen)
                {
                    switchButtonWeb.Value = true;
                }
                DocumentSetting.Default.ErrorClose = false;
                DocumentSetting.Default.saveToFile();
            }
        }

        private void InitChinaRegion()
        {
            //异步加载中国省市边界
            BackgroundWorker loadChinaWorker = new BackgroundWorker();
            loadChinaWorker.DoWork += new DoWorkEventHandler(loadChinaWorker_DoWork);
            loadChinaWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadChinaWorker_RunWorkerCompleted);
            loadChinaWorker.RunWorkerAsync();
        }

        void loadChinaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (China == null)
            {
                LogEvent("加载中国省市边界失败！");
                return;
            }

        }

        public void showGeoFence(string name, string rings)
        {
            GMapPolygon polygon = GMapChinaRegion.ChinaMapRegion.GetRegionPolygon(name, rings);
            if (polygon != null)
            {
                GMapPolygonLib.GMapAreaPolygon areaPolygon = new GMapPolygonLib.GMapAreaPolygon(polygon.Points, name);
                RectLatLng rect = GMapUtil.PolygonUtils.GetRegionMaxRect(polygon);
                regionOverlay.Clear();
                regionOverlay.Polygons.Add(areaPolygon);
                this.gMapControl1.SetZoomToFitRect(rect);
            }
        }

        public void clearGeoFence()
        {
            regionOverlay.Clear();
        }

        void loadChinaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //byte[] buffer = Properties.Resources.ChinaBoundary_Province_City;
                byte[] buffer = Properties.Resources.ChinaBoundary;
                China = GMapChinaRegion.ChinaMapRegion.GetChinaRegionFromJsonBinaryBytes(buffer);
            }
            catch (Exception ex)
            {
                LogEvent(ex.Message);
            }
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        private void InitMap()
        {
            gMapControl1.ShowCenter = false;
            gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;
            gMapControl1.CacheLocation = Environment.CurrentDirectory + "\\MapCache\\"; // Map cache location
            //mapControl.MapProvider = GMapProviders.GoogleChinaMap;
            //gMapControl1.MapProvider = GMapProvidersExt.Baidu.BaiduMapProvider.Instance;
            gMapControl1.MapProvider = GMapProvidersExt.TianDitu.TiandituMapProviderWithAnno.Instance;
            gMapControl1.Position = new PointLatLng(35.043, 100.773);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 5;

            //gMapControl1.Overlays.Add(polygonsOverlay);
            //gMapControl1.Overlays.Add(regionOverlay);
            gMapControl1.Overlays.Add(poiOverlay);
            gMapControl1.Overlays.Add(regionOverlay);
            //gMapControl1.Overlays.Add(routeOverlay);


            //draw = new Draw(this.mapControl);
            //draw.DrawComplete += new EventHandler<DrawEventArgs>(draw_DrawComplete);

            //drawDistance = new DrawDistance(this.mapControl);
            //drawDistance.DrawComplete += new EventHandler<DrawDistanceEventArgs>(drawDistance_DrawComplete);
        }



        /// <summary>
        /// 加载配置信息
        /// </summary>
        private void loadConfig()
        {
            string file = Application.StartupPath + @"\config.dat";
            if (File.Exists(file))
            {
                try
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    DocumentSetting.Default = bf.Deserialize(fs) as DocumentSetting;
                    fs.Close();
                }
                catch (Exception e)
                {
                    LogEvent("加载配置文件失败！");
                    LogEvent(e.Message);
                }
            }

        }


        /// <summary>
        /// 更新状态定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            creatNew();
            updatePool();
            updateStation();
            this.dataGridViewConnections.AutoGenerateColumns = false;
            this.dataGridViewMountPoints.AutoGenerateColumns = false;
            this.dataGridViewPool.AutoGenerateColumns = false;
            List<UserAccount> userAccounts = new List<UserAccount>();
            userAccounts.AddRange(CasterForm.Default.connecedUserList);
            this.dataGridViewConnections.Rows.Clear();
            foreach (UserAccount account in userAccounts)
            {
                object[] data = new object[] { account.ConnectionID, account.Username, account.MountPoint, account.Address, account.ConnectStaion ,
                    account.ConnectTime ,account.UpdateTime, account.CurrentLat,account.CurrentLon,account.CurrentStatus,account.DataSize};
                dataGridViewConnections.Rows.Add(data);
            }

            List<MountPoint> mountPoints = new List<MountPoint>();
            mountPoints.AddRange(CasterForm.Default.mountpointList);
            this.dataGridViewMountPoints.Rows.Clear();
            foreach (MountPoint mount in mountPoints)
            {
                object[] data = new object[] { mount.LeiXing, mount.JieRuDian, mount.ShiBieHao, mount.ChaFenGeShi, mount.PinLv, mount.ZaiBoXiangWei, mount.DaoHangXiTong,
                mount.WangLuo,mount.GuoJia,mount.WeiDu,mount.JingDu,mount.NMEA,mount.JiZhanLeiXing,mount.RuanJianMingCheng,mount.YaSuoSuanFa,mount.FangWenBaoHu,mount.YN,mount.BiTeLv};
                dataGridViewMountPoints.Rows.Add(data);
            }

            List<NtripAccount> ntripAccounts = new List<NtripAccount>();
            dataGridViewPool.Rows.Clear();
            foreach (NTRIPClient client in poolNtripLists)
            {
                object[] data = new object[] { client.NtripAccount.Name, client.NtripAccount.Username, client.NtripAccount.StartTime,
                     client.NtripAccount.ConnectCount,   client.NtripAccount.DataSize,
                    client.NtripAccount.CurrentLon, client.NtripAccount.CurrentLat, client.NtripAccount.IP, client.NtripAccount.Port };
                dataGridViewPool.Rows.Add(data);
                ntripAccounts.Add(client.NtripAccount);
            }
            int size = 0;
            //if (this.mapControl1.NtripAccounts != null)
            //{
            //    size = this.mapControl1.NtripAccounts.Count;
            //}

            //this.mapControl1.NtripAccounts = ntripAccounts;
            //this.mapControl1.UserAccounts = userAccounts;
            //if (size != ntripAccounts.Count)
            //{
            //    //this.mapControl1.AutoAdjustView();
            //}
            SystemStatus.Default.UserAccounts = userAccounts;
            SystemStatus.Default.NtripAccounts = ntripAccounts;
            updateStatus();
            updateMap(userAccounts, ntripAccounts);
        }

        /// <summary>
        /// 更新地图
        /// </summary>
        /// <param name="userAccounts"></param>
        /// <param name="ntripAccounts"></param>
        private void updateMap(List<UserAccount> userAccounts, List<NtripAccount> ntripAccounts)
        {
            this.poiOverlay.Markers.Clear();
            this.poiOverlay.Routes.Clear();
            this.poiOverlay.Polygons.Clear();
            for (int i = 0; i < tCPServers.Count; i++)
            {
                if (CoordUtil.isInChina(tCPServers[i].TCP.Lat, tCPServers[i].TCP.Lon))
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(tCPServers[i].TCP.Lat, tCPServers[i].TCP.Lon), GMarkerGoogleType.orange_dot);
                    marker.ToolTipText = "名称:" + tCPServers[i].TCP.Name;
                    this.poiOverlay.Markers.Add(marker);
                    GMapRoute gMapPolygon = PolygonUtils.CreateCircleRoute(new PointLatLng(tCPServers[i].TCP.Lat, tCPServers[i].TCP.Lon),
                        tCPServers[i].TCP.BufferSize, tCPServers[i].TCP.Name);
                    gMapPolygon.Stroke = new Pen(Brushes.Orange, 1);
                    this.poiOverlay.Routes.Add(gMapPolygon);
                }
            }

            foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
            {
                if (DocumentSetting.Default.NtripAccounts[key].UseCoord && CoordUtil.isInChina(DocumentSetting.Default.NtripAccounts[key].StationLat,
                    DocumentSetting.Default.NtripAccounts[key].StationLon))
                {
                    GMarkerGoogle marker = new GMarkerGoogle(
                        new PointLatLng(DocumentSetting.Default.NtripAccounts[key].StationLat,
                    DocumentSetting.Default.NtripAccounts[key].StationLon), GMarkerGoogleType.red_dot);
                    marker.ToolTipText = "名称:" + DocumentSetting.Default.NtripAccounts[key].Name;
                    this.poiOverlay.Markers.Add(marker);

                    GMapRoute gMapPolygon = PolygonUtils.CreateCircleRoute(new PointLatLng(DocumentSetting.Default.NtripAccounts[key].StationLat,
                    DocumentSetting.Default.NtripAccounts[key].StationLon), DocumentSetting.Default.NtripAccounts[key].BufferSize,
                    DocumentSetting.Default.NtripAccounts[key].Name);
                    gMapPolygon.Stroke = new Pen(Brushes.Red, 1);
                    this.poiOverlay.Routes.Add(gMapPolygon);
                }
            }


            for (int i = 0; i < ntripAccounts.Count; i++)
            {
                if (CoordUtil.isInChina(ntripAccounts[i].StationLat, ntripAccounts[i].StationLon))
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(ntripAccounts[i].StationLat, ntripAccounts[i].StationLon), GMarkerGoogleType.purple_dot);
                    marker.ToolTipText = "名称:" + ntripAccounts[i].Name + "\r\n开始时间：" + ntripAccounts[i].StartTime +
                        "\r\n连接数：" + ntripAccounts[i].ConnectCount + "\r\n数据量：" + ntripAccounts[i].DataSize;
                    this.poiOverlay.Markers.Add(marker);

                    GMapRoute gMapPolygon = PolygonUtils.CreateCircleRoute(new PointLatLng(ntripAccounts[i].StationLat, ntripAccounts[i].StationLat), ntripAccounts[i].BufferSize, ntripAccounts[i].Name);
                    gMapPolygon.Stroke = new Pen(Brushes.Purple, 1);
                    this.poiOverlay.Routes.Add(gMapPolygon);
                }
            }
            for (int i = 0; i < userAccounts.Count; i++)
            {
                if (CoordUtil.isInChina(userAccounts[i].CurrentLat, userAccounts[i].CurrentLon))
                {
                    GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(userAccounts[i].CurrentLat, userAccounts[i].CurrentLon), GMarkerGoogleType.red_small);
                    marker.ToolTipText = "名称:" + userAccounts[i].Username + "\r\n连接时间：" + userAccounts[i].ConnectTime +
                        "\r\n状态：" + userAccounts[i].CurrentStatus + "\r\n接入点：" + userAccounts[i].MountPoint + "\r\n数据量：" + userAccounts[i].DataSize;
                    this.poiOverlay.Markers.Add(marker);
                }
            }
            this.gMapControl1.Refresh();
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        private void updateStatus()
        {
            try
            {
                labelConnect.Text = "连接数：" + CasterForm.Default.connecedUserList.Count + "/" + DocumentSetting.Default.MaxConnectionCount;
                progressBarConnect.Value = CasterForm.Default.connecedUserList.Count * 100 / DocumentSetting.Default.MaxConnectionCount;
                int num = getPoolAccountNum();
                labelPool.Text = "账号池：" + poolNtripLists.Count + "/" + num;
                if (num != 0)
                {
                    progressBarPool.Value = poolNtripLists.Count * 100 / num;
                }

                SystemStatus.Default.PoolCount = num;
                SystemStatus.Default.PoolUseCount = poolNtripLists.Count;

                if (DocumentSetting.Default.IsEmailAlarm && DocumentSetting.Default.IsPoolAlarm)
                {
                    if (SystemStatus.Default.PoolCount == SystemStatus.Default.PoolUseCount)
                    {
                        if (PoolAlarmTime == null || PoolAlarmTime < DateTime.Now.AddMinutes(-DocumentSetting.Default.AlarmInterval))
                        {
                            sendAlarmMail("NtripShare账号池资源已耗尽！");
                        }
                    }
                }
                num = getNtripAccountNum();
                labelNtrip.Text = "固定站：" + staticNtripLists.Count + "/" + num;
                if (num != 0)
                {
                    progressBarNtrip.Value = staticNtripLists.Count * 100 / num;
                }
                SystemStatus.Default.NtripCount = num;
                SystemStatus.Default.NtripUseCount = staticNtripLists.Count;
                labelUser.Text = "登录用户数：" + CasterForm.Default.connecedUserList.Count + "/" + DocumentSetting.Default.UserAccounts.Count;
                if (DocumentSetting.Default.UserAccounts.Count != 0)
                {
                    progressBarUser.Value = CasterForm.Default.connecedUserList.Count * 100 / DocumentSetting.Default.UserAccounts.Count;
                }

                SystemStatus.Default.UserCount = DocumentSetting.Default.UserAccounts.Count;
                SystemStatus.Default.LoginUserCount = CasterForm.Default.connecedUserList.Count;
                labelTCP.Text = "TCP数：" + tCPServers.Count + "/" + DocumentSetting.Default.Tcps.Count;
                if (DocumentSetting.Default.Tcps.Count != 0)
                {
                    progressBarTCP.Value = tCPServers.Count * 100 / DocumentSetting.Default.Tcps.Count;
                }

               

                SystemStatus.Default.TCPUseCount = tCPServers.Count;
                SystemStatus.Default.TCPCount = DocumentSetting.Default.Tcps.Count;
                if (DocumentSetting.Default.IsEmailAlarm && DocumentSetting.Default.IsConnectionAlarm)
                {
                    if (SystemStatus.Default.TCPUseCount == SystemStatus.Default.TCPCount)
                    {
                        if (ConnectionAlartmTime == null || ConnectionAlartmTime < DateTime.Now.AddMinutes(-DocumentSetting.Default.AlarmInterval))
                        {
                            sendAlarmMail("NtripShare用户连接数已达上限，上限限制" + SystemStatus.Default.TCPCount);
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int getPoolAccountNum()
        {
            int num = 0;
            foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
            {
                if (!DocumentSetting.Default.NtripAccounts[key].UseCoord)
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int getNtripAccountNum()
        {
            int num = 0;
            foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
            {
                if (DocumentSetting.Default.NtripAccounts[key].UseCoord)
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>
        /// 创建账号池
        /// </summary>
        private void creatNew()
        {
            for (int i = 0; i < CasterForm.Default.connecedUserList.Count; i++)
            {
                UserAccount userAccount = CasterForm.Default.connecedUserList[i];
                bool haveStation = false;
                if (DocumentSetting.Default.PoolName == userAccount.MountPoint)
                {
                    if (!CoordUtil.isInChina(userAccount.CurrentLat, userAccount.CurrentLon))
                    {
                        break;
                    }
                    foreach (TCPServer tCPServer in tCPServers)
                    {
                        //if (tCPServer.TCP.AddToPool) {
                        double dis = CoordUtil.GetDistance(tCPServer.TCP.Lat, tCPServer.TCP.Lon, userAccount.CurrentLat, userAccount.CurrentLon);
                        if (dis < tCPServer.TCP.BufferSize)
                        {
                            userAccount.ConnectStaion = tCPServer.TCP.Name;
                            haveStation = true;
                            break;
                        }
                        //}
                    }
                    if (!haveStation)
                    {
                        foreach (NTRIPClient client in staticNtripLists)
                        {
                            if (client.NtripAccount.UseCoord)
                            {
                                double dis = CoordUtil.GetDistance(client.NtripAccount.StationLat, client.NtripAccount.StationLon, userAccount.CurrentLat, userAccount.CurrentLon);
                                if (dis < client.NtripAccount.BufferSize)
                                {
                                    userAccount.ConnectStaion = client.NtripAccount.Name;
                                    haveStation = true;
                                    break;
                                }
                            }

                        }
                    }

                    if (!haveStation)
                    {
                        foreach (NTRIPClient client in poolNtripLists)
                        {
                            if (!client.NtripAccount.UseCoord)
                            {
                                double dis = CoordUtil.GetDistance(client.NtripAccount.CurrentLat, client.NtripAccount.CurrentLon, userAccount.CurrentLat, userAccount.CurrentLon);
                                if (dis < client.NtripAccount.BufferSize)
                                {
                                    userAccount.ConnectStaion = client.NtripAccount.Name;
                                    haveStation = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!haveStation)
                    {
                        if (DocumentSetting.Default.StartGuDingDynamic)
                        {
                            foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
                            {
                                if (DocumentSetting.Default.NtripAccounts[key].UseCoord &&
                                    CoordUtil.isInChina(DocumentSetting.Default.NtripAccounts[key].StationLat,
                                    DocumentSetting.Default.NtripAccounts[key].StationLon))
                                {
                                    double dis = CoordUtil.GetDistance(DocumentSetting.Default.NtripAccounts[key].StationLat,
                                        DocumentSetting.Default.NtripAccounts[key].StationLon, userAccount.CurrentLat, userAccount.CurrentLon);
                                    if (dis < DocumentSetting.Default.NtripAccounts[key].BufferSize)
                                    {
                                        startClient(DocumentSetting.Default.NtripAccounts[key]);
                                        userAccount.ConnectStaion = DocumentSetting.Default.NtripAccounts[key].Name;
                                        haveStation = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!haveStation)
                        {
                            AddPoolClient(userAccount);
                        }
                    }
                }
                else
                {
                    foreach (TCPServer client in tCPServers)
                    {
                        if (client.TCP.ShareMountPoint == userAccount.MountPoint)
                        {
                            userAccount.ConnectStaion = client.TCP.Name;
                            haveStation = true;
                            break;
                        }
                    }
                    foreach (NTRIPClient client in staticNtripLists)
                    {
                        if (client.NtripAccount.ShareMountPoint == userAccount.MountPoint)
                        {
                            userAccount.ConnectStaion = client.NtripAccount.Name;
                            haveStation = true;
                            break;
                        }
                    }
                    if (!haveStation)
                    {
                        foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
                        {
                            if (DocumentSetting.Default.NtripAccounts[key].CreatMountPoint &&
                                DocumentSetting.Default.NtripAccounts[key].ShareMountPoint == userAccount.MountPoint)
                            {

                                startClient(DocumentSetting.Default.NtripAccounts[key]);
                                userAccount.ConnectStaion = DocumentSetting.Default.NtripAccounts[key].Name;
                                break;
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 启动固定站
        /// </summary>
        /// <param name="MountPoint"></param>
        private void startClient(NtripAccount ntripAccount)
        {
            try
            {
                LogEvent("启动固定站Ntrip服务，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.ShareMountPoint);
                NTRIPClient client = new NTRIPClient(ntripAccount);
                client.StartNTRIP();
                staticNtripLists.Add(client);
                LogEvent("启动固定站Ntrip服务成功，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.ShareMountPoint);
            }
            catch (Exception ex)
            {
                LogEvent("启动Ntrip服务失败，IP " + ntripAccount.IP + "端口 " + ntripAccount.Port + "接入点 " + ntripAccount.ShareMountPoint);
                //NtripAccountErrorList.Add(ntripAccount.Name);
            }
        }

        /// <summary>
        /// 更新固定站
        /// </summary>
        private void updateStation()
        {
            lock (staticNtripLists)
            {
                for (int i = 0; i < staticNtripLists.Count; i++)
                {
                    NTRIPClient client = staticNtripLists[i];
                    int count = 0;
                    double lat = 0;
                    double lon = 0;
                    getStationInfo(client, ref count, ref lat, ref lon);

                    if (count != 0)
                    {
                        client.NtripAccount.ConnectCount = count;
                        client.NtripAccount.CurrentLat = lat;
                        client.NtripAccount.CurrentLon = lon;
                        if (client.NtripAccount.UseCoord)
                        {
                            Random Rdm = new Random();
                            double d = Rdm.NextDouble() / 10000;
                            client.NtripAccount.CurrentLat = client.NtripAccount.StationLat + d;
                            client.NtripAccount.CurrentLon = client.NtripAccount.StationLon + d;

                        }
                        if (!double.IsNaN(client.NtripAccount.CurrentLat) && !double.IsNaN(client.NtripAccount.CurrentLon))
                        {
                            client.sendGGA();
                        }

                    }
                    else
                    {
                        if (client.NtripAccount.UseCoord)
                        {
                            Random Rdm = new Random();
                            double d = Rdm.NextDouble() / 10000;
                            client.NtripAccount.CurrentLat = client.NtripAccount.StationLat + d;
                            client.NtripAccount.CurrentLon = client.NtripAccount.StationLon + d;
                            if (!double.IsNaN(client.NtripAccount.CurrentLat) && !double.IsNaN(client.NtripAccount.CurrentLon))
                            {
                                client.sendGGA();
                            }
                        }
                        if (DocumentSetting.Default.StartGuDingDynamic)
                        {
                            LogEvent("回收固定站账号资源：" + client.NtripAccount.Name);
                            client.StopNTRIP();
                            staticNtripLists.Remove(client);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 更新账号池，发送GGA
        /// </summary>
        private void updatePool()
        {
            for (int i = 0; i < poolNtripLists.Count; i++)
            {
                NTRIPClient client = poolNtripLists[i];
                int count = 0;
                double lat = 0;
                double lon = 0;
                getStationInfo(client, ref count, ref lat, ref lon);
                if (count == 0)
                {
                    LogEvent("账号池回收账号资源：" + client.NtripAccount.Name);
                    DocumentSetting.Default.NtripAccounts[client.NtripAccount.Name].CurrentLat = client.NtripAccount.CurrentLat;
                    DocumentSetting.Default.NtripAccounts[client.NtripAccount.Name].CurrentLon = client.NtripAccount.CurrentLon;
                    DocumentSetting.Default.NtripAccounts[client.NtripAccount.Name].LastTime = DateTime.Now;
                    client.StopNTRIP();
                    poolNtripLists.Remove(client);
                    NtripAccountInUseList.Remove(client.NtripAccount.Name);

                }
                else
                {
                    client.NtripAccount.ConnectCount = count;
                    client.NtripAccount.CurrentLat = lat;
                    client.NtripAccount.CurrentLon = lon;
                    if (!double.IsNaN(client.NtripAccount.CurrentLat) && !double.IsNaN(client.NtripAccount.CurrentLon))
                    {
                        client.sendGGA();
                    }
                }
            }
        }

        /// <summary>
        /// 计算基站参数
        /// </summary>
        /// <param name="client"></param>
        /// <param name="count"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        private void getStationInfo(NTRIPClient client, ref int count, ref double lat, ref double lon)
        {
            count = 0;
            int num = 0;
            double x = 0;
            double y = 0;
            foreach (UserAccount userAccount in CasterForm.Default.connecedUserList)
            {
                if (userAccount.ConnectStaion == client.NtripAccount.Name)
                {
                    count++;
                    if (userAccount.CurrentLat != 0 && userAccount.CurrentLon != 0 && CoordUtil.isInChina(userAccount.CurrentLat, userAccount.CurrentLon))
                    {
                        x += userAccount.CurrentLat;
                        y += userAccount.CurrentLon;
                        num++;
                    }
                }
            }
            lat = x / num;
            lon = y / num;
        }


        /// <summary>
        /// 创建账号池
        /// </summary>
        /// <param name="userAccount"></param>
        private void AddPoolClient(UserAccount userAccount)
        {
            LogEvent("登录账户" + userAccount.Username + "未匹配数据源，账号池开始创建数据源");
            foreach (string key in DocumentSetting.Default.NtripAccounts.Keys)
            {
                if (!NtripAccountInUseList.Contains(DocumentSetting.Default.NtripAccounts[key].Name)
                    && !NtripAccountErrorList.Contains(DocumentSetting.Default.NtripAccounts[key].Name)
                    && !DocumentSetting.Default.NtripAccounts[key].UseCoord)
                {

                    if (DocumentSetting.Default.NtripAccounts[key].LastTime != null)
                    {
                        double dis = CoordUtil.GetDistance(DocumentSetting.Default.NtripAccounts[key].CurrentLat,
                        DocumentSetting.Default.NtripAccounts[key].CurrentLon, userAccount.CurrentLat, userAccount.CurrentLon);
                        if (DocumentSetting.Default.NtripAccounts[key].LastTime.AddHours(DocumentSetting.Default.CoolTime) > DateTime.Now
                            && dis > DocumentSetting.Default.NtripAccounts[key].BufferSize)
                        {
                            continue;
                        }
                    }
                    try
                    {
                        LogEvent("账号池创建" + DocumentSetting.Default.NtripAccounts[key].Name + "数据源");
                        NTRIPClient nTRIPClient = new NTRIPClient(DocumentSetting.Default.NtripAccounts[key]);
                        nTRIPClient.NtripAccount.CurrentLat = userAccount.CurrentLat;
                        nTRIPClient.NtripAccount.CurrentLon = userAccount.CurrentLon;
                        nTRIPClient.StartNTRIP();
                        nTRIPClient.NtripAccount.StartTime = DateTime.Now;
                        userAccount.ConnectStaion = nTRIPClient.NtripAccount.Name;
                        NtripAccountInUseList.Add(DocumentSetting.Default.NtripAccounts[key].Name);
                        poolNtripLists.Add(nTRIPClient);
                        LogEvent("账号池创建数据源成功，已用账号数量为" + NtripAccountInUseList.Count + "个,剩余账号数量为" +
                            (getPoolAccountNum() - NtripAccountInUseList.Count) + "个");
                    }
                    catch (Exception e)
                    {
                        LogEvent("启动Ntrip服务失败，IP " + DocumentSetting.Default.NtripAccounts[key].IP + "端口 " + DocumentSetting.Default.NtripAccounts[key].Port + "接入点 " + DocumentSetting.Default.NtripAccounts[key].ShareMountPoint);
                        NtripAccountErrorList.Add(DocumentSetting.Default.NtripAccounts[key].Name);
                    }

                    return;
                }
            }
            LogEvent("账号池创建数据源失败，账号资源已全部使用");
        }

        /// <summary>
        /// tcp连接狗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void comboBoxEx5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 点击选择基准站
        /// </summary>
        /// <param name="e"></param>
        private void sideBarBase_ItemClick(SideBar.SbItemEventArgs e)
        {
            for (int i = 0; i < sideBarBase.Groups[0].Items.Count; i++)
            {
                sideBarBase.Groups[0].Items[i].Pushed = false;
            }
            e.Item.Pushed = true;
        }

        /// <summary>
        /// 点击流动站
        /// </summary>
        /// <param name="e"></param>
        private void sideBarRover_ItemClick(SideBar.SbItemEventArgs e)
        {
            for (int i = 0; i < sideBarTCP.Groups[0].Items.Count; i++)
            {
                sideBarTCP.Groups[0].Items[i].Pushed = false;
            }

            e.Item.Pushed = true;
        }




        /// 定期查询星历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSp3_Tick(object sender, EventArgs e)
        {
            this.switchButtonServer.Value = false;
            this.switchButtonServer_ValueChanged(null, null);
            MessageBox.Show("试用期已结束，请联系作者giserpeng@163.com");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (NTRIPClient client in staticNtripLists)
            {
                client.StopNTRIP();
            }
            staticNtripLists.Clear();
            FrmStatus.Default.NtripDataDic.Clear();

            for (int i = 0; i < tCPServers.Count; i++)
            {
                tCPServers[i].StopServer();
            }
            tCPServers.Clear();
            FrmStatus.Default.TCPDataDic.Clear();

            for (int i = 0; i < poolNtripLists.Count; i++)
            {
                poolNtripLists[i].StopNTRIP();
            }
            poolNtripLists.Clear();
            NtripAccountErrorList.Clear();
            NtripAccountInUseList.Clear();
            if (WebServerThread != null)
            {
                manageServer.Stop();
                WebServerThread.Abort();
                WebServerThread = null;
            }

            CasterForm.Default.StopServer();
            FrmStatus.Default.TCPDataDic.Clear();

            if (ProxyProcess != null) {
                stopProxy();
            }

            timerDog.Stop();
            //timerSp3.Stop();
            timerStatus.Stop();

        }

        /// <summary>
        /// 界面显示日志
        /// </summary>
        /// <param name="Message"></param>
        public void LogEvent(string Message)
        {
            if (rtbEvents.TextLength > 5000)
            {
                string NewText = rtbEvents.Text.Substring(999); //Drop first 1000 characters
                NewText = NewText.Remove(0, System.Convert.ToInt32(NewText.IndexOf(Strings.ChrW(10)) + 1)); //Drop up to the next new line
                rtbEvents.Text = NewText;
            }

            rtbEvents.AppendText("\r\n" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " - " + Message);
            rtbEvents.SelectionStart = rtbEvents.TextLength;
            rtbEvents.ScrollToCaret();

            if (DocumentSetting.Default.WriteEventsToFile)
            {
                logUtil.WriteLog(Message);
            }
        }

        /// <summary>
        /// socket日志回调
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="ConnID"></param>
        /// <param name="Message"></param>
        public void LogToUIThread(int Action, string Message)
        {
            try
            {
                LogToUIThreadDelegate uidel = new LogToUIThreadDelegate(LogOnUIThread);
                object[] o = new object[2];
                o[0] = Action;
                o[1] = Message;
                Invoke(uidel, o);
            }
            catch (Exception e)
            {
            }
        }
        delegate void LogToUIThreadDelegate(int Action, string Message);
        private void LogOnUIThread(int Action, string Message)
        {
            switch (Action)
            {
                case 0: //On start up, list IP and port
                    LogEvent(Message);
                    break;
                case 1: //On start up, list IP and port
                    //updateStatus();
                    break;
            }
        }



        /// <summary>
        /// socket数据回调
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="id"></param>
        /// <param name="myBytes"></param>
        public void UpdateDataUIThread(int Item, object account, byte[] myBytes)
        {
            try
            {
                UpdateUIThreadDelegate uidel = new UpdateUIThreadDelegate(DataCallBacktoUIThread);
                object[] o = new object[3];
                o[0] = Item;
                o[1] = account;
                o[2] = myBytes;
                Invoke(uidel, o);
            }
            catch (Exception)
            {
            }
        }
        delegate void UpdateUIThreadDelegate(int Item, object account, byte[] myBytes);
        /// <summary>
        /// 处理流数据
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="id"></param>
        /// <param name="myBytes"></param>
        public void DataCallBacktoUIThread(int Item, object account, byte[] myBytes)
        {
            switch (Item)
            {
                case 0:
                    NtripAccount ntripAccount = account as NtripAccount;
                    //if (!ntripAccount.UseCoord)
                    //{
                    CasterForm.Default.SendBytesToCasterThread(ntripAccount.Name, DocumentSetting.Default.PoolName, myBytes);
                    FrmStatus.Default.addPoolData(ntripAccount.Name, myBytes.Length);
                    //}
                    if (ntripAccount.CreatMountPoint)
                    {
                        CasterForm.Default.SendBytesToCasterThread(ntripAccount.ShareMountPoint, myBytes);
                        FrmStatus.Default.addNtripData(ntripAccount.Name, myBytes.Length);
                    }
                    break;
                case 1:///tcp
                    TCP tcp = account as TCP;
                    if (tcp.CreateMountPoint)
                    {
                        CasterForm.Default.SendBytesToCasterThread(tcp.ShareMountPoint, myBytes);
                    }

                    //if (tcp.AddToPool) {
                    CasterForm.Default.SendBytesToCasterThread(tcp.Name, DocumentSetting.Default.PoolName, myBytes);
                    //}
                    FrmStatus.Default.addTCPData(tcp.Name, myBytes.Length);
                    break;
                case 99:///tcp
                    NTRIPClient client = account as NTRIPClient;
                    client.StopNTRIP();

                    if (!client.NtripAccount.UseCoord)
                    {
                        NtripAccountInUseList.Remove(client.NtripAccount.Name);
                        poolNtripLists.Remove(client);
                        NtripAccountErrorList.Add(client.NtripAccount.Name);
                    }
                    else
                    {
                        staticNtripLists.Remove(client);
                    }

                    break;
            }
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sideBarBase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < sideBarBase.Groups[0].Items.Count; i++)
            {
                if (sideBarBase.Groups[0].Items[i].Pushed)
                {
                    string name = sideBarBase.Groups[0].Items[i].Tag.ToString();
                    NtripAccount ntripAccount = DocumentSetting.Default.NtripAccounts[name];

                    Frm.FrmAccount frmAccount = new Frm.FrmAccount();
                    frmAccount.NtripAccount = ntripAccount;
                    frmAccount.StartPosition = FormStartPosition.CenterScreen;
                    if (frmAccount.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("编辑Ntrip账号成功");
                        updateNtripAccount();
                    }
                }
            }

            for (int i = 0; i < sideBarBase.Groups[1].Items.Count; i++)
            {
                if (sideBarBase.Groups[1].Items[i].Pushed)
                {
                    string name = sideBarBase.Groups[1].Items[i].Tag.ToString();

                    NtripAccount ntripAccount = DocumentSetting.Default.NtripAccounts[name];

                    Frm.FrmAccount frmAccount = new Frm.FrmAccount();
                    frmAccount.NtripAccount = ntripAccount;
                    frmAccount.StartPosition = FormStartPosition.CenterScreen;
                    if (frmAccount.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("编辑Ntrip账号成功");
                        updateNtripAccount();
                    }
                }
            }
        }

        /// <summary>
        /// 双击监测站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sideBarRover_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < sideBarTCP.Groups[0].Items.Count; i++)
            {
                if (sideBarTCP.Groups[0].Items[i].Pushed)
                {
                    string name = sideBarTCP.Groups[0].Items[i].Tag.ToString();

                    TCP tCP = DocumentSetting.Default.Tcps[name];

                    Frm.FrmTCP frmTCP = new Frm.FrmTCP();
                    frmTCP.TCP = tCP;
                    frmTCP.StartPosition = FormStartPosition.CenterScreen;
                    if (frmTCP.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("编辑TCP成功");
                        updateTCP();
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchButtonServer_ValueChanged(object sender, EventArgs e)
        {
            if (switchButtonServer.Value)
            {
                
                if (IsPortOccupedFun2(DocumentSetting.Default.ServerPort))
                {
                    MessageBox.Show("端口" + DocumentSetting.Default.ServerPort + "被占用");
                    return;
                }

                string mountStr = RTCM32.Replace("##", DocumentSetting.Default.PoolName);
                CasterForm.Default.mountpointList.Clear();
                CasterForm.Default.addMountPoint(mountStr);
                foreach (NtripAccount ntripAccount in DocumentSetting.Default.NtripAccounts.Values)
                {
                    if (ntripAccount.CreatMountPoint)
                    {
                        CasterForm.Default.addMountPoint(RTCM32.Replace("##", ntripAccount.ShareMountPoint));
                    }
                }
                foreach (TCP tCP in DocumentSetting.Default.Tcps.Values)
                {
                    if (tCP.CreateMountPoint)
                    {
                        CasterForm.Default.addMountPoint(RTCM32.Replace("##", tCP.ShareMountPoint));
                    }

                }
                CasterForm.Default.port = DocumentSetting.Default.ServerPort;
                CasterForm.Default.StartServer();

                if (DocumentSetting.Default.ProxyEnable) {
                    FileProperties fileProperties = new FileProperties(Application.StartupPath + "\\proxy\\conf\\config.properties");

                    //fileProperties["client.key"] = recode;
                    fileProperties["server.host"] = DocumentSetting.Default.ProxyIp;
                    fileProperties["server.port"] = DocumentSetting.Default.ProxyPort;
                    fileProperties.save(Application.StartupPath + "\\proxy\\conf\\config.properties");
                    startProxy();
                }
            }
            else
            {
                CasterForm.Default.StopServer();
                FrmStatus.Default.TCPDataDic.Clear();
                for (int i = 0; i < poolNtripLists.Count; i++)
                {
                    poolNtripLists[i].StopNTRIP();
                }
                poolNtripLists.Clear();
                NtripAccountErrorList.Clear();
                NtripAccountInUseList.Clear();
                stopProxy();
            }
        }

        Process ProxyProcess = null;
        /// <summary>
        /// 启动代理服务器
        /// </summary>
        private void startProxy() {
            try
            {
              
                if (ProxyProcess != null)
                {
                    stopProxy();
                }

                ProxyProcess = new Process();
                ProxyProcess.StartInfo.FileName = Application.StartupPath + "\\proxy\\bin\\startup.bat";
                ProxyProcess.StartInfo.WorkingDirectory = Application.StartupPath + "\\proxy\\bin\\";
                ProxyProcess.StartInfo.CreateNoWindow = true;
                ProxyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
                ProxyProcess.Start();
                LogEvent("启动代理服务器成功！");
                //ProxyProcess.WaitForExit();

            }
            catch (Exception e) {
                LogEvent("启动代理服务器出错！");
            }
        }
        /// <summary>
        /// 关闭代理服务器
        /// </summary>
        private void stopProxy()
        {
            if (ProxyProcess != null) {
                try
                {
                    ProxyProcess.Close();
                    //ProxyProcess.Kill();
                    ProxyProcess = null;
                    LogEvent("关闭代理服务器成功！");
                }
                catch (Exception e) {
                    LogEvent("关闭代理服务器出错！");
                }
            }
        }

        /// <summary>
        /// 判断指定端口号是否被占用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static Boolean IsPortOccupedFun2(Int32 port)
        {
            Boolean result = false;
            try
            {
                System.Net.NetworkInformation.IPGlobalProperties iproperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
                System.Net.IPEndPoint[] ipEndPoints = iproperties.GetActiveTcpListeners();
                foreach (var item in ipEndPoints)
                {
                    if (item.Port == port)
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private void sideNavItemUser_Click(object sender, EventArgs e)
        {
            FrmUserList frmUserList = new FrmUserList();
            frmUserList.StartPosition = FormStartPosition.CenterScreen;
            frmUserList.ShowDialog();
        }

        /// <summary>
        /// 启动TCP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchButtonTCP_ValueChanged(object sender, EventArgs e)
        {
            if (switchButtonTCP.Value)
            {
                foreach (TCP tCP in DocumentSetting.Default.Tcps.Values)
                {
                    if (IsPortOccupedFun2(tCP.Port))
                    {
                        LogEvent("端口" + tCP.Port + "已被占用");
                        continue;
                    }
                    TCPServer tCPServer = new TCPServer(tCP);
                    tCPServer.StartServer();
                    tCPServers.Add(tCPServer);
                }
                addTCP.Enabled = false;
                removeTCP.Enabled = false;
            }
            else
            {
                for (int i = 0; i < tCPServers.Count; i++)
                {
                    tCPServers[i].StopServer();
                }
                tCPServers.Clear();
                FrmStatus.Default.TCPDataDic.Clear();
                addTCP.Enabled = true;
                removeTCP.Enabled = true;
            }
        }

        private void buttonXDetail_Click(object sender, EventArgs e)
        {
            FrmStatus frmStatus = FrmStatus.Default;
            frmStatus.StartPosition = FormStartPosition.CenterScreen;
            frmStatus.ShowDialog();
        }

        /// <summary>
        /// rtk解算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer30_Tick(object sender, EventArgs e)
        {

        }

        private void labelX3_Click(object sender, EventArgs e)
        {
            NtripAccount n = null;
            n.BufferSize += 1;
        }

        private void switchButtonWeb_ValueChanged(object sender, EventArgs e)
        {
            if (switchButtonWeb.Value)
            {
                if (IsPortOccupedFun2(DocumentSetting.Default.WebPort))
                {
                    MessageBox.Show("端口" + DocumentSetting.Default.ServerPort + "被占用");
                    switchButtonWeb.Value = false;
                    return;
                }
                LogEvent("正在启动Web Server 管理端，端口" + DocumentSetting.Default.WebPort);
                WebServerThread = new Thread(startWebServer);
                WebServerThread.Start();
                LogEvent("启动Web Server 管理端成功，端口" + DocumentSetting.Default.WebPort);
            }
            else
            {
                LogEvent("正在关闭Web Server 管理端，端口" + DocumentSetting.Default.WebPort);
                if (WebServerThread != null)
                {
                    manageServer.Stop();
                    WebServerThread = null;
                }
                LogEvent("已关闭Web Server 管理端，端口" + DocumentSetting.Default.WebPort);
            }
        }

        private void sideNavPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确实要退出程序吗？", "退出程序", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
