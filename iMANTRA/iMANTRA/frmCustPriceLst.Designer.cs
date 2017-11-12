namespace iMANTRA
{
    partial class frmCustPriceLst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustPriceLst));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.btnAccountNm = new iMANTRA.PopupButton();
            this.txtac_nm = new System.Windows.Forms.TextBox();
            this.txtDisp_nm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper1 = new iMANTRA.Grouper();
            this.lblF2 = new System.Windows.Forms.Label();
            this.dgvCustPriceLst = new iMANTRA.MyDataGridView();
            this.prod_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OTHER_DET1 = new iMANTRA.POPUPBUTTON_FOR_GRID();
            this.btnAdd = new iMANTRA.PopupButton();
            this.btnRemove = new iMANTRA.PopupButton();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.cplId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ac_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.popuptextboX_FOR_GRID1 = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grouper2.SuspendLayout();
            this.grouper1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustPriceLst)).BeginInit();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(235, 25);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 463);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grouper2);
            this.tabPage1.Controls.Add(this.grouper1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(682, 432);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Customer Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.OTHER_DET);
            this.grouper2.Controls.Add(this.btnAccountNm);
            this.grouper2.Controls.Add(this.txtac_nm);
            this.grouper2.Controls.Add(this.txtDisp_nm);
            this.grouper2.Controls.Add(this.label2);
            this.grouper2.Controls.Add(this.label1);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Account Details";
            this.grouper2.Location = new System.Drawing.Point(4, 3);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(673, 126);
            this.grouper2.TabIndex = 3;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.Location = new System.Drawing.Point(170, 95);
            this.OTHER_DET.Margin = new System.Windows.Forms.Padding(4);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(178, 26);
            this.OTHER_DET.TabIndex = 7;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "OTHER DETAILS";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // btnAccountNm
            // 
            this.btnAccountNm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccountNm.BackgroundImage")));
            this.btnAccountNm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAccountNm.Dispddlfields = "";
            this.btnAccountNm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAccountNm.Location = new System.Drawing.Point(579, 26);
            this.btnAccountNm.Margin = new System.Windows.Forms.Padding(0);
            this.btnAccountNm.Name = "btnAccountNm";
            this.btnAccountNm.Primaryddl = "";
            this.btnAccountNm.Query_con = "";
            this.btnAccountNm.Reftbltran_cd = "";
            this.btnAccountNm.Size = new System.Drawing.Size(29, 29);
            this.btnAccountNm.TabIndex = 5;
            this.btnAccountNm.Tbl_nm = "";
            this.btnAccountNm.UseVisualStyleBackColor = true;
            this.btnAccountNm.Click += new System.EventHandler(this.btnAccountNm_Click);
            // 
            // txtac_nm
            // 
            this.txtac_nm.Location = new System.Drawing.Point(170, 29);
            this.txtac_nm.Margin = new System.Windows.Forms.Padding(4);
            this.txtac_nm.Name = "txtac_nm";
            this.txtac_nm.Size = new System.Drawing.Size(403, 26);
            this.txtac_nm.TabIndex = 4;
            this.txtac_nm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txtac_nm.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // txtDisp_nm
            // 
            this.txtDisp_nm.Location = new System.Drawing.Point(170, 64);
            this.txtDisp_nm.Margin = new System.Windows.Forms.Padding(4);
            this.txtDisp_nm.Name = "txtDisp_nm";
            this.txtDisp_nm.Size = new System.Drawing.Size(403, 26);
            this.txtDisp_nm.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Display Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Name";
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lblF2);
            this.grouper1.Controls.Add(this.dgvCustPriceLst);
            this.grouper1.Controls.Add(this.btnAdd);
            this.grouper1.Controls.Add(this.btnRemove);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Price List";
            this.grouper1.Location = new System.Drawing.Point(4, 132);
            this.grouper1.Margin = new System.Windows.Forms.Padding(0);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(675, 296);
            this.grouper1.TabIndex = 8;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblF2.Location = new System.Drawing.Point(432, 30);
            this.lblF2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(162, 16);
            this.lblF2.TabIndex = 8;
            this.lblF2.Text = "Press F2 - To Get List";
            this.lblF2.Visible = false;
            // 
            // dgvCustPriceLst
            // 
            this.dgvCustPriceLst.AllowUserToAddRows = false;
            this.dgvCustPriceLst.AllowUserToDeleteRows = false;
            this.dgvCustPriceLst.AllowUserToResizeColumns = false;
            this.dgvCustPriceLst.AllowUserToResizeRows = false;
            this.dgvCustPriceLst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustPriceLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustPriceLst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prod_cd,
            this.prod_no,
            this.ptserial,
            this.prod_nm,
            this.prod_desc,
            this.rate,
            this.OTHER_DET1});
            this.dgvCustPriceLst.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCustPriceLst.height = 229;
            this.dgvCustPriceLst.Location = new System.Drawing.Point(6, 60);
            this.dgvCustPriceLst.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCustPriceLst.Name = "dgvCustPriceLst";
            this.dgvCustPriceLst.RowHeadersVisible = false;
            this.dgvCustPriceLst.Size = new System.Drawing.Size(662, 229);
            this.dgvCustPriceLst.TabIndex = 11;
            this.dgvCustPriceLst.width = 662;
            this.dgvCustPriceLst.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustPriceLst_CellClick);
            this.dgvCustPriceLst.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustPriceLst_CellContentClick);
            this.dgvCustPriceLst.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustPriceLst_CellEnter);
            this.dgvCustPriceLst.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustPriceLst_CellLeave);
            this.dgvCustPriceLst.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCustPriceLst_CellValidating);
            this.dgvCustPriceLst.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCustPriceLst_EditingControlShowing);
            this.dgvCustPriceLst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvCustPriceLst_KeyPress);
            // 
            // prod_cd
            // 
            this.prod_cd.DataPropertyName = "prod_cd";
            this.prod_cd.HeaderText = "prod_cd";
            this.prod_cd.Name = "prod_cd";
            this.prod_cd.ReadOnly = true;
            this.prod_cd.Visible = false;
            // 
            // prod_no
            // 
            this.prod_no.HeaderText = "prod_no";
            this.prod_no.Name = "prod_no";
            this.prod_no.ReadOnly = true;
            this.prod_no.Visible = false;
            // 
            // ptserial
            // 
            this.ptserial.HeaderText = "ptserial";
            this.ptserial.Name = "ptserial";
            this.ptserial.ReadOnly = true;
            this.ptserial.Visible = false;
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            this.prod_nm.Dispddlfields = "";
            this.prod_nm.HeaderText = "Product Name";
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.Primaryddl = "";
            this.prod_nm.Query_con = "";
            this.prod_nm.Reftbltran_cd = "";
            this.prod_nm.Tbl_nm = "";
            // 
            // prod_desc
            // 
            this.prod_desc.DataPropertyName = "prod_desc";
            this.prod_desc.HeaderText = "Description";
            this.prod_desc.Name = "prod_desc";
            // 
            // rate
            // 
            this.rate.DataPropertyName = "rate";
            this.rate.HeaderText = "Rate";
            this.rate.Name = "rate";
            // 
            // OTHER_DET1
            // 
            this.OTHER_DET1.Dispddlfields = "";
            this.OTHER_DET1.HeaderText = "Other Details";
            this.OTHER_DET1.Name = "OTHER_DET1";
            this.OTHER_DET1.Primaryddl = "";
            this.OTHER_DET1.Query_con = "";
            this.OTHER_DET1.Reftbltran_cd = "";
            this.OTHER_DET1.Tbl_nm = "";
            this.OTHER_DET1.Text = "Other Details";
            this.OTHER_DET1.UseColumnTextForButtonValue = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Dispddlfields = "";
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(11, 28);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primaryddl = "";
            this.btnAdd.Query_con = "";
            this.btnAdd.Reftbltran_cd = "";
            this.btnAdd.Size = new System.Drawing.Size(76, 26);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Tbl_nm = "";
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Dispddlfields = "";
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(92, 28);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Primaryddl = "";
            this.btnRemove.Query_con = "";
            this.btnRemove.Reftbltran_cd = "";
            this.btnRemove.Size = new System.Drawing.Size(79, 27);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Tbl_nm = "";
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
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
            this.grpbxSearch.GroupTitle = "Account List";
            this.grpbxSearch.Location = new System.Drawing.Point(0, 25);
            this.grpbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(232, 463);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(7, 436);
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
            this.cplId,
            this.ac_nm});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 357;
            this.dgvSearch.Location = new System.Drawing.Point(10, 70);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(214, 357);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 214;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // cplId
            // 
            this.cplId.DataPropertyName = "cplId";
            this.cplId.HeaderText = "Price Id";
            this.cplId.Name = "cplId";
            this.cplId.Visible = false;
            // 
            // ac_nm
            // 
            this.ac_nm.DataPropertyName = "ac_nm";
            this.ac_nm.HeaderText = "Account Name";
            this.ac_nm.Name = "ac_nm";
            this.ac_nm.ReadOnly = true;
            this.ac_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(10, 36);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(214, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
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
            this.ucToolBar1.Size = new System.Drawing.Size(925, 22);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // popuptextboX_FOR_GRID1
            // 
            this.popuptextboX_FOR_GRID1.DataPropertyName = "prod_nm";
            this.popuptextboX_FOR_GRID1.Dispddlfields = "";
            this.popuptextboX_FOR_GRID1.HeaderText = "Product Name";
            this.popuptextboX_FOR_GRID1.Name = "popuptextboX_FOR_GRID1";
            this.popuptextboX_FOR_GRID1.Primaryddl = "";
            this.popuptextboX_FOR_GRID1.Query_con = "";
            this.popuptextboX_FOR_GRID1.Reftbltran_cd = "";
            this.popuptextboX_FOR_GRID1.Tbl_nm = "";
            this.popuptextboX_FOR_GRID1.Width = 187;
            // 
            // frmCustPriceLst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(925, 488);
            this.ControlBox = false;
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCustPriceLst";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCustPriceLst_FormClosed);
            this.Load += new System.EventHandler(this.frmCustPriceLst_Load);
            this.Enter += new System.EventHandler(this.frmCustPriceLst_Enter);
            this.Resize += new System.EventHandler(this.frmCustPriceLst_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustPriceLst)).EndInit();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MyDataGridView dgvCustPriceLst;
        private PopupButton btnRemove;
        private PopupButton btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private POPUPTEXTBOX_FOR_GRID popuptextboX_FOR_GRID1;
        private System.Windows.Forms.TextBox txtDisp_nm;
        private System.Windows.Forms.TextBox txtac_nm;
        private Grouper grouper1;
        private Grouper grouper2;
        private UCToolBar ucToolBar1;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private PopupButton btnAccountNm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cplId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ac_nm;
        private System.Windows.Forms.Label lblF2;
        private PopupButton OTHER_DET;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptserial;
        private POPUPTEXTBOX_FOR_GRID prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate;
        private POPUPBUTTON_FOR_GRID OTHER_DET1;
        private System.Windows.Forms.Label lblRowsCount;
    }
}