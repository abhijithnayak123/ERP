namespace iMANTRA
{
    partial class frmDebitEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebitEntry));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsalesaddlduty = new System.Windows.Forms.TextBox();
            this.txtsaleshcessduty = new System.Windows.Forms.TextBox();
            this.txtsalescessduty = new System.Windows.Forms.TextBox();
            this.txtsalesexduty = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvcreditdetails = new MyDataGridView();
            this.details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.credit_excise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit_Cess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit_SHCess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit_Addl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adjust_Excise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adjust_Cess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adjust_SHCess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adjust_Addl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttl_amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new UserDT();
            this.txtentryno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpDutyFrom = new UserDT();
            this.dtpDutyTo = new UserDT();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcreditdetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtsalesaddlduty);
            this.groupBox1.Controls.Add(this.txtsaleshcessduty);
            this.groupBox1.Controls.Add(this.txtsalescessduty);
            this.groupBox1.Controls.Add(this.txtsalesexduty);
            this.groupBox1.Location = new System.Drawing.Point(13, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1088, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = true;
            this.groupBox1.Text = "Total Duty Payable Amount ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Addl. Duty Amount ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Total S and H Cess Duty Amount  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total Edu. Cess Duty Amount ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total Excise Duty Amount";
            // 
            // txtsalesaddlduty
            // 
            this.txtsalesaddlduty.Location = new System.Drawing.Point(197, 116);
            this.txtsalesaddlduty.Name = "txtsalesaddlduty";
            this.txtsalesaddlduty.ReadOnly = true;
            this.txtsalesaddlduty.Size = new System.Drawing.Size(143, 20);
            this.txtsalesaddlduty.TabIndex = 3;
            // 
            // txtsaleshcessduty
            // 
            this.txtsaleshcessduty.Location = new System.Drawing.Point(197, 87);
            this.txtsaleshcessduty.Name = "txtsaleshcessduty";
            this.txtsaleshcessduty.ReadOnly = true;
            this.txtsaleshcessduty.Size = new System.Drawing.Size(143, 20);
            this.txtsaleshcessduty.TabIndex = 2;
            // 
            // txtsalescessduty
            // 
            this.txtsalescessduty.Location = new System.Drawing.Point(197, 55);
            this.txtsalescessduty.Name = "txtsalescessduty";
            this.txtsalescessduty.ReadOnly = true;
            this.txtsalescessduty.Size = new System.Drawing.Size(143, 20);
            this.txtsalescessduty.TabIndex = 1;
            // 
            // txtsalesexduty
            // 
            this.txtsalesexduty.Location = new System.Drawing.Point(197, 22);
            this.txtsalesexduty.Name = "txtsalesexduty";
            this.txtsalesexduty.ReadOnly = true;
            this.txtsalesexduty.Size = new System.Drawing.Size(143, 20);
            this.txtsalesexduty.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvcreditdetails);
            this.groupBox2.Location = new System.Drawing.Point(8, 294);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1093, 162);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = true;
            this.groupBox2.Text = "Available Credit And Adjustable Details ";
            // 
            // dgvcreditdetails
            // 
            this.dgvcreditdetails.AllowUserToAddRows = false;
            this.dgvcreditdetails.AllowUserToDeleteRows = false;
            this.dgvcreditdetails.AllowUserToResizeRows = false;
            this.dgvcreditdetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvcreditdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcreditdetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.details,
            this.credit_excise,
            this.Credit_Cess,
            this.Credit_SHCess,
            this.Credit_Addl,
            this.Adjust_Excise,
            this.Adjust_Cess,
            this.Adjust_SHCess,
            this.Adjust_Addl,
            this.ttl_amt});
            this.dgvcreditdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvcreditdetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvcreditdetails.Location = new System.Drawing.Point(3, 16);
            this.dgvcreditdetails.Name = "dgvcreditdetails";
            this.dgvcreditdetails.RowHeadersVisible = false;
            this.dgvcreditdetails.Size = new System.Drawing.Size(1087, 143);
            this.dgvcreditdetails.TabIndex = 0;
            this.dgvcreditdetails.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvcreditdetails_CellValidating);
            // 
            // details
            // 
            this.details.HeaderText = "";
            this.details.Name = "details";
            this.details.ReadOnly = true;
            // 
            // credit_excise
            // 
            this.credit_excise.HeaderText = "Credit Excise";
            this.credit_excise.Name = "credit_excise";
            this.credit_excise.ReadOnly = true;
            // 
            // Credit_Cess
            // 
            this.Credit_Cess.HeaderText = "Credit Cess";
            this.Credit_Cess.Name = "Credit_Cess";
            this.Credit_Cess.ReadOnly = true;
            // 
            // Credit_SHCess
            // 
            this.Credit_SHCess.HeaderText = "Credit S&H Cess";
            this.Credit_SHCess.Name = "Credit_SHCess";
            this.Credit_SHCess.ReadOnly = true;
            // 
            // Credit_Addl
            // 
            this.Credit_Addl.HeaderText = "Credit Addl. Duty";
            this.Credit_Addl.Name = "Credit_Addl";
            this.Credit_Addl.ReadOnly = true;
            // 
            // Adjust_Excise
            // 
            this.Adjust_Excise.HeaderText = "Adjust Excise";
            this.Adjust_Excise.Name = "Adjust_Excise";
            // 
            // Adjust_Cess
            // 
            this.Adjust_Cess.HeaderText = "Adjust Cess";
            this.Adjust_Cess.Name = "Adjust_Cess";
            // 
            // Adjust_SHCess
            // 
            this.Adjust_SHCess.HeaderText = "Adjust S&H Cess";
            this.Adjust_SHCess.Name = "Adjust_SHCess";
            // 
            // Adjust_Addl
            // 
            this.Adjust_Addl.HeaderText = "Adjust Addl. Duty";
            this.Adjust_Addl.Name = "Adjust_Addl";
            // 
            // ttl_amt
            // 
            this.ttl_amt.HeaderText = "Total Amount";
            this.ttl_amt.Name = "ttl_amt";
            this.ttl_amt.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(125, 26);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(123, 20);
            this.dtpDate.TabIndex = 5;
            // 
            // txtentryno
            // 
            this.txtentryno.Location = new System.Drawing.Point(711, 34);
            this.txtentryno.Name = "txtentryno";
            this.txtentryno.Size = new System.Drawing.Size(192, 20);
            this.txtentryno.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(619, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Debit Entry No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Duty Payable Form ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(254, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "To";
            // 
            // dtpDutyFrom
            // 
            this.dtpDutyFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpDutyFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDutyFrom.Location = new System.Drawing.Point(126, 64);
            this.dtpDutyFrom.Name = "dtpDutyFrom";
            this.dtpDutyFrom.Size = new System.Drawing.Size(122, 20);
            this.dtpDutyFrom.TabIndex = 10;
          //  this.dtpDutyFrom.ValueChanged += new System.EventHandler(this.dtpDutyFrom_ValueChanged);
            // 
            // dtpDutyTo
            // 
            this.dtpDutyTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpDutyTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDutyTo.Location = new System.Drawing.Point(279, 64);
            this.dtpDutyTo.Name = "dtpDutyTo";
            this.dtpDutyTo.Size = new System.Drawing.Size(122, 20);
            this.dtpDutyTo.TabIndex = 11;
        //    this.dtpDutyTo.ValueChanged += new System.EventHandler(this.dtpDutyTo_ValueChanged);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.BackColor = System.Drawing.Color.White;
            this.ucToolBar1.Close = true;
            this.ucToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Form_height = 0;
            this.ucToolBar1.Form_width = 0;
            this.ucToolBar1.Height1 = 0;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(1042, 19);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmDebitEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 465);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.dtpDutyTo);
            this.Controls.Add(this.dtpDutyFrom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtentryno);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDebitEntry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDebitEntry_FormClosed);
            this.Load += new System.EventHandler(this.frmDebitEntry_Load);
            this.Enter += new System.EventHandler(this.frmDebitEntry_Enter);
            this.Resize += new System.EventHandler(this.frmDebitEntry_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvcreditdetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtsalesaddlduty;
        private System.Windows.Forms.TextBox txtsaleshcessduty;
        private System.Windows.Forms.TextBox txtsalescessduty;
        private System.Windows.Forms.TextBox txtsalesexduty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private MyDataGridView dgvcreditdetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn details;
        private System.Windows.Forms.DataGridViewTextBoxColumn credit_excise;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit_Cess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit_SHCess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit_Addl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adjust_Excise;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adjust_Cess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adjust_SHCess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Adjust_Addl;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttl_amt;
        private System.Windows.Forms.Label label1;
        private UserDT dtpDate;
        private System.Windows.Forms.TextBox txtentryno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private UserDT dtpDutyFrom;
        private UserDT dtpDutyTo;
        private UCToolBar ucToolBar1;
    }
}