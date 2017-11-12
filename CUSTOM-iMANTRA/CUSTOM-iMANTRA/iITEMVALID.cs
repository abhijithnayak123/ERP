using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iITEMVALID
    {

        private iInit objinit = new iInit();

        private Hashtable _hashitemvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();

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
        public bool iITEMValidate()
        {
            bool flg = true;
            if (ValidateProduct())
            {
                //Sharanamma Jekeen on 11.08.13 05.15 PM
                //Inode Technologies Pvt. Ltd.
                //Update Rate Field in Item Grid with Price List or from item rate if it is not picked from other transaction.
                foreach (DictionaryEntry entry in objBLFD.HTITEM)
                {
                    if (((Hashtable)entry.Value).Count != 0 && objBLFD.Tran_mode != "view_mode")
                    {
                        if (((Hashtable)entry.Value).Contains("RATE"))
                        {
                            if (entry.Key.ToString() == _hashitemvalue["ptserial"].ToString())
                            {
                                if (_hashitemvalue["RATE"] == null || _hashitemvalue["RATE"].ToString() == "0.00" || Convert.ToDecimal(_hashitemvalue["RATE"].ToString()) == 0)
                                {
                                    DataSet dsVP = objdblayer.dsquery("select rate from vpitem where ac_nm='" + objBLFD.HTMAIN["AC_NM"].ToString().Replace("'","''") + "' and ac_id='" + objBLFD.HTMAIN["ac_id"].ToString() + "' and prod_nm='" + _hashitemvalue["PROD_NM"].ToString().Replace("'","''") + "' and prod_cd='" + _hashitemvalue["PROD_CD"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                                    if (dsVP != null && dsVP.Tables.Count != 0 && dsVP.Tables[0].Rows.Count != 0)
                                    {
                                        ((Hashtable)entry.Value)["RATE"] = dsVP.Tables[0].Rows[0]["RATE"].ToString();
                                        _hashitemvalue["RATE"] = dsVP.Tables[0].Rows[0]["RATE"].ToString();
                                    }
                                    else
                                    {
                                        DataSet dsCP = objdblayer.dsquery("select rate from cpitem where ac_nm='" + objBLFD.HTMAIN["AC_NM"].ToString().Replace("'","''") + "' and ac_id='" + objBLFD.HTMAIN["ac_id"].ToString() + "' and prod_nm='" + _hashitemvalue["PROD_NM"].ToString().Replace("'","''") + "' and prod_cd='" + _hashitemvalue["PROD_CD"].ToString() + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
                                        if (dsCP != null && dsCP.Tables.Count != 0 && dsCP.Tables[0].Rows.Count != 0)
                                        {
                                            ((Hashtable)entry.Value)["RATE"] = dsCP.Tables[0].Rows[0]["RATE"].ToString();
                                            _hashitemvalue["RATE"] = dsCP.Tables[0].Rows[0]["RATE"].ToString();
                                        }
                                    }
                                    if (_hashitemvalue["RATE"] == null || _hashitemvalue["RATE"].ToString() == "0.00" || Convert.ToDecimal(_hashitemvalue["RATE"].ToString()) == 0)
                                    {
                                        DataSet dsPTMast = objdblayer.dsquery("select CUR_COST from pt_mast where prod_nm='" + ((Hashtable)entry.Value)["PROD_NM"].ToString().Replace("'","''") + "'");
                                        if (dsPTMast != null && dsPTMast.Tables.Count != 0 && dsPTMast.Tables[0].Rows.Count != 0)
                                        {
                                            ((Hashtable)entry.Value)["RATE"] = dsPTMast.Tables[0].Rows[0]["CUR_COST"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //end
                if (objBLFD.Code == "LI" || objBLFD.Code == "CE" || ACTIVE_BL.Code=="PP")
                {
                    if ((objBLFD.ObjControlSet.neg_stk != null && objBLFD.ObjControlSet.neg_stk.ToString() != "") ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
                    {
                        frmstkstatus stockstatus = new frmstkstatus();
                        stockstatus.ACTIVE_BL = objBLFD;
                        stockstatus.Hashstkvalue = _hashitemvalue;
                        stockstatus.ShowDialog();
                    }
                }
                if (objBLFD.Code == "SE")
                {
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Count != 0 && objBLFD.Tran_mode == "add_mode")
                        {
                            if (((Hashtable)entry.Value)["RATE"] == null || ((Hashtable)entry.Value)["RATE"].ToString() == "0.00" || ((Hashtable)entry.Value)["RATE"].ToString() == "0")
                            {
                                DataSet dsname = objdblayer.dsquery("select prod_cd,prod_nm,rate from pt_mast where prod_nm='" + ((Hashtable)entry.Value)["PROD_NM"].ToString().Replace("'","''") + "'");
                                if (dsname != null && dsname.Tables[0].Rows.Count != 0)
                                {
                                    ((Hashtable)entry.Value)["RATE"] = dsname.Tables[0].Rows[0]["RATE"].ToString();
                                }
                            }
                        }
                    }
                }
                if (objBLFD.Code == "BO")
                {
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            if (_hashitemvalue.ContainsKey("prod_nm"))
                            {
                                if (((Hashtable)entry.Value)["ptserial"].ToString() != _hashitemvalue["ptserial"].ToString())
                                {
                                    if (((Hashtable)entry.Value)["prod_nm"].ToString() == _hashitemvalue["prod_nm"].ToString())
                                    {
                                        flg = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (!flg)
                    {
                        bL_FIELDS.Errormsg = "Sorry! Same Product Adding not Allowed";
                        return false;
                    }
                }
                return true;
            }
            else
            {
                if (bL_FIELDS.Errormsg.Length == 0)
                {
                    bL_FIELDS.Errormsg = "Product is not Valid";
                }
                return false;
            }
        }

        public bool ValidateProduct()
        {
            bL_FIELDS.Errormsg = "";
            DataSet dsname = objdblayer.dsquery("select prod_cd,prod_nm,prod_desc,prod_ty_nm,uom from pt_mast where prod_nm='" + _hashitemvalue["PROD_NM"].ToString().Replace("'","''") + "'");
            if (dsname != null && dsname.Tables.Count != 0 && dsname.Tables[0].Rows.Count != 0)
            {
                bool flg = true;
                if (ACTIVE_BL.Tran_mode == "edit_mode")
                {
                    //if (objBLFD.Ref_type != null && objBLFD.Ref_type != "")
                    //{
                    DataSet dsprod = objdblayer.dsquery("select prod_nm,prod_cd from " + objBLFD.Item_tbl_nm + " where ptserial='" + _hashitemvalue["ptserial"] + "' and tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "'");
                    if (dsprod != null && dsprod.Tables.Count != 0 && dsprod.Tables[0].Rows.Count != 0)
                    {
                        if (dsprod.Tables[0].Rows[0]["prod_nm"].ToString().ToLower() != _hashitemvalue["PROD_NM"].ToString().ToLower())
                        {
                            DataSet dsetreferCode = objdblayer.dsquery("select behavier_cd refer_cd from tran_set where ref_type in ('" + objBLFD.Code + "')");
                            if (dsetreferCode != null && dsetreferCode.Tables.Count != 0 && dsetreferCode.Tables[0].Rows.Count != 0)
                            {
                                DataSet dsetproduct = objdblayer.dsquery("select count(*) cnt from " + dsetreferCode.Tables[0].Rows[0]["refer_cd"].ToString() + "ref where ref_ptserial='" + _hashitemvalue["ptserial"] + "' and ref_tran_id='" + objBLFD.Tran_id.ToString() + "' and ref_tran_cd='" + objBLFD.Code + "'");//objdblayer.dsprocedure("ISP_Check_Edit_Product", htparam);
                                if (dsetproduct != null && dsetproduct.Tables.Count != 0 && dsetproduct.Tables[0].Rows.Count != 0)
                                {
                                    if (int.Parse(dsetproduct.Tables[0].Rows[0]["cnt"].ToString()) > 0)
                                    {
                                        BL_FIELDS.Errormsg = "Sorry!! This Entry already refered in another transaction";
                                        flg = false;
                                    }
                                }
                            }
                        }
                    }
                    //}
                }
                if (flg)
                {
                    if (_hashitemvalue.ContainsKey("prod_cd"))
                    {
                        _hashitemvalue["prod_cd"] = dsname.Tables[0].Rows[0]["prod_cd"].ToString();
                    }
                    if (_hashitemvalue.ContainsKey("PROD_DESCRIP"))
                    {
                        _hashitemvalue["PROD_DESCRIP"] = dsname.Tables[0].Rows[0]["prod_desc"].ToString();
                    }
                    if (_hashitemvalue.ContainsKey("prod_ty_nm"))
                    {
                        _hashitemvalue["prod_ty_nm"] = dsname.Tables[0].Rows[0]["prod_ty_nm"].ToString();
                    }
                    if (_hashitemvalue.ContainsKey("uom") && objBLFD.Code == "BO")
                    {
                        _hashitemvalue["uom"] = dsname.Tables[0].Rows[0]["uom"].ToString();
                    }

                    if (objBLFD.TRAN_CD == "PE" || objBLFD.TRAN_CD == "SE")
                    {
                        foreach (DictionaryEntry entry in objBLFD.HTITEM)
                        {
                            if (((Hashtable)entry.Value).Count != 0)
                            {
                                if (_hashitemvalue.ContainsKey("prod_ty_nm"))
                                {
                                    if (((Hashtable)entry.Value)["prod_ty_nm"].ToString() == "MACHINERY/STORES")
                                    {
                                        if (((Hashtable)entry.Value)["prod_ty_nm"].ToString() != _hashitemvalue["prod_ty_nm"].ToString())
                                        {
                                            flg = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (_hashitemvalue["prod_ty_nm"].ToString() == "MACHINERY/STORES")
                                        {
                                            flg = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (!flg)
                        {
                            bL_FIELDS.Errormsg = "You conn't combine Raw Material & Capital Goods in single Purchase";
                        }
                        return flg;
                    }
                }
                return flg;
            }
            else
            {
                BL_FIELDS.Errormsg = "Invalid Product";
                return false;
            }
        }
    }
}
