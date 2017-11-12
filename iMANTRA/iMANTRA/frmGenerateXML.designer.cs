namespace iMANTRA
{
    partial class frmGenerateXML
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
            this.btnClose = new iMANTRA.PopupButton();
            this.btnDone = new iMANTRA.PopupButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grpGenerateXML = new iMANTRA.Grouper();
            this.txtSystemIp = new iMANTRA.PopupTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSystemNm = new iMANTRA.PopupTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMotherBoardId = new iMANTRA.PopupTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMacId = new iMANTRA.PopupTextBox();
            this.txtNoOfUsers = new iMANTRA.PopupTextBox();
            this.txtModules_nm = new iMANTRA.PopupTextBox();
            this.cmbOrgType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOrgNm = new System.Windows.Forms.ComboBox();
            this.grpGenerateXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Dispddlfields = "";
            this.btnClose.GradientBottom = System.Drawing.Color.Gray;
            this.btnClose.GradientTop = System.Drawing.SystemColors.Control;
            this.btnClose.Location = new System.Drawing.Point(536, 453);
            this.btnClose.Name = "btnClose";
            this.btnClose.Primaryddl = "";
            this.btnClose.Query_con = "";
            this.btnClose.Reftbltran_cd = "";
            this.btnClose.Size = new System.Drawing.Size(76, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Tbl_nm = "";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.GradientBottom = System.Drawing.Color.Gray;
            this.btnDone.GradientTop = System.Drawing.SystemColors.Control;
            this.btnDone.Location = new System.Drawing.Point(357, 453);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(175, 25);
            this.btnDone.TabIndex = 3;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Generate License";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(615, 23);
            this.ucToolBar1.TabIndex = 2;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.Color.Black;
            this.ucToolBar1.Width1 = 0;
            // 
            // grpGenerateXML
            // 
            this.grpGenerateXML.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpGenerateXML.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpGenerateXML.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpGenerateXML.BorderColor = System.Drawing.Color.Black;
            this.grpGenerateXML.BorderThickness = 1F;
            this.grpGenerateXML.Controls.Add(this.txtSystemIp);
            this.grpGenerateXML.Controls.Add(this.label8);
            this.grpGenerateXML.Controls.Add(this.txtSystemNm);
            this.grpGenerateXML.Controls.Add(this.label7);
            this.grpGenerateXML.Controls.Add(this.txtMotherBoardId);
            this.grpGenerateXML.Controls.Add(this.label6);
            this.grpGenerateXML.Controls.Add(this.txtMacId);
            this.grpGenerateXML.Controls.Add(this.txtNoOfUsers);
            this.grpGenerateXML.Controls.Add(this.txtModules_nm);
            this.grpGenerateXML.Controls.Add(this.cmbOrgType);
            this.grpGenerateXML.Controls.Add(this.label5);
            this.grpGenerateXML.Controls.Add(this.label4);
            this.grpGenerateXML.Controls.Add(this.label3);
            this.grpGenerateXML.Controls.Add(this.label2);
            this.grpGenerateXML.Controls.Add(this.label1);
            this.grpGenerateXML.Controls.Add(this.cmbOrgNm);
            this.grpGenerateXML.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpGenerateXML.GroupImage = null;
            this.grpGenerateXML.GroupTitle = "License Details";
            this.grpGenerateXML.Location = new System.Drawing.Point(4, 28);
            this.grpGenerateXML.Name = "grpGenerateXML";
            this.grpGenerateXML.Padding = new System.Windows.Forms.Padding(20);
            this.grpGenerateXML.PaintGroupBox = true;
            this.grpGenerateXML.RoundCorners = 1;
            this.grpGenerateXML.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpGenerateXML.ShadowControl = false;
            this.grpGenerateXML.ShadowThickness = 1;
            this.grpGenerateXML.Size = new System.Drawing.Size(607, 422);
            this.grpGenerateXML.TabIndex = 0;
            // 
            // txtSystemIp
            // 
            this.txtSystemIp.Dispddlfields = "";
            this.txtSystemIp.Location = new System.Drawing.Point(217, 388);
            this.txtSystemIp.Name = "txtSystemIp";
            this.txtSystemIp.Primaryddl = "";
            this.txtSystemIp.Query_con = "";
            this.txtSystemIp.ReadOnly = true;
            this.txtSystemIp.Reftbltran_cd = "";
            this.txtSystemIp.Size = new System.Drawing.Size(384, 26);
            this.txtSystemIp.TabIndex = 15;
            this.txtSystemIp.Tbl_nm = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 391);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "System IP  : ";
            // 
            // txtSystemNm
            // 
            this.txtSystemNm.Dispddlfields = "";
            this.txtSystemNm.Location = new System.Drawing.Point(217, 337);
            this.txtSystemNm.Name = "txtSystemNm";
            this.txtSystemNm.Primaryddl = "";
            this.txtSystemNm.Query_con = "";
            this.txtSystemNm.ReadOnly = true;
            this.txtSystemNm.Reftbltran_cd = "";
            this.txtSystemNm.Size = new System.Drawing.Size(384, 26);
            this.txtSystemNm.TabIndex = 13;
            this.txtSystemNm.Tbl_nm = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 341);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "System Name : ";
            // 
            // txtMotherBoardId
            // 
            this.txtMotherBoardId.Dispddlfields = "";
            this.txtMotherBoardId.Location = new System.Drawing.Point(217, 287);
            this.txtMotherBoardId.Name = "txtMotherBoardId";
            this.txtMotherBoardId.Primaryddl = "";
            this.txtMotherBoardId.Query_con = "";
            this.txtMotherBoardId.ReadOnly = true;
            this.txtMotherBoardId.Reftbltran_cd = "";
            this.txtMotherBoardId.Size = new System.Drawing.Size(384, 26);
            this.txtMotherBoardId.TabIndex = 11;
            this.txtMotherBoardId.Tbl_nm = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Mother Board Id : ";
            // 
            // txtMacId
            // 
            this.txtMacId.Dispddlfields = "";
            this.txtMacId.Location = new System.Drawing.Point(217, 237);
            this.txtMacId.Name = "txtMacId";
            this.txtMacId.Primaryddl = "";
            this.txtMacId.Query_con = "";
            this.txtMacId.ReadOnly = true;
            this.txtMacId.Reftbltran_cd = "";
            this.txtMacId.Size = new System.Drawing.Size(384, 26);
            this.txtMacId.TabIndex = 9;
            this.txtMacId.Tbl_nm = "";
            // 
            // txtNoOfUsers
            // 
            this.txtNoOfUsers.Dispddlfields = "";
            this.txtNoOfUsers.Location = new System.Drawing.Point(217, 187);
            this.txtNoOfUsers.Name = "txtNoOfUsers";
            this.txtNoOfUsers.Primaryddl = "";
            this.txtNoOfUsers.Query_con = "";
            this.txtNoOfUsers.Reftbltran_cd = "";
            this.txtNoOfUsers.Size = new System.Drawing.Size(384, 26);
            this.txtNoOfUsers.TabIndex = 8;
            this.txtNoOfUsers.Tbl_nm = "";
            this.txtNoOfUsers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfUsers_KeyPress);
            // 
            // txtModules_nm
            // 
            this.txtModules_nm.Dispddlfields = "";
            this.txtModules_nm.Location = new System.Drawing.Point(217, 137);
            this.txtModules_nm.Name = "txtModules_nm";
            this.txtModules_nm.Primaryddl = "";
            this.txtModules_nm.Query_con = "";
            this.txtModules_nm.ReadOnly = true;
            this.txtModules_nm.Reftbltran_cd = "";
            this.txtModules_nm.Size = new System.Drawing.Size(384, 26);
            this.txtModules_nm.TabIndex = 7;
            this.txtModules_nm.Tbl_nm = "";
            // 
            // cmbOrgType
            // 
            this.cmbOrgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgType.FormattingEnabled = true;
            this.cmbOrgType.Location = new System.Drawing.Point(217, 86);
            this.cmbOrgType.Name = "cmbOrgType";
            this.cmbOrgType.Size = new System.Drawing.Size(384, 26);
            this.cmbOrgType.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Mac Id : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "No. Of Users : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Modules Name : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Organization Type : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Organization Name : ";
            // 
            // cmbOrgNm
            // 
            this.cmbOrgNm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgNm.FormattingEnabled = true;
            this.cmbOrgNm.Location = new System.Drawing.Point(217, 38);
            this.cmbOrgNm.Name = "cmbOrgNm";
            this.cmbOrgNm.Size = new System.Drawing.Size(384, 26);
            this.cmbOrgNm.TabIndex = 0;
            this.cmbOrgNm.SelectedIndexChanged += new System.EventHandler(this.cmbOrgNm_SelectedIndexChanged);
            // 
            // frmGenerateXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(615, 481);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grpGenerateXML);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmGenerateXML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGenerateXML_FormClosed);
            this.Load += new System.EventHandler(this.frmGenerateXML_Load);
            this.Enter += new System.EventHandler(this.frmGenerateXML_Enter);
            this.grpGenerateXML.ResumeLayout(false);
            this.grpGenerateXML.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grpGenerateXML;
        private UCToolBar ucToolBar1;
        private PopupButton btnDone;
        private PopupButton btnClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOrgNm;
        private System.Windows.Forms.ComboBox cmbOrgType;
        private PopupTextBox txtModules_nm;
        private PopupTextBox txtNoOfUsers;
        private PopupTextBox txtMacId;
        private PopupTextBox txtMotherBoardId;
        private System.Windows.Forms.Label label6;
        private PopupTextBox txtSystemIp;
        private System.Windows.Forms.Label label8;
        private PopupTextBox txtSystemNm;
        private System.Windows.Forms.Label label7;
    }
}