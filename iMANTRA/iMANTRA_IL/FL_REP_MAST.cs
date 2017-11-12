using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA_IL
{
    public class FL_REP_MAST
    {
        public Company objCompany = new Company();
        DL_REP_MAST objRep = new DL_REP_MAST();

        public DataSet Get_Rep_Documents(BL_BASEFIELD objBLFD)
        {           
            return objRep.Get_Rep_Documents(objBLFD);
        }
        public DataSet Get_Report_Details(string rep_nm, string compid)
        {           
            return objRep.Get_Report_details(rep_nm,compid);
        }
        public DataSet Get_Report_list(string grp_nm, string compid)
        {           
            return objRep.Get_Report_list(grp_nm,compid);
        }
        public DataSet REPORT_TRANSACTION(string tran_id, string tran_cd, string sp_nm)
        {           
            return objRep.REPORT_TRANSACTION(tran_id, tran_cd, sp_nm);
        }
        public DataSet REPORT_SHOW(Hashtable ht, string _cond)
        {
            return objRep.REPORT_SHOW(ht, _cond);
        }
    }
}
