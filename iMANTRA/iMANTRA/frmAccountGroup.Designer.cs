namespace iMANTRA
{
    partial class frmAccountGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountGroup));
            this.grouper1 = new iMANTRA.Grouper();
            this.lblParentMan = new System.Windows.Forms.Label();
            this.lblGroupMan = new System.Windows.Forms.Label();
            this.btnParent = new iMANTRA.PopupButton();
            this.btnGroup = new iMANTRA.PopupButton();
            this.OTHER_DET = new iMANTRA.PopupButton();
            this.chkOverLimit = new System.Windows.Forms.CheckBox();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.lblCreditLimit = new System.Windows.Forms.Label();
            this.txtCreditDays = new System.Windows.Forms.TextBox();
            this.lblCreditDays = new System.Windows.Forms.Label();
            this.cmbLedger = new System.Windows.Forms.ComboBox();
            this.lblLedger = new System.Windows.Forms.Label();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.lblParent = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.grpbxSearch = new iMANTRA.Grouper();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tvGroup = new System.Windows.Forms.TreeView();
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.grouper1.SuspendLayout();
            this.grpbxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grouper1.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lblParentMan);
            this.grouper1.Controls.Add(this.lblGroupMan);
            this.grouper1.Controls.Add(this.btnParent);
            this.grouper1.Controls.Add(this.btnGroup);
            this.grouper1.Controls.Add(this.OTHER_DET);
            this.grouper1.Controls.Add(this.chkOverLimit);
            this.grouper1.Controls.Add(this.txtCreditLimit);
            this.grouper1.Controls.Add(this.lblCreditLimit);
            this.grouper1.Controls.Add(this.txtCreditDays);
            this.grouper1.Controls.Add(this.lblCreditDays);
            this.grouper1.Controls.Add(this.cmbLedger);
            this.grouper1.Controls.Add(this.lblLedger);
            this.grouper1.Controls.Add(this.txtParent);
            this.grouper1.Controls.Add(this.lblParent);
            this.grouper1.Controls.Add(this.txtGroup);
            this.grouper1.Controls.Add(this.lblGroup);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Basic Details";
            this.grouper1.Location = new System.Drawing.Point(467, 26);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = true;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 1;
            this.grouper1.Size = new System.Drawing.Size(458, 506);
            this.grouper1.TabIndex = 56;
            // 
            // lblParentMan
            // 
            this.lblParentMan.AutoSize = true;
            this.lblParentMan.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblParentMan.ForeColor = System.Drawing.Color.Red;
            this.lblParentMan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblParentMan.Location = new System.Drawing.Point(130, 82);
            this.lblParentMan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParentMan.Name = "lblParentMan";
            this.lblParentMan.Size = new System.Drawing.Size(15, 16);
            this.lblParentMan.TabIndex = 132;
            this.lblParentMan.Text = "*";
            // 
            // lblGroupMan
            // 
            this.lblGroupMan.AutoSize = true;
            this.lblGroupMan.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblGroupMan.ForeColor = System.Drawing.Color.Red;
            this.lblGroupMan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblGroupMan.Location = new System.Drawing.Point(127, 45);
            this.lblGroupMan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupMan.Name = "lblGroupMan";
            this.lblGroupMan.Size = new System.Drawing.Size(15, 16);
            this.lblGroupMan.TabIndex = 131;
            this.lblGroupMan.Text = "*";
            // 
            // btnParent
            // 
            this.btnParent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParent.BackgroundImage")));
            this.btnParent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnParent.Dispddlfields = "";
            this.btnParent.GradientBottom = System.Drawing.Color.Gray;
            this.btnParent.GradientTop = System.Drawing.SystemColors.Control;
            this.btnParent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnParent.IsQcd = false;
            this.btnParent.Location = new System.Drawing.Point(415, 82);
            this.btnParent.Margin = new System.Windows.Forms.Padding(0);
            this.btnParent.Name = "btnParent";
            this.btnParent.Primaryddl = "";
            this.btnParent.QcdCondition = "";
            this.btnParent.Query_con = "";
            this.btnParent.Reftbltran_cd = "";
            this.btnParent.Size = new System.Drawing.Size(32, 24);
            this.btnParent.TabIndex = 4;
            this.btnParent.Tbl_nm = "";
            this.btnParent.UseVisualStyleBackColor = true;
            this.btnParent.Click += new System.EventHandler(this.btnParent_Click);
            // 
            // btnGroup
            // 
            this.btnGroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGroup.BackgroundImage")));
            this.btnGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGroup.Dispddlfields = "";
            this.btnGroup.GradientBottom = System.Drawing.Color.Gray;
            this.btnGroup.GradientTop = System.Drawing.SystemColors.Control;
            this.btnGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGroup.IsQcd = false;
            this.btnGroup.Location = new System.Drawing.Point(415, 40);
            this.btnGroup.Margin = new System.Windows.Forms.Padding(0);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Primaryddl = "";
            this.btnGroup.QcdCondition = "";
            this.btnGroup.Query_con = "";
            this.btnGroup.Reftbltran_cd = "";
            this.btnGroup.Size = new System.Drawing.Size(32, 24);
            this.btnGroup.TabIndex = 2;
            this.btnGroup.Tbl_nm = "";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // OTHER_DET
            // 
            this.OTHER_DET.Dispddlfields = "";
            this.OTHER_DET.GradientBottom = System.Drawing.Color.Gray;
            this.OTHER_DET.GradientTop = System.Drawing.SystemColors.Control;
            this.OTHER_DET.IsQcd = false;
            this.OTHER_DET.Location = new System.Drawing.Point(153, 242);
            this.OTHER_DET.Name = "OTHER_DET";
            this.OTHER_DET.Primaryddl = "";
            this.OTHER_DET.QcdCondition = "";
            this.OTHER_DET.Query_con = "";
            this.OTHER_DET.Reftbltran_cd = "";
            this.OTHER_DET.Size = new System.Drawing.Size(192, 31);
            this.OTHER_DET.TabIndex = 14;
            this.OTHER_DET.Tbl_nm = "";
            this.OTHER_DET.Text = "Other Details";
            this.OTHER_DET.UseVisualStyleBackColor = true;
            this.OTHER_DET.Click += new System.EventHandler(this.OTHER_DET_Click);
            // 
            // chkOverLimit
            // 
            this.chkOverLimit.AutoSize = true;
            this.chkOverLimit.Location = new System.Drawing.Point(270, 200);
            this.chkOverLimit.Name = "chkOverLimit";
            this.chkOverLimit.Size = new System.Drawing.Size(137, 22);
            this.chkOverLimit.TabIndex = 8;
            this.chkOverLimit.Text = "Over Limit?";
            this.chkOverLimit.UseVisualStyleBackColor = true;
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.Location = new System.Drawing.Point(153, 200);
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(99, 26);
            this.txtCreditLimit.TabIndex = 7;
            this.txtCreditLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditLimit_KeyPress);
            // 
            // lblCreditLimit
            // 
            this.lblCreditLimit.AutoSize = true;
            this.lblCreditLimit.Location = new System.Drawing.Point(5, 200);
            this.lblCreditLimit.Name = "lblCreditLimit";
            this.lblCreditLimit.Size = new System.Drawing.Size(128, 18);
            this.lblCreditLimit.TabIndex = 10;
            this.lblCreditLimit.Text = "Credit Limit";
            // 
            // txtCreditDays
            // 
            this.txtCreditDays.Location = new System.Drawing.Point(153, 160);
            this.txtCreditDays.Name = "txtCreditDays";
            this.txtCreditDays.Size = new System.Drawing.Size(99, 26);
            this.txtCreditDays.TabIndex = 6;
            this.txtCreditDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditDays_KeyPress);
            // 
            // lblCreditDays
            // 
            this.lblCreditDays.AutoSize = true;
            this.lblCreditDays.Location = new System.Drawing.Point(5, 160);
            this.lblCreditDays.Name = "lblCreditDays";
            this.lblCreditDays.Size = new System.Drawing.Size(118, 18);
            this.lblCreditDays.TabIndex = 8;
            this.lblCreditDays.Text = "Credit Days";
            // 
            // cmbLedger
            // 
            this.cmbLedger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLedger.FormattingEnabled = true;
            this.cmbLedger.Location = new System.Drawing.Point(153, 120);
            this.cmbLedger.Margin = new System.Windows.Forms.Padding(0);
            this.cmbLedger.Name = "cmbLedger";
            this.cmbLedger.Size = new System.Drawing.Size(256, 26);
            this.cmbLedger.TabIndex = 5;
            this.cmbLedger.Validating += new System.ComponentModel.CancelEventHandler(this.cmbLedger_Validating);
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(5, 120);
            this.lblLedger.Margin = new System.Windows.Forms.Padding(0);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(148, 18);
            this.lblLedger.TabIndex = 6;
            this.lblLedger.Text = "Ledger Posting";
            // 
            // txtParent
            // 
            this.txtParent.Location = new System.Drawing.Point(153, 80);
            this.txtParent.Margin = new System.Windows.Forms.Padding(0);
            this.txtParent.Name = "txtParent";
            this.txtParent.Size = new System.Drawing.Size(258, 26);
            this.txtParent.TabIndex = 3;
            this.txtParent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtParent_KeyDown);
            this.txtParent.Validating += new System.ComponentModel.CancelEventHandler(this.txtParent_Validating);
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(5, 80);
            this.lblParent.Margin = new System.Windows.Forms.Padding(0);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(128, 18);
            this.lblParent.TabIndex = 3;
            this.lblParent.Text = "Parent Group";
            // 
            // txtGroup
            // 
            this.txtGroup.BackColor = System.Drawing.SystemColors.Window;
            this.txtGroup.Location = new System.Drawing.Point(153, 40);
            this.txtGroup.Margin = new System.Windows.Forms.Padding(0);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(256, 26);
            this.txtGroup.TabIndex = 1;
            this.txtGroup.Validating += new System.ComponentModel.CancelEventHandler(this.txtGroup_Validating);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(5, 40);
            this.lblGroup.Margin = new System.Windows.Forms.Padding(0);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(58, 18);
            this.lblGroup.TabIndex = 0;
            this.lblGroup.Text = "Group";
            // 
            // grpbxSearch
            // 
            this.grpbxSearch.BackgroundColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this.grpbxSearch.BackgroundGradientMode = iMANTRA.Grouper.GroupBoxGradientMode.None;
            this.grpbxSearch.BorderColor = System.Drawing.Color.Black;
            this.grpbxSearch.BorderThickness = 1F;
            this.grpbxSearch.Controls.Add(this.txtSearch);
            this.grpbxSearch.Controls.Add(this.tvGroup);
            this.grpbxSearch.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grpbxSearch.GroupImage = null;
            this.grpbxSearch.GroupTitle = "COA Structure";
            this.grpbxSearch.Location = new System.Drawing.Point(4, 26);
            this.grpbxSearch.Name = "grpbxSearch";
            this.grpbxSearch.Padding = new System.Windows.Forms.Padding(20);
            this.grpbxSearch.PaintGroupBox = true;
            this.grpbxSearch.RoundCorners = 1;
            this.grpbxSearch.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpbxSearch.ShadowControl = false;
            this.grpbxSearch.ShadowThickness = 1;
            this.grpbxSearch.Size = new System.Drawing.Size(460, 506);
            this.grpbxSearch.TabIndex = 55;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(8, 26);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(447, 26);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // tvGroup
            // 
            this.tvGroup.AllowDrop = true;
            this.tvGroup.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvGroup.Location = new System.Drawing.Point(5, 58);
            this.tvGroup.Name = "tvGroup";
            this.tvGroup.Size = new System.Drawing.Size(450, 442);
            this.tvGroup.TabIndex = 0;
            this.tvGroup.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvGroup_ItemDrag);
            this.tvGroup.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvGroup_NodeMouseClick);
            this.tvGroup.Click += new System.EventHandler(this.tvGroup_Click);
            this.tvGroup.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvGroup_DragDrop);
            this.tvGroup.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvGroup_DragEnter);
            this.tvGroup.DragOver += new System.Windows.Forms.DragEventHandler(this.tvGroup_DragOver);
            this.tvGroup.DragLeave += new System.EventHandler(this.tvGroup_DragLeave);
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
            this.ucToolBar1.Size = new System.Drawing.Size(931, 23);
            this.ucToolBar1.TabIndex = 54;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // frmAccountGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(931, 537);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Controls.Add(this.grpbxSearch);
            this.Controls.Add(this.ucToolBar1);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmAccountGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAccountGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmAccountGroup_Load);
            this.Enter += new System.EventHandler(this.frmAccountGroup_Enter);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.grpbxSearch.ResumeLayout(false);
            this.grpbxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UCToolBar ucToolBar1;
        private Grouper grpbxSearch;
        private Grouper grouper1;
        private System.Windows.Forms.TreeView tvGroup;
        private System.Windows.Forms.CheckBox chkOverLimit;
        private System.Windows.Forms.TextBox txtCreditLimit;
        private System.Windows.Forms.Label lblCreditLimit;
        private System.Windows.Forms.TextBox txtCreditDays;
        private System.Windows.Forms.Label lblCreditDays;
        private System.Windows.Forms.ComboBox cmbLedger;
        private System.Windows.Forms.Label lblLedger;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label lblGroup;
        private PopupButton OTHER_DET;
        private System.Windows.Forms.TextBox txtSearch;
        private PopupButton btnParent;
        private PopupButton btnGroup;
        private System.Windows.Forms.Label lblParentMan;
        private System.Windows.Forms.Label lblGroupMan;
    }
}