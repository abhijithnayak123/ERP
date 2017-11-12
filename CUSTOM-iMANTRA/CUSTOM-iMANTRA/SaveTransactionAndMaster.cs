using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class SaveTransactionAndMaster
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.08.13 12.19PM
         * 1.0 find type is transaction/master/custommaster & Get Order No & Column Order No. for Valid List in Custom Fields Master.
         * 2.0 Get Order No & Column Order No. for Valid List in Custom Fields Master. --Method
         * 3.0 Sharanamma Jekeen on 11.29.13 ==> Update Amendement details.
         * 4.0 Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 12.03.13 ==>Multiple PickUp 
         * 5.0 Navyashri.A Inode Technologies Pvt. Ltd. on 07.12.13 ==>Show Field On PickUp 
         * 6.0 Navyashri.A Inode Technologies Pvt. Ltd. on 15.02.14 ==>Copy Eventsubtype name to another field to help in pickup 
       * */
        BLHT objhashtables = new BLHT();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        Hashtable _htcustom = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        dblayer objdblayer = new dblayer();
        private int _local_order_no = 1, _local_col_order_no = 1;

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public bool ValidateSave()
        {
            BL_FIELDS.SaveDetailsOrNOtFromDefault = true;
            if (objBLFD.HTMAIN != null)
            {
                if (objBLFD.HTMAIN.Contains("CUSER_NM") && objBLFD.Tran_mode == "add_mode")
                    objBLFD.HTMAIN["CUSER_NM"] = objBLFD.ObjLoginUser.CurUser;
                if (objBLFD.HTMAIN.Contains("CUSER_DT"))
                    objBLFD.HTMAIN["CUSER_DT"] = objBLFD.HTMAIN["CUSER_DT"] != null && objBLFD.HTMAIN["CUSER_DT"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["CUSER_DT"].ToString()).ToString("yyyy/MM/dd") : DateTime.Now.ToString("yyyy/MM/dd");
                if (objBLFD.HTMAIN.Contains("MUSER_NM") && objBLFD.Tran_mode == "edit_mode")
                    objBLFD.HTMAIN["MUSER_NM"] = objBLFD.ObjLoginUser.CurUser;
                if (objBLFD.HTMAIN.Contains("MUSER_DT"))
                    objBLFD.HTMAIN["MUSER_DT"] = objBLFD.HTMAIN["MUSER_DT"] != null && objBLFD.HTMAIN["MUSER_DT"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["MUSER_DT"].ToString()).ToString("yyyy/MM/dd") : DateTime.Now.ToString("yyyy/MM/dd");
            }
            if (objBLFD.Ac_post && objBLFD.HT_ACDET != null && objBLFD.HT_ACDET.Count != 0)
            {
                decimal dramt = 0, cramt = 0;
                foreach (DictionaryEntry entry in objBLFD.HT_ACDET)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (((Hashtable)entry.Value)["acc_account_type"] != null && ((Hashtable)entry.Value)["acc_account_type"].ToString() == "CR")
                        {
                            cramt += decimal.Parse(((Hashtable)entry.Value)["acc_amount"] != null && ((Hashtable)entry.Value)["acc_amount"].ToString() != "" ? ((Hashtable)entry.Value)["acc_amount"].ToString().Replace(",", "") : "0.00");
                        }
                        if (((Hashtable)entry.Value)["acc_account_type"] != null && ((Hashtable)entry.Value)["acc_account_type"].ToString() == "DR")
                        {
                            dramt += decimal.Parse(((Hashtable)entry.Value)["acc_amount"] != null && ((Hashtable)entry.Value)["acc_amount"].ToString() != "" ? ((Hashtable)entry.Value)["acc_amount"].ToString().Replace(",", "") : "0.00");
                        }
                    }
                }
                if ((dramt - cramt) != 0)
                {
                    BL_FIELDS.Errormsg = " Amount Difference is " + (dramt - cramt);
                    return false;
                }
            }
            #region stock effect
            foreach (DictionaryEntry entry in ACTIVE_BL.HTITEM)
            {
                if (((Hashtable)entry.Value).Contains("STK_EFFECT"))
                {
                    ((Hashtable)entry.Value)["STK_EFFECT"] = ACTIVE_BL.Stk_effect;
                }
            }
            if (ACTIVE_BL.Stk_effect == "+")
            {
                if (!StockCheck() && ACTIVE_BL.Tran_mode == "edit_mode")
                    return false;
            }
            #endregion
            if (objBLFD.HTMAIN != null && objBLFD.HTMAIN.Contains("RULE"))
            {
                #region rule changes
                if (objBLFD.HTMAIN["RULE"].ToString().ToUpper() == "EXCISABLE")
                {
                    foreach (DictionaryEntry entry in ACTIVE_BL.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Contains("EXAMT"))
                        {
                            if (((Hashtable)entry.Value)["EXAMT"] == null || ((Hashtable)entry.Value)["EXAMT"].ToString() == "" || ((Hashtable)entry.Value)["EXAMT"].ToString() == "0" || ((Hashtable)entry.Value)["EXAMT"].ToString() == "0.00")
                            {
                                BL_FIELDS.Errormsg = "Basic Duty Amount should be greater than zero";
                                return false;
                            }
                        }
                        if (((Hashtable)entry.Value).Contains("CESS_AMT"))
                        {
                            if (((Hashtable)entry.Value)["CESS_AMT"] == null || ((Hashtable)entry.Value)["CESS_AMT"].ToString() == "" || ((Hashtable)entry.Value)["CESS_AMT"].ToString() == "0" || ((Hashtable)entry.Value)["CESS_AMT"].ToString() == "0.00")
                            {
                                BL_FIELDS.Errormsg = "Education Cess should be greater than zero";
                                return false;
                            }
                        }
                        if (((Hashtable)entry.Value).Contains("SHCESS_AMT"))
                        {
                            if (((Hashtable)entry.Value)["SHCESS_AMT"] == null || ((Hashtable)entry.Value)["SHCESS_AMT"].ToString() == "" || ((Hashtable)entry.Value)["SHCESS_AMT"].ToString() == "0" || ((Hashtable)entry.Value)["SHCESS_AMT"].ToString() == "0.00")
                            {
                                BL_FIELDS.Errormsg = "HS Cess Amount should be greater than zero";
                                return false;
                            }
                        }
                        //if (((Hashtable)entry.Value).Contains("CVD"))
                        //{
                        //    if (((Hashtable)entry.Value)["CVD"] == null || ((Hashtable)entry.Value)["CVD"].ToString() == "" || ((Hashtable)entry.Value)["CVD"].ToString() == "0" || ((Hashtable)entry.Value)["SHCESS_AMT"].ToString() == "0.00")
                        //    {
                        //        BL_FIELDS.Errormsg = "Additional Duty Amount should be greater than zero";
                        //        return false;
                        //    }
                        //}
                    }
                }
                else if (objBLFD.HTMAIN["RULE"].ToString().ToUpper() == "NON-EXCISABLE")
                {
                    foreach (DictionaryEntry entry in ACTIVE_BL.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Contains("EXAMT"))
                        {
                            ((Hashtable)entry.Value)["EXAMT"] = "0.00";
                        }
                        if (((Hashtable)entry.Value).Contains("CESS_AMT"))
                        {
                            ((Hashtable)entry.Value)["CESS_AMT"] = "0.00";
                        }
                        if (((Hashtable)entry.Value).Contains("SHCESS_AMT"))
                        {
                            ((Hashtable)entry.Value)["SHCESS_AMT"] = "0.00";
                        }
                        //if (((Hashtable)entry.Value).Contains("CVD"))
                        //{
                        //    ((Hashtable)entry.Value)["CVD"] = "0.00";
                        //}
                    }
                }
                #endregion rule changes
            }
            if (ACTIVE_BL.Code == "PL")
            {
                #region
                if (objBLFD.HTMAIN["net_amt"] == null || objBLFD.HTMAIN["net_amt"].ToString() == "" || decimal.Parse(objBLFD.HTMAIN["net_amt"].ToString()) == 0)
                {
                    BL_FIELDS.Errormsg = "Please enter PLA Amount";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "LR")
            {
                #region
                BLHT objhashtables = new BLHT();
                objhashtables = ACTIVE_BL.HASHTABLES;
                if (objhashtables == null || objhashtables.HashIssueAndReceipt == null || objhashtables.HashIssueAndReceipt.Count == 0)
                {
                    BL_FIELDS.Errormsg = "Please Select Labour issue againest you are getting this product";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "SE" || ACTIVE_BL.Code == "EI")
            {
                #region
                if (ACTIVE_BL.HTMAIN.Contains("BOND_AMT"))
                {
                    if (ACTIVE_BL.HTMAIN["BOND_AMT"] == null || ACTIVE_BL.HTMAIN["BOND_AMT"].ToString() == "")
                    {
                        ACTIVE_BL.HTMAIN["BOND_AMT"] = "0.00";
                    }
                }
                foreach (DictionaryEntry entry in ACTIVE_BL.HTITEM)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (((Hashtable)entry.Value).Contains("net_wt"))
                        {
                            if (((Hashtable)entry.Value)["net_wt"] == null || ((Hashtable)entry.Value)["net_wt"].ToString() == "")
                            {
                                ((Hashtable)entry.Value)["net_wt"] = "0.00";
                            }
                        }
                        if (((Hashtable)entry.Value).Contains("gro_wt"))
                        {
                            if (((Hashtable)entry.Value)["gro_wt"] == null || ((Hashtable)entry.Value)["gro_wt"].ToString() == "")
                            {
                                ((Hashtable)entry.Value)["gro_wt"] = "0.00";
                            }
                        }
                    }
                }
                if (ACTIVE_BL.HTMAIN["CTDESC"].ToString() == "CT-3")
                {
                    if (ACTIVE_BL.HTMAIN["CT_NO"] != null || ACTIVE_BL.HTMAIN["CT_NO"].ToString() != "")
                    {
                        DataSet ds = objdblayer.dsquery(" Select SEMAIN.CT_NO se_ctno, SOMAIN.ct_no so_ctno from SEMAIN inner join SOMAIN on(SEMAIN.CT_NO= SOMAIN.ct_no) where SEMAIN.CT_NO='" + ACTIVE_BL.HTMAIN["CT_NO"] + "' ");
                        if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            bool flg = true;
                            foreach (DictionaryEntry entry in ACTIVE_BL.HTITEM)
                            {
                                if (((Hashtable)entry.Value).Contains("prod_nm"))
                                {
                                    DataSet ds1 = objdblayer.dsquery("select prod_cd,prod_nm,qty from SOITEM where ct_no='" + ACTIVE_BL.HTMAIN["CT_NO"] + "'");
                                    if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                                    {
                                        foreach (DataRow ds1_row in ds1.Tables[0].Rows)
                                        {

                                            if (((Hashtable)entry.Value)["prod_nm"].ToString() == ds1_row["prod_nm"].ToString())
                                            {
                                                flg = true;
                                                DataSet ds2 = objdblayer.dsquery("select tran_id,prod_nm,ISNULL(SUM(qty),0) tot_qty from SEITEM where ct_no='" + ACTIVE_BL.HTMAIN["CT_NO"] + "' and tran_id !='" + ACTIVE_BL.Tran_id + "' and prod_nm='" + ((Hashtable)entry.Value)["PROD_NM"].ToString().Replace("'", "''") + "' Group By prod_nm,tran_id ");
                                                DataSet ds3 = objdblayer.dsquery("select prod_nm,ISNULL(SUM(qty),0) tot_qty from SOITEM where ct_no='" + ACTIVE_BL.HTMAIN["CT_NO"] + "' and prod_nm='" + ds1_row["PROD_NM"].ToString().Replace("'", "''") + "' group by prod_nm");
                                                if (ds3 != null && ds3.Tables.Count != 0 && ds3.Tables[0].Rows.Count != 0)
                                                {
                                                    decimal qty = 0;
                                                    if (ds2 != null && ds2.Tables.Count != 0 && ds2.Tables[0].Rows.Count != 0)
                                                    {
                                                        qty = decimal.Parse(ds2.Tables[0].Rows[0]["tot_qty"].ToString()) + decimal.Parse(((Hashtable)entry.Value)["qty"].ToString());
                                                    }
                                                    else
                                                    {
                                                        qty = decimal.Parse(((Hashtable)entry.Value)["qty"].ToString());
                                                    }
                                                    if (qty <= decimal.Parse(ds3.Tables[0].Rows[0]["tot_qty"].ToString()))
                                                    {
                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        BL_FIELDS.Errormsg = ((Hashtable)entry.Value)["PROD_NM"].ToString().Replace("'", "''") + " exceeds qty ";
                                                        return false;
                                                    }

                                                }
                                                break;
                                            }
                                            else
                                            {
                                                flg = false;
                                            }
                                        }
                                        if (!flg)
                                        {
                                            BL_FIELDS.Errormsg = ((Hashtable)entry.Value)["PROD_NM"].ToString().Replace("'", "''") + " doesn't belong to CT No" + ACTIVE_BL.HTMAIN["CT_NO"];
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "PE")
            {
                #region
                if (objBLFD.HTMAIN.Contains("pur_credit_per"))
                {
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Contains("prod_ty_nm"))
                        {
                            if (((Hashtable)entry.Value)["prod_ty_nm"] != null && ((Hashtable)entry.Value)["prod_ty_nm"].ToString() != "" && ((Hashtable)entry.Value)["prod_ty_nm"].ToString() == "MACHINERY/STORES")
                            {
                                objBLFD.HTMAIN["pur_credit_per"] = objBLFD.ObjCompany.Pur_credit_per == null ? "" : objBLFD.ObjCompany.Pur_credit_per;
                                break;
                            }
                        }
                    }
                }
                foreach (DictionaryEntry entry in objBLFD.HTITEM)
                {
                    if (((Hashtable)entry.Value).Contains("_ispick"))
                    {
                        ((Hashtable)entry.Value)["_ispick"] = false;
                        foreach (DictionaryEntry entry1 in objBLFD.HTMAINREF)
                        {
                            if (((Hashtable)entry.Value)["ptserial"].ToString() == ((Hashtable)entry1.Value)["ptserial"].ToString())
                            {
                                ((Hashtable)entry.Value)["_ispick"] = true;
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "EM")
            {
                #region
                string[] strValidIn;
                string strValidate = "";
                string width = "0", precision = "0", strOriginal_code = objBLFD.HTMAIN["code"].ToString();
                string[] len = objBLFD.HTMAIN["valid_mast"].ToString().Split(',');
                if (len.Length > 1)
                {
                    strValidIn = new string[len.Length + 1];
                }
                else if (len[len.Length - 1] != null && len[len.Length - 1].Trim() != "")
                {
                    strValidIn = new string[len.Length + 1];
                }
                else
                {
                    strValidIn = new string[1];
                }

                int i = 0;
                foreach (string str in objBLFD.HTMAIN["valid_mast"].ToString().Split(','))
                {
                    if (str.Trim() != "")
                    {
                        strValidIn[i] = str;
                        if (strValidate != "")
                        {
                            strValidate += "," + "'" + str + "'";
                        }
                        else
                        {
                            strValidate = "'" + str + "'";
                        }
                        i++;
                    }
                }
                strValidIn[strValidIn.Length - 1] = objBLFD.HTMAIN["code"].ToString();
                if (strValidate != "")
                {
                    strValidate += "," + "'" + objBLFD.HTMAIN["code"].ToString() + "'";
                }
                else
                {
                    strValidate = "'" + objBLFD.HTMAIN["code"].ToString() + "'";
                }
                if (strValidate == "''" || strValidate == "")
                {
                    DataSet dsetcode = objdblayer.dsquery("select distinct code from tran_set where tran_nm='" + ACTIVE_BL.HTMAIN["tran_nm"].ToString() + "'");
                    if (dsetcode != null && dsetcode.Tables.Count != 0 && dsetcode.Tables[0].Rows.Count != 0)//fld exists
                    {
                        strValidate = "'" + dsetcode.Tables[0].Rows[0]["code"].ToString() + "'";
                        strOriginal_code = dsetcode.Tables[0].Rows[0]["code"].ToString();
                        strValidIn[strValidIn.Length - 1] = strOriginal_code;
                    }
                }
                Hashtable _htcond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                DataSet dsettbl_nm;
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,main_tbl_nm tbl_nm,tran_nm,Behavier_cd,Ref_behaiver_cd,CASE WHEN Ref_Type='' THEN Ref_behaiver_cd ELSE Ref_Type END Ref_Type from tran_set where code in (" + strValidate + ")");
                }
                else
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,item_tbl_nm tbl_nm,tran_nm,Behavier_cd,Ref_behaiver_cd,CASE WHEN Ref_Type='' THEN Ref_behaiver_cd ELSE Ref_Type END Ref_Type from tran_set where code in (" + strValidate + ")");
                }

                string _primary_value = "0", _UpdateFieldValue = "0";
                foreach (string str in strValidIn)
                {
                    _htcustom.Clear();
                    foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                    {
                        _htcustom[entry.Key] = entry.Value;
                    }
                    _primary_value = "0";
                    if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                    {
                        DataRow[] row = dsettbl_nm.Tables[0].Select("code='" + str + "'");
                        if (row != null && row.Length > 0 && row[0] != null)//transaction exist.
                        {
                            DataSet dset = objdblayer.dsquery("select * from icustomfields where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'");
                            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && strOriginal_code == str)//fld exists
                            {
                                BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                return false;
                            }
                            else//fld not existed
                            {
                                string strtblQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettbl = objdblayer.dsquery(strtblQuery);
                                if (dsettbl != null && dsettbl.Tables.Count != 0 && dsettbl.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    _primary_value = _htcustom[objBLFD.Primary_id] == null ? "0" : _htcustom[objBLFD.Primary_id].ToString();

                                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and DATA_TYPE = '" + _htcustom["data_ty"].ToString() + "' and ";
                                    switch (_htcustom["data_ty"].ToString().ToLower().Trim())
                                    {
                                        case "varchar": strQuery += " CHARACTER_MAXIMUM_LENGTH = " + _htcustom["_fld_width"].ToString() + " and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                        case "decimal": strQuery += " numeric_precision=" + _htcustom["_fld_width"].ToString() + "and numeric_scale=" + _htcustom["_fld_pre"].ToString() + " and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                        case "int":
                                        case "bit":
                                        case "datetime": strQuery += " TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                    }
                                    DataSet dset1 = objdblayer.dsquery(strQuery);
                                    if (!(dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0))//fld exists in specific table
                                    {
                                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (string str in strValidIn)
                {
                    _htcustom.Clear();
                    foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                    {
                        _htcustom[entry.Key] = entry.Value;
                    }
                    _primary_value = "0";
                    _UpdateFieldValue = "0";
                    if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                    {
                        DataRow[] row = dsettbl_nm.Tables[0].Select("code='" + str + "'");
                        if (row != null && row.Length > 0 && row[0] != null)//transaction exist.
                        {
                            DataSet dset = objdblayer.dsquery("select * from icustomfields where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'");
                            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && strOriginal_code == str)//fld exists
                            {
                                BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                return false;
                            }
                            else//fld not existed
                            {
                                string strtblQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettbl = objdblayer.dsquery(strtblQuery);
                                if (dsettbl != null && dsettbl.Tables.Count != 0 && dsettbl.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    _primary_value = _htcustom[objBLFD.Primary_id] == null ? "0" : _htcustom[objBLFD.Primary_id].ToString();

                                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and DATA_TYPE = '" + _htcustom["data_ty"].ToString() + "' and ";
                                    switch (_htcustom["data_ty"].ToString().ToLower().Trim())
                                    {
                                        case "varchar": strQuery += " CHARACTER_MAXIMUM_LENGTH = " + _htcustom["_fld_width"].ToString() + " and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                        case "decimal": strQuery += " numeric_precision=" + _htcustom["_fld_width"].ToString() + "and numeric_scale=" + _htcustom["_fld_pre"].ToString() + " and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                        case "int":
                                        case "bit":
                                        case "datetime": strQuery += " TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'"; break;
                                    }
                                    DataSet dset1 = objdblayer.dsquery(strQuery);
                                    if (!(dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0))//fld exists in specific table
                                    {
                                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                        return false;
                                    }
                                    else
                                    {
                                        DataSet dsetCustomid = objdblayer.dsquery("select custom_id from icustomfields where fld_nm='" + _htcustom["fld_nm"].ToString() + "' and code='" + str + "' ");
                                        if (dsetCustomid != null && dsetCustomid.Tables.Count != 0 && dsetCustomid.Tables[0].Rows.Count != 0)
                                        {
                                            _UpdateFieldValue = dsetCustomid.Tables[0].Rows[0]["custom_id"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    _primary_value = "0";
                                    _UpdateFieldValue = "0";
                                    string strQuery = "";
                                    strQuery = "alter table " + row[0]["tbl_nm"].ToString() + " add " + _htcustom["fld_nm"].ToString() + " " + _htcustom["data_ty"].ToString();
                                    switch (_htcustom["data_ty"].ToString().ToLower().Trim())
                                    {
                                        case "varchar": strQuery += " (" + _htcustom["_fld_width"].ToString() + ")"; break;
                                        case "decimal": strQuery += " (" + _htcustom["_fld_width"].ToString() + "," + _htcustom["_fld_pre"].ToString() + ")"; break;
                                        case "int":
                                        case "bit":
                                        case "datetime": strQuery += ""; break;
                                    }

                                    if (!objdblayer.execQuery(strQuery))
                                    {
                                        BL_FIELDS.Errormsg = "adding field to table " + row[0]["tbl_nm"].ToString() + " is not successful.";
                                        return false;
                                    }
                                    else
                                    {
                                        string strQuery1 = "";
                                        strQuery1 = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["fld_nm"].ToString() + "=";
                                        switch (_htcustom["data_ty"].ToString().ToLower().Trim())
                                        {
                                            case "varchar": strQuery1 += "'" + " " + "'"; break;
                                            case "decimal": strQuery1 += "'0.00'"; break;
                                            case "int": strQuery1 += "0"; break;
                                            case "bit": strQuery1 += "'0'"; break;
                                            case "datetime": strQuery1 += "null"; break;
                                        }
                                        objdblayer.execQuery(strQuery1);
                                    }
                                }
                            }
                            width = "0"; precision = "0";
                            switch (_htcustom["data_ty"].ToString().ToLower().Trim())
                            {
                                case "varchar": width = _htcustom["_fld_width"].ToString(); precision = "1"; break;
                                case "decimal": width = "17"; precision = "5"; break;
                                case "int": width = "10"; precision = "1"; break;
                                case "bit": width = "50"; precision = "1"; break;
                                case "datetime": width = "17"; precision = "5"; break;
                            }
                            _htcustom["fld_wid"] = width;
                            _htcustom["fld_desc"] = precision;
                            _htcustom["tran_nm"] = row[0]["tran_nm"].ToString();
                            if (_htcustom["tbl_nm"] == null || _htcustom["tbl_nm"].ToString() == "")
                            {
                                _htcustom["tbl_nm"] = row[0]["tbl_nm"].ToString();
                            }
                            _htcustom["code"] = str;
                            _htcustom["_isDeleteAllowed"] = true;
                            _htcustom["_top"] = bool.Parse(_htcustom["disp_head"].ToString());
                            // if (_htcustom[objBLFD.Primary_id] != null && _htcustom[objBLFD.Primary_id].ToString() != "" && int.Parse(_htcustom[objBLFD.Primary_id].ToString()) > 0)
                            if ((_primary_value != "0" && strOriginal_code == str) || (_primary_value == "0" && _UpdateFieldValue != "0"))
                            {
                                if (_UpdateFieldValue != "0")
                                {
                                    _htcond[objBLFD.Primary_id] = _UpdateFieldValue;
                                }
                                else
                                {
                                    _htcond[objBLFD.Primary_id] = _htcustom[objBLFD.Primary_id];
                                }
                                _htcustom.Remove(objBLFD.Primary_id);
                                objdblayer.execUpdate("iCUSTOMFIELDS", _htcustom, _htcond);
                            }
                            else if (_primary_value == "0")
                            {
                                if (strOriginal_code != str)
                                {
                                    _htcustom["valid_mast"] = "";
                                    #region 1.0
                                    int _type = 0;
                                    string _btn = "", _tab = "";
                                    if (_htcustom["_btntype"] != null && _htcustom["_btntype"].ToString() != "")
                                    {
                                        _type = 2;
                                        _btn = _htcustom["_btntype"].ToString();
                                    }
                                    if (_htcustom["_tab"] != null && _htcustom["_tab"].ToString() != "")
                                    {
                                        _type = 3;
                                        _tab = _htcustom["_tab"].ToString();
                                    }
                                    if (_htcustom["disp_head"] != null && _htcustom["disp_head"].ToString() != "")
                                    {
                                        if (_htcustom["disp_head"].ToString() == "0")
                                        {
                                            _htcustom["disp_head"] = false;
                                        }
                                        else if (_htcustom["disp_head"].ToString() == "1")
                                        {
                                            _htcustom["disp_head"] = true;
                                        }
                                        if (bool.Parse(_htcustom["disp_head"].ToString()))
                                        {
                                            _type = 1;
                                        }
                                    }
                                    GetColumnNumber(str, _type, bool.Parse(_htcustom["typewise"].ToString()), _btn, _tab);
                                    _htcustom["order_no"] = _local_order_no;
                                    _htcustom["col_order_no"] = _local_col_order_no;
                                    #endregion 1.0
                                }
                                _htcustom.Remove(objBLFD.Primary_id);
                                #region 5.0
                                if (strOriginal_code != str)
                                {
                                    _htcustom["disp_pickup"] = "False";
                                    _htcustom["disp_nw_pickup"] = "False";
                                }
                                objdblayer.execInsert("iCUSTOMFIELDS", _htcustom);
                            }

                            if (strOriginal_code == str)
                            {
                                string striReference = "select * from iREFERENCE where valid_mast='" + str + "' and fld_nm='" + _htcustom["fld_nm"].ToString() + "' and typewise='" + _htcustom["typewise"] + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettbliReference = objdblayer.dsquery(striReference);
                                bool copyordisp = false;
                                if (dsettbliReference != null && dsettbliReference.Tables.Count != 0 && dsettbliReference.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    if ((_htcustom["disp_pickup"] != null && _htcustom["disp_pickup"].ToString() != "" && !bool.Parse(_htcustom["disp_pickup"].ToString())) || (_htcustom["disp_nw_pickup"] != null && _htcustom["disp_nw_pickup"].ToString() != "" && !bool.Parse(_htcustom["disp_nw_pickup"].ToString())))
                                    {
                                        objdblayer.execDeleteQuery("delete from iREFERENCE where valid_mast='" + str + "' and fld_nm='" + _htcustom["fld_nm"].ToString() + "' and typewise='" + _htcustom["typewise"] + "'");//+ _tableNm[str].ToString() + "'";
                                    }
                                    else if ((_htcustom["disp_pickup"] != null && _htcustom["disp_pickup"].ToString() != "" && bool.Parse(_htcustom["disp_pickup"].ToString())) || (_htcustom["disp_nw_pickup"] != null && _htcustom["disp_nw_pickup"].ToString() != "" && bool.Parse(_htcustom["disp_nw_pickup"].ToString())))
                                    {
                                        if (bool.Parse(_htcustom["disp_pickup"].ToString()))
                                            copyordisp = true;
                                        objdblayer.execQuery("Update iREFERENCE set _copy ='" + copyordisp + "' where valid_mast='" + str + "' and fld_nm='" + _htcustom["fld_nm"].ToString() + "' and typewise='" + _htcustom["typewise"] + "'");
                                    }
                                }
                                else
                                {
                                    string order_no = "1";
                                    DataSet dsetReferenceTbl = objdblayer.dsquery("SELECT code,Behavier_cd,Ref_behaiver_cd,CASE WHEN Ref_Type='' THEN Ref_behaiver_cd ELSE Ref_Type END Ref_Type FROM dbo.TRAN_SET WHERE Ref_Type like ('%" + str + "%')");
                                    if (dsetReferenceTbl != null && dsetReferenceTbl.Tables.Count != 0 && dsetReferenceTbl.Tables[0].Rows.Count != 0)
                                    {
                                        DataSet dsetOrder = objdblayer.dsquery("select max(isnull(order_no,0))+1 order_no from iREFERENCE where tran_cd='" + str + "' and typewise='" + _htcustom["typewise"] + "'");
                                        if (dsetOrder != null && dsetOrder.Tables.Count != 0 && dsetOrder.Tables[0].Rows.Count != 0)
                                        {
                                            order_no = dsetOrder.Tables[0].Rows[0]["order_no"].ToString();
                                        }
                                        //if (_htcustom["disp_pickup"] != null && _htcustom["disp_pickup"].ToString() != "" && bool.Parse(_htcustom["disp_pickup"].ToString()))
                                        //{
                                        //    objdblayer.execQuery("INSERT INTO dbo.iREFERENCE(typewise,Tran_cd,beh_cd,ref_beh_cd,valid_mast,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,tbl_nm,[_copy],[_read],compid) VALUES('" + _htcustom["typewise"] + "','" + dsetReferenceTbl.Tables[0].Rows[0]["code"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Behavier_cd"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Ref_behaiver_cd"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Ref_Type"].ToString() + "','" + _htcustom["head_nm"] + "','" + order_no + "','1','" + _htcustom["fld_nm"] + "','" + _htcustom["data_ty"] + "','" + _htcustom["fld_wid"] + "','" + _htcustom["fld_desc"] + "','" + _htcustom["inter_val"] + "','" + _htcustom["tbl_nm"] + "','True','True','" + objBLFD.ObjCompany.Compid + "')");
                                        //}
                                        if ((_htcustom["disp_pickup"] != null && _htcustom["disp_pickup"].ToString() != "" && bool.Parse(_htcustom["disp_pickup"].ToString())) || (_htcustom["disp_nw_pickup"] != null && _htcustom["disp_nw_pickup"].ToString() != "" && bool.Parse(_htcustom["disp_nw_pickup"].ToString())))
                                        {
                                            if (bool.Parse(_htcustom["disp_pickup"].ToString()))
                                                copyordisp = true;
                                            else if (bool.Parse(_htcustom["disp_nw_pickup"].ToString()))
                                                copyordisp = false;
                                            objdblayer.execQuery("INSERT INTO dbo.iREFERENCE(typewise,Tran_cd,beh_cd,ref_beh_cd,valid_mast,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,tbl_nm,[_copy],[_read],compid) VALUES('" + _htcustom["typewise"] + "','" + dsetReferenceTbl.Tables[0].Rows[0]["code"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Behavier_cd"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Ref_behaiver_cd"].ToString() + "','" + dsetReferenceTbl.Tables[0].Rows[0]["Ref_Type"].ToString() + "','" + _htcustom["head_nm"] + "','" + order_no + "','1','" + _htcustom["fld_nm"] + "','" + _htcustom["data_ty"] + "','" + _htcustom["fld_wid"] + "','" + _htcustom["fld_desc"] + "','" + _htcustom["inter_val"] + "','" + _htcustom["tbl_nm"] + "','" + copyordisp + "','True','" + objBLFD.ObjCompany.Compid + "')");
                                        }
                                    }
                                #endregion 5.0
                                }
                            }
                            BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
                        }
                    }
                }
                foreach (DictionaryEntry entry in ACTIVE_BL.HASHTABLES.HashMaintbl)
                {
                    string tbl_nm = "", column_nm = "", primary_key = "0";
                    switch (entry.Key.ToString().Split(',')[1])
                    {
                        case "EM": tbl_nm = "icustomfields"; column_nm = "order_no"; primary_key = "custom_id"; break;
                        case "BM": tbl_nm = "ibasefields"; column_nm = "order_no"; primary_key = "baseid"; break;
                        case "DM": tbl_nm = "dc_mast"; column_nm = "corder"; primary_key = "dc_id"; break;
                    }
                    bool bResult = objdblayer.execQuery("Update " + tbl_nm + " set " + column_nm + "=" + entry.Value + " where tran_cd='" + entry.Key.ToString().Split(',')[1].ToString() + "' and " + primary_key + "=" + entry.Key.ToString().Split(',')[0].ToString());
                }
                foreach (DictionaryEntry entry in ACTIVE_BL.HASHTABLES.HashItemtbl)
                {
                    string tbl_nm = "", primary_key = "0";
                    switch (entry.Key.ToString().Split(',')[1])
                    {
                        case "EM": tbl_nm = "icustomfields"; primary_key = "custom_id"; break;
                        case "BM": tbl_nm = "ibasefields"; primary_key = "baseid"; break;
                        case "DM": tbl_nm = "dc_mast"; primary_key = "dc_id"; break;
                    }
                    bool bResult = objdblayer.execQuery("Update " + tbl_nm + " set fld_wid=" + entry.Value.ToString().Split(',')[0] + ",fld_desc=" + entry.Value.ToString().Split(',')[1] + " where tran_cd='" + entry.Key.ToString().Split(',')[1].ToString() + "' and " + primary_key + "=" + entry.Key.ToString().Split(',')[0].ToString());
                }
                AutoOrderUpdate();
                #endregion
            }
            if (ACTIVE_BL.Code == "DM")
            {
                #region
                string[] strValidIn;
                string strValidate = "", strOriginal_code = objBLFD.HTMAIN["code"].ToString();
                string[] len = objBLFD.HTMAIN["validity"].ToString().Split(',');
                if (len.Length > 1)
                {
                    strValidIn = new string[len.Length + 1];
                }
                else if (len[len.Length - 1] != null && len[len.Length - 1].Trim() != "")
                {
                    strValidIn = new string[len.Length + 1];
                }
                else
                {
                    strValidIn = new string[1];
                }

                int i = 0;
                foreach (string str in objBLFD.HTMAIN["validity"].ToString().Split(','))
                {
                    if (str.Trim() != "")
                    {
                        strValidIn[i] = str;
                        if (strValidate != "")
                        {
                            strValidate += "," + "'" + str + "'";
                        }
                        else
                        {
                            strValidate = "'" + str + "'";
                        }
                        i++;
                    }
                }
                strValidIn[strValidIn.Length - 1] = objBLFD.HTMAIN["code"].ToString();
                if (strValidate != "")
                {
                    strValidate += "," + "'" + objBLFD.HTMAIN["code"].ToString() + "'";
                }
                else
                {
                    strValidate = "'" + objBLFD.HTMAIN["code"].ToString() + "'";
                }
                Hashtable _htcond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                DataSet dsettbl_nm;
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,main_tbl_nm tbl_nm,tran_nm from tran_set where code in (" + strValidate + ")");
                }
                else
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,item_tbl_nm tbl_nm,tran_nm from tran_set where code in (" + strValidate + ")");
                }
                BL_FIELDS.Errormsg = "";
                string _primary_value = "0";
                foreach (string str in strValidIn)
                {
                    _htcustom.Clear();
                    foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                    {
                        _htcustom[entry.Key] = entry.Value;
                    }
                    _primary_value = "0";
                    if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                    {
                        DataRow[] row = dsettbl_nm.Tables[0].Select("code='" + str + "'");
                        if (row != null && row.Length > 0 && row[0] != null)//transaction exist.
                        {
                            DataSet dset = objdblayer.dsquery("select * from dc_mast where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'");
                            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && objBLFD.HTMAIN["code"].ToString() == str)//fld exists
                            {
                                BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                return false;
                            }
                            if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                            {
                                DataSet dsetper = objdblayer.dsquery("select * from dc_mast where pert_name='" + ACTIVE_BL.HTMAIN["pert_name"].ToString() + "' and code='" + str + "'");
                                if (dsetper != null && dsetper.Tables.Count != 0 && dsetper.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && objBLFD.HTMAIN["code"].ToString() == str)//fld exists
                                {
                                    BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                    return false;
                                }
                            }
                            if (BL_FIELDS.Errormsg.Length == 0)//fld not existed
                            {
                                string strtblQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettbl = objdblayer.dsquery(strtblQuery);
                                if (dsettbl != null && dsettbl.Tables.Count != 0 && dsettbl.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    _primary_value = _htcustom[objBLFD.Primary_id] != null ? _htcustom[objBLFD.Primary_id].ToString() : "0";
                                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and DATA_TYPE = 'decimal' and ";
                                    strQuery += " numeric_precision = 10 and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'";

                                    DataSet dset1 = objdblayer.dsquery(strQuery);
                                    if (dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0)//fld exists in specific table
                                    {
                                        BL_FIELDS.Errormsg = "";
                                    }
                                    else
                                    {
                                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                        return false;
                                    }
                                }
                            }
                            if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                            {
                                string strtblQueryperc = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["pert_name"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettblper = objdblayer.dsquery(strtblQueryperc);
                                if (dsettblper != null && dsettblper.Tables.Count != 0 && dsettblper.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["pert_name"].ToString() + "' and DATA_TYPE = 'decimal' and ";
                                    strQuery += " numeric_precision = 5 and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'";

                                    DataSet dset1per = objdblayer.dsquery(strQuery);
                                    if (dset1per != null && dset1per.Tables.Count != 0 && dset1per.Tables[0].Rows.Count != 0)//fld exists in specific table
                                    {
                                        BL_FIELDS.Errormsg = "";
                                    }
                                    else
                                    {
                                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                _primary_value = "0";
                string _UpdateFieldValue = "0";
                foreach (string str in strValidIn)
                {
                    _htcustom.Clear();
                    foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                    {
                        _htcustom[entry.Key] = entry.Value;
                    }
                    _primary_value = "0";
                    _UpdateFieldValue = "0";
                    if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                    {
                        DataRow[] row = dsettbl_nm.Tables[0].Select("code='" + str + "'");
                        if (row != null && row.Length > 0 && row[0] != null)//transaction exist.
                        {
                            DataSet dset = objdblayer.dsquery("select * from dc_mast where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'");
                            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && objBLFD.HTMAIN["code"].ToString() == str)//fld exists
                            {
                                BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                return false;
                            }
                            if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                            {
                                DataSet dsetper = objdblayer.dsquery("select * from dc_mast where pert_name='" + ACTIVE_BL.HTMAIN["pert_name"].ToString() + "' and code='" + str + "'");
                                if (dsetper != null && dsetper.Tables.Count != 0 && dsetper.Tables[0].Rows.Count != 0 && ACTIVE_BL.Tran_mode == "add_mode" && objBLFD.HTMAIN["code"].ToString() == str)//fld exists
                                {
                                    BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                    return false;
                                }
                            }
                            if (BL_FIELDS.Errormsg.Length == 0)//fld not existed
                            {
                                string strtblQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                DataSet dsettbl = objdblayer.dsquery(strtblQuery);
                                if (dsettbl != null && dsettbl.Tables.Count != 0 && dsettbl.Tables[0].Rows.Count != 0)//fld exists in specific table
                                {
                                    _primary_value = _htcustom[objBLFD.Primary_id] != null ? _htcustom[objBLFD.Primary_id].ToString() : "0";
                                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["fld_nm"].ToString() + "' and DATA_TYPE = 'decimal' and ";
                                    strQuery += " numeric_precision = 10 and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'";

                                    DataSet dset1 = objdblayer.dsquery(strQuery);
                                    if (dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0)//fld exists in specific table
                                    {
                                        DataSet dsetCustomid = objdblayer.dsquery("select dc_id from DC_MAST where fld_nm='" + _htcustom["fld_nm"].ToString() + "' and code='" + str + "' ");
                                        if (dsetCustomid != null && dsetCustomid.Tables.Count != 0 && dsetCustomid.Tables[0].Rows.Count != 0)
                                        {
                                            _UpdateFieldValue = dsetCustomid.Tables[0].Rows[0]["dc_id"].ToString();
                                        }
                                        BL_FIELDS.Errormsg = "";
                                    }
                                    else
                                    {
                                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    _primary_value = "0";
                                    _UpdateFieldValue = "0";
                                    string strQueryfld_nm = "alter table " + row[0]["tbl_nm"].ToString() + " add " + _htcustom["fld_nm"].ToString() + " decimal(10,2)";

                                    if (!objdblayer.execQuery(strQueryfld_nm))
                                    {
                                        BL_FIELDS.Errormsg = "adding field to table " + row[0]["tbl_nm"].ToString() + " is not successful.";
                                        return false;
                                    }
                                    else
                                    {
                                        if (!bool.Parse(_htcustom["typewise"].ToString()) && _htcustom["bef_aft"].ToString() == "2")
                                        {
                                            strQueryfld_nm = "alter table " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " add " + _htcustom["fld_nm"].ToString() + " decimal(10,2)";

                                            if (!objdblayer.execQuery(strQueryfld_nm))
                                            {
                                                BL_FIELDS.Errormsg = "adding field to table " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " is not successful.";
                                                return false;
                                            }

                                            string strQuery1 = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["fld_nm"].ToString() + "='0.00'";
                                            objdblayer.execQuery(strQuery1);
                                            if (!bool.Parse(_htcustom["typewise"].ToString()) && _htcustom["bef_aft"].ToString() == "2")
                                            {
                                                strQuery1 = "update " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " set " + _htcustom["fld_nm"].ToString() + "='0.00'";
                                                objdblayer.execQuery(strQuery1);
                                            }
                                        }
                                        else
                                        {
                                            if (bool.Parse(_htcustom["typewise"].ToString()))
                                            {
                                                string strQuery1perc = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["fld_nm"].ToString() + "='0.00'";
                                                objdblayer.execQuery(strQuery1perc);
                                            }
                                            else
                                            {
                                                string strQuery1 = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["fld_nm"].ToString() + "='0.00'";
                                                objdblayer.execQuery(strQuery1);
                                            }
                                        }
                                    }
                                }
                                if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                                {
                                    string strtblQueryperc = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["pert_name"].ToString() + "' and TABLE_NAME='" + row[0]["tbl_nm"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                                    DataSet dsettblper = objdblayer.dsquery(strtblQueryperc);
                                    if (dsettblper != null && dsettblper.Tables.Count != 0 && dsettblper.Tables[0].Rows.Count != 0)//fld exists in specific table
                                    {
                                        string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + _htcustom["pert_name"].ToString() + "' and DATA_TYPE = 'decimal' and ";
                                        strQuery += " numeric_precision = 5 and TABLE_NAME = '" + row[0]["tbl_nm"].ToString() + "'";

                                        DataSet dset1per = objdblayer.dsquery(strQuery);
                                        if (dset1per != null && dset1per.Tables.Count != 0 && dset1per.Tables[0].Rows.Count != 0)//fld exists in specific table
                                        {
                                            //DataSet dsetCustomid = objdblayer.dsquery("select dc_id from DC_MAST where fld_nm='" + _htcustom["fld_nm"].ToString() + "' and code='" + str + "' ");
                                            //if (dsetCustomid != null && dsetCustomid.Tables.Count != 0 && dsetCustomid.Tables[0].Rows.Count != 0)
                                            //{
                                            //    _UpdateFieldValue = dsetCustomid.Tables[0].Rows[0]["dc_id"].ToString();
                                            //}
                                            BL_FIELDS.Errormsg = "";
                                        }
                                        else
                                        {
                                            BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        string strQueryper = "alter table " + row[0]["tbl_nm"].ToString() + " add " + _htcustom["pert_name"].ToString() + " decimal(5,2)";

                                        if (!objdblayer.execQuery(strQueryper))
                                        {
                                            BL_FIELDS.Errormsg = "adding field to table " + row[0]["tbl_nm"].ToString() + " is not successful.";
                                            return false;
                                        }
                                        else
                                        {
                                            if (!bool.Parse(_htcustom["typewise"].ToString()) && _htcustom["bef_aft"].ToString() == "2")
                                            {
                                                strQueryper = "alter table " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " add " + _htcustom["pert_name"].ToString() + " decimal(5,2)";

                                                if (!objdblayer.execQuery(strQueryper))
                                                {
                                                    BL_FIELDS.Errormsg = "adding field to table " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " is not successful.";
                                                    return false;
                                                }

                                                string strQuery1perc = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["pert_name"].ToString() + "='0.00'";
                                                objdblayer.execQuery(strQuery1perc);
                                                if (!bool.Parse(_htcustom["typewise"].ToString()) && _htcustom["bef_aft"].ToString() == "2")
                                                {
                                                    strQuery1perc = "update " + row[0]["tbl_nm"].ToString().Replace("ITEM", "MAIN") + " set " + _htcustom["pert_name"].ToString() + "='0.00'";
                                                    objdblayer.execQuery(strQuery1perc);
                                                }
                                            }
                                            else
                                            {
                                                if (bool.Parse(_htcustom["typewise"].ToString()))
                                                {
                                                    string strQuery1perc = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["pert_name"].ToString() + "='0.00'";
                                                    objdblayer.execQuery(strQuery1perc);
                                                }
                                                else
                                                {
                                                    string strQuery1 = "update " + row[0]["tbl_nm"].ToString() + " set " + _htcustom["pert_name"].ToString() + "='0.00'";
                                                    objdblayer.execQuery(strQuery1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            _htcustom["fld_wid"] = "17";
                            _htcustom["fld_desc"] = "5";
                            _htcustom["tran_nm"] = row[0]["tran_nm"].ToString();
                            _htcustom["tbl_nm"] = row[0]["tbl_nm"].ToString();
                            _htcustom["code"] = str;
                            //_htcustom["_top"] = bool.Parse(_htcustom["disp_head"].ToString());
                            //if (_htcustom[objBLFD.Primary_id] != null && _htcustom[objBLFD.Primary_id].ToString() != "" && int.Parse(_htcustom[objBLFD.Primary_id].ToString()) > 0)
                            if ((_primary_value != "0" && strOriginal_code == str) || (_primary_value == "0" && _UpdateFieldValue != "0"))
                            {
                                if (_primary_value == "0" && _UpdateFieldValue != "0")
                                {
                                    _htcond[objBLFD.Primary_id] = _UpdateFieldValue;
                                }
                                else
                                {
                                    _htcond[objBLFD.Primary_id] = _htcustom[objBLFD.Primary_id];
                                }
                                _htcustom.Remove(objBLFD.Primary_id);
                                objdblayer.execUpdate("DC_MAST", _htcustom, _htcond);
                            }
                            else if (_primary_value == "0")
                            {
                                if (strOriginal_code != str) { _htcustom["validity"] = ""; }
                                _htcustom.Remove(objBLFD.Primary_id);
                                objdblayer.execInsert("DC_MAST", _htcustom);
                            }
                            BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
                        }
                    }
                }
                foreach (DictionaryEntry entry in ACTIVE_BL.HASHTABLES.HashMaintbl)
                {
                    string tbl_nm = "", column_nm = "", primary_key = "0";
                    switch (entry.Key.ToString().Split(',')[1])
                    {
                        case "EM": tbl_nm = "icustomfields"; column_nm = "order_no"; primary_key = "custom_id"; break;
                        case "BM": tbl_nm = "ibasefields"; column_nm = "order_no"; primary_key = "baseid"; break;
                        case "DM": tbl_nm = "dc_mast"; column_nm = "corder"; primary_key = "dc_id"; break;
                    }
                    bool bResult = objdblayer.execQuery("Update " + tbl_nm + " set " + column_nm + "=" + entry.Value + " where tran_cd='" + entry.Key.ToString().Split(',')[1].ToString() + "' and " + primary_key + "=" + entry.Key.ToString().Split(',')[0].ToString());
                }
                //AutoOrderUpdate();
                #endregion
            }
            if (ACTIVE_BL.Code == "ST")
            {
                #region
                Hashtable _htcond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable hash = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                {
                    hash[entry.Key] = entry.Value;
                }
                DataSet dsset = objdblayer.dsquery("select tran_nm,main_tbl_nm from tran_set where code ='" + ACTIVE_BL.HTMAIN["code"] + "'");
                if (dsset != null && dsset.Tables.Count != 0 && dsset.Tables[0].Rows.Count != 0)
                {
                    DataSet dsset1 = objdblayer.dsquery("select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + dsset.Tables[0].Rows[0]["main_tbl_nm"] + "' and COLUMN_NAME IN ('tax_nm','tax_amt')");
                    if (dsset1 != null && dsset1.Tables.Count != 0 && dsset1.Tables[0].Rows.Count != 0)
                    {
                        DataSet ds = objdblayer.dsquery("select tax_nm,code from st_mast where tax_nm ='" + ACTIVE_BL.HTMAIN["Tax_nm"] + "' and code='" + ACTIVE_BL.HTMAIN["code"] + "'");
                        if (ACTIVE_BL.Tran_mode == "edit_mode")
                        {
                            if (ds != null && ds.Tables[0].Rows.Count != 0)
                            {
                                //BL_FIELDS.Errormsg = "Tax Name for this transaction already exists";
                                //return false;
                            }
                        }
                    }
                    else
                    {
                        BL_FIELDS.Errormsg = "There is no Tax_nm,tax_amt field in " + dsset.Tables[0].Rows[0]["tran_nm"] + "";
                        return false;
                    }
                }
                if (objBLFD.HTMAIN["valid_tran"] != null || objBLFD.HTMAIN["valid_tran"].ToString() != "")
                {
                    foreach (string str in objBLFD.HTMAIN["valid_tran"].ToString().Split(','))
                    {
                        if (str.Trim() != "")
                        {
                            DataSet ds3 = objdblayer.dsquery("select tran_nm,main_tbl_nm from tran_set where code ='" + str + "'");
                            if (ds3 != null && ds3.Tables.Count != 0 && ds3.Tables[0].Rows.Count != 0)
                            {
                                DataSet ds4 = objdblayer.dsquery("select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + ds3.Tables[0].Rows[0]["main_tbl_nm"] + "' and COLUMN_NAME IN ('tax_nm','tax_amt')");
                                if (ds4 != null && ds4.Tables.Count != 0 && ds4.Tables[0].Rows.Count != 0)
                                {
                                    DataSet ds1 = objdblayer.dsquery("select tax_nm,code from st_mast where tax_nm ='" + hash["Tax_nm"] + "' and code='" + str + "'");
                                    if (ds1 != null && ds1.Tables[0].Rows.Count != 0)
                                    {
                                    }
                                    else
                                    {
                                        DataSet ds2 = objdblayer.dsquery("select tran_nm,code from tran_set where code ='" + str + "'");
                                        if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                        {
                                            hash["code"] = str;
                                            hash["tran_nm"] = ds2.Tables[0].Rows[0]["tran_nm"];
                                            hash.Remove(objBLFD.Primary_id);
                                            objdblayer.execInsert("ST_MAST", hash);
                                        }
                                    }
                                }
                                else
                                {
                                    BL_FIELDS.Errormsg = "There is no Tax_nm,tax_amt field in " + ds3.Tables[0].Rows[0]["tran_nm"] + "";
                                    return false;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "WO")
            {
                #region
                BL_FIELDS.Errormsg = "";
                BLHT objhashtables = ACTIVE_BL.HASHTABLES;
                if (objhashtables != null && objhashtables.HashMaintbl != null && objhashtables.HashMaintbl.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        if (!objhashtables.HashMaintbl.Contains(entry.Key))
                        {
                            BL_FIELDS.Errormsg = "Please Select BOM for " + entry.Key.ToString();
                            return false;
                        }
                    }
                }
                else
                {
                    BL_FIELDS.Errormsg = "Please Select BOM";
                    return false;
                }
                #endregion

            }
            if (ACTIVE_BL.Code == "TS")
            {
                #region
                if (ACTIVE_BL.Tran_mode == "add_mode")
                {
                    DataSet dsetBaseField = objdblayer.dsquery("select * from ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "'");
                    if (dsetBaseField != null && dsetBaseField.Tables.Count != 0 && dsetBaseField.Tables[0].Rows.Count != 0)
                    {
                        BL_FIELDS.Errormsg = "Sorry Transaction Code is already exist";
                        return false;
                    }
                }
                if (objBLFD.HTMAIN.Contains("isApprove") && objBLFD.HTMAIN["isApprove"] != null && objBLFD.HTMAIN["isApprove"].ToString() != "")
                {
                    #region Approve
                    if (bool.Parse(objBLFD.HTMAIN["isApprove"].ToString()))
                    {
                        bool itemflg = Save_Levels(objBLFD.Tran_id);
                        if (!itemflg)
                        {
                            BL_FIELDS.Errormsg = "Approve table Updation Failed";
                            return false;
                        }
                    }
                    else
                    {
                        DataSet dsetLevels = objdblayer.dsquery("select * from levels where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                        if (dsetLevels != null && dsetLevels.Tables.Count != 0 && dsetLevels.Tables[0].Rows.Count != 0)
                        {
                            objdblayer.execDeleteQuery("delete from levels where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                        }
                    }
                    #endregion
                }
                if (objBLFD.HTMAIN.Contains("isAmendment") && objBLFD.HTMAIN["isAmendment"] != null && objBLFD.HTMAIN["isAmendment"].ToString() != "")
                {
                    #region 3.0
                    DataSet dsettbl_nm;
                    dsettbl_nm = objdblayer.dsquery("select distinct code,main_tbl_nm tbl_nm,tran_nm,Behavier_cd,Ref_behaiver_cd,CASE WHEN Ref_Type='' THEN Ref_behaiver_cd ELSE Ref_Type END Ref_Type from tran_set where code in ('" + objBLFD.HTMAIN["code"].ToString() + "')");
                    if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                    {
                        if (bool.Parse(objBLFD.HTMAIN["isAmendment"].ToString()))
                        {
                            Save_Amendment(dsettbl_nm.Tables[0].Rows[0]["tbl_nm"].ToString());
                        }
                        else
                        {
                            string strQuery = "if exists(select * from ibasefields where code='" + objBLFD.HTMAIN["code"].ToString() + "' and fld_nm in ('AMD_BTN') and compid='" + objBLFD.ObjCompany.Compid + "') delete from ibasefields where code='" + objBLFD.HTMAIN["code"].ToString() + "' and fld_nm in ('AMD_BTN') and compid='" + objBLFD.ObjCompany.Compid + "'";

                            if (!objdblayer.execDeleteQuery(strQuery))
                            {
                                BL_FIELDS.Errormsg = "deleting field's from Base Table is not successful.";
                                return false;
                            }

                            strQuery = "if exists(select * from icustomfields where code='" + objBLFD.HTMAIN["code"].ToString() + "' and fld_nm in ('IM_AMDNO','IM_AMDDT','IM_AMDREQ') and compid='" + objBLFD.ObjCompany.Compid + "') delete from icustomfields where code='" + objBLFD.HTMAIN["code"].ToString() + "' and fld_nm in ('IM_AMDNO','IM_AMDDT','IM_AMDREQ') and compid='" + objBLFD.ObjCompany.Compid + "'";

                            if (!objdblayer.execDeleteQuery(strQuery))
                            {
                                BL_FIELDS.Errormsg = "deleting field's from Custom Table is not successful.";
                                return false;
                            }

                            DataSet dset = objdblayer.dsquery("select * from icustomfields where fld_nm=('IM_AMDREQ','IM_AMDNO','IM_AMDDT') and code!='" + objBLFD.HTMAIN["code"].ToString() + "' and tbl_nm='" + dsettbl_nm.Tables[0].Rows[0]["tbl_nm"].ToString() + "'");
                            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)//fld exists
                            {
                                //BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                            }
                            else//fld not existed
                            {
                                DataSet dsetExists = objdblayer.dsquery("select * from icustomfields where fld_nm=('IM_AMDREQ','IM_AMDNO','IM_AMDDT') and code='" + objBLFD.HTMAIN["code"].ToString() + "' and tbl_nm='" + dsettbl_nm.Tables[0].Rows[0]["tbl_nm"].ToString() + "'");
                                if (dsetExists != null && dsetExists.Tables.Count != 0 && dsetExists.Tables[0].Rows.Count != 0)//fld exists
                                {
                                    strQuery = "alter table " + dsettbl_nm.Tables[0].Rows[0]["tbl_nm"].ToString() + " drop column IM_AMDREQ,IM_AMDNO,IM_AMDDT";
                                    if (!objdblayer.execQuery(strQuery))
                                    {
                                        BL_FIELDS.Errormsg = "droping field's from table " + dsettbl_nm.Tables[0].Rows[0]["tbl_nm"].ToString() + " is not successful.";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion 3.0
                }
                if (objBLFD.HTMAIN.Contains("isSchedule") && objBLFD.HTMAIN["isSchedule"] != null && objBLFD.HTMAIN["isSchedule"].ToString() != "")
                {
                    #region schedule
                    if (Convert.ToBoolean(objBLFD.HTMAIN["isSchedule"].ToString()))
                    {
                        string strGetColNo = "if not exists(select * from ibasefields WHERE fld_nm='BTN_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "') select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 union all select distinct  isnull(max(corder),0), 1 from dc_mast where typewise=0 and code='" + objBLFD.HTMAIN["Code"].ToString() + "')vw";
                        DataSet dsetGetColNo = objdblayer.dsquery(strGetColNo);
                        if (dsetGetColNo != null && dsetGetColNo.Tables.Count != 0 && dsetGetColNo.Tables[0].Rows.Count != 0)//fld exists
                        {
                            string strQuery = "insert into ibasefields (code,[type],typewise,TRAN_CD,head_nm,order_no,col_order_no,fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre)";
                            string strQueryVal = "select code='" + objBLFD.HTMAIN["Code"].ToString() + "',[type],typewise,'BM',head_nm,order_no='" + dsetGetColNo.Tables[0].Rows[0]["order_no"].ToString() + "',col_order_no='" + dsetGetColNo.Tables[0].Rows[0]["col_order_no"].ToString() + "',fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre from iIMPORTBASEFIELDS where code='SD'";
                            objdblayer.execQuery(strQuery + strQueryVal);
                        }
                    }
                    else
                    {
                        objdblayer.execDeleteQuery("if exists(select * from ibasefields WHERE fld_nm='BTN_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "') delete from ibasefields WHERE fld_nm='BTN_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                    }
                    #endregion
                }
                if (objBLFD.HTMAIN.Contains("isDeSchedule") && objBLFD.HTMAIN["isDeSchedule"] != null && objBLFD.HTMAIN["isDeSchedule"].ToString() != "")
                {
                    #region de-schedule
                    if (Convert.ToBoolean(objBLFD.HTMAIN["isDeSchedule"].ToString()))
                    {
                        string strGetColNo = "if not exists(select * from ibasefields WHERE fld_nm='BTN_DE_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "') select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 union all select distinct  isnull(max(corder),0), 1 from dc_mast where typewise=0 and code='" + objBLFD.HTMAIN["Code"].ToString() + "')vw";
                        DataSet dsetGetColNo = objdblayer.dsquery(strGetColNo);
                        if (dsetGetColNo != null && dsetGetColNo.Tables.Count != 0 && dsetGetColNo.Tables[0].Rows.Count != 0)//fld exists
                        {
                            string strQuery = "insert into ibasefields (code,[type],typewise,TRAN_CD,head_nm,order_no,col_order_no,fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre)";
                            string strQueryVal = "select code='" + objBLFD.HTMAIN["Code"].ToString() + "',[type],typewise,'BM',head_nm,order_no='" + dsetGetColNo.Tables[0].Rows[0]["order_no"].ToString() + "',col_order_no='" + dsetGetColNo.Tables[0].Rows[0]["col_order_no"].ToString() + "',fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre from iIMPORTBASEFIELDS where code='DS'";
                            objdblayer.execQuery(strQuery + strQueryVal);
                        }
                    }
                    else
                    {
                        objdblayer.execDeleteQuery("if exists(select * from ibasefields WHERE fld_nm='BTN_DE_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "') delete from ibasefields WHERE fld_nm='BTN_DE_SCHEDULE' and code='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                    }
                    #endregion
                }
                if (objBLFD.HTMAIN.Contains("ref_type") && objBLFD.HTMAIN["ref_type"] != null)// && objBLFD.HTMAIN["ref_type"].ToString() != "")
                {
                    #region 4.0
                    string serialnos = "''";
                    DataSet dsetPickCheck = objdblayer.dsquery("select distinct valid_mast from iREFERENCE where tran_cd='" + objBLFD.HTMAIN["code"].ToString() + "' and  compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                    if (dsetPickCheck != null && dsetPickCheck.Tables.Count != 0 && dsetPickCheck.Tables[0].Rows.Count != 0 && objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashGeneraltbl != null)
                    {
                        foreach (DataRow row in dsetPickCheck.Tables[0].Rows)
                        {
                            if (objBLFD.HTMAIN["ref_type"].ToString().Contains(row["valid_mast"].ToString()))
                            {
                                if (serialnos == "''")
                                    serialnos = "'" + row["valid_mast"].ToString() + "'";
                                else
                                    serialnos += ",'" + row["valid_mast"].ToString() + "'";
                            }
                        }
                    }

                    objdblayer.execDeleteQuery("delete from iREFERENCE where valid_mast not in (" + serialnos + ") and tran_cd='" + objBLFD.HTMAIN["code"].ToString() + "' and  compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");

                    if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashGeneraltbl != null && objBLFD.HASHTABLES.HashGeneraltbl.Count != 0)
                    {
                        Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        bool bResult = false;

                        foreach (string str in objBLFD.HTMAIN["ref_type"].ToString().Split(','))
                        {
                            //update exist pickup reference details
                            DataSet dsetPickExists = objdblayer.dsquery("select * from iREFERENCE where tran_cd='" + objBLFD.HTMAIN["code"].ToString() + "' and valid_mast='" + str + "' and  compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                            if (dsetPickExists != null && dsetPickExists.Tables.Count != 0 && dsetPickExists.Tables[0].Rows.Count != 0)
                            {
                                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashGeneraltbl)
                                {
                                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                                    {
                                        htFields[entry1.Key] = entry1.Value;
                                    }
                                    if (htFields.Count != 0)
                                    {
                                        htFieldscond["ref_id"] = htFields["ref_id"];
                                        htFields.Remove("ref_id");
                                        bResult = objdblayer.execUpdate("iREFERENCE", htFields, htFieldscond);
                                    }
                                    if (bResult)
                                    {
                                        htFields.Clear();
                                        htFieldscond.Clear();
                                    }
                                }
                            }
                            else
                            {
                                //add new pickup reference
                                DataSet dsetPickBeh = objdblayer.dsquery("select behavier_cd from TRAN_SET where code='" + str + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                                if (dsetPickBeh != null && dsetPickBeh.Tables.Count != 0 && dsetPickBeh.Tables[0].Rows.Count != 0)
                                {
                                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashGeneraltbl)
                                    {
                                        if (entry.Key.ToString().Split(',')[0] == str)
                                        {
                                            foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                                            {
                                                htFields[entry1.Key] = entry1.Value;
                                            }
                                            if (htFields.Count != 0)
                                            {
                                                htFields["tran_cd"] = objBLFD.HTMAIN["code"].ToString();
                                                htFields["beh_cd"] = objBLFD.HTMAIN["behavier_cd"].ToString();
                                                htFields["ref_beh_cd"] = dsetPickBeh.Tables[0].Rows[0]["behavier_cd"].ToString();
                                                htFields["valid_mast"] = str;
                                                htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                                                // htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;

                                                htFields.Remove("ref_id");
                                                bResult = objdblayer.execInsert("iREFERENCE", htFields);
                                            }
                                            if (bResult)
                                            {
                                                htFields.Clear();
                                                htFieldscond.Clear();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion 3.0
                }
                else
                {
                    return true;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "AP")
            {
                #region Approve

                DataSet dsetlevels = new DataSet();
                DataSet dsetApproveData = new DataSet();
                DataSet dsetUnApprove = new DataSet();
                int i = 0;
                string strTran_no = "0";
                bool flg = true;


                string _tran_cd = "", _app_tbl_nm = "", _main_tbl_nm = "";
                DataSet dsetApproveTbl = new DataSet();
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashMaintbl != null)
                {
                    Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    bool bResult = false;

                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            if (_tran_cd != ((Hashtable)entry.Value)["tran_cd"].ToString())
                            {
                                dsetApproveTbl = objdblayer.dsquery("SELECT Approve_tbl_nm,Main_tbl_nm FROM dbo.TRAN_SET WHERE code='" + ((Hashtable)entry.Value)["tran_cd"].ToString() + "' AND CompId=" + objBLFD.ObjCompany.Compid);
                                if (dsetApproveTbl != null && dsetApproveTbl.Tables.Count != 0 && dsetApproveTbl.Tables[0].Rows.Count != 0)
                                {
                                    _app_tbl_nm = dsetApproveTbl.Tables[0].Rows[0]["Approve_tbl_nm"].ToString();
                                    _tran_cd = ((Hashtable)entry.Value)["tran_cd"].ToString();
                                    _main_tbl_nm = dsetApproveTbl.Tables[0].Rows[0]["Main_tbl_nm"].ToString();
                                }
                            }
                            if (_app_tbl_nm != "")
                            {
                                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                                {
                                    htFields.Add(entry1.Key, entry1.Value);
                                }
                                if (htFields.Count != 0)
                                {
                                    htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr.ToString();
                                    if (FindApproveExistance(htFields["tran_id"].ToString(), htFields["tran_cd"].ToString(), _app_tbl_nm))
                                    {
                                        htFieldscond["tran_id"] = htFields["tran_id"];
                                        htFieldscond["tran_cd"] = htFields["tran_cd"];
                                        htFields.Remove("tran_id");
                                        htFields.Remove("tran_cd");
                                        bResult = objdblayer.execUpdate(_app_tbl_nm, htFields, htFieldscond);

                                        DataSet dsetApprove1 = objdblayer.dsquery("SELECT * FROM " + _app_tbl_nm + " WHERE tran_id IN (SELECT tran_id FROM " + _main_tbl_nm + " WHERE tran_cd='" + _tran_cd + "' and i_approved=1 and tran_id='" + htFieldscond["tran_id"].ToString() + "') and tran_cd='" + _tran_cd + "'");
                                        if (dsetApprove1 != null && dsetApprove1.Tables.Count != 0 && dsetApprove1.Tables[0].Rows.Count != 0)
                                        {
                                            dsetlevels = objdblayer.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where code='" + _tran_cd + "' and compid=" + objBLFD.ObjCompany.Compid + " order by cast(si_no as int)");
                                            if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
                                            {
                                                if (bool.Parse(dsetlevels.Tables[0].Rows[0]["main_cond_req"].ToString()))
                                                {
                                                    foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                                                    {
                                                        if (objBLFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString().ToLower())
                                                        {
                                                            foreach (DataRow row1 in dsetApprove1.Tables[0].Rows)
                                                            {
                                                                if (row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() != "APPROVE")
                                                                {
                                                                    if (strTran_no == "")
                                                                    {
                                                                        strTran_no = "'" + row1["tran_id"].ToString() + "'";
                                                                    }
                                                                    else
                                                                    {
                                                                        strTran_no = strTran_no + ",'" + row1["tran_id"].ToString() + "'";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (DataRow row1 in dsetApprove1.Tables[0].Rows)
                                                    {
                                                        flg = true;
                                                        i = int.Parse(dsetlevels.Tables[0].Rows[0]["level_cnt"].ToString());
                                                        while (i > 0)
                                                        {
                                                            if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                            {
                                                                flg = false;
                                                                break;
                                                            }
                                                            i--;
                                                        }
                                                        if (!flg)
                                                        {
                                                            if (strTran_no == "")
                                                            {
                                                                strTran_no = "'" + row1["tran_id"].ToString() + "'";
                                                            }
                                                            else
                                                            {
                                                                strTran_no = strTran_no + ",'" + row1["tran_id"].ToString() + "'";
                                                            }
                                                        }
                                                    }
                                                }
                                                objdblayer.execQuery("update " + _main_tbl_nm + " set i_approved=0 where tran_id in (" + strTran_no + ") and tran_cd='" + _tran_cd + "'");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (DictionaryEntry entry1 in htFieldscond)
                                        {
                                            if (!htFields.ContainsKey(entry1.Key))
                                            {
                                                htFields.Add(entry1.Key, entry1.Value);
                                            }
                                        }
                                        bResult = objdblayer.execInsert(_app_tbl_nm, htFields);
                                    }
                                    if (bResult)
                                    {
                                        htFields.Clear();
                                        htFieldscond.Clear();
                                    }
                                    else
                                        return false;
                                }
                            }
                        }
                    }

                    //update i_approve field in main table

                    i = 0;
                    strTran_no = "0";
                    flg = true;
                    DataSet dsetApprove = objdblayer.dsquery("select tran_set.code,tran_nm,Main_tbl_nm,Approve_tbl_nm from tran_set inner join levels on tran_set.code=levels.code where isApprove=1 and tran_type='Transaction' and user_nm='" + objBLFD.ObjLoginUser.CurUser + "' and tran_set.compid=" + objBLFD.ObjCompany.Compid);
                    if (dsetApprove != null && dsetApprove.Tables.Count != 0 && dsetApprove.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow row in dsetApprove.Tables[0].Rows)
                        {
                            strTran_no = "0";
                            flg = true;

                            dsetlevels = objdblayer.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where code='" + row["code"].ToString() + "' and compid=" + objBLFD.ObjCompany.Compid + " order by cast(si_no as int)");
                            if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
                            {
                                dsetUnApprove = objdblayer.dsquery("SELECT * FROM " + row["Approve_tbl_nm"].ToString() + " WHERE tran_id IN (SELECT tran_id FROM " + row["Main_tbl_nm"].ToString() + " WHERE tran_cd='" + row["code"].ToString() + "' and i_approved=0) and tran_cd='" + row["code"].ToString() + "'");
                                if (dsetUnApprove != null && dsetUnApprove.Tables.Count != 0 && dsetUnApprove.Tables[0].Rows.Count != 0)
                                {
                                    if (bool.Parse(dsetlevels.Tables[0].Rows[0]["main_cond_req"].ToString()))
                                    {
                                        foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                                        {
                                            if (objBLFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString().ToLower())
                                            {
                                                foreach (DataRow row1 in dsetUnApprove.Tables[0].Rows)
                                                {
                                                    if (row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() == "APPROVE")
                                                    {
                                                        if (strTran_no == "")
                                                        {
                                                            strTran_no = "'" + row1["tran_id"].ToString() + "'";
                                                        }
                                                        else
                                                        {
                                                            strTran_no = strTran_no + ",'" + row1["tran_id"].ToString() + "'";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow row1 in dsetUnApprove.Tables[0].Rows)
                                        {
                                            flg = true;
                                            i = int.Parse(dsetlevels.Tables[0].Rows[0]["level_cnt"].ToString());
                                            while (i > 0)
                                            {
                                                if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                {
                                                    flg = false;
                                                    break;
                                                }
                                                i--;
                                            }
                                            if (flg)
                                            {
                                                if (strTran_no == "")
                                                {
                                                    strTran_no = "'" + row1["tran_id"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    strTran_no = strTran_no + ",'" + row1["tran_id"].ToString() + "'";
                                                }
                                            }
                                        }
                                    }
                                }
                                objdblayer.execQuery("update " + row["Main_tbl_nm"].ToString() + " set i_approved=1 where tran_id in (" + strTran_no + ") and tran_cd='" + row["code"].ToString() + "'");
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "TM")
            {
                #region
                BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
                return Save_Main_Trasaction(objBLFD, objBLFD.Main_tbl_nm).Length != 0 ? false : true;
                #endregion
            }
            if (ACTIVE_BL.Code == "PM" || ACTIVE_BL.Code == "QM" || ACTIVE_BL.Code == "CP" || ACTIVE_BL.Code == "VP" || ACTIVE_BL.Code == "JP")
            {
                #region
                BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
                return Save_Main_Trasaction(objBLFD, objBLFD.Main_tbl_nm).Length != 0 ? false : true;
                #endregion
            }
            if (ACTIVE_BL.Code == "RL")
            {
                BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
                string strMessage = Save_Main_Trasaction(objBLFD, "ROLES");
                if (strMessage.Length != 0)
                {
                    BL_FIELDS.Errormsg = strMessage;
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (ACTIVE_BL.IsSchedule || ACTIVE_BL.Code == "PH")
            {
                #region
                //BLHT objhashtables = new BLHT();
                //objhashtables = ACTIVE_BL.HASHTABLES;
                //if (objhashtables.HashMaintbl.Count == 0)
                //{
                //    BL_FIELDS.Errormsg = "Please Select Labour issue againest you are getting this product";
                //    return false;
                //}
                #endregion
            }
            // BL_FIELDS.SaveDetailsOrNOtFromDefault = false;
            return true;
        }

        #region 3.0
        private bool Save_Amendment(string tbl_nm)
        {
            DataSet dsetBase = objdblayer.dsquery("declare @baseflg int,@amd_no int,@amd_dt int,@amd_req int select @baseflg=case when count(*)>0 then 1 else 2 end from ibasefields where fld_nm='AMD_BTN' and code='" + objBLFD.HTMAIN["code"].ToString() + "' select @amd_no=case when count(*)>0 then 1 else  2 end from icustomfields where fld_nm='IM_AMDNO' and code='" + objBLFD.HTMAIN["code"].ToString() + "' select @amd_dt=case when count(*)>0 then 1 else  2 end from icustomfields where fld_nm='IM_AMDDT' and code='" + objBLFD.HTMAIN["code"].ToString() + "' select @amd_req=case when count(*)>0 then 1 else  2 end from icustomfields where fld_nm='IM_AMDREQ' and code='" + objBLFD.HTMAIN["code"].ToString() + "' select case when(@baseflg=@amd_no) then case when(@baseflg=@amd_dt) then case when(@baseflg=@amd_req) then case when(@baseflg=1) then 1 else 2 end else 0 end else 0 end else 0 end amdreq");
            if (dsetBase != null && dsetBase.Tables.Count != 0 && dsetBase.Tables[0].Rows.Count != 0)//fld exists
            {
                if (dsetBase.Tables[0].Rows[0]["amdreq"].ToString() == "0")
                {
                    BL_FIELDS.Errormsg = "Sorry Amendment Field's Already Exist in the Custom Field Master.";
                    return false;
                }
                else if (dsetBase.Tables[0].Rows[0]["amdreq"].ToString() == "2")
                {
                    string strGetColNo = "select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + objBLFD.HTMAIN["code"].ToString() + "' and typewise=1 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + objBLFD.HTMAIN["code"].ToString() + "' and typewise=1)vw";
                    DataSet dsetGetColNo = objdblayer.dsquery(strGetColNo);
                    if (dsetGetColNo != null && dsetGetColNo.Tables.Count != 0 && dsetGetColNo.Tables[0].Rows.Count != 0)//fld exists
                    {
                        string strQuery = "insert into ibasefields (code,[type],typewise,TRAN_CD,head_nm,order_no,col_order_no,fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre)";
                        string strQueryVal = "select code='" + objBLFD.HTMAIN["Code"].ToString() + "',[type],typewise,'BM',head_nm,order_no='" + dsetGetColNo.Tables[0].Rows[0]["order_no"].ToString() + "',col_order_no='" + dsetGetColNo.Tables[0].Rows[0]["col_order_no"].ToString() + "',fld_nm,data_ty,parent_nm,parent_text,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre from iIMPORTBASEFIELDS where code='AD'";
                        objdblayer.execQuery(strQuery + strQueryVal);

                        strQuery = "insert into icustomfields ([type],typewise,tran_nm,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,_mon_con,default_con,inter_val,mandatory,disp_pickup,disp_head,valid_mast,remarks,_tab,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_btntype,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_Dpopflds,_copy,_pickup,_top,frm_nm,reftbltran_cd,_fld_width,_fld_pre, _isDeleteAllowed)";
                        strQueryVal = "select [type],typewise,tran_nm,code='" + objBLFD.HTMAIN["Code"].ToString() + "','EM',head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,_mon_con,default_con,inter_val,mandatory,disp_pickup,disp_head,valid_mast,remarks,_tab,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_btntype,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_Dpopflds,_copy,_pickup,_top,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isDeleteAllowed from iIMPORTCUSTOMFIELDS where code='AD'";
                        objdblayer.execQuery(strQuery + strQueryVal);
                    }

                    DataSet dsetTblFields = objdblayer.dsquery("declare @amd_no int,@amd_dt int,@amd_req int select @amd_no=case when count(*)>0 then 1 else  2 end from information_schema.columns where column_name='IM_AMDNO' and table_name='" + tbl_nm + "' select @amd_dt=case when count(*)>0 then 1 else  2 end from information_schema.columns where column_name='IM_AMDDT' and table_name='" + tbl_nm + "' select @amd_req=case when count(*)>0 then 1 else  2 end from information_schema.columns where column_name='IM_AMDREQ' and table_name='" + tbl_nm + "' select  case when(@amd_no=@amd_dt) then case when(@amd_no=@amd_req) then case when(@amd_no=1) then 1 else 2 end else 0 end else 0 end amdreq");
                    if (dsetTblFields != null && dsetTblFields.Tables.Count != 0 && dsetTblFields.Tables[0].Rows.Count != 0)//fld exists
                    {
                        if (dsetTblFields.Tables[0].Rows[0]["amdreq"].ToString() == "0")
                        {
                            BL_FIELDS.Errormsg = "Sorry Amendment Field's Already Exist in the Custom Field Master.";
                            return false;
                        }
                        else if (dsetTblFields.Tables[0].Rows[0]["amdreq"].ToString() == "2")
                        {
                            string strQuery = "alter table " + tbl_nm + " add IM_AMDREQ bit,IM_AMDNO varchar(50),IM_AMDDT datetime";

                            if (!objdblayer.execQuery(strQuery))
                            {
                                BL_FIELDS.Errormsg = "adding field's to table " + tbl_nm + " is not successful.";
                                return false;
                            }
                            else
                            {
                                strQuery = "update " + tbl_nm + " set IM_AMDREQ=false,IM_AMDNO ='',IM_AMDDT='1900-01-01 00:00:00.000'";
                                objdblayer.execQuery(strQuery);
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion 3.0

        public bool Save_Levels(string pkValue)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = true;
            string serialnos = "0";

            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashItemtbl)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (serialnos == "")
                        serialnos = "'" + ((Hashtable)entry.Value)["si_no"].ToString() + "'";
                    else
                        serialnos += ",'" + ((Hashtable)entry.Value)["si_no"].ToString() + "'";
                }
            }

            DataSet dsetNotExistSINo = objdblayer.dsquery("select * from LEVELS where si_no not in (" + serialnos + ") and tran_id='" + pkValue + "'  order by cast(si_no as int)");
            if (dsetNotExistSINo != null && dsetNotExistSINo.Tables.Count != 0 && dsetNotExistSINo.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dsetNotExistSINo.Tables[0].Rows)
                {
                    bool flg = RemoveField("level" + row["si_no"].ToString() + "_status", row["level_nm"].ToString(), row["Behavier_cd"].ToString() + "_APPROVE", row["Code"].ToString(), row["beh_code"].ToString());
                    if (flg)
                    {
                        if (BL_FIELDS.Errormsg != "NOTREQUIRED")
                        {
                            RemoveField("level" + row["si_no"].ToString() + "_status_id", row["level_nm"].ToString(), row["beh_code"].ToString() + "_APPROVE", row["Code"].ToString(), row["beh_code"].ToString());
                            RemoveField("level" + row["si_no"].ToString() + "_remarks", row["level_nm"].ToString(), row["beh_code"].ToString() + "_APPROVE", row["Code"].ToString(), row["beh_code"].ToString());
                            RemoveField("level" + row["si_no"].ToString() + "_app_by", row["level_nm"].ToString(), row["beh_code"].ToString() + "_APPROVE", row["Code"].ToString(), row["beh_code"].ToString());
                            RemoveField("level" + row["si_no"].ToString() + "_app_dt", row["level_nm"].ToString(), row["beh_code"].ToString() + "_APPROVE", row["Code"].ToString(), row["beh_code"].ToString());
                        }
                    }
                    else
                    {
                        BL_FIELDS.Errormsg = "Sorry Removing Level is not Successfull";
                        return false;
                    }
                }
            }

            objdblayer.execDeleteQuery("delete from LEVELS where si_no not in (" + serialnos + ") and tran_id='" + pkValue + "'");

            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashItemtbl)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        if (entry1.Key.ToString().ToLower() != "si_no")
                        {
                            htFields.Add(entry1.Key, entry1.Value);
                        }
                        else
                        {
                            if (entry1.Value.ToString() == "")
                                htFieldscond.Add(entry1.Key, "0");
                            else
                                htFieldscond.Add(entry1.Key, entry1.Value);
                        }

                    }
                    if (htFields.Count != 0)
                    {
                        htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                        htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr.ToString();
                        htFields["tran_cd"] = objBLFD.TRAN_CD;
                        htFields["tran_id"] = objBLFD.Tran_id;
                        htFields["code"] = objBLFD.HTMAIN["code"].ToString();
                        htFields["beh_code"] = objBLFD.HTMAIN["Behavier_cd"].ToString();
                        if (FindItemExistance(pkValue, htFieldscond["si_no"].ToString()))
                        {
                            htFieldscond["code"] = htFields["code"];
                            htFields.Remove("code");
                            bResult = objdblayer.execUpdate("LEVELS", htFields, htFieldscond);
                        }
                        else
                        {
                            bool flg = CreateNewField("level" + htFieldscond["si_no"].ToString() + "_status", htFields["beh_code"].ToString() + "_APPROVE", htFields["code"].ToString(), "varchar", "50", "1");
                            if (flg)
                            {
                                CreateNewField("level" + htFieldscond["si_no"].ToString() + "_status_id", htFields["beh_code"].ToString() + "_APPROVE", htFields["code"].ToString(), "int", "0", "0");
                            } if (flg)
                            {
                                CreateNewField("level" + htFieldscond["si_no"].ToString() + "_remarks", htFields["beh_code"].ToString() + "_APPROVE", htFields["code"].ToString(), "varchar", "2000", "0");
                            } if (flg)
                            {
                                CreateNewField("level" + htFieldscond["si_no"].ToString() + "_app_by", htFields["beh_code"].ToString() + "_APPROVE", htFields["code"].ToString(), "varchar", "250", "0");
                            }
                            if (flg)
                            {
                                CreateNewField("level" + htFieldscond["si_no"].ToString() + "_app_dt", htFields["beh_code"].ToString() + "_APPROVE", htFields["code"].ToString(), "datetime", "0", "0");
                            }
                            if (!flg)
                            {
                                BL_FIELDS.Errormsg = "Sorry! Creating Levels is not Successfull";
                                return false;
                            }
                            else
                            {
                                foreach (DictionaryEntry entry1 in htFieldscond)
                                {
                                    if (!htFields.ContainsKey(entry1.Key))
                                    {
                                        htFields.Add(entry1.Key, entry1.Value);
                                    }
                                }
                                bResult = objdblayer.execInsert("LEVELS", htFields);
                            }
                        }
                        if (bResult)
                        {
                            htFields.Clear();
                            htFieldscond.Clear();
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return bResult;
        }

        private bool CreateNewField(string col_nm, string tbl_nm, string code, string data_ty, string _width, string _precision)
        {
            try
            {
                string strtblQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + col_nm + "' and TABLE_NAME='" + tbl_nm + "'";//+ _tableNm[str].ToString() + "'";
                DataSet dsettbl = objdblayer.dsquery(strtblQuery);
                if (dsettbl != null && dsettbl.Tables.Count != 0 && dsettbl.Tables[0].Rows.Count != 0)//fld exists in specific table
                {
                    string strQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where COLUMN_NAME='" + col_nm + "' and DATA_TYPE = '" + data_ty + "' and ";
                    switch (data_ty)
                    {
                        case "varchar": strQuery += " CHARACTER_MAXIMUM_LENGTH = " + _width + " and TABLE_NAME = '" + tbl_nm + "'"; break;
                        case "decimal": strQuery += " numeric_precision=" + _width + "and numeric_scale=" + _precision + " and TABLE_NAME = '" + tbl_nm + "'"; break;
                        case "int":
                        case "bit":
                        case "datetime": strQuery += " TABLE_NAME = '" + tbl_nm + "'"; break;
                    }
                    DataSet dset1 = objdblayer.dsquery(strQuery);
                    if (!(dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0))//fld exists in specific table
                    {
                        BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                        return false;
                    }
                }
                else
                {
                    string strQuery = "";
                    strQuery = "alter table " + tbl_nm + " add " + col_nm + " " + data_ty;
                    switch (data_ty)
                    {
                        case "varchar": strQuery += " (" + _width + ")"; break;
                        case "decimal": strQuery += " (" + _width + "," + _precision + ")"; break;
                        case "int":
                        case "bit":
                        case "datetime": strQuery += ""; break;
                    }

                    if (!objdblayer.execQuery(strQuery))
                    {
                        BL_FIELDS.Errormsg = "adding field to table " + tbl_nm + " is not successful.";
                        return false;
                    }
                    else
                    {
                        string strQuery1 = "";
                        strQuery1 = "update " + tbl_nm + " set " + col_nm + "=";
                        switch (data_ty)
                        {
                            case "varchar": strQuery1 += "'" + " " + "'"; break;
                            case "decimal": strQuery1 += "'0.00'"; break;
                            case "int": strQuery1 += "0"; break;
                            case "bit": strQuery1 += "'0'"; break;
                            case "datetime": strQuery1 += "null"; break;
                        }
                        objdblayer.execQuery(strQuery1);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private bool RemoveField(string col_nm, string level_nm, string tbl_nm, string code, string beh_code)
        {
            try
            {
                BL_FIELDS.Errormsg = "";
                DataSet dset = objdblayer.dsquery("select * from LEVELS where level_nm='" + level_nm + "' and code!='" + code + "' and beh_code='" + beh_code + "' order by cast(si_no as int)");
                if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)//fld exists
                {
                    BL_FIELDS.Errormsg = "NOTREQUIRED";
                    // return false;
                }
                else
                {
                    string strQuery = "";
                    strQuery = "ALTER TABLE " + tbl_nm + " DROP COLUMN " + col_nm;
                    if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                    {
                        BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Table is not successful.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool FindItemExistance(string pkValue, string item_no)
        {
            string strSql = String.Format("SELECT count(*) cnt from  LEVELS where tran_id='{0}' AND si_no='{1}'", pkValue.ToString(), item_no);
            DataSet ds = objdblayer.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool FindApproveExistance(string pkValue, string tran_cd, string approve_tbl)
        {
            string strSql = String.Format("SELECT count(*) cnt from " + approve_tbl + "  where tran_id='{0}' AND tran_cd='{1}'", pkValue.ToString(), tran_cd);
            DataSet ds = objdblayer.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        private void AutoOrderUpdate()
        {
            string strquery = GetQuery();
            DataSet dsetDCOrder = objdblayer.dsquery(strquery);
            int Order_cnt = 0, count = 0;
            string tbl_nm1 = "", column_nm1 = "", primary_key1 = "0", _type = "";
            if (dsetDCOrder != null && dsetDCOrder.Tables.Count != 0 && dsetDCOrder.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in dsetDCOrder.Tables[0].Rows)
                {
                    if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        if (Order_cnt >= 0 && _type != row["_type"].ToString())
                        {
                            _type = row["_type"].ToString();
                            Order_cnt = 0;
                            count = 0;
                        }
                    }

                    if (Order_cnt == 0)
                    {
                        count = 1;
                        Order_cnt = 1;
                    }
                    else
                    {
                        //if (!(row["parent_ctrl"].ToString() != "" && row["data_ty"].ToString() == "button"))
                        //{                       
                        //   
                        //}
                        Order_cnt = Order_cnt + int.Parse(dsetDCOrder.Tables[0].Rows[count - 1]["add_cnt"].ToString());
                        count++;
                    }
                    tbl_nm1 = ""; column_nm1 = ""; primary_key1 = "0";
                    switch (row["tran_cd1"].ToString())
                    {
                        case "EM": tbl_nm1 = "icustomfields"; column_nm1 = "order_no"; primary_key1 = "custom_id"; break;
                        case "BM": tbl_nm1 = "ibasefields"; column_nm1 = "order_no"; primary_key1 = "baseid"; break;
                        case "DM": tbl_nm1 = "dc_mast"; column_nm1 = "corder"; primary_key1 = "dc_id"; break;
                    }
                    bool bResult = objdblayer.execQuery("Update " + tbl_nm1 + " set " + column_nm1 + "=" + Order_cnt + " where tran_cd='" + row["tran_cd1"].ToString() + "' and " + primary_key1 + "=" + row["custom_id"].ToString());
                }
            }
        }
        private string GetQuery()
        {
            string strquery = "";
            if (objBLFD.HTMAIN["type"].ToString() == "1")
            {
                if (!bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,case when (data_ty='button' and parent_ctrl!='') then 0 else 1 end add_cnt,parent_ctrl,data_ty from ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd tran_cd1,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end,parent_ctrl='',data_ty='' from dc_mast where typewise=0 and code='" + objBLFD.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    }
                    else
                    {
                        strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_btntype _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 and disp_head!=1 and order_no>0 order by _btntype,order_no,col_order_no";
                    }
                }
                else
                {
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no,col_order_no";
                    }
                    else
                    {
                        strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_btntype _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head!=1 and order_no>0 order by _btntype,order_no,col_order_no";
                    }
                }
            }
            else if (objBLFD.HTMAIN["type"].ToString() == "2")
            {
                if (!bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,case when (data_ty='button' and parent_ctrl!='') then 0 else 1 end add_cnt,parent_ctrl,data_ty from ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd tran_cd1,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end,parent_ctrl='',data_ty='' from dc_mast where typewise=0 and code='" + objBLFD.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    }
                    else
                    {
                        strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_btntype _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=0 and disp_head!=1 and order_no>0 order by _btntype,order_no,col_order_no";
                    }
                }
                else
                {
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no,col_order_no";
                    }
                    else
                    {
                        strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_btntype _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head!=1 and order_no>0 order by _btntype,order_no,col_order_no";
                    }
                }
            }
            else
            {
                if (objBLFD.HTMAIN["type"].ToString() == "0")
                {
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                    {
                        strquery = "select * from (select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head=1 and order_no>0 union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and data_ty!='TAB' and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0))vw order by order_no,col_order_no";
                    }
                    else
                    {
                        strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_tab _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head!=1 and order_no>0 order by _tab,order_no,col_order_no";
                    }
                }
            }
            return strquery;
        }

        public string Save_Main_Trasaction(BL_BASEFIELD objBLFD, string tbl_nm)
        {
            try
            {
                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult;

                foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                {
                    if (entry.Key.ToString().ToLower() != objBLFD.Primary_id.ToLower())
                    {
                        htFields.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        if (objBLFD.HTMAIN[objBLFD.Primary_id.ToUpper()].ToString() == "")
                            htFieldscond.Add(entry.Key, "0");
                        else
                            htFieldscond.Add(entry.Key, entry.Value);
                    }
                }
                if (htFields.Count > 0)
                {
                    htFields["tran_cd"] = objBLFD.Code;
                    htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                    if (int.Parse(htFieldscond[objBLFD.Primary_id.ToUpper()].ToString()) > 0)
                    {
                        bResult = objdblayer.execUpdate(tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        bResult = objdblayer.execInsert(tbl_nm, htFields);
                    }
                    if (bResult)
                    {
                        if (objBLFD.HTITEM.Count > 0)
                        {
                            int tran_id = 0;
                            if (ACTIVE_BL.Tran_mode == "add_mode")
                            {
                                tran_id = getMainId(objBLFD.HTMAIN, objBLFD.Main_tbl_nm, objBLFD.Primary_id);
                            }
                            else
                            {
                                tran_id = int.Parse(objBLFD.Tran_id);
                            }
                            if (tran_id > 0)
                            {
                                if (ACTIVE_BL.Code == "TM")
                                {
                                    bool itemflg = Save_Item_Rights(objBLFD, tran_id, "CONTROL_SET");
                                }
                                else if (ACTIVE_BL.Code == "PM" || ACTIVE_BL.Code == "QM" || ACTIVE_BL.Code == "CP" || ACTIVE_BL.Code == "VP" || ACTIVE_BL.Code == "JP")
                                {
                                    bool itemflg = Save_Item_Rights(objBLFD, tran_id, objBLFD.Item_tbl_nm);
                                }
                                else
                                {
                                    bool itemflg = Save_Item_Rights(objBLFD, tran_id, "RIGHTS");
                                }
                            }
                        }
                    }
                    else
                    {
                        return "Main Table Updation is Failed";
                    }
                    return "";
                }
                else
                {
                    return "Main Table Updation is Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Save_Item_Rights(BL_BASEFIELD objBSFD, int tran_id, string tbl_nm)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            string val = "";
            //string serialnos = "";
            string strItemPriFld = GetPrimaryKeyFldNm(tbl_nm);

            string serialnos = "";
            if (ACTIVE_BL.Code == "PM" || ACTIVE_BL.Code == "QM" || ACTIVE_BL.Code == "CP" || ACTIVE_BL.Code == "VP" || ACTIVE_BL.Code == "JP")
            {
                foreach (DictionaryEntry entry in objBLFD.HTITEM)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos = "'" + ((Hashtable)entry.Value)["ptserial"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["ptserial"].ToString() + "'";
                    }
                }
                bool flg = objdblayer.execDeleteQuery("delete from " + objBSFD.Item_tbl_nm + " where ptserial not in (" + serialnos + ") and " + objBSFD.Primary_id + "='" + objBSFD.Tran_id + "' and TRAN_CD='" + objBSFD.Code + "'");
            }
            foreach (DictionaryEntry entry in objBLFD.HTITEM)
            {
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    if (entry1.Key.ToString().ToLower() != strItemPriFld.ToLower())
                    {
                        htFields.Add(entry1.Key, val == "" ? entry1.Value : val);
                        val = "";
                    }
                    else
                    {
                        if (entry1.Value.ToString() == "")
                            htFieldscond.Add(entry1.Key, "0");
                        else
                            htFieldscond.Add(entry1.Key, entry1.Value);
                    }
                }
                if (htFields.Count != 0)
                {
                    htFields[objBLFD.Primary_id.ToUpper()] = tran_id;
                    htFieldscond[objBLFD.Primary_id.ToUpper()] = tran_id;
                    htFields["tran_cd"] = objBLFD.Code;
                    htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;

                    if (int.Parse(htFieldscond[strItemPriFld].ToString()) > 0)
                    {
                        htFields.Remove(objBLFD.Primary_id);
                        htFields.Remove(strItemPriFld);
                        bResult = objdblayer.execUpdate(tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        foreach (DictionaryEntry entry1 in htFieldscond)
                        {
                            if (!htFields.ContainsKey(entry1.Key))
                            {
                                htFields.Add(entry1.Key, entry1.Value);
                            }
                        }
                        htFields.Remove(strItemPriFld);
                        bResult = objdblayer.execInsert(tbl_nm, htFields);
                    }
                    if (bResult)
                    {
                        htFields.Clear();
                        htFieldscond.Clear();
                    }
                }
            }
            return bResult;
        }
        public bool Save_Extra_Rights(BL_BASEFIELD objBSFD, int tran_id, string tbl_nm)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            string val = "";
            //string serialnos = "";
            string strItemPriFld = GetPrimaryKeyFldNm(tbl_nm);
            string serialnos = "''";
            if (ACTIVE_BL.Code == "EV" || ACTIVE_BL.Code == "FA" || ACTIVE_BL.Code == "ES")
            {
                foreach (DictionaryEntry entry in objBLFD.HTEXTRA)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos = "'" + ((Hashtable)entry.Value)["ptserial1"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["ptserial1"].ToString() + "'";
                    }
                }
                bool flg = objdblayer.execDeleteQuery("delete from " + objBSFD.Extra_tbl_nm + " where ptserial1 not in (" + serialnos + ") and " + objBSFD.Primary_id + "='" + objBSFD.Tran_id + "' and TRAN_CD='" + objBSFD.Code + "'");
            }



            //string serialnos = "";
            //if (ACTIVE_BL.Code == "EV" || ACTIVE_BL.Code == "FA" || ACTIVE_BL.Code == "ES")
            //{
            //    foreach (DictionaryEntry entry in objBLFD.HTEXTRA)
            //    {
            //        if (((Hashtable)entry.Value).Count != 0)
            //        {
            //            if (serialnos == "")
            //                serialnos += "'" + ((Hashtable)entry.Value)["ptserial1"].ToString() + "'";
            //            else
            //                serialnos += ",'" + ((Hashtable)entry.Value)["ptserial1"].ToString() + "'";
            //        }
            //    }
            //    bool flg = objdblayer.execDeleteQuery("delete from " + objBSFD.Extra_tbl_nm + " where ptserial1 not in (" + serialnos + ") and " + objBSFD.Primary_id + "='" + objBSFD.Tran_id + "' and TRAN_CD='" + objBSFD.Code + "'");

            //}

            foreach (DictionaryEntry entry in objBLFD.HTEXTRA)
            {
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    if (entry1.Key.ToString().ToLower() != strItemPriFld.ToLower())
                    {
                        htFields.Add(entry1.Key, val == "" ? entry1.Value : val);
                        val = "";
                    }
                    else
                    {
                        if (entry1.Value.ToString() == "")
                            htFieldscond.Add(entry1.Key, "0");
                        else
                            htFieldscond.Add(entry1.Key, entry1.Value);
                    }
                }
                if (htFields.Count != 0)
                {
                    htFields[objBLFD.Primary_id.ToUpper()] = tran_id;
                    htFieldscond[objBLFD.Primary_id.ToUpper()] = tran_id;
                    htFields["tran_cd"] = objBLFD.Code;
                    htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;

                    if (int.Parse(htFieldscond[strItemPriFld].ToString()) > 0)
                    {
                        htFields.Remove(objBLFD.Primary_id);
                        htFields.Remove(strItemPriFld);
                        bResult = objdblayer.execUpdate(tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        foreach (DictionaryEntry entry1 in htFieldscond)
                        {
                            if (!htFields.ContainsKey(entry1.Key))
                            {
                                htFields.Add(entry1.Key, entry1.Value);
                            }
                        }
                        htFields.Remove(strItemPriFld);
                        bResult = objdblayer.execInsert(tbl_nm, htFields);
                    }
                    if (bResult)
                    {
                        htFields.Clear();
                        htFieldscond.Clear();
                    }
                }
            }

            return bResult;
        }

        public int getMainId(Hashtable HTMAIN, string tbl_nm, string field)
        {
            int tran_id = 0;
            string strSql;
            strSql = String.Format("SELECT  top 1 " + objBLFD.Primary_id + "  from " + tbl_nm + " order by " + field + " desc");
            ArrayList arRes = objdblayer.SelectQueryFixed(strSql);
            if (arRes.Count > 0)
            {
                ArrayList arRow = arRes[0] as ArrayList;
                tran_id = int.Parse(arRow[0].ToString());
                arRes.Clear();
                arRow.Clear();
            }
            return tran_id;
        }

        public string GetPrimaryKeyFldNm(string tbl_nm)
        {
            DataSet ds = new DataSet();
            ds = objdblayer.dsquery("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tbl_nm + "'");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                return ds.Tables[0].Rows[0]["column_name"].ToString();
            }
            return "";
        }

        private bool StockCheck()
        {
            if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
            {
                Hashtable htStkPlusItem = new Hashtable();
                foreach (DictionaryEntry entry in objBLFD.HTITEM)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (htStkPlusItem != null && htStkPlusItem.Contains(((Hashtable)entry.Value)["prod_nm"].ToString()))
                        {
                            htStkPlusItem[((Hashtable)entry.Value)["prod_nm"].ToString()] = decimal.Parse(((Hashtable)entry.Value)["qty"].ToString()) + decimal.Parse(htStkPlusItem[((Hashtable)entry.Value)["prod_nm"].ToString()].ToString());
                        }
                        else
                        {
                            htStkPlusItem[((Hashtable)entry.Value)["prod_nm"].ToString()] = decimal.Parse(((Hashtable)entry.Value)["qty"].ToString());
                        }
                    }
                }
                foreach (DictionaryEntry entry1 in htStkPlusItem)
                {
                    decimal original_qty = 0;

                    DataSet dsetQty = objdblayer.dsquery("select isnull(sum(isnull(qty,0)),0) qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + entry1.Key.ToString() + "'");
                    if (dsetQty != null && dsetQty.Tables.Count != 0 && dsetQty.Tables[0].Rows.Count != 0)
                    {
                        original_qty = decimal.Parse(dsetQty.Tables[0].Rows[0]["qty"].ToString());
                    }

                    Hashtable htparam = new Hashtable();
                    htparam.Add("@Item", entry1.Key.ToString());
                    htparam.Add("@date", DateTime.Now);
                    htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                    htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
                    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);

                    if (dsetStock != null && dsetStock.Tables.Count != 0 && dsetStock.Tables[0].Rows.Count != 0)
                    {
                        if (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - original_qty + decimal.Parse(entry1.Value.ToString()) >= 0)
                        {
                            return true;
                        }
                        else
                        {
                            BL_FIELDS.Errormsg = "Available quantity for item : " + entry1.Key.ToString() + " is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - original_qty + decimal.Parse(entry1.Value.ToString())).ToString();
                            return false;
                        }
                    }
                    //else
                    //{
                    //    BL_FIELDS.Errormsg = "No Stock available for : " + entry1.Key.ToString();
                    //    return false;
                    //}
                }
            }
            return true;
        }

        #region 2.0
        private void GetColumnNumber(string code, int coltype, bool _headerOrItem, string _btn_nm, string _tab_nm)
        {
            DataSet ds = new DataSet();
            // string code = (objBLFD.HTMAIN.Count != 0 && objBLFD.HTMAIN["code"] != null) ? objBLFD.HTMAIN["code"].ToString() : "";
            if (coltype == 1)
            {
                if (_headerOrItem)
                {
                    ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + code + "' and typewise=1 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + code + "' and typewise=1)vw");
                }
                else
                {
                    ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + code + "' and typewise=0 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + code + "' and typewise=0)vw");
                }
            }
            else if (coltype == 2)
            {
                if (_headerOrItem)
                {
                    ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _btntype='" + _btn_nm + "'  and typewise=1");
                }
                else { ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _btntype='" + _btn_nm + "'  and typewise=0"); }
            }
            else if (coltype == 3)
            {
                if (_headerOrItem)
                {
                    ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _tab='" + _tab_nm + "' and typewise=1");
                }
                else
                {
                    ds = objdblayer.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _tab='" + _tab_nm + "' and typewise=0");
                }
            }
            else
            {
                _local_order_no = 1;
                _local_col_order_no = 1;
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                _local_order_no = Convert.ToInt32(ds.Tables[0].Rows[0]["order_no"].ToString());
                _local_col_order_no = Convert.ToInt32(ds.Tables[0].Rows[0]["col_order_no"].ToString());
            }
        }
        #endregion 2.0
    }
}
