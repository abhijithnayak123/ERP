using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA_DL
{
    public class DL_TRANSACTION
    {
        SqlConnection con;

        public Company objCompany = new Company();
        //SqlCommand com;
        DL_GENINVOICE objDL_GENINVOICE = new DL_GENINVOICE();
        DL_ADAPTER objDLAdapter = new DL_ADAPTER();

        Ini objIni = new Ini();
        private string _primary_key_nm, _main_tbl_nm, _item_tbl_nm, _ref_tbl_nm, _extra_tbl_nm, _ac_tbl_nm, _alloc_tbl_nm;

        public DL_TRANSACTION()
        {
        }
        private void GetCorrectCon()
        {
            objDLAdapter.objCompany = objCompany;
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }
        public string Save_Trasaction(BL_BASEFIELD objBLBASEFIELD)
        {
            try
            {
                GetCorrectCon();
                _primary_key_nm = GetPrimaryKeyFldNm(objBLBASEFIELD.Main_tbl_nm, objBLBASEFIELD.Tbl_catalog);
                _main_tbl_nm = objBLBASEFIELD.Main_tbl_nm;
                _item_tbl_nm = objBLBASEFIELD.Item_tbl_nm;
                _ref_tbl_nm = objBLBASEFIELD.Ref_tbl_nm;
                _extra_tbl_nm = objBLBASEFIELD.Extra_tbl_nm;
                _ac_tbl_nm = objBLBASEFIELD.Ac_tbl_nm;
                _alloc_tbl_nm = objBLBASEFIELD.Alloc_tbl_nm;
                return (Save_Main_Trasaction(objBLBASEFIELD));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Save_Main_Trasaction(BL_BASEFIELD objBLFD)
        {
            try
            {
                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult;

                foreach (DictionaryEntry entry in objBLFD.HTMAIN)
                {
                    if (entry.Key.ToString().ToLower() != _primary_key_nm.ToLower())
                    {
                        htFields.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        if (objBLFD.HTMAIN[_primary_key_nm.ToUpper()].ToString() == "")
                            htFieldscond.Add(entry.Key, "0");
                        else
                            htFieldscond.Add(entry.Key, entry.Value);
                    }
                }
                if (int.Parse(htFieldscond[_primary_key_nm.ToUpper()].ToString()) > 0)
                {
                    bResult = objDLAdapter.execUpdate(_main_tbl_nm, htFields, htFieldscond);
                }
                else
                {
                    bResult = objDLAdapter.execInsert(_main_tbl_nm, htFields);
                }
                if (bResult)
                {
                    if (objBLFD.Code == "OM" && objBLFD.Tran_mode == "add_mode")
                    {
                        Save_Company_Details(objBLFD);
                    }
                    int tran_id = getMainId(objBLFD.HTMAIN);
                    if (tran_id > 0)
                    {
                        objBLFD.Tran_id = tran_id.ToString();
                        if (objBLFD.Tran_type != "Accounting")
                        {
                            if (objBLFD.HTITEM.Count > 0)
                            {
                                if (objBLFD.Item_tbl_nm != "")
                                {
                                    bool itemflg = Save_Item_Trasaction(objBLFD.HTMAIN, objBLFD.HTITEM, tran_id);
                                    if (itemflg)
                                    {
                                        if (objBLFD.HTMAINREF != null && objBLFD.HTMAINREF.Count != 0)
                                        {
                                            bool refflg = Save_Reference_Transation(objBLFD.HTMAINREF, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString());
                                            if (!refflg)
                                            {
                                                return "Reference table updation failed";
                                            }
                                        }
                                        if (objBLFD.HtPur_Ref != null && objBLFD.HtPur_Ref.Count != 0)
                                        {
                                            //objBLFD.HtPur_Ref["tran_cd"] = objBLFD.Code;
                                            //objBLFD.HtPur_Ref["compid"] = objBLFD.ObjCompany.Compid;
                                            bool purrefflg = Save_Extra_Tbl_Transation(objBLFD.HtPur_Ref, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString(), objBLFD.ObjCompany.Compid.ToString());
                                            if (!purrefflg)
                                            {
                                                return "Purchase Reference table updation failed";
                                            }
                                        }
                                        if (objBLFD.HT_ACDET != null && objBLFD.HT_ACDET.Count != 0)
                                        {
                                            bool refflg = Save_Account_Details_Transation(objBLFD.HT_ACDET, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString(), objBLFD.Code, objBLFD.ObjCompany.Compid, objBLFD.ObjCompany.Fin_yr, objBLFD.HTMAIN["TRAN_SR"].ToString(), objBLFD.HTMAIN["TRAN_DT"].ToString());
                                            if (!refflg)
                                            {
                                                return "Posting table updation failed";
                                            }
                                        }
                                        if (objBLFD.HT_ALLOC != null && objBLFD.HT_ALLOC.Count != 0)
                                        {
                                            //bool refflg = Save_Account_Allocation_Details_Transation(objBLFD.HT_ALLOC, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString(), objBLFD.Code, objBLFD.ObjCompany.Compid, objBLFD.ObjCompany.Fin_yr, objBLFD.HTMAIN["TRAN_SR"].ToString(), objBLFD.HTMAIN["TRAN_DT"].ToString());
                                            bool refflg = Save_Account_Allocation_Details_Transation(objBLFD);
                                            if (!refflg)
                                            {
                                                return "Allocation table updation failed";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return "Item table Updation Failed";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (objBLFD.Ac_tbl_nm != "" && objBLFD.HT_ACDET != null && objBLFD.HT_ACDET.Count != 0)
                            {
                                bool refflg = Save_Account_Details_Transation(objBLFD.HT_ACDET, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString(), objBLFD.Code, objBLFD.ObjCompany.Compid, objBLFD.ObjCompany.Fin_yr, objBLFD.HTMAIN["TRAN_SR"].ToString(), objBLFD.HTMAIN["TRAN_DT"].ToString());
                                if (!refflg)
                                {
                                    return "Posting table updation failed";
                                }
                            }
                            if (objBLFD.Alloc_tbl_nm != "" && objBLFD.HT_ALLOC != null && objBLFD.HT_ALLOC.Count != 0)
                            {
                                bool refflg = Save_Account_Allocation_Details_Transation(objBLFD);//Save_Account_Allocation_Details_Transation(objBLFD.HT_ALLOC, tran_id, objBLFD.HTMAIN["TRAN_NO"].ToString(), objBLFD.Code, objBLFD.ObjCompany.Compid, objBLFD.ObjCompany.Fin_yr, objBLFD.HTMAIN["TRAN_SR"].ToString(), objBLFD.HTMAIN["TRAN_DT"].ToString());
                                if (!refflg)
                                {
                                    return "Allocation table updation failed";
                                }
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void Save_Company_Details(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();
                DataSet dsetComp = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select top 1 Compid,comp_nm,fin_yr from  " + objBLFD.Main_tbl_nm + " order by compid desc", con);
                da.Fill(dsetComp);
                if (dsetComp != null && dsetComp.Tables[0].Rows.Count != 0)
                {
                    SqlCommand cmdinsert;
                    cmdinsert = new SqlCommand("insert into LOGIN_MAST(user_nm,pwd,CompId,fin_yr,comp_nm,sec_code,tran_cd) values ('admin','inode','" + dsetComp.Tables[0].Rows[0]["Compid"].ToString() + "','" + dsetComp.Tables[0].Rows[0]["fin_yr"].ToString() + "','" + dsetComp.Tables[0].Rows[0]["Comp_nm"].ToString() + "','UrhzyWIW++JzUTKnIAisfQ==','UL')", con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();

                    string compid = Get_Company_Id(objBLFD);
                    objBLFD.CompId = compid;
                    Update_Company_ID_INODE(objBLFD);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public bool Save_Item_Trasaction(Hashtable HTMAIN, Hashtable HTITEM, int tran_id)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            string val = "";
            string serialnos = "";

            foreach (DictionaryEntry entry in HTITEM)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (serialnos == "")
                        serialnos += "'" + ((Hashtable)entry.Value)["ptserial"].ToString() + "'";
                    else
                        serialnos += ",'" + ((Hashtable)entry.Value)["ptserial"].ToString() + "'";
                    //bool flg = DELETE_ITEM(entry.Key.ToString(), HTMAIN[_primary_key_nm.Trim().ToUpper()].ToString(), HTMAIN["TRAN_CD"].ToString(), _primary_key_nm, _item_tbl_nm, _ref_tbl_nm, _extra_tbl_nm);
                }
            }

            bool flg = objDLAdapter.execDeleteQuery("delete from " + _item_tbl_nm + " where ptserial not in (" + serialnos + ") and " + _primary_key_nm + "='" + HTMAIN[_primary_key_nm.Trim().ToUpper()].ToString() + "' and TRAN_CD='" + HTMAIN["TRAN_CD"].ToString() + "' and compid='" + HTMAIN["COMPID"].ToString() + "' and fin_yr='" + HTMAIN["fin_yr"].ToString() + "'");

            foreach (DictionaryEntry entry in HTITEM)
            {
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    if (entry1.Key.ToString().ToLower() != "prod_no")
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
                    htFields[_primary_key_nm.ToUpper()] = tran_id.ToString();
                    htFieldscond[_primary_key_nm.ToUpper()] = tran_id.ToString();
                    if (FindItemExistance(tran_id, htFieldscond["PROD_NO"].ToString(), HTMAIN["COMPID"].ToString(), HTMAIN["fin_yr"].ToString()))
                    {
                        htFields.Remove(_primary_key_nm.ToUpper());
                        bResult = objDLAdapter.execUpdate(_item_tbl_nm, htFields, htFieldscond);
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
                        bResult = objDLAdapter.execInsert(_item_tbl_nm, htFields);
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
        private bool Save_Reference_Transation(Hashtable HTREF, int tran_id, string tran_no)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            foreach (DictionaryEntry entry in HTREF)
            {
                htFields.Clear();
                htFieldscond.Clear();
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    htFields.Add(entry1.Key, entry1.Value);
                }
                if (htFields.Count != 0)
                {
                    htFields[_primary_key_nm] = tran_id.ToString();
                    htFields["TRAN_NO"] = tran_no;
                    htFieldscond[_primary_key_nm] = htFields[_primary_key_nm].ToString();
                    htFieldscond["TRAN_CD"] = htFields["TRAN_CD"].ToString();
                    htFieldscond["PTSERIAL"] = htFields["PTSERIAL"].ToString();

                    if (FindRefExistance(_ref_tbl_nm, int.Parse(htFields[_primary_key_nm].ToString()), htFields["TRAN_CD"].ToString(), htFields["PTSERIAL"].ToString(), _primary_key_nm, "PTSERIAL"))
                    {
                        htFields.Remove(_primary_key_nm);
                        htFields.Remove("TRAN_CD");
                        htFields.Remove("PTSERIAL");
                        bResult = objDLAdapter.execUpdate(_ref_tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        bResult = objDLAdapter.execInsert(_ref_tbl_nm, htFields);
                        //objDL_GENINVOICE.Save_Gen_Miss(HTMAIN);
                    }
                }
            }
            return bResult;
        }

        private bool Save_Extra_Tbl_Transation(Hashtable HTEXTRA_TBL, int tran_id, string tran_no, string compid)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            foreach (DictionaryEntry entry in HTEXTRA_TBL)
            {
                htFields.Clear();
                htFieldscond.Clear();
                string pkfld_nm = GetPrimaryKeyFldNm(_extra_tbl_nm, "");
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    if (pkfld_nm != "" && entry1.Key.ToString().ToLower() == pkfld_nm.ToLower())
                    {
                    }
                    else
                    {
                        htFields.Add(entry1.Key, entry1.Value);
                    }
                }
                if (htFields.Count != 0)
                {
                    htFields[_primary_key_nm] = tran_id.ToString();
                    htFields["TRAN_NO"] = tran_no;
                    htFields["compid"] = compid;
                    htFieldscond[_primary_key_nm] = htFields[_primary_key_nm].ToString();
                    htFieldscond["TRAN_CD"] = htFields["TRAN_CD"].ToString();
                    htFieldscond["PTSERIAL"] = htFields["PTSERIAL"].ToString();
                    htFieldscond["entry_no"] = htFields["entry_no"].ToString();
                    htFieldscond["rgpageno"] = htFields["rgpageno"].ToString();

                    if (FindExtra_tbl_Existance(_extra_tbl_nm, int.Parse(htFields[_primary_key_nm].ToString()), htFields["TRAN_CD"].ToString(), htFields["PTSERIAL"].ToString(), htFields["entry_no"].ToString(), htFields["rgpageno"].ToString()))
                    {
                        htFields.Remove(_primary_key_nm);
                        htFields.Remove("TRAN_CD");
                        htFields.Remove("PTSERIAL");
                        htFields.Remove("entry_no");
                        htFields.Remove("rgpageno");
                        bResult = objDLAdapter.execUpdate(_extra_tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        bResult = objDLAdapter.execInsert(_extra_tbl_nm, htFields);
                    }
                }
            }
            return bResult;
        }

        private bool Save_Account_Details_Transation(Hashtable HTACDET, int tran_id, string tran_no, string tran_cd, int compid, string fin_yr, string tran_sr, string tran_dt)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;

            string serialnos = "";

            foreach (DictionaryEntry entry in HTACDET)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (serialnos == "")
                        serialnos += "'" + ((Hashtable)entry.Value)["acserial"].ToString() + "'";
                    else
                        serialnos += ",'" + ((Hashtable)entry.Value)["acserial"].ToString() + "'";
                    //bool flg = DELETE_ITEM(entry.Key.ToString(), HTMAIN[_primary_key_nm.Trim().ToUpper()].ToString(), HTMAIN["TRAN_CD"].ToString(), _primary_key_nm, _item_tbl_nm, _ref_tbl_nm, _extra_tbl_nm);
                }
            }

            bool flg = objDLAdapter.execDeleteQuery("delete from " + _ac_tbl_nm + " where acserial not in (" + serialnos + ") and " + _primary_key_nm + "='" + tran_id + "' and TRAN_CD='" + tran_cd + "' and compid='" + compid + "' and fin_yr='" + fin_yr + "'");

            foreach (DictionaryEntry entry in HTACDET)
            {
                htFields.Clear();
                htFieldscond.Clear();
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    //htFields.Add(entry1.Key, entry1.Value);
                    if (entry1.Key.ToString().ToLower() != "acc_no")
                    {
                        htFields.Add(entry1.Key, entry1.Value != null && entry1.Value.ToString() != "" ? entry1.Value.ToString() : "0");
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
                    htFields["compid"] = compid.ToString();
                    htFields["fin_yr"] = fin_yr;
                    htFields[_primary_key_nm] = tran_id.ToString();
                    htFields["TRAN_NO"] = tran_no;
                    htFields["TRAN_CD"] = tran_cd;
                    htFields["TRAN_SR"] = tran_sr;
                    htFields["TRAN_DT"] = tran_dt;

                    htFieldscond[_primary_key_nm] = htFields[_primary_key_nm].ToString();
                    htFieldscond["TRAN_CD"] = htFields["TRAN_CD"].ToString();
                    htFieldscond["acserial"] = htFields["acserial"].ToString();

                    if (FindAccountExistance(_ac_tbl_nm, int.Parse(htFields[_primary_key_nm].ToString()), htFields["TRAN_CD"].ToString(), htFields["acserial"].ToString(), _primary_key_nm, "acserial"))
                    {
                        htFields.Remove(_primary_key_nm);
                        htFields.Remove("TRAN_CD");
                        htFields.Remove("acserial");
                        htFields.Remove("acc_no");
                        bResult = objDLAdapter.execUpdate(_ac_tbl_nm, htFields, htFieldscond);
                    }
                    else
                    {
                        htFields.Remove("acc_no");
                        bResult = objDLAdapter.execInsert(_ac_tbl_nm, htFields);
                    }
                }
            }
            return bResult;
        }

        //private bool Save_Account_Allocation_Details_Transation(Hashtable HT_AC_ALLOC_DET, int tran_id, string tran_no, string tran_cd, int compid, string fin_yr, string tran_sr, string tran_dt)
        private bool Save_Account_Allocation_Details_Transation(BL_BASEFIELD objBLFD)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldsDelete = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;


            string serialnos = "", _alloc_acserial_nm = "", strDeleteKey = "";
            string _alloc_tbl_name = "", _alloc_acserial = "", _alloc_tran_id = "0", _alloc_tran_cd = "", _alloc_tran_no = "", _alloc_tran_sr = "", _save_tbl_nm = "";
            string _ref_alloc_acserial = "", _ref_alloc_tran_id = "0", _ref_alloc_tran_cd = "", _ref_alloc_tran_no = "", _ref_alloc_tran_sr = "";
            DateTime _alloc_tran_dt = DateTime.Now, _ref_alloc_tran_dt = DateTime.Now;
            DataSet dsetAlloc = new DataSet();
            _save_tbl_nm = _alloc_tbl_nm;

            foreach (DictionaryEntry entry in objBLFD.HT_ALLOC)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    serialnos = "";
                    _save_tbl_nm = _alloc_tbl_nm;

                    _alloc_acserial_nm = "acserial";
                    _alloc_tran_id = objBLFD.Tran_id;
                    _alloc_tran_cd = objBLFD.Code;
                    _alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                    _alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                    _alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                    _alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();

                    _ref_alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                    _ref_alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                    _ref_alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                    _ref_alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                    _ref_alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();
                    _ref_alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;

                    if (objBLFD.Tran_mode != "add_mode")
                    {
                        _alloc_tbl_name = _alloc_tbl_nm;
                        if (objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS != null)
                        {
                            if (((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code]) != null && ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code]).Count != 0)
                            {
                                _alloc_tbl_name = ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code])["alloc_tbl_nm"] != null ? ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code])["alloc_tbl_nm"].ToString() : _alloc_tbl_nm;
                            }
                        }
                        dsetAlloc = objDLAdapter.dsquery("select " + _primary_key_nm + " from " + _alloc_tbl_name + " where " + _primary_key_nm + "=" + objBLFD.Tran_id + " and tran_cd='" + objBLFD.Code + "' and acserial='" + ((Hashtable)entry.Value)["acserial"].ToString() + "' and ref_tran_id=" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + " and ref_tran_cd='" + ((Hashtable)entry.Value)["ref_tran_cd"].ToString() + "' and ref_acserial='" + ((Hashtable)entry.Value)["ref_acserial"].ToString() + "'");
                        if (dsetAlloc != null && dsetAlloc.Tables.Count != 0 && dsetAlloc.Tables[0].Rows.Count != 0)
                        {
                            _save_tbl_nm = _alloc_tbl_name;
                            _alloc_acserial_nm = "acserial";
                            _alloc_tran_id = objBLFD.Tran_id;
                            _alloc_tran_cd = objBLFD.Code;
                            _alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                            _alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                            _alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                            _alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();

                            _ref_alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                            _ref_alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                            _ref_alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                            _ref_alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                            _ref_alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();
                            _ref_alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;
                        }

                        if (objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS != null)
                        {
                            if (((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()]) != null && ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()]).Count != 0)
                            {
                                _alloc_tbl_name = ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()])["alloc_tbl_nm"] != null ? ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()])["alloc_tbl_nm"].ToString() : _alloc_tbl_nm;
                            }
                        }

                        dsetAlloc = objDLAdapter.dsquery("select " + _primary_key_nm + " from " + _alloc_tbl_name + " where " + _primary_key_nm + "=" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + " and tran_cd='" + ((Hashtable)entry.Value)["ref_tran_cd"].ToString() + "' and acserial='" + ((Hashtable)entry.Value)["ref_acserial"].ToString() + "' and ref_tran_id=" + objBLFD.Tran_id + " and ref_tran_cd='" + objBLFD.Code + "' and ref_acserial='" + ((Hashtable)entry.Value)["acserial"].ToString() + "'");
                        if (dsetAlloc != null && dsetAlloc.Tables.Count != 0 && dsetAlloc.Tables[0].Rows.Count != 0)
                        {
                            _save_tbl_nm = _alloc_tbl_name;
                            _alloc_acserial_nm = "ref_acserial";
                            _alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                            _alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                            _alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                            _alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                            _alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;
                            _alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();

                            _ref_alloc_tran_id = objBLFD.Tran_id;
                            _ref_alloc_tran_cd = objBLFD.Code;
                            _ref_alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                            _ref_alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                            _ref_alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();
                            _ref_alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                        }

                        if (serialnos == "")
                            serialnos += "'" + ((Hashtable)entry.Value)[_alloc_acserial_nm].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)[_alloc_acserial_nm].ToString() + "'";

                        strDeleteKey = _alloc_tran_id.ToString() + _alloc_tran_cd;// + _alloc_acserial;
                        if (!htFieldsDelete.Contains(strDeleteKey))
                        {
                            htFieldsDelete[strDeleteKey] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            ((Hashtable)htFieldsDelete[strDeleteKey])["tbl_nm"] = _save_tbl_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] = serialnos;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_key"] = _primary_key_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_value"] = _alloc_tran_id;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["TRAN_CD"] = _alloc_tran_cd;
                        }
                        else
                        {
                           // ((Hashtable)htFieldsDelete[strDeleteKey])["tbl_nm"] = _save_tbl_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] = ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] + "," + serialnos;
                            //((Hashtable)htFieldsDelete[strDeleteKey])["primary_key"] = _primary_key_nm;
                            //((Hashtable)htFieldsDelete[strDeleteKey])["primary_value"] = _alloc_tran_id;
                            //((Hashtable)htFieldsDelete[strDeleteKey])["TRAN_CD"] = _alloc_tran_cd;
                        }
                    }
                }
            }

            foreach (DictionaryEntry entryDelete in htFieldsDelete)
            {
                if (((Hashtable)entryDelete.Value) != null && ((Hashtable)entryDelete.Value).Count != 0)
                {
                    objDLAdapter.execDeleteQuery("delete from " + ((Hashtable)entryDelete.Value)["tbl_nm"] + " where acserial not in (" + ((Hashtable)entryDelete.Value)["acserial"] + ") and " + ((Hashtable)entryDelete.Value)["primary_key"] + "='" + ((Hashtable)entryDelete.Value)["primary_value"].ToString() + "' and TRAN_CD='" + ((Hashtable)entryDelete.Value)["TRAN_CD"] + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "' and fin_yr='" + objBLFD.ObjCompany.Fin_yr + "'");
                }
            }

            foreach (DictionaryEntry entry in objBLFD.HT_ALLOC)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    serialnos = "";
                    _save_tbl_nm = _alloc_tbl_nm;

                    _alloc_acserial_nm = "acserial";
                    _alloc_tran_id = objBLFD.Tran_id;
                    _alloc_tran_cd = objBLFD.Code;
                    _alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                    _alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                    _alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                    _alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();

                    _ref_alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                    _ref_alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                    _ref_alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                    _ref_alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                    _ref_alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();
                    _ref_alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;

                    if (objBLFD.Tran_mode != "add_mode")
                    {
                        _alloc_tbl_name = _alloc_tbl_nm;
                        if (objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS != null)
                        {
                            if (((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code]) != null && ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code]).Count != 0)
                            {
                                _alloc_tbl_name = ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code])["alloc_tbl_nm"] != null ? ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[objBLFD.Code])["alloc_tbl_nm"].ToString() : _alloc_tbl_nm;
                            }
                        }
                        dsetAlloc = objDLAdapter.dsquery("select " + _primary_key_nm + " from " + _alloc_tbl_name + " where " + _primary_key_nm + "=" + objBLFD.Tran_id + " and tran_cd='" + objBLFD.Code + "' and acserial='" + ((Hashtable)entry.Value)["acserial"].ToString() + "' and ref_tran_id=" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + " and ref_tran_cd='" + ((Hashtable)entry.Value)["ref_tran_cd"].ToString() + "' and ref_acserial='" + ((Hashtable)entry.Value)["ref_acserial"].ToString() + "'");
                        if (dsetAlloc != null && dsetAlloc.Tables.Count != 0 && dsetAlloc.Tables[0].Rows.Count != 0)
                        {
                            _save_tbl_nm = _alloc_tbl_name;
                            _alloc_acserial_nm = "acserial";
                            _alloc_tran_id = objBLFD.Tran_id;
                            _alloc_tran_cd = objBLFD.Code;
                            _alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                            _alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                            _alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                            _alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();

                            _ref_alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                            _ref_alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                            _ref_alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                            _ref_alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                            _ref_alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();
                            _ref_alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;
                        }

                        if (objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS != null)
                        {
                            if (((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()]) != null && ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()]).Count != 0)
                            {
                                _alloc_tbl_name = ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()])["alloc_tbl_nm"] != null ? ((Hashtable)objBLFD.ObjBL_TRAN_SET.HT_TRAN_TBLS[((Hashtable)entry.Value)["ref_tran_cd"].ToString()])["alloc_tbl_nm"].ToString() : _alloc_tbl_nm;
                            }
                        }

                        dsetAlloc = objDLAdapter.dsquery("select " + _primary_key_nm + " from " + _alloc_tbl_name + " where " + _primary_key_nm + "=" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + " and tran_cd='" + ((Hashtable)entry.Value)["ref_tran_cd"].ToString() + "' and acserial='" + ((Hashtable)entry.Value)["ref_acserial"].ToString() + "' and ref_tran_id=" + objBLFD.Tran_id + " and ref_tran_cd='" + objBLFD.Code + "' and ref_acserial='" + ((Hashtable)entry.Value)["acserial"].ToString() + "'");
                        if (dsetAlloc != null && dsetAlloc.Tables.Count != 0 && dsetAlloc.Tables[0].Rows.Count != 0)
                        {
                            _save_tbl_nm = _alloc_tbl_name;
                            _alloc_acserial_nm = "ref_acserial";
                            _alloc_tran_id = ((Hashtable)entry.Value)["ref_tran_id"].ToString();
                            _alloc_tran_cd = ((Hashtable)entry.Value)["ref_tran_cd"].ToString();
                            _alloc_tran_no = ((Hashtable)entry.Value)["ref_tran_no"].ToString();
                            _alloc_tran_sr = ((Hashtable)entry.Value)["ref_tran_sr"].ToString();
                            _alloc_tran_dt = ((Hashtable)entry.Value)["ref_tran_dt"] != null && ((Hashtable)entry.Value)["ref_tran_dt"].ToString() != "" ? DateTime.Parse(((Hashtable)entry.Value)["ref_tran_dt"].ToString()) : DateTime.Now;
                            _alloc_acserial = ((Hashtable)entry.Value)["ref_acserial"].ToString();

                            _ref_alloc_tran_id = objBLFD.Tran_id;
                            _ref_alloc_tran_cd = objBLFD.Code;
                            _ref_alloc_tran_no = objBLFD.HTMAIN["tran_no"].ToString();
                            _ref_alloc_tran_sr = objBLFD.HTMAIN["tran_sr"].ToString();
                            _ref_alloc_acserial = ((Hashtable)entry.Value)["acserial"].ToString();
                            _ref_alloc_tran_dt = objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? DateTime.Parse(objBLFD.HTMAIN["tran_dt"].ToString()) : DateTime.Now;
                        }

                        if (serialnos == "")
                            serialnos += "'" + ((Hashtable)entry.Value)[_alloc_acserial_nm].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)[_alloc_acserial_nm].ToString() + "'";

                        strDeleteKey = _alloc_tran_id.ToString() + _alloc_tran_cd;
                        if (!htFieldsDelete.Contains(strDeleteKey))
                        {
                            htFieldsDelete = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            ((Hashtable)htFieldsDelete[strDeleteKey])["tbl_nm"] = _save_tbl_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] = serialnos;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_key"] = _primary_key_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_value"] = _alloc_tran_id;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["TRAN_CD"] = _alloc_tran_cd;
                        }
                        else
                        {
                            ((Hashtable)htFieldsDelete[strDeleteKey])["tbl_nm"] = _save_tbl_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] = ",'" + ((Hashtable)htFieldsDelete[strDeleteKey])["acserial"] + "'";
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_key"] = _primary_key_nm;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["primary_value"] = _alloc_tran_id;
                            ((Hashtable)htFieldsDelete[strDeleteKey])["TRAN_CD"] = _alloc_tran_cd;
                        }
                    }

                    htFields.Clear();
                    htFieldscond.Clear();
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        //htFields.Add(entry1.Key, entry1.Value);
                        if (entry1.Key.ToString().ToLower() != "ac_alloc_id")
                        {
                            htFields.Add(entry1.Key, entry1.Value != null && entry1.Value.ToString() != "" ? entry1.Value.ToString() : "0");
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
                        htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                        htFields[_primary_key_nm] = _alloc_tran_id;
                        htFields["TRAN_NO"] = _alloc_tran_no;
                        htFields["TRAN_CD"] = _alloc_tran_cd;
                        htFields["TRAN_SR"] = _alloc_tran_sr;
                        htFields["TRAN_DT"] = _alloc_tran_dt;

                        htFields["REF_TRAN_ID"] = _ref_alloc_tran_id;
                        htFields["REF_TRAN_NO"] = _ref_alloc_tran_no;
                        htFields["REF_TRAN_CD"] = _ref_alloc_tran_cd;
                        htFields["REF_TRAN_SR"] = _ref_alloc_tran_sr;
                        htFields["REF_TRAN_DT"] = _ref_alloc_tran_dt;
                        htFields["REF_ACSERIAL"] = _ref_alloc_acserial;

                        htFieldscond[_primary_key_nm] = htFields[_primary_key_nm].ToString();
                        htFieldscond["TRAN_CD"] = htFields["TRAN_CD"].ToString();
                        htFieldscond["acserial"] = _alloc_acserial;//htFields["acserial"].ToString();

                        //if (FindAccountAllocationExistance(_save_tbl_nm, _ref_alloc_tran_id, _ref_alloc_tran_cd, _ref_alloc_acserial, int.Parse(htFields[_primary_key_nm].ToString()), htFields["TRAN_CD"].ToString(), htFields["acserial"].ToString(), _primary_key_nm, "acserial"))
                        if (FindAccountAllocationExistance(_save_tbl_nm, _ref_alloc_tran_id, _ref_alloc_tran_cd, _ref_alloc_acserial, int.Parse(_alloc_tran_id), _alloc_tran_cd, _alloc_acserial, _primary_key_nm, "acserial"))
                        {
                            htFields.Remove(_primary_key_nm);
                            htFields.Remove("TRAN_CD");
                            htFields.Remove("acserial");
                            htFields.Remove("ac_alloc_id");
                            bResult = objDLAdapter.execUpdate(_save_tbl_nm, htFields, htFieldscond);
                        }
                        else
                        {
                            htFields.Remove("ac_alloc_id");
                            bResult = objDLAdapter.execInsert(_save_tbl_nm, htFields);
                        }
                    }

                    //bool flg = objDLAdapter.execDeleteQuery("delete from " + _alloc_tbl_nm + " where acserial not in (" + serialnos + ") and " + _primary_key_nm + "='" + tran_id + "' and TRAN_CD='" + tran_cd + "' and compid='" + compid + "' and fin_yr='" + fin_yr + "'");
                }
            }
            #region
            //foreach (DictionaryEntry entry in objBLFD.HT_ALLOC)
            //{
            //    htFields.Clear();
            //    htFieldscond.Clear();
            //    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
            //    {
            //        //htFields.Add(entry1.Key, entry1.Value);
            //        if (entry1.Key.ToString().ToLower() != "ac_alloc_id")
            //        {
            //            htFields.Add(entry1.Key, entry1.Value != null && entry1.Value.ToString() != "" ? entry1.Value.ToString() : "0");
            //        }
            //        else
            //        {
            //            if (entry1.Value.ToString() == "")
            //                htFieldscond.Add(entry1.Key, "0");
            //            else
            //                htFieldscond.Add(entry1.Key, entry1.Value);
            //        }
            //    }
            //    if (htFields.Count != 0)
            //    {
            //        htFields["compid"] = compid.ToString();
            //        htFields["fin_yr"] = fin_yr;
            //        htFields[_primary_key_nm] = tran_id.ToString();
            //        htFields["TRAN_NO"] = tran_no;
            //        htFields["TRAN_CD"] = tran_cd;
            //        htFields["TRAN_SR"] = tran_sr;
            //        htFields["TRAN_DT"] = tran_dt;

            //        htFieldscond[_primary_key_nm] = htFields[_primary_key_nm].ToString();
            //        htFieldscond["TRAN_CD"] = htFields["TRAN_CD"].ToString();
            //        htFieldscond["acserial"] = htFields["acserial"].ToString();

            //        if (FindAccountExistance(_alloc_tbl_nm, int.Parse(htFields[_primary_key_nm].ToString()), htFields["TRAN_CD"].ToString(), htFields["acserial"].ToString(), _primary_key_nm, "acserial"))
            //        {
            //            htFields.Remove(_primary_key_nm);
            //            htFields.Remove("TRAN_CD");
            //            htFields.Remove("acserial");
            //            htFields.Remove("ac_alloc_id");
            //            bResult = objDLAdapter.execUpdate(_alloc_tbl_nm, htFields, htFieldscond);
            //        }
            //        else
            //        {
            //            htFields.Remove("ac_alloc_id");
            //            bResult = objDLAdapter.execInsert(_alloc_tbl_nm, htFields);
            //        }
            //    }
            //}
            #endregion
            return bResult;
        }

        public bool FindRefExistance(string tbl_nm, int tran_id, string tran_cd, string ptserial, string prim_key, string serial)
        {
            GetCorrectCon();
            string strSql = String.Format("SELECT count(*) cnt from  " + tbl_nm + "  where " + prim_key + "='{0}' AND [tran_cd]='{1}' and " + serial + "='{2}'", tran_id.ToString(), tran_cd, ptserial);
            DataSet ds = objDLAdapter.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool FindAccountExistance(string tbl_nm, int tran_id, string tran_cd, string ac_nm_value, string prim_key, string ac_nm_key)
        {
            GetCorrectCon();
            string strSql = String.Format("SELECT count(*) cnt from  " + tbl_nm + "  where " + prim_key + "='{0}' AND [tran_cd]='{1}' and " + ac_nm_key + "='{2}'", tran_id.ToString(), tran_cd, ac_nm_value);
            DataSet ds = objDLAdapter.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool FindAccountAllocationExistance(string tbl_nm, string ref_tran_id, string ref_tran_cd, string ref_acserial, int tran_id, string tran_cd, string ac_nm_value, string prim_key, string ac_nm_key)
        {
            GetCorrectCon();
            string strSql = String.Format("SELECT count(*) cnt from  " + tbl_nm + "  where " + prim_key + "='{0}' AND [tran_cd]='{1}' and " + ac_nm_key + "='{2}' and ref_tran_id='{3}' and ref_tran_cd='{4}' and ref_acserial='{5}' ", tran_id.ToString(), tran_cd, ac_nm_value, ref_tran_id, ref_tran_cd, ref_acserial);
            DataSet ds = objDLAdapter.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public string GetPrimaryKeyFldNm(string tbl_nm, string catalog)
        {
            GetCorrectCon();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tbl_nm + "'", con);
            da.Fill(ds);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count != 0)
            {
                return ds.Tables[0].Rows[0]["column_name"].ToString();
            }
            return "";
        }
        public bool FindExtra_tbl_Existance(string tbl_nm, int tran_id, string tran_cd, string ptserial, string entryno, string rgpageno)
        {
            GetCorrectCon();
            objDLAdapter.objCompany = objCompany;
            string strSql = String.Format("SELECT count(*) cnt from  " + tbl_nm + "  where tran_id='{0}' AND [tran_cd]='{1}' and ptserial='{2}' and entry_no='{3}' and rgpageno='{4}'", tran_id.ToString(), tran_cd, ptserial, entryno, rgpageno);
            DataSet ds = objDLAdapter.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool FindItemExistance(int tran_id, string item_no, string compid, string fin_yr)
        {
            GetCorrectCon();
            string strSql = String.Format("SELECT count(*) cnt from  " + _item_tbl_nm + "  where " + _primary_key_nm + "='{0}' AND [PROD_NO]='{1}' and compid='{2}' and fin_yr='{3}'", tran_id.ToString(), item_no, compid, fin_yr);
            DataSet ds = objDLAdapter.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public int getMainId(Hashtable HTMAIN)
        {
            GetCorrectCon();
            int tran_id = 0;
            string strSql = String.Format("SELECT  " + _primary_key_nm + "  from " + _main_tbl_nm + " where TRAN_CD='{0}' AND [tran_no]='{1}' AND [TRAN_SR]='{2}' and compid='{3}' and fin_yr='{4}'", HTMAIN["TRAN_CD"].ToString(), HTMAIN["TRAN_NO"].ToString(), HTMAIN["TRAN_SR"].ToString(), HTMAIN["COMPID"].ToString(), HTMAIN["FIN_YR"].ToString());
            ArrayList arRes = objDLAdapter.SelectQueryFixed(strSql);
            if (arRes.Count > 0)
            {
                ArrayList arRow = arRes[0] as ArrayList;
                tran_id = int.Parse(arRow[0].ToString());
                arRes.Clear();
                arRow.Clear();
            }
            return tran_id;
        }
        public DataSet GET_ALL_NAVIGATION_DATA(BL_BASEFIELD objBLBASEFIELD, string condition)
        {
            GetCorrectCon();
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            string strQuery = "";

            if (objBLBASEFIELD.Tran_type == "Transaction")
            {
                strQuery = "select " + objBLBASEFIELD.Primary_id + ",tran_cd from  " + objBLBASEFIELD.Main_tbl_nm + "  where ";
                if (condition != "")
                {
                    strQuery += condition + "and ";
                }
                strQuery += "tran_cd='" + objBLBASEFIELD.Code + "' and compid='" + objBLBASEFIELD.ObjCompany.Compid.ToString() + "' and fin_yr='" + objBLBASEFIELD.ObjCompany.Fin_yr.ToString() + "' order by DATEADD(dd, 0, DATEDIFF(dd, 0, tran_dt)),tran_no";
            }
            else
            {
                strQuery = "select " + objBLBASEFIELD.Primary_id + ",tran_cd from  " + objBLBASEFIELD.Main_tbl_nm + "  where ";
                if (condition != "")
                {
                    strQuery += condition + "and ";
                }
                strQuery += "tran_cd='" + objBLBASEFIELD.Code + "' and compid='" + objBLBASEFIELD.ObjCompany.Compid.ToString() + "' order by " + objBLBASEFIELD.Primary_id;
            }
            da = new SqlDataAdapter(strQuery, con);
            da.Fill(ds);
            return ds;
        }
        public DataSet GET_MASTER_DATA(BL_BASEFIELD objBLBASEFIELD)
        {
            GetCorrectCon();
            DataSet ds = new DataSet();
            _primary_key_nm = GetPrimaryKeyFldNm(objBLBASEFIELD.Main_tbl_nm, objBLBASEFIELD.Tbl_catalog);
            SqlDataAdapter da;
            if (objBLBASEFIELD.Code != "OM")
            {
                da = new SqlDataAdapter("select * from  " + objBLBASEFIELD.Main_tbl_nm + " where " + objBLBASEFIELD.Primary_id + "=" + objBLBASEFIELD.Tran_id + " and compid='" + objBLBASEFIELD.ObjCompany.Compid.ToString() + "' order by " + _primary_key_nm, con);
            }
            else
            {
                da = new SqlDataAdapter("select * from  " + objBLBASEFIELD.Main_tbl_nm + " where " + objBLBASEFIELD.Primary_id + "=" + objBLBASEFIELD.Tran_id + " order by " + _primary_key_nm, con);
            }
            da.SelectCommand.Parameters.Add("@compid", SqlDbType.Int).Value = objCompany.Compid;
            //AddCompanyIdParamtoDataAdapter(da);
            da.Fill(ds);
            return ds;
        }
        public bool DELETE_TRANSACTION(BL_BASEFIELD objBLFIELD)
        {
            GetCorrectCon();
            bool flg = false;
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            htFieldscond.Add(objBLFIELD.Primary_id.Trim().ToUpper(), objBLFIELD.HTMAIN[objBLFIELD.Primary_id].ToString());
            htFieldscond.Add("TRAN_CD", objBLFIELD.Code);
            if (objBLFIELD.HTMAIN[objBLFIELD.Primary_id].ToString() != "")
            {
                try
                {
                    if (objBLFIELD.Item_tbl_nm != null && objBLFIELD.Item_tbl_nm != "")
                    {
                        flg = objDLAdapter.execDelete(objBLFIELD.Item_tbl_nm, htFieldscond);
                    }
                    else flg = true;
                    if (flg)
                    {
                        if (flg && objBLFIELD.Ref_tbl_nm != null && objBLFIELD.Ref_tbl_nm != "")
                        {
                            flg = objDLAdapter.execDelete(objBLFIELD.Ref_tbl_nm, htFieldscond);
                        }
                        if (flg && objBLFIELD.Extra_tbl_nm != null && objBLFIELD.Extra_tbl_nm != "")
                        {
                            flg = objDLAdapter.execDelete(objBLFIELD.Extra_tbl_nm, htFieldscond);
                        }
                        if (flg && objBLFIELD.Ac_tbl_nm != null && objBLFIELD.Ac_tbl_nm != "")
                        {
                            flg = objDLAdapter.execDelete(objBLFIELD.Ac_tbl_nm, htFieldscond);
                        }
                        if (flg && objBLFIELD.Alloc_tbl_nm != null && objBLFIELD.Alloc_tbl_nm != "")
                        {
                            flg = objDLAdapter.execDelete(objBLFIELD.Alloc_tbl_nm, htFieldscond);
                        }
                        if (flg)
                        {
                            flg = objDLAdapter.execDelete(objBLFIELD.Main_tbl_nm, htFieldscond);
                        }
                    }
                }
                catch (Exception ex)
                {
                    flg = false;

                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                    {
                        File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                    }
                    StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                    sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                    try
                    {
                        sw.WriteLine(ex.Message);
                    }
                    catch (Exception ex1)
                    {
                        sw.WriteLine("text file not found" + ex1.Message);
                    }
                    sw.WriteLine("-------------------------------------------end------------------------------------------------");
                    sw.Close();
                }
            }
            return flg;
        }
        //public bool DELETE_ITEM(string itserial, string tran_id, string tran_cd, string tbl_fld_name, string item_tbl_nm, string _ref_tbl_nm, string extra_tbl_nm)
        //{
        //    GetCorrectCon();
        //    bool flg = false;
        //    Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        //    htFieldscond.Add(tbl_fld_name.Trim().ToUpper(), tran_id);
        //    htFieldscond.Add("TRAN_CD", tran_cd);
        //    htFieldscond.Add("PTSERIAL", itserial);
        //    if (tran_id != "")
        //    {
        //        try
        //        {
        //            flg = objDLAdapter.execDelete(item_tbl_nm, htFieldscond);
        //            if (flg)
        //            {
        //                if (_ref_tbl_nm != "")
        //                {
        //                    flg = objDLAdapter.execDelete(_ref_tbl_nm, htFieldscond);
        //                }
        //                if (extra_tbl_nm != "")
        //                {
        //                    flg = objDLAdapter.execDelete(extra_tbl_nm, htFieldscond);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            flg = false;
        //        }
        //    }
        //    return flg;
        //}
        public bool DELETE_MASTER(BL_BASEFIELD objBLFIELD)
        {
            GetCorrectCon();
            bool flg = false;
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            htFieldscond.Add(objBLFIELD.Primary_id.Trim().ToUpper(), objBLFIELD.HTMAIN[objBLFIELD.Primary_id].ToString());
            htFieldscond.Add("TRAN_CD", objBLFIELD.Code);
            if (objBLFIELD.HTMAIN[objBLFIELD.Primary_id].ToString() != "")
            {
                try
                {
                    flg = objDLAdapter.execDelete(objBLFIELD.Main_tbl_nm, htFieldscond);
                }
                catch
                {
                    flg = false;
                }
            }
            return flg;
        }
        public bool Find_Deleting_Fld_Usaged_In_OtherTbl(BL_BASEFIELD objBLFD)
        {
            GetCorrectCon();
            DataSet dsForeignKey = new DataSet();
            SqlCommand cmd = new SqlCommand("ISP_FIND_DELETING_FOREIGN_KEYS", con);
            cmd.Parameters.Add(new SqlParameter("@SearchStr", objBLFD.Tran_id));
            cmd.Parameters.Add(new SqlParameter("@fld_nm", objBLFD.Primary_id));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsForeignKey);
            if (dsForeignKey != null && dsForeignKey.Tables[0].Rows.Count != 0)
            {
                return false;
            }
            return true;
        }
        public bool Find_Reference(string tran_id, string tran_no, string tran_cd, string ptserial)
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("SELECT count(*) cnt from  IFN_REFERENCE_DETAILS(" + tran_id + ",'" + tran_no + "','" + tran_cd + "','" + ptserial + "')");
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            if (ds != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return false;
            }
            return true;
        }

        public bool FindPartyManufacturerOrNot(string ac_nm)
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("SELECT count(*) cnt from  CM_MAST where tran_cd='VM' and ac_nm='" + ac_nm + "' and (vend_nm='MANUFACTURER' or vend_nm='IMPORTER')");
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            if (ds != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public int GetRGPageNo()
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("select isnull(max(rgpageno),0)+1 rgno from peitem where tran_cd='GR'");
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            if (ds != null && int.Parse(ds.Tables[0].Rows[0]["rgno"].ToString()) > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0]["rgno"].ToString());
            }
            return 1;
        }
        public bool FindRGPageNo(string rgpageno)
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("select count(*) rgno from peitem where tran_cd='GR' and rgpageno=" + rgpageno);
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            if (ds != null && int.Parse(ds.Tables[0].Rows[0]["rgno"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        public DataSet GetExtra_field(string tbl_nm, string tran_id, string tran_cd, string compid, string fin_yr)
        {
            // DataSet dsetpurref = objDLAdapter.dsquery("select tran_id,tran_cd,tran_no,ptserial,qty,ac_nm,prod_nm,ref_tran_id,ref_tran_cd,ref_tran_no,ref_ptserial,entry_no,rgpageno,shcess_amt,examt,cess_amt,manu_imp_nm,SEL,bill_dt,bill_no,total_qty,bal_qty from " + tbl_nm + " where "+objBLFD.Primary_id+"='" + tran_id + "' and tran_cd='" + tran_cd + "'");
            DataSet dsetpurref = objDLAdapter.dsquery("select * from " + tbl_nm + " where tran_id='" + tran_id + "' and tran_cd='" + tran_cd + "' and compid='" + compid + "' and fin_yr='" + fin_yr + "'");
            return dsetpurref;
        }
        public DataSet GetTrans_Settings(string tran_cd, string compid)
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("select  * from tran_set where code=case when('" + tran_cd + "'!='') then '" + tran_cd + "' else code end and compid='" + compid + "'");
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            return ds;
        }

        public DataSet GetApproveSetting(string code, string compid)
        {
            GetCorrectCon();
            string strSql = "";
            strSql = String.Format("select  * from LEVELS where code='" + code + "' and compid='" + compid + "' order by cast(si_no as int)");
            DataSet ds = new DataSet();
            ds = objDLAdapter.dsquery(strSql);
            return ds;
        }

        public bool Update_Company_ID(BL_BASEFIELD objBLFD)
        {
            GetCorrectCon();
            DataSet dsForeignKey = new DataSet();
            SqlCommand cmd = new SqlCommand("ISP_UPDATE_COMPANY_ID", con);
            cmd.Parameters.Add(new SqlParameter("@acompid", objBLFD.CompId));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsForeignKey);
            if (dsForeignKey != null && dsForeignKey.Tables.Count != 0 && dsForeignKey.Tables[0].Rows.Count != 0)
            {
                return false;
            }
            return true;
        }
        public bool Update_Module_to_ReportAndTran_Set(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();

                SqlCommand cmdinsert, cmdReport;
                con.Open();
                DataSet dsForeignKey = new DataSet();
                foreach (DictionaryEntry entry in objBLFD.HtModuleList)
                {
                    cmdinsert = new SqlCommand("insert into " + objBLFD.HTMAIN["DB_NM"].ToString() + ".dbo.TRAN_SET select code,tran_no_nm,Tran_cd,Tran_nm,Tran_type,Behavier_cd,Ref_behaiver_cd,Defrep_nm,Tran_no_wid,Main_tbl_nm,Item_tbl_nm,Ref_Type,Ref_tbl_nm,ref_tran_fld,ref_tran_nm,Approve_tbl_nm,Extra_tbl_nm,lock,auto_tran_no,Tran_narr,Pt_narr,ac_narr,due_dt,Prod_det,prnt_saving,validity,Pt_type_avail,bck_entry,Pt_pop_sel,curr_date,Ac_pop_sel,disp_locate,stk_effect,CompId,due_dt_on,copies_nm,print_once,edit_tran_no,round_groamt,round_asses_amt,cons,ac_pt_info,isDCApp,isdispPL,isApprove,isTransCopy,isTaxApp,isTaxRound,seachfield,ac_grp,def_acc,def_consignee,filter_req,def_acc_id,def_consignee_id,isRule,isFileAttach,isAmendment,isSchedule,isDeSchedule from InodeMFG.dbo.TRAN_SET_ALL where parent_cd like '%" + entry.Key.ToString() + "%'", con);
                    cmdinsert.ExecuteNonQuery();

                    cmdReport = new SqlCommand("insert into " + objBLFD.HTMAIN["DB_NM"].ToString() + ".dbo.REP_MAST select group_nm,[desc],rep_nm,def_rep,frm_dt,to_dt,frm_ac,to_ac,frm_item,to_item,frm_amt,to_amt,spl_cond,query_tbl,frm_city,to_city,hide_rep,sql_query,is_sp,sp_nm,order_no,sec_cd,cuser_nm,cuser_dt,muser_nm,muser_dt,is_rep,compid from InodeMFG.dbo.REP_MAST_ALL where parent_cd like '%" + entry.Key.ToString() + "%'", con);
                    cmdReport.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return true;
        }
        public void Update_Module_Reference_Type(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();
                SqlCommand cmdReference = new SqlCommand("ISP_UPDATE_REFERENCE_TYPE", con);
                cmdReference.Parameters.Add(new SqlParameter("@aBasic", objBLFD.HtModuleList.Contains("BSIC") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aExciseMfg", objBLFD.HtModuleList.Contains("BMFG") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aExciseTrd", objBLFD.HtModuleList.Contains("BTRD") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aMarketing", objBLFD.HtModuleList.Contains("MKTG") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aProcurement", objBLFD.HtModuleList.Contains("PRCM") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aInventory", objBLFD.HtModuleList.Contains("INVT") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aProduction", objBLFD.HtModuleList.Contains("PROD") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aPlanning", objBLFD.HtModuleList.Contains("PLAN") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aQuality", objBLFD.HtModuleList.Contains("QUAL") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@aJobWork", objBLFD.HtModuleList.Contains("JOBW") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@WareHouse", objBLFD.HtModuleList.Contains("WRHS") ? 1 : 0));
                cmdReference.Parameters.Add(new SqlParameter("@acompid", objBLFD.CompId));
                cmdReference.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmdReference.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        public bool Update_Company_ID_INODE(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();
                SqlCommand cmdinsert, cmdMenu;
                con.Open();
                cmdMenu = new SqlCommand("insert into InodeMFG.dbo.MENU_TBL select menu_nm,parent_id,menu_level_id,alias_nm,order_no,level_id,frm_nm,tran_cd,code,menu_type,parent_cd,condition,compid='" + objBLFD.CompId.ToString() + "' from InodeMFG.dbo.MENU_TBL_ALL where menu_type='STD' and parent_cd=''", con);
                cmdMenu.ExecuteNonQuery();
                foreach (DictionaryEntry entry in objBLFD.HtModuleList)
                {
                    cmdinsert = new SqlCommand("insert into InodeMFG.dbo.CMOD select module_name,module_desc,module_group,tran_cd,compid='" + objBLFD.CompId.ToString() + "',code='',alias_nm='',[level]='',[type]='' from InodeMFG.dbo.CMOD_ALL where parent_cd like '%" + entry.Key.ToString() + "%'", con);
                    cmdinsert.ExecuteNonQuery();

                    cmdMenu = new SqlCommand("insert into InodeMFG.dbo.MENU_TBL select menu_nm,parent_id,menu_level_id,alias_nm,order_no,level_id,frm_nm,tran_cd,code,menu_type,parent_cd,condition,compid='" + objBLFD.CompId.ToString() + "' from InodeMFG.dbo.MENU_TBL_ALL where parent_cd like '%" + entry.Key.ToString() + "%' and menu_level_id not in (select menu_level_id from InodeMFG.dbo.MENU_TBL where compid='" + objBLFD.CompId.ToString() + "')", con);
                    cmdMenu.ExecuteNonQuery();
                }
                DataSet dsForeignKey = new DataSet();
                SqlCommand cmd = new SqlCommand("ISP_INSERT_INITIAL_TABLES", con);
                cmd.Parameters.Add(new SqlParameter("@acompid", objBLFD.CompId));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsForeignKey);
                if (dsForeignKey != null && dsForeignKey.Tables.Count != 0 && dsForeignKey.Tables[0].Rows.Count != 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return true;
        }
        public string Get_Company_Id(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();
                DataSet dsetComp = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select top 1 Compid,comp_nm,fin_yr from  " + objBLFD.Main_tbl_nm + " order by compid desc", con);
                da.Fill(dsetComp);
                if (dsetComp != null && dsetComp.Tables[0].Rows.Count != 0)
                {
                    return dsetComp.Tables[0].Rows[0]["Compid"].ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
            }
            finally
            {
                con.Close();
            }
        }

        private DataSet CheckConnection(string strQuery)
        {
            DataSet dsSet = new DataSet();
            try
            {
                GetCorrectCon();
                SqlDataAdapter da = new SqlDataAdapter(strQuery, con);
                da.Fill(dsSet);
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
            }
            return dsSet;
        }
    }
}
