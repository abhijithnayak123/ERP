using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using CUSTOM_iMANTRA_BL;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iFinalUpdateTransactionAndMaster
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.13.13 1.39PM
      * 1.0 modified save option for iSCHEDULE
      * 
      * */

        BLHT objhashtables = new BLHT();
        dblayer objdblayer = new dblayer();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public void FinalUpdate()
        {
            if (objBLFD.IsFileAttach || objBLFD.Code == "EV")
            {
                #region
                string file_Path = "", file_nm = "",exist_file="";
                int _lastIndex = 0;
                //create floder if it is not existed.
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"/" + objBLFD.ObjCompany.Db_nm + "/FILES"))
                {
                    string pathString = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"" + objBLFD.ObjCompany.Db_nm, "FILES");
                    System.IO.Directory.CreateDirectory(pathString);
                }
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashFileUpload != null && objBLFD.HASHTABLES.HashFileUpload.Count != 0)
                {
                    Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    bool bResult = false;

                    string serialnos = "''", ptkey = "", removing_item = "", ptserial = "0";

                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashFileUpload)
                    {
                      if (((Hashtable)entry.Value).Count != 0 )
                        {
                            file_Path = ((Hashtable)entry.Value)["file_path"].ToString();
                            _lastIndex = file_Path.LastIndexOf('\\') + 1;
                            file_nm = objBLFD.Code + "_" + objBLFD.Tran_id + "_" + entry.Key.ToString() + "_" + file_Path.Substring(_lastIndex);

                            if (serialnos == "''")
                            {
                                serialnos = "'" + ((Hashtable)entry.Value)["si_no"].ToString() + "'";
                                ptkey = "'" + entry.Key.ToString().Split(',')[0].ToString() + "'";
                                ptserial = entry.Key.ToString().Split(',')[0].ToString();
                                removing_item += "\"'" + file_nm + "'";
                            }
                            else
                            {
                                serialnos += ",'" + ((Hashtable)entry.Value)["si_no"].ToString() + "'";
                                removing_item += "\"'" + file_nm + "'";
                            }
                            if (entry.Key.ToString().Split(',')[0].ToString() != ptserial)
                            {
                                objdblayer.execDeleteQuery("delete from FILE_UPLOAD where si_no not in (" + serialnos + ") and ptserial in (" + ptkey + ") and tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");
                                serialnos = "''";
                            }
                        }
                        else
                        {
                            ptkey = "'" + entry.Key.ToString().Split(',')[0].ToString() + "'";
                            ptserial = entry.Key.ToString().Split(',')[0].ToString();
                        }
                    }
                    objdblayer.execDeleteQuery("delete from FILE_UPLOAD where si_no not in (" + serialnos + ") and ptserial in (" + ptkey + ") and tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");
                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashFileUpload)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            // To avoid saving multiple Copies while edit
                             exist_file = objBLFD.Code + "_" + objBLFD.Tran_id + "_" + entry.Key.ToString() + "_" + ((Hashtable)entry.Value)["file_nm"].ToString();

                            file_Path = ((Hashtable)entry.Value)["file_path"].ToString();
                            _lastIndex = file_Path.LastIndexOf('\\') + 1;

                            file_nm = objBLFD.Code + "_" + objBLFD.Tran_id + "_" + entry.Key.ToString() + "_" + file_Path.Substring(_lastIndex);

                            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + @"\FILES\" + exist_file))
                            {
                                try
                                {
                                    System.IO.File.Copy(file_Path, AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + @"\FILES\" + file_nm);
                                }
                                catch (Exception ex)
                                {
                                }
                                ((Hashtable)entry.Value)["file_path"] = AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + @"\FILES\" + file_nm;
                            }
                            else
                            {
                                if (!(removing_item.Split('"').Contains(file_nm)))
                                {
                                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + @"\FILES\" + file_nm);
                                }
                            }

                            foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                            {
                                htFields.Add(entry1.Key, entry1.Value);
                            }
                            if (htFields.Count != 0)
                            {
                                if (htFields["fileId"] != null && htFields["fileId"].ToString() != "0")
                                {
                                    htFieldscond["tran_id"] = objBLFD.Tran_id;
                                    htFieldscond["tran_cd"] = objBLFD.Code;
                                    htFieldscond["compid"] = objBLFD.ObjCompany.Compid.ToString();
                                    htFieldscond["ptserial"] = entry.Key.ToString().Split(',')[0].ToString();
                                    htFieldscond["fileId"] = htFields["fileId"].ToString();
                                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;

                                    htFields.Remove("fileId");
                                    htFields.Remove("tran_id");
                                    htFields.Remove("tran_cd");
                                    htFields.Remove("ptserial");
                                    htFields.Remove("compid");

                                    bResult = objdblayer.execUpdate("FILE_UPLOAD", htFields, htFieldscond);
                                }
                                else
                                {
                                    htFields["tran_id"] = objBLFD.Tran_id;
                                    htFields["tran_cd"] = objBLFD.Code;
                                    htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                    htFields["compid"] = objBLFD.ObjCompany.Compid.ToString();
                                    htFields["ptserial"] = entry.Key.ToString().Split(',')[0].ToString();
                                    htFields["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                                    htFields.Remove("fileId");
                                    bResult = objdblayer.execInsert("FILE_UPLOAD", htFields);
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
                #endregion
            }
            if (objBLFD.IsAmendment)
            {
                #region 2.0
                if (objBLFD.HTMAIN.Contains("IM_AMDREQ") && objBLFD.HTMAIN["IM_AMDREQ"] != null && objBLFD.HTMAIN["IM_AMDREQ"].ToString() != "" && bool.Parse(objBLFD.HTMAIN["IM_AMDREQ"].ToString()))
                {
                    if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashGeneraltbl != null && objBLFD.HASHTABLES.HashGeneraltbl.Count != 0)
                    {
                        foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashGeneraltbl)
                        {
                            ((Hashtable)entry.Value)["tran_id"] = objBLFD.Tran_id;
                            ((Hashtable)entry.Value)["tran_cd"] = objBLFD.Code;
                            ((Hashtable)entry.Value)["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                            ((Hashtable)entry.Value)["compid"] = objBLFD.ObjCompany.Compid;
                            ((Hashtable)entry.Value)["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                            ((Hashtable)entry.Value)["amd_no"] = objBLFD.HTMAIN["IM_AMDNO"];
                            ((Hashtable)entry.Value)["amd_dt"] = objBLFD.HTMAIN["IM_AMDDT"];
                            objdblayer.execInsert("TBL_AMD", ((Hashtable)entry.Value));
                        }
                    }
                }
                #endregion 2.0                
            }
            if (ACTIVE_BL.Code == "LR")
            {
                #region
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult = false;

                foreach (DictionaryEntry entry in objhashtables.HashIssueAndReceipt)
                {
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        if (((Hashtable)entry.Value).Count == 0)
                        {
                            bool flg = DELETE_ITEM("ptserial", ((Hashtable)entry.Value)["ptserial"].ToString(), objBLFD.Tran_id, objBLFD.Code, "LR_LI_DET");
                        }
                        else
                        {
                            htFields.Add(entry1.Key, entry1.Value);
                        }
                    }
                    if (htFields.Count != 0)
                    {
                        if (htFields["tran_id"].ToString() != "0")
                        {
                            htFieldscond["ptserial"] = htFields["ptserial"];
                            htFieldscond["ref_tran_id"] = htFields["ref_tran_id"];
                            htFieldscond["ref_ptserial"] = htFields["ref_ptserial"];
                            htFieldscond["tran_id"] = objBLFD.Tran_id;
                            htFieldscond["tran_cd"] = objBLFD.Code;

                            htFields.Remove("ptserial");
                            htFields.Remove("ref_tran_id");
                            htFields.Remove("ref_ptserial");
                            htFields.Remove("tran_id");
                            htFields.Remove("tran_cd");

                            bResult = objdblayer.execUpdate("LR_LI_DET", htFields, htFieldscond);
                        }
                        else
                        {
                            htFields["tran_id"] = objBLFD.Tran_id;
                            bResult = objdblayer.execInsert("LR_LI_DET", htFields);
                        }
                        if (bResult)
                        {
                            htFields.Clear();
                            htFieldscond.Clear();
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.IsSchedule)
            {
                #region 1.0
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult = false;
                string serialnos = "";

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos = "'" + ((Hashtable)entry.Value)["schedule_id"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["schedule_id"].ToString() + "'";
                    }
                }

                objdblayer.execDeleteQuery("delete from iSCHEDULE where schedule_id not in (" + serialnos + ") and tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            htFields.Add(entry1.Key, entry1.Value);
                        }
                        if (htFields.Count != 0)
                        {
                            if (htFields["schedule_id"] != null && htFields["schedule_id"].ToString() != "0")
                            {
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFieldscond["schedule_id"] = htFields["schedule_id"];
                                htFieldscond["tran_id"] = objBLFD.Tran_id;
                                htFieldscond["tran_cd"] = objBLFD.Code;
                                htFieldscond["ptserial"] = htFields["ptserial"];

                                htFields.Remove("schedule_id");
                                htFields.Remove("tran_id");
                                htFields.Remove("tran_cd");
                                htFields.Remove("ptserial");

                                bResult = objdblayer.execUpdate("iSCHEDULE", htFields, htFieldscond);
                            }
                            else
                            {
                                htFields["tran_id"] = objBLFD.Tran_id;
                                htFields["tran_cd"] = objBLFD.Code;
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFields.Remove("schedule_id");
                                bResult = objdblayer.execInsert("iSCHEDULE", htFields);
                            }
                            if (bResult)
                            {
                                htFields.Clear();
                                htFieldscond.Clear();
                            }
                        }
                    }
                }
                #endregion 1.0
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                #region 1.0
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult = false;
                string serialnos = "";

                foreach (DictionaryEntry entry in objhashtables.HashDeallocateSchedule)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos = "'" + ((Hashtable)entry.Value)["de_schedule_id"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["de_schedule_id"].ToString() + "'";
                    }
                }

                objdblayer.execDeleteQuery("delete from iDE_SCHEDULE where de_schedule_id not in (" + serialnos + ") and tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");

                foreach (DictionaryEntry entry in objhashtables.HashDeallocateSchedule)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            htFields.Add(entry1.Key, entry1.Value);
                        }
                        if (htFields.Count != 0)
                        {
                            if (htFields["de_schedule_id"] != null && htFields["de_schedule_id"].ToString() != "0")
                            {
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFieldscond["de_schedule_id"] = htFields["de_schedule_id"];
                                htFieldscond["tran_id"] = objBLFD.Tran_id;
                                htFieldscond["tran_cd"] = objBLFD.Code;
                                htFieldscond["ptserial"] = htFields["ptserial"];

                                htFields.Remove("de_schedule_id");
                                htFields.Remove("tran_id");
                                htFields.Remove("tran_cd");
                                htFields.Remove("ptserial");

                                bResult = objdblayer.execUpdate("iDE_SCHEDULE", htFields, htFieldscond);
                            }
                            else
                            {
                                htFields["tran_id"] = objBLFD.Tran_id;
                                htFields["tran_cd"] = objBLFD.Code;
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFields.Remove("de_schedule_id");
                                bResult = objdblayer.execInsert("iDE_SCHEDULE", htFields);
                            }
                            if (bResult)
                            {
                                htFields.Clear();
                                htFieldscond.Clear();
                            }
                        }
                    }
                }
                #endregion 1.0
            }

            if (ACTIVE_BL.Code == "CE" || ACTIVE_BL.Code == "PP")
            {
                #region
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult = false;
                string serialnos = "";

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos += "'" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + "'";
                    }
                }

                objdblayer.execDeleteQuery("delete from IP_WO_DET where ref_tran_id not in (" + serialnos + ") and tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        //    bool flg = DELETE_ITEM("ref_tran_id", entry.Key.ToString().Split(',')[0], objBLFD.Tran_id, objBLFD.Code, "IP_WO_DET");
                        //}
                        //else
                        //{
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            if (!(entry1.Key.ToString() == "ref_tran_id1" || entry1.Key.ToString() == "ref_ptserial1" || entry1.Key.ToString() == "ref_prod_cd1" || entry1.Key.ToString() == "ref_tran_no1"))
                            {
                                htFields.Add(entry1.Key, entry1.Value);
                            }
                        }
                        if (htFields.Count != 0)
                        {
                            if (htFields["ip_wo_id"] != null && htFields["ip_wo_id"].ToString() != "0")
                            {
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFieldscond["ip_wo_id"] = htFields["ip_wo_id"];
                                htFieldscond["ref_tran_id"] = htFields["ref_tran_id"];
                                htFieldscond["ref_ptserial"] = htFields["ref_ptserial"];
                                htFieldscond["tran_id"] = objBLFD.Tran_id;
                                htFieldscond["tran_cd"] = objBLFD.Code;

                                htFields.Remove("ip_wo_id");
                                htFields.Remove("ref_tran_id");
                                htFields.Remove("ref_ptserial");
                                htFields.Remove("tran_id");
                                htFields.Remove("tran_cd");

                                bResult = objdblayer.execUpdate("IP_WO_DET", htFields, htFieldscond);
                            }
                            else
                            {
                                htFields["tran_id"] = objBLFD.Tran_id;
                                htFields["tran_cd"] = objBLFD.Code;
                                htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFields.Remove("ip_wo_id");
                                bResult = objdblayer.execInsert("IP_WO_DET", htFields);
                            }
                            if (bResult)
                            {
                                htFields.Clear();
                                htFieldscond.Clear();
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "PD")
            {
                #region
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                bool bResult = false;

                string serialnos = "";

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (serialnos == "")
                            serialnos += "'" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + "'";
                        else
                            serialnos += ",'" + ((Hashtable)entry.Value)["ref_tran_id"].ToString() + "'";
                    }
                }

                objdblayer.execDeleteQuery("delete from OP_WO_DET where ref_tran_id not in (" + serialnos + ") and tran_id='" + objBLFD.Tran_id + "'");

                foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        //    bool flg = DELETE_ITEM("ref_tran_id", entry.Key.ToString().Split(',')[0], objBLFD.Tran_id, objBLFD.Code, "OP_WO_DET");
                        //}
                        //else
                        //{                    
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            htFields.Add(entry1.Key, entry1.Value);
                        }
                        if (htFields.Count != 0)
                        {
                            if (htFields["op_wo_id"] != null && htFields["op_wo_id"].ToString() != "0")
                            {
                                // htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];

                                // htFieldscond["op_wo_id"] = htFields["op_wo_id"];
                                htFieldscond["ref_tran_id"] = htFields["ref_tran_id"];
                                htFieldscond["ref_ptserial"] = htFields["ref_ptserial"];
                                htFieldscond["tran_id"] = objBLFD.Tran_id;
                                htFieldscond["tran_cd"] = objBLFD.Code;

                                htFields.Remove("op_wo_id");
                                htFields.Remove("ref_tran_id");
                                htFields.Remove("ref_ptserial");
                                htFields.Remove("tran_id");
                                htFields.Remove("tran_cd");

                                bResult = objdblayer.execUpdate("OP_WO_DET", htFields, htFieldscond);
                            }
                            else
                            {
                                htFields["tran_id"] = objBLFD.Tran_id;
                                htFields["tran_cd"] = objBLFD.Code;
                                htFields["tran_no"] = objBLFD.HTMAIN["TRAN_NO"];
                                htFields.Remove("op_wo_id");
                                bResult = objdblayer.execInsert("OP_WO_DET", htFields);
                            }
                            if (bResult)
                            {
                                htFields.Clear();
                                htFieldscond.Clear();
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "WO")
            {
                objhashtables = ACTIVE_BL.HASHTABLES;

                Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Save_Main_Trasaction();
            }
            if (ACTIVE_BL.Code == "TS")
            {
                DataSet dsetBaseField = objdblayer.dsquery("select * from ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "'");
                if (dsetBaseField != null && dsetBaseField.Tables.Count != 0 && dsetBaseField.Tables[0].Rows.Count != 0)
                {
                }
                else
                {
                    string strQuery = "insert into ibasefields (code,[type],typewise,TRAN_CD,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre)";
                    string strQueryVal = "select code='" + objBLFD.HTMAIN["Code"].ToString() + "',[type],typewise,TRAN_CD,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,mandatory,frm_nm,valid_mast,remarks,parent_ctrl,ctrl_not_show,_mul,reftbltran_cd,tbl_nm,sel_item,sel_val,_query,_querycon,_primddl,_Dpopflds,_top,_copy,when_con,valid_con,error_con,_mon_con,default_con,_read,_pickup,compid,_fld_width,_fld_pre from ibasefields where code='" + objBLFD.HTMAIN["behavier_cd"].ToString() + "'";
                    objdblayer.execQuery(strQuery + strQueryVal);
                }
            }
        }

        public string Save_Main_Trasaction()
        {
            Hashtable htFieldsMain = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscondMain = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = true;

            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
            {
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    if (entry1.Key.ToString().ToLower() != "woboid")
                    {
                        htFieldsMain.Add(entry1.Key, entry1.Value);
                    }
                    else
                    {
                        if (entry1.Value.ToString() == "")
                            htFieldscondMain.Add(entry1.Key, "0");
                        else
                            htFieldscondMain.Add(entry1.Key, entry1.Value);
                    }
                }
                if (htFieldsMain.Count != 0)
                {
                    htFieldsMain["compid"] = objBLFD.ObjCompany.Compid.ToString();
                    htFieldsMain["fin_yr"] = objBLFD.ObjCompany.Fin_yr.ToString();
                    htFieldsMain.Remove(("woboid").ToUpper());
                    htFieldsMain[objBLFD.Primary_id] = objBLFD.Tran_id;
                    // htFieldscondMain[objBLFD.Primary_id] = objBLFD.Tran_id;
                    //htFieldsMain.Remove(objBLFD.Primary_id);
                    if (int.Parse(htFieldscondMain[("woboid").ToUpper()].ToString()) > 0)
                    {
                        bResult = objdblayer.execUpdate("WO_BO_MAIN", htFieldsMain, htFieldscondMain);
                    }
                    else
                    {
                        bResult = objdblayer.execInsert("WO_BO_MAIN", htFieldsMain);
                    }
                }
                if (bResult)
                {
                    if (objBLFD.HASHTABLES.HashItemtbl.Count > 0)
                    {
                        int woboid = getMainId("woboid", "WO_BO_MAIN", htFieldsMain["ptserial"].ToString());
                        if (woboid > 0)
                        {
                            bool itemflg = Save_Item_Trasaction(woboid, htFieldsMain["ptserial"].ToString());
                            if (!itemflg)
                            {
                                return "Item table Updation Failed";
                            }
                        }
                    }
                }
                if (bResult)
                {
                    htFieldsMain.Clear();
                    htFieldscondMain.Clear();
                }
            }
            return "";
        }
        public bool Save_Item_Trasaction(int pkValue, string ptserial)
        {
            Hashtable htFields = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool bResult = false;
            string serialnos = "";

            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashItemtbl)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (serialnos == "")
                        serialnos = "'" + ((Hashtable)entry.Value)["woboitno"].ToString() + "'";
                    else
                        serialnos += ",'" + ((Hashtable)entry.Value)["woboitno"].ToString() + "'";
                }
            }

            objdblayer.execDeleteQuery("delete from WO_BO_ITEM where woboitno not in (" + serialnos + ") and woboid='" + pkValue + "' and tran_id='" + objBLFD.HTMAIN["tran_id"].ToString() + "' and ptserial='" + ptserial + "'");

            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashItemtbl)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (((Hashtable)entry.Value)["ptserial"].ToString() == ptserial)
                    {
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            if (entry1.Key.ToString().ToLower() != "woboitno")
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
                            htFields[objBLFD.Primary_id] = objBLFD.Tran_id;
                            htFields[("woboid").ToUpper()] = pkValue.ToString();
                            htFieldscond[("woboid").ToUpper()] = pkValue.ToString();
                            if (FindItemExistance(pkValue, htFieldscond["woboitno"].ToString()))
                            {
                                htFields.Remove(("woboid").ToUpper());
                                htFields.Remove(("woboitno").ToUpper());
                                bResult = objdblayer.execUpdate("WO_BO_ITEM", htFields, htFieldscond);
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
                                bResult = objdblayer.execInsert("WO_BO_ITEM", htFields);
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
            return bResult;
        }
        public int getMainId(string _primary_key_nm, string _main_tbl_nm, string ptserial)
        {
            int tran_id = 0;
            string strSql = "";
            if (objBLFD.Tran_mode == "add_mode")
            {
                strSql = String.Format("SELECT  max(" + _primary_key_nm + ")  from " + _main_tbl_nm + " where TRAN_CD='{0}' AND [tran_no]='{1}'", objBLFD.HTMAIN["tran_cd"].ToString(), objBLFD.HTMAIN["tran_no"].ToString());
            }
            else
            {
                strSql = String.Format("SELECT  " + _primary_key_nm + "  from " + _main_tbl_nm + " where TRAN_CD='{0}' AND [tran_no]='{1}' and ptserial='{2}' and compid='{3}'", objBLFD.HTMAIN["tran_cd"].ToString(), objBLFD.HTMAIN["tran_no"].ToString(), ptserial, objBLFD.ObjCompany.Compid);
            }
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
        public bool FindItemExistance(int pkValue, string item_no)
        {
            string strSql = String.Format("SELECT count(*) cnt from  WO_BO_ITEM where woboid='{0}' AND [woboitno]='{1}'", pkValue.ToString(), item_no);
            DataSet ds = objdblayer.dsquery(strSql);
            if (ds.Tables[0] != null && int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DELETE_ITEM(string col_nm, string itserial, string tran_id, string tran_cd, string tbl_nm)
        {
            bool flg = false;
            Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            htFieldscond.Add("tran_id", tran_id);
            htFieldscond.Add("TRAN_CD", tran_cd);
            htFieldscond.Add(col_nm, itserial);
            if (tran_id != "")
            {
                try
                {
                    flg = objdblayer.execDelete(tbl_nm, htFieldscond);
                }
                catch
                {
                    flg = false;
                }
            }
            return flg;
        }
    }
}
