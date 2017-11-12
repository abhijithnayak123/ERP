using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using iMANTRA_DL;
using iMANTRA_BL;

namespace iMANTRA_IL
{
    public class FL_BASEFIELD
    {
        public Company objCompany = new Company();
        //  BL_BASEFIELD objBF = new BL_BASEFIELD();
        DL_BASEFIELD objDL_BASEFIELD = new DL_BASEFIELD();

        public DataSet GETBASEFIELD(string tran_cd, string compid)
        {
            return objDL_BASEFIELD.GETBASEFIELD(tran_cd, compid);
        }
        public DataSet GETBASEFIELDFORGRID(string tran_cd, string compid)
        {
            return objDL_BASEFIELD.GETBASEFIELDFORGRID(tran_cd, compid);
        }
        public DataSet GETCUSTOMFIELD(string tran_cd, string compid)
        {
            return objDL_BASEFIELD.GETCUSTOMFIELDS(tran_cd, compid);
        }
        public DataSet GETCUSTOMFIELDFORGRID(string tran_cd, string compid)
        {
            return objDL_BASEFIELD.GETCUSTOMFIELDFORGRID(tran_cd, compid);
        }
        public DataSet GETDCFIELDFORGRID(string tran_cd, string compid,string tran_dt)
        {
            return objDL_BASEFIELD.GETDCFIELDFORGRID(tran_cd, compid,tran_dt);
        }
        public DataSet GETPOPUPDETAILS(string query)
        {
            return objDL_BASEFIELD.GETPOPUPDETAILS(query);
        }
        public DataSet GET_HEADERDATA(BL_BASEFIELD objBLBF)
        {
            return objDL_BASEFIELD.GET_HEADERDATA(objBLBF);
        }
        public DataSet GET_GRIDDATA(BL_BASEFIELD objBLBF)
        {
            return objDL_BASEFIELD.GET_GRIDDATA(objBLBF);
        }
        public DataSet GETDCHEADERFIELD(string tran_cd, string compid)
        {
            return objDL_BASEFIELD.GETDCHEADERFIELD(tran_cd, compid);
        }
        public DataSet GETDCHEADERFIELDBasedonCode(string tran_cd, string compid,string tran_dt)
        {
            return objDL_BASEFIELD.GETDCHEADERFIELDBasedonCode(tran_cd, compid,tran_dt);
        }
        public DataSet GETSTHEADERFIELD(string tran_cd, string compid,string tran_dt)
        {
            return objDL_BASEFIELD.GETSTHEADERFIELD(tran_cd, compid,tran_dt);
        }

        public DataSet GETSTHEADERFIELDByVendor(string tran_cd, string ac_nm, string compid,string tran_dt)
        {
            return objDL_BASEFIELD.GETSTHEADERFIELDByVendor(tran_cd, ac_nm, compid,tran_dt);
        }

        public DataSet GetControl_Setting(string compid, string user_theme)
        {
            return objDL_BASEFIELD.GetControl_Setting(compid, user_theme);
        }

        public string Validate_Fields(BL_BASEFIELD objBF)
        {
            foreach (DataRow row in objBF.dsBASEFIELDMAIN.Tables[0].Rows)
            {
                if (row["data_ty"].ToString().ToLower() == "decimal")
                {
                    Regex rgx = new Regex(@"[0-9]+[\.]?[0-9]*");
                    if (!rgx.IsMatch(objBF.HTMAIN[row["fld_nm"].ToString().ToUpper()].ToString()))
                    {
                        return row["head_nm"].ToString();
                    }
                }
            }
            return "";
        }
    }
}
