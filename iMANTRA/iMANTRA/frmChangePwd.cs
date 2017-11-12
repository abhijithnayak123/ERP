using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using iMANTRA_DL;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using System.Reflection;
using iMANTRA_IL;
using iMANTRA_iniL;
using CUSTOM_iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmChangePwd : BaseClass
    {
        private Ini objIni = new Ini();
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

        private string tran_mode = "view_mode", tran_cd, tran_id;

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
        public frmChangePwd(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmChangePwd_Load(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            txNpwd.ReadOnly = true;
            txtNRpwd.ReadOnly = true;

            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;
        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                switch (tran_mode)
                {
                    case "add_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (!(c is Label)) c.Enabled = false;
                        }
                        break;

                    case "edit_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (c is GroupBox)
                            {
                                ((GroupBox)c).Enabled = true;
                            }
                            if (c is TextBox)
                            {
                                ((TextBox)c).Clear();
                            }
                        }
                        ViewUserDetails();
                        txtOldPwd.Clear();
                        txtOldPwd.Enabled = true;
                        break;

                    case "view_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (c is GroupBox)
                            {
                                ((GroupBox)c).Enabled = false;
                            }

                            if (c is TextBox)
                            {
                                ((TextBox)c).Clear();
                            }
                        }
                        ViewUserDetails();
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override bool SendMessageToClient(BL_BASEFIELD objBLFD, string msg)
        {
            objBASEFILEDS = objBLFD;
            if (msg != "SAVE")
            {
                DisplayControlsonMode(objBLFD.Tran_mode);
            }
            else
            {
                return Click_Save();
            }
            return true;
        }

        private bool Click_Save()
        {
            if (txtOldPwd.Text != "" && txNpwd.Text != "" && txtNRpwd.Text != "")
            {
                if (txNpwd.Text == txtNRpwd.Text)
                {
                    objBASEFILEDS.HTMAIN["pwd"] = txNpwd.Text;
                    objBASEFILEDS.HTMAIN["r_pwd"] = txtNRpwd.Text;
                }
                else
                {
                    AutoClosingMessageBox.Show("passwords don't match","Validation",3000);
                    txNpwd.Clear();
                    txtNRpwd.Clear();
                    return false;
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Enter all fields","Validation",3000);
                return false;
            }
            return true;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (txNpwd.Text != " " && txNpwd.Text != null)
            {
                if (txtNRpwd.Text == txNpwd.Text)
                {
                    objBASEFILEDS.HTMAIN["r_pwd"] = txNpwd.Text;
                }
                else
                {
                    AutoClosingMessageBox.Show("Password doesn't match","Validation",3000);
                    txtNRpwd.Text = "";

                }
            }
            else
            {
                AutoClosingMessageBox.Show("Enter Password","Validation",3000);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtOldPwd.Validating -= new CancelEventHandler(txtOldPwd_Validating);
            txNpwd.Validating -= new CancelEventHandler(txNpwd_Validating_1);
            txtNRpwd.Validating -= new CancelEventHandler(txtNRpwd_Validating_1);
            this.Close();
        }

        private void frmChangePwd_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                objBASEFILEDS.Tran_mode_type = "edit_only";
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmChangePwd_FormClosed(object sender, FormClosedEventArgs e)
        {
           if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void ViewUserDetails()
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");

            DataSet ds = objDL_ADAPTER.dsquery("select * from Login_mast where user_nm='" + objBASEFILEDS.ObjLoginUser.CurUser + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if ((ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0))
            {
                if (objBASEFILEDS.Tran_mode == "edit_mode")
                {
                    txtOldPwd.Enabled = true;
                    if (txtOldPwd.Text != "")
                    {
                        if (ds.Tables[0].Rows[0]["pwd"].ToString() == txtOldPwd.Text)
                        {
                            txNpwd.Enabled = true;
                            txtNRpwd.Enabled = true;
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("passwords don't match","Validation",3000);
                            txNpwd.Enabled = false;
                            txtNRpwd.Enabled = false;
                        }
                    }
                }
                objBASEFILEDS.Tran_id = ds.Tables[0].Rows[0]["userid"].ToString();
            }
            else
            {
                txNpwd.Enabled = false;
                txtNRpwd.Enabled = false;
            }
            objBASEFILEDS.HTMAIN["userid"] = objBASEFILEDS.Tran_id;
        }

        private void frmChangePwd_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void txNpwd_Validating_1(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtOldPwd.Text != "")
                {
                    if (txNpwd.Text == "")
                    {
                        AutoClosingMessageBox.Show("Please enter New Password","Validation",3000);
                        e.Cancel = true;
                    }
                }
                else
                {
                    txNpwd.Validating -= new CancelEventHandler(txNpwd_Validating_1);
                }
            }
        }

        private void txtNRpwd_Validating_1(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtOldPwd.Text != "")
                {
                    if (txNpwd.Text != "" && txtNRpwd.Text != "")
                    {
                        if (txNpwd.Text != txtNRpwd.Text)
                        {
                            AutoClosingMessageBox.Show("passwords don't match","Validation",3000);
                            txtNRpwd.Clear();
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter Password Fields","Validation",3000);
                        e.Cancel = true;
                    }
                }
                else
                {
                    txtNRpwd.Validating -= new CancelEventHandler(txtNRpwd_Validating_1);
                }
            }
        }

        private void txtOldPwd_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtOldPwd.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Password","Validation",3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDL_ADAPTER.dsquery("select * from LOGIN_MAST where user_nm='" + objBASEFILEDS.ObjLoginUser.CurUser + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        if (txtOldPwd.Text == ds.Tables[0].Rows[0]["pwd"].ToString())
                        {
                            AutoClosingMessageBox.Show("Account Verified","Validation",3000);
                            txNpwd.ReadOnly = false;
                            txtNRpwd.ReadOnly = false;
                            e.Cancel = false;
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Wrong Password","Validation",3000);
                            txtOldPwd.Text = "";
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void txtOldPwd_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
