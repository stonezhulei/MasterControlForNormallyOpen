using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UITest
{
    static class Program
    {
        #region field
        /// <summary>
        /// 互斥信号量
        /// </summary>
        private static System.Threading.Mutex mutex;
        #endregion field

        #region main method
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitMiniDumper();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Program.IsSingleton("MasterControl/UITest"))
            {
                Application.Run(new Form5());
                ReleaseSingleton();
            }
            else
            {
                MessageBox.Show("软件已运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 获得互斥体
        private static bool IsSingleton(string identifying)
        {
            bool isSingleton = false; 
            mutex = new System.Threading.Mutex(false, identifying, out isSingleton);
            return isSingleton;
        }

        // 释放互斥体
        private static void ReleaseSingleton()
        {
            if (null == mutex)
            {
                return;
            }

            mutex.Close();
        }

        private static void InitMiniDumper()
        {
            // 初始化min dumper
            MiniDumper.DumperDir = System.IO.Path.Combine(Environment.CurrentDirectory, @"log\");
            MiniDumper.MaxDumpNum = 3;
            MiniDumper.ProcessSuffix = "MasterControl";
            //string fname = @"log\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dmp";
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((obj, e) => MiniDumper.TryDump(fname));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke", Justification = "此处获取Error code，在非托管调用抛出异常情况时，可能对调试问题有用。")]
        private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            int ecode = System.Runtime.InteropServices.Marshal.GetExceptionCode();
            int lerror = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

            // 记异常日志
            Console.WriteLine(string.Format("UnhandledException expCode is {0}, last error is {1}.", ecode, lerror));
            Console.WriteLine(e.ExceptionObject);
            MiniDumper.RecordDump();
        }
        #endregion main method
    }
}
