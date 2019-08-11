using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.OpenXml4Net.OPC;
using NPOI.POIFS.FileSystem;


namespace ExcelTool
{
    public class ExcelHelper
    {
        private FileStream _fs; // 独占流
        private IWorkbook _wb;
        private string _path;
        private bool _append;
        private string _templatepath;
        private OperationExcel _oe = new OperationExcel();

        public ExcelHelper(string templatepath)
        {
            this._templatepath = templatepath;
        }

        private static int headerLines = 0;

        private static readonly int HeaderLines = 2;

        private static object locker = new object();

        public delegate FileStream OnOpenStream(string path);

        /// <summary>
        /// 创建单元格格式
        /// </summary>
        public static ICellStyle CreateStyle(IWorkbook wb)
        {
            // 样式
            ICellStyle style = wb.CreateCellStyle();
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            // 设置边框
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.WrapText = true; // 自动换行

            return style;
        }

        public static void CloseStream(FileStream fs)
        {
            if (fs != null)
            {
                fs.Close(); fs.Dispose(); fs = null;
            }
        }

        public static FileStream OpenReaderStream(string path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public static FileStream OpenWriterStream(string path)
        {
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
            if (Directory.Exists(dir))
            {
                return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            }

            return null;
        }

        public static FileStream OpenStreamWithPrompt(OnOpenStream onOpenStream, string path)
        {
            FileStream fs = null;

            try
            {
                fs = onOpenStream(path);
            }
            catch (FileNotFoundException fnf)
            {
                MessageBox.Show(Path.GetFileName(path) + " 文件缺失", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw fnf;
            }
            catch (IOException ioe)
            {
                DialogResult dr = MessageBox.Show(Path.GetFileName(path) + " 文件可能被打开，请“重试”", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                
                while (dr == DialogResult.Retry)
                {
                    try
                    {
                        fs = onOpenStream(path);
                        break;
                    }
                    catch
                    {
                        dr = MessageBox.Show(Path.GetFileName(path) + " 文件可能被打开，请“重试”", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    }
                }


                if (fs == null)
                {
                    MessageBox.Show(System.IO.Path.GetFileName(path) + " 文件打开失败，无法保存数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw ioe;
                } 
            }

            return fs;
        }

        private static IWorkbook OpenWorkbookWithPrompt(string path)
        {
            IWorkbook wb = null;

            try
            {
                wb = OpenWorkbook(path);
            }
            catch (FileNotFoundException fnf)
            {
                MessageBox.Show(Path.GetFileName(path) + " 模板缺失", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw fnf;
            }
            catch (IOException ioe)
            {
                DialogResult dr = MessageBox.Show(Path.GetFileName(path) + " 模板可能被打开，请关闭后“重试”", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);

                while (dr == DialogResult.Retry)
                {
                    try
                    {
                        wb = OpenWorkbook(path);
                        break;
                    }
                    catch
                    {
                        dr = MessageBox.Show(Path.GetFileName(path) + " 模板可能被打开，请“重试”", "警告", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    }
                }

                
                if (wb == null)
                {
                    MessageBox.Show(System.IO.Path.GetFileName(path) + " 模板打开失败，无法保存数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw ioe;
                } 
            }

            return wb;
        }

        /// <summary>
        /// 打开 Workbook 后关闭文件流
        /// </summary>
        private static IWorkbook OpenWorkbook(string path)
        {
            FileStream excelStream = null;

            try
            {
                //excelStream = OpenReaderStream(path);
                excelStream = OpenStreamWithPrompt(OpenReaderStream, path);

                if (POIFSFileSystem.HasPOIFSHeader(excelStream))
                {
                    return new HSSFWorkbook(excelStream);
                }

                if (POIXMLDocument.HasOOXMLHeader(excelStream))
                {
                    return new XSSFWorkbook(OPCPackage.Open(excelStream));
                }

                if (path.ToLower().EndsWith(".xlsx"))
                {
                    return new XSSFWorkbook(excelStream);
                }

                if (path.ToLower().EndsWith(".xls"))
                {
                    return new HSSFWorkbook(excelStream);
                }

                throw new Exception("Your InputStream was neither an OLE2 stream, nor an OOXML stream");
            }
            finally
            {
                CloseStream(excelStream);
            }
        }

        private static IWorkbook CreateWorkbook(string path)
        {
            if (path.ToLower().EndsWith(".xlsx"))
            {
                return new XSSFWorkbook();
            }
            else if (path.ToLower().EndsWith(".xls"))
            {
                return new HSSFWorkbook();
            }

            throw new Exception("Your InputStream was neither an OLE2 stream, nor an OOXML stream");
        }

        private static IWorkbook CreateWorkbookByTemplate(string templatePath, string savedataPath)
        {
            string text = System.IO.Path.GetExtension(templatePath).ToLower();
            string dext = System.IO.Path.GetExtension(savedataPath).ToLower();
            if (text != dext)
            {
                savedataPath = savedataPath.Replace(dext, text);
                //throw new Exception("Template file and data file suffixes are inconsistent");
            }

            if (File.Exists(savedataPath))
            {
                MessageBox.Show(Path.GetFileName(savedataPath) + " 模板文件缺失", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Template file not found");
            }

            IWorkbook wbTemplate = OpenWorkbookWithPrompt(templatePath);
            FileStream datafile = OpenStreamWithPrompt(OpenWriterStream, savedataPath);
            try
            {
                WriteToFile(datafile, wbTemplate);
            }
            finally
            {
                CloseStream(datafile);
            }

            return wbTemplate;
        }

        private static List<StringCollection> ParseSheet(IEnumerator rows)
        {
            List<StringCollection> data = new List<StringCollection>();
            while (rows.MoveNext())
            {
                IRow row = (IRow)rows.Current;
                StringCollection rdata = new StringCollection();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        rdata.Add(null);
                    }
                    else
                    {
                        rdata.Add(cell.ToString());
                    }
                }

                data.Add(rdata);
            }

            return data;
        }

        /// <summary>
        /// 读取 Workbook 中的数据到 DataTable
        /// </summary>
        public static List<DataTable> Read(string path)
        {
            List<DataTable> dtList = new List<DataTable>();

            try
            {
                IWorkbook workbook = OpenWorkbook(path);
                for (int k = 0; k < workbook.NumberOfSheets; k++)
                {
                    ISheet sheet = workbook.GetSheetAt(k);
                    IEnumerator rows = sheet.GetRowEnumerator();
                    List<StringCollection> data = ParseSheet(rows);

                    DataTable dt = new DataTable();
                    long columnNum = 0;
                    for (int r = 0; r < data.Count; r++)
                    {
                        if (data[r].Count > columnNum)
                        {
                            columnNum = data[r].Count;
                        }
                    }

                    for (int c = 0; c < columnNum; c++)
                    {
                        dt.Columns.Add(Convert.ToChar(((int)'A') + c).ToString());
                    }

                    foreach (var rdata in data)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < rdata.Count; i++)
                        {
                            dr[i] = rdata[i];
                        }

                        dt.Rows.Add(dr);
                    }

                    dtList.Add(dt);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return dtList;
        }

        #region 写 Excel 到文件流
        private static void WriteToFile(string path, IWorkbook wb)
        {
            // Write the stream data of workbook to the root directory
            FileStream fs = OpenWriterStream(path);
            wb.Write(fs);
            fs.Flush();
            fs.Close();
        }

        private static void WriteToFile(FileStream fs, IWorkbook wb)
        {
            wb.Write(fs);
            fs.Flush();
        }
        #endregion

        #region Excel 数据操作
        private static void Makeup(ISheet sheet, StringCollection data)
        {
            IRow row = CreateRow(sheet);

            for (int c = 0; c < data.Count; c++)
            {
                ICell cell = row.CreateCell(c + 1);
                cell.SetCellValue(data[c]);
            }
        }

        private static void Makeup(ISheet sheet, List<StringCollection> datalist)
        {
            foreach (StringCollection data in datalist)
            {
                Makeup(sheet, data);
            }
        }

        private static void Makeup(Dictionary<ISheet, StringCollection> sheetdata)
        {
            foreach (ISheet sheet in sheetdata.Keys)
            {
                Makeup(sheet, sheetdata[sheet]);
            }
        }

        private static void Makeup(Dictionary<ISheet, List<StringCollection>> sheetdata)
        {
            foreach (ISheet sheet in sheetdata.Keys)
            {
                Makeup(sheet, sheetdata[sheet]);
            }
        }

        private static void Makeup(IWorkbook wb, StringCollection data, int sheetIndex, bool append)
        {
            ISheet sheet = GetSheet(wb, sheetIndex, append);
            Makeup(sheet, data);
        }

        private static void Makeup(IWorkbook wb, List<StringCollection> datalist, int sheetIndex, bool append)
        {
            ISheet sheet = GetSheet(wb, sheetIndex, append);
            Makeup(sheet, datalist);
        }

        private static void Makeup(IWorkbook wb, Dictionary<int, List<StringCollection>> sheetdata, bool append)
        {  
            foreach (int sheetIndex in sheetdata.Keys)
            {
                ISheet sheet = GetSheet(wb, sheetIndex, append);
                List<StringCollection> datalist = sheetdata[sheetIndex];
                Makeup(sheet, datalist);
            }
        }

        private static void Makeup(IWorkbook wb, StringCollection data, string sheetName, bool append)
        {
            ISheet sheet = GetSheet(wb, sheetName, append);
            Makeup(sheet, data);
        }

        private static void Makeup(IWorkbook wb, List<StringCollection> datalist, string sheetName, bool append)
        {
            ISheet sheet = GetSheet(wb, sheetName, append);
            Makeup(sheet, datalist);
        }

        private static void Makeup(IWorkbook wb, Dictionary<string, List<StringCollection>> sheetdata, bool append)
        {
            foreach (string sheetName in sheetdata.Keys)
            {
                ISheet sheet = GetSheet(wb, sheetName, append);
                List<StringCollection> datalist = sheetdata[sheetName];
                Makeup(sheet, datalist);
            }
        }
        #endregion
        
        /// <summary>
        /// 根据 index 向指定 sheet 写入一行
        /// </summary>
        public static bool Write(string path, StringCollection data, int sheetIndex, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, data, sheetIndex, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
            	Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 根据 index 向指定 sheet 写入多行
        /// </summary>
        public static bool Write(string path, List<StringCollection> datalist, int sheetIndex, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, datalist, sheetIndex, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 根据 index 向多个 sheet 写入数据
        /// </summary>
        public static bool Write(string path, Dictionary<int, List<StringCollection>> sheetdata, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, sheetdata, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 根据 name 向指定 sheet 写入一行
        /// </summary>
        public static bool Write(string path, StringCollection data, string sheetName, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, data, sheetName, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 根据 name 向指定 sheet 写入多行
        /// </summary>
        public static bool Write(string path, List<StringCollection> datalist, string sheetName, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, datalist, sheetName, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 根据 name 向多个 sheet 写入数据
        /// </summary>
        public static bool Write(string path, Dictionary<string, List<StringCollection>> sheetdata, bool append, bool hasTemplate = false, string templatePath = "")
        {
            FileStream fs = null;

            try
            {
                IWorkbook wb = File.Exists(path) ? OpenWorkbook(path) : hasTemplate ? CreateWorkbookByTemplate(templatePath, path) : CreateWorkbook(path);
                Makeup(wb, sheetdata, append);
                WriteToFile(path, wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseStream(fs);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写一行（独占方式）
        /// </summary>
        public bool WriteExcel(string path, StringCollection data, int sheetIndex, bool append)
        { 
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, data, sheetIndex, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
            	Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel(); // 必须关闭流，才能再次写入
                ReOpenExcel(path, append);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写多行（独占方式）
        /// </summary>
        public bool WriteExcel(string path, List<StringCollection> datalist, int sheetIndex, bool append)
        {
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, datalist, sheetIndex, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel();
                ReOpenExcel(path, append);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写多个 sheet （独占方式）
        /// </summary>
        public bool WriteExcel(string path, Dictionary<int, List<StringCollection>> sheetdata, bool append)
        {
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, sheetdata, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel();
                ReOpenExcel(path, append);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写一行（独占方式）
        /// </summary>
        public bool WriteExcel(string path, StringCollection data, string sheetName, bool append)
        {
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, data, sheetName, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel();
                ReOpenExcel(path, append);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写多行（独占方式）
        /// </summary>
        public bool WriteExcel(string path, List<StringCollection> datalist, string sheetName, bool append)
        {
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, datalist, sheetName, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel();
                ReOpenExcel(path, append);
            }

            return false;
        }

        /// <summary>
        /// 带切换文件的，写多个 sheet （独占方式）
        /// </summary>
        public bool WriteExcel(string path, Dictionary<string, List<StringCollection>> sheetdata, bool append)
        {
            try
            {
                this.ReOpenExcel(path, append);
                Makeup(_wb, sheetdata, _append);
                WriteToFile(_fs, _wb);
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseExcel();
                ReOpenExcel(path, append);
            }

            return false;
        }


        private static void InsertRow(ISheet sheet, int startRowIndex, List<StringCollection> datalist)
        {
            int insertRowCount = datalist.Count;

            IRow formatRow = null;
            if (startRowIndex <= sheet.LastRowNum)
            {
                sheet.ShiftRows(startRowIndex, sheet.LastRowNum, insertRowCount, true, false); // 中间插入
            }
            else
            {
                startRowIndex = sheet.LastRowNum + 1; // 追加
            }

            // 获取原格式行
            formatRow = sheet.GetRow(startRowIndex - 1);

            for (int i = startRowIndex; i < startRowIndex + insertRowCount; i++)
            {
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;
                targetRow = sheet.CreateRow(i);
                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    sourceCell = formatRow.GetCell(m);
                    if (sourceCell == null)
                    {
                        continue;
                    }

                    targetCell = targetRow.CreateCell(m);
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }

            for (int i = startRowIndex; i < startRowIndex + insertRowCount; i++)
            {
                IRow firstTargetRow = sheet.GetRow(i);
                ICell firstSourceCell = null;
                ICell firstTargetCell = null;
                StringCollection data = datalist[i - startRowIndex];

                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    firstSourceCell = formatRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (firstSourceCell == null)
                    {
                        continue;
                    }

                    firstTargetCell = firstTargetRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                    firstTargetCell.SetCellType(firstSourceCell.CellType);
                    if (data != null && data.Count > 0)
                    {
                        firstTargetCell.SetCellValue(data[m]);
                    }
                    else
                    {
                        firstTargetCell.SetCellValue("test" + (i + 1));
                    }
                }
            }
        }

        public bool Insert(string path, StringCollection data, bool append)
        {
            try
            {
                this._path = path;
                this._append = append;
                TemplateInit();
                return _oe.AppendExcel(this._path, data);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //CloseExcel();
            }

            return false;
        }

        public void TemplateInit()
        {
            if (File.Exists(this._path))
            {
                return;
            }

            _wb = CreateWorkbookByTemplate(this._templatepath, this._path);
        }

        /// <summary>
        /// 切换 Excel
        /// </summary>
        public void ReOpenExcel(string path, bool append)
        {
            if (_fs == null || path != this._path || append != this._append)
            {
                lock (locker)
                {
                    if (_fs == null || path != this._path || append != this._append)
                    {
                        this.CloseExcel();
                        this.OpenExcel(path, append);
                    }
                }
            }
        }

        /// <summary>
        /// 打开 Excel
        /// </summary>
        public void OpenExcel(string path, bool append)
        {
            if (File.Exists(path))
            {
                _wb = OpenWorkbook(path);
            }
            else
            {
                _wb = CreateWorkbook(path);
            }

            _fs = OpenWriterStream(path);  
            
            if (_fs != null)
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
        /// 关闭 Excel
        /// </summary>
        public void CloseExcel()
        {
            CloseStream(_fs);
            _fs = null;
        }

        private static ISheet GetSheet(IWorkbook wb, int sheetIndex, bool append)
        {
            while (sheetIndex >= wb.NumberOfSheets)
            {
                wb.CreateSheet();
            }

            ISheet sheet = wb.GetSheetAt(sheetIndex);
            if (!append && sheet != null)
            {
                for (int r = sheet.LastRowNum; r >= 0; r--)
                {
                    sheet.RemoveRow(sheet.GetRow(r)); // 重写模式，删除 sheet 所有行
                }
            }

            return sheet;
        }

        private static ISheet GetSheet(IWorkbook wb, string sheetName, bool append)
        {
            if (!append && wb.GetSheet(sheetName) != null)
            {
                wb.RemoveSheetAt(wb.GetSheetIndex(sheetName)); // 重写模式，删除 sheet
            }

            ISheet sheet = wb.GetSheet(sheetName);
            if (sheet == null)
            {
                sheet = wb.CreateSheet(sheetName);
            }

            return sheet;
        }

        private static IRow CreateRow(ISheet sheet)
        {
            int id = sheet.LastRowNum + 1;
            IRow row = sheet.CreateRow(id);
            ICell cell = row.CreateCell(0); // 第 1 列，添加行索引
            cell.SetCellValue(id + 1);
            return row;
        }
    }
}
