using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iTRANSACTION
    {
        /*  Created by Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.26.13
        * This Class is used Transaction Level Methods Validations.
         * 1.0 Sharanamma Jekeen on 11.29.13 ==> Update Amendement details.
         * 
         * 
         * 
       * */
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        dblayer objdblayer = new dblayer();
        private iInit objinit = new iInit();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }
        public BL_BASEFIELD ACTIVE_TRANSACTION
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public bool ValidateBillNo()
        {
            if (ACTIVE_TRANSACTION.Code == "GR" || ACTIVE_TRANSACTION.Code == "PE" || ACTIVE_TRANSACTION.Code == "SC" || ACTIVE_TRANSACTION.Code == "PL")
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("BILL_NO") && ACTIVE_TRANSACTION.HTMAIN.Contains("AC_NM"))
                {
                    string strquery = "select count(*) cnt from " + ACTIVE_TRANSACTION.Main_tbl_nm + " where bill_no='" + ACTIVE_TRANSACTION.HTMAIN["BILL_NO"].ToString() + "' and ac_nm='" + ACTIVE_TRANSACTION.HTMAIN["AC_NM"].ToString().Replace("'","''") + "' and tran_cd='" + ACTIVE_TRANSACTION.Code + "' and tran_id!=" + ACTIVE_TRANSACTION.HTMAIN["TRAN_ID"].ToString();
                    DataSet ds = objdblayer.dsquery(strquery);
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        if (int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
                        {
                            if (ACTIVE_TRANSACTION.Code == "PL")
                            {
                                BL_FIELDS.Errormsg = "Cheque no is already exist";
                            }
                            else
                            {
                                BL_FIELDS.Errormsg = "Bill no is already exist";
                            }
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public bool ValidateBillDate()
        {
            if (ACTIVE_TRANSACTION.Code == "GR" || ACTIVE_TRANSACTION.Code == "PE" || ACTIVE_TRANSACTION.Code == "PE" || ACTIVE_TRANSACTION.Code == "SC")
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("BILL_DT") && ACTIVE_TRANSACTION.HTMAIN["BILL_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["BILL_DT"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["BILL_DT"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "Bill Date should be less than equal to Transaction Date";
                        return false;
                    }
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["BILL_DT"].ToString()) > DateTime.Now)
                    {
                        BL_FIELDS.Errormsg = "Bill Date should be less than equal to current date";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool ValidateCreditAvailDate()
        {
            if (ACTIVE_TRANSACTION.Code == "SC")
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("credit_tkn_dt") && ACTIVE_TRANSACTION.HTMAIN["credit_tkn_dt"] != null && ACTIVE_TRANSACTION.HTMAIN["credit_tkn_dt"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["credit_tkn_dt"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "Credit Avail Date should be less than equal to Date";
                        return false;
                    }
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["credit_tkn_dt"].ToString()) > DateTime.Now)
                    {
                        BL_FIELDS.Errormsg = "Credit Avail Date should be less than equal to current date";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool CopyConsigneeName()
        {
            if (ACTIVE_TRANSACTION.HTMAIN.Contains("AC_NM") && ACTIVE_TRANSACTION.HTMAIN.Contains("CONS_NM"))
            {
                if (ACTIVE_TRANSACTION.HTMAIN["CONS_NM"] != null && ACTIVE_TRANSACTION.HTMAIN["CONS_NM"].ToString() == "")
                {
                    ACTIVE_TRANSACTION.HTMAIN["CONS_NM"] = ACTIVE_TRANSACTION.HTMAIN["AC_NM"];
                    ACTIVE_TRANSACTION.HTMAIN["CONS_ID"] = ACTIVE_TRANSACTION.HTMAIN["AC_ID"];
                }
            }
            return true;
        }
        public bool ValidateConsigneeName()
        {
            if (ACTIVE_TRANSACTION.HTMAIN.Contains("CONS_NM"))
            {
                DataSet ds = objdblayer.dsquery("select ac_nm,ac_id from cm_mast where tran_cd in (select reftbltran_cd from ibasefields where code='" + ACTIVE_TRANSACTION.Code + "' and fld_nm='CONS_NM') and ac_nm='" + ACTIVE_TRANSACTION.HTMAIN["CONS_NM"].ToString() + "'");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ACTIVE_TRANSACTION.HTMAIN.Contains("CONS_ID"))
                    {
                        ACTIVE_TRANSACTION.HTMAIN["CONS_ID"] = ds.Tables[0].Rows[0]["ac_id"].ToString();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool OpeningStockDateChecking()
        {
            if (ACTIVE_TRANSACTION.Code == "OS")
            {
                DateTime os_dt = DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString());
                DateTime fin_yr_dt = DateTime.Parse(ACTIVE_TRANSACTION.ObjCompany.Fin_yr_sta.ToString());
                int result = DateTime.Compare(os_dt, fin_yr_dt);
                if (result != 0)
                    return false;
            }
            return true;
        }

        public bool ValidateServiceCreditAmount()
        {
            if (objBLFD.HTMAIN.Contains("BILL_AMT") && (objBLFD.HTMAIN["BILL_AMT"] == null || objBLFD.HTMAIN["BILL_AMT"].ToString() == ""))
            {
                objBLFD.HTMAIN["BILL_AMT"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("ser_tax_amt") && (objBLFD.HTMAIN["ser_tax_amt"] == null || objBLFD.HTMAIN["ser_tax_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["ser_tax_amt"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("ser_cess_amt") && (objBLFD.HTMAIN["ser_cess_amt"] == null || objBLFD.HTMAIN["ser_cess_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["ser_cess_amt"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("ser_hcess_amt") && (objBLFD.HTMAIN["ser_hcess_amt"] == null || objBLFD.HTMAIN["ser_hcess_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["ser_hcess_amt"] = "0.00";
            }
            if (decimal.Parse(objBLFD.HTMAIN["BILL_AMT"].ToString()) <= 0)
            {
                BL_FIELDS.Errormsg = "Please enter Bill/Invoice Amount";
                return false;
            }
            if (decimal.Parse(objBLFD.HTMAIN["BILL_AMT"].ToString()) < decimal.Parse(objBLFD.HTMAIN["ser_tax_amt"].ToString()))
            {
                BL_FIELDS.Errormsg = "Service Tax Amount should be less than Bill Amount.";
                return false;
            }
            if (decimal.Parse(objBLFD.HTMAIN["ser_tax_amt"].ToString()) < decimal.Parse(objBLFD.HTMAIN["ser_cess_amt"].ToString()))
            {
                BL_FIELDS.Errormsg = "Education Cess Amount should be less than Service Tax Amount.";
                return false;
            }
            if (decimal.Parse(objBLFD.HTMAIN["ser_cess_amt"].ToString()) < decimal.Parse(objBLFD.HTMAIN["ser_hcess_amt"].ToString()))
            {
                BL_FIELDS.Errormsg = "Secondary Cess Amount should be less than Education Cess Amount.";
                return false;
            }
            if (ACTIVE_TRANSACTION.HTMAIN.Contains("ser_tax_amt") && ACTIVE_TRANSACTION.HTMAIN["ser_tax_amt"] != null && ACTIVE_TRANSACTION.HTMAIN["ser_tax_amt"].ToString() != "" && decimal.Parse(ACTIVE_TRANSACTION.HTMAIN["ser_tax_amt"].ToString()) > 0)
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("ser_cess_amt"))
                {
                    ACTIVE_TRANSACTION.HTMAIN["ser_cess_amt"] = decimal.Parse(ACTIVE_TRANSACTION.HTMAIN["ser_tax_amt"].ToString()) * 2 / 100;
                }
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("ser_hcess_amt"))
                {
                    ACTIVE_TRANSACTION.HTMAIN["ser_hcess_amt"] = decimal.Parse(ACTIVE_TRANSACTION.HTMAIN["ser_tax_amt"].ToString()) * 1 / 100;
                }
            }
            return true;
        }
        public bool ValidatePLACreditAmount()
        {
            if (objBLFD.HTMAIN.Contains("pla_excise_amt") && (objBLFD.HTMAIN["pla_excise_amt"] == null || objBLFD.HTMAIN["pla_excise_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["pla_excise_amt"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("pla_cess_amt") && (objBLFD.HTMAIN["pla_cess_amt"] == null || objBLFD.HTMAIN["pla_cess_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["pla_cess_amt"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("pla_hcess_amt") && (objBLFD.HTMAIN["pla_hcess_amt"] == null || objBLFD.HTMAIN["pla_hcess_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["pla_hcess_amt"] = "0.00";
            }
            if (objBLFD.HTMAIN.Contains("net_amt") && (objBLFD.HTMAIN["net_amt"] == null || objBLFD.HTMAIN["net_amt"].ToString() == ""))
            {
                objBLFD.HTMAIN["net_amt"] = "0.00";
            }
            objBLFD.HTMAIN["net_amt"] = decimal.Parse(objBLFD.HTMAIN["pla_excise_amt"].ToString()) + decimal.Parse(objBLFD.HTMAIN["pla_cess_amt"].ToString()) + decimal.Parse(objBLFD.HTMAIN["pla_hcess_amt"].ToString());
            return true;
        }
        public bool ValidateProductMain()
        {
            DataSet dsname = objdblayer.dsquery("select prod_cd,prod_nm,prod_desc,uom from pt_mast where prod_nm='" + ACTIVE_TRANSACTION.HTMAIN["PROD_NM"].ToString().Replace("'","''") + "'");
            if (dsname != null && dsname.Tables[0].Rows.Count != 0)
            {
                if (ACTIVE_TRANSACTION.HTMAIN.ContainsKey("prod_cd"))
                {
                    ACTIVE_TRANSACTION.HTMAIN["prod_cd"] = dsname.Tables[0].Rows[0]["prod_cd"].ToString();
                }
                if (ACTIVE_TRANSACTION.HTMAIN["FG_QTY"].ToString() == "" || ACTIVE_TRANSACTION.HTMAIN["FG_QTY"] == null || ACTIVE_TRANSACTION.HTMAIN["FG_QTY"].ToString() == "0.00")
                {
                    ACTIVE_TRANSACTION.HTMAIN["FG_QTY"] = "1.00";
                }
                if (ACTIVE_TRANSACTION.HTMAIN.ContainsKey("uom") && ACTIVE_TRANSACTION.Code == "BO")
                {
                    ACTIVE_TRANSACTION.HTMAIN["uom"] = dsname.Tables[0].Rows[0]["uom"].ToString();
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool ValidatePrepDateAndTime()
        {
          if (ACTIVE_TRANSACTION.HTMAIN.Contains("PREP_DT") && ACTIVE_TRANSACTION.HTMAIN.Contains("REMOV_DT") && ACTIVE_TRANSACTION.HTMAIN["PREP_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["REMOV_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["PREP_DT"].ToString() != "" && ACTIVE_TRANSACTION.HTMAIN["REMOV_DT"].ToString() != "")
            {
                if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["REMOV_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()))
                {
                    BL_FIELDS.Errormsg = "Removal Date should be greater than Transaction Date";
                    return false;
                }
                else
                {
                    TimeSpan ts = DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["REMOV_DT"].ToString()).Subtract(DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["PREP_DT"].ToString()));
                    if (ts.Days < 0)
                    {
                        BL_FIELDS.Errormsg = "Removing Date Should be greater than Preparation Date";
                        return false;
                    }
                    else if (ts.Days == 0)
                    {
                        if (ACTIVE_TRANSACTION.HTMAIN.Contains("TIME_P") && ACTIVE_TRANSACTION.HTMAIN.Contains("TIME_R") && ACTIVE_TRANSACTION.HTMAIN["TIME_P"] != null && ACTIVE_TRANSACTION.HTMAIN["TIME_R"] != null && ACTIVE_TRANSACTION.HTMAIN["TIME_P"].ToString() != "" && ACTIVE_TRANSACTION.HTMAIN["TIME_R"].ToString() != "")
                        {
                            TimeSpan ts1 = DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TIME_R"].ToString()).Subtract(DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TIME_P"].ToString()));
                            if (ts1.Seconds < 0)
                            {
                                BL_FIELDS.Errormsg = "Removing Time Should be greater than Preparation Time";
                                return false;
                            }
                            else if (ts1.Minutes < 0)
                            {
                                BL_FIELDS.Errormsg = "Removing Time Should be greater than Preparation Time";
                                return false;
                            }
                            else if (ts1.Hours < 0)
                            {
                                BL_FIELDS.Errormsg = "Removing Time Should be greater than Preparation Time";
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public bool ValidateDate()
        {
            if (ACTIVE_TRANSACTION.Code == "SE")
            {
                if (ACTIVE_TRANSACTION.HTMAIN["DUE_DAYS"].ToString() != "0.00" || ACTIVE_TRANSACTION.HTMAIN["DUE_DAYS"].ToString() != "0")
                {
                    objinit.DisableField("DUE_DT", 1);
                    DateTime tran_dt = DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString());
                    Double due_days = Double.Parse(ACTIVE_TRANSACTION.HTMAIN["DUE_DAYS"].ToString());
                    ACTIVE_TRANSACTION.HTMAIN["DUE_DT"] = tran_dt.AddDays(due_days);
                }
            }
            if (ACTIVE_TRANSACTION.Code == "GR")
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("PO_DT") && ACTIVE_TRANSACTION.HTMAIN["PO_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["PO_DT"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["PO_DT"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "Po Date should be less Transaction Date";
                        return false;
                    }
                }
                if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()).Date < DateTime.Now.Date)
                {
                    BL_FIELDS.Errormsg = "Transaction Date should be less than equal to current date";
                    return false;
                }
            }
            if (ACTIVE_TRANSACTION.Code == "SO")
            {
                if (ACTIVE_TRANSACTION.HTMAIN.Contains("AMD_DT") && ACTIVE_TRANSACTION.HTMAIN["AMD_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["AMD_DT"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["AMD_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "Amendment Date should be greater than Transaction Date";
                        return false;
                    }
                }               
            }
            if (ACTIVE_TRANSACTION.Code == "CT")
            {
                 if (ACTIVE_TRANSACTION.HTMAIN.Contains("CT_DT") && ACTIVE_TRANSACTION.HTMAIN["CT_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["CT_DT"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["CT_DT"].ToString()) > DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "CT Date should be less than Transaction Date";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool ValidatePrepDate()
        {
            if (ACTIVE_TRANSACTION.HTMAIN.Contains("PREP_DT") && ACTIVE_TRANSACTION.HTMAIN.Contains("TRAN_DT") && ACTIVE_TRANSACTION.HTMAIN["PREP_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"] != null && ACTIVE_TRANSACTION.HTMAIN["PREP_DT"].ToString() != "" && ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString() != "")
            {
                if (DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["PREP_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTMAIN["TRAN_DT"].ToString()))
                {
                    BL_FIELDS.Errormsg = "Preparation Date should be greater than Transaction Date";
                    return false;
                }
            }
            return true;
        }
        public bool ValidateDelDate()
        {
            if (ACTIVE_TRANSACTION.Code == "SO")
            {
                if (ACTIVE_TRANSACTION.HTITEM_VALUE.Contains("DEL_DT") && ACTIVE_TRANSACTION.HTITEM_VALUE["DEL_DT"] != null && ACTIVE_TRANSACTION.HTITEM_VALUE["DEL_DT"].ToString() != "")
                {
                    if (DateTime.Parse(ACTIVE_TRANSACTION.HTITEM_VALUE["DEL_DT"].ToString()) < DateTime.Parse(ACTIVE_TRANSACTION.HTITEM_VALUE["TRAN_DT"].ToString()))
                    {
                        BL_FIELDS.Errormsg = "Delivery Date should be greater than Transaction Date";
                        return false;
                    }
                }
            }
            return true;
        }

        #region 1.0
        public bool ValidateAmedementDetails()
        {
            if (objBLFD.IsAmendment && objBLFD.Tran_mode != "view_mode")
            {
                if (objBLFD.HTMAIN["IM_AMDREQ"] != null && objBLFD.HTMAIN["IM_AMDREQ"].ToString() != "")
                {
                    if (bool.Parse(objBLFD.HTMAIN["IM_AMDREQ"].ToString()))
                    {
                        objinit.EnableField("IM_AMDNO", 0);
                        objinit.EnableField("IM_AMDDT", 0);
                    }
                    else
                    {
                        objinit.DisableField("IM_AMDNO", 0);
                        objinit.DisableField("IM_AMDDT", 0);
                    }
                }
                else
                {
                    objinit.DisableField("IM_AMDNO", 0);
                    objinit.DisableField("IM_AMDDT", 0);
                }
            }
            return true;
        }
        public bool ValidateAmedementDate()
        {
            if (objBLFD.IsAmendment)
            {
                if (objBLFD.HTMAIN["IM_AMDDT"] != null && objBLFD.HTMAIN["IM_AMDDT"].ToString() != "")
                {
                    if (Convert.ToDateTime(objBLFD.HTMAIN["IM_AMDDT"].ToString()).ToString("yy/mm/dd") != Convert.ToDateTime("1900/01/01").ToString("yy/mm/dd"))
                    {
                        if (Convert.ToDateTime(objBLFD.HTMAIN["tran_dt"].ToString()) > Convert.ToDateTime(objBLFD.HTMAIN["IM_AMDDT"].ToString()))
                        {
                            BL_FIELDS.Errormsg = "Amedement Date should be greater or equal to Transaction Date";
                            return false;
                        }
                        //else if (DateTime.Now > Convert.ToDateTime(objBLFD.HTMAIN["I_AMDDT"].ToString()))
                        //{
                        //    BL_FIELDS.Errormsg = "Amedement Date should be greater or equal to Today's Date";
                        //    return false;
                        //}
                    }
                }
            }
            return true;
        }
        #endregion 1.0
    }
}
