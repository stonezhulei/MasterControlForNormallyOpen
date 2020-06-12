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
        private BindingSource bs = new BindingSource();

        public Viewer()
        {
            InitializeComponent();

            this.bindingNavigatorMoveFirstItem.Enabled = true;
            this.bindingNavigatorMovePreviousItem.Enabled = true;
            this.bindingNavigatorMoveNextItem.Enabled = true;
            this.bindingNavigatorMoveLastItem.Enabled = true;

            object o = SQLHelper.ExecuteScalar("select count(*) from data");
            int count1 = Convert.ToInt32(o);

            // 测试 200 万条数据
            DataSet ds = SQLHelper.GetDataSet("select * from data");
            DataTable dt = ds.Tables[0];
            bs.DataSource = dt;
            this.bindingNavigator.BindingSource = bs;
            this.dgvResult.DataSource = bs;
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            //this.bs.MoveFirst();
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
    }
}
