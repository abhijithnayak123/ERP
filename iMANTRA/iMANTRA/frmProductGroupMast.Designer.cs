namespace iMANTRA
{
    partial class frmProductGroupMast
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
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkProd_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.pt_grp_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pt_grp_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grouper4 = new iMANTRA.Grouper();
            this.cmbUOM = new System.Windows.Forms.ComboBox();
            this.cmbStockable = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.grouper1 = new iMANTRA.Grouper();
            this.cmProd_Type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProdGrDesc = new System.Windows.Forms.TextBox();
            this.txtProd_gr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grouper2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.grouper4.SuspendLayout();
            this.grouper1.SuspendLayout();
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
            this.grouper2.Location = new System.Drawing.Point(675, 307);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(174, 65);
            this.grouper2.TabIndex = 13;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.IsQcd = false;
            this.OTHER_DET.Location = new System.Drawing.Point(11, 31);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.QcdCondition = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(155, 27);
            this.OTHER_DET.TabIndex = 14;
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
            this.groupBox5.Location = new System.Drawing.Point(340, 307);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(331, 65);
            this.groupBox5.TabIndex = 10;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(191, 29);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(136, 26);
            this.dtp_deactive_from.TabIndex = 12;
            // 
            // chkProd_active
            // 
            this.chkProd_active.AutoSize = true;
            this.chkProd_active.Location = new System.Drawing.Point(5, 31);
            this.chkProd_active.Name = "chkProd_active";
            this.chkProd_active.Size = new System.Drawing.Size(137, 22);
            this.chkProd_active.TabIndex = 11;
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
            this.grpbxSearch.GroupTitle = "Product Group List";
            this.grpbxSearch.Location = new System.Drawing.Point(2, 29);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(333, 345);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(9, 316);
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
            this.pt_grp_id,
            this.pt_grp_nm,
            this.prod_desc});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 246;
            this.dgvSearch.Location = new System.Drawing.Point(6, 61);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(320, 246);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 320;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // pt_grp_id
            // 
            this.pt_grp_id.DataPropertyName = "pt_grp_id";
            this.pt_grp_id.HeaderText = "pt_grp_id";
            this.pt_grp_id.Name = "pt_grp_id";
            this.pt_grp_id.Visible = false;
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
            this.grouper4.Controls.Add(this.label4);
            this.grouper4.Controls.Add(this.cmbUOM);
            this.grouper4.Controls.Add(this.cmbStockable);
            this.grouper4.Controls.Add(this.label24);
            this.grouper4.Controls.Add(this.label27);
            this.grouper4.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper4.GroupImage = null;
            this.grouper4.GroupTitle = "Inventory";
            this.grouper4.Location = new System.Drawing.Point(340, 227);
            this.grouper4.Name = "grouper4";
            this.grouper4.Padding = new System.Windows.Forms.Padding(20);
            this.grouper4.PaintGroupBox = true;
            this.grouper4.RoundCorners = 1;
            this.grouper4.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper4.ShadowControl = false;
            this.grouper4.ShadowThickness = 1;
            this.grouper4.Size = new System.Drawing.Size(509, 73);
            this.grouper4.TabIndex = 7;
            // 
            // cmbUOM
            // 
            this.cmbUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUOM.FormattingEnabled = true;
            this.cmbUOM.Location = new System.Drawing.Point(145, 33);
            this.cmbUOM.Name = "cmbUOM";
            this.cmbUOM.Size = new System.Drawing.Size(112, 26);
            this.cmbUOM.TabIndex = 8;
            this.cmbUOM.Validating += new System.ComponentModel.CancelEventHandler(this.cmbUOM_Validating);
            // 
            // cmbStockable
            // 
            this.cmbStockable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockable.FormattingEnabled = true;
            this.cmbStockable.Location = new System.Drawing.Point(367, 32);
            this.cmbStockable.Name = "cmbStockable";
            this.cmbStockable.Size = new System.Drawing.Size(118, 26);
            this.cmbStockable.TabIndex = 9;
            this.cmbStockable.Validating += new System.ComponentModel.CancelEventHandler(this.cmbStockable_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(263, 37);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(98, 18);
            this.label24.TabIndex = 10;
            this.label24.Text = "Stockable";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 36);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(38, 18);
            this.label27.TabIndex = 7;
            this.label27.Text = "UOM";
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label18);
            this.grouper1.Controls.Add(this.cmProd_Type);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.txtProdGrDesc);
            this.grouper1.Controls.Add(this.txtProd_gr);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Product Group Details";
            this.grouper1.Location = new System.Drawing.Point(340, 28);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(509, 193);
            this.grouper1.TabIndex = 3;
            // 
            // cmProd_Type
            // 
            this.cmProd_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmProd_Type.FormattingEnabled = true;
            this.cmProd_Type.Location = new System.Drawing.Point(147, 154);
            this.cmProd_Type.Name = "cmProd_Type";
            this.cmProd_Type.Size = new System.Drawing.Size(319, 26);
            this.cmProd_Type.TabIndex = 6;
            this.cmProd_Type.Validating += new System.ComponentModel.CancelEventHandler(this.cmProd_Type_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Product Type";
            // 
            // txtProdGrDesc
            // 
            this.txtProdGrDesc.Location = new System.Drawing.Point(147, 83);
            this.txtProdGrDesc.Multiline = true;
            this.txtProdGrDesc.Name = "txtProdGrDesc";
            this.txtProdGrDesc.Size = new System.Drawing.Size(319, 60);
            this.txtProdGrDesc.TabIndex = 5;
            // 
            // txtProd_gr
            // 
            this.txtProd_gr.Location = new System.Drawing.Point(147, 37);
            this.txtProd_gr.Name = "txtProd_gr";
            this.txtProd_gr.Size = new System.Drawing.Size(319, 26);
            this.txtProd_gr.TabIndex = 4;
            this.txtProd_gr.Validating += new System.ComponentModel.CancelEventHandler(this.txtProd_gr_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 57);
            this.label3.TabIndex = 2;
            this.label3.Text = "Product Group Description";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Group Code";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(127, 42);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 16);
            this.label18.TabIndex = 129;
            this.label18.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(127, 159);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 130;
            this.label2.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(127, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 16);
            this.label4.TabIndex = 129;
            this.label4.Text = "*";
            // 
            // frmProductGroupMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(853, 376);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.grouper4);
            this.Controls.Add(this.grouper1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmProductGroupMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductGroupMast_FormClosed);
            this.Load += new System.EventHandler(this.frmProductGroupMast_Load);
            this.Enter += new System.EventHandler(this.frmProductGroupMast_Enter);
            this.Resize += new System.EventHandler(this.frmProductGroupMast_Resize);
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

        private Grouper grouper2;
        private PopupButton OTHER_DET;
        private Grouper groupBox5;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.CheckBox chkProd_active;
        private System.Windows.Forms.Label label14;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper grouper4;
        private System.Windows.Forms.ComboBox cmbUOM;
        private System.Windows.Forms.ComboBox cmbStockable;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private Grouper grouper1;
        private System.Windows.Forms.ComboBox cmProd_Type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProdGrDesc;
        private System.Windows.Forms.TextBox txtProd_gr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pt_grp_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pt_grp_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;


    }
}