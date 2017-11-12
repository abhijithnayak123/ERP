using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA_IL
{
    public class FL_GENERAL
    {
        DL_GENERAL objDLGeneral = new DL_GENERAL();

        public DataSet GetWorkOrderList(string compid)
        {
            return objDLGeneral.GetWorkOrderList(compid);
        }
        public DataSet GetFile_List(BL_BASEFIELD objBF)
        {
            return objDLGeneral.GetFile_List(objBF);
        }
        public DataSet GetMenuListParent(BL_BASEFIELD objBF, string Module_list)
        {
            return objDLGeneral.GetMenuListParent(objBF,Module_list);
        }
        public DataSet GetMenuListChild(BL_BASEFIELD objBF, string parent_id, string Module_list)
        {
            return objDLGeneral.GetMenuListChild(objBF, parent_id, Module_list);
        }

        public DataSet GetDataSetByProcedure(string proc_nm,Hashtable ht)
        {
            return objDLGeneral.GetDataSetByProcedure(proc_nm,ht);
        }

        public DataSet GetDataSetByQuery(string query)
        {
            return objDLGeneral.GetDataSetByQuery(query);
        }
    }
}
