using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UITest
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string fname = @"log\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".dmp";
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((obj, e) => MiniDumper.TryDump(fname));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Program.IsSingleton("MasterControl/UITest"))
            {
                Application.Run(new Form3());
            }
            else
            {
                MessageBox.Show("软件已运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 限制单例运行
        public static bool IsSingleton(string identifying)
        {
            bool isSingleton = false; 
            mutex = new System.Threading.Mutex(true, identifying, out isSingleton);
            return isSingleton;
        }
    }
}
