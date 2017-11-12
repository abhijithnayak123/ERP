namespace iMANTRA
{
    partial class frmProductMast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductMast));
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkProd_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.prod_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pt_grp_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grouper4 = new iMANTRA.Grouper();
            this.cmbPurUOM = new System.Windows.Forms.ComboBox();
            this.cmbSecUOM = new System.Windows.Forms.ComboBox();
            this.chkInStk = new System.Windows.Forms.CheckBox();
            this.cmdUOM = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtReorderLvl = new System.Windows.Forms.TextBox();
            this.txtCurrentCost = new System.Windows.Forms.TextBox();
            this.txtSellingRate = new System.Windows.Forms.TextBox();
            this.cmbStockable = new System.Windows.Forms.ComboBox();
            this.txtConvRatio = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grouper1 = new iMANTRA.Grouper();
            this.btnProd_Gr = new iMANTRA.PopupButton();
            this.cmbprod_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtprod_desc = new System.Windows.Forms.TextBox();
            this.txtprod_gr = new System.Windows.Forms.TextBox();
            this.txtprod_nm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.grouper2.Location = new System.Drawing.Point(694, 443);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(193, 61);
            this.grouper2.TabIndex = 22;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OTHER_DET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.IsQcd = false;
            this.OTHER_DET.Location = new System.Drawing.Point(13, 27);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.QcdCondition = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(169, 27);
            this.OTHER_DET.TabIndex = 23;
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
            this.groupBox5.Location = new System.Drawing.Point(342, 442);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(343, 61);
            this.groupBox5.TabIndex = 19;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(195, 29);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(136, 26);
            this.dtp_deactive_from.TabIndex = 21;
            // 
            // chkProd_active
            // 
            this.chkProd_active.AutoSize = true;
            this.chkProd_active.Location = new System.Drawing.Point(5, 31);
            this.chkProd_active.Name = "chkProd_active";
            this.chkProd_active.Size = new System.Drawing.Size(137, 22);
            this.chkProd_active.TabIndex = 20;
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
            this.grpbxSearch.GroupTitle = "Product List";
            this.grpbxSearch.Location = new System.Drawing.Point(4, 28);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(333, 474);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(8, 449);
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
            this.prod_cd,
            this.prod_nm,
            this.pt_grp_nm,
            this.prod_desc});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 379;
            this.dgvSearch.Location = new System.Drawing.Point(6, 61);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(320, 379);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 320;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // prod_cd
            // 
            this.prod_cd.DataPropertyName = "prod_cd";
            this.prod_cd.HeaderText = "prod_cd";
            this.prod_cd.Name = "prod_cd";
            this.prod_cd.Visible = false;
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            this.prod_nm.HeaderText = "Code";
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.ReadOnly = true;
            this.prod_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pt_grp_nm
            // 
            this.pt_grp_nm.DataPropertyName = "pt_grp_nm";
            this.pt_grp_nm.HeaderText = "Group";
            this.pt_grp_nm.Name = "pt_grp_nm";
            this.pt_grp_nm.ReadOnly = true;
            // 
            // prod_desc
            // 
            this.prod_desc.DataPropertyName = "prod_desc";
            this.prod_desc.HeaderText = "Description";
            this.prod_desc.Name = "prod_desc";
            this.prod_desc.ReadOnly = true;
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
            this.grouper4.Controls.Add(this.label5);
            this.grouper4.Controls.Add(this.cmbPurUOM);
            this.grouper4.Controls.Add(this.cmbSecUOM);
            this.grouper4.Controls.Add(this.chkInStk);
            this.grouper4.Controls.Add(this.cmdUOM);
            this.grouper4.Controls.Add(this.label13);
            this.grouper4.Controls.Add(this.label20);
            this.grouper4.Controls.Add(this.txtReorderLvl);
            this.grouper4.Controls.Add(this.txtCurrentCost);
            this.grouper4.Controls.Add(this.txtSellingRate);
            this.grouper4.Controls.Add(this.cmbStockable);
            this.grouper4.Controls.Add(this.txtConvRatio);
            this.grouper4.Controls.Add(this.label21);
            this.grouper4.Controls.Add(this.label22);
            this.grouper4.Controls.Add(this.label23);
            this.grouper4.Controls.Add(this.label24);
            this.grouper4.Controls.Add(this.label26);
            this.grouper4.Controls.Add(this.label27);
            this.grouper4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper4.GroupImage = null;
            this.grouper4.GroupTitle = "Inventory";
            this.grouper4.Location = new System.Drawing.Point(342, 236);
            this.grouper4.Name = "grouper4";
            this.grouper4.Padding = new System.Windows.Forms.Padding(20);
            this.grouper4.PaintGroupBox = true;
            this.grouper4.RoundCorners = 1;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 1;
            this.grouper4.Size = new System.Drawing.Size(546, 204);
            this.grouper4.TabIndex = 9;
            // 
            // cmbPurUOM
            // 
            this.cmbPurUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurUOM.FormattingEnabled = true;
            this.cmbPurUOM.Location = new System.Drawing.Point(153, 106);
            this.cmbPurUOM.Name = "cmbPurUOM";
            this.cmbPurUOM.Size = new System.Drawing.Size(129, 26);
            this.cmbPurUOM.TabIndex = 26;
            this.cmbPurUOM.Validating += new System.ComponentModel.CancelEventHandler(this.cmbPurUOM_Validating_1);
            // 
            // cmbSecUOM
            // 
            this.cmbSecUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecUOM.FormattingEnabled = true;
            this.cmbSecUOM.Location = new System.Drawing.Point(153, 70);
            this.cmbSecUOM.Name = "cmbSecUOM";
            this.cmbSecUOM.Size = new System.Drawing.Size(129, 26);
            this.cmbSecUOM.TabIndex = 25;
            this.cmbSecUOM.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSecUOM_Validating_1);
            // 
            // chkInStk
            // 
            this.chkInStk.AutoSize = true;
            this.chkInStk.Location = new System.Drawing.Point(8, 175);
            this.chkInStk.Name = "chkInStk";
            this.chkInStk.Size = new System.Drawing.Size(187, 22);
            this.chkInStk.TabIndex = 18;
            this.chkInStk.Text = "Include In Stock";
            this.chkInStk.UseVisualStyleBackColor = true;
            // 
            // cmdUOM
            // 
            this.cmdUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdUOM.FormattingEnabled = true;
            this.cmdUOM.Location = new System.Drawing.Point(153, 34);
            this.cmdUOM.Name = "cmdUOM";
            this.cmdUOM.Size = new System.Drawing.Size(129, 26);
            this.cmdUOM.TabIndex = 10;
            this.cmdUOM.Validating += new System.ComponentModel.CancelEventHandler(this.cmdUOM_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 18);
            this.label13.TabIndex = 24;
            this.label13.Text = "Purchase UOM";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(286, 143);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(138, 18);
            this.label20.TabIndex = 23;
            this.label20.Text = "Reorder Level";
            // 
            // txtReorderLvl
            // 
            this.txtReorderLvl.Location = new System.Drawing.Point(451, 138);
            this.txtReorderLvl.Name = "txtReorderLvl";
            this.txtReorderLvl.Size = new System.Drawing.Size(92, 26);
            this.txtReorderLvl.TabIndex = 17;
            this.txtReorderLvl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReorderLvl_KeyPress);
            // 
            // txtCurrentCost
            // 
            this.txtCurrentCost.Location = new System.Drawing.Point(451, 104);
            this.txtCurrentCost.Name = "txtCurrentCost";
            this.txtCurrentCost.Size = new System.Drawing.Size(92, 26);
            this.txtCurrentCost.TabIndex = 15;
            this.txtCurrentCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCurrentCost_KeyPress);
            // 
            // txtSellingRate
            // 
            this.txtSellingRate.Location = new System.Drawing.Point(451, 70);
            this.txtSellingRate.Name = "txtSellingRate";
            this.txtSellingRate.Size = new System.Drawing.Size(92, 26);
            this.txtSellingRate.TabIndex = 13;
            this.txtSellingRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSellingRate_KeyPress);
            // 
            // cmbStockable
            // 
            this.cmbStockable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockable.FormattingEnabled = true;
            this.cmbStockable.Location = new System.Drawing.Point(153, 141);
            this.cmbStockable.Name = "cmbStockable";
            this.cmbStockable.Size = new System.Drawing.Size(129, 26);
            this.cmbStockable.TabIndex = 16;
            this.cmbStockable.Validating += new System.ComponentModel.CancelEventHandler(this.cmbStockable_Validating);
            // 
            // txtConvRatio
            // 
            this.txtConvRatio.Location = new System.Drawing.Point(451, 38);
            this.txtConvRatio.Name = "txtConvRatio";
            this.txtConvRatio.Size = new System.Drawing.Size(92, 26);
            this.txtConvRatio.TabIndex = 11;
            this.txtConvRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConvRatio_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(286, 108);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(128, 18);
            this.label21.TabIndex = 13;
            this.label21.Text = "Current Cost";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(286, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(128, 18);
            this.label22.TabIndex = 12;
            this.label22.Text = "Selling Rate";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(5, 73);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(138, 18);
            this.label23.TabIndex = 11;
            this.label23.Text = "Secondary UOM";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 142);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 18);
            this.label24.TabIndex = 10;
            this.label24.Text = "Stockable";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(282, 39);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(168, 18);
            this.label26.TabIndex = 8;
            this.label26.Text = "Conversion Ratio";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(5, 35);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(38, 18);
            this.label27.TabIndex = 7;
            this.label27.Text = "UOM";
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
            this.ucToolBar1.Size = new System.Drawing.Size(894, 24);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.label18);
            this.grouper1.Controls.Add(this.btnProd_Gr);
            this.grouper1.Controls.Add(this.cmbprod_type);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.txtprod_desc);
            this.grouper1.Controls.Add(this.txtprod_gr);
            this.grouper1.Controls.Add(this.txtprod_nm);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Product Details";
            this.grouper1.Location = new System.Drawing.Point(342, 27);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(546, 207);
            this.grouper1.TabIndex = 3;
            // 
            // btnProd_Gr
            // 
            this.btnProd_Gr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProd_Gr.BackgroundImage")));
            this.btnProd_Gr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProd_Gr.Dispddlfields = "";
            this.btnProd_Gr.GradientBottom = System.Drawing.Color.Gray;
            this.btnProd_Gr.GradientTop = System.Drawing.SystemColors.Control;
            this.btnProd_Gr.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProd_Gr.IsQcd = false;
            this.btnProd_Gr.Location = new System.Drawing.Point(502, 70);
            this.btnProd_Gr.Margin = new System.Windows.Forms.Padding(0);
            this.btnProd_Gr.Name = "btnProd_Gr";
            this.btnProd_Gr.Primaryddl = "";
            this.btnProd_Gr.QcdCondition = "";
            this.btnProd_Gr.Query_con = "";
            this.btnProd_Gr.Reftbltran_cd = "";
            this.btnProd_Gr.Size = new System.Drawing.Size(32, 24);
            this.btnProd_Gr.TabIndex = 6;
            this.btnProd_Gr.Tbl_nm = "";
            this.btnProd_Gr.UseVisualStyleBackColor = true;
            this.btnProd_Gr.Click += new System.EventHandler(this.btnProd_Gr_Click);
            // 
            // cmbprod_type
            // 
            this.cmbprod_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbprod_type.FormattingEnabled = true;
            this.cmbprod_type.Location = new System.Drawing.Point(152, 171);
            this.cmbprod_type.Name = "cmbprod_type";
            this.cmbprod_type.Size = new System.Drawing.Size(295, 26);
            this.cmbprod_type.TabIndex = 8;
            this.cmbprod_type.Validating += new System.ComponentModel.CancelEventHandler(this.cmbprod_type_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Product Type";
            // 
            // txtprod_desc
            // 
            this.txtprod_desc.Location = new System.Drawing.Point(152, 102);
            this.txtprod_desc.Multiline = true;
            this.txtprod_desc.Name = "txtprod_desc";
            this.txtprod_desc.Size = new System.Drawing.Size(339, 60);
            this.txtprod_desc.TabIndex = 7;
            // 
            // txtprod_gr
            // 
            this.txtprod_gr.Location = new System.Drawing.Point(152, 70);
            this.txtprod_gr.Name = "txtprod_gr";
            this.txtprod_gr.Size = new System.Drawing.Size(339, 26);
            this.txtprod_gr.TabIndex = 5;
            this.txtprod_gr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtprod_gr_KeyDown);
            this.txtprod_gr.Validating += new System.ComponentModel.CancelEventHandler(this.txtprod_gr_Validating);
            // 
            // txtprod_nm
            // 
            this.txtprod_nm.Location = new System.Drawing.Point(152, 35);
            this.txtprod_nm.Name = "txtprod_nm";
            this.txtprod_nm.Size = new System.Drawing.Size(339, 26);
            this.txtprod_nm.TabIndex = 4;
            this.txtprod_nm.Validating += new System.ComponentModel.CancelEventHandler(this.txtprod_nm_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 50);
            this.label3.TabIndex = 2;
            this.label3.Text = "Product Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Code";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(136, 42);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 16);
            this.label18.TabIndex = 130;
            this.label18.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(136, 177);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 131;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(136, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 132;
            this.label5.Text = "*";
            // 
            // frmProductMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(894, 508);
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
            this.Name = "frmProductMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductMast_FormClosed);
            this.Load += new System.EventHandler(this.frmProductMast_Load);
            this.Enter += new System.EventHandler(this.frmProductMast_Enter);
            this.Resize += new System.EventHandler(this.frmProductMast_Resize);
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
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtprod_desc;
        private System.Windows.Forms.TextBox txtprod_gr;
        private System.Windows.Forms.TextBox txtprod_nm;
        private System.Windows.Forms.ComboBox cmbprod_type;
        private System.Windows.Forms.Label label6;
        private Grouper grouper4;
        private System.Windows.Forms.CheckBox chkInStk;
        private System.Windows.Forms.ComboBox cmdUOM;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtReorderLvl;
        private System.Windows.Forms.TextBox txtCurrentCost;
        private System.Windows.Forms.TextBox txtSellingRate;
        private System.Windows.Forms.ComboBox cmbStockable;
        private System.Windows.Forms.TextBox txtConvRatio;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper grouper2;
        private PopupButton OTHER_DET;
        private Grouper groupBox5;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.CheckBox chkProd_active;
        private System.Windows.Forms.Label label14;
        private PopupButton btnProd_Gr;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn pt_grp_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc;
        private System.Windows.Forms.ComboBox cmbPurUOM;
        private System.Windows.Forms.ComboBox cmbSecUOM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label18;
    }
}