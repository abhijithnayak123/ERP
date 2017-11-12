namespace CUSTOM_iMANTRA
{
    partial class frmDispatchSchedule
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
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.dgvDispatchSchedule = new CUSTOM_iMANTRA.MyDataGridView();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.schedule_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedule_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedule_dt = new CUSTOM_iMANTRA.POPUPDATETIME_FOR_GRID();
            this.dis_schedule_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedule_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispatchSchedule)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(447, 23);
            this.ucToolBar1.TabIndex = 11;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // dgvDispatchSchedule
            // 
            this.dgvDispatchSchedule.AllowUserToAddRows = false;
            this.dgvDispatchSchedule.AllowUserToDeleteRows = false;
            this.dgvDispatchSchedule.AllowUserToResizeColumns = false;
            this.dgvDispatchSchedule.AllowUserToResizeRows = false;
            this.dgvDispatchSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDispatchSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDispatchSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.schedule_id,
            this.schedule_no,
            this.schedule_dt,
            this.dis_schedule_qty,
            this.schedule_qty});
            this.dgvDispatchSchedule.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDispatchSchedule.Location = new System.Drawing.Point(0, 77);
            this.dgvDispatchSchedule.Name = "dgvDispatchSchedule";
            this.dgvDispatchSchedule.Size = new System.Drawing.Size(447, 169);
            this.dgvDispatchSchedule.TabIndex = 12;
            this.dgvDispatchSchedule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDispatchSchedule_CellContentClick);
            this.dgvDispatchSchedule.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDispatchSchedule_CellValidated);
            this.dgvDispatchSchedule.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.myDataGridView1_CellValidating);
            this.dgvDispatchSchedule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDispatchSchedule_EditingControlShowing);
            this.dgvDispatchSchedule.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDispatchSchedule_RowPostPaint);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(265, 251);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 26);
            this.btnDone.TabIndex = 13;
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(352, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 25);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(99, 253);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(92, 25);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 252);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 26);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Maroon;
            this.lblName.Location = new System.Drawing.Point(5, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 18);
            this.lblName.TabIndex = 17;
            this.lblName.Text = "Product";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.Maroon;
            this.lblQty.Location = new System.Drawing.Point(5, 51);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(38, 18);
            this.lblQty.TabIndex = 18;
            this.lblQty.Text = "Qty";
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
            // 
            // schedule_dt
            // 
            this.schedule_dt.DataPropertyName = "schedule_dt";
            this.schedule_dt.HeaderText = "Schedule Date";
            this.schedule_dt.Name = "schedule_dt";
            // 
            // dis_schedule_qty
            // 
            this.dis_schedule_qty.DataPropertyName = "dis_schedule_qty";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dis_schedule_qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.dis_schedule_qty.HeaderText = "Dispatched Qty";
            this.dis_schedule_qty.Name = "dis_schedule_qty";
            this.dis_schedule_qty.ReadOnly = true;
            // 
            // schedule_qty
            // 
            this.schedule_qty.DataPropertyName = "schedule_qty";
            this.schedule_qty.HeaderText = "Schedule Qty";
            this.schedule_qty.Name = "schedule_qty";
            // 
            // frmDispatchSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(447, 282);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.dgvDispatchSchedule);
            this.Controls.Add(this.ucToolBar1);
            this.Name = "frmDispatchSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDispatchSchedule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispatchSchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCToolBar ucToolBar1;
        private MyDataGridView dgvDispatchSchedule;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_no;
        private POPUPDATETIME_FOR_GRID schedule_dt;
        private System.Windows.Forms.DataGridViewTextBoxColumn dis_schedule_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedule_qty;
    }
}