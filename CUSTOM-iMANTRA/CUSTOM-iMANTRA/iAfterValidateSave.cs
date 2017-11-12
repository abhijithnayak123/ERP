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
    public class iAfterValidateSave
    {
        /*  Created by Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.26.13
        * This Class is used after validation over & before Save details to DataBase.
         * 1.0 Sharanamma Jekeen on 11.29.13 ==> Update Amendement details.
         * 
         * 
         * 
       * */
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        BLHT objhashtables = new BLHT();

        Hashtable _htcustom = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        dblayer objdblayer = new dblayer();

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
            try
            {
                bool flg = true;
                if (objBLFD.IsAmendment)
                {
                    if (objBLFD.HTMAIN.Contains("IM_AMDREQ") && objBLFD.HTMAIN["IM_AMDREQ"] != null && objBLFD.HTMAIN["IM_AMDREQ"].ToString() != "" && bool.Parse(objBLFD.HTMAIN["IM_AMDREQ"].ToString()))
                    {
                        flg = AmendementDetails();
                    }
                }
                if (ACTIVE_BL.IsDeSchedule)
                {
                    flg = CheckDispatchScheduleisExist();
                }
                return flg;
            }
            catch (Exception ex)
            {
                BL_FIELDS.Errormsg = ex.Message;
                return false;
            }
        }
        #region 1.0
        public bool AmendementDetails()
        {
            bool _flgContinue = false;
            BL_BASEFIELD oldFileds = new BL_BASEFIELD();//(BL_BASEFIELD)ACTIVE_BL.Clone();
            DataSet dsetview = objdblayer.dsquery("select top 1 * from " + ACTIVE_BL.Main_tbl_nm + " where tran_cd='" + ACTIVE_BL.Code + "'  and " + ACTIVE_BL.Primary_id + "='" + ACTIVE_BL.HTMAIN[ACTIVE_BL.Primary_id].ToString() + "' and compid='" + ACTIVE_BL.ObjCompany.Compid.ToString() + "' order by " + ACTIVE_BL.Primary_id + " desc");

            Hashtable HTITEMVal = ACTIVE_BL.HTMAIN;
            foreach (DictionaryEntry entry in HTITEMVal)
            {
                oldFileds.HTMAIN[entry.Key] = entry.Value;
            }
            if (dsetview != null && dsetview.Tables.Count != 0 && dsetview.Tables[0].Rows.Count != 0)
            {
                if (objBLFD.HTMAIN["IM_AMDNO"] != null && objBLFD.HTMAIN["IM_AMDNO"].ToString() != "" && objBLFD.HTMAIN["IM_AMDNO"].ToString() != dsetview.Tables[0].Rows[0]["IM_AMDNO"].ToString())
                {
                    _flgContinue = true;
                    foreach (DataRow row in dsetview.Tables[0].Rows)
                    {
                        foreach (DataColumn column in dsetview.Tables[0].Columns)
                        {
                            if (oldFileds.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                            {
                                if (column.DataType.Name.ToString().ToLower() == "datetime")
                                {
                                    if (row[column.ColumnName] != null && row[column.ColumnName].ToString() != "")
                                    {
                                        oldFileds.HTMAIN[column.ColumnName.Trim().ToUpper()] = DateTime.Parse(row[column.ColumnName].ToString()).ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        oldFileds.HTMAIN[column.ColumnName.Trim().ToUpper()] = DateTime.Parse("01/01/1900");
                                    }
                                }
                                else
                                {
                                    oldFileds.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString().Trim();
                                }
                            }
                        }
                    }
                }
            }
            if (_flgContinue && oldFileds.Item_tbl_nm != "")
            {
                dsetview.Clear();
                dsetview = objdblayer.dsquery("select * from " + ACTIVE_BL.Item_tbl_nm + " where tran_cd='" + ACTIVE_BL.Code + "' and " + ACTIVE_BL.Primary_id + "='" + ACTIVE_BL.HTMAIN[ACTIVE_BL.Primary_id].ToString() + "' and compid='" + ACTIVE_BL.ObjCompany.Compid.ToString() + "' order by cast(ptserial as int)");
                if (dsetview != null && dsetview.Tables.Count != 0 && dsetview.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dsetview.Tables[0].Rows)
                    {
                        oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry in ACTIVE_BL.htitem_details)
                        {
                            ((Hashtable)oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()])[entry.Key] = entry.Value.ToString().Trim();
                        }
                        foreach (DataColumn column in dsetview.Tables[0].Columns)
                        {
                            if (((Hashtable)oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()]).ContainsKey(column.ColumnName.Trim().ToUpper()))
                            {
                                if (column.DataType.Name.ToString().ToLower() == "datetime")
                                {
                                    if (row[column.ColumnName] != null && row[column.ColumnName].ToString() != "")
                                    {
                                        ((Hashtable)oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()])[column.ColumnName.Trim().ToUpper()] = DateTime.Parse(row[column.ColumnName].ToString()).ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        ((Hashtable)oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()])[column.ColumnName.Trim().ToUpper()] = DateTime.Parse("01/01/1900");
                                    }
                                }
                                else
                                {
                                    ((Hashtable)oldFileds.HTITEM[row["PTSERIAL"].ToString().Trim()])[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString().Trim();
                                }
                            }
                        }
                    }
                }
            }
            if (_flgContinue && ACTIVE_BL != null && oldFileds != null)
            {
                try
                {
                    if (objBLFD.HASHTABLES != null)
                    {
                        objhashtables = objBLFD.HASHTABLES;
                    }
                    else
                    {
                        objhashtables.HashGeneraltbl = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    int k = 0;
                    foreach (DictionaryEntry entry in ACTIVE_BL.HTMAIN)
                    {
                        if (oldFileds.HTMAIN.Contains(entry.Key) && ACTIVE_BL.HTMAIN[entry.Key].ToString() != oldFileds.HTMAIN[entry.Key].ToString())
                        {
                            k++;
                            objhashtables.HashGeneraltbl[k] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            ((Hashtable)objhashtables.HashGeneraltbl[k])["fld_nm"] = entry.Key;
                            ((Hashtable)objhashtables.HashGeneraltbl[k])["old_value"] = oldFileds.HTMAIN[entry.Key].ToString();
                            ((Hashtable)objhashtables.HashGeneraltbl[k])["new_value"] = entry.Value.ToString();
                            ((Hashtable)objhashtables.HashGeneraltbl[k])["ptserial"] = "";
                        }
                    }
                    foreach (DictionaryEntry entry1 in ACTIVE_BL.HTITEM)
                    {
                        if (((Hashtable)ACTIVE_BL.HTITEM).Count != 0)
                        {
                            if (oldFileds.HTITEM.Contains(entry1.Key))
                            {
                                foreach (DictionaryEntry entry in ((Hashtable)entry1.Value))
                                {
                                    if (((Hashtable)oldFileds.HTITEM[entry1.Key]).Contains(entry.Key.ToString()) && entry.Value.ToString() != ((Hashtable)oldFileds.HTITEM[entry1.Key])[entry.Key.ToString()].ToString())
                                    {
                                        k++;
                                        objhashtables.HashGeneraltbl[k] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                        ((Hashtable)objhashtables.HashGeneraltbl[k])["fld_nm"] = entry.Key;
                                        ((Hashtable)objhashtables.HashGeneraltbl[k])["old_value"] = ((Hashtable)oldFileds.HTITEM[entry1.Key])[entry.Key].ToString();
                                        ((Hashtable)objhashtables.HashGeneraltbl[k])["new_value"] = entry.Value.ToString();
                                        ((Hashtable)objhashtables.HashGeneraltbl[k])["ptserial"] = entry1.Key;
                                    }
                                }
                            }
                            else
                            {
                                k++;
                                objhashtables.HashGeneraltbl[k] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                ((Hashtable)objhashtables.HashGeneraltbl[k])["fld_nm"] = "ptserial";
                                ((Hashtable)objhashtables.HashGeneraltbl[k])["old_value"] = "";
                                ((Hashtable)objhashtables.HashGeneraltbl[k])["new_value"] = "";
                                ((Hashtable)objhashtables.HashGeneraltbl[k])["ptserial"] = entry1.Key;
                            }
                        }
                    }
                    objBLFD.HASHTABLES = objhashtables;
                }
                catch (Exception ex)
                {
                }
            }
            return true;
        }
        #endregion 1.0

        private bool CheckDispatchScheduleisExist()
        {
            decimal qty = 0;
            foreach (DictionaryEntry entry in objBLFD.HTITEM)
            {
                if (((Hashtable)entry.Value).Count != 0 && objBLFD.HTMAINREF != null && objBLFD.HTMAINREF.Count != 0 && ((Hashtable)objBLFD.HTMAINREF[entry.Key.ToString()]) != null && ((Hashtable)objBLFD.HTMAINREF[entry.Key.ToString()]).Count != 0)
                {
                    qty = 0;
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@aref_tran_id", ((Hashtable)objBLFD.HTMAINREF[entry.Key.ToString()])["ref_tran_id"].ToString());
                    htparam.Add("@aref_tran_cd", ((Hashtable)objBLFD.HTMAINREF[entry.Key.ToString()])["ref_tran_cd"].ToString());
                    htparam.Add("@aref_ptserial", ((Hashtable)objBLFD.HTMAINREF[entry.Key.ToString()])["ref_ptserial"].ToString());
                    htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
                    htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
                    htparam.Add("@aptserial", entry.Key.ToString());
                    htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

                    DataSet dsetDSSchedule = objdblayer.dsprocedure("ISP_DISPATCH_QTY", htparam);
                    if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
                    {
                        if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashDeallocateSchedule != null && objBLFD.HASHTABLES.HashDeallocateSchedule.Count != 0)
                        {
                            foreach (DictionaryEntry entry1 in objBLFD.HASHTABLES.HashDeallocateSchedule)
                            {
                                if (entry1.Key.ToString().Split(',')[0] == entry.Key.ToString() && ((Hashtable)entry1.Value).Count != 0)
                                {
                                    if (((Hashtable)entry1.Value)["de_schedule_qty"] != null && ((Hashtable)entry1.Value)["de_schedule_qty"].ToString() != "")
                                    {
                                        qty += Convert.ToDecimal(((Hashtable)entry1.Value)["de_schedule_qty"].ToString());
                                    }
                                    else
                                    {
                                        qty += 0;
                                    }
                                }
                            }
                            if (((Hashtable)entry.Value).Contains("qty") && Convert.ToDecimal(((Hashtable)entry.Value)["qty"].ToString()) != qty)
                            {
                                BL_FIELDS.Errormsg = "Sorry Order Schedule Quantity doesn't match with Dispatch Quantity";
                                return false;
                            }
                        }
                        else
                        {
                            BL_FIELDS.Errormsg = "Sorry Please use order schedule to Dispatch";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
