namespace iMANTRA
{
    partial class frmEmployeeMast
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
            this.grouper3 = new iMANTRA.Grouper();
            this.dtpDot = new iMANTRA.UserDT();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpDoj = new iMANTRA.UserDT();
            this.label8 = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkProd_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.emp_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emp_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ln = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grouper1 = new iMANTRA.Grouper();
            this.txtDesg = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.txtMiddle = new System.Windows.Forms.TextBox();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmployee_cd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper3.SuspendLayout();
            this.grouper2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper3
            // 
            this.grouper3.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper3.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper3.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper3.BorderColor = System.Drawing.Color.Black;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.dtpDot);
            this.grouper3.Controls.Add(this.label9);
            this.grouper3.Controls.Add(this.dtpDoj);
            this.grouper3.Controls.Add(this.label8);
            this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper3.GroupImage = null;
            this.grouper3.GroupTitle = "Duration Details";
            this.grouper3.Location = new System.Drawing.Point(340, 317);
            this.grouper3.Margin = new System.Windows.Forms.Padding(0);
            this.grouper3.Name = "grouper3";
            this.grouper3.Padding = new System.Windows.Forms.Padding(20);
            this.grouper3.PaintGroupBox = true;
            this.grouper3.RoundCorners = 1;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 1;
            this.grouper3.Size = new System.Drawing.Size(509, 65);
            this.grouper3.TabIndex = 11;
            // 
            // dtpDot
            // 
            this.dtpDot.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpDot.CustomFormat = "dd-MMM-yyyy";
            this.dtpDot.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtpDot.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDot.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDot.Location = new System.Drawing.Point(352, 30);
            this.dtpDot.Margin = new System.Windows.Forms.Padding(0);
            this.dtpDot.Name = "dtpDot";
            this.dtpDot.Size = new System.Drawing.Size(129, 26);
            this.dtpDot.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(300, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "DOT";
            // 
            // dtpDoj
            // 
            this.dtpDoj.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpDoj.CustomFormat = "dd-MMM-yyyy";
            this.dtpDoj.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtpDoj.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDoj.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDoj.Location = new System.Drawing.Point(68, 30);
            this.dtpDoj.Margin = new System.Windows.Forms.Padding(0);
            this.dtpDoj.Name = "dtpDoj";
            this.dtpDoj.Size = new System.Drawing.Size(130, 26);
            this.dtpDoj.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "DOJ ";
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(853, 25);
            this.ucToolBar1.TabIndex = 52;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.OTHER_DET);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Custom Fields";
            this.grouper2.Location = new System.Drawing.Point(675, 393);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(174, 65);
            this.grouper2.TabIndex = 17;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.Location = new System.Drawing.Point(11, 31);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(155, 27);
            this.OTHER_DET.TabIndex = 18;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "OTHER DETAILS";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox5.BorderColor = System.Drawing.Color.Black;
            this.groupBox5.BorderThickness = 1F;
            this.groupBox5.Controls.Add(this.dtp_deactive_from);
            this.groupBox5.Controls.Add(this.chkProd_active);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox5.GroupImage = null;
            this.groupBox5.GroupTitle = "Status";
            this.groupBox5.Location = new System.Drawing.Point(340, 393);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(331, 65);
            this.groupBox5.TabIndex = 14;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(191, 29);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(136, 26);
            this.dtp_deactive_from.TabIndex = 16;
            // 
            // chkProd_active
            // 
            this.chkProd_active.AutoSize = true;
            this.chkProd_active.Location = new System.Drawing.Point(5, 31);
            this.chkProd_active.Name = "chkProd_active";
            this.chkProd_active.Size = new System.Drawing.Size(137, 22);
            this.chkProd_active.TabIndex = 15;
            this.chkProd_active.Text = "De-Activate";
            this.chkProd_active.UseVisualStyleBackColor = true;
            this.chkProd_active.CheckedChanged += new System.EventHandler(this.chkProd_active_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(139, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 18);
            this.label14.TabIndex = 1;
            this.label14.Text = "From";
            // 
            // grpbxSearch
            // 
            this.grpbxSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpbxSearch.BorderColor = System.Drawing.Color.Black;
            this.grpbxSearch.BorderThickness = 1F;
            this.grpbxSearch.Controls.Add(this.lblRowsCount);
            this.grpbxSearch.Controls.Add(this.dgvSearch);
            this.grpbxSearch.Controls.Add(this.txtSearch);
            this.grpbxSearch.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpbxSearch.GroupImage = null;
            this.grpbxSearch.GroupTitle = "Employee List";
            this.grpbxSearch.Location = new System.Drawing.Point(2, 29);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(333, 429);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(9, 403);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 4;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.AllowUserToResizeRows = false;
            this.dgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.emp_cd,
            this.emp_code,
            this.fn,
            this.ln});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 339;
            this.dgvSearch.Location = new System.Drawing.Point(6, 61);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(320, 339);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 320;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // emp_cd
            // 
            this.emp_cd.DataPropertyName = "emp_cd";
            this.emp_cd.HeaderText = "emp_cd";
            this.emp_cd.Name = "emp_cd";
            this.emp_cd.Visible = false;
            // 
            // emp_code
            // 
            this.emp_code.DataPropertyName = "emp_code";
            this.emp_code.HeaderText = "Code";
            this.emp_code.Name = "emp_code";
            this.emp_code.ReadOnly = true;
            // 
            // fn
            // 
            this.fn.DataPropertyName = "fn";
            this.fn.HeaderText = "FirstName";
            this.fn.Name = "fn";
            this.fn.ReadOnly = true;
            // 
            // ln
            // 
            this.ln.DataPropertyName = "ln";
            this.ln.HeaderText = "LastName";
            this.ln.Name = "ln";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(4, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(323, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.txtDesg);
            this.grouper1.Controls.Add(this.txtEmail);
            this.grouper1.Controls.Add(this.txtContact);
            this.grouper1.Controls.Add(this.label7);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.txtLast);
            this.grouper1.Controls.Add(this.txtMiddle);
            this.grouper1.Controls.Add(this.txtFirst);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.txtEmployee_cd);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Employee Details";
            this.grouper1.Location = new System.Drawing.Point(340, 28);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(509, 286);
            this.grouper1.TabIndex = 3;
            // 
            // txtDesg
            // 
            this.txtDesg.Location = new System.Drawing.Point(147, 247);
            this.txtDesg.Name = "txtDesg";
            this.txtDesg.Size = new System.Drawing.Size(352, 26);
            this.txtDesg.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(147, 212);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(352, 26);
            this.txtEmail.TabIndex = 9;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(147, 177);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(352, 26);
            this.txtContact.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Designation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Email-Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Contact No";
            // 
            // txtLast
            // 
            this.txtLast.Location = new System.Drawing.Point(147, 142);
            this.txtLast.Name = "txtLast";
            this.txtLast.Size = new System.Drawing.Size(352, 26);
            this.txtLast.TabIndex = 7;
            // 
            // txtMiddle
            // 
            this.txtMiddle.Location = new System.Drawing.Point(147, 107);
            this.txtMiddle.Name = "txtMiddle";
            this.txtMiddle.Size = new System.Drawing.Size(352, 26);
            this.txtMiddle.TabIndex = 6;
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(147, 72);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(352, 26);
            this.txtFirst.TabIndex = 5;
            this.txtFirst.Validating += new System.ComponentModel.CancelEventHandler(this.txtFirst_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Last Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Middle Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            // 
            // txtEmployee_cd
            // 
            this.txtEmployee_cd.Location = new System.Drawing.Point(147, 37);
            this.txtEmployee_cd.Name = "txtEmployee_cd";
            this.txtEmployee_cd.Size = new System.Drawing.Size(352, 26);
            this.txtEmployee_cd.TabIndex = 4;
            this.txtEmployee_cd.Validating += new System.ComponentModel.CancelEventHandler(this.txtEmployee_cd_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee Code";
            // 
            // frmEmployeeMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(853, 464);
            this.ControlBox = false;
            this.Controls.Add(this.grouper3);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.grouper1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmEmployeeMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEmployeeMast_FormClosed);
            this.Load += new System.EventHandler(this.frmEmployeeMast_Load);
            this.Enter += new System.EventHandler(this.frmEmployeeMast_Enter);
            this.Resize += new System.EventHandler(this.frmEmployeeMast_Resize);
            this.grouper3.ResumeLayout(false);
            this.grouper3.PerformLayout();
            this.grouper2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper2;
        private PopupButton OTHER_DET;
        private Grouper groupBox5;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.CheckBox chkProd_active;
        private System.Windows.Forms.Label label14;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtEmployee_cd;
        private System.Windows.Forms.Label label1;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.TextBox txtLast;
        private System.Windows.Forms.TextBox txtMiddle;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn emp_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn emp_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn fn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ln;
        private System.Windows.Forms.TextBox txtDesg;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private Grouper grouper3;
        private UserDT dtpDot;
        private System.Windows.Forms.Label label9;
        private UserDT dtpDoj;
        private System.Windows.Forms.Label label8;


    }
}