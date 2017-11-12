using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using iMANTRA_BL;
using iMANTRA_iniL;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace iMANTRA_DL
{
    public class DL_MAST
    {
        public Company objCompany = new Company();
        // SqlConnection con = new SqlConnection("data source=SHARANAMMA\\iMANTRA;initial catalog=iMANTRA;user=sa;pwd=iMANTRA");
        SqlConnection con;// = new SqlConnection("data source=INODE-SHA\\iMANTRA;initial catalog=iMANTRA;user=sa;pwd=sa1985");
        // SqlCommand com;
        DL_TRANSACTION objDLTran = new DL_TRANSACTION();
        DL_ADAPTER objdbLayer = new DL_ADAPTER();

        Ini objIni = new Ini();
        string strquery = "";

        public DL_MAST()
        {
            //string str = objIni.ReadKeysInSection("SQL");
            //  con = new SqlConnection(objIni.GetSectionDetails("SQL",";"));
        }

        private void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
        }

        public DataSet GETMASTBASEHEADERFIELD(string tran_cd, string compid)
        {
            if (tran_cd != "OM")
            {
                strquery = "select * from iBASEFIELDS where code='" + tran_cd + "' and typewise=1 and data_ty!='TAB' and compid='" + compid + "' order by order_no,col_order_no";
            }
            else
            {
                strquery = "select * from iBASEFIELDS where code='" + tran_cd + "' and typewise=1 and data_ty!='TAB' order by order_no,col_order_no";
            }
            return objdbLayer.dsquery(strquery);
        }

        public DataSet GETMASTBASETABFIELD(string tran_cd, string compid)
        {
            if (tran_cd != "OM")
            {
                strquery = "select * from iBASEFIELDS where code='" + tran_cd + "' and data_ty='TAB' and compid='" + compid + "' order by order_no,col_order_no";
            }
            else
            {
                strquery = "select * from iBASEFIELDS where code='" + tran_cd + "' and data_ty='TAB' order by order_no,col_order_no";
            }
            return objdbLayer.dsquery(strquery);
        }

        public DataSet GETMASTBASETABDETAILS(string tran_cd, string tab_nm, string compid)
        {
            if (tran_cd != "OM")
            {
                strquery = "select * from iCUSTOMFIELDS where code='" + tran_cd + "' and _tab='" + tab_nm + "' and compid='" + compid + "' order by order_no,col_order_no";
            }
            else
            {
                strquery = "select * from iCUSTOMFIELDS where code='" + tran_cd + "' and _tab='" + tab_nm + "' order by order_no,col_order_no";
            }
            return objdbLayer.dsquery(strquery);
        }

        public DataSet Find_Name_Existance(string fld_nm, string tbl_nm, string _value, string _primary_value, string catalog, string compid)
        {
            string primary_filed = objDLTran.GetPrimaryKeyFldNm(tbl_nm, catalog);
            return objdbLayer.dsquery("select * from  " + tbl_nm + " where " + fld_nm + "='" + _value + "' and " + primary_filed + "!='" + _primary_value + "' and compid='" + compid + "' order by " + fld_nm);
        }

        public DataSet Find_Name_Details(string fld_nm, string tbl_nm, string _value, string _primary_value, string catalog, string compid)
        {
            string primary_filed = objDLTran.GetPrimaryKeyFldNm(tbl_nm, catalog);
            return objdbLayer.dsquery("select * from  " + tbl_nm + " where " + fld_nm + "='" + _value + "' and compid='" + compid + "' order by " + fld_nm);
        }

        public DataSet Get_Product_group_Details(string tbl_nm, string fields, string pkeyId, string compid)
        {
            string primary_filed = objDLTran.GetPrimaryKeyFldNm(tbl_nm, "");
            return objdbLayer.dsquery("select * from  " + tbl_nm + " where " + primary_filed + "='" + pkeyId + "' and compid='" + compid + "' order by " + primary_filed);
        }

        public bool Find_Login_User(string user_nm, string pwd, string fin_yr, string comp_id)
        {
            try
            {
                GetCorrectCon();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select login_mast.* from login_mast inner join roles_mapping on login_mast.userid=roles_mapping.userid where login_mast.user_nm='" + user_nm + "' and login_mast.pwd='" + pwd + "' and roles_mapping.fin_yr='" + fin_yr + "' and login_mast.compid=" + comp_id, con);
                // 
                da.Fill(ds);
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["pwd"].ToString() == pwd.ToString())
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
            }
            return false;
        }

        public bool Find_DB_Existance(string db_nm)
        {
            try
            {
                GetCorrectCon();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select db_id('" + db_nm + "') db_nm", con);
                da.Fill(ds);
                if (ds != null && ds.Tables[0].Rows[0]["db_nm"] != null && ds.Tables[0].Rows[0]["db_nm"].ToString() != "")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
            }
            return false;
        }

        public int Find_DB_Count(string db_nm)
        {
            SqlConnection myConn = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source").ToString() + ";initial catalog=master;user=" + objIni.GetKeyFieldValue("SQL", "user").ToString() + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd").ToString());
            try
            {
                GetCorrectCon();
                DataSet ds = new DataSet();
                myConn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select count(*) cnt from sysdatabases where [name] like '" + db_nm + "'", myConn);
                da.Fill(ds);
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    return int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
                return 0;
            }
            finally
            {
                myConn.Close();
            }
        }

        public bool Create_DataBase(string db_nm, string restore_bak_up_nm)
        {
            DataSet dset = new DataSet();
            objIni.SetKeyFieldValue("SQL", "initial catalog", "master");
            ServerConnection con = new ServerConnection(objIni.GetKeyFieldValue("SQL", "data source"), objIni.GetKeyFieldValue("SQL", "user"), objIni.GetKeyFieldValue("SQL", "pwd"));//"INODE-SHA\\iMANTRA", "sa", "sa1985");
            bool flg = true;
            try
            {
                #region comment
                string dbName = db_nm;
                string backupFileName = AppDomain.CurrentDomain.BaseDirectory + @"DATABASE\" + restore_bak_up_nm + ".bak";
                Server server = new Server(con);

                if (!server.Databases.Contains(dbName))
                {
                    Database database = new Database(server, db_nm);
                    database.FileGroups.Add(new FileGroup(database, "PRIMARY"));

                    DataFile dtPrimary = new DataFile(database.FileGroups["PRIMARY"], db_nm, AppDomain.CurrentDomain.BaseDirectory + @"DATABASE\" + db_nm + ".mdf".Trim());
                    dtPrimary.Size = 77.0 * 1024.0;
                    dtPrimary.GrowthType = FileGrowthType.KB;
                    dtPrimary.Growth = 1.0 * 1024.0;
                    database.FileGroups["PRIMARY"].Files.Add(dtPrimary);

                    LogFile logFile = new LogFile(database, db_nm + "_log", AppDomain.CurrentDomain.BaseDirectory + @"DATABASE\" + db_nm + ".ldf".Trim());
                    logFile.Size = 7.0 * 1024.0;
                    logFile.GrowthType = FileGrowthType.Percent;
                    logFile.Growth = 10.0;

                    database.LogFiles.Add(logFile);
                    database.Create();
                    database.Refresh();
                }


                SqlConnection myConn = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));//"data source=INODE-SHA\\iMANTRA;initial catalog=master;user=sa;pwd=sa1985");
                string str = "RESTORE FILELISTONLY FROM DISK = '" + backupFileName + "'";
                SqlDataAdapter cmd = new SqlDataAdapter(str, myConn);
                myConn.Open();
                cmd.Fill(dset);

                Restore restore = new Restore();
                Database database1 = server.Databases[db_nm];
                restore.Database = database1.Name;
                restore.ReplaceDatabase = true;
                restore.Action = RestoreActionType.Database;
                restore.Devices.AddDevice(backupFileName, DeviceType.File);

                RelocateFile relocateDataFile = new RelocateFile(dset.Tables[0].Rows[0][0].ToString(), AppDomain.CurrentDomain.BaseDirectory + @"DATABASE\" + db_nm + ".mdf");
                RelocateFile relocateLogFile = new RelocateFile(dset.Tables[0].Rows[1][0].ToString(), AppDomain.CurrentDomain.BaseDirectory + @"DATABASE\" + db_nm + ".ldf");

                restore.RelocateFiles.Add(relocateDataFile);
                restore.RelocateFiles.Add(relocateLogFile);

                restore.ReplaceDatabase = true;
                restore.PercentCompleteNotification = 10;

                restore.PercentComplete += new PercentCompleteEventHandler(ProgressEventHandler);
                restore.SqlRestore(server);
                #endregion
            }
            catch (Exception ex)
            {
                flg = false;
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
            }
            finally
            {
                con.Disconnect();
            }
            return flg;
        }
        public bool Create_BakUp(string db_nm, string path)
        {
            string str = "";
            try
            {
                DataSet dset = new DataSet();
                objIni.SetKeyFieldValue("SQL", "initial catalog", db_nm);
                GetCorrectCon();
                str = "BACKUP DATABASE InodeMFG TO DISK='" + path + "\\InodeMFG" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak'";

                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();

                str = "BACKUP DATABASE " + db_nm + " TO DISK='" + path + "\\" + db_nm + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak'";

                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        static void ProgressEventHandler(object sender, PercentCompleteEventArgs e)
        {
        }
        public bool CreateFolder(string floder_nm)
        {
            try
            {
                if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory))
                {
                    string pathString = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, floder_nm);
                    System.IO.Directory.CreateDirectory(pathString);
                }
                return true;
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
                return false;
            }
        }

        public int Get_Company_Name(BL_BASEFIELD objBLFD)
        {
            try
            {
                GetCorrectCon();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter("select count(*)+1 cnt from ORG_MAST where comp_nm like left('" + objBLFD.HTMAIN["COMP_NM"].ToString() + "',1)+'%' and year('" + DateTime.Parse(objBLFD.HTMAIN["FIN_YR_STA"].ToString()).ToString("yyyy/MM/dd") + "')=year(fin_yr_sta) and year('" + DateTime.Parse(objBLFD.HTMAIN["FIN_YR_END"].ToString()).ToString("yyyy/MM/dd") + "')=year(fin_yr_end)", con);
                da.Fill(ds);
                return int.Parse(ds.Tables[0].Rows[0]["cnt"].ToString());
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Connection Error : " + ex.Message, "Error", 5000);
                return 0;
            }
        }

        public DataSet GETFIN_YEAR()
        {
            return objdbLayer.dsquery("select distinct fin_yr from COMP_FIN_YR_MAPPING order by fin_yr desc");
        }

        public DataSet Get_Company_Details(string comp_id)
        {
            return objdbLayer.dsquery("select * from ORG_MAST where compid=case when('" + comp_id + "'!='0') then " + comp_id + " else compid end");
        }
        public DataSet Get_Company_Details_Fin_Yr(string comp_id, string fin_yr)
        {
            string strQuery = "select ORG_MAST.[CompId],ORG_MAST.[Comp_nm],[Add1],[Add2],[Add3],[Remark],[Logo],[Dir_nm],[Pan_no],[Tin_no],[cst_no],[Tds_no],[Phone],[Email],[Decimal],[Dir_bkup],[Bkup_dt],COMP_FIN_YR_MAPPING.[Fin_yr_sta],COMP_FIN_YR_MAPPING.[Fin_yr_end],[holiday],[neg_pt_bal],[fax],[website],[db_nm],[frm_bakup],[folder_nm],[rate_dec],[curr],[area_nm],[zone_nm],[city_nm],[zip],[state_nm],[country_nm],[cuser_nm],[cuser_dt],[muser_nm],[muser_dt],[tran_cd],COMP_FIN_YR_MAPPING.[Fin_yr],[range],[division],[collrate],[ecc_no],[iec_no],[rangeadd],[colladd],[DIVADD],[state_id],[country_id],[Punch_ln],[ser_tax_no] from ORG_MAST inner join COMP_FIN_YR_MAPPING on ORG_MAST.compid=COMP_FIN_YR_MAPPING.compid where ORG_MAST.compid=case when('" + comp_id + "'!='0') then " + comp_id + " else ORG_MAST.compid end and COMP_FIN_YR_MAPPING.fin_yr='" + fin_yr + "'";
            return objdbLayer.dsquery(strQuery);
            //return objdbLayer.dsquery("select * from ORG_MAST where compid=case when('" + comp_id + "'!='0') then " + comp_id + " else compid end");
        }

        public DataSet GET_Comp_by_Fin_yr(string fin_yr)
        {
            return objdbLayer.dsquery("select ORG_MAST.Comp_nm,ORG_MAST.compid from ORG_MAST inner join COMP_FIN_YR_MAPPING on ORG_MAST.compid=COMP_FIN_YR_MAPPING.compid where COMP_FIN_YR_MAPPING.fin_yr like '" + fin_yr + "' order by compid");
        }
    }
}
