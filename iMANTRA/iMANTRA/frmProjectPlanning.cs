using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using iMANTRA_DL;
using iMANTRA_BL;
using iMANTRA_IL;
using CUSTOM_iMANTRA;
using CUSTOM_iMANTRA_BL;


namespace iMANTRA
{
    public partial class frmProjectPlanning : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        private int itserial = 0;
        string strSearchField = "prod_nm";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        DeleteItem objiDeleteItem = new DeleteItem();
        btn_event objiButtonEvent = new btn_event();//Sharanamma on 04.24.13,rg: btn click events

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

        public frmProjectPlanning(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = objBL.Code;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmProjectPlanning_Load(object sender, EventArgs e)
        {
            dgvProject.AutoGenerateColumns = false;

            objBASEFILEDS.HTMAIN.Clear();
            objBASEFILEDS.dsBASEFIELDMAIN = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Main_tbl_nm + " where 1=2");
            AddToHashTB(objBASEFILEDS.dsBASEFIELDMAIN, objBASEFILEDS.HTMAIN, 1);

            objBASEFILEDS.htitem_details.Clear();
            objBASEFILEDS.dsBASEFIELDITEM = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Item_tbl_nm + " where 1=2");
            AddToHashTB(objBASEFILEDS.dsBASEFIELDITEM, objBASEFILEDS.htitem_details, 0);

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

            objBASEFILEDS.dsBASEADDIFIELDITEM = new DataSet();
            objBASEFILEDS.dsBASEADDIFIELDITEM = objFLBaseField.GETCUSTOMFIELDFORGRID(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
            foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELDITEM.Tables[0].Rows)
            {
                if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                {
                    objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "0.00";
                }
                else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                {
                    objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToString("yyyy/MM/dd");
                }
                else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                {
                    objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                }
                else
                {
                    objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                }
            }
        }
        private void AddToHashTB(DataSet ds, Hashtable hb, int _mainOrItem)
        {
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                if (column.DataType.Name.ToString().ToLower() == "int32")
                {
                    if (dgvProject.Columns.Contains(column.ColumnName) && _mainOrItem == 0)
                    {
                        dgvProject.Columns[column.ColumnName].Tag = "int";
                    }
                    hb[column.ColumnName.Trim().ToUpper()] = "0";
                }
                if (column.DataType.Name.ToString().ToLower() == "boolean")
                {
                    if (dgvProject.Columns.Contains(column.ColumnName) && _mainOrItem == 0)
                    {
                        dgvProject.Columns[column.ColumnName].Tag = "bool";
                    }
                    hb[column.ColumnName.Trim().ToUpper()] = false;
                }
                if (column.DataType.Name.ToString().ToLower() == "string")
                {
                    if (dgvProject.Columns.Contains(column.ColumnName) && _mainOrItem == 0)
                    {
                        dgvProject.Columns[column.ColumnName].Tag = "string";
                    }
                    hb[column.ColumnName.Trim().ToUpper()] = "";
                }
                if (column.DataType.Name.ToString().ToLower() == "decimal")
                {
                    if (dgvProject.Columns.Contains(column.ColumnName) && _mainOrItem == 0)
                    {
                        dgvProject.Columns[column.ColumnName].Tag = "decimal";
                    }
                    hb[column.ColumnName.Trim().ToUpper()] = "0.00";
                }
                if (column.DataType.Name.ToString().ToLower() == "datetime")
                {
                    if (dgvProject.Columns.Contains(column.ColumnName) && _mainOrItem == 0)
                    {
                        dgvProject.Columns[column.ColumnName].Tag = "datetime";
                    }
                    hb[column.ColumnName.Trim().ToUpper()] = DateTime.Parse("1900/01/01");
                }
            }
        }
        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);
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
            objBASEFILEDS.HTITEM.Clear();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Item_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id + "");
            foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
            {
                if (!objBASEFILEDS.HTITEM.Contains(row["PTSERIAL"].ToString()))
                {
                    objBASEFILEDS.HTITEM[row["PTSERIAL"].ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DictionaryEntry entry in objBASEFILEDS.htitem_details)
                    {
                        ((Hashtable)objBASEFILEDS.HTITEM[row["PTSERIAL"].ToString()])[entry.Key] = entry.Value;
                    }
                }
                foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                {
                    if (((Hashtable)objBASEFILEDS.HTITEM[row["ptserial"].ToString()]).ContainsKey(column.ColumnName.Trim().ToUpper()))
                    {
                        ((Hashtable)objBASEFILEDS.HTITEM[row["ptserial"].ToString()])[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString();
                    }
                }
            }
        }
        private void InsertFieldValueToHashTable()
        {
            objBASEFILEDS.HTMAIN["project_id"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["prod_nm"] = txtProduct.Text;
            objBASEFILEDS.HTMAIN["wo_qty"] = txtWoQty.Text;
            objBASEFILEDS.HTMAIN["wo_no"] = txtWoNo.Text;
            objBASEFILEDS.HTMAIN["wo_ptserial"] = txtSerial.Text;
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;

            int i = 0;
            foreach (DataGridViewRow row in dgvProject.Rows)
            {
                objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry entry in objBASEFILEDS.htitem_details)
                {
                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[entry.Key] = entry.Value;
                }
                foreach (DataGridViewColumn column in dgvProject.Columns)
                {
                    if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()]).ContainsKey(column.Name.Trim().ToUpper()))
                    {
                        if (row.Cells[column.Name].Value != null)
                        {
                            if (column.Tag.ToString() != "datetime")
                            {
                                ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = row.Cells[column.Name].Value;
                            }
                            else
                            {
                                if (row.Cells[column.Name].Value.ToString() != "")
                                {
                                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = row.Cells[column.Name].Value;
                                }
                                else
                                {
                                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = "1900/01/01";
                                }
                            }
                        }
                        else if (row.Cells[column.Name].Value == null)
                        {
                            if (column.Tag.ToString() == "string")
                            {
                                ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = "";
                            }
                            else if (column.Tag.ToString() == "decimal")
                            {
                                ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = "0.00";
                            }
                            else if (column.Tag.ToString() == "datetime")
                            {
                                ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = "1900/01/01";
                            }
                        }
                    }
                }
                i++;
            }
            if (dgvProject.Rows.Count != 0)
            {
                itserial = int.Parse(dgvProject.Rows[--i].Cells["pro_order"].Value.ToString().Trim());
            }
        }
        private void GetFieldValueFromHashTable()
        {
            if (objBASEFILEDS.Tran_id != "0")
            {
                txtProduct.Text = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
                txtWoNo.Text = objBASEFILEDS.HTMAIN["wo_no"].ToString();
                txtSerial.Text = objBASEFILEDS.HTMAIN["wo_ptserial"].ToString();
                txtWoQty.Text = objBASEFILEDS.HTMAIN["wo_qty"].ToString();
                ClearGrid(dgvProject);
                int i = 0, itserial_internal = 0;
                foreach (DictionaryEntry entry in objBASEFILEDS.HTITEM)
                {
                    if (itserial_internal < int.Parse(entry.Key.ToString()))
                    {
                        itserial_internal = int.Parse(entry.Key.ToString());
                    }

                    dgvProject.Rows.Add();
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        if (dgvProject.Columns.Contains(entry1.Key.ToString()))
                        {
                            if (dgvProject.Columns[entry1.Key.ToString()].Tag.ToString() != "datetime")
                            {
                                dgvProject.Rows[i].Cells[entry1.Key.ToString()].Value = entry1.Value;
                            }
                            else
                            {
                                if (entry1.Value != null && entry1.Value.ToString() != "")
                                {
                                    dgvProject.Rows[i].Cells[entry1.Key.ToString()].Value = entry1.Value;
                                }
                                else
                                {
                                    dgvProject.Rows[i].Cells[entry1.Key.ToString()].Value = "1900/01/01";
                                }
                            }
                        }
                    }
                    i++;
                }
                if (dgvProject.Rows.Count != 0)
                {
                    itserial = itserial_internal;//int.Parse(dgvProcess.Rows[--i].Cells["ptserial"].Value.ToString().Trim());
                }
            }
            else
            {
                txtProduct.Text = "";
                txtWoNo.Text = "";
                txtSerial.Text = "";
                txtWoQty.Text = "0.00";
                ClearGrid(dgvProject);
                itserial = 0;
            }
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
            foreach (DictionaryEntry entry in objBASEFILEDS.HTITEM)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (((Hashtable)entry.Value)["pro_nm"] != null && ((Hashtable)entry.Value)["pro_nm"].ToString() != "")
                    {
                        ((Hashtable)entry.Value)["prod_cd"] = objBASEFILEDS.HTMAIN["prod_cd"].ToString();
                        ((Hashtable)entry.Value)["prod_nm"] = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
                        ((Hashtable)entry.Value)["fin_yr"] = objBASEFILEDS.HTMAIN["fin_yr"].ToString();
                        ((Hashtable)entry.Value)["compid"] = objBASEFILEDS.HTMAIN["compid"].ToString();
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter valid Process Name For Serial No : " + entry.Key.ToString(),"Validation",3000);
                        flg = false;
                        break;
                    }
                }
            }
            //  if (flg && txtProduct.Text == "") { AutoClosingMessageBox.Show("Please enter valid Product Name.","Validation",3000); flg = false; }
            return flg;
        }
        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                switch (tran_mode)
                {
                    case "add_mode":
                        foreach (Control con1 in this.Controls)
                        {
                            foreach (Control c in con1.Controls)
                            {
                                if (c is DataGridView)
                                {
                                    foreach (DataGridViewColumn col in dgvProject.Columns)
                                    {
                                        if (!(col is DataGridViewButtonColumn) && col.Visible == true)
                                        {
                                            col.ReadOnly = false;
                                        }
                                    }
                                }
                                else
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
                                    c.Enabled = true;
                                }
                            }
                        }
                        itserial = 0;
                        ClearGrid(dgvProject);
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        btnWO.Focus();
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
                                    if (c is DataGridView)
                                    {
                                        foreach (DataGridViewColumn col in dgvProject.Columns)
                                        {
                                            if (!(col is DataGridViewButtonColumn) && col.Visible == true)
                                            {
                                                col.ReadOnly = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        c.Enabled = true;
                                    }
                                }
                            }
                        }
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        btnWO.Enabled = false;
                        txtWoNo.Focus();
                        break;
                    case "view_mode":
                        // ClearGrid(dgvProject);
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
                                    if (c is DataGridView)
                                    {
                                        foreach (DataGridViewColumn col in dgvProject.Columns)
                                        {
                                            if (!(col is DataGridViewButtonColumn) && col.Visible == true)
                                            {
                                                col.ReadOnly = true;
                                            }
                                        }
                                    }
                                    else
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
                        }
                        txtSearch.Enabled = true;
                        dgvSearch.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                strSearchField = "prod_nm";
                if (txtSearch.Text != "")
                    BindSearch(txtSearch.Text);
                else
                    BindSearch("%"); 
                if (objBASEFILEDS.Tran_mode != "add_mode")
                {
                    SelectSearchGrid();
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception",3000);
            }
        }      
        private void frmProjectPlanning_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmProjectPlanning_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (objBASEFILEDS.Code == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmProjectPlanning_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }        

        private void dgvProject_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
            {
                dgvProject.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (dgvProject != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {

                }
            }
        }
        private void dgvProject_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvProject.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        private void dgvProject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    DataGridViewButtonCell b = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                    if (b != null)
                    {
                        frmAddl_Info objfrmAddl_Info = new frmAddl_Info(((Hashtable)objBASEFILEDS.HTITEM[(dgv.CurrentRow.Cells["PTSERIAL"].Value)]), 1, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, b.OwningColumn.Name, b.OwningColumn.HeaderText);
                        objfrmAddl_Info.dset = objBASEFILEDS.dsBASEADDIFIELDITEM;
                        objfrmAddl_Info.ObjBSFD = objBASEFILEDS;
                        objfrmAddl_Info.ShowDialog();

                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception",3000);
            }
        }
        private void dgvProject_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    DataGridView dgv = sender as DataGridView;
                    if (dgv != null)
                    {
                        DataGridViewButtonCell b = dgv.CurrentCell as DataGridViewButtonCell;
                        if (b != null)
                        {
                            frmAddl_Info objfrmAddl_Info = new frmAddl_Info(((Hashtable)objBASEFILEDS.HTITEM[(dgv.CurrentRow.Cells["PTSERIAL"].Value)]), 1, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, b.OwningColumn.Name, b.OwningColumn.HeaderText);
                            objfrmAddl_Info.dset = objBASEFILEDS.dsBASEADDIFIELDITEM;
                            objfrmAddl_Info.ObjBSFD = objBASEFILEDS;
                            objfrmAddl_Info.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtProduct_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                e.Cancel = checkProductValid();
            }
        }
        private bool checkProductValid()
        {
            if (txtProduct.Text == "")
            {
                AutoClosingMessageBox.Show("Please enter valid Product.","Validation",3000);
                // return true;
            }
            //else
            //{
            //    DataSet ds = objDBAdaper.dsquery("select prod_cd,prod_nm from PMMAIN where prod_nm ='" + txtProduct.Text + "' and " + objBASEFILEDS.Primary_id + "!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
            //    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            //    {
            //        AutoClosingMessageBox.Show("Product already exists","Validation",3000);
            //        return true;
            //    }
            //    else
            //    {
            //        DataSet dsetProd = objDBAdaper.dsquery("select prod_nm,prod_desc from PT_MAST where prod_cd='" + objBASEFILEDS.HTMAIN["prod_cd"].ToString() + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
            //        if (dsetProd != null && dsetProd.Tables.Count != 0 && dsetProd.Tables[0].Rows.Count != 0)
            //        {
            //            txtDesc.Text = dsetProd.Tables[0].Rows[0]["prod_desc"].ToString();
            //        }
            //    }
            //}
            return false;
        }
        private void BindControlsFromView()
        {
            foreach (Control c in this.Controls[1].Controls)
            {
                if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                {
                    if (c is PictureBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                        {
                            ((PictureBox)c).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c.Name].ToString());
                        }
                    }
                    else if (c is CheckBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                        {
                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "0")
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = "false";
                            }
                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "1")
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = "true";
                            }
                            ((CheckBox)c).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                        }
                    }
                    else if (c is ComboBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                        {
                            ((ComboBox)c).Text = objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember].ToString();
                            ((ComboBox)c).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember].ToString();
                        }
                    }
                    else
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                        {
                            c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                        }
                    }
                }

                foreach (DataGridViewRow row in dgvProject.Rows)
                {
                    foreach (DataGridViewColumn column in dgvProject.Columns)
                    {
                        if (objBASEFILEDS.HTITEM.Contains(row.Cells["PTSERIAL"].Value.ToString()))
                        {
                            if (column is POPUPTEXTBOX_FOR_GRID)
                            {
                                if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] != null)
                                {
                                    if (column.Tag.ToString() == "decimal")
                                    {
                                        row.Cells[column.Name].Value = decimal.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString());
                                    }
                                    else
                                    {
                                        row.Cells[column.Name].Value = ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString();
                                    }
                                }
                            }
                            else if (column is POPUPDATETIME_FOR_GRID)
                            {
                                if (row.Cells[column.Name].Value != null && row.Cells[column.Name].Value.ToString() != "")
                                {
                                    row.Cells[column.Name].Value = Convert.ToDateTime(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString());
                                }
                                else
                                {
                                    row.Cells[column.Name].Value = Convert.ToDateTime("1900/01/01");
                                }
                            }
                        }
                    }
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

        private void btnWO_Click(object sender, EventArgs e)
        {
            frmWO_List objWO_List = new frmWO_List(objBASEFILEDS);
            objWO_List.ShowDialog();
            txtWoNo.Text = objBASEFILEDS.HTMAIN["wo_no"].ToString();
            txtSerial.Text = objBASEFILEDS.HTMAIN["wo_ptserial"].ToString();
            txtProduct.Text = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
            txtWoQty.Text = objBASEFILEDS.HTMAIN["wo_qty"].ToString();
            BindProjectGrid();
        }
        private void BindProjectGrid()
        {
            ClearGrid(dgvProject);
            dgvProject.AutoGenerateColumns = false;
            DataSet dsetProcessList;
            string strQuery = "";

            if (objBASEFILEDS.Tran_mode == "add_mode")
            {
                strQuery = "SELECT PRO_ORDER,PRO_ORDER_ID,PRO_NM,PRO_DESC,PRO_SETUP,PRO_CYCLE,MAC_TYPE,MAC_TYPE_ID,OPT_TYPE,OPT_ID,PRO_COST,PTSERIAL FROM PMITEM WHERE COMPID='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' AND PROD_CD='" + objBASEFILEDS.HTMAIN["PROD_CD"].ToString() + "' AND PROD_NM='" + objBASEFILEDS.HTMAIN["PROD_NM"].ToString() + "'";
            }
            else
            {
                strQuery = "SELECT project_order_id,WO_ID,WO_NO,WO_CD,WO_PTSERIAL,JPMAIN.PROD_CD,JPMAIN.PROD_NM,PRO_ORDER,PRO_ORDER_ID,PRO_NM,PRO_DESC,PRO_SETUP,PRO_CYCLE,MAC_TYPE,MAC_TYPE_ID,OPT_TYPE,OPT_ID,PRO_COST,PTSERIAL FROM JPMAIN INNER JOIN JPITEM ON JPMAIN.TRAN_CD=JPITEM.TRAN_CD AND JPMAIN.PROJECT_ID=JPITEM.PROJECT_ID WHERE COMPID='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' AND PROJECT_ID='" + objBASEFILEDS.Tran_id + "'";
            }

            dsetProcessList = objDBAdaper.dsquery(strQuery);
            if (dsetProcessList != null && dsetProcessList.Tables.Count != 0 && dsetProcessList.Tables[0].Rows.Count != 0)
            {
                //dgvProject.DataSource = dsetProcessList.Tables[0];
                //dgvProject.Update();
                int i = 0;
                foreach (DataRow row in dsetProcessList.Tables[0].Rows)
                {
                    dgvProject.Rows.Add();
                    foreach (DataGridViewColumn column in dgvProject.Columns)
                    {
                        if (dsetProcessList.Tables[0].Columns.Contains(column.Name))
                        {
                            //dgvProject.Rows[i].Cells[column.Name].Value = row[column.Name];

                            if (dgvProject.Columns[column.Name].Tag.ToString() != "datetime")
                            {
                                dgvProject.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                            else
                            {
                                if (row[column.Name] != null && row[column.Name].ToString() != "")
                                {
                                    dgvProject.Rows[i].Cells[column.Name].Value = row[column.Name];
                                }
                                else
                                {
                                    dgvProject.Rows[i].Cells[column.Name].Value = "1900/01/01";
                                }
                            }
                        }
                    }
                    i++;
                }
            }
        }
        private void cmdProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void BindSearch(string strMessage)
        {
            string strQuery = "select project_id,prod_nm,wo_no from " + objBASEFILEDS.Main_tbl_nm + " where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
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
                ClearGrid(dgvSearch);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                BindSearch(txtSearch.Text);
            else
                BindSearch("%");
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
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["project_id"].Value.ToString();
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
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["project_id"].Value.ToString();
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
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["project_id"].Value.ToString();
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
                    if (row.Cells["project_id"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
