using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA_DL
{
    public class DL_BASEFIELD
    {
        public Company objCompany = new Company();
        Ini objIni = new Ini();

        DL_ADAPTER objdbLayer = new DL_ADAPTER();

        //  SqlConnection con = new SqlConnection("data source=SHARANAMMA\\iMANTRA;initial catalog=iMANTRA;user=sa;pwd=iMANTRA");
        SqlConnection con; //= new SqlConnection("data source=INODE-SHA\\iMANTRA;initial catalog=iMANTRA;user=sa;pwd=sa1985");
        //DataSet dsBASEFIELDFORGRID = new DataSet();    
        public DL_BASEFIELD()
        {
            //  con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        public DataSet GETBASEFIELD(string tran_cd, string compid)
        {
            return objdbLayer.dsquery("select * from iBASEFIELDS where code='" + tran_cd + "' and typewise=1 and compid='" + compid + "' order by order_no,col_order_no");
        }
        public DataSet GETBASEFIELDFORGRID(string tran_cd, string compid)
        {
            return objdbLayer.dsquery("select * from iBASEFIELDS where code='" + tran_cd + "' and typewise=0 and compid='" + compid + "' order by order_no,col_order_no");
        }
        public DataSet GETCUSTOMFIELDS(string tran_cd, string compid)
        {
            if (tran_cd != "OM")
            {
                return objdbLayer.dsquery("select * from iCUSTOMFIELDS where code='" + tran_cd + "' and typewise=1 and compid='" + compid + "' order by order_no,col_order_no");
            }
            else
            {
                return objdbLayer.dsquery("select * from iCUSTOMFIELDS where code='" + tran_cd + "' and typewise=1 order by order_no,col_order_no");
            }
        }
        public DataSet GETCUSTOMFIELDFORGRID(string tran_cd, string compid)
        {
            return objdbLayer.dsquery("select * from iCUSTOMFIELDS where code='" + tran_cd + "' and typewise=0 and compid='" + compid + "' order by order_no,col_order_no");
        }
        public DataSet GETDCFIELDFORGRID(string tran_cd, string compid, string tran_dt)
        {
            return objdbLayer.dsquery("select * from DC_MAST where code='" + tran_cd + "' and typewise=0 and compid='" + compid + "' and isnull(isdeactive,0)=case when(isdeactive=1) then case when(datediff(day,'" + tran_dt + "',case when(deactfrm='1900-01-01 00:00:00.000' or deactfrm='2000-01-01 00:00:00.000') then '" + tran_dt + "' else deactfrm end)>=0) then 1 else 0 end else 0 end order by cast(corder as int)");
        }
        public DataSet GETPOPUPDETAILS(string query)
        {
            return objdbLayer.dsquery(query);
        }

        public DataSet GET_HEADERDATA(BL_BASEFIELD objBLBF)
        {
            return objdbLayer.dsquery("select top 1 * from " + objBLBF.Main_tbl_nm + " where tran_cd='" + objBLBF.Code + "'  and " + objBLBF.Primary_id + "='" + objBLBF.HTMAIN[objBLBF.Primary_id].ToString() + "' and compid='" + objBLBF.ObjCompany.Compid.ToString() + "' order by " + objBLBF.Primary_id + " desc");
        }
        public DataSet GET_GRIDDATA(BL_BASEFIELD objBLBF)
        {
            return objdbLayer.dsquery("select * from " + objBLBF.Item_tbl_nm + " where tran_cd='" + objBLBF.Code + "' and " + objBLBF.Primary_id + "='" + objBLBF.HTMAIN[objBLBF.Primary_id].ToString() + "' and compid='" + objBLBF.ObjCompany.Compid.ToString() + "' order by cast(ptserial as int)");
        }

        public DataSet GETDCHEADERFIELD(string tran_cd, string compid)
        {
            return objdbLayer.dsquery("select * from DC_MAST where code='" + tran_cd + "' and typewise=1 and compid='" + compid + "' order by corder");
        }
        public DataSet GETDCHEADERFIELDBasedonCode(string tran_cd, string compid, string tran_dt)
        {
            return objdbLayer.dsquery("select * from DC_MAST where code='" + tran_cd + "' and typewise=1 and compid='" + compid + "' and isnull(isdeactive,0)=case when(isdeactive=1) then case when(datediff(day,'" + tran_dt + "',case when(deactfrm='1900-01-01 00:00:00.000' or deactfrm='2000-01-01 00:00:00.000') then '" + tran_dt + "' else deactfrm end)>=0) then 1 else 0 end else 0 end order by corder");
        }

        public DataSet GETSTHEADERFIELD(string tran_cd, string compid, string tran_dt)
        {
            return objdbLayer.dsquery("select * from ST_MAST where code='" + tran_cd + "' and compid='" + compid + "'  and isnull(isdeactive,0)=case when(isdeactive=1) then case when(datediff(day,'" + tran_dt + "',case when(deactfrm='1900-01-01 00:00:00.000' or deactfrm='2000-01-01 00:00:00.000') then '" + tran_dt + "' else deactfrm end)>=0) then 1 else 0 end else 0 end");
        }

        public DataSet GETSTHEADERFIELDByVendor(string tran_cd, string ac_nm, string compid, string tran_dt)
        {
            return objdbLayer.dsquery("select distinct * from st_mast where ST_MAST.stax_nm='' and code='" + tran_cd + "' union all select distinct ST_MAST.* from ST_MAST inner join cm_mast on ST_MAST.stax_nm=cm_mast.stax_nm where cm_mast.ac_nm like '" + ac_nm + "' and ST_MAST.code='" + tran_cd + "' and ST_MAST.compid='" + compid + "'  and isnull(ST_MAST.isdeactive,0)=case when(ST_MAST.isdeactive=1) then case when(datediff(day,'" + tran_dt + "',case when(ST_MAST.deactfrm='1900-01-01 00:00:00.000' or ST_MAST.deactfrm='2000-01-01 00:00:00.000') then '" + tran_dt + "' else ST_MAST.deactfrm end)>=0) then 1 else 0 end else 0 end");
        }

        public DataSet GetControl_Setting(string compid, string user_theme)
        {
            string strQuery = "";
            if (user_theme != "")
            {
                strQuery = "select CONTROL_SET.*,neg_stk,qty_dec,rate_dec from CONTROL_SET left join ctrl_set on CONTROL_SET.compid='" + compid + "' and ctrl_set.compid='" + compid + "' where CONTROL_SET.theme_nm='" + user_theme + "'";
            }
            else
            {
                strQuery = "declare @theme_nm varchar(250) IF NOT EXISTS(select theme_nm from ctrl_set) BEGIN set @theme_nm='default' END ELSE BEGIN select @theme_nm=theme_nm from ctrl_set END select CONTROL_SET.*,neg_stk,qty_dec,rate_dec from control_set left join ctrl_set on CONTROL_SET.compid='" + compid + "' and ctrl_set.compid='" + compid + "' where CONTROL_SET.theme_nm=@theme_nm";
            }
            DataSet dset = objdbLayer.dsquery(strQuery);
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            return dset;
        }
    }
}
