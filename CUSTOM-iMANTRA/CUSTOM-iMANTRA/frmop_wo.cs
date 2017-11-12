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
    public partial class frmop_wo : CustomBaseForm
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

        public frmop_wo()
        {
            InitializeComponent();
        }

        private void frmip_wo_Load(object sender, EventArgs e)
        {
            dgvopwo.AutoGenerateColumns = false;

            //Hashtable htparam = new Hashtable();
            //htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            //htparam.Add("@atran_cd", objBLFD.Code);
            //htparam.Add("@aptserial", "");
            //htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            //htparam.Add("@acompid", objBLFD.ObjCompany.Compid);

            //DataSet dsetWOwithBOM = objdblayer.dsprocedure("ISP_GET_OPWO_HEADER", htparam);
            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
            htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
            htparam.Add("@atran_dt", objBLFD.HTMAIN["tran_dt"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid);

            DataSet dsetWOwithBOM = objdblayer.dsprocedure("ISP_GET_OPWO_ITEM_SELECTED", htparam);
            if (dsetWOwithBOM != null && dsetWOwithBOM.Tables.Count != 0 && dsetWOwithBOM.Tables[0].Rows.Count != 0)
            {
                dgvopwo.DataSource = dsetWOwithBOM.Tables[0];
                dgvopwo.Update();
                int i = 0;
                foreach (DataRow row in dsetWOwithBOM.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvopwo.Columns)
                    {
                        if (dsetWOwithBOM.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvopwo.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
            }

            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashMaintbl.Clear();
                objBLFD.HASHTABLES = objHashtables;
                dgvopwo.Enabled = false;
                btnDone.Enabled = false;
            }
            #region
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
                foreach (DataGridViewRow row in dgvopwo.Rows)
                {
                    foreach (DictionaryEntry entry in _hashlocalipwomain)
                    {
                        key = row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_prod_cd"].Value.ToString();
                        if (key == entry.Key.ToString())
                        {
                            if (((Hashtable)entry.Value).Count != 0)
                            {
                                foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                                {
                                    if (dgvopwo.Columns.Contains(entry1.Key.ToString()))
                                    {
                                        row.Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
                                    }
                                }
                                row.Cells["sel"].Value = true;
                            }
                        }
                    }
                }
            }
            else
            {
                objHashtables = new BLHT();
            }
            #endregion
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Work Order Details";
        }

        private void dgvopwo_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "sel")
                {
                    gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    foreach (DataGridViewRow row in gridview.Rows)
                    {
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
                            key = row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_prod_cd"].Value.ToString();
                            if (!_hashlocalipwomain.Contains(key))
                            {
                                _hashlocalipwomain[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DataGridViewColumn column in gridview.Columns)
                            {
                                if (column.Name.ToString().ToLower() != "sel")
                                {
                                    ((Hashtable)_hashlocalipwomain[key])[column.Name] = row.Cells[column.Name].Value;
                                }
                            }
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
                                if (entry.Key.ToString().Contains(row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_prod_cd"].Value.ToString()))
                                    ((Hashtable)_hashlocalipwomain[entry.Key]).Clear();
                            }
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            chk.Value = false;
                        }
                    }
                }
            }
        }

        private void dgvwo_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;

            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (gridview.CurrentCell.OwningColumn.Name == "qty")
                {
                    if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse(gridview.CurrentRow.Cells["avail_qty"].Value.ToString()))
                    {
                        AutoClosingMessageBox.Show("Quantity should be lesser than or equal to Available Quantity", "Validation");
                        e.Cancel = true;
                    }
                    else if (decimal.Parse(e.FormattedValue.ToString()) > decimal.Parse(gridview.CurrentRow.Cells["ref_tran_qty"].Value.ToString()))
                    {
                        AutoClosingMessageBox.Show("Quantity should be lesser than or equal to Work Order Quantity", "Validation");
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
            if (ACTIVE_BL.Tran_mode != "view_mode")
            {
                foreach (DataGridViewRow row in dgvopwo.Rows)
                {
                    key = row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_prod_cd"].Value.ToString();
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
                        foreach (DataGridViewColumn column in dgvopwo.Columns)
                        {
                            if (column.Name.ToString().ToLower() != "sel")
                            {
                                ((Hashtable)_hashlocalipwomain[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                    }
                }
                foreach (DictionaryEntry entry in _hashlocalipwomain)
                {
                    if (!objHashtables.HashMaintbl.Contains(entry.Key))
                    {
                        objHashtables.HashMaintbl[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key])[entry1.Key] = entry1.Value;
                        }
                        ((Hashtable)objHashtables.HashMaintbl[entry.Key])["compid"] = objBLFD.ObjCompany.Compid;
                        ((Hashtable)objHashtables.HashMaintbl[entry.Key])["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                    }
                    else
                    {
                        ((Hashtable)objHashtables.HashMaintbl[entry.Key]).Clear();
                    }
                }
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
                    if (((Hashtable)entry.Value).Count != 0 && decimal.Parse(((Hashtable)entry.Value)["qty"].ToString()) > 0)
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
                        ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["prod_nm"] = ((Hashtable)entry.Value)["ref_prod_nm"].ToString();
                        ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["prod_cd"] = ((Hashtable)entry.Value)["ref_prod_cd"].ToString();
                        ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')])["qty"] = ((Hashtable)entry.Value)["qty"].ToString();
                    }
                    else
                    {
                        ((Hashtable)objBLFD.HTITEM[i.ToString().Trim().PadLeft(5, '0')]).Clear();
                    }
                }
            }
            this.Close();
        }
    }
}
