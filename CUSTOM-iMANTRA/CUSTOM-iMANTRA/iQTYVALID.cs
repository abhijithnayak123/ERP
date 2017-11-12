using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using iMANTRA_BL;
using System.Data;
using System.Windows.Forms;

namespace CUSTOM_iMANTRA
{
    public class iQTYVALID
    {
        dblayer objdblayer = new dblayer();

        private Hashtable _hashqtyvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public Hashtable Hashqtyvalue
        {
            get { return _hashqtyvalue; }
            set { _hashqtyvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public bool iQTYValidate()
        {
            try
            {
                //if (objBLFD.Code == "CE")
                //{
                //    return iQTYValidateOpening();
                //}
                //else
                //{
                if (objBLFD.Tran_mode == "edit_mode")
                {
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@atran_id", objBLFD.Tran_id);
                    htparam.Add("@atran_cd", objBLFD.Code);
                    htparam.Add("@aptserial", _hashqtyvalue["PTSERIAL"].ToString());
                    htparam.Add("@abehaiver_cd", objBLFD.Behavier_cd);

                    //To check referred qty 
                    DataSet dsetpurref = objdblayer.dsprocedure("ISP_Check_Edit_Qty", htparam);
                    if (dsetpurref != null && dsetpurref.Tables.Count != 0 && dsetpurref.Tables[0].Rows.Count != 0)
                    {
                        if (decimal.Parse(dsetpurref.Tables[0].Rows[0]["qty"].ToString()) > decimal.Parse(_hashqtyvalue["qty"].ToString()))
                        {
                            bL_FIELDS.Errormsg = "Sorry Changing qty is refered in other transaction";
                            return false;
                        }
                    }
                    //To check Referring qty
                    if (objBLFD.Ref_type != null && objBLFD.Ref_type != "")
                    {
                        htparam.Add("@arefbeh_cd", objBLFD.Ref_behaiver_cd);
                        htparam.Add("@aref_code", objBLFD.Ref_type);                        
                        DataSet dsetpurref1 = objdblayer.dsprocedure("ISP_Check_Edit_More_Qty", htparam);
                        if (dsetpurref1 != null && dsetpurref1.Tables.Count != 0 && dsetpurref1.Tables[0].Rows.Count != 0)
                        {
                            if (decimal.Parse(dsetpurref1.Tables[0].Rows[0]["qty"].ToString()) < decimal.Parse(_hashqtyvalue["qty"].ToString()))
                            {
                                bL_FIELDS.Errormsg = "Sorry qty should be less or equal to refering qty";
                                return false;
                            }
                        }
                    }
                }

                if (ACTIVE_BL.Code == "LI" || ACTIVE_BL.Code == "SE" || ACTIVE_BL.Code == "CE" || ACTIVE_BL.Code=="PP")
                {
                    if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
                    {
                      return StockCheck();
                    }
                }
                if (ACTIVE_BL.Code == "WO")
                {
                    DataSet dsetworef = objdblayer.dsquery("select ISNULL(SUM(isnull(main_issued_qty,0)),0) qty from IP_WO_DET where ref_tran_id='"+objBLFD.Tran_id+"' and ref_tran_cd='"+objBLFD.Code+"' and ref_ptserial='"+_hashqtyvalue["ptserial"].ToString()+"' and compid='"+ACTIVE_BL.ObjCompany.Compid.ToString()+"'");
                    if (dsetworef != null && dsetworef.Tables.Count != 0 && dsetworef.Tables[0].Rows.Count != 0)
                    {
                        if (decimal.Parse(dsetworef.Tables[0].Rows[0]["qty"].ToString()) > decimal.Parse(_hashqtyvalue["qty"].ToString()))
                        {
                            bL_FIELDS.Errormsg = "Sorry Changing qty is refered in other transaction";
                            return false;
                        }
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                bL_FIELDS.Errormsg = ex.Message;
                return false;
            }
            return true;
        }

        public bool StockCheck()
        {
            string item_nm = _hashqtyvalue["PROD_NM"].ToString();
            decimal total_qty = 0, assigned_qty = 0, original_qty = 0;
            foreach (DictionaryEntry entry in objBLFD.HTITEM)
            {
                original_qty = 0;
                if (((Hashtable)entry.Value).Count != 0 && item_nm == ((Hashtable)entry.Value)["PROD_NM"].ToString())
                {
                    if (((Hashtable)entry.Value)["QTY"] != null && ((Hashtable)entry.Value)["QTY"].ToString() != "")
                        total_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                    if (_hashqtyvalue["PTSERIAL"].ToString() != entry.Key.ToString())
                    {
                        DataSet dsetQty = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashqtyvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + entry.Key.ToString() + "'");
                        if (dsetQty != null && dsetQty.Tables[0].Rows.Count != 0)
                        {
                            original_qty = decimal.Parse(dsetQty.Tables[0].Rows[0]["qty"].ToString());
                        }
                        if (original_qty != decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString()))
                        {
                            assigned_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                        }
                    }
                }
            }
            Hashtable htparam = new Hashtable();
            htparam.Add("@Item", _hashqtyvalue["PROD_NM"].ToString());
            htparam.Add("@date", _hashqtyvalue["TRAN_DT"].ToString());
            htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
            htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
            DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);
            original_qty = 0;
            DataSet dsetQty1 = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashqtyvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + _hashqtyvalue["PTSERIAL"].ToString() + "'");
            if (dsetQty1 != null && dsetQty1.Tables[0].Rows.Count != 0)
            {
                original_qty = decimal.Parse(dsetQty1.Tables[0].Rows[0]["qty"].ToString());
            }
            if (dsetStock != null && dsetStock.Tables[0].Rows.Count != 0)
            {
                if (decimal.Parse(_hashqtyvalue["QTY"].ToString()) <= decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) + original_qty - assigned_qty)
                {
                    return true;
                }
                else
                {
                    bL_FIELDS.Errormsg = "Available quantity for item : " + item_nm + " is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) + original_qty - assigned_qty).ToString();
                    return false;
                }
            }
            return true;
            //else
            //{
            //    bL_FIELDS.Errormsg = "No Stock available for : " + item_nm;
            //    return false;
            //}
        }
        public bool iQTYValidateOpening()
        {
            try
            {
                #region
                //if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
                //{
                //    string item_nm = _hashqtyvalue["PROD_NM"].ToString();
                //    decimal total_qty = 0;
                //    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                //    {
                //        if (((Hashtable)entry.Value).Count != 0 && item_nm == ((Hashtable)entry.Value)["PROD_NM"].ToString())
                //        {
                //            if (((Hashtable)entry.Value)["QTY"] != null && ((Hashtable)entry.Value)["QTY"].ToString() != "")
                //                total_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                //        }
                //    }
                //    Hashtable htparam = new Hashtable();
                //    htparam.Add("@Item", _hashqtyvalue["PROD_NM"].ToString());
                //    htparam.Add("@date", _hashqtyvalue["TRAN_DT"].ToString());
                //    htparam.Add("@TRAN_ID", objBLFD.Tran_id);
                //    //htparam.Add("@PTSERIAL", _hashqtyvalue["PTSERIAL"].ToString());
                //    htparam.Add("@RULE", objBLFD.HTMAIN["rule"].ToString());
                //    htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
                //    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS_OPENING", htparam);
                //    if (dsetStock != null && dsetStock.Tables[0].Rows.Count != 0)
                //    {
                //        if (total_qty <= decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()))
                //        {
                //            return true;
                //        }
                //        else
                //        {
                //            bL_FIELDS.Errormsg = "Available quantity for item : " + item_nm + " ( " + objBLFD.HTMAIN["rule"] + " ) is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - total_qty + decimal.Parse(_hashqtyvalue["qty"].ToString())).ToString();
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Stock is not available for " + item_nm + "  (" + objBLFD.HTMAIN["rule"] + " )");
                //        return false;
                //    }
                //}
                //return true;
                #endregion
                if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
                {
                    string item_nm = _hashqtyvalue["PROD_NM"].ToString();
                    decimal total_qty = 0, assigned_qty = 0, original_qty = 0;
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        original_qty = 0;
                        if (((Hashtable)entry.Value).Count != 0 && item_nm == ((Hashtable)entry.Value)["PROD_NM"].ToString())
                        {
                            if (((Hashtable)entry.Value)["QTY"] != null && ((Hashtable)entry.Value)["QTY"].ToString() != "")
                                total_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                            if (_hashqtyvalue["PTSERIAL"].ToString() != entry.Key.ToString())
                            {
                                DataSet dsetQty = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashqtyvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + entry.Key.ToString() + "'");
                                if (dsetQty != null && dsetQty.Tables[0].Rows.Count != 0)
                                {
                                    original_qty = decimal.Parse(dsetQty.Tables[0].Rows[0]["qty"].ToString());
                                }
                                if (original_qty != decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString()))
                                {
                                    assigned_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                                }
                            }
                        }
                    }
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@Item", _hashqtyvalue["PROD_NM"].ToString());
                    htparam.Add("@date", _hashqtyvalue["TRAN_DT"].ToString());
                    htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                    htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());
                    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);
                    original_qty = 0;
                    DataSet dsetQty1 = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashqtyvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + _hashqtyvalue["PTSERIAL"].ToString() + "'");
                    if (dsetQty1 != null && dsetQty1.Tables[0].Rows.Count != 0)
                    {
                        original_qty = decimal.Parse(dsetQty1.Tables[0].Rows[0]["qty"].ToString());
                    }
                    if (dsetStock != null && dsetStock.Tables[0].Rows.Count != 0)
                    {
                        if (decimal.Parse(_hashqtyvalue["QTY"].ToString()) <= decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) + original_qty - assigned_qty)
                        {
                            return true;
                        }
                        else
                        {
                            bL_FIELDS.Errormsg = "Available quantity for item : " + item_nm + " is :" + (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) + original_qty - assigned_qty).ToString();
                            return false;
                        }
                    }
                    else
                    {
                        bL_FIELDS.Errormsg = "No Stock available for : " + item_nm;
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                bL_FIELDS.Errormsg = ex.Message;
                return false;
            }
        }
    }
}
