using DevComponents.DotNetBar.SuperGrid;
using NtripShare.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NtripShare.Frm
{
    public partial class FrmUserList : Form
    {
        public FrmUserList()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FrmUser frmUser = new FrmUser();
            frmUser.StartPosition = FormStartPosition.CenterScreen;
            if (frmUser.ShowDialog() == DialogResult.OK) {
                List<UserAccount> users = new List<UserAccount>();
                users.AddRange(DocumentSetting.Default.UserAccounts.Values);
                dataGridViewConnections.Rows.Clear();
                foreach (UserAccount account in users)
                {
                    object[] data = new object[] { account.Username, account.Password, account.MaxConnectCount, account.DeadLineTime, account.Des };
                    dataGridViewConnections.Rows.Add(data);
                }
                //this.dataGridViewConnections.DataSource = users;
            }
        }

        private void buttonXEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewConnections.SelectedRows.Count == 0) {
                MessageBox.Show("请先选中数据！");
                return;
            }
            FrmUser frmUser = new FrmUser();
            DataGridViewRow row = dataGridViewConnections.SelectedRows[0] ;
            frmUser.UserAccount = DocumentSetting.Default.UserAccounts[row.Cells[0].Value.ToString()];
            frmUser.StartPosition = FormStartPosition.CenterScreen;
            if (frmUser.ShowDialog() == DialogResult.OK)
            {
                List<UserAccount> users = new List<UserAccount>();
                users.AddRange(DocumentSetting.Default.UserAccounts.Values);
                dataGridViewConnections.Rows.Clear();
                foreach (UserAccount account in users)
                {
                    object[] data = new object[] { account.Username, account.Password, account.MaxConnectCount, account.DeadLineTime, account.Des };
                    dataGridViewConnections.Rows.Add(data);
                }
                //this.dataGridViewConnections.DataSource = users;
            }
        }

        private void buttonXRemove_Click(object sender, EventArgs e)
        {
            if (dataGridViewConnections.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中数据！");
                return;
            }
            if (MessageBox.Show("您确定要删除该账号吗？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DataGridViewRow row = dataGridViewConnections.SelectedRows[0];
                string id =row.Cells[0].Value.ToString();
                DocumentSetting.Default.removeUserAccount(id);
                MessageBox.Show("删除账号成功");
                List<UserAccount> users = new List<UserAccount>();
                users.AddRange(DocumentSetting.Default.UserAccounts.Values);
                //this.dataGridViewConnections.DataSource = users;
                dataGridViewConnections.Rows.Clear();
                foreach (UserAccount account in users)
                {
                    object[] data = new object[] { account.Username, account.Password, account.MaxConnectCount, account.DeadLineTime, account.Des };
                    dataGridViewConnections.Rows.Add(data);
                }
                return;
            }
         
        }

        private void FrmUserList_Load(object sender, EventArgs e)
        {
            ////GridPanel primaryGrid = dataGridViewUser.PrimaryGrid;
            ////获取要设置时间格式的列
            //var createTimeColumn = dataGridViewUser.Columns["DeadLineTime"];
            ////获取该列绑定的控件 
            //GridDateTimeInputEditControl createTimeColumnRenderControl = (GridDateTimeInputEditControl)createTimeColumn.RenderControl;
            ////设置 format 为自定义
            //createTimeColumnRenderControl.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            ////设置自定义格式            
            //createTimeColumnRenderControl.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            ////设置该列的AutoSizeMode
            //createTimeColumn.AutoSizeMode = ColumnAutoSizeMode.AllCells;
            dataGridViewConnections.AutoGenerateColumns = false;
            List<UserAccount> users = new List<UserAccount>();
            users.AddRange(DocumentSetting.Default.UserAccounts.Values);
            //this.dataGridViewConnections.DataSource = users;

            dataGridViewConnections.Rows.Clear();
            foreach (UserAccount account in users)
            {
                object[] data = new object[] { account.Username, account.Password, account.MaxConnectCount, account.DeadLineTime, account.Des };
                dataGridViewConnections.Rows.Add(data);
            }
        }
    }
}
