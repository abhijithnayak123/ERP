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
using iMANTRA_IL;

namespace iMANTRA
{
    public partial class frmAccountAlloc : BaseClass
    {
        BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        FL_GENERAL objFLGeneral = new FL_GENERAL();

        DataSet dsetAccountAlloc = new DataSet();

        private Hashtable _hashRowList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable objHashtables_local = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
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
                //objBLFD.HT_ALLOC.Clear();

                objHashtables_local.Clear();
                objBLFD.HT_ALLOC = objHashtables_local;
            }

            dgvAccountAlloc.AutoGenerateColumns = false;
            dgvAccountAlloc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            lblAc_nm.Text = "Account Name : " + objBLFD.HTMAIN["ac_nm"].ToString();
            lblLedgerAcc.Text = "Ledger Name : " + _hashRowList["acc_ac_nm"].ToString();
            lblTotLedgerAmt.Text = "Total Ledger Amt : " + decimal.Parse(_hashRowList["acc_amount"].ToString()).ToString("F");

            Hashtable htparam = new Hashtable();

            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aacserial", _hashRowList["acserial"].ToString());

            htparam.Add("@aac_nm", objBLFD.HTMAIN["ac_nm"].ToString());
            htparam.Add("@aposting_ac_nm", _hashRowList["acc_ac_nm"].ToString());
            htparam.Add("@adate", objBLFD.HTMAIN["TRAN_DT"].ToString());

            htparam.Add("@aacc_type", _hashRowList["acc_account_type"].ToString().Trim().ToUpper() == "CR" ? "DR" : "CR");
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

            if (objBLFD.Tran_mode != "view_mode")
            {
                dsetAccountAlloc = objFLGeneral.GetDataSetByProcedure("ISP_ALLOCATION", htparam);
            }
            else
            {
                dsetAccountAlloc = objFLGeneral.GetDataSetByProcedure("ISP_ALLOCATION_VIEWMODE", htparam);
            }
            if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
            {
                dgvAccountAlloc.Rows.Clear();

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
                    dgvAccountAlloc.Rows[i].Cells["net_amt"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[i].Cells["net_amt"].Value);
                    dgvAccountAlloc.Rows[i].Cells["i_allocating"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[i].Cells["allocating_amt"].Value);
                    dgvAccountAlloc.Rows[i].Cells["i_allocated"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[i].Cells["allocated_amt"].Value); //Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["allocated_amt"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["i_allocating"].Value.ToString());
                    dgvAccountAlloc.Rows[i].Cells["i_pend"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[i].Cells["pending_amt"].Value); //Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["i_allocating"].Value.ToString());
                    i++;
                }
            }

            //if (ACTIVE_BL.Tran_mode != "add_mode")
            //{
            //    EditfrmAllocation();
            //}
            objHashtables_local = objBLFD.HT_ALLOC;
            #region 3.0
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                bool flg = true;
                if (objBLFD.HT_ALLOC != null && objBLFD.HT_ALLOC.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HT_ALLOC)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _hashRowList["acserial"].ToString())
                        {
                            flg = false;
                            break;
                        }
                    }
                }
                if (flg)
                {
                    EditfrmAllocation();
                }
            }
            #endregion 3.0
            if (objHashtables_local != null && objHashtables_local.Count != 0)
            {
                //if (objBLFD.HT_ALLOC != null && objBLFD.HT_ALLOC.Count != 0)
                //{
                int j = 0;
                if (_hashlocalaccountalloc != null)
                {
                    _hashlocalaccountalloc.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HT_ALLOC)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _hashRowList["acserial"].ToString())
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
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvAccountAlloc.Columns.Contains(entry1.Key.ToString()))
                            {
                                dgvAccountAlloc.Rows[j].Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
                            }
                        }
                        dgvAccountAlloc.Rows[j].Cells["net_amt"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[j].Cells["net_amt"].Value);
                        dgvAccountAlloc.Rows[j].Cells["i_allocating"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[j].Cells["allocating_amt"].Value);
                        dgvAccountAlloc.Rows[j].Cells["i_allocated"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[j].Cells["allocated_amt"].Value); //Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["allocated_amt"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["i_allocating"].Value.ToString());
                        dgvAccountAlloc.Rows[j].Cells["i_pend"].Value = String.Format("{0:F2}", dgvAccountAlloc.Rows[j].Cells["pending_amt"].Value); //Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.Rows[i].Cells["i_allocating"].Value.ToString());
                        //dgvAccountAlloc.Rows[j].Cells["i_allocating"].Value = dgvAccountAlloc.Rows[j].Cells["allocating_amt"].Value;
                        //dgvAccountAlloc.Rows[j].Cells["i_allocated"].Value = dgvAccountAlloc.Rows[j].Cells["allocated_amt"].Value; //Convert.ToDecimal(dgvAccountAlloc.Rows[j].Cells["allocated_amt"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.Rows[j].Cells["i_allocating"].Value.ToString());
                        //dgvAccountAlloc.Rows[j].Cells["i_pend"].Value = dgvAccountAlloc.Rows[j].Cells["pending_amt"].Value;//Convert.ToDecimal(dgvAccountAlloc.Rows[j].Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.Rows[j].Cells["i_allocating"].Value.ToString());
                        j++;
                    }
                }
                //}
            }
            else
            {
                objHashtables_local = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            }
            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                dgvAccountAlloc.ReadOnly = true;
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "CustomMaster");
            ucToolBar1.Titlebar = "Account Allocation";
        }

        private void EditfrmAllocation()
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

            //DataSet dsetAccountAlloc = objdblayer.dsprocedure("ISP_ALLOCATION", htparam);
            if (objBLFD.Tran_mode != "view_mode")
            {
                dsetAccountAlloc = objFLGeneral.GetDataSetByProcedure("ISP_ALLOCATION", htparam);
            }
            else
            {
                dsetAccountAlloc = objFLGeneral.GetDataSetByProcedure("ISP_ALLOCATION_VIEWMODE", htparam);
            }
            if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
            {
                DataRow[] row1 = dsetAccountAlloc.Tables[0].Select("allocated_amt<>0");
                string key;
                foreach (DataRow r in row1)
                {
                    key = _hashRowList["acserial"].ToString() + "," + r["ref_tran_id"].ToString() + "," + r["ref_acserial"].ToString();

                    if (objHashtables_local != null && !objHashtables_local.Contains(key))
                    {
                        objHashtables_local[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetAccountAlloc.Tables[0].Columns)
                        {
                            if (dgvAccountAlloc.Columns.Contains(column.ColumnName))
                            {
                                ((Hashtable)objHashtables_local[key])[column.ColumnName] = r[column.ColumnName];
                            }
                        }
                    }
                }
            }

            decimal alloc_amt = 0;
            foreach (DictionaryEntry entry in objHashtables_local)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (((Hashtable)entry.Value)["allocating_amt"] != null && ((Hashtable)entry.Value)["allocating_amt"].ToString() != "")
                    {
                        alloc_amt += Convert.ToDecimal(((Hashtable)entry.Value)["allocating_amt"].ToString());
                    }
                }
            }

            lblAllocatedAmt.Text = "Allocated Amt : " + alloc_amt.ToString("F2");
            lblBalance.Text = "Balance : " + (Convert.ToDecimal(_hashRowList["acc_amount"].ToString()) - alloc_amt).ToString("F2");

            objBLFD.HT_ALLOC = objHashtables_local;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvAccountAlloc.Rows)
            {
                key = _hashRowList["acserial"].ToString() + "," + row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_acserial"].Value.ToString();
                flgSelect = false;
                if (dgvAccountAlloc.Columns.Contains("allocating_amt") && row.Cells["allocating_amt"].Value != null && row.Cells["allocating_amt"].Value.ToString() != "")
                {
                    if (row.Cells["allocating_amt"].Value.ToString() == "0" || row.Cells["allocating_amt"].Value.ToString() == "0.00" || decimal.Parse(row.Cells["allocating_amt"].Value.ToString()) == 0)
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
                        if (column.Name != "i_allocated" && column.Name != "i_pend" && column.Name != "i_allocating")
                        {
                            ((Hashtable)_hashlocalaccountalloc[key])[column.Name] = row.Cells[column.Name].Value;
                        }
                        if (column.Name == "i_allocated")
                        {
                            ((Hashtable)_hashlocalaccountalloc[key])["allocated_amt"] = row.Cells[column.Name].Value;
                        }
                        if (column.Name == "i_pend")
                        {
                            ((Hashtable)_hashlocalaccountalloc[key])["pending_amt"] = row.Cells[column.Name].Value;
                        }
                    }
                }
            }
            decimal alloc_amt = 0;
            foreach (DictionaryEntry entry in _hashlocalaccountalloc)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (((Hashtable)entry.Value)["allocating_amt"] != null && ((Hashtable)entry.Value)["allocating_amt"].ToString() != "")
                    {
                        alloc_amt += Convert.ToDecimal(((Hashtable)entry.Value)["allocating_amt"].ToString());
                    }
                }
            }
            if (alloc_amt > Convert.ToDecimal(_hashRowList["acc_amount"].ToString()))
            {
                AutoClosingMessageBox.Show("Total Allocated Amount should be less than equal to Ledger Amount", "Validation");
            }
            else
            {
                foreach (DictionaryEntry entry in _hashlocalaccountalloc)
                {
                    if (((Hashtable)entry.Value) != null && ((Hashtable)entry.Value).Count != 0 && ((Hashtable)entry.Value).Contains("allocating_amt") && ((Hashtable)entry.Value)["allocating_amt"] != null && decimal.Parse(((Hashtable)entry.Value)["allocating_amt"].ToString()) != 0)
                    {
                        if (objHashtables_local != null && !objHashtables_local.Contains(entry.Key))
                        {
                            objHashtables_local[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                        {
                            ((Hashtable)objHashtables_local[entry.Key])[entry1.Key] = entry1.Value;
                        }
                        ((Hashtable)objHashtables_local[entry.Key])["tran_id"] = objBLFD.Tran_id;
                        ((Hashtable)objHashtables_local[entry.Key])["tran_cd"] = objBLFD.Code;
                        ((Hashtable)objHashtables_local[entry.Key])["tran_no"] = objBLFD.HTMAIN["tran_no"].ToString();
                        ((Hashtable)objHashtables_local[entry.Key])["tran_dt"] = objBLFD.HTMAIN["tran_dt"].ToString();
                        ((Hashtable)objHashtables_local[entry.Key])["ACSERIAL"] = _hashRowList["ACSERIAL"].ToString();
                        ((Hashtable)objHashtables_local[entry.Key])["acc_ac_nm"] = _hashRowList["acc_ac_nm"].ToString();
                        ((Hashtable)objHashtables_local[entry.Key])["acc_amount"] = _hashRowList["acc_amount"].ToString();
                        ((Hashtable)objHashtables_local[entry.Key])["tran_type"] = _hashRowList["acc_account_type"].ToString();
                    }
                }
                objBLFD.HT_ALLOC = objHashtables_local;
                this.Close();
            }
        }

        private void dgvAccountAlloc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "allocating_amt")
                {
                    flgSelect = false;
                    if (gridview.CurrentRow.Cells["allocating_amt"].Value != null && gridview.CurrentRow.Cells["allocating_amt"].Value.ToString() != "")
                    {
                        if (gridview.CurrentRow.Cells["allocating_amt"].Value.ToString() == "0" || gridview.CurrentRow.Cells["allocating_amt"].Value.ToString() == "0.00")
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
                        //gridview.CurrentRow.Cells["allocating_amt"].Value = Convert.ToDecimal(gridview.CurrentRow.Cells["allocating_amt"].Value.ToString());

                        key = _hashRowList["acserial"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_acserial"].Value.ToString();
                        if (_hashlocalaccountalloc != null && !_hashlocalaccountalloc.Contains(key))
                        {
                            _hashlocalaccountalloc[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DataGridViewColumn column in dgvAccountAlloc.Columns)
                            {
                                if (column.Name != "i_allocated" && column.Name != "i_pend" && column.Name != "i_allocating")
                                {
                                    ((Hashtable)_hashlocalaccountalloc[key])[column.Name] = gridview.CurrentRow.Cells[column.Name].Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        key = _hashRowList["acserial"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_acserial"].Value.ToString();
                        if (_hashlocalaccountalloc.Contains(key))
                        {
                            _hashlocalaccountalloc.Remove(key);
                            objBLFD.HT_ALLOC = objHashtables_local;
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
                    key = _hashRowList["acserial"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_acserial"].Value.ToString();
                    if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "allocating_amt")
                    {
                        flgSelect = false;
                        if (e.FormattedValue != null && e.FormattedValue.ToString() != "")
                        {
                            if (e.FormattedValue.ToString() == "0" && e.FormattedValue.ToString() == "0.00")
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
                            //gridview.CurrentRow.Cells["i_allocating"].Value = e.FormattedValue; //Convert.ToDecimal(gridview.CurrentRow.Cells["allocating_amt"].Value.ToString());

                            key = _hashRowList["acserial"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_acserial"].Value.ToString();
                            if (_hashlocalaccountalloc != null)
                            {
                                if (!_hashlocalaccountalloc.Contains(key))
                                {
                                    _hashlocalaccountalloc[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                }
                                foreach (DataGridViewColumn column in dgvAccountAlloc.Columns)
                                {
                                    if (column.Name != "i_allocated" && column.Name != "i_pend" && column.Name != "i_allocating")
                                    {
                                        ((Hashtable)_hashlocalaccountalloc[key])[column.Name] = gridview.CurrentRow.Cells[column.Name].Value;
                                    }
                                }
                            }
                        }
                        else
                        {
                            key = _hashRowList["acserial"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_acserial"].Value.ToString();
                            if (_hashlocalaccountalloc.Contains(key))
                            {
                                _hashlocalaccountalloc.Remove(key);
                                objBLFD.HT_ALLOC = objHashtables_local;
                            }
                        }

                        flgSelect = false;
                        if (e.FormattedValue != null && e.FormattedValue.ToString() != "" && _hashlocalaccountalloc != null && _hashlocalaccountalloc.Count != 0)
                        {
                            flgSelect = true;
                        }
                        if (flgSelect)
                        {
                            decimal bal_qty = 0, allocated_amt = 0;
                            bal_qty = Convert.ToDecimal(e.FormattedValue.ToString());

                            allocated_amt = Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocated_amt"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString());// +Convert.ToDecimal(e.FormattedValue.ToString());

                            //if (bal_qty <= Convert.ToDecimal(gridview.CurrentRow.Cells["pending_amt"].Value.ToString()))
                            if (bal_qty <= Convert.ToDecimal(gridview.CurrentRow.Cells["net_amt"].Value.ToString()) - allocated_amt)
                            {
                                ((Hashtable)_hashlocalaccountalloc[key])[gridview.CurrentCell.OwningColumn.Name] = Convert.ToDecimal(e.FormattedValue.ToString());

                                dgvAccountAlloc.CurrentRow.Cells["i_allocated"].Value = String.Format("{0:F2}", Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocated_amt"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) + Convert.ToDecimal(e.FormattedValue.ToString()));
                                dgvAccountAlloc.CurrentRow.Cells["i_pend"].Value = String.Format("{0:F2}", Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) - Convert.ToDecimal(e.FormattedValue.ToString()));
                                //((Hashtable)_hashlocalaccountalloc[key])["allocated_amt"] = Convert.ToDecimal(e.FormattedValue.ToString());
                                e.Cancel = false;
                            }
                            else
                            {
                                AutoClosingMessageBox.Show("Allocating Amount should be less than or equal to Balance Amount", "Validation");
                                ((Hashtable)_hashlocalaccountalloc[key])[gridview.CurrentCell.OwningColumn.Name] = Convert.ToDecimal(e.FormattedValue.ToString());
                                e.Cancel = true;
                            }
                            if (!e.Cancel)
                            {
                                decimal alloc_amt = 0;
                                foreach (DictionaryEntry entry in _hashlocalaccountalloc)
                                {
                                    if (((Hashtable)entry.Value).Count != 0)
                                    {
                                        if (((Hashtable)entry.Value)["allocating_amt"] != null && ((Hashtable)entry.Value)["allocating_amt"].ToString() != "")
                                        {
                                            alloc_amt += Convert.ToDecimal(((Hashtable)entry.Value)["allocating_amt"].ToString());
                                        }
                                    }
                                }
                                lblAllocatedAmt.Text = "Allocated Amt : " + alloc_amt.ToString("F2");
                                lblBalance.Text = "Balance : " + (Convert.ToDecimal(_hashRowList["acc_amount"].ToString()) - alloc_amt).ToString("F2");
                                // lblBalance.Text = Convert.ToDecimal(_hashRowList["acc_amount"].ToString()) - allocated_amt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvAccountAlloc_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if (allocating_amt > Convert.ToDecimal(_hashRowList["acc_amount"].ToString()))
            //{
            //    AutoClosingMessageBox.Show("Total Allocating Amount not equal to Legder Amount", "Validation");
            //}
        }

        private void dgvAccountAlloc_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)// && txt.Name == "allocating_amt"
            {
                txt.KeyDown -= new KeyEventHandler(Account_txt_key_down);
                txt.KeyDown += new KeyEventHandler(Account_txt_key_down);
            }
        }
        private void Account_txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt != null)//&& txt.Name == "allocating_amt")
                {
                    if (e.KeyData == Keys.F2)
                    {
                        decimal bal_qty = 0, acc_qty = 0, allocated_amt = 0;

                        decimal alloc_amt = 0;
                        foreach (DataGridViewRow row in dgvAccountAlloc.Rows)
                        {
                            if (row.Cells["allocating_amt"].Value != null && row.Cells["allocating_amt"].Value.ToString() != "")
                            {
                                alloc_amt += Convert.ToDecimal(row.Cells["allocating_amt"].Value.ToString());
                            }
                        }

                        //foreach (DictionaryEntry entry in _hashlocalaccountalloc)
                        //{
                        //    if (((Hashtable)entry.Value).Count != 0)
                        //    {
                        //        if (((Hashtable)entry.Value)["allocated_amt"] != null && ((Hashtable)entry.Value)["allocated_amt"].ToString() != "")
                        //        {
                        //            allocated_amt += Convert.ToDecimal(((Hashtable)entry.Value)["allocated_amt"].ToString());
                        //        }
                        //    }
                        //}


                        acc_qty = Convert.ToDecimal(_hashRowList["acc_amount"].ToString());
                        bal_qty = Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_pend"].Value.ToString());

                        //if (bal_qty <= Convert.ToDecimal(gridview.CurrentRow.Cells["pending_amt"].Value.ToString()))
                        if (acc_qty - alloc_amt > 0)
                        {
                            if (acc_qty - alloc_amt > bal_qty)
                            {
                                dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value = String.Format("{0:F2}", bal_qty);
                                //dgvAccountAlloc.CurrentRow.Cells["i_allocated"].Value = String.Format("{0:F2}", allocated_amt - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value.ToString()));
                                //dgvAccountAlloc.CurrentRow.Cells["i_pend"].Value = String.Format("{0:F2}", Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value.ToString()));
                                txt.Text = String.Format("{0:F2}", bal_qty);
                            }
                            else
                            {
                                dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value = String.Format("{0:F2}", acc_qty - alloc_amt);
                                //dgvAccountAlloc.CurrentRow.Cells["i_allocated"].Value = String.Format("{0:F2}", allocated_amt - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value.ToString()));
                                //dgvAccountAlloc.CurrentRow.Cells["i_pend"].Value = String.Format("{0:F2}", Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["pending_amt"].Value.ToString()) + Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["i_allocating"].Value.ToString()) - Convert.ToDecimal(dgvAccountAlloc.CurrentRow.Cells["allocating_amt"].Value.ToString()));
                                txt.Text = String.Format("{0:F2}", acc_qty - alloc_amt);
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
