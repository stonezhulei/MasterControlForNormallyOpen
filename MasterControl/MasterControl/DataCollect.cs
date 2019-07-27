using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OmronPlc;

namespace MasterControl
{
    public class DataCollect
    {
        private Para para;
        private bool needStop;

        private FinsProxy[] fins = new FinsProxy[Para.PLCNUM];
        public static readonly object[] aLocker = new object[Para.OPNUM];
        public static readonly object[] rLocker = new object[Para.OPNUM];
        public static readonly object[] pLocker = new object[Para.PLCNUM];

        private ushort[] status = new ushort[Para.OPNUM];
        private long[] results = new long[Para.OPNUM];
        private long[] okCount = new long[Para.OPNUM];

        public delegate void OnShownTitle(int id, string title);
        public delegate void OnShownStatus(int id, int vsalue);
        public delegate void OnShownResult(int id, long count, long ok);
        public OnShownStatus onShownStatus = null;
        public OnShownResult onShownResult = null;
        public OnShownTitle onShownTitle = null;

        public DataCollect(string path)
        {
            para = new Para(path);
            for (int i = 0; i < Para.PLCNUM; i++)
            {
                pLocker[i] = new object();
                fins[i] = new FinsProxy(para.IpConfig[i]);
                bool connected = fins[i].Connect();
            }

            for (int i = 0; i < Para.OPNUM; i++)
            {
                aLocker[i] = new object();
                rLocker[i] = new object();
            }
        }

        public void Start()
        {
            // 监听清空统计
            ThreadPool.QueueUserWorkItem(o => ListenResultClear());

            // 监听 PLC 状态
            ThreadPool.QueueUserWorkItem(o => ListenStatus());

            // 监听状态刷新
            for (int op = 0; op < Para.OPNUM; op++)
            {
                onShownTitle(op, para.WorklistName[op]);
                ThreadPool.QueueUserWorkItem(Run, op);
            }
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
                    SendPLCAck(op);
                    noRead = false;
                }

                if (!noRead && IsEndState(op))
                {
                    noRead = true;
                }

                Thread.Sleep(500);
            }
        }

        public void ListenStatus()
        {
            while (!needStop)
            {
                for (int op = 0; op < Para.OPNUM; op++)
                {
                    setStatus(op);
                }
                //Parallel.For(0, Para.OPNUM, op => setAlarm(op));
            }
        }

        public void ListenResultClear()
        {
            bool noRead = true;
            while (!needStop)
            {
                if (noRead && IsResultNeedClear(Para.OPTOTAL))
                {
                    // 清空
                    clearResult();
                    SendPLCAck(Para.OPTOTAL);
                    noRead = false;
                }

                if (!noRead && !IsResultNeedClear(Para.OPTOTAL))
                {
                    noRead = true;
                }

                Thread.Sleep(500);
            }
        }

        public void SendPLCAck(int op)
        {
            WriteDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl], (ushort)10);
        }

        public bool IsResultNeedClear(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.ClearControl]);
            return 9 == value;
        }

        public bool IsResultReady(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl]);
            return 9 == value;
        }

        public bool IsEndState(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl]);
            return 8 == value;
        }

        public void clearResult()
        {
            for (int op = 0; op < Para.OPNUM; op++)
            {
                lock (rLocker[op])
                {
                    results[op] = 0;
                    okCount[op] = 0;
                    onShownResult(op, results[op], okCount[op]);
                }
            }
        }

        public void setResult(int op)
        {
            lock (rLocker[op])
            {
                if (op != Para.OPTOTAL)
                {
                    results[op]++;
                    ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STA]);
                    okCount[op] = ('9' == value) ? okCount[op] + 1 : okCount[op];
                }
                else
                {
                    long okNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.OKNUM]);
                    long ngNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.NGNUM]);
                    long totalNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.TOTALNUM]);
                    results[op] = totalNum;
                    okCount[op] = okNum;
                }

                onShownResult(op, results[op], okCount[op]);
            }
        }

        public void setStatus(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STA_STU]);

            lock (rLocker[op])
            {
                status[op] = value;
                onShownStatus(op, value);
            }
        }

        private uint ReadDM1(int pid, ushort address)
        {
            uint value = 0;

            lock (pLocker[pid])
            {
                fins[pid].ReadDM(address, ref value);
            }

            return value;
        }

        private ushort ReadDM(int pid, ushort address)
        {
            ushort value = 0;

            lock (pLocker[pid])
            {
                fins[pid].ReadDM(address, ref value);
            }

            return value;
        }

        private void WriteDM(int pid, ushort address, ushort data)
        {
            lock (pLocker[pid])
            {
                fins[pid].WriteDM(address, data);
            }
        }
    }
}
