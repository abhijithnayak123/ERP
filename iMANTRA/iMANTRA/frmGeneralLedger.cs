using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using iMANTRA_DL;
using iMANTRA_IL;
using iMANTRA_iniL;
using CUSTOM_iMANTRA;
using CUSTOM_iMANTRA_BL;
using System.Reflection;

namespace iMANTRA
{
    public partial class frmGeneralLedger : BaseClass
    {
        SqlConnection conn;
        Ini objIni = new Ini();
        DataTable dtCOA;
        DataTable dtLedger;

        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

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

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        public frmGeneralLedger(BL_BASEFIELD objBF)
        {
            InitializeComponent();
            this.Tran_cd = objBF.TRAN_CD;
            objBASEFILEDS = objBF;
            objBASEFILEDS.HTMAIN["tran_cd"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBF.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBF.Primary_id.ToString()] = this.tran_id.ToString();
            }
        }

        private void frmGeneralLedger_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void frmGeneralLedger_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);
            }
        }

        private void frmGeneralLedger_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                {
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
                }
            }
        }

        private void frmGeneralLedger_Load(object sender, EventArgs e)
        {
            BindControls();
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

        private void chkProd_active_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProd_active.Checked)
            {
                if (dtp_deactive_from.DtValue.ToString() == "1900-01-01 12:00:00 AM" || dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") == "2000-00-01" || dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") == "1900-00-01" || dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") == "1900/00/01" || dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") == "2000/00/01")
                {
                    dtp_deactive_from.bUpdateFlag = true;
                    dtp_deactive_from.DtValue = DateTime.Now;
                    dtp_deactive_from.Enabled = true;
                }
            }
            else
            {
                dtp_deactive_from.DtValue = DateTime.Parse("01/01/1900");
                dtp_deactive_from.Enabled = false;
                if (dtp_deactive_from.DtValue.ToString() != "1900-01-01 12:00:00 AM" && dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") != "2000-00-01" && dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") != "1900-00-01" && dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") != "1900/00/01" && dtp_deactive_from.DtValue.ToString("yyyy/mm/dd") != "2000/00/01")
                {
                    dtp_deactive_from.bUpdateFlag = true;
                }
                else
                {
                    dtp_deactive_from.bUpdateFlag = false;
                    dtp_deactive_from.DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                }
            }
        }

        private void OTHER_DET_Click(object sender, EventArgs e)
        {
            frmAddl_Info frmadd = new frmAddl_Info(objBASEFILEDS.HTMAIN, 0, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, OTHER_DET.Name, OTHER_DET.Text);
            frmadd.dset = objBASEFILEDS.dsBASEADDIFIELD;
            frmadd.ObjBSFD = objBASEFILEDS;
            frmadd.ShowDialog();
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

        private void BindControls()
        {
            DataSet dsest = new DataSet();
            DataSet dset = new DataSet();
            string code = objBASEFILEDS.Code;

            dsest = objDBAdaper.dsquery("SELECT DISTINCT * FROM POSTING");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            cmbLedPost.DataSource = dsest.Tables[0];
            cmbLedPost.DisplayMember = "ledger_post";
            cmbLedPost.ValueMember = "ledger_post";
            cmbLedPost.Update();


            String[] myArray = { "", "BANK", "CASH" };
            cmbLedgerTyp.DataSource = myArray.ToArray();
            //cmbLedgerTyp.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);
        }

        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

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
            objBASEFILEDS.HTMAIN["ledger_id"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["ledger_nm"] = txtLedgerNm.Text;
            objBASEFILEDS.HTMAIN["ac_grp_nm"] = txtGroup.Text;
            objBASEFILEDS.HTMAIN["contact"] = txtContactPerson.Text;
            objBASEFILEDS.HTMAIN["add1"] = txtAdd1.Text;
            objBASEFILEDS.HTMAIN["add2"] = txtAdd2.Text;
            objBASEFILEDS.HTMAIN["add3"] = txtAdd3.Text;
            objBASEFILEDS.HTMAIN["city_nm"] = txtCity.Text;
            objBASEFILEDS.HTMAIN["zip"] = txtZip.Text;
            objBASEFILEDS.HTMAIN["phone"] = txtContactNo.Text;
            objBASEFILEDS.HTMAIN["fax"] = txtFax.Text;
            objBASEFILEDS.HTMAIN["email"] = txtEmail.Text;
            objBASEFILEDS.HTMAIN["state_nm"] = txtState.Text;
            objBASEFILEDS.HTMAIN["country_nm"] = txtCountry.Text;
            objBASEFILEDS.HTMAIN["designation"] = txtDesignation.Text;
            objBASEFILEDS.HTMAIN["mobile"] = txtMobile.Text;
            objBASEFILEDS.HTMAIN["office"] = txtOffTel.Text;
            objBASEFILEDS.HTMAIN["ledger_post"] = cmbLedPost.SelectedValue;
            objBASEFILEDS.HTMAIN["legder_type"] = cmbLedgerTyp.SelectedValue;
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            objBASEFILEDS.HTMAIN["isdeactive"] = chkProd_active.Checked;
            objBASEFILEDS.HTMAIN["deactfrm"] = dtp_deactive_from.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
        }

        private void GetFieldValueFromHashTable()
        {
            txtLedgerNm.Text = objBASEFILEDS.HTMAIN["ledger_nm"].ToString();
            txtGroup.Text = objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString();
            txtContactPerson.Text = objBASEFILEDS.HTMAIN["contact"].ToString();
            txtAdd1.Text = objBASEFILEDS.HTMAIN["add1"].ToString();
            txtAdd2.Text = objBASEFILEDS.HTMAIN["add2"].ToString();
            txtAdd3.Text = objBASEFILEDS.HTMAIN["add3"].ToString();
            txtCity.Text = objBASEFILEDS.HTMAIN["city_nm"].ToString();
            txtZip.Text = objBASEFILEDS.HTMAIN["zip"].ToString();
            txtContactNo.Text = objBASEFILEDS.HTMAIN["phone"].ToString();
            txtFax.Text = objBASEFILEDS.HTMAIN["fax"].ToString();
            txtEmail.Text = objBASEFILEDS.HTMAIN["email"].ToString();
            txtState.Text = objBASEFILEDS.HTMAIN["state_nm"].ToString();
            txtCountry.Text = objBASEFILEDS.HTMAIN["country_nm"].ToString();
            txtDesignation.Text = objBASEFILEDS.HTMAIN["designation"].ToString();
            txtMobile.Text = objBASEFILEDS.HTMAIN["mobile"].ToString();
            txtOffTel.Text = objBASEFILEDS.HTMAIN["office"].ToString();
            cmbLedgerTyp.Text = objBASEFILEDS.HTMAIN["legder_type"].ToString();
            cmbLedPost.Text = objBASEFILEDS.HTMAIN["ledger_post"].ToString();
            chkProd_active.Checked = objBASEFILEDS.HTMAIN["isdeactive"] != null && objBASEFILEDS.HTMAIN["isdeactive"].ToString() != "" ? bool.Parse(objBASEFILEDS.HTMAIN["isdeactive"].ToString()) : false;
            dtp_deactive_from.DtValue = objBASEFILEDS.HTMAIN["deactfrm"] != null && objBASEFILEDS.HTMAIN["deactfrm"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["deactfrm"].ToString()) : DateTime.Parse("01/01/1900");
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
            if (txtLedgerNm.Text == "") { AutoClosingMessageBox.Show("Please enter Ledger Name.", "Validation", 3000); flg = false; }
            //  else if (cmProd_Type.Text == "") { AutoClosingMessageBox.Show("Please Select Valid Product Type","Validation",3000); flg = false; }
            //   else if (cmbUOM.Text == "") { AutoClosingMessageBox.Show("Please select valid Machine UOM","Validation",3000); flg = false; }
            //  else if (cmbStockable.Text == null) { AutoClosingMessageBox.Show("Please select valid Stockable","Validation",3000); flg = false; }
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
                        txtSearch.Enabled = false;
                        tvGroup.Enabled = false;
                        txtLedgerNm.Focus();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        BindControls();
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
                        txtSearch.Enabled = false;
                        tvGroup.Enabled = false;
                        txtLedgerNm.Focus();
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
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void btnCountry_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "COUNTRY", "", "country_id,country_nm", "country_nm;Country", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtCountry.Text = objBASEFILEDS.HTMAIN["country_nm"].ToString();
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "[STATE]", "", "state_id,state_nm", "state_nm;State", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtState.Text = objBASEFILEDS.HTMAIN["state_nm"].ToString();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "ac_grp_id,ac_grp_nm", "ac_grp_nm;Account Group", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtGroup.Text = objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString();
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "ac_grp_id,ac_grp_nm", "ac_grp_nm;Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtGroup.Text = objBASEFILEDS.HTMAIN["ac_grp_nm"].ToString();
            }
        }

        private void txtGroup_Enter(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            { lblInfo.Visible = true; }
        }

        private void txtGroup_Leave(object sender, EventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            { lblInfo.Visible = false; }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
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
                // if the text properties match, color the item           
                if (tn.Text.ToLower().StartsWith(txtSearch.Text.ToLower()))
                {
                    tn.BackColor = txtSearch.Text != "" ? Color.Yellow : Color.White;
                }
                else
                {
                    tn.BackColor = Color.White;
                }
                FindRecursive(tn);
            }
        }

        private void Load_TreeView()
        {
            tvGroup.Nodes.Clear();
            dtCOA = new DataTable();
            conn = new SqlConnection(objIni.GetSectionDetails("SQL", ";"));
            String Sequel = "select AC_GRP_ID,AC_GRP_NM,PARENT_ID,PARENT_NM from CM_GROUP where AC_GRP_NM not in ('ASSET','LIABILITIES')";
            SqlDataAdapter da = new SqlDataAdapter(Sequel, conn);
            conn.Open();
            da.Fill(dtCOA);

            dtLedger = new DataTable();
            String strLedger = "select ledger_id,ledger_nm,ac_grp_id,ac_grp_nm from ledger_mast";
            SqlDataAdapter da1 = new SqlDataAdapter(strLedger, conn);
            da1.Fill(dtLedger);

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
            // tvGroup.ExpandAll();
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            if (dtCOA != null && dtCOA.Rows.Count != 0)
            {
                DataRow[] rows = dtCOA.Select("PARENT_ID=" + parentId);
                TreeNode childNode;
                if (rows != null && rows.Length != 0)
                {
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
                //else
                //{
                //    PopulateLeafTreeView(parentId, parentNode);
                //}
            }
           
        }

        private void PopulateLeafTreeView(int parentId, TreeNode parentNode)
        {
            if (dtLedger != null && dtLedger.Rows.Count != 0)
            {
                DataRow[] rows = dtLedger.Select("ac_grp_id=" + parentId);
                TreeNode childNode;
                foreach (DataRow dr in rows)
                {
                    if (parentNode == null)
                    {
                        childNode = tvGroup.Nodes.Add(dr["ledger_id"].ToString(), dr["ledger_nm"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 2;
                        childNode.Tag = "child";
                    }
                    else
                    {
                        childNode = parentNode.Nodes.Add(dr["ledger_id"].ToString(), dr["ledger_nm"].ToString());
                        childNode.ImageIndex = childNode.SelectedImageIndex = 2;
                        childNode.Tag = "child";
                    }
                    PopulateLeafTreeView(Convert.ToInt32(dr["ledger_id"].ToString()), childNode);
                }
            }
        }

        private void tvGroup_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null && e.Node.Tag.ToString() == "child")
            {
                objBASEFILEDS.Tran_id = e.Node.Name;
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
            }
        }
    }
}
