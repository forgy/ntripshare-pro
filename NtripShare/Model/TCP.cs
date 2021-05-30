using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
    [Serializable]
    public class TCP
    {
        public string Name { get; set; }

        public int Port{ get; set; }

        public string Des { get; set; }

        public string DataType { get; set; }

        public string ShareMountPoint { get; set; }

        public double Lon { get; set; }

        public double Lat { get; set; }

        public bool CreateMountPoint { get; set; } = false;

        public double BufferSize { get; set; } = 30000;
    }
}
