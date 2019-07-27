using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UITest
{
    public partial class Form2 : Form
    {
        private const int OPNUM = 18;
        private List<WorkStation> wslist = new List<WorkStation>();
        public Form2()
        {
            InitializeComponent();
            // 取得当前的屏幕除任务栏外的工作域大小
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //// 取得当前的屏幕包括任务栏的工作域大小
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            //this.IsMdiContainer = true;//设置父窗体是容器
            CreateWorkstation();
        }

        public void CreateWorkstation()
        {
            for (int i = 0; i < 18; i++)
            {
                WorkStation ws = new WorkStation(i + 1);
                ws.TopLevel = false;
                ws.Parent = this;
                //ws.MdiParent = this;
                this.tpnl.Controls.Add(ws);
                ws.Show();

                int r = i / 6;
                int c = i % 6;

                ws.Left = c * ws.Width;
                ws.Top = r * ws.Height;
                wslist.Add(ws);
            }
        }
    }
}
