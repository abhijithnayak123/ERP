using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using iMANTRA_BL;
using System.Collections;


namespace CUSTOM_iMANTRA
{
    public partial class frmCustomReportCond : CustomBaseForm
    {
        Hashtable iHTFILTER = new Hashtable();

        public Hashtable IHTFILTER
        {
            get { return iHTFILTER; }
            set { iHTFILTER = value; }
        }

        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmCustomReportCond()
        {
            InitializeComponent();
        }
        private void frmCustomReportCond_Load(object sender, EventArgs e)
        {
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Report Filter";
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            IHTFILTER["spl_cond"] = txtSplCon.Text;
            this.Close();
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                bool flgItemEdit = false;
                if (e.KeyData == Keys.F2)
                {
                    if (!flgItemEdit)
                    {
                        frmPopup objfrmPopup = new frmPopup("SEMAIN", "SE", "AREID,ARE_NO", "ARE_NO;ARE NO", "Please select", "");
                        objfrmPopup.ResultFieldValue = txtSplCon.Text;
                        objfrmPopup.ShowDialog();
                        txtSplCon.Text = objfrmPopup.ResultFieldValue.Split(',')[1];
                     }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
