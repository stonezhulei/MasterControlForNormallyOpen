using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTool
{
    public class FileHelper
    { 
        private StreamWriter _sw; // CSV 流
        private string _path; // CSV 路径
        private bool _append; // CSV 追加方式
        private long _lines;  // CSV 文件行数

        ///// <summary>
        ///// 以独占方式打开 CSV
        ///// </summary>
        //public FileHelper(string path, bool append)
        //{
        //    this.OpenCSV(path, append);
        //}

        private static object locker = new object();

        /// <summary>
        ///  检查文件是否存在
        /// </summary>
        public static bool CheckFileExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 检查目录是否存在
        /// </summary>
        public static bool CheckDirExist(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 创建多级目录
        /// </summary>
        public static bool CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            	return false;
            }

            return true;
        }

        /// <summary>
        /// 读取已打开流的行数
        /// </summary>
        public static long GetTotalLines(StreamReader sr)
        {
            long lines = 0;
            string lineString;
            
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            while ((lineString = sr.ReadLine()) != null)
            {
                lines++;
            }

            sw.Stop();
            return lines;
        }

        /// <summary>
        /// 读取未打开文件的行数
        /// </summary>
        public static long GetTotalLines(string path)
        {
            long lines = 0;
            using (var sr = new StreamReader(path))
            {
                lines = GetTotalLines(sr);
            }

            return lines;
        }

        /// <summary>
        ///  打开字符读取流
        /// </summary>
        public static StreamReader OpenReaderStream(string path)
        {
            if (CheckFileExist(path))
            {
                return new StreamReader(path);
            }

            return null;
        }

        /// <summary>
        ///  打开字符写入流
        /// </summary>
        public static StreamWriter OpenWriterStream(string path, bool append)
        {
            // FileStream 类操作的是字节和字节数组，而Stream类操作的是字符数据
            if (CreateDirectory(Path.GetDirectoryName(path)))
            {
                return new StreamWriter(path, append);
            }

            return null;
        }

        /// <summary>
        /// 关闭字符读取流
        /// </summary>
        public static void CloseReaderStream(StreamReader sr)
        {
            if (sr != null)
            {
                sr.Close(); sr.Dispose(); sr = null;
            }
        }

        /// <summary>
        /// 关闭字符写入流
        /// </summary>
        public static void CloseWriterStream(StreamWriter sw)
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close(); sw.Dispose(); sw = null;
            }
        }

        /// <summary>
        /// 向已打开的流中写入数据
        /// </summary>
        public static bool Write(StreamWriter sw, string lineString, bool needlf = false)
        {
            bool success = true;

            if (needlf)
            {
                sw.WriteLine(lineString);
            }
            else
            {
                sw.Write(lineString);
            }

            sw.Flush();

            return success;
        }

        /// <summary>
        /// 向已打开的流中写入数据
        /// </summary>
        public static bool Write(StreamWriter sw, StringBuilder sb, bool needlf = false)
        {
            bool success = true;

            if (needlf)
            {
                sw.WriteLine(sb.ToString());
            }
            else
            {
                sw.Write(sb.ToString());
            }

            sw.Flush();

            return success;
        }

        /// <summary>
        /// 向已打开的流中写入数据
        /// </summary>
        public static bool Write(StreamWriter sw, StringCollection data, string separator = "\r\n")
        {
            string lineString = string.Join(separator, data.OfType<string>());
            return Write(sw, lineString, separator != "\r\n");
        }

        /// <summary>
        /// 单次打开文件写入后关闭（不适应于大文件频繁写入）
        /// </summary>
        public static bool Write(string path, string lineString, bool append)
        {
            bool success = true;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                if (append)
                {
                    StreamWriter sw = File.AppendText(path);
                    sw.Write(lineString);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                else
                {
                    File.WriteAllText(path, lineString);
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// 带备份的单次打开文件写入后关闭（不适应于大文件频繁写入）
        /// </summary>
        public static bool Write(string path, string lineString, string bkPath)
        {
            bool success = true;

            try
            {
                if (CheckFileExist(bkPath))
                {
                    SetFileHile(bkPath, true);
                    success = Write(lineString, bkPath, true);
                    File.Delete(path); // 存在备份文件，尝试删除原文件
                    File.Move(path, bkPath);
                    SetFileHile(path, false);
                }
                else
                {
                    success = Write(lineString, path, true);
                    if (!success)
                    {
                        File.Move(path, bkPath);
                        SetFileHile(path, true);
                    }
                }   
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public long LFCount(string lineString)
        {
            long count = lineString.LongCount(c => c == '\n');
            return count;
        }

        /// <summary>
        /// 写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string header, string lineString)
        {
            long id = _lines;
            bool success = true;

            if (id == 0)
            {
                success = Write(_sw, header, true);
                id = success ? id + 1 : id;
            }

            StringBuilder sb = new StringBuilder();
            //string[] newlines = lineString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); // \r\n 才能分割
            string[] newlines = lineString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); // '\r' '\n' "\r\n"分割

            for (int i = 0; i < newlines.Length; i++)
            {
                sb.Append((id + i) + ", " + newlines[i] + "\r\n");
            }

            success = success && Write(_sw, sb.ToString(), false);
            _lines = success ? id + newlines.Length + 1 : id;
            return success;
        }

        /// <summary>
        /// 写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string header, StringBuilder sb)
        {
            return WriteCSV(header, sb.ToString());
        }

        /// <summary>
        /// 写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string header, StringCollection data)
        {
            string lineString = string.Join("\r\n", data.OfType<string>());
            return WriteCSV(header, lineString);
        }

        /// <summary>
        /// 带切换文件的写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string path, string header, string lineString, bool append)
        {
            this.ReOpenCSV(path, append);
            return WriteCSV(header, lineString);
        }

        /// <summary>
        /// 带切换文件的写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string path, string header, StringBuilder sb, bool append)
        {
            return WriteCSV(path, header, sb.ToString(), append);
        }

        /// <summary>
        /// 带切换文件的写 CSV（独占方式）
        /// </summary>
        public bool WriteCSV(string path, string header, StringCollection data, bool append)
        {
            string lineString = string.Join("\r\n", data.OfType<string>());
            return WriteCSV(path, header, lineString, append);
        }

        /// <summary>
        /// 切换 CSV
        /// </summary>
        public void ReOpenCSV(string path, bool append)
        {
            if (_sw == null || path != this._path || append != this._append)
            {
                lock (locker)
                {
                    if (_sw == null || path != this._path || append != this._append)
                    {
                        this.CloseCSV();
                        this.OpenCSV(path, append);
                    }
                }
            }
        }

        /// <summary>
        /// 打开 CSV
        /// </summary>
        public void OpenCSV(string path, bool append)
        {
            StreamReader sr;
            if ((sr = OpenReaderStream(path)) != null)
            {
                _lines = GetTotalLines(sr);
                CloseReaderStream(sr);
            }
            else
            {
                _lines = 0;
            }

            _sw = OpenWriterStream(path, append);

            if (_sw != null)
            {
                this._path = path;
                this._append = append;
            }
            else
            {
                throw new Exception(Path.GetFileName(path) + " Open fail");
            }
        }

        /// <summary>
        /// 关闭 CSV
        /// </summary>
        public void CloseCSV()
        {
            CloseWriterStream(_sw);
            _sw = null;
        }

        /// <summary>
        ///  设置文件隐藏 / 可见
        /// </summary>
        public static void SetFileHile(string path, bool hide)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Attributes = hide ? FileAttributes.Hidden : FileAttributes.Normal;
            }
        }

        /// <summary>
        /// 按指定格式获取时间字符串
        /// </summary>
        public static string GetDateTimeString(string format)
        {
            string s;
            //s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); // 2019-08-09 11:42:27
            //s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); // 2019-08-09 23:42:27.044
            s = DateTime.Now.ToString(format);
            return s;
        }
    }
}
