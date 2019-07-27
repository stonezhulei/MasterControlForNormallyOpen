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

        public static void WriteCSV(string _fileName, string lineString)
        {
            StreamWriter sw = File.AppendText(_fileName);
            sw.Write(lineString);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}
