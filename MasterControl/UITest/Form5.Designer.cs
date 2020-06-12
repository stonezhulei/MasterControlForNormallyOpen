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
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1914, 1047);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageBoard
            // 
            this.tabPageBoard.Location = new System.Drawing.Point(4, 25);
            this.tabPageBoard.Name = "tabPageBoard";
            this.tabPageBoard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBoard.Size = new System.Drawing.Size(1906, 1018);
            this.tabPageBoard.TabIndex = 0;
            this.tabPageBoard.Text = " 看板";
            this.tabPageBoard.UseVisualStyleBackColor = true;
            // 
            // tabPageDBView
            // 
            this.tabPageDBView.Location = new System.Drawing.Point(4, 25);
            this.tabPageDBView.Name = "tabPageDBView";
            this.tabPageDBView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDBView.Size = new System.Drawing.Size(1906, 1018);
            this.tabPageDBView.TabIndex = 1;
            this.tabPageDBView.Text = " 溯源";
            this.tabPageDBView.UseVisualStyleBackColor = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1914, 1047);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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