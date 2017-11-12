namespace iMANTRA
{
    partial class frmBakUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBakUp));
            this.lblWait = new System.Windows.Forms.Label();
            this.btnCancel = new iMANTRA.PopupButton();
            this.btnProceed = new iMANTRA.PopupButton();
            this.btnHeaderNm = new iMANTRA.PopupButton();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblnewbakup = new System.Windows.Forms.Label();
            this.lblOld = new System.Windows.Forms.Label();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.SuspendLayout();
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblWait.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWait.Font = new System.Drawing.Font("Courier New", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.ForeColor = System.Drawing.Color.Maroon;
            this.lblWait.Location = new System.Drawing.Point(0, 19);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(572, 23);
            this.lblWait.TabIndex = 44;
            this.lblWait.Text = "Please Wait....";
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(490, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnProceed
            // 
            this.btnProceed.Dispddlfields = "";
            this.btnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProceed.Location = new System.Drawing.Point(401, 141);
            this.btnProceed.Name = "btnProceed";
            this.btnProceed.Primaryddl = "";
            this.btnProceed.Query_con = "";
            this.btnProceed.Reftbltran_cd = "";
            this.btnProceed.Size = new System.Drawing.Size(75, 25);
            this.btnProceed.TabIndex = 42;
            this.btnProceed.Tbl_nm = "";
            this.btnProceed.Text = "&Proceed";
            this.btnProceed.UseVisualStyleBackColor = true;
            this.btnProceed.Click += new System.EventHandler(this.btnProceed_Click);
            // 
            // btnHeaderNm
            // 
            this.btnHeaderNm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHeaderNm.BackgroundImage")));
            this.btnHeaderNm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHeaderNm.Dispddlfields = "";
            this.btnHeaderNm.Location = new System.Drawing.Point(532, 109);
            this.btnHeaderNm.Margin = new System.Windows.Forms.Padding(0);
            this.btnHeaderNm.Name = "btnHeaderNm";
            this.btnHeaderNm.Primaryddl = "";
            this.btnHeaderNm.Query_con = "";
            this.btnHeaderNm.Reftbltran_cd = "";
            this.btnHeaderNm.Size = new System.Drawing.Size(36, 25);
            this.btnHeaderNm.TabIndex = 41;
            this.btnHeaderNm.Tbl_nm = "";
            this.btnHeaderNm.UseVisualStyleBackColor = true;
            this.btnHeaderNm.Click += new System.EventHandler(this.btnHeaderNm_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(103, 109);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(427, 22);
            this.txtPath.TabIndex = 40;
            // 
            // lblnewbakup
            // 
            this.lblnewbakup.Location = new System.Drawing.Point(5, 99);
            this.lblnewbakup.Name = "lblnewbakup";
            this.lblnewbakup.Size = new System.Drawing.Size(92, 45);
            this.lblnewbakup.TabIndex = 39;
            this.lblnewbakup.Text = "New Back Up Path";
            // 
            // lblOld
            // 
            this.lblOld.Location = new System.Drawing.Point(5, 46);
            this.lblOld.Margin = new System.Windows.Forms.Padding(0);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(563, 50);
            this.lblOld.TabIndex = 38;
            this.lblOld.Text = "label1";
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(572, 19);
            this.ucToolBar1.TabIndex = 37;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmBakUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(572, 173);
            this.ControlBox = false;
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnProceed);
            this.Controls.Add(this.btnHeaderNm);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblnewbakup);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBakUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBakUp_Load);
            this.Resize += new System.EventHandler(this.frmBakUp_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private iMANTRA.UCToolBar ucToolBar1;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.Label lblnewbakup;
        private System.Windows.Forms.TextBox txtPath;
        private PopupButton btnHeaderNm;
        private PopupButton btnProceed;
        private PopupButton btnCancel;
        private System.Windows.Forms.Label lblWait;
    }
}