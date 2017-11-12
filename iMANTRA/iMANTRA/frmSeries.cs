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
using CUSTOM_iMANTRA;
using iMANTRA_IL;
using System.Reflection;
using CUSTOM_iMANTRA_BL;
using System.Collections;

namespace iMANTRA
{
    public partial class frmSeries : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        dblayer objdblayer = new dblayer();

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

        public frmSeries(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmSeries_Load(object sender, EventArgs e)
        {
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
                        objBASEFILEDS.Tran_id = "0";
                        foreach (Control con in this.Controls)
                        {
                            if (con is Grouper)
                            {
                                foreach (Control c in con.Controls)
                                {
                                    if (c is TextBox)
                                    {
                                        ((TextBox)c).Text = "";
                                        ((TextBox)c).Enabled = true;
                                    }
                                    else
                                    {
                                        c.Enabled = true;
                                    }
                                }
                            }
                        }
                        btnSeries.Enabled = false;
                        btnValid.Enabled = true;
                        chkDeactivate.Checked = false;
                        dtpDate.Enabled = false;
                        dtpDate.DtValue = new DateTime(1900, 01, 01);
                        break;

                    case "edit_mode":
                        foreach (Control con in this.Controls)
                        {
                            if (con is Grouper)
                            {
                                foreach (Control c in con.Controls)
                                {
                                    c.Enabled = true;
                                }
                            }
                        }
                        txtSeries.Enabled = false;
                        btnSeries.Enabled = false;
                        txtValid.Enabled = false;
                        ViewSeriesDetails();
                        break;

                    case "view_mode":
                        foreach (Control con in this.Controls)
                        {
                            if (con is Grouper)
                            {
                                foreach (Control c in con.Controls)
                                {
                                    if (!(c is Label)) c.Enabled = false;
                                }
                            }
                        }
                        grbSeries.Enabled = true;
                        btnSeries.Enabled = true;
                        ViewSeriesDetails();
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
            if (txtSeries.Text != "")
            {
                DataSet ds = objdblayer.dsquery("select sr_id,tran_sr,compid from Series where sr_id=" + objBASEFILEDS.Tran_id + " and tran_sr ='" + txtSeries.Text + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("Series already exists", "Validation", 3000);
                    return false;
                }
                else
                {
                    objBASEFILEDS.HTMAIN["sr_id"] = objBASEFILEDS.Tran_id;
                    objBASEFILEDS.HTMAIN["tran_sr"] = txtSeries.Text;
                    objBASEFILEDS.HTMAIN["prefix"] = txtPrefix.Text;
                    objBASEFILEDS.HTMAIN["suffix"] = txtSuffix.Text;
                    objBASEFILEDS.HTMAIN["validity"] = txtValid.Text;
                    objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid;
                    objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr;
                    objBASEFILEDS.HTMAIN["isdeactive"] = chkDeactivate.Checked;
                    objBASEFILEDS.HTMAIN["deactfrm"] = dtpDate.DtValue.ToString("yyyy/MM/dd"); 
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Enter Series Name", "Validation", 3000);
                return false;
            }
            return true;
        }

        private void frmSeries_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmSeries_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void btnSeries_Click(object sender, EventArgs e)
        {
            frmPopup objfrmpopup = new frmPopup(objBASEFILEDS.HTMAIN, "Series", "SS", "sr_id,tran_sr", "sr_id;Series Id,tran_sr;Transaction Series", "Please select", "compid='" + objBASEFILEDS.ObjCompany.Compid + "'",false,"", "0");
            //objfrmpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objfrmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objfrmpopup.ObjBFD = objBASEFILEDS;
            objfrmpopup.ShowDialog();
            if (objBASEFILEDS.HTMAIN["tran_sr"].ToString() != "" && objBASEFILEDS.HTMAIN["tran_sr"] != null)
            {
                txtSeries.Text = objBASEFILEDS.HTMAIN["tran_sr"].ToString();
                objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["sr_id"].ToString();
            }
            ViewSeriesDetails();
        }

        private void ViewSeriesDetails()
        {
            DataSet ds = objdblayer.dsquery("select * from series where sr_id='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtSeries.Text = ds.Tables[0].Rows[0]["tran_sr"].ToString();
                txtPrefix.Text = ds.Tables[0].Rows[0]["prefix"].ToString();
                txtSuffix.Text = ds.Tables[0].Rows[0]["suffix"].ToString();
                txtValid.Text = ds.Tables[0].Rows[0]["Validity"].ToString();
                if (ds.Tables[0].Rows[0]["isdeactive"] == null || ds.Tables[0].Rows[0]["isdeactive"].ToString() == "")
                    chkDeactivate.Checked = false;
                else
                    chkDeactivate.Checked = bool.Parse(ds.Tables[0].Rows[0]["isdeactive"].ToString());
                if (ds.Tables[0].Rows[0]["deactfrm"] == null || ds.Tables[0].Rows[0]["deactfrm"].ToString() == "")
                    dtpDate.DtValue = new DateTime(1900, 01, 01);
                else
                    dtpDate.DtValue = DateTime.Parse(ds.Tables[0].Rows[0]["deactfrm"].ToString());
                if (chkDeactivate.Checked == false)
                {
                    dtpDate.Enabled = false;
                    dtpDate.DtValue = new DateTime(1900, 01, 01);
                }


                objBASEFILEDS.HTMAIN["tran_sr"] = txtSeries.Text;
                objBASEFILEDS.HTMAIN["prefix"] = txtPrefix.Text;
                objBASEFILEDS.HTMAIN["suffix"] = txtSuffix.Text;
                objBASEFILEDS.HTMAIN["validity"] = txtValid.Text;
                objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid;
                objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr;
                objBASEFILEDS.HTMAIN["isdeactive"] = chkDeactivate.Checked;
                objBASEFILEDS.HTMAIN["deactfrm"] = dtpDate.DtValue.ToString("yyyy/MM/dd"); 
            }
            objBASEFILEDS.HTMAIN["sr_id"] = objBASEFILEDS.Tran_id;
        }

        private void btnValid_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "validity";
            objBASEFILEDS.HTMAIN["code"] = "";
            if (objBASEFILEDS.HTMAIN["validity"] == null || objBASEFILEDS.HTMAIN["validity"].ToString() == "")
                objBASEFILEDS.HTMAIN["validity"] = "";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            objBASEFILEDS.HTMAIN.Remove("code");
            txtValid.Text = objBASEFILEDS.HTMAIN["validity"].ToString();
        }

        private void chkDeactivate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeactivate.Checked == false)
            {
                dtpDate.DtValue = new DateTime(1900, 01, 01);
                dtpDate.Enabled = false;
            }
            else
                dtpDate.Enabled = true;
            dtpDate.DtValue = DateTime.Now;
        }

        private void txtSeries_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtSeries.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objdblayer.dsquery("select sr_id,tran_sr,compid from Series where tran_sr ='" + txtSeries.Text.Replace("'", "''") + "' and sr_id !='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Series already exists", "Validation", 3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }
    }
}
