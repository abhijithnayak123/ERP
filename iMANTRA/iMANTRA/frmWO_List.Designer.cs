namespace iMANTRA
{
    partial class frmWO_List
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
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.dgvWOList = new iMANTRA.MyDataGridView();
            this.tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ptserial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWOList)).BeginInit();
            this.SuspendLayout();
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(473, 17);
            this.ucToolBar1.TabIndex = 34;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // dgvWOList
            // 
            this.dgvWOList.AllowUserToAddRows = false;
            this.dgvWOList.AllowUserToDeleteRows = false;
            this.dgvWOList.AllowUserToOrderColumns = true;
            this.dgvWOList.AllowUserToResizeColumns = false;
            this.dgvWOList.AllowUserToResizeRows = false;
            this.dgvWOList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWOList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWOList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tran_id,
            this.tran_no,
            this.tran_cd,
            this.ptserial,
            this.prod_cd,
            this.prod_nm,
            this.qty});
            this.dgvWOList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWOList.Location = new System.Drawing.Point(0, 18);
            this.dgvWOList.Name = "dgvWOList";
            this.dgvWOList.RowHeadersVisible = false;
            this.dgvWOList.Size = new System.Drawing.Size(473, 343);
            this.dgvWOList.TabIndex = 35;
            this.dgvWOList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWOList_CellContentDoubleClick);
            // 
            // tran_id
            // 
            this.tran_id.DataPropertyName = "tran_id";
            this.tran_id.HeaderText = "Transaction Id";
            this.tran_id.Name = "tran_id";
            this.tran_id.ReadOnly = true;
            this.tran_id.Visible = false;
            // 
            // tran_no
            // 
            this.tran_no.DataPropertyName = "tran_no";
            this.tran_no.HeaderText = "Transaction No";
            this.tran_no.Name = "tran_no";
            this.tran_no.ReadOnly = true;
            // 
            // tran_cd
            // 
            this.tran_cd.DataPropertyName = "tran_cd";
            this.tran_cd.HeaderText = "Transaction Code";
            this.tran_cd.Name = "tran_cd";
            this.tran_cd.ReadOnly = true;
            this.tran_cd.Visible = false;
            // 
            // ptserial
            // 
            this.ptserial.DataPropertyName = "ptserial";
            this.ptserial.HeaderText = "Serial No";
            this.ptserial.Name = "ptserial";
            this.ptserial.ReadOnly = true;
            // 
            // prod_cd
            // 
            this.prod_cd.DataPropertyName = "prod_cd";
            this.prod_cd.HeaderText = "Product Id";
            this.prod_cd.Name = "prod_cd";
            this.prod_cd.ReadOnly = true;
            this.prod_cd.Visible = false;
            // 
            // prod_nm
            // 
            this.prod_nm.DataPropertyName = "prod_nm";
            this.prod_nm.HeaderText = "Product Name";
            this.prod_nm.Name = "prod_nm";
            this.prod_nm.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "qty";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // frmWO_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(473, 361);
            this.ControlBox = false;
            this.Controls.Add(this.dgvWOList);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmWO_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmWO_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWOList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private MyDataGridView dgvWOList;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ptserial;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
    }
}