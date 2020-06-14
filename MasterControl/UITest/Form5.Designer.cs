namespace UITest
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBoard = new System.Windows.Forms.TabPage();
            this.tabPageDBView = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageBoard);
            this.tabControl.Controls.Add(this.tabPageDBView);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.ItemSize = new System.Drawing.Size(1000, 35);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1531, 838);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageBoard
            // 
            this.tabPageBoard.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPageBoard.Location = new System.Drawing.Point(4, 39);
            this.tabPageBoard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageBoard.Name = "tabPageBoard";
            this.tabPageBoard.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageBoard.Size = new System.Drawing.Size(1523, 795);
            this.tabPageBoard.TabIndex = 0;
            this.tabPageBoard.Text = "             看 板             ";
            this.tabPageBoard.UseVisualStyleBackColor = true;
            // 
            // tabPageDBView
            // 
            this.tabPageDBView.Location = new System.Drawing.Point(4, 39);
            this.tabPageDBView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageDBView.Name = "tabPageDBView";
            this.tabPageDBView.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageDBView.Size = new System.Drawing.Size(1523, 795);
            this.tabPageDBView.TabIndex = 1;
            this.tabPageDBView.Text = "             溯 源             ";
            this.tabPageDBView.UseVisualStyleBackColor = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1531, 838);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "常开线";
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBoard;
        private System.Windows.Forms.TabPage tabPageDBView;
    }
}