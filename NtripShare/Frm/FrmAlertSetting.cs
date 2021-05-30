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

namespace NtripShare.Frm
{
    public partial class FrmAlertSetting : Form
    {
        public FrmAlertSetting()
        {
            InitializeComponent();
        }

        private void FrmAlertSetting_Load(object sender, EventArgs e)
        {
            textBoxAlarmEmail.Text = DocumentSetting.Default.AlarmEmail;
            textBoxSMTPServer.Text = DocumentSetting.Default.SmtpServer;
            integerInputSmtpPort.Value = DocumentSetting.Default.SmtpServerPort;
            textBoxMailSend.Text = DocumentSetting.Default.HostEmail;
            textBoxMailPassword.Text = DocumentSetting.Default.HostEmailPassword;
        }

        private void buttonXJSSave_Click(object sender, EventArgs e)
        {
            DocumentSetting.Default.AlarmEmail = textBoxAlarmEmail.Text  ;
            DocumentSetting.Default.SmtpServer = textBoxSMTPServer.Text  ;
            DocumentSetting.Default.SmtpServerPort = integerInputSmtpPort.Value;
            DocumentSetting.Default.HostEmail = textBoxMailSend.Text ;
            DocumentSetting.Default.HostEmailPassword = textBoxMailPassword.Text  ;

            FileStream fs = new FileStream(Application.StartupPath + @"\config.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, DocumentSetting.Default);
            fs.Close();
            MessageBox.Show("设置保存成功！");
            this.Close();
        }
    }
}
