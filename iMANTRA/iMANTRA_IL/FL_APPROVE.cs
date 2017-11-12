using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA_IL
{
    public class FL_APPROVE
    {
        DL_APPROVE objDL_Approve = new DL_APPROVE();
        public DataSet GetApproveListData(string tran_cd, string tbl_nm)
        {
            return objDL_Approve.GetApproveListData(tran_cd, tbl_nm);
        }
        public DataSet GetApproveLevelsCount(string tran_cd, string compid)
        { return objDL_Approve.GetApproveLevelsCount(tran_cd, compid); }
    }
}
