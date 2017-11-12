namespace CUSTOM_iMANTRA
{
    partial class frmFileUpload
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
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.dgvFileUpload = new CUSTOM_iMANTRA.MyDataGridView();
            this.fileid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.si_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn = new CUSTOM_iMANTRA.POPUPBUTTON_FOR_GRID();
            this.file_path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileUpload)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(604, 26);
            this.ucToolBar1.TabIndex = 12;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(99, 203);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(92, 25);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 202);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 26);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(508, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 25);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(421, 201);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(85, 26);
            this.btnDone.TabIndex = 18;
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // dgvFileUpload
            // 
            this.dgvFileUpload.AllowUserToAddRows = false;
            this.dgvFileUpload.AllowUserToDeleteRows = false;
            this.dgvFileUpload.AllowUserToResizeRows = false;
            this.dgvFileUpload.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFileUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileUpload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileid,
            this.si_no,
            this.btn,
            this.file_path,
            this.file_nm});
            this.dgvFileUpload.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvFileUpload.Location = new System.Drawing.Point(1, 27);
            this.dgvFileUpload.Name = "dgvFileUpload";
            this.dgvFileUpload.Size = new System.Drawing.Size(603, 169);
            this.dgvFileUpload.TabIndex = 17;
            this.dgvFileUpload.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFileUpload_CellClick);
            this.dgvFileUpload.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvFileUpload_RowPostPaint);
            this.dgvFileUpload.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvFileUpload_KeyPress);
            // 
            // fileid
            // 
            this.fileid.DataPropertyName = "fileid";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fileid.DefaultCellStyle = dataGridViewCellStyle1;
            this.fileid.HeaderText = "fileid";
            this.fileid.Name = "fileid";
            this.fileid.ReadOnly = true;
            this.fileid.Visible = false;
            // 
            // si_no
            // 
            this.si_no.DataPropertyName = "si_no";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.si_no.DefaultCellStyle = dataGridViewCellStyle2;
            this.si_no.HeaderText = "SL NO";
            this.si_no.Name = "si_no";
            this.si_no.ReadOnly = true;
            this.si_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.si_no.Visible = false;
            // 
            // btn
            // 
            this.btn.DataPropertyName = "btn";
            this.btn.Dispddlfields = "";
            this.btn.HeaderText = "Upload Image";
            this.btn.Name = "btn";
            this.btn.Primaryddl = "";
            this.btn.Query_con = "";
            this.btn.Reftbltran_cd = "";
            this.btn.Tbl_nm = "";
            this.btn.Text = "Upload Image";
            this.btn.UseColumnTextForButtonValue = true;
            // 
            // file_path
            // 
            this.file_path.DataPropertyName = "file_path";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.file_path.DefaultCellStyle = dataGridViewCellStyle3;
            this.file_path.HeaderText = "File Path";
            this.file_path.Name = "file_path";
            this.file_path.ReadOnly = true;
            // 
            // file_nm
            // 
            this.file_nm.DataPropertyName = "file_nm";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.file_nm.DefaultCellStyle = dataGridViewCellStyle4;
            this.file_nm.HeaderText = "File Name";
            this.file_nm.Name = "file_nm";
            this.file_nm.ReadOnly = true;
            this.file_nm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(316, 201);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(99, 26);
            this.btnPreview.TabIndex = 22;
            this.btnPreview.Text = "&Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // frmFileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(604, 231);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.dgvFileUpload);
            this.Controls.Add(this.ucToolBar1);
            this.Name = "frmFileUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmFileUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileUpload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private MyDataGridView dgvFileUpload;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileid;
        private System.Windows.Forms.DataGridViewTextBoxColumn si_no;
        private POPUPBUTTON_FOR_GRID btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_path;
        private System.Windows.Forms.DataGridViewTextBoxColumn file_nm;
    }
}