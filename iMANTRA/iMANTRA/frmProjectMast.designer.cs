namespace iMANTRA
{
    partial class frmProjectMast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectMast));
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkProd_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.proj_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proj_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proj_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proj_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grouper4 = new iMANTRA.Grouper();
            this.dtp_end = new iMANTRA.UserDT();
            this.dtp_appr = new iMANTRA.UserDT();
            this.dtp_start = new iMANTRA.UserDT();
            this.dtpRec = new iMANTRA.UserDT();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grouper1 = new iMANTRA.Grouper();
            this.txtproj_no = new System.Windows.Forms.TextBox();
            this.txtProj_dur = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnProd_nm = new iMANTRA.PopupButton();
            this.cmbproj_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtproj_desc = new System.Windows.Forms.TextBox();
            this.txtproj_nm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grouper2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.grouper4.SuspendLayout();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
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
            this.grouper2.Location = new System.Drawing.Point(694, 417);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(244, 61);
            this.grouper2.TabIndex = 18;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OTHER_DET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.Location = new System.Drawing.Point(26, 27);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(195, 27);
            this.OTHER_DET.TabIndex = 19;
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
            this.groupBox5.Location = new System.Drawing.Point(342, 416);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(343, 61);
            this.groupBox5.TabIndex = 15;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(195, 29);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(136, 26);
            this.dtp_deactive_from.TabIndex = 17;
            // 
            // chkProd_active
            // 
            this.chkProd_active.AutoSize = true;
            this.chkProd_active.Location = new System.Drawing.Point(5, 31);
            this.chkProd_active.Name = "chkProd_active";
            this.chkProd_active.Size = new System.Drawing.Size(137, 22);
            this.chkProd_active.TabIndex = 16;
            this.chkProd_active.Text = "De-Activate";
            this.chkProd_active.UseVisualStyleBackColor = true;
            this.chkProd_active.CheckedChanged += new System.EventHandler(this.chkProd_active_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(143, 32);
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
            this.grpbxSearch.GroupTitle = "Project List";
            this.grpbxSearch.Location = new System.Drawing.Point(4, 28);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(333, 450);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(9, 425);
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
            this.dgvSearch.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dgvSearch.BackgroundImage")));
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.proj_cd,
            this.proj_no,
            this.proj_nm,
            this.proj_desc});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 357;
            this.dgvSearch.Location = new System.Drawing.Point(6, 61);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(320, 357);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 320;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // proj_cd
            // 
            this.proj_cd.DataPropertyName = "proj_cd";
            this.proj_cd.HeaderText = "proj_cd";
            this.proj_cd.Name = "proj_cd";
            this.proj_cd.Visible = false;
            // 
            // proj_no
            // 
            this.proj_no.DataPropertyName = "proj_no";
            this.proj_no.HeaderText = "Code";
            this.proj_no.Name = "proj_no";
            this.proj_no.ReadOnly = true;
            this.proj_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // proj_nm
            // 
            this.proj_nm.DataPropertyName = "proj_nm";
            this.proj_nm.HeaderText = "Name";
            this.proj_nm.Name = "proj_nm";
            this.proj_nm.ReadOnly = true;
            // 
            // proj_desc
            // 
            this.proj_desc.DataPropertyName = "proj_desc";
            this.proj_desc.HeaderText = "Description";
            this.proj_desc.Name = "proj_desc";
            this.proj_desc.ReadOnly = true;
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
            // grouper4
            // 
            this.grouper4.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper4.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper4.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper4.BorderColor = System.Drawing.Color.Black;
            this.grouper4.BorderThickness = 1F;
            this.grouper4.Controls.Add(this.dtp_end);
            this.grouper4.Controls.Add(this.dtp_appr);
            this.grouper4.Controls.Add(this.dtp_start);
            this.grouper4.Controls.Add(this.dtpRec);
            this.grouper4.Controls.Add(this.label9);
            this.grouper4.Controls.Add(this.label8);
            this.grouper4.Controls.Add(this.label7);
            this.grouper4.Controls.Add(this.label5);
            this.grouper4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper4.GroupImage = null;
            this.grouper4.GroupTitle = "Details";
            this.grouper4.Location = new System.Drawing.Point(342, 294);
            this.grouper4.Name = "grouper4";
            this.grouper4.Padding = new System.Windows.Forms.Padding(20);
            this.grouper4.PaintGroupBox = true;
            this.grouper4.RoundCorners = 1;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 1;
            this.grouper4.Size = new System.Drawing.Size(596, 115);
            this.grouper4.TabIndex = 10;
            // 
            // dtp_end
            // 
            this.dtp_end.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_end.CustomFormat = "dd-MMM-yyyy";
            this.dtp_end.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_end.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_end.Location = new System.Drawing.Point(446, 77);
            this.dtp_end.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_end.Name = "dtp_end";
            this.dtp_end.Size = new System.Drawing.Size(130, 26);
            this.dtp_end.TabIndex = 14;
            this.dtp_end.Validating += new System.ComponentModel.CancelEventHandler(this.dtp_end_Validating);
            // 
            // dtp_appr
            // 
            this.dtp_appr.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_appr.CustomFormat = "dd-MMM-yyyy";
            this.dtp_appr.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_appr.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_appr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_appr.Location = new System.Drawing.Point(446, 35);
            this.dtp_appr.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_appr.Name = "dtp_appr";
            this.dtp_appr.Size = new System.Drawing.Size(130, 26);
            this.dtp_appr.TabIndex = 12;
            // 
            // dtp_start
            // 
            this.dtp_start.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_start.CustomFormat = "dd-MMM-yyyy";
            this.dtp_start.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_start.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_start.Location = new System.Drawing.Point(155, 77);
            this.dtp_start.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_start.Name = "dtp_start";
            this.dtp_start.Size = new System.Drawing.Size(130, 26);
            this.dtp_start.TabIndex = 13;
            // 
            // dtpRec
            // 
            this.dtpRec.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpRec.CustomFormat = "dd-MMM-yyyy";
            this.dtpRec.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtpRec.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRec.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRec.Location = new System.Drawing.Point(155, 35);
            this.dtpRec.Margin = new System.Windows.Forms.Padding(0);
            this.dtpRec.Name = "dtpRec";
            this.dtpRec.Size = new System.Drawing.Size(129, 26);
            this.dtpRec.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(303, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 18);
            this.label9.TabIndex = 13;
            this.label9.Text = "Ending Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(303, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 18);
            this.label8.TabIndex = 12;
            this.label8.Text = "Approval Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "Starting Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Receiving Date";
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.txtproj_no);
            this.grouper1.Controls.Add(this.txtProj_dur);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.btnProd_nm);
            this.grouper1.Controls.Add(this.cmbproj_type);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.txtproj_desc);
            this.grouper1.Controls.Add(this.txtproj_nm);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Project Details";
            this.grouper1.Location = new System.Drawing.Point(342, 27);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(596, 261);
            this.grouper1.TabIndex = 3;
            // 
            // txtproj_no
            // 
            this.txtproj_no.Location = new System.Drawing.Point(145, 36);
            this.txtproj_no.Name = "txtproj_no";
            this.txtproj_no.Size = new System.Drawing.Size(399, 26);
            this.txtproj_no.TabIndex = 4;
            this.txtproj_no.Validating += new System.ComponentModel.CancelEventHandler(this.txtproj_no_Validating);
            // 
            // txtProj_dur
            // 
            this.txtProj_dur.Location = new System.Drawing.Point(145, 212);
            this.txtProj_dur.Name = "txtProj_dur";
            this.txtProj_dur.Size = new System.Drawing.Size(399, 26);
            this.txtProj_dur.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 50);
            this.label4.TabIndex = 10;
            this.label4.Text = "Project Duration";
            // 
            // btnProd_nm
            // 
            this.btnProd_nm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProd_nm.BackgroundImage")));
            this.btnProd_nm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProd_nm.Dispddlfields = "";
            this.btnProd_nm.GradientBottom = System.Drawing.Color.Gray;
            this.btnProd_nm.GradientTop = System.Drawing.SystemColors.Control;
            this.btnProd_nm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProd_nm.Location = new System.Drawing.Point(551, 70);
            this.btnProd_nm.Margin = new System.Windows.Forms.Padding(0);
            this.btnProd_nm.Name = "btnProd_nm";
            this.btnProd_nm.Primaryddl = "";
            this.btnProd_nm.Query_con = "";
            this.btnProd_nm.Reftbltran_cd = "";
            this.btnProd_nm.Size = new System.Drawing.Size(32, 24);
            this.btnProd_nm.TabIndex = 6;
            this.btnProd_nm.Tbl_nm = "";
            this.btnProd_nm.UseVisualStyleBackColor = true;
            this.btnProd_nm.Click += new System.EventHandler(this.btnProd_nm_Click);
            // 
            // cmbproj_type
            // 
            this.cmbproj_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbproj_type.FormattingEnabled = true;
            this.cmbproj_type.Location = new System.Drawing.Point(145, 171);
            this.cmbproj_type.Name = "cmbproj_type";
            this.cmbproj_type.Size = new System.Drawing.Size(355, 26);
            this.cmbproj_type.TabIndex = 8;
            this.cmbproj_type.Validating += new System.ComponentModel.CancelEventHandler(this.cmbprod_type_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Project Type";
            // 
            // txtproj_desc
            // 
            this.txtproj_desc.Location = new System.Drawing.Point(145, 102);
            this.txtproj_desc.Multiline = true;
            this.txtproj_desc.Name = "txtproj_desc";
            this.txtproj_desc.Size = new System.Drawing.Size(399, 60);
            this.txtproj_desc.TabIndex = 7;
            // 
            // txtproj_nm
            // 
            this.txtproj_nm.Location = new System.Drawing.Point(145, 69);
            this.txtproj_nm.Name = "txtproj_nm";
            this.txtproj_nm.Size = new System.Drawing.Size(399, 26);
            this.txtproj_nm.TabIndex = 5;
            this.txtproj_nm.Validating += new System.ComponentModel.CancelEventHandler(this.txtproj_nm_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 50);
            this.label3.TabIndex = 2;
            this.label3.Text = "Project Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Project Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Code";
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(943, 26);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmProjectMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(943, 483);
            this.ControlBox = false;
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.grouper4);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grouper1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmProjectMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProjectMast_FormClosed);
            this.Load += new System.EventHandler(this.frmProjectMast_Load);
            this.Enter += new System.EventHandler(this.frmProjectMast_Enter);
            this.Resize += new System.EventHandler(this.frmProjectMast_Resize);
            this.grouper2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.grouper4.ResumeLayout(false);
            this.grouper4.PerformLayout();
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtproj_desc;
        private System.Windows.Forms.TextBox txtproj_nm;
        private System.Windows.Forms.ComboBox cmbproj_type;
        private System.Windows.Forms.Label label6;
        private Grouper grouper4;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper grouper2;
        private PopupButton OTHER_DET;
        private Grouper groupBox5;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.CheckBox chkProd_active;
        private System.Windows.Forms.Label label14;
        private PopupButton btnProd_nm;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.TextBox txtProj_dur;
        private System.Windows.Forms.Label label4;
        private UserDT dtpRec;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private UserDT dtp_start;
        private UserDT dtp_end;
        private UserDT dtp_appr;
        private System.Windows.Forms.TextBox txtproj_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn proj_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn proj_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn proj_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn proj_desc;
        private UCToolBar ucToolBar1;
    }
}