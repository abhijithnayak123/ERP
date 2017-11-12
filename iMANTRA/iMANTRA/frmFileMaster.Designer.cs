namespace iMANTRA
{
    partial class frmFileMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileMaster));
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.tran_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caption_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tran_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.groupBox1 = new iMANTRA.Grouper();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.button1 = new iMANTRA.PopupButton();
            this.txtTransaction_nm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnHeaderWise = new System.Windows.Forms.RadioButton();
            this.rbtnDetailsWise = new System.Windows.Forms.RadioButton();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(651, 23);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // grpbxSearch
            // 
            this.grpbxSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpbxSearch.BorderColor = System.Drawing.Color.Black;
            this.grpbxSearch.BorderThickness = 1F;
            this.grpbxSearch.Controls.Add(this.lblRowsCount);
            this.grpbxSearch.Controls.Add(this.dgvSearch);
            this.grpbxSearch.Controls.Add(this.txtSearch);
            this.grpbxSearch.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpbxSearch.GroupImage = null;
            this.grpbxSearch.GroupTitle = "Search";
            this.grpbxSearch.Location = new System.Drawing.Point(4, 26);
            this.grpbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(29, 24, 29, 24);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(252, 389);
            this.grpbxSearch.TabIndex = 1;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(6, 363);
            this.lblRowsCount.Name = "lblRowsCount";
            this.lblRowsCount.Size = new System.Drawing.Size(108, 18);
            this.lblRowsCount.TabIndex = 4;
            this.lblRowsCount.Text = "Rows Count";
            // 
            // dgvSearch
            // 
            this.dgvSearch.AllowUserToAddRows = false;
            this.dgvSearch.AllowUserToDeleteRows = false;
            this.dgvSearch.AllowUserToResizeRows = false;
            this.dgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tran_id,
            this.caption_nm,
            this.tran_nm});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 299;
            this.dgvSearch.Location = new System.Drawing.Point(5, 60);
            this.dgvSearch.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(241, 299);
            this.dgvSearch.TabIndex = 3;
            this.dgvSearch.width = 241;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // tran_id
            // 
            this.tran_id.DataPropertyName = "tran_id";
            this.tran_id.HeaderText = "tran_id";
            this.tran_id.Name = "tran_id";
            this.tran_id.Visible = false;
            // 
            // caption_nm
            // 
            this.caption_nm.DataPropertyName = "caption_nm";
            this.caption_nm.HeaderText = "Caption ";
            this.caption_nm.Name = "caption_nm";
            this.caption_nm.ReadOnly = true;
            this.caption_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tran_nm
            // 
            this.tran_nm.DataPropertyName = "tran_nm";
            this.tran_nm.HeaderText = "Transaction Name";
            this.tran_nm.Name = "tran_nm";
            this.tran_nm.ReadOnly = true;
            this.tran_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 29);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(240, 26);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox1.BorderColor = System.Drawing.Color.Black;
            this.groupBox1.BorderThickness = 1F;
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCaption);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtTransaction_nm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbtnHeaderWise);
            this.groupBox1.Controls.Add(this.rbtnDetailsWise);
            this.groupBox1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox1.GroupImage = null;
            this.groupBox1.GroupTitle = "Basic Details";
            this.groupBox1.Location = new System.Drawing.Point(260, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupBox1.PaintGroupBox = true;
            this.groupBox1.RoundCorners = 1;
            this.groupBox1.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox1.ShadowControl = false;
            this.groupBox1.ShadowThickness = 1;
            this.groupBox1.Size = new System.Drawing.Size(390, 389);
            this.groupBox1.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(124, 88);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(12, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(124, 136);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 18);
            this.label7.TabIndex = 31;
            this.label7.Text = "Caption";
            // 
            // txtCaption
            // 
            this.txtCaption.Location = new System.Drawing.Point(142, 82);
            this.txtCaption.Margin = new System.Windows.Forms.Padding(0);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(244, 26);
            this.txtCaption.TabIndex = 7;
            this.txtCaption.Validating += new System.ComponentModel.CancelEventHandler(this.txtCaption_Validating);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Dispddlfields = "";
            this.button1.Location = new System.Drawing.Point(350, 128);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.button1.Name = "button1";
            this.button1.Primaryddl = "";
            this.button1.Query_con = "";
            this.button1.Reftbltran_cd = "";
            this.button1.Size = new System.Drawing.Size(31, 28);
            this.button1.TabIndex = 9;
            this.button1.Tbl_nm = "";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTransaction_nm
            // 
            this.txtTransaction_nm.Location = new System.Drawing.Point(142, 130);
            this.txtTransaction_nm.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtTransaction_nm.Name = "txtTransaction_nm";
            this.txtTransaction_nm.Size = new System.Drawing.Size(207, 26);
            this.txtTransaction_nm.TabIndex = 8;
            this.txtTransaction_nm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTransaction_nm_KeyDown);
            this.txtTransaction_nm.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransaction_nm_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Module Name";
            // 
            // rbtnHeaderWise
            // 
            this.rbtnHeaderWise.AutoSize = true;
            this.rbtnHeaderWise.Location = new System.Drawing.Point(7, 33);
            this.rbtnHeaderWise.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.rbtnHeaderWise.Name = "rbtnHeaderWise";
            this.rbtnHeaderWise.Size = new System.Drawing.Size(126, 22);
            this.rbtnHeaderWise.TabIndex = 5;
            this.rbtnHeaderWise.TabStop = true;
            this.rbtnHeaderWise.Text = "HeaderWise";
            this.rbtnHeaderWise.UseVisualStyleBackColor = true;
            // 
            // rbtnDetailsWise
            // 
            this.rbtnDetailsWise.AutoSize = true;
            this.rbtnDetailsWise.Location = new System.Drawing.Point(145, 33);
            this.rbtnDetailsWise.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.rbtnDetailsWise.Name = "rbtnDetailsWise";
            this.rbtnDetailsWise.Size = new System.Drawing.Size(136, 22);
            this.rbtnDetailsWise.TabIndex = 6;
            this.rbtnDetailsWise.TabStop = true;
            this.rbtnDetailsWise.Text = "DetailsWise";
            this.rbtnDetailsWise.UseVisualStyleBackColor = true;
            // 
            // frmFileMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(651, 417);
            this.ControlBox = false;
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmFileMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFileMaster_FormClosed);
            this.Load += new System.EventHandler(this.frmFileMaster_Load);
            this.Enter += new System.EventHandler(this.frmFileMaster_Enter);
            this.Resize += new System.EventHandler(this.frmFileMaster_Resize);
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper grpbxSearch;
        private System.Windows.Forms.Label lblRowsCount;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper groupBox1;
        private System.Windows.Forms.Label label12;
        private PopupButton button1;
        private System.Windows.Forms.TextBox txtTransaction_nm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnHeaderWise;
        private System.Windows.Forms.RadioButton rbtnDetailsWise;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn base_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn caption_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_nm;
        private System.Windows.Forms.DataGridViewTextBoxColumn tran_id;
    }
}