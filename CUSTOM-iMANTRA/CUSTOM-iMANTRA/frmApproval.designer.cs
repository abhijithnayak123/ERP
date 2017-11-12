namespace CUSTOM_iMANTRA
{
    partial class frmApproval
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.cmbApprove = new System.Windows.Forms.ComboBox();
            this.btnCancel = new CUSTOM_iMANTRA.PopupButton();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lbl1.Location = new System.Drawing.Point(10, 39);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(138, 18);
            this.lbl1.TabIndex = 14;
            this.lbl1.Text = "Authorised by";
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
            this.ucToolBar1.Size = new System.Drawing.Size(431, 19);
            this.ucToolBar1.TabIndex = 12;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // cmbApprove
            // 
            this.cmbApprove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApprove.FormattingEnabled = true;
            this.cmbApprove.Location = new System.Drawing.Point(167, 36);
            this.cmbApprove.Name = "cmbApprove";
            this.cmbApprove.Size = new System.Drawing.Size(252, 26);
            this.cmbApprove.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.Location = new System.Drawing.Point(326, 68);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(93, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(236, 68);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(84, 25);
            this.btnDone.TabIndex = 16;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // frmApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(431, 101);
            this.Controls.Add(this.cmbApprove);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.ucToolBar1);
            this.Name = "frmApproval";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmApproval_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cmbApprove;
        private PopupButton btnCancel;
        private PopupButton btnDone;
    }
}