using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBViewer;

namespace UITest
{
    public partial class Form5 : Form
    {
        private Board  pageBoard;
        private Viewer pageDBView;

        public Form5()
        {
            InitializeComponent();
            this.SetShowSize();
            this.InitUi(); 
        }

        private void SetShowSize()
        {
            // 取得当前的屏幕除任务栏外的工作域大小
            this.Width = SystemInformation.WorkingArea.Width;
            this.Height = SystemInformation.WorkingArea.Height;

            //// 取得当前的屏幕包括任务栏的工作域大小
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

        private void InitUi()
        {
            // 看板页
            this.pageBoard = new Board();
            this.pageBoard.TopLevel = false;
            this.tabPageBoard.Controls.Add(this.pageBoard);
            this.pageBoard.Dock = DockStyle.Fill;
            this.pageBoard.Show();

            // 数据页
            this.pageDBView = new Viewer();
            this.pageDBView.TopLevel = false;
            this.tabPageDBView.Controls.Add(this.pageDBView);
            this.pageDBView.Dock = DockStyle.Fill;
            this.pageDBView.Show();
        }
    }
}
