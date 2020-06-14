namespace UITest
{
    partial class WorkStation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxWs = new System.Windows.Forms.GroupBox();
            this.tbxCapacity = new System.Windows.Forms.TextBox();
            this.tbxYield = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnGeen = new System.Windows.Forms.Button();
            this.lblYield = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.gbxWs.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxWs
            // 
            this.gbxWs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxWs.Controls.Add(this.tbxCapacity);
            this.gbxWs.Controls.Add(this.tbxYield);
            this.gbxWs.Controls.Add(this.lblTitle);
            this.gbxWs.Controls.Add(this.lblStatus);
            this.gbxWs.Controls.Add(this.btnGeen);
            this.gbxWs.Controls.Add(this.lblYield);
            this.gbxWs.Controls.Add(this.lblCapacity);
            this.gbxWs.Controls.Add(this.btnYellow);
            this.gbxWs.Controls.Add(this.btnRed);
            this.gbxWs.Location = new System.Drawing.Point(5, 8);
            this.gbxWs.Name = "gbxWs";
            this.gbxWs.Size = new System.Drawing.Size(233, 319);
            this.gbxWs.TabIndex = 30;
            this.gbxWs.TabStop = false;
            // 
            // tbxCapacity
            // 
            this.tbxCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxCapacity.BackColor = System.Drawing.SystemColors.Control;
            this.tbxCapacity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxCapacity.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.tbxCapacity.ForeColor = System.Drawing.Color.Blue;
            this.tbxCapacity.Location = new System.Drawing.Point(121, 81);
            this.tbxCapacity.Name = "tbxCapacity";
            this.tbxCapacity.Size = new System.Drawing.Size(105, 22);
            this.tbxCapacity.TabIndex = 35;
            this.tbxCapacity.Text = "8888888";
            this.tbxCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxYield
            // 
            this.tbxYield.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxYield.BackColor = System.Drawing.SystemColors.Control;
            this.tbxYield.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxYield.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.tbxYield.ForeColor = System.Drawing.Color.Blue;
            this.tbxYield.Location = new System.Drawing.Point(121, 172);
            this.tbxYield.Name = "tbxYield";
            this.tbxYield.Size = new System.Drawing.Size(105, 22);
            this.tbxYield.TabIndex = 34;
            this.tbxYield.Text = "88.88%";
            this.tbxYield.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(3, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(227, 29);
            this.lblTitle.TabIndex = 31;
            this.lblTitle.Text = "OP010";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Lime;
            this.lblStatus.Location = new System.Drawing.Point(74, 258);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 26);
            this.lblStatus.TabIndex = 30;
            this.lblStatus.Text = "运行中";
            // 
            // btnGeen
            // 
            this.btnGeen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGeen.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnGeen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeen.Font = new System.Drawing.Font("宋体", 18F);
            this.btnGeen.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnGeen.Location = new System.Drawing.Point(20, 253);
            this.btnGeen.Name = "btnGeen";
            this.btnGeen.Size = new System.Drawing.Size(36, 36);
            this.btnGeen.TabIndex = 30;
            this.btnGeen.UseVisualStyleBackColor = false;
            // 
            // lblYield
            // 
            this.lblYield.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYield.AutoSize = true;
            this.lblYield.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblYield.ForeColor = System.Drawing.Color.Black;
            this.lblYield.Location = new System.Drawing.Point(74, 168);
            this.lblYield.Name = "lblYield";
            this.lblYield.Size = new System.Drawing.Size(50, 26);
            this.lblYield.TabIndex = 6;
            this.lblYield.Text = "良率";
            // 
            // lblCapacity
            // 
            this.lblCapacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCapacity.ForeColor = System.Drawing.Color.Black;
            this.lblCapacity.Location = new System.Drawing.Point(74, 78);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(50, 26);
            this.lblCapacity.TabIndex = 4;
            this.lblCapacity.Text = "产能";
            // 
            // btnYellow
            // 
            this.btnYellow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnYellow.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYellow.Font = new System.Drawing.Font("宋体", 18F);
            this.btnYellow.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnYellow.Location = new System.Drawing.Point(20, 165);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(36, 36);
            this.btnYellow.TabIndex = 3;
            this.btnYellow.UseVisualStyleBackColor = false;
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnRed.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRed.Font = new System.Drawing.Font("宋体", 18F);
            this.btnRed.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnRed.Location = new System.Drawing.Point(20, 76);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(36, 36);
            this.btnRed.TabIndex = 2;
            this.btnRed.UseVisualStyleBackColor = false;
            // 
            // WorkStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(243, 338);
            this.Controls.Add(this.gbxWs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WorkStation";
            this.Text = "WorkStation";
            this.gbxWs.ResumeLayout(false);
            this.gbxWs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxWs;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnGeen;
        private System.Windows.Forms.Label lblYield;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.TextBox tbxCapacity;
        private System.Windows.Forms.TextBox tbxYield;
    }
}