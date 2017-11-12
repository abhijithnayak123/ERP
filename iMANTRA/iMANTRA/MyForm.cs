using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_DL;
using iMANTRA_BL;
using iMANTRA_IL;

namespace iMANTRA
{
    public partial class MyForm : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string strSearchField = "AC_NM";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        public string Tran_id
        {
            get { return tran_id; }
            set { tran_id = value; }
        }

        public string Tran_cd
        {
            get { return tran_cd; }
            set { tran_cd = value; }
        }

        public string Tran_mode
        {
            get { return tran_mode; }
            set { tran_mode = value; }
        }

        public MyForm(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            if (objBASEFILEDS != null)
            {
                MessageBox.Show("Load");
            }
            else
            {
                MessageBox.Show("arg not passed" + e);
            }
        }
    }
}
