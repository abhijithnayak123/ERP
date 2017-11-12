namespace iMANTRA
{
    partial class frmEventMast
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
            this.grouper2 = new iMANTRA.Grouper();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.groupBox5 = new iMANTRA.Grouper();
            this.dtp_deactive_from = new iMANTRA.UserDT();
            this.chkProd_active = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.lblRowsCount = new System.Windows.Forms.Label();
            this.dgvSearch = new iMANTRA.MyDataGridView();
            this.EVENT_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EVENT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grouper1 = new iMANTRA.Grouper();
            this.txtSemHall = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVisitors = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_date = new iMANTRA.UserDT();
            this.txtSpeaker = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEventType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grouper2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).BeginInit();
            this.grouper1.SuspendLayout();
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(866, 25);
            this.ucToolBar1.TabIndex = 52;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // grouper2
            // 
            this.grouper2.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper2.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper2.BorderColor = System.Drawing.Color.Black;
            this.grouper2.BorderThickness = 1F;
            this.grouper2.Controls.Add(this.OTHER_DET);
            this.grouper2.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper2.GroupImage = null;
            this.grouper2.GroupTitle = "Custom Fields";
            this.grouper2.Location = new System.Drawing.Point(677, 367);
            this.grouper2.Margin = new System.Windows.Forms.Padding(0);
            this.grouper2.Name = "grouper2";
            this.grouper2.Padding = new System.Windows.Forms.Padding(20);
            this.grouper2.PaintGroupBox = true;
            this.grouper2.RoundCorners = 1;
            this.grouper2.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper2.ShadowControl = false;
            this.grouper2.ShadowThickness = 1;
            this.grouper2.Size = new System.Drawing.Size(174, 65);
            this.grouper2.TabIndex = 13;
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OTHER_DET.Location = new System.Drawing.Point(11, 31);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(155, 27);
            this.OTHER_DET.TabIndex = 14;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "OTHER DETAILS";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackgroundColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.groupBox5.BorderColor = System.Drawing.Color.Black;
            this.groupBox5.BorderThickness = 1F;
            this.groupBox5.Controls.Add(this.dtp_deactive_from);
            this.groupBox5.Controls.Add(this.chkProd_active);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.CustomGroupBoxColor = System.Drawing.Color.White;
            this.groupBox5.GroupImage = null;
            this.groupBox5.GroupTitle = "Status";
            this.groupBox5.Location = new System.Drawing.Point(342, 367);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox5.PaintGroupBox = true;
            this.groupBox5.RoundCorners = 1;
            this.groupBox5.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupBox5.ShadowControl = false;
            this.groupBox5.ShadowThickness = 1;
            this.groupBox5.Size = new System.Drawing.Size(331, 65);
            this.groupBox5.TabIndex = 10;
            // 
            // dtp_deactive_from
            // 
            this.dtp_deactive_from.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_deactive_from.CustomFormat = "dd-MMM-yyyy";
            this.dtp_deactive_from.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_deactive_from.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_deactive_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_deactive_from.Location = new System.Drawing.Point(191, 29);
            this.dtp_deactive_from.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_deactive_from.Name = "dtp_deactive_from";
            this.dtp_deactive_from.Size = new System.Drawing.Size(136, 26);
            this.dtp_deactive_from.TabIndex = 12;
            // 
            // chkProd_active
            // 
            this.chkProd_active.AutoSize = true;
            this.chkProd_active.Location = new System.Drawing.Point(5, 31);
            this.chkProd_active.Name = "chkProd_active";
            this.chkProd_active.Size = new System.Drawing.Size(137, 22);
            this.chkProd_active.TabIndex = 11;
            this.chkProd_active.Text = "De-Activate";
            this.chkProd_active.UseVisualStyleBackColor = true;
            this.chkProd_active.CheckedChanged += new System.EventHandler(this.chkProd_active_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(139, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 18);
            this.label14.TabIndex = 1;
            this.label14.Text = "From";
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
            this.grpbxSearch.GroupTitle = "Event List";
            this.grpbxSearch.Location = new System.Drawing.Point(2, 29);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(333, 405);
            this.grpbxSearch.TabIndex = 0;
            // 
            // lblRowsCount
            // 
            this.lblRowsCount.AutoSize = true;
            this.lblRowsCount.BackColor = System.Drawing.Color.Transparent;
            this.lblRowsCount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowsCount.ForeColor = System.Drawing.Color.Red;
            this.lblRowsCount.Location = new System.Drawing.Point(3, 382);
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
            this.EVENT_CD,
            this.EVENT_TYPE,
            this.TITLE});
            this.dgvSearch.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSearch.height = 292;
            this.dgvSearch.Location = new System.Drawing.Point(6, 61);
            this.dgvSearch.Name = "dgvSearch";
            this.dgvSearch.RowHeadersVisible = false;
            this.dgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSearch.Size = new System.Drawing.Size(320, 292);
            this.dgvSearch.TabIndex = 2;
            this.dgvSearch.width = 320;
            this.dgvSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearch_CellClick);
            this.dgvSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvSearch_KeyDown);
            // 
            // EVENT_CD
            // 
            this.EVENT_CD.DataPropertyName = "EVENT_CD";
            this.EVENT_CD.HeaderText = "EVENT_CD";
            this.EVENT_CD.Name = "EVENT_CD";
            this.EVENT_CD.Visible = false;
            // 
            // EVENT_TYPE
            // 
            this.EVENT_TYPE.DataPropertyName = "EVENT_TYPE";
            this.EVENT_TYPE.HeaderText = "Event Type";
            this.EVENT_TYPE.Name = "EVENT_TYPE";
            this.EVENT_TYPE.ReadOnly = true;
            // 
            // TITLE
            // 
            this.TITLE.DataPropertyName = "TITLE";
            this.TITLE.HeaderText = "Title";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(4, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(323, 26);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.txtSemHall);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.txtVisitors);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.dtp_date);
            this.grouper1.Controls.Add(this.txtSpeaker);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.txtTitle);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.txtEventType);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Event Details";
            this.grouper1.Location = new System.Drawing.Point(340, 28);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(509, 336);
            this.grouper1.TabIndex = 3;
            // 
            // txtSemHall
            // 
            this.txtSemHall.Location = new System.Drawing.Point(148, 258);
            this.txtSemHall.Multiline = true;
            this.txtSemHall.Name = "txtSemHall";
            this.txtSemHall.Size = new System.Drawing.Size(319, 55);
            this.txtSemHall.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 23);
            this.label6.TabIndex = 23;
            this.label6.Text = "Seminar Hall";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 23);
            this.label5.TabIndex = 22;
            this.label5.Text = "Visitors";
            // 
            // txtVisitors
            // 
            this.txtVisitors.Location = new System.Drawing.Point(147, 154);
            this.txtVisitors.Multiline = true;
            this.txtVisitors.Name = "txtVisitors";
            this.txtVisitors.Size = new System.Drawing.Size(319, 55);
            this.txtVisitors.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "Date";
            // 
            // dtp_date
            // 
            this.dtp_date.CalendarTitleBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtp_date.CustomFormat = "dd-MMM-yyyy";
            this.dtp_date.DtValue = new System.DateTime(2013, 11, 15, 16, 23, 5, 89);
            this.dtp_date.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_date.Location = new System.Drawing.Point(147, 215);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(136, 26);
            this.dtp_date.TabIndex = 8;
            // 
            // txtSpeaker
            // 
            this.txtSpeaker.Location = new System.Drawing.Point(147, 113);
            this.txtSpeaker.Name = "txtSpeaker";
            this.txtSpeaker.Size = new System.Drawing.Size(319, 26);
            this.txtSpeaker.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 23);
            this.label3.TabIndex = 18;
            this.label3.Text = "Speaker";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(147, 75);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(319, 26);
            this.txtTitle.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Title";
            // 
            // txtEventType
            // 
            this.txtEventType.Location = new System.Drawing.Point(147, 37);
            this.txtEventType.Name = "txtEventType";
            this.txtEventType.Size = new System.Drawing.Size(319, 26);
            this.txtEventType.TabIndex = 4;
            this.txtEventType.Validating += new System.ComponentModel.CancelEventHandler(this.txtProd_gr_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type of Event";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 258);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(319, 55);
            this.textBox1.TabIndex = 24;
            // 
            // frmEventMast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(866, 438);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grouper2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.grouper1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmEventMast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductGroupMast_FormClosed);
            this.Load += new System.EventHandler(this.frmProductGroupMast_Load);
            this.Enter += new System.EventHandler(this.frmProductGroupMast_Enter);
            this.Resize += new System.EventHandler(this.frmProductGroupMast_Resize);
            this.grouper2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearch)).EndInit();
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper2;
        private PopupButton OTHER_DET;
        private Grouper groupBox5;
        private UserDT dtp_deactive_from;
        private System.Windows.Forms.CheckBox chkProd_active;
        private System.Windows.Forms.Label label14;
        private Grouper grpbxSearch;
        private MyDataGridView dgvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtEventType;
        private System.Windows.Forms.Label label1;
        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lblRowsCount;
        private UserDT dateTimePicker1;
        private System.Windows.Forms.TextBox txtSpeaker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVisitors;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSemHall;
        private UserDT dtp_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn EVENT_CD;
        private System.Windows.Forms.DataGridViewTextBoxColumn EVENT_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;


    }
}