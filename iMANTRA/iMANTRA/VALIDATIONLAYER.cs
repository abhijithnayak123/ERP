using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using iMANTRA_BL;
using iMANTRA_iniL;
using iMANTRA_DL;
using iMANTRA_IL;
using CUSTOM_iMANTRA;

namespace iMANTRA
{
    public class VALIDATIONLAYER
    {
        Ini objIni = new Ini();
        string errormsg = "";
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBLFD
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        FL_GRIDEVENTS objFL_GRIDEVENTS = new FL_GRIDEVENTS();
        FL_TRANSACTION objFLTRANS = new FL_TRANSACTION();
        FL_MAST objFLMAST = new FL_MAST();

        iMASTER objiMASTER = new iMASTER();
        //iTRANSACTION objiTRANSACTION = new iTRANSACTION();
        SetFieldsValue _objSetFieldsValue = new SetFieldsValue();

        iTRANSACTION objiTRANSACTION = new iTRANSACTION();
        iITEMVALID objiITEMVALID = new iITEMVALID();
        iQTYVALID objiQTYVALID = new iQTYVALID();
        iGRIDITEM objiGRIDITEM = new iGRIDITEM();
        SaveTransactionAndMaster objSaveTranAndMaster = new SaveTransactionAndMaster();

        public SetFieldsValue objSetFieldsValue
        {
            get { return _objSetFieldsValue; }
            set { _objSetFieldsValue = value; }
        }

        DL_MAST objDLMAST = new DL_MAST();

        public bool ValidateFields()
        {

            return false;
        }

        public bool DuplicateCopy(string source_nm, string dest_nm)
        {
            if (ObjBLFD.Tran_mode == "add_mode")
            {
                if (ObjBLFD.HTMAIN[source_nm] != null && ObjBLFD.HTMAIN[source_nm].ToString() != "" && ObjBLFD.HTMAIN[dest_nm].ToString() != "")
                {
                    ObjBLFD.HTMAIN[dest_nm] = ObjBLFD.HTMAIN[source_nm].ToString();
                }
            }
            return true;

        }
        public bool ValidateName()
        {
            //string tbl_nm = ObjBLFD.Main_tbl_nm;

            string _tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
            string catalog = ObjBLFD.Tbl_catalog;
            DataSet dsname = objDLMAST.Find_Name_Details(_objSetFieldsValue.Fld_nm, _objSetFieldsValue.Fld_tbl_nm, _objSetFieldsValue.Fld_value, _tran_id, catalog, objBLFD.ObjCompany.Compid.ToString());
            if (dsname != null && dsname.Tables.Count != 0 && dsname.Tables[0].Rows.Count != 0)
            {
                //if (interflg == "0")
                //{
                if (ObjBLFD.HTMAIN.ContainsKey(_objSetFieldsValue.Fld_nm))
                {
                    ObjBLFD.HTMAIN[_objSetFieldsValue.Fld_nm] = dsname.Tables[0].Rows[0][_objSetFieldsValue.Fld_nm].ToString();
                    string keyId = objFLTRANS.GetPrimaryKeyFldNm(_objSetFieldsValue.Fld_tbl_nm, catalog);
                    if (objBLFD.HTMAIN.ContainsKey(keyId.Trim().ToUpper()))
                    {
                        ObjBLFD.HTMAIN[keyId] = dsname.Tables[0].Rows[0][keyId].ToString();
                    }
                }
                else
                {
                    ObjBLFD.HTITEM_VALUE[_objSetFieldsValue.Fld_nm] = dsname.Tables[0].Rows[0][_objSetFieldsValue.Fld_nm].ToString();
                    string keyId = objFLTRANS.GetPrimaryKeyFldNm(_objSetFieldsValue.Fld_tbl_nm, catalog);
                    if (objBLFD.HTMAIN.ContainsKey(keyId.Trim().ToUpper()))
                    {
                        ObjBLFD.HTITEM_VALUE[keyId] = dsname.Tables[0].Rows[0][keyId].ToString();
                    }
                }
                //}
                return true;
            }

            return false;
        }
        public bool ValidateDuplicateName()
        {
            // string tbl_nm = ObjBLFD.Main_tbl_nm;
            string _tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
            string catalog = ObjBLFD.Tbl_catalog;

            DataSet dsname = objDLMAST.Find_Name_Existance(_objSetFieldsValue.Fld_nm, _objSetFieldsValue.Fld_tbl_nm, _objSetFieldsValue.Fld_value, _tran_id, catalog, objBLFD.ObjCompany.Compid.ToString());
            if (dsname != null && dsname.Tables[0].Rows.Count != 0)
            {
                return false;
            }
            else
            {
                if (ObjBLFD.Tran_mode == "add_mode")
                {
                    if ((objBLFD.Code == "CM" || objBLFD.Code == "VM") && ObjBLFD.HTMAIN["AC_NM"] != null && ObjBLFD.HTMAIN["AC_NM"].ToString() != "")
                    {
                        ObjBLFD.HTMAIN["DISPLAY_NM"] = ObjBLFD.HTMAIN["AC_NM"].ToString();
                    }
                }
            }
            return true;
        }
        public bool ValidateProductGroup()
        {
            if (objBLFD.HTMAIN["PT_GRP_NM"] != null && objBLFD.HTMAIN["PT_GRP_NM"].ToString() != "")
            {
                if (ValidateName())
                {
                    DataSet ds = objDLMAST.Get_Product_group_Details(_objSetFieldsValue.Fld_tbl_nm, "PROD_TY_NM,PROD_TYID,UOM,CHAP_NO,CHAP_NM", objBLFD.HTMAIN["PT_GRP_ID"].ToString(), objBLFD.ObjCompany.Compid.ToString());
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        objBLFD.HTMAIN["PROD_TY_NM"] = ds.Tables[0].Rows[0]["PROD_TY_NM"].ToString();
                        objBLFD.HTMAIN["PROD_TYID"] = ds.Tables[0].Rows[0]["PROD_TYID"].ToString();
                        objBLFD.HTMAIN["UOM"] = ds.Tables[0].Rows[0]["UOM"].ToString();
                        objBLFD.HTMAIN["CHAP_NO"] = ds.Tables[0].Rows[0]["CHAP_NO"].ToString();
                        objBLFD.HTMAIN["CHAP_NM"] = ds.Tables[0].Rows[0]["CHAP_NM"].ToString();
                        objBLFD.HTMAIN["S_UOM"] = ds.Tables[0].Rows[0]["UOM"].ToString();
                        objBLFD.HTMAIN["PUR_UNIT"] = ds.Tables[0].Rows[0]["UOM"].ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
            return true;
        }

        public bool ValidateUOM()
        {
            if (objBLFD.HTMAIN["UOM"] != null && objBLFD.HTMAIN["UOM"].ToString() != "")
            {
                objBLFD.HTMAIN["S_UOM"] = objBLFD.HTMAIN["UOM"].ToString();
                objBLFD.HTMAIN["PUR_UNIT"] = objBLFD.HTMAIN["UOM"].ToString();
                return true;
            }
            return false;
        }

        public string ValidateToolSave()
        {
            string strValue = "";
            if (objBLFD.Tran_type == "Transaction")
            {
                if (ObjBLFD.dsHeader != null && ObjBLFD.dsHeader.Tables.Count != 0 && ObjBLFD.dsHeader.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ObjBLFD.dsHeader.Tables[0].Rows)
                    {
                        if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")
                        {
                            bool bln = false;
                            bln = ValidateFields(row, "_mon_con", 0, "transaction", "");
                            if (bln && row["valid_con"].ToString() != "")
                            {
                                bln = ValidateFields(row, "valid_con", 0, "transaction", "");
                            }
                            if (!bln)
                            {
                                if (errormsg.Length == 0)
                                {
                                    strValue = "Please Enter Valid Data in " + row["head_nm"].ToString();
                                }
                                else
                                {
                                    strValue = errormsg;
                                }
                                break;
                            }
                        }
                    }
                }
                if (ObjBLFD.dsBASEADDIFIELD != null && ObjBLFD.dsBASEADDIFIELD.Tables.Count != 0 && ObjBLFD.dsBASEADDIFIELD.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ObjBLFD.dsBASEADDIFIELD.Tables[0].Rows)
                    {
                        if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")
                        {
                            bool bln = false;
                            bln = ValidateFields(row, "_mon_con", 0, "transaction", "");
                            if (bln && row["valid_con"].ToString() != "")
                            {
                                bln = ValidateFields(row, "valid_con", 0, "transaction", "");
                            }
                            if (!bln)
                            {
                                if (errormsg.Length == 0)
                                {
                                    strValue = "Please Enter Valid Data in " + row["head_nm"].ToString();
                                }
                                else
                                {
                                    strValue = errormsg;
                                }
                                break;
                            }
                        }
                    }
                }
                if (strValue == "")
                {
                    if (objBLFD.Item_tbl_nm != "")
                    {
                        if (ObjBLFD.HTITEM.Count == 0)
                        {
                            strValue = "ITEM";
                        }
                        else
                        {
                            Hashtable HTITEMVal = ObjBLFD.HTITEM;
                            Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DictionaryEntry entry in HTITEMVal)
                            {
                                objBLFD.HTITEM_VALUE = (Hashtable)entry.Value;
                                htcuritem.Clear();
                                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                                {
                                    htcuritem.Add(entry1.Key, entry1.Value);
                                }
                                foreach (DictionaryEntry entry1 in htcuritem)
                                {
                                    DataRow[] row = null;
                                    if (ObjBLFD.dsFooter != null && ObjBLFD.dsFooter.Tables.Count != 0 && ObjBLFD.dsFooter.Tables[0].Rows.Count != 0)
                                    {
                                        //row = ObjBLFD.dsFooter.Tables[0].Select("fld_nm='" + entry1.Key + "' and valid_con<>'' and mandatory <> 0");
                                        row = ObjBLFD.dsFooter.Tables[0].Select("fld_nm='" + entry1.Key + "' and mandatory <> 0");
                                    }
                                    if (row != null && row.Length > 0)
                                    {
                                        bool bln = false;
                                        bln = ValidateFields(row[0], "_mon_con", 1, "transaction", entry.Key.ToString());
                                        if (bln && row[0]["valid_con"].ToString() != "")
                                        {
                                            bln = ValidateFields(row[0], "valid_con", 1, "transaction", entry.Key.ToString());
                                        }
                                        if (!bln)
                                        {
                                            if (errormsg.Length == 0)
                                            {
                                                strValue = "Please Enter Valid Data in " + row[0]["head_nm"].ToString();
                                            }
                                            else
                                            {
                                                strValue = errormsg;
                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        DataRow[] row1 = null;
                                        if (ObjBLFD.dsBASEADDIFIELDITEM != null && ObjBLFD.dsBASEADDIFIELDITEM.Tables.Count != 0 && ObjBLFD.dsBASEADDIFIELDITEM.Tables[0].Rows.Count != 0)
                                        {
                                            // row1 = ObjBLFD.dsBASEADDIFIELDITEM.Tables[0].Select("fld_nm='" + entry1.Key + "' and valid_con<>'' and mandatory <> 0");
                                            row1 = ObjBLFD.dsBASEADDIFIELDITEM.Tables[0].Select("fld_nm='" + entry1.Key + "' and mandatory <> 0");
                                        }
                                        if (row1 != null && row1.Length > 0)
                                        {
                                            bool bln = false;
                                            bln = ValidateFields(row1[0], "_mon_con", 1, "transaction", entry.Key.ToString());
                                            if (bln && row1[0]["valid_con"].ToString() != "")
                                            {
                                                bln = ValidateFields(row1[0], "valid_con", 1, "transaction", entry.Key.ToString());
                                            }
                                            // bool bln = ValidateFields(row1[0], 1, "transaction", entry.Key.ToString());
                                            if (!bln)
                                            {
                                                if (errormsg.Length == 0)
                                                {
                                                    strValue = "Please Enter Valid Data in " + row[0]["head_nm"].ToString();
                                                }
                                                else
                                                {
                                                    strValue = errormsg;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (strValue != "")
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (objBLFD.Tran_type == "Master")
            {
                if (ObjBLFD.dsBASEFIELDMAIN != null && ObjBLFD.dsBASEFIELDMAIN.Tables.Count != 0 && ObjBLFD.dsBASEFIELDMAIN.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in ObjBLFD.dsBASEFIELDMAIN.Tables[0].Rows)
                    {
                        if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")//&& row["valid_con"].ToString() != ""
                        {
                            //bool bln = ValidateFields(row, 0, "master", "");
                            bool bln = false;
                            bln = ValidateFields(row, "_mon_con", 0, "master", "");
                            if (bln && row["valid_con"].ToString() != "")
                            {
                                bln = ValidateFields(row, "valid_con", 0, "master", "");
                            }
                            if (!bln)
                            {
                                if (errormsg.Length == 0)
                                {
                                    strValue = "Please Enter Valid Data in " + row["head_nm"].ToString();
                                }
                                else
                                {
                                    strValue = errormsg;
                                }
                                break;
                            }
                        }
                    }
                }
                if (ObjBLFD.dsTab != null && ObjBLFD.dsTab.Tables.Count != 0 && ObjBLFD.dsTab.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row1 in ObjBLFD.dsTab.Tables[0].Rows)
                    {
                        ObjBLFD.dsTabDet = objFLMAST.GETMASTBASETABDETAILS(objBLFD.Code, row1["fld_nm"].ToString(), objBLFD.ObjCompany.Compid.ToString());
                        foreach (DataRow row in ObjBLFD.dsTabDet.Tables[0].Rows)
                        {
                            if (objBLFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")//&& row["valid_con"].ToString() != ""
                            {
                                //bool bln = ValidateFields(row, 0, "master", "");
                                bool bln = false;
                                bln = ValidateFields(row, "_mon_con", 0, "master", "");
                                if (bln && row["valid_con"].ToString() != "")
                                {
                                    bln = ValidateFields(row, "valid_con", 0, "master", "");
                                }
                                if (!bln)
                                {
                                    if (errormsg.Length == 0)
                                    {
                                        strValue = "Please Enter Valid Data in " + row["head_nm"].ToString();
                                    }
                                    else
                                    {
                                        strValue = errormsg;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
            }
            return strValue;
        }
        public bool ValidateFields(DataRow row, string con_nm, int flg = 0, string tran_type = "", string itserial = "")
        {
            objFL_GRIDEVENTS.objBASEFILEDS = ObjBLFD;

            string fld_nm = row["fld_nm"].ToString();

            string _strValue = "";

            bool validflg = true;

            string exp = row[con_nm].ToString();
            string[] ar = exp.Split(new Char[] { '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (ar.Length > 1)
            {
                if (ar.Length == 1)
                {
                    validflg = CallCustomMethod(ar[0], row["tbl_nm"].ToString(), fld_nm, _strValue, itserial, flg, tran_type);
                }
                else
                {
                    if (ar[0].Contains("!EMPTY"))
                    {
                        string[] cond = ar[0].Split(new string[] { "!EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                        _strValue = cod1;
                        if (cod1 == "")
                        {
                            validflg = false;
                        }
                        else
                        {
                            validflg = true;
                        }
                    }
                    else if (ar[0].Contains("EMPTY"))
                    {
                        string[] cond = ar[0].Split(new string[] { "EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                        _strValue = cod1;
                        if (cod1 == "")
                        {
                            validflg = true;
                        }
                        else
                        {
                            validflg = false;
                        }
                    }
                    else
                    {
                        string valu = ar[0].Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$").Replace("$=", "=$");
                        string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cond.Length == 3)
                        {
                            string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "decimal") : cond[0];
                            string cod2 = cond[2].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[2], "decimal") : cond[2];
                            switch (cond[1])
                            {
                                case "<": if (decimal.Parse(cod1) < decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                case ">": if (decimal.Parse(cod1) > decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                case "<=": if (decimal.Parse(cod1) <= decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                case ">=": if (decimal.Parse(cod1) >= decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                case "==": if (decimal.Parse(cod1) == decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                case "!=": if (decimal.Parse(cod1) != decimal.Parse(cod2)) validflg = true; else { validflg = false; } break;
                                default: validflg = false; break;
                            }
                        }
                    }

                    if (validflg)
                    {
                        if (ar[1].ToString().Trim().ToUpper() != "TRUE")
                        {
                            validflg = CallCustomMethod(ar[1], row["tbl_nm"].ToString(), fld_nm, _strValue, itserial, flg, tran_type);
                        }
                    }
                    if (!validflg)
                    {
                        if (ar[2].ToString().Trim().ToUpper() != "FALSE")
                        {
                            validflg = CallCustomMethod(ar[2], row["tbl_nm"].ToString(), fld_nm, _strValue, itserial, flg, tran_type);
                        }
                    }
                }

            }
            return validflg;
        }
        private bool CallCustomMethod(string _method_nm, string tbl_nm, string fld_nm, string fld_val, string itserial, int flg, string tran_type)
        {
            bool validflg = true;

            if (this.GetType().GetMethod(_method_nm) != null)
            {
                this.ObjBLFD = ObjBLFD;
                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                this.objSetFieldsValue = objSetFieldsValue;
                MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(_method_nm);
                validflg = bool.Parse(methodInfo.Invoke(this, null).ToString().Trim());
                if (validflg)
                {
                    ObjBLFD.HTMAIN = this.ObjBLFD.HTMAIN;
                    ObjBLFD.HTITEM = this.ObjBLFD.HTITEM;
                    // BindControlsFromView();
                }
            }
            else
            {
                if (tran_type == "master" && objiMASTER.GetType().GetMethod(_method_nm) != null)
                {
                    objiMASTER.ACTIVE_MASTER = objBLFD;
                    SetFieldsValue(tbl_nm, fld_nm, fld_val);
                    MethodInfo methodInfo = typeof(iMASTER).GetMethod(_method_nm);
                    validflg = bool.Parse(methodInfo.Invoke(objiMASTER, null).ToString().Trim());
                    if (validflg)
                    {
                        objBLFD.HTMAIN = objiMASTER.ACTIVE_MASTER.HTMAIN;
                        // BindControlsFromView();
                    }
                    else
                    {
                        if (objiMASTER.BL_FIELDS.Errormsg.Length != 0)
                        {
                            errormsg = objiMASTER.BL_FIELDS.Errormsg;
                        }
                    }
                }
                if (tran_type == "transaction" && objiTRANSACTION.GetType().GetMethod(_method_nm) != null)
                {
                    objiTRANSACTION.ACTIVE_TRANSACTION = ObjBLFD;
                    SetFieldsValue(tbl_nm, fld_nm, fld_val);
                    MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(_method_nm);
                    validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, null).ToString().Trim());
                    if (validflg)
                    {
                        ObjBLFD.HTMAIN = objiTRANSACTION.ACTIVE_TRANSACTION.HTMAIN;
                        ObjBLFD.HTITEM = objiTRANSACTION.ACTIVE_TRANSACTION.HTITEM;
                        // BindControlsFromView();
                    }
                    else
                    {
                        if (objiTRANSACTION.BL_FIELDS.Errormsg.Length != 0)
                        {
                            errormsg = objiTRANSACTION.BL_FIELDS.Errormsg;
                        }
                    }
                }
                else
                {
                    if (flg == 1)
                    {
                        if (tran_type == "transaction" && fld_nm.ToLower() == "prod_nm")
                        {
                            if (objiITEMVALID.GetType().GetMethod(_method_nm) != null)
                            {
                                objiITEMVALID.Hashitemvalue = ((Hashtable)ObjBLFD.HTITEM[itserial]);
                                objiITEMVALID.ACTIVE_BL = objBLFD;
                                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                                MethodInfo methodInfo = typeof(iITEMVALID).GetMethod(_method_nm);
                                validflg = bool.Parse(methodInfo.Invoke(objiITEMVALID, null).ToString().Trim());
                                if (validflg)
                                {
                                    Hashtable HTITEMVal = ObjBLFD.HTITEM;
                                    Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in HTITEMVal)
                                    {
                                        htcuritem.Clear();
                                        foreach (DictionaryEntry entry1 in objiITEMVALID.Hashitemvalue)
                                        {
                                            htcuritem.Add(entry1.Key, entry1.Value);
                                        }
                                        foreach (DictionaryEntry entry1 in htcuritem)
                                        {
                                            if (itserial == entry.Key.ToString())
                                            {
                                                if (((Hashtable)ObjBLFD.HTITEM[entry.Key]).Contains(entry1.Key))
                                                {
                                                    ((Hashtable)ObjBLFD.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                                                }
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (objiITEMVALID.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        errormsg = objiITEMVALID.BL_FIELDS.Errormsg;
                                    }
                                }
                            }
                        }
                        else if (tran_type == "transaction" && fld_nm.ToLower() == "qty")
                        {
                            if (objiQTYVALID.GetType().GetMethod(_method_nm) != null)
                            {
                                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                                objiQTYVALID.Hashqtyvalue = ((Hashtable)ObjBLFD.HTITEM[itserial]);
                                objiQTYVALID.ACTIVE_BL = objBLFD;
                                MethodInfo methodInfo = typeof(iQTYVALID).GetMethod(_method_nm);
                                validflg = bool.Parse(methodInfo.Invoke(objiQTYVALID, null).ToString().Trim());
                                if (validflg)
                                {
                                    Hashtable HTITEMVal = ObjBLFD.HTITEM;
                                    Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in HTITEMVal)
                                    {
                                        htcuritem.Clear();
                                        foreach (DictionaryEntry entry1 in objiQTYVALID.Hashqtyvalue)
                                        {
                                            htcuritem.Add(entry1.Key, entry1.Value);
                                        }
                                        foreach (DictionaryEntry entry1 in htcuritem)
                                        {
                                            if (itserial == entry.Key.ToString())
                                            {
                                                if (((Hashtable)ObjBLFD.HTITEM[entry.Key]).Contains(entry1.Key))
                                                {
                                                    ((Hashtable)ObjBLFD.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (objiQTYVALID.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        errormsg = objiQTYVALID.BL_FIELDS.Errormsg;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (objiGRIDITEM.GetType().GetMethod(_method_nm) != null)
                            {
                                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                                objiGRIDITEM.HashgridItemvalue = ((Hashtable)ObjBLFD.HTITEM[itserial]);
                                objiGRIDITEM.ACTIVE_BL = objBLFD;
                                MethodInfo methodInfo = typeof(iGRIDITEM).GetMethod(_method_nm);
                                validflg = bool.Parse(methodInfo.Invoke(objiGRIDITEM, null).ToString().Trim());
                                if (validflg)
                                {
                                    Hashtable HTITEMVal = ObjBLFD.HTITEM;
                                    Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in HTITEMVal)
                                    {
                                        htcuritem.Clear();
                                        foreach (DictionaryEntry entry1 in objiGRIDITEM.HashgridItemvalue)
                                        {
                                            htcuritem.Add(entry1.Key, entry1.Value);
                                        }
                                        foreach (DictionaryEntry entry1 in htcuritem)
                                        {
                                            if (itserial == entry.Key.ToString())
                                            {
                                                if (((Hashtable)ObjBLFD.HTITEM[entry.Key]).Contains(entry1.Key))
                                                {
                                                    ((Hashtable)ObjBLFD.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (objiGRIDITEM.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        errormsg = objiGRIDITEM.BL_FIELDS.Errormsg;
                                    }
                                }
                            }
                            else
                            {
                                validflg = false;
                            }
                        }
                    }
                }
            }
            if (validflg)
            {
                objSaveTranAndMaster.ACTIVE_BL = objBLFD;
                validflg = objSaveTranAndMaster.ValidateSave();
                if (!validflg)
                {
                    if (objSaveTranAndMaster.BL_FIELDS.Errormsg.Length != 0)
                    {
                        errormsg = objSaveTranAndMaster.BL_FIELDS.Errormsg;
                    }
                }
            }
            return validflg;
        }

        private void SetFieldsValue(string cur_tbl_nm, string fld_nm, string fld_val)
        {
            objSetFieldsValue.Fld_tbl_nm = cur_tbl_nm;
            objSetFieldsValue.Fld_nm = fld_nm;
            objSetFieldsValue.Fld_value = fld_val;
        }
        public bool GetCompanyName()
        {
            if (ObjBLFD.Tran_mode == "add_mode")
            {
                if (ObjBLFD.HTMAIN["COMP_NM"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_END"] != null && ObjBLFD.HTMAIN["FIN_YR_END"].ToString() != "")
                {
                    int count = objDLMAST.Get_Company_Name(ObjBLFD);
                    string comp_nm = ObjBLFD.HTMAIN["COMP_NM"].ToString().Substring(0, 1).ToUpper() + count.ToString().PadLeft(2, '0') + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).ToString("yy") + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).ToString("yy");
                    if (Find_DB_Existance(comp_nm))
                    {
                        comp_nm += objDLMAST.Find_DB_Count(comp_nm).ToString();
                    }
                    ObjBLFD.HTMAIN["DB_NM"] = comp_nm;
                    ObjBLFD.HTMAIN["FOLDER_NM"] = ObjBLFD.HTMAIN["DB_NM"];
                    ObjBLFD.HTMAIN["DIR_NM"] = objIni.GetKeyFieldValue("APP_PATH", "path") + @"\" + ObjBLFD.HTMAIN["DB_NM"].ToString();
                    // string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                    //ObjBLFD.HTMAIN["FIN_YR"] =DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).Year;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool GetFinacial_Year()
        {
            if (ObjBLFD.Tran_mode == "add_mode")
            {
                if (ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "" && ObjBLFD.HTMAIN["FIN_YR_END"] != null && ObjBLFD.HTMAIN["FIN_YR_END"].ToString() != "")
                {
                    ObjBLFD.HTMAIN["FIN_YR"] = DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year.ToString() + "-" + DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_END"].ToString()).Year.ToString();
                }
            }
            return true;
        }
        public bool AddFinacial_Year()
        {
            if (ObjBLFD.Tran_mode == "add_mode")
            {
                if (ObjBLFD.HTMAIN["FIN_YR_STA"] != null && ObjBLFD.HTMAIN["FIN_YR_STA"].ToString() != "")
                {
                    //ObjBLFD.HTMAIN["FIN_YR_END"] = DateTime.Parse("03/31/" + (DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).Year + 1).ToString()).ToString(); //DateTime.Parse("31" + "-03-" + DateTime.Now.AddYears(1).Year.ToString());//.ToString("yyyy/MM/dd");
                    ObjBLFD.HTMAIN["FIN_YR_END"] = (DateTime.Parse(ObjBLFD.HTMAIN["FIN_YR_STA"].ToString()).AddYears(1)).AddDays(-1).ToString("yyyy/MM/dd");
                }
            }
            //ObjBLFD.HTMAIN["FIN_YR_END"] = DateTime.Now;//.ToString("yyyy/MM/dd");
            return true;
        }
        public bool Find_Login_User(string user_nm, string pwd, string fin_yr, string comp_nm)
        {
            return objDLMAST.Find_Login_User(user_nm, pwd, fin_yr, comp_nm);
        }
        public bool Find_DB_Existance(string db_nm = "InodeMFG")
        {
            return objDLMAST.Find_DB_Existance(db_nm);
        }
        public bool Create_DataBase(string db_nm, string bak_up_nm)
        {
            return objDLMAST.Create_DataBase(db_nm, bak_up_nm);
        }
        public bool Create_BakUp(string db_nm, string path)
        {
            return objDLMAST.Create_BakUp(db_nm, path);
        }
        public bool CreateFolder(string floder_nm)
        {
            return objDLMAST.CreateFolder(floder_nm);
        }
        public DataSet GETFIN_YEAR()
        {
            return objDLMAST.GETFIN_YEAR();
        }
        public DataSet GET_Comp_by_Fin_yr(string fin_yr)
        {
            return objDLMAST.GET_Comp_by_Fin_yr(fin_yr);
        }
        public DataSet Get_Company_Details(string comp_id)
        {
            return objDLMAST.Get_Company_Details(comp_id);
        }
        public DataSet Get_Company_Details_Fin_Yr(string comp_id, string fin_yr)
        {
            return objDLMAST.Get_Company_Details_Fin_Yr(comp_id, fin_yr);
        }
        //
        public void GetHashTableModuleList()
        {
            objBLFD.HtModuleList.Clear();
            if (objBLFD.HTMAIN.Contains("module_cd") && objBLFD.HTMAIN["module_cd"] != null && objBLFD.HTMAIN["module_cd"].ToString() != "")
            {
                string[] str = objBLFD.HTMAIN["module_cd"].ToString().Split(new string[] { "+$IPTLM$+" }, StringSplitOptions.RemoveEmptyEntries);
                string[] strDecrypt = new string[str.Length];
                int i = 0;
                if (str.Length != 0)
                {
                    foreach (string s in str)
                    {
                        strDecrypt[i] = VALIDATIONLAYER.Decrypt(s);
                        i++;
                    }
                    foreach (string st in strDecrypt)
                    {
                        if (objBLFD.HtModuleList != null && !objBLFD.HtModuleList.Contains(st))
                        {
                            objBLFD.HtModuleList[st] = "1";
                        }
                    }
                }
            }
        }
        //
        #region 1.0
        public static string Encrypt(string plainText)
        {
            string passPhrase = "Inode@01";
            string saltValue = "salt@Inode";
            string hashAlgorithm = "SHA1";
            int passwordIterations = 2;
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 256;
            string cipherText = "";
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream = new MemoryStream();

                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                cipherText = Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in encryption : " + ex.Message);
            }
            return cipherText;
        }
        public static string Decrypt(string cipherText)
        {
            string passPhrase = "Inode@01";
            string saltValue = "salt@Inode";
            string hashAlgorithm = "SHA1";
            int passwordIterations = 2;
            string initVector = "@1B2c3D4e5F6g7H8";
            int keySize = 256;
            string plainText = "";
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);

                RijndaelManaged symmetricKey = new RijndaelManaged();

                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                // plainText = "";
            }
            return plainText;
        }
        #endregion

        public static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
    }
}
