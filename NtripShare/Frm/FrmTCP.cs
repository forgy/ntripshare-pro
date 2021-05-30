using NtripShare.Model;
using NtripShare.Util;
using SharpGPS.NTRIP;
using System;
using System.Net;
using System.Windows.Forms;

namespace NtripShare.Frm
{
    public partial class FrmTCP : Form
    {
        public TCP TCP { get; set; }

        public FrmTCP()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("请先选择名称");
                return;
            }
            if (string.IsNullOrEmpty(tbPort.Text) || !StrUtil.IsInt(tbPort.Text))
            {
                MessageBox.Show("请输入端口");
                return;
            }
            if (string.IsNullOrEmpty(textBoxMountPoint.Text))
            {
                MessageBox.Show("请输入接入点名称");
                return;
            }
            if (string.IsNullOrEmpty(comboBoxDataType.Text))
            {
                MessageBox.Show("请选择数据类型");
                return;
            }
            if (string.IsNullOrEmpty(textBoxLat.Text)|| !StrUtil.IsNumeric(textBoxLat.Text))
            {
                MessageBox.Show("请输入纬度");
                return;
            }
            if (string.IsNullOrEmpty(textBoxLon.Text) || !StrUtil.IsNumeric(textBoxLon.Text))
            {
                MessageBox.Show("请输入经度");
                return;
            }

            TCP tcp = new TCP();
            if (TCP != null)
            {
                tcp = TCP;
            }
         
            tcp.Name = textBoxName.Text;
            tcp.ShareMountPoint = textBoxMountPoint.Text;
            tcp.Port = int.Parse(tbPort.Text);
            tcp.ShareMountPoint = textBoxMountPoint.Text;
            tcp.DataType = comboBoxDataType.Text;
            tcp.Des = tbDes.Text;
            tcp.Lon = double.Parse(textBoxLon.Text);
            tcp.Lat = double.Parse(textBoxLat.Text);
            tcp.CreateMountPoint = checkBoxAddToPool.Checked;
            if (tcp.BufferSize == 0.0) {
                tcp.BufferSize = 30000;
            }
            tcp.BufferSize = (double)numericUpDownBuffer.Value;

            if (DocumentSetting.Default.Tcps == null) {
                DocumentSetting.Default.Tcps = new System.Collections.Generic.Dictionary<string, TCP>();
            }
            DocumentSetting.Default.addOrUpdateTcp(tcp);
            MessageBox.Show("保存成功！");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

      
        private void FrmAccount_Load(object sender, EventArgs e)
        {
            if (TCP != null)
            {
                textBoxName.Enabled = false;
                tbPort.Text = TCP.Port.ToString();
                textBoxName.Text = TCP.Name;
                tbDes.Text = TCP.Des;
                textBoxMountPoint.Text = TCP.ShareMountPoint;
                comboBoxDataType.Text = TCP.DataType;
                textBoxLon.Text = TCP.Lon.ToString();
                textBoxLat.Text = TCP.Lat.ToString();
                checkBoxAddToPool.Checked = TCP.CreateMountPoint;
                numericUpDownBuffer.Value = (decimal)TCP.BufferSize;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxMountPoint.Text = textBoxName.Text + "-" + comboBoxDataType.Text;
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void buttonMap_Click(object sender, EventArgs e)
        {
            FrmCoord frmCoord = FrmCoord.Default;
            try
            {
                frmCoord.Coord = new GMap.NET.PointLatLng(double.Parse(textBoxLat.Text), double.Parse(textBoxLon.Text));
            }
            catch (Exception eee)
            {

            }
            if (frmCoord.ShowDialog() == DialogResult.OK)
            {
                textBoxLon.Text = frmCoord.Coord.Lng.ToString();
                textBoxLat.Text = frmCoord.Coord.Lat.ToString();
            }
        }
    }
}
