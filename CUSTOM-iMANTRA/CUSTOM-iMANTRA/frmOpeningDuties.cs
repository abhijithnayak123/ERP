using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmOpeningDuties : CustomBaseForm
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmOpeningDuties()
        {
            InitializeComponent();
        }

        private void dgvOpenDuties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmOpeningDuties_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            if (ACTIVE_BL.Code == "OB" )
            {
                LoadOpeningDuties();
                if (ACTIVE_BL.Tran_mode == "view_mode")
                { dgvOpenDuties.Enabled = false; }
                 AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
                 ucToolBar1.Titlebar = "Opening Duties";
            }
        }

        private void LoadOpeningDuties()
        {
            for (int i = 0; i <= 3; i++) // (DataRow row in dsetDebit.Tables[0].Rows)
            {
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


                foreach (DataGridViewRow row in dgvOpenDuties.Rows)
                {
                    if (row.Cells["row_col"].Value.ToString() == "RG23A")
                    {
                        row.Cells["ob_excise"].Value = objBLFD.HTMAIN["ob_rg23a_ex_amt"];
                        row.Cells["ob_Cess"].Value = objBLFD.HTMAIN["ob_rg23a_cess_amt"];
                        row.Cells["ob_SHCess"].Value = objBLFD.HTMAIN["ob_rg23a_hcess_amt"];
                        row.Cells["ob_Addl"].Value = objBLFD.HTMAIN["ob_rg23a_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "RG23C")
                    {
                        row.Cells["ob_excise"].Value = objBLFD.HTMAIN["ob_rg23c_ex_amt"];
                        row.Cells["ob_Cess"].Value = objBLFD.HTMAIN["ob_rg23c_cess_amt"];
                        row.Cells["ob_SHCess"].Value = objBLFD.HTMAIN["ob_rg23c_hcess_amt"];
                        row.Cells["ob_Addl"].Value = objBLFD.HTMAIN["ob_rg23c_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "ST")
                    {
                        row.Cells["ob_excise"].Value = objBLFD.HTMAIN["ob_st_ex_amt"];
                        row.Cells["ob_Cess"].Value = objBLFD.HTMAIN["ob_st_cess_amt"];
                        row.Cells["ob_SHCess"].Value = objBLFD.HTMAIN["ob_st_hcess_amt"];
                        row.Cells["ob_Addl"].Value = objBLFD.HTMAIN["ob_st_add_amt"];
                    }
                    if (row.Cells["row_col"].Value.ToString() == "PLA")
                    {
                        row.Cells["ob_excise"].Value = objBLFD.HTMAIN["ob_pla_ex_amt"];
                        row.Cells["ob_Cess"].Value = objBLFD.HTMAIN["ob_pla_cess_amt"];
                        row.Cells["ob_SHCess"].Value = objBLFD.HTMAIN["ob_pla_hcess_amt"];
                        row.Cells["ob_Addl"].Value = objBLFD.HTMAIN["ob_pla_add_amt"];

                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvOpenDuties.Rows)
            {
                if (row.Cells["row_col"].Value.ToString() == "RG23A")
                {
                    objBLFD.HTMAIN["ob_rg23a_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23a_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23a_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23a_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (row.Cells["row_col"].Value.ToString() == "RG23C")
                {
                    objBLFD.HTMAIN["ob_rg23c_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23c_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23c_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_rg23c_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (row.Cells["row_col"].Value.ToString() == "ST")
                {
                    objBLFD.HTMAIN["ob_st_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_st_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_st_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_st_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                }
                if (row.Cells["row_col"].Value.ToString() == "PLA")
                {
                    objBLFD.HTMAIN["ob_pla_ex_amt"] = row.Cells["ob_excise"].Value != null && row.Cells["ob_excise"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_excise"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_pla_cess_amt"] = row.Cells["ob_Cess"].Value != null && row.Cells["ob_Cess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Cess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_pla_hcess_amt"] = row.Cells["ob_SHCess"].Value != null && row.Cells["ob_SHCess"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_SHCess"].Value.ToString()) : decimal.Parse("0.00");
                    objBLFD.HTMAIN["ob_pla_add_amt"] = row.Cells["ob_Addl"].Value != null && row.Cells["ob_Addl"].Value.ToString() != "" ? decimal.Parse(row.Cells["ob_Addl"].Value.ToString()) : decimal.Parse("0.00");
                }
            }
            this.Close();
        }
    }
}
