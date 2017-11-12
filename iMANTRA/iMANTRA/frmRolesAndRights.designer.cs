namespace iMANTRA
{
    partial class frmRolesAndRights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolesAndRights));
            this.btnSel = new iMANTRA.PopupButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.lblRoles = new System.Windows.Forms.Label();
            this.dgvRights = new iMANTRA.MyDataGridView();
            this.intRightsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.module_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.view_access = new iMANTRA.GridViewCheckBoxColumn();
            this.create_access = new iMANTRA.GridViewCheckBoxColumn();
            this.edit_access = new iMANTRA.GridViewCheckBoxColumn();
            this.delete_access = new iMANTRA.GridViewCheckBoxColumn();
            this.print_access = new iMANTRA.GridViewCheckBoxColumn();
            this.approve_access = new iMANTRA.GridViewCheckBoxColumn();
            this.plan_access = new iMANTRA.GridViewCheckBoxColumn();
            this.exc_access = new iMANTRA.GridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRights)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSel
            // 
            this.btnSel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSel.BackgroundImage")));
            this.btnSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSel.Dispddlfields = "";
            this.btnSel.GradientBottom = System.Drawing.Color.Gray;
            this.btnSel.GradientTop = System.Drawing.SystemColors.Control;
            this.btnSel.IsQcd = false;
            this.btnSel.Location = new System.Drawing.Point(325, 30);
            this.btnSel.Name = "btnSel";
            this.btnSel.Primaryddl = "";
            this.btnSel.QcdCondition = "";
            this.btnSel.Query_con = "";
            this.btnSel.Reftbltran_cd = "";
            this.btnSel.Size = new System.Drawing.Size(36, 23);
            this.btnSel.TabIndex = 2;
            this.btnSel.Tbl_nm = "";
            this.btnSel.UseVisualStyleBackColor = true;
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click_1);
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
            this.ucToolBar1.Size = new System.Drawing.Size(938, 23);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(70, 30);
            this.txtRole.Name = "txtRole";
            this.txtRole.Size = new System.Drawing.Size(242, 26);
            this.txtRole.TabIndex = 4;
            this.txtRole.Validating += new System.ComponentModel.CancelEventHandler(this.txtRole_Validating);
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(14, 33);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(48, 18);
            this.lblRoles.TabIndex = 3;
            this.lblRoles.Text = "Role";
            // 
            // dgvRights
            // 
            this.dgvRights.AllowUserToAddRows = false;
            this.dgvRights.AllowUserToDeleteRows = false;
            this.dgvRights.AllowUserToResizeRows = false;
            this.dgvRights.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRights.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.intRightsId,
            this.Module_Id,
            this.module_name,
            this.view_access,
            this.create_access,
            this.edit_access,
            this.delete_access,
            this.print_access,
            this.approve_access,
            this.plan_access,
            this.exc_access});
            this.dgvRights.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvRights.height = 439;
            this.dgvRights.Location = new System.Drawing.Point(0, 67);
            this.dgvRights.Name = "dgvRights";
            this.dgvRights.RowHeadersVisible = false;
            this.dgvRights.RowHeadersWidth = 60;
            this.dgvRights.Size = new System.Drawing.Size(933, 439);
            this.dgvRights.TabIndex = 0;
            this.dgvRights.width = 933;
            this.dgvRights.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRights_CellContentClick_1);
            this.dgvRights.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRights_CellValidating);
            // 
            // intRightsId
            // 
            this.intRightsId.HeaderText = "intRightsId";
            this.intRightsId.Name = "intRightsId";
            this.intRightsId.Visible = false;
            this.intRightsId.Width = 143;
            // 
            // Module_Id
            // 
            this.Module_Id.HeaderText = "ModuleId";
            this.Module_Id.Name = "Module_Id";
            this.Module_Id.ReadOnly = true;
            this.Module_Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Module_Id.Visible = false;
            this.Module_Id.Width = 94;
            // 
            // module_name
            // 
            this.module_name.HeaderText = "Module";
            this.module_name.Name = "module_name";
            this.module_name.ReadOnly = true;
            this.module_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.module_name.Width = 74;
            // 
            // view_access
            // 
            this.view_access.FillWeight = 150F;
            this.view_access.HeaderText = "View ";
            this.view_access.Name = "view_access";
            this.view_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.view_access.Width = 64;
            // 
            // create_access
            // 
            this.create_access.FillWeight = 150F;
            this.create_access.HeaderText = "Create ";
            this.create_access.Name = "create_access";
            this.create_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.create_access.Width = 84;
            // 
            // edit_access
            // 
            this.edit_access.FillWeight = 150F;
            this.edit_access.HeaderText = "Edit ";
            this.edit_access.Name = "edit_access";
            this.edit_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.edit_access.Width = 64;
            // 
            // delete_access
            // 
            this.delete_access.FillWeight = 150F;
            this.delete_access.HeaderText = "Delete ";
            this.delete_access.Name = "delete_access";
            this.delete_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.delete_access.Width = 84;
            // 
            // print_access
            // 
            this.print_access.FillWeight = 150F;
            this.print_access.HeaderText = "Print ";
            this.print_access.Name = "print_access";
            this.print_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.print_access.Width = 74;
            // 
            // approve_access
            // 
            this.approve_access.FillWeight = 150F;
            this.approve_access.HeaderText = "Approve ";
            this.approve_access.Name = "approve_access";
            this.approve_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.approve_access.Width = 94;
            // 
            // plan_access
            // 
            this.plan_access.HeaderText = "Planning ";
            this.plan_access.Name = "plan_access";
            this.plan_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.plan_access.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.plan_access.Width = 123;
            // 
            // exc_access
            // 
            this.exc_access.FillWeight = 150F;
            this.exc_access.HeaderText = "Execution ";
            this.exc_access.Name = "exc_access";
            this.exc_access.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.exc_access.Width = 114;
            // 
            // frmRolesAndRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(938, 512);
            this.ControlBox = false;
            this.Controls.Add(this.btnSel);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.lblRoles);
            this.Controls.Add(this.dgvRights);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmRolesAndRights";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRolesAndRights_FormClosed);
            this.Load += new System.EventHandler(this.frmRolesAndRights_Load);
            this.Enter += new System.EventHandler(this.frmRolesAndRights_Enter);
            this.Resize += new System.EventHandler(this.frmRolesAndRights_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRights)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.TextBox txtRole;
        private UCToolBar ucToolBar1;
        private PopupButton btnSel;
        private MyDataGridView dgvRights;
        private System.Windows.Forms.DataGridViewTextBoxColumn intRightsId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn module_name;
        private GridViewCheckBoxColumn view_access;
        private GridViewCheckBoxColumn create_access;
        private GridViewCheckBoxColumn edit_access;
        private GridViewCheckBoxColumn delete_access;
        private GridViewCheckBoxColumn print_access;
        private GridViewCheckBoxColumn approve_access;
        private GridViewCheckBoxColumn plan_access;
        private GridViewCheckBoxColumn exc_access;
    }
}