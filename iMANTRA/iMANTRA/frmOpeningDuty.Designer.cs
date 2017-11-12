namespace iMANTRA
{
    partial class frmOpeningDuty
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
            this.dtpTranDt = new UserDT();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.lblTranNo = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dgvOpenDuties = new iMANTRA.MyDataGridView();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.row_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_excise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_Cess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_SHCess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_Addl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenDuties)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTranDt
            // 
            this.dtpTranDt.Location = new System.Drawing.Point(69, 31);
            this.dtpTranDt.Name = "dtpTranDt";
            this.dtpTranDt.Size = new System.Drawing.Size(143, 21);
            this.dtpTranDt.TabIndex = 45;
            this.dtpTranDt.Validating += new System.ComponentModel.CancelEventHandler(this.dtpTranDt_Validating);
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(393, 31);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(203, 21);
            this.txtNo.TabIndex = 44;
            this.txtNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtNo_Validating);
            // 
            // lblTranNo
            // 
            this.lblTranNo.AutoSize = true;
            this.lblTranNo.Location = new System.Drawing.Point(280, 36);
            this.lblTranNo.Name = "lblTranNo";
            this.lblTranNo.Size = new System.Drawing.Size(105, 15);
            this.lblTranNo.TabIndex = 42;
            this.lblTranNo.Text = "Transaction No";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(9, 36);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 41;
            this.lblDate.Text = "Date";
            // 
            // dgvOpenDuties
            // 
            this.dgvOpenDuties.AllowUserToAddRows = false;
            this.dgvOpenDuties.AllowUserToDeleteRows = false;
            this.dgvOpenDuties.AllowUserToResizeRows = false;
            this.dgvOpenDuties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOpenDuties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenDuties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.row_col,
            this.ob_excise,
            this.ob_Cess,
            this.ob_SHCess,
            this.ob_Addl});
            this.dgvOpenDuties.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvOpenDuties.Location = new System.Drawing.Point(3, 67);
            this.dgvOpenDuties.Name = "dgvOpenDuties";
            this.dgvOpenDuties.RowHeadersVisible = false;
            this.dgvOpenDuties.Size = new System.Drawing.Size(618, 167);
            this.dgvOpenDuties.TabIndex = 40;
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(625, 23);
            this.ucToolBar1.TabIndex = 39;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // row_col
            // 
            this.row_col.DataPropertyName = "row_col";
            this.row_col.HeaderText = "";
            this.row_col.Name = "row_col";
            this.row_col.ReadOnly = true;
            // 
            // ob_excise
            // 
            this.ob_excise.DataPropertyName = "ob_excise";
            this.ob_excise.HeaderText = "Excise Amount";
            this.ob_excise.Name = "ob_excise";
            // 
            // ob_Cess
            // 
            this.ob_Cess.DataPropertyName = "ob_Cess";
            this.ob_Cess.HeaderText = "Cess Amount";
            this.ob_Cess.Name = "ob_Cess";
            // 
            // ob_SHCess
            // 
            this.ob_SHCess.DataPropertyName = "ob_SHCess";
            this.ob_SHCess.HeaderText = "S & H Cess Amount";
            this.ob_SHCess.Name = "ob_SHCess";
            // 
            // ob_Addl
            // 
            this.ob_Addl.DataPropertyName = "ob_Addl";
            this.ob_Addl.HeaderText = "Additional Duty";
            this.ob_Addl.Name = "ob_Addl";
            // 
            // frmOpeningDuty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 238);
            this.ControlBox = false;
            this.Controls.Add(this.dtpTranDt);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.lblTranNo);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dgvOpenDuties);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOpeningDuty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOpeningDuty_FormClosed);
            this.Load += new System.EventHandler(this.frmOpeningDuty_Load);
            this.Enter += new System.EventHandler(this.frmOpeningDuty_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenDuties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iMANTRA.UCToolBar ucToolBar1;
        private MyDataGridView dgvOpenDuties;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTranNo;
        private System.Windows.Forms.TextBox txtNo;
        private UserDT dtpTranDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_excise;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_Cess;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_SHCess;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_Addl;
    }
}