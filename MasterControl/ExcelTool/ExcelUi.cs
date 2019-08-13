using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ExcelTool
{
    public partial class ExcelUi : Form
    {
        private ExcelHelper eh = new ExcelHelper(@"d:\template.xls");

        public ExcelUi()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            List<DataTable> dtList = ExcelHelper.Read(@"d:\2.xls");
            //List<DataTable> dtList = ExcelHelper.Read(@"d:\template.xls");

            if (dtList.Count <= 0) return;

            dataSet1.Tables.Clear();
            tabControl1.TabPages.Clear();

            foreach (var dt in dtList)
            {
                dataSet1.Tables.Add(dt);
            }

            //dataGridView1.DataSource = dataSet1.Tables[0];
            //dataGridView2.DataSource = dataSet1.Tables[1];

            for (int i = 0; i < dataSet1.Tables.Count; i++)
            {
                DataGridView dataGridView3 = new DataGridView();
                dataGridView3.Dock = DockStyle.Fill;
                dataGridView3.DataSource = dataSet1.Tables[i];
                tabControl1.TabPages.Add("tabPage" + (i + 1));
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(dataGridView3);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            StringCollection data = new StringCollection();
            data.Add("111");
            data.Add("222");
            data.Add("333");

            StringCollection data2 = new StringCollection();
            data2.Add("444");
            data2.Add("555");
            data2.Add("666");

            List<StringCollection> datalist = new List<StringCollection>();
            datalist.Add(data);
            datalist.Add(data2);

            Dictionary<int, List<StringCollection>> sheetdata = new Dictionary<int, List<StringCollection>>();
            sheetdata.Add(0, datalist);
            sheetdata.Add(1, datalist);
            sheetdata.Add(2, datalist);

            Dictionary<string, List<StringCollection>> sheetdata2 = new Dictionary<string, List<StringCollection>>();
            sheetdata2.Add("S1", datalist);
            sheetdata2.Add("S2", datalist);
            sheetdata2.Add("S3", datalist);

            ExcelHelper.Write(@"d:\6.xlsx", data, 0, true, true, @"d:\template.xlsx");
            //ExcelHelper.Write(@"d:\7.xls", data, 0, true, true, @"d:\template.xls");
            //ExcelHelper.Write(@"d:\1.xls", data, "data", true, true, @"d:\template.xls");
            //ExcelHelper.Write(@"d:\2.xls", datalist, "data", true, true, @"d:\template.xls");

            //this.btnImport_Click(sender, e);
            //eh.WriteExcel(@"d:\2.xls", data, 0, true);

            //eh.Insert(@"d:\111.xls", data, true);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = @"d:\template.xlsx";
                process.StartInfo.ErrorDialog = true;
                process.Start();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
