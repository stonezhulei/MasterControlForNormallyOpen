namespace DBViewer
{
    partial class Viewer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cbxResultSelect = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxEndTime = new System.Windows.Forms.TextBox();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxStartTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlPageCountSetting = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxLimitPageSize = new System.Windows.Forms.CheckBox();
            this.tbxPageSize = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorSetting = new System.Windows.Forms.ToolStripButton();
            this.pnlTop.SuspendLayout();
            this.pnlMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlPageCountSetting.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnQuery);
            this.pnlTop.Controls.Add(this.cbxResultSelect);
            this.pnlTop.Controls.Add(this.label4);
            this.pnlTop.Controls.Add(this.tbxEndTime);
            this.pnlTop.Controls.Add(this.dtpEndTime);
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.tbxStartTime);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.tbxBarcode);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.dtpStartTime);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1430, 28);
            this.pnlTop.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(976, 1);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(95, 23);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            // 
            // cbxResultSelect
            // 
            this.cbxResultSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxResultSelect.FormattingEnabled = true;
            this.cbxResultSelect.Items.AddRange(new object[] {
            "全部",
            "OK",
            "NG"});
            this.cbxResultSelect.Location = new System.Drawing.Point(760, 5);
            this.cbxResultSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cbxResultSelect.Name = "cbxResultSelect";
            this.cbxResultSelect.Size = new System.Drawing.Size(126, 20);
            this.cbxResultSelect.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(708, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "结果：";
            // 
            // tbxEndTime
            // 
            this.tbxEndTime.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbxEndTime.Location = new System.Drawing.Point(522, 5);
            this.tbxEndTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbxEndTime.Name = "tbxEndTime";
            this.tbxEndTime.Size = new System.Drawing.Size(128, 21);
            this.tbxEndTime.TabIndex = 7;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(647, 5);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(13, 21);
            this.dtpEndTime.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(494, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "至";
            // 
            // tbxStartTime
            // 
            this.tbxStartTime.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbxStartTime.Location = new System.Drawing.Point(332, 5);
            this.tbxStartTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbxStartTime.Name = "tbxStartTime";
            this.tbxStartTime.Size = new System.Drawing.Size(128, 21);
            this.tbxStartTime.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(277, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "日期：";
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbxBarcode.Location = new System.Drawing.Point(116, 5);
            this.tbxBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(150, 21);
            this.tbxBarcode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "产品二维码：";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(458, 5);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(2);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(13, 21);
            this.dtpStartTime.TabIndex = 0;
            // 
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.dgvResult);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMid.Location = new System.Drawing.Point(0, 28);
            this.pnlMid.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Size = new System.Drawing.Size(1430, 742);
            this.pnlMid.TabIndex = 3;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(2);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvResult.RowTemplate.Height = 27;
            this.dgvResult.RowTemplate.ReadOnly = true;
            this.dgvResult.Size = new System.Drawing.Size(1430, 742);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResult_RowPostPaint);
            this.dgvResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvResult_MouseClick);
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBottom.Controls.Add(this.pnlPageCountSetting);
            this.pnlBottom.Controls.Add(this.statusStrip);
            this.pnlBottom.Controls.Add(this.bindingNavigator);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlBottom.Location = new System.Drawing.Point(0, 769);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1430, 45);
            this.pnlBottom.TabIndex = 4;
            // 
            // pnlPageCountSetting
            // 
            this.pnlPageCountSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPageCountSetting.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPageCountSetting.Controls.Add(this.label6);
            this.pnlPageCountSetting.Controls.Add(this.cbxLimitPageSize);
            this.pnlPageCountSetting.Controls.Add(this.tbxPageSize);
            this.pnlPageCountSetting.Location = new System.Drawing.Point(1169, 2);
            this.pnlPageCountSetting.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPageCountSetting.Name = "pnlPageCountSetting";
            this.pnlPageCountSetting.Size = new System.Drawing.Size(212, 22);
            this.pnlPageCountSetting.TabIndex = 5;
            this.pnlPageCountSetting.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "条记录(每页)";
            // 
            // cbxLimitPageSize
            // 
            this.cbxLimitPageSize.AutoSize = true;
            this.cbxLimitPageSize.Checked = true;
            this.cbxLimitPageSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxLimitPageSize.Location = new System.Drawing.Point(7, 4);
            this.cbxLimitPageSize.Margin = new System.Windows.Forms.Padding(2);
            this.cbxLimitPageSize.Name = "cbxLimitPageSize";
            this.cbxLimitPageSize.Size = new System.Drawing.Size(72, 16);
            this.cbxLimitPageSize.TabIndex = 4;
            this.cbxLimitPageSize.Text = "限制记录";
            this.cbxLimitPageSize.UseVisualStyleBackColor = true;
            this.cbxLimitPageSize.CheckedChanged += new System.EventHandler(this.cbxLimitPerPageCount_CheckedChanged);
            // 
            // tbxPageSize
            // 
            this.tbxPageSize.Location = new System.Drawing.Point(80, 2);
            this.tbxPageSize.Margin = new System.Windows.Forms.Padding(2);
            this.tbxPageSize.Name = "tbxPageSize";
            this.tbxPageSize.Size = new System.Drawing.Size(55, 21);
            this.tbxPageSize.TabIndex = 4;
            this.tbxPageSize.Text = "50";
            this.tbxPageSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxPageSize_KeyPress);
            this.tbxPageSize.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxPageSize_KeyUp);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripSeparator1,
            this.toolStripStatusLabel2,
            this.toolStripSeparator2,
            this.toolStripStatusLabel3,
            this.toolStripSeparator3,
            this.toolStripStatusLabel4});
            this.statusStrip.Location = new System.Drawing.Point(0, 22);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(1430, 23);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(300, 20);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(900, 20);
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(200, 20);
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(400, 20);
            this.toolStripStatusLabel4.Text = "第 1 条记录 (共 2000000 条) 于第 9999 页";
            this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator3,
            this.bindingNavigatorSetting});
            this.bindingNavigator.Location = new System.Drawing.Point(1220, 5);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(202, 25);
            this.bindingNavigator.TabIndex = 4;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总页数";
            this.bindingNavigatorCountItem.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            this.bindingNavigatorMoveFirstItem.ToolTipText = "第一页";
            this.bindingNavigatorMoveFirstItem.Click += new System.EventHandler(this.bindingNavigatorMoveFirstItem_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            this.bindingNavigatorMovePreviousItem.ToolTipText = "前一页\r\n";
            this.bindingNavigatorMovePreviousItem.Click += new System.EventHandler(this.bindingNavigatorMovePreviousItem_Click);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(55, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            this.bindingNavigatorPositionItem.TextChanged += new System.EventHandler(this.bindingNavigatorPositionItem_TextChanged);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            this.bindingNavigatorMoveNextItem.ToolTipText = "下一页";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            this.bindingNavigatorMoveLastItem.ToolTipText = "最后一页";
            this.bindingNavigatorMoveLastItem.Click += new System.EventHandler(this.bindingNavigatorMoveLastItem_Click);
            // 
            // bindingNavigatorSeparator3
            // 
            this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
            this.bindingNavigatorSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorSetting
            // 
            this.bindingNavigatorSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorSetting.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorSetting.Image")));
            this.bindingNavigatorSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorSetting.Name = "bindingNavigatorSetting";
            this.bindingNavigatorSetting.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorSetting.Text = "Setting";
            this.bindingNavigatorSetting.ToolTipText = "限制记录设置";
            this.bindingNavigatorSetting.Click += new System.EventHandler(this.bindingNavigatorSetting_Click);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 814);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Viewer";
            this.Text = "Form1";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlPageCountSetting.ResumeLayout(false);
            this.pnlPageCountSetting.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ComboBox cbxResultSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxEndTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxStartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Panel pnlMid;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlPageCountSetting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxLimitPageSize;
        private System.Windows.Forms.TextBox tbxPageSize;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator3;
        private System.Windows.Forms.ToolStripButton bindingNavigatorSetting;
        private System.Windows.Forms.Button btnQuery;
    }
}

