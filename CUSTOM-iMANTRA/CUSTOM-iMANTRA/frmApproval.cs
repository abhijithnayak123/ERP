using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using iMANTRA_BL;
using System.Collections;
using iMANTRA_iniL;

namespace CUSTOM_iMANTRA
{
    public partial class frmApproval : CustomBaseForm
    {
        Hashtable iHTFILTER = new Hashtable();
        Ini objIni = new Ini();

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

        public frmApproval()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (cmbApprove.SelectedIndex == 0)
            {
                AutoClosingMessageBox.Show("Please Select Valid Authorised User");
            }
            else
            {
                if (objBLFD.HTMAIN.Contains("authorse_by"))
                {
                    objBLFD.HTMAIN["authorse_by"] = cmbApprove.SelectedValue != null ? cmbApprove.SelectedValue : "";
                    if (objBLFD.HTMAIN["authorse_by"].ToString() == "NOT APPLICABLE")
                    {
                       // objBLFD.HTMAIN["i_approved"] = true;
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Add Authorised By field");
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmApproval_Load(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source") + ";initial catalog=InodeMFG;user=" + objIni.GetKeyFieldValue("SQL", "user") + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd"));
            try
            {
                string strQuery = "select top 1 ' ' user_nm from login_mast union all select top 1 'NOT APPLICABLE' user_nm from login_mast union all select distinct lm.user_nm from login_mast lm inner join roles_mapping rm on lm.userid=rm.userid inner join rights rh on rm.intRoleId=rm.intRoleId and approve_access=1 inner join cmod cd on cd.module_id=rh.module_id where cd.module_name='" + objBLFD.Tran_nm + "' and rm.fin_yr='" + objBLFD.ObjCompany.Fin_yr + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strQuery, con1);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    cmbApprove.DataSource = ds.Tables[0];
                    cmbApprove.DisplayMember = "user_nm";
                    cmbApprove.ValueMember = "user_nm";
                    cmbApprove.Update();
                }
                if (ACTIVE_BL.Tran_mode == "view_mode")
                {
                    cmbApprove.Enabled = false;
                    btnDone.Enabled = false;
                }
                else
                {
                    cmbApprove.Enabled = true;
                    btnDone.Enabled = true;
                }
                if (objBLFD.HTMAIN.Contains("authorse_by"))
                {
                    cmbApprove.Text = objBLFD.HTMAIN["authorse_by"] != null ? objBLFD.HTMAIN["authorse_by"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (con1 != null)
                {
                    con1.Close();
                }
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Approval";
        }
    }
}
