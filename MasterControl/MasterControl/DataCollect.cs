using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using OmronPlc;
using ExcelTool;
using FileTool;

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
        private long[] alCount = new long[Para.OPNUM];
        private long[] okCount = new long[Para.OPNUM];

        private byte[][] bytes = new byte[Para.PLCBUFNUM][];
        private long[] fileLines = new long[Para.PRESSNUM];

        public delegate void OnShownTitle(int id, string title);
        public delegate void OnShownStatus(int id, int vsalue);
        public delegate void OnShownYeild(int id, long count, long ok);
        public delegate void OnShownResult(StringCollection data);
        public OnShownResult onShownResult = null;
        public OnShownStatus onShownStatus = null;
        public OnShownYeild onShownYeild = null;
        public OnShownTitle onShownTitle = null;

        private delegate bool OnRun(int op);

        private ExcelHelper excelHelper;

        public DataCollect(string path)
        {
            string fileName = DateTime.Now.ToString("yyMMdd") + ".xls";

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

            for (int i = 0; i < Para.PLCBUFNUM; i++)
            {
                bytes[i] = new byte[Para.PLCBUFLEN];
            }

            // 加载历史数据
            para.LoadYeildData(alCount, okCount);
        }

        public void Start()
        {
            // 刷新历史数据
            onShownResult(para.LoadResultData());
            for (int op = 0; op < Para.OPNUM; op++)
            {
                onShownYeild(op, alCount[op], okCount[op]);
            }

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

                //Thread.Sleep(10);
            }
        }

        private void Test1(int op, OnRun start, OnRun end, OnRun action, OnRun ack)
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

                //Thread.Sleep(10);
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
            Test1(Para.OPTOTAL, IsResultReady, IsResultReady, setResult, SendPLCAck1);
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
                //Thread.Sleep(10);
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
                    alCount[op] = 0;
                    okCount[op] = 0;
                    onShownYeild(op, alCount[op], okCount[op]);
                    para.SaveYeildData(alCount, okCount);
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
                    alCount[op]++;
                    ushort value = ReadDM(para.Worklist[op][Para.PID], para.Worklist[op][Para.STA]);
                    okCount[op] = ('9' == value) ? okCount[op] + 1 : okCount[op];
                }
                else
                {
                    long okNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.OKNUM]);
                    long ngNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.NGNUM]);
                    long totalNum = ReadDM1(para.Worklist[op][Para.PID], para.Worklist[op][Para.TOTALNUM]);
                    alCount[op] = totalNum;
                    okCount[op] = okNum;
                }

                onShownYeild(op, alCount[op], okCount[op]);
                para.SaveYeildData(alCount, okCount);
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
                // OP10 - OP70
                Array.Clear(bytes[0], 0, 120);
                fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr1], bytes[0]);
                //while (!fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr1], bytes[0]))
                //{
                //    Thread.Sleep(100);
                //}

                // OP80 - OP110
                Array.Clear(bytes[1], 0, 120);
                fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr2], bytes[1]);
                //while (!fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr2], bytes[1]))
                //{
                //    Thread.Sleep(100);
                //}

                // OP120 - OP130
                Array.Clear(bytes[2], 0, 120);
                fins[pid].ReadDM(1600, bytes[2]);
                //while (!fins[pid].ReadDM(para.Worklist[op][Para.TOPCDataStr3], bytes[2]))
                //{
                //    Thread.Sleep(100);
                //}
            }

            StringCollection data = new StringCollection();
            data.Add(System.Text.Encoding.Default.GetString(bytes[0]));
            data.Add(System.Text.Encoding.Default.GetString(bytes[1]));
            data.Add(System.Text.Encoding.Default.GetString(bytes[2]));

            //onParse1(data);

            //onParse(data);
            //onSendWeb(data);
            //onShownResult(data);
            //para.SaveResultData(data);

            return true;
        }

        public void onSendWeb(StringCollection data)
        {
            //data.Add("111");
            //data.Add("2");
            //data.Add("33");
            string s = string.Join(",", data.OfType<string>());
            Console.WriteLine(s); // 111,2,33
        }

        public void onParse1(StringCollection data)
        {
            string[] td1 = data[0].Split('\'');
            string[] td2 = data[1].Split('\'');
            string[] td3 = data[2].Split('\'');

            data.Clear();

            string s;
            // 1.时间 'A0731132230' -> 2019-07-31 13:22:30
            s = td1[0];
            string s1 = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                s[0] - 'A' + 2019,
                s.Substring(1, 2),
                s.Substring(3, 2),
                s.Substring(5, 2),
                s.Substring(7, 2),
                s.Substring(9, 2));

            // 2.产品类型

            // 3.工单条码

            // 4.器具条码

            // 5.条码

            // 6.RFID序列号 @280980980

            // 7.OP10结果 0109  -> 01-OP10 09-OK 01-NG

            // 7.OP10过渡套视觉检测 A0731 150958

            // 8.OP10程序号

            // 9.OP20测虑网视觉检测 0209 A0731 150958 -> 02-OP20 09-OK 01-NG

            // 10.OP30常开阀结果 0309  -> 03-OP30 09-OK 01-NG

            // 11.OP30GT测试高度 21579 -> 21.579

            // 12.OP30程序号 CK045 -> CK0.45  TC100 -> TC1.0

            // 13.OP40结果 0409  -> 04-OP40 09-OK 01-NG

            // 14.OP40压力值 3061  -> 3.061

            // 15.OP40位移值 49555  -> 49.555

            // 16.OP40程序号 0001 -> A-Pr01 0002 -> B-Pr01 ?? 【9种】

            // 17.OP40文件存储

            // 18.OP50结果 0509 -> 05-OP50 09-OK 01-NG

            // 19.OP50GT测试深度 12023 -> 12.023  12030 -> 12.030

            // 20.OP50程序号 CK045 -> CK0.45  TC100 -> TC1.0

            // 21.OP60结果 0609 -> 06-OP60 09-OK 01-NG

            // 22.OP60弹簧型号  A -> A振盘

            // 23.OP70结果 0709 -> 07-OP70 09-OK 01-NG

            // 24.OP70阀杆理论值 12213 -> 12.213

            // 25.OP70阀杆实际值 12213 -> 12.213

            // 26.OP70气隙值 186 -> 0.186

            // 27.OP80结果 0809 -> 08-OP80 09-OK 01-NG

            // 28.OP90结果 0909 -> 09-OP90 09-OK 01-NG

            // 29.OP90初始行程值 755 -> 0.755

            // 30.OP90最终行程值 755 -> 0.755

            // 31.OP90压力值 35 -> 0.35

            // 32.OP90位移值 70021 -> 70.021

            // 33.OP90程序号 0001 - 0009 -> A-CK0.45 A-CK0.65 【9种】

            // 34.OP90文件存储

            // 35.OP100结果 1009 -> 10-OP100 09-OK 01-NG

            // 36.OP100焊接功率

            // 37.OP100焊接时间 22 -> 2.2

            // 38.OP100焊接转速 6 -> 0.6

            // 39.OP110结果 1109 -> 11-OP110 09-OK 01-NG

            // 40.OP110二维码

            // 41.OP120结果 1209 -> 12-OP120 09-OK 01-NG

            // 42.OP120测试行程值 241 -> 0.241

            // 43.OP120程序号 A -> A-CK0.45  B -> CK0.65 【9种】

            // 44.OP130结果 1309 -> 13-OP130 09-OK 01-NG

            // 45.OP130单项密封圈视觉检测 A0731 140958

            // 46.OP140结果 1409 -> 14-OP140 09-OK 01-NG

            // 47.OP140压力值  65 -> 0.65

            // 48.OP140位移值  70021 -> 70.021

            // 49.OP140程序号 0001 -> Pr01 0002 -> Pr02？？？？

            // 50.OP140文件存储

            // 51.OP140滤网铆点视觉检测 A0731 14112

            // 51.OP150结果 1509 -> 15-OP150 09-OK 01-NG

            // 52.OP150测试气压 1300 -> 130.0

            // 53.OP150泄露量 100 -> 1

            // 54.OP150充气气压 1321 -> 132.1

            // 55.OP150程序号 A -> A-Pr01 B -> B-Pr01 

            // 56.OP150文件存储

            // 57.OP160结果 1609 -> 16-OP160 09-OK 01-NG

            // 58.OP160测试气压 99 -> 9.9

            // 59.OP160泄漏量

            // 60.OP160 P1值 99 -> 9.9

            // 61.OP160 P2值 421 -> 4.21

            // 62.OP160 P2-P1值 999 -> 9.99

            // 63.OP160程序号 A -> A-CK0.45  B -> CK0.65 【9种】

            // 64.OP160文件存储

            // 65.OP170结果 1709 -> 17-OP170 09-OK 01-NG
        }

        public void onParse(StringCollection data)
        {
            string[] td1 = data[0].Split(',');
            string[] td2 = data[1].Split(',');
            string[] td3 = data[2].Split(',');

            if (td1.Length < 16 || td2.Length < 17 || td3.Length < 10)
            {
                return;
            }

            data.Clear();

            // OP20
            data.Add(td1[2]);
            data.Add(td1[3]);
            data.Add(td1[4]);
            data.Add(td1[4]);

            // OP30      
            data.Add(ParseFloat(td1[7], 1000, "KN"));
            data.Add(ParseFloat(td1[8], 1000, "mm"));
            //data.Add();
            data.Add(td1[6]);

            // OP50
            data.Add(ParseFloat(td1[9], 1000, "mm"));

            // OP60
            data.Add(ParseFloat(td1[10], 1000, "mm"));
            data.Add(ParseFloat(td1[11], 1000, "mm"));

            // OP70
            data.Add(ParseFloat(td1[12], 1000, "mm"));
            data.Add(ParseFloat(td1[14], 1000, "KN"));
            data.Add(ParseFloat(td1[15], 1000, "mm"));
            data.Add(td1[13]);// == 调换

            // OP80
            data.Add(td2[16]);

            // OP90
            data.Add(ParseFloat(td2[5], 1000, "mm"));

            // OP100
            data.Add(td2[2]);
            data.Add(td2[3].Substring(1));
            data.Add(td2[4]);

            // OP110-1
            data.Add(td2[6]);
            data.Add(td2[7].Substring(1));
            data.Add(td2[8]);

            // OP110-2
            data.Add(td2[9]);
            data.Add(td2[10].Substring(1));
            data.Add(td2[11]);

            // OP110-3
            data.Add(ParseFloat(td2[12], 1000, "KN"));
            data.Add(ParseFloat(td2[13], 100, "mm"));
            data.Add(td2[14]);

            // OP120
            data.Add(ParseFloat(td3[2], 1000, "bar"));
            data.Add(ParseFloat(td3[3], 1000, "ml/min"));
            data.Add(ParseFloat(td3[4], 1000, "Mpa"));
            data.Add(td3[5]);

            // OP130
            data.Add(ParseFloat(td3[6], 10, "pa"));
            data.Add(ParseFloat(td3[7], 100, "bar"));
            data.Add(ParseFloat(td3[8], 100, "bar"));
            data.Add(td3[9]);

            Debug.Assert(data.Count == Para.UISHOWNUM, string.Format("总站数据长度 > {0}, 请修改否则 UI 无法显示", Para.UISHOWNUM));
        }

        public string ParseFloat(string data, int n, string u)
        {
            float fdata;
            string result = "";
            if (float.TryParse(data, out fdata))
            {
                result = fdata / n + " " + u;
            }

            return result;
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
                    total_lines = FileHelper.CheckFileExist(bkPath) ? FileHelper.GetTotalLines(bkPath) : FileHelper.GetTotalLines(path);
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
            FileHelper.Write(lineString.ToString(), path, bkPath);
        }

    }
}
