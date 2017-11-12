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
    public class FL_GEN_INVOICE
    {
        public Company objCompany = new Company();
        DL_GENINVOICE objgen_invoice = new DL_GENINVOICE();
        public string user, no_of_user;

        public string Gen_Number(BL_BASEFIELD objBSFD,string tran_sr)//string tran_cd, string tran_sr, string fin_yr, string compid)
        {
            return objgen_invoice.Gen_Number(objBSFD, tran_sr);//tran_cd, tran_sr, fin_yr,compid);
        }
        //public void SaveGenInvoiceNumber(Hashtable HTMAIN, int gen_no)
        public void SaveGenInvoiceNumber(BL_BASEFIELD objBSFD, int gen_no)
        {
            objgen_invoice.SaveGenInvoiceNumber(objBSFD, gen_no);
        }
        public int SaveGenMiss(Hashtable HTMAIN)
        {            
            return objgen_invoice.SaveGenMiss(HTMAIN);
        }
        public int Find_Gen_Miss(Hashtable HTMAIN)
        {            
            return objgen_invoice.Find_Gen_Miss(HTMAIN);
        }
        public DataSet GET_SERIES(string tran_cd, string compid)
        {            
            return objgen_invoice.GET_SERIES(tran_cd,compid);
        }
        public void Delete_Gen_Miss(string tran_cd, string tran_id, string tran_sr, string tran_no, string compid)
        {            
            objgen_invoice.Delete_Gen_Miss(tran_cd, tran_id, tran_sr, tran_no,compid);
        }
        public DataSet GET_TBL_VAL(string tbl_nm, string tran_cd, string compid)
        {            
            return objgen_invoice.GET_TBL_VAL(tbl_nm, tran_cd,compid);
        }
        public DataSet Execute_Procedure_Query(string query, string paramenter)
        {            
            return objgen_invoice.Execute_Procedure_Query(query, paramenter);
        }
        public DataSet Execute_Query(string query)
        {
            return objgen_invoice.Execute_Query(query);
        }
        public string Gen_RG_Page_No(string tran_cd, string fin_yr, string ptserial,string compid)
        {
            return objgen_invoice.Gen_RG_Page_No(tran_cd, fin_yr, ptserial,compid);
        }
        public int Find_Gen_RG_Page_No_Miss(string tran_cd, string rgpageno, string fin_yr, string tran_id, string ptserial,string compid)
        {
            return objgen_invoice.Find_Gen_RG_Page_No_Miss(tran_cd, rgpageno, fin_yr, tran_id, ptserial,compid);
        }
        public DataSet Get_Manufacturer_Deatils(string compid)
        {
            return objgen_invoice.Get_Manufacturer_Deatils(compid);
        }

        //sharanamma on 05.08.13 reason:user restriction
        //begin
        public int GetLoginStatus(string user, int _comingfrm, string _no_of_users, string localip)
        {            
            return objgen_invoice.GetLoginStatus(user, _comingfrm, _no_of_users,localip);
        }
        public void InsertUpdateAndDelete(string user, string _status,string localip)
        {            
            objgen_invoice.InsertUpdateAndDelete(user, _status,localip);
        }

        //public void TimerCallBak(object state)
        //{
        //    int return_type = this.GetLoginStatus(user, 1, no_of_user);
        //    if (return_type == 4)
        //    {
        //        InsertUpdateAndDelete(user, "4");//update datetime
        //    }
        //}
        //end
    }
}
