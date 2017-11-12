using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using iMANTRA_DL;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using System.Reflection;
using CUSTOM_iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmDCMast : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string strSearchField = "head_nm";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        string key = "";

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

        public frmDCMast(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmDCMast_Load(object sender, EventArgs e)
        {
            // this.Dock = DockStyle.Fill;
            dgvOrder.AutoGenerateColumns = false;
            BindControls();
            String[] myArray1 = { "", "%", "@" };
            cmbApply.DataSource = myArray1.ToArray();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
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
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = ("01-01-1900");
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
            objBASEFILEDS.HTMAIN["typewise"] = rbtnHeaderWise.Checked ? true : false;
            objBASEFILEDS.HTMAIN["bef_aft"] = rbtnDetailsWise.Checked ? (chkbef_aft.Checked ? 1 : 2) : 0;
            objBASEFILEDS.HTMAIN["tran_nm"] = txtTransactionType.Text;
            objBASEFILEDS.HTMAIN["dr_ac_nm"] = txtDrAcNm.Text;
            objBASEFILEDS.HTMAIN["cr_ac_nm"] = txtCrAcNm.Text;
            switch (cmbChargesType.SelectedValue.ToString())
            {
                case "EXCISE": objBASEFILEDS.HTMAIN["charge_type"] = "E"; break;
                case "TAXABLE-CHARGES": objBASEFILEDS.HTMAIN["charge_type"] = "A"; objBASEFILEDS.HTMAIN["istaxable"] = true; break;
                case "TAXABLE-DISCOUNT": objBASEFILEDS.HTMAIN["charge_type"] = "D"; objBASEFILEDS.HTMAIN["istaxable"] = true; break;
                case "NON-TAXABLE-CHARGES": objBASEFILEDS.HTMAIN["charge_type"] = "A"; objBASEFILEDS.HTMAIN["istaxable"] = false; break;
                case "NON-TAXABLE-DISCOUNT": objBASEFILEDS.HTMAIN["charge_type"] = "D"; objBASEFILEDS.HTMAIN["istaxable"] = false; break;
            }
            objBASEFILEDS.HTMAIN["validity"] = txtValidIn.Text;
            objBASEFILEDS.HTMAIN["incl_in_stk"] = chkStock.Checked;
            objBASEFILEDS.HTMAIN["incl_in_stkfrm"] = dtpStockFrom.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["incl_in_stkto"] = dtpStockTo.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["isdeactive"] = chkDeActive.Checked;
            objBASEFILEDS.HTMAIN["deactfrm"] = dtpdeactivefrom.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["head_nm"] = txtHeaderNm.Text;
            objBASEFILEDS.HTMAIN["fld_nm"] = txtFldNm.Text;
            objBASEFILEDS.HTMAIN["corder"] = txtOrderNo.Text;
            objBASEFILEDS.HTMAIN["def_pert"] = txtDefRate.Text;
            objBASEFILEDS.HTMAIN["disp_sign"] = cmbApply.SelectedValue;
            objBASEFILEDS.HTMAIN["amtexpr"] = txtAmtExpr.Text;
            objBASEFILEDS.HTMAIN["disp_pert"] = chkDispPerc.Checked;
            objBASEFILEDS.HTMAIN["pert_name"] = txtPer_nm.Text;
            objBASEFILEDS.HTMAIN["round_off"] = chkRoundOff.Checked;
            objBASEFILEDS.HTMAIN["_read"] = chkRead.Checked;
            objBASEFILEDS.HTMAIN["inter_val"] = chkHide.Checked;

        }
        private void GetFieldValueFromHashTable()
        {
            rbtnHeaderWise.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString());
            rbtnDetailsWise.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString()) ? false : true;
            chkbef_aft.Checked = int.Parse(objBASEFILEDS.HTMAIN["bef_aft"].ToString()) == 1 ? true : false;
            txtTransactionType.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
            txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
            switch (objBASEFILEDS.HTMAIN["charge_type"].ToString())
            {
                case "E": cmbChargesType.SelectedItem = "EXCISE"; break;
                case "A": if (bool.Parse(objBASEFILEDS.HTMAIN["istaxable"].ToString())) { cmbChargesType.SelectedItem = "TAXABLE-CHARGES"; } else { cmbChargesType.SelectedItem = "NON-TAXABLE-CHARGES"; } break;
                case "D": if (bool.Parse(objBASEFILEDS.HTMAIN["istaxable"].ToString())) { cmbChargesType.SelectedItem = "TAXABLE-DISCOUNT"; } else { cmbChargesType.SelectedItem = "NON-TAXABLE-DISCOUNT"; } break;
            }
            txtValidIn.Text = objBASEFILEDS.HTMAIN["validity"].ToString();
            chkStock.Checked = bool.Parse(objBASEFILEDS.HTMAIN["incl_in_stk"].ToString());
            dtpStockFrom.DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN["incl_in_stkfrm"].ToString());
            dtpStockTo.DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN["incl_in_stkto"].ToString());
            chkDeActive.Checked = bool.Parse(objBASEFILEDS.HTMAIN["isdeactive"].ToString());
            dtpdeactivefrom.DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN["deactfrm"].ToString());
            txtHeaderNm.Text = objBASEFILEDS.HTMAIN["head_nm"].ToString();
            txtFldNm.Text = objBASEFILEDS.HTMAIN["fld_nm"].ToString();
            txtOrderNo.Text = objBASEFILEDS.HTMAIN["corder"].ToString();
            txtDefRate.Text = objBASEFILEDS.HTMAIN["def_pert"].ToString();
            cmbApply.SelectedItem = objBASEFILEDS.HTMAIN["disp_sign"].ToString();
            txtAmtExpr.Text = objBASEFILEDS.HTMAIN["amtexpr"].ToString();
            chkDispPerc.Checked = bool.Parse(objBASEFILEDS.HTMAIN["disp_pert"].ToString());
            txtPer_nm.Text = objBASEFILEDS.HTMAIN["pert_name"].ToString();
            chkRoundOff.Checked = bool.Parse(objBASEFILEDS.HTMAIN["round_off"].ToString());
            chkRead.Checked = bool.Parse(objBASEFILEDS.HTMAIN["_read"].ToString());
            chkHide.Checked = bool.Parse(objBASEFILEDS.HTMAIN["inter_val"].ToString());

        }
        private void BindControls()
        {
            if (cmbChargesType.Items.Count != 0)
            {
                //cmbChargesType.Items.Clear();
            }
            if (rbtnHeaderWise.Checked)
            {
                String[] myArray = { "", "TAXABLE-CHARGES", "TAXABLE-DISCOUNT", "NON-TAXABLE-CHARGES", "NON-TAXABLE-DISCOUNT" };
                cmbChargesType.DataSource = myArray.ToArray();
                //cmbChargesType.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);
            }
            else if (rbtnDetailsWise.Checked && chkbef_aft.Checked)
            {
                String[] myArray = { "", "TAXABLE-CHARGES", "TAXABLE-DISCOUNT" };
                cmbChargesType.DataSource = myArray.ToArray();
                //cmbChargesType.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);
            }
            else
            {
                String[] myArray = { "", "EXCISE", "TAXABLE-CHARGES", "TAXABLE-DISCOUNT", "NON-TAXABLE-CHARGES", "NON-TAXABLE-DISCOUNT" };
                cmbChargesType.DataSource = myArray.ToArray();
                //cmbChargesType.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);
            }
            //cmbChargesType.SelectedValueChanged -= new EventHandler(this.comboBox1_SelectedValueChanged);
            //cmbChargesType.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);       

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox1.SelectedItem.ToString());
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

            if (txtTransactionType.Text == "") { AutoClosingMessageBox.Show("Please Enter Valid Transaction Name", "Validation", 3000); flg = false; }
            else if (cmbChargesType.SelectedValue.ToString().Trim() == "") { AutoClosingMessageBox.Show("Please enter Chargeable Type", "Validation", 3000); flg = false; }
            else if (txtHeaderNm.Text == "") { AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000); flg = false; }
            else if (cmbApply.SelectedValue.ToString().Trim() == "") { AutoClosingMessageBox.Show("Please enter Type", "Validation", 3000); flg = false; }
            else if (cmbApply.SelectedValue.ToString().Trim() == "%") if (txtPer_nm.Text == "") { AutoClosingMessageBox.Show("Please enter %Field Name", "Validation", 3000); flg = false; }
                else if (txtFldNm.Text == "") { AutoClosingMessageBox.Show("Please enter Field Name", "Validation", 3000); flg = false; }
                else if (txtOrderNo.Text == "") { AutoClosingMessageBox.Show("Please enter Column Order", "Validation", 3000); flg = false; }
            //else if (txtPer_nm.Text == "") { if (chkDispPerc.Checked) { AutoClosingMessageBox.Show("Please enter %Field Name","Validation",3000); flg = false; } }          
            if (txtDefRate.Text.Trim() == "") { objBASEFILEDS.HTMAIN["def_pert"] = "0.00"; }

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
                            }
                        }
                        rbtnHeaderWise.Checked = true;
                        chkStock.Checked = true;
                        btnHeaderNm.Enabled = false;
                        btnFldNm.Enabled = false;
                        chkDispPerc.Checked = false;
                        lblPerNm.Visible = false;
                        txtPer_nm.Visible = false;
                        chkRoundOff.Visible = false;
                        txtDefRate.Text = "0.00";
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtHeaderNm.Focus();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        //InsertFieldValueToHashTable();
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
                                            else if (c is UserDT)
                                            {
                                                if (((UserDT)c).DtValue.ToString() != "1900-01-01 12:00:00 AM" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900-00-01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "1900/00/01" && ((UserDT)c).DtValue.ToString("yyyy/mm/dd") != "2000/00/01")
                                                    ((UserDT)c).bUpdateFlag = true;
                                                else
                                                {
                                                    ((UserDT)c).bUpdateFlag = false;
                                                    ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                                }
                                                c.Enabled = true;
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
                        GetFieldValueFromHashTable();
                        txtTransactionType.Enabled = false;
                        chkbef_aft.Enabled = false;
                        cmbApply.Enabled = false;
                        cmbChargesType.Enabled = false;
                        txtFldNm.Enabled = false;
                        txtValidIn.Enabled = false;
                        btnHeaderNm.Enabled = false;
                        btnFldNm.Enabled = false;
                        if (chkDispPerc.Checked)
                        {
                            txtPer_nm.Enabled = false;
                            txtPer_nm.Visible = true;
                        }
                        else
                        {
                            txtPer_nm.Enabled = true;
                            txtPer_nm.Visible = false;
                        }
                        chkDispPerc.Enabled = false;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtHeaderNm.Focus();
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        foreach (Control con1 in this.Controls)
                        {
                            foreach (Control con2 in con1.Controls)
                            {
                                foreach (Control con in con2.Controls)
                                {
                                    foreach (Control c in con.Controls)
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
                        if (chkDispPerc.Checked)
                        {
                            txtPer_nm.Visible = true;
                        }
                        else
                        {
                            txtPer_nm.Visible = false;
                        }
                        btnHeaderNm.Enabled = true;
                        btnFldNm.Enabled = true;
                        linkLabel1.Enabled = true;
                        txtSearch.Enabled = true;
                        dgvSearch.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                //objHashtables.HashMaintbl.Clear();

                BindGrid();
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

        private void frmDCMast_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmDCMast_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmDCMast_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isDcApp='1'", false, "", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtTransactionType.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            BindOrderGrid();
            //txttype.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            //txtcode.Text = objBASEFILEDS.HTMAIN["code"].ToString() == null ? "" : objBASEFILEDS.HTMAIN["code"].ToString();
        }
        private void btnValidIn_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "validity";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtValidIn.Text = objBASEFILEDS.HTMAIN["validity"].ToString();
        }

        private void rbtnDetailsWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDetailsWise.Checked)
            {
                chkbef_aft.Visible = true;
                txtOrderNo.Enabled = true;
                BindControls();
            }
        }
        private void rbtnHeaderWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHeaderWise.Checked)
            {
                chkbef_aft.Checked = false;
                chkbef_aft.Visible = false;
                txtOrderNo.Text = "0";
                txtOrderNo.Enabled = false;
                BindControls();
            }
        }
        private void cmbChargesType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void chkStock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStock.Checked)
            {
                dtpStockFrom.Enabled = true;
                dtpStockTo.Enabled = true;
                dtpStockFrom.DtValue = objBASEFILEDS.ObjCompany.Fin_yr_sta;
                dtpStockTo.DtValue = objBASEFILEDS.ObjCompany.Fin_yr_end;
            }
            else
            {
                dtpStockFrom.Enabled = false;
                dtpStockTo.Enabled = false;
            }
        }
        private void chkDeActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeActive.Checked)
            {
                if (dtpdeactivefrom.DtValue.ToString() == "1900-01-01 12:00:00 AM" || dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") == "2000-00-01" || dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") == "1900-00-01" || dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") == "1900/00/01" || dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") == "2000/00/01")
                {
                    dtpdeactivefrom.bUpdateFlag = true;
                    dtpdeactivefrom.DtValue = DateTime.Now;
                    dtpdeactivefrom.Enabled = true;
                }
            }
            else
            {
                dtpdeactivefrom.DtValue = DateTime.Parse("01/01/1900");
                dtpdeactivefrom.Enabled = false;
                if (dtpdeactivefrom.DtValue.ToString() != "1900-01-01 12:00:00 AM" && dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") != "2000-00-01" && dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") != "1900-00-01" && dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") != "1900/00/01" && dtpdeactivefrom.DtValue.ToString("yyyy/mm/dd") != "2000/00/01")
                {
                    dtpdeactivefrom.bUpdateFlag = true;
                }
                else
                {
                    dtpdeactivefrom.bUpdateFlag = false;
                    dtpdeactivefrom.DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                }
            }
        }

        private void btnHeaderNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "dc_mast", "", "dc_id,head_nm", "fld_nm;Field Name,code;Transaction Code,tran_nm;Transaction Name,head_nm;Caption", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtHeaderNm.Text = objBASEFILEDS.HTMAIN["head_nm"].ToString();
            objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["dc_id"].ToString();
            //AddFieldToHashTable();
            //GetFieldValueFromHashTable(); 
            //if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            //{
            //    ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            //}
            DisplayControlsonMode("view_mode");
        }
        private void btnFldNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "dc_mast", "", "dc_id,fld_nm", "fld_nm;Field Name,code;Transaction Code,tran_nm;Transaction Name,head_nm;Caption", "Please Select", "", false, "", "0");//tran_nm;Trasaction Name,
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtFldNm.Text = objBASEFILEDS.HTMAIN["fld_nm"].ToString();
            objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["dc_id"].ToString();
            //AddFieldToHashTable();
            //GetFieldValueFromHashTable(); 
            //if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            //{
            //    ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            //}
            DisplayControlsonMode("view_mode");
        }
        private void chkDispPerc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDispPerc.Checked)
            {
                lblPerNm.Visible = true;
                label18.Visible = true;
                txtPer_nm.Visible = true;
                //chkRoundOff.Visible = true;
            }
            else
            {
                lblPerNm.Visible = false;
                label18.Visible = false;
                txtPer_nm.Text = "";
                txtPer_nm.Visible = false;
                //chkRoundOff.Checked = false;
                //chkRoundOff.Visible = false;
            }
        }
        private void txtHeaderNm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtHeaderNm.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void txtFldNm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtFldNm.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Field Name", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void txtPer_nm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtPer_nm.Text == "" && chkDispPerc.Checked)
                {
                    AutoClosingMessageBox.Show("Please enter % Field Name", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void chkbef_aft_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbef_aft.Checked)
            {
                BindControls();
            }
            else
            {
                BindControls();
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AutoClosingMessageBox.Show("MAIN TABLE : HTMAIN[\"GRO_AMT\"] \nAnd ITEM TABLE : HTITEM_VALUE[\"QTY\"]*HTITEM_VALUE[\"RATE\"]", "Syntax", 3000);
        }
        private void txtTransactionType_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtTransactionType.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Select Transaction Type", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    BindOrderGrid();
                }
            }
        }
        private void cmbApply_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbApply.SelectedIndex == 0)
                {
                    AutoClosingMessageBox.Show("Please Select Type", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void cmbChargesType_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbChargesType.SelectedIndex == 0)
                {
                    AutoClosingMessageBox.Show("Please Select Charges Type", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void tcDC_Selected(object sender, TabControlEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (tcDC.SelectedIndex == 1)
                {
                    if (txtTransactionType.Text != "")
                    {
                        BindOrderGrid();
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please Select Transaction Type", "Validation", 3000);
                    }
                }
                else
                {
                    objBASEFILEDS.HASHTABLES = objHashtables;
                }
            }
        }

        private void BindGrid()
        {
            DataSet dsetDCOrder;
            if (rbtnDetailsWise.Checked)
            {
                if (txtTransactionType.Text != "")
                {
                    //string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' union all select distinct  dc_id,pert_name,corder+1,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and disp_pert=1)vw order by order_no,col_order_no";
                    // string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
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
                    ClearGrid();
                    //MessageBox.Show("Please Enter Transaction Type");
                }
            }
            else
            {
                if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
                {
                    objHashtables.HashMaintbl.Clear();
                }
                ClearGrid();
            }
        }
        private void BindOrderGrid()
        {
            DataSet dsetDCOrder;
            if (rbtnDetailsWise.Checked)
            {
                if (txtTransactionType.Text != "")
                {
                    //string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' union all select distinct  dc_id,pert_name,corder+1,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "'  and disp_pert=1)vw order by order_no,col_order_no";
                    //string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,tran_cd,inter_val from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
                    string strquery = "select * from (select distinct baseid custom_id,head_nm,order_no,col_order_no,tran_cd tran_cd1,inter_val,add_cnt=1 from ibasefields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and typewise=0 and order_no!=0 union all select distinct custom_id,head_nm,order_no,col_order_no,tran_cd,inter_val,add_cnt=1 from icustomfields where code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "' and _top=1 and typewise=0 union all select distinct  dc_id,head_nm,corder,col_order_no=0,tran_cd,inter_val,add_cnt=case when disp_pert=1 then 2 else 1 end from dc_mast where typewise=0 and code='" + objBASEFILEDS.HTMAIN["Code"].ToString() + "')vw order by order_no,col_order_no";
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
                                    //dgvOrder.Columns[row.Cells["head_nm"].Value.ToString()].DisplayIndex = int.Parse(entry.Value.ToString());
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
                    ClearGrid();
                    //MessageBox.Show("Please Enter Transaction Type");
                }
            }
            else
            {
                ClearGrid();
            }
        }

        private void txtTransactionType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isDcApp='1'", false, "", "0");
                    //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objpopup.ObjBFD = objBASEFILEDS;
                    objpopup.ShowDialog();
                    txtTransactionType.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
                }
            }
            catch (Exception ex)
            {

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
        private void btnArrange_Click(object sender, EventArgs e)
        {
            int Order_cnt = 0;
            Order_cnt = int.Parse(txtOrderNo.Text == "" ? "0" : txtOrderNo.Text);
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (tran_mode != "add_mode" && txtHeaderNm.Text == row.Cells["head_nm"].Value.ToString())
                {
                    row.Cells["order_no"].Value = Order_cnt;
                }
                if (int.Parse(row.Cells["order_no"].Value.ToString()) > Order_cnt || (int.Parse(row.Cells["order_no"].Value.ToString()) == Order_cnt && txtHeaderNm.Text != row.Cells["head_nm"].Value.ToString()))
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
        private void cmbApply_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbApply.SelectedValue.ToString() == "@")
            {
                chkDispPerc.Enabled = false;
                chkDispPerc.Checked = false;
            }
            else
            {
                chkDispPerc.Enabled = true;
                chkDispPerc.Checked = true;
            }
        }
        private void ClearGrid()
        {
            if (dgvOrder != null && dgvOrder.Rows.Count != 0)
            {
                //dgvOrder.Rows.Clear();
                while (dgvOrder.Rows.Count > 0)
                {
                    if (!dgvOrder.Rows[0].IsNewRow)
                    {
                        dgvOrder.Rows.RemoveAt(0);
                    }
                }
            }
        }

        private void ClearSearchGrid(DataGridView dgv)
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
        private void BindSearchGrid(string strMessage)
        {
            string strQuery = "select dc_id dc_id2,head_nm head_nm2,tran_nm from dc_mast where " + strSearchField + " like '%" + strMessage.Replace("'", "''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetCustom = objDBAdaper.dsquery(strQuery);
            dgvSearch.AutoGenerateColumns = false;
            if (dsetCustom != null && dsetCustom.Tables.Count != 0 && dsetCustom.Tables[0].Rows.Count != 0)
            {
                dgvSearch.DataSource = dsetCustom.Tables[0];
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
                ClearSearchGrid(dgvSearch);
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
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["dc_id2"].Value.ToString();
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
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["dc_id2"].Value.ToString();
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
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["dc_id2"].Value.ToString();
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
                    if (row.Cells["dc_id2"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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

        private void btnDrAc_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;dr_ac_id,ac_nm;dr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
           // frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "AC_GRP_ID;dr_ac_id,AC_GRP_NM;dr_ac_nm", "PARENT_NM;Parent,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "IVW_ACCOUNTS", "", "primary_id;dr_ac_id,primary_nm;dr_ac_nm", "primary_nm; Account Name", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
        }

        private void btnCrAc_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;cr_ac_id,ac_nm;cr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_GROUP", "", "AC_GRP_ID;cr_ac_id,AC_GRP_NM;cr_ac_nm", "PARENT_NM;Parent,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "IVW_ACCOUNTS", "", "primary_id;cr_ac_id,primary_nm;cr_ac_nm", "primary_nm; Account Name", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
        }

        private void txtDrAcNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;dr_ac_id,ac_nm;dr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtDrAcNm.Text = objBASEFILEDS.HTMAIN["dr_ac_nm"].ToString();
            }
        }

        private void txtCrAcNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id;cr_ac_id,ac_nm;cr_ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtCrAcNm.Text = objBASEFILEDS.HTMAIN["cr_ac_nm"].ToString();
            }
        }
    }
}
