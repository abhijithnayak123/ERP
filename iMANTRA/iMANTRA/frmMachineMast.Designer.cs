namespace iMANTRA
{
    partial class frmMachineMast
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
            this.grouper1 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.mac_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mac_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mac_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mac_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkmac_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new iMANTRA.Grouper();
            this.txtmac_make = new System.Windows.Forms.TextBox();
            this.txtmac_manu = new System.Windows.Forms.TextBox();
            this.stpmac_install_dt = new iMANTRA.UserDT();
            this.dtpmac_pur_dt = new iMANTRA.UserDT();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new iMANTRA.Grouper();
            this.txtmac_cost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new iMANTRA.Grouper();
            this.txtmac_serial = new System.Windows.Forms.TextBox();
            this.txtmac_desc = new System.Windows.Forms.TextBox();
            this.cmbmac_loc = new System.Windows.Forms.ComboBox();
            this.cmbmac_type = new System.Windows.Forms.ComboBox();
            this.txtmac_nm = new System.Windows.Forms.TextBox();
            this.txtmac_no = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grouper1.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.OTHER_DET);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Custom Fields";
            this.grouper1.Location = new System.Drawing.Point(652, 406);
            this.grouper1.Margin = new System.Windows.Forms.Padding(0);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(314, 73);
            this.grouper1.TabIndex = 20;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.Location = new System.Drawing.Point(67, 31);
            this.OTHER_DET.Margin = new System.Windows.Forms.Padding(4);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(195, 27);
            this.OTHER_DET.TabIndex = 21;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "OTHER DETAILS";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.btnCustom_Click);
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
            this.grpbxSearch.GroupTitle = "Machine List";
            this.grpbxSearch.Location = new System.Drawing.Point(4, 29);
            this.grpbxSearch.Margin = new System.Windows.Forms.Padding(0);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 3;
            this.grpbxSearch.Size = new System.Drawing.Size(291, 450);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(7, 424);
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
            this.mac_id,
            this.mac_no,
            this.mac_nm,
            this.mac_type});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 345;
            this.dgvSearch.Location = new System.Drawing.Point(6, 70);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(281, 345);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 281;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // mac_id
            // 
            this.mac_id.DataPropertyName = "mac_id";
            this.mac_id.HeaderText = "machine_id";
            this.mac_id.Name = "mac_id";
            this.mac_id.Visible = false;
            // 
            // mac_no
            // 
            this.mac_no.DataPropertyName = "mac_no";
            this.mac_no.HeaderText = "Machine No";
            this.mac_no.Name = "mac_no";
            this.mac_no.ReadOnly = true;
            this.mac_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // mac_nm
            // 
            this.mac_nm.DataPropertyName = "mac_nm";
            this.mac_nm.HeaderText = "Machine Name";
            this.mac_nm.Name = "mac_nm";
            this.mac_nm.ReadOnly = true;
            this.mac_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // mac_type
            // 
            this.mac_type.DataPropertyName = "mac_type";
            this.mac_type.HeaderText = "Machine Type";
            this.mac_type.Name = "mac_type";
            this.mac_type.ReadOnly = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 37);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(281, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox5.BorderColor = System.Drawing.Color.Black;
            this.groupBox5.BorderThickness = 1F;
            this.groupBox5.Controls.Add(this.dtp_deactive_from);
            this.groupBox5.Controls.Add(this.chkmac_active);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox5.GroupImage = null;
            this.groupBox5.GroupTitle = "Status";
            this.groupBox5.Location = new System.Drawing.Point(298, 405);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(333, 73);
            this.groupBox5.TabIndex = 17;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(187, 33);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(130, 26);
            this.dtp_deactive_from.TabIndex = 19;
            // 
            // chkmac_active
            // 
            this.chkmac_active.AutoSize = true;
            this.chkmac_active.Location = new System.Drawing.Point(4, 34);
            this.chkmac_active.Margin = new System.Windows.Forms.Padding(4);
            this.chkmac_active.Name = "chkmac_active";
            this.chkmac_active.Size = new System.Drawing.Size(137, 22);
            this.chkmac_active.TabIndex = 18;
            this.chkmac_active.Text = "De-Activate";
            this.chkmac_active.UseVisualStyleBackColor = true;
            this.chkmac_active.CheckedChanged += new System.EventHandler(this.chkmac_active_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(140, 37);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 18);
            this.label14.TabIndex = 1;
            this.label14.Text = "From";
            // 
            // groupBox4
            // 
            this.groupBox4.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox4.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox4.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox4.BorderColor = System.Drawing.Color.Black;
            this.groupBox4.BorderThickness = 1F;
            this.groupBox4.Controls.Add(this.txtmac_make);
            this.groupBox4.Controls.Add(this.txtmac_manu);
            this.groupBox4.Controls.Add(this.stpmac_install_dt);
            this.groupBox4.Controls.Add(this.dtpmac_pur_dt);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox4.GroupImage = null;
            this.groupBox4.GroupTitle = "SetUp";
            this.groupBox4.Location = new System.Drawing.Point(298, 282);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox4.PaintGroupBox = true;
            this.groupBox4.RoundCorners = 1;
            this.groupBox4.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox4.ShadowControl = false;
            this.groupBox4.ShadowThickness = 1;
            this.groupBox4.Size = new System.Drawing.Size(668, 121);
            this.groupBox4.TabIndex = 12;
            // 
            // txtmac_make
            // 
            this.txtmac_make.Location = new System.Drawing.Point(460, 79);
            this.txtmac_make.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_make.Name = "txtmac_make";
            this.txtmac_make.Size = new System.Drawing.Size(200, 26);
            this.txtmac_make.TabIndex = 16;
            // 
            // txtmac_manu
            // 
            this.txtmac_manu.Location = new System.Drawing.Point(131, 79);
            this.txtmac_manu.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_manu.Name = "txtmac_manu";
            this.txtmac_manu.Size = new System.Drawing.Size(195, 26);
            this.txtmac_manu.TabIndex = 15;
            // 
            // stpmac_install_dt
            // 
            this.stpmac_install_dt.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.stpmac_install_dt.CustomFormat = "dd-MMM-yyyy";
            this.stpmac_install_dt.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.stpmac_install_dt.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stpmac_install_dt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.stpmac_install_dt.Location = new System.Drawing.Point(460, 27);
            this.stpmac_install_dt.Margin = new System.Windows.Forms.Padding(4);
            this.stpmac_install_dt.Name = "stpmac_install_dt";
            this.stpmac_install_dt.Size = new System.Drawing.Size(154, 26);
            this.stpmac_install_dt.TabIndex = 14;
            // 
            // dtpmac_pur_dt
            // 
            this.dtpmac_pur_dt.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpmac_pur_dt.CustomFormat = "dd-MMM-yyyy";
            this.dtpmac_pur_dt.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtpmac_pur_dt.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpmac_pur_dt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpmac_pur_dt.Location = new System.Drawing.Point(131, 37);
            this.dtpmac_pur_dt.Margin = new System.Windows.Forms.Padding(4);
            this.dtpmac_pur_dt.Name = "dtpmac_pur_dt";
            this.dtpmac_pur_dt.Size = new System.Drawing.Size(158, 26);
            this.dtpmac_pur_dt.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(330, 83);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 18);
            this.label11.TabIndex = 3;
            this.label11.Text = "Make/Model";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 82);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 18);
            this.label10.TabIndex = 2;
            this.label10.Text = "Manufacturer";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(323, 26);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 36);
            this.label9.TabIndex = 1;
            this.label9.Text = "Installation Date";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(7, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 36);
            this.label8.TabIndex = 0;
            this.label8.Text = "Purchase Date";
            // 
            // groupBox3
            // 
            this.groupBox3.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox3.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox3.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox3.BorderColor = System.Drawing.Color.Black;
            this.groupBox3.BorderThickness = 1F;
            this.groupBox3.Controls.Add(this.txtmac_cost);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox3.GroupImage = null;
            this.groupBox3.GroupTitle = "Cost";
            this.groupBox3.Location = new System.Drawing.Point(298, 204);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox3.PaintGroupBox = true;
            this.groupBox3.RoundCorners = 1;
            this.groupBox3.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox3.ShadowControl = false;
            this.groupBox3.ShadowThickness = 1;
            this.groupBox3.Size = new System.Drawing.Size(668, 76);
            this.groupBox3.TabIndex = 10;
            // 
            // txtmac_cost
            // 
            this.txtmac_cost.Location = new System.Drawing.Point(125, 30);
            this.txtmac_cost.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_cost.Name = "txtmac_cost";
            this.txtmac_cost.Size = new System.Drawing.Size(192, 26);
            this.txtmac_cost.TabIndex = 11;
            this.txtmac_cost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmac_cost_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Cost/Hour";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.BorderThickness = 1F;
            this.groupBox2.Controls.Add(this.txtmac_serial);
            this.groupBox2.Controls.Add(this.txtmac_desc);
            this.groupBox2.Controls.Add(this.cmbmac_loc);
            this.groupBox2.Controls.Add(this.cmbmac_type);
            this.groupBox2.Controls.Add(this.txtmac_nm);
            this.groupBox2.Controls.Add(this.txtmac_no);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "Basic";
            this.groupBox2.Location = new System.Drawing.Point(298, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox2.PaintGroupBox = true;
            this.groupBox2.RoundCorners = 1;
            this.groupBox2.ShadowColor = System.Drawing.Color.Transparent;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 1;
            this.groupBox2.Size = new System.Drawing.Size(668, 172);
            this.groupBox2.TabIndex = 3;
            // 
            // txtmac_serial
            // 
            this.txtmac_serial.Location = new System.Drawing.Point(451, 126);
            this.txtmac_serial.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_serial.Name = "txtmac_serial";
            this.txtmac_serial.Size = new System.Drawing.Size(211, 26);
            this.txtmac_serial.TabIndex = 9;
            // 
            // txtmac_desc
            // 
            this.txtmac_desc.Location = new System.Drawing.Point(125, 82);
            this.txtmac_desc.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_desc.Name = "txtmac_desc";
            this.txtmac_desc.Size = new System.Drawing.Size(192, 26);
            this.txtmac_desc.TabIndex = 6;
            // 
            // cmbmac_loc
            // 
            this.cmbmac_loc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmac_loc.FormattingEnabled = true;
            this.cmbmac_loc.Location = new System.Drawing.Point(125, 127);
            this.cmbmac_loc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbmac_loc.Name = "cmbmac_loc";
            this.cmbmac_loc.Size = new System.Drawing.Size(192, 26);
            this.cmbmac_loc.TabIndex = 8;
            // 
            // cmbmac_type
            // 
            this.cmbmac_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbmac_type.FormattingEnabled = true;
            this.cmbmac_type.Location = new System.Drawing.Point(451, 82);
            this.cmbmac_type.Margin = new System.Windows.Forms.Padding(4);
            this.cmbmac_type.Name = "cmbmac_type";
            this.cmbmac_type.Size = new System.Drawing.Size(211, 26);
            this.cmbmac_type.TabIndex = 7;
            // 
            // txtmac_nm
            // 
            this.txtmac_nm.Location = new System.Drawing.Point(451, 41);
            this.txtmac_nm.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_nm.Name = "txtmac_nm";
            this.txtmac_nm.Size = new System.Drawing.Size(211, 26);
            this.txtmac_nm.TabIndex = 5;
            this.txtmac_nm.Validating += new System.ComponentModel.CancelEventHandler(this.txtmac_nm_Validating);
            // 
            // txtmac_no
            // 
            this.txtmac_no.Location = new System.Drawing.Point(125, 42);
            this.txtmac_no.Margin = new System.Windows.Forms.Padding(4);
            this.txtmac_no.Name = "txtmac_no";
            this.txtmac_no.Size = new System.Drawing.Size(192, 26);
            this.txtmac_no.TabIndex = 4;
            this.txtmac_no.Validating += new System.ComponentModel.CancelEventHandler(this.txtmac_no_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 84);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "Description";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(324, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 36);
            this.label5.TabIndex = 4;
            this.label5.Text = "Machine Serial No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Machine Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Machine Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(7, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Machine No";
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(968, 26);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmMachineMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(968, 482);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMachineMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMachineMast_FormClosed);
            this.Load += new System.EventHandler(this.frmMachineMast_Load);
            this.Enter += new System.EventHandler(this.frmMachineMast_Enter);
            this.Resize += new System.EventHandler(this.frmMachineMast_Resize);
            this.grouper1.ResumeLayout(false);
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper groupBox2;
        private Grouper groupBox3;
        private Grouper groupBox4;
        private Grouper groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtmac_serial;
        private System.Windows.Forms.TextBox txtmac_desc;
        private System.Windows.Forms.ComboBox cmbmac_loc;
        private System.Windows.Forms.ComboBox cmbmac_type;
        private System.Windows.Forms.TextBox txtmac_nm;
        private System.Windows.Forms.TextBox txtmac_no;
        private UserDT stpmac_install_dt;
        private UserDT dtpmac_pur_dt;
        private System.Windows.Forms.TextBox txtmac_make;
        private System.Windows.Forms.TextBox txtmac_manu;
        private System.Windows.Forms.CheckBox chkmac_active;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.TextBox txtmac_cost;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_type;
        private PopupButton OTHER_DET;
        private Grouper grouper1;
        private System.Windows.Forms.Label lblRowsCount;
    }
}