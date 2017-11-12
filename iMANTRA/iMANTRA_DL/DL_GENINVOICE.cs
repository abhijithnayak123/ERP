using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using iMANTRA_iniL;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using iMANTRA_BL;

namespace iMANTRA_DL
{
    public class DL_GENINVOICE
    {
        public Company objCompany = new Company();

        SqlConnection con;
        SqlCommand com;
        Ini objIni = new Ini();
        IniKey objIniKey = new IniKey();
        DL_ADAPTER objdbLayer = new DL_ADAPTER(); 


        public DL_GENINVOICE()
        {

        }
        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }
        public string Gen_Number(BL_BASEFIELD objBSFD, string tran_sr)//, string tran_cd, string tran_sr, string fin_yr, string compid)
        {
            GetCorrectCon();
            DataSet dsGENNO = new DataSet();
            DataSet dsSER = new DataSet();
            string tran_no = "1";
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("select isnull(max(tran_no),0)+1 from GEN_TRAN where tran_cd='" + objBSFD.Code + "' and tran_sr='" + tran_sr + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter("select MAX(tran_no) tran_no from (select isnull(max(tran_no),0)+1 tran_no from GEN_TRAN where tran_cd='" + objBSFD.Code + "' and tran_sr='" + tran_sr + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "' union all select isnull(max(tran_no),0)+1 tran_no from GEN_MISS where tran_cd='" + objBSFD.Code + "' and tran_sr='" + tran_sr + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "' and flg='Y')vw", con);
                SqlDataAdapter ds = new SqlDataAdapter("select Prefix,Suffix from SERIES where tran_sr='" + tran_sr + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "'", con);
                da.Fill(dsGENNO);
                ds.Fill(dsSER);
                if (dsGENNO != null && dsGENNO.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dsGENNO.Tables[0].Rows)
                    {
                        tran_no = row[0].ToString();
                    }
                }
                if (dsSER != null && dsSER.Tables[0].Rows.Count != 0)
                {
                    if ((dsSER.Tables[0].Rows[0]["Prefix"].ToString() == "" && dsSER.Tables[0].Rows[0]["Prefix"] == null) && (dsSER.Tables[0].Rows[0]["Suffix"].ToString() == "" && dsSER.Tables[0].Rows[0]["Suffix"] == null))
                    {
                        tran_no = tran_no.PadLeft(int.Parse(objBSFD.Tran_no_wid), '0');
                    }
                    if (dsSER.Tables[0].Rows[0]["Prefix"].ToString() != "" && dsSER.Tables[0].Rows[0]["Prefix"] != null)
                    {
                        tran_no = dsSER.Tables[0].Rows[0]["Prefix"].ToString() + "-" + tran_no.PadLeft(int.Parse(objBSFD.Tran_no_wid), '0');
                    }
                    if (dsSER.Tables[0].Rows[0]["Suffix"].ToString() != "" && dsSER.Tables[0].Rows[0]["Suffix"] != null)
                    {
                        tran_no = tran_no.PadLeft(int.Parse(objBSFD.Tran_no_wid), '0') + "-" + dsSER.Tables[0].Rows[0]["Suffix"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return tran_no.PadLeft(int.Parse(objBSFD.Tran_no_wid), '0');
        }
        //public void SaveGenInvoiceNumber(Hashtable HTMAIN, int gen_no)
        public void SaveGenInvoiceNumber(BL_BASEFIELD objBSFD, int gen_no)
        {
            try
            {
                GetCorrectCon();

                DataSet dsGEN_NO = new DataSet();
                SqlCommand cmdinsert;
                gen_no = Find_Gen_Miss(objBSFD.HTMAIN);
                if (gen_no == 0)
                {
                    //HTMAIN["TRAN_NO"] = Gen_Number(HTMAIN["TRAN_CD"].ToString(), HTMAIN["TRAN_SR"].ToString(), HTMAIN["fin_yr"].ToString(), HTMAIN["compid"].ToString());
                    objBSFD.HTMAIN["TRAN_NO"] = Gen_Number(objBSFD, objBSFD.HTMAIN["TRAN_SR"].ToString());
                }
                SqlDataAdapter da = new SqlDataAdapter("select tran_no from gen_tran where tran_cd='" + objBSFD.Code + "' and tran_sr='" + objBSFD.HTMAIN["TRAN_SR"].ToString() + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "'", con);
                //SqlDataAdapter da = new SqlDataAdapter("select MAX(tran_no) tran_no from (select isnull(max(tran_no),0)+1 tran_no from GEN_TRAN where tran_cd='" + objBSFD.Code + "' and tran_sr='" + objBSFD.HTMAIN["TRAN_SR"].ToString() + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "' union all select isnull(max(tran_no),0)+1 tran_no from GEN_MISS where tran_cd='" + objBSFD.Code + "' and tran_sr='" + objBSFD.HTMAIN["TRAN_SR"].ToString() + "' and fin_yr='" + objBSFD.ObjCompany.Fin_yr + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "' and flg='Y')vw", con);
                da.Fill(dsGEN_NO);
                string tran_no;
                tran_no = Regex.Match(objBSFD.HTMAIN["TRAN_NO"].ToString(), @"\d+").Value;

                if (dsGEN_NO != null && dsGEN_NO.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(dsGEN_NO.Tables[0].Rows[0]["tran_no"].ToString()) < int.Parse(tran_no.TrimStart('0')))
                    {
                        cmdinsert = new SqlCommand("update gen_tran set tran_no='" + tran_no.TrimStart('0') + "' where tran_cd='" + objBSFD.Code + "' and tran_sr='" + objBSFD.HTMAIN["TRAN_SR"].ToString() + "' and compid='" + objBSFD.ObjCompany.Compid.ToString() + "'", con);
                        con.Open();
                        cmdinsert.ExecuteNonQuery();
                    }
                }
                else
                {
                    cmdinsert = new SqlCommand("insert into gen_tran values('" + objBSFD.ObjCompany.Compid.ToString() + "','" + objBSFD.Code + "','" + objBSFD.HTMAIN["TRAN_DT"].ToString() + "','" + (tran_no.TrimStart('0')) + "','" + objBSFD.HTMAIN["TRAN_SR"].ToString() + "','" + objBSFD.ObjCompany.Fin_yr + "')", con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            finally
            {
                con.Close();
            }
        }
        public int SaveGenMiss(Hashtable HTMAIN)
        {
            int gen_no = 0;
            try
            {
                GetCorrectCon();
                SqlCommand cmdinsert;
                gen_no = Find_Gen_Miss(HTMAIN);
                string tran_no;
                tran_no = Regex.Match(HTMAIN["TRAN_NO"].ToString(), @"\d+").Value;
                if (gen_no == 1)
                {
                    cmdinsert = new SqlCommand("update gen_miss set flg='Y' where tran_no='" + tran_no + "' and tran_cd='" + HTMAIN["TRAN_CD"].ToString() + "' and tran_sr='" + HTMAIN["TRAN_SR"].ToString() + "' and compid='" + HTMAIN["compid"].ToString() + "'", con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();
                    //con.Close();
                }
                else if (gen_no == 0)
                {
                    cmdinsert = new SqlCommand("insert into gen_miss values('" + HTMAIN["compid"].ToString() + "','" + HTMAIN["TRAN_CD"].ToString() + "','Y','" + HTMAIN["TRAN_DT"].ToString() + "','" + tran_no + "','" + HTMAIN["TRAN_SR"].ToString() + "','" + HTMAIN["fin_yr"].ToString() + "')", con);
                    con.Open();
                    cmdinsert.ExecuteNonQuery();
                    //con.Close();
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
                return 2;
            }
            finally
            {
                con.Close();
            }
            return gen_no;
        }
        public int Find_Gen_Miss(Hashtable HTMAIN)
        {
            GetCorrectCon();

            int gen_no = 0;
            try
            {
                DataSet dsGEN_NO = new DataSet();
                DataSet dsGEN_Miss = new DataSet();
                string tran_no;
                tran_no = Regex.Match(HTMAIN["TRAN_NO"].ToString(), @"\d+").Value;
                SqlDataAdapter da = new SqlDataAdapter("select * from gen_miss where tran_cd='" + HTMAIN["TRAN_CD"].ToString() + "' and tran_no='" + tran_no + "' and tran_sr='" + HTMAIN["TRAN_SR"].ToString() + "' and compid='" + HTMAIN["compid"].ToString() + "' and fin_yr='" + HTMAIN["fin_yr"].ToString() + "'", con);

                da.Fill(dsGEN_NO);
                if (dsGEN_NO != null && dsGEN_NO.Tables[0].Rows.Count > 0)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select * from gen_miss where tran_cd='" + HTMAIN["TRAN_CD"].ToString() + "' and tran_no='" + tran_no + "' and tran_sr='" + HTMAIN["TRAN_SR"].ToString() + "' AND flg='N' and compid='" + HTMAIN["compid"].ToString() + "' and fin_yr='" + HTMAIN["fin_yr"].ToString() + "'", con);

                    da1.Fill(dsGEN_Miss);
                    if (dsGEN_Miss != null && dsGEN_Miss.Tables[0].Rows.Count > 0)
                    {
                        gen_no = 1;
                    }
                    else
                    {
                        gen_no = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return gen_no;
        }
        public DataSet GET_SERIES(string tran_cd, string compid)
        {
            return objdbLayer.dsquery("select * from SERIES where VALIDITY LIKE '%" + tran_cd + "%' and compid='" + compid + "'");
        }
        public DataSet GET_TBL_VAL(string tbl_nm, string tran_cd, string compid)
        {
            GetCorrectCon();
            DataSet dsTBL = new DataSet();
            SqlDataAdapter da;
            DataSet dsDDL = new DataSet();
            DataSet dsDDL1 = new DataSet();
            try
            {
                SqlDataAdapter daDDL = new SqlDataAdapter("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + tbl_nm + "' AND COLUMN_NAME = 'Validity'", con);
                daDDL.Fill(dsDDL);
                SqlDataAdapter daDDL1 = new SqlDataAdapter("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + tbl_nm + "' AND COLUMN_NAME = 'compid'", con);
                daDDL1.Fill(dsDDL1);
                if (dsDDL != null && dsDDL.Tables[0].Rows.Count != 0 && tran_cd != "")
                {
                    if (dsDDL1 != null && dsDDL1.Tables[0].Rows.Count != 0)
                        da = new SqlDataAdapter("select * from " + tbl_nm + " where VALIDITY LIKE '%" + tran_cd + "%' and compid='" + compid + "'", con);
                    else { da = new SqlDataAdapter("select * from " + tbl_nm + " where VALIDITY LIKE '%" + tran_cd + "%'", con); }
                }
                else
                {
                    if (dsDDL1 != null && dsDDL1.Tables[0].Rows.Count != 0)
                    {
                        da = new SqlDataAdapter("select * from " + tbl_nm + " where compid='" + compid + "'", con);
                    }
                    else
                    {
                        da = new SqlDataAdapter("select * from " + tbl_nm, con);
                    }
                }
                da.Fill(dsTBL);
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return dsTBL;
        }
        public bool Find_Gen_Miss(string tran_cd, string tran_no, string compid)
        {
            try
            {
                GetCorrectCon();
                tran_no = Regex.Match(tran_no, @"\d+").Value;
                DataSet dsGEN_NO = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select TRAN_NO cnt from gen_miss where tran_cd='" + tran_cd + "' and tran_no='" + tran_no + "' and compid='" + compid + "'", con);

                da.Fill(dsGEN_NO);
                if (dsGEN_NO != null && dsGEN_NO.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return false;
        }
        public void Delete_Gen_Miss(string tran_cd, string tran_id, string tran_sr, string tran_no, string compid)
        {
            try
            {
                GetCorrectCon();
                tran_no = Regex.Match(tran_no, @"\d+").Value;
                SqlCommand cmddelete = new SqlCommand("update gen_miss set flg='N' where tran_cd='" + tran_cd + "' and tran_no='" + Regex.Match(tran_id, @"\d+").Value + "' and tran_sr='" + tran_sr + "' and compid='" + compid + "'", con);

                con.Open();
                cmddelete.ExecuteNonQuery();

                DataSet dsGEN_NO = new DataSet();
                DataSet dsGEN_No1 = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter("select tran_no from gen_tran where tran_cd='" + tran_cd + "' and tran_sr='" + tran_sr + "' and compid='" + compid + "'", con);

                da.Fill(dsGEN_NO);
                if (dsGEN_NO != null && dsGEN_NO.Tables[0].Rows.Count > 0)
                {
                    if (int.Parse(dsGEN_NO.Tables[0].Rows[0]["tran_no"].ToString()) == int.Parse(tran_no))
                    {
                        string int_tran_no = "0";
                        SqlDataAdapter da1 = new SqlDataAdapter("select isnull(max(tran_no),0) tran_no from gen_miss where tran_cd='" + tran_cd + "' and tran_sr='" + tran_sr + "' and flg='Y' and compid='" + compid + "'", con);

                        da1.Fill(dsGEN_No1);
                        if (dsGEN_No1 != null && dsGEN_No1.Tables[0].Rows.Count > 0)
                        {
                            int_tran_no = dsGEN_No1.Tables[0].Rows[0]["tran_no"].ToString();
                        }
                        if (int.Parse(int_tran_no) < int.Parse(tran_no))
                        {
                            if (int_tran_no != "0" && int.Parse(tran_no) - int.Parse(int_tran_no) > 0)
                            {
                                int_tran_no = (int.Parse(tran_no) - (int.Parse(tran_no) - int.Parse(int_tran_no))).ToString();
                            }
                        }
                        cmddelete = new SqlCommand("update gen_tran set tran_no='" + int_tran_no + "' where tran_cd='" + tran_cd + "' and tran_sr='" + tran_sr + "' and compid='" + compid + "'", con);
                        cmddelete.ExecuteNonQuery();
                        //cmddelete = new SqlCommand("update gen_miss set flg='N' where tran_cd='" + tran_cd + "' and tran_no='" + int_tran_no + "' and tran_sr='" + tran_sr + "' and compid='" + compid + "'", con);
                        //cmddelete.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            finally
            {
                con.Close();
            }
        }

        public string Gen_RG_Page_No(string tran_cd, string fin_yr, string ptserial, string compid)
        {
            GetCorrectCon();
            DataSet dsGENNO = new DataSet();
            string tran_no = "1";
            try
            {
                // SqlDataAdapter da = new SqlDataAdapter("select isnull(max(rgpageno),0)+1 from peitem where tran_cd='" + tran_cd + "' and fin_yr='" + fin_yr + "' and ptserial!='" + ptserial + "'", con);
                SqlDataAdapter da = new SqlDataAdapter("select isnull(max(rgpageno),0)+1 from peitem where compid='" + compid + "' and tran_cd='" + tran_cd + "' and fin_yr='" + fin_yr + "'", con);
                da.Fill(dsGENNO);
                if (dsGENNO != null && dsGENNO.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dsGENNO.Tables[0].Rows)
                    {
                        tran_no = row[0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return tran_no.PadLeft(6, '0');
        }
        public int Find_Gen_RG_Page_No_Miss(string tran_cd, string rgpageno, string fin_yr, string tran_id, string ptserial, string compid)
        {
            GetCorrectCon();
            int gen_no = 0;
            try
            {
                DataSet dsGEN_NO = new DataSet();
                DataSet dsGEN_Miss = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select * from peitem where compid='" + compid + "' and tran_cd='" + tran_cd + "' and rgpageno='" + rgpageno + "' and fin_yr='" + fin_yr + "' and tran_id!=" + tran_id, con);// and ptserial!='"+ptserial+"'"
                // SqlDataAdapter da = new SqlDataAdapter("select * from peitem where tran_cd='" + tran_cd + "' and rgpageno='" + rgpageno + "' and fin_yr='" + fin_yr + "'", con);
                da.Fill(dsGEN_NO);
                if (dsGEN_NO != null && dsGEN_NO.Tables[0].Rows.Count > 0)
                {
                    gen_no = 0;
                }
                else
                {
                    gen_no = 1;
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return gen_no;
        }
        public DataSet Get_Manufacturer_Deatils(string compid)
        {
            return objdbLayer.dsquery("SELECT * FROM cm_mast where vend_nm='MANUFACTURER' and compid='" + compid + "'");
        }

        public DataSet Execute_Procedure_Query(string query, string paramenter)
        {
            GetCorrectCon();
            DataSet dsRepdoc = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@spl_con", paramenter));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsRepdoc);
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            return dsRepdoc;
        }

        public DataSet Execute_Query(string query)
        {
            GetCorrectCon();
            DataSet dsRepdoc = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsRepdoc);
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
            }
            return dsRepdoc;
        }
        //sharanamma on 05.08.13 reason:user restriction
        //begin
        public int GetLoginStatus(string user, int _comingfrm, string _no_of_users, string localIp)
        {
            SqlConnection con1 = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source") + ";initial catalog=InodeMFG;user=" + objIni.GetKeyFieldValue("SQL", "user") + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd"));
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet dset = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select count(*) cnt from USER_LOG", con1);
            da.Fill(ds);
            SqlDataAdapter da2 = new SqlDataAdapter("select user_id,last_updated,getdate() current_dt,datediff(second,last_updated,getdate()) sec from USER_LOG group by user_id,last_updated", con1);
            da2.Fill(ds2);
            if (ds2 != null && ds2.Tables.Count != 0 && ds2.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow row in ds2.Tables[0].Rows)
                {
                    //if (DateTime.Parse(row["current_dt"].ToString()).Subtract(DateTime.Parse(row["last_updated"].ToString())).Seconds > 60)
                    if (int.Parse(row["sec"].ToString()) > 60)
                    {
                        InsertUpdateAndDelete(row["user_id"].ToString(), "2", localIp);
                    }
                }
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                if (int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString()) < int.Parse(_no_of_users))
                {
                    // SqlDataAdapter da1 = new SqlDataAdapter("select *,getdate() current_dt from USER_LOG where user_id='" + user + "' and machine_user='" + localIP + "'", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter("select *,getdate() current_dt,datediff(second,last_updated,getdate()) sec from USER_LOG where user_id='" + user + "'", con1);
                    da1.Fill(dset);
                    if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
                    {
                        // if (DateTime.Parse(dset.Tables[0].Rows[0]["current_dt"].ToString()).Subtract(DateTime.Parse(dset.Tables[0].Rows[0]["last_updated"].ToString())).Seconds > 60)
                        if (int.Parse(dset.Tables[0].Rows[0]["sec"].ToString()) > 60)
                        {
                            return 2;//delete user and new user
                        }
                        else
                        {
                            if (_comingfrm == 1)
                            {
                                return 4;//update datetime
                            }
                            else
                            {
                                return 5;//already exist
                            }
                        }
                    }
                    else
                    {
                        return 1;//add new user
                    }
                }
                else
                {
                    return 3;//user access denied
                }
            }
            else
            {
                return 1;//add new user
            }
        }
        public void InsertUpdateAndDelete(string user, string _status, string localIP)
        {
            //SqlConnection con1 = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source") + ";initial catalog=InodeMFG;user=" + objIni.GetKeyFieldValue("SQL", "user") + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd"));
            SqlConnection con1 = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
            try
            {
                if (_status == "1")
                {
                    com = new SqlCommand("insert into USER_LOG([user_id],[log_dt],[machine],[machine_user],[log_status]) values('" + user + "',getdate(),'" + Dns.GetHostName() + "','" + localIP + "','Log-In')", con1);
                    con1.Open();
                    com.ExecuteNonQuery();
                    con1.Close();
                }
                else if (_status == "2")
                {
                    com = new SqlCommand("insert into USER_LOG([user_id],[log_dt],[machine],[machine_user],[log_status]) values('" + user + "',getdate(),'" + Dns.GetHostName() + "','" + localIP + "','Log-Off')", con1);
                    con1.Open();
                    com.ExecuteNonQuery();
                    con1.Close();
                    //com = new SqlCommand("update USER_LOG set log_end_dt='" + DateTime.Now.ToString() + "' where user_id='" + user + "' and machine_user='" + localIP + "'", con1);
                    //// com = new SqlCommand("update USER_LOG set last_updated='" + DateTime.Now.ToString() + "' where user_id='" + user + "'", con1);
                    //con1.Open();
                    //com.ExecuteNonQuery();
                    //con1.Close();
                }
                //else if (_status == "2")
                //{
                //    com = new SqlCommand("delete from USER_LOG where user_id='" + user + "'", con1);
                //    // com = new SqlCommand("delete from USER_LOG where user_id='" + user + "'  and machine_user='" + localIP + "'", con1);
                //    con1.Open();
                //    com.ExecuteNonQuery();
                //    con1.Close();
                //}
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
            }
            finally
            {
                con1.Close();
            }
        }

        public DataSet ReturnDataBase(string query)
        {
            SqlConnection con1 = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source") + ";initial catalog=InodeMFG;user=" + objIni.GetKeyFieldValue("SQL", "user") + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd"));
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query, con1);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error",5000);
                return null;
            }
            finally
            {
                con1.Close();
            }
        }
        //end        
    }
}
