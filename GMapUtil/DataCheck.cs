using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMapUtil
{
    public class DataCheck
    {
        public string getData()
        {
            return GetResistText(getCpu() + GetDiskVolumeSerialNumber());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="softName"></param>
        /// <param name="timeLeft"></param>
        /// <returns></returns>
        public bool checkData(string data, string softName, ref DateTime timeLeft)
        {
            DateTime time = GetBeijingTime();
            string txt = GetResistText( GetResistText(getCpu() + GetDiskVolumeSerialNumber()));
            string fos = StrToHex(softName);
         
            if (data.IndexOf(txt) == 0 && data.IndexOf(fos) > 0)
            {
                DateTime limitTime = HexToTime(data.Replace(txt, "").Substring(0, 8));
                if (limitTime < time)
                {
                    return false;
                }
                timeLeft = limitTime;
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="softName"></param>
        /// <param name="timeLeft"></param>
        /// <returns></returns>
        public bool checkData2(string data)
        {
            DateTime time = GetBeijingTime();
            string txt = GetResistText(GetResistText(getCpu() + GetDiskVolumeSerialNumber()));
            if (data.IndexOf(txt) == 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 把十六进制数据转化成时间
        /// </summary>
        /// <param name="HexStr">16进制数据字符串</param>
        /// <returns></returns>
        private static DateTime HexToTime(string HexStr)
        {
            HexStr = HexStr.Replace(" ", "");
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 01, 01));
            if (HexStr.StartsWith("oX") || HexStr.StartsWith("OX"))
            {
                HexStr = HexStr.Substring(2, HexStr.Length - 2);
            }

            if (HexStr.Length % 2 != 0)
            {
                HexStr += " ";
            }
            byte[] bytes = new byte[HexStr.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[bytes.Length - 1 - i] = Convert.ToByte(HexStr.Substring(i * 2, 2), 16);
            }
            uint unitSpan = System.BitConverter.ToUInt32(bytes, 0);
            return time.AddSeconds(unitSpan);
        }

        ///<summary>
        /// 获取标准北京时间
        ///</summary>
        ///<returns></returns>
        private static DateTime GetBeijingTime()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.baidu.com");
                request.Method = "HEAD";
                request.AllowAutoRedirect = false;
                HttpWebResponse reponse = (HttpWebResponse)request.GetResponse();
                string cc = reponse.GetResponseHeader("date");
                reponse.Close();
                DateTime time;
                bool s = GMTStrParse(cc, out time);
                return time.AddHours(8); //GMT要加8个小时才是北京时间
            }
            catch (Exception e) {
                return System.DateTime.Now;
            }
          
        }
        private static bool GMTStrParse(string gmtStr, out DateTime gmtTime)  //抓取的date是GMT格式的字符串，这里转成datetime
        {
            CultureInfo enUS = new CultureInfo("en-US");
            bool s = DateTime.TryParseExact(gmtStr, "r", enUS, DateTimeStyles.None, out gmtTime);
            return s;
        }

        /// <summary>
        /// 获取CPU的参数
        /// </summary>
        /// <returns></returns>
        private string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        /// <summary>
        /// 获取硬盘的参数
        /// </summary>
        /// <returns></returns>
        private string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        private int[] intCode = new int[127];//用于存密钥
        private void setIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 10;
            }
        }
        private int[] intNumber = new int[25];//用于存机器码的Ascii值
        private char[] Charcode = new char[25];//存储机器码字


        /// <summary>
        /// 根据机器码获取注册码
        /// </summary>
        /// <param name="machineText"></param>
        /// <returns></returns>
        private string GetResistText(string machineText)
        {
            //把机器码存入数组中
            setIntCode();//初始化127位数组
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(machineText.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);

            }
            string strAsciiName = null;//用于存储机器码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    { strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString(); }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }

                }
            }
            return strAsciiName;
        }

        private string StrToHex(string mStr) //返回处理后的十六进制字符串
        {
            return BitConverter.ToString(
            ASCIIEncoding.Default.GetBytes(mStr)).Replace("-", "");
        } /* StrToHex */
    }
}
