namespace iMANTRA
{
    partial class frmDynamicAccountAlloc
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
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAllocatedAmt = new System.Windows.Forms.Label();
            this.lblLedgerAcc = new System.Windows.Forms.Label();
            this.btnDone = new iMANTRA.PopupButton();
            this.btnCancel = new iMANTRA.PopupButton();
            this.lblTotLedgerAmt = new System.Windows.Forms.Label();
            this.lblAc_nm = new System.Windows.Forms.Label();
            this.dgvAccountAlloc = new System.Windows.Forms.DataGridView();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(492, 281);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(108, 18);
            this.lblBalance.TabIndex = 16;
            this.lblBalance.Text = "Balance : ";
            // 
            // lblAllocatedAmt
            // 
            this.lblAllocatedAmt.AutoSize = true;
            this.lblAllocatedAmt.Location = new System.Drawing.Point(227, 281);
            this.lblAllocatedAmt.Name = "lblAllocatedAmt";
            this.lblAllocatedAmt.Size = new System.Drawing.Size(198, 18);
            this.lblAllocatedAmt.TabIndex = 15;
            this.lblAllocatedAmt.Text = "Allocated Amount : ";
            // 
            // lblLedgerAcc
            // 
            this.lblLedgerAcc.AutoSize = true;
            this.lblLedgerAcc.Location = new System.Drawing.Point(492, 244);
            this.lblLedgerAcc.Name = "lblLedgerAcc";
            this.lblLedgerAcc.Size = new System.Drawing.Size(148, 18);
            this.lblLedgerAcc.TabIndex = 14;
            this.lblLedgerAcc.Text = "Ledger Name : ";
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.GradientBottom = System.Drawing.Color.Gray;
            this.btnDone.GradientTop = System.Drawing.SystemColors.Control;
            this.btnDone.IsQcd = false;
            this.btnDone.Location = new System.Drawing.Point(781, 275);
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
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.GradientBottom = System.Drawing.Color.Gray;
            this.btnCancel.GradientTop = System.Drawing.SystemColors.Control;
            this.btnCancel.IsQcd = false;
            this.btnCancel.Location = new System.Drawing.Point(680, 275);
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
            // lblTotLedgerAmt
            // 
            this.lblTotLedgerAmt.AutoSize = true;
            this.lblTotLedgerAmt.Location = new System.Drawing.Point(5, 281);
            this.lblTotLedgerAmt.Name = "lblTotLedgerAmt";
            this.lblTotLedgerAmt.Size = new System.Drawing.Size(158, 18);
            this.lblTotLedgerAmt.TabIndex = 11;
            this.lblTotLedgerAmt.Text = "Total Amount : ";
            // 
            // lblAc_nm
            // 
            this.lblAc_nm.AutoSize = true;
            this.lblAc_nm.Location = new System.Drawing.Point(5, 244);
            this.lblAc_nm.Name = "lblAc_nm";
            this.lblAc_nm.Size = new System.Drawing.Size(138, 18);
            this.lblAc_nm.TabIndex = 10;
            this.lblAc_nm.Text = "Party Name : ";
            // 
            // dgvAccountAlloc
            // 
            this.dgvAccountAlloc.AllowUserToAddRows = false;
            this.dgvAccountAlloc.AllowUserToDeleteRows = false;
            this.dgvAccountAlloc.AllowUserToResizeRows = false;
            this.dgvAccountAlloc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAccountAlloc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountAlloc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAccountAlloc.Location = new System.Drawing.Point(1, 28);
            this.dgvAccountAlloc.Name = "dgvAccountAlloc";
            this.dgvAccountAlloc.RowHeadersVisible = false;
            this.dgvAccountAlloc.Size = new System.Drawing.Size(866, 207);
            this.dgvAccountAlloc.TabIndex = 9;
            this.dgvAccountAlloc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccountAlloc_CellContentClick);
            this.dgvAccountAlloc.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvAccountAlloc_CellValidating);
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
            this.ucToolBar1.Size = new System.Drawing.Size(870, 26);
            this.ucToolBar1.TabIndex = 8;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmDynamicAccountAlloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(870, 310);
            this.ControlBox = false;
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblAllocatedAmt);
            this.Controls.Add(this.lblLedgerAcc);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTotLedgerAmt);
            this.Controls.Add(this.lblAc_nm);
            this.Controls.Add(this.dgvAccountAlloc);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDynamicAccountAlloc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDynamicAccountAlloc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountAlloc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotLedgerAmt;
        private System.Windows.Forms.Label lblAc_nm;
        private System.Windows.Forms.DataGridView dgvAccountAlloc;
        private iMANTRA.UCToolBar ucToolBar1;
        private iMANTRA.PopupButton btnDone;
        private iMANTRA.PopupButton btnCancel;
        private System.Windows.Forms.Label lblLedgerAcc;
        private System.Windows.Forms.Label lblAllocatedAmt;
        private System.Windows.Forms.Label lblBalance;
    }
}