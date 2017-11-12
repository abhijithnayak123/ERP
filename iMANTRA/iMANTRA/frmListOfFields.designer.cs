namespace iMANTRA
{
    partial class frmListOfFields
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
            this.btnDone = new iMANTRA.PopupButton();
            this.btnCancel = new iMANTRA.PopupButton();
            this.dgvtranset = new iMANTRA.MyDataGridView();
            this.Sel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.head_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fld_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtranset)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(358, 19);
            this.ucToolBar1.TabIndex = 35;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Location = new System.Drawing.Point(282, 458);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(75, 27);
            this.btnDone.TabIndex = 5;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(200, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(79, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvtranset
            // 
            this.dgvtranset.AllowUserToAddRows = false;
            this.dgvtranset.AllowUserToDeleteRows = false;
            this.dgvtranset.AllowUserToResizeColumns = false;
            this.dgvtranset.AllowUserToResizeRows = false;
            this.dgvtranset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvtranset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtranset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sel,
            this.head_nm,
            this.code,
            this.fld_nm});
            this.dgvtranset.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvtranset.height = 434;
            this.dgvtranset.Location = new System.Drawing.Point(0, 18);
            this.dgvtranset.Name = "dgvtranset";
            this.dgvtranset.RowHeadersVisible = false;
            this.dgvtranset.Size = new System.Drawing.Size(358, 434);
            this.dgvtranset.TabIndex = 3;
            this.dgvtranset.width = 358;
            // 
            // Sel
            // 
            this.Sel.DataPropertyName = "Sel";
            this.Sel.HeaderText = "Select";
            this.Sel.Name = "Sel";
            // 
            // head_nm
            // 
            this.head_nm.DataPropertyName = "head_nm";
            this.head_nm.HeaderText = "Header Name";
            this.head_nm.Name = "head_nm";
            this.head_nm.ReadOnly = true;
            this.head_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // code
            // 
            this.code.DataPropertyName = "code";
            this.code.HeaderText = "tran_cd";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Visible = false;
            // 
            // fld_nm
            // 
            this.fld_nm.DataPropertyName = "fld_nm";
            this.fld_nm.HeaderText = "Field Name";
            this.fld_nm.Name = "fld_nm";
            this.fld_nm.ReadOnly = true;
            this.fld_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmListOfFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(358, 490);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvtranset);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmListOfFields";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmListOfFields_Load);
            this.Resize += new System.EventHandler(this.frmListOfFields_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtranset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PopupButton btnDone;
        private PopupButton btnCancel;
        private MyDataGridView dgvtranset;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn head_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn fld_nm;
    }
}