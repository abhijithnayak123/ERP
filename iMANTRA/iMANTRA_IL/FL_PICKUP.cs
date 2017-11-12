using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA_IL
{
    public class FL_PICKUP
    {
        public Company objCompany = new Company();
        DL_PICKUP objDLPICKUP = new DL_PICKUP();

        public DataSet Get_Ref_main_Details(BL_BASEFIELD objBLFD, string ac_nm, string _rule, string ref_type)
        {           
            return objDLPICKUP.Get_Ref_main_Details(objBLFD,ac_nm,_rule,ref_type);
        }
        //public DataSet Get_Ref_item_Details(string ref_tran_id, string ref_tran_cd, string tran_mode, string tran_id, string tran_cd,string beh_cd,string ref_beh_cd,string ac_nm)
        //{objDLPICKUP.objCompany = objCompany;
        //    return objDLPICKUP.Get_Ref_item_Details(ref_tran_id, ref_tran_cd, tran_mode, tran_id, tran_cd,beh_cd,ref_beh_cd,ac_nm);
        //}
        public DataSet Get_Ref_item_Details(string ref_tran_id, string ref_tran_cd, string tran_mode,BL_BASEFIELD objBLFD,string ac_nm,string _rule)
        {           
            return objDLPICKUP.Get_Ref_item_Details(ref_tran_id, ref_tran_cd, tran_mode, objBLFD,ac_nm,_rule);
        }
        public DataSet Get_Ref_Details_byTransaction(string tran_id, string tran_cd, string beh_cd)
        {           
            return objDLPICKUP.Get_Ref_Details_byTransaction(tran_id, tran_cd, beh_cd);
        }
        public DataSet Get_Ref_Main_Grid(BL_BASEFIELD objBLFD,string refering_cd)
        {           
            return objDLPICKUP.Get_Ref_Main_Grid(objBLFD,refering_cd);
        }
        public DataSet Get_Ref_Item_Grid(BL_BASEFIELD objBLFD, string refering_cd)
        {           
            return objDLPICKUP.Get_Ref_Item_Grid(objBLFD,refering_cd);
        }
        public DataSet Get_PickUpDetails(BL_BASEFIELD objBLFD, string refering_cd,string tran_cd)
        {
            return objDLPICKUP.Get_PickUpDetails(objBLFD, refering_cd,tran_cd);
        }
    }
}
