using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;


namespace CUSTOM_iMANTRA
{
    public class DeleteTransactionAndMaster
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
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
        public bool tsDeleteTransactionAndMaster()
        {
            if (ACTIVE_BL.Stk_effect == "+")
            {
                if (!StockCheck())
                    return false;
            }
            if (ACTIVE_BL.Code == "GM")
            {
                if (ACTIVE_BL.HTMAIN.Contains("ac_grp_nm"))
                {
                    if (ACTIVE_BL.HTMAIN["ac_grp_nm"] != null && ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() != "")
                    {
                        //DataSet dsetgroupdelete = objdblayer.dsquery("select count(*) cnt from cm_mast  where ac_grp_nm='" + ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() + "' union all select count(*) cnt from LEDGER_MAST where ac_grp_nm='" + ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() + "'");
                        DataSet dsetgroupdelete = objdblayer.dsquery("select count(*) cnt from cm_mast  where ac_grp_nm='" + ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() + "' having count(*)>0 union all select count(*) cnt from LEDGER_MAST where ac_grp_nm='" + ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() + "' having count(*)>0 union all select count(*) cnt from CM_GROUP where parent_nm='" + ACTIVE_BL.HTMAIN["ac_grp_nm"].ToString() + "' having count(*)>0");

                        if (dsetgroupdelete != null && dsetgroupdelete.Tables.Count != 0 && dsetgroupdelete.Tables[0].Rows.Count != 0)
                        {
                            BL_FIELDS.Errormsg = "Deleting this COA Group is not posible, Reason: Group used in other Master or Delete sub-group first";
                            return false;
                        }
                    }
                }
            }
            if (ACTIVE_BL.Code == "PG")
            {
                Hashtable htparam = new Hashtable();
                DataSet dsetpurref = objdblayer.dsquery("select count(*) cnt from pt_mast where pt_grp_nm='" + objBLFD.HTMAIN["pt_grp_nm"].ToString() + "'");
                if (dsetpurref != null && dsetpurref.Tables[0].Rows.Count != 0)
                {
                    if (int.Parse(dsetpurref.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        BL_FIELDS.Errormsg = "Deleting this Product Group is not posible, Reason: Group used in Product Master";
                        return false;
                    }
                }
            }
            if (ACTIVE_BL.Code == "ST")
            {
                #region
                DataSet ds2 = objdblayer.dsquery("select Behavier_cd  from TRAN_SET where code='" + ACTIVE_BL.HTMAIN["Code"] + "' ");
                string code = ds2.Tables[0].Rows[0]["Behavier_cd"].ToString();
                DataSet ds1 = objdblayer.dsquery("select ST_MAST.tax_nm,ST_MAST.pert_val from " + code + "MAIN Inner Join ST_MAST ON(ST_MAST.TAX_NM=" + code + "MAIN.TAX_NM) and ST_MAST.code= " + code + "MAIN.Tran_cd where ST_MAST.tax_nm='" + ACTIVE_BL.HTMAIN["TAX_NM"] + "' and " + code + "MAIN.tran_cd='" + ACTIVE_BL.HTMAIN["Code"] + "'");
                if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    if (ACTIVE_BL.HTMAIN["Tax_nm"].ToString() == ds1.Tables[0].Rows[0]["Tax_nm"].ToString())
                    {
                        BL_FIELDS.Errormsg = "Deleting this Sales Tax is not posible, Reason: Used in Transactions";
                        return false;
                    }
                }
                string code1 = ACTIVE_BL.HTMAIN["code"].ToString();
                string strQueryUpdate = "update ST_MAST set valid_tran=replace(replace(replace(valid_tran,'," + code1 + "',''),'" + code1 + ",',''),'" + code1 + "','')  where tax_nm='" + ACTIVE_BL.HTMAIN["Tax_nm"] + "'";
                if (!objdblayer.execDeleteQuery(strQueryUpdate))//fld exists in specific table
                {
                    BL_FIELDS.Errormsg = "Sorry Updating Field Name in the Sales Tax table is not successful.";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "LR")
            {
                Hashtable hashcon = new Hashtable();
                hashcon["tran_id"] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete("LR_LI_DET", hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting Labour Receipt is not Successful";
                    return false;
                }
            }
            if (ACTIVE_BL.IsSchedule || ACTIVE_BL.Code == "PH")
            {
               DataSet dsetDSSchedule = objdblayer.dsquery("select * from ide_schedule where ref_tran_id=" + objBLFD.Tran_id + " and ref_tran_cd='" + objBLFD.Code + "'");
               if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
               {
                   BL_FIELDS.Errormsg = "Sorry Scheduling Refered in De-Schedule";
                   return false;
               }
               else
               {
                   Hashtable hashcon = new Hashtable();
                   hashcon["tran_id"] = objBLFD.Tran_id;
                   hashcon["tran_cd"] = objBLFD.Code;
                   bool blnFlg = objdblayer.execDelete("iSCHEDULE", hashcon);
                   if (!blnFlg)
                   {
                       BL_FIELDS.Errormsg = "Deleting Dispatching Schedule is not Successful";
                       return false;
                   }
               }
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                Hashtable hashcon = new Hashtable();
                hashcon["tran_id"] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete("iDE_SCHEDULE", hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting Schedule is not Successful";
                    return false;
                }
            }
            if (ACTIVE_BL.Code == "CE")
            {
                #region
                bool flgContinue = true;
                BL_FIELDS.Errormsg = "";

                //get production details
                DataSet dsetceref = objdblayer.dsquery("select SUM(qty) op_qty,ref_tran_id,ref_tran_cd,ref_ptserial from OP_WO_DET group by ref_tran_id,ref_tran_cd,ref_ptserial");

                if (dsetceref != null && dsetceref.Tables.Count != 0 && dsetceref.Tables[0].Rows.Count != 0)
                {
                    //get total consumption details
                    DataSet dsetcetotal = objdblayer.dsquery("select min(issue_qty) min_issue_qty,ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial from (select sum(issue_qty)*bom_qty/rm_qty issue_qty,ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,rm_nm from ip_wo_det group by ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,bom_qty,rm_qty,rm_nm)vw group by ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial");
                    //get consumption except current consumption details
                    DataSet dsetCENotCurrentTran_id = objdblayer.dsquery("select min(issue_qty) cur_issue_qty,ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial from (select sum(issue_qty)*bom_qty/rm_qty issue_qty,ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,rm_nm from ip_wo_det where tran_id!=" + objBLFD.Tran_id + " and tran_cd='" + objBLFD.Code + "' group by ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,bom_qty,rm_qty,rm_nm )vw group by ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial");
                    DataSet dsetCECurrentTran_id = objdblayer.dsquery("select ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial from ip_wo_det where tran_id=" + objBLFD.Tran_id + " and tran_cd='" + objBLFD.Code + "'");
                    foreach (DataRow rowcurrent in dsetCECurrentTran_id.Tables[0].Rows)
                    {
                        foreach (DataRow row in dsetceref.Tables[0].Rows)
                        {
                            if (flgContinue && rowcurrent["ref_tran_id"].ToString() + "," + rowcurrent["ref_tran_cd"].ToString() + "," + rowcurrent["ref_ptserial"].ToString() == row["ref_tran_id"].ToString() + "," + row["ref_tran_cd"].ToString() + "," + row["ref_ptserial"].ToString())
                            {
                                foreach (DataRow total_row in dsetcetotal.Tables[0].Rows)
                                {
                                    if (flgContinue && rowcurrent["ref_tran_id"].ToString() + "," + rowcurrent["ref_tran_cd"].ToString() + "," + rowcurrent["ref_ptserial"].ToString() == total_row["ref_tran_id"].ToString() + "," + total_row["ref_tran_cd"].ToString() + "," + total_row["ref_ptserial"].ToString())
                                    {
                                        if (decimal.Parse(total_row["min_issue_qty"].ToString()) > decimal.Parse(row["op_qty"].ToString()))
                                        {
                                            foreach (DataRow Not_Current_row in dsetCENotCurrentTran_id.Tables[0].Rows)
                                            {
                                                if (flgContinue && rowcurrent["ref_tran_id"].ToString() + "," + rowcurrent["ref_tran_cd"].ToString() + "," + rowcurrent["ref_ptserial"].ToString() == Not_Current_row["ref_tran_id"].ToString() + "," + Not_Current_row["ref_tran_cd"].ToString() + "," + Not_Current_row["ref_ptserial"].ToString())
                                                {
                                                    if (decimal.Parse(Not_Current_row["cur_issue_qty"].ToString()) < decimal.Parse(row["op_qty"].ToString()))
                                                    {
                                                        flgContinue = false;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            flgContinue = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (flgContinue)
                {
                    Hashtable hashcon = new Hashtable();
                    hashcon["tran_id"] = objBLFD.Tran_id;
                    hashcon["tran_cd"] = objBLFD.Code;
                    bool blnFlg = objdblayer.execDelete("IP_WO_DET", hashcon);
                    if (!blnFlg)
                    {
                        BL_FIELDS.Errormsg = "Deleting in " + objBLFD.Tran_nm + " is not Successful";
                        return false;
                    }
                }
                else
                {
                    BL_FIELDS.Errormsg = "Sorry delteing Qty of " + objBLFD.Tran_nm + " is not posible, Reason: " + objBLFD.Tran_nm + " used in Production";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "PP")
            {
                Hashtable hashcon = new Hashtable();
                hashcon["tran_id"] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete("IP_WO_DET", hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting in " + objBLFD.Tran_nm + " is not Successful";
                    return false;
                }
            }
            if (ACTIVE_BL.Code == "PD")
            {
                #region
                Hashtable hashcon = new Hashtable();
                hashcon["tran_id"] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete("OP_WO_DET", hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting Work Order in " + objBLFD.Tran_nm + " is not Successful";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "WO")
            {
                #region
                DataSet dsetpurref = objdblayer.dsquery("select count(*) cnt from IP_WO_DET where  ref_tran_id='" + objBLFD.Tran_id + "' and ref_tran_cd='" + ACTIVE_BL.Code + "'");
                if (dsetpurref != null && dsetpurref.Tables.Count != 0 && dsetpurref.Tables[0].Rows.Count != 0)
                {
                    if (int.Parse(dsetpurref.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        BL_FIELDS.Errormsg = "Deleting this Work Order is not posible, Reason: Work Order used in Consumption";
                        return false;
                    }
                }
                Hashtable hashcon = new Hashtable();
                hashcon["tran_id"] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete("WO_BO_ITEM", hashcon);
                if (blnFlg)
                {
                    bool blnFlg1 = objdblayer.execDelete("WO_BO_MAIN", hashcon);
                    if (!blnFlg1)
                    {
                        BL_FIELDS.Errormsg = "Deleting Work Order Main Table is not Successful";
                        return false;
                    }
                }
                else
                {
                    BL_FIELDS.Errormsg = "Deleting Work Order Item Table is not Successful";
                    return false;
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "EM")
            {
                #region
                string[] strValidIn = objBLFD.HTMAIN["code"].ToString().Split(',');
                Hashtable _tableNm = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable _htcond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                DataSet dsettbl_nm;
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,main_tbl_nm tbl_nm from tran_set where code in ('" + objBLFD.HTMAIN["code"].ToString() + "')");
                }
                else
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,item_tbl_nm tbl_nm from tran_set where code in ('" + objBLFD.HTMAIN["code"].ToString() + "')");
                }
                if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                {
                    foreach (DataRow row in dsettbl_nm.Tables[0].Rows)
                    {
                        _tableNm[row["code"].ToString()] = row["tbl_nm"].ToString();
                    }
                }
                foreach (string str in strValidIn)
                {
                    if (_tableNm.Contains(str))//transaction exist.
                    {
                        DataSet dset = objdblayer.dsquery("select * from icustomfields where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code!='" + str + "' and tbl_nm='" + _tableNm[str].ToString() + "'");
                        if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)//fld exists
                        {
                            BL_FIELDS.Errormsg = "Sorry Given Field Name Already Exist in the Table.";
                        }
                        else//fld not existed
                        {
                            string strQuery = "ALTER TABLE " + _tableNm[str].ToString() + " DROP COLUMN " + objBLFD.HTMAIN["fld_nm"].ToString() + "";
                            if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                            {
                                BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Table is not successful.";
                                return false;
                            }
                        }
                        string strQuery1 = "delete from icustomfields where fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'";
                        if (!objdblayer.execDeleteQuery(strQuery1))//fld exists in specific table
                        {
                            BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Customfield table is not successful.";
                            return false;
                        }
                        string strQueryUpdate = "update icustomfields set  valid_mast=replace(replace(replace(valid_mast,'," + str + "',''),'" + str + ",',''),'" + str + "','')  where fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "'";
                        if (!objdblayer.execDeleteQuery(strQueryUpdate))//fld exists in specific table
                        {
                            BL_FIELDS.Errormsg = "Sorry Updating Field Name in the Customfield table is not successful.";
                            return false;
                        }

                        string striReference = "select * from iREFERENCE where tran_cd='" + str + "' and fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "' and typewise='" + objBLFD.HTMAIN["typewise"].ToString() + "'";//+ _tableNm[str].ToString() + "'";
                        DataSet dsettbliReference = objdblayer.dsquery(striReference);
                        if (dsettbliReference != null && dsettbliReference.Tables.Count != 0 && dsettbliReference.Tables[0].Rows.Count != 0)//fld exists in specific table
                        {
                            //if (objBLFD.HTMAIN["disp_pickup"] != null && objBLFD.HTMAIN["disp_pickup"].ToString() != "" && bool.Parse(objBLFD.HTMAIN["disp_pickup"].ToString()))
                            //{
                            objdblayer.execDeleteQuery("delete from iREFERENCE where tran_cd='" + str + "' and fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "' and typewise='" + objBLFD.HTMAIN["typewise"].ToString() + "'");//+ _tableNm[str].ToString() + "'";
                            //}
                        }
                        AutoOrderUpdate();
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "DM")
            {
                #region
                BL_FIELDS.Errormsg = "";
                string[] strValidIn = objBLFD.HTMAIN["code"].ToString().Split(',');
                Hashtable _tableNm = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                Hashtable _htcond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                DataSet dsettbl_nm;
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,main_tbl_nm tbl_nm from tran_set where code in ('" + objBLFD.HTMAIN["code"].ToString() + "')");
                }
                else
                {
                    dsettbl_nm = objdblayer.dsquery("select distinct code,item_tbl_nm tbl_nm from tran_set where code in ('" + objBLFD.HTMAIN["code"].ToString() + "')");
                }
                if (dsettbl_nm != null && dsettbl_nm.Tables.Count != 0 && dsettbl_nm.Tables[0].Rows.Count != 0)//fld exists
                {
                    foreach (DataRow row in dsettbl_nm.Tables[0].Rows)
                    {
                        _tableNm[row["code"].ToString()] = row["tbl_nm"].ToString();
                    }
                }
                foreach (string str in strValidIn)
                {
                    if (_tableNm.Contains(str))//transaction exist.
                    {
                        DataSet dset = objdblayer.dsquery("select * from dc_mast where fld_nm='" + ACTIVE_BL.HTMAIN["fld_nm"].ToString() + "' and code!='" + str + "' and tbl_nm='" + _tableNm[str].ToString() + "'");
                        if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)//fld exists
                        {
                            BL_FIELDS.Errormsg = "Already Exist in the Table.";
                            // return false;
                        }
                        else
                        {
                            string strQuery = "";
                            strQuery = "ALTER TABLE " + _tableNm[str].ToString() + " DROP COLUMN " + objBLFD.HTMAIN["fld_nm"].ToString();
                            if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                            {
                                BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Table is not successful.";
                                return false;
                            }
                            if (!bool.Parse(objBLFD.HTMAIN["typewise"].ToString()) && objBLFD.HTMAIN["bef_aft"].ToString() == "2")
                            {
                                strQuery = "ALTER TABLE " + _tableNm[str].ToString().Replace("ITEM", "MAIN") + " DROP COLUMN " + objBLFD.HTMAIN["fld_nm"].ToString();
                                if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                                {
                                    BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Table is not successful.";
                                    return false;
                                }
                            }
                            if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                            {
                                DataSet dsetper = objdblayer.dsquery("select * from dc_mast where pert_name='" + ACTIVE_BL.HTMAIN["pert_name"].ToString() + "' and code!='" + str + "' and tbl_nm='" + _tableNm[str].ToString() + "'");
                                if (dsetper != null && dsetper.Tables.Count != 0 && dsetper.Tables[0].Rows.Count != 0)//fld exists
                                {
                                    BL_FIELDS.Errormsg = "Exist in other Table.";
                                }
                                else
                                {
                                    strQuery = "";
                                    if (ACTIVE_BL.HTMAIN.Contains("disp_pert") && ACTIVE_BL.HTMAIN["disp_pert"] != null && ACTIVE_BL.HTMAIN["disp_pert"].ToString() != "" && bool.Parse(ACTIVE_BL.HTMAIN["disp_pert"].ToString()))
                                    {
                                        strQuery = "ALTER TABLE " + _tableNm[str].ToString() + " DROP COLUMN " + objBLFD.HTMAIN["pert_name"].ToString();
                                        if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                                        {
                                            BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Table is not successful.";
                                            return false;
                                        }
                                        if (!bool.Parse(objBLFD.HTMAIN["typewise"].ToString()) && objBLFD.HTMAIN["bef_aft"].ToString() == "2")
                                        {
                                            strQuery = "ALTER TABLE " + _tableNm[str].ToString().Replace("ITEM", "MAIN") + " DROP COLUMN " + objBLFD.HTMAIN["pert_name"].ToString();
                                            if (!objdblayer.execDeleteQuery(strQuery))//fld exists in specific table
                                            {
                                                BL_FIELDS.Errormsg = "Sorry deleting %Field Name in the Table is not successful.";
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        string strQuerydel = "delete from dc_mast where fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "' and code='" + str + "'";
                        if (!objdblayer.execDeleteQuery(strQuerydel))//fld exists in specific table
                        {
                            BL_FIELDS.Errormsg = "Sorry deleting Field Name in the Discount & Charges Master table is not successful.";
                            return false;
                        }
                        string strQueryUpdate = "update dc_mast set validity=replace(replace(replace(validity,'," + str + "',''),'" + str + ",',''),'" + str + "','')  where fld_nm='" + objBLFD.HTMAIN["fld_nm"].ToString() + "'";
                        if (!objdblayer.execDeleteQuery(strQueryUpdate))//fld exists in specific table
                        {
                            BL_FIELDS.Errormsg = "Sorry Updating Field Name in the DC table is not successful.";
                            return false;
                        }
                        //AutoOrderUpdate();
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "TS")
            {
                #region
                Hashtable hashcon = new Hashtable();
                hashcon["Code"] = objBLFD.HTMAIN["Code"].ToString();
                bool blnFlg = objdblayer.execDelete("iBASEFIELDS", hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting " + objBLFD.HTMAIN["Tran_nm"].ToString() + " in " + objBLFD.Tran_nm + " is not Successful";
                    return false;
                }
                else
                {
                    objdblayer.execDeleteQuery("delete from ireference WHERE tran_cd='" + objBLFD.HTMAIN["Code"].ToString() + "' or valid_mast='" + objBLFD.HTMAIN["Code"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");                    
                    objdblayer.execQuery("update tran_set set ref_type=replace(replace(replace(valid_tran,'," + objBLFD.HTMAIN["Code"].ToString() + "',''),'" + objBLFD.HTMAIN["Code"].ToString() + ",',''),'" + objBLFD.HTMAIN["Code"].ToString() + "','') where compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                    if (objBLFD.HTMAIN.Contains("isSchedule") && objBLFD.HTMAIN["isSchedule"] != null && objBLFD.HTMAIN["isSchedule"].ToString() != "")
                    {
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "SS")
            {
                #region
                BL_FIELDS.Errormsg = "";
                string[] strValidIn = objBLFD.HTMAIN["validity"].ToString().Split(',');
                foreach (string str in strValidIn)
                {
                    if (str != "")
                    {
                        DataSet dset = objdblayer.dsquery("select * from tran_set where code='" + str + "'");
                        if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
                        {
                            DataSet dset1 = objdblayer.dsquery("select * from " + dset.Tables[0].Rows[0]["main_tbl_nm"].ToString() + " where tran_cd='" + str + "' and sr_id='" + objBLFD.HTMAIN["sr_id"].ToString() + "'");
                            if (dset1 != null && dset1.Tables.Count != 0 && dset1.Tables[0].Rows.Count != 0)
                            {
                                BL_FIELDS.Errormsg = "Cannot be deleted as it is referred in Transaction";
                                return false;
                            }
                        }
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "TM")
            {
                #region
                Hashtable hashcon = new Hashtable();
                hashcon[objBLFD.Primary_id] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                if (objBLFD.HTMAIN.Contains("theme_nm") && objBLFD.HTMAIN["theme_nm"].ToString().ToLower() == "default")
                {
                    BL_FIELDS.Errormsg = "Deleting Default Theme is not Possible";
                    return false;
                }
                else
                {
                    bool blnFlg = objdblayer.execDelete("CONTROL_SET", hashcon);
                    if (!blnFlg)
                    {
                        BL_FIELDS.Errormsg = "Deleting Theme is not Successful";
                        return false;
                    }
                }
                #endregion
            }
            if (ACTIVE_BL.Code == "PM" || ACTIVE_BL.Code == "QM" || ACTIVE_BL.Code == "CP")
            {
                #region
                Hashtable hashcon = new Hashtable();
                hashcon[objBLFD.Primary_id] = objBLFD.Tran_id;
                hashcon["tran_cd"] = objBLFD.Code;
                bool blnFlg = objdblayer.execDelete(objBLFD.Item_tbl_nm, hashcon);
                if (!blnFlg)
                {
                    BL_FIELDS.Errormsg = "Deleting " + objBLFD.Tran_nm + " is not Successful";
                    return false;
                }
                #endregion
            }
            //if (ACTIVE_BL.Code == "QM")
            //{
            //    #region
            //    Hashtable hashcon = new Hashtable();
            //    hashcon[objBLFD.Primary_id] = objBLFD.Tran_id;
            //    hashcon["tran_cd"] = objBLFD.Code;
            //    bool blnFlg = objdblayer.execDelete(objBLFD.Item_tbl_nm, hashcon);
            //    if (!blnFlg)
            //    {
            //        BL_FIELDS.Errormsg = "Deleting Process is not Successful";
            //        return false;
            //    }
            //    #endregion
            //}
            if (ACTIVE_BL.Code == "MM")
            {
                Hashtable htparam = new Hashtable();
                htparam.Add("@SearchStr", objBLFD.HTMAIN["mac_nm"].ToString());
                htparam.Add("@fld_nm", "mac_nm");
                DataSet dsetMachine = objdblayer.dsprocedure("ISP_FIND_DELETING_FOREIGN_KEYS", htparam);
                if (dsetMachine != null && dsetMachine.Tables.Count != 0 && dsetMachine.Tables[0].Rows.Count != 0)
                {
                    BL_FIELDS.Errormsg = "Deleting Machine Name Some where it is Used.";
                    return false;
                }
            }
            if (objBLFD.IsFileAttach)
            {
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashFileUpload != null && objBLFD.HASHTABLES.HashFileUpload.Count != 0)
                {
                    objdblayer.execDeleteQuery("delete from FILE_UPLOAD where tran_id='" + objBLFD.Tran_id + "' and tran_cd='" + objBLFD.Code + "'");
                }
            }
            return true;
        }

        private string GetQuery()
        {
            string strquery = "";
            if (objBLFD.HTMAIN["type"].ToString() == "1")
            {
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
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
                    if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
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
                if (bool.Parse(objBLFD.HTMAIN["typewise"].ToString()))
                {
                    if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
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
                    if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
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
                //if (bool.Parse(objBLFD.HTMAIN["type"].ToString()))
                //{
                if (bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
                {
                    strquery = "select * from (select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head=1 and order_no>0 union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and data_ty!='TAB' and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0,parent_ctrl,data_ty from ibasefields where ibasefields.code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0))vw order by order_no,col_order_no";
                }
                else
                {
                    strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1,_tab _type,parent_ctrl,data_ty from icustomfields where code='" + objBLFD.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head!=1 and order_no>0 order by _tab,order_no,col_order_no";
                }
                //}
            }
            return strquery;
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
                    if (!bool.Parse(objBLFD.HTMAIN["disp_head"].ToString()))
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
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@Item", entry1.Key.ToString());
                    htparam.Add("@date", DateTime.Now);
                    htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                    htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
                    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);

                    if (dsetStock != null && dsetStock.Tables.Count != 0 && dsetStock.Tables[0].Rows.Count != 0)
                    {
                        if (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - decimal.Parse(entry1.Value.ToString()) >= 0)
                        {
                            return true;
                        }
                        else
                        {
                            BL_FIELDS.Errormsg = "Available quantity for item : " + entry1.Key.ToString() + " is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - decimal.Parse(entry1.Value.ToString())).ToString();
                            return false;
                        }
                    }
                    //else
                    //{
                    //    BL_FIELDS.Errormsg = "Sorry!! No Stock available for : " + entry1.Key.ToString();
                    //    return false;
                    //}
                }
            }
            return true;
        }
    }
}
