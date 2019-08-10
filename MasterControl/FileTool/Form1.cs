using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;

namespace FileTool
{
    public partial class Form1 : Form
    {
        public FileHelper fh = new FileHelper(@"d:\1.csv", true);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ooo");
            sb.Append(", ");
            sb.Append("222");
            fh.WriteCSV(@"d:\2.csv", "id, p1, p2", sb, true);

            sb.Clear();
            sb.Append("111, 111");
            //sb.Append("\r\n");
            sb.Append(Environment.NewLine);
            sb.Append("222, 222");
            fh.WriteCSV(@"d:\3.csv", "id, p1, p2", sb, true);

            StringCollection sc = new StringCollection();
            sc.Add("111, 111");
            sc.Add("222, 222");
            fh.WriteCSV(@"d:\7.csv", "id, p1, p2", sc, true);
        }
    }
}
