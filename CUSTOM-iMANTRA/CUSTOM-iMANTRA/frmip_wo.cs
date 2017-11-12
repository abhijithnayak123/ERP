using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmip_wo : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private Hashtable _hashItemList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalipwomain = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalipwomainTemp = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string key = "";
        bool flgSelect = false;

        public Hashtable HashItemList
        {
            get { return _hashItemList; }
            set { _hashItemList = value; }
        }

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmip_wo()
        {
            InitializeComponent();
        }

        private void frmip_wo_Load(object sender, EventArgs e)
        {
            dgvwo.AutoGenerateColumns = false;
            dgvworm.AutoGenerateColumns = false;

            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aptserial", "");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            DataSet dsetWOwithBOM = objdblayer.dsprocedure("ISP_GET_IPWO_HEADER", htparam);

            if (dsetWOwithBOM != null && dsetWOwithBOM.Tables.Count != 0 && dsetWOwithBOM.Tables[0].Rows.Count != 0)
            {
                dgvwo.DataSource = dsetWOwithBOM.Tables[0];
                dgvwo.Update();
                int i = 0;
                foreach (DataRow row in dsetWOwithBOM.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvwo.Columns)
                    {
                        if (dsetWOwithBOM.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvwo.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
            }

            Bind_Item_Grid();
            Get_Item_Details();

            //if (ACTIVE_BL.Tran_mode != "add_mode")
            //{
            //    EditWOIP();
            //}

            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashMaintbl.Clear();
                objBLFD.HASHTABLES = objHashtables;
                EditWOIP();
                dgvwo.Enabled = false;
                dgvworm.Enabled = false;
                btnDone.Enabled = false;
            }

            objHashtables = objBLFD.HASHTABLES;
            if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
            {
                if (_hashlocalipwomain != null)
                {
                    _hashlocalipwomain.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
                {
                    _hashlocalipwomain[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                    {
                        ((Hashtable)_hashlocalipwomain[entry.Key])[entry1.Key] = entry1.Value;
                    }
                }
                foreach (DataGridViewRow row in dgvwo.Rows)
                {
                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                    {
                        key = row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_prod_cd"].Value.ToString();
                        if (key == entry.Key.ToString().Split(',')[0] + "," + entry.Key.ToString().Split(',')[1] + "," + entry.Key.ToString().Split(',')[2])
                        {
                            if (((Hashtable)entry.Value).Count != 0)
                            {
                                //row.Cells["issue_qty"].Value = (decimal.Parse(((Hashtable)entry.Value)["req_qty"].ToString()) * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString())) / decimal.Parse(((Hashtable)entry.Value)["rm_qty"].ToString());
                                row.Cells["sel"].Value = true;
                                break;
                            }
                        }
                    }
                }
                foreach (DataGridViewRow row in dgvworm.Rows)
                {
                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                    {
                        if (((Hashtable)entry.Value).Count != 0 && row.Cells["prod_cd"].Value.ToString() == entry.Key.ToString().Split(',')[3].ToString())
                        {
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                if (dgvworm.Columns.Contains(entry1.Key.ToString()))
                                {
                                    row.Cells[entry1.Key.ToString()].Value = entry1.Value;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                objHashtables = new BLHT();
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Work Order Details";
        }

        private void EditWOIP()
        {
            string key = "";

            Hashtable htparam = new Hashtable();
            htparam.Add("@awo_id", 0);
            htparam.Add("@awo_cd", "");
            htparam.Add("@awoserial", "");
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
            DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM_SELECTED", htparam);
            if (dsetIPwithWOItem != null && dsetIPwithWOItem.Tables.Count != 0 && dsetIPwithWOItem.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetIPwithWOItem.Tables[0].Rows)
                {
                    key = r["ref_tran_id1"].ToString() + "," + r["ref_ptserial1"].ToString() + "," + r["ref_prod_cd1"].ToString() + "," + r["prod_cd"].ToString();
                    if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetIPwithWOItem.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashMaintbl[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void dgvwo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                Get_Item_Details();
                //Calculate_RM_Quantity(dgvwo, dgvwo.CurrentRow.Cells["issue_qty"].Value);
            }
        }

        private void dgvwo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "sel")
                {
                    flgSelect = false;
                    if (gridview.CurrentRow.Cells["sel"].Value != null && gridview.CurrentRow.Cells["sel"].Value.ToString() != "")
                    {
                        if (gridview.CurrentRow.Cells["sel"].Value.ToString() == "1" || gridview.CurrentRow.Cells["sel"].Value.ToString() == "True")
                        {
                            flgSelect = true;
                        }
                        else if (gridview.CurrentRow.Cells["sel"].Value.ToString() == "0" || gridview.CurrentRow.Cells["sel"].Value.ToString() == "False")
                        {
                            flgSelect = false;
                        }
                    }
                    if (flgSelect)
                    {
                        Calculate_RM_Quantity(gridview, gridview.CurrentRow.Cells["issue_qty"].Value);
                        BindGridToHashtable(1);
                    }
                    else
                    {
                        foreach (DictionaryEntry entry in _hashlocalipwomain)
                        {
                            if (!_hashlocalipwomainTemp.Contains(entry.Key))
                            {
                                _hashlocalipwomainTemp[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)_hashlocalipwomainTemp[entry.Key])[entry1.Key] = entry1.Value;
                            }
                        }
                        foreach (DictionaryEntry entry in _hashlocalipwomainTemp)
                        {
                            if (entry.Key.ToString().Contains(dgvwo.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + dgvwo.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + dgvwo.CurrentRow.Cells["ref_prod_cd"].Value.ToString()))
                                ((Hashtable)_hashlocalipwomain[entry.Key]).Clear();
                        }
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[0];
                        chk.Value = false;
                        gridview.CurrentRow.Cells["issue_qty"].Value = 0;
                        Calculate_RM_Quantity(gridview, gridview.CurrentRow.Cells["issue_qty"].Value);
                    }
                }
            }
        }

        private void BindGridToHashtable(int i = 0)
        {
            decimal decVal = 0;
            foreach (DataGridViewRow row1 in dgvwo.Rows)
            {
                flgSelect = false;
                if (row1.Cells["sel"].Value != null && row1.Cells["sel"].Value.ToString() != "")
                {
                    if (row1.Cells["sel"].Value.ToString() == "1" || row1.Cells["sel"].Value.ToString() == "True")
                    {
                        flgSelect = true;
                    }
                    else if (row1.Cells["sel"].Value.ToString() == "0" || row1.Cells["sel"].Value.ToString() == "False")
                    {
                        flgSelect = false;
                    }
                }
                if (flgSelect)
                {
                    foreach (DataGridViewRow row in dgvworm.Rows)
                    {
                        if (row1.Cells["ref_tran_id"].Value.ToString() + "," + row1.Cells["ref_ptserial"].Value.ToString() + "," + row1.Cells["ref_prod_cd"].Value.ToString() == row.Cells["ref_tran_id1"].Value.ToString() + "," + row.Cells["ref_ptserial1"].Value.ToString() + "," + row.Cells["ref_prod_cd1"].Value.ToString())
                        {
                            key = row1.Cells["ref_tran_id"].Value.ToString() + "," + row1.Cells["ref_ptserial"].Value.ToString() + "," + row1.Cells["ref_prod_cd"].Value.ToString() + "," + row.Cells["prod_cd"].Value.ToString();
                            if (!_hashlocalipwomain.Contains(key))
                            {
                                _hashlocalipwomain[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DataGridViewColumn column in dgvworm.Columns)
                            {
                                ((Hashtable)_hashlocalipwomain[key])[column.Name] = row.Cells[column.Name].Value;
                                if (i == 1 && column.Name == "ip_wo_id")
                                {
                                    ((Hashtable)_hashlocalipwomain[key])["ip_wo_id"] = "0";
                                }
                            }
                            foreach (DataGridViewColumn column1 in dgvwo.Columns)
                            {
                                if (column1.Name.ToString().ToLower() != "sel" && column1.Name.ToString().ToLower() != "issue_qty")
                                {
                                    ((Hashtable)_hashlocalipwomain[key])[column1.Name] = row1.Cells[column1.Name].Value;
                                }
                                else if (column1.Name.ToString().ToLower() == "issue_qty")
                                {
                                    if (row.Cells["issue_qty"].Value != null && row.Cells["issue_qty"].Value.ToString() != "")
                                    {
                                        if (row.Cells["req_qty"].Value != null && row.Cells["req_qty"].Value.ToString() != "" && row.Cells["pend_qty"].Value != null && row.Cells["pend_qty"].Value.ToString() != "")
                                        {
                                            if (decimal.Parse(row.Cells["req_qty"].Value.ToString()) < decimal.Parse(row.Cells["pend_qty"].Value.ToString()))
                                            {
                                                if (decimal.Parse(row.Cells["req_qty"].Value.ToString()) < decimal.Parse(row.Cells["issue_qty"].Value.ToString()))
                                                {
                                                    decVal = (decimal.Parse(row.Cells["issue_qty"].Value.ToString()) - (decimal.Parse(row.Cells["pend_qty"].Value.ToString()) - decimal.Parse(row.Cells["req_qty"].Value.ToString()))) * decimal.Parse(((Hashtable)_hashlocalipwomain[key])["bom_qty"].ToString()) / decimal.Parse(((Hashtable)_hashlocalipwomain[key])["rm_qty"].ToString());
                                                }
                                                else
                                                {
                                                    decVal = (decimal.Parse(row.Cells["issue_qty"].Value.ToString())) * decimal.Parse(((Hashtable)_hashlocalipwomain[key])["bom_qty"].ToString()) / decimal.Parse(((Hashtable)_hashlocalipwomain[key])["rm_qty"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                decVal = (decimal.Parse(row.Cells["issue_qty"].Value.ToString())) * decimal.Parse(((Hashtable)_hashlocalipwomain[key])["bom_qty"].ToString()) / decimal.Parse(((Hashtable)_hashlocalipwomain[key])["rm_qty"].ToString());
                                            }
                                        }
                                    }
                                    ((Hashtable)_hashlocalipwomain[key])["main_issued_qty"] = decVal;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Calculate_RM_Quantity(DataGridView gridview, object main_issue_qty)
        {
            if (main_issue_qty != null && main_issue_qty.ToString() != "")
            {
                foreach (DataGridViewRow row in dgvworm.Rows)
                {
                    row.Cells["req_qty"].Value = decimal.Parse(main_issue_qty.ToString()) * decimal.Parse(row.Cells["rm_qty"].Value.ToString()) / decimal.Parse(row.Cells["bom_qty"].Value.ToString());
                    decimal qty_value = (decimal.Parse(gridview.CurrentRow.Cells["ref_tran_qty"].Value.ToString()) * decimal.Parse(row.Cells["rm_qty"].Value.ToString()) / decimal.Parse(row.Cells["bom_qty"].Value.ToString())); //- decimal.Parse(row.Cells["issued_qty"].Value.ToString());
                    decimal pend_qty = (decimal.Parse(gridview.CurrentRow.Cells["issued_qty"].Value.ToString()) * decimal.Parse(row.Cells["rm_qty"].Value.ToString()) / decimal.Parse(row.Cells["bom_qty"].Value.ToString())) - decimal.Parse(row.Cells["issued_qty"].Value.ToString());
                    #region
                    //if (qty_value < decimal.Parse(row.Cells["req_qty"].Value.ToString()))
                    //{
                    //    row.Cells["pend_qty"].Value = qty_value;//decimal.Parse(gridview.CurrentRow.Cells["ref_tran_qty"].Value.ToString()) * decimal.Parse(row.Cells["rm_qty"].Value.ToString()) - decimal.Parse(row.Cells["issued_qty"].Value.ToString());
                    //}
                    //else
                    //{
                    //    row.Cells["pend_qty"].Value = row.Cells["req_qty"].Value.ToString();
                    //}
                    //if (decimal.Parse(row.Cells["stk_qty"].Value.ToString()) < decimal.Parse(row.Cells["pend_qty"].Value.ToString()))
                    //{
                    //    row.Cells["pend_qty"].Value = row.Cells["stk_qty"].Value.ToString();
                    //}
                    #endregion
                    row.Cells["pend_qty"].Value = pend_qty + decimal.Parse(row.Cells["req_qty"].Value.ToString());
                    row.Cells["issue_qty"].Value = row.Cells["pend_qty"].Value;
                }
            }
        }

        private void dgvwo_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (gridview.CurrentCell.OwningColumn.Name == "issue_qty")
                {
                    if (decimal.Parse(e.FormattedValue.ToString()) <= decimal.Parse(gridview.CurrentRow.Cells["ref_tran_qty"].Value.ToString()) - decimal.Parse(gridview.CurrentRow.Cells["issued_qty"].Value.ToString()))
                    {
                        flgSelect = false;
                        if (gridview.CurrentRow.Cells["sel"].Value != null && gridview.CurrentRow.Cells["sel"].Value.ToString() != "")
                        {
                            if (gridview.CurrentRow.Cells["sel"].Value.ToString() == "1" || gridview.CurrentRow.Cells["sel"].Value.ToString() == "True")
                            {
                                flgSelect = true;
                            }
                            else if (gridview.CurrentRow.Cells["sel"].Value.ToString() == "0" || gridview.CurrentRow.Cells["sel"].Value.ToString() == "False")
                            {
                                flgSelect = false;
                            }
                        }
                        if (flgSelect)
                        {
                            Calculate_RM_Quantity(gridview, e.FormattedValue);
                            BindGridToHashtable();
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Issueing Quantity should be lesser than or equal to Available Work Order Quantity", "Validation");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void Get_Item_Details()
        {
            Hashtable htparam = new Hashtable();
            htparam.Add("@awo_id", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_tran_id"].Value != null && dgvwo.CurrentRow.Cells["ref_tran_id"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_tran_id"].Value.ToString() : "0");
            htparam.Add("@awo_cd", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_tran_cd"].Value != null && dgvwo.CurrentRow.Cells["ref_tran_cd"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_tran_cd"].Value.ToString() : "");
            htparam.Add("@awoserial", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_ptserial"].Value != null && dgvwo.CurrentRow.Cells["ref_ptserial"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_ptserial"].Value.ToString() : "");
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
            //DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM", htparam);
            DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM_SELECTED", htparam);
            #region
            //Hashtable htparam = new Hashtable();
            //htparam.Add("@awo_id", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_tran_id"].Value != null && dgvwo.CurrentRow.Cells["ref_tran_id"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_tran_id"].Value.ToString() : "0");
            //htparam.Add("@awo_cd", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_tran_cd"].Value != null && dgvwo.CurrentRow.Cells["ref_tran_cd"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_tran_cd"].Value.ToString() : "");
            //htparam.Add("@awoserial", dgvwo.CurrentRow != null && dgvwo.CurrentRow.Cells["ref_ptserial"].Value != null && dgvwo.CurrentRow.Cells["ref_ptserial"].Value.ToString() != "" ? dgvwo.CurrentRow.Cells["ref_ptserial"].Value.ToString() : "");
            //htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            //htparam.Add("@acompid", objBLFD.ObjCompany.Compid);
            //htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            //htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");

            //DataSet dsetIPwithWOItem = objdblayer.dsprocedure("ISP_GET_IPWO_ITEM_SELECTED", htparam);
            #endregion
            if (dsetIPwithWOItem != null && dsetIPwithWOItem.Tables.Count != 0 && dsetIPwithWOItem.Tables[0].Rows.Count != 0)
            {
                while (dgvworm.Rows.Count > 0)
                {
                    if (!dgvworm.Rows[0].IsNewRow)
                    {
                        dgvworm.Rows.RemoveAt(0);
                    }
                }
                int i = 0;
                foreach (DataRow row in dsetIPwithWOItem.Tables[0].Rows)
                {
                    dgvworm.Rows.Add(1);
                    foreach (DataGridViewColumn column in dgvworm.Columns)
                    {
                        if (dsetIPwithWOItem.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvworm.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                //foreach (DataGridViewRow row in dgvworm.Rows)
                //{
                //    foreach (DictionaryEntry entry in _hashlocalipwomain)
                //    {
                //        if (((Hashtable)entry.Value).Count != 0 && row.Cells["prod_cd"].Value.ToString() == entry.Key.ToString().Split(',')[3].ToString())
                //        {
                //            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                //            {
                //                if (dgvworm.Columns.Contains(entry1.Key.ToString()))
                //                {
                //                    row.Cells[entry1.Key.ToString()].Value = entry1.Value;
                //                }
                //            }
                //        }
                //    }
                //}
            }
        }
        private void Bind_Item_Grid()
        {
            DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
            txtcol.HeaderText = "Primary Key";
            txtcol.Name = "ip_wo_id";
            dgvworm.Columns.Add(txtcol);
            dgvworm.Columns[txtcol.Name].Visible = false;
            dgvworm.Columns[txtcol.Name].ReadOnly = true;
            dgvworm.Columns[txtcol.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol1 = new DataGridViewTextBoxColumn();
            txtcol1.HeaderText = "Raw Materials";
            txtcol1.Name = "rm_nm";
            dgvworm.Columns.Add(txtcol1);
            dgvworm.Columns[txtcol1.Name].Visible = true;
            dgvworm.Columns[txtcol1.Name].ReadOnly = true;
            dgvworm.Columns[txtcol1.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol1.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol2 = new DataGridViewTextBoxColumn();
            txtcol2.HeaderText = "prod_cd";
            txtcol2.Name = "prod_cd";
            dgvworm.Columns.Add(txtcol2);
            dgvworm.Columns[txtcol2.Name].Visible = false;
            dgvworm.Columns[txtcol2.Name].ReadOnly = false;
            dgvworm.Columns[txtcol2.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            DataGridViewTextBoxColumn txtcol3 = new DataGridViewTextBoxColumn();
            txtcol3.HeaderText = "Qty";
            txtcol3.Name = "rm_qty";
            dgvworm.Columns.Add(txtcol3);
            dgvworm.Columns[txtcol3.Name].Visible = false;
            dgvworm.Columns[txtcol3.Name].ReadOnly = false;
            dgvworm.Columns[txtcol3.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn txtcol4 = new DataGridViewTextBoxColumn();
            txtcol4.HeaderText = "Required Qty";
            txtcol4.Name = "req_qty";
            dgvworm.Columns.Add(txtcol4);
            dgvworm.Columns[txtcol4.Name].Visible = true;
            dgvworm.Columns[txtcol4.Name].ReadOnly = true;
            dgvworm.Columns[txtcol4.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol4.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn txtcol5 = new DataGridViewTextBoxColumn();
            txtcol5.HeaderText = "Already Issued Qty";
            txtcol5.Name = "issued_qty";
            dgvworm.Columns.Add(txtcol5);
            dgvworm.Columns[txtcol5.Name].Visible = true;
            dgvworm.Columns[txtcol5.Name].ReadOnly = true;
            dgvworm.Columns[txtcol5.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol5.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol6 = new DataGridViewTextBoxColumn();
            txtcol6.HeaderText = "Total Pending Qty";
            txtcol6.Name = "pend_qty";
            dgvworm.Columns.Add(txtcol6);
            dgvworm.Columns[txtcol6.Name].Visible = true;
            dgvworm.Columns[txtcol6.Name].ReadOnly = true;
            dgvworm.Columns[txtcol6.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol6.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol7 = new DataGridViewTextBoxColumn();
            txtcol7.HeaderText = "Stock";
            txtcol7.Name = "stk_qty";
            dgvworm.Columns.Add(txtcol7);
            dgvworm.Columns[txtcol7.Name].Visible = true;
            dgvworm.Columns[txtcol7.Name].ReadOnly = true;
            dgvworm.Columns[txtcol7.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvworm.Columns[txtcol7.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol8 = new DataGridViewTextBoxColumn();
            txtcol8.HeaderText = "Issuing Qty";
            txtcol8.Name = "issue_qty";
            dgvworm.Columns.Add(txtcol8);
            dgvworm.Columns[txtcol8.Name].Visible = true;
            dgvworm.Columns[txtcol8.Name].ReadOnly = false;
            dgvworm.Columns[txtcol8.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol9 = new DataGridViewTextBoxColumn();
            txtcol9.HeaderText = "BOM Qt";
            txtcol9.Name = "bom_qty";
            dgvworm.Columns.Add(txtcol9);
            dgvworm.Columns[txtcol9.Name].Visible = false;
            dgvworm.Columns[txtcol9.Name].ReadOnly = false;
            dgvworm.Columns[txtcol9.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol10 = new DataGridViewTextBoxColumn();
            txtcol10.HeaderText = "Ref_tran_no";
            txtcol10.Name = "ref_tran_no1";
            dgvworm.Columns.Add(txtcol10);
            dgvworm.Columns[txtcol10.Name].Visible = false;
            dgvworm.Columns[txtcol10.Name].ReadOnly = false;
            dgvworm.Columns[txtcol10.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol11 = new DataGridViewTextBoxColumn();
            txtcol11.HeaderText = "ref_ptserial";
            txtcol11.Name = "ref_ptserial1";
            dgvworm.Columns.Add(txtcol11);
            dgvworm.Columns[txtcol11.Name].Visible = false;
            dgvworm.Columns[txtcol11.Name].ReadOnly = false;
            dgvworm.Columns[txtcol11.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol12 = new DataGridViewTextBoxColumn();
            txtcol12.HeaderText = "ref_prod_cd";
            txtcol12.Name = "ref_prod_cd1";
            dgvworm.Columns.Add(txtcol12);
            dgvworm.Columns[txtcol12.Name].Visible = false;
            dgvworm.Columns[txtcol12.Name].ReadOnly = false;
            dgvworm.Columns[txtcol12.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn txtcol13 = new DataGridViewTextBoxColumn();
            txtcol13.HeaderText = "Ref_tran_id";
            txtcol13.Name = "ref_tran_id1";
            dgvworm.Columns.Add(txtcol13);
            dgvworm.Columns[txtcol13.Name].Visible = false;
            dgvworm.Columns[txtcol13.Name].ReadOnly = false;
            dgvworm.Columns[txtcol13.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void dgvworm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvworm_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (gridview.CurrentCell.OwningColumn.Name == "issue_qty" && e.FormattedValue != null && e.FormattedValue.ToString() != "" && gridview.CurrentRow.Cells["pend_qty"].Value != null && gridview.CurrentRow.Cells["pend_qty"].Value.ToString() != "")
                {
                    if (decimal.Parse(e.FormattedValue.ToString()) <= decimal.Parse(gridview.CurrentRow.Cells["pend_qty"].Value.ToString()))
                    {
                        flgSelect = false;
                        if (dgvwo.CurrentRow.Cells["sel"].Value != null && dgvwo.CurrentRow.Cells["sel"].Value.ToString() != "")
                        {
                            if (dgvwo.CurrentRow.Cells["sel"].Value.ToString() == "1" || dgvwo.CurrentRow.Cells["sel"].Value.ToString() == "True")
                            {
                                flgSelect = true;
                            }
                            else if (dgvwo.CurrentRow.Cells["sel"].Value.ToString() == "0" || dgvwo.CurrentRow.Cells["sel"].Value.ToString() == "False")
                            {
                                flgSelect = false;
                            }
                        }
                        if (flgSelect)
                        {
                            gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            BindGridToHashtable();
                            e.Cancel = !ValidateEdit();
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Issueing Quantity should be lesser than or equal to Pending Quantity", "Validation");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Hashtable objhashitem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            Hashtable objhashitem_issued_qty = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            bool blnFlg = false;
            if (ACTIVE_BL.Tran_mode != "view_mode")
            {
                if (ValidateEdit())
                {
                    // main_issued_qty = 0;
                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                    {
                        if (!objHashtables.HashMaintbl.Contains(entry.Key))
                        {
                            objHashtables.HashMaintbl[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            key = entry.Key.ToString().Split(',')[0] + "," + entry.Key.ToString().Split(',')[1] + "," + entry.Key.ToString().Split(',')[2];


                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)objHashtables.HashMaintbl[entry.Key])[entry1.Key] = entry1.Value;
                            }

                            if (objhashitem_issued_qty != null && objhashitem_issued_qty.Count != 0)
                            {
                                if (objhashitem_issued_qty.Contains(key))
                                {
                                    if (decimal.Parse(((Hashtable)entry.Value)["main_issued_qty"].ToString()) > decimal.Parse(objhashitem_issued_qty[key].ToString()))
                                    {
                                        objhashitem_issued_qty[key] = ((Hashtable)entry.Value)["main_issued_qty"].ToString();
                                    }
                                }
                                else
                                {
                                    objhashitem_issued_qty[key] = ((Hashtable)entry.Value)["main_issued_qty"].ToString();
                                }
                            }
                            else
                            {
                                objhashitem_issued_qty[key] = ((Hashtable)entry.Value)["main_issued_qty"].ToString();
                            }

                            if (objhashitem.Contains(((Hashtable)entry.Value)["rm_nm"].ToString()))
                            {
                                objhashitem[((Hashtable)entry.Value)["rm_nm"].ToString()] = decimal.Parse(objhashitem[((Hashtable)entry.Value)["rm_nm"].ToString()].ToString()) + decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString());
                            }
                            else
                            {
                                objhashitem[((Hashtable)entry.Value)["rm_nm"].ToString()] = ((Hashtable)entry.Value)["issue_qty"].ToString();
                            }
                            //if()
                            //((Hashtable)objHashtables.HashMaintbl[entry.Key])["main_issued_qty"] = main_issued_qty;
                            #region
                            //if (decimal.Parse(((Hashtable)entry.Value)["req_qty"].ToString()) == decimal.Parse(((Hashtable)entry.Value)["pend_qty"].ToString()))
                            //{
                            //    if (main_issued_qty == 0 || main_issued_qty >= (decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString())) * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString()) / decimal.Parse(((Hashtable)entry.Value)["ref_tran_qty"].ToString()))
                            //    {
                            //        main_issued_qty = (decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString())) * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString()) / decimal.Parse(((Hashtable)entry.Value)["ref_tran_qty"].ToString());
                            //        if (main_issued_qty < 0)
                            //        {
                            //            main_issued_qty *= -1;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (main_issued_qty == 0 || main_issued_qty >= (decimal.Parse(((Hashtable)entry.Value)["req_qty"].ToString()) - decimal.Parse(((Hashtable)entry.Value)["pend_qty"].ToString())) * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString()) / decimal.Parse(((Hashtable)entry.Value)["ref_tran_qty"].ToString()))
                            //    {
                            //        vardec = decimal.Parse(((Hashtable)entry.Value)["pend_qty"].ToString()) - (decimal.Parse(((Hashtable)entry.Value)["pend_qty"].ToString()) - decimal.Parse(((Hashtable)entry.Value)["req_qty"].ToString()));
                            //        main_issued_qty = vardec * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString()) / decimal.Parse(((Hashtable)entry.Value)["ref_tran_qty"].ToString());
                            //        if (main_issued_qty < 0)
                            //        {
                            //            main_issued_qty *= -1;
                            //        }
                            //    }
                            //}
                            //((Hashtable)objHashtables.HashMaintbl[entry.Key])["main_issued_qty"] = (decimal.Parse(objhashitem[((Hashtable)entry.Value)["req_qty"].ToString()].ToString()) - decimal.Parse(((Hashtable)entry.Value)["pend_qty"].ToString())) * decimal.Parse(((Hashtable)entry.Value)["bom_qty"].ToString()) / decimal.Parse(((Hashtable)entry.Value)["wo_qty"].ToString());
                            #endregion
                        }
                        else
                        {
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key]).Clear();
                        }
                        //((Hashtable)objHashtables.HashMaintbl[entry.Key])["main_issued_qty"] = main_issued_qty;
                    }

                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            key = entry.Key.ToString().Split(',')[0] + "," + entry.Key.ToString().Split(',')[1] + "," + entry.Key.ToString().Split(',')[2];
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key])["main_issued_qty"] = objhashitem_issued_qty[key];
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key])["compid"] = objBLFD.ObjCompany.Compid;
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key])["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                            if (objhashitem.Contains(((Hashtable)entry.Value)["rm_nm"].ToString()))
                            {
                                if (decimal.Parse(objhashitem[((Hashtable)entry.Value)["rm_nm"].ToString()].ToString()) > decimal.Parse(((Hashtable)entry.Value)["stk_qty"].ToString()))
                                {
                                    AutoClosingMessageBox.Show("Sorry!! stock is not available for " + ((Hashtable)entry.Value)["rm_nm"].ToString(), "Validation");
                                    blnFlg = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (!blnFlg)
                    {
                        objBLFD.HASHTABLES = objHashtables;
                        objBLFD.HTITEM.Clear();
                        int i = 0;
                        foreach (DictionaryEntry entry in objHashtables.HashMaintbl)
                        {
                            i++;
                            if (!objBLFD.HTITEM.Contains(i.ToString().Trim().PadLeft(5, '0')))
                            {
                                objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            if (((Hashtable)entry.Value).Count != 0 && decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString()) > 0)
                            {
                                foreach (DictionaryEntry entry1 in objBLFD.htitem_details)
                                {
                                    ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])[entry1.Key] = entry1.Value;
                                }
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["PTSERIAL"] = i.ToString().Trim().PadLeft(5, '0');
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["PROD_NO"] = i.ToString();
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["TRAN_CD"] = objBLFD.Code;
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["TRAN_DT"] = DateTime.Now.ToString("yyyy/MM/dd");
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])[objBLFD.Primary_id] = "0";
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["prod_nm"] = ((Hashtable)entry.Value)["rm_nm"].ToString();
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["prod_cd"] = ((Hashtable)entry.Value)["prod_cd"].ToString();
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["qty"] = ((Hashtable)entry.Value)["issue_qty"].ToString();
                            }
                            else
                            {
                                ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')]).Clear();
                            }
                        }
                    }
                }
                else
                {
                    blnFlg = true;
                }
            }
            if (!blnFlg)
            {
                this.Close();
            }
        }

        private bool ValidateEdit()
        {
            if (objBLFD.Tran_id != "0")
            {
                DataSet dsetpd = objdblayer.dsquery("select SUM(qty) op_qty,ref_tran_id,ref_tran_cd,ref_ptserial from OP_WO_DET group by ref_tran_id,ref_tran_cd,ref_ptserial");
                DataSet dsetceref = objdblayer.dsquery("select sum(issue_qty) issue_qty,ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,rm_nm,rm_qty from ip_wo_det where tran_id!=" + objBLFD.Tran_id + " and tran_cd='" + objBLFD.Code + "' group by ref_tran_no,ref_tran_id,ref_tran_cd,ref_ptserial,bom_qty,rm_qty,rm_nm");
                foreach (DataRow rowpd in dsetpd.Tables[0].Rows)
                {
                    if (dsetceref != null && dsetceref.Tables.Count != 0 && dsetceref.Tables[0].Rows.Count != 0)
                    {
                        foreach (DataRow rowceissued in dsetceref.Tables[0].Rows)
                        {
                            if (rowceissued["ref_tran_id"].ToString() + "," + rowceissued["ref_tran_cd"].ToString() + "," + rowceissued["ref_ptserial"].ToString() == rowpd["ref_tran_id"].ToString() + "," + rowpd["ref_tran_cd"].ToString() + "," + rowpd["ref_ptserial"].ToString())
                            {
                                if (decimal.Parse(rowceissued["issue_qty"].ToString()) < decimal.Parse(rowpd["op_qty"].ToString()) * decimal.Parse(rowceissued["rm_qty"].ToString()))
                                {
                                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                                    {
                                        if (((Hashtable)entry.Value).Count != 0)
                                        {
                                            if (entry.Key.ToString().Split(',')[0] + "," + entry.Key.ToString().Split(',')[1] == rowpd["ref_tran_id"].ToString() + "," + rowpd["ref_ptserial"].ToString())
                                            {
                                                if (decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString()) + decimal.Parse(rowceissued["issue_qty"].ToString()) < decimal.Parse(rowpd["op_qty"].ToString()) * decimal.Parse(((Hashtable)entry.Value)["rm_qty"].ToString()))
                                                {
                                                    AutoClosingMessageBox.Show(" Sorry Editing issue qty is not posible because it is used in production", "Validation");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DictionaryEntry entry in _hashlocalipwomain)
                        {
                            if (((Hashtable)entry.Value).Count != 0)
                            {
                                if (entry.Key.ToString().Split(',')[0] + "," + entry.Key.ToString().Split(',')[1] == rowpd["ref_tran_id"].ToString() + "," + rowpd["ref_ptserial"].ToString())
                                {
                                    if (decimal.Parse(((Hashtable)entry.Value)["issue_qty"].ToString()) < decimal.Parse(rowpd["op_qty"].ToString()) * decimal.Parse(((Hashtable)entry.Value)["rm_qty"].ToString()))
                                    {
                                        AutoClosingMessageBox.Show(" Sorry Editing issue qty is not posible because it is used in production", "Validation");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
