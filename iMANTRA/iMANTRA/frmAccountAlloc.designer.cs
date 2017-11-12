namespace iMANTRA
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
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAllocatedAmt = new System.Windows.Forms.Label();
            this.lblLedgerAcc = new System.Windows.Forms.Label();
            this.lblTotLedgerAmt = new System.Windows.Forms.Label();
            this.lblAc_nm = new System.Windows.Forms.Label();
            this.dgvAccountAlloc = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDone = new iMANTRA.PopupButton();
            this.btnCancel = new iMANTRA.PopupButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.ac_alloc_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allocating_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bill_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bill_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i_allocating = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.net_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allocated_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i_allocated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pending_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.i_pend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_dt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_acserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fin_yr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_sr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(499, 272);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(108, 18);
            this.lblBalance.TabIndex = 23;
            this.lblBalance.Text = "Balance : ";
            // 
            // lblAllocatedAmt
            // 
            this.lblAllocatedAmt.AutoSize = true;
            this.lblAllocatedAmt.Location = new System.Drawing.Point(256, 272);
            this.lblAllocatedAmt.Name = "lblAllocatedAmt";
            this.lblAllocatedAmt.Size = new System.Drawing.Size(168, 18);
            this.lblAllocatedAmt.TabIndex = 22;
            this.lblAllocatedAmt.Text = "Allocated Amt : ";
            // 
            // lblLedgerAcc
            // 
            this.lblLedgerAcc.AutoSize = true;
            this.lblLedgerAcc.Location = new System.Drawing.Point(499, 243);
            this.lblLedgerAcc.Name = "lblLedgerAcc";
            this.lblLedgerAcc.Size = new System.Drawing.Size(148, 18);
            this.lblLedgerAcc.TabIndex = 21;
            this.lblLedgerAcc.Text = "Ledger Name : ";
            // 
            // lblTotLedgerAmt
            // 
            this.lblTotLedgerAmt.AutoSize = true;
            this.lblTotLedgerAmt.Location = new System.Drawing.Point(12, 273);
            this.lblTotLedgerAmt.Name = "lblTotLedgerAmt";
            this.lblTotLedgerAmt.Size = new System.Drawing.Size(128, 18);
            this.lblTotLedgerAmt.TabIndex = 18;
            this.lblTotLedgerAmt.Text = "Total Amt : ";
            // 
            // lblAc_nm
            // 
            this.lblAc_nm.AutoSize = true;
            this.lblAc_nm.Location = new System.Drawing.Point(12, 243);
            this.lblAc_nm.Name = "lblAc_nm";
            this.lblAc_nm.Size = new System.Drawing.Size(138, 18);
            this.lblAc_nm.TabIndex = 17;
            this.lblAc_nm.Text = "Party Name : ";
            // 
            // dgvAccountAlloc
            // 
            this.dgvAccountAlloc.AllowUserToAddRows = false;
            this.dgvAccountAlloc.AllowUserToDeleteRows = false;
            this.dgvAccountAlloc.AllowUserToResizeRows = false;
            this.dgvAccountAlloc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvAccountAlloc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountAlloc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ac_alloc_id,
            this.tran_type,
            this.allocating_amt,
            this.bill_no,
            this.bill_dt,
            this.i_allocating,
            this.ref_tran_cd,
            this.net_amt,
            this.allocated_amt,
            this.i_allocated,
            this.pending_amt,
            this.i_pend,
            this.ref_tran_dt,
            this.ref_tran_no,
            this.ref_acserial,
            this.fin_yr,
            this.ref_tran_id,
            this.ref_tran_sr});
            this.dgvAccountAlloc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAccountAlloc.Location = new System.Drawing.Point(1, 28);
            this.dgvAccountAlloc.Margin = new System.Windows.Forms.Padding(1);
            this.dgvAccountAlloc.Name = "dgvAccountAlloc";
            this.dgvAccountAlloc.RowHeadersVisible = false;
            this.dgvAccountAlloc.Size = new System.Drawing.Size(935, 207);
            this.dgvAccountAlloc.TabIndex = 9;
            this.dgvAccountAlloc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccountAlloc_CellContentClick);
            this.dgvAccountAlloc.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccountAlloc_CellValidated);
            this.dgvAccountAlloc.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAccountAlloc_CellValidating);
            this.dgvAccountAlloc.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAccountAlloc_EditingControlShowing);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ac_alloc_id";
            this.dataGridViewTextBoxColumn1.HeaderText = "PRIMARY KEY";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 124;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "tran_type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 104;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "allocating_amt";
            this.dataGridViewTextBoxColumn3.HeaderText = "Allocating Amount";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 107;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ref_tran_cd";
            this.dataGridViewTextBoxColumn4.HeaderText = "Transaction Code";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 106;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ref_tran_dt";
            this.dataGridViewTextBoxColumn5.HeaderText = "Date";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 55;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ref_tran_no";
            this.dataGridViewTextBoxColumn6.HeaderText = "Transaction No.";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 99;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "ref_acserial";
            this.dataGridViewTextBoxColumn7.HeaderText = "Serial No.";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 72;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "net_amt";
            this.dataGridViewTextBoxColumn8.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 68;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "allocated_amt";
            this.dataGridViewTextBoxColumn9.HeaderText = "Allocated Amount";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.dataGridViewTextBoxColumn9.Width = 176;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "i_allocated";
            this.dataGridViewTextBoxColumn10.HeaderText = "Allocated Amount";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 105;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "pending_amt";
            this.dataGridViewTextBoxColumn11.HeaderText = "Balance";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 71;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "i_pend";
            this.dataGridViewTextBoxColumn12.HeaderText = "Pending Amount";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 104;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "fin_yr";
            this.dataGridViewTextBoxColumn13.HeaderText = "Fiscal Year";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 78;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "ref_tran_id";
            this.dataGridViewTextBoxColumn14.HeaderText = "Ref_tran_id";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Visible = false;
            this.dataGridViewTextBoxColumn14.Width = 143;
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.GradientBottom = System.Drawing.Color.Gray;
            this.btnDone.GradientTop = System.Drawing.SystemColors.Control;
            this.btnDone.IsQcd = false;
            this.btnDone.Location = new System.Drawing.Point(849, 268);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.QcdCondition = "";
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
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.GradientBottom = System.Drawing.Color.Gray;
            this.btnCancel.GradientTop = System.Drawing.SystemColors.Control;
            this.btnCancel.IsQcd = false;
            this.btnCancel.Location = new System.Drawing.Point(751, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.QcdCondition = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(95, 27);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(938, 26);
            this.ucToolBar1.TabIndex = 8;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // ac_alloc_id
            // 
            this.ac_alloc_id.DataPropertyName = "ac_alloc_id";
            this.ac_alloc_id.HeaderText = "PRIMARY KEY";
            this.ac_alloc_id.Name = "ac_alloc_id";
            this.ac_alloc_id.ReadOnly = true;
            this.ac_alloc_id.Visible = false;
            this.ac_alloc_id.Width = 124;
            // 
            // tran_type
            // 
            this.tran_type.DataPropertyName = "tran_type";
            this.tran_type.HeaderText = "tran_type";
            this.tran_type.Name = "tran_type";
            this.tran_type.ReadOnly = true;
            this.tran_type.Visible = false;
            this.tran_type.Width = 104;
            // 
            // allocating_amt
            // 
            this.allocating_amt.DataPropertyName = "allocating_amt";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.NullValue = null;
            this.allocating_amt.DefaultCellStyle = dataGridViewCellStyle1;
            this.allocating_amt.HeaderText = "Allocating Amount";
            this.allocating_amt.Name = "allocating_amt";
            this.allocating_amt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.allocating_amt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.allocating_amt.Width = 166;
            // 
            // bill_no
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bill_no.DefaultCellStyle = dataGridViewCellStyle2;
            this.bill_no.HeaderText = "Bill/Challan No";
            this.bill_no.Name = "bill_no";
            this.bill_no.ReadOnly = true;
            this.bill_no.Width = 167;
            // 
            // bill_dt
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bill_dt.DefaultCellStyle = dataGridViewCellStyle3;
            this.bill_dt.HeaderText = "Bill/Challan Date";
            this.bill_dt.Name = "bill_dt";
            this.bill_dt.ReadOnly = true;
            this.bill_dt.Width = 185;
            // 
            // i_allocating
            // 
            this.i_allocating.DataPropertyName = "i_allocating";
            this.i_allocating.HeaderText = "Allocating";
            this.i_allocating.Name = "i_allocating";
            this.i_allocating.ReadOnly = true;
            this.i_allocating.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.i_allocating.Visible = false;
            this.i_allocating.Width = 133;
            // 
            // ref_tran_cd
            // 
            this.ref_tran_cd.DataPropertyName = "ref_tran_cd";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ref_tran_cd.DefaultCellStyle = dataGridViewCellStyle4;
            this.ref_tran_cd.HeaderText = "Transaction Code";
            this.ref_tran_cd.Name = "ref_tran_cd";
            this.ref_tran_cd.ReadOnly = true;
            this.ref_tran_cd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ref_tran_cd.Width = 157;
            // 
            // net_amt
            // 
            this.net_amt.DataPropertyName = "net_amt";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.NullValue = null;
            this.net_amt.DefaultCellStyle = dataGridViewCellStyle5;
            this.net_amt.HeaderText = "Amount";
            this.net_amt.Name = "net_amt";
            this.net_amt.ReadOnly = true;
            this.net_amt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.net_amt.Width = 74;
            // 
            // allocated_amt
            // 
            this.allocated_amt.DataPropertyName = "allocated_amt";
            this.allocated_amt.HeaderText = "Allocated Amount";
            this.allocated_amt.Name = "allocated_amt";
            this.allocated_amt.ReadOnly = true;
            this.allocated_amt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.allocated_amt.Visible = false;
            this.allocated_amt.Width = 157;
            // 
            // i_allocated
            // 
            this.i_allocated.DataPropertyName = "i_allocated";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.i_allocated.DefaultCellStyle = dataGridViewCellStyle6;
            this.i_allocated.HeaderText = "Allocated Amount";
            this.i_allocated.Name = "i_allocated";
            this.i_allocated.ReadOnly = true;
            this.i_allocated.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.i_allocated.Width = 157;
            // 
            // pending_amt
            // 
            this.pending_amt.DataPropertyName = "pending_amt";
            this.pending_amt.HeaderText = "Balance";
            this.pending_amt.Name = "pending_amt";
            this.pending_amt.ReadOnly = true;
            this.pending_amt.Visible = false;
            this.pending_amt.Width = 103;
            // 
            // i_pend
            // 
            this.i_pend.DataPropertyName = "i_pend";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.i_pend.DefaultCellStyle = dataGridViewCellStyle7;
            this.i_pend.HeaderText = "Pending Amount";
            this.i_pend.Name = "i_pend";
            this.i_pend.ReadOnly = true;
            this.i_pend.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.i_pend.Width = 85;
            // 
            // ref_tran_dt
            // 
            this.ref_tran_dt.DataPropertyName = "ref_tran_dt";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ref_tran_dt.DefaultCellStyle = dataGridViewCellStyle8;
            this.ref_tran_dt.HeaderText = "Transaction Date";
            this.ref_tran_dt.Name = "ref_tran_dt";
            this.ref_tran_dt.ReadOnly = true;
            this.ref_tran_dt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ref_tran_dt.Width = 157;
            // 
            // ref_tran_no
            // 
            this.ref_tran_no.DataPropertyName = "ref_tran_no";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ref_tran_no.DefaultCellStyle = dataGridViewCellStyle9;
            this.ref_tran_no.HeaderText = "Transaction No.";
            this.ref_tran_no.Name = "ref_tran_no";
            this.ref_tran_no.ReadOnly = true;
            this.ref_tran_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ref_tran_no.Width = 148;
            // 
            // ref_acserial
            // 
            this.ref_acserial.DataPropertyName = "ref_acserial";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ref_acserial.DefaultCellStyle = dataGridViewCellStyle10;
            this.ref_acserial.HeaderText = "Serial No.";
            this.ref_acserial.Name = "ref_acserial";
            this.ref_acserial.ReadOnly = true;
            this.ref_acserial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ref_acserial.Visible = false;
            this.ref_acserial.Width = 76;
            // 
            // fin_yr
            // 
            this.fin_yr.DataPropertyName = "fin_yr";
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fin_yr.DefaultCellStyle = dataGridViewCellStyle11;
            this.fin_yr.HeaderText = "Fiscal Year";
            this.fin_yr.Name = "fin_yr";
            this.fin_yr.ReadOnly = true;
            this.fin_yr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fin_yr.Width = 76;
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
            // ref_tran_sr
            // 
            this.ref_tran_sr.DataPropertyName = "ref_tran_sr";
            this.ref_tran_sr.HeaderText = "Series";
            this.ref_tran_sr.Name = "ref_tran_sr";
            this.ref_tran_sr.ReadOnly = true;
            this.ref_tran_sr.Visible = false;
            this.ref_tran_sr.Width = 93;
            // 
            // frmAccountAlloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(938, 297);
            this.ControlBox = false;
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblAllocatedAmt);
            this.Controls.Add(this.lblLedgerAcc);
            this.Controls.Add(this.lblTotLedgerAmt);
            this.Controls.Add(this.lblAc_nm);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvAccountAlloc);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAccountAlloc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAccountAlloc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAccountAlloc;
        private iMANTRA.UCToolBar ucToolBar1;
        private iMANTRA.PopupButton btnDone;
        private iMANTRA.PopupButton btnCancel;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblAllocatedAmt;
        private System.Windows.Forms.Label lblLedgerAcc;
        private System.Windows.Forms.Label lblTotLedgerAmt;
        private System.Windows.Forms.Label lblAc_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn ac_alloc_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn allocating_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn bill_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn i_allocating;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn net_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn allocated_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn i_allocated;
        private System.Windows.Forms.DataGridViewTextBoxColumn pending_amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn i_pend;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_acserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn fin_yr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_sr;
    }
}