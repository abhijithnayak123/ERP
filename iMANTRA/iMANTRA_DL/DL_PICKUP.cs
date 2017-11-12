using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iMANTRA_BL;
using iMANTRA_iniL;
using System.Collections;

namespace iMANTRA_DL
{
    public class DL_PICKUP
    {
        public Company objCompany = new Company();
        Ini objIni = new Ini();

        DL_ADAPTER objdlAdapter = new DL_ADAPTER();
        Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        SqlConnection con;

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        public DataSet Get_Ref_Main_Grid(BL_BASEFIELD objBLFD, string refering_cd)
        {
            return objdlAdapter.dsquery("select * from ireference where valid_mast='" + refering_cd + "' and tran_cd='" + objBLFD.Code + "' and typewise=1 and compid='" + objBLFD.ObjCompany.Compid.ToString() + "' order by cast(order_no as int)");
        }

        public DataSet Get_PickUpDetails(BL_BASEFIELD objBLFD, string refering_cd, string tran_cd)
        {
            return objdlAdapter.dsquery("if exists(select * from ireference where tran_cd='" + tran_cd + "' and valid_mast='" + refering_cd + "'  and  compid='" + objBLFD.ObjCompany.Compid.ToString() + "') select ref_id,typewise,Tran_cd,beh_cd,ref_beh_cd,valid_mast,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,tbl_nm,_copy,_read,compid from iREFERENCE where tran_cd='" + tran_cd + "' and valid_mast='" + refering_cd + "' and  compid='" + objBLFD.ObjCompany.Compid.ToString() + "' order by typewise,order_no  else select im_ref_id ref_id,typewise,Tran_cd,beh_cd,ref_beh_cd,valid_mast,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,inter_val,tbl_nm,_copy,_read,compid from IMPORT_iREFERENCE order by typewise,cast(order_no as int)");
        }

        public DataSet Get_Ref_Item_Grid(BL_BASEFIELD objBLFD, string refering_cd)
        {
            return objdlAdapter.dsquery("select * from ireference where valid_mast='" + refering_cd + "' and tran_cd='" + objBLFD.Code + "' and typewise=0 and compid='" + objBLFD.ObjCompany.Compid.ToString() + "' order by cast(order_no as int)");
        }

        public DataSet Get_Ref_main_Details(BL_BASEFIELD objBLFD, string ac_nm, string _rule, string ref_type)
        {
            htFieldscond.Clear();
            htFieldscond.Add("@atran_id", objBLFD.HTMAIN[objBLFD.Primary_id.ToString()].ToString());
            htFieldscond.Add("@atran_cd", objBLFD.Code);
            htFieldscond.Add("@abehaiver_cd", objBLFD.Behavier_cd);
            htFieldscond.Add("@areftran_cd", ref_type);
            htFieldscond.Add("@arefbehaiver_cd", objBLFD.Ref_behaiver_cd);
            htFieldscond.Add("@atran_mode", objBLFD.Tran_mode);
            htFieldscond.Add("@aac_nm", ac_nm);
            htFieldscond.Add("@arule", _rule);
            htFieldscond.Add("@acompid", objBLFD.ObjCompany.Compid);
            htFieldscond.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            return objdlAdapter.dsprocedure("ISP_GET_Ref_Header_Details", htFieldscond);
        }
        //public DataSet Get_Ref_item_Details(string ref_tran_id, string ref_tran_cd, string tran_mode,string tran_id,string tran_cd,string beh_cd,string ref_beh_cd,string ac_nm)
        public DataSet Get_Ref_item_Details(string ref_tran_id, string ref_tran_cd, string tran_mode, BL_BASEFIELD objBLFD, string ac_nm, string _rule)
        {
            htFieldscond.Clear();
            htFieldscond.Add("@aref_tran_id", ref_tran_id);
            htFieldscond.Add("@aref_tran_cd", ref_tran_cd);
            htFieldscond.Add("@arefbehaiver_cd", objBLFD.Ref_behaiver_cd);
            htFieldscond.Add("@atran_mode", tran_mode);
            htFieldscond.Add("@atran_id", objBLFD.Tran_id);
            htFieldscond.Add("@atran_cd", objBLFD.Code);
            htFieldscond.Add("@abehaiver_cd", objBLFD.Behavier_cd);
            htFieldscond.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());
            htFieldscond.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htFieldscond.Add("@aac_nm", ac_nm);
            htFieldscond.Add("@arule", _rule);
            return objdlAdapter.dsprocedure("ISP_GET_Ref_Item_Details", htFieldscond);
        }
        public DataSet Get_Ref_Details_byTransaction(string tran_id, string tran_cd, string beh_cd)
        {
            htFieldscond.Clear();
            htFieldscond.Add("@atran_id", tran_id);
            htFieldscond.Add("@atran_cd", tran_cd);
            htFieldscond.Add("@abeh_cd", beh_cd);
            return objdlAdapter.dsprocedure("ISP_GET_Ref_Details_byTransaction", htFieldscond);
        }
    }
}
