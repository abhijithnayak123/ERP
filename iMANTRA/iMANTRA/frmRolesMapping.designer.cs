namespace iMANTRA
{
    partial class frmRolesMapping
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolesMapping));
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.intRoleMappingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comp_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fin_yr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUser = new iMANTRA.PopupButton();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.cmbFinYr = new System.Windows.Forms.ComboBox();
            this.cmbComp = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblFin = new System.Windows.Forms.Label();
            this.lblComp = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbxSearch
            // 
            this.grpbxSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpbxSearch.BorderColor = System.Drawing.Color.Black;
            this.grpbxSearch.BorderThickness = 1F;
            this.grpbxSearch.Controls.Add(this.lblRowsCount);
            this.grpbxSearch.Controls.Add(this.txtSearch);
            this.grpbxSearch.Controls.Add(this.dgvSearch);
            this.grpbxSearch.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpbxSearch.GroupImage = null;
            this.grpbxSearch.GroupTitle = "Search";
            this.grpbxSearch.Location = new System.Drawing.Point(5, 35);
            this.grpbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(4);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(369, 307);
            this.grpbxSearch.TabIndex = 37;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(12, 281);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 4;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(11, 28);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(353, 26);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.AllowUserToResizeRows = false;
            this.dgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.intRoleMappingId,
            this.user_nm,
            this.comp_nm,
            this.Fin_yr,
            this.role_nm});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 207;
            this.dgvSearch.Location = new System.Drawing.Point(11, 65);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(353, 207);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 353;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick_1);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // intRoleMappingId
            // 
            this.intRoleMappingId.DataPropertyName = "intRoleMappingId";
            this.intRoleMappingId.HeaderText = "Id";
            this.intRoleMappingId.Name = "intRoleMappingId";
            this.intRoleMappingId.Visible = false;
            // 
            // user_nm
            // 
            this.user_nm.DataPropertyName = "user_nm";
            this.user_nm.HeaderText = "User Name";
            this.user_nm.Name = "user_nm";
            this.user_nm.ReadOnly = true;
            // 
            // comp_nm
            // 
            this.comp_nm.DataPropertyName = "comp_nm";
            this.comp_nm.HeaderText = "Company";
            this.comp_nm.Name = "comp_nm";
            this.comp_nm.ReadOnly = true;
            // 
            // Fin_yr
            // 
            this.Fin_yr.DataPropertyName = "Fin_yr";
            this.Fin_yr.HeaderText = "Financial Year";
            this.Fin_yr.Name = "Fin_yr";
            this.Fin_yr.ReadOnly = true;
            // 
            // role_nm
            // 
            this.role_nm.DataPropertyName = "role_nm";
            this.role_nm.HeaderText = "Role";
            this.role_nm.Name = "role_nm";
            this.role_nm.ReadOnly = true;
            // 
            // btnUser
            // 
            this.btnUser.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUser.BackgroundImage")));
            this.btnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUser.Dispddlfields = "";
            this.btnUser.GradientBottom = System.Drawing.Color.Gray;
            this.btnUser.GradientTop = System.Drawing.SystemColors.Control;
            this.btnUser.IsQcd = false;
            this.btnUser.Location = new System.Drawing.Point(748, 66);
            this.btnUser.Margin = new System.Windows.Forms.Padding(4);
            this.btnUser.Name = "btnUser";
            this.btnUser.Primaryddl = "";
            this.btnUser.QcdCondition = "";
            this.btnUser.Query_con = "";
            this.btnUser.Reftbltran_cd = "";
            this.btnUser.Size = new System.Drawing.Size(29, 28);
            this.btnUser.TabIndex = 2;
            this.btnUser.Tbl_nm = "";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click_1);
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(541, 241);
            this.cmbRole.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(202, 26);
            this.cmbRole.TabIndex = 5;
            this.cmbRole.SelectedIndexChanged += new System.EventHandler(this.cmbRole_SelectedIndexChanged);
            this.cmbRole.Validating += new System.ComponentModel.CancelEventHandler(this.cmbRole_Validating);
            // 
            // cmbFinYr
            // 
            this.cmbFinYr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFinYr.FormattingEnabled = true;
            this.cmbFinYr.Location = new System.Drawing.Point(541, 182);
            this.cmbFinYr.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFinYr.Name = "cmbFinYr";
            this.cmbFinYr.Size = new System.Drawing.Size(202, 26);
            this.cmbFinYr.TabIndex = 4;
            this.cmbFinYr.SelectedIndexChanged += new System.EventHandler(this.cmbFinYr_SelectedIndexChanged);
            this.cmbFinYr.Validating += new System.ComponentModel.CancelEventHandler(this.cmbFinYr_Validating);
            // 
            // cmbComp
            // 
            this.cmbComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComp.FormattingEnabled = true;
            this.cmbComp.Location = new System.Drawing.Point(541, 122);
            this.cmbComp.Margin = new System.Windows.Forms.Padding(4);
            this.cmbComp.Name = "cmbComp";
            this.cmbComp.Size = new System.Drawing.Size(202, 26);
            this.cmbComp.TabIndex = 3;
            this.cmbComp.SelectedIndexChanged += new System.EventHandler(this.cmbComp_SelectedIndexChanged);
            this.cmbComp.Validating += new System.ComponentModel.CancelEventHandler(this.cmbComp_Validating);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(541, 66);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(202, 26);
            this.txtUser.TabIndex = 1;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtUser.Validating += new System.ComponentModel.CancelEventHandler(this.txtUser_Validating);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(382, 251);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(48, 18);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Role";
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Location = new System.Drawing.Point(382, 186);
            this.lblFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(148, 18);
            this.lblFin.TabIndex = 2;
            this.lblFin.Text = "Financial Year";
            // 
            // lblComp
            // 
            this.lblComp.AutoSize = true;
            this.lblComp.Location = new System.Drawing.Point(382, 124);
            this.lblComp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(78, 18);
            this.lblComp.TabIndex = 1;
            this.lblComp.Text = "Company";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(382, 66);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(98, 18);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User Name";
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(784, 26);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmRolesMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 350);
            this.ControlBox = false;
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.cmbFinYr);
            this.Controls.Add(this.cmbComp);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.lblComp);
            this.Controls.Add(this.lblUser);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRolesMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRolesMapping_FormClosed);
            this.Load += new System.EventHandler(this.frmRolesMapping_Load);
            this.Enter += new System.EventHandler(this.frmRolesMapping_Enter);
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblComp;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.ComboBox cmbComp;
        private System.Windows.Forms.ComboBox cmbFinYr;
        private System.Windows.Forms.ComboBox cmbRole;
        private UCToolBar ucToolBar1;
        private PopupButton btnUser;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn intRoleMappingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn user_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn comp_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fin_yr;
        private System.Windows.Forms.DataGridViewTextBoxColumn role_nm;
        private System.Windows.Forms.Label lblRowsCount;
    }
}