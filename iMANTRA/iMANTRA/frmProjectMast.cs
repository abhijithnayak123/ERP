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

namespace iMANTRA
{
    public partial class frmProjectMast : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private FL_BASEFIELD objFLBaseField = new FL_BASEFIELD();

        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        string strSearchField = "proj_no";

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

        public frmProjectMast(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmProjectMast_Load(object sender, EventArgs e)
        {
            BindControls();
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
        private void BindControls()
        {
            DataSet dsest = new DataSet();
            DataSet dset = new DataSet();
            string code = objBASEFILEDS.Code;

            dsest = objDBAdaper.dsquery("SELECT * FROM PROJECT_TYPE");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            cmbproj_type.DataSource = dsest.Tables[0];
            cmbproj_type.DisplayMember = "proj_ty_nm";
            cmbproj_type.ValueMember = "proj_ty_nm";
            cmbproj_type.Update();
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
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = DateTime.Parse("01/01/1900");
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
            objBASEFILEDS.HTMAIN["proj_cd"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["proj_no"] = txtproj_no.Text;
            objBASEFILEDS.HTMAIN["proj_nm"] = txtproj_nm.Text;
            objBASEFILEDS.HTMAIN["proj_ty_nm"] = cmbproj_type.Text;
            objBASEFILEDS.HTMAIN["proj_desc"] = txtproj_desc.Text;
            objBASEFILEDS.HTMAIN["proj_dur"] = txtProj_dur.Text;
            objBASEFILEDS.HTMAIN["recvd_dt"] = dtpRec.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["start_dt"] = dtp_start.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["appr_dt"] = dtp_appr.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["end_dt"] = dtp_end.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            objBASEFILEDS.HTMAIN["isdeactive"] = chkProd_active.Checked;
            objBASEFILEDS.HTMAIN["deactfrm"] = dtp_deactive_from.DtValue.ToString("yyyy/MM/dd");
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
        }
        private void GetFieldValueFromHashTable()
        {
            txtproj_no.Text = objBASEFILEDS.HTMAIN["proj_no"].ToString();
            txtproj_nm.Text = objBASEFILEDS.HTMAIN["proj_nm"].ToString();
            cmbproj_type.Text = objBASEFILEDS.HTMAIN["proj_ty_nm"].ToString();
            txtproj_desc.Text = objBASEFILEDS.HTMAIN["proj_desc"].ToString();
            txtProj_dur.Text = objBASEFILEDS.HTMAIN["proj_dur"].ToString();
            dtpRec.DtValue = objBASEFILEDS.HTMAIN["recvd_dt"] != null && objBASEFILEDS.HTMAIN["recvd_dt"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["recvd_dt"].ToString()) : DateTime.Parse("01/01/1900");
            dtp_start.DtValue = objBASEFILEDS.HTMAIN["start_dt"] != null && objBASEFILEDS.HTMAIN["start_dt"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["start_dt"].ToString()) : DateTime.Parse("01/01/1900");
            dtp_appr.DtValue = objBASEFILEDS.HTMAIN["appr_dt"] != null && objBASEFILEDS.HTMAIN["appr_dt"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["appr_dt"].ToString()) : DateTime.Parse("01/01/1900");
            dtp_end.DtValue = objBASEFILEDS.HTMAIN["end_dt"] != null && objBASEFILEDS.HTMAIN["end_dt"].ToString() != "" ? DateTime.Parse(objBASEFILEDS.HTMAIN["end_dt"].ToString()) : DateTime.Parse("01/01/1900");
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
            if (txtproj_no.Text == "") { AutoClosingMessageBox.Show("Please select valid Project Code", "Validation", 3000); flg = false; }
            else if (txtproj_nm.Text == "") { AutoClosingMessageBox.Show("Please enter valid Project Name", "Validation", 3000); flg = false; }
            else if (cmbproj_type.Text == "") { AutoClosingMessageBox.Show("Please select valid Project Type", "Validation", 3000); flg = false; }

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
                        dtpRec.DtValue = DateTime.Now;
                        dtp_start.DtValue = DateTime.Now;
                        txtproj_no.Focus();
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
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
                        txtproj_no.Enabled = false;
                        txtproj_nm.Enabled = false;
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        txtproj_desc.Focus();
                        // txtmac_no.Enabled = false;
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
                strSearchField = "proj_no";
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

        private void frmProjectMast_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmProjectMast_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }
        private void frmProjectMast_Resize(object sender, EventArgs e)
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

        private void txtproj_no_Validating(object snder, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtproj_no.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter valid Project Code", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDBAdaper.dsquery("select proj_cd,proj_no from " + objBASEFILEDS.Main_tbl_nm + " where proj_no ='" + txtproj_no.Text.Replace("'", "''") + "' and proj_cd!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Project Code already exists", "Validation", 3000);
                        e.Cancel = true;
                    }
                }
            }
        }
        private void txtproj_nm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtproj_nm.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter valid Project Name", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDBAdaper.dsquery("select proj_cd,proj_nm from " + objBASEFILEDS.Main_tbl_nm + " where proj_nm ='" + txtproj_nm.Text.Replace("'", "''") + "' and proj_cd!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Project Name already exists", "Validation", 3000);
                        e.Cancel = true;
                    }
                }
            }
        }
        private void btnProd_nm_Click(object sender, EventArgs e)
        {
            try
            {
                frmPopup objpopup = new frmPopup(objBASEFILEDS.HTMAIN, "PROJECT_MAST", "", "proj_cd,proj_nm", "proj_nm;Project Name", "Please Select", "", false, "", "0");//,tran_nm;Trasaction Name
                //objpopup.objCompany = objBASEFILEDS.ObjCompany;
                //objpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                objpopup.ObjBFD = objBASEFILEDS;
                objpopup.ShowDialog();
                txtproj_nm.Text = objBASEFILEDS.HTMAIN["proj_nm"].ToString();
                //CopyProductGroupDetails();
            }
            catch (Exception ex)
            {

            }
        }

        private void KeyPressEvent(TextBox txt, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

            }
        }

        private void cmbprod_type_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbproj_type.SelectedIndex == 0)
                {
                    AutoClosingMessageBox.Show("Please Select Valid Product Type", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void dtp_end_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (dtp_end.DtValue < dtp_start.DtValue)
                {
                    AutoClosingMessageBox.Show("End Date should be greater than equal to Start Date", "Validation", 3000);
                    e.Cancel = false;
                }
            }
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
            string strQuery = "select proj_cd,proj_no,proj_nm,proj_desc from " + objBASEFILEDS.Main_tbl_nm + " where " + strSearchField + " like '%" + strMessage.Replace("'", "''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
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
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvSearch != null)
                {
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["proj_cd"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
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
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["proj_cd"].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
                SelectSearchGrid();
                //DisplayControlsonMode("view_mode");
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
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["proj_cd"].Value.ToString();
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
                    if (row.Cells["proj_cd"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
