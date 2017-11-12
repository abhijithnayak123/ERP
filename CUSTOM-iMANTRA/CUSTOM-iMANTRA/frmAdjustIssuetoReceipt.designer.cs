namespace CUSTOM_iMANTRA
{
    partial class frmAdjustIssuetoReceipt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblProd_nm = new System.Windows.Forms.Label();
            this.dgvAdjustLI = new System.Windows.Forms.DataGridView();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ac_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cons_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avail_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adj_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wastage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.days = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gre_180 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustLI)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDone);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.lblQty);
            this.panel1.Controls.Add(this.lblProd_nm);
            this.panel1.Controls.Add(this.dgvAdjustLI);
            this.panel1.Location = new System.Drawing.Point(0, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 337);
            this.panel1.TabIndex = 0;
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(789, 306);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(86, 27);
            this.btnDone.TabIndex = 4;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(691, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(95, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(258, 309);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(68, 18);
            this.lblQty.TabIndex = 2;
            this.lblQty.Text = "label2";
            // 
            // lblProd_nm
            // 
            this.lblProd_nm.AutoSize = true;
            this.lblProd_nm.Location = new System.Drawing.Point(13, 307);
            this.lblProd_nm.Name = "lblProd_nm";
            this.lblProd_nm.Size = new System.Drawing.Size(68, 18);
            this.lblProd_nm.TabIndex = 1;
            this.lblProd_nm.Text = "label1";
            // 
            // dgvAdjustLI
            // 
            this.dgvAdjustLI.AllowUserToAddRows = false;
            this.dgvAdjustLI.AllowUserToDeleteRows = false;
            this.dgvAdjustLI.AllowUserToResizeRows = false;
            this.dgvAdjustLI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvAdjustLI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdjustLI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sel,
            this.ref_tran_no,
            this.ref_tran_dt,
            this.ref_prod_nm,
            this.qty,
            this.tran_id,
            this.ref_tran_id,
            this.tran_cd,
            this.tran_dt,
            this.tran_no,
            this.ptserial,
            this.ref_tran_cd,
            this.ref_ptserial,
            this.prod_nm,
            this.ac_nm,
            this.cons_qty,
            this.avail_qty,
            this.adj_qty,
            this.wastage,
            this.days,
            this.gre_180});
            this.dgvAdjustLI.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAdjustLI.Location = new System.Drawing.Point(2, 3);
            this.dgvAdjustLI.Name = "dgvAdjustLI";
            this.dgvAdjustLI.RowHeadersVisible = false;
            this.dgvAdjustLI.Size = new System.Drawing.Size(873, 299);
            this.dgvAdjustLI.TabIndex = 0;
            this.dgvAdjustLI.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdjustLI_CellContentClick);
            this.dgvAdjustLI.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAdjustLI_CellValidating);
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
            this.ucToolBar1.Size = new System.Drawing.Size(878, 19);
            this.ucToolBar1.TabIndex = 7;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // sel
            // 
            this.sel.DataPropertyName = "sel";
            this.sel.HeaderText = "Issue No";
            this.sel.Name = "sel";
            this.sel.Width = 94;
            // 
            // ref_tran_no
            // 
            this.ref_tran_no.DataPropertyName = "ref_tran_no";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_no.DefaultCellStyle = dataGridViewCellStyle1;
            this.ref_tran_no.HeaderText = "Issue Number";
            this.ref_tran_no.Name = "ref_tran_no";
            this.ref_tran_no.ReadOnly = true;
            this.ref_tran_no.Width = 140;
            // 
            // ref_tran_dt
            // 
            this.ref_tran_dt.DataPropertyName = "ref_tran_dt";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_dt.DefaultCellStyle = dataGridViewCellStyle2;
            this.ref_tran_dt.HeaderText = "Issue Date";
            this.ref_tran_dt.Name = "ref_tran_dt";
            this.ref_tran_dt.ReadOnly = true;
            this.ref_tran_dt.Width = 86;
            // 
            // ref_prod_nm
            // 
            this.ref_prod_nm.DataPropertyName = "ref_prod_nm";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_prod_nm.DefaultCellStyle = dataGridViewCellStyle3;
            this.ref_prod_nm.HeaderText = "Issue Item";
            this.ref_prod_nm.Name = "ref_prod_nm";
            this.ref_prod_nm.ReadOnly = true;
            this.ref_prod_nm.Width = 86;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.qty.HeaderText = "Issued Quantity";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Width = 167;
            // 
            // tran_id
            // 
            this.tran_id.DataPropertyName = "tran_id";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tran_id.DefaultCellStyle = dataGridViewCellStyle5;
            this.tran_id.HeaderText = "tran_id";
            this.tran_id.Name = "tran_id";
            this.tran_id.ReadOnly = true;
            this.tran_id.Visible = false;
            this.tran_id.Width = 103;
            // 
            // ref_tran_id
            // 
            this.ref_tran_id.DataPropertyName = "ref_tran_id";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_id.DefaultCellStyle = dataGridViewCellStyle6;
            this.ref_tran_id.HeaderText = "ref_tran_id";
            this.ref_tran_id.Name = "ref_tran_id";
            this.ref_tran_id.ReadOnly = true;
            this.ref_tran_id.Visible = false;
            this.ref_tran_id.Width = 143;
            // 
            // tran_cd
            // 
            this.tran_cd.DataPropertyName = "tran_cd";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tran_cd.DefaultCellStyle = dataGridViewCellStyle7;
            this.tran_cd.HeaderText = "Transaction Code";
            this.tran_cd.Name = "tran_cd";
            this.tran_cd.ReadOnly = true;
            this.tran_cd.Visible = false;
            this.tran_cd.Width = 176;
            // 
            // tran_dt
            // 
            this.tran_dt.DataPropertyName = "tran_dt";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tran_dt.DefaultCellStyle = dataGridViewCellStyle8;
            this.tran_dt.HeaderText = "Date";
            this.tran_dt.Name = "tran_dt";
            this.tran_dt.ReadOnly = true;
            this.tran_dt.Visible = false;
            this.tran_dt.Width = 73;
            // 
            // tran_no
            // 
            this.tran_no.DataPropertyName = "tran_no";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tran_no.DefaultCellStyle = dataGridViewCellStyle9;
            this.tran_no.HeaderText = "Number";
            this.tran_no.Name = "tran_no";
            this.tran_no.ReadOnly = true;
            this.tran_no.Visible = false;
            this.tran_no.Width = 93;
            // 
            // ptserial
            // 
            this.ptserial.DataPropertyName = "ptserial";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ptserial.DefaultCellStyle = dataGridViewCellStyle10;
            this.ptserial.HeaderText = "ptserial";
            this.ptserial.Name = "ptserial";
            this.ptserial.ReadOnly = true;
            this.ptserial.Visible = false;
            this.ptserial.Width = 113;
            // 
            // ref_tran_cd
            // 
            this.ref_tran_cd.DataPropertyName = "ref_tran_cd";
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_tran_cd.DefaultCellStyle = dataGridViewCellStyle11;
            this.ref_tran_cd.HeaderText = "ref_tran_cd";
            this.ref_tran_cd.Name = "ref_tran_cd";
            this.ref_tran_cd.ReadOnly = true;
            this.ref_tran_cd.Visible = false;
            this.ref_tran_cd.Width = 143;
            // 
            // ref_ptserial
            // 
            this.ref_ptserial.DataPropertyName = "ref_ptserial";
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ref_ptserial.DefaultCellStyle = dataGridViewCellStyle12;
            this.ref_ptserial.HeaderText = "ref_ptserial";
            this.ref_ptserial.Name = "ref_ptserial";
            this.ref_ptserial.ReadOnly = true;
            this.ref_ptserial.Visible = false;
            this.ref_ptserial.Width = 153;
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.prod_nm.DefaultCellStyle = dataGridViewCellStyle13;
            this.prod_nm.HeaderText = "prod_nm";
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.ReadOnly = true;
            this.prod_nm.Visible = false;
            this.prod_nm.Width = 103;
            // 
            // ac_nm
            // 
            this.ac_nm.DataPropertyName = "ac_nm";
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ac_nm.DefaultCellStyle = dataGridViewCellStyle14;
            this.ac_nm.HeaderText = "party_nm";
            this.ac_nm.Name = "ac_nm";
            this.ac_nm.ReadOnly = true;
            this.ac_nm.Visible = false;
            this.ac_nm.Width = 113;
            // 
            // cons_qty
            // 
            this.cons_qty.DataPropertyName = "cons_qty";
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cons_qty.DefaultCellStyle = dataGridViewCellStyle15;
            this.cons_qty.HeaderText = "Consumed Issued Quantity";
            this.cons_qty.Name = "cons_qty";
            this.cons_qty.ReadOnly = true;
            this.cons_qty.Width = 248;
            // 
            // avail_qty
            // 
            this.avail_qty.DataPropertyName = "avail_qty";
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.avail_qty.DefaultCellStyle = dataGridViewCellStyle16;
            this.avail_qty.HeaderText = "Available Quantity";
            this.avail_qty.Name = "avail_qty";
            this.avail_qty.ReadOnly = true;
            this.avail_qty.Width = 194;
            // 
            // adj_qty
            // 
            this.adj_qty.DataPropertyName = "adj_qty";
            this.adj_qty.HeaderText = "Adjusting Quantity";
            this.adj_qty.Name = "adj_qty";
            this.adj_qty.Width = 194;
            // 
            // wastage
            // 
            this.wastage.DataPropertyName = "wastage";
            this.wastage.HeaderText = "Wastage";
            this.wastage.Name = "wastage";
            this.wastage.Width = 103;
            // 
            // days
            // 
            this.days.DataPropertyName = "days";
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.WhiteSmoke;
            this.days.DefaultCellStyle = dataGridViewCellStyle17;
            this.days.HeaderText = "Days";
            this.days.Name = "days";
            this.days.ReadOnly = true;
            this.days.Width = 73;
            // 
            // gre_180
            // 
            this.gre_180.DataPropertyName = "gre_180";
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gre_180.DefaultCellStyle = dataGridViewCellStyle18;
            this.gre_180.HeaderText = ">180";
            this.gre_180.Name = "gre_180";
            this.gre_180.ReadOnly = true;
            this.gre_180.Width = 73;
            // 
            // frmAdjustIssuetoReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(878, 357);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdjustIssuetoReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAdjustIssuetoReceipt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustLI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAdjustLI;
        private PopupButton btnDone;
        private PopupButton btnCancel;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblProd_nm;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_ptserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ac_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cons_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn avail_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn adj_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn wastage;
        private System.Windows.Forms.DataGridViewTextBoxColumn days;
        private System.Windows.Forms.DataGridViewTextBoxColumn gre_180;
    }
}