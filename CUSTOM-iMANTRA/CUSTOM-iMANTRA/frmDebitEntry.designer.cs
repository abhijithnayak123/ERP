namespace CUSTOM_iMANTRA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDebitEntry));
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvcreditdetails = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSHCessPay = new System.Windows.Forms.Label();
            this.lblAddlPay = new System.Windows.Forms.Label();
            this.lblCessPay = new System.Windows.Forms.Label();
            this.lblExcisePay = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsalesaddlduty = new System.Windows.Forms.TextBox();
            this.txtsaleshcessduty = new System.Windows.Forms.TextBox();
            this.txtsalescessduty = new System.Windows.Forms.TextBox();
            this.txtsalesexduty = new System.Windows.Forms.TextBox();
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
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcreditdetails)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.ucToolBar1.Size = new System.Drawing.Size(920, 19);
            this.ucToolBar1.TabIndex = 7;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(821, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(93, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(732, 360);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(85, 26);
            this.btnDone.TabIndex = 2;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvcreditdetails);
            this.groupBox2.Location = new System.Drawing.Point(2, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(914, 182);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Credit And Adjustable Details ";
            // 
            // dgvcreditdetails
            // 
            this.dgvcreditdetails.AllowUserToAddRows = false;
            this.dgvcreditdetails.AllowUserToDeleteRows = false;
            this.dgvcreditdetails.AllowUserToResizeRows = false;
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
            this.dgvcreditdetails.Location = new System.Drawing.Point(3, 22);
            this.dgvcreditdetails.Name = "dgvcreditdetails";
            this.dgvcreditdetails.RowHeadersVisible = false;
            this.dgvcreditdetails.Size = new System.Drawing.Size(908, 157);
            this.dgvcreditdetails.TabIndex = 0;
            this.dgvcreditdetails.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvcreditdetails_CellValidated);
            this.dgvcreditdetails.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvcreditdetails_CellValidating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSHCessPay);
            this.groupBox1.Controls.Add(this.lblAddlPay);
            this.groupBox1.Controls.Add(this.lblCessPay);
            this.groupBox1.Controls.Add(this.lblExcisePay);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtsalesaddlduty);
            this.groupBox1.Controls.Add(this.txtsaleshcessduty);
            this.groupBox1.Controls.Add(this.txtsalescessduty);
            this.groupBox1.Controls.Add(this.txtsalesexduty);
            this.groupBox1.Location = new System.Drawing.Point(2, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(914, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total Duty Payable Amount ";
            // 
            // lblSHCessPay
            // 
            this.lblSHCessPay.AutoSize = true;
            this.lblSHCessPay.ForeColor = System.Drawing.Color.Red;
            this.lblSHCessPay.Location = new System.Drawing.Point(521, 90);
            this.lblSHCessPay.Name = "lblSHCessPay";
            this.lblSHCessPay.Size = new System.Drawing.Size(68, 18);
            this.lblSHCessPay.TabIndex = 11;
            this.lblSHCessPay.Text = "label8";
            // 
            // lblAddlPay
            // 
            this.lblAddlPay.AutoSize = true;
            this.lblAddlPay.ForeColor = System.Drawing.Color.Blue;
            this.lblAddlPay.Location = new System.Drawing.Point(521, 120);
            this.lblAddlPay.Name = "lblAddlPay";
            this.lblAddlPay.Size = new System.Drawing.Size(68, 18);
            this.lblAddlPay.TabIndex = 10;
            this.lblAddlPay.Text = "label3";
            // 
            // lblCessPay
            // 
            this.lblCessPay.AutoSize = true;
            this.lblCessPay.ForeColor = System.Drawing.Color.Blue;
            this.lblCessPay.Location = new System.Drawing.Point(521, 57);
            this.lblCessPay.Name = "lblCessPay";
            this.lblCessPay.Size = new System.Drawing.Size(68, 18);
            this.lblCessPay.TabIndex = 9;
            this.lblCessPay.Text = "label2";
            // 
            // lblExcisePay
            // 
            this.lblExcisePay.AutoSize = true;
            this.lblExcisePay.ForeColor = System.Drawing.Color.Red;
            this.lblExcisePay.Location = new System.Drawing.Point(521, 26);
            this.lblExcisePay.Name = "lblExcisePay";
            this.lblExcisePay.Size = new System.Drawing.Size(68, 18);
            this.lblExcisePay.TabIndex = 8;
            this.lblExcisePay.Text = "label1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "Addl. Duty Amount ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(328, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Total S and H Cess Duty Amount  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total Edu. Cess Duty Amount ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total Excise Duty Amount";
            // 
            // txtsalesaddlduty
            // 
            this.txtsalesaddlduty.Location = new System.Drawing.Point(340, 116);
            this.txtsalesaddlduty.Name = "txtsalesaddlduty";
            this.txtsalesaddlduty.ReadOnly = true;
            this.txtsalesaddlduty.Size = new System.Drawing.Size(143, 26);
            this.txtsalesaddlduty.TabIndex = 3;
            // 
            // txtsaleshcessduty
            // 
            this.txtsaleshcessduty.Location = new System.Drawing.Point(340, 87);
            this.txtsaleshcessduty.Name = "txtsaleshcessduty";
            this.txtsaleshcessduty.ReadOnly = true;
            this.txtsaleshcessduty.Size = new System.Drawing.Size(143, 26);
            this.txtsaleshcessduty.TabIndex = 2;
            // 
            // txtsalescessduty
            // 
            this.txtsalescessduty.Location = new System.Drawing.Point(340, 55);
            this.txtsalescessduty.Name = "txtsalescessduty";
            this.txtsalescessduty.ReadOnly = true;
            this.txtsalescessduty.Size = new System.Drawing.Size(143, 26);
            this.txtsalescessduty.TabIndex = 1;
            // 
            // txtsalesexduty
            // 
            this.txtsalesexduty.Location = new System.Drawing.Point(340, 22);
            this.txtsalesexduty.Name = "txtsalesexduty";
            this.txtsalesexduty.ReadOnly = true;
            this.txtsalesexduty.Size = new System.Drawing.Size(143, 26);
            this.txtsalesexduty.TabIndex = 0;
            // 
            // details
            // 
            this.details.DataPropertyName = "details";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.details.DefaultCellStyle = dataGridViewCellStyle1;
            this.details.HeaderText = "";
            this.details.Name = "details";
            this.details.ReadOnly = true;
            // 
            // credit_excise
            // 
            this.credit_excise.DataPropertyName = "credit_excise";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.credit_excise.DefaultCellStyle = dataGridViewCellStyle2;
            this.credit_excise.HeaderText = "Credit BED";
            this.credit_excise.Name = "credit_excise";
            this.credit_excise.ReadOnly = true;
            // 
            // Credit_Cess
            // 
            this.Credit_Cess.DataPropertyName = "Credit_Cess";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Credit_Cess.DefaultCellStyle = dataGridViewCellStyle3;
            this.Credit_Cess.HeaderText = "Credit Cess";
            this.Credit_Cess.Name = "Credit_Cess";
            this.Credit_Cess.ReadOnly = true;
            // 
            // Credit_SHCess
            // 
            this.Credit_SHCess.DataPropertyName = "Credit_SHCess";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Credit_SHCess.DefaultCellStyle = dataGridViewCellStyle4;
            this.Credit_SHCess.HeaderText = "Credit S&H Cess";
            this.Credit_SHCess.Name = "Credit_SHCess";
            this.Credit_SHCess.ReadOnly = true;
            // 
            // Credit_Addl
            // 
            this.Credit_Addl.DataPropertyName = "Credit_Addl";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Credit_Addl.DefaultCellStyle = dataGridViewCellStyle5;
            this.Credit_Addl.HeaderText = "Credit AED";
            this.Credit_Addl.Name = "Credit_Addl";
            this.Credit_Addl.ReadOnly = true;
            // 
            // Adjust_Excise
            // 
            this.Adjust_Excise.DataPropertyName = "Adjust_Excise";
            this.Adjust_Excise.HeaderText = "Adjust BED";
            this.Adjust_Excise.Name = "Adjust_Excise";
            // 
            // Adjust_Cess
            // 
            this.Adjust_Cess.DataPropertyName = "Adjust_Cess";
            this.Adjust_Cess.HeaderText = "Adjust Cess";
            this.Adjust_Cess.Name = "Adjust_Cess";
            // 
            // Adjust_SHCess
            // 
            this.Adjust_SHCess.DataPropertyName = "Adjust_SHCess";
            this.Adjust_SHCess.HeaderText = "Adjust S&H Cess";
            this.Adjust_SHCess.Name = "Adjust_SHCess";
            // 
            // Adjust_Addl
            // 
            this.Adjust_Addl.DataPropertyName = "Adjust_Addl";
            this.Adjust_Addl.HeaderText = "Adjust AED";
            this.Adjust_Addl.Name = "Adjust_Addl";
            // 
            // ttl_amt
            // 
            this.ttl_amt.DataPropertyName = "ttl_amt";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ttl_amt.DefaultCellStyle = dataGridViewCellStyle6;
            this.ttl_amt.HeaderText = "Total Amount";
            this.ttl_amt.Name = "ttl_amt";
            this.ttl_amt.ReadOnly = true;
            // 
            // frmDebitEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(920, 393);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDebitEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDebitEntry_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvcreditdetails)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.DataGridView dgvcreditdetails;
        private PopupButton btnDone;
        private PopupButton btnCancel;
        private System.Windows.Forms.Label lblSHCessPay;
        private System.Windows.Forms.Label lblAddlPay;
        private System.Windows.Forms.Label lblCessPay;
        private System.Windows.Forms.Label lblExcisePay;
        private UCToolBar ucToolBar1;
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
    }
}