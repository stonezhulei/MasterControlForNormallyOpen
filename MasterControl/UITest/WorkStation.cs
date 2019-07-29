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
    public partial class WorkStation : Form
    {
        public WorkStation(int id)
        {
            InitializeComponent();
            this.lblTitle.Text = id.ToString();
            RefreshYeild(0, 0);

            if (id == 19)
            {
                this.Width = 250;
                this.Height = 280;
                this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.lblCapacity.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.lblYield.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.tbxCapacity.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                this.tbxYield.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
        }

        public void RefreshStatus(int status)
        {
            if (status == 8)
            {
                lblStatus.Text = "准备";
            }
            else if (status == 9)
            {
                lblStatus.Text = "运行";
            }
            else if (status == 1)
            {
                lblStatus.Text = "停止";
            }

            RefreshLight(status);
        }

        public void RefreshYeild(long count, long ok)
        {
            ok = (ok > 0) ? ok : 0;
            count = (count > 0) ? count : 0;
            double rate = (count != 0) ? ok * 100.0 / count : 0;
            
            tbxCapacity.Text = count.ToString();
            tbxYield.Text = rate.ToString("0.##");
            tbxYield.Text += (tbxYield.Text != "0") ? "%" : "";
        }

        public void RefreshLight(int status)
        { 
            btnYellow.BackColor = SystemColors.AppWorkspace;
            btnGeen.BackColor = SystemColors.AppWorkspace;
            btnRed.BackColor = SystemColors.AppWorkspace;

            if (status == 8)
            {
                btnYellow.BackColor = Color.Orange;
            }
            else if (status == 9)
            {
                btnGeen.BackColor = Color.LimeGreen;
            }
            else if (status == 1)
            {
                btnRed.BackColor = Color.Red;
            }
        }

        public void RefreshTitle(string title)
        {
            this.lblTitle.Text = title;
        }
    }
}
