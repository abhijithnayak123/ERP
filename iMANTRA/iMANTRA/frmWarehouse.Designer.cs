namespace iMANTRA
{
    partial class frmWarehouse
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
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.tcWarehouse = new System.Windows.Forms.TabControl();
            this.tpWarehouse = new System.Windows.Forms.TabPage();
            this.grouper4 = new iMANTRA.Grouper();
            this.txtWHDesc = new System.Windows.Forms.TextBox();
            this.txtWHName = new System.Windows.Forms.TextBox();
            this.txtWHCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper1 = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.txtWHSearch = new System.Windows.Forms.TextBox();
            this.dgvWareHouse = new iMANTRA.MyDataGridView();
            this.WAREHOUSEID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRIPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpRack = new System.Windows.Forms.TabPage();
            this.grouper5 = new iMANTRA.Grouper();
            this.txtRackWHId = new System.Windows.Forms.TextBox();
            this.txtRackDesc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRackWH = new System.Windows.Forms.TextBox();
            this.txtRackName = new System.Windows.Forms.TextBox();
            this.txtRackCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grouper2 = new iMANTRA.Grouper();
            this.txtRackSearch = new System.Windows.Forms.TextBox();
            this.dgvRack = new iMANTRA.MyDataGridView();
            this.RACKID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RACKNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSEID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WAREHOUSENAME1 = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.tpBin = new System.Windows.Forms.TabPage();
            this.grouper6 = new iMANTRA.Grouper();
            this.txtBinRackId = new System.Windows.Forms.TextBox();
            this.txtBinDesc = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBinMin = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBinMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBinRack = new System.Windows.Forms.TextBox();
            this.txtBinName = new System.Windows.Forms.TextBox();
            this.txtBinCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grouper3 = new iMANTRA.Grouper();
            this.txtBinSearch = new System.Windows.Forms.TextBox();
            this.dgvBin = new iMANTRA.MyDataGridView();
            this.BINID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BINNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RACKID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RACKNAME1 = new iMANTRA.POPUPTEXTBOX_FOR_GRID();
            this.MAX_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblrowsRack = new System.Windows.Forms.Label();
            this.lblRowsBin = new System.Windows.Forms.Label();
            this.tcWarehouse.SuspendLayout();
            this.tpWarehouse.SuspendLayout();
            this.grouper4.SuspendLayout();
            this.grouper1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWareHouse)).BeginInit();
            this.tpRack.SuspendLayout();
            this.grouper5.SuspendLayout();
            this.grouper2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRack)).BeginInit();
            this.tpBin.SuspendLayout();
            this.grouper6.SuspendLayout();
            this.grouper3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBin)).BeginInit();
            this.SuspendLayout();
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(861, 25);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // tcWarehouse
            // 
            this.tcWarehouse.Controls.Add(this.tpWarehouse);
            this.tcWarehouse.Controls.Add(this.tpRack);
            this.tcWarehouse.Controls.Add(this.tpBin);
            this.tcWarehouse.Location = new System.Drawing.Point(3, 28);
            this.tcWarehouse.Margin = new System.Windows.Forms.Padding(0);
            this.tcWarehouse.Name = "tcWarehouse";
            this.tcWarehouse.SelectedIndex = 0;
            this.tcWarehouse.Size = new System.Drawing.Size(857, 481);
            this.tcWarehouse.TabIndex = 0;
            this.tcWarehouse.SelectedIndexChanged += new System.EventHandler(this.tcWarehouse_SelectedIndexChanged);
            this.tcWarehouse.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcWarehouse_Deselecting);
            // 
            // tpWarehouse
            // 
            this.tpWarehouse.Controls.Add(this.grouper4);
            this.tpWarehouse.Controls.Add(this.grouper1);
            this.tpWarehouse.Location = new System.Drawing.Point(4, 27);
            this.tpWarehouse.Margin = new System.Windows.Forms.Padding(4);
            this.tpWarehouse.Name = "tpWarehouse";
            this.tpWarehouse.Padding = new System.Windows.Forms.Padding(4);
            this.tpWarehouse.Size = new System.Drawing.Size(849, 450);
            this.tpWarehouse.TabIndex = 0;
            this.tpWarehouse.Text = "WAREHOUSE";
            this.tpWarehouse.UseVisualStyleBackColor = true;
            // 
            // grouper4
            // 
            this.grouper4.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper4.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper4.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper4.BorderColor = System.Drawing.Color.Black;
            this.grouper4.BorderThickness = 1F;
            this.grouper4.Controls.Add(this.txtWHDesc);
            this.grouper4.Controls.Add(this.txtWHName);
            this.grouper4.Controls.Add(this.txtWHCode);
            this.grouper4.Controls.Add(this.label3);
            this.grouper4.Controls.Add(this.label2);
            this.grouper4.Controls.Add(this.label1);
            this.grouper4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper4.GroupImage = null;
            this.grouper4.GroupTitle = "Basic WareHouse Details";
            this.grouper4.Location = new System.Drawing.Point(377, 8);
            this.grouper4.Margin = new System.Windows.Forms.Padding(0);
            this.grouper4.Name = "grouper4";
            this.grouper4.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper4.PaintGroupBox = true;
            this.grouper4.RoundCorners = 1;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 3;
            this.grouper4.Size = new System.Drawing.Size(464, 432);
            this.grouper4.TabIndex = 2;
            // 
            // txtWHDesc
            // 
            this.txtWHDesc.Location = new System.Drawing.Point(136, 156);
            this.txtWHDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtWHDesc.Name = "txtWHDesc";
            this.txtWHDesc.Size = new System.Drawing.Size(320, 26);
            this.txtWHDesc.TabIndex = 3;
            // 
            // txtWHName
            // 
            this.txtWHName.Location = new System.Drawing.Point(136, 108);
            this.txtWHName.Margin = new System.Windows.Forms.Padding(4);
            this.txtWHName.Name = "txtWHName";
            this.txtWHName.Size = new System.Drawing.Size(320, 26);
            this.txtWHName.TabIndex = 2;
            // 
            // txtWHCode
            // 
            this.txtWHCode.Location = new System.Drawing.Point(136, 60);
            this.txtWHCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtWHCode.Name = "txtWHCode";
            this.txtWHCode.Size = new System.Drawing.Size(320, 26);
            this.txtWHCode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code";
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lblRowsCount);
            this.grouper1.Controls.Add(this.txtWHSearch);
            this.grouper1.Controls.Add(this.dgvWareHouse);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "WareHouse List";
            this.grouper1.Location = new System.Drawing.Point(10, 8);
            this.grouper1.Margin = new System.Windows.Forms.Padding(4);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(359, 432);
            this.grouper1.TabIndex = 1;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(6, 405);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 4;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // txtWHSearch
            // 
            this.txtWHSearch.Location = new System.Drawing.Point(9, 36);
            this.txtWHSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtWHSearch.Name = "txtWHSearch";
            this.txtWHSearch.Size = new System.Drawing.Size(344, 26);
            this.txtWHSearch.TabIndex = 1;
            this.txtWHSearch.TextChanged += new System.EventHandler(this.txtWHSearch_TextChanged);
            // 
            // dgvWareHouse
            // 
            this.dgvWareHouse.AllowUserToAddRows = false;
            this.dgvWareHouse.AllowUserToDeleteRows = false;
            this.dgvWareHouse.AllowUserToResizeColumns = false;
            this.dgvWareHouse.AllowUserToResizeRows = false;
            this.dgvWareHouse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWareHouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWareHouse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WAREHOUSEID,
            this.WAREHOUSENAME,
            this.DESCRIPTION});
            this.dgvWareHouse.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWareHouse.Location = new System.Drawing.Point(9, 66);
            this.dgvWareHouse.Margin = new System.Windows.Forms.Padding(0);
            this.dgvWareHouse.Name = "dgvWareHouse";
            this.dgvWareHouse.RowHeadersVisible = false;
            this.dgvWareHouse.Size = new System.Drawing.Size(346, 329);
            this.dgvWareHouse.TabIndex = 0;
            this.dgvWareHouse.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWareHouse_CellClick);
            // 
            // WAREHOUSEID
            // 
            this.WAREHOUSEID.DataPropertyName = "WAREHOUSEID";
            this.WAREHOUSEID.HeaderText = "WAREHOUSEID";
            this.WAREHOUSEID.Name = "WAREHOUSEID";
            this.WAREHOUSEID.ReadOnly = true;
            this.WAREHOUSEID.Visible = false;
            // 
            // WAREHOUSENAME
            // 
            this.WAREHOUSENAME.DataPropertyName = "WAREHOUSENAME";
            this.WAREHOUSENAME.HeaderText = "WAREHOUSE";
            this.WAREHOUSENAME.Name = "WAREHOUSENAME";
            // 
            // DESCRIPTION
            // 
            this.DESCRIPTION.DataPropertyName = "DESCRIPTION";
            this.DESCRIPTION.HeaderText = "DESCRIPTION";
            this.DESCRIPTION.Name = "DESCRIPTION";
            // 
            // tpRack
            // 
            this.tpRack.Controls.Add(this.grouper5);
            this.tpRack.Controls.Add(this.grouper2);
            this.tpRack.Location = new System.Drawing.Point(4, 27);
            this.tpRack.Margin = new System.Windows.Forms.Padding(4);
            this.tpRack.Name = "tpRack";
            this.tpRack.Size = new System.Drawing.Size(849, 450);
            this.tpRack.TabIndex = 2;
            this.tpRack.Text = "RACK";
            this.tpRack.UseVisualStyleBackColor = true;
            // 
            // grouper5
            // 
            this.grouper5.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper5.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper5.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper5.BorderColor = System.Drawing.Color.Black;
            this.grouper5.BorderThickness = 1F;
            this.grouper5.Controls.Add(this.txtRackWHId);
            this.grouper5.Controls.Add(this.txtRackDesc);
            this.grouper5.Controls.Add(this.label13);
            this.grouper5.Controls.Add(this.txtRackWH);
            this.grouper5.Controls.Add(this.txtRackName);
            this.grouper5.Controls.Add(this.txtRackCode);
            this.grouper5.Controls.Add(this.label4);
            this.grouper5.Controls.Add(this.label5);
            this.grouper5.Controls.Add(this.label6);
            this.grouper5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper5.GroupImage = null;
            this.grouper5.GroupTitle = "Basic Rack Details";
            this.grouper5.Location = new System.Drawing.Point(377, 8);
            this.grouper5.Margin = new System.Windows.Forms.Padding(0);
            this.grouper5.Name = "grouper5";
            this.grouper5.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper5.PaintGroupBox = true;
            this.grouper5.RoundCorners = 1;
            this.grouper5.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper5.ShadowControl = false;
            this.grouper5.ShadowThickness = 3;
            this.grouper5.Size = new System.Drawing.Size(464, 432);
            this.grouper5.TabIndex = 3;
            // 
            // txtRackWHId
            // 
            this.txtRackWHId.Location = new System.Drawing.Point(136, 250);
            this.txtRackWHId.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackWHId.Name = "txtRackWHId";
            this.txtRackWHId.Size = new System.Drawing.Size(141, 26);
            this.txtRackWHId.TabIndex = 22;
            this.txtRackWHId.Visible = false;
            // 
            // txtRackDesc
            // 
            this.txtRackDesc.Location = new System.Drawing.Point(136, 156);
            this.txtRackDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackDesc.Name = "txtRackDesc";
            this.txtRackDesc.Size = new System.Drawing.Size(320, 26);
            this.txtRackDesc.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 160);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 18);
            this.label13.TabIndex = 14;
            this.label13.Text = "Description";
            // 
            // txtRackWH
            // 
            this.txtRackWH.Location = new System.Drawing.Point(136, 204);
            this.txtRackWH.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackWH.Name = "txtRackWH";
            this.txtRackWH.ReadOnly = true;
            this.txtRackWH.Size = new System.Drawing.Size(320, 26);
            this.txtRackWH.TabIndex = 4;
            this.txtRackWH.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRackWH_KeyDown);
            // 
            // txtRackName
            // 
            this.txtRackName.Location = new System.Drawing.Point(136, 108);
            this.txtRackName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackName.Name = "txtRackName";
            this.txtRackName.Size = new System.Drawing.Size(320, 26);
            this.txtRackName.TabIndex = 2;
            // 
            // txtRackCode
            // 
            this.txtRackCode.Location = new System.Drawing.Point(136, 60);
            this.txtRackCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackCode.Name = "txtRackCode";
            this.txtRackCode.Size = new System.Drawing.Size(320, 26);
            this.txtRackCode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 208);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Warehouse";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Code";
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.lblrowsRack);
            this.grouper2.Controls.Add(this.txtRackSearch);
            this.grouper2.Controls.Add(this.dgvRack);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Rack List";
            this.grouper2.Location = new System.Drawing.Point(10, 8);
            this.grouper2.Margin = new System.Windows.Forms.Padding(4);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 3;
            this.grouper2.Size = new System.Drawing.Size(359, 432);
            this.grouper2.TabIndex = 2;
            // 
            // txtRackSearch
            // 
            this.txtRackSearch.Location = new System.Drawing.Point(10, 34);
            this.txtRackSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtRackSearch.Name = "txtRackSearch";
            this.txtRackSearch.Size = new System.Drawing.Size(343, 26);
            this.txtRackSearch.TabIndex = 2;
            this.txtRackSearch.TextChanged += new System.EventHandler(this.txtRackSearch_TextChanged);
            // 
            // dgvRack
            // 
            this.dgvRack.AllowUserToAddRows = false;
            this.dgvRack.AllowUserToDeleteRows = false;
            this.dgvRack.AllowUserToResizeColumns = false;
            this.dgvRack.AllowUserToResizeRows = false;
            this.dgvRack.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RACKID,
            this.RACKNAME,
            this.WAREHOUSEID1,
            this.WAREHOUSENAME1});
            this.dgvRack.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvRack.Location = new System.Drawing.Point(9, 64);
            this.dgvRack.Margin = new System.Windows.Forms.Padding(0);
            this.dgvRack.Name = "dgvRack";
            this.dgvRack.RowHeadersVisible = false;
            this.dgvRack.Size = new System.Drawing.Size(346, 333);
            this.dgvRack.TabIndex = 1;
            this.dgvRack.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRack_CellClick);
            // 
            // RACKID
            // 
            this.RACKID.DataPropertyName = "RACKID";
            this.RACKID.HeaderText = "RACKID";
            this.RACKID.Name = "RACKID";
            this.RACKID.ReadOnly = true;
            this.RACKID.Visible = false;
            // 
            // RACKNAME
            // 
            this.RACKNAME.DataPropertyName = "RACKNAME";
            this.RACKNAME.HeaderText = "RACK";
            this.RACKNAME.Name = "RACKNAME";
            // 
            // WAREHOUSEID1
            // 
            this.WAREHOUSEID1.DataPropertyName = "WAREHOUSEID1";
            this.WAREHOUSEID1.HeaderText = "WAREHOUSEID";
            this.WAREHOUSEID1.Name = "WAREHOUSEID1";
            this.WAREHOUSEID1.ReadOnly = true;
            this.WAREHOUSEID1.Visible = false;
            // 
            // WAREHOUSENAME1
            // 
            this.WAREHOUSENAME1.DataPropertyName = "WAREHOUSENAME1";
            this.WAREHOUSENAME1.Dispddlfields = "";
            this.WAREHOUSENAME1.HeaderText = "WAREHOUSE";
            this.WAREHOUSENAME1.Name = "WAREHOUSENAME1";
            this.WAREHOUSENAME1.Primaryddl = "";
            this.WAREHOUSENAME1.Query_con = "";
            this.WAREHOUSENAME1.Reftbltran_cd = "";
            this.WAREHOUSENAME1.Tbl_nm = "";
            // 
            // tpBin
            // 
            this.tpBin.Controls.Add(this.grouper6);
            this.tpBin.Controls.Add(this.grouper3);
            this.tpBin.Location = new System.Drawing.Point(4, 27);
            this.tpBin.Margin = new System.Windows.Forms.Padding(4);
            this.tpBin.Name = "tpBin";
            this.tpBin.Padding = new System.Windows.Forms.Padding(4);
            this.tpBin.Size = new System.Drawing.Size(849, 450);
            this.tpBin.TabIndex = 1;
            this.tpBin.Text = "BIN";
            this.tpBin.UseVisualStyleBackColor = true;
            // 
            // grouper6
            // 
            this.grouper6.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper6.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper6.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper6.BorderColor = System.Drawing.Color.Black;
            this.grouper6.BorderThickness = 1F;
            this.grouper6.Controls.Add(this.txtBinRackId);
            this.grouper6.Controls.Add(this.txtBinDesc);
            this.grouper6.Controls.Add(this.label14);
            this.grouper6.Controls.Add(this.txtBinMin);
            this.grouper6.Controls.Add(this.label11);
            this.grouper6.Controls.Add(this.txtBinMax);
            this.grouper6.Controls.Add(this.label10);
            this.grouper6.Controls.Add(this.txtBinRack);
            this.grouper6.Controls.Add(this.txtBinName);
            this.grouper6.Controls.Add(this.txtBinCode);
            this.grouper6.Controls.Add(this.label7);
            this.grouper6.Controls.Add(this.label8);
            this.grouper6.Controls.Add(this.label9);
            this.grouper6.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper6.GroupImage = null;
            this.grouper6.GroupTitle = "Basic Bin Details";
            this.grouper6.Location = new System.Drawing.Point(377, 8);
            this.grouper6.Margin = new System.Windows.Forms.Padding(0);
            this.grouper6.Name = "grouper6";
            this.grouper6.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper6.PaintGroupBox = true;
            this.grouper6.RoundCorners = 1;
            this.grouper6.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper6.ShadowControl = false;
            this.grouper6.ShadowThickness = 1;
            this.grouper6.Size = new System.Drawing.Size(464, 432);
            this.grouper6.TabIndex = 3;
            // 
            // txtBinRackId
            // 
            this.txtBinRackId.Location = new System.Drawing.Point(34, 346);
            this.txtBinRackId.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinRackId.Name = "txtBinRackId";
            this.txtBinRackId.Size = new System.Drawing.Size(141, 26);
            this.txtBinRackId.TabIndex = 21;
            this.txtBinRackId.Visible = false;
            // 
            // txtBinDesc
            // 
            this.txtBinDesc.Location = new System.Drawing.Point(150, 156);
            this.txtBinDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinDesc.Name = "txtBinDesc";
            this.txtBinDesc.Size = new System.Drawing.Size(307, 26);
            this.txtBinDesc.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 160);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 18);
            this.label14.TabIndex = 20;
            this.label14.Text = "Description";
            // 
            // txtBinMin
            // 
            this.txtBinMin.Location = new System.Drawing.Point(150, 252);
            this.txtBinMin.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinMin.Name = "txtBinMin";
            this.txtBinMin.Size = new System.Drawing.Size(307, 26);
            this.txtBinMin.TabIndex = 6;
            this.txtBinMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBinMin_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 256);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 18);
            this.label11.TabIndex = 16;
            this.label11.Text = "Capacity(Min)";
            // 
            // txtBinMax
            // 
            this.txtBinMax.Location = new System.Drawing.Point(150, 300);
            this.txtBinMax.Margin = new System.Windows.Forms.Padding(0);
            this.txtBinMax.Name = "txtBinMax";
            this.txtBinMax.Size = new System.Drawing.Size(307, 26);
            this.txtBinMax.TabIndex = 7;
            this.txtBinMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBinMax_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 304);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 18);
            this.label10.TabIndex = 14;
            this.label10.Text = "Capacity(Max)";
            // 
            // txtBinRack
            // 
            this.txtBinRack.Location = new System.Drawing.Point(150, 204);
            this.txtBinRack.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinRack.Name = "txtBinRack";
            this.txtBinRack.ReadOnly = true;
            this.txtBinRack.Size = new System.Drawing.Size(307, 26);
            this.txtBinRack.TabIndex = 4;
            this.txtBinRack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBinRack_KeyDown);
            // 
            // txtBinName
            // 
            this.txtBinName.Location = new System.Drawing.Point(150, 108);
            this.txtBinName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinName.Name = "txtBinName";
            this.txtBinName.Size = new System.Drawing.Size(307, 26);
            this.txtBinName.TabIndex = 2;
            // 
            // txtBinCode
            // 
            this.txtBinCode.Location = new System.Drawing.Point(150, 60);
            this.txtBinCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinCode.Name = "txtBinCode";
            this.txtBinCode.Size = new System.Drawing.Size(307, 26);
            this.txtBinCode.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 208);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Rack";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 18);
            this.label8.TabIndex = 8;
            this.label8.Text = "Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 64);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "Code";
            // 
            // grouper3
            // 
            this.grouper3.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper3.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper3.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper3.BorderColor = System.Drawing.Color.Black;
            this.grouper3.BorderThickness = 1F;
            this.grouper3.Controls.Add(this.lblRowsBin);
            this.grouper3.Controls.Add(this.txtBinSearch);
            this.grouper3.Controls.Add(this.dgvBin);
            this.grouper3.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper3.GroupImage = null;
            this.grouper3.GroupTitle = "Bin List";
            this.grouper3.Location = new System.Drawing.Point(10, 8);
            this.grouper3.Margin = new System.Windows.Forms.Padding(4);
            this.grouper3.Name = "grouper3";
            this.grouper3.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grouper3.PaintGroupBox = true;
            this.grouper3.RoundCorners = 1;
            this.grouper3.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper3.ShadowControl = false;
            this.grouper3.ShadowThickness = 1;
            this.grouper3.Size = new System.Drawing.Size(359, 432);
            this.grouper3.TabIndex = 2;
            // 
            // txtBinSearch
            // 
            this.txtBinSearch.Location = new System.Drawing.Point(7, 34);
            this.txtBinSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtBinSearch.Name = "txtBinSearch";
            this.txtBinSearch.Size = new System.Drawing.Size(343, 26);
            this.txtBinSearch.TabIndex = 2;
            this.txtBinSearch.TextChanged += new System.EventHandler(this.txtBinSearch_TextChanged);
            // 
            // dgvBin
            // 
            this.dgvBin.AllowUserToAddRows = false;
            this.dgvBin.AllowUserToDeleteRows = false;
            this.dgvBin.AllowUserToResizeColumns = false;
            this.dgvBin.AllowUserToResizeRows = false;
            this.dgvBin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BINID,
            this.BINNAME,
            this.RACKID1,
            this.RACKNAME1,
            this.MAX_QTY});
            this.dgvBin.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBin.Location = new System.Drawing.Point(6, 64);
            this.dgvBin.Margin = new System.Windows.Forms.Padding(0);
            this.dgvBin.Name = "dgvBin";
            this.dgvBin.RowHeadersVisible = false;
            this.dgvBin.Size = new System.Drawing.Size(346, 333);
            this.dgvBin.TabIndex = 1;
            this.dgvBin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBin_CellClick);
            // 
            // BINID
            // 
            this.BINID.DataPropertyName = "BINID";
            this.BINID.HeaderText = "BINID";
            this.BINID.Name = "BINID";
            this.BINID.ReadOnly = true;
            this.BINID.Visible = false;
            // 
            // BINNAME
            // 
            this.BINNAME.DataPropertyName = "BINNAME";
            this.BINNAME.HeaderText = "BIN";
            this.BINNAME.Name = "BINNAME";
            // 
            // RACKID1
            // 
            this.RACKID1.DataPropertyName = "RACKID1";
            this.RACKID1.HeaderText = "RACKID";
            this.RACKID1.Name = "RACKID1";
            this.RACKID1.ReadOnly = true;
            this.RACKID1.Visible = false;
            // 
            // RACKNAME1
            // 
            this.RACKNAME1.DataPropertyName = "RACKNAME1";
            this.RACKNAME1.Dispddlfields = "";
            this.RACKNAME1.HeaderText = "RACK";
            this.RACKNAME1.Name = "RACKNAME1";
            this.RACKNAME1.Primaryddl = "";
            this.RACKNAME1.Query_con = "";
            this.RACKNAME1.Reftbltran_cd = "";
            this.RACKNAME1.Tbl_nm = "";
            // 
            // MAX_QTY
            // 
            this.MAX_QTY.DataPropertyName = "MAX_QTY";
            this.MAX_QTY.HeaderText = "CAPACITY MAX";
            this.MAX_QTY.Name = "MAX_QTY";
            // 
            // lblrowsRack
            // 
            this.lblrowsRack.AutoSize = true;
            this.lblrowsRack.BackColor = System.Drawing.Color.Transparent;
            this.lblrowsRack.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrowsRack.ForeColor = System.Drawing.Color.Red;
            this.lblrowsRack.Location = new System.Drawing.Point(8, 407);
            this.lblrowsRack.Name = "lblrowsRack";
            this.lblrowsRack.Size = new System.Drawing.Size(108, 18);
            this.lblrowsRack.TabIndex = 5;
            this.lblrowsRack.Text = "Rows Count";
            // 
            // lblRowsBin
            // 
            this.lblRowsBin.AutoSize = true;
            this.lblRowsBin.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsBin.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsBin.ForeColor = System.Drawing.Color.Red;
            this.lblRowsBin.Location = new System.Drawing.Point(7, 405);
            this.lblRowsBin.Name = "lblRowsBin";
            this.lblRowsBin.Size = new System.Drawing.Size(108, 18);
            this.lblRowsBin.TabIndex = 5;
            this.lblRowsBin.Text = "Rows Count";
            // 
            // frmWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(861, 511);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.tcWarehouse);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmWarehouse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWarehouse_FormClosed);
            this.Load += new System.EventHandler(this.frmWarehouse_Load);
            this.Enter += new System.EventHandler(this.frmWarehouse_Enter);
            this.Resize += new System.EventHandler(this.frmWarehouse_Resize);
            this.tcWarehouse.ResumeLayout(false);
            this.tpWarehouse.ResumeLayout(false);
            this.grouper4.ResumeLayout(false);
            this.grouper4.PerformLayout();
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWareHouse)).EndInit();
            this.tpRack.ResumeLayout(false);
            this.grouper5.ResumeLayout(false);
            this.grouper5.PerformLayout();
            this.grouper2.ResumeLayout(false);
            this.grouper2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRack)).EndInit();
            this.tpBin.ResumeLayout(false);
            this.grouper6.ResumeLayout(false);
            this.grouper6.PerformLayout();
            this.grouper3.ResumeLayout(false);
            this.grouper3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcWarehouse;
        private System.Windows.Forms.TabPage tpWarehouse;
        private System.Windows.Forms.TabPage tpBin;
        private System.Windows.Forms.TabPage tpRack;
        private MyDataGridView dgvWareHouse;
        private MyDataGridView dgvBin;
        private MyDataGridView dgvRack;
        private UCToolBar ucToolBar1;
        private Grouper grouper1;
        private Grouper grouper2;
        private Grouper grouper3;
        private System.Windows.Forms.TextBox txtBinSearch;
        private System.Windows.Forms.TextBox txtRackSearch;
        private System.Windows.Forms.TextBox txtWHSearch;
        private Grouper grouper4;
        private Grouper grouper5;
        private Grouper grouper6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWHDesc;
        private System.Windows.Forms.TextBox txtWHName;
        private System.Windows.Forms.TextBox txtWHCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRackWH;
        private System.Windows.Forms.TextBox txtRackName;
        private System.Windows.Forms.TextBox txtRackCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBinRack;
        private System.Windows.Forms.TextBox txtBinName;
        private System.Windows.Forms.TextBox txtBinCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBinMin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBinMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRackDesc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBinDesc;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBinRackId;
        private System.Windows.Forms.TextBox txtRackWHId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RACKID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RACKNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSEID1;
        private POPUPTEXTBOX_FOR_GRID WAREHOUSENAME1;
        private System.Windows.Forms.DataGridViewTextBoxColumn BINID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BINNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn RACKID1;
        private POPUPTEXTBOX_FOR_GRID RACKNAME1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAX_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSEID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WAREHOUSENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRIPTION;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.Label lblrowsRack;
        private System.Windows.Forms.Label lblRowsBin;
    }
}