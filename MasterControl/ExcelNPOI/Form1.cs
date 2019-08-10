using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelNPOI
{
    public partial class Form1 : Form
    {
        OperationExcel oe;
        ExcelHelper excelHelper;

        public Form1()
        {
            InitializeComponent();

            excelHelper = new ExcelHelper(Application.StartupPath + "\\template.xls", Application.StartupPath + "\\1.xls");
            excelHelper.Write();

            //oe = new OperationExcel(12, 3); //第一行开始插入5行，第三个参数是对应要添加到新添加行的每一列的数据
            //oe.EditorExcel(Application.StartupPath + "\\template.xlsx", Application.StartupPath + "\\template.xlsx", oe);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            excelHelper.Write();
        }
    }
}
