using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OmronPlc;

namespace MasterControl
{
    public enum CTRL
    {
        PID = 0,
        STAControl,
        STA,
        STA_STU,
    }

    public partial class Form1 : Form
    {
        private static object pLocker = new object();
        private static object rLocker = new object();
        private static object aLocker = new object();

        private const int NUM = 4;
        private const int PLCADDRSIZE = 4;
        private const int OPNUM = 18;

        private const int PID = 0;
        private const int STAControl = 1;
        private const int STA = 2;
        private const int STA_STU = 3;

        private bool needStop = false;

        private int[] alarms = new int[OPNUM];
        private int[] results = new int[OPNUM];
        private List<ushort[]> worklist = new List<ushort[]>();

        private bool systemInited = false;
        private IniFiles iniManger = new IniFiles(Application.StartupPath + "\\plcAddress.ini");

        private FinsProxy[] fins = {
            new FinsProxy("192.168.9.17"),
            new FinsProxy("192.168.9.15"),
            new FinsProxy("192.168.0.1"),
            new FinsProxy("192.168.0.1"),
        };
 
        public Form1()
        {
            InitializeComponent();

            LoadConfig();
            Init();
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        public void LoadConfig()
        {

            for (int op = 0; op < OPNUM; op++)
            {
                ushort[] plcAddr = new ushort[PLCADDRSIZE];
                plcAddr[PID] = (ushort)(iniManger.ReadInteger("OP" + (op + 1), "PID", 0) - 1);
                plcAddr[STAControl] = (ushort)iniManger.ReadInteger("OP" + (op + 1), "STAControl", 0);
                plcAddr[STA] = (ushort)iniManger.ReadInteger("OP" + (op + 1), "STA", 0);
                plcAddr[STA_STU] = (ushort)iniManger.ReadInteger("OP" + (op + 1), "STA_STU", 0);

                worklist.Add(plcAddr);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < NUM; i++)
            {
                fins[i].Connect();
            }

            // 监听 PLC 报警
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (!needStop)
                {
                    if (!systemInited)
                    {
                        continue;
                    }

                    for (int op = 0; op < OPNUM; op++)
                    {
                        setAlarm(op);
                    }
                }
            });

            // 监听监测过程
            for (int op = 0; op < OPNUM; op++)
            {
                ThreadPool.QueueUserWorkItem(Run, op);
            }

            // UI刷新
            timer.Start();
        }

        public void Run(Object o)
        {
            int op = (int)o;
            bool noRead = true;
            while (!needStop)
            {
                if (noRead && IsResultReady(op))
                {
                    setResult(op);
                    SendAck(op);
                    noRead = false;
                }

                if (!noRead && IsEndState(op))
                {
                    noRead = true;
                }

                Thread.Sleep(500);
            }
        }

        public void setResult(int op)
        {
            ushort value = ReadDM(worklist[op][PID], worklist[op][STA]);

            lock(rLocker)
            {
                results[op] = value;
            }
        }

        public int getResult(int op)
        {
            int value = 0;

            lock(rLocker)
            {
                value = results[op];
            }

            return value;
        }

        public void setAlarm(int op)
        {
            ushort value = ReadDM(worklist[op][PID], worklist[op][STA_STU]);

            lock (rLocker)
            {
                alarms[op] = value;
            }
        }

        public int getAlarm(int op)
        {
            int value = 0;

            lock (aLocker)
            {
                value = alarms[op];
            }

            return value;
        }

        public ushort ReadDM(int pid, ushort address)
        {
            ushort value = 0;

            lock (pLocker)
            {
                fins[pid].ReadDM(address, ref value);
            }

            return value;
        }

        public void WriteDM(int pid, ushort address, ushort data)
        {
            lock (pLocker)
            {
                fins[pid].WriteDM(address, data);
            }
        }

        public void SendAck(int op)
        {
            WriteDM(worklist[op][PID], worklist[op][STAControl], (ushort)10);
        }

        public bool IsResultReady(int op)
        {
            ushort value = ReadDM(worklist[op][PID], worklist[op][STAControl]);
            return 9 == value;
        }

        public bool IsEndState(int op)
        {
            ushort value = ReadDM(worklist[op][PID], worklist[op][STAControl]);
            return 8 == value;
        }

        private int IsPLCAlarm(int op)
        {
            int value = getAlarm(op);
            return value;
        }

        /// <summary>
        /// 刷新 UI
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            for (int op = 0; op < OPNUM; op++)
            {
                int value = getResult(op);

                Button btnAlarm = null;
                Button btnResult = null;

                string result = "B" + (op + 1) + "_2";
                string alarm = "B" + (op + 1) + "_3";

                foreach (Control cp1 in this.Controls)
                {
                    if (!(cp1 is Panel)) continue;
                    foreach (Control cp in cp1.Controls)
                    {
                        if (!(cp is GroupBox)) continue;
                        foreach (Control cc in cp.Controls)
                        {
                            if (cc.Name == result)
                            {
                                btnResult = cc as Button;
                            }

                            if (cc.Name == alarm)
                            {
                                btnAlarm = cc as Button;
                            }
                        }

                    }
                }

                RefreshAlarm(op, btnAlarm);
                RefreshResult(op, btnResult);
            }

            if (!systemInited)
            {
                systemInited = true;
            }
        }

        void RefreshResult(int op, Button btnResult)
        {
            int value = getResult(op);
            if (57 == value)
            {
                btnResult.BackColor = Color.Green;
                //Console.WriteLine("{0}:结果 OK", op); // OK
            }
            else if (49 == value)
            {
                btnResult.BackColor = Color.Red;
                //Console.WriteLine("{0}:结果 NG", op); // NG
            }
        }

        void RefreshAlarm(int op, Button btnAlarm)
        {
            int alarm = IsPLCAlarm(op);

            if (alarm == 9)
            {
                btnAlarm.BackColor = Color.Green;
                //Console.WriteLine("{0}:正常生产", op);
            }
            else if (alarm == 1)
            {
                btnAlarm.BackColor = Color.Red;
                //Console.WriteLine("{0}:本站异常", op);
            }
        }
    }
}
