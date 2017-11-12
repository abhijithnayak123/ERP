namespace iMANTRA
{
    partial class frmORGModule
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
            this.btnCancel = new iMANTRA.PopupButton();
            this.btnDone = new iMANTRA.PopupButton();
            this.grpAddOnModule = new iMANTRA.Grouper();
            this.chkGraphics = new System.Windows.Forms.CheckBox();
            this.chkJobWork = new System.Windows.Forms.CheckBox();
            this.chkQuality = new System.Windows.Forms.CheckBox();
            this.chkProduction = new System.Windows.Forms.CheckBox();
            this.chkProcurement = new System.Windows.Forms.CheckBox();
            this.chkWarehouse = new System.Windows.Forms.CheckBox();
            this.chkPlanning = new System.Windows.Forms.CheckBox();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkMarketing = new System.Windows.Forms.CheckBox();
            this.grpBase = new iMANTRA.Grouper();
            this.rbtnExciseTrd = new System.Windows.Forms.RadioButton();
            this.rbtnExciseMfg = new System.Windows.Forms.RadioButton();
            this.rbtnBasic = new System.Windows.Forms.RadioButton();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grpAddOnModule.SuspendLayout();
            this.grpBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Dispddlfields = "";
            this.btnCancel.GradientBottom = System.Drawing.Color.Gray;
            this.btnCancel.GradientTop = System.Drawing.SystemColors.Control;
            this.btnCancel.Location = new System.Drawing.Point(359, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Primaryddl = "";
            this.btnCancel.Query_con = "";
            this.btnCancel.Reftbltran_cd = "";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 57;
            this.btnCancel.Tbl_nm = "";
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.Dispddlfields = "";
            this.btnDone.GradientBottom = System.Drawing.Color.Gray;
            this.btnDone.GradientTop = System.Drawing.SystemColors.Control;
            this.btnDone.Location = new System.Drawing.Point(439, 363);
            this.btnDone.Name = "btnDone";
            this.btnDone.Primaryddl = "";
            this.btnDone.Query_con = "";
            this.btnDone.Reftbltran_cd = "";
            this.btnDone.Size = new System.Drawing.Size(75, 25);
            this.btnDone.TabIndex = 56;
            this.btnDone.Tbl_nm = "";
            this.btnDone.Text = "&Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // grpAddOnModule
            // 
            this.grpAddOnModule.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpAddOnModule.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpAddOnModule.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpAddOnModule.BorderColor = System.Drawing.Color.Black;
            this.grpAddOnModule.BorderThickness = 1F;
            this.grpAddOnModule.Controls.Add(this.chkGraphics);
            this.grpAddOnModule.Controls.Add(this.chkJobWork);
            this.grpAddOnModule.Controls.Add(this.chkQuality);
            this.grpAddOnModule.Controls.Add(this.chkProduction);
            this.grpAddOnModule.Controls.Add(this.chkProcurement);
            this.grpAddOnModule.Controls.Add(this.chkWarehouse);
            this.grpAddOnModule.Controls.Add(this.chkPlanning);
            this.grpAddOnModule.Controls.Add(this.chkInventory);
            this.grpAddOnModule.Controls.Add(this.chkMarketing);
            this.grpAddOnModule.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpAddOnModule.GroupImage = null;
            this.grpAddOnModule.GroupTitle = "Add On Module";
            this.grpAddOnModule.Location = new System.Drawing.Point(4, 111);
            this.grpAddOnModule.Name = "grpAddOnModule";
            this.grpAddOnModule.Padding = new System.Windows.Forms.Padding(20);
            this.grpAddOnModule.PaintGroupBox = true;
            this.grpAddOnModule.RoundCorners = 1;
            this.grpAddOnModule.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpAddOnModule.ShadowControl = false;
            this.grpAddOnModule.ShadowThickness = 1;
            this.grpAddOnModule.Size = new System.Drawing.Size(512, 248);
            this.grpAddOnModule.TabIndex = 55;
            // 
            // chkGraphics
            // 
            this.chkGraphics.AutoSize = true;
            this.chkGraphics.Location = new System.Drawing.Point(23, 210);
            this.chkGraphics.Name = "chkGraphics";
            this.chkGraphics.Size = new System.Drawing.Size(187, 22);
            this.chkGraphics.TabIndex = 8;
            this.chkGraphics.Text = "GRAPHICAL REPORT";
            this.chkGraphics.UseVisualStyleBackColor = true;
            // 
            // chkJobWork
            // 
            this.chkJobWork.AutoSize = true;
            this.chkJobWork.Location = new System.Drawing.Point(261, 168);
            this.chkJobWork.Name = "chkJobWork";
            this.chkJobWork.Size = new System.Drawing.Size(97, 22);
            this.chkJobWork.TabIndex = 7;
            this.chkJobWork.Text = "JOBWORK";
            this.chkJobWork.UseVisualStyleBackColor = true;
            // 
            // chkQuality
            // 
            this.chkQuality.AutoSize = true;
            this.chkQuality.Location = new System.Drawing.Point(261, 130);
            this.chkQuality.Name = "chkQuality";
            this.chkQuality.Size = new System.Drawing.Size(97, 22);
            this.chkQuality.TabIndex = 6;
            this.chkQuality.Text = "QUALITY";
            this.chkQuality.UseVisualStyleBackColor = true;
            // 
            // chkProduction
            // 
            this.chkProduction.AutoSize = true;
            this.chkProduction.Location = new System.Drawing.Point(261, 88);
            this.chkProduction.Name = "chkProduction";
            this.chkProduction.Size = new System.Drawing.Size(127, 22);
            this.chkProduction.TabIndex = 5;
            this.chkProduction.Text = "PRODUCTION";
            this.chkProduction.UseVisualStyleBackColor = true;
            // 
            // chkProcurement
            // 
            this.chkProcurement.AutoSize = true;
            this.chkProcurement.Location = new System.Drawing.Point(261, 48);
            this.chkProcurement.Name = "chkProcurement";
            this.chkProcurement.Size = new System.Drawing.Size(137, 22);
            this.chkProcurement.TabIndex = 4;
            this.chkProcurement.Text = "PROCUREMENT";
            this.chkProcurement.UseVisualStyleBackColor = true;
            // 
            // chkWarehouse
            // 
            this.chkWarehouse.AutoSize = true;
            this.chkWarehouse.Location = new System.Drawing.Point(24, 168);
            this.chkWarehouse.Name = "chkWarehouse";
            this.chkWarehouse.Size = new System.Drawing.Size(117, 22);
            this.chkWarehouse.TabIndex = 3;
            this.chkWarehouse.Text = "WAREHOUSE";
            this.chkWarehouse.UseVisualStyleBackColor = true;
            // 
            // chkPlanning
            // 
            this.chkPlanning.AutoSize = true;
            this.chkPlanning.Location = new System.Drawing.Point(24, 126);
            this.chkPlanning.Name = "chkPlanning";
            this.chkPlanning.Size = new System.Drawing.Size(107, 22);
            this.chkPlanning.TabIndex = 2;
            this.chkPlanning.Text = "PLANNING";
            this.chkPlanning.UseVisualStyleBackColor = true;
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Location = new System.Drawing.Point(24, 84);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(117, 22);
            this.chkInventory.TabIndex = 1;
            this.chkInventory.Text = "INVENTORY";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkMarketing
            // 
            this.chkMarketing.AutoSize = true;
            this.chkMarketing.Location = new System.Drawing.Point(24, 44);
            this.chkMarketing.Name = "chkMarketing";
            this.chkMarketing.Size = new System.Drawing.Size(117, 22);
            this.chkMarketing.TabIndex = 0;
            this.chkMarketing.Text = "MARKETING";
            this.chkMarketing.UseVisualStyleBackColor = true;
            // 
            // grpBase
            // 
            this.grpBase.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpBase.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpBase.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpBase.BorderColor = System.Drawing.Color.Black;
            this.grpBase.BorderThickness = 1F;
            this.grpBase.Controls.Add(this.rbtnExciseTrd);
            this.grpBase.Controls.Add(this.rbtnExciseMfg);
            this.grpBase.Controls.Add(this.rbtnBasic);
            this.grpBase.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpBase.GroupImage = null;
            this.grpBase.GroupTitle = "Base Module";
            this.grpBase.Location = new System.Drawing.Point(4, 28);
            this.grpBase.Name = "grpBase";
            this.grpBase.Padding = new System.Windows.Forms.Padding(20);
            this.grpBase.PaintGroupBox = true;
            this.grpBase.RoundCorners = 1;
            this.grpBase.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpBase.ShadowControl = false;
            this.grpBase.ShadowThickness = 1;
            this.grpBase.Size = new System.Drawing.Size(512, 79);
            this.grpBase.TabIndex = 54;
            // 
            // rbtnExciseTrd
            // 
            this.rbtnExciseTrd.AutoSize = true;
            this.rbtnExciseTrd.Location = new System.Drawing.Point(338, 39);
            this.rbtnExciseTrd.Name = "rbtnExciseTrd";
            this.rbtnExciseTrd.Size = new System.Drawing.Size(96, 22);
            this.rbtnExciseTrd.TabIndex = 2;
            this.rbtnExciseTrd.TabStop = true;
            this.rbtnExciseTrd.Text = "TRADING";
            this.rbtnExciseTrd.UseVisualStyleBackColor = true;
            this.rbtnExciseTrd.CheckedChanged += new System.EventHandler(this.rbtnExciseTrd_CheckedChanged);
            // 
            // rbtnExciseMfg
            // 
            this.rbtnExciseMfg.AutoSize = true;
            this.rbtnExciseMfg.Location = new System.Drawing.Point(126, 39);
            this.rbtnExciseMfg.Name = "rbtnExciseMfg";
            this.rbtnExciseMfg.Size = new System.Drawing.Size(156, 22);
            this.rbtnExciseMfg.TabIndex = 1;
            this.rbtnExciseMfg.TabStop = true;
            this.rbtnExciseMfg.Text = "MANUFACTURING";
            this.rbtnExciseMfg.UseVisualStyleBackColor = true;
            this.rbtnExciseMfg.CheckedChanged += new System.EventHandler(this.rbtnExciseMfg_CheckedChanged);
            // 
            // rbtnBasic
            // 
            this.rbtnBasic.AutoSize = true;
            this.rbtnBasic.Location = new System.Drawing.Point(24, 39);
            this.rbtnBasic.Name = "rbtnBasic";
            this.rbtnBasic.Size = new System.Drawing.Size(76, 22);
            this.rbtnBasic.TabIndex = 0;
            this.rbtnBasic.TabStop = true;
            this.rbtnBasic.Text = "BASIC";
            this.rbtnBasic.UseVisualStyleBackColor = true;
            this.rbtnBasic.CheckedChanged += new System.EventHandler(this.rbtnBasic_CheckedChanged);
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
            this.ucToolBar1.Size = new System.Drawing.Size(520, 25);
            this.ucToolBar1.TabIndex = 53;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmORGModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(520, 393);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.grpAddOnModule);
            this.Controls.Add(this.grpBase);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmORGModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmORGModule_Load);
            this.grpAddOnModule.ResumeLayout(false);
            this.grpAddOnModule.PerformLayout();
            this.grpBase.ResumeLayout(false);
            this.grpBase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper grpBase;
        private System.Windows.Forms.RadioButton rbtnExciseTrd;
        private System.Windows.Forms.RadioButton rbtnExciseMfg;
        private System.Windows.Forms.RadioButton rbtnBasic;
        private Grouper grpAddOnModule;
        private System.Windows.Forms.CheckBox chkJobWork;
        private System.Windows.Forms.CheckBox chkQuality;
        private System.Windows.Forms.CheckBox chkProduction;
        private System.Windows.Forms.CheckBox chkProcurement;
        private System.Windows.Forms.CheckBox chkWarehouse;
        private System.Windows.Forms.CheckBox chkPlanning;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkMarketing;
        private PopupButton btnDone;
        private PopupButton btnCancel;
        private System.Windows.Forms.CheckBox chkGraphics;
    }
}