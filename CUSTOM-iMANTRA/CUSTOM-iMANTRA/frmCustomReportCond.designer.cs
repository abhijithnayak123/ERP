namespace CUSTOM_iMANTRA
{
    partial class frmCustomReportCond
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
            this.txtSplCon = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.btnDone = new CUSTOM_iMANTRA.PopupButton();
            this.ucToolBar1 = new CUSTOM_iMANTRA.UCToolBar();
            this.SuspendLayout();
            // 
            // txtSplCon
            // 
            this.txtSplCon.Location = new System.Drawing.Point(70, 27);
            this.txtSplCon.Name = "txtSplCon";
            this.txtSplCon.Size = new System.Drawing.Size(280, 26);
            this.txtSplCon.TabIndex = 2;
            this.txtSplCon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lbl1.Location = new System.Drawing.Point(14, 30);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(54, 13);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "ARE NO";
            // 
            // btnDone
            // 
            this.btnDone.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDone.Dispddlfields = "";
            this.btnDone.Location = new System.Drawing.Point(274, 56);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(75, 25);
            this.btnDone.TabIndex = 9;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
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
            this.ucToolBar1.Size = new System.Drawing.Size(364, 19);
            this.ucToolBar1.TabIndex = 10;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.Firebrick;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmCustomReportCond
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(364, 83);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txtSplCon);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomReportCond";
            this.Text = "";
            this.Load += new System.EventHandler(this.frmCustomReportCond_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSplCon;
        private System.Windows.Forms.Label lbl1;
        private PopupButton btnDone;
        private UCToolBar ucToolBar1;
    }
}