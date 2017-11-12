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
using iMANTRA_IL;
using iMANTRA_iniL;
using CUSTOM_iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmRolesMapping : BaseClass
    {
        private Ini objIni = new Ini();
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();
        string strSearchField = "user_nm";

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

        public frmRolesMapping(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }
        private void frmRolesMapping_Load(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            //DataSet ds1 = objdblayer.dsquery("select * from ORG_MAST");
            BindCompany();

            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            //this.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Back_color);
            //this.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Font_color);
            //ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            //ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color);
            //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family, float.Parse(objBASEFILEDS.ObjControlSet.Font_size));
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;
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
        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                BindSearchGrid("%");
                switch (tran_mode)
                {
                    case "add_mode":

                        objBASEFILEDS.Tran_id = "0";
                        txtUser.ReadOnly = false;
                        txtUser.Text = string.Empty;
                        dgvSearch.Enabled = false;
                        foreach (Control c in this.Controls)
                        {
                            if (c is ComboBox)
                            {
                                ((ComboBox)c).Enabled = true;
                                if (((ComboBox)c).SelectedIndex != -1)
                                {
                                    ((ComboBox)c).SelectedIndex = 0;
                                }
                            }
                            else if (c is TextBox)
                            {
                                ((TextBox)c).Text = "";
                                ((TextBox)c).Enabled = false;
                            }
                            else if (c is Button)
                            {
                                ((Button)c).Enabled = true;
                            }
                        }
                        txtUser.Focus();
                        break;

                    case "edit_mode":
                        ViewMapping();
                        foreach (Control c in this.Controls)
                        {
                            c.Enabled = true;
                        }
                        btnUser.Enabled = false;
                        txtUser.ReadOnly = true;
                        txtSearch.Text = "";
                        txtSearch.Enabled = false;
                        dgvSearch.Enabled = false;
                        break;

                    case "view_mode":
                         dgvSearch.Enabled = true;
                        ViewMapping();
                        foreach (Control c in this.Controls)
                        {
                            if (c is UCToolBar)
                            {
                            }
                            else if (c is Grouper)
                            {
                                c.Enabled = true;
                            }
                            else
                            {
                                if (!(c is Label)) c.Enabled = false;
                            }
                        }
                       
                        btnUser.Enabled = true;
                        txtSearch.Focus();
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void frmRolesMapping_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        private void frmRolesMapping_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
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
            // bool flg = true;
            if (txtUser.Text != "" && cmbComp.Text != "" && cmbFinYr.Text != "" && cmbRole.Text != "")
            {
                DataSet ds = objDBAdaper.dsquery("select intRoleMappingId,user_nm,* from Roles_mapping where user_nm='" + txtUser.Text.Replace("'", "''") + "' ");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    DataSet ds1 = objDBAdaper.dsquery("select intRoleMappingId,user_nm,* from Roles_mapping where user_nm='" + txtUser.Text.Replace("'", "''") + "' and compid='" + cmbComp.SelectedValue + "' and fin_yr='" + cmbFinYr.Text + "'");
                    if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
                    {
                        //MessageBox.Show("This User is already mapped.Do you want to re-map?");
                        DialogResult res = MessageBox.Show("This User is already mapped.Do you want to re-map?", "Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (res == DialogResult.Yes)
                        {
                            objBASEFILEDS.HTMAIN["intRoleMappingId"] = ds.Tables[0].Rows[0]["intRoleMappingId"];
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN["intRoleMappingId"] = objBASEFILEDS.Tran_id;
                    }
                }
                else
                {
                    objBASEFILEDS.HTMAIN["intRoleMappingId"] = objBASEFILEDS.Tran_id;
                }
                objBASEFILEDS.HTMAIN["user_nm"] = txtUser.Text;
                objBASEFILEDS.HTMAIN["compid"] = cmbComp.SelectedValue;
                objBASEFILEDS.HTMAIN["fin_yr"] = cmbFinYr.Text;
                objBASEFILEDS.HTMAIN["intRoleId"] = cmbRole.SelectedValue;
                objBASEFILEDS.HTMAIN["role_nm"] = cmbRole.Text;
                return true;
            }
            else
            {
                AutoClosingMessageBox.Show("Select all Fields", "Validation", 3000);
                return false;
            }
        }

        private void cmbComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //cmbFinYr.Refresh();
                if (cmbComp.Text.ToString() != "System.Data.DataRowView")
                {
                    //objBASEFILEDS.HTMAIN["Compid"] = cmbComp.SelectedValue;
                   // DataSet ds = objDBAdaper.dsquery("select Fin_yr from ORG_MAST where compid='" + cmbComp.Text + "'");
                    DataSet ds = objDBAdaper.dsquery("select COMP_FIN_YR_MAPPING.fin_yr from ORG_MAST inner join COMP_FIN_YR_MAPPING on ORG_MAST.compid=COMP_FIN_YR_MAPPING.compid where ORG_MAST.comp_nm = '" + cmbComp.Text.ToString().Replace("'", "''") + "' order by COMP_FIN_YR_MAPPING.compid");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        cmbFinYr.DataSource = ds.Tables[0];
                        cmbFinYr.ValueMember = "fin_yr";
                        cmbFinYr.DisplayMember = "fin_yr";
                        cmbFinYr.Update();
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            cmbFinYr.SelectedIndex = 0;
                        }
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            cmbFinYr.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception", 3000);
            }
        }
        private void cmbFinYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFinYr.Text.ToString() != "System.Data.DataRowView")
            {
                //cmbRole.Refresh();
                DataSet ds = objDBAdaper.dsquery("select roles.Role_nm,roles.intRoleId,roles.fin_yr from Roles inner join COMP_FIN_YR_MAPPING on roles.compid=COMP_FIN_YR_MAPPING.compid where COMP_FIN_YR_MAPPING.comp_nm='" + cmbComp.Text.Replace("'", "''") + "'  and COMP_FIN_YR_MAPPING.fin_yr='" + cmbFinYr.Text + "'");
                if (ds.Tables.Count != 0)
                {
                    cmbRole.DataSource = ds.Tables[0];
                    cmbRole.ValueMember = "intRoleId";
                    cmbRole.DisplayMember = "role_nm";
                    cmbRole.Update();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        cmbRole.SelectedIndex = 0;
                    }
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        cmbRole.Text = "";
                    }
                }
            }
        }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ViewMapping()
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            DataSet ds = objDBAdaper.dsquery("select userid,user_nm,fin_yr,compid,intRoleId from roles_mapping where intRoleMappingId='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtUser.Text = ds.Tables[0].Rows[0]["user_nm"].ToString();
                objBASEFILEDS.HTMAIN["userid"] = ds.Tables[0].Rows[0]["userid"].ToString();
                cmbComp.SelectedValue = ds.Tables[0].Rows[0]["compid"].ToString();
                cmbRole.SelectedValue = ds.Tables[0].Rows[0]["intRoleId"].ToString();
                cmbFinYr.SelectedValue = ds.Tables[0].Rows[0]["fin_yr"].ToString();
            }
            objBASEFILEDS.HTMAIN["intRoleMappingId"] = objBASEFILEDS.Tran_id;
        }
        private void btnUser_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (objBASEFILEDS.Tran_mode == "add_mode")
                {
                    frmPopup objfrmpopup = new frmPopup(objBASEFILEDS.HTMAIN, "Login_Mast", "UL", "userid,user_nm", "userid;User Id,user_nm;User Name", "Please select", "", false,"", "0");
                    //objfrmpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmpopup.ObjBFD = objBASEFILEDS;
                    objfrmpopup.ShowDialog();
                    txtUser.Text = objBASEFILEDS.HTMAIN["user_nm"].ToString();
                }
                else
                {
                    frmPopup objfrmpopup = new frmPopup(objBASEFILEDS.HTMAIN, "Roles_Mapping", "RM", "intRoleMappingId,user_nm", "userid;User Id,user_nm;User Name", "Please select", "fin_yr='" + objBASEFILEDS.ObjCompany.Fin_yr + "'", false, "", "0");
                    //objfrmpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmpopup.ObjBFD = objBASEFILEDS;
                    objfrmpopup.ShowDialog();
                    txtUser.Text = objBASEFILEDS.HTMAIN["user_nm"].ToString();
                    objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["intRoleMappingId"].ToString();
                    ViewMapping();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txtUser_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtUser.Text != "")
                {
                    DataSet ds = objDBAdaper.dsquery("select compid,comp_nm from Login_mast where user_nm='" + txtUser.Text.Replace("'", "''") + "'");// where user_nm='" + txtUser.Text + "'
                    if (ds.Tables.Count != 0)
                    {
                        cmbComp.DataSource = ds.Tables[0];
                        cmbComp.DisplayMember = "comp_nm";
                        cmbComp.ValueMember = "compid";
                        cmbComp.Update();
                    }
                }
            }
        }
        private void BindCompany()
        {
            DataSet ds = objDBAdaper.dsquery("select compid,comp_nm from ORG_MAST");// where user_nm='" + txtUser.Text + "'
            if (ds.Tables.Count != 0)
            {
                cmbComp.DataSource = ds.Tables[0];
                cmbComp.ValueMember = "compid";
                cmbComp.DisplayMember = "comp_nm";
                cmbComp.Update();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    cmbComp.SelectedIndex = 0;
                }
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {

            }
        }
        private void cmbComp_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbComp.Text == "")
                {
                    AutoClosingMessageBox.Show("Select Company", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void cmbFinYr_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbFinYr.Text == "")
                {
                    AutoClosingMessageBox.Show("Select Financial Year", "Validation", 3000);
                    e.Cancel = true;
                }
            }
        }
        private void cmbRole_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (cmbRole.Text == "")
                {
                    AutoClosingMessageBox.Show("Select Role", "Validation", 3000);
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
            string strQuery = "select intRoleMappingId,user_nm,compid comp_nm,fin_yr,role_nm from ROLES_MAPPING where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and fin_yr='"+objBASEFILEDS.ObjCompany.Fin_yr+"'";
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
        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                BindSearchGrid(txtSearch.Text);
            else
                BindSearchGrid("%");
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
                    objBASEFILEDS.Tran_id = dgvSearch.CurrentRow.Cells["intRoleMappingId"].Value.ToString();
                    //AddFieldToHashTable();
                    //GetFieldValueFromHashTable();
                    DisplayControlsonMode("view_mode");
                    txtSearch.Focus();
                }
            }
        }
        private void dgvSearch_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["intRoleMappingId"].Value.ToString();
                ViewMapping();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    if (dgvSearch.Columns[e.ColumnIndex].Name == "user_nm")
                    {
                        strSearchField = "user_nm";
                    }
                    else
                        strSearchField = dgvSearch.Columns[e.ColumnIndex].Name;
                }
                else
                    strSearchField = "user_nm";
            }
        }
        private void dgvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells["intRoleMappingId"].Value.ToString();
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
                    if (row.Cells["intRoleMappingId"].Value.ToString().Equals(objBASEFILEDS.Tran_id))
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
