namespace CUSTOM_iMANTRA
{
    partial class frmAccountAlloc
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
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblProd_nm = new System.Windows.Forms.Label();
            this.dgvAccountAlloc = new System.Windows.Forms.DataGridView();
            this.alllocating_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_acserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.net_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allocated_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pending_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin_yr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(870, 26);
            this.ucToolBar1.TabIndex = 8;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(781, 239);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(86, 27);
            this.btnDone.TabIndex = 13;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(683, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(95, 27);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(250, 244);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(68, 18);
            this.lblQty.TabIndex = 11;
            this.lblQty.Text = "label2";
            // 
            // lblProd_nm
            // 
            this.lblProd_nm.AutoSize = true;
            this.lblProd_nm.Location = new System.Drawing.Point(5, 244);
            this.lblProd_nm.Name = "lblProd_nm";
            this.lblProd_nm.Size = new System.Drawing.Size(68, 18);
            this.lblProd_nm.TabIndex = 10;
            this.lblProd_nm.Text = "label1";
            // 
            // dgvAccountAlloc
            // 
            this.dgvAccountAlloc.AllowUserToAddRows = false;
            this.dgvAccountAlloc.AllowUserToDeleteRows = false;
            this.dgvAccountAlloc.AllowUserToResizeRows = false;
            this.dgvAccountAlloc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvAccountAlloc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountAlloc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alllocating_amt,
            this.ref_tran_cd,
            this.ref_tran_dt,
            this.ref_tran_no,
            this.ref_acserial,
            this.net_amt,
            this.allocated_amt,
            this.pending_amt,
            this.fin_yr,
            this.ref_tran_id});
            this.dgvAccountAlloc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAccountAlloc.Location = new System.Drawing.Point(1, 28);
            this.dgvAccountAlloc.Name = "dgvAccountAlloc";
            this.dgvAccountAlloc.RowHeadersVisible = false;
            this.dgvAccountAlloc.Size = new System.Drawing.Size(866, 207);
            this.dgvAccountAlloc.TabIndex = 9;
            this.dgvAccountAlloc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccountAlloc_CellContentClick);
            this.dgvAccountAlloc.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAccountAlloc_CellValidating);
            // 
            // alllocating_amt
            // 
            this.alllocating_amt.DataPropertyName = "alllocating_amt";
            this.alllocating_amt.HeaderText = "Allocating Amount";
            this.alllocating_amt.Name = "alllocating_amt";
            this.alllocating_amt.Width = 185;
            // 
            // ref_tran_cd
            // 
            this.ref_tran_cd.DataPropertyName = "ref_tran_cd";
            this.ref_tran_cd.HeaderText = "Transaction Code";
            this.ref_tran_cd.Name = "ref_tran_cd";
            this.ref_tran_cd.ReadOnly = true;
            this.ref_tran_cd.Width = 176;
            // 
            // ref_tran_dt
            // 
            this.ref_tran_dt.DataPropertyName = "ref_tran_dt";
            this.ref_tran_dt.HeaderText = "Date";
            this.ref_tran_dt.Name = "ref_tran_dt";
            this.ref_tran_dt.ReadOnly = true;
            this.ref_tran_dt.Width = 73;
            // 
            // ref_tran_no
            // 
            this.ref_tran_no.DataPropertyName = "ref_tran_no";
            this.ref_tran_no.HeaderText = "Transaction No.";
            this.ref_tran_no.Name = "ref_tran_no";
            this.ref_tran_no.ReadOnly = true;
            this.ref_tran_no.Width = 167;
            // 
            // ref_acserial
            // 
            this.ref_acserial.DataPropertyName = "ref_acserial";
            this.ref_acserial.HeaderText = "Serial No.";
            this.ref_acserial.Name = "ref_acserial";
            this.ref_acserial.ReadOnly = true;
            this.ref_acserial.Width = 95;
            // 
            // net_amt
            // 
            this.net_amt.DataPropertyName = "net_amt";
            this.net_amt.HeaderText = "Amount";
            this.net_amt.Name = "net_amt";
            this.net_amt.ReadOnly = true;
            this.net_amt.Width = 93;
            // 
            // allocated_amt
            // 
            this.allocated_amt.DataPropertyName = "allocated_amt";
            this.allocated_amt.HeaderText = "Allocated Amount";
            this.allocated_amt.Name = "allocated_amt";
            this.allocated_amt.ReadOnly = true;
            this.allocated_amt.Width = 176;
            // 
            // pending_amt
            // 
            this.pending_amt.DataPropertyName = "pending_amt";
            this.pending_amt.HeaderText = "Balance";
            this.pending_amt.Name = "pending_amt";
            this.pending_amt.ReadOnly = true;
            this.pending_amt.Width = 103;
            // 
            // fin_yr
            // 
            this.fin_yr.DataPropertyName = "fin_yr";
            this.fin_yr.HeaderText = "Fiscal Year";
            this.fin_yr.Name = "fin_yr";
            this.fin_yr.ReadOnly = true;
            this.fin_yr.Width = 95;
            // 
            // ref_tran_id
            // 
            this.ref_tran_id.DataPropertyName = "ref_tran_id";
            this.ref_tran_id.HeaderText = "Ref_tran_id";
            this.ref_tran_id.Name = "ref_tran_id";
            this.ref_tran_id.ReadOnly = true;
            this.ref_tran_id.Visible = false;
            this.ref_tran_id.Width = 143;
            // 
            // frmAccountAlloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(870, 270);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblProd_nm);
            this.Controls.Add(this.dgvAccountAlloc);
            this.Controls.Add(this.ucToolBar1);
            this.Name = "frmAccountAlloc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAccountAlloc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCToolBar ucToolBar1;
        private PopupButton btnDone;
        private PopupButton btnCancel;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblProd_nm;
        private System.Windows.Forms.DataGridView dgvAccountAlloc;
        private System.Windows.Forms.DataGridViewTextBoxColumn alllocating_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_acserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn net_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn allocated_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn pending_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_yr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
    }
}