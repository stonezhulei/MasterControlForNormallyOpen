using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

namespace MasterControl
{
    class FileHelper
    {
        public static bool CheckFileExist(string _fileName)
        {
            return File.Exists(_fileName);
        }

        // 读取文件行数
        public static int ReadLines(string _fileName)
        {
            Stopwatch sw = new Stopwatch();
            var path = _fileName;
            int lines = 0;

            //按行读取
            sw.Restart();
            using (var sr = new StreamReader(path))
            {
                var ls = "";
                while ((ls = sr.ReadLine()) != null)
                {
                    lines++;
                }
            }
            sw.Stop();
            return lines;
        }

        public static bool SaveFile(string lineString, string _fileName, bool app, bool hide)
        {
            bool success = true;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fileName));

                if (app)
                {
                    StreamWriter sw = File.AppendText(_fileName);
                    sw.Write(lineString);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
                else
                {
                    File.WriteAllText(_fileName, lineString);
                }

                FileInfo fi = new FileInfo(_fileName);
                if (fi.Exists)
                {
                    fi.Attributes = hide ? FileAttributes.Hidden : FileAttributes.Normal;
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }


        public static bool SaveFile(string lineString, string path, string bkPath)
        {
            bool success = true;
            if (CheckFileExist(bkPath))
            {
                SaveFile(lineString, bkPath, true, true);
                try
                {
                    File.Delete(path); // 存在备份文件，尝试删除原文件
                    File.Move(path, bkPath);
                   
                }
                catch
                {
                    success = false;
                }
            }
            else
            {
                try
                {
                   SaveFile(lineString, path, true, false);     
                }
                catch
                {
                    success = false;
                    File.Move(path, bkPath);
                }
            }

            return success;
        }
    }
}
