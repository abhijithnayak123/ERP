using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA_DL
{
    public class DL_GENERAL
    {
        SqlConnection con;
        Ini objIni = new Ini();
        DL_ADAPTER objdlAdapter = new DL_ADAPTER();

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }
        public DataSet GetWorkOrderList(string compid)
        {
            string sqlQuery = "SELECT TRAN_NO,TRAN_ID,TRAN_CD,PTSERIAL,PROD_CD,PROD_NM,QTY from WOITEM WHERE COMPID=" + compid + "";
            return objdlAdapter.dsquery(sqlQuery);
        }
        public DataSet GetFile_List(BL_BASEFIELD objBF)
        {
            string sqlQuery = "SELECT * from FILE_MAST WHERE code='" + objBF.Code + "' and COMPID=" + objBF.ObjCompany.Compid.ToString() + "";
            return objdlAdapter.dsquery(sqlQuery);
        }
        public DataSet GetMenuListParent(BL_BASEFIELD objBF, string Module_list)
        {
            string sqlQuery = "select * FROM MENU_TBL where [level_id]=0 and compid='" + objBF.ObjCompany.Compid.ToString() + "' order by order_no";
            return objdlAdapter.dsquery(sqlQuery);
        }
        public DataSet GetMenuListChild(BL_BASEFIELD objBF, string parent_id, string Module_list)
        {
            string sqlQuery = "select * FROM MENU_TBL where [level_id]!=0 and parent_id=" + parent_id + " and compid='" + objBF.ObjCompany.Compid.ToString() + "' order by order_no";
            return objdlAdapter.dsquery(sqlQuery);
        }

        public DataSet GetDataSetByProcedure(string proc_nm, Hashtable ht)
        {
            return objdlAdapter.dsprocedure(proc_nm, ht);
        }

        public DataSet GetDataSetByQuery(string query)
        {
            return objdlAdapter.dsquery(query);
        }
    }
}
