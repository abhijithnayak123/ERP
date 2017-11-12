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
    public partial class frmOpeningDuty : BaseClass
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
        public frmOpeningDuty(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmOpeningDuty_Load(object sender, EventArgs e)
        {
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;
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
            if (txtNo.Text != "")
            {
                objBASEFILEDS.HTMAIN["tran_id"] = objBASEFILEDS.Tran_id;
                objBASEFILEDS.HTMAIN["tran_no"] = txtNo.Text;
                objBASEFILEDS.HTMAIN["tran_dt"] = dtpTranDt.DtValue.ToString("yyyy/MM/dd"); 
                objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid;
                objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr;

                foreach (DataGridViewRow row in dgvOpenDuties.Rows)
                {
                    if (row.Cells["row_col"].Value.ToString() == "RG23A")
                    {
                        objBASEFILEDS.HTMAIN["ob_rg23a_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23a_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23a_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23a_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    }
                    if (row.Cells["row_col"].Value.ToString() == "RG23C")
                    {
                        objBASEFILEDS.HTMAIN["ob_rg23c_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23c_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23c_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_rg23c_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    }
                    if (row.Cells["row_col"].Value.ToString() == "ST")
                    {
                        objBASEFILEDS.HTMAIN["ob_st_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_st_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_st_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_st_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    }
                    if (row.Cells["row_col"].Value.ToString() == "PLA")
                    {
                        objBASEFILEDS.HTMAIN["ob_pla_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_pla_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_pla_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                        objBASEFILEDS.HTMAIN["ob_pla_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    }
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Transaction No should not be empty","Validation",3000);
                return false;
            }
            return true;
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
                            c.Enabled = true;
                        }
                        dtpTranDt.DtValue = objBASEFILEDS.ObjCompany.Fin_yr_sta;
                        txtNo.Text = "000001";
                        LoadOpeningDuties();
                        break;

                    case "edit_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (c is DataGridView)
                            {
                                ((DataGridView)c).Enabled = true;
                            }
                            else if (c is ToolBar)
                                c.Enabled = true;
                            else 
                                if (!(c is Label)) c.Enabled = false;
                        }
                        ViewDetails();
                        break;

                    case "view_mode":
                        foreach (Control c in this.Controls)
                        {
                            if (c is UCToolBar)
                                c.Enabled = true;
                            else
                            if (!(c is Label)) c.Enabled = false;
                        }
                        ViewDetails();
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadOpeningDuties()
        {
            for (int i = 0; i <= 3; i++) // (DataRow row in dsetDebit.Tables[0].Rows)
            {
                if (dgvOpenDuties.Rows.Count <=3)
                    dgvOpenDuties.Rows.Add();

                if (i == 0)
                {
                    dgvOpenDuties.Rows[i].Cells[0].Value = "RG23A";
                }
                else if (i == 1)
                {
                    dgvOpenDuties.Rows[i].Cells[0].Value = "RG23C";
                }
                else if (i == 2)
                {
                    dgvOpenDuties.Rows[i].Cells[0].Value = "ST";
                }
                else if (i == 3)
                {
                    dgvOpenDuties.Rows[i].Cells[0].Value = "PLA";
                }

                for (int k = 0; k <= 4; k++)
                {
                    if (dgvOpenDuties.Rows[i].Cells[k].Value != null && dgvOpenDuties.Rows[i].Cells[k].Value.ToString() != " ")
                    {
                        dgvOpenDuties.Rows[i].Cells["ob_excise"].Value = dgvOpenDuties.Rows[i].Cells[k].ToString();
                        dgvOpenDuties.Rows[i].Cells["ob_Cess"].Value = dgvOpenDuties.Rows[i].Cells[k].ToString();
                        dgvOpenDuties.Rows[i].Cells["ob_SHCess"].Value = dgvOpenDuties.Rows[i].Cells[k].ToString();
                        dgvOpenDuties.Rows[i].Cells["ob_Addl"].Value = dgvOpenDuties.Rows[i].Cells[k].ToString();
                    }
                    else
                    {
                        dgvOpenDuties.Rows[i].Cells["ob_excise"].Value = "0.00";
                        dgvOpenDuties.Rows[i].Cells["ob_Cess"].Value = "0.00";
                        dgvOpenDuties.Rows[i].Cells["ob_SHCess"].Value = "0.00";
                        dgvOpenDuties.Rows[i].Cells["ob_Addl"].Value = "0.00";
                    }

                }
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    foreach (DataGridViewRow row in dgvOpenDuties.Rows)
                    {
                        if (row.Cells["row_col"].Value.ToString() == "RG23A")
                        {
                            row.Cells["ob_excise"].Value = "0.00";
                            row.Cells["ob_Cess"].Value = "0.00";
                            row.Cells["ob_SHCess"].Value = "0.00";
                            row.Cells["ob_Addl"].Value = "0.00";
                        }
                        if (row.Cells["row_col"].Value.ToString() == "RG23C")
                        {
                            row.Cells["ob_excise"].Value = "0.00";
                            row.Cells["ob_Cess"].Value = "0.00";
                            row.Cells["ob_SHCess"].Value = "0.00";
                            row.Cells["ob_Addl"].Value = "0.00";
                        }
                        if (row.Cells["row_col"].Value.ToString() == "ST")
                        {
                            row.Cells["ob_excise"].Value = "0.00";
                            row.Cells["ob_Cess"].Value = "0.00";
                            row.Cells["ob_SHCess"].Value = "0.00";
                            row.Cells["ob_Addl"].Value = "0.00";
                        }
                        if (row.Cells["row_col"].Value.ToString() == "PLA")
                        {
                            row.Cells["ob_excise"].Value = "0.00";
                            row.Cells["ob_Cess"].Value = "0.00";
                            row.Cells["ob_SHCess"].Value = "0.00";
                            row.Cells["ob_Addl"].Value = "0.00";
                        }
                    }
                }
            }
        }

        private void LoadView()
        {
            DataSet ds = objdblayer.dsquery("select * from OBMAIN where tran_id='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (objBASEFILEDS.Tran_mode == "view_mode")
            {
                foreach (DataGridViewRow row in dgvOpenDuties.Rows)
                {
                    if (row.Cells["row_col"].Value.ToString() == "RG23A")
                    {
                        objBASEFILEDS.HTMAIN["ob_rg23a_ex_amt"] = ds.Tables[0].Rows[0]["ob_rg23a_ex_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23a_cess_amt"] = ds.Tables[0].Rows[0]["ob_rg23a_cess_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23a_hcess_amt"] = ds.Tables[0].Rows[0]["ob_rg23a_hcess_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23a_add_amt"] = ds.Tables[0].Rows[0]["ob_rg23a_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "RG23C")
                    {
                        objBASEFILEDS.HTMAIN["ob_rg23c_ex_amt"] = ds.Tables[0].Rows[0]["ob_rg23c_ex_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23c_cess_amt"] = ds.Tables[0].Rows[0]["ob_rg23c_cess_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23c_hcess_amt"] = ds.Tables[0].Rows[0]["ob_rg23c_hcess_amt"];
                        objBASEFILEDS.HTMAIN["ob_rg23c_add_amt"] = ds.Tables[0].Rows[0]["ob_rg23c_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "ST")
                    {
                        objBASEFILEDS.HTMAIN["ob_st_ex_amt"] = ds.Tables[0].Rows[0]["ob_st_ex_amt"];
                        objBASEFILEDS.HTMAIN["ob_st_cess_amt"] = ds.Tables[0].Rows[0]["ob_st_cess_amt"];
                        objBASEFILEDS.HTMAIN["ob_st_hcess_amt"] = ds.Tables[0].Rows[0]["ob_st_hcess_amt"];
                        objBASEFILEDS.HTMAIN["ob_st_add_amt"] = ds.Tables[0].Rows[0]["ob_st_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "PLA")
                    {
                        objBASEFILEDS.HTMAIN["ob_pla_ex_amt"] = ds.Tables[0].Rows[0]["ob_pla_ex_amt"];
                        objBASEFILEDS.HTMAIN["ob_pla_cess_amt"] = ds.Tables[0].Rows[0]["ob_pla_cess_amt"];
                        objBASEFILEDS.HTMAIN["ob_pla_hcess_amt"] = ds.Tables[0].Rows[0]["ob_pla_hcess_amt"];
                        objBASEFILEDS.HTMAIN["ob_pla_add_amt"] = ds.Tables[0].Rows[0]["ob_pla_add_amt"];
                    }
                }
            }
        }

        private void LoadEdit()
        {
            foreach (DataGridViewRow row in dgvOpenDuties.Rows)
            {
                if (row.Cells["row_col"].Value.ToString() == "RG23A")
                {
                    row.Cells["ob_excise"].Value = objBASEFILEDS.HTMAIN["ob_rg23a_ex_amt"];
                    row.Cells["ob_Cess"].Value = objBASEFILEDS.HTMAIN["ob_rg23a_cess_amt"];
                    row.Cells["ob_SHCess"].Value = objBASEFILEDS.HTMAIN["ob_rg23a_hcess_amt"];
                    row.Cells["ob_Addl"].Value = objBASEFILEDS.HTMAIN["ob_rg23a_add_amt"];
                }
                if (row.Cells["row_col"].Value.ToString() == "RG23C")
                {
                    row.Cells["ob_excise"].Value = objBASEFILEDS.HTMAIN["ob_rg23c_ex_amt"];
                    row.Cells["ob_Cess"].Value = objBASEFILEDS.HTMAIN["ob_rg23c_cess_amt"];
                    row.Cells["ob_SHCess"].Value = objBASEFILEDS.HTMAIN["ob_rg23c_hcess_amt"];
                    row.Cells["ob_Addl"].Value = objBASEFILEDS.HTMAIN["ob_rg23c_add_amt"];
                }
                if (row.Cells["row_col"].Value.ToString() == "ST")
                {
                    row.Cells["ob_excise"].Value = objBASEFILEDS.HTMAIN["ob_st_ex_amt"];
                    row.Cells["ob_Cess"].Value = objBASEFILEDS.HTMAIN["ob_st_cess_amt"];
                    row.Cells["ob_SHCess"].Value = objBASEFILEDS.HTMAIN["ob_st_hcess_amt"];
                    row.Cells["ob_Addl"].Value = objBASEFILEDS.HTMAIN["ob_st_add_amt"];
                }
                if (row.Cells["row_col"].Value.ToString() == "PLA")
                {
                    row.Cells["ob_excise"].Value = objBASEFILEDS.HTMAIN["ob_pla_ex_amt"];
                    row.Cells["ob_Cess"].Value = objBASEFILEDS.HTMAIN["ob_pla_cess_amt"];
                    row.Cells["ob_SHCess"].Value = objBASEFILEDS.HTMAIN["ob_pla_hcess_amt"];
                    row.Cells["ob_Addl"].Value = objBASEFILEDS.HTMAIN["ob_pla_add_amt"];
                }
            }
        }

        private void frmOpeningDuty_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void frmOpeningDuty_Enter(object sender, EventArgs e)
        {
            DataSet ds = objdblayer.dsquery("select * from OBMAIN");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                objBASEFILEDS.Tran_mode_type = "edit_only";
            }
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void ViewDetails()
        {
            DataSet ds = objdblayer.dsquery("select * from OBMAIN where tran_id='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtNo.Text = ds.Tables[0].Rows[0]["Tran_no"].ToString();
                if (ds.Tables[0].Rows[0]["tran_dt"] == null || ds.Tables[0].Rows[0]["tran_dt"].ToString() == "")
                    dtpTranDt.DtValue = new DateTime(1900, 01, 01);
                else
                    dtpTranDt.DtValue = DateTime.Parse(ds.Tables[0].Rows[0]["tran_dt"].ToString());

                LoadOpeningDuties();
                LoadView();
                LoadEdit();
            }
            objBASEFILEDS.HTMAIN["sr_id"] = objBASEFILEDS.Tran_id;
        }

        private void dtpTranDt_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (dtpTranDt.DtValue == null || dtpTranDt.DtValue.ToString() == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtNo_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtNo.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption","Validation",3000);
                    e.Cancel = true;
                }
            }
        }
    }
}
