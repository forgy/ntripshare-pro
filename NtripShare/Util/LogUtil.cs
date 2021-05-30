using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Util
{
    class LogUtil
    {
        /// <summary>
                /// 锁对象
                /// </summary>
        private static object lockHelper = new object();


        /// <summary>
                /// 写error级别日志
                /// </summary>
                /// <param name="errorMessage">异常信息</param>
                /// <param name="ex">异常类</param>
        public void WriteErrorLog(string errorMessage, Exception ex)
        {
            string errorMsg = string.Empty;
            if (ex.InnerException != null)
            {
                errorMsg = ex.InnerException.Message;
            }
            errorMsg = errorMsg + errorMessage + ex.StackTrace;
            WriteLog(errorMsg);
        }

        /// <summary>
                /// 写日志
                /// </summary>
                /// <param name="msg">日志信息</param>    
        public void WriteLog(string msg)
        {
            lock (lockHelper)
            {
                string FilePath = string.Empty;
                string AbsolutePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log";
                if (!Directory.Exists(AbsolutePath))
                {
                    Directory.CreateDirectory(AbsolutePath);
                }
                FilePath = AbsolutePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                File.AppendAllText(FilePath, "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + msg, Encoding.GetEncoding("gb2312"));


            }
        }
    }
}
