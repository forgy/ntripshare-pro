using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
    [Serializable]
    public  class NtripAccount
    {
        public long ID { get; set; } = 0;
        public string Name { get; set; } = "";
        public string IP { get; set; } = "";
        public int Port { get; set; } = 5555;
        public string MountPoint { get; set; } = "";
        public string ShareMountPoint { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool AutoChange { get; set; } = false;
        public bool CreatMountPoint { get; set; } = false;
        public bool NeedCool { get; set; } = false;
        public bool UseCoord { get; set; } = false;
        public double StationLon { get; set; }
        public double StationLat { get; set; }
        public double StationHeight { get; set; }
        public double CurrentLon { get; set; }
        public double CurrentLat { get; set; }
        public double ConnectCount { get; set; }
        public DateTime StartTime { get; set; }
        public int DataSize { get; set; }
        public double BufferSize { get; set; } = 50000;
        public string DataType { get; set; }
        public DateTime LastTime { get; set; }
    }
}
