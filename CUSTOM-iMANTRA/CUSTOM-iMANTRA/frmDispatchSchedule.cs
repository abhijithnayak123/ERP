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
    public partial class frmDispatchSchedule : CustomBaseForm
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.08.13 06.39PM
         * 1.0 Add Schedule details to hashtable and find item qty >=schedule qty.
         * 
         * 2.0 Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.13.13 --> added new field schedule_id 
         * 3.0 Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.13.13 --> edit_mode & add/remove option.
         * 
      * */

        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private BLHT objHashtables = new BLHT();

        private Hashtable _hashItemList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalDispatchSchedule = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string key = "";

        string _local_schedule_qty = "0";

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

        public frmDispatchSchedule()
        {
            InitializeComponent();
        }

        private void frmDispatchSchedule_Load(object sender, EventArgs e)
        {
            lblName.Text = "Product : " + _hashItemList["prod_nm"].ToString();
            lblQty.Text = "Qty : " + _hashItemList["qty"].ToString();
            dgvDispatchSchedule.AutoGenerateColumns = false;

            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashMaintbl.Clear();
                objBLFD.HASHTABLES = objHashtables;
                dgvDispatchSchedule.Enabled = false;
                btnDone.Enabled = false;
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
            }
            #region
            objHashtables = objBLFD.HASHTABLES;
            #region 3.0
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                bool flg = true;
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashMaintbl != null && objBLFD.HASHTABLES.HashMaintbl.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
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
            #endregion 3.0
            if (objHashtables != null && objHashtables.HashMaintbl != null && objHashtables.HashMaintbl.Count != 0)
            {
                int i = 0;
                int j = 0;
                while (dgvDispatchSchedule.Rows.Count > 0)
                {
                    if (!dgvDispatchSchedule.Rows[0].IsNewRow)
                    {
                        dgvDispatchSchedule.Rows.RemoveAt(0);
                    }
                }

                if (_hashlocalDispatchSchedule != null)
                {
                    _hashlocalDispatchSchedule.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashMaintbl)
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
                i = 0;
                foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                {
                    //i = i + 1;
                    //key = _hashItemList["ptserial"].ToString() + "," + i;

                    //if (_hashlocalDispatchSchedule.Contains(key))
                    //{
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        dgvDispatchSchedule.Rows.Add(1);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvDispatchSchedule.Columns.Contains(entry1.Key.ToString()))
                            {
                                dgvDispatchSchedule.Rows[j].Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
                            }
                        }
                        j++;
                    }
                    //}
                }
            }
            else
            {
                objHashtables = new BLHT();
            }
            #endregion
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Schedule";
        }

        private void EditDSSchedule()
        {
            dgvDispatchSchedule.AutoGenerateColumns = false;

            DataSet dsetDSSchedule = objdblayer.dsquery("select ISCHEDULE.schedule_id,ISCHEDULE.schedule_no,schedule_dt,schedule_qty,tran_no,tran_id,ptserial,compid,fin_yr,isnull(sum_de_schedule_qty,0) dis_schedule_qty from ISCHEDULE LEFT JOIN IVW_DISPATCH_QTY vw ON iSCHEDULE.tran_id=vw.ref_tran_id and iSCHEDULE.tran_cd=vw.ref_tran_cd and iSCHEDULE.tran_no=vw.ref_tran_no and iSCHEDULE.ptserial=vw.ref_ptserial  and iSCHEDULE.schedule_id=vw.schedule_id where tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "' and ptserial='" + _hashItemList["ptserial"].ToString() + "'");
            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetDSSchedule.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["schedule_no"].ToString();
                    if (objHashtables.HashMaintbl != null && !objHashtables.HashMaintbl.Contains(key))
                    {
                        objHashtables.HashMaintbl[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetDSSchedule.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashMaintbl[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void myDataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                if (dgvDispatchSchedule.CurrentCell.OwningColumn.Name == "schedule_dt" && (e.FormattedValue == null || e.FormattedValue.ToString() == ""))
                {
                    AutoClosingMessageBox.Show("Please enter schedule date","Validation");
                    e.Cancel = true;
                }
                if (dgvDispatchSchedule.CurrentCell.OwningColumn.Name == "schedule_qty")
                {
                    _local_schedule_qty = "0";
                    if (e.FormattedValue == null || e.FormattedValue.ToString() == "")
                    {
                        _local_schedule_qty = "0";
                        AutoClosingMessageBox.Show("Please enter quantity","Validation");
                        e.Cancel = true;
                    }
                    else
                    {
                        _local_schedule_qty = e.FormattedValue.ToString();
                    }
                    if (decimal.Parse(dgvDispatchSchedule.CurrentRow.Cells["dis_schedule_qty"].Value.ToString()) > decimal.Parse(_local_schedule_qty))
                    {
                        AutoClosingMessageBox.Show("Sorry Changing Schedule Quantity is not possible bcos it is already de-scheduled","Validation");
                        e.Cancel = true;
                    }
                    // e.Cancel = !ReturnChangingScheduleQuantityAllowed(_local_schedule_qty);
                }
            }
        }

        private bool ReturnChangingScheduleQuantityAllowed(string qty)
        {
            DataSet dsetDSSchedule = objdblayer.dsquery("select * from IVW_DISPATCH_QTY where ref_tran_id=" + objBLFD.Tran_id + " and ref_tran_cd='" + objBLFD.Code + "' and ref_ptserial='" + _hashItemList["ptserial"].ToString() + "' and schedule_no='" + dgvDispatchSchedule.CurrentRow.Cells["schedule_no"].Value + "'");
            if (dsetDSSchedule != null && dsetDSSchedule.Tables.Count != 0 && dsetDSSchedule.Tables[0].Rows.Count != 0)
            {
                if (decimal.Parse(dsetDSSchedule.Tables[0].Rows[0]["sum_de_schedule_qty"].ToString()) > decimal.Parse(qty))
                {
                    AutoClosingMessageBox.Show("Sorry Changing Schedule Quantity is not possible bcos it is already de-scheduled","Validation");
                    return false;
                }
            }
            return true;
        }

        private void dgvDispatchSchedule_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                if (txt.Name == "schedule_qty")
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
                if (txt.Name == "schedule_qty")
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (ACTIVE_BL.Tran_mode != "view_mode")
            {
                foreach (DataGridViewRow row in dgvDispatchSchedule.Rows)
                {
                    row.Cells["schedule_no"].Value = dgvDispatchSchedule["schedule_no", row.Index].Value;
                    key = _hashItemList["ptserial"].ToString() + "," + row.Cells["schedule_no"].Value.ToString();

                    foreach (DataGridViewColumn column in dgvDispatchSchedule.Columns)
                    {
                        if (column.Name.ToLower() == "schedule_dt" && (row.Cells[column.Name].Value == null || row.Cells[column.Name].Value.ToString() == ""))
                        {
                            ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = DateTime.Parse("1900/01/01");
                        }
                        else
                        {
                            ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = row.Cells[column.Name].Value;
                        }
                    }
                    ((Hashtable)_hashlocalDispatchSchedule[key])["tran_no"] = ACTIVE_BL.HTMAIN["tran_no"].ToString();
                    ((Hashtable)_hashlocalDispatchSchedule[key])["tran_id"] = ACTIVE_BL.Tran_id;
                    ((Hashtable)_hashlocalDispatchSchedule[key])["tran_cd"] = ACTIVE_BL.Code;
                    ((Hashtable)_hashlocalDispatchSchedule[key])["ptserial"] = _hashItemList["ptserial"].ToString();
                }
                #region 1.0
                decimal qty = 0;
                foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (!objHashtables.HashMaintbl.Contains(entry.Key))
                        {
                            objHashtables.HashMaintbl[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }

                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key])[entry1.Key] = entry1.Value;
                        }
                        if (((Hashtable)entry.Value)["schedule_qty"] != null && ((Hashtable)entry.Value)["schedule_qty"].ToString() != "")
                        {
                            qty += Convert.ToDecimal(((Hashtable)entry.Value)["schedule_qty"].ToString());
                        }
                        ((Hashtable)objHashtables.HashMaintbl[entry.Key])["compid"] = objBLFD.ObjCompany.Compid;
                        ((Hashtable)objHashtables.HashMaintbl[entry.Key])["fin_yr"] = objBLFD.ObjCompany.Fin_yr;
                    }
                    else
                    {
                        if (((Hashtable)objHashtables.HashMaintbl).Contains(entry.Key))
                        {
                            ((Hashtable)objHashtables.HashMaintbl[entry.Key]).Clear();
                        }
                    }
                }
                if (qty != Convert.ToDecimal(_hashItemList["qty"].ToString()))
                {
                    AutoClosingMessageBox.Show("Total Schedule Qty can't be greater than Order Qty","Validation");
                    foreach (DictionaryEntry entry in _hashlocalDispatchSchedule)
                    {
                        objHashtables.HashMaintbl.Remove(entry.Key);
                    }
                }
                else
                {
                    objBLFD.HASHTABLES = objHashtables;
                    this.Close();
                }
                #endregion 1.0
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvDispatchSchedule.Rows.Add(1);
            dgvDispatchSchedule.Rows[dgvDispatchSchedule.Rows.Count - 1].Cells["schedule_dt"].Selected = true;
            dgvDispatchSchedule["schedule_id", dgvDispatchSchedule.Rows.Count - 1].Value = "0";//2.0
            dgvDispatchSchedule["dis_schedule_qty", dgvDispatchSchedule.Rows.Count - 1].Value = "0.00";//2.0
            dgvDispatchSchedule["schedule_no", dgvDispatchSchedule.Rows.Count - 1].Value = dgvDispatchSchedule.Rows.Count;
            dgvDispatchSchedule.CurrentCell = dgvDispatchSchedule["schedule_dt", dgvDispatchSchedule.Rows.Count - 1];

            //dgvDispatchSchedule.CurrentRow.Cells["schedule_dt"].ReadOnly = false;
            //dgvDispatchSchedule.CurrentRow.Cells["qty"].ReadOnly = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //if (ReturnChangingScheduleQuantityAllowed("0"))
            //{
            if (dgvDispatchSchedule.CurrentRow.Cells["dis_schedule_qty"].Value == null || dgvDispatchSchedule.CurrentRow.Cells["dis_schedule_qty"].Value.ToString()=="" || dgvDispatchSchedule.CurrentRow.Cells["dis_schedule_qty"].Value.ToString() == "0.00")
            {
                if (_hashlocalDispatchSchedule.Contains(HashItemList["ptserial"] + "," + dgvDispatchSchedule.CurrentRow.Cells["schedule_no"].Value.ToString()))
                {
                    ((Hashtable)_hashlocalDispatchSchedule[HashItemList["ptserial"] + "," + dgvDispatchSchedule.CurrentRow.Cells["schedule_no"].Value.ToString()]).Clear();
                }
                dgvDispatchSchedule.Rows.Remove(dgvDispatchSchedule.CurrentRow);
            }
            else
            {
                AutoClosingMessageBox.Show("Sorry Removing Schedule is not possible bcos it is already de-scheduled","Validation");
            }
            //}
        }

        private void dgvDispatchSchedule_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDispatchSchedule.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvDispatchSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                foreach (DataGridViewRow row in gridview.Rows)
                {
                    row.Cells["schedule_no"].Value = dgvDispatchSchedule["schedule_no", row.Index].Value;
                    key = _hashItemList["ptserial"].ToString() + "," + row.Cells["schedule_no"].Value.ToString();

                    if (!_hashlocalDispatchSchedule.Contains(key))
                    {
                        _hashlocalDispatchSchedule[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    foreach (DataGridViewColumn column in gridview.Columns)
                    {
                        ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
            }
        }

        private void dgvDispatchSchedule_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
                foreach (DataGridViewRow row in gridview.Rows)
                {
                    row.Cells["schedule_no"].Value = dgvDispatchSchedule["schedule_no", row.Index].Value;
                    key = _hashItemList["ptserial"].ToString() + "," + row.Cells["schedule_no"].Value.ToString();

                    if (!_hashlocalDispatchSchedule.Contains(key))
                    {
                        _hashlocalDispatchSchedule[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    foreach (DataGridViewColumn column in gridview.Columns)
                    {
                        ((Hashtable)_hashlocalDispatchSchedule[key])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
            }
        }
    }
}
