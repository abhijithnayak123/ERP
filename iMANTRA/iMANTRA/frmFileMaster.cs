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
    public partial class frmFileMaster : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, _tran_id = "0";
        string strSearchField = "caption_nm";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

       // string key = "";

        public string Tran_id
        {
            get { return _tran_id; }
            set { _tran_id = value; }
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

        public frmFileMaster(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmFileMaster_Load(object sender, EventArgs e)
        {
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
        }
        private void frmFileMaster_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmFileMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmFileMaster_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
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
            if (txtTransaction_nm.Text == "") { AutoClosingMessageBox.Show("Please Enter Valid Transaction Name","Validation",3000); flg = false; }
            else if (txtCaption.Text == "") { AutoClosingMessageBox.Show("Please enter Caption","Validation",3000); flg = false; }
            flg = FileUploadPathExistance();
            return flg;
        }
        private bool FileUploadPathExistance()
        {
            DataSet dsetFile = objDBAdaper.dsquery("select * from FILE_MAST where code='" + objBASEFILEDS.HTMAIN["code"].ToString() + "' and typewise='" + objBASEFILEDS.HTMAIN["typewise"].ToString() + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            if (dsetFile != null && dsetFile.Tables.Count != 0 && dsetFile.Tables[0].Rows.Count != 0)
            {
                AutoClosingMessageBox.Show("Sorry File Upload Button already exists in specified Transaction","Validation",3000);
                return false;
            }
            return true;
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
                        rbtnHeaderWise.Checked = true;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtCaption.Focus();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        GetFieldValueFromHashTable();
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
                        txtTransaction_nm.Enabled = false;
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
                            if (con1 is UCToolBar)
                            {
                            }
                            else
                            {
                                foreach (Control c in con1.Controls)
                                {
                                    if (c is UserDT)
                                    {
                                        ((UserDT)c).bUpdateFlag = true;
                                    }
                                    if (!(c is Label)) c.Enabled = false;
                                }
                            }
                        }
                        txtSearch.Enabled = true;
                        dgvSearch.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                strSearchField = "caption_nm";
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
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            objBASEFILEDS.HTMAIN["typewise"] = rbtnHeaderWise.Checked ? true : false;
            objBASEFILEDS.HTMAIN["caption_nm"] = txtCaption.Text;
            objBASEFILEDS.HTMAIN["tran_nm"] = txtTransaction_nm.Text;
            //  objBASEFILEDS.HTMAIN["valid_mast"] = txtValidIn.Text;
            objBASEFILEDS.HTMAIN["fld_nm"] = "FILE_UPLOAD_BTN";
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid;
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr;
        }
        private void GetFieldValueFromHashTable()
        {
            rbtnHeaderWise.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString());
            rbtnDetailsWise.Checked = bool.Parse(objBASEFILEDS.HTMAIN["typewise"].ToString()) ? false : true;
            txtCaption.Text = objBASEFILEDS.HTMAIN["caption_nm"].ToString();
            txtTransaction_nm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
            //   txtValidIn.Text = objBASEFILEDS.HTMAIN["valid_mast"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isFileAttach='1'",false,"", "0");
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtTransaction_nm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
        }
        private void txtTransaction_nm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "tran_set", "", "code,tran_nm", "code;Transaction Code,tran_nm;Trasaction Name", "Please Select", "tran_type='Transaction' and isFileAttach='1'",false,"", "0");
                    //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objpopup.ObjBFD = objBASEFILEDS;
                    objpopup.ShowDialog();
                    txtTransaction_nm.Text = objBASEFILEDS.HTMAIN["tran_nm"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtValidIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                frmListOfMenus objfrmListMenus = new frmListOfMenus();
                objfrmListMenus.Type = "1";
                objfrmListMenus.Validity_fld_nm = "valid_mast";
                objfrmListMenus.Condition = "isFileAttach='1'";
                objfrmListMenus.ObjBFD = objBASEFILEDS;
                objfrmListMenus.ShowDialog();
                // txtValidIn.Text = objBASEFILEDS.HTMAIN["valid_mast"].ToString();
            }
        }
        private void btnValidIn_Click(object sender, EventArgs e)
        {
            frmListOfMenus objfrmListMenus = new frmListOfMenus();
            objfrmListMenus.Type = "1";
            objfrmListMenus.Validity_fld_nm = "valid_mast";
            objfrmListMenus.Condition = "isFileAttach='1'";
            objfrmListMenus.ObjBFD = objBASEFILEDS;
            objfrmListMenus.ShowDialog();
            //  txtValidIn.Text = objBASEFILEDS.HTMAIN["valid_mast"].ToString();
        }

        private void txtTransaction_nm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtTransaction_nm.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Valid Transaction Name","Validation",3000);
                    e.Cancel = true;
                }
            }
        }
        private void txtCaption_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtCaption.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter valid Caption","Validation",3000);
                    e.Cancel = true;
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
            string strQuery = "select tran_id,caption_nm,tran_nm from FILE_MAST where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
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
        private void SelectSearchGrid()
        {
            dgvSearch.ScrollBars = ScrollBars.Both;
            if (dgvSearch.CurrentRow != null && objBASEFILEDS.Tran_id != "0")
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dgvSearch.Rows)
                {
                    if (row.Cells["tran_id"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                BindSearchGrid(txtSearch.Text);
            else
                BindSearchGrid("%");
            SelectSearchGrid();
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvSearch != null)
                {
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["tran_id"].Value.ToString();
                    DisplayControlsonMode("view_mode");
                    txtSearch.Focus();
                }
            }
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgvSearch.Focus();
            }
        }
        private void dgvSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["tran_id"].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
                SelectSearchGrid();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    strSearchField = dgvSearch.Columns[e.ColumnIndex].Name;
                }
                else
                    strSearchField = "caption_nm";
            }
        }
        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["tran_id"].Value.ToString();
                    DisplayControlsonMode("view_mode");
                }
                else if (e.KeyData == Keys.Up && dgvView.CurrentRow.Index < 1)
                {
                    txtSearch.Focus();
                }
            }
        }
    }
}
