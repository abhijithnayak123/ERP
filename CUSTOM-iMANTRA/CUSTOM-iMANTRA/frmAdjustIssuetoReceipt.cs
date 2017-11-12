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
    public partial class frmAdjustIssuetoReceipt : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private Hashtable _hashRowList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalissuerecord = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
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

        public frmAdjustIssuetoReceipt()
        {
            InitializeComponent();
        }

        private void frmAdjustIssuetoReceipt_Load(object sender, EventArgs e)
        {
            if (this.Width > this.ClientSize.Width)
            {
                this.Width = this.ClientSize.Width;
            }
            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashIssueAndReceipt.Clear();
                objBLFD.HASHTABLES = objHashtables;
            }
            dgvAdjustLI.AutoGenerateColumns = false;
            dgvAdjustLI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblProd_nm.Text = "Product Name : " + _hashRowList["prod_nm"].ToString();
            lblQty.Text = "Quantity : " + _hashRowList["qty"].ToString();

            Hashtable htparam = new Hashtable();

            htparam.Add("@atran_id", _hashRowList["tran_id"].ToString() != "0" ? objBLFD.Tran_id : _hashRowList["tran_id"].ToString());
            htparam.Add("@atran_cd", _hashRowList["tran_id"].ToString() != "0" ? objBLFD.Code : "");
            htparam.Add("@aptserial", _hashRowList["tran_id"].ToString() != "0" ? _hashRowList["ptserial"].ToString() : "");
            htparam.Add("@aac_nm", objBLFD.HTMAIN["ac_nm"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());
            htparam.Add("@atran_dt", objBLFD.HTMAIN["TRAN_DT"].ToString());

            DataSet dsetIssueAndReceipt = objdblayer.dsprocedure("ISP_GET_LABOURISSUE_FROM_LABOURRECEIPT", htparam);
            if (dsetIssueAndReceipt != null && dsetIssueAndReceipt.Tables.Count != 0 && dsetIssueAndReceipt.Tables[0].Rows.Count != 0)
            {
                dgvAdjustLI.DataSource = dsetIssueAndReceipt.Tables[0];
                dgvAdjustLI.Update();
                int i = 0;
                foreach (DataRow row in dsetIssueAndReceipt.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvAdjustLI.Columns)
                    {
                        dgvAdjustLI.Rows[i].Cells[column.Name].Value = row[column.Name];
                    }
                    i++;
                }
            }
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                EditfrmAdjusttoReceipt();
            }
            objHashtables = objBLFD.HASHTABLES;
            if (objHashtables != null && objHashtables.HashIssueAndReceipt != null && objHashtables.HashIssueAndReceipt.Count != 0)
            {
                decimal sumadjamt = 0, sumwastage = 0, cur_adj = 0, cur_wastage = 0, cons_qty = 0, avail_qty = 0;
                foreach (DataGridViewRow row in dgvAdjustLI.Rows)
                {
                    sumadjamt = 0; sumwastage = 0; cur_adj = 0; cur_wastage = 0; cons_qty = 0; avail_qty = 0;
                    foreach (DictionaryEntry entry in objHashtables.HashIssueAndReceipt)
                    {
                        string[] strKeys = entry.Key.ToString().Split(',');
                        if (row.Cells["ref_tran_id"].Value.ToString() + "," + row.Cells["ref_ptserial"].Value.ToString() == strKeys[1] + "," + strKeys[2])
                        {
                            if (_hashRowList["PTSERIAL"].ToString() == strKeys[0])
                            {
                                cur_adj = decimal.Parse(((Hashtable)entry.Value)["adj_qty"].ToString());
                                cur_wastage = decimal.Parse(((Hashtable)entry.Value)["wastage"].ToString());
                                row.Cells["sel"].Value = bool.Parse(((Hashtable)entry.Value)["sel"].ToString());
                            }
                            //else
                            //{
                            sumadjamt += decimal.Parse(((Hashtable)entry.Value)["adj_qty"].ToString());
                            sumwastage += decimal.Parse(((Hashtable)entry.Value)["wastage"].ToString());
                            //}
                        }
                    }
                    if (dsetIssueAndReceipt != null && dsetIssueAndReceipt.Tables.Count != 0 && dsetIssueAndReceipt.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] row1 = dsetIssueAndReceipt.Tables[0].Select("ref_tran_id='" + row.Cells["ref_tran_id"].Value.ToString() + "' and ref_ptserial='" + row.Cells["ref_ptserial"].Value.ToString() + "'");
                        foreach (DataRow r in row1)
                        {
                            cons_qty = decimal.Parse(r["cons_qty"].ToString());
                            avail_qty = decimal.Parse(r["avail_qty"].ToString());
                        }
                    }
                    row.Cells["adj_qty"].Value = cur_adj;
                    row.Cells["wastage"].Value = cur_wastage;
                    row.Cells["cons_qty"].Value = cons_qty + (sumadjamt) + (sumwastage) - cur_adj - cur_wastage;
                    row.Cells["avail_qty"].Value = decimal.Parse(row.Cells["qty"].Value.ToString()) - decimal.Parse(row.Cells["cons_qty"].Value.ToString());//avail_qty - sumadjamt - sumwastage + cur_adj + cur_wastage;
                }
                if (_hashlocalissuerecord != null)
                {
                    _hashlocalissuerecord.Clear();
                }
                foreach (DictionaryEntry entry in objHashtables.HashIssueAndReceipt)
                {
                    if (_hashlocalissuerecord != null && !_hashlocalissuerecord.Contains(entry.Key))
                    {
                        _hashlocalissuerecord[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                    {
                        ((Hashtable)_hashlocalissuerecord[entry.Key]).Add(entry1.Key, entry1.Value);
                    }
                    //((Hashtable)_hashlocalissuerecord[entry.Key])["cons_qty"] = decimal.Parse(((Hashtable)_hashlocalissuerecord[entry.Key])["adj_qty"].ToString()) + decimal.Parse(((Hashtable)_hashlocalissuerecord[entry.Key])["wastage"].ToString());
                }
            }
            else
            {
                objHashtables = new BLHT();
            }
            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                dgvAdjustLI.ReadOnly = true;
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Labour Job Material Allocation";
        }

        private void EditfrmAdjusttoReceipt()
        {
            Hashtable htparam = new Hashtable();
            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            htparam.Add("@aptserial", "");
            htparam.Add("@aac_nm", objBLFD.HTMAIN["ac_nm"].ToString());
            htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());
            htparam.Add("@atran_dt", objBLFD.HTMAIN["TRAN_DT"].ToString());

            DataSet dsetIssueAndReceipt = objdblayer.dsprocedure("ISP_GET_LABOURISSUE_FROM_LABOURRECEIPT", htparam);
            if (dsetIssueAndReceipt != null && dsetIssueAndReceipt.Tables.Count != 0 && dsetIssueAndReceipt.Tables[0].Rows.Count != 0)
            {
                DataRow[] row1 = dsetIssueAndReceipt.Tables[0].Select("sel=1");
                string key;
                foreach (DataRow r in row1)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["ref_tran_id"].ToString() + "," + r["ref_ptserial"].ToString();
                    if (objHashtables.HashIssueAndReceipt != null && !objHashtables.HashIssueAndReceipt.Contains(key))
                    {
                        objHashtables.HashIssueAndReceipt[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetIssueAndReceipt.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashIssueAndReceipt[key]).Add(column.ColumnName, r[column.ColumnName]);
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void dgvAdjustLI_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[0];
                        chk.Value = true;
                        gridview.CurrentRow.Cells["sel"].Value = "true";
                        gridview.CurrentRow.Cells["adj_qty"].Value = decimal.Parse(gridview.CurrentRow.Cells["avail_qty"].Value.ToString());

                        gridview.CurrentRow.Cells["tran_cd"].Value = objBLFD.Code;
                        gridview.CurrentRow.Cells["tran_no"].Value = objBLFD.HTMAIN["tran_no"].ToString();
                        gridview.CurrentRow.Cells["ptserial"].Value = _hashRowList["ptserial"].ToString();
                        gridview.CurrentRow.Cells["prod_nm"].Value = _hashRowList["prod_nm"].ToString();
                        gridview.CurrentRow.Cells["tran_dt"].Value = _hashRowList["tran_dt"].ToString();
                        gridview.CurrentRow.Cells["tran_id"].Value = _hashRowList["tran_id"].ToString();

                        gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        key = _hashRowList["PTSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString();
                        if (_hashlocalissuerecord != null && !_hashlocalissuerecord.Contains(key))
                        {
                            _hashlocalissuerecord[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DataGridViewColumn column in dgvAdjustLI.Columns)
                            {
                                ((Hashtable)_hashlocalissuerecord[key]).Add(column.Name, gridview.CurrentRow.Cells[column.Name].Value);
                            }
                        }
                    }
                    else
                    {
                        gridview.CurrentRow.Cells["adj_qty"].Value = 0;
                        gridview.CurrentRow.Cells["wastage"].Value = 0;
                        key = _hashRowList["PTSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString();
                        _hashlocalissuerecord.Remove(key);
                        objBLFD.HASHTABLES = objHashtables;
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells[0];
                        chk.Value = false;
                    }
                }
            }
        }

        private void dgvAdjustLI_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridView gridview = (DataGridView)sender;
                if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    key = _hashRowList["PTSERIAL"].ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString();
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
                    if (flgSelect && (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "adj_qty" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "wastage"))
                    {
                        decimal bal_qty = 0;
                        if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "adj_qty")
                        {
                            bal_qty = decimal.Parse(e.FormattedValue.ToString()) + decimal.Parse(gridview.CurrentRow.Cells["wastage"].Value.ToString());
                        }
                        else
                        {
                            bal_qty = decimal.Parse(gridview.CurrentRow.Cells["adj_qty"].Value.ToString()) + decimal.Parse(e.FormattedValue.ToString());
                        }

                        if (bal_qty <= decimal.Parse(gridview.CurrentRow.Cells["avail_qty"].Value.ToString()))
                        {
                            ((Hashtable)_hashlocalissuerecord[key])[gridview.CurrentCell.OwningColumn.Name] = decimal.Parse(e.FormattedValue.ToString());
                            e.Cancel = false;
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("adjusted quantity + wastage should be less than or equal to available quantity", "Validation");
                            ((Hashtable)_hashlocalissuerecord[key])[gridview.CurrentCell.OwningColumn.Name] = decimal.Parse(e.FormattedValue.ToString());
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            foreach (DictionaryEntry entry in _hashlocalissuerecord)
            {
                if (objHashtables.HashIssueAndReceipt != null && !objHashtables.HashIssueAndReceipt.Contains(entry.Key))
                {
                    objHashtables.HashIssueAndReceipt[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                }
                foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                {
                    ((Hashtable)objHashtables.HashIssueAndReceipt[entry.Key])[entry1.Key] = entry1.Value;
                }
                ((Hashtable)objHashtables.HashIssueAndReceipt[entry.Key])["cons_qty"] = decimal.Parse(((Hashtable)objHashtables.HashIssueAndReceipt[entry.Key])["adj_qty"].ToString()) + decimal.Parse(((Hashtable)objHashtables.HashIssueAndReceipt[entry.Key])["wastage"].ToString());
            }
            objBLFD.HASHTABLES = objHashtables;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
