using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.OpenXml4Net.OPC;

namespace ExcelTool
{
    public class OperationExcel
    {
        private int insertRowIndex;
        private int insertRowCount;
        private StringCollection insertData;
        private Dictionary<int, StringCollection> insertOneRowDataBySheetIndex; // sheet一行数据
        private Dictionary<string, StringCollection> insertOneRowDataBySheetName; // sheet一行数据
        private int startRowCount = 0; // 起始行数
        private bool firstWrite = false;

        public OperationExcel()
        {
            insertRowCount = 1;
        }

        public OperationExcel(int insertRowIndex, int insertRowCount, StringCollection insertData = null)
        {
            if (insertData != null)
            {
                this.insertData = insertData;
            }

            this.insertRowIndex = insertRowIndex;
            this.insertRowCount = insertRowCount;
        }

        private IWorkbook NPOIOpenExcel(string filename)
        {
            Stream excelStream = null;

            try
            {
                excelStream = OpenResource(filename);

                if (POIFSFileSystem.HasPOIFSHeader(excelStream))
                {
                    return new HSSFWorkbook(excelStream);
                }

                if (POIXMLDocument.HasOOXMLHeader(excelStream))
                {
                    return new XSSFWorkbook(OPCPackage.Open(excelStream));
                }

                if (filename.EndsWith(".xlsx"))
                {
                    return new XSSFWorkbook(excelStream);
                }

                if (filename.EndsWith(".xls"))
                {
                    new HSSFWorkbook(excelStream);
                }

                throw new Exception("Your InputStream was neither an OLE2 stream, nor an OOXML stream");
            }
            finally
            {
            	excelStream.Close();
            }
        }

        private Stream OpenResource(string filename)
        {
            //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return fs;
        }

        private void InsertRow(ISheet sheet, int insertRowIndex, int insertRowCount)
        {
            IRow formatRow = null;
            if (insertRowIndex <= sheet.LastRowNum)
            {
                sheet.ShiftRows(insertRowIndex, sheet.LastRowNum, insertRowCount, true, false); // 中间插入
            }
            else
            {
                insertRowIndex = sheet.LastRowNum + 1; // 追加
            }

            // 获取原格式行
            formatRow = sheet.GetRow(insertRowIndex - 1);

            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
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

            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
            {
                IRow firstTargetRow = sheet.GetRow(i);
                ICell firstSourceCell = null;
                ICell firstTargetCell = null;

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
                    if (this.insertData != null && this.insertData.Count > m)
                    {
                        firstTargetCell.SetCellValue(insertData[m]);
                    }
                    else
                    {
                        firstTargetCell.SetCellValue("test" + (insertRowIndex + 1));
                    }
                }
            }
        }

        public void WriteToFile(IWorkbook workbook, string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                workbook.Write(fs);
                fs.Close();
            }
        }

        public void OpenExcel(string filename)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.ErrorDialog = true;
            process.Start();
        }

        public void EditorExcel(string savePath, string readPath, OperationExcel oe)
        {
            try
            {
                IWorkbook workbook = oe.NPOIOpenExcel(readPath);

                int sheetNum = workbook.NumberOfSheets;
                for (int i = 0; i < sheetNum; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    oe.InsertRow(sheet, insertRowIndex, insertRowCount);
                }

                oe.WriteToFile(workbook, savePath);
                oe.OpenExcel(savePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AppendExcel(string path, StringCollection insertData)
        {
            try
            {
                this.insertData = insertData;
                IWorkbook workbook = this.NPOIOpenExcel(path);
                ISheet sheet = workbook.GetSheet("data"); // 写模板 Sheet["data"]
                insertRowIndex = sheet.LastRowNum + 1;

                if (!firstWrite) {
                    firstWrite = true;
                    startRowCount = insertRowIndex; // 获取模板起始行数
                }

                int showRowIndex = insertRowIndex - startRowCount + 1;  // 显示的行号
                this.insertData.Insert(0, showRowIndex.ToString()); // 第 1 列插入行号
                this.InsertRow(sheet, insertRowIndex, insertRowCount);
                this.WriteToFile(workbook, path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
