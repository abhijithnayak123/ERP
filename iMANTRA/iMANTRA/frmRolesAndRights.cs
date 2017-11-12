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
    public partial class frmRolesAndRights : BaseClass
    {
        /********************************************************************************************
      * Sharanamma Jekeen Inode Technologies Pvt. Ltd.
      * 1.0 on 11.22.13 Added header level CheckBox
      * 
      * 
      * 
      * 
      * 
      * 
      * 
      * ******************************************************************************************/
        private Ini objIni = new Ini();
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";
        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

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

        public frmRolesAndRights(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.Tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmRolesAndRights_Load(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            //this.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Back_color);
            //this.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Font_color);
            //ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            //ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color);
            //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family, float.Parse(objBASEFILEDS.ObjControlSet.Font_size));
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;

            this.dgvRights.BackgroundColor = Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color);
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
            if (txtRole.Text != "")
            {
                DataSet ds = objDL_ADAPTER.dsquery("select intRoleId,role_nm from roles where role_nm ='" + txtRole.Text + "' and intRoleId!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("Role already exists", "Validation", 3000);
                    return false;
                }
                else
                {
                    objBASEFILEDS.HTMAIN["intRoleId"] = objBASEFILEDS.Tran_id;
                    objBASEFILEDS.HTMAIN["role_nm"] = txtRole.Text;
                    objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
                    dgvRights.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    foreach (DataGridViewRow row in dgvRights.Rows)
                    {
                        int module_id = int.Parse(row.Cells["module_id"].Value.ToString()); if (!objBASEFILEDS.HTITEM.Contains(module_id))
                        {
                            objBASEFILEDS.HTITEM[module_id] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["intRightsId"] = row.Cells["intRightsId"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["module_id"] = row.Cells["module_id"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["intRoleId"] = objBASEFILEDS.Tran_id;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["view_access"] = row.Cells["view_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["create_access"] = row.Cells["create_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["edit_access"] = row.Cells["edit_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["delete_access"] = row.Cells["delete_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["print_access"] = row.Cells["print_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["approve_access"] = row.Cells["approve_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["plan_access"] = row.Cells["plan_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["exc_access"] = row.Cells["exc_access"].Value;
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["tran_cd"] = "RT";
                        ((Hashtable)objBASEFILEDS.HTITEM[module_id])["compid"] = objBASEFILEDS.ObjCompany.Compid;
                    }
                    return true;
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Enter Role", "Validation", 3000);
                return false;
            }
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
                        objBASEFILEDS.Tran_id = "0";
                        lblRoles.Visible = true;
                        btnSel.Visible = false;
                        txtRole.Enabled = true;
                        dgvRights.Rows.Clear();
                        dgvRights.ReadOnly = false;
                        dgvRights.Columns["Module_Id"].ReadOnly = true;
                        dgvRights.Columns["module_name"].ReadOnly = true;
                        foreach (Control c in this.Controls)
                        {
                            if (c is CheckBox)
                            {
                                ((CheckBox)c).Enabled = true;
                                ((CheckBox)c).Checked = false;
                            }
                        }
                        txtRole.Select();
                        AddRights();
                        break;
                    case "edit_mode":
                        dgvRights.ReadOnly = false;
                        dgvRights.Columns["Module_Id"].ReadOnly = true;
                        dgvRights.Columns["module_name"].ReadOnly = true;
                        foreach (Control c in this.Controls)
                        {
                            if (c is CheckBox)
                            {
                                ((CheckBox)c).Enabled = true;
                            }
                        }
                        txtRole.Enabled = false;
                        btnSel.Enabled = false;
                        LoadRights();
                        break;
                    case "view_mode":
                        LoadRights();
                        foreach (Control c in this.Controls)
                        {
                            if (c is CheckBox)
                            {
                                ((CheckBox)c).Enabled = false;
                            }
                        }
                        btnSel.Visible = true;
                        txtRole.Enabled = false;
                        dgvRights.ReadOnly = true;
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AddRights()
        {
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            txtRole.Clear();
            DataSet ds = objDL_ADAPTER.dsquery("select Module_id, Module_name from CMOD where compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow ds_row in ds.Tables[0].Rows)
                {
                    dgvRights.Rows.Add();
                    int RowIndex = dgvRights.RowCount - 1;
                    dgvRights["intRightsId", RowIndex].Value = "0";
                    dgvRights["module_id", RowIndex].Value = ds_row["module_id"];
                    dgvRights["module_name", RowIndex].Value = ds_row["module_name"];
                    dgvRights["view_access", RowIndex].Value = false;
                    dgvRights["create_access", RowIndex].Value = false;
                    dgvRights["edit_access", RowIndex].Value = false;
                    dgvRights["delete_access", RowIndex].Value = false;
                    dgvRights["print_access", RowIndex].Value = false;
                    dgvRights["approve_access", RowIndex].Value = false;
                    dgvRights["plan_access", RowIndex].Value = false;
                    dgvRights["exc_access", RowIndex].Value = false;
                }
            }
        }
        private void LoadRights()
        {
            if (objBASEFILEDS.Tran_mode == "view_mode")
            {
                dgvRights.Rows.Clear();
            }
            else
            {
                dgvRights.Rows.Clear();
                DataSet ds = objDL_ADAPTER.dsquery("Select intRoleId from Roles_Mapping where intRoleId='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                }
                else
                {
                    txtRole.Enabled = true;
                }
            }
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
            DataSet ds1 = objDL_ADAPTER.dsquery("select intRoleId,role_nm from roles where intRoleId='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "' ");
            if (ds1 != null && ds1.Tables.Count != 0 && ds1.Tables[0].Rows.Count != 0)
            {
                txtRole.Text = ds1.Tables[0].Rows[0]["role_nm"].ToString();
                objBASEFILEDS.Tran_id = ds1.Tables[0].Rows[0]["intRoleId"].ToString();
                DataSet ds = objDL_ADAPTER.dsquery("select RIGHTS.intRightsId,CMOD.module_id, CMOD.module_name,RIGHTS.intRoleId,RIGHTS.view_access,RIGHTS.create_access,Rights.edit_access,RIGHTS.delete_access,RIGHTS.print_access,RIGHTS.approve_access,RIGHTS.plan_access,RIGHTS.exc_access from CMOD inner join RIGHTS on (CMOD.module_id= RIGHTS.module_id) where Rights.intRoleId='" + ds1.Tables[0].Rows[0]["intRoleId"] + "' and CMOD.compid='" + objBASEFILEDS.ObjCompany.Compid + "' and RIGHTS.compid='" + objBASEFILEDS.ObjCompany.Compid + "' order by CMOD.module_id ");
                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow ds_row in ds.Tables[0].Rows)
                    {
                        dgvRights.Rows.Add();
                        int RowIndex = dgvRights.RowCount - 1;
                        dgvRights["intRightsId", RowIndex].Value = ds_row["intRightsId"];
                        dgvRights["module_id", RowIndex].Value = ds_row["module_id"];
                        dgvRights["module_name", RowIndex].Value = ds_row["module_name"];
                        dgvRights["view_access", RowIndex].Value = Convert.ToBoolean(ds_row["view_access"]);
                        dgvRights["create_access", RowIndex].Value = Convert.ToBoolean(ds_row["create_access"]);
                        dgvRights["edit_access", RowIndex].Value = Convert.ToBoolean(ds_row["edit_access"]);
                        dgvRights["delete_access", RowIndex].Value = Convert.ToBoolean(ds_row["delete_access"]);
                        dgvRights["print_access", RowIndex].Value = Convert.ToBoolean(ds_row["print_access"]);
                        dgvRights["approve_access", RowIndex].Value = Convert.ToBoolean(ds_row["approve_access"]);
                        dgvRights["plan_access", RowIndex].Value = Convert.ToBoolean(ds_row["plan_access"]);
                        dgvRights["exc_access", RowIndex].Value = Convert.ToBoolean(ds_row["exc_access"]);
                    }
                }
            }

            objBASEFILEDS.HTMAIN["intRoleId"] = objBASEFILEDS.Tran_id;
            objBASEFILEDS.HTMAIN["role_nm"] = txtRole.Text;
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();


            foreach (DataGridViewRow row in dgvRights.Rows)
            {
                int module_id = int.Parse(row.Cells["module_id"].Value.ToString());
                if (!objBASEFILEDS.HTITEM.Contains(module_id))
                {
                    objBASEFILEDS.HTITEM[module_id] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                }
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["intRightsId"] = row.Cells["intRightsId"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["module_id"] = row.Cells["module_id"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["intRoleId"] = objBASEFILEDS.Tran_id;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["view_access"] = row.Cells["view_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["create_access"] = row.Cells["create_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["edit_access"] = row.Cells["edit_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["delete_access"] = row.Cells["delete_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["print_access"] = row.Cells["print_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["approve_access"] = row.Cells["approve_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["plan_access"] = row.Cells["plan_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["exc_access"] = row.Cells["exc_access"].Value;
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["tran_cd"] = "RT";
                ((Hashtable)objBASEFILEDS.HTITEM[module_id])["compid"] = objBASEFILEDS.ObjCompany.Compid;
            }
        }

        private void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void frmRolesAndRights_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmRolesAndRights_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void txtRole_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtRole.Text == "")
                {
                    AutoClosingMessageBox.Show("Please enter Caption", "Validation", 3000);
                    e.Cancel = true;
                }
                else
                {
                    DataSet ds = objDL_ADAPTER.dsquery("select intRoleId,role_nm from roles where role_nm ='" + txtRole.Text.Replace("'", "''") + "' and intRoleId!='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Role already exists", "Validation", 3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
        }

        private void btnSel_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmPopup objfrmpopup = new frmPopup(objBASEFILEDS.HTMAIN, "ROLES", "RL", "intRoleId,role_nm", "intRoleId;Role Id,role_nm;Role", "Please select", "", false, "", "0");
                //objfrmpopup.objCompany = objBASEFILEDS.ObjCompany;
                //objfrmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                objfrmpopup.ObjBFD = objBASEFILEDS;
                objfrmpopup.ShowDialog();
                txtRole.Text = objBASEFILEDS.HTMAIN["role_nm"].ToString();
                objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN["intRoleId"].ToString();
                LoadRights();
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvRights_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            #region
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //gridview.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);
                if (gridview.CurrentRow.Cells["view_access"].Value != null && gridview.CurrentRow.Cells["view_access"].Value.ToString() != "")
                {
                    if (gridview.CurrentRow.Cells["view_access"].Value.ToString() == "1")
                    {
                        gridview.CurrentRow.Cells["view_access"].Value = true;
                    }
                    else if (gridview.CurrentRow.Cells["view_access"].Value.ToString() == "0")
                    {
                        gridview.CurrentRow.Cells["view_access"].Value = false;
                    }
                }

                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "create_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "edit_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "delete_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "print_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "approve_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "plan_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "exc_access")
                {
                    if (!bool.Parse(gridview.CurrentRow.Cells["view_access"].Value.ToString()))
                    {
                        AutoClosingMessageBox.Show("Sorry!! Please Select View Access rights before selecting " + gridview.CurrentCell.OwningColumn.Name.Trim().ToLower(), "Access Rights", 3000);
                        // gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = false;
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()];
                        if (chk != null)
                        {
                            if (chk != null && gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].EditedFormattedValue != null && gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].EditedFormattedValue.ToString().ToLower() == "true")
                            {
                                chk.Value = false;
                                gridview.CancelEdit();
                            }
                            //  gridview.CancelEdit();
                        }
                    }
                    else
                    {
                        if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value != null && gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() != "")
                        {
                            if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() == "1")
                            {
                                gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = true;
                            }
                            else if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() == "0")
                            {
                                gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = false;
                            }
                        }
                    }
                }
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "view_access")
                {
                    //gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if (!bool.Parse(gridview.CurrentRow.Cells["view_access"].Value.ToString()))
                    {
                        foreach (DataGridViewColumn col in gridview.Columns)
                        {
                            if (col.Name.Trim().ToLower() == "create_access" || col.Name.Trim().ToLower() == "edit_access" || col.Name.Trim().ToLower() == "delete_access" || col.Name.Trim().ToLower() == "print_access" || col.Name.Trim().ToLower() == "approve_access" || col.Name.Trim().ToLower() == "exc_access" || col.Name.Trim().ToLower() == "plan_access")
                            {
                                DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[col.Name];
                                if (chk1 != null)
                                {
                                    chk1.Value = false;
                                }
                            }
                        }
                    }
                }
                gridview.EndEdit(DataGridViewDataErrorContexts.CurrentCellChange);
            }
            #endregion
        }

        private void frmRolesAndRights_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void dgvRights_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //DataGridView gridview = (DataGridView)sender;
            //if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            //{
            //    gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //    if (gridview.CurrentRow.Cells["view_access"].Value != null && gridview.CurrentRow.Cells["view_access"].Value.ToString() != "")
            //    {
            //        if (gridview.CurrentRow.Cells["view_access"].Value.ToString() == "1")
            //        {
            //            gridview.CurrentRow.Cells["view_access"].Value = true;
            //        }
            //        else if (gridview.CurrentRow.Cells["view_access"].Value.ToString() == "0")
            //        {
            //            gridview.CurrentRow.Cells["view_access"].Value = false;
            //        }
            //    }

            //    if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "create_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "edit_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "delete_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "print_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "approve_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "plan_access" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "exc_access")
            //    {
            //        if (!bool.Parse(gridview.CurrentRow.Cells["view_access"].Value.ToString()))
            //        {
            //            AutoClosingMessageBox.Show("Sorry!! Please Select View Access rights before selecting " + gridview.CurrentCell.OwningColumn.Name.Trim().ToLower(), "Access Rights", 3000);
            //            // gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = false;
            //            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()];
            //            if (chk != null)
            //            {
            //                chk.Value = false;
            //            }

            //        }
            //        else
            //        {
            //            if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value != null && gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() != "")
            //            {
            //                if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() == "1")
            //                {
            //                    gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = true;
            //                }
            //                else if (gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value.ToString() == "0")
            //                {
            //                    gridview.CurrentRow.Cells[gridview.CurrentCell.OwningColumn.Name.Trim().ToLower()].Value = false;
            //                }
            //            }
            //        }
            //    }
            //    if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "view_access")
            //    {
            //        //gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //        if (!bool.Parse(gridview.CurrentRow.Cells["view_access"].Value.ToString()))
            //        {
            //            foreach (DataGridViewColumn col in gridview.Columns)
            //            {
            //                if (col.Name.Trim().ToLower() == "create_access" || col.Name.Trim().ToLower() == "edit_access" || col.Name.Trim().ToLower() == "delete_access" || col.Name.Trim().ToLower() == "print_access" || col.Name.Trim().ToLower() == "approve_access" || col.Name.Trim().ToLower() == "exc_access" || col.Name.Trim().ToLower() == "plan_access")
            //                {
            //                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[col.Name];
            //                    if (chk != null)
            //                    {
            //                        chk.Value = false;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    gridview.EndEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }
    }
}
