using NtripShare.Model;
using NtripShare.Util;
using SharpGPS.NTRIP;
using System;
using System.Net;
using System.Windows.Forms;

namespace NtripShare.Frm
{
    public partial class FrmAccount : Form
    {
        public NtripAccount NtripAccount { get; set; }

        public FrmAccount()
        {
            InitializeComponent();
        }

        private void btGetMountPoints_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.tbIP.Text), int.Parse(tbPort.Text));
                SharpGPS.NTRIP.NTRIPClient client = new SharpGPS.NTRIP.NTRIPClient(iPEndPoint, tbUsername.Text, tbPassword.Text, null);
                SourceTable sourceTable = client.GetSourceTable();
                cbMountPoints.Items.Clear();
                for (int i = 0; i < sourceTable.DataStreams.Count; i++)
                {
                    cbMountPoints.Items.Add(sourceTable.DataStreams[i].MountPoint);
                    cbMountPoints.Text = sourceTable.DataStreams[i].MountPoint;
                }
            }
            catch
            {
                MessageBox.Show("获取接入点失败！");
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbMountPoints.Text))
            {
                MessageBox.Show("请先选择接入点");
                return;
            }
            if (string.IsNullOrEmpty(tbIP.Text))
            {
                MessageBox.Show("请输入IP地址");
                return;
            }
            if (string.IsNullOrEmpty(tbPort.Text))
            {
                MessageBox.Show("请输入端口");
                return;
            }
            if (string.IsNullOrEmpty(tbUsername.Text))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            if (NtripAccount == null && DocumentSetting.Default.NtripAccounts.ContainsKey(textBoxName.Text))
            {
                MessageBox.Show("用户名已存在");
                return;
            }

            NtripAccount ntripAccount = new NtripAccount();
            if (NtripAccount != null)
            {
                ntripAccount = NtripAccount;
            
            }
            else {
                ntripAccount.ID = GuidUtil.GuidToLongID();
            }
            ntripAccount.IP = tbIP.Text;
            ntripAccount.MountPoint = cbMountPoints.Text;
            ntripAccount.Name = textBoxName.Text;
            ntripAccount.ShareMountPoint = ntripAccount.Name /*+ "-"*/ + ntripAccount.MountPoint;
            ntripAccount.Password = tbPassword.Text;
            ntripAccount.Port = int.Parse(tbPort.Text);
            ntripAccount.Username = tbUsername.Text;
            ntripAccount.UseCoord = checkBoxFloatCoord.Checked;
            if (ntripAccount.BufferSize == 0.0)
            {
                ntripAccount.BufferSize = 5000;
            }
            ntripAccount.BufferSize = (double)numericUpDownBuffer.Value;
            ntripAccount.NeedCool = checkBoxCool.Checked;
            if (checkBoxFloatCoord.Checked) {
             
                ntripAccount.StationLon = double.Parse(tbLon.Text);
                ntripAccount.StationLat = double.Parse(tbLat.Text);
                if (!CoordUtil.isInChina(ntripAccount.StationLat, ntripAccount.StationLon)) {
                    MessageBox.Show("坐标不在中国范围内，请填写正确坐标！");
                    return;
                }
            }
        
            ntripAccount.CreatMountPoint = checkBoxAddToPool.Checked;
            ntripAccount.DataType = comboBoxDataType.Text;

            DocumentSetting.Default.addOrUpdateNtripAccount(ntripAccount);
            MessageBox.Show("保存成功！");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

      
        private void FrmAccount_Load(object sender, EventArgs e)
        {
            if (NtripAccount != null)
            {
                textBoxName.Enabled = false;
                NtripAccount ntripAccount = NtripAccount;
                tbIP.Text = ntripAccount.IP;
                cbMountPoints.Text = ntripAccount.MountPoint;
                textBoxName.Text = ntripAccount.Name;
                tbPassword.Text = ntripAccount.Password;
                tbPort.Text = ntripAccount.Port.ToString();
                tbUsername.Text = ntripAccount.Username;
                checkBoxFloatCoord.Checked = ntripAccount.UseCoord;
                tbLon.Text = ntripAccount.StationLon.ToString();
                tbLat.Text = ntripAccount.StationLat.ToString();
                checkBoxAddToPool.Checked = ntripAccount.CreatMountPoint;
                comboBoxDataType.Text = ntripAccount.DataType;
                if (ntripAccount.BufferSize == 0.0)
                {
                    ntripAccount.BufferSize = 5000;
                }
                numericUpDownBuffer.Value = (decimal)ntripAccount.BufferSize;
                checkBoxCool.Checked = ntripAccount.NeedCool ;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void checkBoxFloatCoord_CheckedChanged(object sender, EventArgs e)
        {
            tbLon.Enabled = checkBoxFloatCoord.Checked;
            tbLat.Enabled = checkBoxFloatCoord.Checked;
        }

        private void checkBoxAddToPool_CheckedChanged(object sender, EventArgs e)
        {
            //checkBoxFloatCoord.Enabled = !checkBoxAddToPool.Checked;
            //tbLon.Enabled = !checkBoxAddToPool.Checked;
            //tbLat.Enabled = !checkBoxAddToPool.Checked;
        }

        private void buttonMap_Click(object sender, EventArgs e)
        {
            FrmCoord frmCoord = FrmCoord.Default;
            try
            {
                frmCoord.Coord = new GMap.NET.PointLatLng(double.Parse(tbLat.Text), double.Parse(tbLon.Text));
            }
            catch (Exception eee) { 
            
            }
            if (frmCoord.ShowDialog() == DialogResult.OK) {
                tbLon.Text = frmCoord.Coord.Lng.ToString();
                tbLat.Text = frmCoord.Coord.Lat.ToString();
            }
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }
    }
}
