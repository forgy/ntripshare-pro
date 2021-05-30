using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
   public  class SystemStatus
    {
        private static SystemStatus defaultInstance;

        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static SystemStatus Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new SystemStatus();
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }

        }

        public int NtripCount { get; set; }

        public int NtripUseCount { get; set; }

        public int TCPCount { get; set; }

        public int TCPUseCount { get; set; }

        public int PoolCount { get; set; }
        public int PoolUseCount { get; set; }

        public int UserCount { get; set; }
        public int MaxConnectCount { get; set; }
        public int LoginUserCount { get; set; }

        public  List<NtripAccount> NtripAccounts { get; set; }

        public List<UserAccount> UserAccounts { get; set; }
    }
}
