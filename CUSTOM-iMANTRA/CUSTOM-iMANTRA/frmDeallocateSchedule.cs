using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using System.Collections;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmDeallocateSchedule : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        string delivery_qty = "0.00";
        decimal _assigning_qty_value = 0, _sum_de_schedule_qty = 0;
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private BLHT objHashtables = new BLHT();

        private Hashtable _hashItemList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalDispatchSchedule = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalDispatchScheduleTemp = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

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

        DataSet dsetDSSchedule = new DataSet();

        public frmDeallocateSchedule()
        {
            InitializeComponent();
        }

        private void frmDeallocateSchedule_Load(object sender, EventArgs e)
        {
            lblName.Text = "Product : " + _hashItemList["prod_nm"].ToString();
            lblQty.Text = "Qty : " + _hashItemList["qty"].ToString();
            // _assigning_qty_value = _hashItemList["qty"] != null && _hashItemList["qty"].ToString() != "" ? decimal.Parse(_hashItemList["qty"].ToString()) : decimal.Parse("0.00");
            dgvDeallocateSchedule.AutoGenerateColumns = false;
            if (objBLFD.HTMAINREF != null && objBLFD.HTMAINREF.Count != 0 && ((Hashtable)objBLFD.HTMAINREF[_hashItemList["ptserial"]]) != null && ((Hashtable)objBLFD.HTMAINREF[_hashItemList["ptserial"]]).Count != 0)
            {
                Hashtable htparam = new Hashtable();
                htparam.Add("@aref_tran_id", ((Hashtable)objBLFD.HTMAINREF[_hashItemList["ptserial"]])["ref_tran_id"].ToString());
                htparam.Add("@aref_tran_cd", ((Hashtable)objBLFD.HTMAINREF[_hashItemList["ptserial"]])["ref_tran_cd"].ToString());
                htparam.Add("@aref_ptserial", ((Hashtable)objBLFD.HTMAINREF[_hashItemList["ptserial"]])["ref_ptserial"].ToString());
                htparam.Add("@atran_id", objBLFD.Tran_id.ToString());
                htparam.Add("@atran_cd", objBLFD.Code != "" ? objBLFD.Code : "");
                htparam.Add("@aptserial", _hashItemList["ptserial"].ToString());
                htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

                dsetDSSchedule = objdblayer.dsprocedure("ISP_DISPATCH_QTY", htparam);

                if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
                {
                    dgvDeallocateSchedule.DataSource = dsetDSSchedule.Tables[0];
                    dgvDeallocateSchedule.Update();
                    int i = 0;
                    foreach (DataRow row in dsetDSSchedule.Tables[0].Rows)
                    {
                        foreach (DataGridViewColumn column in dgvDeallocateSchedule.Columns)
                        {
                            if (dsetDSSchedule.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvDeallocateSchedule.Rows[i].Cells[column.Name].Value = row[column.Name];
                            }
                        }
                        i++;
                    }
                }
                if (ACTIVE_BL.Tran_mode == "view_mode")
                {
                    objHashtables.HashDeallocateSchedule.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                    dgvDeallocateSchedule.Enabled = false;
                    btnDone.Enabled = false;
                }
                if (ACTIVE_BL.Tran_mode != "add_mode")
                {
                    bool flg = true;
                    if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashDeallocateSchedule != null && objBLFD.HASHTABLES.HashDeallocateSchedule.Count != 0)
                    {
                        foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashDeallocateSchedule)
                        {
                            if (entry.Key.ToString().Split(',')[0].ToString() == _hashItemList["ptserial"].ToString())
                            {
                                flg = false;
                                break;
                            }
                        }
                    }
                    if (flg)
                    {
                        EditDSSchedule();
                    }
                }
                #region
                objHashtables = objBLFD.HASHTABLES;
                if (objHashtables != null && objHashtables.HashDeallocateSchedule != null && objHashtables.HashDeallocateSchedule.Count != 0)
                {
                    if (_hashlocalDispatchSchedule != null)
                    {
                        _hashlocalDispatchSchedule.Clear();
                    }
                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashDeallocateSchedule)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            if (entry.Key.ToString().Split(',')[0].ToString() == _hashItemList["ptserial"].ToString())
                            {
                                _hashlocalDispatchSchedule[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                                {
                                    ((Hashtable)_hashlocalDispatchSchedule[entry.Key])[entry1.Key] = entry1.Value;
                                }
                            }
                        }
                    }
                    foreach (DataGridViewRow row in dgvDeallocateSchedule.Rows)
                    {
                        foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                        {
                            key = _hashItemList["ptserial"].ToString() + "," + row.Cells["schedule_no"].Value.ToString();
                            if (((Hashtable)entry.Value).Count != 0 && key == entry.Key.ToString())
                            {
                                if (((Hashtable)entry.Value).Count != 0)
                                {
                                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                                    {
                                        if (dgvDeallocateSchedule.Columns.Contains(entry1.Key.ToString()))
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
            }
            else
            {
                dgvDeallocateSchedule.Enabled = false;
                btnDone.Enabled = false;
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "De-Allocate Schedule";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void EditDSSchedule()
        {
            dgvDeallocateSchedule.AutoGenerateColumns = false;

            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetDSSchedule.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["schedule_no"].ToString();
                    if (objHashtables.HashDeallocateSchedule != null && !objHashtables.HashDeallocateSchedule.Contains(key))
                    {
                        objHashtables.HashDeallocateSchedule[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetDSSchedule.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashDeallocateSchedule[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void dgvDeallocateSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "sel")
                {
                    gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    //foreach (DataGridViewRow row in gridview.Rows)
                    //{
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
                        if (gridview.CurrentCell.OwningColumn.Name == "sel")
                        {
                            _sum_de_schedule_qty = 0; _assigning_qty_value = 0;
                            foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                            {
                                if (((Hashtable)entry.Value).Count != 0)
                                {
                                    if (((Hashtable)entry.Value)["de_schedule_qty"] != null && ((Hashtable)entry.Value)["de_schedule_qty"].ToString() != "")
                                    {
                                        _sum_de_schedule_qty += Convert.ToDecimal(((Hashtable)entry.Value)["de_schedule_qty"].ToString());
                                    }
                                }
                            }
                            if (_sum_de_schedule_qty != 0)
                            {
                                _assigning_qty_value = decimal.Parse(_hashItemList["qty"].ToString()) - _sum_de_schedule_qty;
                            }
                            else
                            {
                                _assigning_qty_value = decimal.Parse(_hashItemList["qty"].ToString());
                            }
                            if (_assigning_qty_value > decimal.Parse(gridview.CurrentRow.Cells["pl_schedule_qty"].Value.ToString()))
                            {
                                gridview.CurrentRow.Cells["de_schedule_qty"].Value = gridview.CurrentRow.Cells["pl_schedule_qty"].Value.ToString();
                                //  _assigning_qty_value -= decimal.Parse(gridview.CurrentRow.Cells["pl_schedule_qty"].Value.ToString());
                            }
                            else
                            {
                                gridview.CurrentRow.Cells["de_schedule_qty"].Value = _assigning_qty_value;
                                // _assigning_qty_value = 0;
                            }
                            gridview.CurrentRow.Cells["de_schedule_dt"].Value = DateTime.Now;
                        }
                        key = _hashItemList["ptserial"].ToString() + "," + gridview.CurrentRow.Cells["schedule_no"].Value.ToString();
                        if (!_hashlocalDispatchSchedule.Contains(key))
                        {
                            _hashlocalDispatchSchedule[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        foreach (DataGridViewColumn column in gridview.Columns)
                        {
                            if (column.Name.ToString().ToLower() != "sel")
                            {
                                ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = gridview.CurrentRow.Cells[column.Name].Value;
                            }
                        }
                    }
                    else
                    {
                        foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                        {
                            if (!_hashlocalDispatchScheduleTemp.Contains(entry.Key))
                            {
                                _hashlocalDispatchScheduleTemp[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)_hashlocalDispatchScheduleTemp[entry.Key])[entry1.Key] = entry1.Value;
                            }
                        }
                        foreach (DictionaryEntry entry in _hashlocalDispatchScheduleTemp)
                        {
                            if (entry.Key.ToString().Contains(_hashItemList["ptserial"].ToString() + "," + gridview.CurrentRow.Cells["schedule_no"].Value.ToString()))
                                ((Hashtable)_hashlocalDispatchSchedule[entry.Key]).Clear();
                        }
                        //  _assigning_qty_value += decimal.Parse(gridview.CurrentRow.Cells["de_schedule_qty"].Value.ToString());
                        gridview.CurrentRow.Cells["de_schedule_qty"].Value = "0.00";
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gridview.CurrentRow.Cells["sel"];
                        chk.Value = false;
                    }
                    //}
                }
            }
        }
        private void dgvDeallocateSchedule_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;

            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (gridview.CurrentCell.OwningColumn.Name == "de_schedule_qty")
                {
                    if (e.FormattedValue != null && e.FormattedValue.ToString() != "")
                    {
                        delivery_qty = e.FormattedValue.ToString();
                    }
                    else
                    {
                        delivery_qty = "0.00";
                    }
                    if (decimal.Parse(delivery_qty) > decimal.Parse(gridview.CurrentRow.Cells["pl_schedule_qty"].Value.ToString()))
                    {
                        AutoClosingMessageBox.Show("Quantity should be lesser than or equal to Schedule Quantity", "Validation");
                        e.Cancel = true;
                    }
                    else
                    {
                        if (((Hashtable)_hashlocalDispatchSchedule).Contains(_hashItemList["ptserial"].ToString() + "," + gridview.CurrentRow.Cells["schedule_no"].Value.ToString()))
                            ((Hashtable)_hashlocalDispatchSchedule[_hashItemList["ptserial"].ToString() + "," + gridview.CurrentRow.Cells["schedule_no"].Value.ToString()])["de_schedule_qty"] = delivery_qty;
                    }
                }
            }
        }
        private void dgvDeallocateSchedule_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDeallocateSchedule.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
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
                foreach (DataGridViewRow row in dgvDeallocateSchedule.Rows)
                {
                    key = _hashItemList["ptserial"].ToString() + "," + row.Cells["schedule_no"].Value.ToString();
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
                        foreach (DataGridViewColumn column in dgvDeallocateSchedule.Columns)
                        {
                            if (column.Name.ToString().ToLower() != "sel")
                            {
                                ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = row.Cells[column.Name].Value;
                            }
                        }
                    }
                }
                decimal qty = 0;
                foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                {
                    if (!objHashtables.HashDeallocateSchedule.Contains(entry.Key))
                    {
                        objHashtables.HashDeallocateSchedule[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key])[entry1.Key] = entry1.Value;
                        }
                        if (((Hashtable)entry.Value)["de_schedule_qty"] != null && ((Hashtable)entry.Value)["de_schedule_qty"].ToString() != "")
                        {
                            qty += Convert.ToDecimal(((Hashtable)entry.Value)["de_schedule_qty"].ToString());
                        }
                        ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key])["compid"] = objBLFD.ObjCompany.Compid;
                        ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key])["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                        ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                        ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key])["ptserial"] = _hashItemList["ptserial"].ToString();
                    }
                    else
                    {
                        ((Hashtable)objHashtables.HashDeallocateSchedule[entry.Key]).Clear();
                    }
                }
                if (qty != Convert.ToDecimal(_hashItemList["qty"].ToString()))
                {
                    AutoClosingMessageBox.Show("Total Dispatching Qty not equal to Order Qty", "Validation");
                    foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                    {
                        objHashtables.HashDeallocateSchedule.Remove(entry.Key);
                    }
                }
                else
                {
                    objBLFD.HASHTABLES = objHashtables;
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void dgvDeallocateSchedule_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                if (txt.Name == "de_schedule_qty")
                {
                    txt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    txt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
            }
        }
        private void txt_Key_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Name == "de_schedule_qty")
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
                if (txt.Tag.ToString().Trim() == "int")
                {
                    if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
