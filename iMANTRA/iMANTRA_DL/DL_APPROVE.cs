using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.IO;
using iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA_DL
{    
    public class DL_APPROVE
    {
        public Company objCompany = new Company();

        SqlConnection con;
        Ini objIni = new Ini();
        DL_ADAPTER objdlAdapter = new DL_ADAPTER();

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        public DataSet GetApproveListData(string tran_cd,string tbl_nm)
        {
            string serachstring = "";
            if (tran_cd == "AL")
            {
                serachstring = "%";
            }
            else
            {
                serachstring = tran_cd;
            }
            string sqlQuery = "select * from " + tbl_nm;
            return objdlAdapter.dsquery(sqlQuery);
        }

        public DataSet GetApproveLevelsCount(string tran_cd, string compid)
        {
            return objdlAdapter.dsquery("select * from LEVELS where code='" + tran_cd + "' and compid=" + compid + " order by CAST(si_no as int)");           
        }
    }
}
