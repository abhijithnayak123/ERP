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
    public partial class frmAccountAlloc : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private BLHT objHashtables = new BLHT();
        private Hashtable _hashRowList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalaccountalloc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string key = "";
        bool flgSelect = false;

        public Hashtable HashRowList
        {
            get { return _hashRowList; }
            set { _hashRowList = value; }
        }

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmAccountAlloc()
        {
            InitializeComponent();
        }

        private void frmAccountAlloc_Load(object sender, EventArgs e)
        {
            this.Bounds = new Rectangle(objBLFD.X_gridAccount, objBLFD.Y_gridAccount, objBLFD.Width_gridAccount, objBLFD.Hgt_gridAccount);

            if (this.Width > this.ClientSize.Width)
            {
                this.Width = this.ClientSize.Width;
            }
            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashAccountAlloc.Clear();
                objBLFD.HASHTABLES = objHashtables;
            }
            dgvAccountAlloc.AutoGenerateColumns = false;
            dgvAccountAlloc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblProd_nm.Text = "Account Name : " + objBLFD.HTMAIN["ac_nm"].ToString();
            lblQty.Text = "Amount : " + _hashRowList["acc_amount"].ToString();

            Hashtable htparam = new Hashtable();

            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aacserial", _hashRowList["acserial"].ToString());

            htparam.Add("@aac_nm", objBLFD.HTMAIN["ac_nm"].ToString());
            htparam.Add("@aposting_ac_nm", _hashRowList["acc_ac_nm"].ToString());
            htparam.Add("@adate", objBLFD.HTMAIN["TRAN_DT"].ToString());

            htparam.Add("@aacc_type", _hashRowList["acc_account_type"].ToString().Trim().ToUpper() == "CR" ? "DR" : "CR");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

            DataSet dsetAccountAlloc = objdblayer.dsprocedure("ISP_ALLOCATION", htparam);
            if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
            {
                dgvAccountAlloc.DataSource = dsetAccountAlloc.Tables[0];
                dgvAccountAlloc.Update();
                int i = 0;
                foreach (DataRow row in dsetAccountAlloc.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvAccountAlloc.Columns)
                    {
                        if (dsetAccountAlloc.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvAccountAlloc.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
            }
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                EditfrmAdjusttoReceipt();
            }
            objHashtables = objBLFD.HASHTABLES;
            #region 3.0
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                bool flg = true;
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashAccountAlloc != null && objBLFD.HASHTABLES.HashAccountAlloc.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashAccountAlloc)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _hashRowList["ACSERIAL"].ToString())
                        {
                            flg = false;
                            break;
                        }
                    }
                }
                if (flg)
                {
                    EditfrmAdjusttoReceipt();
                }
            }
            #endregion 3.0
            if (objHashtables != null && objHashtables.HashAccountAlloc != null && objHashtables.HashAccountAlloc.Count != 0)
            {
                int j = 0;
                //while (dgvAccountAlloc.Rows.Count > 0)
                //{
                //    if (!dgvAccountAlloc.Rows[0].IsNewRow)
                //    {
                //        dgvAccountAlloc.Rows.RemoveAt(0);
                //    }
                //}

                if (_hashlocalaccountalloc != null)
                {
                    _hashlocalaccountalloc.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashAccountAlloc)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _hashRowList["ACSERIAL"].ToString())
                        {
                            _hashlocalaccountalloc[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)_hashlocalaccountalloc[entry.Key])[entry1.Key] = entry1.Value;
                            }
                        }
                    }
                }
                j = 0;
                foreach (DictionaryEntry entry in _hashlocalaccountalloc)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        //dgvAccountAlloc.Rows.Add();
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvAccountAlloc.Columns.Contains(entry1.Key.ToString()))
                            {
                                dgvAccountAlloc.Rows[j].Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
                            }
                        }
                        j++;
                    }
                }
            }
            else
            {
                objHashtables = new BLHT();
            }
            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                dgvAccountAlloc.ReadOnly = true;
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Account Allocation";
        }

        private void EditfrmAdjusttoReceipt()
        {
            Hashtable htparam = new Hashtable();

            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aacserial", _hashRowList["acserial"].ToString());

            htparam.Add("@aac_nm", objBLFD.HTMAIN["ac_nm"].ToString());
            htparam.Add("@aposting_ac_nm", _hashRowList["acc_ac_nm"].ToString());
            htparam.Add("@adate", objBLFD.HTMAIN["TRAN_DT"].ToString());

            //htparam.Add("@aacc_type", objBLFD.IsDefAccTranType ? "DR" : "CR");
            htparam.Add("@aacc_type", _hashRowList["acc_account_type"].ToString().Trim().ToUpper() == "CR" ? "DR" : "CR");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

            DataSet dsetAccountAlloc = objdblayer.dsprocedure("ISP_ALLOCATION", htparam);
            if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
            {
                DataRow[] row1 = dsetAccountAlloc.Tables[0].Select("allocated_amt<>0");
                string key;
                foreach (DataRow r in row1)
                {
                    key = _hashRowList["ACSERIAL"].ToString() + "," + r["ref_tran_id"].ToString() + "," + r["ref_ACSERIAL"].ToString();
                    if (objHashtables.HashAccountAlloc != null && !objHashtables.HashAccountAlloc.Contains(key))
                    {
                        objHashtables.HashAccountAlloc[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetAccountAlloc.Tables[0].Columns)
                        {
                            //if (dsetAccountAlloc.Tables[0].Columns.Contains(column.ColumnName))
                            if (dgvAccountAlloc.Columns.Contains(column.ColumnName))
                            {
                                ((Hashtable)objHashtables.HashAccountAlloc[key]).Add(column.ColumnName, r[column.ColumnName]);
                            }
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvAccountAlloc.Rows)
            {
                key = _hashRowList["ACSERIAL"].ToString() + "," + row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ACSERIAL"].Value.ToString();
                flgSelect = false;
                if (dgvAccountAlloc.Columns.Contains("alllocating_amt") && row.Cells["alllocating_amt"].Value != null && row.Cells["alllocating_amt"].Value.ToString() != "")
                {
                    if (row.Cells["alllocating_amt"].Value.ToString() == "0" || row.Cells["alllocating_amt"].Value.ToString() == "0.00")
                    {
                        flgSelect = false;
                    }
                    else
                    {
                        flgSelect = true;
                    }
                }
                if (flgSelect)
                {
                    foreach (DataGridViewColumn column in dgvAccountAlloc.Columns)
                    {
                        ((Hashtable)_hashlocalaccountalloc[key])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
            }
            foreach (DictionaryEntry entry in _hashlocalaccountalloc)
            {
                if (((Hashtable)entry.Value) != null && ((Hashtable)entry.Value).Count != 0)
                {
                    if (objHashtables.HashAccountAlloc != null && !objHashtables.HashAccountAlloc.Contains(entry.Key))
                    {
                        objHashtables.HashAccountAlloc[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])[entry1.Key] = entry1.Value;
                    }
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["tran_id"] = objBLFD.Tran_id;
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["tran_cd"] = objBLFD.Code;
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["tran_no"] = objBLFD.HTMAIN["tran_no"].ToString();
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["tran_dt"] = objBLFD.HTMAIN["tran_dt"].ToString();
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["ACSERIAL"] = _hashRowList["ACSERIAL"].ToString();
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["acc_ac_nm"] = _hashRowList["acc_ac_nm"].ToString();
                    ((Hashtable)objHashtables.HashAccountAlloc[entry.Key])["acc_amount"] = _hashRowList["acc_amount"].ToString();
                }
            }
            objBLFD.HASHTABLES = objHashtables;
            this.Close();
        }

        private void dgvAccountAlloc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "alllocating_amt")
                {
                    flgSelect = false;
                    if (gridview.CurrentRow.Cells["alllocating_amt"].Value != null && gridview.CurrentRow.Cells["alllocating_amt"].Value.ToString() != "")
                    {
                        if (gridview.CurrentRow.Cells["alllocating_amt"].Value.ToString() == "0" || gridview.CurrentRow.Cells["alllocating_amt"].Value.ToString() == "0.00")
                        {
                            flgSelect = false;
                        }
                        else
                        {
                            flgSelect = true;
                        }
                    }
                    if (flgSelect)
                    {
                        gridview.CurrentRow.Cells["alllocating_amt"].Value = decimal.Parse(gridview.CurrentRow.Cells["alllocating_amt"].Value.ToString());

                        //gridview.CurrentRow.Cells["tran_id"].Value = objBLFD.Tran_id;
                        //gridview.CurrentRow.Cells["tran_cd"].Value = objBLFD.Code;
                        //gridview.CurrentRow.Cells["tran_no"].Value = objBLFD.HTMAIN["tran_no"].ToString();
                        //gridview.CurrentRow.Cells["tran_dt"].Value = objBLFD.HTMAIN["tran_dt"].ToString();
                        //gridview.CurrentRow.Cells["ACSERIAL"].Value = _hashRowList["ACSERIAL"].ToString();
                        //gridview.CurrentRow.Cells["acc_ac_nm"].Value = _hashRowList["acc_ac_nm"].ToString();
                        //gridview.CurrentRow.Cells["acc_amount"].Value = _hashRowList["acc_amount"].ToString();

                        key = _hashRowList["ACSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ACSERIAL"].Value.ToString();
                        if (_hashlocalaccountalloc != null && !_hashlocalaccountalloc.Contains(key))
                        {
                            _hashlocalaccountalloc[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DataGridViewColumn column in dgvAccountAlloc.Columns)
                            {
                                ((Hashtable)_hashlocalaccountalloc[key])[column.Name] = gridview.CurrentRow.Cells[column.Name].Value;
                            }
                        }
                    }
                    else
                    {
                        key = _hashRowList["ACSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ACSERIAL"].Value.ToString();
                        if (_hashlocalaccountalloc.Contains(key))
                        {
                            _hashlocalaccountalloc.Remove(key);
                            objBLFD.HASHTABLES = objHashtables;
                        }
                    }
                }
            }
        }

        private void dgvAccountAlloc_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridView gridview = (DataGridView)sender;
                if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    key = _hashRowList["ACSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ACSERIAL"].Value.ToString();
                    if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "alllocating_amt")
                    {
                        flgSelect = false;
                        if (e.FormattedValue != null && e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString() == "0" || e.FormattedValue.ToString() == "0.00")
                            {
                                flgSelect = false;
                            }
                            else
                            {
                                flgSelect = true;
                            }
                        }
                        if (flgSelect)
                        {
                            decimal bal_qty = 0;
                            bal_qty = decimal.Parse(e.FormattedValue.ToString());
                            if (bal_qty <= decimal.Parse(gridview.CurrentRow.Cells["pending_amt"].Value.ToString()))
                            {
                                ((Hashtable)_hashlocalaccountalloc[key])[gridview.CurrentCell.OwningColumn.Name] = decimal.Parse(e.FormattedValue.ToString());
                                e.Cancel = false;
                            }
                            else
                            {
                                AutoClosingMessageBox.Show("Allocating Amount should be less than or equal to Balance Amount", "Validation");
                                ((Hashtable)_hashlocalaccountalloc[key])[gridview.CurrentCell.OwningColumn.Name] = decimal.Parse(e.FormattedValue.ToString());
                                e.Cancel = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
