using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmDebitEntry : CustomBaseForm
    {
        private string _tran_cd, _tran_id, _tran_mode;
        decimal decexcise = 0, deccess = 0, decshcess = 0, decaddl = 0;

        private Hashtable _hashstkvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private dblayer objdblayer = new dblayer();
        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public Hashtable Hashstkvalue
        {
            get { return _hashstkvalue; }
            set { _hashstkvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public string Tran_mode
        {
            get { return _tran_mode; }
            set { _tran_mode = value; }
        }
        //private int ;

        public string Tran_id
        {
            get { return _tran_id; }
            set { _tran_id = value; }
        }

        public string Tran_cd
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }

        public frmDebitEntry()
        {
            InitializeComponent();
        }

        private void frmDebitEntry_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            if (ACTIVE_BL.Code == "DE")
            {
                LoadDebitEntry();
                ShowAdjustingAmount();
                if (ACTIVE_BL.Tran_mode == "view_mode") { dgvcreditdetails.Enabled = false; }
                AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
                ucToolBar1.Titlebar = "Debit Entry";
            }
        }

        private void LoadDebitEntry()
        {
            Hashtable htparam = new Hashtable();
            htparam.Add("@asdate", DateTime.Parse(objBLFD.HTMAIN["duty_frm"].ToString()));
            htparam.Add("@aedate", DateTime.Parse(objBLFD.HTMAIN["duty_to"].ToString()));
            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());
            DataSet dsetDebit = objdblayer.dsprocedure("ISP_GET_DEBIT_ENTRY_DETAILS", htparam);
            dgvcreditdetails.Rows.Clear();
            if (dsetDebit != null && dsetDebit.Tables != null && dsetDebit.Tables[0].Rows.Count != 0)
            {
                int j = 0;
                for (int i = 1; i <= 5; i++) // (DataRow row in dsetDebit.Tables[0].Rows)
                {
                    if (i == 1)
                    {
                        if (dsetDebit.Tables[0].Rows[j]["grp"].ToString() == i.ToString())
                        {
                            txtsalesexduty.Text = dsetDebit.Tables[0].Rows[j]["ex_duty"].ToString();
                            txtsalescessduty.Text = dsetDebit.Tables[0].Rows[j]["cess_duty"].ToString();
                            txtsaleshcessduty.Text = dsetDebit.Tables[0].Rows[j]["shcess_duty"].ToString();
                            txtsalesaddlduty.Text = dsetDebit.Tables[0].Rows[j]["addl_duty"].ToString();
                        }
                    }
                    else
                    {
                        if (i > 1)
                        {
                            dgvcreditdetails.Rows.Add();
                            if (j == 0)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "RG23A";
                            }
                            else if (j == 1)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "RG23C";
                            }
                            else if (j == 2)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "ST";
                            }
                            else if (j == 3)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "PLA";
                            }
                            DataRow[] row = dsetDebit.Tables[0].Select("grp='" + (i) + "'");

                            if (row != null && row.Length != 0)
                            {
                                dgvcreditdetails.Rows[j].Cells["credit_excise"].Value = row[0]["ex_duty"].ToString();
                                // dgvcreditdetails.Rows[j].Cells["credit_cvd"].Value = row[0]["cvd"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Credit_Cess"].Value = row[0]["cess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Credit_SHCess"].Value = row[0]["shcess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Credit_Addl"].Value = row[0]["addl_duty"].ToString();

                                dgvcreditdetails.Rows[j].Cells["Adjust_Excise"].Value = row[0]["adj_ex_duty"].ToString();
                                // dgvcreditdetails.Rows[j].Cells["Adjust_Cvd"].Value = row[0]["adj_cvd_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Adjust_Cess"].Value = row[0]["adj_cess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Adjust_SHCess"].Value = row[0]["adj_hcess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["Adjust_Addl"].Value = row[0]["adj_addl_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells["ttl_amt"].Value = row[0]["ttl_amt"].ToString();
                            }
                            else
                            {
                                dgvcreditdetails.Rows[j].Cells["credit_excise"].Value = "0.00";
                                //  dgvcreditdetails.Rows[j].Cells["credit_cvd"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Credit_Cess"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Credit_SHCess"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Credit_Addl"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Adjust_Excise"].Value = "0.00";
                                // dgvcreditdetails.Rows[j].Cells["Adjust_Cvd"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Adjust_Cess"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Adjust_SHCess"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["Adjust_Addl"].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells["ttl_amt"].Value = "0.00";
                            }
                            j++;
                        }
                    }
                }
            }
            //if (ACTIVE_BL.Tran_mode == "add_mode" && objBLFD.HTMAIN["new_entry"] != null && objBLFD.HTMAIN["new_entry"].ToString() != "" && !bool.Parse(objBLFD.HTMAIN["new_entry"].ToString()))
            //{
            //txtsalesexduty.Text = objBLFD.HTMAIN["sal_excise_amt"].ToString();
            //txtsalescessduty.Text = objBLFD.HTMAIN["sal_cess_amt"].ToString();
            //txtsaleshcessduty.Text = objBLFD.HTMAIN["sal_hcess_amt"].ToString();
            //txtsalesaddlduty.Text = objBLFD.HTMAIN["sal_addl_amt"].ToString();

            foreach (DataGridViewRow row in dgvcreditdetails.Rows)
            {
                if (row.Cells["details"].Value.ToString() == "RG23A")
                {
                    //row.Cells["credit_excise"].Value = objBLFD.HTMAIN["crdt_rg23a_ex_amt"];
                    //row.Cells["Credit_Cess"].Value = objBLFD.HTMAIN["crdt_rg23a_cess_amt"];
                    //row.Cells["Credit_SHCess"].Value = objBLFD.HTMAIN["crdt_rg23a_hcess_amt"];
                    //row.Cells["Credit_Addl"].Value = objBLFD.HTMAIN["crdt_rg23a_add_amt"];
                    row.Cells["Adjust_Excise"].Value = objBLFD.HTMAIN["adj_rg23a_ex_amt"];
                    // row.Cells["Adjust_Cvd"].Value = objBLFD.HTMAIN["adj_rg23a_cvd_amt"];
                    row.Cells["Adjust_Cess"].Value = objBLFD.HTMAIN["adj_rg23a_cess_amt"];
                    row.Cells["Adjust_SHCess"].Value = objBLFD.HTMAIN["adj_rg23a_hcess_amt"];
                    row.Cells["Adjust_Addl"].Value = objBLFD.HTMAIN["adj_rg23a_add_amt"];
                    row.Cells["ttl_amt"].Value = objBLFD.HTMAIN["tot_ex_amt"];
                }
                if (row.Cells["details"].Value.ToString() == "RG23C")
                {
                    //row.Cells["credit_excise"].Value = objBLFD.HTMAIN["crdt_rg23c_ex_amt"];
                    //row.Cells["Credit_Cess"].Value = objBLFD.HTMAIN["crdt_rg23c_cess_amt"];
                    //row.Cells["Credit_SHCess"].Value = objBLFD.HTMAIN["crdt_rg23c_hcess_amt"];
                    //row.Cells["Credit_Addl"].Value = objBLFD.HTMAIN["crdt_rg23c_add_amt"];
                    row.Cells["Adjust_Excise"].Value = objBLFD.HTMAIN["adj_rg23c_ex_amt"];
                    // row.Cells["Adjust_Cvd"].Value = objBLFD.HTMAIN["adj_rg23c_cvd_amt"];
                    row.Cells["Adjust_Cess"].Value = objBLFD.HTMAIN["adj_rg23c_cess_amt"];
                    row.Cells["Adjust_SHCess"].Value = objBLFD.HTMAIN["adj_rg23c_hcess_amt"];
                    row.Cells["Adjust_Addl"].Value = objBLFD.HTMAIN["adj_rg23c_add_amt"];
                    row.Cells["ttl_amt"].Value = objBLFD.HTMAIN["tot_cess_amt"];
                }
                if (row.Cells["details"].Value.ToString() == "ST")
                {
                    //row.Cells["credit_excise"].Value = objBLFD.HTMAIN["crdt_st_ex_amt"];
                    //row.Cells["Credit_Cess"].Value = objBLFD.HTMAIN["crdt_st_cess_amt"];
                    //row.Cells["Credit_SHCess"].Value = objBLFD.HTMAIN["crdt_st_hcess_amt"];
                    //row.Cells["Credit_Addl"].Value = objBLFD.HTMAIN["crdt_st_add_amt"];
                    row.Cells["Adjust_Excise"].Value = objBLFD.HTMAIN["adj_st_ex_amt"];
                    // row.Cells["Adjust_Cvd"].Value = objBLFD.HTMAIN["adj_st_cvd_amt"];
                    row.Cells["Adjust_Cess"].Value = objBLFD.HTMAIN["adj_st_cess_amt"];
                    row.Cells["Adjust_SHCess"].Value = objBLFD.HTMAIN["adj_st_hcess_amt"];
                    row.Cells["Adjust_Addl"].Value = objBLFD.HTMAIN["adj_st_add_amt"];
                    row.Cells["ttl_amt"].Value = objBLFD.HTMAIN["tot_hcess_amt"];
                }
                if (row.Cells["details"].Value.ToString() == "PLA")
                {
                    //row.Cells["credit_excise"].Value = objBLFD.HTMAIN["crdt_pla_ex_amt"];
                    //row.Cells["Credit_Cess"].Value = objBLFD.HTMAIN["crdt_pla_cess_amt"];
                    //row.Cells["Credit_SHCess"].Value = objBLFD.HTMAIN["crdt_pla_hcess_amt"];
                    //row.Cells["Credit_Addl"].Value = objBLFD.HTMAIN["crdt_pla_add_amt"];
                    row.Cells["Adjust_Excise"].Value = objBLFD.HTMAIN["adj_pla_ex_amt"];
                    //  row.Cells["Adjust_Cvd"].Value = objBLFD.HTMAIN["adj_pla_cvd_amt"];
                    row.Cells["Adjust_Cess"].Value = objBLFD.HTMAIN["adj_pla_cess_amt"];
                    row.Cells["Adjust_SHCess"].Value = objBLFD.HTMAIN["adj_pla_hcess_amt"];
                    row.Cells["Adjust_Addl"].Value = objBLFD.HTMAIN["adj_pla_add_amt"];
                    row.Cells["ttl_amt"].Value = objBLFD.HTMAIN["tot_add_amt"];
                }
            }
            //}
        }

        private void dgvcreditdetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // decimal decexcise = 0, deccvd = 0, deccess = 0, decshcess = 0, decaddl = 0;
            decexcise = 0; deccess = 0; decshcess = 0; decaddl = 0;
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dgvcreditdetails.CurrentCell.OwningColumn.Name == "Adjust_Excise")
                {
                    decexcise = e.FormattedValue != null && e.FormattedValue.ToString() != "" ? decimal.Parse(e.FormattedValue.ToString()) : decimal.Parse("0.00");
                }
                else
                {
                    decexcise = dgvcreditdetails.CurrentRow.Cells["Adjust_Excise"].Value != null && dgvcreditdetails.CurrentRow.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                }
                //if (dgvcreditdetails.CurrentCell.OwningColumn.Name == "Adjust_Cvd")
                //{
                //    deccvd = e.FormattedValue != null && e.FormattedValue.ToString() != "" ? decimal.Parse(e.FormattedValue.ToString()) : decimal.Parse("0.00");
                //}
                //else
                //{
                //    deccvd = dgvcreditdetails.CurrentRow.Cells["Adjust_Cvd"].Value != null && dgvcreditdetails.CurrentRow.Cells["Adjust_Cvd"].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Adjust_Cvd"].Value.ToString()) : decimal.Parse("0.00");
                //}
                if (dgvcreditdetails.CurrentCell.OwningColumn.Name == "Adjust_Cess")
                {
                    deccess = e.FormattedValue != null && e.FormattedValue.ToString() != "" ? decimal.Parse(e.FormattedValue.ToString()) : decimal.Parse("0.00");
                }
                else
                {
                    deccess = dgvcreditdetails.CurrentRow.Cells["Adjust_Cess"].Value != null && dgvcreditdetails.CurrentRow.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (dgvcreditdetails.CurrentCell.OwningColumn.Name == "Adjust_SHCess")
                {
                    decshcess = e.FormattedValue != null && e.FormattedValue.ToString() != "" ? decimal.Parse(e.FormattedValue.ToString()) : decimal.Parse("0.00");
                }
                else
                {
                    decshcess = dgvcreditdetails.CurrentRow.Cells["Adjust_SHCess"].Value != null && dgvcreditdetails.CurrentRow.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (dgvcreditdetails.CurrentCell.OwningColumn.Name == "Adjust_Addl")
                {
                    decaddl = e.FormattedValue != null && e.FormattedValue.ToString() != "" ? decimal.Parse(e.FormattedValue.ToString()) : decimal.Parse("0.00");
                }
                else
                {
                    decaddl = dgvcreditdetails.CurrentRow.Cells["Adjust_Addl"].Value != null && dgvcreditdetails.CurrentRow.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells["credit_excise"].Value.ToString()) < decexcise)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " excise duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " excise duty", "Validation");
                    e.Cancel = true;
                }
                //if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells["credit_cvd"].Value.ToString()) < deccvd)
                //{
                //    MessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cvd duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cvd duty");
                //    e.Cancel = true;
                //}
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Credit_Cess"].Value.ToString()) < deccess)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cess duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cess duty", "Validation");
                    e.Cancel = true;
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Credit_SHCess"].Value.ToString()) < decshcess)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " S&H Cess duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " S&H Cess duty", "Validation");
                    e.Cancel = true;
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells["Credit_Addl"].Value.ToString()) < decaddl)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Addl. duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Addl. duty", "Validation");
                    e.Cancel = true;
                }
                if (!e.Cancel)
                {
                    dgvcreditdetails.CurrentRow.Cells["ttl_amt"].Value = decexcise + deccess + decshcess + decaddl;
                }
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            bool flgContinue = true;
            string strMessage = "";

            objBLFD.HTMAIN["sal_excise_amt"] = txtsalesexduty.Text;
            objBLFD.HTMAIN["sal_cess_amt"] = txtsalescessduty.Text;
            objBLFD.HTMAIN["sal_hcess_amt"] = txtsaleshcessduty.Text;
            objBLFD.HTMAIN["sal_addl_amt"] = txtsalesaddlduty.Text;
            decexcise = 0; deccess = 0; decshcess = 0; decaddl = 0;
            foreach (DataGridViewRow row in dgvcreditdetails.Rows)
            {
                if (row.Cells["details"].Value.ToString() == "RG23A")
                {
                    objBLFD.HTMAIN["crdt_rg23a_ex_amt"] = row.Cells["credit_excise"].Value;
                    //   objBLFD.HTMAIN["crdt_rg23a_cvd_amt"] = row.Cells["credit_cvd"].Value;
                    objBLFD.HTMAIN["crdt_rg23a_cess_amt"] = row.Cells["Credit_Cess"].Value;
                    objBLFD.HTMAIN["crdt_rg23a_hcess_amt"] = row.Cells["Credit_SHCess"].Value;
                    objBLFD.HTMAIN["crdt_rg23a_add_amt"] = row.Cells["Credit_Addl"].Value;
                    objBLFD.HTMAIN["adj_rg23a_ex_amt"] = row.Cells["Adjust_Excise"].Value != null && row.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                    //   objBLFD.HTMAIN["adj_rg23a_cvd_amt"] = row.Cells["Adjust_Cvd"].Value != null && row.Cells["Adjust_Cvd"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cvd"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23a_cess_amt"] = row.Cells["Adjust_Cess"].Value != null && row.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23a_hcess_amt"] = row.Cells["Adjust_SHCess"].Value != null && row.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23a_add_amt"] = row.Cells["Adjust_Addl"].Value != null && row.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["tot_ex_amt"] = row.Cells["ttl_amt"].Value != null && row.Cells["ttl_amt"].Value.ToString() != "" ? decimal.Parse(row.Cells["ttl_amt"].Value.ToString()) : decimal.Parse("0.00");

                    decexcise += decimal.Parse(objBLFD.HTMAIN["adj_rg23a_ex_amt"].ToString());
                    deccess += decimal.Parse(objBLFD.HTMAIN["adj_rg23a_cess_amt"].ToString());
                    decshcess += decimal.Parse(objBLFD.HTMAIN["adj_rg23a_hcess_amt"].ToString());
                    decaddl += decimal.Parse(objBLFD.HTMAIN["adj_rg23a_add_amt"].ToString());
                }
                if (row.Cells["details"].Value.ToString() == "RG23C")
                {
                    objBLFD.HTMAIN["crdt_rg23c_ex_amt"] = row.Cells["credit_excise"].Value;
                    //    objBLFD.HTMAIN["crdt_rg23c_cvd_amt"] = row.Cells["credit_cvd"].Value;
                    objBLFD.HTMAIN["crdt_rg23c_cess_amt"] = row.Cells["Credit_Cess"].Value;
                    objBLFD.HTMAIN["crdt_rg23c_hcess_amt"] = row.Cells["Credit_SHCess"].Value;
                    objBLFD.HTMAIN["crdt_rg23c_add_amt"] = row.Cells["Credit_Addl"].Value;
                    objBLFD.HTMAIN["adj_rg23c_ex_amt"] = row.Cells["Adjust_Excise"].Value != null && row.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                    //   objBLFD.HTMAIN["adj_rg23c_cvd_amt"] = row.Cells["Adjust_Cvd"].Value != null && row.Cells["Adjust_Cvd"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cvd"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23c_cess_amt"] = row.Cells["Adjust_Cess"].Value != null && row.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23c_hcess_amt"] = row.Cells["Adjust_SHCess"].Value != null && row.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_rg23c_add_amt"] = row.Cells["Adjust_Addl"].Value != null && row.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["tot_cess_amt"] = row.Cells["ttl_amt"].Value != null && row.Cells["ttl_amt"].Value.ToString() != "" ? decimal.Parse(row.Cells["ttl_amt"].Value.ToString()) : decimal.Parse("0.00");

                    decexcise += decimal.Parse(objBLFD.HTMAIN["adj_rg23c_ex_amt"].ToString());
                    deccess += decimal.Parse(objBLFD.HTMAIN["adj_rg23c_cess_amt"].ToString());
                    decshcess += decimal.Parse(objBLFD.HTMAIN["adj_rg23c_hcess_amt"].ToString());
                    decaddl += decimal.Parse(objBLFD.HTMAIN["adj_rg23c_add_amt"].ToString());
                }
                if (row.Cells["details"].Value.ToString() == "ST")
                {
                    objBLFD.HTMAIN["crdt_st_ex_amt"] = row.Cells["credit_excise"].Value;
                    //  objBLFD.HTMAIN["crdt_st_cvd_amt"] = row.Cells["credit_cvd"].Value;
                    objBLFD.HTMAIN["crdt_st_cess_amt"] = row.Cells["Credit_Cess"].Value;
                    objBLFD.HTMAIN["crdt_st_hcess_amt"] = row.Cells["Credit_SHCess"].Value;
                    objBLFD.HTMAIN["crdt_st_add_amt"] = row.Cells["Credit_Addl"].Value;
                    objBLFD.HTMAIN["adj_st_ex_amt"] = row.Cells["Adjust_Excise"].Value != null && row.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                    //   objBLFD.HTMAIN["adj_st_cvd_amt"] = row.Cells["Adjust_Cvd"].Value != null && row.Cells["Adjust_Cvd"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cvd"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_st_cess_amt"] = row.Cells["Adjust_Cess"].Value != null && row.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_st_hcess_amt"] = row.Cells["Adjust_SHCess"].Value != null && row.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_st_add_amt"] = row.Cells["Adjust_Addl"].Value != null && row.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["tot_hcess_amt"] = row.Cells["ttl_amt"].Value != null && row.Cells["ttl_amt"].Value.ToString() != "" ? decimal.Parse(row.Cells["ttl_amt"].Value.ToString()) : decimal.Parse("0.00");

                    decexcise += decimal.Parse(objBLFD.HTMAIN["adj_st_ex_amt"].ToString());
                    deccess += decimal.Parse(objBLFD.HTMAIN["adj_st_cess_amt"].ToString());
                    decshcess += decimal.Parse(objBLFD.HTMAIN["adj_st_hcess_amt"].ToString());
                    decaddl += decimal.Parse(objBLFD.HTMAIN["adj_st_add_amt"].ToString());
                }
                if (row.Cells["details"].Value.ToString() == "PLA")
                {
                    objBLFD.HTMAIN["crdt_pla_ex_amt"] = row.Cells["credit_excise"].Value;
                    // objBLFD.HTMAIN["crdt_pla_cvd_amt"] = row.Cells["credit_cvd"].Value;
                    objBLFD.HTMAIN["crdt_pla_cess_amt"] = row.Cells["Credit_Cess"].Value;
                    objBLFD.HTMAIN["crdt_pla_hcess_amt"] = row.Cells["Credit_SHCess"].Value;
                    objBLFD.HTMAIN["crdt_pla_add_amt"] = row.Cells["Credit_Addl"].Value;
                    objBLFD.HTMAIN["adj_pla_ex_amt"] = row.Cells["Adjust_Excise"].Value != null && row.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                    //  objBLFD.HTMAIN["adj_pla_cvd_amt"] = row.Cells["Adjust_Cvd"].Value != null && row.Cells["Adjust_Cvd"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cvd"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_pla_cess_amt"] = row.Cells["Adjust_Cess"].Value != null && row.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_pla_hcess_amt"] = row.Cells["Adjust_SHCess"].Value != null && row.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["adj_pla_add_amt"] = row.Cells["Adjust_Addl"].Value != null && row.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["tot_add_amt"] = row.Cells["ttl_amt"].Value != null && row.Cells["ttl_amt"].Value.ToString() != "" ? decimal.Parse(row.Cells["ttl_amt"].Value.ToString()) : decimal.Parse("0.00");

                    decexcise += decimal.Parse(objBLFD.HTMAIN["adj_pla_ex_amt"].ToString());
                    deccess += decimal.Parse(objBLFD.HTMAIN["adj_pla_cess_amt"].ToString());
                    decshcess += decimal.Parse(objBLFD.HTMAIN["adj_pla_hcess_amt"].ToString());
                    decaddl += decimal.Parse(objBLFD.HTMAIN["adj_pla_add_amt"].ToString());
                }
            }

            if (decexcise != decimal.Parse(txtsalesexduty.Text))
            {
                flgContinue = false;
                strMessage += "Excise";
            }
            if (deccess != decimal.Parse(txtsalescessduty.Text))
            {
                flgContinue = false;
                strMessage += ",Cess";
            }
            if (decshcess != decimal.Parse(txtsaleshcessduty.Text))
            {
                flgContinue = false;
                strMessage += ",S&HCess";
            }
            if (decaddl != decimal.Parse(txtsalesaddlduty.Text))
            {
                flgContinue = false;
                strMessage += ",Additional";
            }
            if (!flgContinue)
            {
                DialogResult res = MessageBox.Show("Adjusted " + strMessage + " Duty does not match with Payable Duty, Are Sure to Continue?", "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Calculate_Difference_Amount()
        {
            decexcise = 0; deccess = 0; decshcess = 0; decaddl = 0;
            foreach (DataGridViewRow row in dgvcreditdetails.Rows)
            {
                decexcise += row.Cells["Adjust_Excise"].Value != null && row.Cells["Adjust_Excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Excise"].Value.ToString()) : decimal.Parse("0.00");
                deccess += row.Cells["Adjust_Cess"].Value != null && row.Cells["Adjust_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Cess"].Value.ToString()) : decimal.Parse("0.00");
                decshcess += row.Cells["Adjust_SHCess"].Value != null && row.Cells["Adjust_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                decaddl += row.Cells["Adjust_Addl"].Value != null && row.Cells["Adjust_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["Adjust_Addl"].Value.ToString()) : decimal.Parse("0.00");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvcreditdetails_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            ShowAdjustingAmount();
        }

        private void ShowAdjustingAmount()
        {
            Calculate_Difference_Amount();
            lblExcisePay.Text = "Ajustable Amount From Excise Duty : " + (decimal.Parse(txtsalesexduty.Text) - decexcise).ToString();
            lblCessPay.Text = "Ajustable Amount From Cess Duty : " + (decimal.Parse(txtsalescessduty.Text) - deccess).ToString();
            lblSHCessPay.Text = "Ajustable Amount From S&HCess Duty : " + ((decimal.Parse(txtsaleshcessduty.Text)) - decshcess).ToString();
            lblAddlPay.Text = "Ajustable Amount From Addl. Duty : " + (decimal.Parse(txtsalesaddlduty.Text) - decaddl).ToString();
        }
    }
}
