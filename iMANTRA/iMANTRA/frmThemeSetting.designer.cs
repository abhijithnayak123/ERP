namespace iMANTRA
{
    partial class frmThemeSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTheme_nm = new System.Windows.Forms.TextBox();
            this.groupBox2 = new iMANTRA.Grouper();
            this.dgvcolor = new iMANTRA.MyDataGridView();
            this.groupBox1 = new iMANTRA.Grouper();
            this.dgvFont = new iMANTRA.MyDataGridView();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.dgvThemes = new iMANTRA.MyDataGridView();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.theme_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Theme_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcolor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 44;
            this.label1.Text = "Theme Name";
            // 
            // txtTheme_nm
            // 
            this.txtTheme_nm.Location = new System.Drawing.Point(425, 36);
            this.txtTheme_nm.Margin = new System.Windows.Forms.Padding(4);
            this.txtTheme_nm.Name = "txtTheme_nm";
            this.txtTheme_nm.Size = new System.Drawing.Size(315, 26);
            this.txtTheme_nm.TabIndex = 43;
            this.txtTheme_nm.Validating += new System.ComponentModel.CancelEventHandler(this.txtTheme_nm_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox2.BorderColor = System.Drawing.Color.Black;
            this.groupBox2.BorderThickness = 1F;
            this.groupBox2.Controls.Add(this.dgvcolor);
            this.groupBox2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox2.GroupImage = null;
            this.groupBox2.GroupTitle = "Color Setting";
            this.groupBox2.Location = new System.Drawing.Point(278, 64);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.PaintGroupBox = true;
            this.groupBox2.RoundCorners = 1;
            this.groupBox2.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox2.ShadowControl = false;
            this.groupBox2.ShadowThickness = 1;
            this.groupBox2.Size = new System.Drawing.Size(653, 199);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = true;
            // 
            // dgvcolor
            // 
            this.dgvcolor.AllowUserToAddRows = false;
            this.dgvcolor.AllowUserToDeleteRows = false;
            this.dgvcolor.AllowUserToOrderColumns = true;
            this.dgvcolor.AllowUserToResizeRows = false;
            this.dgvcolor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvcolor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcolor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvcolor.Location = new System.Drawing.Point(7, 28);
            this.dgvcolor.Margin = new System.Windows.Forms.Padding(4);
            this.dgvcolor.Name = "dgvcolor";
            this.dgvcolor.RowHeadersVisible = false;
            this.dgvcolor.Size = new System.Drawing.Size(628, 166);
            this.dgvcolor.TabIndex = 37;
            this.dgvcolor.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvcolor_EditingControlShowing);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox1.BorderColor = System.Drawing.Color.Black;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.dgvFont);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "Font Setting";
            this.groupBox1.Location = new System.Drawing.Point(278, 267);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            this.groupBox1.Size = new System.Drawing.Size(652, 191);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = true;
            // 
            // dgvFont
            // 
            this.dgvFont.AllowUserToAddRows = false;
            this.dgvFont.AllowUserToDeleteRows = false;
            this.dgvFont.AllowUserToOrderColumns = true;
            this.dgvFont.AllowUserToResizeRows = false;
            this.dgvFont.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFont.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFont.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFont.Location = new System.Drawing.Point(4, 27);
            this.dgvFont.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFont.Name = "dgvFont";
            this.dgvFont.RowHeadersVisible = false;
            this.dgvFont.Size = new System.Drawing.Size(631, 160);
            this.dgvFont.TabIndex = 40;
            this.dgvFont.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvFont_EditingControlShowing);
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
            this.ucToolBar1.Size = new System.Drawing.Size(934, 25);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // dgvThemes
            // 
            this.dgvThemes.AllowUserToAddRows = false;
            this.dgvThemes.AllowUserToDeleteRows = false;
            this.dgvThemes.AllowUserToOrderColumns = true;
            this.dgvThemes.AllowUserToResizeColumns = false;
            this.dgvThemes.AllowUserToResizeRows = false;
            this.dgvThemes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThemes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThemes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.theme_id,
            this.Theme_nm});
            this.dgvThemes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvThemes.Location = new System.Drawing.Point(4, 37);
            this.dgvThemes.Margin = new System.Windows.Forms.Padding(4);
            this.dgvThemes.Name = "dgvThemes";
            this.dgvThemes.RowHeadersVisible = false;
            this.dgvThemes.Size = new System.Drawing.Size(266, 393);
            this.dgvThemes.TabIndex = 7;
            this.dgvThemes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThemes_CellClick);
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(14, 436);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 45;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // theme_id
            // 
            this.theme_id.DataPropertyName = "theme_id";
            this.theme_id.HeaderText = "Theme Id";
            this.theme_id.Name = "theme_id";
            this.theme_id.Visible = false;
            // 
            // Theme_nm
            // 
            this.Theme_nm.DataPropertyName = "Theme_nm";
            this.Theme_nm.HeaderText = "Theme Name";
            this.Theme_nm.Name = "Theme_nm";
            this.Theme_nm.ReadOnly = true;
            this.Theme_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmThemeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(934, 462);
            this.ControlBox = false;
            this.Controls.Add(this.lblRowsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTheme_nm);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.dgvThemes);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmThemeSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAppearance_FormClosed);
            this.Load += new System.EventHandler(this.frmAppearance_Load);
            this.Enter += new System.EventHandler(this.frmAppearance_Enter);
            this.Resize += new System.EventHandler(this.frmThemeSetting_Resize);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvcolor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThemes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyDataGridView dgvThemes;
        private UCToolBar ucToolBar1;
        private MyDataGridView dgvcolor;
        private MyDataGridView dgvFont;
        private Grouper groupBox1;
        private Grouper groupBox2;
        private System.Windows.Forms.TextBox txtTheme_nm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRowsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn theme_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Theme_nm;
    }
}