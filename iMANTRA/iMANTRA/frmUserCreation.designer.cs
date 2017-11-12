namespace iMANTRA
{
    partial class frmUserCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserCreation));
            this.Theme = new System.Windows.Forms.Label();
            this.cmbTheme = new System.Windows.Forms.ComboBox();
            this.btnUser = new iMANTRA.PopupButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.dtpDeact = new iMANTRA.UserDT();
            this.cmbStat = new System.Windows.Forms.ComboBox();
            this.txtMob = new System.Windows.Forms.TextBox();
            this.txtEid = new System.Windows.Forms.TextBox();
            this.txtRpwd = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtConct = new System.Windows.Forms.TextBox();
            this.txtDesgn = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblDeact = new System.Windows.Forms.Label();
            this.lblMob = new System.Windows.Forms.Label();
            this.lblEid = new System.Windows.Forms.Label();
            this.lblStat = new System.Windows.Forms.Label();
            this.lblConct = new System.Windows.Forms.Label();
            this.lblDesgn = new System.Windows.Forms.Label();
            this.lblRpwd = new System.Windows.Forms.Label();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Theme
            // 
            this.Theme.AutoSize = true;
            this.Theme.Location = new System.Drawing.Point(12, 179);
            this.Theme.Name = "Theme";
            this.Theme.Size = new System.Drawing.Size(58, 18);
            this.Theme.TabIndex = 39;
            this.Theme.Text = "Theme";
            this.Theme.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbTheme
            // 
            this.cmbTheme.FormattingEnabled = true;
            this.cmbTheme.Location = new System.Drawing.Point(153, 179);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(175, 26);
            this.cmbTheme.TabIndex = 38;
            // 
            // btnUser
            // 
            this.btnUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUser.BackgroundImage")));
            this.btnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUser.Dispddlfields = "";
            this.btnUser.GradientBottom = System.Drawing.Color.Gray;
            this.btnUser.GradientTop = System.Drawing.SystemColors.Control;
            this.btnUser.Location = new System.Drawing.Point(338, 28);
            this.btnUser.Name = "btnUser";
            this.btnUser.Primaryddl = "";
            this.btnUser.Query_con = "";
            this.btnUser.Reftbltran_cd = "";
            this.btnUser.Size = new System.Drawing.Size(31, 20);
            this.btnUser.TabIndex = 2;
            this.btnUser.Tbl_nm = "";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click_1);
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(762, 23);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // dtpDeact
            // 
            this.dtpDeact.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpDeact.CustomFormat = " ";
            this.dtpDeact.DtValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDeact.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDeact.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeact.Location = new System.Drawing.Point(580, 151);
            this.dtpDeact.Margin = new System.Windows.Forms.Padding(0);
            this.dtpDeact.Name = "dtpDeact";
            this.dtpDeact.Size = new System.Drawing.Size(132, 26);
            this.dtpDeact.TabIndex = 11;
            // 
            // cmbStat
            // 
            this.cmbStat.FormattingEnabled = true;
            this.cmbStat.Location = new System.Drawing.Point(153, 146);
            this.cmbStat.Name = "cmbStat";
            this.cmbStat.Size = new System.Drawing.Size(175, 26);
            this.cmbStat.TabIndex = 10;
            this.cmbStat.SelectedIndexChanged += new System.EventHandler(this.cmbStat_SelectedIndexChanged);
            // 
            // txtMob
            // 
            this.txtMob.Location = new System.Drawing.Point(579, 118);
            this.txtMob.Name = "txtMob";
            this.txtMob.Size = new System.Drawing.Size(175, 26);
            this.txtMob.TabIndex = 9;
            // 
            // txtEid
            // 
            this.txtEid.Location = new System.Drawing.Point(579, 90);
            this.txtEid.Name = "txtEid";
            this.txtEid.Size = new System.Drawing.Size(175, 26);
            this.txtEid.TabIndex = 7;
            // 
            // txtRpwd
            // 
            this.txtRpwd.Location = new System.Drawing.Point(579, 58);
            this.txtRpwd.Name = "txtRpwd";
            this.txtRpwd.PasswordChar = '*';
            this.txtRpwd.Size = new System.Drawing.Size(175, 26);
            this.txtRpwd.TabIndex = 5;
            this.txtRpwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtRpwd_Validating);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(579, 29);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(175, 26);
            this.txtCode.TabIndex = 3;
            // 
            // txtConct
            // 
            this.txtConct.Location = new System.Drawing.Point(153, 117);
            this.txtConct.Name = "txtConct";
            this.txtConct.Size = new System.Drawing.Size(175, 26);
            this.txtConct.TabIndex = 8;
            // 
            // txtDesgn
            // 
            this.txtDesgn.Location = new System.Drawing.Point(153, 86);
            this.txtDesgn.Name = "txtDesgn";
            this.txtDesgn.Size = new System.Drawing.Size(175, 26);
            this.txtDesgn.TabIndex = 6;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(153, 57);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(175, 26);
            this.txtPwd.TabIndex = 4;
            this.txtPwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtPwd_Validating);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(153, 28);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(175, 26);
            this.txtUser.TabIndex = 1;
            this.txtUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtUser_Validating);
            // 
            // lblDeact
            // 
            this.lblDeact.AutoSize = true;
            this.lblDeact.Location = new System.Drawing.Point(393, 155);
            this.lblDeact.Name = "lblDeact";
            this.lblDeact.Size = new System.Drawing.Size(158, 18);
            this.lblDeact.TabIndex = 9;
            this.lblDeact.Text = "Deactivate From";
            // 
            // lblMob
            // 
            this.lblMob.AutoSize = true;
            this.lblMob.Location = new System.Drawing.Point(393, 121);
            this.lblMob.Name = "lblMob";
            this.lblMob.Size = new System.Drawing.Size(68, 18);
            this.lblMob.TabIndex = 8;
            this.lblMob.Text = "Mobile";
            // 
            // lblEid
            // 
            this.lblEid.AutoSize = true;
            this.lblEid.Location = new System.Drawing.Point(393, 94);
            this.lblEid.Name = "lblEid";
            this.lblEid.Size = new System.Drawing.Size(88, 18);
            this.lblEid.TabIndex = 7;
            this.lblEid.Text = "Email Id";
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(12, 150);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(68, 18);
            this.lblStat.TabIndex = 6;
            this.lblStat.Text = "Status";
            this.lblStat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblConct
            // 
            this.lblConct.AutoSize = true;
            this.lblConct.Location = new System.Drawing.Point(12, 120);
            this.lblConct.Name = "lblConct";
            this.lblConct.Size = new System.Drawing.Size(108, 18);
            this.lblConct.TabIndex = 5;
            this.lblConct.Text = "Contact No";
            // 
            // lblDesgn
            // 
            this.lblDesgn.AutoSize = true;
            this.lblDesgn.Location = new System.Drawing.Point(12, 89);
            this.lblDesgn.Name = "lblDesgn";
            this.lblDesgn.Size = new System.Drawing.Size(118, 18);
            this.lblDesgn.TabIndex = 4;
            this.lblDesgn.Text = "Designation";
            // 
            // lblRpwd
            // 
            this.lblRpwd.AutoSize = true;
            this.lblRpwd.Location = new System.Drawing.Point(393, 61);
            this.lblRpwd.Name = "lblRpwd";
            this.lblRpwd.Size = new System.Drawing.Size(168, 18);
            this.lblRpwd.TabIndex = 3;
            this.lblRpwd.Text = "Re-Type Password";
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(12, 60);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(88, 18);
            this.lblPwd.TabIndex = 2;
            this.lblPwd.Text = "Password";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(393, 32);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(98, 18);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "User Code";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 31);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(98, 18);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User Name";
            // 
            // frmUserCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(762, 212);
            this.ControlBox = false;
            this.Controls.Add(this.Theme);
            this.Controls.Add(this.cmbTheme);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.dtpDeact);
            this.Controls.Add(this.cmbStat);
            this.Controls.Add(this.txtMob);
            this.Controls.Add(this.txtEid);
            this.Controls.Add(this.txtRpwd);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtConct);
            this.Controls.Add(this.txtDesgn);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblDeact);
            this.Controls.Add(this.lblMob);
            this.Controls.Add(this.lblEid);
            this.Controls.Add(this.lblStat);
            this.Controls.Add(this.lblConct);
            this.Controls.Add(this.lblDesgn);
            this.Controls.Add(this.lblRpwd);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblUser);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserCreation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUserCreation_FormClosed);
            this.Load += new System.EventHandler(this.frmUserCreation_Load);
            this.Enter += new System.EventHandler(this.frmUserCreation_Enter);
            this.Resize += new System.EventHandler(this.frmUserCreation_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Label lblRpwd;
        private System.Windows.Forms.Label lblDesgn;
        private System.Windows.Forms.Label lblConct;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Label lblEid;
        private System.Windows.Forms.Label lblMob;
        private System.Windows.Forms.Label lblDeact;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtDesgn;
        private System.Windows.Forms.TextBox txtConct;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtRpwd;
        private System.Windows.Forms.TextBox txtEid;
        private System.Windows.Forms.TextBox txtMob;
        private System.Windows.Forms.ComboBox cmbStat;
        private UserDT dtpDeact;
        private UCToolBar ucToolBar1;
        private PopupButton btnUser;
        private System.Windows.Forms.ComboBox cmbTheme;
        private System.Windows.Forms.Label Theme;
    }
}