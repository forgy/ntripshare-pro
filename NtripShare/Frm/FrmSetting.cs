using GMap.NET.WindowsForms;
using GMapChinaRegion;
using NtripShare.Frm;
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
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
        }
        private static FrmSetting defaultInstance;
        private string selectRings;
        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static FrmSetting Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new FrmSetting();
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
            DocumentSetting document = DocumentSetting.Default;
            integerInputPort.Text = document.ServerPort.ToString();
            textBoxPoolName.Text = document.PoolName.ToString();
            //integerInputBuffer.Text = (document.BufferSize/1000).ToString();
            //integerInputBuffer2.Text = (document.GuDingBufferSize / 1000).ToString();
            integerInputMaxCount.Text = document.MaxConnectionCount.ToString();
            integerInputMaxUserCount.Text = document.MaxUserCount.ToString();
            checkBoxSaveRaw.Checked = document.SaveRawData;
            checkBoxGeo.Checked = document.StartGeoFence;
            textBoxSaveFolder.Text = document.SavePath.ToString();
            integerInputCool.Value = document.CoolTime;
            advTreeChina.Enabled = document.StartGeoFence;
            integerInputWebPort.Value = document.WebPort;
            textBoxUserName.Text = document.UserName;
            textBoxPassword.Text = document.PassWord;

            checkBoxDynamic.Checked = document.StartGuDingDynamic;
            checkBoxPass.Checked = document.IsCheckPass;
            InitCountryTree(document.GeoFence);

            checkBoxXProxy.Checked = document.ProxyEnable;

            integerInputProxyPort.Value = document.ProxyPort;
            textBoxXProxyIp.Text = document.ProxyIp;
        }

        private void InitCountryTree(string fens)
        {
            TreeNode rootNode = new TreeNode("中国");
            this.advTreeChina.Nodes.Add(rootNode);
         
            TreeNode selectNode = null;
            try
            {
                if (MainForm.Default.China.Province != null)
                {
                    foreach (var provice in MainForm.Default.China.Province)
                    {
                        TreeNode pNode = new TreeNode(provice.name);
                        pNode.Tag = provice;
                        if (provice.rings == fens) {
                            selectNode = pNode;
                        }
                        if (provice.City != null)
                        {
                            foreach (var city in provice.City)
                            {
                                TreeNode cNode = new TreeNode(city.name);
                                cNode.Tag = city;
                                if (city.rings == fens)
                                {
                                    selectNode = cNode;
                                }
                                if (city.Piecearea != null)
                                {
                                    foreach (var piecearea in city.Piecearea)
                                    {
                                        TreeNode areaNode = new TreeNode(piecearea.name);
                                        areaNode.Tag = piecearea;
                                        if (piecearea.rings == fens)
                                        {
                                            selectNode = areaNode;
                                        }
                                        cNode.Nodes.Add(areaNode);
                                    }
                                }
                                pNode.Nodes.Add(cNode);
                            }
                        }
                        //TreeNode rootNode = this.advTreeChina.Nodes[0];
                        rootNode.Nodes.Add(pNode);
                    }
                }
                rootNode.Expand();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (selectNode != null) {
                advTreeChina.Focus();
                advTreeChina.SelectedNode = selectNode;
            }
            this.advTreeChina.NodeMouseClick += new TreeNodeMouseClickEventHandler(advTreeChina_NodeMouseClick);
        }

        void advTreeChina_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.advTreeChina.SelectedNode = sender as TreeNode;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                string name = e.Node.Text;
                string rings = null;
                switch (e.Node.Level)
                {
                    case 0:
                        break;
                    case 1:
                        Province province = e.Node.Tag as Province;
                        name = province.name;
                        rings = province.rings;
                        break;
                    case 2:
                        City city = e.Node.Tag as City;
                        name = city.name;
                        rings = city.rings;
                        break;
                    case 3:
                        Piecearea piecearea = e.Node.Tag as Piecearea;
                        name = piecearea.name;
                        rings = piecearea.rings;
                        break;
                }
                if (rings != null && !string.IsNullOrEmpty(rings))
                {
                    MainForm.Default.showGeoFence(name,rings);
                    selectRings = rings;
                }
            }
        }

        private void buttonX1_Click_2(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择数据保存文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                textBoxSaveFolder.Text = dialog.SelectedPath;
            }
        }

        private void buttonXJSSave_Click(object sender, EventArgs e)
        {
            DocumentSetting document = DocumentSetting.Default;
            document.ServerPort= integerInputPort.Value ;
            document.PoolName = textBoxPoolName.Text ;
            document.MaxUserCount = integerInputMaxUserCount.Value;
            //document.BufferSize = integerInputBuffer.Value *1000;
            //document.GuDingBufferSize = integerInputBuffer2.Value * 1000;
            document.MaxConnectionCount = integerInputMaxCount.Value;
            document.SaveRawData = checkBoxSaveRaw.Checked;
            document.SavePath = textBoxSaveFolder.Text;
            document.CoolTime = integerInputCool.Value ;
            document.StartGeoFence = checkBoxGeo.Checked;
            document.StartGuDingDynamic = checkBoxDynamic.Checked;
            document.WebPort = integerInputWebPort.Value;
            document.UserName = textBoxUserName.Text;
            document.PassWord = textBoxPassword.Text;
            document.IsCheckPass = checkBoxPass.Checked;

            document.ProxyEnable = checkBoxXProxy.Checked ;

            document.ProxyPort = integerInputProxyPort.Value  ;
            document.ProxyIp = textBoxXProxyIp.Text ;
            if (document.StartGeoFence) {
                if (advTreeChina.SelectedNode != null)
                {
                    document.GeoFence = selectRings;
                }
                else {
                    document.GeoFence = "";
                }
            }

            FileStream fs = new FileStream(Application.StartupPath + @"\config.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, document);
            fs.Close();
            MessageBox.Show("设置保存成功！");
            this.Close();
        }

        private void buttonXCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void advTreeChina_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void checkBoxGeo_CheckedChanged(object sender, EventArgs e)
        {
            advTreeChina.Enabled = checkBoxGeo.Checked;
        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Default.clearGeoFence();
        }

        private void buttonAlert_Click(object sender, EventArgs e)
        {
            FrmAlertSetting frmAccount = new FrmAlertSetting();
            frmAccount.StartPosition = FormStartPosition.CenterScreen;
            if (frmAccount.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
