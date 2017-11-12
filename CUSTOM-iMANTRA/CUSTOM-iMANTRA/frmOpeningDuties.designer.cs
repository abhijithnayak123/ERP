namespace CUSTOM_iMANTRA
{
    partial class frmOpeningDuties
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
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.dgvOpenDuties = new System.Windows.Forms.DataGridView();
            this.row_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_excise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_Cess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_SHCess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ob_Addl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenDuties)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(767, 26);
            this.ucToolBar1.TabIndex = 7;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(670, 227);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(93, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(568, 227);
            this.btnDone.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(97, 27);
            this.btnDone.TabIndex = 1;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
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
            this.dgvOpenDuties.Location = new System.Drawing.Point(0, 26);
            this.dgvOpenDuties.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgvOpenDuties.Name = "dgvOpenDuties";
            this.dgvOpenDuties.RowHeadersVisible = false;
            this.dgvOpenDuties.Size = new System.Drawing.Size(766, 197);
            this.dgvOpenDuties.TabIndex = 0;
            this.dgvOpenDuties.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOpenDuties_CellContentClick);
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
            // frmOpeningDuties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(767, 256);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.dgvOpenDuties);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpeningDuties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmOpeningDuties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenDuties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOpenDuties;
        private PopupButton btnDone;
        private PopupButton btnCancel;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn row_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_excise;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_Cess;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_SHCess;
        private System.Windows.Forms.DataGridViewTextBoxColumn ob_Addl;



    }
}