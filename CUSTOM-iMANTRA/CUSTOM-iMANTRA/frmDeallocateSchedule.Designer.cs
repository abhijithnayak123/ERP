namespace CUSTOM_iMANTRA
{
    partial class frmDeallocateSchedule
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
            this.lblQty = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.dgvDeallocateSchedule = new CUSTOM_iMANTRA.MyDataGridView();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.schedule_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ref_ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.schedule_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedule_dt = new CUSTOM_iMANTRA.POPUPDATETIME_FOR_GRID();
            this.schedule_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pl_schedule_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.de_schedule_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.de_schedule_dt = new CUSTOM_iMANTRA.POPUPDATETIME_FOR_GRID();
            this.de_schedule_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeallocateSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.Maroon;
            this.lblQty.Location = new System.Drawing.Point(12, 57);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(38, 18);
            this.lblQty.TabIndex = 25;
            this.lblQty.Text = "Qty";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Maroon;
            this.lblName.Location = new System.Drawing.Point(12, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 18);
            this.lblName.TabIndex = 24;
            this.lblName.Text = "Product";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(651, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 25);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(564, 415);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 26);
            this.btnDone.TabIndex = 20;
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // dgvDeallocateSchedule
            // 
            this.dgvDeallocateSchedule.AllowUserToAddRows = false;
            this.dgvDeallocateSchedule.AllowUserToDeleteRows = false;
            this.dgvDeallocateSchedule.AllowUserToResizeRows = false;
            this.dgvDeallocateSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDeallocateSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeallocateSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schedule_id,
            this.ref_tran_no,
            this.ref_tran_id,
            this.ref_tran_cd,
            this.ref_ptserial,
            this.sel,
            this.schedule_no,
            this.schedule_dt,
            this.schedule_qty,
            this.pl_schedule_qty,
            this.de_schedule_id,
            this.de_schedule_dt,
            this.de_schedule_qty});
            this.dgvDeallocateSchedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDeallocateSchedule.Location = new System.Drawing.Point(3, 83);
            this.dgvDeallocateSchedule.Name = "dgvDeallocateSchedule";
            this.dgvDeallocateSchedule.Size = new System.Drawing.Size(739, 327);
            this.dgvDeallocateSchedule.TabIndex = 19;
            this.dgvDeallocateSchedule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeallocateSchedule_CellContentClick);
            this.dgvDeallocateSchedule.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDeallocateSchedule_CellValidating);
            this.dgvDeallocateSchedule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDeallocateSchedule_EditingControlShowing);
            this.dgvDeallocateSchedule.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDeallocateSchedule_RowPostPaint);
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
            this.ucToolBar1.Size = new System.Drawing.Size(745, 25);
            this.ucToolBar1.TabIndex = 12;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // schedule_id
            // 
            this.schedule_id.DataPropertyName = "schedule_id";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.schedule_id.DefaultCellStyle = dataGridViewCellStyle1;
            this.schedule_id.HeaderText = "schedule_id";
            this.schedule_id.Name = "schedule_id";
            this.schedule_id.ReadOnly = true;
            this.schedule_id.Visible = false;
            this.schedule_id.Width = 143;
            // 
            // ref_tran_no
            // 
            this.ref_tran_no.DataPropertyName = "ref_tran_no";
            this.ref_tran_no.HeaderText = "ref_tran_no";
            this.ref_tran_no.Name = "ref_tran_no";
            this.ref_tran_no.ReadOnly = true;
            this.ref_tran_no.Visible = false;
            this.ref_tran_no.Width = 143;
            // 
            // ref_tran_id
            // 
            this.ref_tran_id.DataPropertyName = "ref_tran_id";
            this.ref_tran_id.HeaderText = "ref_tran_id";
            this.ref_tran_id.Name = "ref_tran_id";
            this.ref_tran_id.ReadOnly = true;
            this.ref_tran_id.Visible = false;
            this.ref_tran_id.Width = 143;
            // 
            // ref_tran_cd
            // 
            this.ref_tran_cd.DataPropertyName = "ref_tran_cd";
            this.ref_tran_cd.HeaderText = "ref_tran_cd";
            this.ref_tran_cd.Name = "ref_tran_cd";
            this.ref_tran_cd.ReadOnly = true;
            this.ref_tran_cd.Visible = false;
            this.ref_tran_cd.Width = 143;
            // 
            // ref_ptserial
            // 
            this.ref_ptserial.DataPropertyName = "ref_ptserial";
            this.ref_ptserial.HeaderText = "ref_ptserial";
            this.ref_ptserial.Name = "ref_ptserial";
            this.ref_ptserial.ReadOnly = true;
            this.ref_ptserial.Visible = false;
            this.ref_ptserial.Width = 153;
            // 
            // sel
            // 
            this.sel.HeaderText = "Select";
            this.sel.Name = "sel";
            this.sel.Width = 74;
            // 
            // schedule_no
            // 
            this.schedule_no.DataPropertyName = "schedule_no";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.schedule_no.DefaultCellStyle = dataGridViewCellStyle2;
            this.schedule_no.HeaderText = "Schedule No";
            this.schedule_no.Name = "schedule_no";
            this.schedule_no.ReadOnly = true;
            this.schedule_no.Visible = false;
            this.schedule_no.Width = 143;
            // 
            // schedule_dt
            // 
            this.schedule_dt.DataPropertyName = "schedule_dt";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.schedule_dt.DefaultCellStyle = dataGridViewCellStyle3;
            this.schedule_dt.HeaderText = "Schedule Date";
            this.schedule_dt.Name = "schedule_dt";
            this.schedule_dt.ReadOnly = true;
            this.schedule_dt.Width = 94;
            // 
            // schedule_qty
            // 
            this.schedule_qty.DataPropertyName = "schedule_qty";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.schedule_qty.DefaultCellStyle = dataGridViewCellStyle4;
            this.schedule_qty.HeaderText = "Schedule Qty";
            this.schedule_qty.Name = "schedule_qty";
            this.schedule_qty.ReadOnly = true;
            this.schedule_qty.Width = 113;
            // 
            // pl_schedule_qty
            // 
            this.pl_schedule_qty.DataPropertyName = "pl_schedule_qty";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pl_schedule_qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.pl_schedule_qty.HeaderText = "Pending-Dispatch";
            this.pl_schedule_qty.Name = "pl_schedule_qty";
            this.pl_schedule_qty.ReadOnly = true;
            this.pl_schedule_qty.Width = 193;
            // 
            // de_schedule_id
            // 
            this.de_schedule_id.DataPropertyName = "de_schedule_id";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.de_schedule_id.DefaultCellStyle = dataGridViewCellStyle6;
            this.de_schedule_id.HeaderText = "de_schedule_id";
            this.de_schedule_id.Name = "de_schedule_id";
            this.de_schedule_id.ReadOnly = true;
            this.de_schedule_id.Visible = false;
            this.de_schedule_id.Width = 173;
            // 
            // de_schedule_dt
            // 
            this.de_schedule_dt.DataPropertyName = "de_schedule_dt";
            this.de_schedule_dt.HeaderText = "Delivery Date";
            this.de_schedule_dt.Name = "de_schedule_dt";
            this.de_schedule_dt.Width = 94;
            // 
            // de_schedule_qty
            // 
            this.de_schedule_qty.DataPropertyName = "de_schedule_qty";
            this.de_schedule_qty.HeaderText = "Delivery Qty";
            this.de_schedule_qty.Name = "de_schedule_qty";
            this.de_schedule_qty.Width = 113;
            // 
            // frmDeallocateSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(745, 443);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.dgvDeallocateSchedule);
            this.Controls.Add(this.ucToolBar1);
            this.Name = "frmDeallocateSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDeallocateSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeallocateSchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private MyDataGridView dgvDeallocateSchedule;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ref_ptserial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_no;
        private POPUPDATETIME_FOR_GRID schedule_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pl_schedule_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn de_schedule_id;
        private POPUPDATETIME_FOR_GRID de_schedule_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn de_schedule_qty;
    }
}