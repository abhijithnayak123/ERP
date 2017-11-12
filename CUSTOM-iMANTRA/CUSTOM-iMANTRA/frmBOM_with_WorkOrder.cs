using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CUSTOM_iMANTRA_BL;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmBOM_with_WorkOrder : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private Hashtable _hashItemList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalwobomain = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalwoboitem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

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

        public frmBOM_with_WorkOrder()
        {
            InitializeComponent();
        }

        private void frmBOM_with_WorkOrder_Load(object sender, EventArgs e)
        {
            dgvBOM.AutoGenerateColumns = false;
            dgvRM_For_BOM.AutoGenerateColumns = false;

            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashMaintbl.Clear();
                objHashtables.HashItemtbl.Clear();
                objBLFD.HASHTABLES = objHashtables;
            }
            if (_hashItemList["tran_id"].ToString() == "0" || ACTIVE_BL.Tran_mode == "view_mode")
            {
                btnAdd.Visible = false;
                btnRemove.Visible = false;
            }

            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", _hashItemList["tran_id"].ToString() != "0" ? objBLFD.Tran_id : _hashItemList["tran_id"].ToString());
            htparam.Add("@atran_cd", _hashItemList["tran_id"].ToString() != "0" ? objBLFD.Code : "");
            htparam.Add("@aptserial", _hashItemList["ptserial"].ToString());
            htparam.Add("@aprod_nm", _hashItemList["prod_nm"].ToString());
            htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());

            DataSet dsetWOwithBOM = objdblayer.dsprocedure("ISP_GET_WO_BO_DETAILS", htparam);
            if (dsetWOwithBOM != null && dsetWOwithBOM.Tables.Count != 0 && dsetWOwithBOM.Tables[0].Rows.Count != 0)
            {
                dgvBOM.DataSource = dsetWOwithBOM.Tables[0];
                dgvBOM.Update();
                int i = 0;
                foreach (DataRow row in dsetWOwithBOM.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvBOM.Columns)
                    {
                        if (dsetWOwithBOM.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvBOM.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                        if (column.Name.ToLower() == "req_qty")
                        {
                            dgvBOM.Rows[i].Cells[column.Name].Value = _hashItemList["qty"].ToString();
                        }
                    }
                    i++;
                }
            }
            Bind_Item_Grid();
            Get_Item_Details();
            objHashtables = objBLFD.HASHTABLES;
            if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
            {
                if (_hashlocalwobomain != null)
                {
                    _hashlocalwobomain.Clear();
                }
                if (_hashlocalwoboitem != null)
                {
                    _hashlocalwoboitem.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
                {
                    if (entry.Key.ToString() == _hashItemList["ptserial"].ToString())
                    {
                        _hashlocalwobomain[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)_hashlocalwobomain[entry.Key])[entry1.Key] = entry1.Value;
                        }
                    }
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashItemtbl)
                {
                    if (entry.Key.ToString().Split(',')[0] == _hashItemList["ptserial"].ToString())
                    {
                        _hashlocalwoboitem[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)_hashlocalwoboitem[entry.Key])[entry1.Key] = entry1.Value;
                        }
                    }
                }
                #region
                foreach (DataGridViewRow row in dgvBOM.Rows)
                {
                    foreach (DictionaryEntry entry in _hashlocalwobomain)
                    {
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvBOM.Columns.Contains(entry1.Key.ToString()))
                            {
                                row.Cells[entry1.Key.ToString()].Value = entry1.Value;
                            }
                        }
                    }
                    if (ACTIVE_BL.Tran_mode != "add_mode")
                    {
                        dgvBOM.Columns["sel"].ReadOnly = true;
                    }
                }
                if (_hashlocalwoboitem.Count != 0)
                {
                    while (dgvRM_For_BOM.Rows.Count > 0)
                    {
                        if (!dgvRM_For_BOM.Rows[0].IsNewRow)
                        {
                            dgvRM_For_BOM.Rows.RemoveAt(0);
                        }
                    }
                }
                //foreach (DictionaryEntry entry in _hashlocalwoboitem)
                //{
                //    if (((Hashtable)entry.Value).Count != 0)// && ((Hashtable)entry.Value)["woboitno"].ToString() == row.Cells["woboitno"].Value.ToString())
                //    {
                //        dgvRM_For_BOM.Rows.Add(1);
                //        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                //        {
                //            if (dgvRM_For_BOM.Columns.Contains(entry1.Key.ToString()))
                //            {
                //                dgvRM_For_BOM.CurrentRow.Cells[entry1.Key.ToString()].Value = entry1.Value;
                //            }
                //        }
                //    }
                //}
                int i = 0;
                foreach (DictionaryEntry entry in _hashlocalwoboitem)
                {
                    if (((Hashtable)entry.Value).Count != 0)// && ((Hashtable)entry.Value)["woboitno"].ToString() == row.Cells["woboitno"].Value.ToString())
                    {
                        dgvRM_For_BOM.Rows.Add(1);
                        foreach (DataGridViewColumn column in dgvRM_For_BOM.Columns)
                        {
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                if (dgvRM_For_BOM.Columns.Contains(entry1.Key.ToString()))
                                {
                                    dgvRM_For_BOM.Rows[i].Cells[entry1.Key.ToString()].Value = entry1.Value;
                                }
                            }
                        }
                        dgvRM_For_BOM.Rows[i].Cells["rm_req_qty"].Value = ((decimal.Parse(dgvRM_For_BOM.Rows[i].Cells["rm_qty"].Value.ToString()) * decimal.Parse(dgvBOM.CurrentRow.Cells["req_qty"].Value.ToString())) / decimal.Parse(dgvBOM.CurrentRow.Cells["bom_qty"].Value.ToString())).ToString();
                        i++;
                    }
                }
                #endregion
            }
            else
            {
                objHashtables = new BLHT();
            }

            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "WO With BOM";
        }

        private void Bind_Item_Grid()
        {
            DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
            txtcol.HeaderText = "woboitno";
            txtcol.Name = "woboitno";
            dgvRM_For_BOM.Columns.Add(txtcol);
            dgvRM_For_BOM.Columns[txtcol.Name].Visible = false;
            dgvRM_For_BOM.Columns[txtcol.Name].ReadOnly = true;
            dgvRM_For_BOM.Columns[txtcol.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvRM_For_BOM.Columns[txtcol.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol1 = new DataGridViewTextBoxColumn();
            txtcol1.HeaderText = "Product Name";
            txtcol1.Name = "prod_nm";
            dgvRM_For_BOM.Columns.Add(txtcol1);
            dgvRM_For_BOM.Columns[txtcol1.Name].Visible = true;
            dgvRM_For_BOM.Columns[txtcol1.Name].ReadOnly = true;
            dgvRM_For_BOM.Columns[txtcol1.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvRM_For_BOM.Columns[txtcol1.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol2 = new DataGridViewTextBoxColumn();
            txtcol2.HeaderText = "prod_cd";
            txtcol2.Name = "prod_cd";
            dgvRM_For_BOM.Columns.Add(txtcol2);
            dgvRM_For_BOM.Columns[txtcol2.Name].Visible = false;
            dgvRM_For_BOM.Columns[txtcol2.Name].ReadOnly = false;
            dgvRM_For_BOM.Columns[txtcol2.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn txtcol3 = new DataGridViewTextBoxColumn();
            txtcol3.HeaderText = "Quantity";
            txtcol3.Name = "rm_qty";
            dgvRM_For_BOM.Columns.Add(txtcol3);
            dgvRM_For_BOM.Columns[txtcol3.Name].Visible = true;
            dgvRM_For_BOM.Columns[txtcol3.Name].ReadOnly = false;
            dgvRM_For_BOM.Columns[txtcol3.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewTextBoxColumn txtcol4 = new DataGridViewTextBoxColumn();
            txtcol4.HeaderText = "Required Quantity";
            txtcol4.Name = "rm_req_qty";
            dgvRM_For_BOM.Columns.Add(txtcol4);
            dgvRM_For_BOM.Columns[txtcol4.Name].Visible = true;
            dgvRM_For_BOM.Columns[txtcol4.Name].ReadOnly = true;
            dgvRM_For_BOM.Columns[txtcol4.Name].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgvRM_For_BOM.Columns[txtcol4.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void GetDetails()
        {
            foreach (DataGridViewRow row in dgvBOM.Rows)
            {
                key = _hashItemList["ptserial"].ToString();
                flgSelect = false;
                if (row.Cells["sel"].Value != null && row.Cells["sel"].Value.ToString() != "")
                {
                    if (row.Cells["sel"].Value.ToString() == "1" || row.Cells["sel"].Value.ToString() == "True")
                    {
                        flgSelect = true;
                    }
                    else if (row.Cells["sel"].Value.ToString() == "0" || row.Cells["sel"].Value.ToString() == "False")
                    {
                        flgSelect = false;
                    }
                }
                if (flgSelect)
                {
                    if (objHashtables == null || objHashtables.HashMaintbl == null || objHashtables.HashMaintbl.Count == 0 || !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataGridViewColumn column in dgvBOM.Columns)
                        {
                            if (column.Name.ToLower() != "req_qty" && column.Name.ToLower() != "sel")
                            {
                                ((Hashtable)objHashtables.HashMaintbl[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                        ((Hashtable)objHashtables.HashMaintbl[key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                        ((Hashtable)objHashtables.HashMaintbl[key])["tran_id"] = ACTIVE_BL.Tran_id;
                        ((Hashtable)objHashtables.HashMaintbl[key])["tran_cd"] = ACTIVE_BL.Code;
                        ((Hashtable)objHashtables.HashMaintbl[key])["ptserial"] = _hashItemList["ptserial"].ToString();
                        ((Hashtable)objHashtables.HashMaintbl[key])["prod_cd"] = _hashItemList["prod_cd"].ToString();
                    }
                }
            }
            int i = 0;
            foreach (DataGridViewRow row in dgvRM_For_BOM.Rows)
            {
                key = _hashItemList["ptserial"].ToString() + "," + row.Cells["prod_cd"].Value.ToString();
                if (objHashtables == null || objHashtables.HashItemtbl == null || objHashtables.HashItemtbl.Count == 0 || !objHashtables.HashItemtbl.Contains(key))
                {
                    objHashtables.HashItemtbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DataGridViewColumn column in dgvRM_For_BOM.Columns)
                    {
                        if (column.Name.ToLower() != "rm_req_qty")
                        {
                            if (column.Name == "woboitno" && (row.Cells[column.Name].Value == null || row.Cells[column.Name].Value.ToString() == "" || row.Cells[column.Name].Value.ToString() == "0"))
                            {
                                ((Hashtable)objHashtables.HashItemtbl[key])[column.Name] = i + 1;
                            }
                            else if (column.Name == "prod_cd")
                            {
                                ((Hashtable)objHashtables.HashItemtbl[key])["prod_cd"] = row.Cells["prod_cd"].Value.ToString();
                            }
                            else
                            {
                                ((Hashtable)objHashtables.HashItemtbl[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                    }
                }
                ((Hashtable)objHashtables.HashItemtbl[key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                ((Hashtable)objHashtables.HashItemtbl[key])["tran_id"] = ACTIVE_BL.Tran_id;
                ((Hashtable)objHashtables.HashItemtbl[key])["tran_cd"] = ACTIVE_BL.Code;
                ((Hashtable)objHashtables.HashItemtbl[key])["ptserial"] = _hashItemList["ptserial"].ToString();
                ((Hashtable)objHashtables.HashItemtbl[key])["woboid"] = ((Hashtable)objHashtables.HashMaintbl[_hashItemList["ptserial"]])["woboid"].ToString();
                ((Hashtable)objHashtables.HashItemtbl[key])["bomid"] = ((Hashtable)objHashtables.HashMaintbl[_hashItemList["ptserial"]])["bomid"].ToString();
                ((Hashtable)objHashtables.HashItemtbl[key])["bom_no"] = ((Hashtable)objHashtables.HashMaintbl[_hashItemList["ptserial"]])["bom_no"].ToString();
                i++;
            }
            objBLFD.HASHTABLES = objHashtables;
        }
        private void GetDetailsForLocal()
        {
            foreach (DataGridViewRow row in dgvBOM.Rows)
            {
                key = _hashItemList["ptserial"].ToString();
                flgSelect = false;
                if (row.Cells["sel"].Value != null && row.Cells["sel"].Value.ToString() != "")
                {
                    if (row.Cells["sel"].Value.ToString() == "1" || row.Cells["sel"].Value.ToString() == "True")
                    {
                        flgSelect = true;
                    }
                    else if (row.Cells["sel"].Value.ToString() == "0" || row.Cells["sel"].Value.ToString() == "False")
                    {
                        flgSelect = false;
                    }
                }
                if (flgSelect)
                {
                    if (_hashlocalwobomain == null || _hashlocalwobomain.Count == 0 || !_hashlocalwobomain.Contains(key))
                    {
                        _hashlocalwobomain[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataGridViewColumn column in dgvBOM.Columns)
                        {
                            if (column.Name.ToLower() != "req_qty" && column.Name.ToLower() != "sel")
                            {
                                ((Hashtable)_hashlocalwobomain[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                        ((Hashtable)_hashlocalwobomain[key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                        ((Hashtable)_hashlocalwobomain[key])["tran_id"] = ACTIVE_BL.Tran_id;
                        ((Hashtable)_hashlocalwobomain[key])["tran_cd"] = ACTIVE_BL.Code;
                        ((Hashtable)_hashlocalwobomain[key])["ptserial"] = _hashItemList["ptserial"].ToString();
                        ((Hashtable)_hashlocalwobomain[key])["prod_cd"] = _hashItemList["prod_cd"].ToString();
                    }
                }
            }
            int i = 0;
            foreach (DataGridViewRow row in dgvRM_For_BOM.Rows)
            {
                key = _hashItemList["ptserial"].ToString() + "," + row.Cells["prod_cd"].Value.ToString();
                if (_hashlocalwoboitem == null || _hashlocalwoboitem.Count == 0 || !_hashlocalwoboitem.Contains(key))
                {
                    _hashlocalwoboitem[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DataGridViewColumn column in dgvRM_For_BOM.Columns)
                    {
                        if (column.Name.ToLower() != "rm_req_qty")
                        {
                            if (column.Name == "woboitno" && (row.Cells[column.Name].Value == null || row.Cells[column.Name].Value.ToString() == "" || row.Cells[column.Name].Value.ToString() == "0"))
                            {
                                ((Hashtable)_hashlocalwoboitem[key])[column.Name] = i + 1;
                            }
                            else if (column.Name == "prod_cd")
                            {
                                ((Hashtable)_hashlocalwoboitem[key])["prod_cd"] = row.Cells["prod_cd"].Value.ToString();
                            }
                            else
                            {
                                ((Hashtable)_hashlocalwoboitem[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                    }
                }
                ((Hashtable)_hashlocalwoboitem[key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                ((Hashtable)_hashlocalwoboitem[key])["tran_id"] = ACTIVE_BL.Tran_id;
                ((Hashtable)_hashlocalwoboitem[key])["tran_cd"] = ACTIVE_BL.Code;
                ((Hashtable)_hashlocalwoboitem[key])["ptserial"] = _hashItemList["ptserial"].ToString();
                ((Hashtable)_hashlocalwoboitem[key])["woboid"] = ((Hashtable)_hashlocalwobomain[_hashItemList["ptserial"]])["woboid"].ToString();
                ((Hashtable)_hashlocalwoboitem[key])["bomid"] = ((Hashtable)_hashlocalwobomain[_hashItemList["ptserial"]])["bomid"].ToString();
                ((Hashtable)_hashlocalwoboitem[key])["bom_no"] = ((Hashtable)_hashlocalwobomain[_hashItemList["ptserial"]])["bom_no"].ToString();
                i++;
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            GetDetailsForLocal();
            foreach (DictionaryEntry entry in _hashlocalwobomain)
            {
                objBLFD.HASHTABLES.HashMaintbl[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                if (((Hashtable)entry.Value).Count != 0)
                {
                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                    {
                        ((Hashtable)objBLFD.HASHTABLES.HashMaintbl[entry.Key])[entry1.Key] = entry1.Value;
                    }
                }
            }
            foreach (DictionaryEntry entry in _hashlocalwoboitem)
            {
                objBLFD.HASHTABLES.HashItemtbl[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                if (((Hashtable)entry.Value).Count != 0)
                {
                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                    {
                        ((Hashtable)objBLFD.HASHTABLES.HashItemtbl[entry.Key])[entry1.Key] = entry1.Value;
                    }
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvRM_For_BOM.Rows.Add(1);
            dgvRM_For_BOM.Rows[dgvRM_For_BOM.Rows.Count - 1].Cells["prod_nm"].Selected = true;
            dgvRM_For_BOM.CurrentCell = dgvRM_For_BOM["prod_nm", dgvRM_For_BOM.Rows.Count - 1];
            dgvRM_For_BOM["woboitno", dgvRM_For_BOM.Rows.Count - 1].Value = dgvRM_For_BOM.Rows.Count;

            dgvRM_For_BOM.CurrentRow.Cells["prod_nm"].ReadOnly = false;
            dgvRM_For_BOM.CurrentRow.Cells["rm_qty"].ReadOnly = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (_hashlocalwoboitem.Contains(_hashItemList["ptserial"] + "," + dgvRM_For_BOM.CurrentRow.Cells["prod_cd"].Value.ToString()))
            {
                ((Hashtable)_hashlocalwoboitem[_hashItemList["ptserial"] + "," + dgvRM_For_BOM.CurrentRow.Cells["prod_cd"].Value.ToString()]).Clear();
            }
            dgvRM_For_BOM.Rows.Remove(dgvRM_For_BOM.CurrentRow);
        }

        private void dgvBOM_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        foreach (DataGridViewRow row in dgvRM_For_BOM.Rows)
                        {
                            row.Cells["rm_req_qty"].Value = ((decimal.Parse(row.Cells["rm_qty"].Value.ToString()) * decimal.Parse(gridview.CurrentRow.Cells["req_qty"].Value.ToString())) / decimal.Parse(gridview.CurrentRow.Cells["bom_qty"].Value.ToString())).ToString();
                        }
                        GetDetailsForLocal();
                    }
                    else
                    {
                        _hashlocalwobomain.Clear();
                        _hashlocalwoboitem.Clear();
                    }
                }
            }
        }

        private void dgvBOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                Get_Item_Details();
            }
        }

        private void Get_Item_Details()
        {
            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", _hashItemList["tran_id"].ToString() != "0" ? objBLFD.Tran_id : _hashItemList["tran_id"].ToString());
            htparam.Add("@atran_cd", _hashItemList["tran_id"].ToString() != "0" ? objBLFD.Code : "");
            htparam.Add("@aptserial", _hashItemList["ptserial"].ToString());
            htparam.Add("@aprod_nm", dgvBOM.CurrentRow != null && dgvBOM.CurrentRow.Cells["bom_item"].Value != null && dgvBOM.CurrentRow.Cells["bom_item"].Value.ToString() != "" ? dgvBOM.CurrentRow.Cells["bom_item"].Value.ToString() : "");
            htparam.Add("@abomid", dgvBOM.CurrentRow != null && dgvBOM.CurrentRow.Cells["bomid"].Value != null && dgvBOM.CurrentRow.Cells["bomid"].Value.ToString() != "" ? dgvBOM.CurrentRow.Cells["bomid"].Value.ToString() : "0");
            htparam.Add("@acompid", ACTIVE_BL.ObjCompany.Compid.ToString());

            DataSet dsetWOwithBOMItem = objdblayer.dsprocedure("ISP_GET_WO_BO_ITEM_DETAILS", htparam);
            if (dsetWOwithBOMItem != null && dsetWOwithBOMItem.Tables.Count != 0 && dsetWOwithBOMItem.Tables[0].Rows.Count != 0)
            {
                while (dgvRM_For_BOM.Rows.Count > 0)
                {
                    if (!dgvRM_For_BOM.Rows[0].IsNewRow)
                    {
                        dgvRM_For_BOM.Rows.RemoveAt(0);
                    }
                }
                int i = 0;
                foreach (DataRow row in dsetWOwithBOMItem.Tables[0].Rows)
                {
                    dgvRM_For_BOM.Rows.Add(1);
                    foreach (DataGridViewColumn column in dgvRM_For_BOM.Columns)
                    {
                        if (dsetWOwithBOMItem.Tables[0].Columns.Contains(column.Name))
                        {
                            if (column.Name == "woboitno" && (row[column.Name] == null || row[column.Name].ToString() == "" || row[column.Name].ToString() == "0"))
                            {
                                dgvRM_For_BOM.Rows[i].Cells[column.Name].Value = i + 1;
                            }
                            else
                            {
                                dgvRM_For_BOM.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                    }
                    dgvRM_For_BOM.Rows[i].Cells["rm_req_qty"].Value = ((decimal.Parse(dgvRM_For_BOM.Rows[i].Cells["rm_qty"].Value.ToString()) * decimal.Parse(dgvBOM.CurrentRow.Cells["req_qty"].Value.ToString())) / decimal.Parse(dgvBOM.CurrentRow.Cells["bom_qty"].Value.ToString())).ToString();
                    i++;
                }
            }
        }

        private void dgvRM_For_BOM_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                if (dgvRM_For_BOM.CurrentCell.OwningColumn.Name == "prod_nm" && (e.FormattedValue == null || e.FormattedValue.ToString() == ""))
                {
                    AutoClosingMessageBox.Show("Please enter product name", "Validation");
                    e.Cancel = true;
                }
                if (dgvRM_For_BOM.CurrentCell.OwningColumn.Name == "rm_qty")
                {
                    if (e.FormattedValue == null || e.FormattedValue.ToString() == "")
                    {
                        AutoClosingMessageBox.Show("Please enter quantity", "Validation");
                        e.Cancel = true;
                    }
                    else
                    {
                        dgvRM_For_BOM.CurrentRow.Cells["rm_req_qty"].Value = ((decimal.Parse(e.FormattedValue.ToString()) * decimal.Parse(dgvBOM.CurrentRow.Cells["req_qty"].Value.ToString())) / decimal.Parse(dgvBOM.CurrentRow.Cells["bom_qty"].Value.ToString())).ToString();
                    }
                }
            }
        }

        private void dgvRM_For_BOM_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                txt.KeyDown -= new KeyEventHandler(txt_key_down);
                txt.KeyDown += new KeyEventHandler(txt_key_down);
            }
        }
        private void txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                bool flgItemEdit = false;
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2 && txt.Name == "prod_nm")
                {
                    if (!flgItemEdit)
                    {
                        frmPopup objfrmPopup = new frmPopup("PT_MAST", "PT", "PROD_CD,PROD_NM", "PROD_NM;ITEM_NAME", "Please select", "prod_ty_nm IN('RAW MATERIAL','SEMI FINISHED')");
                        objfrmPopup.ResultFieldValue = txt.Text;
                        objfrmPopup.ShowDialog();
                        txt.Text = objfrmPopup.ResultFieldValue.Split(',')[1];
                        dgvRM_For_BOM.CurrentRow.Cells["prod_cd"].Value = objfrmPopup.ResultFieldValue.Split(',')[0];
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
