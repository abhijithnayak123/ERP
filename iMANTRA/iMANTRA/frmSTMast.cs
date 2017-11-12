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
    public partial class frmSTMast : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string strSearchField = "head_nm";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

       // string key = "";

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

        public frmSTMast(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmSTMast_Load(object sender, EventArgs e)
        {
            BindControls();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            AddTextBoxEvent();
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
            objBASEFILEDS.HTMAIN["tax_nm"] = txtTaxNm.Text;
            objBASEFILEDS.HTMAIN["valid_tran"] = txtTaxValid.Text;
            objBASEFILEDS.HTMAIN["tran_nm"] = txtModuleNm.Text;
            objBASEFILEDS.HTMAIN["stax_nm"] = cmbTaxType.SelectedValue;
            objBASEFILEDS.HTMAIN["pert_val"] = txtPercentage.Text;
            objBASEFILEDS.HTMAIN["issue_frm"] = txtissued.Text;
            objBASEFILEDS.HTMAIN["receive_frm"] = txtreceived.Text;
            objBASEFILEDS.HTMAIN["ac_nm"] = txtAcc_nm.Text;
            objBASEFILEDS.HTMAIN["isdeactive"] = chkTaxDeactivate.Checked;
            objBASEFILEDS.HTMAIN["deactfrm"] = dtpTaxDeactiveFrom.DtValue.ToString("yyyy/MM/dd"); // != null && dtpTaxDeactiveFrom.Value.ToString() != "" ? dtpTaxDeactiveFrom.Value.ToString("yyyy/MM/dd") : DateTime.Parse("01/01/1900").ToString("yyyy/MM/dd");
        }

        private void GetFieldValueFromHashTable()
        {
            txtTaxNm.Text = objBASEFILEDS.HTMAIN["tax_nm"].ToString();
            txtTaxValid.Text = objBASEFILEDS.HTMAIN["valid_tran"].ToString();
            txtModuleNm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            cmbTaxType.SelectedItem = objBASEFILEDS.HTMAIN["stax_nm"].ToString();
            txtPercentage.Text = objBASEFILEDS.HTMAIN["pert_val"].ToString();
            txtissued.Text = objBASEFILEDS.HTMAIN["issue_frm"].ToString();
            txtreceived.Text = objBASEFILEDS.HTMAIN["receive_frm"].ToString();
            txtAcc_nm.Text = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
            chkTaxDeactivate.Checked = objBASEFILEDS.HTMAIN["isdeactive"] != null && objBASEFILEDS.HTMAIN["isdeactive"].ToString() != "" ? bool.Parse(objBASEFILEDS.HTMAIN["isdeactive"].ToString()) : false;
            dtpTaxDeactiveFrom.DtValue = objBASEFILEDS.HTMAIN["deactfrm"]!= null && objBASEFILEDS.HTMAIN["deactfrm"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["deactfrm"].ToString()) : DateTime.Now;

        }

        private void BindControls()
        {
            String[] myArray = { "LOCAL", "OUT OF STATE" };
            cmbTaxType.DataSource = myArray.ToArray();
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

            if (txtTaxNm.Text == "") { AutoClosingMessageBox.Show("Please enter Valid Tax Name","Validation",3000); flg = false; }
            else if (txtModuleNm.Text == "") { AutoClosingMessageBox.Show("Please Enter Valid Transaction Name","Validation",3000); flg = false; }
            else if (txtPercentage.Text == "") { AutoClosingMessageBox.Show("Please enter Valid Percentage","Validation",3000); flg = false; }

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
                                        else if (c is UserDT)
                                        {
                                            ((UserDT)c).bUpdateFlag = false;
                                            ((UserDT)c).DtValue = DateTime.Now;
                                        }
                                        else if (c is RadioButton)
                                        {

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
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        txtTaxNm.Enabled = true;
                        txtTaxValid.Enabled = true;
                        txtModuleNm.Enabled = true;
                        btnTaxNm.Enabled = false;
                        btnTaxValidIn.Enabled = true;
                        btnModuleNm.Enabled = true;
                        dgvTaxSearch.Enabled = false;
                        txtTaxSearch.Text = "";
                        txtTaxSearch.Enabled = false;
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
                                            if (c is UserDT)
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
                        txtTaxNm.Enabled = false;
                        txtTaxValid.Enabled = false;
                        txtModuleNm.Enabled = false;
                        btnTaxNm.Enabled = false;
                        btnTaxValidIn.Enabled = true;
                        btnModuleNm.Enabled = false;
                        txtTaxSearch.Text = "";
                        dgvTaxSearch.Enabled = false;
                        txtTaxSearch.Enabled = false;
                        cmbTaxType.Enabled = false;
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
                                            c.Enabled = false;
                                        }
                                        if (!(c is Label)) c.Enabled = false;
                                    }
                                }
                            }
                        }
                        GetFieldValueFromHashTable();
                        btnTaxNm.Enabled = true;
                        dgvTaxSearch.Enabled = true;
                        txtTaxSearch.Enabled = true;
                        txtTaxSearch.Focus();
                        break;
                    default: break;
                }

                strSearchField = "tax_nm";
                if (txtTaxSearch.Text != "")
                    BindSearchGrid(txtTaxSearch.Text);
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

        private void frmSTMast_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmSTMast_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }        

        private void txtModuleNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isTaxApp='1'", false, "", "0");
                //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtModuleNm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            }
        }

        private void btnModuleNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isTaxApp='1'", false, "", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtModuleNm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();

            if (txtModuleNm.Text != "")
            {
                CopyCode();
            }
            else
            {
                AutoClosingMessageBox.Show("Please select valid Module","Validation",3000);
                txtModuleNm.Focus();
            }
        }

        private void btnTaxNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "st_mast", "", "STAX_ID,TAX_NM", "STAX_ID,TAX_NM;Sales Tax Name", "Please Select", "", false, "", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtTaxNm.Text = objBASEFILEDS.HTMAIN["tax_nm"].ToString();
        }

        private void btnTaxValidIn_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "valid_tran";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            txtTaxValid.Text = objBASEFILEDS.HTMAIN["valid_tran"].ToString();
        }

        public bool ValidateTaxName()
        {
            if (objBASEFILEDS.Code == "ST")
            {
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    DataSet ds = objDBAdaper.dsquery("select tax_nm,code from st_mast where tax_nm = '" + txtTaxNm.Text.Replace("'", "''") + "' and code='" + objBASEFILEDS.HTMAIN["CODE"].ToString() + "'");
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Tax Name for this transaction already exists","Validation",3000);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CopyCode()
        {
            if (objBASEFILEDS.HTMAIN.Contains("TRAN_NM"))
            {
                DataSet ds = objDBAdaper.dsquery("select code,tran_cd from Tran_set where Tran_nm='" + txtModuleNm.Text.Replace("'", "''") + "'");
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    objBASEFILEDS.HTMAIN["CODE"] = ds.Tables[0].Rows[0]["CODE"].ToString();
                }
                else
                {
                    AutoClosingMessageBox.Show("Invalid Module","Validation",3000);
                    return true;
                }
            }
            return false;
        }

        public bool CheckEdit()
        {
            if (objBASEFILEDS.Tran_mode == "edit_mode")
            {
                DataSet ds2 = objDBAdaper.dsquery("select Behavier_cd  from TRAN_SET where code='" + objBASEFILEDS.HTMAIN["Code"] + "' ");
                string code = ds2.Tables[0].Rows[0]["Behavier_cd"].ToString();
                DataSet ds1 = objDBAdaper.dsquery("select ST_MAST.tax_nm,ST_MAST.pert_val from " + code + "MAIN Inner Join ST_MAST ON(ST_MAST.TAX_NM=" + code + "MAIN.TAX_NM) and ST_MAST.code= " + code + "MAIN.Tran_cd where ST_MAST.tax_nm='" + txtTaxNm.Text + "' and " + code + "MAIN.tran_cd='" + objBASEFILEDS.HTMAIN["Code"] + "'");
                if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                {
                    if (txtTaxNm.Text.ToLower() == ds1.Tables[0].Rows[0]["Tax_nm"].ToString().ToLower())
                    {
                        if (Decimal.Parse(txtPercentage.Text != "" ? txtPercentage.Text : "0.00") != Decimal.Parse(ds1.Tables[0].Rows[0]["PERT_VAL"].ToString()))
                        {
                            AutoClosingMessageBox.Show("Editing this field is not posible, Reason: Used in Transaction","Validation",3000);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void txtTaxNm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtTaxNm.Text != "")
                {
                    e.Cancel = ValidateTaxName();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please enter Valid Tax Name","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtPercentage_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtPercentage.Text != "")
                {
                    e.Cancel = CheckEdit();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please enter Valid Percentage","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void cmbTaxType_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbTaxType.SelectedValue != null && cmbTaxType.SelectedValue.ToString() != "" && cmbTaxType.Text != "")
                {
                    e.Cancel = CheckEdit();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please select valid Tax Type","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtModuleNm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtModuleNm.Text != "")
                {
                    e.Cancel = CopyCode();
                }
                else
                {
                    AutoClosingMessageBox.Show("Please select valid Module","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void txtTaxValid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmListOfMenus objfrmListMenus = new frmListOfMenus();
                objfrmListMenus.Type = "1";
                objfrmListMenus.Validity_fld_nm = "valid_tran";
                objfrmListMenus.ObjBFD = objBASEFILEDS;
                objfrmListMenus.ShowDialog();
                txtTaxValid.Text = objBASEFILEDS.HTMAIN["valid_tran"].ToString();
            }
        }

        private void frmSTMast_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }

        private void BindSearchGrid(string strMessage)
        {
            string strQuery = "select stax_id,tax_nm,tran_nm tax_tran_nm from st_mast where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetTax = objDBAdaper.dsquery(strQuery);
            dgvTaxSearch.AutoGenerateColumns = false;
            if (dsetTax != null && dsetTax.Tables.Count != 0 && dsetTax.Tables[0].Rows.Count != 0)
            {
                dgvTaxSearch.DataSource = dsetTax.Tables[0];
                dgvTaxSearch.Update();
                int i = 0;
                foreach (DataRow row in dsetTax.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvTaxSearch.Columns)
                    {
                        if (dsetTax.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvTaxSearch.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblRowsCount.Text = "Total Records : " + dgvTaxSearch.Rows.Count;
            }
            else
            {
                lblRowsCount.Text = "Total Records : 0";
                ClearSearchGrid(dgvTaxSearch);
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

        private void dgvTaxSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["stax_id"].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
                SelectSearchGrid();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    if (dgvTaxSearch.Columns[e.ColumnIndex].Name == "tax_tran_nm")
                    {
                        strSearchField = "tran_nm";
                    }
                    else
                        strSearchField = dgvTaxSearch.Columns[e.ColumnIndex].Name;
                }
                else
                    strSearchField = "tax_nm";
            }
        }

        private void dgvTaxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["stax_id"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
                    DisplayControlsonMode("view_mode");
                    // dgvSearch.Focus();
                }
                else if (e.KeyData == Keys.Up && dgvView.CurrentRow.Index < 1)
                {
                    txtTaxSearch.Focus();
                }
                else
                {
                    // dgvSearch.Focus();
                }
            }
        }

        private void txtTaxSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtTaxSearch.Text != "")
                BindSearchGrid(txtTaxSearch.Text);
            else
                BindSearchGrid("%");
            SelectSearchGrid();
        }

        private void txtTaxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgvTaxSearch.Focus();
            }
        }

        private void txtTaxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvTaxSearch != null)
                {
                    objBASEFILEDS.Tran_id = dgvTaxSearch.CurrentRow.Cells["stax_id"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
                    DisplayControlsonMode("view_mode");
                    txtTaxSearch.Focus();
                }
            }
        }

        private void SelectSearchGrid()
        {
            dgvTaxSearch.ScrollBars = ScrollBars.Both;
            if (dgvTaxSearch.CurrentRow != null && objBASEFILEDS.Tran_id != "0")
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dgvTaxSearch.Rows)
                {
                    if (row.Cells["stax_id"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
                    {
                        rowIndex = row.Index;
                        // break;
                    }
                    dgvTaxSearch.Rows[row.Index].DefaultCellStyle.BackColor = dgvTaxSearch.DefaultCellStyle.BackColor;
                }
                if (rowIndex != -1)
                {
                    dgvTaxSearch.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            //frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id,ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "IVW_ACCOUNTS", "", "primary_id;ac_id,primary_nm;ac_nm", "primary_nm; Account Name", "Please Select", "", false, "", "0");
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtAcc_nm.Text = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
        }

        private void txtAcc_nm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "CM_MAST", "", "ac_id,ac_nm", "ac_nm;Account Name,ac_grp_nm; Account Group", "Please Select", "", false, "", "0");
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtAcc_nm.Text = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
            }
        }        
    }
}
