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
    public partial class frmWarehouse : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        private FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        private Hashtable _hashlocaldc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string strSearchField = "prod_nm";

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

        public frmWarehouse(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code; this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = objBL.Code;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            dgvWareHouse.AutoGenerateColumns = false;
            dgvRack.AutoGenerateColumns = false;
            dgvBin.AutoGenerateColumns = false;

            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
        }

        private void BindWareHouseGrid(string strMessage)
        {
            string strQuery = "select WAREHOUSEID,WAREHOUSENAME,[DESCRIPTION] from WAREHOUSE_MAST where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetWarehouse = objDBAdaper.dsquery(strQuery);
            dgvWareHouse.AutoGenerateColumns = false;
            if (dsetWarehouse != null && dsetWarehouse.Tables.Count != 0 && dsetWarehouse.Tables[0].Rows.Count != 0)
            {
                dgvWareHouse.DataSource = dsetWarehouse.Tables[0];
                dgvWareHouse.Update();
                int i = 0;
                foreach (DataRow row in dsetWarehouse.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvWareHouse.Columns)
                    {
                        if (dsetWarehouse.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvWareHouse.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblRowsCount.Text = "Total Records : " + dgvWareHouse.Rows.Count;
            }
            else
            {
                lblRowsCount.Text = "Total Records : 0";
                ClearSearchdgvProcess(dgvWareHouse);
            }
        }

        private void BindRackGrid(string strMessage)
        {
            string strQueryRack = "select RACKID,RACKNAME,WAREHOUSEID WAREHOUSEID1,WAREHOUSENAME WAREHOUSENAME1,[DESCRIPTION] DESCRIPTION1 from RACK_MAST where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetRack = objDBAdaper.dsquery(strQueryRack);
            dgvRack.AutoGenerateColumns = false;
            if (dsetRack != null && dsetRack.Tables.Count != 0 && dsetRack.Tables[0].Rows.Count != 0)
            {
                dgvRack.DataSource = dsetRack.Tables[0];
                dgvRack.Update();
                int i = 0;
                foreach (DataRow row in dsetRack.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvRack.Columns)
                    {
                        if (dsetRack.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvRack.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblrowsRack.Text = "Total Records : " + dgvRack.Rows.Count;
            }
            else
            {
                lblrowsRack.Text = "Total Records : 0";
                ClearSearchdgvProcess(dgvRack);
            }
        }

        private void BindBinGrid(string strMessage)
        {
            string strQueryBin = "select RACKID RACKID1,RACKNAME RACKNAME1,BINID,BINNAME,[DESCRIPTION] DESCRIPTION2,MAX_QTY,MIN_QTY from BIN_MAST where " + strSearchField + " like '%" + strMessage.Replace("'","''") + "%' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
            DataSet dsetBin = objDBAdaper.dsquery(strQueryBin);
            dgvBin.AutoGenerateColumns = false;
            if (dsetBin != null && dsetBin.Tables.Count != 0 && dsetBin.Tables[0].Rows.Count != 0)
            {
                dgvBin.DataSource = dsetBin.Tables[0];
                dgvBin.Update();
                int i = 0;
                foreach (DataRow row in dsetBin.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvBin.Columns)
                    {
                        if (dsetBin.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvBin.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblRowsBin.Text = "Total Records : " + dgvBin.Rows.Count;
            }
            else
            {
                lblRowsBin.Text = "Total Records : 0";
                ClearSearchdgvProcess(dgvBin);
            }
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

        private void AddFieldToHashTable()
        {
            objBASEFILEDS.HTMAIN.Clear();
            //objBASEFILEDS.dsetview = new DataSet();
            if (tcWarehouse.SelectedIndex == 0)
            {
                objBASEFILEDS.Primary_id = "WAREHOUSEID";
                objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  WAREHOUSE_MAST where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);
                AddToHashTB(objBASEFILEDS.dsetview, objBASEFILEDS.HTMAIN);
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
            else if (tcWarehouse.SelectedIndex == 1)
            {
                objBASEFILEDS.Primary_id = "RACKID";
                objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  RACK_MAST where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);
                AddToHashTB(objBASEFILEDS.dsetview, objBASEFILEDS.HTMAIN);
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
            else
            {
                objBASEFILEDS.Primary_id = "BINID";
                objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  BIN_MAST where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);
                AddToHashTB(objBASEFILEDS.dsetview, objBASEFILEDS.HTMAIN);
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

        private void InsertFieldValueToHashTable()
        {
            objBASEFILEDS.HTMAIN["fin_yr"] = objBASEFILEDS.ObjCompany.Fin_yr.ToString();
            objBASEFILEDS.HTMAIN["compid"] = objBASEFILEDS.ObjCompany.Compid.ToString();
            objBASEFILEDS.HTMAIN["tran_cd"] = objBASEFILEDS.Code;
            if (tcWarehouse.SelectedIndex == 0)
            {
                ControlsValue(1);
            }
            else if (tcWarehouse.SelectedIndex == 1)
            {
                ControlsValue(1);
            }
            else
            {
                ControlsValue(1);
            }
        }

        private void GetFieldValueFromHashTable()
        {
            if (objBASEFILEDS.Tran_id != "0")
            {
                if (tcWarehouse.SelectedIndex == 0)
                {
                    ControlsValue(0);
                }
                else if (tcWarehouse.SelectedIndex == 1)
                {
                    ControlsValue(0);
                }
                else
                {
                    ControlsValue(0);
                }
            }
            else
            {

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
            if (tcWarehouse.SelectedIndex == 0)
            {
                objBASEFILEDS.Code = "WM";
                objBASEFILEDS.Primary_id = "WAREHOUSEID";
                objBASEFILEDS.Main_tbl_nm = "WAREHOUSE_MAST";

                if (txtWHName.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Valid Warehouse","Validation",3000); flg = false;
                }
            }
            else if (tcWarehouse.SelectedIndex == 1)
            {
                objBASEFILEDS.Code = "RM";
                objBASEFILEDS.Primary_id = "RACKID";
                objBASEFILEDS.Main_tbl_nm = "RACK_MAST";

                if (txtRackName.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Valid Rack","Validation",3000); flg = false;
                }
                if (flg && txtRackWH.Text == "")
                {
                    AutoClosingMessageBox.Show("Please select Valid Warehouse","Validation",3000); flg = false;
                }
            }
            else
            {
                objBASEFILEDS.Code = "BM";
                objBASEFILEDS.Primary_id = "BINID";
                objBASEFILEDS.Main_tbl_nm = "BIN_MAST";

                if (txtBinName.Text == "")
                {
                    AutoClosingMessageBox.Show("Please Enter Valid Bin","Validation",3000); flg = false;
                }
                if (flg && txtBinRack.Text == "")
                {
                    AutoClosingMessageBox.Show("Please select Valid Rack","Validation",3000); flg = false;
                }
            }
            return flg;
        }
        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                switch (tran_mode)
                {
                    case "add_mode":
                        if (tcWarehouse.SelectedIndex == 0)
                        {
                            objBASEFILEDS.Primary_id = "WAREHOUSEID";
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, true);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                        }
                        else if (tcWarehouse.SelectedIndex == 1)
                        {
                            objBASEFILEDS.Primary_id = "RACKID";
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, true);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                        }
                        else
                        {
                            objBASEFILEDS.Primary_id = "BINID";
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, true);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                        }

                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        if (tcWarehouse.SelectedIndex == 0)
                        {
                            objBASEFILEDS.Primary_id = "WAREHOUSEID";
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, true);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                        }
                        else if (tcWarehouse.SelectedIndex == 1)
                        {
                            objBASEFILEDS.Primary_id = "RACKID";
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, true);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                        }
                        else
                        {
                            objBASEFILEDS.Primary_id = "BINID";
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, true);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                        }
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        GetFieldValueFromHashTable();
                        if (tcWarehouse.SelectedIndex == 0)
                        {
                            objBASEFILEDS.Primary_id = "WAREHOUSEID";
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);

                            strSearchField = "WAREHOUSENAME";
                            BindWareHouseGrid("%");
                        }
                        else if (tcWarehouse.SelectedIndex == 1)
                        {
                            objBASEFILEDS.Primary_id = "RACKID";
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);

                            strSearchField = "rackname";
                            BindRackGrid("%");
                        }
                        else
                        {
                            objBASEFILEDS.Primary_id = "BINID";
                            ControlsVisibility(tpBin, dgvBin, txtBinSearch, false);
                            ControlsVisibility(tpWarehouse, dgvWareHouse, txtWHSearch, false);
                            ControlsVisibility(tpRack, dgvRack, txtRackSearch, false);

                            strSearchField = "binname";
                            BindBinGrid("%");
                        }
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ControlsValue(int flgInsert)
        {
            if (flgInsert == 1)
            {
                if (tcWarehouse.SelectedIndex == 0)
                {
                    objBASEFILEDS.HTMAIN["code"] = txtWHCode.Text;
                    objBASEFILEDS.HTMAIN["WAREHOUSENAME"] = txtWHName.Text;
                    objBASEFILEDS.HTMAIN["DESCRIPTION"] = txtWHDesc.Text;
                }
                else if (tcWarehouse.SelectedIndex == 1)
                {
                    objBASEFILEDS.HTMAIN["code"] = txtRackCode.Text;
                    objBASEFILEDS.HTMAIN["RACKNAME"] = txtRackName.Text;
                    objBASEFILEDS.HTMAIN["DESCRIPTION"] = txtRackDesc.Text;
                    objBASEFILEDS.HTMAIN["WAREHOUSENAME"] = txtRackWH.Text;
                    objBASEFILEDS.HTMAIN["WAREHOUSEID"] = txtRackWHId.Text;
                }
                else
                {
                    objBASEFILEDS.HTMAIN["code"] = txtBinCode.Text;
                    objBASEFILEDS.HTMAIN["BINNAME"] = txtBinName.Text;
                    objBASEFILEDS.HTMAIN["DESCRIPTION"] = txtBinDesc.Text;
                    objBASEFILEDS.HTMAIN["RACKNAME"] = txtBinRack.Text;
                    objBASEFILEDS.HTMAIN["RACKID"] = txtBinRackId.Text;
                    objBASEFILEDS.HTMAIN["MIN_QTY"] = txtBinMin.Text;
                    objBASEFILEDS.HTMAIN["MAX_QTY"] = txtBinMax.Text;
                }
            }
            else
            {
                if (tcWarehouse.SelectedIndex == 0)
                {
                    txtWHCode.Text = objBASEFILEDS.HTMAIN["code"].ToString();
                    txtWHName.Text = objBASEFILEDS.HTMAIN["WAREHOUSENAME"].ToString();
                    txtWHDesc.Text = objBASEFILEDS.HTMAIN["DESCRIPTION"].ToString();
                }
                else if (tcWarehouse.SelectedIndex == 1)
                {
                    txtRackCode.Text = objBASEFILEDS.HTMAIN["code"].ToString();
                    txtRackName.Text = objBASEFILEDS.HTMAIN["RACKNAME"].ToString();
                    txtRackDesc.Text = objBASEFILEDS.HTMAIN["DESCRIPTION"].ToString();
                    txtRackWH.Text = objBASEFILEDS.HTMAIN["WAREHOUSENAME"].ToString();
                    txtRackWHId.Text = objBASEFILEDS.HTMAIN["WAREHOUSEID"].ToString();
                }
                else
                {
                    txtBinCode.Text = objBASEFILEDS.HTMAIN["code"].ToString();
                    txtBinName.Text = objBASEFILEDS.HTMAIN["BINNAME"].ToString();
                    txtBinDesc.Text = objBASEFILEDS.HTMAIN["DESCRIPTION"].ToString();
                    txtBinRack.Text = objBASEFILEDS.HTMAIN["RACKNAME"].ToString();
                    txtBinRackId.Text = objBASEFILEDS.HTMAIN["RACKID"].ToString();
                    txtBinMin.Text = objBASEFILEDS.HTMAIN["MIN_QTY"].ToString();
                    txtBinMax.Text = objBASEFILEDS.HTMAIN["MAX_QTY"].ToString();
                }
            }
        }

        private void ControlsVisibility(TabPage tp, DataGridView dgv, TextBox txt, bool enable_disable)
        {
            foreach (Control con1 in tp.Controls)
            {
                foreach (Control c in con1.Controls)
                {
                    if (c is TextBox)
                    {
                        if (objBASEFILEDS.Tran_mode == "add_mode")
                        {
                            ((TextBox)c).Text = "";
                        }
                    }
                    c.Enabled = enable_disable;
                }
            }
            txt.Text = "";
            txt.Enabled = !enable_disable;
            dgv.Enabled = !enable_disable;
        }

        private void frmWarehouse_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmWarehouse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (objBASEFILEDS.Code == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void frmWarehouse_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void tcWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBASEFILEDS.HTMAIN.Clear();
            if (tcWarehouse.SelectedIndex == 0)
            {
                objBASEFILEDS.Code = "WM";
                objBASEFILEDS.Primary_id = "WAREHOUSEID";
                objBASEFILEDS.Main_tbl_nm = "WAREHOUSE_MAST";
                strSearchField = "WAREHOUSENAME";
                BindWareHouseGrid("%");
            }
            else if (tcWarehouse.SelectedIndex == 1)
            {
                objBASEFILEDS.Code = "RM";
                objBASEFILEDS.Primary_id = "RACKID";
                objBASEFILEDS.Main_tbl_nm = "RACK_MAST";
                strSearchField = "RACKNAME";
                BindRackGrid("%");
            }
            else
            {
                objBASEFILEDS.Code = "BM";
                objBASEFILEDS.Primary_id = "BINID";
                objBASEFILEDS.Main_tbl_nm = "BIN_MAST";
                strSearchField = "BINNAME";
                BindBinGrid("%");
            }
            AddFieldToHashTable();
            InsertFieldValueToHashTable();
            GetFieldValueFromHashTable();
            objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, "");

            if (objBASEFILEDS.dsNavigation.Tables[0].Rows.Count > 0)
            {
                objBASEFILEDS.Tran_id = objBASEFILEDS.dsNavigation.Tables[0].Rows.Count > 0 ? objBASEFILEDS.dsNavigation.Tables[0].Rows[objBASEFILEDS.dsNavigation.Tables[0].Rows.Count - 1][objBASEFILEDS.Primary_id].ToString() : "0";
                objBASEFILEDS.ObjLoginUser.HASHTOOL[objBASEFILEDS.Code + objBASEFILEDS.ObjLoginUser.CurUser + objBASEFILEDS.Curr_date_time] = objBASEFILEDS.dsNavigation.Tables[0].Rows.Count - 1;
            }
            else
            {
                objBASEFILEDS.Tran_id = "0";
                objBASEFILEDS.ObjLoginUser.HASHTOOL[objBASEFILEDS.Code + objBASEFILEDS.ObjLoginUser.CurUser + objBASEFILEDS.Curr_date_time] = 0;
            }
        }
        private void tcWarehouse_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcWarehouse.SelectedIndex == 0)
            {
                if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
                {
                    DialogResult res = MessageBox.Show("Are You Sure to change Tab,Yes means you'll be loosing modified Warehouse", "Change Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (DialogResult.No == res)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        objBASEFILEDS.Tran_mode = "view_mode";
                    }
                }
            }
            else if (tcWarehouse.SelectedIndex == 1)
            {
                if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
                {
                    DialogResult res = MessageBox.Show("Are You Sure to change Tab,Yes means you'll be loosing modified Rack", "Change Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (DialogResult.No == res)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        objBASEFILEDS.Tran_mode = "view_mode";
                    }
                }
            }
            else
            {
                if (objBASEFILEDS.Tran_mode == "add_mode" || objBASEFILEDS.Tran_mode == "edit_mode")
                {
                    DialogResult res = MessageBox.Show("Are You Sure to change Tab,Yes means you'll be loosing modified Bin", "Change Tab", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (DialogResult.No == res)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        objBASEFILEDS.Tran_mode = "view_mode";
                    }
                }
            }
        }

        private void dgvWareHouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells[objBASEFILEDS.Primary_id].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    strSearchField = dgvWareHouse.Columns[e.ColumnIndex].Name;
                    foreach (DataGridViewColumn row in dgvWareHouse.Columns)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                    dgvWareHouse.Columns[strSearchField].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void dgvRack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells[objBASEFILEDS.Primary_id].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    strSearchField = dgvRack.Columns[e.ColumnIndex].Name;
                    foreach (DataGridViewColumn row in dgvRack.Columns)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                    dgvRack.Columns[strSearchField].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void dgvBin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvView = (DataGridView)sender;
            if (dgvView != null && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvView.CurrentRow.Cells[objBASEFILEDS.Primary_id].Value.ToString();
                AddFieldToHashTable();
                GetFieldValueFromHashTable();
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    strSearchField = dgvBin.Columns[e.ColumnIndex].Name;
                    foreach (DataGridViewColumn row in dgvBin.Columns)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                    dgvBin.Columns[strSearchField].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }

        private void txtRackWH_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objfrmPopup = new frmPopup(objBASEFILEDS.HTMAIN, "WAREHOUSE_MAST", "", "WAREHOUSEID,WAREHOUSENAME", "WAREHOUSENAME;Warehouse", "Please select", "",false,"");
                    //objfrmPopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmPopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmPopup.ObjBFD = objBASEFILEDS;
                    objfrmPopup.ShowDialog();
                    txt.Text = objBASEFILEDS.HTMAIN["WAREHOUSENAME"].ToString();
                    txtRackWHId.Text = objBASEFILEDS.HTMAIN["WAREHOUSEID"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtBinRack_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                try
                {
                    TextBox txt = (TextBox)sender;
                    if (e.KeyData == Keys.F2)
                    {
                        frmPopup objfrmPopup = new frmPopup(objBASEFILEDS.HTMAIN, "RACK_MAST", "", "RACKID,RACKNAME", "RACKNAME;Rack,WAREHOUSENAME;WareHouse", "Please select", "", false, "");
                        //objfrmPopup.objCompany = objBASEFILEDS.ObjCompany;
                        //objfrmPopup.objControlSet = objBASEFILEDS.ObjControlSet;
                        objfrmPopup.ObjBFD = objBASEFILEDS;
                        objfrmPopup.ShowDialog();
                        txt.Text = objBASEFILEDS.HTMAIN["RACKNAME"].ToString();
                        txtBinRackId.Text = objBASEFILEDS.HTMAIN["RACKID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtBinMin_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBinMax_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtWHSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtWHSearch.Text != "")
                BindWareHouseGrid(txtWHSearch.Text);
            else
                BindWareHouseGrid("%");
        }

        private void txtRackSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtRackSearch.Text != "")
                BindRackGrid(txtRackSearch.Text);
            else
                BindRackGrid("%");
        }

        private void txtBinSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBinSearch.Text != "")
                BindBinGrid(txtBinSearch.Text);
            else
                BindBinGrid("%");
        }
    }
}
