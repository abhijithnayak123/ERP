using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;
using System.Data;

namespace CUSTOM_iMANTRA
{
    public class DeleteItem
    {
        private Hashtable _hashitemvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();
        BLHT objhashtables = new BLHT();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        dblayer objdblayer = new dblayer();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public Hashtable Hashitemvalue
        {
            get { return _hashitemvalue; }
            set { _hashitemvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public bool DeleteTransactionItem()
        {
            bL_FIELDS.Errormsg = "";
            if (ACTIVE_BL.Stk_effect == "+")
            {
                if (!StockCheck() && ACTIVE_BL.Tran_mode == "edit_mode")
                    return false;
            }
            if (ACTIVE_BL.Code == "LR")
            {
                objhashtables = ACTIVE_BL.HASHTABLES;
                if (objhashtables != null)
                {
                    foreach (DictionaryEntry entry in objhashtables.HashIssueAndReceipt)
                    {
                        if (entry.Key.ToString().Split(',')[0] == _hashitemvalue["ptserial"].ToString())
                        {
                            objhashtables.HashIssueAndReceipt.Remove(entry.Key);
                            break;
                        }
                    }
                }
                objBLFD.HASHTABLES = objhashtables;
            }
            if (ACTIVE_BL.IsSchedule || ACTIVE_BL.Code == "PH" || ACTIVE_BL.Code == "PB" || ACTIVE_BL.Code == "DS")
            {
                objhashtables = ACTIVE_BL.HASHTABLES;
                if (objhashtables != null)
                {
                    foreach (DictionaryEntry entry in objhashtables.HashMaintbl)
                    {
                        if (entry.Key.ToString().Split(',')[0] == _hashitemvalue["ptserial"].ToString())
                        {
                            DataSet dsetDSSchedule = objdblayer.dsquery("select * from ide_schedule where ref_tran_id=" + objBLFD.Tran_id + " and ref_tran_cd='" + objBLFD.Code + "' and ref_ptserial='" + _hashitemvalue["ptserial"].ToString() + "'");
                            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
                            {
                                BL_FIELDS.Errormsg = "Sorry Scheduling Item Refered in De-Schedule";
                                return false;
                            }
                            else
                            {
                                objhashtables.HashMaintbl.Remove(entry.Key);
                                break;
                            }
                        }
                    }
                }
                objBLFD.HASHTABLES = objhashtables;
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                objhashtables = ACTIVE_BL.HASHTABLES;
                if (objhashtables != null)
                {
                    foreach (DictionaryEntry entry in objhashtables.HashDeallocateSchedule)
                    {
                        if (entry.Key.ToString().Split(',')[0] == _hashitemvalue["ptserial"].ToString())
                        {
                            objhashtables.HashDeallocateSchedule.Remove(entry.Key);
                            break;
                        }
                    }
                }
                objBLFD.HASHTABLES = objhashtables;
            }
            if (ACTIVE_BL.Code == "WO")
            {
                DataSet dsetpurref = objdblayer.dsquery("select count(*) cnt from IP_WO_DET where  ref_tran_id='" + ACTIVE_BL.Tran_id + "' and ref_tran_cd='" + ACTIVE_BL.Code + "' and ref_ptserial='" + Hashitemvalue["PTSERIAL"].ToString() + "'");
                if (dsetpurref != null && dsetpurref.Tables[0].Rows.Count != 0)
                {
                    if (int.Parse(dsetpurref.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        BL_FIELDS.Errormsg = "Deleting this Work Order Item is not posible, Reason: Work Order Item used in Consumption";
                        return false;
                    }
                }
            }
            return true;
        }

        private bool StockCheck()
        {
            if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
            {
                decimal original_qty = 0;

                DataSet dsetQty = objdblayer.dsquery("select isnull(sum(isnull(qty,0)),0) qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashitemvalue["PROD_NM"].ToString().Replace("'","''") + "'");
                if (dsetQty != null && dsetQty.Tables.Count != 0 && dsetQty.Tables[0].Rows.Count != 0)
                {
                    original_qty = decimal.Parse(dsetQty.Tables[0].Rows[0]["qty"].ToString());
                }

                Hashtable htparam = new Hashtable();
                htparam.Add("@Item", _hashitemvalue["prod_nm"].ToString());
                htparam.Add("@date", DateTime.Now);
                htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
                DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);

                if (dsetStock != null && dsetStock.Tables.Count != 0 && dsetStock.Tables[0].Rows.Count != 0)
                {
                    if (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - original_qty + decimal.Parse(_hashitemvalue["qty"].ToString()) >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        BL_FIELDS.Errormsg = "Available quantity for item : " + _hashitemvalue["PROD_NM"].ToString().Replace("'","''") + " is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - original_qty + decimal.Parse(_hashitemvalue["qty"].ToString())).ToString();
                        return false;
                    }
                }
                //else
                //{
                //    BL_FIELDS.Errormsg = "No Stock available for : " + _hashitemvalue["prod_nm"].ToString();
                //    return false;
                //}
            }
            return true;
        }
    }
}
