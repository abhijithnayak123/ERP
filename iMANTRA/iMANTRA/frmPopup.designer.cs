namespace iMANTRA
{
    partial class frmPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPopup));
            this.ucToolBar1 = new iMANTRA.UCToolBar();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.dgvpopup = new iMANTRA.MyDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpopup)).BeginInit();
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
            this.ucToolBar1.Size = new System.Drawing.Size(430, 19);
            this.ucToolBar1.TabIndex = 36;
            this.ucToolBar1.Titlebar = "Title";
            this.ucToolBar1.UCbackcolor = System.Drawing.Color.White;
            this.ucToolBar1.UCforcolor = System.Drawing.SystemColors.ControlText;
            this.ucToolBar1.Width1 = 0;
            // 
            // TextBox1
            // 
            this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox1.Location = new System.Drawing.Point(0, 21);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(429, 26);
            this.TextBox1.TabIndex = 1;
            this.TextBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.TextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            this.TextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            // 
            // dgvpopup
            // 
            this.dgvpopup.AllowUserToAddRows = false;
            this.dgvpopup.AllowUserToDeleteRows = false;
            this.dgvpopup.AllowUserToResizeRows = false;
            this.dgvpopup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvpopup.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvpopup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvpopup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpopup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvpopup.EnableHeadersVisualStyles = false;
            this.dgvpopup.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvpopup.height = 300;
            this.dgvpopup.Location = new System.Drawing.Point(1, 49);
            this.dgvpopup.Margin = new System.Windows.Forms.Padding(0);
            this.dgvpopup.MultiSelect = false;
            this.dgvpopup.Name = "dgvpopup";
            this.dgvpopup.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvpopup.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvpopup.RowHeadersVisible = false;
            this.dgvpopup.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvpopup.Size = new System.Drawing.Size(428, 300);
            this.dgvpopup.TabIndex = 0;
            this.dgvpopup.width = 428;
            this.dgvpopup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpopup_CellClick);
            this.dgvpopup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpopup_CellDoubleClick);
            this.dgvpopup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvpopup_KeyDown);
            // 
            // frmPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(430, 353);
            this.ControlBox = false;
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.dgvpopup);
            this.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPopup_Load);
            this.Resize += new System.EventHandler(this.frmPopup_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvpopup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyDataGridView dgvpopup;
        private System.Windows.Forms.TextBox TextBox1;
        private UCToolBar ucToolBar1;
    }
}