using NtripShare.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NtripShare
{
    static class Program
    {
        private const int SW_SHOW = 5;

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Thread.Sleep(2000);
            Process p = RunningInstance();
            if (p == null)
            {
                Application.Run(MainForm.Default);
            }
            else
            {
                HandleRunningInstance(p);
            }
            //Application.Run( MainForm.Default);
        }

        /// <summary>
        /// Get current process
        /// </summary>
        /// <returns></returns>
        private static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processByName = Process.GetProcessesByName(currentProcess.ProcessName);
            return processByName.FirstOrDefault(process2 => (process2.Id != currentProcess.Id) && (Assembly.GetExecutingAssembly().Location.Replace("/", @"\") == currentProcess.MainModule.FileName));
        }

        private static void HandleRunningInstance(Process p)
        {
            MessageBox.Show("已经有一个实例在运行，一台电脑只能运行一个实例！");
            ShowWindowAsync(p.MainWindowHandle, SW_SHOW);
        }

        /// <summary>
        ///错误弹窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var error = e.Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            WriteLog(str);
            DocumentSetting.Default.ErrorClose = true;
            DocumentSetting.Default.IsNtripOpen = MainForm.Default.isNtripOpen();
            DocumentSetting.Default.IsServerOpen = MainForm.Default.isServerOpen();
            DocumentSetting.Default.IsTcpOpen = MainForm.Default.isTcpOpen();
            DocumentSetting.Default.saveToFile();
          
            Application.Restart();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var error = e.ExceptionObject as Exception;
            var strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now + "\r\n";
            var str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            WriteLog(str);
            DocumentSetting.Default.ErrorClose = true;
            DocumentSetting.Default.IsNtripOpen = MainForm.Default.isNtripOpen();
            DocumentSetting.Default.IsServerOpen = MainForm.Default.isServerOpen();
            DocumentSetting.Default.IsTcpOpen = MainForm.Default.isTcpOpen();
            DocumentSetting.Default.IsWebOpen = MainForm.Default.isWebOpen();
            DocumentSetting.Default.saveToFile();
            Application.Restart();
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="str"></param>
        static void WriteLog(string str)
        {
            if (!Directory.Exists("ErrLog"))
            {
                Directory.CreateDirectory("ErrLog");
            }

            using (var sw = new StreamWriter(@"ErrLog\ErrLog.txt", true))
            {
                sw.WriteLine(str);
                sw.WriteLine("---------------------------------------------------------");
                sw.Close();
            }
        }
    }
}
