using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using iMANTRA_IL;
using iMANTRA_BL;
using Ionic.Zip;
using iMANTRA_iniL;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CUSTOM_iMANTRA;
using System.Threading;
using System.Diagnostics;
using WebToolKit;
using License;
using System.Data.SqlClient;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace iMANTRA
{
    public partial class frm_mainmenu : BaseClass
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd.  
        * 1.0 on 11.08.13 => License file created. --> added new field establish(application installation date.) & created ini file.
        * 2.0 on 11.15.13 => Saving Logo based on Company 
        * 3.0 on 11.21.13 => Final Update
        * 4.0 Sharanamma Jekeen on 11.26.13 ==> Added new Class in Custom Layer
         * 5.0 Sharanamma Jekeen on 12.04.13 ==> Added New Master File Upload Master
         * 6.0 Sharanamma Jekeen on 12.30.13 ==> dyanamically create Menus.
         * 
         * 
        * */

        ToolStripMenuItem MnuStripItem;
        SqlConnection conn;

        //business layer defined.
        private BL_MAIN_FIELDS _objBLMainFields = new BL_MAIN_FIELDS();
        private BL_TRAN_SET _objBL_TRAN_SET = new BL_TRAN_SET();

        private Company objBLComp = new Company();
        private BL_BASEFIELD activeBLBF = new BL_BASEFIELD();
        //themes are defined.
        private BL_CONTROL_SET objControlSetTran = new BL_CONTROL_SET();
        public BL_CONTROL_SET ObjControlSetTran
        {
            get { return objControlSetTran; }
            set { objControlSetTran = value; }
        }
        private BL_CONTROL_SET objControlSetMaster = new BL_CONTROL_SET();
        private BL_CONTROL_SET objControlSetReport = new BL_CONTROL_SET();
        private BL_CONTROL_SET objControlSetCustomMaster = new BL_CONTROL_SET();


        public BL_TRAN_SET ObjBL_TRAN_SET
        {
            get { return _objBL_TRAN_SET; }
            set { _objBL_TRAN_SET = value; }
        }

        //intermediate/facade layer classes defined.
        LI_License objLicense = new LI_License();

        FL_BASEFIELD objBaseField = new FL_BASEFIELD();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        FL_GEN_INVOICE objFLGenInvoice = new FL_GEN_INVOICE();
        FL_MAST objFLMAST = new FL_MAST();
        FL_REP_MAST objFLRep = new FL_REP_MAST();
        FL_Roles objRoles = new FL_Roles();
        FL_GENERAL objFLGeneral = new FL_GENERAL();

        


        //validations are defined. 
        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();
        //custom layer classes defined.
        SaveTransactionAndMaster objSAVETRANSANDMASTER = new SaveTransactionAndMaster();//save trigger
        iAfterValidateSave objAfterValidateSave = new iAfterValidateSave();//2.0//after save trigger
        AddTransactionAndMaster objADDTRANSANDMASTER = new AddTransactionAndMaster();//add trigger
        EditTransactionAndMaster objEditTRANSANDMASTER = new EditTransactionAndMaster();//edit trigger
        DeleteTransactionAndMaster objDeleteTRANSANDMASTER = new DeleteTransactionAndMaster();//delete trigger
        ViewTransactionAndMaster objViewTRANSANDMASTER = new ViewTransactionAndMaster();//view trigger
        iFinalUpdateTransactionAndMaster objfinalUpdate = new iFinalUpdateTransactionAndMaster();//final update trigger no return value
        Ini objIni = new Ini();//initialise/show or hide controls in the page

        Hashtable _htModuleList_menumain = new Hashtable(StringComparer.InvariantCultureIgnoreCase);//contains selected modules list.

        ifrm_transaction objtransaction;//transaction template class instance
        ifrm_Accounting objAccounting;
        frmReportPreview objfrmrp;//report preview instance

        frm_mast_item objfrm_mast;//master template instance

        //  frmShortCut objShortCut;//left panel shortcuts

        // DataSet ActiveBLBF.dsNavigation = new DataSet();

        public BL_BASEFIELD ActiveBLBF
        {
            get { return activeBLBF; }
            set { activeBLBF = value; }
        }
        public BL_MAIN_FIELDS ObjBLMainFields
        {
            get { return _objBLMainFields; }
            set { _objBLMainFields = value; }
        }
        public Company ObjBLComp
        {
            get { return objBLComp; }
            set { objBLComp = value; }
        }

        private DataRow[] row;
        string tran_cd = "", tran_mode = "view_mode";
        string Module_list = "";

        public string Tran_cd
        {
            get { return tran_cd; }
            set { tran_cd = value; }
        }
        public string Tran_mode
        {
            get { return tran_mode; }
            set { tran_mode = value; }
        }
        string tran_id, login_frm;
        int i = 0;
        private string _Comp_Id, _fin_yr;
        private static string user, no_of_user;
        private bool _reLoginBit = false, _showShortCut = true;

        public bool ShowShortCut
        {
            get { return _showShortCut; }
            set { _showShortCut = value; }
        }

        public string Fin_yr
        {
            get { return _fin_yr; }
            set { _fin_yr = value; }
        }

        public string Comp_Id
        {
            get { return _Comp_Id; }
            set { _Comp_Id = value; }
        }

        private int xpos = 0, ypos = 0;

        public frm_mainmenu()
        {
            InitializeComponent();
        }
        private void frm_mainmenu_Load(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("iMANTRA-MANUFACTURING");
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Left = Top = 0;
                Width = Screen.PrimaryScreen.WorkingArea.Width;
                Height = Screen.PrimaryScreen.WorkingArea.Height;
                user = ObjBLMainFields.CurUser;
                no_of_user = ObjBLMainFields.No_of_users;

                FL_GEN_INVOICE objGen = new FL_GEN_INVOICE();
                objGen.user = user;
                objGen.no_of_user = no_of_user;

                login_frm = _objBLMainFields.Login_type;
                _Comp_Id = ObjBLMainFields.Comp_nm;
                _fin_yr = ObjBLMainFields.Fin_yr;
                //main/parent title bar
                tsstatuslbl.Text = "Logged User : " + ObjBLMainFields.CurUser + " - on " + String.Format("{0:dddd, MMMM dd, yyyy}", DateTime.Now);
                //find the size of the screen to adjust our forms based on this.
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                this.ClientSize = new Size(this.ClientSize.Width, this.Height);
                this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\inodeimage.jpg");

                enableCloseModeButtons();//tools-->close mode activate.
                //check its from login or from creating new form.
                if (login_frm != "login")//from new company
                {
                    Open_Comp_Menu();//call method to create new company.
                    //set default theme settings
                    this.BackColor = Color.White;
                    this.ForeColor = Color.Black; ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
                    ucToolBar1.Width1 = this.Width;
                    ucToolBar1.Titlebar = "Inode Technologies Pvt. Ltd.";
                    ucToolBar1.UCbackcolor = Color.Firebrick;
                    objControlSetMaster.Back_color = "White";
                    objControlSetMaster.Font_color = "Black";
                    objControlSetMaster.Uc_color = "Firebrick";
                    objControlSetMaster.Tab_back_color = "White";
                    objControlSetMaster.Tab_font_color = "Black";
                }
                else
                {
                    //set selected user theme setting to theme class instance based on tranction type.
                    objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);

                    _objBL_TRAN_SET.HT_TRAN_TBLS.Clear();

                    DataSet dsetTranSet = objFLTransaction.GetTrans_Settings("", ObjBLComp.Compid.ToString());

                    if (dsetTranSet != null && dsetTranSet.Tables.Count != 0 && dsetTranSet.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow row in dsetTranSet.Tables[0].Rows)
                        {
                            if (!_objBL_TRAN_SET.HT_TRAN_TBLS.Contains(row["code"]))
                            {
                                _objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["Main_tbl_nm"] = row["Main_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["Item_tbl_nm"] = row["Item_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["Ref_tbl_nm"] = row["Ref_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["Approve_tbl_nm"] = row["Approve_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["Extra_tbl_nm"] = row["Extra_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["ac_tbl_nm"] = row["ac_tbl_nm"];
                            ((Hashtable)_objBL_TRAN_SET.HT_TRAN_TBLS[row["code"]])["alloc_tbl_nm"] = row["alloc_tbl_nm"];
                        }
                    }


                    DataSet dset = objBaseField.GetControl_Setting(objBLComp.Compid.ToString(), ObjBLMainFields.User_theme);

                    Type t = typeof(BL_CONTROL_SET);
                    PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    if (dset != null && dset.Tables.Count != 0)
                    {
                        DataRow[] rows = dset.Tables[0].Select("tran_type='Transaction'");
                        AssignThemeToSystem(rows, ObjControlSetTran, publicFieldInfos, dset);

                        DataRow[] rowsMaster = dset.Tables[0].Select("tran_type='Master'");
                        AssignThemeToSystem(rowsMaster, objControlSetMaster, publicFieldInfos, dset);

                        DataRow[] rowsReport = dset.Tables[0].Select("tran_type='Report'");
                        AssignThemeToSystem(rowsReport, objControlSetReport, publicFieldInfos, dset);

                        DataRow[] rowCustomMaster = dset.Tables[0].Select("tran_type='CustomMaster'");
                        AssignThemeToSystem(rowCustomMaster, objControlSetCustomMaster, publicFieldInfos, dset);
                    }
                    ucToolBar1.Width1 = this.Width;
                    activeBLBF.ObjControlSet = objControlSetMaster;
                    //page themes setting
                    AddThemesToTitleBar((Form)this, ucToolBar1, activeBLBF, "Master");
                    ucToolBar1.Maximize = true;
                    if (objLicense.License_code == "sZLKa7BcYsepmYbILRVBlg==")
                    {
                        ucToolBar1.Titlebar = "iMANTRA - " + ObjBLComp.Comp_nm + " - Financial Year : " + objBLComp.Fin_yr + " DEMO VERSION ";
                    }
                    else
                    {
                        ucToolBar1.Titlebar = "iMANTRA - " + ObjBLComp.Comp_nm + " - Financial Year : " + objBLComp.Fin_yr;
                    }
                    //ucToolBar1.Font = new Font(ucToolBar1.Font.FontFamily,14);
                    //check company & modules are existed or not.
                    if (objBLComp != null && objBLComp.Module_cd != null && objBLComp.Module_cd != "")
                    {
                        string[] str = objBLComp.Module_cd.Split(new string[] { "+$IPTLM$+" }, StringSplitOptions.RemoveEmptyEntries);
                        string[] strDecrypt = new string[str.Length];
                        int i = 0;
                        if (str.Length != 0)
                        {
                            foreach (string s in str)
                            {
                                strDecrypt[i] = VALIDATIONLAYER.Decrypt(s);
                                i++;
                            }
                            _htModuleList_menumain.Clear();
                            foreach (string st in strDecrypt)
                            {
                                if (_htModuleList_menumain != null && !_htModuleList_menumain.Contains(st))
                                {
                                    _htModuleList_menumain[st] = "1";
                                }
                            }
                            foreach (string st in strDecrypt)
                            {
                                if (Module_list != "")
                                {
                                    Module_list += " or parent_cd like '%" + st + "%'";
                                }
                                else
                                {
                                    Module_list = "(parent_cd like '' or  parent_cd like '%" + st + "%'";
                                }
                            }
                            Module_list += ")";
                        }
                    }

                    //set page title bar.
                    ucToolBar1.Titlebar = "iMANTRA - " + ObjBLComp.Comp_nm + " - Financial Year : " + objBLComp.Fin_yr;

                    activeBLBF.HtModuleList = _htModuleList_menumain;//assign modules to business layer
                    ActiveBLBF.ObjCompany = objBLComp; activeBLBF.ObjBL_TRAN_SET = _objBL_TRAN_SET;//assign company details to business layer.
                    activeBLBF.ObjLoginUser = ObjBLMainFields;
                    //menu creation start here.
                    DataSet dsetParent = objFLGeneral.GetMenuListParent(activeBLBF, Module_list);
                    if (dsetParent != null && dsetParent.Tables.Count != 0 && dsetParent.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow dr in dsetParent.Tables[0].Rows)
                        {
                            MnuStripItem = new ToolStripMenuItem(dr["menu_nm"].ToString());
                            SubMenu(MnuStripItem, dr["menu_nm"].ToString(), dr["parent_id"].ToString());//create sub menu
                            mstool.Items.Add(MnuStripItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.Close();
            }
        }

        #region MENUS 6.0
        //sub menu creation method.
        public void SubMenu(ToolStripMenuItem mnu, string submenu, string parentId)
        {
            //get the child/sub menu details from main/parent menu id.
            DataSet dsetChild = objFLGeneral.GetMenuListChild(activeBLBF, parentId, Module_list);
            if (dsetChild != null && dsetChild.Tables.Count != 0 && dsetChild.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in dsetChild.Tables[0].Rows)
                {
                    ToolStripMenuItem SSMenu = new ToolStripMenuItem();
                    SSMenu.Text = dr["menu_nm"].ToString();
                    if ((dr["code"].ToString() != "" && dr["frm_nm"].ToString() != "") || (dr["menu_nm"].ToString().ToLower() == "alerts" || dr["menu_nm"].ToString().ToLower() == "about" || dr["menu_nm"].ToString().ToLower() == "help" || dr["menu_nm"].ToString().ToLower() == "back up" || dr["menu_nm"].ToString().ToLower() == "&log-off" || dr["menu_nm"].ToString().ToLower() == "&exit"))
                    {
                        SSMenu.Tag = dr["code"].ToString() + "$" + dr["frm_nm"].ToString() + "$" + dr["condition"].ToString() + "$" + dr["menu_type"].ToString();
                        if (dr["code"].ToString() == "OM" || dr["code"].ToString() == "UL" || dr["code"].ToString() == "RL" || dr["code"].ToString() == "RM")
                        {
                            SSMenu.Click += new EventHandler(ORGChildClick);
                        }
                        else
                        {
                            SSMenu.Click += new EventHandler(ChildClick);
                        }
                    }
                    else if (dr["menu_type"] != null && dr["menu_type"].ToString() == "Report" && dr["frm_nm"].ToString() != "")
                    {
                        SSMenu.Tag = dr["alias_nm"].ToString() + "$" + dr["condition"].ToString() + "$" + dr["menu_type"].ToString();
                        SSMenu.Click += new EventHandler(ReportChildClick);
                    }
                    SubMenu(SSMenu, dr["menu_nm"].ToString(), dr["menu_level_id"].ToString());//contruct all menu's by iterating sub menu method.
                    mnu.DropDownItems.Add(SSMenu);
                }
            }
        }
        //implement transaction child menu acions
        public void ChildClick(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (clickedItem != null)
            {
                if (clickedItem.Text.ToLower() == "&log-off")//menu ABOUT create
                {
                    if (this.ActiveMdiChild != null)
                    {
                        AutoClosingMessageBox.Show("Please Close all the child forms", "Close");
                    }
                    else
                    {
                        this._reLoginBit = true;
                        this.Hide();
                        // objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                        this.Close();
                        frmLogin objLogin = new frmLogin(1);
                        objLogin.ShowDialog();
                    }
                }
                else if (clickedItem.Text.ToLower() == "&exit")//menu ABOUT create
                {
                    if (this.ActiveMdiChild != null)
                    {
                        AutoClosingMessageBox.Show("Please Close all the child forms", "Close");
                    }
                    else
                    {
                        // objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                        this.Close();
                    }
                }
                else if (clickedItem.Text.ToLower() == "about")//menu ABOUT create
                {
                    frmAbout objabout = new frmAbout();
                    objabout.MdiParent = this;
                    objabout.Show();
                }
                else if (clickedItem.Text.ToLower() == "alerts")//menu ABOUT create
                {
                    BL_BASEFIELD objBaseField = new BL_BASEFIELD();
                    ActiveBLBF = objBaseField;
                    ActiveBLBF.ObjCompany = objBLComp; activeBLBF.ObjBL_TRAN_SET = _objBL_TRAN_SET; activeBLBF.HtModuleList = _htModuleList_menumain;
                    activeBLBF.ObjControlSet = objControlSetCustomMaster;
                    activeBLBF.ObjLoginUser = ObjBLMainFields;

                }
                else if (clickedItem.Text.ToLower() == "send information")//menu ABOUT create
                {
                    objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                    BL_BASEFIELD objBaseField = new BL_BASEFIELD();
                    ActiveBLBF = objBaseField;
                    ActiveBLBF.ObjCompany = objBLComp; activeBLBF.ObjBL_TRAN_SET = _objBL_TRAN_SET; activeBLBF.HtModuleList = _htModuleList_menumain;
                    activeBLBF.ObjControlSet = objControlSetCustomMaster;
                    activeBLBF.ObjLoginUser = ObjBLMainFields;
                    //frmSendInfo objabout = new frmSendInfo(activeBLBF);
                    //objabout.MdiParent = this;
                    //objabout.Show();
                }

                else if (clickedItem.Text.ToLower() == "help")//menu HELP creation
                {
                    string path = objIni.GetKeyFieldValue("APP_PATH", "path");
                    Process.Start(path + @"\i-Mantra.pdf");
                }
                else if (clickedItem.Text.ToLower() == "back up")//menu BACK UP creation
                {
                    BL_BASEFIELD objBaseField = new BL_BASEFIELD();
                    ActiveBLBF = objBaseField;
                    ActiveBLBF.ObjCompany = objBLComp; activeBLBF.ObjBL_TRAN_SET = _objBL_TRAN_SET; activeBLBF.HtModuleList = _htModuleList_menumain;
                    activeBLBF.ObjControlSet = objControlSetCustomMaster;
                    activeBLBF.ObjLoginUser = ObjBLMainFields;
                    frmBakUp objbakup = new frmBakUp(activeBLBF);
                    objbakup.ShowDialog();
                }
                else
                {
                    string[] strCode = clickedItem.Tag.ToString().Split('$');//default all transaction template creation.
                    try
                    {
                        objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);//setting database
                        BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD(); objBASEFILEDS.ObjCompany = objBLComp;
                        if (BindTransactionSetting(objBASEFILEDS, strCode[0]))//assign all properties of transcation from transaction setting to business layer
                        {
                            objRoles.ObjMainFields = ObjBLMainFields;//logged users details
                            if (objRoles.CheckPermisson("view," + objBASEFILEDS.Tran_nm))//check authority/permission given to this transaction or not.
                            {
                                objBASEFILEDS.Tran_mode = "view_mode";//open transacation in view mode
                                objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();//find primary key
                                objBASEFILEDS.FormCondition = strCode.Length == 3 && strCode[2] != null ? strCode[2] : "";//set default conditions
                                objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, objBASEFILEDS.FormCondition);//get all data of the transactaion table

                                objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                                //assign company,selected modules list,theme,login users details to business layer.
                                ActiveBLBF = objBASEFILEDS; ActiveBLBF.ObjCompany = objBLComp; activeBLBF.ObjBL_TRAN_SET = _objBL_TRAN_SET;
                                activeBLBF.HtModuleList = _htModuleList_menumain;
                                if (((strCode.Length == 4 && strCode[3] != null) ? strCode[3] : "") == "Transaction")
                                {
                                    activeBLBF.ObjControlSet = objControlSetTran;
                                }
                                else
                                {
                                    activeBLBF.ObjControlSet = objControlSetCustomMaster;
                                }
                                activeBLBF.ObjLoginUser = ObjBLMainFields;
                                //  frmVendorMast objVendorMast = new frmVendorMast(objBASEFILEDS);
                                Type t = Type.GetType("iMANTRA." + strCode[1]);//create form type by form name.

                                activeBLBF.TRAN_CD = strCode[0];//assign transcation code to business layer.
                                if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                                {
                                    i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                                    activeBLBF.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                    ActiveBLBF.Tran_id = activeBLBF.Tran_id;
                                    ObjBLMainFields.HASHTOOL[activeBLBF.TRAN_CD + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                }
                                else
                                {
                                    activeBLBF.Tran_id = "0";
                                    ActiveBLBF.Tran_id = activeBLBF.Tran_id;
                                    ObjBLMainFields.HASHTOOL[activeBLBF.TRAN_CD + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                    i = 0;
                                }
                                activeBLBF.Tran_mode = "view_mode";
                                ViewModeTrigger();//call trigger
                                Form objForm = Activator.CreateInstance(t, new object[] { activeBLBF }) as Form;//create instance of the transcation from form type.
                                //if (activeBLBF.Code == "TS")
                                //{
                                //    MessageBox.Show("Transaction Setting --1 ");
                                //}
                                objForm.Name = objBASEFILEDS.Tran_nm;
                                objForm.MdiParent = this;
                                objForm.Show();//call transaction template.
                            }
                            else
                            {
                                AutoClosingMessageBox.Show("Access Denied", "Access Rights");//permission is denied.
                            }
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Transaction is not exist", "Invalid Transaction");//transaction is not existed.
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        this.Activate();
                    }
                }
            }
        }
        private void ViewModeTrigger()
        {
            bool flgValid = true;
            objViewTRANSANDMASTER.ACTIVE_BL = activeBLBF;
            //call viw trigger & get the default setting in edit mode if it is assigned.
            flgValid = objViewTRANSANDMASTER.tsViewTransactionAndMaster();
            if (flgValid)
            {
                if (activeBLBF.HTMAIN != null)
                {
                    activeBLBF.HTMAIN = objViewTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                }
                if (activeBLBF.HTITEM != null)
                {
                    activeBLBF.HTITEM = objViewTRANSANDMASTER.ACTIVE_BL.HTITEM;
                }
                if (activeBLBF.HTEXTRA != null)
                {
                    activeBLBF.HTEXTRA = objViewTRANSANDMASTER.ACTIVE_BL.HTEXTRA;
                }
            }
            else
            {
                if (objViewTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                {
                    AutoClosingMessageBox.Show(objViewTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                    this.Tran_mode = "view_mode";
                    RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                }
            }
        }


        //implement Comapny master actions
        public void ORGChildClick(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (clickedItem != null)
            {
                string[] strCode = clickedItem.Tag.ToString().Split('$');
                if (strCode[0] != "OM")//company is created.
                {
                    BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
                    objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                    objBASEFILEDS.Tran_mode = "view_mode";
                    objBASEFILEDS.Tran_type = "CustomMaster";
                    objBASEFILEDS.Tbl_catalog = "InodeMFG";
                    objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                    if (strCode[0] == "UL")//users creation
                    {
                        objBASEFILEDS.Code = "UL";
                        objBASEFILEDS.Main_tbl_nm = "LOGIN_MAST";
                        objBASEFILEDS.Item_tbl_nm = "";
                        objBASEFILEDS.Tran_nm = clickedItem.Text.ToLower() == "change password" ? "Change Password" : "User Creation";
                        strCode[1] = clickedItem.Text.ToLower() == "change password" ? "frmChangePwd" : "frmUserCreation";
                    }
                    else if (strCode[0] == "RL")//roles & rights
                    {
                        objBASEFILEDS.Code = "RL";
                        objBASEFILEDS.Main_tbl_nm = "ROLES";
                        objBASEFILEDS.Item_tbl_nm = "RIGHTS";
                        objBASEFILEDS.Tran_nm = "Roles And Rights";
                        strCode[1] = "frmRolesAndRights";
                    }
                    else if (strCode[0] == "RM")//roles mapping
                    {
                        objBASEFILEDS.Code = "RM";
                        objBASEFILEDS.Main_tbl_nm = "ROLES_MAPPING";
                        objBASEFILEDS.Item_tbl_nm = "";
                        objBASEFILEDS.Tran_nm = "Role Mapping";
                        strCode[1] = "frmRolesMapping";
                    }
                    try
                    {
                        //create business layer & assign all properties & open the one of the above masters
                        objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();
                        objBASEFILEDS.ObjCompany = objBLComp;
                        objBASEFILEDS.HtModuleList = _htModuleList_menumain;
                        objBASEFILEDS.ObjControlSet = objControlSetCustomMaster;
                        objBASEFILEDS.ObjLoginUser = ObjBLMainFields;
                        objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, "");//, "ac_nm not in ('WORK ORDER','CONSUMPTION','PRODUCTION')");
                        objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                        ActiveBLBF = objBASEFILEDS;
                        Type t = Type.GetType("iMANTRA." + strCode[1]);
                        activeBLBF.TRAN_CD = strCode[0];
                        if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                        {
                            i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                            activeBLBF.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                            ActiveBLBF.Tran_id = activeBLBF.Tran_id;
                            ObjBLMainFields.HASHTOOL[activeBLBF.TRAN_CD + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                        }
                        else
                        {
                            activeBLBF.Tran_id = "0";
                            ActiveBLBF.Tran_id = activeBLBF.Tran_id;
                            ObjBLMainFields.HASHTOOL[activeBLBF.TRAN_CD + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                            i = 0;
                        }
                        activeBLBF.Tran_mode = "view_mode";
                        Form objForm = Activator.CreateInstance(t, new object[] { activeBLBF }) as Form;

                        objForm.Name = objBASEFILEDS.Tran_nm;
                        objForm.MdiParent = this;
                        objForm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    finally
                    {
                        this.Activate();
                    }
                }
                else
                {
                    tsbtnFirst.Enabled = false;
                    tsbtnLast.Enabled = false;
                    tsbtnNext.Enabled = false;
                    tsbtnPrev.Enabled = false;
                    Open_Comp_Menu();
                }
            }
        }
        //implement Report actions
        public void ReportChildClick(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (clickedItem != null)
            {
                string[] strCode = clickedItem.Tag.ToString().Split('$');
                try
                {
                    enablePrintPreviewModeButtons();//print tool bar activate
                    objfrmrp = new frmReportPreview();//create report preview instance.
                    objfrmrp.MdiParent = this; //objfrmrp.ObjCompany = objBLComp;
                    BL_BASEFIELD objBLBF = new BL_BASEFIELD();
                    objBLBF.Tran_type = "Report";
                    objBLBF.ObjControlSet = objControlSetReport;
                    objBLBF.ObjCompany = objBLComp;
                    objBLBF.HtModuleList = _htModuleList_menumain;
                    objfrmrp.ObjBLFD = objBLBF;
                    ActiveBLBF = objBLBF;
                    //assign report name/group name.
                    objfrmrp.Rep_group = strCode.Length != 0 && strCode[0] == "" ? clickedItem.Text : strCode[0];
                    objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
                    objfrmrp.Show();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this.Activate();
                }
            }
        }
        #endregion 6.0
        //set theme class fields properties from selected user theme.
        private void AssignThemeToSystem(DataRow[] rows, BL_CONTROL_SET objControlSetForAll, PropertyInfo[] publicFieldInfos, DataSet dset)
        {
            if (rows != null && rows.Length != 0)
            {
                foreach (PropertyInfo field in publicFieldInfos)
                {
                    foreach (DataColumn col in dset.Tables[0].Columns)
                    {
                        if (null != field && field.CanWrite && field.Name.ToLower() == col.ColumnName.ToLower())
                        {
                            if (field.PropertyType.Name.ToLower() != "string")
                            {
                                if (dset.Tables[0].Rows[0][col.ColumnName] != null && rows[0][col.ColumnName].ToString() != "")
                                {
                                    objControlSetForAll.GetType().GetProperty(field.Name).SetValue(objControlSetForAll, Convert.ChangeType(rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                                }
                            }
                            else
                                objControlSetForAll.GetType().GetProperty(field.Name).SetValue(objControlSetForAll, Convert.ChangeType(rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                        }
                    }
                }
            }
        }
        //NAVIGATION KEYS ARE ENABLES ONLY IN VIEW MODE.
        #region NAVIGATION KEY DETAILS
        //show or hide navigation tool bar buttons based on data in the transaction table.
        private void Btn_Show()
        {
            // ActiveBLBF.dsNavigation = (DataSet)HASHDATASET[Tran_cd];
            if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1 == 0)//not data in table.
            {
                tsbtnLast.Enabled = false;
                tsbtnNext.Enabled = false;
                tsbtnFirst.Enabled = false;
                tsbtnPrev.Enabled = false;
            }
            else if (ActiveBLBF.dsNavigation != null && i == ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1)//last details of the table.
            {
                tsbtnLast.Enabled = false;
                tsbtnNext.Enabled = false;
                tsbtnFirst.Enabled = true;
                tsbtnPrev.Enabled = true;
            }
            else if (i == 0)//table first details
            {
                tsbtnLast.Enabled = true;
                tsbtnNext.Enabled = true;
                tsbtnFirst.Enabled = false;
                tsbtnPrev.Enabled = false;
            }
            else //table intermediate details showing
            {
                tsbtnLast.Enabled = true;
                tsbtnNext.Enabled = true;
                tsbtnFirst.Enabled = true;
                tsbtnPrev.Enabled = true;
            }
        }
        //navigation in transaction template
        private void Navigation_Transaction()
        {
            objtransaction = this.ActiveMdiChild as ifrm_transaction;
            if (objtransaction != null)//check active child is transaction or not.
            {
                objtransaction.Tran_mode = this.Tran_mode;
                objtransaction.Tran_cd = this.Tran_cd;
                //find the trasaction primary key id
                objtransaction.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                ActiveBLBF.Tran_id = objtransaction.Tran_id;
                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                //add navigation key points
                ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                objtransaction.objBASEFILEDS = ActiveBLBF;
                //call all DisplayControlsonMode method in transaction teplate
                //it will enables/shows the controls data in the transaction based on transaction id
                objtransaction.DisplayControlsonMode(this.Tran_mode, 0);
                //refresh the tool bar buttons
                RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
            }
            else
            {
                objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                if (objAccounting != null)//check active child is transaction or not.
                {
                    objAccounting.Tran_mode = this.Tran_mode;
                    objAccounting.Tran_cd = this.Tran_cd;
                    //find the trasaction primary key id
                    objAccounting.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                    ActiveBLBF.Tran_id = objAccounting.Tran_id;
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                    //add navigation key points
                    ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objAccounting.objBASEFILEDS = ActiveBLBF;
                    //call all DisplayControlsonMode method in transaction teplate
                    //it will enables/shows the controls data in the transaction based on transaction id
                    objAccounting.DisplayControlsonMode(this.Tran_mode, 0);
                    //refresh the tool bar buttons
                    RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                }
            }
        }
        //navigation in custom  master forms
        private void Navigation_CustomMaster()
        {
            ActiveBLBF.Tran_mode = this.Tran_mode;
            ActiveBLBF.Code = this.Tran_cd;
            ActiveBLBF.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
            ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = ActiveBLBF.Tran_id;
            ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
            BaseClass objfrm = this.ActiveMdiChild as BaseClass;//created instance of base class using this will call custom forms.
            //call all SendMessageToClient method in custom method.
            //it will enables/shows the controls data in the transaction based on transaction id
            objfrm.SendMessageToClient(activeBLBF, "NAV");
            RefreshToolbar(ActiveBLBF.Code, ActiveBLBF.Tran_mode, activeBLBF.Tran_mode_type);
        }
        //navigation in master template
        private void Navigation_Master()
        {
            objfrm_mast = this.ActiveMdiChild as frm_mast_item;
            if (objfrm_mast != null)//check active child is master or not.
            {
                objfrm_mast.Tran_mode = this.Tran_mode;
                objfrm_mast.Tran_cd = this.Tran_cd;
                objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                objfrm_mast.objBASEFILEDS = ActiveBLBF;
                objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
            }
        }
        //see the first transaction/master/custom master details.
        private void tsbtnFirst_Click(object sender, EventArgs e)
        {
            i = 0;
            if (ActiveBLBF.dsNavigation != null)
            {
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Master")
                {
                    Navigation_Master();
                }
                else
                {
                    Navigation_CustomMaster();
                }
                Btn_Show();
            }
        }
        //see the previous transaction/master/custom master details.
        private void tsbtnPrev_Click(object sender, EventArgs e)
        {
            i--;
            if (ActiveBLBF.dsNavigation != null && i <= ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1 && i >= 0)
            {
                ObjBLMainFields.HASHTOOL[this.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Master")
                {
                    Navigation_Master();
                }
                else
                {
                    Navigation_CustomMaster();
                }
                Btn_Show();
            }
            else
            {
                i++;
                Btn_Show();
            }
        }
        //see the next transaction/master/custom master details.
        private void tsbtnNext_Click(object sender, EventArgs e)
        {
            i++;
            if (ActiveBLBF.dsNavigation != null && i <= ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1 && i >= 0)
            {
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Master")
                {
                    Navigation_Master();
                }
                else
                {
                    Navigation_CustomMaster();
                }
                Btn_Show();
            }
            else
            {
                i--;
                Btn_Show();
            }
        }
        //see the last transaction/master/custom master details.
        private void tsbtnLast_Click(object sender, EventArgs e)
        {
            i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
            if (ActiveBLBF.dsNavigation != null)
            {
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    Navigation_Transaction();
                }
                else if (ActiveBLBF.Tran_type == "Master")
                {
                    Navigation_Master();
                }
                else
                {
                    Navigation_CustomMaster();
                }
                Btn_Show();
            }
        }
        #endregion

        #region TOOL BAR DETAILS
        //transaction actions based on tool bar buttons click
        public void Button_Transaction_Action()
        {
            objtransaction = this.ActiveMdiChild as ifrm_transaction;
            if (objtransaction != null)//active child is transaction
            {
                #region
                objtransaction.Tran_mode = this.Tran_mode;
                objtransaction.Tran_cd = this.Tran_cd;
                bool flgValid = true;
                if (this.Tran_mode == "add_mode")//transaction in ADD MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("add," + activeBLBF.Tran_nm))//check ADD rights of transaction
                    {
                        objtransaction.Tran_id = "0";
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                        ActiveBLBF.Tran_id = objtransaction.Tran_id;
                        //call add trigger & get the default setting in add mode if it is assigned.
                        objADDTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            activeBLBF.HTITEM = objADDTRANSANDMASTER.ACTIVE_BL.HTITEM;
                        }
                        else
                        {
                            if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                else if (this.Tran_mode == "edit_mode")//transaction in EDIT MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("edit," + activeBLBF.Tran_nm))//check EDIT rights of transaction
                    {
                        //find current transaction id.
                        i = int.Parse(ObjBLMainFields.HASHTOOL[this.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                        objtransaction.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                        ActiveBLBF.Tran_id = objtransaction.Tran_id;

                        objEditTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        //call edit trigger & get the default setting in edit mode if it is assigned.
                        flgValid = objEditTRANSANDMASTER.tsEditTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objEditTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            activeBLBF.HTITEM = objEditTRANSANDMASTER.ACTIVE_BL.HTITEM;
                        }
                        else
                        {
                            if (objEditTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objEditTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                else if (this.Tran_mode == "view_mode") //transaction in VIEW MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("view," + activeBLBF.Tran_nm))//check VIEW rights of transaction
                    {
                        if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                        {
                            i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            objtransaction.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                        }
                        else
                        {
                            objtransaction.Tran_id = "0";
                        }

                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                        ActiveBLBF.Tran_id = objtransaction.Tran_id;
                        objViewTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        //call viw trigger & get the default setting in edit mode if it is assigned.
                        flgValid = objViewTRANSANDMASTER.tsViewTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objViewTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            activeBLBF.HTITEM = objViewTRANSANDMASTER.ACTIVE_BL.HTITEM;
                        }
                        else
                        {
                            if (objViewTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objViewTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                if (flgValid)
                {
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                    ActiveBLBF.Tran_id = objtransaction.Tran_id;
                    ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objtransaction.objBASEFILEDS = ActiveBLBF;
                    //call all DisplayControlsonMode method in transaction teplate
                    if (this.Tran_mode == "add_mode")
                    {
                        //it will bind the controls data in the transaction based on mode & current transcation id.
                        //1--> add new row in grid otherwise its 0.
                        objtransaction.DisplayControlsonMode(this.Tran_mode, 1);
                    }
                    else
                    {
                        objtransaction.DisplayControlsonMode(this.Tran_mode, 0);
                    }
                    RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
                }
                #endregion
            }
            else
            {
                objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                if (objAccounting != null)//active child is transaction
                {
                    #region
                    objAccounting.Tran_mode = this.Tran_mode;
                    objAccounting.Tran_cd = this.Tran_cd;
                    bool flgValid = true;
                    if (this.Tran_mode == "add_mode")//transaction in ADD MODE
                    {
                        objRoles.ObjMainFields = ObjBLMainFields;
                        if (objRoles.CheckPermisson("add," + activeBLBF.Tran_nm))//check ADD rights of transaction
                        {
                            objAccounting.Tran_id = "0";
                            ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                            ActiveBLBF.Tran_id = objAccounting.Tran_id;
                            //call add trigger & get the default setting in add mode if it is assigned.
                            objADDTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                            flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                            if (flgValid)
                            {
                                activeBLBF.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                                activeBLBF.HTITEM = objADDTRANSANDMASTER.ACTIVE_BL.HTITEM;
                            }
                            else
                            {
                                if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                    this.Tran_mode = "view_mode";
                                    RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                                }
                            }
                        }
                        else
                        {
                            flgValid = false;
                            AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                        }
                    }
                    else if (this.Tran_mode == "edit_mode")//transaction in EDIT MODE
                    {
                        objRoles.ObjMainFields = ObjBLMainFields;
                        if (objRoles.CheckPermisson("edit," + activeBLBF.Tran_nm))//check EDIT rights of transaction
                        {
                            //find current transaction id.
                            i = int.Parse(ObjBLMainFields.HASHTOOL[this.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            objAccounting.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                            ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                            ActiveBLBF.Tran_id = objAccounting.Tran_id;

                            objEditTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                            //call edit trigger & get the default setting in edit mode if it is assigned.
                            flgValid = objEditTRANSANDMASTER.tsEditTransactionAndMaster();
                            if (flgValid)
                            {
                                activeBLBF.HTMAIN = objEditTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                                activeBLBF.HTITEM = objEditTRANSANDMASTER.ACTIVE_BL.HTITEM;
                            }
                            else
                            {
                                if (objEditTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    AutoClosingMessageBox.Show(objEditTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                    this.Tran_mode = "view_mode";
                                    RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                                }
                            }
                        }
                        else
                        {
                            flgValid = false;
                            AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                        }
                    }
                    else if (this.Tran_mode == "view_mode") //transaction in VIEW MODE
                    {
                        objRoles.ObjMainFields = ObjBLMainFields;
                        if (objRoles.CheckPermisson("view," + activeBLBF.Tran_nm))//check VIEW rights of transaction
                        {
                            if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                            {
                                i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                objAccounting.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                            }
                            else
                            {
                                objAccounting.Tran_id = "0";
                            }

                            ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                            ActiveBLBF.Tran_id = objAccounting.Tran_id;
                            objViewTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                            //call viw trigger & get the default setting in edit mode if it is assigned.
                            flgValid = objViewTRANSANDMASTER.tsViewTransactionAndMaster();
                            if (flgValid)
                            {
                                activeBLBF.HTMAIN = objViewTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                                activeBLBF.HTITEM = objViewTRANSANDMASTER.ACTIVE_BL.HTITEM;
                            }
                            else
                            {
                                if (objViewTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    AutoClosingMessageBox.Show(objViewTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                    this.Tran_mode = "view_mode";
                                    RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                                }
                            }
                        }
                        else
                        {
                            flgValid = false;
                            AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                        }
                    }
                    if (flgValid)
                    {
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                        ActiveBLBF.Tran_id = objAccounting.Tran_id;
                        ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                        objAccounting.objBASEFILEDS = ActiveBLBF;
                        //call all DisplayControlsonMode method in transaction teplate
                        if (this.Tran_mode == "add_mode")
                        {
                            //it will bind the controls data in the transaction based on mode & current transcation id.
                            //1--> add new row in grid otherwise its 0.
                            objAccounting.DisplayControlsonMode(this.Tran_mode, 1);
                        }
                        else
                        {
                            objAccounting.DisplayControlsonMode(this.Tran_mode, 0);
                        }
                        RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                    }
                    #endregion
                }
            }
        }
        //custom master actions based on tool bar buttons click
        public void Button_CustomMaster_Action()
        {
            string Tran_id = "0", Tran_mode, Tran_cd;
            // Form objform = this.ActivateMdiChild;
            Tran_mode = activeBLBF.Tran_mode;
            Tran_cd = activeBLBF.Code;
            bool flgValid = true;
            if (this.Tran_mode == "add_mode")
            {
                objRoles.ObjMainFields = ObjBLMainFields;
                if (objRoles.CheckPermisson("add," + activeBLBF.Tran_nm))//check ADD rights of transaction
                {
                    Tran_id = "0";
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = Tran_id;
                    ActiveBLBF.Tran_id = Tran_id;

                    objADDTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                    flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                    if (flgValid)
                    {
                        activeBLBF.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                        activeBLBF.HTITEM = objADDTRANSANDMASTER.ACTIVE_BL.HTITEM;
                    }
                    else
                    {
                        if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                            this.Tran_mode = "view_mode";
                            RefreshToolbar(ActiveBLBF.Code, Tran_mode, activeBLBF.Tran_mode_type);
                        }
                    }
                }
                else
                {
                    flgValid = false;
                    AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                }
            }
            else if (this.Tran_mode == "edit_mode" && activeBLBF.Tran_mode_type == "regular") //transaction mode type should be regular.
            {
                objRoles.ObjMainFields = ObjBLMainFields;
                if (objRoles.CheckPermisson("edit," + activeBLBF.Tran_nm))//check EDIT rights of transaction
                {
                    if (activeBLBF.Code == "GM")
                    {
                        if (activeBLBF.HTMAIN.Contains("ac_grp_nm"))
                        {
                            if (activeBLBF.HTMAIN["ac_grp_nm"] != null && activeBLBF.HTMAIN["ac_grp_nm"].ToString() != "")
                            {
                                if (activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "ASSET" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "LIABILITIES" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "EXPENSE" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "INCOME" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING EXPENSE" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING INCOME")
                                {
                                    AutoClosingMessageBox.Show("Editing Parent of COA is not Possible", "COA EDIT");
                                    flgValid = false;
                                }
                            }
                        }
                    }
                    if (flgValid)
                    {
                        row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                        ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                        i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                        Tran_id = activeBLBF.Tran_id;

                        objEditTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        flgValid = objEditTRANSANDMASTER.tsEditTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objEditTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            activeBLBF.HTITEM = objEditTRANSANDMASTER.ACTIVE_BL.HTITEM;
                        }
                        else
                        {
                            if (objEditTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objEditTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                }
                else
                {
                    flgValid = false;
                    AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                }
            }
            else if (this.Tran_mode == "view_mode" && activeBLBF.Tran_mode_type == "regular")
            {
                objRoles.ObjMainFields = ObjBLMainFields;
                if (objRoles.CheckPermisson("view," + activeBLBF.Tran_nm))//check VIEW rights of transaction
                {
                    if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                    {
                        if (activeBLBF.Tran_id != "0")
                        {
                            row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                            ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                            i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            Tran_id = activeBLBF.Tran_id;
                        }
                        else
                        {
                            i = int.Parse(ObjBLMainFields.HASHTOOL[Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                            // Tran_id = activeBLBF.Tran_id;
                        }
                    }
                    else
                    {
                        Tran_id = "0";
                    }
                    activeBLBF.Tran_id = Tran_id;
                    objViewTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                    flgValid = objViewTRANSANDMASTER.tsViewTransactionAndMaster();
                    if (flgValid)
                    {
                        activeBLBF.HTMAIN = objViewTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                        activeBLBF.HTITEM = objViewTRANSANDMASTER.ACTIVE_BL.HTITEM;
                    }
                    else
                    {
                        if (objViewTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objViewTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                            this.Tran_mode = "view_mode";
                            RefreshToolbar(ActiveBLBF.Code, Tran_mode, activeBLBF.Tran_mode_type);
                        }
                    }
                }
                else
                {
                    flgValid = false;
                    AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                }
            }
            if (flgValid)
            {
                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = Tran_id;
                ActiveBLBF.Tran_id = Tran_id;
                //call all SendMessageToClient method in custom method with parameters like business layer and tool bar action name.
                //it will bind the controls data in the custom master based on sending parameters.
                BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                objfrm.SendMessageToClient(activeBLBF, "ADD/EDIT/VIEW");
                RefreshToolbar(Tran_cd, Tran_mode, activeBLBF.Tran_mode_type);
            }
        }
        //master actions based on tool bar buttons click
        private void Button_Master_Action()
        {
            objfrm_mast = this.ActiveMdiChild as frm_mast_item;
            if (objfrm_mast != null)//active child is master
            {
                objfrm_mast.Tran_mode = this.Tran_mode;
                objfrm_mast.Tran_cd = this.Tran_cd;
                bool flgValid = true;
                if (this.Tran_mode == "add_mode")//master in ADD MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("add," + activeBLBF.Tran_nm))//check ADD rights of master
                    {
                        objfrm_mast.Tran_id = "0";
                        ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                        objADDTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        //call add trigger & get the default setting in add mode if it is assigned.
                        flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                        }
                        else
                        {
                            if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                else if (this.Tran_mode == "edit_mode")//master in EDIT MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("edit," + activeBLBF.Tran_nm))//check EDIT rights of master
                    {
                        //find the current master id 
                        if (ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() != "0")
                        {
                            ActiveBLBF.Tran_id = ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString();
                            row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                            ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                            i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                        }
                        else
                        {
                            i = int.Parse(ObjBLMainFields.HASHTOOL[this.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                        }
                        ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                        objEditTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        //call edit trigger & get the default setting in edit mode if it is assigned.
                        flgValid = objEditTRANSANDMASTER.tsEditTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objEditTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                        }
                        else
                        {
                            if (objEditTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objEditTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                else if (this.Tran_mode == "view_mode")//master in VIEW MODE
                {
                    objRoles.ObjMainFields = ObjBLMainFields;
                    if (objRoles.CheckPermisson("view," + activeBLBF.Tran_nm))//check VIEW rights of master
                    {
                        if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                        {
                            i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                            objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                            ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                        }
                        else
                        {
                            objfrm_mast.Tran_id = "0";
                            ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                        }
                        ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                        objViewTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                        //call view trigger & get the default setting in edit mode if it is assigned.
                        flgValid = objViewTRANSANDMASTER.tsViewTransactionAndMaster();
                        if (flgValid)
                        {
                            activeBLBF.HTMAIN = objViewTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                        }
                        else
                        {
                            if (objViewTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objViewTRANSANDMASTER.BL_FIELDS.Errormsg, "Error ");
                                this.Tran_mode = "view_mode";
                                RefreshToolbar(ActiveBLBF.Code, this.Tran_mode, activeBLBF.Tran_mode_type);
                            }
                        }
                    }
                    else
                    {
                        flgValid = false;
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                    }
                }
                if (flgValid)
                {
                    ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objfrm_mast.objBASEFILEDS = ActiveBLBF;
                    //call all DisplayControlsonMode method in master teplate
                    //it will bind the controls data in the master based on mode & current master id.
                    objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                    RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
                }
            }
        }
        //add button in tool bar is clicked
        public void tsbtn_add_Click(object sender, EventArgs e)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    this.Tran_mode = "add_mode";
                    ActiveBLBF.Tran_mode = this.tran_mode;
                    if (ActiveBLBF.Tran_type == "Transaction")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Accounting")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Master")
                    {
                        Button_Master_Action();
                    }
                    else
                    {
                        Button_CustomMaster_Action();
                    }
                }
            }
        }
        //edit button in tool bar is clicked
        public void tsbtn_edit_Click(object sender, EventArgs e)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    this.Tran_mode = "edit_mode";
                    ActiveBLBF.Tran_mode = this.tran_mode;
                    if (ActiveBLBF.Tran_type == "Transaction")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Accounting")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Master")
                    {
                        Button_Master_Action();
                    }
                    else
                    {
                        Button_CustomMaster_Action();
                    }
                }
            }
        }
        //Save button in tool bar is clicked
        public void tsbtn_save_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            if (this.HasChildren)
            {
                //show waiting image
                pictureBox1.Visible = true;
                pictureBox1.Width = this.Width;
                pictureBox1.BackColor = this.BackColor;
                pictureBox1.Bounds = new Rectangle(this.Width * 25 / 100, this.Height * 25 / 100, this.Width / 2, this.Height * 1 / 20);

                objSAVETRANSANDMASTER.BL_FIELDS.SaveDetailsOrNOtFromDefault = true;//make flag true for allowing default save.
                objSAVETRANSANDMASTER.BL_FIELDS.Errormsg = "";//no error msg from trigger.

                if (this.ActiveMdiChild != null)
                {
                    if (ActiveBLBF.HTMAIN.Contains("FIN_YR"))
                    {
                        ActiveBLBF.HTMAIN["FIN_YR"] = objBLComp.Fin_yr;
                    }
                    if (ActiveBLBF.HTMAIN.Contains("COMPID"))
                    {
                        ActiveBLBF.HTMAIN["COMPID"] = objBLComp.Compid;
                    }
                    if (this.tran_cd != "")
                    {
                        ActiveBLBF.HTMAIN["TRAN_CD"] = this.tran_cd;
                    }
                    else
                    {
                        ActiveBLBF.HTMAIN["TRAN_CD"] = activeBLBF.Code;
                    }
                    #region
                    //objSAVETRANSANDMASTER.ACTIVE_BL = ActiveBLBF;
                    //if (objSAVETRANSANDMASTER.ValidateSave())
                    //{
                    #endregion
                    if (ActiveBLBF.Tran_type == "Transaction")
                    {
                        objtransaction = this.ActiveMdiChild as ifrm_transaction;
                        if (objtransaction != null)
                        {
                            ((ifrm_transaction)this.ActiveMdiChild).toolbar_clicked();
                            objSAVETRANSANDMASTER.ACTIVE_BL = ActiveBLBF;
                            if (objSAVETRANSANDMASTER.ValidateSave())
                            {
                                objVALIDATION.ObjBLFD = ActiveBLBF;
                                errormsg = objVALIDATION.ValidateToolSave();
                                if (errormsg == "")
                                {
                                    //_copy main_vw data into item_vw field
                                    DataRow[] rowtops = ActiveBLBF.dsHeader.Tables[0].Select("_copy=1");
                                    foreach (DictionaryEntry entry in ActiveBLBF.HTITEM)
                                    {
                                        if (((Hashtable)entry.Value).Count > 0)
                                        {
                                            foreach (DataRow row in rowtops)
                                            {
                                                if (row["fld_nm"] != null)
                                                {
                                                    ((Hashtable)entry.Value)[row["fld_nm"].ToString()] = ActiveBLBF.HTMAIN[row["fld_nm"].ToString()].ToString();
                                                }
                                            }
                                        }
                                    }
                                    #region
                                    //DataRow[] rowtops1 = ActiveBLBF.dsBASEADDIFIELD.Tables[0].Select("_copy=1");
                                    //foreach (DictionaryEntry entry in ActiveBLBF.HTITEM)
                                    //{
                                    //    if (((Hashtable)entry.Value).Count > 0)
                                    //    {
                                    //        foreach (DataRow row in rowtops1)
                                    //        {
                                    //            ((Hashtable)entry.Value)[row["fld_nm"].ToString()] = ActiveBLBF.HTMAIN[row["fld_nm"].ToString()].ToString();
                                    //        }
                                    //    }
                                    //}
                                    #endregion
                                    if (ActiveBLBF.HTMAIN.Contains("FIN_YR"))
                                    {
                                        activeBLBF.HTMAIN["FIN_YR"] = objBLComp.Fin_yr;
                                    }
                                    if (ActiveBLBF.HTMAIN.Contains("COMPID"))
                                    {
                                        activeBLBF.HTMAIN["COMPID"] = objBLComp.Compid;
                                    }
                                    objAfterValidateSave.ACTIVE_BL = ActiveBLBF;//2.0
                                    if (objAfterValidateSave.ValidateSave())//2.0
                                    {
                                        int gen_no = objFLGenInvoice.Find_Gen_Miss(ActiveBLBF.HTMAIN);
                                        if ((gen_no != 2) || (ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() != "" && ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() != "0"))
                                        {
                                            ActiveBLBF.Tran_mode = this.tran_mode;
                                            string error_msg = objFLTransaction.Save_Trasaction(ActiveBLBF);

                                            if (error_msg == "")
                                            {
                                                if (ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() == "" || ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() == "0")
                                                {
                                                    objFLGenInvoice.SaveGenInvoiceNumber(ActiveBLBF, gen_no);
                                                    objFLGenInvoice.SaveGenMiss(ActiveBLBF.HTMAIN);
                                                    //objFLGenInvoice.SaveGenInvoiceNumber(ActiveBLBF.HTMAIN, gen_no);                                               
                                                }
                                                if (this.Tran_mode == "add_mode")
                                                {
                                                    ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                                }
                                                if (this.Tran_mode == "add_mode")
                                                {
                                                    i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                                                    ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                                    tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                    objtransaction.Tran_id = tran_id;
                                                }
                                                else
                                                {
                                                    i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                    tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                    objtransaction.Tran_id = tran_id;
                                                }
                                                this.Tran_mode = "view_mode";
                                                ActiveBLBF.Tran_mode = this.tran_mode;
                                                objtransaction = this.ActiveMdiChild as ifrm_transaction;
                                                objtransaction.Tran_mode = this.Tran_mode;
                                                objtransaction.Tran_cd = this.Tran_cd;
                                                objtransaction.Tran_id = tran_id;
                                                ActiveBLBF.Tran_id = objtransaction.Tran_id;
                                                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                                                objtransaction.objBASEFILEDS = ActiveBLBF;
                                                FinalUpdateCall();//3.0
                                                objtransaction.DisplayControlsonMode(this.Tran_mode, 0);
                                                RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
                                                AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " Updated Successfully", "Update");
                                            }
                                            else
                                            {
                                                MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " Not Updated Successfully, Reason :" + error_msg, "Update Failed");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Sorry Transaction No. already exists!! ", "Validation");
                                        }
                                    }
                                    else
                                    {
                                        if (objAfterValidateSave.BL_FIELDS.Errormsg.Length != 0)
                                        {
                                            MessageBox.Show(objAfterValidateSave.BL_FIELDS.Errormsg, "Validation");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(errormsg, "Validation");
                                }
                            }
                            else
                            {
                                if (objSAVETRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    MessageBox.Show(objSAVETRANSANDMASTER.BL_FIELDS.Errormsg, "Validation");
                                }
                            }
                        }
                    }
                    else if (ActiveBLBF.Tran_type == "Accounting")
                    {
                        objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                        if (objAccounting != null)
                        {
                            ((ifrm_Accounting)this.ActiveMdiChild).toolbar_clicked();
                            objSAVETRANSANDMASTER.ACTIVE_BL = ActiveBLBF;
                            if (objSAVETRANSANDMASTER.ValidateSave())
                            {
                                objVALIDATION.ObjBLFD = ActiveBLBF;
                                errormsg = objVALIDATION.ValidateToolSave();
                                if (errormsg == "")
                                {
                                    if (ActiveBLBF.HTMAIN.Contains("FIN_YR"))
                                    {
                                        activeBLBF.HTMAIN["FIN_YR"] = objBLComp.Fin_yr;
                                    }
                                    if (ActiveBLBF.HTMAIN.Contains("COMPID"))
                                    {
                                        activeBLBF.HTMAIN["COMPID"] = objBLComp.Compid;
                                    }
                                    objAfterValidateSave.ACTIVE_BL = ActiveBLBF;//2.0
                                    if (objAfterValidateSave.ValidateSave())//2.0
                                    {
                                        int gen_no = objFLGenInvoice.Find_Gen_Miss(ActiveBLBF.HTMAIN);
                                        if ((gen_no != 2) || (ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() != "" && ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() != "0"))
                                        {
                                            ActiveBLBF.Tran_mode = this.tran_mode;
                                            string error_msg = objFLTransaction.Save_Trasaction(ActiveBLBF);

                                            if (error_msg == "")
                                            {
                                                if (ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() == "" || ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString() == "0")
                                                {
                                                    objFLGenInvoice.SaveGenInvoiceNumber(ActiveBLBF, gen_no);
                                                    objFLGenInvoice.SaveGenMiss(ActiveBLBF.HTMAIN);
                                                    //objFLGenInvoice.SaveGenInvoiceNumber(ActiveBLBF.HTMAIN, gen_no);                                               
                                                }
                                                if (this.Tran_mode == "add_mode")
                                                {
                                                    ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                                }
                                                if (this.Tran_mode == "add_mode")
                                                {
                                                    i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                                                    ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                                    tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                    objAccounting.Tran_id = tran_id;
                                                }
                                                else
                                                {
                                                    i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                    tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                    objAccounting.Tran_id = tran_id;
                                                }
                                                this.Tran_mode = "view_mode";
                                                ActiveBLBF.Tran_mode = this.tran_mode;
                                                objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                                                objAccounting.Tran_mode = this.Tran_mode;
                                                objAccounting.Tran_cd = this.Tran_cd;
                                                objAccounting.Tran_id = tran_id;
                                                ActiveBLBF.Tran_id = objAccounting.Tran_id;
                                                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                                                objAccounting.objBASEFILEDS = ActiveBLBF;
                                                FinalUpdateCall();//3.0
                                                objAccounting.DisplayControlsonMode(this.Tran_mode, 0);
                                                RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                                                AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " Updated Successfully", "Update");
                                            }
                                            else
                                            {
                                                MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " Not Updated Successfully, Reason :" + error_msg, "Update Failed");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Sorry Transaction No. already exists!! ", "Validation");
                                        }
                                    }
                                    else
                                    {
                                        if (objAfterValidateSave.BL_FIELDS.Errormsg.Length != 0)
                                        {
                                            MessageBox.Show(objAfterValidateSave.BL_FIELDS.Errormsg, "Validation");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(errormsg, "Validation");
                                }
                            }
                            else
                            {
                                if (objSAVETRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    MessageBox.Show(objSAVETRANSANDMASTER.BL_FIELDS.Errormsg, "Validation");
                                }
                            }
                        }
                    }
                    else if (ActiveBLBF.Tran_type == "Master")
                    {
                        objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                        if (objfrm_mast != null)
                        {
                            ((frm_mast_item)this.ActiveMdiChild).toolbar_clicked();
                            objSAVETRANSANDMASTER.ACTIVE_BL = ActiveBLBF;

                            if (ActiveBLBF.Code == "UL")
                            {
                                if (ActiveBLBF.Tran_mode == "add_mode")
                                {
                                    ActiveBLBF.HTMAIN["SEC_CODE"] = VALIDATIONLAYER.Encrypt(ActiveBLBF.HTMAIN["USER_NM"].ToString());
                                }
                            }

                            if (objSAVETRANSANDMASTER.ValidateSave())
                            {
                                objVALIDATION.ObjBLFD = ActiveBLBF;
                                errormsg = objVALIDATION.ValidateToolSave();
                                if (errormsg == "")
                                {
                                    if (tran_cd == "OM")
                                    {
                                        if (this.Tran_mode == "add_mode")
                                        {
                                            activeBLBF.HTMAIN["frm_bakup"] = objIni.GetKeyFieldValue("APP_PATH", "bakuppath");
                                            activeBLBF.HTMAIN["Bkup_dt"] = DateTime.Now.ToString("yyyy/MM/dd");
                                            #region 1.0
                                            //activeBLBF.HTMAIN["establish"] = VALIDATIONLAYER.Encrypt(DateTime.Now.ToString());
                                            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"" + activeBLBF.HTMAIN["db_nm"].ToString() + "License.ini"))
                                            //{
                                            //    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"" + activeBLBF.HTMAIN["db_nm"].ToString() + "License.ini");
                                            //}
                                            //File.Create(AppDomain.CurrentDomain.BaseDirectory + @"" + activeBLBF.HTMAIN["db_nm"].ToString() + "License.ini").Dispose();
                                            //StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"" + activeBLBF.HTMAIN["db_nm"].ToString() + "License.ini");
                                            //try
                                            //{
                                            //    sw.WriteLine("[ORGANIZATION]");
                                            //    sw.WriteLine("ORGANIZATION NAME = " + VALIDATIONLAYER.Encrypt(activeBLBF.HTMAIN["comp_nm"].ToString()));
                                            //    sw.WriteLine("ESTABLISH = " + activeBLBF.HTMAIN["establish"].ToString());
                                            //}
                                            //catch (Exception ex1)
                                            //{
                                            //    sw.WriteLine("text file not found" + ex1.Message);
                                            //}
                                            //File.SetAttributes(AppDomain.CurrentDomain.BaseDirectory + @"" + activeBLBF.HTMAIN["db_nm"].ToString() + "License.ini", FileAttributes.ReadOnly);
                                            //sw.Close();
                                            #endregion 1.0
                                        }
                                        #region 2.0
                                        int _lastIndex = activeBLBF.HTMAIN["logo"].ToString().LastIndexOf('\\');
                                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE/" + activeBLBF.HTMAIN["logo"].ToString().Substring(_lastIndex)))
                                        {
                                            System.IO.File.Copy(activeBLBF.HTMAIN["logo"].ToString(), AppDomain.CurrentDomain.BaseDirectory + @"IMAGE" + activeBLBF.HTMAIN["logo"].ToString().Substring(_lastIndex));
                                            activeBLBF.HTMAIN["logo"] = AppDomain.CurrentDomain.BaseDirectory + @"IMAGE" + activeBLBF.HTMAIN["logo"].ToString().Substring(_lastIndex);
                                        }
                                        #endregion 2.0
                                    }
                                    objAfterValidateSave.ACTIVE_BL = ActiveBLBF;//2.0
                                    if (objAfterValidateSave.ValidateSave())//2.0
                                    {
                                        if (ActiveBLBF.HTMAIN.Contains("module_cd") && ActiveBLBF.HTMAIN["module_cd"] != null && ActiveBLBF.HTMAIN["module_cd"].ToString() != "")
                                        {
                                            if (tran_cd == "OM" && activeBLBF.Tran_mode == "add_mode")
                                            {
                                                objVALIDATION.GetHashTableModuleList();
                                            }
                                            objFLTransaction.Save_Trasaction(ActiveBLBF);

                                            if (this.Tran_mode == "add_mode")
                                            {
                                                ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                            }
                                            if (this.Tran_mode == "add_mode")
                                            {
                                                i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                                                ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                                tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                objfrm_mast.Tran_id = tran_id;
                                            }
                                            else
                                            {
                                                i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                objfrm_mast.Tran_id = tran_id;
                                            }
                                            if (tran_cd == "OM" && this.Tran_mode == "add_mode")
                                            {
                                                string strBakUpNm = "iMFG-bakup";
                                                foreach (DictionaryEntry entry in activeBLBF.HtModuleList)
                                                {
                                                    if (entry.Key.ToString() == "BTRD")
                                                    {
                                                        strBakUpNm = "iMFG-bakup-trd";
                                                    }
                                                }

                                                if (objVALIDATION.Create_DataBase(ActiveBLBF.HTMAIN["DB_NM"].ToString(), strBakUpNm))
                                                {
                                                    objVALIDATION.CreateFolder(ActiveBLBF.HTMAIN["FOLDER_NM"].ToString());
                                                    try
                                                    {
                                                        objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                                                        activeBLBF.CompId = objFLTransaction.Get_Company_Id(activeBLBF);
                                                        if (activeBLBF.Tran_mode == "add_mode")
                                                        {
                                                            //
                                                            //objVALIDATION.GetHashTableModuleList();
                                                            objIni.SetKeyFieldValue("SQL", "initial catalog", "master");
                                                            objFLTransaction.Update_Module_to_ReportAndTran_Set(activeBLBF);
                                                            //
                                                        }
                                                        objIni.SetKeyFieldValue("SQL", "initial catalog", ActiveBLBF.HTMAIN["DB_NM"].ToString());
                                                        objFLTransaction.Update_Company_ID(activeBLBF);
                                                        if (activeBLBF.Tran_mode == "add_mode")
                                                        {
                                                            objFLTransaction.Update_Module_Reference_Type(activeBLBF);
                                                        }

                                                        string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + ActiveBLBF.HTMAIN["FOLDER_NM"].ToString();
                                                        foreach (DictionaryEntry entry in activeBLBF.HtModuleList)
                                                        {
                                                            using (ZipFile zip = ZipFile.Read(AppDomain.CurrentDomain.BaseDirectory + "ZIPFILES" + @"\ITPLMOD" + entry.Key + ".zip"))
                                                            {
                                                                foreach (ZipEntry zipe in zip)
                                                                {
                                                                    zipe.Extract(path, ExtractExistingFileAction.OverwriteSilently);  // overwrite == true
                                                                }
                                                            }
                                                        }

                                                        //string path1 = AppDomain.CurrentDomain.BaseDirectory;
                                                        //foreach (DictionaryEntry entry in activeBLBF.HtModuleList)
                                                        //{
                                                        //    if (entry.Key.ToString() == "BTRD")
                                                        //    {
                                                        //        using (ZipFile zip = ZipFile.Read(AppDomain.CurrentDomain.BaseDirectory + "SOURCEFILES" + @"\ITPLMOD" + entry.Key + ".zip"))
                                                        //        {
                                                        //            foreach (ZipEntry zipe in zip)
                                                        //            {
                                                        //                zipe.Extract(path1, ExtractExistingFileAction.OverwriteSilently);  // overwrite == true
                                                        //            }
                                                        //        }
                                                        //    }
                                                        //}
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show(ex.ToString(), "Exception");
                                                    }

                                                    AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(objfrm_mast.Name.ToLower()) + " Updated Successfully", "Update");
                                                    this.Hide();
                                                    objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                                                    //frmGenerateXML objGenerateXML = new frmGenerateXML();
                                                    //objGenerateXML.ShowDialog();

                                                    string path1 = AppDomain.CurrentDomain.BaseDirectory;
                                                    foreach (DictionaryEntry entry in activeBLBF.HtModuleList)
                                                    {
                                                        if (entry.Key.ToString() == "BTRD")
                                                        {
                                                            using (ZipFile zip = ZipFile.Read(AppDomain.CurrentDomain.BaseDirectory + "SOURCEFILES" + @"\ITPLMOD" + entry.Key + ".zip"))
                                                            {
                                                                foreach (ZipEntry zipe in zip)
                                                                {
                                                                    zipe.Extract(path1, ExtractExistingFileAction.OverwriteSilently);  // overwrite == true
                                                                }
                                                            }
                                                        }
                                                    }

                                                    this.Close();
                                                }
                                                else
                                                {
                                                    MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(objfrm_mast.Name.ToLower()) + " Not Updated Successfully", "Update Failed");
                                                }
                                            }
                                            else
                                            {
                                                this.Tran_mode = "view_mode";
                                                ActiveBLBF.Tran_mode = this.tran_mode;
                                                objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                                                objfrm_mast.Tran_mode = this.Tran_mode;
                                                objfrm_mast.Tran_cd = this.Tran_cd;
                                                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                                                ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                                                objfrm_mast.objBASEFILEDS = ActiveBLBF;
                                                FinalUpdateCall();//3.0
                                                objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                                                RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
                                                AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(objfrm_mast.Name.ToLower()) + " Updated Successfully", "Update");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Sorry! Please Select the Module", "Module Validation");
                                        }
                                    }
                                    else
                                    {
                                        if (objAfterValidateSave.BL_FIELDS.Errormsg.Length != 0)
                                        {
                                            MessageBox.Show(objAfterValidateSave.BL_FIELDS.Errormsg, "Validation");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(errormsg, "Validation");
                                }
                            }
                            else
                            {
                                if (objSAVETRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    MessageBox.Show(objSAVETRANSANDMASTER.BL_FIELDS.Errormsg, "Validation");
                                }
                            }
                        }
                    }
                    else
                    {
                        BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                        if (objfrm.SendMessageToClient(activeBLBF, "SAVE"))
                        {
                            objSAVETRANSANDMASTER.ACTIVE_BL = ActiveBLBF;
                            if (objSAVETRANSANDMASTER.ValidateSave())
                            {
                                objVALIDATION.ObjBLFD = ActiveBLBF;
                                errormsg = objVALIDATION.ValidateToolSave();
                                if (errormsg == "")
                                {
                                    ActiveBLBF.HTMAIN["TRAN_CD"] = activeBLBF.Code;//this.tran_cd;
                                    if (objSAVETRANSANDMASTER.BL_FIELDS.SaveDetailsOrNOtFromDefault)
                                    {
                                        objAfterValidateSave.ACTIVE_BL = ActiveBLBF;//2.0
                                        if (objAfterValidateSave.ValidateSave())//2.0
                                        {
                                            objFLTransaction.Save_Trasaction(ActiveBLBF);
                                        }
                                        else
                                        {
                                            if (objAfterValidateSave.BL_FIELDS.Errormsg.Length != 0)
                                            {
                                                MessageBox.Show(objAfterValidateSave.BL_FIELDS.Errormsg, "Validation");
                                            }
                                        }
                                    }
                                    if (activeBLBF.Tran_mode_type == "regular")
                                    {
                                        if (this.Tran_mode == "add_mode")
                                        {
                                            ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                        }
                                        if (this.Tran_mode == "add_mode")
                                        {
                                            i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                                            ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                            tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                            ActiveBLBF.Tran_id = tran_id;
                                        }
                                        else
                                        {
                                            row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                                            ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                                            i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            tran_id = activeBLBF.Tran_id;

                                            //i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            //tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                            //ActiveBLBF.Tran_id = tran_id;
                                            //tran_id = activeBLBF.Tran_id;
                                        }
                                    }

                                    this.Tran_mode = "view_mode";
                                    ActiveBLBF.Tran_mode = this.tran_mode;
                                    ActiveBLBF.Tran_mode = this.Tran_mode;
                                    ActiveBLBF.Code = this.Tran_cd;
                                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = ActiveBLBF.Tran_id;
                                    FinalUpdateCall();//3.0
                                    //BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                                    objfrm.SendMessageToClient(activeBLBF, "ADD/EDIT/VIEW");
                                    RefreshToolbar(ActiveBLBF.Code, activeBLBF.Tran_mode, activeBLBF.Tran_mode_type);
                                    AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " Updated Successfully", "Update");
                                }
                                else
                                {
                                    MessageBox.Show(errormsg, "Validation");
                                }
                            }
                            else
                            {
                                if (objSAVETRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    MessageBox.Show(objSAVETRANSANDMASTER.BL_FIELDS.Errormsg, "Validation");
                                }
                            }
                        }
                    }

                    if (activeBLBF.Prnt_saving)
                    {
                        PrintDocument();
                    }
                }
                pictureBox1.Visible = false;
            }
        }
        #region 3.0
        //Final update trigger call
        private void FinalUpdateCall()
        {
            if (objSAVETRANSANDMASTER.BL_FIELDS.Errormsg.Length == 0)
            {
                objfinalUpdate.ACTIVE_BL = activeBLBF;
                objfinalUpdate.FinalUpdate();
            }
        }
        #endregion 3.0
        //cancel button in tool bar is clicked
        public void tsbtnCancel_Click(object sender, EventArgs e)
        {
            enableViewModeButtons();
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    this.Tran_mode = "view_mode";
                    ActiveBLBF.Tran_mode = this.tran_mode;
                    if (ActiveBLBF.Tran_type == "Transaction")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Accounting")
                    {
                        Button_Transaction_Action();
                    }
                    else if (ActiveBLBF.Tran_type == "Master")
                    {
                        Button_Master_Action();
                    }
                    else
                    {
                        Button_CustomMaster_Action();
                    }
                }
            }
        }
        //delete button in tool bar is clicked
        public void tsbtn_delete_Click(object sender, EventArgs e)
        {
            objRoles.ObjMainFields = ObjBLMainFields;
            if (objRoles.CheckPermisson("delete," + activeBLBF.Tran_nm))
            {
                enableViewModeButtons();
                if (this.HasChildren)
                {

                    if (this.ActiveMdiChild != null)
                    {
                        if (ActiveBLBF.Tran_type == "Transaction")
                        {
                            if (activeBLBF.Ac_post)
                            {
                                //Hashtable htparam = new Hashtable();

                                //htparam.Add("@atran_id", activeBLBF.Tran_id);
                                //htparam.Add("@atran_cd", activeBLBF.Code);
                                //htparam.Add("@acompid", activeBLBF.ObjCompany.Compid.ToString());
                                //DataSet dsetAccountAlloc = objdblayer.dsprocedure("ISP_ALLOCATION_LOADING", htparam);
                                DataSet dsetAccountAlloc = objFLGeneral.GetDataSetByQuery("select * from IVW_AC_ALLOC where ref_tran_id='" + activeBLBF.Tran_id + "' and ref_tran_cd='" + activeBLBF.Code + "'");

                                if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
                                {
                                    AutoClosingMessageBox.Show("Sorry! We can't delete because it is refered in the other Accounting transaction", "Delete");
                                    return;
                                }
                            }
                            foreach (DictionaryEntry entry in ActiveBLBF.HTITEM)
                            {
                                if (((Hashtable)entry.Value).Count != 0 && !objFLTransaction.Find_Reference(ActiveBLBF.HTMAIN[activeBLBF.Primary_id.ToString()].ToString(), ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), ActiveBLBF.Code, entry.Key.ToString()))
                                {
                                    AutoClosingMessageBox.Show("Sorry! We can't delete because it is refered in the other transaction", "Delete");
                                    return;
                                }
                            }
                            DialogResult result = MessageBox.Show("Are you sure to delete?", "Delete Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                objDeleteTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                                if (objDeleteTRANSANDMASTER.tsDeleteTransactionAndMaster())
                                {
                                    activeBLBF.HTMAIN = objDeleteTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                                    activeBLBF.HTITEM = objDeleteTRANSANDMASTER.ACTIVE_BL.HTITEM;

                                    if (!objFLTransaction.DELETE_TRANSACTION(ActiveBLBF))
                                    {
                                        AutoClosingMessageBox.Show("Sorry! " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is not successfully", "Delete Failed");
                                    }
                                    else
                                    {
                                        AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is successfully", "Delete");
                                    }
                                    objFLGenInvoice.Delete_Gen_Miss(ActiveBLBF.Code, ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), ActiveBLBF.HTMAIN["TRAN_SR"].ToString(), ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), activeBLBF.ObjCompany.Compid.ToString());
                                    this.Tran_mode = "view_mode";
                                    ActiveBLBF.Tran_mode = this.tran_mode;
                                    objtransaction = this.ActiveMdiChild as ifrm_transaction;
                                    if (objtransaction != null)
                                    {
                                        ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                        i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                        if (ActiveBLBF.dsNavigation != null && i < ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1)
                                        {
                                            //i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            i++;
                                            ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                        }
                                        else if (ActiveBLBF.dsNavigation != null && i > 0)
                                        {
                                            // i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            i--;
                                            ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                        }
                                        if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                                            objtransaction.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                        else
                                            objtransaction.Tran_id = "0";
                                        objtransaction.Tran_mode = this.Tran_mode;
                                        objtransaction.Tran_cd = this.Tran_cd;
                                        ActiveBLBF.Tran_id = objtransaction.Tran_id;
                                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                                        objtransaction.objBASEFILEDS = ActiveBLBF;
                                        objtransaction.DisplayControlsonMode(this.Tran_mode, 0);
                                        RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
                                    }
                                }
                                else
                                {
                                    if (objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        AutoClosingMessageBox.Show(objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg, "Delete");
                                    }
                                }
                            }
                        }
                        else if (ActiveBLBF.Tran_type == "Accounting")
                        {
                            if (activeBLBF.Ac_post)
                            {
                                //Hashtable htparam = new Hashtable();

                                //htparam.Add("@atran_id", activeBLBF.Tran_id);
                                //htparam.Add("@atran_cd", activeBLBF.Code);
                                //htparam.Add("@acompid", activeBLBF.ObjCompany.Compid.ToString());
                                //DataSet dsetAccountAlloc = objdblayer.dsprocedure("ISP_ALLOCATION_LOADING", htparam);
                                DataSet dsetAccountAlloc = objFLGeneral.GetDataSetByQuery("select * from IVW_AC_ALLOC where ref_tran_id='" + activeBLBF.Tran_id + "' and ref_tran_cd='" + activeBLBF.Code + "'");

                                if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
                                {
                                    AutoClosingMessageBox.Show("Sorry! We can't delete because it is refered in the other Accounting transaction", "Delete");
                                    return;
                                }
                            }
                            DialogResult result = MessageBox.Show("Are you sure to delete?", "Delete Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                objDeleteTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                                if (objDeleteTRANSANDMASTER.tsDeleteTransactionAndMaster())
                                {
                                    activeBLBF.HTMAIN = objDeleteTRANSANDMASTER.ACTIVE_BL.HTMAIN;

                                    if (!objFLTransaction.DELETE_TRANSACTION(ActiveBLBF))
                                    {
                                        AutoClosingMessageBox.Show("Sorry! " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is not successfully", "Delete Failed");
                                    }
                                    else
                                    {
                                        AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is successfully", "Delete");
                                    }
                                    objFLGenInvoice.Delete_Gen_Miss(ActiveBLBF.Code, ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), ActiveBLBF.HTMAIN["TRAN_SR"].ToString(), ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), activeBLBF.ObjCompany.Compid.ToString());
                                    this.Tran_mode = "view_mode";
                                    ActiveBLBF.Tran_mode = this.tran_mode;

                                    objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                                    if (objAccounting != null)
                                    {
                                        ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                        i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                        if (ActiveBLBF.dsNavigation != null && i < ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1)
                                        {
                                            //i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            i++;
                                            ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                        }
                                        else if (ActiveBLBF.dsNavigation != null && i > 0)
                                        {
                                            // i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            i--;
                                            ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                        }
                                        if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                                            objAccounting.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                        else
                                            objAccounting.Tran_id = "0";
                                        objAccounting.Tran_mode = this.Tran_mode;
                                        objAccounting.Tran_cd = this.Tran_cd;
                                        ActiveBLBF.Tran_id = objAccounting.Tran_id;
                                        ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                                        objAccounting.objBASEFILEDS = ActiveBLBF;
                                        objAccounting.DisplayControlsonMode(this.Tran_mode, 0);
                                        RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                                    }
                                }
                                else
                                {
                                    if (objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        AutoClosingMessageBox.Show(objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg, "Delete");
                                    }
                                }
                            }
                        }
                        else if (ActiveBLBF.Tran_type == "CustomMaster")
                        {
                            //foreach (DictionaryEntry entry in ActiveBLBF.HTITEM)
                            //{
                            //    if (((Hashtable)entry.Value).Count != 0 && !objFLTransaction.Find_Reference(ActiveBLBF.HTMAIN[activeBLBF.Primary_id.ToString()].ToString(), ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), ActiveBLBF.Code, entry.Key.ToString()))
                            //    {
                            //        AutoClosingMessageBox.Show("Sorry! We can't delete because it is refered in the other transaction","Delete",3000);
                            //        return;
                            //    }
                            //}

                            bool flg = true;
                            if (activeBLBF.Code == "GM")
                            {
                                if (activeBLBF.HTMAIN.Contains("ac_grp_nm"))
                                {
                                    if (activeBLBF.HTMAIN["ac_grp_nm"] != null && activeBLBF.HTMAIN["ac_grp_nm"].ToString() != "")
                                    {
                                        if (activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "ASSET" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "LIABILITIES" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "EXPENSE" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "INCOME" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING EXPENSE" || activeBLBF.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING INCOME")
                                        {
                                            AutoClosingMessageBox.Show("Deleting Parent of COA is not Possible", "COA DELETE");
                                            flg = false;
                                        }
                                    }
                                }
                            }
                            if (flg)
                            {
                                DialogResult result = MessageBox.Show("Are you sure to delete?", "Delete Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                {
                                    objDeleteTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                                    if (objDeleteTRANSANDMASTER.tsDeleteTransactionAndMaster())
                                    {
                                        activeBLBF.HTMAIN = objDeleteTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                                        activeBLBF.HTITEM = objDeleteTRANSANDMASTER.ACTIVE_BL.HTITEM;
                                        if (activeBLBF.Code == "CM" || activeBLBF.Code == "VM" || activeBLBF.Code == "PT" || activeBLBF.Code == "PG" || activeBLBF.Code == "FA" || activeBLBF.Code == "FG" || activeBLBF.Code == "LC")
                                        {
                                            if (!objFLTransaction.Find_Deleting_Fld_Usaged_In_OtherTbl(ActiveBLBF))
                                            {
                                                flg = false;
                                            }
                                        }
                                        if (flg)
                                        {
                                            if (!objFLTransaction.DELETE_TRANSACTION(ActiveBLBF))
                                            {
                                                AutoClosingMessageBox.Show("Sorry! " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is not successfully", "Delete Failed");
                                            }
                                            else
                                            {
                                                AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is successfully", "Delete");
                                            }
                                            //  objFLGenInvoice.Delete_Gen_Miss(ActiveBLBF.Code, ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), ActiveBLBF.HTMAIN["TRAN_SR"].ToString(), ActiveBLBF.HTMAIN["TRAN_NO"].ToString(), activeBLBF.ObjCompany.Compid.ToString());
                                            this.Tran_mode = "view_mode";
                                            ActiveBLBF.Tran_mode = this.tran_mode;

                                            ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                            i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                            if (ActiveBLBF.dsNavigation != null && i < ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1)
                                            {
                                                //i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                i++;
                                                ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                            }
                                            else if (ActiveBLBF.dsNavigation != null && i > 0)
                                            {
                                                //i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                i--;
                                                ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                            }
                                            if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                                                activeBLBF.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                            else
                                                activeBLBF.Tran_id = "0";
                                            activeBLBF.Tran_mode = this.Tran_mode;
                                            ActiveBLBF.Code = this.Tran_cd;
                                            ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = activeBLBF.Tran_id;

                                            BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                                            objfrm.SendMessageToClient(activeBLBF, "DELETE");
                                            RefreshToolbar(ActiveBLBF.Code, activeBLBF.Tran_mode, activeBLBF.Tran_mode_type);
                                        }
                                        else
                                        {
                                            AutoClosingMessageBox.Show("Sorry!! this is refered in Transaction", "Delete Validation");
                                        }
                                    }
                                    else
                                    {
                                        if (objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                        {
                                            AutoClosingMessageBox.Show(objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg, "Delete");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.Tran_cd != "OM")
                            {
                                DialogResult result = MessageBox.Show("Are you sure to delete?", "Delete Master", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                {
                                    objDeleteTRANSANDMASTER.ACTIVE_BL = activeBLBF;
                                    if (objDeleteTRANSANDMASTER.tsDeleteTransactionAndMaster())
                                    {
                                        activeBLBF.HTMAIN = objDeleteTRANSANDMASTER.ACTIVE_BL.HTMAIN;

                                        if (objFLTransaction.Find_Deleting_Fld_Usaged_In_OtherTbl(ActiveBLBF))
                                        {
                                            if (!objFLTransaction.DELETE_MASTER(ActiveBLBF))
                                            {
                                                AutoClosingMessageBox.Show("Sorry! " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is not successfully", "Delete");
                                            }
                                            else
                                            {
                                                AutoClosingMessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(activeBLBF.Tran_nm.ToLower()) + " deletion is successfully", "Delete");
                                            }
                                            this.Tran_mode = "view_mode";
                                            ActiveBLBF.Tran_mode = this.tran_mode;
                                            objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                                            if (objfrm_mast != null)
                                            {
                                                ActiveBLBF.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(ActiveBLBF, "");
                                                i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                if (ActiveBLBF.dsNavigation != null && i < ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1)
                                                {
                                                    //i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                    i++;
                                                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                                }
                                                else if (ActiveBLBF.dsNavigation != null && i > 0)
                                                {
                                                    //i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                                                    i--;
                                                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                                                }
                                                if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                                                    objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                                                else
                                                    objfrm_mast.Tran_id = "0";
                                                objfrm_mast.Tran_mode = this.Tran_mode;
                                                objfrm_mast.Tran_cd = this.Tran_cd;
                                                ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                                                ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                                                objfrm_mast.objBASEFILEDS = ActiveBLBF;
                                                objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                                                RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
                                            }
                                        }
                                        else
                                        {
                                            AutoClosingMessageBox.Show("Sorry!! this is refered in Transaction", "Delete Validation");
                                        }
                                    }
                                    else
                                    {
                                        if (objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                                        {
                                            AutoClosingMessageBox.Show(objDeleteTRANSANDMASTER.BL_FIELDS.Errormsg, "Delete");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AutoClosingMessageBox.Show("Sorry deleting Organisation Not Posible", "Delete Validation");
                            }
                        }
                    }
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Access Denied", "Access Rights");
            }
        }
        public void tsbtnPreview_Click(object sender, EventArgs e)
        {
            if (activeBLBF.Tran_type == "Report")
                enablePrintPreviewModeButtons();
            else
                enableViewModeButtons();

            bool flgDocorReport = false;

            objtransaction = this.ActiveMdiChild as ifrm_transaction;

            if (objtransaction != null)
            {
                flgDocorReport = true;
                frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, true);
                objfrmpp.ShowDialog();
            }
            else
            {
                objAccounting = this.ActiveMdiChild as ifrm_Accounting;

                if (objAccounting != null)
                {
                    flgDocorReport = true;
                    frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, true);
                    objfrmpp.ShowDialog();
                }
                else
                {
                    if (activeBLBF.Tran_type != "Report")
                    {
                        BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                        if (objfrm != null)
                        {
                            flgDocorReport = true;
                            frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, true);
                            objfrmpp.ShowDialog();
                        }
                    }
                    else
                    {
                        objfrmrp = this.ActiveMdiChild as frmReportPreview;
                        flgDocorReport = false;
                        frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, true);
                        objfrmrp.CopyDatatoHashTable();
                        objfrmpp.HTFilter = objfrmrp.HTFilter;
                        objfrmrp.ObjBLFD = activeBLBF;
                        objfrmpp.ShowDialog();
                    }
                }
            }
        }
        private void PreviewOrSendMail()
        {
            bool flgDocorReport = false;

            objtransaction = this.ActiveMdiChild as ifrm_transaction;

            if (objtransaction != null)
            {
                flgDocorReport = true;
                frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, false);
                objfrmpp.ShowDialog();
            }
            else
            {
                objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                if (objAccounting != null)
                {
                    flgDocorReport = true;
                    frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, false);
                    objfrmpp.ShowDialog();
                }
            }
        }

        public void tsbtn_print_Click(object sender, EventArgs e)
        {
            try
            {
                objRoles.ObjMainFields = ObjBLMainFields;
                if (objRoles.CheckPermisson("print," + activeBLBF.Tran_nm))
                {
                    tsbtn_edit.Enabled = true;
                    tsbtn_add.Enabled = true;
                    tsbtn_save.Enabled = false;
                    tsbtn_delete.Enabled = true;
                    tsbtnCancel.Enabled = false;
                    tsbtn_clschild.Enabled = true;
                    tsbtnPreview.Enabled = true;
                    tsbtnSendMail.Enabled = activeBLBF.IsSendMail;
                    tsbtn_print.Enabled = true;
                    tsLocate.Enabled = true;
                    PrintDocument();
                }
                else
                {
                    AutoClosingMessageBox.Show("Access Denied", "Access Rights");
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Printing document is Un-Successfull", "Print Validation");
            }
        }

        private void PrintDocument()
        {

            DataSet dsetRep = objFLRep.Get_Rep_Documents(activeBLBF);
            if (dsetRep != null && dsetRep.Tables[0].Rows.Count != 0)
            {
                ReportDocument doc = new ReportDocument();
                PrintDialog pDialog = new PrintDialog();
                //pDialog.AllowSomePages = true;
                //pDialog.AllowCurrentPage = true;
                //pDialog.AllowSelection = true;
                if (pDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataSet ds = objFLRep.REPORT_TRANSACTION(ActiveBLBF.HTMAIN[activeBLBF.Primary_id.ToString()].ToString(), tran_cd, dsetRep.Tables[0].Rows[0]["sp_nm"].ToString());
                        //doc.Load(objIni.GetKeyFieldValue("APP_PATH", "path") + @"\" + objIni.GetKeyFieldValue("SQL", "initial catalog") + @"\" + dsetRep.Tables[0].Rows[0]["rep_nm"].ToString());
                        doc.Load(AppDomain.CurrentDomain.BaseDirectory + objIni.GetKeyFieldValue("SQL", "initial catalog") + @"\" + dsetRep.Tables[0].Rows[0]["rep_nm"].ToString().Trim() + ".rpt");
                        doc.SetDataSource(ds.Tables[0]);
                    }
                    catch (Exception ex)
                    {
                        AutoClosingMessageBox.Show("Sorry!! Report design missing", "rpt file is Missing");
                    }
                    Type t = typeof(Company);
                    PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    ParameterFieldDefinitions crParameterdef;
                    crParameterdef = doc.DataDefinition.ParameterFields;
                    foreach (PropertyInfo field in publicFieldInfos)
                    {
                        foreach (ParameterFieldDefinition def in crParameterdef)
                        {
                            if (null != field)
                            {
                                if (def.Name.Equals("ORG." + field.Name.ToLower()))    // check if parameter exists in report
                                {
                                    doc.SetParameterValue("ORG." + field.Name.ToLower(), field.GetValue(ObjBLComp, null));    // set the parameter value in the report
                                }
                            }
                        }
                    }

                    doc.PrintOptions.PrinterName = pDialog.PrinterSettings.PrinterName;
                    doc.PrintToPrinter(pDialog.PrinterSettings.Copies, false, pDialog.PrinterSettings.FromPage, pDialog.PrinterSettings.ToPage);
                    AutoClosingMessageBox.Show("Printing document is Successfull", "Print");
                }
            }
            else
            {
                AutoClosingMessageBox.Show("No documents exist", "Print");
            }
        }
        public void tsbtn_clschild_Click(object sender, EventArgs e)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    #region
                    //if (ActiveBLBF.Tran_type == "Transaction")
                    //{
                    //    objtransaction = this.ActiveMdiChild as ifrm_transaction;
                    //    if (objtransaction != null)
                    //    {
                    //        ObjBLMainFields.HASHTOOL.Remove(objtransaction.Tran_cd + ObjBLMainFields.CurUser + objtransaction.objBASEFILEDS.Curr_date_time);                           
                    //    }
                    //}
                    //else if (ActiveBLBF.Tran_type == "Report")
                    //{
                    //    objfrmrp = this.ActiveMdiChild as frmReportPreview;
                    //    if (objfrmrp != null)
                    //    {
                    //        ReportChildWindowActivate(objfrmrp);
                    //    }
                    //}
                    //else
                    //{
                    //    objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                    //    if (objfrm_mast != null)
                    //    {
                    //        ObjBLMainFields.HASHTOOL.Remove(objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + objfrm_mast.objBASEFILEDS.Curr_date_time);                            
                    //    }
                    //}
                    #endregion
                    this.ActiveMdiChild.Close();
                    if (this.ActiveMdiChild != null)
                    {
                        if (ActiveBLBF.Tran_type == "Transaction")
                        {
                            objtransaction = this.ActiveMdiChild as ifrm_transaction;
                            if (objtransaction != null)
                            {
                                ChildWindowActivate(objtransaction);
                            }
                        }
                        else if (ActiveBLBF.Tran_type == "Accounting")
                        {
                            objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                            if (objAccounting != null)
                            {
                                AccountChildWindowActivate(objAccounting);
                            }
                        }
                        else if (ActiveBLBF.Tran_type == "Report")
                        {
                            objfrmrp = this.ActiveMdiChild as frmReportPreview;
                            if (objfrmrp != null)
                            {
                                ReportChildWindowActivate(objfrmrp);
                            }
                        }
                        else if (ActiveBLBF.Tran_type == "Master")
                        {
                            objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                            if (objfrm_mast != null)
                            {
                                MasterChildWindowActivate(objfrm_mast);
                            }
                        }
                        else
                        {
                            CustomChildWindowActivate(activeBLBF);
                        }
                    }
                    else
                    {
                        enableCloseModeButtons();
                    }
                }
                else
                {
                    enableCloseModeButtons();
                }
            }
        }
        public void CloseChildWindow(int flg)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    if (flg == 1)
                    {
                        this.ActiveMdiChild.Close();
                    }
                    else
                    {
                        if (this.Tran_mode != "view_mode")
                        {
                            DialogResult result = MessageBox.Show("Are you sure to cancel?", "Delete Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                            }
                            else
                            {
                                if (ActiveBLBF.Tran_type == "Transaction")
                                {
                                    objtransaction = this.ActiveMdiChild as ifrm_transaction;
                                    if (objtransaction != null)
                                    {
                                        ObjBLMainFields.HASHTOOL.Remove(objtransaction.Tran_cd + ObjBLMainFields.CurUser + objtransaction.objBASEFILEDS.Curr_date_time);
                                    }
                                }
                                else if (ActiveBLBF.Tran_type == "Accounting")
                                {
                                    objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                                    if (objAccounting != null)
                                    {
                                        ObjBLMainFields.HASHTOOL.Remove(objAccounting.Tran_cd + ObjBLMainFields.CurUser + objAccounting.objBASEFILEDS.Curr_date_time);
                                    }
                                }
                                else if (ActiveBLBF.Tran_type == "Report")
                                {
                                    objfrmrp = this.ActiveMdiChild as frmReportPreview;
                                    if (objfrmrp != null)
                                    {
                                        ReportChildWindowActivate(objfrmrp);
                                    }
                                }
                                else if (ActiveBLBF.Tran_type == "Master")
                                {
                                    objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                                    if (objfrm_mast != null)
                                    {
                                        ObjBLMainFields.HASHTOOL.Remove(objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + objfrm_mast.objBASEFILEDS.Curr_date_time);
                                    }
                                }
                                else
                                {
                                    ObjBLMainFields.HASHTOOL.Remove(activeBLBF.Tran_id + ObjBLMainFields.CurUser + objtransaction.objBASEFILEDS.Curr_date_time);
                                }
                            }
                            enableCloseModeButtons();
                        }
                        else
                        {
                            enableCloseModeButtons();
                        }
                    }
                }
            }
        }
        public void CloseCustomChildWindow(int flg, BL_BASEFIELD objBSFD)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    if (flg == 1)
                    {
                        this.ActiveMdiChild.Close();
                    }
                    else
                    {
                        if (objBSFD.Tran_type == "Transaction")
                        {
                            ObjBLMainFields.HASHTOOL.Remove(objBSFD.Tran_id + ObjBLMainFields.CurUser + objBSFD.Curr_date_time);
                        }
                        else
                        {
                            ObjBLMainFields.HASHTOOL.Remove(objBSFD.Tran_id + ObjBLMainFields.CurUser + objBSFD.Curr_date_time);
                        }
                    }
                    enableCloseModeButtons();
                }
                else
                {
                    enableCloseModeButtons();
                }
            }
        }
        public void tsbtnexit_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                AutoClosingMessageBox.Show("Please Close all the child forms", "Close");
            }
            else
            {
                // objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                this.Close();
            }
        }
        public void tsrelogin_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                AutoClosingMessageBox.Show("Please Close all the child forms", "Close");
            }
            else
            {
                this._reLoginBit = true;
                this.Hide();
                // objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                this.Close();
                frmLogin objLogin = new frmLogin(1);
                objLogin.ShowDialog();
            }
        }
        #endregion

        #region TOOL BAR BUTTON VISIBILITY
        private void enableViewModeButtons()
        {
            tsbtn_add.Enabled = true;
            tsbtn_edit.Enabled = true;
            if (ActiveBLBF != null && activeBLBF.Tran_type.ToLower() != "master")// && activeBLBF.Tran_type.ToLower() != "custommaster")
            {
                tsbtnPreview.Enabled = true;
                tsbtn_print.Enabled = true;
                tsbtnSendMail.Enabled = activeBLBF.IsSendMail;
            }
            else
            {
                tsbtnPreview.Enabled = false;
                tsbtn_print.Enabled = false;
                tsbtnSendMail.Enabled = false;
            }
            tsbtn_delete.Enabled = true;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = true;
            tsbtnLast.Enabled = true;
            tsbtnNext.Enabled = true;
            tsbtnPrev.Enabled = true;

            tsbtnCancel.Enabled = false;
            tsbtn_save.Enabled = false;
            tsLocate.Enabled = true;
        }
        public void enableAddModeButtons()
        {
            tsbtn_save.Enabled = true;
            tsbtnCancel.Enabled = true;

            tsbtn_edit.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = false;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = false;
        }
        public void enableOnlyAddModeButtons()
        {
            tsbtn_add.Enabled = true;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_save.Enabled = false;
            tsLocate.Enabled = false;
        }
        public void enableOnlyEditModeButtons()
        {
            tsbtn_add.Enabled = false;
            tsbtn_edit.Enabled = true;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_save.Enabled = false;
            tsLocate.Enabled = false;
        }
        public void enableOnlyApproveModeButtons()
        {
            tsbtn_add.Enabled = false;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_save.Enabled = false;
            tsLocate.Enabled = false;
        }
        private void enableEditModeButtons()
        {
            tsbtn_save.Enabled = true;
            tsbtnCancel.Enabled = true;

            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = false;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = false;
        }
        public void enableCloseModeButtons()
        {
            tsbtn_save.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = false;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = false;
        }
        public void enableLocateModeButtons()
        {
            tsbtn_save.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = true;
        }
        private void enableCompanyModeButtons()
        {
            tsbtnCancel.Enabled = false;
            tsbtn_save.Enabled = true;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = false;
            tsbtnSendMail.Enabled = false;
            tsbtn_print.Enabled = false;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = false;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = false;
            tsbtnexit.Enabled = false;
        }
        private void enablePrintPreviewModeButtons()
        {
            tsbtn_save.Enabled = false;
            tsbtnCancel.Enabled = false;
            tsbtn_edit.Enabled = false;
            tsbtnPreview.Enabled = true;
            tsbtnSendMail.Enabled = activeBLBF.IsSendMail;
            tsbtn_print.Enabled = true;
            tsbtn_add.Enabled = false;
            tsbtn_delete.Enabled = false;
            tsbtn_clschild.Enabled = true;
            tsbtnFirst.Enabled = false;
            tsbtnLast.Enabled = false;
            tsbtnNext.Enabled = false;
            tsbtnPrev.Enabled = false;
            tsLocate.Enabled = false;
        }
        #endregion

        #region CHILD WINDOWS ACTIVATION CODE
        public void CustomChildWindowActivate(BL_BASEFIELD objBF)
        {
            ActiveBLBF = objBF;
            if (ActiveBLBF.Tbl_catalog == "")
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
            }
            else
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            }
            this.Tran_cd = objBF.Code;
            this.Tran_mode = objBF.Tran_mode;
            ActiveBLBF.Tran_mode = this.tran_mode;
            RefreshToolbar(objBF.Code, objBF.Tran_mode, activeBLBF.Tran_mode_type);
        }
        public void ChildWindowActivate(ifrm_transaction childObj)
        {
            iInit.ActiveFrm = childObj;
            ActiveBLBF = childObj.objBASEFILEDS;
            if (ActiveBLBF.Tbl_catalog == "")
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
            }
            else
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            }
            this.Tran_cd = childObj.Tran_cd;
            this.Tran_mode = childObj.Tran_mode;
            ActiveBLBF.Tran_mode = this.tran_mode;
            activeBLBF.Tran_type = "Transaction";
            RefreshToolbar(childObj.Tran_cd, childObj.Tran_mode, activeBLBF.Tran_mode_type);
        }
        public void AccountChildWindowActivate(ifrm_Accounting childObj)
        {
            iInit.ActiveFrm = childObj;
            ActiveBLBF = childObj.objBASEFILEDS;
            if (ActiveBLBF.Tbl_catalog == "")
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
            }
            else
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            }
            this.Tran_cd = childObj.Tran_cd;
            this.Tran_mode = childObj.Tran_mode;
            ActiveBLBF.Tran_mode = this.tran_mode;
            activeBLBF.Tran_type = "Accounting";
            RefreshToolbar(childObj.Tran_cd, childObj.Tran_mode, activeBLBF.Tran_mode_type);
        }
        public void MasterChildWindowActivate(frm_mast_item childObj)
        {
            // objInit.ActiveFrm = childObj;
            iInit.ActiveFrm = childObj;
            ActiveBLBF = childObj.objBASEFILEDS;
            if (ActiveBLBF.Tbl_catalog == "")
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
            }
            else
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            }
            this.Tran_cd = childObj.Tran_cd;
            this.Tran_mode = childObj.Tran_mode;
            ActiveBLBF.Tran_mode = this.tran_mode;
            activeBLBF.Tran_type = "Master";
            RefreshToolbar(childObj.Tran_cd, childObj.Tran_mode, activeBLBF.Tran_mode_type);
        }
        public void ReportChildWindowActivate(frmReportPreview childObj)
        {
            // objInit.ActiveFrm = childObj;
            iInit.ActiveFrm = childObj;
            enablePrintPreviewModeButtons();
            ActiveBLBF.Tran_type = "Report";
        }
        public void RefreshToolbar(string tran_code, string tran_mode, string _type)
        {
            // get the accesss rights 
            // change the toolbar button as per mode
            if (_type == "regular")
            {
                switch (tran_mode)
                {
                    case "view_mode":
                        if (activeBLBF.Locate_fin_yr)
                        {
                            enableLocateModeButtons();
                        }
                        else if (ActiveBLBF.dsNavigation.Tables[0].Rows.Count != 0)
                        {
                            enableViewModeButtons();
                        }
                        else
                        {
                            enableOnlyAddModeButtons();
                        }
                        break;
                    case "add_mode":
                        if (login_frm != "login")
                        { enableCompanyModeButtons(); }
                        else
                        {
                            enableAddModeButtons();
                        }
                        break;
                    case "edit_mode":
                        enableEditModeButtons();
                        break;
                }
            }
            else if (_type == "edit_only")
            {
                switch (tran_mode)
                {
                    case "view_mode":
                        enableOnlyEditModeButtons();
                        break;
                    case "edit_mode":
                        enableEditModeButtons();
                        break;
                }
            }
            else if (_type == "add_only")
            {
                switch (tran_mode)
                {
                    case "view_mode":
                        enableOnlyAddModeButtons();
                        break;
                    case "add_mode":
                        enableAddModeButtons();
                        break;
                }
            }
            else if (_type == "approve_only")
            {
                enableOnlyApproveModeButtons();
            }
        }
        #endregion

        #region MENU BAR DETAILS
        public bool BindTransactionSetting(BL_BASEFIELD objBASEFILEDS, string tran_cd)
        {
            DataSet dset = objFLTransaction.GetTrans_Settings(tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
            Type t = typeof(BL_BASEFIELD);
            PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (dset != null && dset.Tables[0].Rows.Count != 0)
            {
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
                                    objBASEFILEDS.GetType().GetProperty(field.Name).SetValue(objBASEFILEDS, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                                }
                            }
                            else
                                objBASEFILEDS.GetType().GetProperty(field.Name).SetValue(objBASEFILEDS, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        private void frm_mainmenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.HasChildren)
            {
                if (this.ActiveMdiChild != null)
                {
                    if (this.Tran_cd != "OM")
                    {
                        AutoClosingMessageBox.Show("Please Close all the child forms", "Close");
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (!_reLoginBit)
                    {
                        DialogResult res = MessageBox.Show("Are You Sure?", "Close Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (res == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            //sharanamma on 05.08.13 reason:user restriction
                            //begin
                            objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                            //end
                        }
                    }
                    else
                    {  //sharanamma on 05.08.13 reason:user restriction
                        //begin
                        objFLGenInvoice.InsertUpdateAndDelete(ObjBLMainFields.CurUser, "2", ObjBLMainFields.Local_ip);
                        //end
                    }
                }
            }
        }

        private void tsLocate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Tran_mode = "view_mode";
                ActiveBLBF.Tran_mode = this.tran_mode;
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    // frmPopup frmpopup = new frmPopup(ActiveBLBF.HTMAIN, ActiveBLBF.Main_tbl_nm, ActiveBLBF.Code, ActiveBLBF.Primary_id + ",TRAN_CD", activeBLBF.Disp_locate, "Locate :", activeBLBF.FormCondition, "1");
                    frmLocate frmpopup = new frmLocate(ActiveBLBF.HTMAIN, ActiveBLBF.Main_tbl_nm, ActiveBLBF.Code, ActiveBLBF.Primary_id + ",TRAN_CD", activeBLBF.Disp_locate, "List :", activeBLBF.FormCondition, "1");
                    frmpopup.ObjBFD = activeBLBF;
                    frmpopup.ShowDialog();
                    objtransaction = this.ActiveMdiChild as ifrm_transaction;
                    if (objtransaction != null)
                    {
                        objtransaction.Tran_mode = this.Tran_mode;
                        objtransaction.Tran_cd = this.Tran_cd;

                        ActiveBLBF.Tran_id = ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString();
                        if (!activeBLBF.Locate_fin_yr)
                        {
                            row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                            ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                            i = int.Parse(ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                        }
                        objtransaction.objBASEFILEDS = ActiveBLBF;
                        objtransaction.DisplayControlsonMode(this.Tran_mode, 0);
                        RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
                    }

                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    frmLocate frmpopup = new frmLocate(ActiveBLBF.HTMAIN, ActiveBLBF.Main_tbl_nm, ActiveBLBF.Code, ActiveBLBF.Primary_id + ",TRAN_CD", activeBLBF.Disp_locate, "List :", activeBLBF.FormCondition, "1");
                    frmpopup.ObjBFD = activeBLBF;
                    frmpopup.ShowDialog();
                    objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                    if (objAccounting != null)
                    {
                        objAccounting.Tran_mode = this.Tran_mode;
                        objAccounting.Tran_cd = this.Tran_cd;

                        ActiveBLBF.Tran_id = ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString();
                        if (!activeBLBF.Locate_fin_yr)
                        {
                            row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                            ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                            i = int.Parse(ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                        }
                        objAccounting.objBASEFILEDS = ActiveBLBF;
                        objAccounting.DisplayControlsonMode(this.Tran_mode, 0);
                        RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                    }
                }
                else if (ActiveBLBF.Tran_type == "Master")
                {
                    frmPopup frmpopup = new frmPopup(ActiveBLBF.HTMAIN, ActiveBLBF.Main_tbl_nm, ActiveBLBF.Code, ActiveBLBF.Primary_id + ",TRAN_CD", activeBLBF.Disp_locate, "Locate :", activeBLBF.FormCondition, false, "", "1");
                    frmpopup.ObjBFD = activeBLBF;
                    frmpopup.ShowDialog();
                    objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                    if (objfrm_mast != null)
                    {
                        objfrm_mast.Tran_mode = this.Tran_mode;
                        objfrm_mast.Tran_cd = this.Tran_cd;
                        ActiveBLBF.Tran_id = ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString();
                        row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                        ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                        i = int.Parse(ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                        objfrm_mast.objBASEFILEDS = ActiveBLBF;
                        objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                        RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
                    }
                }
                else
                {
                    frmPopup frmpopup = new frmPopup(ActiveBLBF.HTMAIN, ActiveBLBF.Main_tbl_nm, ActiveBLBF.Code, ActiveBLBF.Primary_id + ",TRAN_CD", activeBLBF.Disp_locate, "Locate :", activeBLBF.FormCondition, false, "", "1");
                    frmpopup.ObjBFD = activeBLBF;
                    frmpopup.ShowDialog();

                    ActiveBLBF.Tran_mode = this.Tran_mode;
                    ActiveBLBF.Code = this.Tran_cd;
                    ActiveBLBF.Tran_id = ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id].ToString();
                    row = ActiveBLBF.dsNavigation.Tables[0].Select(ActiveBLBF.Primary_id + "='" + ActiveBLBF.Tran_id + "'");
                    ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = ActiveBLBF.dsNavigation.Tables[0].Rows.IndexOf(row[0]).ToString();
                    i = int.Parse(ObjBLMainFields.HASHTOOL[ActiveBLBF.Code + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time].ToString());
                    BaseClass objfrm = this.ActiveMdiChild as BaseClass;
                    objfrm.SendMessageToClient(activeBLBF, "LOC");
                    RefreshToolbar(ActiveBLBF.Code, ActiveBLBF.Tran_mode, activeBLBF.Tran_mode_type);

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Open_Comp_Menu()
        {
            try
            {
                BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
                objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                objBASEFILEDS.Code = "OM";
                objBASEFILEDS.Tran_mode = "view_mode";
                objBASEFILEDS.Tran_type = "Master";
                objBASEFILEDS.Main_tbl_nm = "ORG_MAST";
                objBASEFILEDS.Item_tbl_nm = "";
                objBASEFILEDS.Tbl_catalog = "InodeMFG";
                objBASEFILEDS.Tran_nm = "Organization Master";
                objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                objBASEFILEDS.ObjCompany = objBLComp;
                objBASEFILEDS.ObjControlSet = objControlSetMaster;
                objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();
                objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, "");
                ActiveBLBF = objBASEFILEDS;
                objfrm_mast = new frm_mast_item(objBASEFILEDS);
                objfrm_mast.Tran_cd = "OM";
                if (ActiveBLBF.dsNavigation != null && ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                {
                    i = ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;
                    objfrm_mast.Tran_id = ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ActiveBLBF.dsNavigation.Tables[0].Rows[i][ActiveBLBF.Primary_id].ToString() : "0";
                    ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                }
                else
                {
                    objfrm_mast.Tran_id = "0";
                    ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                    i = 0;
                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                }
                ActiveBLBF.Tran_id = this._Comp_Id;

                objfrm_mast.Tran_mode = "view_mode";
                objfrm_mast.Name = objBASEFILEDS.Tran_nm;
                // objfrm_mast.Text = objBASEFILEDS.Tran_nm;
                objfrm_mast.MdiParent = this;
                if (login_frm != "login")
                {
                    objBASEFILEDS.ObjCompany.Compid = 0;
                    this.tran_cd = "OM";
                    this.tran_mode = "add_mode";
                    enableAddModeButtons();
                    objfrm_mast.Tran_mode = "add_mode";
                    objfrm_mast.ControlBox = false;

                    //if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"License.ini"))
                    //{
                    //    MessageBox.Show("Sorry!! License File Not Found");
                    //    this.Close();
                    //}
                }
                objfrm_mast.Show();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Activate();
            }
        }
        private void onMenuClick(object sender, EventArgs e)
        {
            try
            {
                if (ActiveBLBF.Tran_type == "Transaction")
                {
                    objtransaction = this.ActiveMdiChild as ifrm_transaction;
                    objtransaction.Tran_mode = this.Tran_mode;
                    objtransaction.Tran_cd = this.Tran_cd;
                    objtransaction.Tran_id = "0";
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objtransaction.Tran_id;
                    ActiveBLBF.Tran_id = objtransaction.Tran_id;
                    ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objtransaction.objBASEFILEDS = ActiveBLBF;
                    objtransaction.DisplayControlsonMode(this.Tran_mode, 1);
                    RefreshToolbar(objtransaction.Tran_cd, objtransaction.Tran_mode, activeBLBF.Tran_mode_type);
                }
                else if (ActiveBLBF.Tran_type == "Accounting")
                {
                    objAccounting = this.ActiveMdiChild as ifrm_Accounting;
                    objAccounting.Tran_mode = this.Tran_mode;
                    objAccounting.Tran_cd = this.Tran_cd;
                    objAccounting.Tran_id = "0";
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objAccounting.Tran_id;
                    ActiveBLBF.Tran_id = objAccounting.Tran_id;
                    ObjBLMainFields.HASHTOOL[objAccounting.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objAccounting.objBASEFILEDS = ActiveBLBF;
                    objAccounting.DisplayControlsonMode(this.Tran_mode, 1);
                    RefreshToolbar(objAccounting.Tran_cd, objAccounting.Tran_mode, activeBLBF.Tran_mode_type);
                }
                else
                {
                    objfrm_mast = this.ActiveMdiChild as frm_mast_item;
                    objfrm_mast.Tran_mode = this.Tran_mode;
                    objfrm_mast.Tran_cd = this.Tran_cd;
                    objfrm_mast.Tran_id = "0";
                    ActiveBLBF.Tran_id = objfrm_mast.Tran_id;
                    ActiveBLBF.HTMAIN[ActiveBLBF.Primary_id] = objfrm_mast.Tran_id;
                    ObjBLMainFields.HASHTOOL[objfrm_mast.Tran_cd + ObjBLMainFields.CurUser + activeBLBF.Curr_date_time] = i;
                    objfrm_mast.objBASEFILEDS = ActiveBLBF;
                    objfrm_mast.DisplayControlsonMode(this.Tran_mode);
                    RefreshToolbar(objfrm_mast.Tran_cd, objfrm_mast.Tran_mode, activeBLBF.Tran_mode_type);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void tsbtnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                bool flgDocorReport = false;

                objtransaction = this.ActiveMdiChild as ifrm_transaction;

                if (objtransaction != null)
                {
                    flgDocorReport = true;
                    frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, false);
                    objfrmpp.ShowDialog();
                }
                else
                {
                    objAccounting = this.ActiveMdiChild as ifrm_Accounting;

                    if (objAccounting != null)
                    {
                        flgDocorReport = true;
                        frmPrintPreview objfrmpp = new frmPrintPreview(activeBLBF, flgDocorReport, false);
                        objfrmpp.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string strLine = "";
            //using (StreamReader reader = new StreamReader("ALERTS.txt", true))
            //{
            //    if (reader.Read() >= 0)
            //    {
            //        strLine = reader.ReadToEnd();
            //    }
            //}
            //label1.Text = strLine;
            //if (this.Width == xpos)
            //{
            //    this.label1.Location = new System.Drawing.Point(0, this.Height - (this.Height * 1 / 10));
            //    xpos = 0;

            //}
            //else
            //{
            //    this.label1.Location = new System.Drawing.Point(xpos, this.Height - (this.Height * 1 / 10));
            //    xpos += 2;
            //}
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
