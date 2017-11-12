using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmNarration : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string _fld_nm = "", _fld_value = "";

        public string Fld_value
        {
            get { return _fld_value; }
            set { _fld_value = value; }
        }

        public string Fld_nm
        {
            get { return _fld_nm; }
            set { _fld_nm = value; }
        }

        public frmNarration()
        {
            InitializeComponent();
        }

        private void frmNarration_Load(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(objBASEFILEDS.X_gridAccount, objBASEFILEDS.Y_gridAccount, this.Width, this.Height);
            rtbNarr.Text = Fld_value;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "Transaction");
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                btnDone.Visible = true;
            }
            else
            {
                btnDone.Visible = false;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Fld_value = rtbNarr.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
