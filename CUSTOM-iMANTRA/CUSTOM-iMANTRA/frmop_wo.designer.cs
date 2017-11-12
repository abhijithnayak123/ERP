namespace CUSTOM_iMANTRA
{
    partial class frmop_wo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gboxfinish = new CUSTOM_iMANTRA.Grouper();
            this.dgvopwo = new System.Windows.Forms.DataGridView();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.op_wo_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_prod_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alloc_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bal_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avail_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bom_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gboxfinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvopwo)).BeginInit();
            this.SuspendLayout();
            // 
            // gboxfinish
            // 
            this.gboxfinish.BackgroundColor = System.Drawing.Color.Transparent;
            this.gboxfinish.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gboxfinish.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.gboxfinish.BorderColor = System.Drawing.Color.Black;
            this.gboxfinish.BorderThickness = 1F;
            this.gboxfinish.Controls.Add(this.dgvopwo);
            this.gboxfinish.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gboxfinish.GroupImage = null;
            this.gboxfinish.GroupTitle = "Finished/Semi Finished Material Details";
            this.gboxfinish.Location = new System.Drawing.Point(2, 21);
            this.gboxfinish.Name = "gboxfinish";
            this.gboxfinish.Padding = new System.Windows.Forms.Padding(20);
            this.gboxfinish.PaintGroupBox = true;
            this.gboxfinish.RoundCorners = 1;
            this.gboxfinish.ShadowColor = System.Drawing.Color.DarkGray;
            this.gboxfinish.ShadowControl = false;
            this.gboxfinish.ShadowThickness = 1;
            this.gboxfinish.Size = new System.Drawing.Size(880, 214);
            this.gboxfinish.TabIndex = 0;
            this.gboxfinish.TabStop = false;
            // 
            // dgvopwo
            // 
            this.dgvopwo.AllowUserToAddRows = false;
            this.dgvopwo.AllowUserToDeleteRows = false;
            this.dgvopwo.AllowUserToOrderColumns = true;
            this.dgvopwo.AllowUserToResizeRows = false;
            this.dgvopwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvopwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvopwo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel,
            this.op_wo_id,
            this.ref_tran_id,
            this.ref_tran_cd,
            this.ref_tran_no,
            this.ref_tran_dt,
            this.ref_prod_nm,
            this.ref_prod_cd,
            this.ref_ptserial,
            this.ref_tran_qty,
            this.alloc_qty,
            this.bal_qty,
            this.avail_qty,
            this.qty,
            this.bom_qty});
            this.dgvopwo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvopwo.Location = new System.Drawing.Point(5, 29);
            this.dgvopwo.Name = "dgvopwo";
            this.dgvopwo.RowHeadersVisible = false;
            this.dgvopwo.Size = new System.Drawing.Size(871, 180);
            this.dgvopwo.TabIndex = 0;
            this.dgvopwo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvopwo_CellContentClick_1);
            this.dgvopwo.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvwo_CellValidating);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(798, 241);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(85, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(717, 241);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(75, 26);
            this.btnDone.TabIndex = 2;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
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
            this.ucToolBar1.Size = new System.Drawing.Size(885, 19);
            this.ucToolBar1.TabIndex = 7;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // Sel
            // 
            this.Sel.HeaderText = "Select";
            this.Sel.Name = "Sel";
            // 
            // op_wo_id
            // 
            this.op_wo_id.DataPropertyName = "op_wo_id";
            this.op_wo_id.HeaderText = "op_wo_id";
            this.op_wo_id.Name = "op_wo_id";
            this.op_wo_id.Visible = false;
            // 
            // ref_tran_id
            // 
            this.ref_tran_id.DataPropertyName = "ref_tran_id";
            this.ref_tran_id.HeaderText = "wo_id";
            this.ref_tran_id.Name = "ref_tran_id";
            this.ref_tran_id.Visible = false;
            // 
            // ref_tran_cd
            // 
            this.ref_tran_cd.DataPropertyName = "ref_tran_cd";
            this.ref_tran_cd.HeaderText = "wo_cd";
            this.ref_tran_cd.Name = "ref_tran_cd";
            this.ref_tran_cd.Visible = false;
            // 
            // ref_tran_no
            // 
            this.ref_tran_no.DataPropertyName = "ref_tran_no";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_no.DefaultCellStyle = dataGridViewCellStyle1;
            this.ref_tran_no.HeaderText = "Work Order No.";
            this.ref_tran_no.Name = "ref_tran_no";
            this.ref_tran_no.ReadOnly = true;
            // 
            // ref_tran_dt
            // 
            this.ref_tran_dt.DataPropertyName = "ref_tran_dt";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_dt.DefaultCellStyle = dataGridViewCellStyle2;
            this.ref_tran_dt.HeaderText = "Work Order Date";
            this.ref_tran_dt.Name = "ref_tran_dt";
            this.ref_tran_dt.ReadOnly = true;
            // 
            // ref_prod_nm
            // 
            this.ref_prod_nm.DataPropertyName = "ref_prod_nm";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_prod_nm.DefaultCellStyle = dataGridViewCellStyle3;
            this.ref_prod_nm.HeaderText = "Product Name";
            this.ref_prod_nm.Name = "ref_prod_nm";
            this.ref_prod_nm.ReadOnly = true;
            // 
            // ref_prod_cd
            // 
            this.ref_prod_cd.DataPropertyName = "ref_prod_cd";
            this.ref_prod_cd.HeaderText = "ref_prod_cd";
            this.ref_prod_cd.Name = "ref_prod_cd";
            this.ref_prod_cd.Visible = false;
            // 
            // ref_ptserial
            // 
            this.ref_ptserial.DataPropertyName = "ref_ptserial";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_ptserial.DefaultCellStyle = dataGridViewCellStyle4;
            this.ref_ptserial.HeaderText = "ptserial";
            this.ref_ptserial.Name = "ref_ptserial";
            this.ref_ptserial.ReadOnly = true;
            this.ref_ptserial.Visible = false;
            // 
            // ref_tran_qty
            // 
            this.ref_tran_qty.DataPropertyName = "ref_tran_qty";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.ref_tran_qty.HeaderText = "Work Order Qty";
            this.ref_tran_qty.Name = "ref_tran_qty";
            this.ref_tran_qty.ReadOnly = true;
            // 
            // alloc_qty
            // 
            this.alloc_qty.DataPropertyName = "alloc_qty";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.alloc_qty.DefaultCellStyle = dataGridViewCellStyle6;
            this.alloc_qty.HeaderText = "Executed Qty";
            this.alloc_qty.Name = "alloc_qty";
            this.alloc_qty.ReadOnly = true;
            // 
            // bal_qty
            // 
            this.bal_qty.DataPropertyName = "bal_qty";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bal_qty.DefaultCellStyle = dataGridViewCellStyle7;
            this.bal_qty.HeaderText = "Balance Qty";
            this.bal_qty.Name = "bal_qty";
            this.bal_qty.ReadOnly = true;
            // 
            // avail_qty
            // 
            this.avail_qty.DataPropertyName = "avail_qty";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.avail_qty.DefaultCellStyle = dataGridViewCellStyle8;
            this.avail_qty.HeaderText = "RMI Qty";
            this.avail_qty.Name = "avail_qty";
            this.avail_qty.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "Qty";
            this.qty.Name = "qty";
            // 
            // bom_qty
            // 
            this.bom_qty.DataPropertyName = "bom_qty";
            this.bom_qty.HeaderText = "BOM Qty";
            this.bom_qty.Name = "bom_qty";
            this.bom_qty.Visible = false;
            // 
            // frmop_wo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(885, 272);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gboxfinish);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmop_wo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmip_wo_Load);
            this.gboxfinish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvopwo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper gboxfinish;
        private PopupButton btnCancel;
        private PopupButton btnDone;
        private System.Windows.Forms.DataGridView dgvopwo;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn op_wo_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_prod_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_ptserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn alloc_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn bal_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn avail_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn bom_qty;
    }
}