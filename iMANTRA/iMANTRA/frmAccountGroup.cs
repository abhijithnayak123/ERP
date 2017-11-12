using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using iMANTRA_DL;
using iMANTRA_BL;
using iMANTRA_IL;
using CUSTOM_iMANTRA;
using System.Reflection;
using CUSTOM_iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA
{
    public partial class frmAccountGroup : BaseClass
    {
        SqlConnection conn;

        Ini objIni = new Ini();
        DataTable dtCOA;
        DataTable dtAccounts;

        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();


        /********************************************************/
        //controls define
        ImageList _imgLst = new ImageList();

        public string Tran_id
        {
            get { return tran_id; }
            set { tran_id = value; }
        }

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

        public frmAccountGroup(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmAccountGroup_Load(object sender, EventArgs e)
        {
            BindControls();

            this.Width = Screen.PrimaryScreen.WorkingArea.Width * 80 / 100;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height * 85 / 100;
            grpbxSearch.Bounds = new Rectangle(2, ucToolBar1.Height, this.Width * 48 / 100, this.Height * 95 / 100);
            txtSearch.Bounds = new Rectangle(grpbxSearch.Width * 2 / 100, ucToolBar1.Height + 10, grpbxSearch.Width * 96 / 100, grpbxSearch.Height / 10);
            tvGroup.Bounds = new Rectangle(grpbxSearch.Width * 2 / 100, ucToolBar1.Height + txtSearch.Height + 10, grpbxSearch.Width * 96 / 100, grpbxSearch.Height - ucToolBar1.Height - txtSearch.Height - 20);
            int hgt = 0, ctrlhgt = 0, wid = 0;
            hgt = ucToolBar1.Height + 10;
            ctrlhgt = grouper1.Height * 10 / 100;
            wid = grouper1.Width;

            grouper1.Bounds = new Rectangle(this.Width * 50 / 100, ucToolBar1.Height, this.Width * 48 / 100, this.Height * 95 / 100);

            lblGroup.Bounds = new Rectangle(wid * 2 / 100, hgt, wid * 30 / 100, ctrlhgt);
            lblGroupMan.Bounds = new Rectangle(lblGroup.Width + (wid * 2 / 100), hgt, wid * 3 / 100, ctrlhgt);
            txtGroup.Bounds = new Rectangle(lblGroupMan.Width + (wid * 30 / 100), hgt, wid * 60 / 100, ctrlhgt);
            btnGroup.Bounds = new Rectangle(wid * 95 / 100, hgt, wid * 7 / 100, ctrlhgt * 60 / 100);

            hgt += ctrlhgt;
            lblParent.Bounds = new Rectangle(wid * 2 / 100, hgt, wid * 30 / 100, ctrlhgt);
            lblParentMan.Bounds = new Rectangle(lblParent.Width + (wid * 2 / 100), hgt, wid * 3 / 100, ctrlhgt);
            txtParent.Bounds = new Rectangle(lblParentMan.Width + (wid * 30 / 100), hgt, wid * 60 / 100, ctrlhgt);
            btnParent.Bounds = new Rectangle(wid * 95 / 100, hgt, wid * 7 / 100, ctrlhgt * 60 / 100);

            hgt += ctrlhgt;
            lblLedger.Bounds = new Rectangle(wid * 2 / 100, hgt, wid * 30 / 100, ctrlhgt);
            cmbLedger.Bounds = new Rectangle(wid * 33 / 100, hgt, wid * 65 / 100, ctrlhgt);

            hgt += ctrlhgt;
            lblCreditDays.Bounds = new Rectangle(wid * 2 / 100, hgt, wid * 30 / 100, ctrlhgt);
            txtCreditDays.Bounds = new Rectangle(wid * 33 / 100, hgt, wid * 20 / 100, ctrlhgt);

            hgt += ctrlhgt;
            lblCreditLimit.Bounds = new Rectangle(wid * 2 / 100, hgt, wid * 30 / 100, ctrlhgt);
            txtCreditLimit.Bounds = new Rectangle(wid * 33 / 100, hgt, wid * 20 / 100, ctrlhgt);
            chkOverLimit.Bounds = new Rectangle(wid * 55 / 100, hgt, wid * 10 / 100, ctrlhgt);

            hgt += ctrlhgt;
            OTHER_DET.Bounds = new Rectangle(wid * 33 / 100, hgt, wid * 60 / 100, ctrlhgt);


            _imgLst.Images.Clear();
            _imgLst.Images.Add(Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\parent_group.png"));
            _imgLst.Images.Add(Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\sub_group.png"));
            _imgLst.Images.Add(Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\child_group.png"));
            tvGroup.ImageList = _imgLst;

            GetAdditionalFieldsDetails();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            AddTextBoxEvent();
        }

        private void Load_TreeView()
        {
            tvGroup.Nodes.Clear();
            dtCOA = new DataTable();
            conn = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
            String Sequel = "select AC_GRP_ID,AC_GRP_NM,PARENT_ID,PARENT_NM from CM_GROUP";
            SqlDataAdapter da = new SqlDataAdapter(Sequel, conn);
            conn.Open();
            da.Fill(dtCOA);

            dtAccounts = new DataTable();
            String strAccounts = "select ac_nm,str(ac_id)+','+tran_cd ac_id,ac_grp_id,ac_grp_nm from cm_mast union all select ledger_nm,str(ledger_id)+','+tran_cd ledger_id,ac_grp_id,ac_grp_nm from ledger_mast";
            SqlDataAdapter da1 = new SqlDataAdapter(strAccounts, conn);
            da1.Fill(dtAccounts);

            int index = 0;
            if (dtCOA != null && dtCOA.Rows.Count != 0)
            {
                DataRow[] rows = dtCOA.Select("PARENT_NM=''");
                foreach (DataRow dr in rows)
                {
                    PopulateTreeView(Convert.ToInt32(dr["AC_GRP_ID"].ToString()), tvGroup.Nodes.Add(dr["AC_GRP_ID"].ToString(), dr["AC_GRP_NM"].ToString(), 0));
                    index = index + 1;
                }
            }
            //tvGroup.ExpandAll();
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            if (dtCOA != null && dtCOA.Rows.Count != 0)
            {
                DataRow[] rows = dtCOA.Select("PARENT_ID=" + parentId);
                TreeNode childNode;
                foreach (DataRow dr in rows)
                {
                    if (parentNode == null)
                    {
                        childNode = tvGroup.Nodes.Add(dr["AC_GRP_ID"].ToString(), dr["AC_GRP_NM"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 1;
                        childNode.Tag = "parent";
                    }
                    else
                    {
                        childNode = parentNode.Nodes.Add(dr["AC_GRP_ID"].ToString(), dr["AC_GRP_NM"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 1;
                        childNode.Tag = "parent";
                    }
                    PopulateLeafTreeView(Convert.ToInt32(dr["AC_GRP_ID"].ToString()), childNode);
                    PopulateTreeView(Convert.ToInt32(dr["AC_GRP_ID"].ToString()), childNode);
                }
            }
        }

        private void PopulateLeafTreeView(int parentId, TreeNode parentNode)
        {
            if (dtAccounts != null && dtAccounts.Rows.Count != 0)
            {
                DataRow[] rows = dtAccounts.Select("ac_grp_id=" + parentId);
                TreeNode childNode;
                foreach (DataRow dr in rows)
                {
                    if (parentNode == null)
                    {
                        childNode = tvGroup.Nodes.Add(dr["AC_ID"].ToString(), dr["AC_NM"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 2;
                        childNode.Tag = "child";
                    }
                    else
                    {
                        childNode = parentNode.Nodes.Add(dr["AC_ID"].ToString(), dr["AC_NM"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 2;
                        childNode.Tag = "child";
                    }
                    PopulateLeafTreeView(Convert.ToInt32(dr["AC_ID"].ToString().Split(',')[0]), childNode);
                }
            }
        }

        private void AddTextBoxEvent()
        {
            foreach (Control con1 in this.Controls)
            {
                foreach (Control c in con1.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Enter -= new EventHandler(txtenter);
                        ((TextBox)c).Enter += new EventHandler(txtenter);
                        ((TextBox)c).Leave -= new EventHandler(txtleave);
                        ((TextBox)c).Leave += new EventHandler(txtleave);
                    }
                }
            }
        }
        private void txtenter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectionStart = txt.Text.Length;
        }
        private void txtleave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.Trim();
            txt.SelectionStart = 0;
        }

        private void GetAdditionalFieldsDetails()
        {
            objBASEFILEDS.dsBASEADDIFIELD = new DataSet();
            objBASEFILEDS.dsBASEADDIFIELD = objFLBaseField.GETCUSTOMFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
            foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Rows)
            {
                if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "0.00";
                }
                if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                }
                else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                }
                else
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                }
            }
        }
        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + "and tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
            {
                if (!objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                {
                    if (column.DataType.Name.ToString().ToLower() == "int32")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "boolean")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = false;
                    }
                    if (column.DataType.Name.ToString().ToLower() == "string")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "decimal")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0.00";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "datetime")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = DateTime.Parse("1900/01/01");
                    }
                }
            }

            foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
            {
                foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                {
                    if (objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString();
                    }
                }
            }
        }
        private void InsertFieldValueToHashTable()
        {
            objBASEFILEDS.HTMAIN["ac_grp_id"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["ac_grp_nm"] = txtGroup.Text;
            objBASEFILEDS.HTMAIN["parent_id"] = objBASEFILEDS.HTMAIN["parent_id"].ToString();
            objBASEFILEDS.HTMAIN["parent_nm"] = txtParent.Text;
            objBASEFILEDS.HTMAIN["cr_days"] = txtCreditDays.Text;
            objBASEFILEDS.HTMAIN["posting"] = cmbLedger.SelectedValue;
            objBASEFILEDS.HTMAIN["cramt"] = txtCreditLimit.Text;
            objBASEFILEDS.HTMAIN["crallow"] = chkOverLimit.Checked;
            // objBASEFILEDS.HTMAIN["st_type"] = cmbTaxType.SelectedValue;
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
        }
        private void GetFieldValueFromHashTable()
        {
            txtGroup.Text = objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString();
            txtParent.Text = objBASEFILEDS.HTMAIN["parent_nm"].ToString();
            txtCreditDays.Text = objBASEFILEDS.HTMAIN["cr_days"].ToString();
            cmbLedger.Text = objBASEFILEDS.HTMAIN["posting"].ToString();
            txtCreditLimit.Text = objBASEFILEDS.HTMAIN["cramt"].ToString();
            chkOverLimit.Checked = objBASEFILEDS.HTMAIN["crallow"] != null && objBASEFILEDS.HTMAIN["crallow"].ToString() != "" ? bool.Parse(objBASEFILEDS.HTMAIN["crallow"].ToString()) : false;
            //cmbTaxType.Text = objBASEFILEDS.HTMAIN["st_type"].ToString();
        }

        public override bool SendMessageToClient(BL_BASEFIELD objBLFD, string msg)
        {
            objBASEFILEDS = objBLFD;
            if (msg != "SAVE")
            {
                DisplayControlsonMode(objBLFD.Tran_mode);
            }
            else
            {
                return Click_Save();
            }
            return true;
        }
        private bool Click_Save()
        {
            bool flg = true;
            InsertFieldValueToHashTable();
            if (txtGroup.Text == "") { AutoClosingMessageBox.Show("Please enter valid COA Group", "Validation", 3000); flg = false; }
            else if (txtParent.Text == "") { AutoClosingMessageBox.Show("Please enter Parent Group", "Validation", 3000); flg = false; }
            else if (cmbLedger.SelectedIndex == 0) { AutoClosingMessageBox.Show("Please enter Ledger Posting", "Validation", 3000); flg = false; }
            else if (txtCreditDays.Text == "") { AutoClosingMessageBox.Show("Please enter Credit Days", "Validation", 3000); flg = false; }
            else if (txtCreditLimit.Text == "") { AutoClosingMessageBox.Show("Please enter Credit Limit", "Validation", 3000); flg = false; }
            return flg;
        }
        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                switch (tran_mode)
                {
                    case "add_mode":
                        foreach (Control con1 in this.Controls)
                        {
                            foreach (Control c in con1.Controls)
                            {
                                if (c is CheckBox)
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                                else if (c is ComboBox)
                                {
                                    if (((ComboBox)c).SelectedIndex != -1)
                                    {
                                        ((ComboBox)c).SelectedIndex = 0;
                                    }
                                }
                                else if (c is RadioButton)
                                {
                                }
                                else if (c is TextBox)
                                {
                                    ((TextBox)c).Text = "";
                                }
                                else if (c is UserDT)
                                {
                                    //((UserDT)c).DtValue = DateTime.Parse("01/01/1900");
                                    ((UserDT)c).bUpdateFlag = false;
                                    ((UserDT)c).DtValue = DateTime.Now;
                                }
                                c.Enabled = true;
                            }
                        }
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        txtSearch.Text = "";
                        txtGroup.Focus();
                        txtSearch.Enabled = false;
                        tvGroup.Enabled = false;
                        btnGroup.Enabled = false;
                        txtCreditDays.Text = "0";
                        txtCreditLimit.Text = "0.00";
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        //if (objBASEFILEDS.HTMAIN.Contains("ac_grp_nm"))
                        //{
                        //    if (objBASEFILEDS.HTMAIN["ac_grp_nm"] != null && objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString() != "")
                        //    {
                        //        if (objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "ASSET" || objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "LIABILITIES" || objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "EXPENSE" || objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "INCOME" || objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING EXPENSE" || objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString().ToUpper() == "TRADING INCOME")
                        //        {
                        //            AutoClosingMessageBox.Show("Editing Parent of COA is not Possible", "COA Edit");
                        //        }
                        //        else
                        //        {
                        foreach (Control con1 in this.Controls)
                        {
                            if (con1 is UCToolBar)
                            {
                            }
                            else
                            {
                                foreach (Control c in con1.Controls)
                                {
                                    if (c is UserDT)
                                    {
                                        if (((UserDT)c).DtValue.ToString() != "1900-01-01 12:00:00 AM" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900/00/01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000/00/01")
                                        {
                                            ((UserDT)c).bUpdateFlag = true;
                                        }
                                        else
                                        {
                                            ((UserDT)c).bUpdateFlag = false;
                                            ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                        }
                                    }
                                    c.Enabled = true;
                                }
                            }
                        }
                        GetFieldValueFromHashTable();
                        txtSearch.Text = "";
                        txtParent.Focus();
                        txtSearch.Enabled = false;
                        tvGroup.Enabled = false;
                        txtGroup.Enabled = false;
                        btnGroup.Enabled = false;
                        //        }
                        //    }
                        //}
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        foreach (Control con1 in this.Controls)
                        {
                            if (con1 is UCToolBar)
                            {
                            }
                            else
                            {
                                foreach (Control c in con1.Controls)
                                {
                                    if (!(c is PopupButton))
                                    {
                                        if (c is UserDT)
                                        {
                                            ((UserDT)c).bUpdateFlag = true;
                                        }
                                        if (!(c is Label)) c.Enabled = false;
                                    }
                                }
                            }
                        }
                        GetFieldValueFromHashTable();
                        Load_TreeView();
                        OTHER_DET.Enabled = true;
                        txtSearch.Text = "";
                        txtSearch.Focus();
                        txtSearch.Enabled = true;
                        tvGroup.Enabled = true;
                        btnGroup.Enabled = true;
                        btnParent.Enabled = false;
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void BindControls()
        {
            String[] myArray = { "", "ENTRY BY ENTRY", "YEARLY", "MONTHLY", "DAILY", "SALES/PURCHASE" };
            cmbLedger.DataSource = myArray.ToArray();

            //String[] myArray1 = { "", "LOCAL", "OUT OF STATE", "OUT OF COUNTRY" };
            //cmbTaxType.DataSource = myArray1.ToArray();
        }

        private void frmAccountGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void frmAccountGroup_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void OTHER_DET_Click(object sender, EventArgs e)
        {
            frmAddl_Info frmadd = new frmAddl_Info(objBASEFILEDS.HTMAIN, 0, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, OTHER_DET.Name, OTHER_DET.Text);
            frmadd.dset = objBASEFILEDS.dsBASEADDIFIELD;
            frmadd.ObjBSFD = objBASEFILEDS;
            frmadd.ShowDialog();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, objBASEFILEDS.Main_tbl_nm, "", "ac_grp_id,ac_grp_nm", "ac_grp_nm;Parent,parent_nm;Group", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtGroup.Text = objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString();
        }

        private void btnParent_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, objBASEFILEDS.Main_tbl_nm, "", "ac_grp_id;parent_id,ac_grp_nm;parent_nm", "ac_grp_nm;Parent", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtParent.Text = objBASEFILEDS.HTMAIN["parent_nm"].ToString();
        }

        private void tvGroup_Click(object sender, EventArgs e)
        {

        }

        private void tvGroup_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null || e.Node.Tag.ToString() == "parent")
            {
                objBASEFILEDS.Tran_id = e.Node.Name;
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
            }
            //Load_TreeView();
        }

        private void ClearBackColor()
        {
            TreeNodeCollection nodes = tvGroup.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }

        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // ClearBackColor();
            FindByText();

        }
        private void FindByText()
        {
            TreeNodeCollection nodes = tvGroup.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }

        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (txtSearch.Text == "")
                {
                    tn.Parent.Collapse();
                }
                else
                {
                    tn.Parent.Expand();
                    // if the text properties match, color the item           
                    if (tn.Text.ToLower().StartsWith(txtSearch.Text.ToLower()))
                    {
                        tn.BackColor = txtSearch.Text != "" ? Color.Yellow : Color.White;
                        // tn.Parent.ExpandAll();
                        tn.Parent.Expand();
                        //tn.Expand();
                    }
                    else
                    {
                        tn.BackColor = Color.White;
                        //tn.Parent.Collapse();
                    }
                    FindRecursive(tn);
                }
            }


        }

        private void txtGroup_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtGroup.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Group", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Main_tbl_nm + " where ac_grp_nm ='" + txtGroup.Text.Replace("'", "''") + "' and ac_grp_id !='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("COA Group already exists", "Validation", 3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }

        private void txtParent_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtParent.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Parent Group", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Main_tbl_nm + " where ac_grp_nm ='" + txtParent.Text.Replace("'", "''") + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (!(ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0))
                    {
                        AutoClosingMessageBox.Show("COA Parent Group is not valid", "Validation", 3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }

        private void cmbLedger_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbLedger.SelectedIndex == 0)
                {
                    AutoClosingMessageBox.Show("Please select valid Ledger", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtParent_KeyDown(object sender, KeyEventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, objBASEFILEDS.Main_tbl_nm, "", "ac_grp_id;parent_id,ac_grp_nm;parent_nm", "ac_grp_nm;Parent", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtParent.Text = objBASEFILEDS.HTMAIN["parent_nm"].ToString();
        }

        private void txtCreditDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                {
                    e.Handled = true;
                }
                string[] str = txt.Text.Split('.');

                if (e.KeyChar == '.' && str.Length > 1)
                {
                    if (str[1] == "")
                        txt.Text = str[0] + ".00";
                    else
                        txt.Text = str[0] + "." + str[1];
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void tvGroup_DragLeave(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure to drag it here", "DRAG", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {

            }
        }

        private void tvGroup_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //DoDragDrop(e.Item, DragDropEffects.Move);

            // Move the dragged node when the left mouse button is used. ----30-09-14///
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            // Copy the dragged node when the right mouse button is used. 
            //else if (e.Button == MouseButtons.Right)
            //{
            //    DoDragDrop(e.Item, DragDropEffects.Copy);
            //}
        }

        private void tvGroup_DragEnter(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Move;

            e.Effect = e.AllowedEffect;

            //// added by kavita wile R& D on drag drop
            //if (e.Data.GetDataPresent(DataFormats.Text))
            //    e.Effect = DragDropEffects.Copy;
            //else
            //    e.Effect = DragDropEffects.None;
        }

        private void tvGroup_DragDrop(object sender, DragEventArgs e)
        {
            //TreeNode NewNode;

            //if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            //{
            //    Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            //    TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
            //    NewNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //    if (DestinationNode.TreeView != NewNode.TreeView)
            //    {
            //        DestinationNode.Nodes.Add((TreeNode)NewNode.Clone());
            //        DestinationNode.Expand();
            //        //Remove Original Node
            //        NewNode.Remove();
            //    }
            //}

            DialogResult res = MessageBox.Show("Are you sure to drag it here", "DRAG", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                // Retrieve the client coordinates of the drop location.
                Point targetPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));

                // Retrieve the node at the drop location.
                TreeNode targetNode = ((TreeView)sender).GetNodeAt(targetPoint);


                // Retrieve the node that was dragged.
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                // Confirm that the node at the drop location is not  
                // the dragged node or a descendant of the dragged node. 
                if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
                {
                    // If it is a move operation, remove the node from its current  
                    // location and add it to the node at the drop location. 
                    if (e.Effect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                        UpdateParentID(targetNode);

                    }

                    // If it is a copy operation, clone the dragged node  
                    // and add it to the node at the drop location. 
                    //else if (e.Effect == DragDropEffects.Copy)
                    //{
                    //    targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                    //}

                    // Expand the node at the location  
                    // to show the dropped node.
                    targetNode.Expand();
                }

                objDBAdaper.execUpdateQuery("update " + objBASEFILEDS.Main_tbl_nm + " set parent_id='" + targetNode.Name + "' , parent_nm='" + targetNode.Text + "' where ac_grp_id='" + draggedNode.Name + "' and ac_grp_nm='" + draggedNode.Text + "'");
                AutoClosingMessageBox.Show(draggedNode.Text + " dragged Successfully", "Message");
                Load_TreeView();
            }

        }
        private void UpdateParentID(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                string st = tn.Parent.ToString();
            }
        }
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node. 
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node,  
            // call the ContainsNode method recursively using the parent of  
            // the second node. 
            return ContainsNode(node1, node2.Parent);
        }

        private void tvGroup_DragOver(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse position.

            Point targetPoint = tvGroup.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            tvGroup.SelectedNode = tvGroup.GetNodeAt(targetPoint);
        }

        // R& D on drag & drop option - 27-09-14ss

        //private void tvGroup_MouseDown(object sender, MouseEventArgs e)
        //{

        //    tvGroup.DoDragDrop(tvGroup.Text, DragDropEffects.Copy | DragDropEffects.Move);

        //}
    }
}
