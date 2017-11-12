namespace iMANTRA
{
    partial class frmReportPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportPreview));
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.ucToolBar1.Margin = new System.Windows.Forms.Padding(0);
            this.ucToolBar1.Maximize = true;
            this.ucToolBar1.Minimize = true;
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Size = new System.Drawing.Size(718, 19);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 248);
            this.panel1.TabIndex = 0;
            // 
            // frmReportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(718, 248);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReportPreview_FormClosed);
            this.Load += new System.EventHandler(this.frmReportPreview_Load);
            this.Enter += new System.EventHandler(this.frmReportPreview_Enter);
            this.Resize += new System.EventHandler(this.frmReportPreview_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UCToolBar ucToolBar1;
    }
}