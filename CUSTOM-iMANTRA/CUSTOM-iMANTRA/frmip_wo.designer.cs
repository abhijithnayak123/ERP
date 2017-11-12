namespace CUSTOM_iMANTRA
{
    partial class frmip_wo
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
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.gboxrm = new CUSTOM_iMANTRA.Grouper();
            this.dgvworm = new System.Windows.Forms.DataGridView();
            this.gboxfinish = new CUSTOM_iMANTRA.Grouper();
            this.dgvwo = new System.Windows.Forms.DataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bomid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bom_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_prod_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issue_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issued_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gboxrm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvworm)).BeginInit();
            this.gboxfinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwo)).BeginInit();
            this.SuspendLayout();
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
            this.ucToolBar1.Size = new System.Drawing.Size(769, 19);
            this.ucToolBar1.TabIndex = 7;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(595, 440);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(77, 25);
            this.btnDone.TabIndex = 3;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(678, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gboxrm
            // 
            this.gboxrm.BackgroundColor = System.Drawing.Color.Transparent;
            this.gboxrm.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gboxrm.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.gboxrm.BorderColor = System.Drawing.Color.Black;
            this.gboxrm.BorderThickness = 1F;
            this.gboxrm.Controls.Add(this.dgvworm);
            this.gboxrm.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gboxrm.GroupImage = null;
            this.gboxrm.GroupTitle = "Required Material Details";
            this.gboxrm.Location = new System.Drawing.Point(1, 229);
            this.gboxrm.Name = "gboxrm";
            this.gboxrm.Padding = new System.Windows.Forms.Padding(20);
            this.gboxrm.PaintGroupBox = true;
            this.gboxrm.RoundCorners = 1;
            this.gboxrm.ShadowColor = System.Drawing.Color.DarkGray;
            this.gboxrm.ShadowControl = false;
            this.gboxrm.ShadowThickness = 1;
            this.gboxrm.Size = new System.Drawing.Size(762, 209);
            this.gboxrm.TabIndex = 1;
            this.gboxrm.TabStop = false;
            // 
            // dgvworm
            // 
            this.dgvworm.AllowUserToAddRows = false;
            this.dgvworm.AllowUserToDeleteRows = false;
            this.dgvworm.AllowUserToOrderColumns = true;
            this.dgvworm.AllowUserToResizeRows = false;
            this.dgvworm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvworm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvworm.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvworm.Location = new System.Drawing.Point(5, 29);
            this.dgvworm.Name = "dgvworm";
            this.dgvworm.RowHeadersVisible = false;
            this.dgvworm.Size = new System.Drawing.Size(753, 174);
            this.dgvworm.TabIndex = 0;
            this.dgvworm.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvworm_CellContentClick);
            this.dgvworm.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvworm_CellValidating);
            // 
            // gboxfinish
            // 
            this.gboxfinish.BackgroundColor = System.Drawing.Color.Transparent;
            this.gboxfinish.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.gboxfinish.BackgroundGradientMode = CUSTOM_iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.gboxfinish.BorderColor = System.Drawing.Color.Black;
            this.gboxfinish.BorderThickness = 1F;
            this.gboxfinish.Controls.Add(this.dgvwo);
            this.gboxfinish.CustomGroupBoxColor = System.Drawing.Color.White;
            this.gboxfinish.GroupImage = null;
            this.gboxfinish.GroupTitle = "Finished/Semi Finished Materials Details";
            this.gboxfinish.Location = new System.Drawing.Point(2, 21);
            this.gboxfinish.Name = "gboxfinish";
            this.gboxfinish.Padding = new System.Windows.Forms.Padding(20);
            this.gboxfinish.PaintGroupBox = true;
            this.gboxfinish.RoundCorners = 1;
            this.gboxfinish.ShadowColor = System.Drawing.Color.DarkGray;
            this.gboxfinish.ShadowControl = false;
            this.gboxfinish.ShadowThickness = 1;
            this.gboxfinish.Size = new System.Drawing.Size(761, 204);
            this.gboxfinish.TabIndex = 0;
            this.gboxfinish.TabStop = false;
            // 
            // dgvwo
            // 
            this.dgvwo.AllowUserToAddRows = false;
            this.dgvwo.AllowUserToDeleteRows = false;
            this.dgvwo.AllowUserToOrderColumns = true;
            this.dgvwo.AllowUserToResizeRows = false;
            this.dgvwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvwo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel,
            this.ref_tran_id,
            this.ref_tran_cd,
            this.ref_tran_no,
            this.ref_tran_dt,
            this.ref_prod_nm,
            this.ref_tran_qty,
            this.bomid,
            this.bom_no,
            this.ref_prod_cd,
            this.ref_ptserial,
            this.issue_qty,
            this.issued_qty});
            this.dgvwo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvwo.Location = new System.Drawing.Point(5, 28);
            this.dgvwo.Name = "dgvwo";
            this.dgvwo.RowHeadersVisible = false;
            this.dgvwo.Size = new System.Drawing.Size(752, 171);
            this.dgvwo.TabIndex = 0;
            this.dgvwo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvwo_CellClick);
            this.dgvwo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvwo_CellContentClick);
            this.dgvwo.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvwo_CellValidating);
            // 
            // Sel
            // 
            this.Sel.DataPropertyName = "Sel";
            this.Sel.HeaderText = "Select";
            this.Sel.Name = "Sel";
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
            // ref_tran_qty
            // 
            this.ref_tran_qty.DataPropertyName = "ref_tran_qty";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.ref_tran_qty.HeaderText = "Work Order Qty";
            this.ref_tran_qty.Name = "ref_tran_qty";
            this.ref_tran_qty.ReadOnly = true;
            // 
            // bomid
            // 
            this.bomid.DataPropertyName = "bomid";
            this.bomid.HeaderText = "Bom Id";
            this.bomid.Name = "bomid";
            this.bomid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.bomid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bomid.Visible = false;
            // 
            // bom_no
            // 
            this.bom_no.DataPropertyName = "bom_no";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.bom_no.DefaultCellStyle = dataGridViewCellStyle5;
            this.bom_no.HeaderText = "Bom No";
            this.bom_no.Name = "bom_no";
            this.bom_no.ReadOnly = true;
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
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_ptserial.DefaultCellStyle = dataGridViewCellStyle6;
            this.ref_ptserial.HeaderText = "ptserial";
            this.ref_ptserial.Name = "ref_ptserial";
            this.ref_ptserial.ReadOnly = true;
            this.ref_ptserial.Visible = false;
            // 
            // issue_qty
            // 
            this.issue_qty.DataPropertyName = "issue_qty";
            this.issue_qty.HeaderText = "Issuing Qty";
            this.issue_qty.Name = "issue_qty";
            // 
            // issued_qty
            // 
            this.issued_qty.DataPropertyName = "issued_qty";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.issued_qty.DefaultCellStyle = dataGridViewCellStyle7;
            this.issued_qty.HeaderText = "Already Issued Qty";
            this.issued_qty.Name = "issued_qty";
            this.issued_qty.ReadOnly = true;
            // 
            // frmip_wo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(769, 469);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gboxrm);
            this.Controls.Add(this.gboxfinish);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmip_wo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmip_wo_Load);
            this.gboxrm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvworm)).EndInit();
            this.gboxfinish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvwo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper gboxfinish;
        private Grouper gboxrm;
        private PopupButton btnCancel;
        private PopupButton btnDone;
        private System.Windows.Forms.DataGridView dgvwo;
        private System.Windows.Forms.DataGridView dgvworm;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn bomid;
        private System.Windows.Forms.DataGridViewTextBoxColumn bom_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_prod_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_ptserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn issue_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn issued_qty;
    }
}