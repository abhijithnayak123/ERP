namespace iMANTRA
{
    partial class frmQCCheckPoints
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQCCheckPoints));
            this.groupBox2 = new iMANTRA.Grouper();
            this.lblF2 = new System.Windows.Forms.Label();
            this.btnRemove = new iMANTRA.PopupButton();
            this.btnAdd = new iMANTRA.PopupButton();
            this.dgvCheckPoint = new iMANTRA.MyDataGridView();
            this.chklst_order = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.chklst_order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.chklst_nm = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.chklst_desc = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.chklst_uom = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.chklst_value = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.tolplus = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.tolminus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priority_nm = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.priority_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OTHER_DET1 = new iMANTRA.POPUPBUTTON_FOR_GRID();
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
            this.chklstId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chklist_prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckPoint)).BeginInit();
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
            this.groupBox2.Controls.Add(this.lblF2);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.dgvCheckPoint);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "Check Points Details";
            this.groupBox2.Location = new System.Drawing.Point(262, 128);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox2.PaintGroupBox = true;
            this.groupBox2.RoundCorners = 1;
            this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 1;
            this.groupBox2.Size = new System.Drawing.Size(672, 360);
            this.groupBox2.TabIndex = 9;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblF2.Location = new System.Drawing.Point(325, 30);
            this.lblF2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(162, 16);
            this.lblF2.TabIndex = 3;
            this.lblF2.Text = "Press F2 - To Get List";
            this.lblF2.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Dispddlfields = "";
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(89, 27);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Primaryddl = "";
            this.btnRemove.Query_con = "";
            this.btnRemove.Reftbltran_cd = "";
            this.btnRemove.Size = new System.Drawing.Size(94, 27);
            this.btnRemove.TabIndex = 11;
            this.btnRemove.Tbl_nm = "";
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dispddlfields = "";
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(11, 27);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primaryddl = "";
            this.btnAdd.Query_con = "";
            this.btnAdd.Reftbltran_cd = "";
            this.btnAdd.Size = new System.Drawing.Size(69, 27);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Tbl_nm = "";
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvCheckPoint
            // 
            this.dgvCheckPoint.AllowUserToAddRows = false;
            this.dgvCheckPoint.AllowUserToDeleteRows = false;
            this.dgvCheckPoint.AllowUserToResizeColumns = false;
            this.dgvCheckPoint.AllowUserToResizeRows = false;
            this.dgvCheckPoint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheckPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheckPoint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chklst_order,
            this.chklst_order_id,
            this.ptserial,
            this.chklst_nm,
            this.chklst_desc,
            this.chklst_uom,
            this.chklst_value,
            this.tolplus,
            this.tolminus,
            this.priority_nm,
            this.priority_id,
            this.OTHER_DET1});
            this.dgvCheckPoint.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCheckPoint.height = 296;
            this.dgvCheckPoint.Location = new System.Drawing.Point(5, 58);
            this.dgvCheckPoint.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCheckPoint.Name = "dgvCheckPoint";
            this.dgvCheckPoint.Size = new System.Drawing.Size(662, 296);
            this.dgvCheckPoint.TabIndex = 12;
            this.dgvCheckPoint.width = 662;
            this.dgvCheckPoint.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckPoint_CellClick);
            this.dgvCheckPoint.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcess_CellContentClick);
            this.dgvCheckPoint.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckPoint_CellEnter);
            this.dgvCheckPoint.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheckPoint_CellLeave);
            this.dgvCheckPoint.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvProcess_CellValidating);
            this.dgvCheckPoint.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvProcess_EditingControlShowing);
            this.dgvCheckPoint.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvProcess_RowPostPaint);
            this.dgvCheckPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvCheckPoint_KeyPress);
            // 
            // chklst_order
            // 
            this.chklst_order.DataPropertyName = "chklst_order";
            this.chklst_order.Dispddlfields = "";
            this.chklst_order.HeaderText = "Check List Order";
            this.chklst_order.Name = "chklst_order";
            this.chklst_order.Primaryddl = "";
            this.chklst_order.Query_con = "";
            this.chklst_order.Reftbltran_cd = "";
            this.chklst_order.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chklst_order.Tbl_nm = "";
            this.chklst_order.Width = 176;
            // 
            // chklst_order_id
            // 
            this.chklst_order_id.DataPropertyName = "chklst_order_id";
            this.chklst_order_id.HeaderText = "Order Id";
            this.chklst_order_id.Name = "chklst_order_id";
            this.chklst_order_id.ReadOnly = true;
            this.chklst_order_id.Visible = false;
            // 
            // ptserial
            // 
            this.ptserial.DataPropertyName = "ptserial";
            this.ptserial.Dispddlfields = "";
            this.ptserial.HeaderText = "PTSERIAL";
            this.ptserial.Name = "ptserial";
            this.ptserial.Primaryddl = "";
            this.ptserial.Query_con = "";
            this.ptserial.ReadOnly = true;
            this.ptserial.Reftbltran_cd = "";
            this.ptserial.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ptserial.Tbl_nm = "";
            this.ptserial.Visible = false;
            // 
            // chklst_nm
            // 
            this.chklst_nm.DataPropertyName = "chklst_nm";
            this.chklst_nm.Dispddlfields = "";
            this.chklst_nm.HeaderText = "Check  Point Name";
            this.chklst_nm.Name = "chklst_nm";
            this.chklst_nm.Primaryddl = "";
            this.chklst_nm.Query_con = "";
            this.chklst_nm.Reftbltran_cd = "";
            this.chklst_nm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chklst_nm.Tbl_nm = "";
            this.chklst_nm.Width = 149;
            // 
            // chklst_desc
            // 
            this.chklst_desc.DataPropertyName = "chklst_desc";
            this.chklst_desc.Dispddlfields = "";
            this.chklst_desc.HeaderText = "Description";
            this.chklst_desc.Name = "chklst_desc";
            this.chklst_desc.Primaryddl = "";
            this.chklst_desc.Query_con = "";
            this.chklst_desc.Reftbltran_cd = "";
            this.chklst_desc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chklst_desc.Tbl_nm = "";
            this.chklst_desc.Width = 143;
            // 
            // chklst_uom
            // 
            this.chklst_uom.DataPropertyName = "chklst_uom";
            dataGridViewCellStyle3.NullValue = null;
            this.chklst_uom.DefaultCellStyle = dataGridViewCellStyle3;
            this.chklst_uom.Dispddlfields = "";
            this.chklst_uom.HeaderText = "UOM";
            this.chklst_uom.Name = "chklst_uom";
            this.chklst_uom.Primaryddl = "";
            this.chklst_uom.Query_con = "";
            this.chklst_uom.Reftbltran_cd = "";
            this.chklst_uom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chklst_uom.Tbl_nm = "";
            this.chklst_uom.Width = 63;
            // 
            // chklst_value
            // 
            this.chklst_value.DataPropertyName = "chklst_value";
            this.chklst_value.Dispddlfields = "";
            this.chklst_value.HeaderText = "Desired Value";
            this.chklst_value.Name = "chklst_value";
            this.chklst_value.Primaryddl = "";
            this.chklst_value.Query_con = "";
            this.chklst_value.Reftbltran_cd = "";
            this.chklst_value.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chklst_value.Tbl_nm = "";
            this.chklst_value.Width = 104;
            // 
            // tolplus
            // 
            this.tolplus.DataPropertyName = "tolplus";
            this.tolplus.Dispddlfields = "";
            this.tolplus.HeaderText = "Tollerance (+)";
            this.tolplus.Name = "tolplus";
            this.tolplus.Primaryddl = "";
            this.tolplus.Query_con = "";
            this.tolplus.Reftbltran_cd = "";
            this.tolplus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tolplus.Tbl_nm = "";
            this.tolplus.Width = 158;
            // 
            // tolminus
            // 
            this.tolminus.DataPropertyName = "tolminus";
            this.tolminus.HeaderText = "Tollerance (-)";
            this.tolminus.Name = "tolminus";
            this.tolminus.Width = 158;
            // 
            // priority_nm
            // 
            this.priority_nm.DataPropertyName = "priority_nm";
            this.priority_nm.Dispddlfields = "";
            this.priority_nm.HeaderText = "Priority";
            this.priority_nm.Name = "priority_nm";
            this.priority_nm.Primaryddl = "";
            this.priority_nm.Query_con = "";
            this.priority_nm.Reftbltran_cd = "";
            this.priority_nm.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.priority_nm.Tbl_nm = "";
            this.priority_nm.Width = 113;
            // 
            // priority_id
            // 
            this.priority_id.HeaderText = "priority_id";
            this.priority_id.Name = "priority_id";
            this.priority_id.ReadOnly = true;
            this.priority_id.Visible = false;
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
            this.OTHER_DET1.Width = 130;
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
            this.groupBox1.Location = new System.Drawing.Point(263, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            this.groupBox1.Size = new System.Drawing.Size(670, 97);
            this.groupBox1.TabIndex = 3;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.Location = new System.Drawing.Point(498, 57);
            this.OTHER_DET.Margin = new System.Windows.Forms.Padding(4);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(163, 29);
            this.OTHER_DET.TabIndex = 8;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "OTHER DETAILS";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // btnProdNm
            // 
            this.btnProdNm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProdNm.BackgroundImage")));
            this.btnProdNm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProdNm.Dispddlfields = "";
            this.btnProdNm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProdNm.Location = new System.Drawing.Point(337, 28);
            this.btnProdNm.Margin = new System.Windows.Forms.Padding(0);
            this.btnProdNm.Name = "btnProdNm";
            this.btnProdNm.Primaryddl = "";
            this.btnProdNm.Query_con = "";
            this.btnProdNm.Reftbltran_cd = "";
            this.btnProdNm.Size = new System.Drawing.Size(32, 27);
            this.btnProdNm.TabIndex = 5;
            this.btnProdNm.Tbl_nm = "";
            this.btnProdNm.UseVisualStyleBackColor = true;
            this.btnProdNm.Click += new System.EventHandler(this.btnProdNm_Click);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(89, 57);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(243, 36);
            this.txtRemarks.TabIndex = 7;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(498, 26);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(164, 26);
            this.txtDesc.TabIndex = 6;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(89, 28);
            this.txtProduct.Margin = new System.Windows.Forms.Padding(4);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(245, 26);
            this.txtProduct.TabIndex = 4;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            this.txtProduct.Validating += new System.ComponentModel.CancelEventHandler(this.txtProduct_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(10, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Remarks";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(375, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product";
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
            this.grpbxSearch.Location = new System.Drawing.Point(3, 30);
            this.grpbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(257, 459);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(9, 438);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 3;
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
            this.chklstId,
            this.prod_nm,
            this.chklist_prod_desc});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 361;
            this.dgvSearch.Location = new System.Drawing.Point(9, 70);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(243, 361);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 243;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // chklstId
            // 
            this.chklstId.DataPropertyName = "chklstId";
            this.chklstId.HeaderText = "Check List Id";
            this.chklstId.Name = "chklstId";
            this.chklstId.Visible = false;
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            this.prod_nm.HeaderText = "Product Name";
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.ReadOnly = true;
            this.prod_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chklist_prod_desc
            // 
            this.chklist_prod_desc.DataPropertyName = "chklist_prod_desc";
            this.chklist_prod_desc.HeaderText = "Description";
            this.chklist_prod_desc.Name = "chklist_prod_desc";
            this.chklist_prod_desc.ReadOnly = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(10, 36);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(242, 26);
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
            this.ucToolBar1.Size = new System.Drawing.Size(937, 29);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmQCCheckPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(937, 491);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmQCCheckPoints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQCCheckPoints_FormClosed);
            this.Load += new System.EventHandler(this.frmQCCheckPoints_Load);
            this.Enter += new System.EventHandler(this.frmQCCheckPoints_Enter);
            this.Resize += new System.EventHandler(this.frmQCCheckPoints_Resize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckPoint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper groupBox2;
        private System.Windows.Forms.Label lblF2;
        private PopupButton btnRemove;
        private PopupButton btnAdd;
        private MyDataGridView dgvCheckPoint;
        private Grouper groupBox1;
        private PopupButton btnProdNm;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn chklstId;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn chklist_prod_desc;
        private PopupButton OTHER_DET;
        private POPUPTEXTBOX_FOR_GRID chklst_order;
        private System.Windows.Forms.DataGridViewTextBoxColumn chklst_order_id;
        private POPUPTEXTBOX_FOR_GRID ptserial;
        private POPUPTEXTBOX_FOR_GRID chklst_nm;
        private POPUPTEXTBOX_FOR_GRID chklst_desc;
        private POPUPTEXTBOX_FOR_GRID chklst_uom;
        private POPUPTEXTBOX_FOR_GRID chklst_value;
        private POPUPTEXTBOX_FOR_GRID tolplus;
        private System.Windows.Forms.DataGridViewTextBoxColumn tolminus;
        private POPUPTEXTBOX_FOR_GRID priority_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn priority_id;
        private POPUPBUTTON_FOR_GRID OTHER_DET1;
        private System.Windows.Forms.Label lblRowsCount;
    }
}