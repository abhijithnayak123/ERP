namespace iMANTRA
{
    partial class frmSTMast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSTMast));
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.tcDC = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new iMANTRA.Grouper();
            this.label28 = new System.Windows.Forms.Label();
            this.dtpTaxDeactiveFrom = new iMANTRA.UserDT();
            this.chkTaxDeactivate = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new iMANTRA.Grouper();
            this.btnAcc = new iMANTRA.PopupButton();
            this.txtAcc_nm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtreceived = new System.Windows.Forms.TextBox();
            this.txtissued = new System.Windows.Forms.TextBox();
            this.txtPercentage = new System.Windows.Forms.TextBox();
            this.btnTaxValidIn = new iMANTRA.PopupButton();
            this.txtTaxValid = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnTaxNm = new iMANTRA.PopupButton();
            this.txtTaxNm = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbTaxType = new System.Windows.Forms.ComboBox();
            this.btnModuleNm = new iMANTRA.PopupButton();
            this.txtModuleNm = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.groupBox6 = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvTaxSearch = new iMANTRA.MyDataGridView();
            this.stax_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_tran_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTaxSearch = new System.Windows.Forms.TextBox();
            this.tcDC.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSearch)).BeginInit();
            this.SuspendLayout();
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
            this.ucToolBar1.Size = new System.Drawing.Size(762, 28);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // tcDC
            // 
            this.tcDC.Controls.Add(this.tabPage3);
            this.tcDC.Location = new System.Drawing.Point(1, 29);
            this.tcDC.Margin = new System.Windows.Forms.Padding(0);
            this.tcDC.Name = "tcDC";
            this.tcDC.Padding = new System.Drawing.Point(0, 0);
            this.tcDC.SelectedIndex = 0;
            this.tcDC.Size = new System.Drawing.Size(763, 415);
            this.tcDC.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(755, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tax Details";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox8.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox8.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox8.BorderColor = System.Drawing.Color.Black;
            this.groupBox8.BorderThickness = 1F;
            this.groupBox8.Controls.Add(this.label28);
            this.groupBox8.Controls.Add(this.dtpTaxDeactiveFrom);
            this.groupBox8.Controls.Add(this.chkTaxDeactivate);
            this.groupBox8.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox8.GroupImage = null;
            this.groupBox8.GroupTitle = "Validity Setting";
            this.groupBox8.Location = new System.Drawing.Point(297, 321);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.groupBox8.PaintGroupBox = true;
            this.groupBox8.RoundCorners = 1;
            this.groupBox8.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox8.ShadowControl = false;
            this.groupBox8.ShadowThickness = 1;
            this.groupBox8.Size = new System.Drawing.Size(453, 59);
            this.groupBox8.TabIndex = 21;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(155, 29);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(48, 18);
            this.label28.TabIndex = 2;
            this.label28.Text = "From";
            // 
            // dtpTaxDeactiveFrom
            // 
            this.dtpTaxDeactiveFrom.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtpTaxDeactiveFrom.CustomFormat = " ";
            this.dtpTaxDeactiveFrom.DtValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpTaxDeactiveFrom.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTaxDeactiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTaxDeactiveFrom.Location = new System.Drawing.Point(232, 25);
            this.dtpTaxDeactiveFrom.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.dtpTaxDeactiveFrom.Name = "dtpTaxDeactiveFrom";
            this.dtpTaxDeactiveFrom.Size = new System.Drawing.Size(175, 26);
            this.dtpTaxDeactiveFrom.TabIndex = 12;
            // 
            // chkTaxDeactivate
            // 
            this.chkTaxDeactivate.AutoSize = true;
            this.chkTaxDeactivate.Location = new System.Drawing.Point(9, 28);
            this.chkTaxDeactivate.Margin = new System.Windows.Forms.Padding(0);
            this.chkTaxDeactivate.Name = "chkTaxDeactivate";
            this.chkTaxDeactivate.Size = new System.Drawing.Size(127, 22);
            this.chkTaxDeactivate.TabIndex = 11;
            this.chkTaxDeactivate.Text = "De- Active";
            this.chkTaxDeactivate.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox7.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox7.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox7.BorderColor = System.Drawing.Color.Black;
            this.groupBox7.BorderThickness = 1F;
            this.groupBox7.Controls.Add(this.btnAcc);
            this.groupBox7.Controls.Add(this.txtAcc_nm);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.txtreceived);
            this.groupBox7.Controls.Add(this.txtissued);
            this.groupBox7.Controls.Add(this.txtPercentage);
            this.groupBox7.Controls.Add(this.btnTaxValidIn);
            this.groupBox7.Controls.Add(this.txtTaxValid);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.btnTaxNm);
            this.groupBox7.Controls.Add(this.txtTaxNm);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.cmbTaxType);
            this.groupBox7.Controls.Add(this.btnModuleNm);
            this.groupBox7.Controls.Add(this.txtModuleNm);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Controls.Add(this.label27);
            this.groupBox7.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox7.GroupImage = null;
            this.groupBox7.GroupTitle = "Basic Field Setting";
            this.groupBox7.Location = new System.Drawing.Point(297, 7);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.groupBox7.PaintGroupBox = true;
            this.groupBox7.RoundCorners = 1;
            this.groupBox7.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox7.ShadowControl = false;
            this.groupBox7.ShadowThickness = 1;
            this.groupBox7.Size = new System.Drawing.Size(453, 306);
            this.groupBox7.TabIndex = 3;
            // 
            // btnAcc
            // 
            this.btnAcc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAcc.BackgroundImage")));
            this.btnAcc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAcc.Dispddlfields = "";
            this.btnAcc.GradientBottom = System.Drawing.Color.Gray;
            this.btnAcc.GradientTop = System.Drawing.SystemColors.Control;
            this.btnAcc.IsQcd = false;
            this.btnAcc.Location = new System.Drawing.Point(413, 168);
            this.btnAcc.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.btnAcc.Name = "btnAcc";
            this.btnAcc.Primaryddl = "";
            this.btnAcc.QcdCondition = "";
            this.btnAcc.Query_con = "";
            this.btnAcc.Reftbltran_cd = "";
            this.btnAcc.Size = new System.Drawing.Size(34, 26);
            this.btnAcc.TabIndex = 38;
            this.btnAcc.Tbl_nm = "";
            this.btnAcc.UseVisualStyleBackColor = true;
            this.btnAcc.Click += new System.EventHandler(this.btnAcc_Click);
            // 
            // txtAcc_nm
            // 
            this.txtAcc_nm.Location = new System.Drawing.Point(158, 168);
            this.txtAcc_nm.Margin = new System.Windows.Forms.Padding(0);
            this.txtAcc_nm.Name = "txtAcc_nm";
            this.txtAcc_nm.Size = new System.Drawing.Size(249, 26);
            this.txtAcc_nm.TabIndex = 36;
            this.txtAcc_nm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAcc_nm_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 18);
            this.label1.TabIndex = 37;
            this.label1.Text = "Account Name";
            // 
            // txtreceived
            // 
            this.txtreceived.Location = new System.Drawing.Point(158, 238);
            this.txtreceived.Margin = new System.Windows.Forms.Padding(0);
            this.txtreceived.Name = "txtreceived";
            this.txtreceived.Size = new System.Drawing.Size(249, 26);
            this.txtreceived.TabIndex = 8;
            // 
            // txtissued
            // 
            this.txtissued.Location = new System.Drawing.Point(158, 203);
            this.txtissued.Margin = new System.Windows.Forms.Padding(0);
            this.txtissued.Name = "txtissued";
            this.txtissued.Size = new System.Drawing.Size(249, 26);
            this.txtissued.TabIndex = 7;
            // 
            // txtPercentage
            // 
            this.txtPercentage.Location = new System.Drawing.Point(158, 133);
            this.txtPercentage.Margin = new System.Windows.Forms.Padding(0);
            this.txtPercentage.Name = "txtPercentage";
            this.txtPercentage.Size = new System.Drawing.Size(249, 26);
            this.txtPercentage.TabIndex = 6;
            this.txtPercentage.Validating += new System.ComponentModel.CancelEventHandler(this.txtPercentage_Validating);
            // 
            // btnTaxValidIn
            // 
            this.btnTaxValidIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTaxValidIn.BackgroundImage")));
            this.btnTaxValidIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTaxValidIn.Dispddlfields = "";
            this.btnTaxValidIn.GradientBottom = System.Drawing.Color.Gray;
            this.btnTaxValidIn.GradientTop = System.Drawing.SystemColors.Control;
            this.btnTaxValidIn.IsQcd = false;
            this.btnTaxValidIn.Location = new System.Drawing.Point(412, 271);
            this.btnTaxValidIn.Margin = new System.Windows.Forms.Padding(0);
            this.btnTaxValidIn.Name = "btnTaxValidIn";
            this.btnTaxValidIn.Primaryddl = "";
            this.btnTaxValidIn.QcdCondition = "";
            this.btnTaxValidIn.Query_con = "";
            this.btnTaxValidIn.Reftbltran_cd = "";
            this.btnTaxValidIn.Size = new System.Drawing.Size(34, 27);
            this.btnTaxValidIn.TabIndex = 10;
            this.btnTaxValidIn.Tbl_nm = "";
            this.btnTaxValidIn.UseVisualStyleBackColor = true;
            this.btnTaxValidIn.Click += new System.EventHandler(this.btnTaxValidIn_Click);
            // 
            // txtTaxValid
            // 
            this.txtTaxValid.Location = new System.Drawing.Point(158, 273);
            this.txtTaxValid.Margin = new System.Windows.Forms.Padding(0);
            this.txtTaxValid.Name = "txtTaxValid";
            this.txtTaxValid.Size = new System.Drawing.Size(249, 26);
            this.txtTaxValid.TabIndex = 9;
            this.txtTaxValid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaxValid_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 278);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 18);
            this.label19.TabIndex = 35;
            this.label19.Text = "Valid In";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 242);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(148, 18);
            this.label20.TabIndex = 32;
            this.label20.Text = "To Be Received";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 206);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 18);
            this.label21.TabIndex = 31;
            this.label21.Text = "To Be Issued";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 134);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(108, 18);
            this.label22.TabIndex = 30;
            this.label22.Text = "Percentage";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(98, 105);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(12, 13);
            this.label23.TabIndex = 29;
            this.label23.Text = "*";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(83, 30);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(12, 13);
            this.label24.TabIndex = 28;
            this.label24.Text = "*";
            // 
            // btnTaxNm
            // 
            this.btnTaxNm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTaxNm.BackgroundImage")));
            this.btnTaxNm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTaxNm.Dispddlfields = "";
            this.btnTaxNm.GradientBottom = System.Drawing.Color.Gray;
            this.btnTaxNm.GradientTop = System.Drawing.SystemColors.Control;
            this.btnTaxNm.IsQcd = false;
            this.btnTaxNm.Location = new System.Drawing.Point(414, 61);
            this.btnTaxNm.Margin = new System.Windows.Forms.Padding(7, 4, 7, 4);
            this.btnTaxNm.Name = "btnTaxNm";
            this.btnTaxNm.Primaryddl = "";
            this.btnTaxNm.QcdCondition = "";
            this.btnTaxNm.Query_con = "";
            this.btnTaxNm.Reftbltran_cd = "";
            this.btnTaxNm.Size = new System.Drawing.Size(34, 26);
            this.btnTaxNm.TabIndex = 4;
            this.btnTaxNm.Tbl_nm = "";
            this.btnTaxNm.UseVisualStyleBackColor = true;
            this.btnTaxNm.Visible = false;
            this.btnTaxNm.Click += new System.EventHandler(this.btnTaxNm_Click);
            // 
            // txtTaxNm
            // 
            this.txtTaxNm.Location = new System.Drawing.Point(158, 63);
            this.txtTaxNm.Margin = new System.Windows.Forms.Padding(0);
            this.txtTaxNm.Name = "txtTaxNm";
            this.txtTaxNm.Size = new System.Drawing.Size(249, 26);
            this.txtTaxNm.TabIndex = 3;
            this.txtTaxNm.Validating += new System.ComponentModel.CancelEventHandler(this.txtTaxNm_Validating);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 62);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(88, 18);
            this.label25.TabIndex = 10;
            this.label25.Text = "Tax Name";
            // 
            // cmbTaxType
            // 
            this.cmbTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxType.FormattingEnabled = true;
            this.cmbTaxType.Location = new System.Drawing.Point(158, 98);
            this.cmbTaxType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbTaxType.Name = "cmbTaxType";
            this.cmbTaxType.Size = new System.Drawing.Size(249, 26);
            this.cmbTaxType.TabIndex = 5;
            this.cmbTaxType.Validating += new System.ComponentModel.CancelEventHandler(this.cmbTaxType_Validating);
            // 
            // btnModuleNm
            // 
            this.btnModuleNm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModuleNm.BackgroundImage")));
            this.btnModuleNm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnModuleNm.Dispddlfields = "";
            this.btnModuleNm.GradientBottom = System.Drawing.Color.Gray;
            this.btnModuleNm.GradientTop = System.Drawing.SystemColors.Control;
            this.btnModuleNm.IsQcd = false;
            this.btnModuleNm.Location = new System.Drawing.Point(414, 27);
            this.btnModuleNm.Margin = new System.Windows.Forms.Padding(0);
            this.btnModuleNm.Name = "btnModuleNm";
            this.btnModuleNm.Primaryddl = "";
            this.btnModuleNm.QcdCondition = "";
            this.btnModuleNm.Query_con = "";
            this.btnModuleNm.Reftbltran_cd = "";
            this.btnModuleNm.Size = new System.Drawing.Size(34, 23);
            this.btnModuleNm.TabIndex = 2;
            this.btnModuleNm.Tbl_nm = "";
            this.btnModuleNm.UseVisualStyleBackColor = true;
            this.btnModuleNm.Click += new System.EventHandler(this.btnModuleNm_Click);
            // 
            // txtModuleNm
            // 
            this.txtModuleNm.Location = new System.Drawing.Point(158, 28);
            this.txtModuleNm.Margin = new System.Windows.Forms.Padding(0);
            this.txtModuleNm.Name = "txtModuleNm";
            this.txtModuleNm.Size = new System.Drawing.Size(249, 26);
            this.txtModuleNm.TabIndex = 1;
            this.txtModuleNm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModuleNm_KeyDown);
            this.txtModuleNm.Validating += new System.ComponentModel.CancelEventHandler(this.txtModuleNm_Validating);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 98);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 18);
            this.label26.TabIndex = 5;
            this.label26.Text = "Tax Type";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 26);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(68, 18);
            this.label27.TabIndex = 4;
            this.label27.Text = "Module";
            // 
            // groupBox6
            // 
            this.groupBox6.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox6.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox6.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox6.BorderColor = System.Drawing.Color.Black;
            this.groupBox6.BorderThickness = 1F;
            this.groupBox6.Controls.Add(this.lblRowsCount);
            this.groupBox6.Controls.Add(this.dgvTaxSearch);
            this.groupBox6.Controls.Add(this.txtTaxSearch);
            this.groupBox6.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox6.GroupImage = null;
            this.groupBox6.GroupTitle = "Search";
            this.groupBox6.Location = new System.Drawing.Point(13, 8);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.PaintGroupBox = true;
            this.groupBox6.RoundCorners = 1;
            this.groupBox6.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox6.ShadowControl = false;
            this.groupBox6.ShadowThickness = 1;
            this.groupBox6.Size = new System.Drawing.Size(273, 372);
            this.groupBox6.TabIndex = 20;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(7, 351);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 4;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // dgvTaxSearch
            // 
            this.dgvTaxSearch.AllowUserToAddRows = false;
            this.dgvTaxSearch.AllowUserToDeleteRows = false;
            this.dgvTaxSearch.AllowUserToResizeRows = false;
            this.dgvTaxSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaxSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaxSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stax_id,
            this.tax_nm,
            this.tax_tran_nm});
            this.dgvTaxSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTaxSearch.height = 296;
            this.dgvTaxSearch.Location = new System.Drawing.Point(9, 55);
            this.dgvTaxSearch.Margin = new System.Windows.Forms.Padding(0);
            this.dgvTaxSearch.Name = "dgvTaxSearch";
            this.dgvTaxSearch.RowHeadersVisible = false;
            this.dgvTaxSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTaxSearch.Size = new System.Drawing.Size(257, 296);
            this.dgvTaxSearch.TabIndex = 2;
            this.dgvTaxSearch.width = 257;
            this.dgvTaxSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaxSearch_CellClick);
            this.dgvTaxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTaxSearch_KeyDown);
            // 
            // stax_id
            // 
            this.stax_id.DataPropertyName = "stax_id";
            this.stax_id.HeaderText = "custom_id2";
            this.stax_id.Name = "stax_id";
            this.stax_id.ReadOnly = true;
            this.stax_id.Visible = false;
            // 
            // tax_nm
            // 
            this.tax_nm.DataPropertyName = "tax_nm";
            this.tax_nm.HeaderText = "Tax Name";
            this.tax_nm.Name = "tax_nm";
            this.tax_nm.ReadOnly = true;
            this.tax_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tax_tran_nm
            // 
            this.tax_tran_nm.DataPropertyName = "tax_tran_nm";
            this.tax_tran_nm.HeaderText = "Transaction Name";
            this.tax_tran_nm.Name = "tax_tran_nm";
            this.tax_tran_nm.ReadOnly = true;
            this.tax_tran_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtTaxSearch
            // 
            this.txtTaxSearch.Location = new System.Drawing.Point(9, 25);
            this.txtTaxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxSearch.Name = "txtTaxSearch";
            this.txtTaxSearch.Size = new System.Drawing.Size(257, 26);
            this.txtTaxSearch.TabIndex = 0;
            this.txtTaxSearch.TextChanged += new System.EventHandler(this.txtTaxSearch_TextChanged);
            this.txtTaxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaxSearch_KeyDown);
            this.txtTaxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxSearch_KeyPress);
            // 
            // frmSTMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(762, 445);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.tcDC);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSTMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSTMast_FormClosed);
            this.Load += new System.EventHandler(this.frmSTMast_Load);
            this.Enter += new System.EventHandler(this.frmSTMast_Enter);
            this.Resize += new System.EventHandler(this.frmSTMast_Resize);
            this.tcDC.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcDC;
        private System.Windows.Forms.TabPage tabPage3;
        private Grouper groupBox8;
        private System.Windows.Forms.Label label28;
        private UserDT dtpTaxDeactiveFrom;
        private System.Windows.Forms.CheckBox chkTaxDeactivate;
        private Grouper groupBox7;
        private System.Windows.Forms.TextBox txtreceived;
        private System.Windows.Forms.TextBox txtissued;
        private System.Windows.Forms.TextBox txtPercentage;
        private PopupButton btnTaxValidIn;
        private System.Windows.Forms.TextBox txtTaxValid;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private PopupButton btnTaxNm;
        private System.Windows.Forms.TextBox txtTaxNm;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbTaxType;
        private PopupButton btnModuleNm;
        private System.Windows.Forms.TextBox txtModuleNm;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private Grouper groupBox6;
        private MyDataGridView dgvTaxSearch;
        private System.Windows.Forms.TextBox txtTaxSearch;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn stax_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_tran_nm;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.TextBox txtAcc_nm;
        private System.Windows.Forms.Label label1;
        private PopupButton btnAcc;

    }
}