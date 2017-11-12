using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using iMANTRA_iniL;

namespace CUSTOM_iMANTRA
{
    public class dblayer
    {
        SqlConnection con;// = new SqlConnection("data source=INODE-SHA\\IMFG;initial catalog=iMFG;user=sa;pwd=sa1985");
        SqlCommand com;
        Ini objIni = new Ini();
        public void GetCorrectCon()
        {
            con = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));//new SqlConnection("data source=PADMA\\IMFG;initial catalog=N011314;user=sa;pwd=sa1985");
        }
        public bool execInsert(string str_table, Hashtable htParams)
        {
            try
            {
                GetCorrectCon();
                /* Create the field list and parameter list */
                string strFieldList = "";
                string strParamList = "";

                foreach (DictionaryEntry p in htParams)
                {
                    //com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                    if (strFieldList.Length > 0)
                        strFieldList = strFieldList + ",[" + (string)p.Key + "]";
                    else
                        strFieldList = strFieldList + "[" + (string)p.Key + "]";

                    if (strParamList.Length > 0)
                        strParamList = strParamList + ",@" + (string)p.Key;
                    else
                        strParamList = strParamList + "@" + (string)p.Key;
                }

                ///* Construct the SQL Query with Parameters */
                string strSql = string.Format("insert into [{0}] ({1}) values ({2})", str_table, strFieldList, strParamList);

                com = new SqlCommand(strSql, con);
                com.CommandType = CommandType.Text;
                /* Add the Parameters */
                string sql = string.Empty;
                if (htParams != null)
                {
                    foreach (DictionaryEntry p in htParams)
                    {
                        sql += p.Value.ToString() + ",";
                        com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));

                    }
                }

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(strSql);
                    sw.WriteLine(sql);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();

                con.Open();

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(ex.Message);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public bool execQuery(string str_query)
        {
            try
            {
                GetCorrectCon();               
              
                com = new SqlCommand(str_query, con);
                com.CommandType = CommandType.Text;
                /* Add the Parameters */
          
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(str_query);                    
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();

                con.Open();

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(ex.Message);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public bool execUpdate(string str_table, Hashtable htParams, Hashtable htCond = null)
        {
            try
            {
                GetCorrectCon();
                /* Create the field list and parameter list */
                string strUpdateList = "";
                string strConditionList = "";
                foreach (DictionaryEntry p in htParams)
                {
                    //com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                    if (strUpdateList.Length > 0)
                        strUpdateList = strUpdateList + ",[" + (string)p.Key + "]=" + "@" + (string)p.Key;
                    else
                        strUpdateList = strUpdateList + "[" + (string)p.Key + "]=" + "@" + (string)p.Key;
                }

                if (htCond != null)
                {
                    foreach (DictionaryEntry p in htCond)
                    {
                        //com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                        if (strConditionList.Length > 0)
                            strConditionList = strConditionList + " AND [" + (string)p.Key + "]=" + "@" + (string)p.Key;
                        else
                            strConditionList = strConditionList + " WHERE [" + (string)p.Key + "]=" + "@" + (string)p.Key;
                    }
                }

                /* Construct the SQL Query with Parameters */
                string strSql = string.Format("update [{0}] set {1} {2}", str_table, strUpdateList, strConditionList);

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(strSql);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();

                com = new SqlCommand(strSql, con);
                com.CommandType = CommandType.Text;
                /* Add the Parameters */
                if (htParams != null)
                {
                    foreach (DictionaryEntry p in htParams)
                    {
                        com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                    }
                }

                if (htCond != null)
                {
                    foreach (DictionaryEntry p in htCond)
                    {
                        com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                    }
                }

                con.Open();


                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(ex.Message);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();

                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public DataSet dsquery(string sqlstr)
        {
            GetCorrectCon();
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            try
            {
                //ds = new DataSet();
                da = new SqlDataAdapter(sqlstr, con);
                da.Fill(ds, "temp");
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
            }
        }

        public bool execDelete(string str_table, Hashtable htCond = null, int nForceLogMode = 1)
        {
            try
            {
                GetCorrectCon();
                /* Create the field list and parameter list */
                string strConditionList = "";

                if (htCond != null)
                {
                    foreach (DictionaryEntry p in htCond)
                    {
                        //com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                        if (strConditionList.Length > 0)
                            strConditionList = strConditionList + " AND [" + (string)p.Key + "]=" + "@" + (string)p.Key;
                        else
                            strConditionList = strConditionList + " WHERE [" + (string)p.Key + "]=" + "@" + (string)p.Key;
                    }
                }

                /* Construct the SQL Query with Parameters */
                string strSql = string.Format("delete from [{0}] {1}", str_table, strConditionList);

                com = new SqlCommand(strSql, con);
                com.CommandType = CommandType.Text;
                /* Add the Parameters */

                if (htCond != null)
                {
                    foreach (DictionaryEntry p in htCond)
                    {
                        com.Parameters.Add(new SqlParameter((string)p.Key, p.Value));
                    }
                }

                con.Open();
                com.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public bool execDeleteQuery(string sqlquery)
        {
            try
            {
                GetCorrectCon();

                string strSql = string.Format(sqlquery);

                com = new SqlCommand(strSql, con);
                com.CommandType = CommandType.Text;

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(strSql);
                   // sw.WriteLine(sql);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();

                con.Open();

                com.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt"))
                {
                    File.Create(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt");
                }
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"hostname.txt", true);
                sw.WriteLine("-------------------------------------------" + DateTime.Now + "------------------------------------------------");
                try
                {
                    sw.WriteLine(ex.Message);
                }
                catch (Exception ex1)
                {
                    sw.WriteLine("text file not found" + ex1.Message);
                }
                sw.WriteLine("-------------------------------------------end------------------------------------------------");
                sw.Close();
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public ArrayList SelectQueryFixed(string sqlstr)
        {
            try
            {
                GetCorrectCon();
                com = new SqlCommand(sqlstr, con);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                ArrayList value_list = new ArrayList();
                int i;
                while (dr.Read())
                {
                    ArrayList row = new ArrayList();
                    for (i = 0; i < dr.FieldCount; i++)
                    {
                        row.Add(dr.GetValue(i));
                    }
                    value_list.Add(row);
                }
                return value_list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public DataSet ExecuteQuery(string query, string paramenter)
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
            }
            return dsRepdoc;
        }
        public DataSet dsprocedure(string sqlstr, Hashtable param)
        {
            DataSet ds = new DataSet();
            try
            {
                GetCorrectCon();
                SqlDataAdapter da = new SqlDataAdapter(sqlstr, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry d in param)
                {
                    da.SelectCommand.Parameters.AddWithValue((string)d.Key, d.Value);
                }
                da.Fill(ds, "temp");
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
            }
        }
    }
}
