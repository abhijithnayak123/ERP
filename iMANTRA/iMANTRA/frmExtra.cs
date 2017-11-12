using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_DL;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using iMANTRA_IL;
using System.Reflection;
using CUSTOM_iMANTRA_BL;
using System.Collections;

namespace iMANTRA
{
    public partial class frmExtra : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string key = "";
        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();
        DataSet ds = new DataSet();
        FL_BASEFIELD objFL_BASEFIELD = new FL_BASEFIELD();
        string strSearchField = "head_nm";
        string strtype = "";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

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

        public frmExtra(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmExtra_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Fill;
            String[] myArray = { "VARCHAR", "INT", "DECIMAL", "BIT", "DATETIME" };
            cmbdataty.DataSource = myArray.ToArray();
            cmbdataty.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);

            dgvOrder.AutoGenerateColumns = false;
            dgvWidth.AutoGenerateColumns = false;
            dgvSearch.AutoGenerateColumns = false;

            //BindSearchGrid("");
            BindControls();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");

            //loadSearch();
            //AddFieldToHashTable();

            //this.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Back_color);
            //this.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Font_color);
            //ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            //ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color);
            //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family, float.Parse(objBASEFILEDS.ObjControlSet.Font_size));
            //ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;

            //tabPage1.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color);
            //tabPage1.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color);

            //tabPage2.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color);
            //tabPage2.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color);

            //this.dgvOrder.BackgroundColor = Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color);
            //this.dgvWidth.BackgroundColor = Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color);
            AddTextBoxEvent();
        }

        private void AddTextBoxEvent()
        {
            foreach (Control con1 in this.Controls)
            {
                foreach (Control c1 in con1.Controls)
                {
                    foreach (Control c2 in c1.Controls)
                    {
                        foreach (Control c in c2.Controls)
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

        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where _isDeleteAllowed='1' and " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

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
            objBASEFILEDS.HTMAIN["custom_id"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["type"] = rbtnTransaction.Checked == true ? "1" : rbtnMaster.Checked == true ? "0" : "2";
            objBASEFILEDS.HTMAIN["typewise"] = rbtnHeader.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            objBASEFILEDS.HTMAIN["head_nm"] = txtCaption.Text;
            objBASEFILEDS.HTMAIN["order_no"] = txtcolorder.Text;
            objBASEFILEDS.HTMAIN["col_order_no"] = txtcolsuborder.Text;
            objBASEFILEDS.HTMAIN["fld_nm"] = txtfld_nm.Text;
            objBASEFILEDS.HTMAIN["code"] = txtcode.Text;
            objBASEFILEDS.HTMAIN["tran_nm"] = txttype.Text;
            objBASEFILEDS.HTMAIN["data_ty"] = cmbdataty.SelectedValue;//txtdataty.Text;
            objBASEFILEDS.HTMAIN["_fld_width"] = txtfld_wid.Text;
            objBASEFILEDS.HTMAIN["_fld_pre"] = txtfld_pre.Text;
            if (objBASEFILEDS.Tran_mode == "add_mode")
            {
                if (chkOnFocus.Checked == false) objBASEFILEDS.HTMAIN["when_con"] = "";
                if (chkValidation.Checked == false) objBASEFILEDS.HTMAIN["valid_con"] = "";
                if (chkDefault.Checked == false) objBASEFILEDS.HTMAIN["default_con"] = "";
            }
            string _mon_con = "";
            if (cmbdataty.SelectedValue != null && chkMandatory.Checked)
            {
                if (cmbdataty.SelectedValue.ToString() == "VARCHAR" || cmbdataty.SelectedValue.ToString() == "DATETIME")
                {
                    if (rbtnHeader.Checked)
                    {
                        _mon_con = "!EMPTY(HTMAIN[\"" + txtfld_nm.Text + "\"]) ? TRUE : FALSE";
                    }
                    else
                    {
                        _mon_con = "!EMPTY(HTITEM_VALUE[\"" + txtfld_nm.Text + "\"])?TRUE:FALSE";
                    }
                }
                else if (cmbdataty.SelectedValue.ToString() == "DECIMAL" || cmbdataty.SelectedValue.ToString() == "INT")
                {
                    if (rbtnHeader.Checked)
                    {
                        _mon_con = "HTMAIN[\"" + txtfld_nm.Text + "\"]>0 ? TRUE : FALSE";
                    }
                    else
                    {
                        _mon_con = "HTITEM_VALUE[\"" + txtfld_nm.Text + "\"]>0?TRUE:FALSE";
                    }
                }
            }
            objBASEFILEDS.HTMAIN["_mon_con"] = chkMandatory.Checked == true ? _mon_con : "";
            objBASEFILEDS.HTMAIN["error_con"] = "";
            objBASEFILEDS.HTMAIN["inter_val"] = chkHide.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["mandatory"] = chkMandatory.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["disp_pickup"] = chkDispPickUp.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["disp_nw_pickup"] = chkDispNwPickUp.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["disp_head"] = chkDispHeader.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["valid_mast"] = txtValidIn.Text;
            objBASEFILEDS.HTMAIN["remarks"] = txtdesc.Text;
            objBASEFILEDS.HTMAIN["_tab"] = cmbTabCtrl.SelectedValue;
            objBASEFILEDS.HTMAIN["_mul"] = (txtpopup.Text != "" || (txtTblNm.Text != "" && txtdipMem.Text != "" && txtVlaueMem.Text != "")) ? true : false;//cmbBtnCtrl.SelectedValue;
            objBASEFILEDS.HTMAIN["tbl_nm"] = txtTblNm.Text;
            objBASEFILEDS.HTMAIN["sel_item"] = txtVlaueMem.Text;
            objBASEFILEDS.HTMAIN["sel_val"] = txtdipMem.Text;
            objBASEFILEDS.HTMAIN["_query"] = txtpopup.Text;
            objBASEFILEDS.HTMAIN["_querycon"] = txtfilteron.Text;
            objBASEFILEDS.HTMAIN["_btntype"] = cmbBtnCtrl.SelectedValue;
            objBASEFILEDS.HTMAIN["_read"] = chkReadOnly.Checked == true ? true : false;
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
            objBASEFILEDS.HTMAIN["_isQcd"] = false;
            objBASEFILEDS.HTMAIN["QcdCondition"] = "";
        }
        private void GetFieldValueFromHashTable()
        {
            rbtnMaster.Checked = true;
            if (objBASEFILEDS.HTMAIN["type"] != null)
            {
                if (objBASEFILEDS.HTMAIN["type"].ToString() == "1")
                {
                    rbtnTransaction.Checked = true;
                }
                else if (objBASEFILEDS.HTMAIN["type"].ToString() == "2")
                {
                    rbtnCustomMast.Checked = true;
                }
            }
            //rbtnMaster.Checked = bool.Parse(objBASEFILEDS.HTMAIN["type"].ToString()) ? false : true;
            //rbtnTransaction.Checked = bool.Parse(objBASEFILEDS.HTMAIN["type"].ToString()) ? true : false;
            rbtnHeader.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString()) ? true : false;
            rbtnItemWise.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString()) ? false : true;
            txtCaption.Text = objBASEFILEDS.HTMAIN["head_nm"].ToString();
            txtcolorder.Text = objBASEFILEDS.HTMAIN["order_no"].ToString();
            txtcolsuborder.Text = objBASEFILEDS.HTMAIN["col_order_no"].ToString();
            txtcode.Text = objBASEFILEDS.HTMAIN["code"].ToString() == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
            txttype.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            txtfld_nm.Text = objBASEFILEDS.HTMAIN["fld_nm"].ToString();
            // txtdataty.Text = objBASEFILEDS.HTMAIN["data_ty"].ToString();
            cmbdataty.SelectedItem = objBASEFILEDS.HTMAIN["data_ty"].ToString();
            txtfld_wid.Text = objBASEFILEDS.HTMAIN["_fld_width"].ToString();
            txtfld_pre.Text = objBASEFILEDS.HTMAIN["_fld_pre"].ToString();
            chkOnFocus.Checked = objBASEFILEDS.HTMAIN["when_con"].ToString() == "" ? false : true;
            chkValidation.Checked = objBASEFILEDS.HTMAIN["valid_con"].ToString() == "" ? false : true;
            chkDefault.Checked = objBASEFILEDS.HTMAIN["default_con"].ToString() == "" ? false : true;
            chkHide.Checked = bool.Parse(objBASEFILEDS.HTMAIN["inter_val"].ToString()) ? true : false;
            chkMandatory.Checked = bool.Parse(objBASEFILEDS.HTMAIN["mandatory"].ToString()) ? true : false;
            chkDispPickUp.Checked = bool.Parse(objBASEFILEDS.HTMAIN["disp_pickup"].ToString()) ? true : false;
            chkDispNwPickUp.Checked = bool.Parse(objBASEFILEDS.HTMAIN["disp_nw_pickup"].ToString()) ? true : false;
            chkDispHeader.Checked = objBASEFILEDS.HTMAIN["disp_head"] != null && objBASEFILEDS.HTMAIN["disp_head"].ToString() != "" ? bool.Parse(objBASEFILEDS.HTMAIN["disp_head"].ToString()) : false;
            chkReadOnly.Checked = bool.Parse(objBASEFILEDS.HTMAIN["_read"].ToString()) ? true : false;
            txtValidIn.Text = objBASEFILEDS.HTMAIN["valid_mast"].ToString();
            txtdesc.Text = objBASEFILEDS.HTMAIN["remarks"].ToString();
            cmbTabCtrl.Text = objBASEFILEDS.HTMAIN["_tab"] != null ? objBASEFILEDS.HTMAIN["_tab"].ToString() : "";
            //  txtpopup.Text = objBASEFILEDS.HTMAIN["head_nm"].ToString();
            cmbBtnCtrl.Text = objBASEFILEDS.HTMAIN["_btntype"] != null ? objBASEFILEDS.HTMAIN["_btntype"].ToString() : "";

            txtTblNm.Text = objBASEFILEDS.HTMAIN["tbl_nm"].ToString();
            txtVlaueMem.Text = objBASEFILEDS.HTMAIN["sel_item"].ToString();
            txtdipMem.Text = objBASEFILEDS.HTMAIN["sel_val"].ToString();
            txtfilteron.Text = objBASEFILEDS.HTMAIN["_querycon"].ToString();
        }
        private void BindControls()
        {
            DataSet dsest = new DataSet();
            DataSet dset = new DataSet();
            string code = objBASEFILEDS.HTMAIN["code"] == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
            if (txttype.Text != "")
            {
                dsest = objDBAdaper.dsquery("select distinct * from  (select distinct _tab='' from ibasefields union all select distinct fld_nm _tab from  ibasefields where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' and code like '" + code + "' and data_ty='TAB')vw  order by _tab ");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            }
            else
            {
                dsest = objDBAdaper.dsquery("select distinct * from  (select distinct _tab='' from ibasefields union all select distinct fld_nm _tab from  ibasefields where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' and code like '%'  and data_ty='TAB')vw  order by _tab ");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            }
            cmbTabCtrl.DataSource = dsest.Tables[0];
            cmbTabCtrl.DisplayMember = "_tab";
            cmbTabCtrl.ValueMember = "_tab";
            cmbTabCtrl.Update();
            if (txttype.Text != "")
            {
                dset = objDBAdaper.dsquery("select distinct * from  (select distinct _btntype='' from ibasefields union all select distinct fld_nm _btntype from  ibasefields where parent_ctrl='' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' and code like '" + code + "' and data_ty='button')vw order by _btntype");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            }
            else
            {
                dset = objDBAdaper.dsquery("select distinct * from  (select distinct _btntype='' from ibasefields union all select distinct fld_nm _btntype from  ibasefields where parent_ctrl='' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' and code like '%' and data_ty='button')vw order by _btntype");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            }
            cmbBtnCtrl.DataSource = dset.Tables[0];
            cmbBtnCtrl.DisplayMember = "_btntype";
            cmbBtnCtrl.ValueMember = "_btntype";
            cmbBtnCtrl.Update();
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
            if (txttype.Text == "") { AutoClosingMessageBox.Show("Please Enter Valid Transaction Name", "Validation", 3000); flg = false; }
            //  else if (txtValidIn.Text == "") { MessageBox.Show("Please enter Valid Transaction Names"); flg = false; }
            else if (txtCaption.Text == "") { AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000); flg = false; }
            else if (txtfld_nm.Text == "") { AutoClosingMessageBox.Show("Please enter Field Name", "Validation", 3000); flg = false; }
            //else if (txtdataty.Text == "") { MessageBox.Show("Please enter Data Type"); flg = false; }
            else if (txtfld_wid.Enabled && txtfld_wid.Text == "") { AutoClosingMessageBox.Show("Please enter Field Width", "Validation", 3000); flg = false; }
            else if (chkDispHeader.Checked == false && cmbBtnCtrl.SelectedIndex == 0 && cmbTabCtrl.SelectedIndex == 0)
            { AutoClosingMessageBox.Show("Please Select Any One Option from Location Setting", "Validation", 3000); flg = false; }
            else if (txtcolorder.Text == "") { AutoClosingMessageBox.Show("Please enter Column Order", "Validation", 3000); flg = false; }
            // else if (txtcolsuborder.Text == "") { MessageBox.Show("Please enter Sub Column Order"); flg = false; }
            else if (txtfld_pre.Text == "") { if (cmbdataty.SelectedValue.ToString() == "DECIMAL") { AutoClosingMessageBox.Show("Please enter Field Precision", "Validation", 3000); flg = false; } }


            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (objHashtables == null)
                {
                    objHashtables = new BLHT();
                }
                if (row.Cells[0].Value != null)
                {
                    objHashtables.HashMaintbl[row.Cells["custom_id"].Value.ToString() + "," + row.Cells["tran_cd1"].Value.ToString()] = row.Cells["order_no"].Value;
                }
            }
            foreach (DataGridViewRow row in dgvWidth.Rows)
            {
                if (objHashtables == null)
                {
                    objHashtables = new BLHT();
                }
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells["adj_fld_wid"].Value != null && row.Cells["adj_fld_desc"].Value != null && row.Cells["adj_fld_wid"].Value.ToString() != "" && row.Cells["adj_fld_desc"].Value.ToString() != "" && row.Cells["adj_fld_wid"].Value.ToString() != "0" && row.Cells["adj_fld_desc"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[row.Cells["custom_id1"].Value.ToString() + "," + row.Cells["tran_cd2"].Value.ToString()] = row.Cells["adj_fld_wid"].Value + "," + row.Cells["adj_fld_desc"].Value;
                    }
                    else if (row.Cells["adj_fld_wid"].Value != null && row.Cells["adj_fld_wid"].Value.ToString() != "" && row.Cells["adj_fld_wid"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[row.Cells["custom_id1"].Value.ToString() + "," + row.Cells["tran_cd2"].Value.ToString()] = row.Cells["adj_fld_wid"].Value + "," + row.Cells["fld_desc"].Value;
                    }
                    else
                    {
                        objHashtables.HashItemtbl[row.Cells["custom_id1"].Value.ToString() + "," + row.Cells["tran_cd2"].Value.ToString()] = row.Cells["fld_wid"].Value + "," + row.Cells["fld_desc"].Value;
                    }
                }
            }
            objBASEFILEDS.HASHTABLES = objHashtables;
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
                            foreach (Control con2 in con1.Controls)
                            {
                                foreach (Control con in con2.Controls)
                                {
                                    foreach (Control c in con.Controls)
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
                                            rbtnMaster.Checked = true;
                                            rbtnHeader.Checked = true;
                                            rbtnItemWise.Checked = false;
                                            rbtnItemWise.Enabled = false;
                                            cmbBtnCtrl.Enabled = false;
                                        }
                                        else if (c is TextBox)
                                        {
                                            ((TextBox)c).Text = "";
                                        }
                                        c.Enabled = true;
                                    }
                                }
                            }
                        }
                        txtfld_pre.Enabled = false;
                        btn_header.Enabled = false;
                        btnType.Enabled = true;
                        txtcolsuborder.Text = "1";
                        txtcolsuborder.Enabled = false;
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtCaption.Focus();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        BindControls();
                        //InsertFieldValueToHashTable();
                        GetFieldValueFromHashTable();
                        foreach (Control con1 in this.Controls)
                        {
                            foreach (Control con2 in con1.Controls)
                            {
                                foreach (Control con in con2.Controls)
                                {
                                    foreach (Control c in con.Controls)
                                    {
                                        if (!(c is RadioButton))
                                        {
                                            if (c is TextBox)
                                            {
                                                if (!(c.Name == "txtfld_nm" || c.Name == "txtdataty" || c.Name == "txtfld_wid" || c.Name == "txtfld_pre" || c.Name == "txttype"))
                                                {
                                                    c.Enabled = true;
                                                }
                                            }
                                            else
                                            {
                                                c.Enabled = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //btnValidIn.Enabled = false;
                        if (rbtnTransaction.Checked)
                        {
                            cmbTabCtrl.Enabled = false;
                        }
                        else
                        {
                            cmbBtnCtrl.Enabled = false;
                        }
                        rbtnHeader.Enabled = false;
                        rbtnItemWise.Enabled = false;

                        txtcolsuborder.Enabled = false;
                        btn_header.Enabled = false;
                        btnType.Enabled = false;
                        cmbdataty.Enabled = false;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtCaption.Focus();
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        GetFieldValueFromHashTable();
                        foreach (Control con1 in this.Controls)
                        {
                            foreach (Control con2 in con1.Controls)
                            {
                                foreach (Control con in con2.Controls)
                                {
                                    foreach (Control c in con.Controls)
                                    {
                                        if (!(c is Label)) c.Enabled = false;
                                    }
                                }
                            }
                        }
                        foreach (Control c in grpbxSearch.Controls)
                        {
                            c.Enabled = true;
                        }
                        // InsertFieldValueToHashTable();
                        btn_header.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                BindGrid();
                BindWidthGrid();
                strSearchField = "head_nm";
                if (txtSearch.Text != "")
                    BindSearchGrid(txtSearch.Text);
                else
                    BindSearchGrid("%");
                if (objBASEFILEDS.Tran_mode != "add_mode")
                {
                    SelectSearchGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void frmExtra_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmExtra_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmExtra_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void chkDispHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDispHeader.Checked)
            {
                if (cmbBtnCtrl.SelectedIndex > 0)
                {
                    cmbBtnCtrl.SelectedIndex = 0;
                }
                if (cmbTabCtrl.SelectedIndex > 0)
                {
                    cmbTabCtrl.SelectedIndex = 0;
                }
                cmbBtnCtrl.Enabled = false;
                cmbTabCtrl.Enabled = false;
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    BindOrderGrid();
                    BindWidthGridTab();
                    GetColumnNumber(1, rbtnHeader.Checked);
                }
            }
            else
            {
                if (rbtnTransaction.Checked || rbtnCustomMast.Checked)
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        cmbBtnCtrl.Enabled = true;
                    }
                }
                else
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        cmbTabCtrl.Enabled = true;
                    }
                }
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    GetColumnNumber(0, rbtnHeader.Checked);
                }
            }
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.SelectedItem.ToString());
        }
        private void cmbBtnCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBtnCtrl.SelectedValue != null && cmbBtnCtrl.SelectedValue.ToString() != "")
            {
                chkDispHeader.Enabled = false;
                chkDispHeader.Checked = false;
                cmbTabCtrl.Enabled = false;
                if (objBASEFILEDS.Tran_mode != "view_mode")
                { BindOrderGrid(); BindWidthGridTab(); GetColumnNumber(2, rbtnHeader.Checked); }
            }
            else
            {
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    GetColumnNumber(0, rbtnHeader.Checked);
                }
                if (!rbtnCustomMast.Checked)
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        chkDispHeader.Enabled = true;
                    }
                }
                if (!rbtnTransaction.Checked)
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        cmbTabCtrl.Enabled = true;
                    }
                }
            }
        }
        private void cmbTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTabCtrl.SelectedValue != null && cmbTabCtrl.SelectedValue.ToString() != "")
            {
                chkDispHeader.Enabled = false;
                chkDispHeader.Checked = false;
                cmbBtnCtrl.Enabled = false;
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    BindOrderGrid(); BindWidthGridTab();
                    GetColumnNumber(3, rbtnHeader.Checked);
                }
            }
            else
            {
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    GetColumnNumber(0, rbtnHeader.Checked);
                }
                if (!rbtnCustomMast.Checked)
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        chkDispHeader.Enabled = true;
                    }
                }
                if (rbtnTransaction.Checked)
                {
                    if (objBASEFILEDS.Tran_mode != "view_mode")
                    {
                        cmbBtnCtrl.Enabled = true;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { }
        private void btnValidIn_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.Type = objBASEFILEDS.HTMAIN["type"].ToString();
            objfrmListMenus.Validity_fld_nm = "valid_mast";
            objfrmListMenus.ShowDialog();
            txtValidIn.Text = objBASEFILEDS.HTMAIN["valid_mast"].ToString();
        }
        private void GetColumnNumber(int coltype, bool _headerOrItem)
        {
            DataSet ds = new DataSet();
            string code = (objBASEFILEDS.HTMAIN.Count != 0 && objBASEFILEDS.HTMAIN["code"] != null) ? objBASEFILEDS.HTMAIN["code"].ToString() : "";
            if (coltype == 1)
            {
                if (_headerOrItem)
                {
                    ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + code + "' and typewise=1 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + code + "' and typewise=1)vw");
                }
                else
                {
                    ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from (select max(order_no) order_no,max(col_order_no) col_order_no from ibasefields where _top=1 and code='" + code + "' and typewise=0 union all select isnull(max(order_no),0) order_no,isnull(max(col_order_no),0) col_order_no from icustomfields where disp_head=1 and code='" + code + "' and typewise=0)vw");
                }
            }
            else if (coltype == 2)
            {
                if (_headerOrItem)
                {
                    ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _btntype='" + cmbBtnCtrl.SelectedValue + "'  and typewise=1");
                }
                else { ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _btntype='" + cmbBtnCtrl.SelectedValue + "'  and typewise=0"); }
            }
            else if (coltype == 3)
            {
                if (_headerOrItem)
                {
                    ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _tab='" + cmbTabCtrl.SelectedValue + "' and typewise=1");
                }
                else
                {
                    ds = objDBAdaper.dsquery("select max(order_no)+1 order_no,max(col_order_no)+1 col_order_no from icustomfields where code='" + code + "' and _tab='" + cmbTabCtrl.SelectedValue + "' and typewise=0");
                }
            }
            else
            {
                txtcolorder.Text = "0";
                txtcolsuborder.Text = "0";
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtcolorder.Text = ds.Tables[0].Rows[0]["order_no"].ToString();
                txtcolsuborder.Text = ds.Tables[0].Rows[0]["col_order_no"].ToString();
            }
        }

        private void rbtnMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMaster.Checked)
            {
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    chkDispHeader.Checked = false;
                    chkDispHeader.Enabled = true;

                    rbtnItemWise.Enabled = false;
                    rbtnHeader.Enabled = true;
                    cmbBtnCtrl.Enabled = false;
                    cmbTabCtrl.Enabled = true;
                    if (cmbBtnCtrl.SelectedIndex > 0)
                    {
                        cmbBtnCtrl.SelectedIndex = 0;
                    }
                    if (cmbTabCtrl.SelectedIndex > 0)
                    {
                        cmbTabCtrl.SelectedIndex = 0;
                    }

                    objBASEFILEDS.HTMAIN["type"] = "0";
                    txttype.Text = "";
                }
                //BindControls();
            }
            else
            {
                rbtnItemWise.Enabled = true;
            }
        }
        private void rbtnTransaction_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnTransaction.Checked)
            {
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    chkDispHeader.Checked = false;
                    chkDispHeader.Enabled = true;
                    cmbBtnCtrl.Enabled = true;
                    cmbTabCtrl.Enabled = false;
                    if (cmbTabCtrl.SelectedIndex > 0)
                    {
                        cmbTabCtrl.SelectedIndex = 0;
                    }
                    if (cmbBtnCtrl.SelectedIndex > 0)
                    {
                        cmbBtnCtrl.SelectedIndex = 0;
                    }

                    objBASEFILEDS.HTMAIN["type"] = "1";
                    txttype.Text = "";
                }
                // BindControls();
            }
        }
        private void rbtnHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHeader.Checked)
            {
                objBASEFILEDS.HTMAIN["typewise"] = rbtnHeader.Checked;
            }
        }
        private void rbtnItemWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnItemWise.Checked)
            {
                objBASEFILEDS.HTMAIN["typewise"] = !rbtnItemWise.Checked;
            }
        }
        private void btnType_Click(object sender, EventArgs e)
        {
            strtype = "tran_type='Master' and code not in('EM','BM')";
            if (objBASEFILEDS.HTMAIN["type"] != null)
            {
                if (objBASEFILEDS.HTMAIN["type"].ToString() == "1")
                {
                    strtype = "tran_type='Transaction'";
                }
                else if (objBASEFILEDS.HTMAIN["type"].ToString() == "2")
                {
                    strtype = "tran_type='CustomMaster'";
                }
            }
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", strtype, false, "", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txttype.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            txtcode.Text = objBASEFILEDS.HTMAIN["code"].ToString() == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
            BindControls();
        }
        private void txttype_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txttype.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Valid Transaction Name", "Validation", 3000);
                    e.Cancel = true;
                }
                BindControls();
            }
        }

        //private void txtdataty_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtdataty.Text == "")
        //    {
        //        MessageBox.Show("Please enter Data Type");
        //        e.Cancel = true;
        //    }
        //}

        private void btn_header_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "icustomfields", "", "custom_id,head_nm", "fld_nm;Field Name,code;Transaction Code,tran_nm;Trasaction Name,head_nm;Caption", "Please Select", "", false, "", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtCaption.Text = objBASEFILEDS.HTMAIN["head_nm"].ToString();
            objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["custom_id"].ToString();
            //txtcode.Text = objBASEFILEDS.HTMAIN["code"].ToString() == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
            //AddFieldToHashTable();
            //GetFieldValueFromHashTable();
            //if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            //{
            //    ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            //}
            DisplayControlsonMode("view_mode");
            //InsertFieldValueToHashTable();
        }
        private void chkValidation_Click(object sender, EventArgs e)
        {
            CheckValidations("valid_con");
            chkValidation.Checked = objBASEFILEDS.HTMAIN["valid_con"].ToString() == "" ? false : true;
        }
        private void chkOnFocus_Click(object sender, EventArgs e)
        {
            CheckValidations("when_con");
            chkOnFocus.Checked = objBASEFILEDS.HTMAIN["when_con"].ToString() == "" ? false : true;
        }
        private void chkDefault_Click(object sender, EventArgs e)
        {
            CheckValidations("default_con");
            chkDefault.Checked = objBASEFILEDS.HTMAIN["default_con"].ToString() == "" ? false : true;
        }
        private void CheckValidations(string validation_nm)
        {
            //DialogResult res = MessageBox.Show("Are You Sure?", "Open Syntax", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            //if (DialogResult.Yes == res)
            //{
            if ((chkValidation.Checked && validation_nm == "valid_con") || (chkOnFocus.Checked && validation_nm == "when_con") || (chkDefault.Checked && validation_nm == "default_con"))
            {
                frmsyntax objfrmSyntax = new frmsyntax(validation_nm, objBASEFILEDS.HTMAIN);
                objfrmSyntax.ObjBLFD = objBASEFILEDS;
                objfrmSyntax.ShowDialog();
                objBASEFILEDS.HTMAIN[validation_nm] = objfrmSyntax.HtLocal[validation_nm].ToString();
            }
            else
            {
                if (objBASEFILEDS.HTMAIN[validation_nm] != null && objBASEFILEDS.HTMAIN[validation_nm].ToString() != "")
                {
                    frmsyntax objfrmSyntax = new frmsyntax(validation_nm, objBASEFILEDS.HTMAIN);
                    objfrmSyntax.ObjBLFD = objBASEFILEDS;
                    objfrmSyntax.ShowDialog();
                    objBASEFILEDS.HTMAIN[validation_nm] = objfrmSyntax.HtLocal[validation_nm].ToString();
                }
                else
                {
                    objBASEFILEDS.HTMAIN[validation_nm] = "";
                }
            }
            //}
            //else
            //{
            //    if (objBASEFILEDS.HTMAIN[validation_nm] != null && objBASEFILEDS.HTMAIN[validation_nm].ToString() != "")
            //    {
            //        frmsyntax objfrmSyntax = new frmsyntax(validation_nm);
            //        objfrmSyntax.ObjBLFD = objBASEFILEDS;
            //        objfrmSyntax.ShowDialog();
            //        objBASEFILEDS.HTMAIN[validation_nm] = objfrmSyntax.ObjBLFD.HTMAIN["valid_con"].ToString();
            //    }
            //}
        }

        private void tcEM_Selected(object sender, TabControlEventArgs e)
        {
            if (tcEM.SelectedIndex == 1)
            {
                if (objBASEFILEDS.Tran_mode != "view_mode")
                {
                    dgvOrder.Enabled = true;
                }
                else
                {
                    dgvOrder.Enabled = false;
                }

                if (txttype.Text != "")
                {
                    BindOrderGrid();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Select Type", "Validation", 3000);
                }
            }
            else if (tcEM.SelectedIndex == 2)
            {
                if (objBASEFILEDS.Tran_mode != "view_mode" && rbtnTransaction.Checked && rbtnItemWise.Checked)
                {
                    dgvWidth.Enabled = true;
                }
                else
                {
                    dgvWidth.Enabled = false;
                }
                if (txttype.Text != "")
                {
                    BindWidthGridTab();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Select Type","Validation",3000);
                }
            }
            else
            {
                objBASEFILEDS.HASHTABLES = objHashtables;
            }
        }

        private void dgvOrder_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvOrder.CurrentCell.OwningColumn.Name == "order_no")
            {
                if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
                {
                    objHashtables.HashMaintbl[dgvOrder.Rows[e.RowIndex].Cells["custom_id"].Value + "," + dgvOrder.Rows[e.RowIndex].Cells["tran_cd1"].Value] = e.FormattedValue;//dgvOrder.CurrentRow.Cells["order_no"].Value;
                }
            }
        }

        private string GetQuery()
        {
            string strquery = "";
            if (rbtnTransaction.Checked)
            {
                if (rbtnItemWise.Checked)
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,case when (data_ty='button' and parent_ctrl!='') then 0 else 1 end add_cnt from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd tran_cd1,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
                else
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _top=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no,col_order_no";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }
            else if (rbtnCustomMast.Checked)
            {
                if (rbtnItemWise.Checked)
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,case when (data_ty='button' and parent_ctrl!='') then 0 else 1 end add_cnt from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd tran_cd1,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
                else
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _top=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no,col_order_no";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }
            else
            {
                if (rbtnHeader.Checked)
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head=1 and order_no>0 union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and data_ty!='TAB' and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=0 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0))vw order by order_no,col_order_no";
                    }
                    else
                    {
                        if (cmbTabCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _tab='" + cmbTabCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }

            return strquery;
        }
        private string GetQueryWidth()
        {
            string strquery = "";
            if (rbtnTransaction.Checked)
            {
                if (rbtnItemWise.Checked)
                {
                    //if (chkDispHeader.Checked)
                    //{
                    strquery = "select * from (select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id custom_id1,head_nm head_nm1,corder,col_order_no=0,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,corder order_no1,col_order_no1=1 from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no1,col_order_no1";
                    //}
                    //else
                    //{
                    //    strquery = "select custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                    //}
                }
                else
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no1,col_order_no1";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }
            else if (rbtnCustomMast.Checked)
            {
                if (rbtnItemWise.Checked)
                {
                    strquery = "select * from (select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=0 union all select distinct  dc_id custom_id1,head_nm head_nm1,corder,col_order_no=0,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,corder order_no1,col_order_no1=1 from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no1,col_order_no1";
                }
                else
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and _top=1) union all select distinct custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_head=1 and typewise=1)vw order by order_no1,col_order_no1";
                    }
                    else
                    {
                        if (cmbBtnCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _btntype='" + cmbBtnCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }
            else
            {
                if (rbtnHeader.Checked)
                {
                    if (chkDispHeader.Checked)
                    {
                        strquery = "select * from (select distinct custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and disp_head=1 and order_no>0 union all select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and data_ty!='TAB' and fld_nm not in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0) union all select distinct baseid custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from ibasefields where ibasefields.code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0 and fld_nm in (select distinct parent_ctrl from  ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and order_no>0))vw order by order_no1,col_order_no1";
                    }
                    else
                    {
                        if (cmbTabCtrl.SelectedValue != null)
                        {
                            strquery = "select custom_id custom_id1,head_nm head_nm1,fld_wid,fld_desc,tran_cd tran_cd2,inter_val inter_val1,fld_wid adj_fld_wid,fld_desc adj_fld_desc,order_no order_no1,col_order_no col_order_no1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=1 and _tab='" + cmbTabCtrl.SelectedValue.ToString() + "' and order_no>0 order by order_no,col_order_no";
                        }
                    }
                }
            }
            return strquery;
        }

        private void BindGrid()
        {
            DataSet dsetDCOrder;

            if (txttype.Text != "" && (chkDispHeader.Checked || (cmbBtnCtrl.SelectedValue != null && cmbBtnCtrl.SelectedValue.ToString() != "") || (cmbTabCtrl.SelectedValue != null && cmbTabCtrl.SelectedValue.ToString() != "")))
            {
                string strquery = "";
                strquery = GetQuery();
                if (strquery != "")
                {
                    dsetDCOrder = objDBAdaper.dsquery(strquery);
                    dgvOrder.AutoGenerateColumns = false;
                    dgvOrder.DataSource = dsetDCOrder.Tables[0];
                    dgvOrder.Update();
                    int i = 0;
                    foreach (DataRow row in dsetDCOrder.Tables[0].Rows)
                    {
                        foreach (DataGridViewColumn column in dgvOrder.Columns)
                        {
                            if (dsetDCOrder.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvOrder.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                        i++;
                    }

                    objHashtables = objBASEFILEDS.HASHTABLES;
                    if (objHashtables != null && objHashtables.HashMaintbl != null)
                    {
                        objHashtables.HashMaintbl.Clear();
                    }
                    else
                    {
                        objHashtables = new BLHT();
                    }
                    foreach (DataGridViewRow row in dgvOrder.Rows)
                    {
                        objHashtables.HashMaintbl[row.Cells["custom_id"].Value.ToString() + "," + row.Cells["tran_cd1"].Value.ToString()] = row.Cells["order_no"].Value;
                    }
                    objBASEFILEDS.HASHTABLES = objHashtables;
                }
                else
                {
                    if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
                    {
                        objHashtables.HashMaintbl.Clear();
                    }
                    ClearGrid(dgvOrder);
                }
            }
        }
        private void BindOrderGrid()
        {
            DataSet dsetDCOrder;
            string strquery = "";
            if (txttype.Text != "")
            {
                strquery = GetQuery();
                if (strquery != "")
                {
                    dsetDCOrder = objDBAdaper.dsquery(strquery);
                    dgvOrder.AutoGenerateColumns = false;
                    dgvOrder.DataSource = dsetDCOrder.Tables[0];
                    dgvOrder.Update();
                    int i = 0;
                    foreach (DataRow row in dsetDCOrder.Tables[0].Rows)
                    {
                        foreach (DataGridViewColumn column in dgvOrder.Columns)
                        {
                            if (dsetDCOrder.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvOrder.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                        i++;
                    }

                    objHashtables = objBASEFILEDS.HASHTABLES;
                    if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
                    {
                        foreach (DataGridViewRow row in dgvOrder.Rows)
                        {
                            foreach (DictionaryEntry entry in objHashtables.HashMaintbl)
                            {
                                key = row.Cells["custom_id"].Value.ToString() + "," + row.Cells["tran_cd1"].Value.ToString();
                                if (key == entry.Key.ToString())
                                {
                                    row.Cells["order_no"].Value = entry.Value.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        objHashtables = new BLHT();
                    }
                }
                else
                {
                    if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
                    {
                        objHashtables.HashMaintbl.Clear();
                    }
                    ClearGrid(dgvOrder);
                    //if (txttype.Text != "")
                    //{
                    //    MessageBox.Show("Please Enter Transaction Type");
                    //}
                }
            }
        }

        private void BindWidthGrid()
        {
            DataSet dsetWidthOrder;

            if (txttype.Text != "" && (chkDispHeader.Checked || (cmbBtnCtrl.SelectedValue != null && cmbBtnCtrl.SelectedValue.ToString() != "") || (cmbTabCtrl.SelectedValue != null && cmbTabCtrl.SelectedValue.ToString() != "")))
            {
                string strquery = GetQueryWidth();
                if (strquery != "")
                {
                    dsetWidthOrder = objDBAdaper.dsquery(strquery);
                    dgvWidth.AutoGenerateColumns = false;
                    dgvWidth.DataSource = dsetWidthOrder.Tables[0];
                    dgvWidth.Update();
                    int i = 0;
                    foreach (DataRow row in dsetWidthOrder.Tables[0].Rows)
                    {
                        foreach (DataGridViewColumn column in dgvWidth.Columns)
                        {
                            if (dsetWidthOrder.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvWidth.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                        i++;
                    }

                    objHashtables = objBASEFILEDS.HASHTABLES;
                    if (objHashtables != null && objHashtables.HashItemtbl != null)
                    {
                        objHashtables.HashItemtbl.Clear();
                    }
                    else
                    {
                        objHashtables = new BLHT();
                    }
                    foreach (DataGridViewRow row in dgvWidth.Rows)
                    {
                        objHashtables.HashItemtbl[row.Cells["custom_id1"].Value.ToString() + "," + row.Cells["tran_cd2"].Value.ToString()] = row.Cells["adj_fld_wid"].Value + "," + row.Cells["adj_fld_desc"].Value;
                    }
                    objBASEFILEDS.HASHTABLES = objHashtables;
                }
                else
                {
                    if (objHashtables != null && objHashtables.HashItemtbl != null && objHashtables.HashItemtbl.Count != 0)
                    {
                        objHashtables.HashItemtbl.Clear();
                    }
                    ClearGrid(dgvWidth);
                }
            }
        }
        private void BindWidthGridTab()
        {
            DataSet dsetWidth;

            if (txttype.Text != "")
            {
                string strquery = GetQueryWidth();
                if (strquery != "")
                {
                    dsetWidth = objDBAdaper.dsquery(strquery);
                    //dgvWidth.AutoGenerateColumns = false;
                    dgvWidth.DataSource = dsetWidth.Tables[0];
                    dgvWidth.Update();
                    int i = 0;
                    foreach (DataRow row in dsetWidth.Tables[0].Rows)
                    {
                        foreach (DataGridViewColumn column in dgvWidth.Columns)
                        {
                            if (dsetWidth.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvWidth.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                        i++;
                    }

                    objHashtables = objBASEFILEDS.HASHTABLES;
                    if (objHashtables != null && objHashtables.HashItemtbl != null && objHashtables.HashItemtbl.Count != 0)
                    {
                        foreach (DataGridViewRow row in dgvWidth.Rows)
                        {
                            foreach (DictionaryEntry entry in objHashtables.HashItemtbl)
                            {
                                key = row.Cells["custom_id1"].Value.ToString() + "," + row.Cells["tran_cd2"].Value.ToString();
                                if (key == entry.Key.ToString())
                                {
                                    row.Cells["adj_fld_wid"].Value = entry.Value.ToString().Split(',')[0];
                                    row.Cells["adj_fld_desc"].Value = entry.Value.ToString().Split(',')[1];
                                }
                            }
                        }
                    }
                    else
                    {
                        objHashtables = new BLHT();
                    }
                }
                else
                {
                    if (objHashtables != null && objHashtables.HashItemtbl != null && objHashtables.HashItemtbl.Count != 0)
                    {
                        objHashtables.HashItemtbl.Clear();
                    }
                    ClearGrid(dgvWidth);
                }
            }
        }

        private void txttype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    strtype = "tran_type='Master' and code not in('EM','BM')";
                    if (objBASEFILEDS.HTMAIN["type"] != null)
                    {
                        if (objBASEFILEDS.HTMAIN["type"].ToString() == "1")
                        {
                            strtype = "tran_type='Transaction'";
                        }
                        else if (objBASEFILEDS.HTMAIN["type"].ToString() == "2")
                        {
                            strtype = "tran_type='CustomMaster'";
                        }
                    }
                    frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", strtype, false, "", "0");
                    //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objpopup.ObjBFD = objBASEFILEDS;
                    objpopup.ShowDialog();
                    txttype.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
                    txtcode.Text = objBASEFILEDS.HTMAIN["code"].ToString() == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
                    BindControls();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearGrid(DataGridView dgv)
        {
            if (dgv != null && dgv.Rows.Count != 0)
            {
                //dgvOrder.Rows.Clear();
                while (dgv.Rows.Count > 0)
                {
                    if (!dgv.Rows[0].IsNewRow)
                    {
                        dgv.Rows.RemoveAt(0);
                    }
                }
            }
        }
        private void groupBox4_Enter(object sender, EventArgs e)
        {
            //if (txttype.Text == "")
            //{
            //    AutoClosingMessageBox.Show("Please enter Type","Validation",3000);
            //    if (txttype.CanFocus)
            //        txttype.Focus();
            //}
            //if (chkDispHeader.Checked == false && cmbBtnCtrl.SelectedIndex == 0 && cmbTabCtrl.SelectedIndex == 0)
            //{
            //    AutoClosingMessageBox.Show("Please Select Any One Option from Location Setting","Validation",3000);
            //    if (chkDispHeader.CanFocus)
            //    {
            //        chkDispHeader.Focus();
            //    }
            //}
        }

        private void dgvWidth_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvWidth.CurrentCell.OwningColumn.Name == "adj_fld_wid")
            {
                if (objHashtables != null && objHashtables.HashItemtbl != null && objHashtables.HashItemtbl.Count != 0)
                {
                    //objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = e.FormattedValue + "," + dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value;//dgvOrder.CurrentRow.Cells["order_no"].Value;
                    if (dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "0" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = e.FormattedValue + "," + dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value;
                    }
                    else if (dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = e.FormattedValue + "," + dgvWidth.Rows[e.RowIndex].Cells["fld_desc"].Value;
                    }
                    else
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = dgvWidth.Rows[e.RowIndex].Cells["fld_wid"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["fld_desc"].Value;
                    }
                }
            }
            else if (dgvWidth.CurrentCell.OwningColumn.Name == "adj_fld_desc")
            {
                if (objHashtables != null && objHashtables.HashItemtbl != null && objHashtables.HashItemtbl.Count != 0)
                {
                    //objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value + "," + e.FormattedValue;//dgvOrder.CurrentRow.Cells["order_no"].Value;
                    if (dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "0" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_desc"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value + "," + e.FormattedValue;
                    }
                    else if (dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value != null && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "" && dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value.ToString() != "0")
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = dgvWidth.Rows[e.RowIndex].Cells["adj_fld_wid"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["fld_desc"].Value;
                    }
                    else
                    {
                        objHashtables.HashItemtbl[dgvWidth.Rows[e.RowIndex].Cells["custom_id1"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["tran_cd2"].Value] = dgvWidth.Rows[e.RowIndex].Cells["fld_wid"].Value + "," + dgvWidth.Rows[e.RowIndex].Cells["fld_desc"].Value;
                    }
                }
            }
        }
        private void txtCaption_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtCaption.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Field Caption","Validation",3000);
                    e.Cancel = true;
                }
                else
                {
                    txtdesc.Text = txtdesc.Text == "" ? txtCaption.Text : txtdesc.Text;
                }
            }
        }
        private void txtfld_pre_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtfld_pre.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Field Precision","Validation",3000);
                    e.Cancel = true;
                }
            }
        }
        private void txtfld_wid_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtfld_wid.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Field Width","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void btnArrange_Click(object sender, EventArgs e)
        {
            int Order_cnt = 0;
            Order_cnt = int.Parse(txtcolorder.Text == "" ? "0" : txtcolorder.Text);
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (tran_mode != "add_mode" && txtCaption.Text == row.Cells["head_nm"].Value.ToString())
                {
                    row.Cells["order_no"].Value = Order_cnt;
                }
            }
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (int.Parse(row.Cells["order_no"].Value.ToString()) > Order_cnt || (int.Parse(row.Cells["order_no"].Value.ToString()) == Order_cnt && txtCaption.Text != row.Cells["head_nm"].Value.ToString()))
                {
                    if (row.Index == 0)
                    {
                        row.Cells["order_no"].Value = Order_cnt + 1;
                    }
                    else
                    {
                        row.Cells["order_no"].Value = Order_cnt + int.Parse(dgvOrder.Rows[row.Index - 1].Cells["add_cnt"].Value.ToString());
                    }
                    Order_cnt = int.Parse(row.Cells["order_no"].Value.ToString());
                }
                objHashtables.HashMaintbl[row.Cells["custom_id"].Value.ToString() + "," + row.Cells["tran_cd1"].Value.ToString()] = row.Cells["order_no"].Value;
            }
        }
        private void cmbdataty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbdataty.SelectedValue.ToString() == "VARCHAR")
            {
                txtfld_wid.Text = txtfld_wid.Text == "" ? "" : txtfld_wid.Text;
                txtfld_pre.Text = "";
                txtfld_wid.Enabled = true;
                txtfld_pre.Enabled = false;
            }
            else if (cmbdataty.SelectedValue.ToString() == "DECIMAL")
            {
                txtfld_wid.Text = txtfld_wid.Text == "" ? "" : txtfld_wid.Text;
                txtfld_pre.Text = txtfld_pre.Text == "" ? "" : txtfld_pre.Text;
                txtfld_wid.Enabled = true;
                txtfld_pre.Enabled = true;
            }
            else
            {
                txtfld_wid.Text = "";
                txtfld_pre.Text = "";
                txtfld_wid.Enabled = false;
                txtfld_pre.Enabled = false;
            }
        }
        private void txtfld_nm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtfld_nm.Text == "") { AutoClosingMessageBox.Show("Please enter Field Name","Validation",3000); e.Cancel = true; }
            }
        }
        private void txtValidIn_Leave(object sender, EventArgs e)
        {
            //BindControls();
        }
        private void rbtnCustomMast_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCustomMast.Checked)
            {
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    chkDispHeader.Checked = false;
                    chkDispHeader.Enabled = false;

                    rbtnItemWise.Enabled = true;
                    rbtnHeader.Enabled = true;
                    cmbBtnCtrl.Enabled = true;
                    cmbTabCtrl.Enabled = false;
                    if (cmbBtnCtrl.SelectedIndex > 0)
                    {
                        cmbBtnCtrl.SelectedIndex = 0;
                    }
                    if (cmbTabCtrl.SelectedIndex > 0)
                    {
                        cmbTabCtrl.SelectedIndex = 0;
                    }

                    objBASEFILEDS.HTMAIN["type"] = "2";
                    txttype.Text = "";
                }
                //BindControls();
            }
        }

        private void ClearSearchGrid()
        {
            if (dgvSearch != null && dgvSearch.Rows.Count != 0)
            {
                //dgvOrder.Rows.Clear();
                while (dgvSearch.Rows.Count > 0)
                {
                    if (!dgvSearch.Rows[0].IsNewRow)
                    {
                        dgvSearch.Rows.RemoveAt(0);
                    }
                }
            }
        }
        private void BindSearchGrid(string strMessage)
        {
            string strQuery = "select custom_id custom_id2,head_nm head_nm2,tran_nm from icustomfields where _isDeleteAllowed='1' and " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetCustom = objDBAdaper.dsquery(strQuery);

            if (dsetCustom != null && dsetCustom.Tables.Count != 0 && dsetCustom.Tables[0].Rows.Count != 0)
            {
                dgvSearch.AutoGenerateColumns = false;
                dgvSearch.DataSource = dsetCustom.Tables[0].DefaultView;
                dgvSearch.Update();
                int i = 0;
                foreach (DataRow row in dsetCustom.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvSearch.Columns)
                    {
                        if (dsetCustom.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvSearch.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblRowsCount.Text = "Total Records : " + dgvSearch.Rows.Count;
            }
            else
            {
                lblRowsCount.Text = "Total Records : 0";
                ClearSearchGrid();
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                BindSearchGrid(txtSearch.Text);
            else
                BindSearchGrid("%");
            SelectSearchGrid();
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgvSearch.Focus();
            }
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvSearch != null)
                {
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["custom_id2"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
                    DisplayControlsonMode("view_mode");
                    txtSearch.Focus();
                }
            }
        }
        private void dgvSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["custom_id2"].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
                SelectSearchGrid();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    if (dgvSearch.Columns[e.ColumnIndex].Name == "head_nm2")
                    {
                        strSearchField = "head_nm";
                    }
                    else
                        strSearchField = dgvSearch.Columns[e.ColumnIndex].Name;
                }
                else
                    strSearchField = "head_nm";
            }
        }
        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["custom_id2"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
                    DisplayControlsonMode("view_mode");
                    // dgvSearch.Focus();
                }
                else if (e.KeyData == Keys.Up && dgvView.CurrentRow.Index < 1)
                {
                    txtSearch.Focus();
                }
                else
                {
                    // dgvSearch.Focus();
                }
            }
        }
        private void SelectSearchGrid()
        {
            dgvSearch.ScrollBars = ScrollBars.Both;
            if (dgvSearch.CurrentRow != null && objBASEFILEDS.Tran_id != "0")
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dgvSearch.Rows)
                {
                    if (row.Cells["custom_id2"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
                    {
                        rowIndex = row.Index;
                        // break;
                    }
                    dgvSearch.Rows[row.Index].DefaultCellStyle.BackColor = dgvSearch.DefaultCellStyle.BackColor;
                }
                if (rowIndex != -1)
                {
                    dgvSearch.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
    }
}
