namespace CUSTOM_iMANTRA
{
    partial class frmstkstatus
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
            this.components = new System.ComponentModel.Container();
            this.gbxstk = new CUSTOM_iMANTRA.Grouper();
            this.lblstkqty = new System.Windows.Forms.Label();
            this.lblprod_nm = new System.Windows.Forms.Label();
            this.lbl2stk = new System.Windows.Forms.Label();
            this.LBL1stk = new System.Windows.Forms.Label();
            this.tmrstk = new System.Windows.Forms.Timer(this.components);
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.gbxstk.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxstk
            // 
            this.gbxstk.BackgroundColor = System.Drawing.Color.Transparent;
            this.gbxstk.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gbxstk.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.gbxstk.BorderColor = System.Drawing.Color.Black;
            this.gbxstk.BorderThickness = 1F;
            this.gbxstk.Controls.Add(this.lblstkqty);
            this.gbxstk.Controls.Add(this.lblprod_nm);
            this.gbxstk.Controls.Add(this.lbl2stk);
            this.gbxstk.Controls.Add(this.LBL1stk);
            this.gbxstk.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gbxstk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxstk.GroupImage = null;
            this.gbxstk.GroupTitle = "Stock Status";
            this.gbxstk.Location = new System.Drawing.Point(0, 29);
            this.gbxstk.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbxstk.Name = "gbxstk";
            this.gbxstk.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.gbxstk.PaintGroupBox = true;
            this.gbxstk.RoundCorners = 1;
            this.gbxstk.ShadowColor = System.Drawing.Color.DarkGray;
            this.gbxstk.ShadowControl = false;
            this.gbxstk.ShadowThickness = 1;
            this.gbxstk.Size = new System.Drawing.Size(360, 102);
            this.gbxstk.TabIndex = 0;
            this.gbxstk.TabStop = false;
            this.gbxstk.UseWaitCursor = true;
            // 
            // lblstkqty
            // 
            this.lblstkqty.AutoSize = true;
            this.lblstkqty.Location = new System.Drawing.Point(160, 76);
            this.lblstkqty.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblstkqty.Name = "lblstkqty";
            this.lblstkqty.Size = new System.Drawing.Size(105, 14);
            this.lblstkqty.TabIndex = 3;
            this.lblstkqty.Text = "Stock Quantity";
            this.lblstkqty.UseWaitCursor = true;
            // 
            // lblprod_nm
            // 
            this.lblprod_nm.AutoSize = true;
            this.lblprod_nm.Location = new System.Drawing.Point(160, 37);
            this.lblprod_nm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblprod_nm.Name = "lblprod_nm";
            this.lblprod_nm.Size = new System.Drawing.Size(100, 14);
            this.lblprod_nm.TabIndex = 2;
            this.lblprod_nm.Text = "Product Name";
            this.lblprod_nm.UseWaitCursor = true;
            // 
            // lbl2stk
            // 
            this.lbl2stk.AutoSize = true;
            this.lbl2stk.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2stk.Location = new System.Drawing.Point(10, 78);
            this.lbl2stk.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl2stk.Name = "lbl2stk";
            this.lbl2stk.Size = new System.Drawing.Size(56, 14);
            this.lbl2stk.TabIndex = 1;
            this.lbl2stk.Text = "Quantity :";
            this.lbl2stk.UseWaitCursor = true;
            // 
            // LBL1stk
            // 
            this.LBL1stk.AutoSize = true;
            this.LBL1stk.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL1stk.Location = new System.Drawing.Point(10, 37);
            this.LBL1stk.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LBL1stk.Name = "LBL1stk";
            this.LBL1stk.Size = new System.Drawing.Size(83, 14);
            this.LBL1stk.TabIndex = 0;
            this.LBL1stk.Text = "Product Name :";
            this.LBL1stk.UseWaitCursor = true;
            // 
            // tmrstk
            // 
            this.tmrstk.Enabled = true;
            this.tmrstk.Interval = 1000;
            this.tmrstk.Tick += new System.EventHandler(this.tmrstk_Tick);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = false;
            this.ucToolBar1.Minimize = false;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(360, 26);
            this.ucToolBar1.TabIndex = 3;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.UseWaitCursor = true;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmstkstatus
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(360, 131);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.gbxstk);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmstkstatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.TransparencyKey = System.Drawing.Color.White;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.frmstkstatus_Load);
            this.gbxstk.ResumeLayout(false);
            this.gbxstk.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper gbxstk;
        private System.Windows.Forms.Label LBL1stk;
        private System.Windows.Forms.Timer tmrstk;
        private System.Windows.Forms.Label lblprod_nm;
        private System.Windows.Forms.Label lbl2stk;
        private System.Windows.Forms.Label lblstkqty;
        private UCToolBar ucToolBar1;
    }
}