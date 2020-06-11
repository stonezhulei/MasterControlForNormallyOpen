using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;
using MasterControl;
using DB.MySql;

namespace UITest
{
    public partial class Form4 : Form
    {
        private BindingSource bs = new BindingSource();
        private List<WorkStation> wslist = new List<WorkStation>();
        private DataCollect dc = new DataCollect(Application.StartupPath);

        public Form4()
        {
            InitializeComponent();
            // 取得当前的屏幕除任务栏外的工作域大小
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //// 取得当前的屏幕包括任务栏的工作域大小
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            this.bindingNavigatorMoveFirstItem.Enabled = true;
            this.bindingNavigatorMovePreviousItem.Enabled = true;
            this.bindingNavigatorMoveNextItem.Enabled = true;
            this.bindingNavigatorMoveLastItem.Enabled = true;

            // 测试 200 万条数据
            DataSet ds = SQLHelper.GetDataSet("select * from data");
            DataTable dt = ds.Tables[0];
            bs.DataSource = dt;
            this.bindingNavigator.BindingSource = bs;
            this.dgvResult.DataSource = bs;

            //MySqlDataReader sdr = SQLHelper.ExecuteReader(@"select table_name from information_schema.tables where table_schema='master_control'");
            //while (sdr.Read())
            //{
            //    cbxSelectTable.Items.Add(sdr[0].ToString());
            //}
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
                //this.dgvResults.Rows.Clear();

                //for (int r = 0; r < data.Count; r++)
                //{
                //    this.dgvResults.Rows.Add();
                //}

                //int i = 0;

                //// OP10
                //this.dgvResults.Rows[0].Cells[0].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[0].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[0].Value = data[i++];

                //// OP20
                //this.dgvResults.Rows[0].Cells[1].Value = data[i++];

                //// OP30
                //this.dgvResults.Rows[0].Cells[2].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[2].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[2].Value = data[i++];

                //// OP50
                //this.dgvResults.Rows[0].Cells[3].Value = data[i++];

                //// OP60
                //this.dgvResults.Rows[0].Cells[4].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[4].Value = data[i++];

                //// OP70
                //this.dgvResults.Rows[0].Cells[5].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[5].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[5].Value = data[i++];
                //this.dgvResults.Rows[3].Cells[5].Value = data[i++];

                //// OP80
                //this.dgvResults.Rows[0].Cells[6].Value = data[i++];

                //// OP90
                //this.dgvResults.Rows[0].Cells[7].Value = data[i++];

                //// OP100
                //this.dgvResults.Rows[0].Cells[8].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[8].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[8].Value = data[i++];

                //// OP110-1
                //this.dgvResults.Rows[0].Cells[9].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[9].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[9].Value = data[i++];

                //// OP110-2
                //this.dgvResults.Rows[0].Cells[10].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[10].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[10].Value = data[i++];

                //// OP110-3
                //this.dgvResults.Rows[0].Cells[11].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[11].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[11].Value = data[i++];

                //// OP120
                //this.dgvResults.Rows[0].Cells[12].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[12].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[12].Value = data[i++];
                //this.dgvResults.Rows[3].Cells[12].Value = data[i++];

                //// OP130
                //this.dgvResults.Rows[0].Cells[13].Value = data[i++];
                //this.dgvResults.Rows[1].Cells[13].Value = data[i++];
                //this.dgvResults.Rows[2].Cells[13].Value = data[i++];
                //this.dgvResults.Rows[3].Cells[13].Value = data[i++];
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

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            this.bs.MoveFirst();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            //this.bs.MovePrevious();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            //this.bs.MoveNext();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            this.bs.MoveLast();
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorSetting_Click(object sender, EventArgs e)
        {
            this.pnlPageCountSetting.Visible = !this.pnlPageCountSetting.Visible;
        }

        private void cbxLimitPerPageCount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbxPageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // TextBox 中只允许输入数字
            string pattern = @"[0-9]+";
            Regex reg = new Regex(pattern);
            if (reg.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '\b')
            {
                e.Handled = false; // 允许删除
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbxPageCount_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                return;
            }
        }

    }
}
