using NtripShare.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NtripShare.Model
{
    [Serializable]
    class DocumentSetting
    {
        private static DocumentSetting defaultInstance;

        public DocumentSetting()
        {
            SavePath = Application.StartupPath + "/Data";
        }

        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static DocumentSetting Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new DocumentSetting();
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }

        }

        /// <summary>
        /// 是否启动账号池报警
        /// </summary>
        public bool IsPoolAlarm { get; set; }

        /// <summary>
        /// 是否启动最大连接数报警
        /// </summary>
        public bool IsConnectionAlarm { get; set; }

        /// <summary>
        /// 报警邮箱
        /// </summary>
        public string AlarmEmail { get; set; }

        /// 发送邮箱服务器
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// 报警间隔
        /// </summary>
        public int AlarmInterval { get; set; }
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int SmtpServerPort { get; set; }
        /// <summary>
        /// 发型邮箱
        /// </summary>
        public string HostEmail { get; set; }
        /// <summary>
        /// 是否启动邮箱报警
        /// </summary>
        public bool IsEmailAlarm { get; set; }
        /// <summary>
        /// 发送邮箱密码
        /// </summary>
        public string HostEmailPassword { get; set; }

        /// <summary>
        /// ntrip账号
        /// </summary>
        public Dictionary<string, NtripAccount> NtripAccounts { get; set; } = new Dictionary<string, NtripAccount>();
        /// <summary>
        /// 用户数量
        /// </summary>
        public Dictionary<string,UserAccount> UserAccounts { get; set; } = new Dictionary<string, UserAccount>();
        /// <summary>
        /// tcp账户
        /// </summary>
        public Dictionary<string, TCP> Tcps { get; set; } = new Dictionary<string, TCP>();
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; set; } = 5000;
        /// <summary>
        /// 用户池名称
        /// </summary>
        public String PoolName { get; set; } = "RTCM-POOL";

        /// <summary>
        /// WebPort
        /// </summary>
        public int WebPort { get; set; } = 6000;

        /// <summary>
        /// web管理IP
        /// </summary>
        public string WebIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// 用户池缓冲区设置
        /// </summary>
        public int CoolTime { get; set; } = 60;

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int MaxConnectionCount { get; set; } = 50;

        /// <summary>
        /// 单用户最大连接数
        /// </summary>
        public int MaxUserCount { get; set; } = 1;
        /// <summary>
        /// 保存原始数据
        /// </summary>
        public bool SaveRawData { get; set; } = false;
        /// <summary>
        /// 原始数据保存位置
        /// </summary>
        public string SavePath { get; set; }

        public bool WriteEventsToFile { get; set; } = false;

        public bool StartGeoFence { get; set; } = false;
        public bool ErrorClose { get; set; } = false;

        public bool IsTcpOpen { get; set; } = false;
        public bool IsWebOpen { get; set; } = false;

        public bool IsServerOpen { get; set; } = false;

        public bool IsNtripOpen { get; set; } = false;
        public bool IsCheckPass { get; set; } = false;

        public bool StartGuDingDynamic { get; set; } = true;

        public string GeoFence { get; set; }

        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool ProxyEnable { get; set; } = false;
        public string ProxyIp { get; set; }
        public int ProxyPort { get; set; }

        /// <summary>
        /// 保存至文件
        /// </summary>
        public void saveToFile() {
            FileStream fs = new FileStream(Application.StartupPath + @"\config.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }

        /// <summary>
        /// 添加或删除用户账户
        /// </summary>
        /// <param name="userAccount"></param>
        public void addOrUpdateTcp(TCP tcp)
        {
            if (Tcps.ContainsKey(tcp.Name))
            {
                Tcps[tcp.Name] = tcp;
            }
            else
            {
                Tcps.Add(tcp.Name, tcp);
            }

            this.saveToFile();
        }

        /// <summary>
        /// 删除用户账户
        /// </summary>
        /// <param name="id"></param>
        public void removeTcp(string id)
        {
            Tcps.Remove(id);
            this.saveToFile();
        }

        /// <summary>
        /// 添加或删除用户账户
        /// </summary>
        /// <param name="userAccount"></param>
        public void addOrUpdateUserAccount(UserAccount userAccount){
            if (UserAccounts.ContainsKey(userAccount.Username))
            {
                UserAccounts[userAccount.Username] = userAccount;
            }
            else {
                UserAccounts.Add(userAccount.Username, userAccount);
            }
         
            this.saveToFile();
        }

        /// <summary>
        /// 删除用户账户
        /// </summary>
        /// <param name="id"></param>
        public void removeUserAccount(string id) {
            UserAccounts.Remove(id);
            this.saveToFile();
        }

        /// <summary>
        /// 添加或更新ntrip账户
        /// </summary>
        /// <param name="ntripAccount"></param>
        public void addOrUpdateNtripAccount(NtripAccount ntripAccount)
        {
            if (NtripAccounts.ContainsKey(ntripAccount.Name))
            {
                NtripAccounts[ntripAccount.Name] = ntripAccount;
            }
            else {
                NtripAccounts.Add(ntripAccount.Name, ntripAccount);
            }
   
            this.saveToFile();
        }

        /// <summary>
        /// 删除ntrip账户
        /// </summary>
        /// <param name="ntripAccount"></param>
        public void removeNtripAccount(NtripAccount ntripAccount)
        {
            NtripAccounts.Remove(ntripAccount.Name);
            this.saveToFile();
        }

        /// <summary>
        /// 删除ntrip账户
        /// </summary>
        /// <param name="name"></param>
        public void removeNtripAccount(string name)
        {
            NtripAccounts.Remove(name);
            this.saveToFile();
        }
    }
}
