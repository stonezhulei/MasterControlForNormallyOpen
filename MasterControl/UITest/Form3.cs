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
    public partial class Form3 : Form
    {
        private List<WorkStation> wslist = new List<WorkStation>();
        private DataCollect dc = new DataCollect(Application.StartupPath);

        public Form3()
        {
            InitializeComponent();
            // 取得当前的屏幕除任务栏外的工作域大小
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //// 取得当前的屏幕包括任务栏的工作域大小
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

        public string ParseFloat(string data, int n, string u)
        {
            float fdata;
            string result = "";
            if (float.TryParse(data, out fdata))
            {
                result = fdata / n + " " + u;
            }

            return result;
        }

        public void RefreshResult(StringCollection data)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<StringCollection>(RefreshResult), data);
                return;
            }

            string[] td1 = data[0].Split(',');
            string[] td2 = data[1].Split(',');
            string[] td3 = data[2].Split(',');

            if (td1.Length < 16 || td2.Length < 17 || td3.Length < 10)
            {
                return;
            }

            try
            {
                this.dgvResults.Rows.Clear();

                for (int i = 0; i < data.Count; i++)
                {
                    this.dgvResults.Rows.Add();
                }

                // OP10
                this.dgvResults.Rows[0].Cells[0].Value = td1[2];
                this.dgvResults.Rows[1].Cells[0].Value = td1[3];
                this.dgvResults.Rows[2].Cells[0].Value = td1[4];

                // OP20
                this.dgvResults.Rows[0].Cells[1].Value = ParseFloat(td1[5], 1000, "mm");

                // OP30      
                this.dgvResults.Rows[0].Cells[2].Value = ParseFloat(td1[7], 1000, "KN");
                this.dgvResults.Rows[1].Cells[2].Value = ParseFloat(td1[8], 1000, "mm");
                this.dgvResults.Rows[2].Cells[2].Value = td1[6]; // == 调换

                // OP50
                this.dgvResults.Rows[0].Cells[3].Value = ParseFloat(td1[9], 1000, "mm");

                // OP60
                this.dgvResults.Rows[0].Cells[4].Value = ParseFloat(td1[10], 1000, "mm");
                this.dgvResults.Rows[1].Cells[4].Value = ParseFloat(td1[11], 1000, "mm");

                // OP70
                this.dgvResults.Rows[0].Cells[5].Value = ParseFloat(td1[12], 1000, "mm");
                this.dgvResults.Rows[1].Cells[5].Value = ParseFloat(td1[14], 1000, "KN");
                this.dgvResults.Rows[2].Cells[5].Value = ParseFloat(td1[15], 1000, "mm");
                this.dgvResults.Rows[3].Cells[5].Value = td1[13];// == 调换

                // OP80
                this.dgvResults.Rows[0].Cells[6].Value = td2[16];

                // OP90
                this.dgvResults.Rows[0].Cells[7].Value = ParseFloat(td2[5], 1000, "mm");

                // OP100
                this.dgvResults.Rows[0].Cells[8].Value = td2[2];
                this.dgvResults.Rows[1].Cells[8].Value = td2[3].Substring(1);
                this.dgvResults.Rows[2].Cells[8].Value = td2[4];

                // OP110-1
                this.dgvResults.Rows[0].Cells[9].Value = td2[6];
                this.dgvResults.Rows[1].Cells[9].Value = td2[7].Substring(1);
                this.dgvResults.Rows[2].Cells[9].Value = td2[8];

                // OP110-2
                this.dgvResults.Rows[0].Cells[10].Value = td2[9];
                this.dgvResults.Rows[1].Cells[10].Value = td2[10].Substring(1);
                this.dgvResults.Rows[2].Cells[10].Value = td2[11];

                // OP110-3
                this.dgvResults.Rows[0].Cells[11].Value = ParseFloat(td2[12], 1000, "KN");
                this.dgvResults.Rows[1].Cells[11].Value = ParseFloat(td2[13], 100, "mm");
                this.dgvResults.Rows[2].Cells[11].Value = td2[14];

                // OP120
                this.dgvResults.Rows[0].Cells[12].Value = ParseFloat(td3[2], 1000, "bar");
                this.dgvResults.Rows[1].Cells[12].Value = ParseFloat(td3[3], 1000, "ml/min");
                this.dgvResults.Rows[2].Cells[12].Value = ParseFloat(td3[4], 1000, "Mpa");
                this.dgvResults.Rows[3].Cells[12].Value = td3[5];

                // OP130
                this.dgvResults.Rows[0].Cells[13].Value = ParseFloat(td3[6], 10, "pa");
                this.dgvResults.Rows[1].Cells[13].Value = ParseFloat(td3[7], 100, "bar");
                this.dgvResults.Rows[2].Cells[13].Value = ParseFloat(td3[8], 100, "bar");
                this.dgvResults.Rows[3].Cells[13].Value = td3[9];
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
                ws.Top = 20 + r * ws.Height;

                if (i == 18)
                {
                    ws.Left = wslist[0].Width * 6 + 50;
                    ws.Top = 100;
                    ws.Scale((float)1.6);
                    //SizeF x = new SizeF((float)(ws.Width * 1), (float)(ws.Height * 1));
                    //ws.Scale(x);
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
