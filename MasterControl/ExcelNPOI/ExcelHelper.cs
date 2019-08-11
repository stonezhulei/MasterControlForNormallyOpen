using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace ExcelNPOI
{
    public class ExcelHelper
    {
        private IWorkbook wb;
        private ICellStyle style;
        private FileStream savefile;
        private OperationExcel oe;
        private string templatePath;
        private string savedataPath;

        public ExcelHelper(string templatePath, string savedataPath)
        {
            this.templatePath = templatePath;
            this.savedataPath = savedataPath;

            string text = System.IO.Path.GetExtension(templatePath);
            string dext = System.IO.Path.GetExtension(savedataPath);
            if (text != dext)
            {
                throw new Exception("Template file and data file suffixes are inconsistent");
            }

            oe = new OperationExcel();  
        }

        public void TemplateInit()
        {
            bool inited = File.Exists(savedataPath);
            if (File.Exists(savedataPath))
            {
                return;
            }

            string extension = System.IO.Path.GetExtension(savedataPath);
            FileStream file = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                
            //根据指定的文件格式创建对应的类
            if (extension.Equals(".xls"))
            {
                wb = new HSSFWorkbook(file);
            }
            else
            {
                wb = new XSSFWorkbook(file);
            }

            try
            {
                savefile = new FileStream(savedataPath, FileMode.OpenOrCreate);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show(System.IO.Path.GetFileName(savedataPath) + " 已打开，请关闭后点击“确定”，否则无法保存数据", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    savefile = new FileStream(savedataPath, FileMode.Open, FileAccess.Write);
                    wb.Write(savefile);
                }
                catch
                {
                    MessageBox.Show(System.IO.Path.GetFileName(savedataPath) + " 打开失败，请重启软件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show(System.IO.Path.GetFileName(savedataPath) + " 打开失败，请重启软件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (wb != null && savefile != null)
            {
                wb.Write(savefile);
                savefile.Flush();
                savefile.Close();
                wb.Close();
                wb = null;
            }
        }

        public bool Write()
        {
            TemplateInit();
            return oe.AppendExcel(savedataPath, null);
        }

        public void Close()
        {
            savefile.Flush();
            savefile.Close();
        }

        public void Write1()
        {
            ISheet sheet = wb.GetSheetAt(0);

            int rowCount = sheet.LastRowNum + 1;
            int columnCount = sheet.GetRow(1).LastCellNum;

            IRow row = sheet.GetRow(rowCount);
            if (row == null)
                row = sheet.CreateRow(rowCount); // 创建新行
            for (int i = 0; i < columnCount; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(i);
            }

            WriteToFile();
        }

       

        void WriteToFile()
        {
            // Write the stream data of workbook to the root directory
            FileStream file = new FileStream(savedataPath, FileMode.Append);
            wb.Write(file);
            file.Close();
        }
    }
}
