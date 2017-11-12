using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_DL;
using iMANTRA_BL;

namespace iMANTRA_IL
{
    public class FL_TRANSACTION
    {
        public Company objCompany = new Company();
        DL_TRANSACTION objDL_Transaction = new DL_TRANSACTION();
        DL_ADAPTER objAdapter = new DL_ADAPTER();

        public string Save_Trasaction(BL_BASEFIELD objSLBF)
        {
            return objDL_Transaction.Save_Trasaction(objSLBF);
        }
        public DataSet GET_ALL_NAVIGATION_DATA(BL_BASEFIELD objSLBF, string condition)
        {
            return objDL_Transaction.GET_ALL_NAVIGATION_DATA(objSLBF, condition);
        }
        public bool DELETE_TRANSACTION(BL_BASEFIELD objBLFIELD)
        {
            return objDL_Transaction.DELETE_TRANSACTION(objBLFIELD);
        }
        public DataSet GET_MASTER_DATA(BL_BASEFIELD objBLBASEFIELD)
        {
            return objDL_Transaction.GET_MASTER_DATA(objBLBASEFIELD);
        }
        public string GetPrimaryKeyFldNm(string tbl_nm, string catalog)
        {
            return objDL_Transaction.GetPrimaryKeyFldNm(tbl_nm, catalog);
        }
        public bool DELETE_MASTER(BL_BASEFIELD objBLFIELD)
        {
            return objDL_Transaction.DELETE_MASTER(objBLFIELD);
        }
        public bool Find_Deleting_Fld_Usaged_In_OtherTbl(BL_BASEFIELD objBLFD)
        {
            return objDL_Transaction.Find_Deleting_Fld_Usaged_In_OtherTbl(objBLFD);
        }
        public bool Find_Reference(string tran_id, string tran_no, string tran_cd, string ptserial)
        {
            return objDL_Transaction.Find_Reference(tran_id, tran_no, tran_cd, ptserial);
        }

        public bool FindPartyManufacturerOrNot(string ac_nm)
        {
            return objDL_Transaction.FindPartyManufacturerOrNot(ac_nm);
        }
        public int GetRGPageNo()
        {
            return objDL_Transaction.GetRGPageNo();
        }
        public bool FindRGPageNo(string rgpageno)
        {
            return objDL_Transaction.FindRGPageNo(rgpageno);
        }

        public DataSet GetExtra_field(string tbl_nm, string tran_id, string tran_cd, string compid, string fin_yr)
        {
            return objDL_Transaction.GetExtra_field(tbl_nm, tran_id, tran_cd, compid, fin_yr);
        }
        public DataSet GetTrans_Settings(string tran_cd, string compid)
        {
            return objDL_Transaction.GetTrans_Settings(tran_cd, compid);
        }
        public DataSet GetDataSet(string sqlQuery)
        {
            return objAdapter.dsquery(sqlQuery);
        }

        public DataSet GetApproveSetting(string code, string compid)
        {
            return objDL_Transaction.GetApproveSetting(code, compid);
        }
        public bool Update_Company_ID(BL_BASEFIELD objBLFD)
        {
            return objDL_Transaction.Update_Company_ID(objBLFD);
        }
        public string Get_Company_Id(BL_BASEFIELD objBLFD)
        { return objDL_Transaction.Get_Company_Id(objBLFD); }
        public bool Update_Module_to_ReportAndTran_Set(BL_BASEFIELD objBLFD)
        {
            return objDL_Transaction.Update_Module_to_ReportAndTran_Set(objBLFD);
        }
        public void Update_Module_Reference_Type(BL_BASEFIELD objBLFD)
        {
            objDL_Transaction.Update_Module_Reference_Type(objBLFD);
        }
    }
}
