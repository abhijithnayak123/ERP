using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Management;
using iMANTRA_BL;
using iMANTRA_IL;
using iMANTRA_iniL;
using iMANTRA_DL;
using System.Collections;

namespace iMANTRA
{
    public partial class frmLogin : BaseClass
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd. 
         * 1.0 on 11.11.13  => License file added.
         * 2.0 on 11.15.13  => Added Logo based on Company 
         * 
         * */
        public BL_MAIN_FIELDS objBLMain = new BL_MAIN_FIELDS();
        
        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

        FL_MAST objFLMAST = new FL_MAST();
        FL_GEN_INVOICE objFLGENINV = new FL_GEN_INVOICE();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        Ini objIni = new Ini();
        IniKey objIniKey = new IniKey();

        //LI_License objLicense = new LI_License();

        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();

        DataSet dset = new DataSet();

        private string tran_cd, system_nm, system_ip, mac_id, mother_board_id, license_type;
        string localIP = "?";
        public string Tran_cd
        {
            get { return tran_cd; }
            set { tran_cd = value; }
        }
        private int flg_close = 0;
        public frmLogin(int i = 0)
        {
            InitializeComponent();
            //objTimer.Interval = 15000;
            //objTimer.Enabled = false;
            //this.objTimer.Elapsed += new ElapsedEventHandler(this.tmr_Login_Tick);
            flg_close = i;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (flg_close == 1)
            {
                //this.Close();
                //timer1.Enabled = false;
                //backgroundWorker1.CancelAsync();
            }
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            #region 2.0
            dset = objVALIDATION.Get_Company_Details("0");
            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
            {
                if (File.Exists(dset.Tables[0].Rows[0]["logo"].ToString()))
                {
                    int _lastIndex = dset.Tables[0].Rows[0]["logo"].ToString().LastIndexOf('\\');
                    if (dset.Tables[0].Rows[0]["logo"].ToString().Substring(_lastIndex).ToString() != "\\window_add.png")
                    {
                        pictureBox2.Image = Image.FromFile(dset.Tables[0].Rows[0]["logo"].ToString());
                    }
                }

                objBLMain.No_of_users = VALIDATIONLAYER.Decrypt(objIniKey.GetKeyFieldValue("KEYTABLE", "key_field"));
                objBLMain.Local_ip = localIP;
                cmbfin_yr.DataSource = objVALIDATION.GETFIN_YEAR().Tables[0];
                cmbfin_yr.DisplayMember = "fin_yr";
                cmbfin_yr.ValueMember = "fin_yr";
                cmbfin_yr.Update();
                Bind_Comp_Value();

                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
                ucToolBar1.UCbackcolor = Color.SlateGray;
                ucToolBar1.Width1 = this.Width;
                ucToolBar1.Titlebar = "iMANTRA Login";
            }
            else
            {
                AutoClosingMessageBox.Show("Sorry!! Organization is not Created.", "Error");
                this.Close();
            }
            #endregion 2.0
        }

        #region 2.0
        private void SetImage()
        {
            if (cmbComp_nm.SelectedValue.ToString() != "System.Data.DataRowView" && cmbfin_yr.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                dset = objVALIDATION.Get_Company_Details_Fin_Yr(cmbComp_nm.SelectedValue.ToString(), cmbfin_yr.SelectedValue.ToString());
                if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
                {
                    if (File.Exists(dset.Tables[0].Rows[0]["logo"].ToString()))
                    {
                        int _lastIndex = dset.Tables[0].Rows[0]["logo"].ToString().LastIndexOf('\\');
                        if (dset.Tables[0].Rows[0]["logo"].ToString().Substring(_lastIndex).ToString() != "\\window_add.png")
                        {
                            pictureBox2.Image = Image.FromFile(dset.Tables[0].Rows[0]["logo"].ToString());
                        }
                        else
                        {
                            pictureBox2.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\inode-logo1.png");
                        }
                    }
                }
            }
        }
        #endregion 2.0

        private void Bind_Comp_Value()
        {
            if (cmbfin_yr.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                cmbComp_nm.DataSource = objVALIDATION.GET_Comp_by_Fin_yr(cmbfin_yr.SelectedValue.ToString()).Tables[0];
                cmbComp_nm.DisplayMember = "Comp_nm";
                cmbComp_nm.ValueMember = "compid";
                cmbComp_nm.Update();
            }
        }
        public void processLogin()
        {
            DataSet dset = objVALIDATION.Get_Company_Details_Fin_Yr(cmbComp_nm.SelectedValue.ToString(), cmbfin_yr.SelectedValue.ToString());
            this.Hide();
            frm_mainmenu objmain = new frm_mainmenu();

            //objmain.Text = dset.Tables[0].Rows[0]["comp_nm"].ToString();
            objmain.ObjBLMainFields = objBLMain;
            objmain.ObjBLMainFields.Login_type = "login";
            objmain.ObjBLMainFields.CurUser = txtUserNm.Text;
            objmain.ObjBLMainFields.Fin_yr = cmbfin_yr.SelectedValue.ToString();
            objmain.ObjBLMainFields.Comp_nm = cmbComp_nm.SelectedValue.ToString();
            objmain.ObjBLMainFields.No_of_users = objBLMain.No_of_users;
            objmain.ObjBLMainFields.User_theme = objBLMain.User_theme;

            if (dset != null && dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
            {
                //user logg. Sharanamma on 05.04.14
                if (dset.Tables[0].Rows[0]["db_nm"] != null && dset.Tables[0].Rows[0]["db_nm"].ToString() != "")
                {
                    objIni.SetKeyFieldValue("SQL", "initial catalog", dset.Tables[0].Rows[0]["db_nm"].ToString());//setting database
                    objFLGENINV.InsertUpdateAndDelete(objBLMain.CurUser, "1", objBLMain.Local_ip);//update datetime
                    objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");//setting database
                }

                Type t = typeof(Company);
                PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                foreach (PropertyInfo field in publicFieldInfos)
                {
                    foreach (DataColumn col in dset.Tables[0].Columns)
                    {
                        if (null != field && field.CanWrite && field.Name.ToLower() == col.ColumnName.ToLower())
                        {
                            if (field.PropertyType.Name.ToLower() != "string")
                            {
                                if (dset.Tables[0].Rows[0][col.ColumnName] != null && dset.Tables[0].Rows[0][col.ColumnName].ToString() != "")
                                {
                                    objmain.ObjBLComp.GetType().GetProperty(field.Name).SetValue(objmain.ObjBLComp, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                                }
                            }
                            else
                                objmain.ObjBLComp.GetType().GetProperty(field.Name).SetValue(objmain.ObjBLComp, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                        }
                    }
                }
                objmain.ObjBLComp.Fin_yr = cmbfin_yr.SelectedValue.ToString();
                objmain.ObjBLComp.Compid = int.Parse(cmbComp_nm.SelectedValue.ToString());
                objIni.SetKeyFieldValue("APP_PATH", "bakuppath", objmain.ObjBLComp.Frm_bakup);
                objmain.ShowDialog();
            }
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //sharanamma on 05.08.13 reason:user restriction
                //begin
                bool flgLoginContinue = true;
                if (cmbComp_nm.SelectedValue != null)
                {
                    #region
                    //string strCon = "";
                    //int days = 1;
                    //string strMotherBoardId = "";
                    //ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                    //foreach (ManagementObject share in searcher.Get())
                    //{
                    //    strMotherBoardId = share.Properties["ProcessorId"].Value.ToString();
                    //    system_nm = share.Properties["SystemName"].Value.ToString();
                    //}
                    //string[] str = new string[] { "I", "T", "P", "L" };
                    //int j = 0;
                    //foreach (string st in str)
                    //{
                    //    j += 1;
                    //    strMotherBoardId = strMotherBoardId.Insert(j * 4 + (j - 1), st);
                    //}
                    //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + strMotherBoardId + ".xml"))
                    //{
                    //    DataSet dsCompDet = objVALIDATION.Get_Company_Details(cmbComp_nm.SelectedValue.ToString());
                    //    if (dsCompDet != null && dsCompDet.Tables.Count != 0 && dsCompDet.Tables[0].Rows.Count != 0)
                    //    {
                    //        XmlDocument xmlDoc = new XmlDocument();
                    //        xmlDoc.Load(strMotherBoardId + ".xml");

                    //        bool flgIn = false;
                    //        string strKey = "";
                    //        foreach (DataRow row in dsCompDet.Tables[0].Rows)
                    //        {
                    //            strKey = "";
                    //            XmlNodeList node = xmlDoc.SelectNodes("Orgnization");

                    //            foreach (XmlNode root in node)
                    //            {
                    //                if (root.HasChildNodes)
                    //                {
                    //                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    //                    {
                    //                        if (root.ChildNodes[i].Name == "key")
                    //                        {
                    //                            strKey = root.ChildNodes[i].InnerText;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            if (strKey != "")
                    //            {
                    //                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("select * from Win32_Processor");
                    //                foreach (ManagementObject share in searcher1.Get())
                    //                {
                    //                    mother_board_id = share.Properties["ProcessorId"].Value.ToString();
                    //                    mac_id = VALIDATIONLAYER.identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
                    //                    system_nm = share.Properties["SystemName"].Value.ToString();
                    //                }
                    //                string[] strArrayKeys = strKey.Split(new string[] { "+$IT-PL$+" }, StringSplitOptions.RemoveEmptyEntries);
                    //                if (strArrayKeys != null && strArrayKeys.Length != 0 && strArrayKeys.Length == 8)
                    //                {
                    //                    flgIn = true;
                    //                    foreach (DataColumn col in dsCompDet.Tables[0].Columns)
                    //                    {
                    //                        switch (col.ColumnName.ToLower())
                    //                        {
                    //                            case "comp_nm": flgIn = strArrayKeys[0] == VALIDATIONLAYER.Encrypt(row[col.ColumnName].ToString()) ? true : false;
                    //                                break;
                    //                            case "module_cd": flgIn = strArrayKeys[2] == row[col.ColumnName].ToString() ? true : false;
                    //                                break;
                    //                        }
                    //                        if (!flgIn)
                    //                        {
                    //                            break;
                    //                        }
                    //                    }
                    //                    if (flgIn)
                    //                    {
                    //                        if (strArrayKeys[4] != VALIDATIONLAYER.Encrypt(mac_id))
                    //                        {
                    //                            flgIn = false;
                    //                        }
                    //                        if (flgIn && strArrayKeys[5] != VALIDATIONLAYER.Encrypt(mother_board_id))
                    //                        {
                    //                            flgIn = false;
                    //                        }
                    //                        if (flgIn)
                    //                        {
                    //                            license_type = VALIDATIONLAYER.Decrypt(strArrayKeys[6]);
                    //                            objBLMain.License_type = license_type;
                    //                            if (license_type == "CUSTOMER LICENSE")
                    //                            {
                    //                                strCon = DateTime.Now.ToString();
                    //                            }
                    //                            else
                    //                            {
                    //                                strCon = VALIDATIONLAYER.Decrypt(strArrayKeys[7]);
                    //                                days = 30;
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    flgIn = false;
                    //                }
                    //            }
                    //        }
                    //        if (flgIn)
                    //        {
                    //            if ((DateTime.Now - Convert.ToDateTime(strCon != "" ? strCon : "01/01/1900")).Days <= days)
                    //            {
                    #endregion
                    if (objVALIDATION.Find_Login_User(txtUserNm.Text.Replace("'", "''"), txtPassword.Text, cmbfin_yr.SelectedValue.ToString(), cmbComp_nm.SelectedValue.ToString().Replace("'", "''")))
                    {
                        #region
                        objBLMain.CurUser = txtUserNm.Text;
                        #endregion
                        //user logg. Sharanamma on 05.04.14
                        objFLGENINV.InsertUpdateAndDelete(objBLMain.CurUser, "1", objBLMain.Local_ip);//update datetime

                        if (flgLoginContinue)
                        {
                            DataSet ds = objDL_ADAPTER.dsquery("select lm.*,rm.role_nm from login_mast lm inner join roles_mapping rm on lm.user_nm=rm.user_nm and lm.compid=rm.compid and lm.userid=rm.userid where lm.USER_NM='" + txtUserNm.Text.Replace("'", "''") + "'");
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                            {
                                string user;
                                user = VALIDATIONLAYER.Decrypt(ds.Tables[0].Rows[0]["SEC_CODE"].ToString());
                                objBLMain.User_theme = ds.Tables[0].Rows[0]["theme_nm"] != null ? ds.Tables[0].Rows[0]["theme_nm"].ToString() : "";
                                objBLMain.Role_nm = ds.Tables[0].Rows[0]["role_nm"] != null ? ds.Tables[0].Rows[0]["role_nm"].ToString() : "";
                                if (user == txtUserNm.Text)
                                {
                                    processLogin();
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show("Unauthorised attempt to login", "Error");
                                    flgLoginContinue = false;
                                    txtUserNm.Clear();
                                    txtPassword.Clear();
                                }
                            }
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter valid user/password", "Error");
                        txtUserNm.Text = "";
                        txtPassword.Text = "";
                        txtUserNm.Focus();
                    }
                    #region
                    //    }
                    //    else
                    //    {
                    //        AutoClosingMessageBox.Show("Sorry!! " + license_type + " Expired. Please Contact Inode Technologies Pvt. Ltd.", "License");
                    //        this.Close();
                    //    }
                    //}
                    //else
                    //{
                    //    AutoClosingMessageBox.Show("Sorry!! Invalid License. Please Contact Inode Technologies Pvt. Ltd.", "License");
                    //    this.Close();
                    //}
                    //    }
                    //}
                    //else
                    //{
                    //    this.Hide();
                    //    frmGenerateXML objGenerateXML = new frmGenerateXML();
                    //    objGenerateXML.ShowDialog();
                    //    this.Close();
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception");
            }
        }

        public static void Decrypt(XmlDocument Doc, RSA Alg, string KeyName)
        {
            try
            {
                // Check the arguments.   
                if (Doc == null)
                    throw new ArgumentNullException("Doc");
                if (Alg == null)
                    throw new ArgumentNullException("Alg");
                if (KeyName == null)
                    throw new ArgumentNullException("KeyName");

                // Create a new EncryptedXml object.
                EncryptedXml exml = new EncryptedXml(Doc);

                // Add a key-name mapping. 
                // This method can only decrypt documents 
                // that present the specified key name.
                exml.AddKeyNameMapping(KeyName, Alg);

                // Decrypt the element.
                exml.DecryptDocument();
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Sorry!! License is not Valid.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbfin_yr_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Comp_Value();
        }
        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.Select();
            }
        }
        private void cmbfin_yr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.Select();
            }
        }
        private void cmbComp_nm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.Select();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //BL_MAIN_FIELDS argumentTest = e.Argument as BL_MAIN_FIELDS;

            //int return_type = objFLGENINV.GetLoginStatus(argumentTest.CurUser, 1, argumentTest.No_of_users, argumentTest.Local_ip);
            //if (return_type == 4)
            //{
            //    objFLGENINV.InsertUpdateAndDelete(argumentTest.CurUser, "4", objBLMain.Local_ip);//update datetime
            //}
            //else if (return_type == 1)
            //{
            //    //if (flg_close == 0)
            //    //{
            //    //    MessageBox.Show("Sorry! Timer Expired");
            //    //}
            //    timer1.Enabled = false;
            //    backgroundWorker1.CancelAsync();
            //}
            //e.Result = argumentTest;
            //if ((backgroundWorker1.CancellationPending == true))
            //{
            //    e.Cancel = true;
            //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (!backgroundWorker1.IsBusy)
            //    backgroundWorker1.RunWorkerAsync(objBLMain);
            //else
            //    MessageBox.Show("Can't run the worker twice!");
        }
        #region 2.0
        private void cmbComp_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetImage();
        }
        #endregion 2.0
    }
}
