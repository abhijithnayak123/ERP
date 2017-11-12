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
using CUSTOM_iMANTRA;
using CUSTOM_iMANTRA_BL;
using System.Collections;
using System.Reflection;
using iMANTRA_iniL;

namespace iMANTRA
{
    public partial class frmTransactionSetting : BaseClass
    {
        /*    
        * 1.0 Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.29.13  ==> Added Amdent Details
         * 2.0 Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 12.03.13 ==> Added Pick Up Details
         * 
         * 
         * 
        * */
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        FL_PICKUP objPickUp = new FL_PICKUP();

        BLHT objhashtable = new BLHT();

        Ini objIni = new Ini();
        string key = "";

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

        public frmTransactionSetting(BL_BASEFIELD objBL)
        {
            try
            {
                //MessageBox.Show("Transaction Setting --2 start");
                InitializeComponent();
                // MessageBox.Show("Transaction Setting --2.1 start");
                this.Tran_cd = objBL.Code;
                objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
                if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
                {
                    objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
                }
                this.objBASEFILEDS = objBL;

                // MessageBox.Show("Transaction Setting --2 end");
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message + "InnerException : " + ex.InnerException);
            }
        }

        private void frmTransactionSetting_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Transaction Setting --3 ");
            BindControls();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            //ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width;

            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");

            rectangleShape3.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape4.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape7.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape2.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape8.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape1.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape6.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape5.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);
            rectangleShape17.Bounds = new Rectangle(rectangleShape10.Location.X, rectangleShape10.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape10.Height - 1);

            rectangleShape1.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape2.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape3.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape4.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape5.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape6.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape7.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape8.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape9.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape10.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape11.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape12.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape13.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape14.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape15.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape16.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape18.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape17.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;

            rectangleShape9.Width = this.tabPage1.Width / 2;
            rectangleShape10.Width = this.tabPage1.Width / 2;
            rectangleShape11.Width = this.tabPage1.Width / 2;
            rectangleShape12.Width = this.tabPage1.Width / 2;
            rectangleShape13.Width = this.tabPage1.Width / 2;
            rectangleShape14.Width = this.tabPage1.Width / 2;
            rectangleShape15.Width = this.tabPage1.Width / 2;
            rectangleShape16.Width = this.tabPage1.Width / 2;
            rectangleShape18.Width = this.tabPage1.Width / 2;
        }

        private void BindControls()
        {
            String[] myArray = { "", "A-Allow Back End Date Entry", "B-Do Not Allow Back End Date Entry", "C-Allow Greater Transaction No. For Back Date Entry" };
            cmdBackDt.DataSource = myArray.ToArray();
            cmdBackDt.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);

            String[] myArray1 = { "NONE", "+", "-" };
            cmbStock.DataSource = myArray1.ToArray();
            DataSet dsetAcGr = objDBAdaper.dsquery("select ac_grp_nm from cm_group where ac_grp_nm!='' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            if (dsetAcGr != null && dsetAcGr.Tables.Count != 0 && dsetAcGr.Tables[0].Rows.Count != 0)
            {
                chklstAccGr.Items.Clear();
                var acitems = chklstAccGr.Items;
                foreach (DataRow row in dsetAcGr.Tables[0].Rows)
                {
                    acitems.Add(row["ac_grp_nm"].ToString());
                }
            }

            DataSet dsetProdType = objDBAdaper.dsquery("select prod_ty_nm from PROD_TYPE where prod_ty_nm!='' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            if (dsetProdType != null && dsetProdType.Tables.Count != 0 && dsetProdType.Tables[0].Rows.Count != 0)
            {
                chklstProdType.Items.Clear();
                var prodTypeitems = chklstProdType.Items;
                foreach (DataRow row in dsetProdType.Tables[0].Rows)
                {
                    prodTypeitems.Add(row["prod_ty_nm"].ToString());
                }
            }
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
            {
                if (!objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                {
                    if (column.DataType.Name.ToString().ToLower() == "int32")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "boolean")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = false;
                    }
                    if (column.DataType.Name.ToString().ToLower() == "string")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "decimal")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0.00";
                    }
                }
            }

            foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
            {
                foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                {
                    if (objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString();
                    }
                }
            }
        }
        private void AddFieldToDataSet(string tran_id)
        {
            DataSet dsetview = new DataSet();
            dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where code='" + tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            if (dsetview != null && dsetview.Tables.Count != 0 && dsetview.Tables[0].Rows.Count != 0)
            {
                objBASEFILEDS.HTMAIN["Main_tbl_nm"] = dsetview.Tables[0].Rows[0]["Main_tbl_nm"].ToString();
                objBASEFILEDS.HTMAIN["Item_tbl_nm"] = dsetview.Tables[0].Rows[0]["Item_tbl_nm"].ToString();
                objBASEFILEDS.HTMAIN["Ref_tbl_nm"] = dsetview.Tables[0].Rows[0]["Ref_tbl_nm"].ToString();
                //objBASEFILEDS.HTMAIN["Ref_Type"] = dsetview.Tables[0].Rows[0]["Ref_Type"].ToString();
                objBASEFILEDS.HTMAIN["Extra_tbl_nm"] = dsetview.Tables[0].Rows[0]["Extra_tbl_nm"].ToString();
                objBASEFILEDS.HTMAIN["ac_tbl_nm"] = dsetview.Tables[0].Rows[0]["ac_tbl_nm"].ToString();
                objBASEFILEDS.HTMAIN["alloc_tbl_nm"] = dsetview.Tables[0].Rows[0]["alloc_tbl_nm"].ToString();

                chkAutoTran.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["auto_tran_no"].ToString());
                chkEditTrans.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["edit_tran_no"].ToString());
                chkAccPost.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["ac_post"].ToString());
                chkCurrDt.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["curr_date"].ToString());
                chkPrintSave.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["prnt_saving"].ToString());
                chkPrintOnce.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["print_once"].ToString());
                chkProdSet.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["Prod_det"].ToString());
                chkAccNarr.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["acc_narr"].ToString());
                chkOvrCrDr.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["over_cr_dr"].ToString());
                chkDefAccTranType.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isDefAccTranType"] != null && dsetview.Tables[0].Rows[0]["isDefAccTranType"].ToString() != "" ? dsetview.Tables[0].Rows[0]["isDefAccTranType"].ToString() : "false");
                txtTransNo.Text = dsetview.Tables[0].Rows[0]["Tran_no_wid"].ToString();
                txtCopy.Text = dsetview.Tables[0].Rows[0]["copies_nm"].ToString();
                txtCrAcNm.Text = dsetview.Tables[0].Rows[0]["cr_ac_nm"].ToString();
                txtDrAcNm.Text = dsetview.Tables[0].Rows[0]["dr_ac_nm"].ToString();
                txtDispLocate.Text = dsetview.Tables[0].Rows[0]["disp_locate"].ToString();
                chkGrossRoundOff.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["round_groamt"].ToString());
                chkAssesRoundOff.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["round_asses_amt"].ToString());
                chkRoundAcc.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["round_acc"].ToString());
                cmdBackDt.SelectedItem = dsetview.Tables[0].Rows[0]["bck_entry"] != null && dsetview.Tables[0].Rows[0]["bck_entry"].ToString() != "" ? dsetview.Tables[0].Rows[0]["bck_entry"].ToString() : "";
                //dsetview.Tables[0].Rows[0]["bck_entry"].ToString();

                string[] strAccGrArray = dsetview.Tables[0].Rows[0]["ac_grp"].ToString().Split(',');

                for (int count = 0; count < chklstAccGr.Items.Count; count++)
                {
                    if (strAccGrArray.Contains(chklstAccGr.Items[count].ToString())) { chklstAccGr.SetItemChecked(count, true); }
                    else { chklstAccGr.SetItemChecked(count, false); }
                }

                txtAccPopUp.Text = dsetview.Tables[0].Rows[0]["Ac_pop_sel"].ToString();
                txtDefAcc.Text = dsetview.Tables[0].Rows[0]["def_acc"].ToString();
                txtDefConsignee.Text = dsetview.Tables[0].Rows[0]["def_consignee"].ToString();
                chkFilteredReq.Checked = dsetview.Tables[0].Rows[0]["filter_req"] != null && dsetview.Tables[0].Rows[0]["filter_req"].ToString() != "" ? bool.Parse(dsetview.Tables[0].Rows[0]["filter_req"].ToString()) : false;

                string[] strProdTyArray = dsetview.Tables[0].Rows[0]["Pt_type_avail"].ToString().Split(',');

                for (int count = 0; count < chklstProdType.Items.Count; count++)
                {
                    if (strProdTyArray.Contains(chklstProdType.Items[count].ToString())) { chklstProdType.SetItemChecked(count, true); }
                    else { chklstProdType.SetItemChecked(count, false); }
                }

                //chkProdNarr.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["Pt_narr"].ToString());
                txtProdPopUp.Text = dsetview.Tables[0].Rows[0]["Pt_pop_sel"].ToString();
                cmbStock.SelectedItem = dsetview.Tables[0].Rows[0]["stk_effect"] != null && dsetview.Tables[0].Rows[0]["stk_effect"].ToString() != "" ? dsetview.Tables[0].Rows[0]["stk_effect"].ToString() : "NONE";
                chkRuleReq.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isRule"] != null && dsetview.Tables[0].Rows[0]["isRule"].ToString() != "" ? dsetview.Tables[0].Rows[0]["isRule"].ToString() : "False");

                chkDc.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isDCApp"].ToString());
                chkApproval.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isApprove"].ToString());
                chkReqAuthority.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isReqAuthority"] != null && dsetview.Tables[0].Rows[0]["isReqAuthority"].ToString() != "" ? dsetview.Tables[0].Rows[0]["isReqAuthority"].ToString() : "False");

                chkTransCopy.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isTransCopy"].ToString());
                chkST.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isTaxApp"].ToString());
                chkSTRoundOff.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isTaxRound"].ToString());
                chkConsignee.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["cons"].ToString());
                chkFileAttach.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isFileAttach"].ToString());
                chkAmendment.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isAmendment"].ToString());
                chk_schedule.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isSchedule"].ToString());
                chkDe_schedule.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isDeSchedule"].ToString());
                chkSendMail.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isSendMail"].ToString());
                chkRaiseApproval.Checked = bool.Parse(dsetview.Tables[0].Rows[0]["isTransferApproval"].ToString());

                string[] strSeachArray = dsetview.Tables[0].Rows[0]["seachfield"].ToString().Split(',');

                for (int count = 0; count < chklstSearch.Items.Count; count++)
                {
                    if (strSeachArray.Contains(chklstSearch.Items[count].ToString())) { chklstSearch.SetItemChecked(count, true); }
                    else { chklstSearch.SetItemChecked(count, false); }
                }

                txtRefTranNm1.Text = dsetview.Tables[0].Rows[0]["ref_tran_nm"].ToString();
                //txtRefCode.Text = dsetview.Tables[0].Rows[0]["ref_type"].ToString();
                //txtRefBehCd.Text = dsetview.Tables[0].Rows[0]["Ref_behaiver_cd"].ToString();
                //txtRefPickUp.Text = dsetview.Tables[0].Rows[0]["ref_tran_fld"].ToString();
                InsertFieldValueToHashTable();
            }
        }
        private void InsertFieldValueToHashTable()
        {
            objBASEFILEDS.HTMAIN["code"] = txtTransType.Text;
            objBASEFILEDS.HTMAIN["Tran_nm"] = txtTransNm.Text;
            objBASEFILEDS.HTMAIN["tran_no_nm"] = txtTransCaption.Text;
            objBASEFILEDS.HTMAIN["Behavier_cd"] = txtBehCode.Text;
            objBASEFILEDS.HTMAIN["auto_tran_no"] = chkAutoTran.Checked;
            objBASEFILEDS.HTMAIN["edit_tran_no"] = chkEditTrans.Checked;
            objBASEFILEDS.HTMAIN["ac_post"] = chkAccPost.Checked;
            objBASEFILEDS.HTMAIN["curr_date"] = chkCurrDt.Checked;
            objBASEFILEDS.HTMAIN["prnt_saving"] = chkPrintSave.Checked;
            objBASEFILEDS.HTMAIN["print_once"] = chkPrintOnce.Checked;
            objBASEFILEDS.HTMAIN["Prod_det"] = chkProdSet.Checked;
            objBASEFILEDS.HTMAIN["acc_narr"] = chkAccNarr.Checked;
            objBASEFILEDS.HTMAIN["over_cr_dr"] = chkOvrCrDr.Checked;
            objBASEFILEDS.HTMAIN["isDefAccTranType"] = chkDefAccTranType.Checked;
            objBASEFILEDS.HTMAIN["Tran_no_wid"] = txtTransNo.Text;
            objBASEFILEDS.HTMAIN["Copies_nm"] = txtCopy.Text;
            objBASEFILEDS.HTMAIN["dr_ac_nm"] = txtDrAcNm.Text;
            objBASEFILEDS.HTMAIN["cr_ac_nm"] = txtCrAcNm.Text;
            objBASEFILEDS.HTMAIN["disp_locate"] = txtDispLocate.Text;
            objBASEFILEDS.HTMAIN["round_groamt"] = chkGrossRoundOff.Checked;
            objBASEFILEDS.HTMAIN["round_asses_amt"] = chkAssesRoundOff.Checked;
            objBASEFILEDS.HTMAIN["round_acc"] = chkRoundAcc.Checked;
            objBASEFILEDS.HTMAIN["bck_entry"] = cmdBackDt.SelectedValue != null && cmdBackDt.SelectedValue.ToString() != "" ? cmdBackDt.SelectedValue : "";//cmdBackDt.SelectedValue;

            string strAccGrArray = "";
            for (int count = 0; count < chklstAccGr.CheckedItems.Count; count++)
            {
                if (strAccGrArray != "") { strAccGrArray = strAccGrArray + "," + chklstAccGr.CheckedItems[count].ToString(); }
                else { strAccGrArray = chklstAccGr.CheckedItems[count].ToString(); }
            }
            objBASEFILEDS.HTMAIN["ac_grp"] = strAccGrArray;
            objBASEFILEDS.HTMAIN["Ac_pop_sel"] = txtAccPopUp.Text;
            objBASEFILEDS.HTMAIN["def_acc"] = txtDefAcc.Text;
            objBASEFILEDS.HTMAIN["def_consignee"] = txtDefConsignee.Text;
            objBASEFILEDS.HTMAIN["filter_req"] = chkFilteredReq.Checked;

            string strProdTyArray = "";

            for (int count = 0; count < chklstProdType.CheckedItems.Count; count++)
            {
                if (strProdTyArray != "") { strProdTyArray = strProdTyArray + "," + chklstProdType.CheckedItems[count].ToString(); }
                else { strProdTyArray = chklstProdType.CheckedItems[count].ToString(); }
            }
            objBASEFILEDS.HTMAIN["Pt_type_avail"] = strProdTyArray;

            // objBASEFILEDS.HTMAIN["Pt_narr"] = chkProdNarr.Checked;
            objBASEFILEDS.HTMAIN["Pt_pop_sel"] = txtProdPopUp.Text;
            objBASEFILEDS.HTMAIN["stk_effect"] = cmbStock.SelectedValue != null && cmbStock.SelectedValue.ToString() != "NONE" ? cmbStock.SelectedValue : "";
            objBASEFILEDS.HTMAIN["isRule"] = chkRuleReq.Checked;

            objBASEFILEDS.HTMAIN["isDCApp"] = chkDc.Checked;
            objBASEFILEDS.HTMAIN["isApprove"] = chkApproval.Checked;
            objBASEFILEDS.HTMAIN["isReqAuthority"] = chkReqAuthority.Checked;
            objBASEFILEDS.HTMAIN["isTransCopy"] = chkTransCopy.Checked;
            objBASEFILEDS.HTMAIN["isTaxApp"] = chkST.Checked;
            objBASEFILEDS.HTMAIN["isTaxRound"] = chkSTRoundOff.Checked;
            objBASEFILEDS.HTMAIN["cons"] = chkConsignee.Checked;
            objBASEFILEDS.HTMAIN["isFileAttach"] = chkFileAttach.Checked;
            objBASEFILEDS.HTMAIN["isAmendment"] = chkAmendment.Checked;
            objBASEFILEDS.HTMAIN["isSchedule"] = chk_schedule.Checked;
            objBASEFILEDS.HTMAIN["isDeSchedule"] = chkDe_schedule.Checked;
            objBASEFILEDS.HTMAIN["isSendMail"] = chkSendMail.Checked;
            objBASEFILEDS.HTMAIN["isTransferApproval"] = chkRaiseApproval.Checked;

            string strSeachArray = "";// = objBASEFILEDS.HTMAIN["seachfield"].ToString().Split(',');

            for (int count = 0; count < chklstSearch.CheckedItems.Count; count++)
            {
                if (strSeachArray != "") { strSeachArray = strSeachArray + "," + chklstSearch.CheckedItems[count].ToString(); }
                else { strSeachArray = chklstSearch.CheckedItems[count].ToString(); }
            }
            objBASEFILEDS.HTMAIN["seachfield"] = strSeachArray;
            //  objBASEFILEDS.HTMAIN["ref_tran_nm"] = txtRefTranNm1.Text;
            //objBASEFILEDS.HTMAIN["Ref_behaiver_cd"] = txtRefBehCd.Text;
            //objBASEFILEDS.HTMAIN["ref_tran_fld"] = txtRefPickUp.Text;
            objBASEFILEDS.HTMAIN["ref_type"] = txtRefTranNm1.Text;
        }
        private void GetFieldValueFromHashTable()
        {
            txtTransType.Text = objBASEFILEDS.HTMAIN["code"].ToString();
            txtTransNm.Text = objBASEFILEDS.HTMAIN["Tran_nm"].ToString();
            txtTransCaption.Text = objBASEFILEDS.HTMAIN["tran_no_nm"].ToString();
            txtBehCode.Text = objBASEFILEDS.HTMAIN["Behavier_cd"].ToString();
            chkAutoTran.Checked = bool.Parse(objBASEFILEDS.HTMAIN["auto_tran_no"].ToString());
            chkEditTrans.Checked = bool.Parse(objBASEFILEDS.HTMAIN["edit_tran_no"].ToString());
            chkAccPost.Checked = bool.Parse(objBASEFILEDS.HTMAIN["ac_post"].ToString());
            chkCurrDt.Checked = bool.Parse(objBASEFILEDS.HTMAIN["curr_date"].ToString());
            chkPrintSave.Checked = bool.Parse(objBASEFILEDS.HTMAIN["prnt_saving"].ToString());
            chkPrintOnce.Checked = bool.Parse(objBASEFILEDS.HTMAIN["print_once"].ToString());
            chkProdSet.Checked = bool.Parse(objBASEFILEDS.HTMAIN["Prod_det"].ToString());
            chkAccNarr.Checked = bool.Parse(objBASEFILEDS.HTMAIN["acc_narr"].ToString());
            chkOvrCrDr.Checked = bool.Parse(objBASEFILEDS.HTMAIN["over_cr_dr"].ToString());
            chkDefAccTranType.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isDefAccTranType"] != null && objBASEFILEDS.HTMAIN["isDefAccTranType"].ToString() != "" ? objBASEFILEDS.HTMAIN["isDefAccTranType"].ToString() : "false");
            txtTransNo.Text = objBASEFILEDS.HTMAIN["Tran_no_wid"].ToString();
            txtCopy.Text = objBASEFILEDS.HTMAIN["Copies_nm"].ToString();
            txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
            txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
            txtDispLocate.Text = objBASEFILEDS.HTMAIN["disp_locate"].ToString();
            chkGrossRoundOff.Checked = bool.Parse(objBASEFILEDS.HTMAIN["round_groamt"].ToString());
            chkAssesRoundOff.Checked = bool.Parse(objBASEFILEDS.HTMAIN["round_asses_amt"].ToString());
            chkRoundAcc.Checked = bool.Parse(objBASEFILEDS.HTMAIN["round_acc"].ToString());
            cmdBackDt.SelectedItem = objBASEFILEDS.HTMAIN["bck_entry"] != null && objBASEFILEDS.HTMAIN["bck_entry"].ToString() != "" ? objBASEFILEDS.HTMAIN["bck_entry"].ToString() : "NONE";//objBASEFILEDS.HTMAIN["bck_entry"].ToString();

            string[] strAccGrArray = objBASEFILEDS.HTMAIN["ac_grp"].ToString().Split(',');

            for (int count = 0; count < chklstAccGr.Items.Count; count++)
            {
                if (strAccGrArray.Contains(chklstAccGr.Items[count].ToString()))
                {
                    chklstAccGr.SetItemChecked(count, true);
                }
                else
                {
                    chklstAccGr.SetItemChecked(count, false);
                }
            }

            txtAccPopUp.Text = objBASEFILEDS.HTMAIN["Ac_pop_sel"].ToString();
            txtDefAcc.Text = objBASEFILEDS.HTMAIN["def_acc"].ToString();
            txtDefConsignee.Text = objBASEFILEDS.HTMAIN["def_consignee"].ToString();
            chkFilteredReq.Checked = objBASEFILEDS.HTMAIN["filter_req"] != null && objBASEFILEDS.HTMAIN["filter_req"].ToString() != "" ? bool.Parse(objBASEFILEDS.HTMAIN["filter_req"].ToString()) : false;

            string[] strProdTyArray = objBASEFILEDS.HTMAIN["Pt_type_avail"].ToString().Split(',');

            for (int count = 0; count < chklstProdType.Items.Count; count++)
            {
                if (strProdTyArray.Contains(chklstProdType.Items[count].ToString()))
                {
                    chklstProdType.SetItemChecked(count, true);
                }
                else
                {
                    chklstProdType.SetItemChecked(count, false);
                }
            }

            //chkProdNarr.Checked = bool.Parse(objBASEFILEDS.HTMAIN["Pt_narr"].ToString());
            txtProdPopUp.Text = objBASEFILEDS.HTMAIN["Pt_pop_sel"].ToString();
            cmbStock.SelectedItem = objBASEFILEDS.HTMAIN["stk_effect"] != null && objBASEFILEDS.HTMAIN["stk_effect"].ToString() != "" ? objBASEFILEDS.HTMAIN["stk_effect"].ToString() : "NONE";
            chkRuleReq.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isRule"].ToString());

            chkDc.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isDCApp"].ToString());
            chkApproval.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isApprove"].ToString());
            chkReqAuthority.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isReqAuthority"] != null && objBASEFILEDS.HTMAIN["isReqAuthority"].ToString() != "" ? objBASEFILEDS.HTMAIN["isReqAuthority"].ToString() : "False");
            chkTransCopy.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isTransCopy"] != null && objBASEFILEDS.HTMAIN["isTransCopy"].ToString() != "" ? objBASEFILEDS.HTMAIN["isTransCopy"].ToString() : "False");
            chkST.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isTaxApp"] != null && objBASEFILEDS.HTMAIN["isTaxApp"].ToString() != "" ? objBASEFILEDS.HTMAIN["isTaxApp"].ToString() : "False");
            chkSTRoundOff.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isTaxRound"] != null && objBASEFILEDS.HTMAIN["isTaxRound"].ToString() != "" ? objBASEFILEDS.HTMAIN["isTaxRound"].ToString() : "False");
            chkConsignee.Checked = bool.Parse(objBASEFILEDS.HTMAIN["cons"] != null && objBASEFILEDS.HTMAIN["cons"].ToString() != "" ? objBASEFILEDS.HTMAIN["cons"].ToString() : "False");
            chkFileAttach.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isFileAttach"] != null && objBASEFILEDS.HTMAIN["isFileAttach"].ToString() != "" ? objBASEFILEDS.HTMAIN["isFileAttach"].ToString() : "False");
            chkAmendment.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isAmendment"] != null && objBASEFILEDS.HTMAIN["isAmendment"].ToString() != "" ? objBASEFILEDS.HTMAIN["isAmendment"].ToString() : "False");
            chk_schedule.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isSchedule"] != null && objBASEFILEDS.HTMAIN["isSchedule"].ToString() != "" ? objBASEFILEDS.HTMAIN["isSchedule"].ToString() : "False");
            chkDe_schedule.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isDeSchedule"] != null && objBASEFILEDS.HTMAIN["isDeSchedule"].ToString() != "" ? objBASEFILEDS.HTMAIN["isDeSchedule"].ToString() : "False");
            chkSendMail.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isSendMail"] != null && objBASEFILEDS.HTMAIN["isSendMail"].ToString() != "" ? objBASEFILEDS.HTMAIN["isSendMail"].ToString() : "False");
            chkRaiseApproval.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isTransferApproval"] != null && objBASEFILEDS.HTMAIN["isTransferApproval"].ToString() != "" ? objBASEFILEDS.HTMAIN["isTransferApproval"].ToString() : "False");

            string[] strSeachArray = objBASEFILEDS.HTMAIN["seachfield"].ToString().Split(',');

            for (int count = 0; count < chklstSearch.Items.Count; count++)
            {
                if (strSeachArray.Contains(chklstSearch.Items[count].ToString()))
                {
                    chklstSearch.SetItemChecked(count, true);
                }
                else
                {
                    chklstSearch.SetItemChecked(count, false);
                }
            }

            // txtRefTranNm1.Text = objBASEFILEDS.HTMAIN["ref_tran_nm"].ToString();
            //txtRefBehCd.Text = objBASEFILEDS.HTMAIN["Ref_behaiver_cd"].ToString();
            //txtRefPickUp.Text = objBASEFILEDS.HTMAIN["ref_tran_fld"].ToString();
            txtRefTranNm1.Text = objBASEFILEDS.HTMAIN["ref_type"].ToString();
        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                if (objhashtable != null && objhashtable.HashGeneraltbl != null)
                {
                    objhashtable.HashGeneraltbl.Clear();
                }
                switch (tran_mode)
                {
                    case "add_mode":
                        foreach (Control c in this.Controls)//form controls
                        {
                            //foreach (Control c in con.Controls)//groupbox controls
                            //{
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        if (c2 is CheckBox)
                                        {
                                            ((CheckBox)c2).Checked = false;
                                        }
                                        else if (c2 is ComboBox)
                                        {
                                            if (((ComboBox)c2).SelectedIndex != -1)
                                            {
                                                ((ComboBox)c2).SelectedIndex = 0;
                                            }
                                        }
                                        else if (c2 is TextBox)
                                        {
                                            ((TextBox)c2).Text = "";
                                        }
                                        c2.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (c is CheckBox)
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                                else if (c is ComboBox)
                                {
                                    if (((ComboBox)c).SelectedIndex != -1)
                                    {
                                        ((ComboBox)c).SelectedIndex = 0;
                                    }
                                }
                                else if (c is TextBox)
                                {
                                    ((TextBox)c).Text = "";
                                }
                                c.Enabled = true;
                            }
                            //}
                        }
                        // EnableApprovePage();
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        btnBehCode.Enabled = true;
                        btnTransNm.Enabled = false;
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        GetFieldValueFromHashTable();
                        BindApproveGrid();
                        CallPickUpGrid();//2.0
                        foreach (Control c in this.Controls)//form controls
                        {
                            //foreach (Control c in con.Controls)//groupbox controls
                            //{
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        c2.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                c.Enabled = true;
                            }
                            //}
                        }
                        // EnableApprovePage();
                        if (objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && objBASEFILEDS.HASHTABLES.HashItemtbl.Count != 0)
                        {
                            btnConstructLvl.Enabled = false;
                        }
                        txtTransType.Enabled = false;
                        txtTransNm.Enabled = false;
                        btnBehCode.Enabled = false;
                        txtBehCode.Enabled = false;
                        btnTransNm.Enabled = true;
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        GetFieldValueFromHashTable();
                        BindApproveGrid();
                        foreach (Control c in this.Controls)//form controls
                        {
                            //foreach (Control c in con.Controls)//groupbox controls
                            //{
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        if (!(c2 is Label)) c2.Enabled = false;
                                    }
                                }
                            }
                            if (c is TextBox || c is Button)
                            {
                                if (!(c is Label)) c.Enabled = false;
                            }
                            //}
                        }
                        btnBehCode.Enabled = false;
                        btnTransNm.Enabled = true;
                        CallPickUpGrid();//2.0
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
            objBASEFILEDS.HTMAIN["Tran_type"] = "Transaction";
            bool flg = true;
            InsertFieldValueToHashTable();
            if (objBASEFILEDS.Tran_mode == "add_mode")
            {
                objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id] = "0";
            }
            if (txtTransType.Text == "") { AutoClosingMessageBox.Show("Please enter Valid Transaction Code", "Validation", 3000); flg = false; }
            else if (txtTransNm.Text == "") { AutoClosingMessageBox.Show("Please enter Transaction Name", "Validation", 3000); flg = false; }
            else if (txtTransNo.Text == "") { AutoClosingMessageBox.Show("Please enter Transaction Number", "Validation", 3000); flg = false; }
            else if (txtDispLocate.Text == "") { AutoClosingMessageBox.Show("Please enter Display Locate", "Validation", 3000); flg = false; }
            else if (txtAccPopUp.Text == "") { AutoClosingMessageBox.Show("Please enter Account Popup Fields", "Validation", 3000); flg = false; }
            else if (txtProdPopUp.Text == "") { AutoClosingMessageBox.Show("Please enter Product Popup Fields", "Validation", 3000); flg = false; }
            else if (chkApproval.Checked)
            {
                if (txtlevelno.Text == "" || txtlevelno.Text == "0") { AutoClosingMessageBox.Show("Please enter Levels Count", "Validation", 3000); flg = false; }
                else
                {
                    if (!(objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && objBASEFILEDS.HASHTABLES.HashItemtbl.Count != 0))
                    {
                        objBASEFILEDS.HASHTABLES = new CUSTOM_iMANTRA_BL.BLHT();
                    }
                    foreach (DataGridViewRow row in dgvApprove.Rows)
                    {
                        if (objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(row.Cells["si_no"].Value.ToString()))
                        {
                            if (row.Cells["isreqlvl"].Value != null && row.Cells["isreqlvl"].Value.ToString() != "")
                            {
                                if (row.Cells["isreqlvl"].Value.ToString() == "1")
                                {
                                    row.Cells["isreqlvl"].Value = true;
                                }
                                else if (row.Cells["isreqlvl"].Value.ToString() == "0")
                                {
                                    row.Cells["isreqlvl"].Value = false;
                                }
                            }
                            else
                            {
                                row.Cells["isreqlvl"].Value = false;
                            }
                            if (chkCondition.Checked)
                            {
                                if (((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["condition"] == null || ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["condition"].ToString() == "")
                                {
                                    AutoClosingMessageBox.Show("Please provive condition for " + ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["level_nm"], "Validation", 3000);
                                    return false;
                                }
                            }
                            else
                            {
                                if (((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["condition"] == null)
                                {
                                    ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["condition"] = "";
                                }
                            }
                            ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["isreqlvl"] = row.Cells["isreqlvl"].Value;
                            ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["level_cnt"] = txtlevelno.Text;
                            ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["main_cond_req"] = chkCondition.Checked;
                            //((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])["isReqAuthority"] = chkReqAuthority.Checked;
                        }
                    }
                }
            }
            return flg;
        }

        private void frmTransactionSetting_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmTransactionSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void btnTransNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "tran_setid,Behavier_cd", "tran_nm;Transaction Name,code;Transaction Code", "Please Select", "Tran_type in ('Transaction','Accounting') and code!='TS' and code!='OM' and code=Behavier_cd", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtBehCode.Text = objBASEFILEDS.HTMAIN["Behavier_cd"].ToString();
            objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["tran_setid"].ToString();
            AddFieldToDataSet(txtBehCode.Text);
        }

        private void btnBehCode_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "tran_setid,tran_nm", "tran_nm;Transaction Name,code;Transaction Code", "Please Select", "Tran_type in ('Transaction','Accounting') and code!='TS' and code!='OM'", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtTransNm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["tran_setid"].ToString();
            DisplayControlsonMode("view_mode");
        }

        private void txtTransType_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtTransType.Text != "")
                {
                    DataSet dsetview = new DataSet();
                    dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where code='" + txtTransType.Text + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

                    if (dsetview != null && dsetview.Tables.Count != 0 && dsetview.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Sorry!! Transaction Code Already Exist", "Validation", 3000);
                        e.Cancel = true;
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Enter Transaction Code", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void btnRefPickUp_Click(object sender, EventArgs e)
        {
            frmListOfFields objfrmListMenus = new frmListOfFields();
            objfrmListMenus.Validity_fld_nm = "ref_tran_fld";
            objfrmListMenus.Filter_code = txtRefTranNm1.Text;//txtRefCode.Text;
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            //txtRefPickUp.Text = objBASEFILEDS.HTMAIN["ref_tran_fld"].ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void txtBehCode_TextChanged(object sender, EventArgs e)
        {
            string tblcode = "";
            tblcode = txtBehCode.Text;
            if (txtBehCode.Text == "")
            {
                tblcode = txtTransType.Text;
            }
            //DataSet dsetSearchFlds = objDBAdaper.dsquery("select distinct fld_nm from ibasefields where code='" + tblcode + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            //if (dsetSearchFlds != null && dsetSearchFlds.Tables.Count != 0 && dsetSearchFlds.Tables[0].Rows.Count != 0)
            //{
            //    chklstSearch.Items.Clear();
            //    var Searchitems = chklstSearch.Items;
            //    foreach (DataRow row in dsetSearchFlds.Tables[0].Rows)
            //    {
            //        Searchitems.Add(row["fld_nm"].ToString());
            //    }
            //}
        }

        private void btnRefTranNm_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "ref_tran_nm";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtRefTranNm1.Text = objBASEFILEDS.HTMAIN["ref_tran_nm"].ToString();
        }

        private void txtTransNm_Validating(object sender, CancelEventArgs e)
        {
            if (txtTransNm.Text == "" && objBASEFILEDS.Tran_mode != "view_mode")
            {
                AutoClosingMessageBox.Show("Please Enter Valid Transaction Name", "Validation", 3000);
                e.Cancel = true;
            }
        }

        private void chkProdSet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProdSet.Checked)
            {
                foreach (Control c in tabControl1.TabPages["tabPage8"].Controls)
                {
                    if (!(c is Label)) c.Enabled = false;
                }
            }
            else
            {
                foreach (Control c in tabControl1.TabPages["tabPage8"].Controls)
                {
                    c.Enabled = true;
                }
            }
        }

        private void btnConstructLvl_Click(object sender, EventArgs e)
        {
            dgvApprove.AutoGenerateColumns = false;
            while (dgvApprove.Rows.Count > 0)
            {
                if (!dgvApprove.Rows[0].IsNewRow)
                {
                    dgvApprove.Rows.RemoveAt(0);
                }
            }

            DataSet dsetApprove = objFLTransaction.GetApproveSetting(txtTransType.Text, objBASEFILEDS.ObjCompany.Compid.ToString());
            if (dsetApprove != null && dsetApprove.Tables.Count != 0 && dsetApprove.Tables[0].Rows.Count != 0)
            {

            }
            else
            {
                for (int i = 0; i < int.Parse(txtlevelno.Text); i++)
                {
                    dgvApprove.Rows.Add();
                    dgvApprove.Rows[i].Cells["si_no"].Value = (i + 1).ToString();
                    dgvApprove.Rows[i].Cells["level_nm"].Value = "Level " + (i + 1).ToString();
                }
                if (objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && objBASEFILEDS.HASHTABLES.HashItemtbl.Count != 0)
                {
                    objBASEFILEDS.HASHTABLES.HashItemtbl.Clear();
                }
                else
                {
                    objBASEFILEDS.HASHTABLES = new CUSTOM_iMANTRA_BL.BLHT();
                }
                foreach (DataGridViewRow row in dgvApprove.Rows)
                {
                    if (!objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(row.Cells["si_no"].Value.ToString()))
                    {
                        objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataGridViewColumn col in dgvApprove.Columns)
                        {
                            ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row.Cells["si_no"].Value.ToString()])[col.Name] = row.Cells[col.Name].Value;
                        }
                    }
                }
            }
        }

        private void BindApproveGrid()
        {
            ClearApproveGrid();
            DataSet dsetApprove = objFLTransaction.GetApproveSetting(txtTransType.Text, objBASEFILEDS.ObjCompany.Compid.ToString());
            if (dsetApprove != null && dsetApprove.Tables.Count != 0 && dsetApprove.Tables[0].Rows.Count != 0)
            {
                int i = 0;
                if (!(objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && objBASEFILEDS.HASHTABLES.HashItemtbl.Count != 0))
                {
                    objBASEFILEDS.HASHTABLES = new CUSTOM_iMANTRA_BL.BLHT();
                }
                else
                {
                    objBASEFILEDS.HASHTABLES.HashItemtbl.Clear();
                }
                foreach (DataRow row in dsetApprove.Tables[0].Rows)
                {
                    dgvApprove.Rows.Add();
                    if (!objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(row["si_no"].ToString()))
                    {
                        objBASEFILEDS.HASHTABLES.HashItemtbl[row["si_no"].ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn col in dsetApprove.Tables[0].Columns)
                        {
                            if (dgvApprove.Columns.Contains(col.ColumnName))
                            {
                                if (col.ColumnName != "condition")
                                {
                                    dgvApprove.Rows[i].Cells[col.ColumnName].Value = row[col.ColumnName];
                                }
                                ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[row["si_no"].ToString()])[col.ColumnName] = row[col.ColumnName].ToString();
                            }
                        }
                    }
                    i++;
                    txtlevelno.Text = row["level_cnt"].ToString();
                    chkCondition.Checked = bool.Parse(row["main_cond_req"] != null && row["main_cond_req"].ToString() != "" ? row["main_cond_req"].ToString() : "False");
                    //chkReqAuthority.Checked = bool.Parse(row["isReqAuthority"] != null && row["isReqAuthority"].ToString() != "" ? row["isReqAuthority"].ToString() : "False");
                }
            }
            else
            {

            }
        }

        private void ClearApproveGrid()
        {
            dgvApprove.AutoGenerateColumns = false;
            while (dgvApprove.Rows.Count > 0)
            {
                if (!dgvApprove.Rows[0].IsNewRow)
                {
                    dgvApprove.Rows.RemoveAt(0);
                }
            }
            if (!(objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && objBASEFILEDS.HASHTABLES.HashItemtbl.Count != 0))
            {
                objBASEFILEDS.HASHTABLES = new CUSTOM_iMANTRA_BL.BLHT();
            }
            else
            {
                objBASEFILEDS.HASHTABLES.HashItemtbl.Clear();
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            // int i = 1;
            DialogResult result = MessageBox.Show("Are you sure to want Add Levels?", "Add Levels", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                txtlevelno.Text = (int.Parse(txtlevelno.Text) + 1).ToString();
                dgvApprove.Rows.Add();
                dgvApprove.Rows[dgvApprove.Rows.Count - 1].Cells["si_no"].Value = (dgvApprove.Rows.Count).ToString();
                dgvApprove.Rows[dgvApprove.Rows.Count - 1].Cells["level_nm"].Value = "Level " + (dgvApprove.Rows.Count).ToString();

                if (objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null && !objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(dgvApprove.Rows.Count.ToString()))
                {
                    objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.Rows.Count.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DataGridViewColumn col in dgvApprove.Columns)
                    {
                        ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.Rows.Count.ToString()])[col.Name] = dgvApprove.Rows[dgvApprove.Rows.Count - 1].Cells[col.Name].Value;
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Hashtable HTApproveTemp = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            DialogResult result = MessageBox.Show("Are you sure to want Remove Levels?", "Remove Levels", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                txtlevelno.Text = (int.Parse(txtlevelno.Text) - 1).ToString();
                if (objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null)
                {
                    objBASEFILEDS.HASHTABLES.HashItemtbl.Remove(dgvApprove.CurrentRow.Index);
                }
                dgvApprove.Rows.RemoveAt(dgvApprove.CurrentRow.Index);
            }

            if (objBASEFILEDS.HASHTABLES != null && objBASEFILEDS.HASHTABLES.HashItemtbl != null) //&& !objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(dgvApprove.Rows.Count.ToString()))
            {
                foreach (DictionaryEntry entry in objBASEFILEDS.HASHTABLES.HashItemtbl)
                {
                    if (!HTApproveTemp.Contains(entry.Key))
                    {
                        HTApproveTemp[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataGridViewColumn col in dgvApprove.Columns)
                        {
                            ((Hashtable)HTApproveTemp[entry.Key])[col.Name] = ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[entry.Key])[col.Name];
                        }
                    }
                }
                objBASEFILEDS.HASHTABLES.HashItemtbl.Clear();
            }

            int i = 1;
            foreach (DataGridViewRow row in dgvApprove.Rows)
            {
                if (!objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(i.ToString()))
                {
                    objBASEFILEDS.HASHTABLES.HashItemtbl[i.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                }
                if (i < int.Parse(row.Cells["si_no"].Value.ToString()))
                {
                    foreach (DictionaryEntry entry in HTApproveTemp)
                    {
                        if (entry.Key.ToString() == row.Cells["si_no"].Value.ToString())
                        {
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                if (entry1.Key.ToString() == "si_no")
                                {
                                    ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[(i).ToString()])[entry1.Key] = (i).ToString();
                                }
                                else if (entry1.Key.ToString() == "level_nm")
                                {
                                    ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[(i).ToString()])[entry1.Key] = "Level " + (i).ToString();
                                }
                                else
                                {
                                    if (objBASEFILEDS.HASHTABLES.HashItemtbl.Contains((i).ToString()))
                                    {
                                        ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[(i).ToString()])[entry1.Key] = entry1.Value;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    row.Cells["si_no"].Value = (i).ToString();
                    row.Cells["level_nm"].Value = "Level " + (i).ToString();
                    //((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[i])["si_no"] = (i).ToString();
                    //((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[i])["level_nm"] = "Level " + (i).ToString();

                }
                else
                {
                    foreach (DictionaryEntry entry in HTApproveTemp)
                    {
                        if (entry.Key.ToString() == i.ToString())
                        {
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                if (objBASEFILEDS.HASHTABLES.HashItemtbl.Contains(entry.Key))
                                {
                                    ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[entry.Key])[entry1.Key] = entry1.Value;
                                }
                            }
                            break;
                        }
                    }
                }
                i++;
            }
        }

        private void dgvApprove_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvApprove_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewButtonCell btn = dgvApprove.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                if (btn != null && btn.OwningColumn.Name == "condition")
                {
                    frmsyntax objfrmSyntax = new frmsyntax(btn.OwningColumn.Name, ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.CurrentRow.Cells["si_no"].Value.ToString()]));
                    objfrmSyntax.ObjBLFD = objBASEFILEDS;
                    objfrmSyntax.ShowDialog();
                    ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.CurrentRow.Cells["si_no"].Value.ToString()])[btn.OwningColumn.Name] = objfrmSyntax.HtLocal[btn.OwningColumn.Name].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvApprove_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView gridview = (DataGridView)sender;
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                    // txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                    txt.KeyDown -= new KeyEventHandler(txt_key_down);
                    txt.KeyDown += new KeyEventHandler(txt_key_down);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt != null && e.KeyData == Keys.F2 && txt.Name == "user_nm")
                {
                    objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                    frmListOfUsers objfrmListMenus = new frmListOfUsers(((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.CurrentRow.Cells["si_no"].Value.ToString()]));
                    objfrmListMenus.Fld_nm = txt.Name;
                    objfrmListMenus.ObjBFD = objBASEFILEDS;
                    objfrmListMenus.ShowDialog();
                    txt.Text = ((Hashtable)objBASEFILEDS.HASHTABLES.HashItemtbl[dgvApprove.CurrentRow.Cells["si_no"].Value.ToString()])[txt.Name].ToString().Trim();
                    objIni.SetKeyFieldValue("SQL", "initial catalog", objBASEFILEDS.ObjCompany.Db_nm);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            string tabName = "  " + tabControl1.TabPages[e.Index].Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            //tabControl1.Font = new System.Drawing.Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objControlSet.Font_family : "Courier New",float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? ObjControlSet.Font_size : "9"));
            tabControl1.Font = new Font(objBASEFILEDS.ObjControlSet.Tab_family != null ? objBASEFILEDS.ObjControlSet.Tab_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Tab_size != null ? objBASEFILEDS.ObjControlSet.Tab_size : "9"));
            Font font = new Font(tabControl1.Font, FontStyle.Bold);
            SolidBrush brushhead = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon);
            SolidBrush brush = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color) : Color.Black);

            //Find if it is selected, this one will be hightlighted...
            if (e.Index == tabControl1.SelectedIndex)
                e.Graphics.FillRectangle(brushhead, e.Bounds);

            e.Graphics.DrawString(tabName, font, brush, tabControl1.GetTabRect(e.Index), stringFormat);
        }

        private void EnableApprovePage()
        {
            if (!(chkApproval.Checked ? true : false))
            {
                DialogResult result = MessageBox.Show("Are you sure to Unapprove?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    txtlevelno.Text = "";
                    chkApproval.Checked = false;
                    ClearApproveGrid();
                }
                else
                {
                    chkApproval.Checked = true;
                }
            }
            foreach (Control c in tabPage7.Controls)
            {
                c.Enabled = !(chkApproval.Checked ? true : false);
            }
        }
        private void chkApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                EnableApprovePage();
            }
        }

        private void btnLocate_Click(object sender, EventArgs e)
        {
            string tblcode = "";
            tblcode = txtBehCode.Text;
            if (txtBehCode.Text == "")
            {
                tblcode = txtTransType.Text;
            }
            frmListOfFields objfrmListMenus = new frmListOfFields();
            objfrmListMenus.Validity_fld_nm = "disp_locate";
            objfrmListMenus.Filter_code = tblcode;
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtDispLocate.Text = objBASEFILEDS.HTMAIN["disp_locate"].ToString();
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (txtTransType.Text == "") { AutoClosingMessageBox.Show("Please enter Valid Transaction Code", "Validation", 3000); if (txtTransType.CanFocus) { txtTransType.Focus(); } }
            else if (txtTransNm.Text == "") { AutoClosingMessageBox.Show("Please enter Transaction Name", "Validation", 3000); if (txtTransNm.CanFocus) { txtTransNm.Focus(); } }
            else if (txtBehCode.Text == "") { AutoClosingMessageBox.Show("Please enter Transaction Behavier Code", "Validation", 3000); if (txtBehCode.CanFocus) { txtBehCode.Focus(); } }
        }

        private void txtBehCode_Validating(object sender, CancelEventArgs e)
        {
            string tblcode = "";
            tblcode = txtBehCode.Text;
            if (txtBehCode.Text == "")
            {
                tblcode = txtTransType.Text;
            }
            if (objBASEFILEDS.Tran_mode == "add_mode")
            {
                DataSet dsetSearchFlds = objDBAdaper.dsquery("select distinct * from ibasefields where code='" + txtBehCode.Text + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                if (dsetSearchFlds != null && dsetSearchFlds.Tables.Count != 0 && dsetSearchFlds.Tables[0].Rows.Count != 0)
                {
                    AddFieldToDataSet(txtBehCode.Text);
                    InsertFieldValueToHashTable();
                    GetFieldValueFromHashTable();
                    BindApproveGrid();
                    btnBehCode.Enabled = true;
                    // txtBehCode.Enabled = false;                  
                    objBASEFILEDS.HTMAIN["Code"] = txtTransType.Text;
                    objBASEFILEDS.HTMAIN["Tran_nm"] = txtTransNm.Text;
                    objBASEFILEDS.HTMAIN["behavier_cd"] = txtBehCode.Text;
                }
            }
        }

        private void btnRefTran_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code;ref_type,behavier_cd;ref_behaiver_cd", "tran_nm;Transaction Name,code;Transaction Code", "Please Select", "Tran_type='Transaction' and code not in ('TS','OM')", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            //objpopup.ShowDialog();
            //txtRefCode.Text = objBASEFILEDS.HTMAIN["ref_type"].ToString();
            //txtRefBehCd.Text = objBASEFILEDS.HTMAIN["ref_behaiver_cd"].ToString();
            //DataSet dsetRef_tran = objDBAdaper.dsquery("select tran_nm from tran_set where code in ('" + txtRefCode.Text + "') and compid=" + objBASEFILEDS.ObjCompany.Compid);
            //if (dsetRef_tran != null && dsetRef_tran.Tables.Count != 0 && dsetRef_tran.Tables[0].Rows.Count != 0)
            //{
            //    txtRefTranNm1.Text = dsetRef_tran.Tables[0].Rows[0]["tran_nm"].ToString();
            //}

            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "ref_type";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtRefTranNm1.Text = objBASEFILEDS.HTMAIN["ref_type"].ToString();
            CallPickUpGrid();
        }

        private void btnPickUpFields_Click(object sender, EventArgs e)
        {
            //if (txtRefTranNm1.Text != "")
            //{
            //    frmListOfFields objfrmListMenus = new frmListOfFields();
            //    objfrmListMenus.Validity_fld_nm = "ref_tran_fld";
            //    objfrmListMenus.Filter_code = txtRefCode.Text;
            //    objfrmListMenus.ObjBFD = objBASEFILEDS;
            //    objfrmListMenus.ShowDialog();
            //    txtRefPickUp.Text = objBASEFILEDS.HTMAIN["ref_tran_fld"].ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Please Enter Referring transaction ");
            //}
        }

        private void btnAccpopup_Click(object sender, EventArgs e)
        {
            frmListOfFields objfrmListMenus = new frmListOfFields();
            objfrmListMenus.Validity_fld_nm = "Ac_pop_sel";
            objfrmListMenus.Filter_code = "CM";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtAccPopUp.Text = objBASEFILEDS.HTMAIN["Ac_pop_sel"].ToString();
        }

        private void btnProdPopup_Click(object sender, EventArgs e)
        {
            frmListOfFields objfrmListMenus = new frmListOfFields();
            objfrmListMenus.Validity_fld_nm = "Pt_pop_sel";
            objfrmListMenus.Filter_code = "PT";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtProdPopUp.Text = objBASEFILEDS.HTMAIN["Pt_pop_sel"].ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (tabControl1.SelectedTab == tabPage8)
                {
                    if (chkProdSet.Checked)
                    {
                        foreach (Control c in tabPage8.Controls)
                        {
                            c.Enabled = true;
                        }
                    }
                    else
                    {
                        foreach (Control c in tabPage8.Controls)
                        {
                            if (!(c is Label)) c.Enabled = false;
                        }
                    }
                }
                else if (tabControl1.SelectedTab == tabPage7)
                {
                    if (chkApproval.Checked)
                    {
                        foreach (Control c in tabPage7.Controls)
                        {
                            c.Enabled = true;
                        }
                    }
                    else
                    {
                        foreach (Control c in tabPage7.Controls)
                        {
                            if (!(c is Label)) c.Enabled = false;
                        }
                    }
                }
                else if (tabControl1.SelectedTab == tabPage9)
                {
                    if (chkAccPost.Checked)
                    {
                        foreach (Control c in tabPage9.Controls)
                        {
                            c.Enabled = true;
                        }
                    }
                    else
                    {
                        foreach (Control c in tabPage9.Controls)
                        {
                            if (!(c is Label)) c.Enabled = false;
                        }
                    }
                }
            }
        }

        private void frmTransactionSetting_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void btnDefAcc_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;def_acc_id,ac_nm;def_acc", "ac_nm;Account Name", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtDefAcc.Text = objBASEFILEDS.HTMAIN["def_acc"].ToString();
        }

        private void btnDefConsignee_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;def_consignee_id,ac_nm;def_consignee", "ac_nm;Consignee Name", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtDefConsignee.Text = objBASEFILEDS.HTMAIN["def_consignee"].ToString();
        }

        private void txtDefAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;def_acc_id,ac_nm;def_acc", "ac_nm;Account Name", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
                //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtDefAcc.Text = objBASEFILEDS.HTMAIN["def_acc"].ToString();
            }
        }

        private void txtDefConsignee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;def_consignee_id,ac_nm;def_consignee", "ac_nm;Consignee Name", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
                //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtDefConsignee.Text = objBASEFILEDS.HTMAIN["def_consignee"].ToString();
            }
        }

        private void chkFileAttach_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chkFileAttach.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck?", "File Attach/Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chkFileAttach.Checked = true;
                    }
                }
            }
        }

        private void chkAmendment_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chkAmendment.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck?", "Amendment", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chkAmendment.Checked = true;
                    }
                }
            }
        }

        private void txtRefTranNm1_KeyDown(object sender, KeyEventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "ref_type";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtRefTranNm1.Text = objBASEFILEDS.HTMAIN["ref_type"].ToString();
            CallPickUpGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPickupGrid(cmbTrans_cdRef.Text);
        }

        #region 2.0
        private void CallPickUpGrid()
        {
            List<string> lstPickUp = new List<string>();
            foreach (string str in txtRefTranNm1.Text.Split(','))
            {
                BindPickupGrid(str);
                lstPickUp.Add(str);
            }
            cmbTrans_cdRef.DataSource = lstPickUp;
            cmbTrans_cdRef.Update();
        }
        private void BindPickupGrid(string referring_cd)
        {
            objhashtable = objBASEFILEDS.HASHTABLES;

            AddPickup(referring_cd);
            while (dgvReference.Rows.Count > 0)
            {
                if (!dgvReference.Rows[0].IsNewRow)
                {
                    dgvReference.Rows.RemoveAt(0);
                }
            }
            if (objhashtable != null && objhashtable.HashGeneraltbl != null && objhashtable.HashGeneraltbl.Count != 0)
            {
                int j = 0;
                foreach (DictionaryEntry entry in objhashtable.HashGeneraltbl)
                {
                    if (((Hashtable)entry.Value).Count != 0 && entry.Key.ToString().Split(',')[0] == referring_cd)
                    {
                        dgvReference.Rows.Add(1);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvReference.Columns.Contains(entry1.Key.ToString()))
                            {
                                dgvReference.Rows[j].Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
                            }
                        }
                        j++;
                    }
                }
            }
            else
            {
                objhashtable = new BLHT();
            }
        }
        private void AddPickup(string referring_cd)
        {
            dgvReference.AutoGenerateColumns = false;

            DataSet dsetPickUp = objPickUp.Get_PickUpDetails(objBASEFILEDS, referring_cd, txtTransType.Text);
            if (referring_cd != "" && dsetPickUp != null && dsetPickUp.Tables.Count != 0 && dsetPickUp.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetPickUp.Tables[0].Rows)
                {
                    key = referring_cd + "," + r["ref_id"].ToString();
                    if (objhashtable.HashGeneraltbl != null && !objhashtable.HashGeneraltbl.Contains(key))
                    {
                        objhashtable.HashGeneraltbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetPickUp.Tables[0].Columns)
                        {
                            ((Hashtable)objhashtable.HashGeneraltbl[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            else
            {
                if (referring_cd == "" && objhashtable != null && objhashtable.HashGeneraltbl != null)
                {
                    objhashtable.HashGeneraltbl.Clear();
                }
            }
            objBASEFILEDS.HASHTABLES = objhashtable;
        }
        private void dgvReference_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = cmbTrans_cdRef.Text + "," + dgvReference.CurrentRow.Cells["ref_id"].Value.ToString();
            dgvReference.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (objhashtable.HashGeneraltbl.Contains(key) && ((Hashtable)objhashtable.HashGeneraltbl[key]).Count != 0)
            {
                ((Hashtable)objhashtable.HashGeneraltbl[key])[dgvReference.CurrentCell.OwningColumn.Name] = dgvReference.CurrentRow.Cells[dgvReference.CurrentCell.OwningColumn.Name].Value;
            }
        }
        private void dgvReference_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
        #endregion 2.0

        private void chk_schedule_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chk_schedule.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck?", "Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chk_schedule.Checked = true;
                    }
                }
            }
        }

        private void chkDe_schedule_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chkDe_schedule.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck?", "De-Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chkDe_schedule.Checked = true;
                    }
                }
            }
        }

        private void chkSendMail_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chkSendMail.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck Mail Option?", "Send Mail", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chkSendMail.Checked = true;
                    }
                }
            }
        }

        private void chkRaiseApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (!(chkRaiseApproval.Checked ? true : false))
                {
                    DialogResult result = MessageBox.Show("Are you sure to UnCheck Mail Option?", "Transfer Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        chkRaiseApproval.Checked = true;
                    }
                }
            }
        }

        private void btnDrAc_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "AC_GRP_ID;dr_ac_id,AC_GRP_NM;dr_ac_nm", "PARENT_NM;Parent,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "IVW_ACCOUNTS", "", "primary_id;dr_ac_id,primary_nm;dr_ac_nm", "primary_nm; Account Name", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
        }

        private void btnCrAc_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "AC_GRP_ID;cr_ac_id,AC_GRP_NM;cr_ac_nm", "PARENT_NM;Parent,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "IVW_ACCOUNTS", "", "primary_id;cr_ac_id,primary_nm;cr_ac_nm", "primary_nm; Account Name", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
        }

        private void txtDrAcNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;dr_ac_id,ac_nm;dr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
            }
        }

        private void txtCrAcNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;cr_ac_id,ac_nm;cr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
            }
        }

        private void chkAccPost_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccPost.Checked)
            {
                foreach (Control c in tabControl1.TabPages["tabPage9"].Controls)
                {
                    if (!(c is Label)) c.Enabled = false;
                }
            }
            else
            {
                foreach (Control c in tabControl1.TabPages["tabPage9"].Controls)
                {
                    c.Enabled = true;
                }
            }
        }
    }
}
