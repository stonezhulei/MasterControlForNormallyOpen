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
            RefreshResult(0, 0);
        }

        public void RefreshStatus(int status)
        {
            if (status == 8)
            {
                lblStatus.Text = "准备OK";
            }
            else if (status == 9)
            {
                lblStatus.Text = "运行中";
            }
            else if (status == 1)
            {
                lblStatus.Text = "故障";
            }

            RefreshLight(status);
        }

        public void RefreshResult(long count, long ok)
        {
            ok = (ok > 0) ? ok : 0;
            count = (count > 0) ? count : 0;
            double rate = (count != 0) ? ok * 100.0 / count : 0;
            
            tbxCapacity.Text = count.ToString();
            tbxYield.Text = rate.ToString("0.##") + ((count != 0) ? "%" : "");
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
