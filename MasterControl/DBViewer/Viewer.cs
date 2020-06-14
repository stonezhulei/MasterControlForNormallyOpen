using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DB.MySql;

namespace DBViewer
{
    public partial class Viewer : Form
    {
        private PageItem pageItem = new PageItem();
        private BindingSource bs = new BindingSource();

        public Viewer()
        {
            InitializeComponent();
            //this.Refresh(true);

            //string sql = string.Format("select * from data where id > {0} order by id asc limit {1} offset {2}",
            //    (this.pageItem.PageCount - 1) * this.pageItem.PageSize,
            //    this.pageItem.PageSize,
            //    0);
            //DataSet ds = SQLHelper.GetDataSet(sql);
            //DataTable dt = ds.Tables[0];
            //bs.DataSource = dt;
            //this.bindingNavigator.BindingSource = bs;
            //this.dgvResult.DataSource = bs;
        }

        private void Refresh(bool readCount = false)
        {
            if (readCount) {
                this.ReadPageSetting();
            }
            this.RefreshNavigator();
            this.RefreshStatus();
        }

        private void ReadPageSetting()
        {
            int recordCount = Convert.ToInt32(SQLHelper.ExecuteScalar("select count(*) from data"));
            int pageSize = this.cbxLimitPageSize.Checked ? Convert.ToInt32(this.tbxPageSize.Text) : recordCount;

            this.pageItem.CurrentSelectRowIndex = 0;
            this.pageItem.CurrentPage = 1;
            this.pageItem.RecordCount = recordCount;
            this.pageItem.PageSize = pageSize;
        }

        private void RefreshNavigator()
        {
            if (this.pageItem.RecordCount == 0)
            {
                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
            }
            else
            {
                if (this.pageItem.CurrentPage < this.pageItem.PageCount)
                {
                    this.bindingNavigatorMoveNextItem.Enabled = true;
                    this.bindingNavigatorMoveLastItem.Enabled = true;
                }
                else
                {
                    this.bindingNavigatorMoveNextItem.Enabled = false;
                    this.bindingNavigatorMoveLastItem.Enabled = false;
                }
                if (this.pageItem.CurrentPage > 0)
                {
                    this.bindingNavigatorMoveFirstItem.Enabled = true;
                    this.bindingNavigatorMovePreviousItem.Enabled = true;
                }
                else
                {
                    this.bindingNavigatorMoveFirstItem.Enabled = false;
                    this.bindingNavigatorMovePreviousItem.Enabled = false;
                }
            }

            this.bindingNavigatorPositionItem.Text = this.pageItem.CurrentShowCount.ToString();
        }

        private void RefreshStatus()
        {
            if (this.pageItem.RecordCount > 0)
            {
                this.toolStripStatusLabel4.Text = string.Format("第 {0} 条记录 (共 {1} 条) 于第 {2} 页",
                    this.pageItem.CurrentShowCount,
                    this.pageItem.RecordCount,
                    this.pageItem.CurrentPage);
            }
        }

        private void LoadData()
        {
            string sql = string.Format("select * from data where id > {0} order by id desc limit {1} offset {2}",
               (this.pageItem.PageCount - this.pageItem.CurrentPage) * this.pageItem.PageSize,
               this.pageItem.PageSize,
               0);
            DataSet ds = SQLHelper.GetDataSet(sql);
            DataTable dt = ds.Tables[0];
            bs.DataSource = dt;
            this.bindingNavigator.BindingSource = bs;
            this.dgvResult.DataSource = bs;
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            this.LoadData();
            if (this.dgvResult.RowCount > 0)
            {
                this.dgvResult.CurrentCell = this.dgvResult[0, this.pageItem.CurrentSelectRowIndex];
                this.dgvResult.FirstDisplayedScrollingRowIndex = this.pageItem.FirstDisplayedScrollingRowIndex;
            }
            this.Refresh();
            //this.bs.MoveFirst();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            this.pageItem.CurrentPage = Math.Max(1, this.pageItem.CurrentPage - 1);
            this.LoadData();
            if (this.dgvResult.RowCount > 0)
            {
                this.dgvResult.CurrentCell = this.dgvResult[0, this.pageItem.CurrentSelectRowIndex];
                this.dgvResult.FirstDisplayedScrollingRowIndex = this.pageItem.FirstDisplayedScrollingRowIndex;
            }
            this.Refresh();
            //this.bs.MovePrevious();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            this.pageItem.CurrentPage = Math.Min(this.pageItem.PageCount, this.pageItem.CurrentPage + 1);
            this.LoadData();
            if (this.dgvResult.RowCount > 0)
            {
                this.dgvResult.CurrentCell = this.dgvResult[0, this.pageItem.CurrentSelectRowIndex];
                this.dgvResult.FirstDisplayedScrollingRowIndex = this.pageItem.FirstDisplayedScrollingRowIndex;
            }
            //this.bs.MoveNext();
            this.Refresh();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            this.pageItem.CurrentPage = this.pageItem.PageCount;
            this.LoadData();
            if (this.dgvResult.RowCount > 0)
            {
                this.dgvResult.CurrentCell = this.dgvResult[0, this.pageItem.CurrentSelectRowIndex];
                this.dgvResult.FirstDisplayedScrollingRowIndex = this.pageItem.FirstDisplayedScrollingRowIndex;
            }

            this.Refresh();
            //this.bs.MoveLast();
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

        private void tbxPageSize_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbxPageSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                return;
            }
        }

        private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dgv.RowHeadersWidth - 4,
                                                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                    dgv.RowHeadersDefaultCellStyle.Font,
                                    rectangle,
                                    dgv.RowHeadersDefaultCellStyle.ForeColor,
                                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvResult_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvResult.CurrentCell != null)
            {
                this.pageItem.CurrentSelectRowIndex = this.dgvResult.CurrentCell.RowIndex;
                this.pageItem.FirstDisplayedScrollingRowIndex = this.dgvResult.FirstDisplayedScrollingRowIndex;
            }
 
            this.RefreshNavigator();
            this.RefreshStatus();
        }
    }
}
