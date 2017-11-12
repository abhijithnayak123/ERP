namespace CUSTOM_iMANTRA
{
    partial class frmBOM_with_WorkOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.groupBox2 = new CUSTOM_iMANTRA.Grouper();
            this.btnRemove = new CUSTOM_iMANTRA.PopupButton();
            this.btnAdd = new CUSTOM_iMANTRA.PopupButton();
            this.dgvRM_For_BOM = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new CUSTOM_iMANTRA.Grouper();
            this.dgvBOM = new System.Windows.Forms.DataGridView();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.woboid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bom_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bom_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bom_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Req_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRM_For_BOM)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(510, 456);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(87, 27);
            this.btnDone.TabIndex = 5;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(417, 456);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.BorderThickness = 1F;
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.dgvRM_For_BOM);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "Raw Materials/Semi Finished Details";
            this.groupBox2.Location = new System.Drawing.Point(4, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox2.PaintGroupBox = true;
            this.groupBox2.RoundCorners = 1;
            this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 1;
            this.groupBox2.Size = new System.Drawing.Size(596, 214);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Dispddlfields = "";
            this.btnRemove.Location = new System.Drawing.Point(93, 187);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Primaryddl = "";
            this.btnRemove.Query_con = "";
            this.btnRemove.Reftbltran_cd = "";
            this.btnRemove.Size = new System.Drawing.Size(85, 25);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Tbl_nm = "";
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dispddlfields = "";
            this.btnAdd.Location = new System.Drawing.Point(7, 187);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primaryddl = "";
            this.btnAdd.Query_con = "";
            this.btnAdd.Reftbltran_cd = "";
            this.btnAdd.Size = new System.Drawing.Size(77, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Tbl_nm = "";
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvRM_For_BOM
            // 
            this.dgvRM_For_BOM.AllowUserToAddRows = false;
            this.dgvRM_For_BOM.AllowUserToDeleteRows = false;
            this.dgvRM_For_BOM.AllowUserToResizeColumns = false;
            this.dgvRM_For_BOM.AllowUserToResizeRows = false;
            this.dgvRM_For_BOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRM_For_BOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRM_For_BOM.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvRM_For_BOM.Location = new System.Drawing.Point(5, 28);
            this.dgvRM_For_BOM.Name = "dgvRM_For_BOM";
            this.dgvRM_For_BOM.RowHeadersVisible = false;
            this.dgvRM_For_BOM.Size = new System.Drawing.Size(587, 153);
            this.dgvRM_For_BOM.TabIndex = 1;
            this.dgvRM_For_BOM.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRM_For_BOM_CellValidating);
            this.dgvRM_For_BOM.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvRM_For_BOM_EditingControlShowing);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox1.BorderColor = System.Drawing.Color.Black;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.dgvBOM);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "Finished/Semi Finished Details";
            this.groupBox1.Location = new System.Drawing.Point(3, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            this.groupBox1.Size = new System.Drawing.Size(597, 208);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // dgvBOM
            // 
            this.dgvBOM.AllowUserToAddRows = false;
            this.dgvBOM.AllowUserToDeleteRows = false;
            this.dgvBOM.AllowUserToOrderColumns = true;
            this.dgvBOM.AllowUserToResizeColumns = false;
            this.dgvBOM.AllowUserToResizeRows = false;
            this.dgvBOM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBOM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sel,
            this.woboid,
            this.bomid,
            this.bom_no,
            this.bom_item,
            this.bom_qty,
            this.Req_qty,
            this.tran_cd,
            this.tran_id,
            this.tran_no,
            this.ptserial});
            this.dgvBOM.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBOM.Location = new System.Drawing.Point(5, 28);
            this.dgvBOM.Name = "dgvBOM";
            this.dgvBOM.RowHeadersVisible = false;
            this.dgvBOM.Size = new System.Drawing.Size(588, 176);
            this.dgvBOM.TabIndex = 0;
            this.dgvBOM.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBOM_CellClick);
            this.dgvBOM.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBOM_CellContentClick);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = false;
            this.ucToolBar1.Minimize = false;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(602, 19);
            this.ucToolBar1.TabIndex = 6;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // sel
            // 
            this.sel.DataPropertyName = "sel";
            this.sel.HeaderText = "Select";
            this.sel.Name = "sel";
            // 
            // woboid
            // 
            this.woboid.DataPropertyName = "woboid";
            this.woboid.HeaderText = "primary key";
            this.woboid.Name = "woboid";
            this.woboid.Visible = false;
            // 
            // bomid
            // 
            this.bomid.DataPropertyName = "bomid";
            this.bomid.HeaderText = "BOM ID";
            this.bomid.Name = "bomid";
            this.bomid.Visible = false;
            // 
            // bom_no
            // 
            this.bom_no.DataPropertyName = "bom_no";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bom_no.DefaultCellStyle = dataGridViewCellStyle1;
            this.bom_no.HeaderText = "BOM NO";
            this.bom_no.Name = "bom_no";
            this.bom_no.ReadOnly = true;
            // 
            // bom_item
            // 
            this.bom_item.DataPropertyName = "bom_item";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bom_item.DefaultCellStyle = dataGridViewCellStyle2;
            this.bom_item.HeaderText = "Product Name";
            this.bom_item.Name = "bom_item";
            this.bom_item.ReadOnly = true;
            // 
            // bom_qty
            // 
            this.bom_qty.DataPropertyName = "bom_qty";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bom_qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.bom_qty.HeaderText = "BOM Quantity";
            this.bom_qty.Name = "bom_qty";
            this.bom_qty.ReadOnly = true;
            // 
            // Req_qty
            // 
            this.Req_qty.DataPropertyName = "Req_qty";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Req_qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.Req_qty.HeaderText = "Required Quantity";
            this.Req_qty.Name = "Req_qty";
            this.Req_qty.ReadOnly = true;
            // 
            // tran_cd
            // 
            this.tran_cd.DataPropertyName = "tran_cd";
            this.tran_cd.HeaderText = "tran_cd";
            this.tran_cd.Name = "tran_cd";
            this.tran_cd.Visible = false;
            // 
            // tran_id
            // 
            this.tran_id.DataPropertyName = "tran_id";
            this.tran_id.HeaderText = "tran_id";
            this.tran_id.Name = "tran_id";
            this.tran_id.Visible = false;
            // 
            // tran_no
            // 
            this.tran_no.DataPropertyName = "tran_no";
            this.tran_no.HeaderText = "tran_no";
            this.tran_no.Name = "tran_no";
            this.tran_no.Visible = false;
            // 
            // ptserial
            // 
            this.ptserial.DataPropertyName = "ptserial";
            this.ptserial.HeaderText = "ptserial";
            this.ptserial.Name = "ptserial";
            this.ptserial.Visible = false;
            // 
            // frmBOM_with_WorkOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(602, 487);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBOM_with_WorkOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBOM_with_WorkOrder_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRM_For_BOM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRM_For_BOM;
        private Grouper groupBox1;
        private System.Windows.Forms.DataGridView dgvBOM;
        private Grouper groupBox2;
        private PopupButton btnCancel;
        private PopupButton btnDone;
        private PopupButton btnRemove;
        private PopupButton btnAdd;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn woboid;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomid;
        private System.Windows.Forms.DataGridViewTextBoxColumn bom_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn bom_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn bom_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Req_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptserial;

    }
}