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
    public class EditTransactionAndMaster
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
        public bool tsEditTransactionAndMaster()
        {
            if (ACTIVE_BL.Code == "LR")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashIssueAndReceipt.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "WO")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    objHashtables.HashItemtbl.Clear();
                    EditfrmBOM();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.IsFileAttach || objBLFD.Code == "EV")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashFileUpload.Clear();
                    EditFileUpload();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "CE" || ACTIVE_BL.Code == "PP")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    EditWOIP();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "PD")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    EditWOOP();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.IsSchedule || ACTIVE_BL.Code == "PH")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    EditSchedule();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                if (objHashtables != null)
                {
                    objHashtables.HashDeallocateSchedule.Clear();
                    EditDeSchedule();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            return true;
        }

        private void EditfrmBOM()
        {
            // GetDetails();
            string key = "";
            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aptserial", "");
            htparam.Add("@aprod_nm", "");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);

            DataSet dsetWOwithBOM = objdblayer.dsprocedure("ISP_GET_WO_BO_DETAILS", htparam);
            if (dsetWOwithBOM != null && dsetWOwithBOM.Tables.Count != 0 && dsetWOwithBOM.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetWOwithBOM.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString();
                    if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetWOwithBOM.Tables[0].Columns)
                        {
                            if (column.ColumnName.ToLower() != "req_qty" && column.ColumnName.ToLower() != "sel")
                            {
                                ((Hashtable)objHashtables.HashMaintbl[key]).Add(column.ColumnName, r[column.ColumnName]);
                            }
                        }
                    }

                    Hashtable htparam1 = new Hashtable();
                    htparam1.Add("@atran_id", objBLFD.Tran_id);
                    htparam1.Add("@atran_cd", objBLFD.Code);
                    htparam1.Add("@aptserial", r["PTSERIAL"].ToString());
                    htparam1.Add("@aprod_nm", r["bom_item"].ToString());
                    htparam1.Add("@abomid", r["bomid"].ToString());
                    htparam1.Add("@acompid", objBLFD.ObjCompany.Compid);

                    DataSet dsetWOwithBOMItem = objdblayer.dsprocedure("ISP_GET_WO_BO_ITEM_DETAILS", htparam1);
                    if (dsetWOwithBOMItem != null && dsetWOwithBOMItem.Tables.Count != 0 && dsetWOwithBOMItem.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow itemrow in dsetWOwithBOMItem.Tables[0].Rows)
                        {
                            key = r["PTSERIAL"].ToString() + "," + itemrow["prod_cd"].ToString();
                            if (objHashtables.HashItemtbl != null && !objHashtables.HashItemtbl.Contains(key))
                            {
                                objHashtables.HashItemtbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DataColumn column in dsetWOwithBOMItem.Tables[0].Columns)
                                {
                                    if (column.ColumnName.ToLower() != "rm_req_qty" && column.ColumnName.ToLower() != "sel")
                                    {
                                        ((Hashtable)objHashtables.HashItemtbl[key]).Add(column.ColumnName, itemrow[column.ColumnName]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }
        private void EditWOIP()
        {
            string key = "";

            Hashtable htparam = new Hashtable();
            htparam.Add("@awo_id", 0);
            htparam.Add("@awo_cd", "");
            htparam.Add("@awoserial", "");
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");

            //DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM_SELECTED", htparam);
            DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM_SELECTED", htparam);
            if (dsetIPwithWOItem != null && dsetIPwithWOItem.Tables.Count != 0 && dsetIPwithWOItem.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetIPwithWOItem.Tables[0].Rows)
                {
                    key = r["ref_tran_id1"].ToString() + "," + r["ref_ptserial1"].ToString() + "," + r["ref_prod_cd1"].ToString() + "," + r["prod_cd"].ToString();
                    if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetIPwithWOItem.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashMaintbl[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }
         private void EditFileUpload()
        {
            string key = "";
            DataSet dsetFileUpload = objdblayer.dsquery("select * from FILE_UPLOAD where tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
            if (dsetFileUpload != null && dsetFileUpload.Tables.Count != 0 && dsetFileUpload.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetFileUpload.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["si_no"].ToString();
                    if (objHashtables != null && objHashtables.HashFileUpload != null && !objHashtables.HashFileUpload.Contains(key))
                    {
                        objHashtables.HashFileUpload[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetFileUpload.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashFileUpload[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
        }
        private void EditWOOP()
        {
            #region
            string key = "";

            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);

            DataSet dsetOPwithWOItem = objdblayer.dsprocedure("ISP_GET_OPWO_ITEM_SELECTED", htparam);
            if (dsetOPwithWOItem != null && dsetOPwithWOItem.Tables.Count != 0 && dsetOPwithWOItem.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetOPwithWOItem.Tables[0].Rows)
                {
                    key = r["ref_tran_id"].ToString() + "," + r["ref_ptserial"].ToString() + "," + r["ref_prod_cd"].ToString();
                    if (r["Sel"].ToString() == "1")
                    {
                        if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                        {
                            objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DataColumn column in dsetOPwithWOItem.Tables[0].Columns)
                            {
                                //if (r[column.ColumnName].ToString() == "1")
                                //{
                                ((Hashtable)objHashtables.HashMaintbl[key])[column.ColumnName] = r[column.ColumnName];
                                //}
                            }
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
            #endregion
        }
        private void EditDeSchedule()
        {
            string key = "";

            Hashtable htparam = new Hashtable();
            htparam.Add("@aref_tran_id", 0);
            htparam.Add("@aref_tran_cd", "");
            htparam.Add("@aref_ptserial", "");
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@aptserial", "");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

            DataSet dsetDSSchedule = objdblayer.dsprocedure("ISP_DISPATCH_QTY_EDIT", htparam);

            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetDSSchedule.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["schedule_no"].ToString();
                    if (objHashtables.HashDeallocateSchedule != null && !objHashtables.HashDeallocateSchedule.Contains(key))
                    {
                        objHashtables.HashDeallocateSchedule[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetDSSchedule.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashDeallocateSchedule[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }
        private void EditSchedule()
        {
            string key = "";
            DataSet dsetDSSchedule = objdblayer.dsquery("select ISCHEDULE.schedule_id,ISCHEDULE.schedule_no,schedule_dt,schedule_qty,tran_no,tran_id,ptserial,compid,fin_yr,isnull(sum_de_schedule_qty,0) dis_schedule_qty from ISCHEDULE LEFT JOIN IVW_DISPATCH_QTY vw ON iSCHEDULE.tran_id=vw.ref_tran_id and iSCHEDULE.tran_cd=vw.ref_tran_cd and iSCHEDULE.tran_no=vw.ref_tran_no and iSCHEDULE.ptserial=vw.ref_ptserial  and iSCHEDULE.schedule_id=vw.schedule_id where tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetDSSchedule.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["schedule_no"].ToString();
                    if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetDSSchedule.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashMaintbl[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }
    }
}
