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
    public partial class frmQCCheckPoints : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        private int itserial = 0;
        string strSearchField = "prod_nm";

        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        DeleteItem objiDeleteItem = new DeleteItem();

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

        public frmQCCheckPoints(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = objBL.Code;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmQCCheckPoints_Load(object sender, EventArgs e)
        {
            dgvCheckPoint.AutoGenerateColumns = false;

            objBASEFILEDS.HTMAIN.Clear();
            objBASEFILEDS.dsBASEFIELDMAIN = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Main_tbl_nm + " where 1=2");
            AddToHashTB(objBASEFILEDS.dsBASEFIELDMAIN, objBASEFILEDS.HTMAIN);

            objBASEFILEDS.htitem_details.Clear();
            objBASEFILEDS.dsBASEFIELDITEM = objDBAdaper.dsquery("select * from " + objBASEFILEDS.Item_tbl_nm + " where 1=2");
            AddToHashTB(objBASEFILEDS.dsBASEFIELDITEM, objBASEFILEDS.htitem_details);

            GetAdditionalFieldsDetails();

            foreach (DataGridViewColumn col in dgvCheckPoint.Columns)
            {
                if (col.Name == "priority_nm")
                {
                    POPUPTEXTBOX_FOR_GRID txtcol = (POPUPTEXTBOX_FOR_GRID)col;
                    txtcol.Dispddlfields = "priority_nm;Priority";
                    txtcol.Primaryddl = "priority_id,priority_nm";
                    txtcol.Query_con = "";
                    txtcol.Tbl_nm = "priority";
                }
                if (col.Name == "uom")
                {
                    POPUPTEXTBOX_FOR_GRID txtcol = (POPUPTEXTBOX_FOR_GRID)col;
                    txtcol.Dispddlfields = "uom;Unit Of Measurement";
                    txtcol.Primaryddl = "uom,uom";
                    txtcol.Query_con = "";
                    txtcol.Tbl_nm = "UOM";
                }
                if (col.Name == "tolminus" || col.Name == "tolplus" || col.Name == "chklst_value")
                {
                    dgvCheckPoint.Columns[col.Name].Tag = "decimal";
                }
                else
                {
                    dgvCheckPoint.Columns[col.Name].Tag = "string";
                }
            }

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
        private void frmQCCheckPoints_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
            // frmQCCheckPoints
        }
        private void frmQCCheckPoints_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (objBASEFILEDS.Code == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmQCCheckPoints_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
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
        private void AddToHashTB(DataSet ds, Hashtable hb)
        {
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                if (column.DataType.Name.ToString().ToLower() == "int32")
                {
                    hb[column.ColumnName.Trim().ToUpper()] = "0";
                }
                if (column.DataType.Name.ToString().ToLower() == "boolean")
                {
                    hb[column.ColumnName.Trim().ToUpper()] = false;
                }
                if (column.DataType.Name.ToString().ToLower() == "string")
                {
                    hb[column.ColumnName.Trim().ToUpper()] = "";
                }
                if (column.DataType.Name.ToString().ToLower() == "decimal")
                {
                    hb[column.ColumnName.Trim().ToUpper()] = "0.00";
                }
                if (column.DataType.Name.ToString().ToLower() == "datetime")
                {
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
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Item_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id + ",chklst_order");
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
            objBASEFILEDS.HTMAIN["chklstId"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["prod_nm"] = txtProduct.Text;
            objBASEFILEDS.HTMAIN["chklist_prod_desc"] = txtDesc.Text;
            objBASEFILEDS.HTMAIN["chklist_remark"] = txtRemarks.Text;
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;

            int i = 0;
            foreach (DataGridViewRow row in dgvCheckPoint.Rows)
            {
                objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry entry in objBASEFILEDS.htitem_details)
                {
                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[entry.Key] = entry.Value;
                }
                foreach (DataGridViewColumn column in dgvCheckPoint.Columns)
                {
                    if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()]).ContainsKey(column.Name.Trim().ToUpper()))
                    {
                        if (row.Cells[column.Name].Value != null)
                        {
                            ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name.Trim().ToUpper()] = row.Cells[column.Name].Value;
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
                        }
                    }
                }
                i++;
            }
            if (dgvCheckPoint.Rows.Count != 0)
            {
                itserial = int.Parse(dgvCheckPoint.Rows[--i].Cells["chklst_order"].Value.ToString().Trim());
            }
        }
        private void GetFieldValueFromHashTable()
        {
            if (objBASEFILEDS.Tran_id != "0")
            {
                txtProduct.Text = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
                txtDesc.Text = objBASEFILEDS.HTMAIN["chklist_prod_desc"].ToString();
                txtRemarks.Text = objBASEFILEDS.HTMAIN["chklist_remark"].ToString();
                ClearSearchdgvProcess(dgvCheckPoint);
                int i = 0, itserial_internal = 0;
                foreach (DictionaryEntry entry in objBASEFILEDS.HTITEM)
                {
                    if (itserial_internal < int.Parse(entry.Key.ToString()))
                    {
                        itserial_internal = int.Parse(entry.Key.ToString());
                    }

                    dgvCheckPoint.Rows.Add();
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        if (dgvCheckPoint.Columns.Contains(entry1.Key.ToString()))
                        {
                            dgvCheckPoint.Rows[i].Cells[entry1.Key.ToString()].Value = entry1.Value;
                        }
                    }
                    i++;
                }
                if (dgvCheckPoint.Rows.Count != 0)
                {
                    itserial = itserial_internal;//int.Parse(dgvProcess.Rows[--i].Cells["ptserial"].Value.ToString().Trim());
                }
            }
            else
            {
                txtProduct.Text = "";
                txtDesc.Text = "";
                txtRemarks.Text = "";
                ClearSearchdgvProcess(dgvCheckPoint);
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
                    if (((Hashtable)entry.Value)["chklst_nm"] != null && ((Hashtable)entry.Value)["chklst_nm"].ToString() != "")
                    {
                        ((Hashtable)entry.Value)["prod_cd"] = objBASEFILEDS.HTMAIN["prod_cd"].ToString();
                        ((Hashtable)entry.Value)["prod_nm"] = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
                        ((Hashtable)entry.Value)["fin_yr"] = objBASEFILEDS.HTMAIN["fin_yr"].ToString();
                        ((Hashtable)entry.Value)["compid"] = objBASEFILEDS.HTMAIN["compid"].ToString();
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter valid Check Point List Name For Serial No : " + entry.Key.ToString(), "Validation", 3000);
                        flg = false;
                        break;
                    }
                }
            }
            if (flg && txtProduct.Text == "") { AutoClosingMessageBox.Show("Please enter valid Product Name.", "Validation", 3000); flg = false; }
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
                                    foreach (DataGridViewColumn col in dgvCheckPoint.Columns)
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
                        ClearSearchdgvProcess(dgvCheckPoint);
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        btnAdd.Enabled = true;
                        btnRemove.Enabled = true;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtProduct.Focus();
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
                                        foreach (DataGridViewColumn col in dgvCheckPoint.Columns)
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
                        btnAdd.Enabled = true;
                        btnRemove.Enabled = true;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtProduct.Enabled = false;
                        btnProdNm.Enabled = false;
                        txtRemarks.Focus();

                        break;
                    case "view_mode":
                        ClearSearchdgvProcess(dgvCheckPoint);
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
                                        foreach (DataGridViewColumn col in dgvCheckPoint.Columns)
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
                        btnAdd.Enabled = false;
                        btnRemove.Enabled = false;
                        txtSearch.Enabled = true;
                        dgvSearch.Enabled = true;
                        txtSearch.Focus();
                        break;
                    default: break;
                }
                strSearchField = "prod_nm";
                if (txtSearch.Text != "")
                    BindSearchdgvProcess(txtSearch.Text);
                else
                    BindSearchdgvProcess("%");
                if (objBASEFILEDS.Tran_mode != "add_mode")
                {
                    SelectSearchGrid();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnProdNm_Click(object sender, EventArgs e)
        {
            frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "pt_mast", "", "prod_cd,prod_nm", "prod_nm;Product Name,prod_desc;Desription", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
            //objpopup.objCompany = objBASEFILEDS.ObjCompany;
            //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
            objpopup.ObjBFD = objBASEFILEDS;
            objpopup.ShowDialog();
            txtProduct.Text = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
            if (checkProductValid())
            {
                txtProduct.Focus();
            }
        }

        private void txtProduct_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                e.Cancel = checkProductValid();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                itserial += 1;
                objBASEFILEDS.HTITEM[itserial.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry entry in objBASEFILEDS.htitem_details)
                {
                    ((Hashtable)objBASEFILEDS.HTITEM[itserial.ToString()])[entry.Key] = entry.Value;
                }
                dgvCheckPoint.Rows.Add(1);
                foreach (DataGridViewColumn col in dgvCheckPoint.Columns)
                {
                    if (col.Visible && (col.DisplayIndex == 1))
                    {
                        dgvCheckPoint.Rows[dgvCheckPoint.Rows.Count - 1].Cells[col.Name].Selected = true;
                        dgvCheckPoint.CurrentCell = dgvCheckPoint[col.Name, dgvCheckPoint.Rows.Count - 1];
                        break;
                    }
                }

                dgvCheckPoint["chklst_order", dgvCheckPoint.Rows.Count - 1].Value = itserial.ToString();
                dgvCheckPoint["ptserial", dgvCheckPoint.Rows.Count - 1].Value = itserial.ToString();
                dgvCheckPoint["TRAN_CD", dgvCheckPoint.Rows.Count - 1].Value = objBASEFILEDS.Code;
                ((Hashtable)objBASEFILEDS.HTITEM[itserial.ToString()])["ptserial"] = itserial.ToString();
                ((Hashtable)objBASEFILEDS.HTITEM[itserial.ToString()])["chklst_order"] = itserial.ToString();
                ((Hashtable)objBASEFILEDS.HTITEM[itserial.ToString()])["TRAN_CD"] = objBASEFILEDS.Code;
                ((Hashtable)objBASEFILEDS.HTITEM[itserial.ToString()])["chklst_order_id"] = "0";

                foreach (DictionaryEntry entry in objBASEFILEDS.htitem_details)
                {
                    if (entry.Value != null && entry.Value.ToString().Trim() != "" && dgvCheckPoint.Columns.Contains(entry.Key.ToString().Trim()))
                    {
                        dgvCheckPoint[entry.Key.ToString().Trim().ToUpper(), dgvCheckPoint.Rows.Count - 1].Value = entry.Value;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (objBASEFILEDS.Item_tbl_nm != "")
                {
                    if (dgvCheckPoint.CurrentRow.Index != -1)
                    {
                        if (this.tran_mode == "edit_mode")
                        {
                            //if (!objFL_Transaction.Find_Reference(objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id.ToString()].ToString(), objBASEFILEDS.HTMAIN["TRAN_NO"].ToString(), objBASEFILEDS.Code, dgvProcess.Rows[dgvProcess.CurrentRow.Index].Cells["PTSERIAL"].Value.ToString()))
                            //{
                            //    AutoClosingMessageBox.Show("Sorry! We can't delete item because it is refered in the other transaction","Remove Product",3000);
                            //    return;
                            //}
                        }
                        DialogResult result = MessageBox.Show("Are you sure to delete Process : " + dgvCheckPoint.CurrentRow.Cells["chklst_nm"].Value, "Delete Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)
                        {
                            //sharanamma on 30.04.13 added new class in customerlayer
                            if (objiDeleteItem.GetType().GetMethod("DeleteTransactionItem") != null)
                            {
                                objiDeleteItem.Hashitemvalue = ((Hashtable)objBASEFILEDS.HTITEM[dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                                objiDeleteItem.ACTIVE_BL = objBASEFILEDS;
                                MethodInfo methodInfo = typeof(DeleteItem).GetMethod("DeleteTransactionItem");
                                bool validflg = bool.Parse(methodInfo.Invoke(objiDeleteItem, null).ToString().Trim());
                                if (validflg)
                                {
                                    ((Hashtable)objBASEFILEDS.HTITEM[dgvCheckPoint.Rows[dgvCheckPoint.CurrentRow.Index].Cells["PTSERIAL"].Value.ToString()]).Clear();
                                    foreach (DictionaryEntry entry in objBASEFILEDS.HtPur_Ref)
                                    {
                                        if (entry.Key.ToString().Split(',')[0] == dgvCheckPoint.Rows[dgvCheckPoint.CurrentRow.Index].Cells["PTSERIAL"].Value.ToString())
                                        {
                                            objBASEFILEDS.HtPur_Ref.Remove(entry.Key);
                                            break;
                                        }
                                    }
                                    dgvCheckPoint.Rows.RemoveAt(dgvCheckPoint.CurrentRow.Index);
                                }
                                if (!validflg)
                                {
                                    if (objiDeleteItem.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        AutoClosingMessageBox.Show(objiDeleteItem.BL_FIELDS.Errormsg, "Remove Product", 3000);
                                    }
                                }
                                else
                                {
                                    Hashtable HTITEMVal = objBASEFILEDS.HTITEM;
                                    Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in HTITEMVal)
                                    {
                                        htcuritem.Clear();
                                        foreach (DictionaryEntry entry1 in objiDeleteItem.Hashitemvalue)
                                        {
                                            htcuritem.Add(entry1.Key, entry1.Value);
                                        }
                                        foreach (DictionaryEntry entry1 in htcuritem)
                                        {
                                            if (dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value.ToString() == entry.Key.ToString())
                                            {
                                                if (((Hashtable)objBASEFILEDS.HTITEM[entry.Key]).Contains(entry1.Key))
                                                {
                                                    ((Hashtable)objBASEFILEDS.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                                                }
                                            }
                                        }
                                    }
                                    BindControlsFromView();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Please add Product....", "No Product", 3000);
            }
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

                foreach (DataGridViewRow row in dgvCheckPoint.Rows)
                {
                    foreach (DataGridViewColumn column in dgvCheckPoint.Columns)
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
                        }
                    }
                }
            }
        }

        private void dgvProcess_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgvProcessview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = dgvProcessview.CurrentCell.OwningColumn.Name.ToString().Trim();
                txt.Tag = dgvProcessview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                if (txt.Tag.ToString().Trim() == "decimal")
                {
                    txt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    txt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
                else
                {
                    txt.KeyDown -= new KeyEventHandler(txt_key_down);
                    txt.KeyDown += new KeyEventHandler(txt_key_down);
                }
            }
        }
        private void dgvProcess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCheckPoint.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        private void dgvProcess_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
            {
                dgvCheckPoint.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (dgvCheckPoint != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (((Hashtable)(objBASEFILEDS.HTITEM[(dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value.ToString())])).ContainsKey(dgvCheckPoint.CurrentCell.OwningColumn.Name))
                    {
                        if ((dgvCheckPoint.CurrentCell.OwningColumn.Name == "chklst_order") || (dgvCheckPoint.CurrentRow.Cells["chklst_nm"].Value != null && dgvCheckPoint.CurrentRow.Cells["chklst_nm"].Value.ToString() != ""))
                        {
                            ((Hashtable)(objBASEFILEDS.HTITEM[(dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value.ToString())]))[dgvCheckPoint.CurrentCell.OwningColumn.Name] = e.FormattedValue.ToString().Trim();
                        }
                    }
                }
            }
        }
        private void dgvCheckPoint_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCheckPoint.CurrentCell.OwningColumn.Name == "priority_nm" || dgvCheckPoint.CurrentCell.OwningColumn.Name == "uom")
            {
                lblF2.Visible = true;
            }
        }
        private void dgvCheckPoint_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCheckPoint.CurrentCell.OwningColumn.Name == "priority_nm" || dgvCheckPoint.CurrentCell.OwningColumn.Name == "uom")
            {
                lblF2.Visible = false;
            }
        }
        private void dgvCheckPoint_CellClick(object sender, DataGridViewCellEventArgs e)
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
                AutoClosingMessageBox.Show(ex.Message, "Exception", 3000);
            }
        }
        private void dgvCheckPoint_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    POPUPTEXTBOX_FOR_GRID dgvProcesscolumncod = (POPUPTEXTBOX_FOR_GRID)dgvCheckPoint.Columns[dgvCheckPoint.CurrentCell.ColumnIndex];
                    frmPopup objfrmPopup = new frmPopup(((Hashtable)objBASEFILEDS.HTITEM[(dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value)]), dgvProcesscolumncod.Tbl_nm, dgvProcesscolumncod.Reftbltran_cd, dgvProcesscolumncod.Primaryddl, dgvProcesscolumncod.Dispddlfields, "Please select", dgvProcesscolumncod.Query_con, dgvProcesscolumncod.IsQcd, dgvProcesscolumncod.QcdCondition);
                    //objfrmPopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmPopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmPopup.ObjBFD = objBASEFILEDS;
                    objfrmPopup.ShowDialog();
                    txt.Text = ((Hashtable)objBASEFILEDS.HTITEM[(dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value)])[txt.Name].ToString().Trim();
                    foreach (string str in dgvProcesscolumncod.Primaryddl.Split(','))
                    {
                        dgvCheckPoint.CurrentRow.Cells[str].Value = ((Hashtable)objBASEFILEDS.HTITEM[(dgvCheckPoint.CurrentRow.Cells["PTSERIAL"].Value)])[str];
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txt_Key_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Tag.ToString().Trim() == "decimal")
                {
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
            }
            catch (Exception ex)
            {

            }
        }
        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "pt_mast", "", "prod_cd,prod_nm", "prod_nm;Product Name,prod_desc;Desription", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
                    //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objpopup.ObjBFD = objBASEFILEDS;
                    objpopup.ShowDialog();
                    txtProduct.Text = objBASEFILEDS.HTMAIN["prod_nm"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvProcess_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvCheckPoint.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        private bool checkProductValid()
        {
            if (txtProduct.Text == "")
            {
                AutoClosingMessageBox.Show("Please enter valid Product.", "Validation", 3000);
                return true;
            }
            else
            {
                DataSet ds = objDBAdaper.dsquery("select prod_cd,prod_nm from " + objBASEFILEDS.Main_tbl_nm + " where prod_nm ='" + txtProduct.Text.Replace("'", "''") + "' and " + objBASEFILEDS.Primary_id + "!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("Product already exists", "Validation", 3000);
                    return true;
                }
                else
                {
                    DataSet dsetProd = objDBAdaper.dsquery("select prod_nm,prod_desc from PT_MAST where prod_cd='" + objBASEFILEDS.HTMAIN["prod_cd"].ToString() + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (dsetProd != null && dsetProd.Tables.Count != 0 && dsetProd.Tables[0].Rows.Count != 0)
                    {
                        txtDesc.Text = dsetProd.Tables[0].Rows[0]["prod_desc"].ToString();
                    }
                }
            }
            return false;
        }

        private void OTHER_DET_Click(object sender, EventArgs e)
        {
            frmAddl_Info frmadd = new frmAddl_Info(objBASEFILEDS.HTMAIN, 0, objBASEFILEDS.Code, objBASEFILEDS.Tran_mode, OTHER_DET.Name, OTHER_DET.Text);
            frmadd.dset = objBASEFILEDS.dsBASEADDIFIELD;
            frmadd.ObjBSFD = objBASEFILEDS;
            frmadd.ShowDialog();
        }

        private void ClearSearchdgvProcess(DataGridView dgv)
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
        private void BindSearchdgvProcess(string strMessage)
        {
            string strQuery = "select chklstId,prod_nm,chklist_prod_desc from " + objBASEFILEDS.Main_tbl_nm + " where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
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
                ClearSearchdgvProcess(dgvSearch);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                BindSearchdgvProcess(txtSearch.Text);
            else
                BindSearchdgvProcess("%");
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
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["chklstId"].Value.ToString();
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
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["chklstId"].Value.ToString();
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
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["chklstId"].Value.ToString();
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
                    if (row.Cells["chklstId"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
