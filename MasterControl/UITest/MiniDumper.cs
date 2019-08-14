using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Globalization;

namespace UITest
{
    public static class MiniDumper
    {
        [Flags]
        public enum MiniDumpType : uint
        {
            // From dbghelp.h:
            Normal = 0x00000000,
            WithDataSegs = 0x00000001,
            WithFullMemory = 0x00000002,
            WithHandleData = 0x00000004,
            FilterMemory = 0x00000008,
            ScanMemory = 0x00000010,
            WithUnloadedModules = 0x00000020,
            WithIndirectlyReferencedMemory = 0x00000040,
            FilterModulePaths = 0x00000080,
            WithProcessThreadData = 0x00000100,
            WithPrivateReadWriteMemory = 0x00000200,
            WithoutOptionalData = 0x00000400,
            WithFullMemoryInfo = 0x00000800,
            WithThreadInfo = 0x00001000,
            WithCodeSegs = 0x00002000,
            WithoutAuxiliaryState = 0x00004000,
            WithFullAuxiliaryState = 0x00008000,
            WithPrivateWriteCopyMemory = 0x00010000,
            IgnoreInaccessibleMemory = 0x00020000,
            ValidTypeFlags = 0x0003ffff,
        }

        enum ExceptionInfo
        {
            None,
            Present
        }


        public static string DumperDir { get; set; }

        public static int MaxDumpNum { get; set; }

        public static string ProcessSuffix { get; set; }

        //typedef struct _MINIDUMP_EXCEPTION_INFORMATION {
        //    DWORD ThreadId;
        //    PEXCEPTION_POINTERS ExceptionPointers;
        //    BOOL ClientPointers;
        //} MINIDUMP_EXCEPTION_INFORMATION, *PMINIDUMP_EXCEPTION_INFORMATION;
        [StructLayout(LayoutKind.Sequential, Pack = 4)]  // Pack=4 is important! So it works also for x64!
        struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptionPointers;
            [MarshalAs(UnmanagedType.Bool)]
            public bool ClientPointers;
        }

        //BOOL
        //WINAPI
        //MiniDumpWriteDump(
        //    __in HANDLE hProcess,
        //    __in DWORD ProcessId,
        //    __in HANDLE hFile,
        //    __in MINIDUMP_TYPE DumpType,
        //    __in_opt PMINIDUMP_EXCEPTION_INFORMATION ExceptionParam,
        //    __in_opt PMINIDUMP_USER_STREAM_INFORMATION UserStreamParam,
        //    __in_opt PMINIDUMP_CALLBACK_INFORMATION CallbackParam
        //    );
        // Overload requiring MiniDumpExceptionInformation
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

        // Overload supporting MiniDumpExceptionInformation == NULL
        [DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();

        static bool Write(SafeHandle fileHandle, MiniDumpType dumpType, ExceptionInfo exceptionInfo)
        {
            Process currentProcess = Process.GetCurrentProcess();
            IntPtr currentProcessHandle = currentProcess.Handle;
            uint currentProcessId = (uint)currentProcess.Id;
            
            MiniDumpExceptionInformation exp;
            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = false;
            exp.ExceptionPointers = IntPtr.Zero;
            if (exceptionInfo == ExceptionInfo.Present)
            {
                exp.ExceptionPointers = Marshal.GetExceptionPointers();
            }

            return exp.ExceptionPointers == IntPtr.Zero ?
                MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)dumpType, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) :
                MiniDumpWriteDump(currentProcessHandle, currentProcessId, fileHandle, (uint)dumpType, ref exp, IntPtr.Zero, IntPtr.Zero);
        }

        static bool Write(SafeHandle fileHandle, MiniDumpType dumpType)
        {
            return Write(fileHandle, dumpType, ExceptionInfo.None);
        }

        public static void TryDump(String dmpPath, MiniDumpType dmpType = MiniDumpType.Normal)
        {
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, dmpPath);
                var dir = Path.GetDirectoryName(path);
                if (dir != null && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    Write(fs.SafeFileHandle, dmpType);
                    fs.Flush();
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("TryDump Error!" + ex.Message);
            }
        }

        /// <summary>
        /// 转储当前进程的异常 Dump.
        /// </summary>
        public static void RecordDump()
        {
            // 捕获dump
            string fmtstr = "yyyy-MM-dd HH-mm-ss";
            string dumpath = System.IO.Path.Combine(
                MiniDumper.DumperDir,
                "MiniDmp{" + DateTime.Now.ToString(fmtstr, CultureInfo.CurrentCulture) + "}" + MiniDumper.ProcessSuffix + ".dmp");

            CheckAndCreateDumpDir();
            TryDump(dumpath, MiniDumpType.WithFullMemory | MiniDumpType.WithHandleData | MiniDumpType.WithUnloadedModules | MiniDumpType.WithProcessThreadData | MiniDumpType.WithThreadInfo);
            ClearSuperfluousDumpFiles();
        }

        /// <summary>
        /// 创建 dump 目录
        /// </summary>
        private static void CheckAndCreateDumpDir()
        {
            if (!System.IO.Directory.Exists(MiniDumper.DumperDir))
            {
                try
                {
                    SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                    FileSystemAccessRule newRule = new FileSystemAccessRule(sid, FileSystemRights.FullControl, inherits, PropagationFlags.NoPropagateInherit, AccessControlType.Allow);
                    DirectorySecurity ds = new DirectorySecurity();
                    ds.AddAccessRule(newRule);
                    System.IO.Directory.CreateDirectory(MiniDumper.DumperDir, ds);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("CreateDirectory Dumper Error!" + ex.Message);
                }
            }
        }

        /// <summary>
        ///  删除多余的 dump 文件
        /// </summary>
        private static void ClearSuperfluousDumpFiles()
        {
            string[] dumpfiles = Directory.GetFiles(MiniDumper.DumperDir, "*.dmp");

            // 根据创建时间排序
            var query = (from f in dumpfiles
                         let fi = new FileInfo(f)
                         orderby fi.CreationTime descending
                         select fi.FullName).Take(MiniDumper.MaxDumpNum);
            string[] retain = query.ToArray(); // 需要保留的 dump 文件
           
            foreach (var f in dumpfiles)
            {
                if (!retain.Contains(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine("CreateDirectory Dumper Error!" + ex.Message);
                    }
                }
            }
        }
    }
}