using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iMASTER
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        dblayer objdblayer = new dblayer();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }
        public BL_BASEFIELD ACTIVE_MASTER
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public bool Validate()
        {
            if (ACTIVE_MASTER.Code == "OM")
            {
                if (ACTIVE_MASTER.Tran_mode != "view_mode")
                {
                    DataSet ds = objdblayer.dsquery("select compid,comp_nm from ORG_MAST where comp_nm = '" + ACTIVE_MASTER.HTMAIN["comp_nm"] + "' and code='" + ACTIVE_MASTER.HTMAIN["Code"] + "' and compid='" + objBLFD.Tran_id + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        BL_FIELDS.Errormsg = "Company Name already exists";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool ValidateProductGroup()
        {
            if (objBLFD.HTMAIN.Contains("PT_GRP_NM") && objBLFD.HTMAIN["PT_GRP_NM"] != null && objBLFD.HTMAIN["PT_GRP_NM"].ToString() != "")
            {
                DataSet dsname = objdblayer.dsquery("select PROD_TY_NM,PROD_TYID,UOM,CHAP_NO,CHAP_NM from prod_group where pt_grp_nm='" + objBLFD.HTMAIN["PT_GRP_NM"].ToString() + "'");
                if (dsname != null && dsname.Tables[0].Rows.Count != 0)
                {
                    objBLFD.HTMAIN["PROD_TY_NM"] = dsname.Tables[0].Rows[0]["PROD_TY_NM"].ToString();
                    objBLFD.HTMAIN["PROD_TYID"] = dsname.Tables[0].Rows[0]["PROD_TYID"].ToString();
                    objBLFD.HTMAIN["UOM"] = dsname.Tables[0].Rows[0]["UOM"].ToString();
                    objBLFD.HTMAIN["CHAP_NO"] = dsname.Tables[0].Rows[0]["CHAP_NO"].ToString();
                    objBLFD.HTMAIN["CHAP_NM"] = dsname.Tables[0].Rows[0]["CHAP_NM"].ToString();
                    objBLFD.HTMAIN["S_UOM"] = dsname.Tables[0].Rows[0]["UOM"].ToString();
                    objBLFD.HTMAIN["PUR_UNIT"] = dsname.Tables[0].Rows[0]["UOM"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool ValidateTaxName()
        {
            if (ACTIVE_MASTER.Code == "ST")
            {
                if (ACTIVE_MASTER.Tran_mode == "add_mode")
                {
                    DataSet ds = objdblayer.dsquery("select tax_nm,code from st_mast where tax_nm = '" + ACTIVE_MASTER.HTMAIN["Tax_nm"] + "' and code='" + ACTIVE_MASTER.HTMAIN["Code"] + "'");
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        BL_FIELDS.Errormsg = "Tax Name for this transaction already exists";
                        return false;
                    }
                }
            }
            if (ACTIVE_MASTER.Code == "SS")
            {
                if (ACTIVE_MASTER.Tran_mode == "add_mode")
                {
                    DataSet ds = objdblayer.dsquery("select tran_sr from series where tran_sr='" + ACTIVE_MASTER.HTMAIN["Tran_sr"] + "'");
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        BL_FIELDS.Errormsg = "Series already exists";
                        return false;
                    }
                }
            }
            return true;
        }
        public bool CopyCode()
        {
            if (ACTIVE_MASTER.HTMAIN.Contains("TRAN_NM"))
            {
                DataSet ds = objdblayer.dsquery("select code,tran_cd from Tran_set where Tran_nm='" + ACTIVE_MASTER.HTMAIN["TRAN_NM"] + "'");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    ACTIVE_MASTER.HTMAIN["CODE"] = ds.Tables[0].Rows[0]["CODE"].ToString();
                }
            }
            return true;
        }
        public bool CheckEdit()
        {
            if (ACTIVE_MASTER.Tran_mode == "edit_mode")
            {
                DataSet ds2 = objdblayer.dsquery("select Behavier_cd  from TRAN_SET where code='" + ACTIVE_MASTER.HTMAIN["Code"] + "' ");
                string code = ds2.Tables[0].Rows[0]["Behavier_cd"].ToString();
                DataSet ds1 = objdblayer.dsquery("select ST_MAST.tax_nm,ST_MAST.pert_val from " + code + "MAIN Inner Join ST_MAST ON(ST_MAST.TAX_NM=" + code + "MAIN.TAX_NM) and ST_MAST.code= " + code + "MAIN.Tran_cd where ST_MAST.tax_nm='" + ACTIVE_MASTER.HTMAIN["TAX_NM"] + "' and " + code + "MAIN.tran_cd='" + ACTIVE_MASTER.HTMAIN["Code"] + "'");
                if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    if (ACTIVE_MASTER.HTMAIN["Tax_nm"].ToString() == ds1.Tables[0].Rows[0]["Tax_nm"].ToString())
                    {
                        if (Decimal.Parse(ACTIVE_MASTER.HTMAIN["PERT_VAL"].ToString()) != Decimal.Parse(ds1.Tables[0].Rows[0]["PERT_VAL"].ToString()))
                        {
                            BL_FIELDS.Errormsg = "Editing this field is not posible, Reason: Used in Transaction";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public bool CheckWidth()
        {
            if (ACTIVE_MASTER.HTMAIN.Contains("ECC_NO"))
            {
                if (ACTIVE_MASTER.HTMAIN["ECC_NO"].ToString().Length > 0)
                {
                    if (ACTIVE_MASTER.HTMAIN["ECC_NO"].ToString().Length > 15 || ACTIVE_MASTER.HTMAIN["ECC_NO"].ToString().Length < 15)
                    {
                        BL_FIELDS.Errormsg = "ECC NO should be 15 digits. You have entered " + ACTIVE_MASTER.HTMAIN["ECC_NO"].ToString().Length + " digits";
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }

            if (ACTIVE_MASTER.HTMAIN.Contains("CHAP_NO"))
            {
                if (ACTIVE_MASTER.HTMAIN["CHAP_NO"].ToString().Length > 0)
                {
                    if (ACTIVE_MASTER.HTMAIN["CHAP_NO"].ToString().Length > 8 || ACTIVE_MASTER.HTMAIN["CHAP_NO"].ToString().Length < 8)
                    {
                        BL_FIELDS.Errormsg = "Chapter NO should be 8 digits. You have entered " + ACTIVE_MASTER.HTMAIN["CHAP_NO"].ToString().Length + " digits";
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }

            if (ACTIVE_MASTER.HTMAIN.Contains("PAN_NO"))
            {
                if (ACTIVE_MASTER.HTMAIN["PAN_NO"].ToString().Length > 0)
                {
                    if (ACTIVE_MASTER.HTMAIN["PAN_NO"].ToString().Length > 10 || ACTIVE_MASTER.HTMAIN["PAN_NO"].ToString().Length < 8)
                    {
                        BL_FIELDS.Errormsg = "PAN NO should be 10 digits. You have entered " + ACTIVE_MASTER.HTMAIN["PAN_NO"].ToString().Length + " digits";
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            if (ACTIVE_MASTER.HTMAIN.Contains("IEC_NO"))
            {
                if (ACTIVE_MASTER.HTMAIN["IEC_NO"].ToString().Length > 0)
                {
                    if (ACTIVE_MASTER.HTMAIN["IEC_NO"].ToString().Length > 10 || ACTIVE_MASTER.HTMAIN["IEC_NO"].ToString().Length < 10)
                    {
                        BL_FIELDS.Errormsg = "IEC NO should be 15 digits. You have entered " + ACTIVE_MASTER.HTMAIN["IEC_NO"].ToString().Length + " digits";
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
    }
}
