namespace iMANTRA
{
    partial class frmChangePwd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePwd));
            this.groupBox1 = new iMANTRA.Grouper();
            this.txtNRpwd = new System.Windows.Forms.TextBox();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOldPwd = new System.Windows.Forms.Label();
            this.txNpwd = new System.Windows.Forms.TextBox();
            this.lblNewPwd = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox1.BorderColor = System.Drawing.Color.Black;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.txtNRpwd);
            this.groupBox1.Controls.Add(this.txtOldPwd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblOldPwd);
            this.groupBox1.Controls.Add(this.txNpwd);
            this.groupBox1.Controls.Add(this.lblNewPwd);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "Password Setting";
            this.groupBox1.Location = new System.Drawing.Point(5, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            this.groupBox1.Size = new System.Drawing.Size(493, 197);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = true;
            // 
            // txtNRpwd
            // 
            this.txtNRpwd.Location = new System.Drawing.Point(197, 147);
            this.txtNRpwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtNRpwd.Name = "txtNRpwd";
            this.txtNRpwd.PasswordChar = '*';
            this.txtNRpwd.Size = new System.Drawing.Size(267, 26);
            this.txtNRpwd.TabIndex = 44;
            this.txtNRpwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtNRpwd_Validating_1);
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(197, 40);
            this.txtOldPwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.Size = new System.Drawing.Size(266, 26);
            this.txtOldPwd.TabIndex = 41;
            this.txtOldPwd.VisibleChanged += new System.EventHandler(this.txtOldPwd_VisibleChanged);
            this.txtOldPwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtOldPwd_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 18);
            this.label1.TabIndex = 45;
            this.label1.Text = "Re-Enter Password";
            // 
            // lblOldPwd
            // 
            this.lblOldPwd.AutoSize = true;
            this.lblOldPwd.Location = new System.Drawing.Point(18, 44);
            this.lblOldPwd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblOldPwd.Name = "lblOldPwd";
            this.lblOldPwd.Size = new System.Drawing.Size(128, 18);
            this.lblOldPwd.TabIndex = 40;
            this.lblOldPwd.Text = "Old Password";
            // 
            // txNpwd
            // 
            this.txNpwd.Location = new System.Drawing.Point(197, 94);
            this.txNpwd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txNpwd.Name = "txNpwd";
            this.txNpwd.PasswordChar = '*';
            this.txNpwd.Size = new System.Drawing.Size(266, 26);
            this.txNpwd.TabIndex = 43;
            this.txNpwd.Validating += new System.ComponentModel.CancelEventHandler(this.txNpwd_Validating_1);
            // 
            // lblNewPwd
            // 
            this.lblNewPwd.AutoSize = true;
            this.lblNewPwd.Location = new System.Drawing.Point(18, 94);
            this.lblNewPwd.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(128, 18);
            this.lblNewPwd.TabIndex = 42;
            this.lblNewPwd.Text = "New Password";
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(503, 28);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmChangePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(503, 226);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmChangePwd_FormClosed);
            this.Load += new System.EventHandler(this.frmChangePwd_Load);
            this.Enter += new System.EventHandler(this.frmChangePwd_Enter);
            this.Resize += new System.EventHandler(this.frmChangePwd_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper groupBox1;
        private System.Windows.Forms.TextBox txtNRpwd;
        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOldPwd;
        private System.Windows.Forms.TextBox txNpwd;
        private System.Windows.Forms.Label lblNewPwd;
    }
}