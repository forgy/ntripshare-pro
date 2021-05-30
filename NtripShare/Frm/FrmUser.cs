// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
using NtripShare.Model;
using NtripShare.Util;
// End of VB project level imports


namespace NtripShare
{
    public partial class FrmUser
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        public UserAccount UserAccount { get; set; }

        public void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            if (tbUsername.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (tbPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (UserAccount == null && DocumentSetting.Default.UserAccounts.ContainsKey(tbUsername.Text))
            {
                MessageBox.Show("用户名已存在");
                return;
            }
            UserAccount ccount = new UserAccount();
            //if (UserAccount != null)
            //{
            //    ccount = UserAccount;
            //}
            //else
            //{
            //    ccount.ID = GuidUtil.GuidToLongID();

            ccount.Username = tbUsername.Text;
            ccount.Password = tbPassword.Text;
            ccount.Des = tbDes.Text;
            ccount.DeadLineTime = dateTimePicker1.Value;
            ccount.MaxConnectCount = (int)numericUpDown1.Value;

            DocumentSetting.Default.addOrUpdateUserAccount(ccount);
            MessageBox.Show("保存成功！");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
           
        }

        public void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btWeek_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(7.0);
        }

        private void btMonth_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(30.0);
        }

        private void btYear_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddYears(1);
        }

        private void btNoLimit_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddYears(100);
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            if (UserAccount != null) {
              tbUsername.Text = UserAccount.Username;
                tbUsername.Enabled = false;
                tbPassword.Text = UserAccount.Password  ;
                tbDes.Text= UserAccount.Des  ;
                dateTimePicker1.Value= UserAccount.DeadLineTime  ;
                numericUpDown1.Value = UserAccount.MaxConnectCount  ;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }

}
