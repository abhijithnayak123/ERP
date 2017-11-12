namespace iMANTRA
{
    partial class frmProcessMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcessMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new iMANTRA.Grouper();
            this.dgvProcess = new iMANTRA.MyDataGridView();
            this.pro_order = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_nm = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_desc = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_setup = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_cycle = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.mac_type = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.mac_type_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opt_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opt_type = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.pro_cost = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.OTHER_DET1 = new iMANTRA.POPUPBUTTON_FOR_GRID();
            this.lblF2 = new System.Windows.Forms.Label();
            this.btnRemove = new iMANTRA.PopupButton();
            this.btnAdd = new iMANTRA.PopupButton();
            this.groupBox1 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.btnProdNm = new iMANTRA.PopupButton();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.pro_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcess)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.BorderThickness = 1F;
            this.groupBox2.Controls.Add(this.dgvProcess);
            this.groupBox2.Controls.Add(this.lblF2);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "Process Details";
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.PaintGroupBox = true;
            this.groupBox2.RoundCorners = 1;
            this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 1;
            // 
            // dgvProcess
            // 
            this.dgvProcess.AllowUserToAddRows = false;
            this.dgvProcess.AllowUserToDeleteRows = false;
            this.dgvProcess.AllowUserToResizeColumns = false;
            this.dgvProcess.AllowUserToResizeRows = false;
            this.dgvProcess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pro_order,
            this.pro_order_id,
            this.ptserial,
            this.pro_nm,
            this.pro_desc,
            this.pro_setup,
            this.pro_cycle,
            this.mac_type,
            this.mac_type_id,
            this.opt_id,
            this.opt_type,
            this.pro_cost,
            this.OTHER_DET1});
            this.dgvProcess.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvProcess.height = 304;
            resources.ApplyResources(this.dgvProcess, "dgvProcess");
            this.dgvProcess.Name = "dgvProcess";
            this.dgvProcess.width = 663;
            this.dgvProcess.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellClick);
            this.dgvProcess.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellContentClick);
            this.dgvProcess.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellEnter);
            this.dgvProcess.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellLeave);
            this.dgvProcess.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvProcess_CellValidating);
            this.dgvProcess.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvProcess_EditingControlShowing);
            this.dgvProcess.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProcess_RowPostPaint);
            this.dgvProcess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvProcess_KeyPress);
            // 
            // pro_order
            // 
            this.pro_order.DataPropertyName = "pro_order";
            this.pro_order.Dispddlfields = "";
            resources.ApplyResources(this.pro_order, "pro_order");
            this.pro_order.IsQcd = false;
            this.pro_order.Name = "pro_order";
            this.pro_order.Primaryddl = "";
            this.pro_order.QcdCondition = "";
            this.pro_order.Query_con = "";
            this.pro_order.Reftbltran_cd = "";
            this.pro_order.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_order.Tbl_nm = "";
            // 
            // pro_order_id
            // 
            this.pro_order_id.DataPropertyName = "pro_order_id";
            resources.ApplyResources(this.pro_order_id, "pro_order_id");
            this.pro_order_id.Name = "pro_order_id";
            this.pro_order_id.ReadOnly = true;
            // 
            // ptserial
            // 
            this.ptserial.DataPropertyName = "ptserial";
            this.ptserial.Dispddlfields = "";
            resources.ApplyResources(this.ptserial, "ptserial");
            this.ptserial.IsQcd = false;
            this.ptserial.Name = "ptserial";
            this.ptserial.Primaryddl = "";
            this.ptserial.QcdCondition = "";
            this.ptserial.Query_con = "";
            this.ptserial.ReadOnly = true;
            this.ptserial.Reftbltran_cd = "";
            this.ptserial.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ptserial.Tbl_nm = "";
            // 
            // pro_nm
            // 
            this.pro_nm.DataPropertyName = "pro_nm";
            this.pro_nm.Dispddlfields = "";
            resources.ApplyResources(this.pro_nm, "pro_nm");
            this.pro_nm.IsQcd = false;
            this.pro_nm.Name = "pro_nm";
            this.pro_nm.Primaryddl = "";
            this.pro_nm.QcdCondition = "";
            this.pro_nm.Query_con = "";
            this.pro_nm.Reftbltran_cd = "";
            this.pro_nm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_nm.Tbl_nm = "";
            // 
            // pro_desc
            // 
            this.pro_desc.DataPropertyName = "pro_desc";
            this.pro_desc.Dispddlfields = "";
            resources.ApplyResources(this.pro_desc, "pro_desc");
            this.pro_desc.IsQcd = false;
            this.pro_desc.Name = "pro_desc";
            this.pro_desc.Primaryddl = "";
            this.pro_desc.QcdCondition = "";
            this.pro_desc.Query_con = "";
            this.pro_desc.Reftbltran_cd = "";
            this.pro_desc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_desc.Tbl_nm = "";
            // 
            // pro_setup
            // 
            this.pro_setup.DataPropertyName = "pro_setup";
            dataGridViewCellStyle3.NullValue = null;
            this.pro_setup.DefaultCellStyle = dataGridViewCellStyle3;
            this.pro_setup.Dispddlfields = "";
            resources.ApplyResources(this.pro_setup, "pro_setup");
            this.pro_setup.IsQcd = false;
            this.pro_setup.Name = "pro_setup";
            this.pro_setup.Primaryddl = "";
            this.pro_setup.QcdCondition = "";
            this.pro_setup.Query_con = "";
            this.pro_setup.Reftbltran_cd = "";
            this.pro_setup.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_setup.Tbl_nm = "";
            // 
            // pro_cycle
            // 
            this.pro_cycle.DataPropertyName = "pro_cycle";
            this.pro_cycle.Dispddlfields = "";
            resources.ApplyResources(this.pro_cycle, "pro_cycle");
            this.pro_cycle.IsQcd = false;
            this.pro_cycle.Name = "pro_cycle";
            this.pro_cycle.Primaryddl = "";
            this.pro_cycle.QcdCondition = "";
            this.pro_cycle.Query_con = "";
            this.pro_cycle.Reftbltran_cd = "";
            this.pro_cycle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_cycle.Tbl_nm = "";
            // 
            // mac_type
            // 
            this.mac_type.DataPropertyName = "mac_type";
            this.mac_type.Dispddlfields = "";
            resources.ApplyResources(this.mac_type, "mac_type");
            this.mac_type.IsQcd = false;
            this.mac_type.Name = "mac_type";
            this.mac_type.Primaryddl = "";
            this.mac_type.QcdCondition = "";
            this.mac_type.Query_con = "";
            this.mac_type.Reftbltran_cd = "";
            this.mac_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mac_type.Tbl_nm = "";
            // 
            // mac_type_id
            // 
            this.mac_type_id.DataPropertyName = "mac_type_id";
            resources.ApplyResources(this.mac_type_id, "mac_type_id");
            this.mac_type_id.Name = "mac_type_id";
            this.mac_type_id.ReadOnly = true;
            // 
            // opt_id
            // 
            this.opt_id.DataPropertyName = "opt_id";
            resources.ApplyResources(this.opt_id, "opt_id");
            this.opt_id.Name = "opt_id";
            this.opt_id.ReadOnly = true;
            // 
            // opt_type
            // 
            this.opt_type.DataPropertyName = "opt_type";
            this.opt_type.Dispddlfields = "";
            resources.ApplyResources(this.opt_type, "opt_type");
            this.opt_type.IsQcd = false;
            this.opt_type.Name = "opt_type";
            this.opt_type.Primaryddl = "";
            this.opt_type.QcdCondition = "";
            this.opt_type.Query_con = "";
            this.opt_type.Reftbltran_cd = "";
            this.opt_type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.opt_type.Tbl_nm = "";
            // 
            // pro_cost
            // 
            this.pro_cost.DataPropertyName = "pro_cost";
            this.pro_cost.Dispddlfields = "";
            resources.ApplyResources(this.pro_cost, "pro_cost");
            this.pro_cost.IsQcd = false;
            this.pro_cost.Name = "pro_cost";
            this.pro_cost.Primaryddl = "";
            this.pro_cost.QcdCondition = "";
            this.pro_cost.Query_con = "";
            this.pro_cost.Reftbltran_cd = "";
            this.pro_cost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pro_cost.Tbl_nm = "";
            // 
            // OTHER_DET1
            // 
            this.OTHER_DET1.Dispddlfields = "";
            this.OTHER_DET1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.OTHER_DET1, "OTHER_DET1");
            this.OTHER_DET1.Name = "OTHER_DET1";
            this.OTHER_DET1.Primaryddl = "";
            this.OTHER_DET1.Query_con = "";
            this.OTHER_DET1.Reftbltran_cd = "";
            this.OTHER_DET1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OTHER_DET1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.OTHER_DET1.Tbl_nm = "";
            this.OTHER_DET1.Text = "Other Details";
            this.OTHER_DET1.UseColumnTextForButtonValue = true;
            // 
            // lblF2
            // 
            resources.ApplyResources(this.lblF2, "lblF2");
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.Name = "lblF2";
            // 
            // btnRemove
            // 
            this.btnRemove.Dispddlfields = "";
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.GradientBottom = System.Drawing.Color.Gray;
            this.btnRemove.GradientTop = System.Drawing.SystemColors.Control;
            this.btnRemove.IsQcd = false;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Primaryddl = "";
            this.btnRemove.QcdCondition = "";
            this.btnRemove.Query_con = "";
            this.btnRemove.Reftbltran_cd = "";
            this.btnRemove.Tbl_nm = "";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dispddlfields = "";
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.GradientBottom = System.Drawing.Color.Gray;
            this.btnAdd.GradientTop = System.Drawing.SystemColors.Control;
            this.btnAdd.IsQcd = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primaryddl = "";
            this.btnAdd.QcdCondition = "";
            this.btnAdd.Query_con = "";
            this.btnAdd.Reftbltran_cd = "";
            this.btnAdd.Tbl_nm = "";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox1.BorderColor = System.Drawing.Color.Black;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.OTHER_DET);
            this.groupBox1.Controls.Add(this.btnProdNm);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "Product Details";
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            resources.ApplyResources(this.OTHER_DET, "OTHER_DET");
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.IsQcd = false;
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.QcdCondition = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // btnProdNm
            // 
            resources.ApplyResources(this.btnProdNm, "btnProdNm");
            this.btnProdNm.Dispddlfields = "";
            this.btnProdNm.GradientBottom = System.Drawing.Color.Gray;
            this.btnProdNm.GradientTop = System.Drawing.SystemColors.Control;
            this.btnProdNm.IsQcd = false;
            this.btnProdNm.Name = "btnProdNm";
            this.btnProdNm.Primaryddl = "";
            this.btnProdNm.QcdCondition = "";
            this.btnProdNm.Query_con = "";
            this.btnProdNm.Reftbltran_cd = "";
            this.btnProdNm.Tbl_nm = "";
            this.btnProdNm.UseVisualStyleBackColor = true;
            this.btnProdNm.Click += new System.EventHandler(this.btnHeaderNm_Click);
            // 
            // txtRemarks
            // 
            resources.ApplyResources(this.txtRemarks, "txtRemarks");
            this.txtRemarks.Name = "txtRemarks";
            // 
            // txtDesc
            // 
            resources.ApplyResources(this.txtDesc, "txtDesc");
            this.txtDesc.Name = "txtDesc";
            // 
            // txtProduct
            // 
            resources.ApplyResources(this.txtProduct, "txtProduct");
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            this.txtProduct.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            resources.ApplyResources(this.grpbxSearch, "grpbxSearch");
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            // 
            // lblRowsCount
            // 
            resources.ApplyResources(this.lblRowsCount, "lblRowsCount");
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Name = "lblRowsCount";
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.AllowUserToResizeRows = false;
            this.dgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pro_id,
            this.prod_nm,
            this.prod_desc});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 372;
            resources.ApplyResources(this.dgvSearch, "dgvSearch");
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.width = 231;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // pro_id
            // 
            this.pro_id.DataPropertyName = "pro_id";
            resources.ApplyResources(this.pro_id, "pro_id");
            this.pro_id.Name = "pro_id";
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            resources.ApplyResources(this.prod_nm, "prod_nm");
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.ReadOnly = true;
            this.prod_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // prod_desc
            // 
            this.prod_desc.DataPropertyName = "prod_desc";
            resources.ApplyResources(this.prod_desc, "prod_desc");
            this.prod_desc.Name = "prod_desc";
            this.prod_desc.ReadOnly = true;
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmProcessMaster
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.ucToolBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmProcessMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProcessMaster_FormClosed);
            this.Load += new System.EventHandler(this.frmProcessMaster_Load);
            this.Enter += new System.EventHandler(this.frmProcessMaster_Enter);
            this.Resize += new System.EventHandler(this.frmProcessMaster_Resize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcess)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper groupBox1;
        private Grouper groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label2;
        private PopupButton btnRemove;
        private PopupButton btnAdd;
        private PopupButton btnProdNm;
        private System.Windows.Forms.Label lblF2;
        private System.Windows.Forms.DataGridViewTextBoxColumn pro_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc;
        private PopupButton OTHER_DET;
        private MyDataGridView dgvProcess;
        private System.Windows.Forms.Label lblRowsCount;
        private POPUPTEXTBOX_FOR_GRID pro_order;
        private System.Windows.Forms.DataGridViewTextBoxColumn pro_order_id;
        private POPUPTEXTBOX_FOR_GRID ptserial;
        private POPUPTEXTBOX_FOR_GRID pro_nm;
        private POPUPTEXTBOX_FOR_GRID pro_desc;
        private POPUPTEXTBOX_FOR_GRID pro_setup;
        private POPUPTEXTBOX_FOR_GRID pro_cycle;
        private POPUPTEXTBOX_FOR_GRID mac_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac_type_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn opt_id;
        private POPUPTEXTBOX_FOR_GRID opt_type;
        private POPUPTEXTBOX_FOR_GRID pro_cost;
        private POPUPBUTTON_FOR_GRID OTHER_DET1;
    }
}