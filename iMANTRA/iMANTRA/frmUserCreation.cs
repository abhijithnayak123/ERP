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
using System.Text.RegularExpressions;

namespace iMANTRA
{
    public partial class frmUserCreation : BaseClass
    {
        private Ini objIni = new Ini();
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";//, pwd = "";
        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

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

        public frmUserCreation(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmUserCreation_Load(object sender, EventArgs e)
        {
            BindTheme();
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            DataSet ds = objDL_ADAPTER.dsquery("select status_nm from user_status where compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                cmbStat.DataSource = ds.Tables[0];
                cmbStat.DisplayMember = "status_nm";
                cmbStat.ValueMember = "status_nm";
                cmbStat.Update();
            }
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            //this.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Back_color);
            //this.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Font_color);
            //ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            //ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color);
            //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family, float.Parse(objBASEFILEDS.ObjControlSet.Font_size));
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
                        objBASEFILEDS.Tran_id = "0";
                        foreach (Control c in this.Controls)
                        {
                            if (c is ComboBox)
                            {
                                ((ComboBox)c).Enabled = true;
                                ((ComboBox)c).Text = "";
                                if (((ComboBox)c).SelectedIndex != -1)
                                {
                                    ((ComboBox)c).SelectedIndex = 0;
                                }
                            }
                            else if (c is TextBox)
                            {
                                ((TextBox)c).Text = "";
                                ((TextBox)c).Enabled = true;
                            }
                            else if (c is Button)
                            {
                                ((Button)c).Enabled = false;
                            }
                            else if (c is UserDT)
                            {
                                //((UserDT)c).DtValue = DateTime.Parse("01/01/1900");
                                ((UserDT)c).bUpdateFlag = false;
                                ((UserDT)c).DtValue = DateTime.Now;
                            }
                        }
                        txtUser.ReadOnly = false;
                        txtCode.Enabled = false;
                        dtpDeact.Enabled = false;
                        dtpDeact.DtValue = new DateTime(1900, 01, 01);
                        break;

                    case "edit_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (c is ComboBox)
                            {
                                ((ComboBox)c).Enabled = true;
                            }
                            else if (c is TextBox)
                            {
                                ((TextBox)c).Enabled = true;
                            }
                            else if (c is Button)
                            {
                                ((Button)c).Enabled = false;
                            }
                            if (c is UserDT)
                            {
                                if (((UserDT)c).DtValue.ToString() != "1900-01-01 12:00:00 AM" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900/00/01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000/00/01")
                                {
                                    ((UserDT)c).bUpdateFlag = true;
                                }
                                else
                                {
                                    ((UserDT)c).bUpdateFlag = false;
                                    ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                }
                                c.Enabled = true;
                            }
                        }
                        txtCode.Enabled = false;
                        txtPwd.Enabled = false;
                        txtRpwd.Enabled = false;
                        dtpDeact.Enabled = false;
                        ViewUserDetails();
                        break;

                    case "view_mode":
                        txtUser.Enabled = false;
                        dtpDeact.Enabled = false;
                        foreach (Control c in this.Controls)
                        {
                            if (c is ComboBox)
                            {
                                ((ComboBox)c).Enabled = false;

                            }
                            else if (c is TextBox)
                            {
                                ((TextBox)c).Enabled = false;
                            }
                            else if (c is Button)
                            {
                                ((Button)c).Enabled = true;
                            }
                            if (c is UserDT)
                            {
                                ((UserDT)c).bUpdateFlag = true;
                                if (!(c is Label)) c.Enabled = false;
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
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            if (cmbTheme.SelectedValue != null && cmbTheme.SelectedValue.ToString() == "")
            {
                AutoClosingMessageBox.Show("Please Enter Theme Name", "Validation", 3000); return false;
            }
            if (objBASEFILEDS.Tran_mode != "view_mode" && txtEid.Text != "")
            {
                Regex emailPattern = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                if (!emailPattern.IsMatch(txtEid.Text))
                {
                    AutoClosingMessageBox.Show("Please provide valid E-mail ID", "Validation", 3000);
                    return false;
                }
            }
            if (objBASEFILEDS.Tran_mode != "view_mode" && txtUser.Text != "")
            {
                Regex mobilePattern = new Regex(@"[a-zA-Z]+$");
                if (!mobilePattern.IsMatch(txtUser.Text))
                {
                    AutoClosingMessageBox.Show("Please enter Alphabets", "Validation", 3000);
                    return false;
                }
            }
            if (objBASEFILEDS.Tran_mode != "view_mode" && txtConct.Text != "")
            {
                Regex contactpattern = new Regex(@"^(?:[0-9]+(?:-[0-9])?)*$");

                if (!contactpattern.IsMatch(txtConct.Text))
                {
                    AutoClosingMessageBox.Show("Please enter valid number", "Validation", 3000);
                    return false;
                }
            }
            if (objBASEFILEDS.Tran_mode != "view_mode" && txtMob.Text != "")
            {
                Regex mobilePattern = new Regex(@"[-+]?[0-9]?[0-9]?\d{10}$");
                if (!mobilePattern.IsMatch(txtMob.Text))
                {
                    AutoClosingMessageBox.Show("Please enter a valid Mobile number", "Validation", 3000);
                    return false;
                }
            }
            if (txtUser.Text != "")
            {
                if (txtPwd.Text != "" && txtRpwd.Text != "")
                {
                    if (txtPwd.Text == txtRpwd.Text)
                    {
                        objBASEFILEDS.HTMAIN["userid"] = objBASEFILEDS.Tran_id;
                        objBASEFILEDS.HTMAIN["user_nm"] = txtUser.Text;
                        objBASEFILEDS.HTMAIN["pwd"] = txtPwd.Text;
                        objBASEFILEDS.HTMAIN["r_pwd"] = txtRpwd.Text;
                        objBASEFILEDS.HTMAIN["status_nm"] = cmbStat.Text;
                        objBASEFILEDS.HTMAIN["email"] = txtEid.Text;
                        objBASEFILEDS.HTMAIN["designation"] = txtDesgn.Text;
                        objBASEFILEDS.HTMAIN["mobile"] = txtMob.Text;
                        objBASEFILEDS.HTMAIN["phone"] = txtConct.Text;
                        objBASEFILEDS.HTMAIN["theme_nm"] = cmbTheme.Text;
                        objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid;
                        objBASEFILEDS.HTMAIN["deactfrm"] = dtpDeact.DtValue.ToString("yyyy/MM/dd");
                        if (objBASEFILEDS.Tran_mode == "add_mode")
                        {
                            objBASEFILEDS.HTMAIN["sec_code"] = VALIDATIONLAYER.Encrypt(txtUser.Text);
                        }
                        else
                        {
                            objBASEFILEDS.HTMAIN["sec_code"] = txtCode.Text;
                        }
                        DataSet ds = objDL_ADAPTER.dsquery("select comp_nm, fin_yr from org_mast where compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            objBASEFILEDS.HTMAIN["comp_nm"] = ds.Tables[0].Rows[0]["comp_nm"].ToString();
                            objBASEFILEDS.HTMAIN["fin_yr"] = ds.Tables[0].Rows[0]["fin_yr"].ToString();
                        }
                        return true;
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("passwords don't match", "Validation", 3000);
                        txtRpwd.Validating -= new CancelEventHandler(txtRpwd_Validating);
                        txtRpwd.Clear();
                        return false;
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Enter Password", "Validation", 3000);
                    return false;
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Enter User Name", "Validation", 3000);
                return false;
            }
        }

        private void frmUserCreation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmUserCreation_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void ViewUserDetails()
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            if (objBASEFILEDS.Tran_mode == "edit_mode")
            {
                DataSet ds2 = objDL_ADAPTER.dsquery("Select intRoleId from Roles_Mapping where userid='" + objBASEFILEDS.Tran_id + "'");
                DataSet ds1 = objDL_ADAPTER.dsquery("select userid from LOGIN_MAST where userid in(select distinct userid from ROLES_MAPPING inner join ROLES on roles.intRoleId=ROLES_MAPPING.intRoleId and ROLES.role_nm='admin')");
                if ((ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0) || (txtUser.Text == objBASEFILEDS.ObjLoginUser.CurUser))
                {
                    txtPwd.Enabled = true;
                    txtRpwd.Enabled = true;
                    if ((ds2 != null && ds2.Tables.Count != 0 && ds2.Tables[0].Rows.Count != 0))
                    {
                        txtUser.Enabled = false;
                    }
                    else
                    {
                        txtUser.Enabled = true;
                    }
                }
                else
                {
                    txtPwd.Enabled = false;
                    txtRpwd.Enabled = false;
                }
            }
            DataSet ds = objDL_ADAPTER.dsquery("select * from Login_mast where userid='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtUser.Text = ds.Tables[0].Rows[0]["user_nm"].ToString();
                txtCode.Text = ds.Tables[0].Rows[0]["sec_code"].ToString();
                txtPwd.Text = ds.Tables[0].Rows[0]["pwd"].ToString();
                txtRpwd.Text = ds.Tables[0].Rows[0]["r_pwd"].ToString();
                txtDesgn.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                txtEid.Text = ds.Tables[0].Rows[0]["email"].ToString();
                txtConct.Text = ds.Tables[0].Rows[0]["phone"].ToString();
                txtMob.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
                cmbStat.Text = ds.Tables[0].Rows[0]["status_nm"].ToString();
                if (ds.Tables[0].Rows[0]["deactfrm"] != null && ds.Tables[0].Rows[0]["deactfrm"].ToString() != "")
                {
                    dtpDeact.DtValue = DateTime.Parse(ds.Tables[0].Rows[0]["deactfrm"].ToString());
                }
                else
                {
                    dtpDeact.DtValue = new DateTime(1900, 01, 01);
                }

                cmbTheme.Text = ds.Tables[0].Rows[0]["theme_nm"].ToString();

                objBASEFILEDS.HTMAIN["user_nm"] = ds.Tables[0].Rows[0]["user_nm"].ToString();
                objBASEFILEDS.HTMAIN["sec_code"] = ds.Tables[0].Rows[0]["sec_code"].ToString();
                objBASEFILEDS.HTMAIN["pwd"] = ds.Tables[0].Rows[0]["pwd"].ToString();
                objBASEFILEDS.HTMAIN["r_pwd"] = ds.Tables[0].Rows[0]["r_pwd"].ToString();
                objBASEFILEDS.HTMAIN["designation"] = ds.Tables[0].Rows[0]["designation"].ToString();
                objBASEFILEDS.HTMAIN["email"] = ds.Tables[0].Rows[0]["email"].ToString();
                objBASEFILEDS.HTMAIN["phone"] = ds.Tables[0].Rows[0]["phone"].ToString();
                objBASEFILEDS.HTMAIN["mobile"] = ds.Tables[0].Rows[0]["mobile"].ToString();
                objBASEFILEDS.HTMAIN["status_nm"] = ds.Tables[0].Rows[0]["status_nm"].ToString();
                objBASEFILEDS.HTMAIN["theme_nm"] = ds.Tables[0].Rows[0]["theme_nm"].ToString();
                objBASEFILEDS.HTMAIN["deactfrm"] = dtpDeact.DtValue.ToString("yyyy/MM/dd"); 
            }
            objBASEFILEDS.HTMAIN["userid"] = objBASEFILEDS.Tran_id;

        }

        private void cmbStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            if (cmbStat.Text.ToString() != "System.Data.DataRowView")
            {
                if (cmbStat.Text == "ACTIVE")
                {
                    dtpDeact.Enabled = false;
                }
                else
                {
                    dtpDeact.DtValue = DateTime.Today;
                    dtpDeact.Enabled = true;
                }
            }
        }

        private void txtUser_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtUser.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    Regex mobilePattern = new Regex(@"[a-zA-Z]+$");
                    if (!mobilePattern.IsMatch(txtUser.Text))
                    {
                        MessageBox.Show("Please enter Alphabets");
                        e.Cancel = true;
                    }
                }



                DataSet ds = objDL_ADAPTER.dsquery("select userid,user_nm from login_mast where user_nm ='" + txtUser.Text.Replace("'", "''") + "' and userid !='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("User Name already exists", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private void txtPwd_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
            {
                if (txtPwd.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtRpwd_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtPwd.Text != "" && txtPwd.Text != null)
                {
                    if (txtPwd.Text == txtRpwd.Text)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Password doesn't match", "Validation", 3000);
                        txtRpwd.Validating -= new CancelEventHandler(txtRpwd_Validating);
                        txtRpwd.Text = "";
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnUser_Click_1(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            frmPopup objfrmpopup = new frmPopup(objBASEFILEDS.HTMAIN, "Login_Mast", "UL", "userid,user_nm", "userid;User Id,user_nm;User Name", "Please select", "compid='" + objBASEFILEDS.ObjCompany.Compid + "'", false, "", "0");
            //objfrmpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objfrmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objfrmpopup.ObjBFD = objBASEFILEDS;
            objfrmpopup.ShowDialog();
            if (objBASEFILEDS.HTMAIN["user_nm"].ToString() != "" && objBASEFILEDS.HTMAIN["user_nm"] != null)
            {
                txtUser.Text = objBASEFILEDS.HTMAIN["user_nm"].ToString();
                objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["userid"].ToString();
            }
            ViewUserDetails();
        }

        private void BindTheme()
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", objBASEFILEDS.ObjCompany.Db_nm);
            DataSet dsest = new DataSet();
            dsest = objDL_ADAPTER.dsquery("select distinct theme_nm from theme_set where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            if (dsest != null && dsest.Tables.Count != 0 && dsest.Tables[0].Rows.Count != 0)
            {
                cmbTheme.DataSource = dsest.Tables[0];
                cmbTheme.DisplayMember = "theme_nm";
                cmbTheme.ValueMember = "theme_nm";
                cmbTheme.Update();
            }
        }
        private void frmUserCreation_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void txtEid_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode" && txtEid.Text != "")
            {
                Regex emailPattern = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                if (!emailPattern.IsMatch(txtEid.Text))
                {
                    MessageBox.Show("Please provide valid E-mail ID");
                    //txtPEmail.Text = "";
                    e.Cancel = true;
                }
            }
        }

        private void txtConct_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.Tran_mode != "view_mode" && txtConct.Text != "")
                {
                    Regex contactpattern = new Regex(@"^(?:[0-9]+(?:-[0-9])?)*$");

                    if (!contactpattern.IsMatch(txtConct.Text))
                    {
                        MessageBox.Show("Please enter valid number");
                        //MessageBox.Show(+mobilePattern);
                        //txtMobile.Text = "";
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception h)
            {
                MessageBox.Show("Please provide number only");
                e.Cancel = true;
            }
        }

        private void txtMob_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.Tran_mode != "view_mode" && txtMob.Text != "")
                {
                    Regex mobilePattern = new Regex(@"[-+]?[0-9]?[0-9]?\d{10}$");
                    if (!mobilePattern.IsMatch(txtMob.Text))
                    {
                        MessageBox.Show("Please enter a valid number");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Please provide number only");
                //txtMobile.Text = "";
                //e.Cancel = true;
            }

        }
    }
}
