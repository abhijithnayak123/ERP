using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmExportDetailsInSales : CustomBaseForm
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        dblayer objdblayer = new dblayer();
        public Hashtable HTAddl_Info = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmExportDetailsInSales()
        {
            InitializeComponent();
        }
        private void frmExportDetailsInSales_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet ds1 = objdblayer.dsquery("select * from CT_MAST");
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    cmbCtDesc.DataSource = ds1.Tables[0];
                    cmbCtDesc.DisplayMember = "CTDESC";
                    cmbCtDesc.ValueMember = "CTID";
                    cmbCtDesc.Update();
                    //ACTIVE_BL.HTMAIN["CTDESC"] = cmbCtDesc.SelectedItem;
                    //ACTIVE_BL.HTMAIN["CTID"] = cmbCtDesc.SelectedValue;
                }

                DataSet ds2 = objdblayer.dsquery("select * from ARE_MAST");
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    cmbAreDesc.DataSource = ds2.Tables[0];
                    cmbAreDesc.DisplayMember = "AREDESC";
                    cmbAreDesc.ValueMember = "AREID";
                    cmbAreDesc.Update();
                    //ACTIVE_BL.HTMAIN["AREDESC"] = cmbAreDesc.SelectedItem;
                    //ACTIVE_BL.HTMAIN["AREID"] = cmbAreDesc.SelectedValue;
                }

                if (ACTIVE_BL.Tran_mode != "add_mode")
                {
                    if (ACTIVE_BL.Tran_mode == "view_mode")
                    {
                        ((Control)tpExpDets1).Enabled = false;
                        ((Control)tpExpDets2).Enabled = false;
                    }
                    if (ACTIVE_BL.HTMAIN["CTDESC"].ToString() != "CT-3")
                    {
                        btn_ct.Enabled = false;
                    }
                    txtExpRef.Text = ACTIVE_BL.HTMAIN["EXP_REF"].ToString();
                    txtPoNo.Text = ACTIVE_BL.HTMAIN["PO_NO"].ToString();
                    txtOtherRef.Text = ACTIVE_BL.HTMAIN["OTHER_REF"].ToString();
                    txtCorg.Text = ACTIVE_BL.HTMAIN["C_ORG"].ToString();
                    txtCFinDest.Text = ACTIVE_BL.HTMAIN["C_FINDEST"].ToString();
                    txtPlaceRecpt.Text = ACTIVE_BL.HTMAIN["PLACE_RECPT"].ToString();
                    txtVesselNo.Text = ACTIVE_BL.HTMAIN["VESSEL_NO"].ToString();
                    dtShipDt.Value = DateTime.Parse(ACTIVE_BL.HTMAIN["SHIP_DT"].ToString());
                    txtPortLoad.Text = ACTIVE_BL.HTMAIN["P_LOAD"].ToString();
                    txtPortDisc.Text = ACTIVE_BL.HTMAIN["P_DISCHARGE"].ToString();
                    txtFinDest.Text = ACTIVE_BL.HTMAIN["FIN_DEST"].ToString();
                    txtPreCarg.Text = ACTIVE_BL.HTMAIN["PRE_CARG"].ToString();
                    txtCont1.Text = ACTIVE_BL.HTMAIN["CONT1_NO"].ToString();
                    txtCont2.Text = ACTIVE_BL.HTMAIN["CONT2_NO"].ToString();
                    txtExcNo1.Text = ACTIVE_BL.HTMAIN["EXC_SEAL1_NO"].ToString();
                    txtExcNo2.Text = ACTIVE_BL.HTMAIN["EXC_SEAL2_NO"].ToString();
                    txtTranNo1.Text = ACTIVE_BL.HTMAIN["TRAN_SEAL1_NO"].ToString();
                    txtTranNo2.Text = ACTIVE_BL.HTMAIN["TRAN_SEAL2_NO"].ToString();
                    txtShipDet.Text = ACTIVE_BL.HTMAIN["SHIP_DETS"].ToString();
                    txtTermDelPay.Text = ACTIVE_BL.HTMAIN["TER_DEL_PAY"].ToString();
                    txtBondNo.Text = ACTIVE_BL.HTMAIN["BOND_NO"].ToString();
                    dtBonddt.Value = DateTime.Parse(ACTIVE_BL.HTMAIN["BOND_DT"].ToString());
                    txtBondAmt.Text = ACTIVE_BL.HTMAIN["BOND_AMT"].ToString();
                    dtCtDt.Value = DateTime.Parse(ACTIVE_BL.HTMAIN["CT_DT"].ToString());
                    txtCtNo.Text = ACTIVE_BL.HTMAIN["CT_NO"].ToString();
                    txtAreNo.Text = ACTIVE_BL.HTMAIN["ARE_NO"].ToString();
                    dtAreDt.Value = DateTime.Parse(ACTIVE_BL.HTMAIN["ARE_DT"].ToString());
                    cmbCtDesc.Text = ACTIVE_BL.HTMAIN["CTDESC"].ToString();
                    cmbAreDesc.Text = ACTIVE_BL.HTMAIN["AREDESC"].ToString();
                    cmbCtDesc.SelectedValue = ACTIVE_BL.HTMAIN["CTID"].ToString();
                    cmbAreDesc.SelectedValue = ACTIVE_BL.HTMAIN["AREID"].ToString();

                }
                else
                {
                    btn_ct.Enabled = false;
                    txtExpRef.Text = ACTIVE_BL.HTMAIN["EXP_REF"].ToString();
                    txtPoNo.Text = ACTIVE_BL.HTMAIN["PO_NO"].ToString();
                    txtOtherRef.Text = ACTIVE_BL.HTMAIN["OTHER_REF"].ToString();
                    txtCorg.Text = ACTIVE_BL.HTMAIN["C_ORG"].ToString();
                    txtCFinDest.Text = ACTIVE_BL.HTMAIN["C_FINDEST"].ToString();
                    txtPlaceRecpt.Text = ACTIVE_BL.HTMAIN["PLACE_RECPT"].ToString();
                    txtVesselNo.Text = ACTIVE_BL.HTMAIN["VESSEL_NO"].ToString();
                    dtShipDt.Value = DateTime.Now;
                    txtPortLoad.Text = ACTIVE_BL.HTMAIN["P_LOAD"].ToString();
                    txtPortDisc.Text = ACTIVE_BL.HTMAIN["P_DISCHARGE"].ToString();
                    txtFinDest.Text = ACTIVE_BL.HTMAIN["FIN_DEST"].ToString();
                    txtPreCarg.Text = ACTIVE_BL.HTMAIN["PRE_CARG"].ToString();
                    txtCont1.Text = ACTIVE_BL.HTMAIN["CONT1_NO"].ToString();
                    txtCont2.Text = ACTIVE_BL.HTMAIN["CONT2_NO"].ToString();
                    txtExcNo1.Text = ACTIVE_BL.HTMAIN["EXC_SEAL1_NO"].ToString();
                    txtExcNo2.Text = ACTIVE_BL.HTMAIN["EXC_SEAL2_NO"].ToString();
                    txtTranNo1.Text = ACTIVE_BL.HTMAIN["TRAN_SEAL1_NO"].ToString();
                    txtTranNo2.Text = ACTIVE_BL.HTMAIN["TRAN_SEAL2_NO"].ToString();
                    txtShipDet.Text = ACTIVE_BL.HTMAIN["SHIP_DETS"].ToString();
                    txtTermDelPay.Text = ACTIVE_BL.HTMAIN["TER_DEL_PAY"].ToString();
                    txtBondNo.Text = ACTIVE_BL.HTMAIN["BOND_NO"].ToString();
                    dtBonddt.Value = DateTime.Now;
                    txtBondAmt.Text = ACTIVE_BL.HTMAIN["BOND_AMT"].ToString();
                    dtCtDt.Value = DateTime.Now;
                    txtCtNo.Text = ACTIVE_BL.HTMAIN["CT_NO"].ToString();

                    txtAreNo.Text = ACTIVE_BL.HTMAIN["ARE_NO"].ToString();
                    dtAreDt.Value = DateTime.Now;

                    //cmbCtDesc.SelectedItem = ACTIVE_BL.HTMAIN["CTDESC"].ToString();
                    //cmbAreDesc.SelectedItem = ACTIVE_BL.HTMAIN["AREDESC"].ToString();

                }

                AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
                ucToolBar1.Titlebar = "Export Details";
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception");
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            ACTIVE_BL.HTMAIN["EXP_REF"] = txtExpRef.Text;
            ACTIVE_BL.HTMAIN["PO_NO"] = txtPoNo.Text;
            ACTIVE_BL.HTMAIN["OTHER_REF"] = txtOtherRef.Text;
            ACTIVE_BL.HTMAIN["C_ORG"] = txtCorg.Text;
            ACTIVE_BL.HTMAIN["C_FINDEST"] = txtCFinDest.Text;
            ACTIVE_BL.HTMAIN["PLACE_RECPT"] = txtPlaceRecpt.Text;
            ACTIVE_BL.HTMAIN["VESSEL_NO"] = txtVesselNo.Text;
            ACTIVE_BL.HTMAIN["SHIP_DT"] = dtShipDt.Value;
            ACTIVE_BL.HTMAIN["P_LOAD"] = txtPortLoad.Text;
            ACTIVE_BL.HTMAIN["P_DISCHARGE"] = txtPortDisc.Text;
            ACTIVE_BL.HTMAIN["FIN_DEST"] = txtFinDest.Text;
            ACTIVE_BL.HTMAIN["PRE_CARG"] = txtPreCarg.Text;
            ACTIVE_BL.HTMAIN["CONT1_NO"] = txtCont1.Text;
            ACTIVE_BL.HTMAIN["CONT2_NO"] = txtCont2.Text;
            ACTIVE_BL.HTMAIN["EXC_SEAL1_NO"] = txtExcNo1.Text;
            ACTIVE_BL.HTMAIN["EXC_SEAL2_NO"] = txtExcNo2.Text;
            ACTIVE_BL.HTMAIN["TRAN_SEAL1_NO"] = txtTranNo1.Text;
            ACTIVE_BL.HTMAIN["TRAN_SEAL2_NO"] = txtTranNo2.Text;
            ACTIVE_BL.HTMAIN["SHIP_DETS"] = txtShipDet.Text;
            ACTIVE_BL.HTMAIN["TER_DEL_PAY"] = txtTermDelPay.Text;
            ACTIVE_BL.HTMAIN["CTDESC"] = cmbCtDesc.Text;
            ACTIVE_BL.HTMAIN["CT_NO"] = txtCtNo.Text;
            ACTIVE_BL.HTMAIN["CT_DT"] = dtCtDt.Value;
            ACTIVE_BL.HTMAIN["AREDESC"] = cmbAreDesc.Text;
            ACTIVE_BL.HTMAIN["ARE_NO"] = txtAreNo.Text;
            ACTIVE_BL.HTMAIN["ARE_DT"] = dtAreDt.Value;
            ACTIVE_BL.HTMAIN["BOND_NO"] = txtBondNo.Text;
            ACTIVE_BL.HTMAIN["BOND_DT"] = dtBonddt.Text;
            ACTIVE_BL.HTMAIN["AREID"] = cmbAreDesc.SelectedValue;
            ACTIVE_BL.HTMAIN["CTID"] = cmbCtDesc.SelectedValue;

            if (ACTIVE_BL.HTMAIN["BOND_AMT"].ToString() == "" || ACTIVE_BL.HTMAIN["BOND_AMT"] == null)
            {
                ACTIVE_BL.HTMAIN["BOND_AMT"] = "0.00";
            }
            else
            {
                ACTIVE_BL.HTMAIN["BOND_AMT"] = txtBondAmt.Text;
            }
            this.Close();
        }
        private void cmbCtDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (ACTIVE_BL.HTMAIN["CTDESC"] != null && ACTIVE_BL.HTMAIN["CTDESC"].ToString() != "")
                if (cmbCtDesc.Text == "CT-3")
                {
                    btn_ct.Enabled = true;
                }
                if (ACTIVE_BL.Tran_mode == "add_mode")
                {
                    if (cmbCtDesc.Text.ToString() != "System.Data.DataRowView")
                    {
                        ACTIVE_BL.HTMAIN["CTDESC"] = cmbCtDesc.Text;
                        ACTIVE_BL.HTMAIN["CTID"] = cmbCtDesc.SelectedValue;
                        DataSet dsname = objdblayer.dsquery("select (isnull(max(ct_no),0)+1) CT_NO from SEMAIN  where SEMAIN.CTDESC='" + cmbCtDesc.Text + "'");
                        txtCtNo.Text = dsname.Tables[0].Rows[0]["CT_NO"].ToString();
                    }
                }
                else if (ACTIVE_BL.Tran_mode == "edit_mode")
                {
                    txtCtNo.ReadOnly = true;
                    txtAreNo.ReadOnly = true;
                    btn_ct.Enabled = false;
                    dtCtDt.Enabled = false;
                    dtAreDt.Enabled = false;                   
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception");
            }
        }

        private void cmbAreDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // if (ACTIVE_BL.HTMAIN["AREDESC"] != null && ACTIVE_BL.HTMAIN["AREDESC"].ToString() != "")
                if (ACTIVE_BL.Tran_mode == "add_mode")
                {
                    if (cmbAreDesc.Text.ToString() != "System.Data.DataRowView")
                    {
                        ACTIVE_BL.HTMAIN["AREDESC"] = cmbAreDesc.Text;
                        ACTIVE_BL.HTMAIN["AREID"] = cmbAreDesc.SelectedValue;
                        DataSet dsname = objdblayer.dsquery("select (isnull(max(are_no),0)+1) ARE_NO from SEMAIN  where SEMAIN.AREDESC='" + cmbAreDesc.Text + "'");
                        txtAreNo.Text = dsname.Tables[0].Rows[0]["ARE_NO"].ToString();
                    }
                }               
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception");
            }

        }

        private void btn_ct_Click(object sender, EventArgs e)
        {
            try
            {
                frmPopup objpopup = new frmPopup("SOMAIN", "CT", "CTID,CT_NO", "CT_NO;CT NO", "Please select", "");
                objpopup.ShowDialog();
                txtCtNo.Text = objpopup.ResultFieldValue.Split(',')[1];
            }
            catch (Exception ex)
            {

            }
        }

        private void dtCtDt_Validating(object sender, CancelEventArgs e)
        {
            if (cmbCtDesc.Text == "CT-3")
            {
                DataSet ds = objdblayer.dsquery("select ct_no,ct_dt from somain where tran_cd ='CT' and ct_no='" + txtCtNo.Text + "'");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (dtCtDt.Value != DateTime.Parse(ds.Tables[0].Rows[0]["CT_DT"].ToString()))
                    {
                        AutoClosingMessageBox.Show("CT-3 Date not valid","Validation");
                        e.Cancel = true;
                    }
                }
            }
        }

    }
}
