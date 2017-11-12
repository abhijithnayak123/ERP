namespace iMANTRA
{
    partial class frmSeries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeries));
            this.grbSeries = new iMANTRA.Grouper();
            this.btnValid = new PopupButton();
            this.btnSeries = new PopupButton();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.txtValid = new System.Windows.Forms.TextBox();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblValid = new System.Windows.Forms.Label();
            this.lblSeries = new System.Windows.Forms.Label();
            this.grbValidity = new iMANTRA.Grouper();
            this.lblDeactDt = new System.Windows.Forms.Label();
            this.dtpDate = new UserDT();
            this.chkDeactivate = new System.Windows.Forms.CheckBox();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grbSeries.SuspendLayout();
            this.grbValidity.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbSeries
            // 
            this.grbSeries.BackgroundColor = System.Drawing.Color.Transparent;
            this.grbSeries.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grbSeries.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grbSeries.BorderColor = System.Drawing.Color.Black;
            this.grbSeries.BorderThickness = 1F;
            this.grbSeries.Controls.Add(this.btnValid);
            this.grbSeries.Controls.Add(this.btnSeries);
            this.grbSeries.Controls.Add(this.txtPrefix);
            this.grbSeries.Controls.Add(this.txtSeries);
            this.grbSeries.Controls.Add(this.txtSuffix);
            this.grbSeries.Controls.Add(this.txtValid);
            this.grbSeries.Controls.Add(this.lblSuffix);
            this.grbSeries.Controls.Add(this.lblPrefix);
            this.grbSeries.Controls.Add(this.lblValid);
            this.grbSeries.Controls.Add(this.lblSeries);
            this.grbSeries.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grbSeries.GroupImage = null;
            this.grbSeries.GroupTitle = "Series Setting";
            this.grbSeries.Location = new System.Drawing.Point(4, 28);
            this.grbSeries.Name = "grbSeries";
            this.grbSeries.Padding = new System.Windows.Forms.Padding(20);
            this.grbSeries.PaintGroupBox = true;
            this.grbSeries.RoundCorners = 1;
            this.grbSeries.ShadowColor = System.Drawing.Color.DarkGray;
            this.grbSeries.ShadowControl = false;
            this.grbSeries.ShadowThickness = 1;
            this.grbSeries.Size = new System.Drawing.Size(399, 175);
            this.grbSeries.TabIndex = 0;
            this.grbSeries.TabStop = true;
            // 
            // btnValid
            // 
            this.btnValid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnValid.BackgroundImage")));
            this.btnValid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValid.Location = new System.Drawing.Point(351, 143);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(36, 23);
            this.btnValid.TabIndex = 6;
            this.btnValid.UseVisualStyleBackColor = true;
            this.btnValid.Click += new System.EventHandler(this.btnValid_Click);
            // 
            // btnSeries
            // 
            this.btnSeries.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSeries.BackgroundImage")));
            this.btnSeries.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSeries.Location = new System.Drawing.Point(351, 32);
            this.btnSeries.Name = "btnSeries";
            this.btnSeries.Size = new System.Drawing.Size(36, 23);
            this.btnSeries.TabIndex = 2;
            this.btnSeries.UseVisualStyleBackColor = true;
            this.btnSeries.Click += new System.EventHandler(this.btnSeries_Click);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(126, 68);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(217, 26);
            this.txtPrefix.TabIndex = 3;
            // 
            // txtSeries
            // 
            this.txtSeries.Location = new System.Drawing.Point(126, 32);
            this.txtSeries.Name = "txtSeries";
            this.txtSeries.Size = new System.Drawing.Size(217, 26);
            this.txtSeries.TabIndex = 1;
            this.txtSeries.Validating += new System.ComponentModel.CancelEventHandler(this.txtSeries_Validating);
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(126, 105);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(217, 26);
            this.txtSuffix.TabIndex = 4;
            // 
            // txtValid
            // 
            this.txtValid.Location = new System.Drawing.Point(126, 143);
            this.txtValid.Name = "txtValid";
            this.txtValid.Size = new System.Drawing.Size(217, 26);
            this.txtValid.TabIndex = 5;
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(8, 108);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(68, 18);
            this.lblSuffix.TabIndex = 3;
            this.lblSuffix.Text = "Suffix";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(8, 71);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(68, 18);
            this.lblPrefix.TabIndex = 2;
            this.lblPrefix.Text = "Prefix";
            // 
            // lblValid
            // 
            this.lblValid.AutoSize = true;
            this.lblValid.Location = new System.Drawing.Point(8, 146);
            this.lblValid.Name = "lblValid";
            this.lblValid.Size = new System.Drawing.Size(88, 18);
            this.lblValid.TabIndex = 1;
            this.lblValid.Text = "Valid In";
            // 
            // lblSeries
            // 
            this.lblSeries.AutoSize = true;
            this.lblSeries.Location = new System.Drawing.Point(8, 35);
            this.lblSeries.Name = "lblSeries";
            this.lblSeries.Size = new System.Drawing.Size(68, 18);
            this.lblSeries.TabIndex = 0;
            this.lblSeries.Text = "Series";
            // 
            // grbValidity
            // 
            this.grbValidity.BackgroundColor = System.Drawing.Color.Transparent;
            this.grbValidity.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grbValidity.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grbValidity.BorderColor = System.Drawing.Color.Black;
            this.grbValidity.BorderThickness = 1F;
            this.grbValidity.Controls.Add(this.lblDeactDt);
            this.grbValidity.Controls.Add(this.dtpDate);
            this.grbValidity.Controls.Add(this.chkDeactivate);
            this.grbValidity.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grbValidity.GroupImage = null;
            this.grbValidity.GroupTitle = "Validity Setting";
            this.grbValidity.Location = new System.Drawing.Point(4, 208);
            this.grbValidity.Name = "grbValidity";
            this.grbValidity.Padding = new System.Windows.Forms.Padding(20);
            this.grbValidity.PaintGroupBox = true;
            this.grbValidity.RoundCorners = 1;
            this.grbValidity.ShadowColor = System.Drawing.Color.DarkGray;
            this.grbValidity.ShadowControl = false;
            this.grbValidity.ShadowThickness = 1;
            this.grbValidity.Size = new System.Drawing.Size(399, 65);
            this.grbValidity.TabIndex = 1;
            this.grbValidity.TabStop = true;
            // 
            // lblDeactDt
            // 
            this.lblDeactDt.AutoSize = true;
            this.lblDeactDt.Location = new System.Drawing.Point(145, 33);
            this.lblDeactDt.Name = "lblDeactDt";
            this.lblDeactDt.Size = new System.Drawing.Size(48, 18);
            this.lblDeactDt.TabIndex = 9;
            this.lblDeactDt.Text = "From";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(202, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(141, 26);
            this.dtpDate.TabIndex = 8;
            // 
            // chkDeactivate
            // 
            this.chkDeactivate.AutoSize = true;
            this.chkDeactivate.Location = new System.Drawing.Point(10, 32);
            this.chkDeactivate.Name = "chkDeactivate";
            this.chkDeactivate.Size = new System.Drawing.Size(127, 22);
            this.chkDeactivate.TabIndex = 7;
            this.chkDeactivate.Text = "Deactivate";
            this.chkDeactivate.UseVisualStyleBackColor = true;
            this.chkDeactivate.CheckedChanged += new System.EventHandler(this.chkDeactivate_CheckedChanged);
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(407, 23);
            this.ucToolBar1.TabIndex = 38;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(407, 278);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.grbValidity);
            this.Controls.Add(this.grbSeries);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSeries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSeries_FormClosed);
            this.Load += new System.EventHandler(this.frmSeries_Load);
            this.Enter += new System.EventHandler(this.frmSeries_Enter);
            this.grbSeries.ResumeLayout(false);
            this.grbSeries.PerformLayout();
            this.grbValidity.ResumeLayout(false);
            this.grbValidity.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grbSeries;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblValid;
        private System.Windows.Forms.Label lblSeries;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TextBox txtSeries;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.TextBox txtValid;
        private PopupButton btnValid;
        private PopupButton btnSeries;
        private Grouper grbValidity;
        private System.Windows.Forms.Label lblDeactDt;
        private UserDT dtpDate;
        private System.Windows.Forms.CheckBox chkDeactivate;
        private UCToolBar ucToolBar1;
    }
}