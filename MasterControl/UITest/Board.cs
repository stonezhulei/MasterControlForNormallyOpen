using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MasterControl;

namespace UITest
{
    public partial class Board : Form
    {
        private List<WorkStation> wslist = new List<WorkStation>();
        private DataCollect dc = new DataCollect(Application.StartupPath);

        public Board()
        {
            InitializeComponent();
            // 取得当前的屏幕除任务栏外的工作域大小
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //// 取得当前的屏幕包括任务栏的工作域大小
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

        public void RefreshResult(StringCollection data)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<StringCollection>(RefreshResult), data);
                return;
            }

            if (data.Count < Para.UISHOWNUM) return;

            try
            {
            }
            catch
            {
            }      
        }

        public void RefreshTitle(int id, string title)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int, string>(RefreshTitle), id, title);
                return;
            }

            wslist[id].RefreshTitle(title);
        }

        public void RefreshStatus(int id, int status)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int, int>(RefreshStatus), id, status);
                return;  
            }

            wslist[id].RefreshStatus(status);
        }

        public void RefreshYeild(int id, long count, long ok)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int, long, long>(RefreshYeild), id, count, ok);
                return;
            }

            wslist[id].RefreshYeild(count, ok);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //this.DoubleBuffered = true; // 设置本窗体
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            //SetStyle(ControlStyles.DoubleBuffer, true); //

            CreateWorkstation();
            LoadForm(sender, e);
            AutoScale(this);

            dc.onShownResult += RefreshResult;
            dc.onShownStatus += RefreshStatus;
            dc.onShownYeild += RefreshYeild;
            dc.onShownTitle += RefreshTitle;
            dc.Start();
        }

        private void CreateWorkstation()
        {
            for (int i = 0; i < Para.OPNUM; i++)
            {
                WorkStation ws = new WorkStation(i + 1);
                ws.TopLevel = false;
                ws.Parent = this;
                this.pnlBody.Controls.Add(ws);
                ws.Show();

                int r = i / 6;
                int c = i % 6;

                ws.Left = c * ws.Width;
                ws.Top = 20 + r * (ws.Height);

                if (i == 18)
                {
                    ws.Left = wslist[0].Width * 6 + 50;
                    ws.Top =  wslist[17].Top - 50;
                    ws.Scale((float)1.4);
                    //SizeF x = new SizeF((float)(ws.Width * 1), (float)(ws.Height * 1));
                    //ws.Scale(x);
                    this.tbxBoardTitle.Left = wslist[5].Right + (this.Width - wslist[5].Right) / 2 - this.tbxBoardTitle.Width / 2;
                }

                wslist.Add(ws);
            }
        }

        /// <summary>
        /// 让整个容器里面的控件一起显示出来
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void LoadForm(object sender, EventArgs e)
        {
            //这里是不同的分辨率设置控件的显示
            this.Tag = 1920 + "," + 1080;
            string[] tmp = ((Form)sender).Tag.ToString().Split(',');
            float width = (float)((Form)sender).Width / (float)Convert.ToInt16(tmp[0]);
            float heigth = (float)((Form)sender).Height / (float)Convert.ToInt16(tmp[1]);

            ((Form)sender).Tag = ((Form)sender).Width.ToString() + "," + ((Form)sender).Height;

            foreach (Control control in ((Form)sender).Controls)
            {
                control.Scale(new SizeF(width, heigth));
            }
        }

        //这里是让所有的控件随窗体的变化而变化
        private new void AutoScale(Form frm)
        {
            frm.Tag = frm.Width.ToString() + "," + frm.Height.ToString();
            frm.SizeChanged += new EventHandler(Form3_SizeChanged);
        }

        private void Form3_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                string[] tmp = ((Form)sender).Tag.ToString().Split(',');
                if ((int)((Form)sender).Width > 200)
                {
                    float width = (float)((Form)sender).Width / (float)Convert.ToInt16(tmp[0]);
                    float heigth = (float)((Form)sender).Height / (float)Convert.ToInt16(tmp[1]);

                    ((Form)sender).Tag = ((Form)sender).Width.ToString() + "," + ((Form)sender).Height;

                    foreach (Control control in ((Form)sender).Controls)
                    {
                        control.Scale(new SizeF(width, heigth));
                    }
                }
            }
            catch
            {
                // log your exception here
            }
        }
    }
}
