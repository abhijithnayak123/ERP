using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using iMANTRA_BL;
using iMANTRA_iniL;
using iMANTRA_DL;
using CUSTOM_iMANTRA;

namespace iMANTRA_IL
{
    public class FL_MAST
    {
        //Ini objIni = new Ini();

        //private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        //public BL_BASEFIELD ObjBLFD
        //{
        //    get { return objBLFD; }
        //    set { objBLFD = value; }
        //}

        //FL_GRIDEVENTS objFL_GRIDEVENTS = new FL_GRIDEVENTS();
        //FL_TRANSACTION objFLTRANS = new FL_TRANSACTION();

        //iMASTER objiMASTER = new iMASTER();
        //iTRANSACTION objiTRANSACTION = new iTRANSACTION();
        public Company objCompany = new Company();
        DL_MAST objDLMAST = new DL_MAST();

        public DataSet GETMASTBASEHEADERFIELD(string tran_cd, string compid)
        {
            
            return objDLMAST.GETMASTBASEHEADERFIELD(tran_cd,compid);
        }
        public DataSet GETMASTBASETABFIELD(string tran_cd, string compid)
        {
            
            return objDLMAST.GETMASTBASETABFIELD(tran_cd,compid);
        }
        public DataSet GETMASTBASETABDETAILS(string tran_cd, string tab_nm, string compid)
        {
            
            return objDLMAST.GETMASTBASETABDETAILS(tran_cd, tab_nm,compid);
        }
        //public bool ValidateFields()
        //{

        //    return false;
        //}

        //public bool DuplicateCopy(string source_nm, string dest_nm)
        //{
        //    if (ObjBLFD.Tran_mode == "add_mode")
        //    {
        //        if (ObjBLFD.HTMAIN[source_nm] != null && ObjBLFD.HTMAIN[source_nm].ToString() != "" && ObjBLFD.HTMAIN[dest_nm].ToString() != "")
        //        {
        //            ObjBLFD.HTMAIN[dest_nm] = ObjBLFD.HTMAIN[source_nm].ToString();
        //        }
        //    }
        //    return true;

        //}
        //public bool ValidateName(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    //string tbl_nm = ObjBLFD.Main_tbl_nm;
        //    string _tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
        //    string catalog = ObjBLFD.Tbl_catalog;
        //    DataSet dsname = objDLMAST.Find_Name_Details(fld_nm, tbl_nm, _value, _tran_id, catalog);
        //    if (dsname != null && dsname.Tables[0].Rows.Count != 0)
        //    {
        //        if (interflg == "0")
        //        {
        //            if (ObjBLFD.HTMAIN.ContainsKey(fld_nm))
        //            {
        //                ObjBLFD.HTMAIN[fld_nm] = dsname.Tables[0].Rows[0][fld_nm].ToString();
        //                string keyId = objFLTRANS.GetPrimaryKeyFldNm(tbl_nm, catalog);
        //                if (objBLFD.HTMAIN.ContainsKey(keyId.Trim().ToUpper()))
        //                {
        //                    ObjBLFD.HTMAIN[keyId] = dsname.Tables[0].Rows[0][keyId].ToString();
        //                }
        //            }
        //            else
        //            {
        //                ObjBLFD.HTITEM_VALUE[fld_nm] = dsname.Tables[0].Rows[0][fld_nm].ToString();
        //                string keyId = objFLTRANS.GetPrimaryKeyFldNm(tbl_nm, catalog);
        //                if (objBLFD.HTMAIN.ContainsKey(keyId.Trim().ToUpper()))
        //                {
        //                    ObjBLFD.HTITEM_VALUE[keyId] = dsname.Tables[0].Rows[0][keyId].ToString();
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}
        //public bool ValidateDuplicateName(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    // string tbl_nm = ObjBLFD.Main_tbl_nm;
        //    string _tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
        //    string catalog = ObjBLFD.Tbl_catalog;

        //    DataSet dsname = objDLMAST.Find_Name_Existance(fld_nm, tbl_nm, _value, _tran_id, catalog);
        //    if (dsname != null && dsname.Tables[0].Rows.Count != 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (ObjBLFD.Tran_mode == "add_mode")
        //        {
        //            if ((objBLFD.Code == "CM" || objBLFD.Code == "VM") && ObjBLFD.HTMAIN["AC_NM"] != null && ObjBLFD.HTMAIN["AC_NM"].ToString() != "")
        //            {
        //                ObjBLFD.HTMAIN["DISPLAY_NM"] = ObjBLFD.HTMAIN["AC_NM"].ToString();
        //            }
        //        }
        //    }
        //    return true;
        //}
        //public bool ValidateProductGroup(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    if (objBLFD.HTMAIN["PT_GRP_NM"] != null && objBLFD.HTMAIN["PT_GRP_NM"].ToString() != "")
        //    {
        //        if (ValidateName(fld_nm, tbl_nm, _value, interflg))
        //        {
        //            DataSet ds = objDLMAST.Get_Product_group_Details(tbl_nm, "PROD_TY_NM,PROD_TYID,UOM,CHAP_NO,CHAP_NM", objBLFD.HTMAIN["PT_GRP_ID"].ToString());
        //            if (ds != null && ds.Tables[0].Rows.Count != 0)
        //            {
        //                objBLFD.HTMAIN["PROD_TY_NM"] = ds.Tables[0].Rows[0]["PROD_TY_NM"].ToString();
        //                objBLFD.HTMAIN["PROD_TYID"] = ds.Tables[0].Rows[0]["PROD_TYID"].ToString();
        //                objBLFD.HTMAIN["UOM"] = ds.Tables[0].Rows[0]["UOM"].ToString();
        //                objBLFD.HTMAIN["CHAP_NO"] = ds.Tables[0].Rows[0]["CHAP_NO"].ToString();
        //                objBLFD.HTMAIN["CHAP_NM"] = ds.Tables[0].Rows[0]["CHAP_NM"].ToString();
        //                objBLFD.HTMAIN["S_UOM"] = ds.Tables[0].Rows[0]["UOM"].ToString();
        //                objBLFD.HTMAIN["PUR_UNIT"] = ds.Tables[0].Rows[0]["UOM"].ToString();
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else return false;
        //    }
        //    return true;
        //}

        //public bool ValidateUOM(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    if (objBLFD.HTMAIN["UOM"] != null && objBLFD.HTMAIN["UOM"].ToString() != "")
        //    {
        //        objBLFD.HTMAIN["S_UOM"] = objBLFD.HTMAIN["UOM"].ToString();
        //        objBLFD.HTMAIN["PUR_UNIT"] = objBLFD.HTMAIN["UOM"].ToString();
        //        return true;
        //    }
        //    return false;
        //}

        //public string ValidateToolSave()
        //{
        //    string strValue = "";
        //    if (objBLFD.Tran_type == "Transaction")
        //    {
        //        foreach (DataRow row in ObjBLFD.dsHeader.Tables[0].Rows)
        //        {
        //            if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["valid_con"].ToString() != "" && row["mandatory"].ToString().ToLower() != "false")
        //            {
        //                bool bln = ValidateFields(row, 0, "transaction");
        //                if (!bln)
        //                {
        //                    strValue = row["head_nm"].ToString();
        //                    break;
        //                }
        //            }
        //        }
        //        foreach (DataRow row in ObjBLFD.dsBASEADDIFIELD.Tables[0].Rows)
        //        {
        //            if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["valid_con"].ToString() != "" && row["mandatory"].ToString().ToLower() != "false")
        //            {
        //                bool bln = ValidateFields(row, 0, "transaction");
        //                if (!bln)
        //                {
        //                    strValue = row["head_nm"].ToString();
        //                    break;
        //                }
        //            }
        //        }
        //        if (strValue == "")
        //        {
        //            if (ObjBLFD.HTITEM.Count == 0)
        //            {
        //                strValue = "ITEM";
        //            }
        //            else
        //            {
        //                Hashtable HTITEMVal = ObjBLFD.HTITEM;
        //                Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        //                foreach (DictionaryEntry entry in HTITEMVal)
        //                {
        //                    objBLFD.HTITEM_VALUE = (Hashtable)entry.Value;
        //                    htcuritem = (Hashtable)entry.Value;
        //                    foreach (DictionaryEntry entry1 in htcuritem)
        //                    {
        //                        DataRow[] row = ObjBLFD.dsFooter.Tables[0].Select("fld_nm='" + entry1.Key + "' and valid_con<>'' and mandatory <> 0");
        //                        if (row.Length > 0)
        //                        {
        //                            bool bln = ValidateFields(row[0], 1, "transaction");
        //                            if (!bln)
        //                            {
        //                                strValue = row[0]["head_nm"].ToString();//entry1.Key.ToString();
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DataRow[] row1 = ObjBLFD.dsBASEADDIFIELDITEM.Tables[0].Select("fld_nm='" + entry1.Key + "' and valid_con<>'' and mandatory <> 0");
        //                            if (row1.Length > 0)
        //                            {
        //                                bool bln = ValidateFields(row1[0], 1, "transaction");
        //                                if (!bln)
        //                                {
        //                                    strValue = row1[0]["head_nm"].ToString();//entry1.Key.ToString();
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (strValue != "")
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else if (objBLFD.Tran_type == "Master")
        //    {
        //        foreach (DataRow row in ObjBLFD.dsBASEFIELDMAIN.Tables[0].Rows)
        //        {
        //            if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["valid_con"].ToString() != "" && row["mandatory"].ToString().ToLower() != "false")
        //            {
        //                bool bln = ValidateFields(row, 0,"master");
        //                if (!bln)
        //                {
        //                    strValue = row["head_nm"].ToString();
        //                    break;
        //                }
        //            }
        //        }
        //        foreach (DataRow row1 in ObjBLFD.dsTab.Tables[0].Rows)
        //        {
        //            ObjBLFD.dsTabDet = GETMASTBASETABDETAILS(objBLFD.Code, row1["fld_nm"].ToString());
        //            foreach (DataRow row in ObjBLFD.dsTabDet.Tables[0].Rows)
        //            {
        //                if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["valid_con"].ToString() != "" && row["mandatory"].ToString().ToLower() != "false")
        //                {
        //                    bool bln = ValidateFields(row, 0,"master");
        //                    if (!bln)
        //                    {
        //                        strValue = row["head_nm"].ToString();
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //    }
        //    return strValue;
        //}
        //public bool ValidateFields(DataRow row, int flg = 0,string tran_type="")
        //{
        //    objFL_GRIDEVENTS.objBASEFILEDS = ObjBLFD;

        //    string fld_nm = row["fld_nm"].ToString();

        //    string _strValue = "";

        //    bool validflg = true;

        //    string exp = row["valid_con"].ToString();
        //    string[] ar = exp.Split(new Char[] { '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
        //    if (ar.Length > 1)
        //    {
        //        if (ar.Length == 1)
        //        {
        //            //validflg = true;
        //            if (this.GetType().GetMethod(ar[0]) != null)
        //            {
        //                object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[0]);
        //                validflg = bool.Parse(methodInfo.Invoke(this, args).ToString());
        //            }
        //            else
        //            {
        //                if (tran_type=="master" && objiMASTER.GetType().GetMethod(ar[0]) != null)
        //                {
        //                    objiMASTER.ACTIVE_MASTER = objBLFD;
        //                    object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                    MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[0]);
        //                    validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
        //                }
        //                else if (tran_type == "transaction" && objiTRANSACTION.GetType().GetMethod(ar[0]) != null)
        //                {
        //                    objiTRANSACTION.ACTIVE_TRANSACTION = objBLFD;
        //                    object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                    MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(ar[0]);
        //                    validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, args).ToString().Trim());
        //                }
        //                else
        //                {
        //                    validflg = false;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (ar[0].Contains("!EMPTY"))
        //            {
        //                string[] cond = ar[0].Split(new string[] { "!EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
        //                string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
        //                _strValue = cod1;
        //                if (cod1 == "")
        //                {
        //                    validflg = false;
        //                }
        //                else
        //                {
        //                    validflg = true;
        //                }
        //            }
        //            else if (ar[0].Contains("EMPTY"))
        //            {
        //                string[] cond = ar[0].Split(new string[] { "EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
        //                string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
        //                _strValue = cod1;
        //                if (cod1 == "")
        //                {
        //                    validflg = true;
        //                }
        //                else
        //                {
        //                    validflg = false;
        //                }
        //            }
        //            else
        //            {
        //                string valu = ar[0].Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$").Replace("$=", "=$");
        //                string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
        //                if (cond.Length == 3)
        //                {
        //                    string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "decimal") : cond[0];
        //                    string cod2 = cond[2].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[2], "decimal") : cond[2];
        //                    switch (cond[1])
        //                    {
        //                        case "<": if (decimal.Parse(cod1) < decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        case ">": if (decimal.Parse(cod1) > decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        case "<=": if (decimal.Parse(cod1) <= decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        case ">=": if (decimal.Parse(cod1) >= decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        case "==": if (decimal.Parse(cod1) == decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        case "!=": if (decimal.Parse(cod1) != decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
        //                        default: validflg = false; break;
        //                    }
        //                }
        //            }

        //            if (validflg)
        //            {
        //                if (ar[1].ToString().Trim().ToUpper() != "TRUE")
        //                {
        //                    if (this.GetType().GetMethod(ar[1]) != null)
        //                    {
        //                        // object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString(), objBLFD.Tbl_catalog, "1" };
        //                        object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                        MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[1]);
        //                        validflg = bool.Parse(methodInfo.Invoke(this, args).ToString());
        //                    }
        //                    else
        //                    {
        //                        if (tran_type=="master" && objiMASTER.GetType().GetMethod(ar[1]) != null)
        //                        {
        //                            objiMASTER.ACTIVE_MASTER = objBLFD;
        //                            object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                            MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[1]);
        //                            validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
        //                        }
        //                        else if (tran_type == "transaction" && objiTRANSACTION.GetType().GetMethod(ar[1]) != null)
        //                        {
        //                            objiTRANSACTION.ACTIVE_TRANSACTION = objBLFD;
        //                            object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                            MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(ar[1]);
        //                            validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, args).ToString().Trim());
        //                        }
        //                        else
        //                        {
        //                            validflg = false;
        //                        }
        //                    }
        //                }
        //            }
        //            if (!validflg)
        //            {
        //                if (ar[2].ToString() != "FALSE")
        //                {
        //                    if (this.GetType().GetMethod(ar[2]) != null)
        //                    {
        //                        object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                        MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[2]);
        //                        validflg = bool.Parse(methodInfo.Invoke(this, args).ToString());
        //                    }
        //                    else
        //                    {
        //                        if (tran_type=="master" && objiMASTER.GetType().GetMethod(ar[2]) != null)
        //                        {
        //                            objiMASTER.ACTIVE_MASTER = objBLFD;
        //                            object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                            MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[2]);
        //                            validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
        //                        }
        //                        else if (tran_type == "transaction" && objiTRANSACTION.GetType().GetMethod(ar[2]) != null)
        //                        {
        //                            objiTRANSACTION.ACTIVE_TRANSACTION = objBLFD;
        //                            object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "1" };
        //                            MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(ar[2]);
        //                            validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, args).ToString().Trim());
        //                        }
        //                        else
        //                        {
        //                            validflg = false;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //MessageBox.Show(fld_nm +" SHOULD NOT BE EMPTY");
        //                }
        //            }
        //        }

        //    }
        //    return validflg;
        //}
        //public bool GetCompanyName(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    if (ObjBLFD.Tran_mode == "add_mode")
        //    {
        //        if (ObjBLFD.HTMAIN["COMP_NM"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_END"] != null && ObjBLFD.HTMAIN["FIN_YR_END"].ToString() != "")
        //        {
        //            int count = objDLMAST.Get_Company_Name(ObjBLFD);
        //            string comp_nm = ObjBLFD.HTMAIN["COMP_NM"].ToString().Substring(0, 1).ToUpper() + count.ToString().PadLeft(2, '0') + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).ToString("yy") + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).ToString("yy");
        //            if (Find_DB_Existance(comp_nm))
        //            {
        //                comp_nm += objDLMAST.Find_DB_Count(comp_nm).ToString();
        //            }
        //            ObjBLFD.HTMAIN["DB_NM"] = comp_nm;
        //            ObjBLFD.HTMAIN["FOLDER_NM"] = ObjBLFD.HTMAIN["DB_NM"];
        //            ObjBLFD.HTMAIN["DIR_NM"] = objIni.GetKeyFieldValue("APP_PATH", "path") + @"\" + ObjBLFD.HTMAIN["DB_NM"].ToString();
        //            // string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        //            //ObjBLFD.HTMAIN["FIN_YR"] =DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).Year;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        //public bool GetFinacial_Year(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    if (ObjBLFD.Tran_mode == "add_mode")
        //    {
        //        if (ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_END"] != null && ObjBLFD.HTMAIN["FIN_YR_END"].ToString() != "")
        //        {
        //            ObjBLFD.HTMAIN["FIN_YR"] = DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year.ToString() + "-" + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).Year.ToString();
        //        }
        //    }
        //    return true;
        //}
        //public bool AddFinacial_Year(string fld_nm, string tbl_nm, string _value, string interflg = "0")
        //{
        //    if (ObjBLFD.Tran_mode == "add_mode")
        //    {
        //        if (ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "")
        //        {
        //            //ObjBLFD.HTMAIN["FIN_YR_END"] = DateTime.Parse("03/31/" + (DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year + 1).ToString()).ToString(); //DateTime.Parse("31" + "-03-" + DateTime.Now.AddYears(1).Year.ToString());//.ToString("yyyy/MM/dd");
        //            ObjBLFD.HTMAIN["FIN_YR_END"] = (DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).AddYears(1)).AddDays(-1).ToString("yyyy/MM/dd");
        //        }
        //    }
        //    //ObjBLFD.HTMAIN["FIN_YR_END"] = DateTime.Now;//.ToString("yyyy/MM/dd");
        //    return true;
        //}
        //public bool Find_Login_User(string user_nm, string pwd, string fin_yr, string comp_nm)
        //{
        //    return objDLMAST.Find_Login_User(user_nm, pwd, fin_yr, comp_nm);
        //}
        //public bool Find_DB_Existance(string db_nm = "InodeMFG")
        //{
        //    return objDLMAST.Find_DB_Existance(db_nm);
        //}
        //public bool Create_DataBase(string db_nm, string bak_up_nm)
        //{
        //    return objDLMAST.Create_DataBase(db_nm, bak_up_nm);
        //}
        //public bool Create_BakUp(string db_nm)
        //{
        //    return objDLMAST.Create_BakUp(db_nm);
        //}
        //public bool CreateFolder(string floder_nm)
        //{
        //    return objDLMAST.CreateFolder(floder_nm);
        //}
        //public DataSet GETFIN_YEAR()
        //{
        //    return objDLMAST.GETFIN_YEAR();
        //}
        //public DataSet GET_Comp_by_Fin_yr(string fin_yr)
        //{
        //    return objDLMAST.GET_Comp_by_Fin_yr(fin_yr);
        //}
        //public DataSet Get_Company_Details(string comp_id)
        //{
        //    return objDLMAST.Get_Company_Details(comp_id);
        //}
    }
}
