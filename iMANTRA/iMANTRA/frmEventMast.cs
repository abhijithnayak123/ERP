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
using iMANTRA_IL;
using CUSTOM_iMANTRA;
using System.Reflection;
using CUSTOM_iMANTRA_BL;
using System.Text.RegularExpressions;


namespace iMANTRA
{
    public partial class frmEventMast : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string strSearchField = "event_type";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

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

        public frmEventMast(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmProductGroupMast_Load(object sender, EventArgs e)
        {
            //  BindControls();
            GetAdditionalFieldsDetails();
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
        //private void BindControls()
        //{
        //    DataSet dsest = new DataSet();
        //    DataSet dset = new DataSet();
        //    string code = objBASEFILEDS.Code;

        //    dsest = objDBAdaper.dsquery("SELECT DISTINCT * FROM PROD_TYPE");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

        //    cmProd_Type.DataSource = dsest.Tables[0];
        //    cmProd_Type.DisplayMember = "prod_ty_nm";
        //    cmProd_Type.ValueMember = "prod_tyid";
        //    cmProd_Type.Update();

        //    dset = objDBAdaper.dsquery("SELECT DISTINCT * FROM [UOM]");

        //    cmbUOM.DataSource = dset.Tables[0];
        //    cmbUOM.DisplayMember = "uom";
        //    cmbUOM.ValueMember = "uom";
        //    cmbUOM.Update();

        //    dset = objDBAdaper.dsquery("SELECT DISTINCT * FROM STOCKABLE");

        //    cmbStockable.DataSource = dset.Tables[0];
        //    cmbStockable.DisplayMember = "Stockable";
        //    cmbStockable.ValueMember = "Stockable";
        //    cmbStockable.Update();
        //}
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
            objBASEFILEDS.HTMAIN["EVENT_CD"] = objBASEFILEDS.Tran_id;

            objBASEFILEDS.HTMAIN["EVENT_TYPE"] = txtEventType.Text;
            objBASEFILEDS.HTMAIN["TITLE"] = txtTitle.Text;
            objBASEFILEDS.HTMAIN["SPEAKER"] = txtSpeaker.Text;
            objBASEFILEDS.HTMAIN["VISITORS"] = txtVisitors.Text;
            objBASEFILEDS.HTMAIN["SHALL"] = txtSemHall.Text;
            objBASEFILEDS.HTMAIN["date"] = dtp_date.DtValue.ToString("yyyy/MM/dd"); 
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            objBASEFILEDS.HTMAIN["isdeactive"] = chkProd_active.Checked;
            objBASEFILEDS.HTMAIN["deactfrm"] = dtp_deactive_from.DtValue.ToString("yyyy/MM/dd"); 
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
        }
        private void GetFieldValueFromHashTable()
        {
            txtEventType.Text = objBASEFILEDS.HTMAIN["EVENT_TYPE"].ToString();
            txtTitle.Text = objBASEFILEDS.HTMAIN["TITLE"].ToString();
            txtSpeaker.Text = objBASEFILEDS.HTMAIN["SPEAKER"].ToString();
            txtVisitors.Text = objBASEFILEDS.HTMAIN["VISITORS"].ToString();
            txtSemHall.Text = objBASEFILEDS.HTMAIN["SHALL"].ToString();
            dtp_date.DtValue = objBASEFILEDS.HTMAIN["DATE"] != null && objBASEFILEDS.HTMAIN["DATE"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["DATE"].ToString()) : DateTime.Parse("01/01/1900");
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
            if (txtEventType.Text == "") { AutoClosingMessageBox.Show("Please enter event type.","Validation",3000); flg = false; }
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
                        dgvSearch.Enabled = false;
                        txtEventType.Focus();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        //BindControls();
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
                        dgvSearch.Enabled = false;
                        // txtmac_no.Enabled = false;
                        txtEventType.Focus();
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
                        OTHER_DET.Enabled = true;
                        txtSearch.Enabled = true;
                        dgvSearch.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                strSearchField = "event_type";
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

        private void frmProductGroupMast_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmProductGroupMast_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmProductGroupMast_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void OTHER_DET_Click(object sender, EventArgs e)
        {
            frmAddl_Info frmadd = new frmAddl_Info(objBASEFILEDS.HTMAIN, 0, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, OTHER_DET.Name, OTHER_DET.Text);
            frmadd.dset = objBASEFILEDS.dsBASEADDIFIELD;
            frmadd.ObjBSFD = objBASEFILEDS;
            frmadd.ShowDialog();
        }
        private void txtProd_gr_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtEventType.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Event Type","Validation",3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDBAdaper.dsquery("select event_cd,event_type,title from " + objBASEFILEDS.Main_tbl_nm + " where event_type ='" + txtEventType.Text + "' and event_cd!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Event Type is already exists","Validation",3000);
                        e.Cancel = true;
                    }
                }
            }
        }
        private void cmProd_Type_Validating(object sender, CancelEventArgs e)
        {
            //if (cmProd_Type.SelectedIndex == 0)
            //{
            //    AutoClosingMessageBox.Show("Please Select Valid Product Type","Validation",3000);
            //    e.Cancel = true;
            //}
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
            string strQuery = "select event_cd,event_type,title from " + objBASEFILEDS.Main_tbl_nm + " where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
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
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["EVENT_CD"].Value.ToString();
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
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["event_cd"].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
                SelectSearchGrid();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    strSearchField = dgvSearch.Columns[e.ColumnIndex].Name;
                    foreach (DataGridViewColumn row in dgvSearch.Columns)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                    dgvSearch.Columns[strSearchField].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }
        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["EVENT_CD"].Value.ToString();
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
                    if (row.Cells["EVENT_CD"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
