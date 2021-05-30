using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMapChinaRegion;
using NtripShare.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NtripShare
{
    public partial class FrmCoord : Form
    {
        public FrmCoord()
        {
            InitializeComponent();
        }
        private static FrmCoord defaultInstance;
        public PointLatLng Coord { get; set; }
        private GMapOverlay poiOverlay = new GMapOverlay("poiOverlay");
        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static FrmCoord Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new FrmCoord();
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            InitMap();
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
           
           
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 9;
         
            poiOverlay.Clear();
            gMapControl1.Overlays.Add(poiOverlay);
            if (Coord.Lat != 0)
            {
                gMapControl1.Position = Coord;
                GMarkerGoogle marker = new GMarkerGoogle(Coord, GMarkerGoogleType.green_dot);
                poiOverlay.Markers.Add(marker);
            }
            else
            {
                gMapControl1.Position = new PointLatLng(32.043, 118.773);
            }

        }


        private void buttonXJSSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonXCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PointLatLng pointLatLng = gMapControl1.FromLocalToLatLng(e.X,e.Y);
            gMapControl1.Position = pointLatLng;
            Coord = pointLatLng;
            poiOverlay.Clear();
            GMarkerGoogle marker = new GMarkerGoogle(pointLatLng, GMarkerGoogleType.green_dot);
            this.poiOverlay.Markers.Add(marker);
        }
    }
}
