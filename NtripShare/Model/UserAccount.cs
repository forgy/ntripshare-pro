using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
    [Serializable]
    public class UserAccount
  {
        public long ID { get; set; } = 0;
        public int ConnectionID { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool AutoChange { get; set; } = false;
        public string MountPoint { get; set; }
        public string UA { get; set; }
        public string Address { get; set; }
        public DateTime DeadLineTime { get; set; }
        public int MaxConnectCount { get; set; }
        public int ConnectCount { get; set; }
        public String ConnectStaion { get; set; }
        public DateTime ConnectTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public double CurrentLat { get; set; }
        public double CurrentLon { get; set; }
        public String CurrentStatus { get; set; }
        public int DataSize { get; set; }
		 public String Des { get; set; }
    }
}
