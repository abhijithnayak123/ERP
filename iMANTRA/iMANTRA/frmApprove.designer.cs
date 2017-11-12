namespace iMANTRA
{
    partial class frmApprove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApprove));
            this.cmbtran_nm = new System.Windows.Forms.ComboBox();
            this.dgvApprove = new iMANTRA.MyDataGridView();
            this.cmbstatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new iMANTRA.PopupButton();
            this.btn_approve = new iMANTRA.PopupButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.lblF2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApprove)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbtran_nm
            // 
            this.cmbtran_nm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtran_nm.FormattingEnabled = true;
            this.cmbtran_nm.Location = new System.Drawing.Point(204, 37);
            this.cmbtran_nm.Name = "cmbtran_nm";
            this.cmbtran_nm.Size = new System.Drawing.Size(190, 26);
            this.cmbtran_nm.TabIndex = 0;
            this.cmbtran_nm.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dgvApprove
            // 
            this.dgvApprove.AllowUserToAddRows = false;
            this.dgvApprove.AllowUserToDeleteRows = false;
            this.dgvApprove.AllowUserToResizeRows = false;
            this.dgvApprove.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvApprove.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApprove.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvApprove.height = 409;
            this.dgvApprove.Location = new System.Drawing.Point(5, 73);
            this.dgvApprove.Name = "dgvApprove";
            this.dgvApprove.RowHeadersVisible = false;
            this.dgvApprove.Size = new System.Drawing.Size(945, 409);
            this.dgvApprove.TabIndex = 2;
            this.dgvApprove.width = 945;
            this.dgvApprove.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvApprove_CellEnter);
            this.dgvApprove.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvApprove_CellLeave);
            this.dgvApprove.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvApprove_CellValidating);
            this.dgvApprove.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvApprove_EditingControlShowing);
            this.dgvApprove.DoubleClick += new System.EventHandler(this.dgvApprove_DoubleClick);
            // 
            // cmbstatus
            // 
            this.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbstatus.FormattingEnabled = true;
            this.cmbstatus.Location = new System.Drawing.Point(508, 39);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(140, 26);
            this.cmbstatus.TabIndex = 7;
            this.cmbstatus.SelectedIndexChanged += new System.EventHandler(this.cmbstatus_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "STATUS : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "TRANSACTION NAME : ";
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.GradientBottom = System.Drawing.Color.Gray;
            this.btnCancel.GradientTop = System.Drawing.SystemColors.Control;
            this.btnCancel.IsQcd = false;
            this.btnCancel.Location = new System.Drawing.Point(655, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.QcdCondition = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(78, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_approve
            // 
            this.btn_approve.Dispddlfields = "";
            this.btn_approve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_approve.GradientBottom = System.Drawing.Color.Gray;
            this.btn_approve.GradientTop = System.Drawing.SystemColors.Control;
            this.btn_approve.IsQcd = false;
            this.btn_approve.Location = new System.Drawing.Point(740, 37);
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Primaryddl = "";
            this.btn_approve.QcdCondition = "";
            this.btn_approve.Query_con = "";
            this.btn_approve.Reftbltran_cd = "";
            this.btn_approve.Size = new System.Drawing.Size(98, 27);
            this.btn_approve.TabIndex = 3;
            this.btn_approve.Tbl_nm = "";
            this.btn_approve.Text = "UPDATE";
            this.btn_approve.UseVisualStyleBackColor = true;
            this.btn_approve.Visible = false;
            this.btn_approve.Click += new System.EventHandler(this.btn_approve_Click);
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
            this.ucToolBar1.Size = new System.Drawing.Size(956, 23);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblF2.Location = new System.Drawing.Point(788, 24);
            this.lblF2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(162, 16);
            this.lblF2.TabIndex = 38;
            this.lblF2.Text = "Press F2 - To Get List";
            this.lblF2.Visible = false;
            // 
            // frmApprove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 490);
            this.ControlBox = false;
            this.Controls.Add(this.lblF2);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.cmbstatus);
            this.Controls.Add(this.dgvApprove);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_approve);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbtran_nm);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmApprove";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmApprove_FormClosed);
            this.Load += new System.EventHandler(this.frmApprove_Load);
            this.Enter += new System.EventHandler(this.frmApprove_Enter);
            this.Resize += new System.EventHandler(this.frmApprove_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApprove)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbtran_nm;
        private MyDataGridView dgvApprove;
        private PopupButton btn_approve;
        private PopupButton btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbstatus;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lblF2;

    }
}