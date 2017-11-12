using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iMANTRA_BL;
using System.Collections;
using iMANTRA_iniL;

namespace iMANTRA_DL
{
    public class DL_REP_MAST
    {
        public Company objCompany = new Company();
        Ini objIni = new Ini();
        DL_ADAPTER objDLAdapter = new DL_ADAPTER();

        Hashtable htFieldscond = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DataSet dsSet = new DataSet();

        SqlConnection con;

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        public DataSet Get_Rep_Documents(BL_BASEFIELD objBLFD)
        {
            DataSet dsSet = new DataSet();
            htFieldscond.Clear();
            htFieldscond.Add("@atran_id", objBLFD.HTMAIN[objBLFD.Primary_id.ToString()].ToString());
            htFieldscond.Add("@atran_cd", objBLFD.Code);
            htFieldscond.Add("@adef_rep_nm", objBLFD.Defrep_nm);
            htFieldscond.Add("@acompid", objBLFD.ObjCompany.Compid);
            dsSet = objDLAdapter.dsprocedure("ISP_Get_Rep_Documents", htFieldscond);
            return dsSet;
        }

        public DataSet Get_Report_list(string grp_nm, string compid)
        {
            DataSet dsSet = new DataSet();
            htFieldscond.Add("@agroup_nm", grp_nm);
            htFieldscond.Add("@acompid", compid);
            dsSet = objDLAdapter.dsprocedure("ISP_Get_Reports_list_from_Group", htFieldscond);
            return dsSet;
        }

        public DataSet Get_Report_details(string rep_nm, string compid)
        {
            DataSet dsSet = new DataSet();
            htFieldscond.Clear();
            htFieldscond.Add("@adef_rep_nm", rep_nm);
            htFieldscond.Add("@acompid", compid);
            dsSet = objDLAdapter.dsprocedure("ISP_Get_Reports", htFieldscond);
            return dsSet;
        }

        public DataSet REPORT_TRANSACTION(string tran_id, string tran_cd, string sp_nm)
        {
            DataSet dsSet = new DataSet();
            htFieldscond.Clear();
            htFieldscond.Add("@tran_id", tran_id);
            htFieldscond.Add("@atrna_cd", tran_cd);
            dsSet = objDLAdapter.dsprocedure(sp_nm, htFieldscond);
            return dsSet;
        }

        public DataSet REPORT_SHOW(Hashtable ht, string _cond)
        {
            DataSet dsSet = new DataSet();
            htFieldscond.Clear();
            htFieldscond.Add("@SDATE", DateTime.Parse(ht["SDATE"].ToString()));
            htFieldscond.Add("@EDATE", DateTime.Parse(ht["EDATE"].ToString()));
            htFieldscond.Add("@SAC", ht["SAC"].ToString());
            htFieldscond.Add("@EAC", ht["EAC"].ToString());
            htFieldscond.Add("@SIT", ht["SIT"].ToString());
            htFieldscond.Add("@EIT", ht["EIT"].ToString());
            htFieldscond.Add("@SPL_COND", ht.Contains("SPL_COND") ? ht["SPL_COND"].ToString() : "");
            htFieldscond.Add("@COMPID", _cond);
            dsSet = objDLAdapter.dsprocedure(ht["sp_nm"].ToString(), htFieldscond);
            return dsSet;
        }
    }
}
