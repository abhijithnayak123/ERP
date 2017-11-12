using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA_IL
{
    public class FL_Roles
    {
        public Company objCompany = new Company();
        BL_MAIN_FIELDS objMainFields = new BL_MAIN_FIELDS();

        public BL_MAIN_FIELDS ObjMainFields
        {
            get { return objMainFields; }
            set { objMainFields = value; }
        }
        DL_GENINVOICE iMANTRA_DL = new DL_GENINVOICE();
        protected string _strErrorMessage;

        public string strErrorMessage
        {
            get { return _strErrorMessage; }
            set { _strErrorMessage = value; }
        }

        DataTable dtRoleAndRights = new DataTable();
        public bool CheckPermisson(string str)
        {
            bool blnFlag = true;
            if (str != "")
            {
                string[] strModuleName = str.Split(',');
                dtRoleAndRights = GetRolesAndRightsFromCache();
                if (dtRoleAndRights != null && dtRoleAndRights.Rows.Count != 0)
                {
                    DataSet dsetmodule = iMANTRA_DL.ReturnDataBase("select module_id from CMOD where module_name ='" + strModuleName[1] + "' and compid='"+ObjMainFields.Comp_nm+"'");
                    if (dsetmodule != null && dsetmodule.Tables.Count != 0 && dsetmodule.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] row = dtRoleAndRights.Select(String.Format("module_id='{0}'", dsetmodule.Tables[0].Rows[0]["module_id"].ToString()));
                        if (row.Length != 0)
                        {
                            if (strModuleName[0] == "view" && !bool.Parse(row[0]["view_access"].ToString()))
                            {
                                blnFlag = false;
                            }
                            else if (bool.Parse(row[0]["view_access"].ToString()))
                            {
                                if (strModuleName[0] == "add" && !bool.Parse(row[0]["create_access"].ToString()))
                                {
                                    blnFlag = false;
                                }
                                else if (strModuleName[0] == "edit" && !bool.Parse(row[0]["edit_access"].ToString()))
                                {
                                    blnFlag = false;
                                }
                                else if (strModuleName[0] == "print" && !bool.Parse(row[0]["print_access"].ToString()))
                                {
                                    blnFlag = false;
                                }
                                else if (strModuleName[0] == "approve" && !bool.Parse(row[0]["approve_access"].ToString()))
                                {
                                    blnFlag = false;
                                }
                                if (strModuleName[0] == "delete" && !bool.Parse(row[0]["delete_access"].ToString()))
                                { blnFlag = false; }
                            }
                            else
                            {
                                blnFlag = false;
                            }
                        }
                        else
                        {
                            blnFlag = false;
                        }
                    }
                    else
                    {
                        blnFlag = false;
                    }
                }
                else
                {
                    blnFlag = false;
                }
            }
            return blnFlag;
        }

        public DataTable GetRolesAndRightsFromCache()
        {
            DataTable dt = getRoleMappingByUIdAndFY(ObjMainFields.CurUser, ObjMainFields.Comp_nm, ObjMainFields.Fin_yr);
            if (dt != null && dt.Rows.Count != 0)
            {
                string strRoleId = dt.Rows[0]["intRoleId"].ToString();
                dtRoleAndRights = getRoleTable(strRoleId);
            }
            return dtRoleAndRights;
        }

        public DataTable getRoleMappingByUIdAndFY(string user_id, string company_name, string fin_year)
        {
            string strSql = String.Format("select intRoleId from [ROLES_MAPPING] where [user_nm]='{0}' and [compid]='{1}' and [fin_yr]='{2}'", user_id, company_name, fin_year);
            DataSet ds = iMANTRA_DL.ReturnDataBase(strSql);
            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable getRoleTable(string intRoleId)
        {
            string strSql = String.Format("select r.intRoleId,role_nm,isAdmin,rt.module_id,rt.view_access,rt.create_access,rt.edit_access,rt.delete_access,rt.print_access ,rt.approve_access from ROLES r inner join RIGHTS rt on r.intRoleId=rt.intRoleId inner join CMOD cm on cm.module_id=rt.module_id where r.intRoleId={0}", intRoleId);
            DataSet ds = iMANTRA_DL.ReturnDataBase(strSql);
            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataSet getFormName(string code)
        {
            string strSql = String.Format("select frm_nm,condition from menu_tbl where code='"+code+"'");
            DataSet ds = iMANTRA_DL.ReturnDataBase(strSql);
            if (ds != null)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
