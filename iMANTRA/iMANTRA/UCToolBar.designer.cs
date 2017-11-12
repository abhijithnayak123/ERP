namespace iMANTRA
{
    partial class UCToolBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnUCMax = new iMANTRA.PopupButton();
            this.btnUCMin = new iMANTRA.PopupButton();
            this.btnUCClose = new iMANTRA.PopupButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Firebrick;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.btnUCMax);
            this.panel1.Controls.Add(this.btnUCMin);
            this.panel1.Controls.Add(this.btnUCClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 23);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(32, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblTitle_DoubleClick);
            this.lblTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDoubleClick);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // btnUCMax
            // 
            this.btnUCMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUCMax.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUCMax.BackColor = System.Drawing.Color.Transparent;
            this.btnUCMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUCMax.Dispddlfields = "";
            this.btnUCMax.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnUCMax.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUCMax.Font = new System.Drawing.Font("Webdings", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUCMax.ForeColor = System.Drawing.Color.White;
            this.btnUCMax.GradientBottom = System.Drawing.Color.Gray;
            this.btnUCMax.GradientTop = System.Drawing.SystemColors.Control;
            this.btnUCMax.Location = new System.Drawing.Point(377, -4);
            this.btnUCMax.Name = "btnUCMax";
            this.btnUCMax.Primaryddl = "";
            this.btnUCMax.Query_con = "";
            this.btnUCMax.Reftbltran_cd = "";
            this.btnUCMax.Size = new System.Drawing.Size(19, 26);
            this.btnUCMax.TabIndex = 4;
            this.btnUCMax.Tbl_nm = "";
            this.btnUCMax.Text = "c";
            this.btnUCMax.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUCMax.UseVisualStyleBackColor = false;
            this.btnUCMax.Click += new System.EventHandler(this.btnMax_Click);
            this.btnUCMax.MouseLeave += new System.EventHandler(this.btnMax_MouseLeave);
            this.btnUCMax.MouseHover += new System.EventHandler(this.btnMax_MouseHover);
            // 
            // btnUCMin
            // 
            this.btnUCMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUCMin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUCMin.BackColor = System.Drawing.Color.Transparent;
            this.btnUCMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUCMin.Dispddlfields = "";
            this.btnUCMin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUCMin.Font = new System.Drawing.Font("Webdings", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUCMin.ForeColor = System.Drawing.Color.White;
            this.btnUCMin.GradientBottom = System.Drawing.Color.Gray;
            this.btnUCMin.GradientTop = System.Drawing.SystemColors.Control;
            this.btnUCMin.Location = new System.Drawing.Point(359, -6);
            this.btnUCMin.Margin = new System.Windows.Forms.Padding(0);
            this.btnUCMin.Name = "btnUCMin";
            this.btnUCMin.Primaryddl = "";
            this.btnUCMin.Query_con = "";
            this.btnUCMin.Reftbltran_cd = "";
            this.btnUCMin.Size = new System.Drawing.Size(19, 29);
            this.btnUCMin.TabIndex = 3;
            this.btnUCMin.Tbl_nm = "";
            this.btnUCMin.Text = "0";
            this.btnUCMin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUCMin.UseVisualStyleBackColor = false;
            this.btnUCMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnUCMin.MouseLeave += new System.EventHandler(this.btnMin_MouseLeave);
            this.btnUCMin.MouseHover += new System.EventHandler(this.btnMin_MouseHover);
            // 
            // btnUCClose
            // 
            this.btnUCClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUCClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUCClose.BackColor = System.Drawing.Color.Transparent;
            this.btnUCClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUCClose.Dispddlfields = "";
            this.btnUCClose.FlatAppearance.BorderSize = 2;
            this.btnUCClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnUCClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUCClose.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUCClose.ForeColor = System.Drawing.Color.White;
            this.btnUCClose.GradientBottom = System.Drawing.Color.Gray;
            this.btnUCClose.GradientTop = System.Drawing.SystemColors.Control;
            this.btnUCClose.Location = new System.Drawing.Point(393, -4);
            this.btnUCClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnUCClose.Name = "btnUCClose";
            this.btnUCClose.Primaryddl = "";
            this.btnUCClose.Query_con = "";
            this.btnUCClose.Reftbltran_cd = "";
            this.btnUCClose.Size = new System.Drawing.Size(19, 26);
            this.btnUCClose.TabIndex = 2;
            this.btnUCClose.Tbl_nm = "";
            this.btnUCClose.Text = "r";
            this.btnUCClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUCClose.UseVisualStyleBackColor = false;
            this.btnUCClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnUCClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnUCClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // UCToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCToolBar";
            this.Size = new System.Drawing.Size(413, 23);
            this.Load += new System.EventHandler(this.UCToolBar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private PopupButton btnUCMax;
        private PopupButton btnUCMin;
        private PopupButton btnUCClose;






    }
}
