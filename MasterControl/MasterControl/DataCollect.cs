using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private long[] fileLines = new long[Para.PRESSNUM];
        StringCollection data = new StringCollection();

        public delegate void OnShownTitle(int id, string title);
        public delegate void OnShownStatus(int id, int vsalue);
        public delegate void OnShownYeild(int id, long count, long ok);
        public delegate void OnShownResult(StringCollection data);
        public OnShownResult onShownResult = null;
        public OnShownStatus onShownStatus = null;
        public OnShownYeild onShownYeild = null;
        public OnShownTitle onShownTitle = null;

        private delegate bool OnRun(int op);

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
            // 监听数据汇总
            ThreadPool.QueueUserWorkItem(o => ListenResult());

            // 监听清空统计
            ThreadPool.QueueUserWorkItem(o => ListenYeildClear());

            // 监听 PLC 状态
            ThreadPool.QueueUserWorkItem(o => ListenStatus());

            // 监听状态刷新
            for (int op = 0; op < Para.OPNUM; op++)
            {
                onShownTitle(op, para.WorklistName[op]);
                ThreadPool.QueueUserWorkItem(ListenYeild, op);
            }

            // 监听压机数据
            for (int id = 0; id < para.Presslist.Count; id++)
            {
                ThreadPool.QueueUserWorkItem(ListenPressResult, id);
            }
        }
        
        private void Test(int op, OnRun start, OnRun end, OnRun action, OnRun ack)
        {
            bool noRead = true;
            while (!needStop)
            {
                if (noRead && start(op))
                {
                    // 清空
                    action(op);
                    ack(op);
                    noRead = false;
                }

                if (!noRead && !end(op))
                {
                    noRead = true;
                }

                //Thread.Sleep(500);
            }
        }

        public void ListenPressResult(Object o)
        {
            Test((int)o, IsPressResultReady, IsPressResultReady, setPressResult, SendPLCPressAck);
        }

        public void ListenYeild(Object o)
        {
            Test((int)o, IsYeildReady, IsEndState, setYeild, SendPLCAck);
        }

        public void ListenYeildClear()
        {
            Test(Para.OPTOTAL, IsYeildNeedClear, IsYeildNeedClear, clearYeild, SendPLCAck);
        }

        public void ListenResult()
        {
            Test(Para.OPTOTAL, IsResultReady, IsResultReady, setResult, SendPLCAck1);
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

        public bool IsResultReady(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.ALLDataContorl]);
            return 9 == value;
        }

        public bool IsYeildNeedClear(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.ClearControl]);
            return 9 == value;
        }

        public bool IsYeildReady(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl]);
            return 9 == value;
        }

        public bool IsEndState(int op)
        {
            ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl]);
            return 8 == value;
        }

        public bool SendPLCAck(int op)
        {
            WriteDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STAControl], (ushort)10);
            return true;
        }

        public bool SendPLCAck1(int op)
        {
            WriteDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.ALLDataContorl], (ushort)10);
            return true;
        }

        public bool clearYeild(int optotal)
        {
            for (int op = 0; op < Para.OPNUM; op++)
            {
                lock (rLocker[op])
                {
                    results[op] = 0;
                    okCount[op] = 0;
                    onShownYeild(op, results[op], okCount[op]);
                }
            }

            return true;
        }

        public bool setYeild(int op)
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

                onShownYeild(op, results[op], okCount[op]);
            }

            return true;
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

        public bool setResult(int op)
        {
            int pid = para.Worklist[op][Para.PID];
            lock (pLocker[pid])
            {
                byte[][] bytes = new byte[3][];

                // OP10 - OP70
                bytes[0] = new byte[120];
                fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr1], bytes[0]);

                // OP80 - OP110
                bytes[1] = new byte[120];
                fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr2], bytes[1]);

                // OP120 - OP130
                bytes[2] = new byte[120];
                fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr3], bytes[2]);

                data.Clear();
                data.Add(System.Text.Encoding.Default.GetString(bytes[0]));
                data.Add(System.Text.Encoding.Default.GetString(bytes[1]));
                data.Add(System.Text.Encoding.Default.GetString(bytes[2]));

                onShownResult(data);
            }

            return true;
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

        private uint ReadDM1(int pid, ushort address)
        {
            uint value = 0;

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

        private void WriteDM(int pid, ushort address, byte[] data)
        {
            lock (pLocker[pid])
            {
                fins[pid].WriteDM(address, data);
            }
        }

        public bool IsPressResultReady(int id)
        {
            ushort value = ReadDM(para.Presslist[id][Para.PID], para.Presslist[id][Para.STAControl]);
            return 9 == value;
        }

        public bool SendPLCPressAck(int id)
        {
            WriteDM(para.Presslist[id][Para.PID], para.Presslist[id][Para.STAControl], (ushort)10);
            return true;
        }

        public bool setPressResult(int id)
        {
            uint pressure = ReadDM1(para.Presslist[id][Para.PID], para.Presslist[id][Para.PRESSURE]);
            uint position = ReadDM1(para.Presslist[id][Para.PID], para.Presslist[id][Para.POSITION]);

            DateTime dt = DateTime.Now;
            string fileName = "Y" + (id + 1) + dt.ToString("MMdd");
            byte[] bytes = System.Text.Encoding.Default.GetBytes(fileName);
            WriteDM(para.Presslist[id][Para.PID], para.Presslist[id][Para.FILENAME], bytes);
            WriteResultToFile(id, fileName, dt.ToString(), pressure, position);
            return true;
        }

        public void WriteResultToFile(int id, string fileName, string dt, uint pressure, uint position)
        {
            long total_lines = 1;
            StringBuilder lineString = new StringBuilder();

            string dir = @"D:\Press";
            string path = dir + "\\" + fileName + ".csv";
	        string bkPath = dir + "\\" + fileName + ".txt";

            if (FileHelper.CheckFileExist(path))
            {
                try
                {
                    total_lines = FileHelper.CheckFileExist(bkPath) ? FileHelper.ReadLines(bkPath) : FileHelper.ReadLines(path);
                    fileLines[id] = total_lines;
                }
                catch
                {
                    total_lines = fileLines[id];
                }
            }
            else
            {
                fileLines[id] = total_lines;
                lineString.Append("NO, DATETIME, PRESS, Position\n");
            }

            // 文件内容
            lineString.Append(string.Format("{0}, {1}, {2}, {3}\n", total_lines, dt, pressure, position));
            FileHelper.SaveFile(lineString.ToString(), path, bkPath);
        }
    }
}
