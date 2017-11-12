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
    public partial class frmFileUpload : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private BLHT objHashtables = new BLHT();

        private Hashtable _hashItemList = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashlocalFIleUpload = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string key = "", _local_ptserial = "0";
        bool _blnTypewise = true;

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

        public frmFileUpload(bool _blnHeaderOrItem)
        {
            InitializeComponent();
            _blnTypewise = _blnHeaderOrItem;
        }

        private void frmFileUpload_Load(object sender, EventArgs e)
        {
            dgvFileUpload.AutoGenerateColumns = false;
            if (_blnTypewise)
            {
                _local_ptserial = "0";
            }
            else
            {
                _local_ptserial = _hashItemList["ptserial"].ToString();
            }

            if (ACTIVE_BL.Tran_mode == "view_mode")
            {
                objHashtables.HashFileUpload.Clear();
                objBLFD.HASHTABLES = objHashtables;
               // dgvFileUpload.Enabled = false;
                btnDone.Enabled = false;
                btnAdd.Enabled = false;
                btnRemove.Enabled = false;
                btnPreview.Enabled = true;
            }
            else
            {
                btnPreview.Enabled = false;
            }
            #region
            objHashtables = objBLFD.HASHTABLES;
            #region 3.0
            if (ACTIVE_BL.Tran_mode != "add_mode")
            {
                bool flg = true;
                if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashFileUpload != null && objBLFD.HASHTABLES.HashFileUpload.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashFileUpload)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _local_ptserial)
                        {
                            flg = false;
                            break;
                        }
                    }
                }
                if (flg)
                {
                    if (objHashtables == null)
                        objHashtables = new BLHT();
                    EditFileUpload();
                }
            }
            #endregion 3.0
            if (objHashtables != null && objHashtables.HashFileUpload != null && objHashtables.HashFileUpload.Count != 0)
            {
                int j = 0;
                while (dgvFileUpload.Rows.Count > 0)
                {
                    if (!dgvFileUpload.Rows[0].IsNewRow)
                    {
                        dgvFileUpload.Rows.RemoveAt(0);
                    }
                }
                if (_hashlocalFIleUpload != null)
                {
                    _hashlocalFIleUpload.Clear();
                }
                foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashFileUpload)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (entry.Key.ToString().Split(',')[0].ToString() == _local_ptserial)
                        {
                            _hashlocalFIleUpload[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)_hashlocalFIleUpload[entry.Key])[entry1.Key] = entry1.Value;
                            }
                        }
                    }
                }
                foreach (DictionaryEntry entry in _hashlocalFIleUpload)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        dgvFileUpload.Rows.Add(1);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            if (dgvFileUpload.Columns.Contains(entry1.Key.ToString()))
                            {
                                dgvFileUpload.Rows[j].Cells[entry1.Key.ToString()].Value = entry1.Value.ToString();
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
            #endregion
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "File Upload";
        }

        private void EditFileUpload()
        {
            dgvFileUpload.AutoGenerateColumns = false;

            DataSet dsetFileUpload = objdblayer.dsquery("select * from FILE_UPLOAD where tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "' and ptserial='" + _local_ptserial + "'");
            if (dsetFileUpload != null && dsetFileUpload.Tables.Count != 0 && dsetFileUpload.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetFileUpload.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["si_no"].ToString();
                    if (objHashtables != null && objHashtables.HashFileUpload != null && !objHashtables.HashFileUpload.Contains(key))
                    {
                        objHashtables.HashFileUpload[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetFileUpload.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashFileUpload[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
            objBLFD.HASHTABLES = objHashtables;
        }

        private void dgvFileUpload_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvFileUpload.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvFileUpload.Rows.Add();
            dgvFileUpload["fileId", dgvFileUpload.Rows.Count - 1].Value = "0";
            dgvFileUpload["si_no", dgvFileUpload.Rows.Count - 1].Value = dgvFileUpload.Rows.Count;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            dgvFileUpload.Rows.Remove(dgvFileUpload.CurrentRow);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (_hashlocalFIleUpload != null)
                {
                    _hashlocalFIleUpload.Clear();
                    foreach (DataGridViewRow row in dgvFileUpload.Rows)
                    {
                        if (_blnTypewise)
                        {
                            key = "0," + row.Cells["si_no"].Value.ToString();
                        }
                        else
                        {
                            key = _local_ptserial + "," + row.Cells["si_no"].Value.ToString();
                        }
                        if (!_hashlocalFIleUpload.Contains(key))
                        {
                            _hashlocalFIleUpload[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        ((Hashtable)_hashlocalFIleUpload[key])["si_no"] = row.Cells["si_no"].Value;
                        ((Hashtable)_hashlocalFIleUpload[key])["file_nm"] = row.Cells["file_nm"].Value;
                        ((Hashtable)_hashlocalFIleUpload[key])["file_path"] = row.Cells["file_path"].Value;
                    }
                    foreach (DictionaryEntry entry in _hashlocalFIleUpload)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            if (!objHashtables.HashFileUpload.Contains(entry.Key))
                            {
                                objHashtables.HashFileUpload[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                            {
                                ((Hashtable)objHashtables.HashFileUpload[entry.Key])[entry1.Key] = entry1.Value;
                            }
                        }
                        else
                        {
                            if (((Hashtable)objHashtables.HashFileUpload).Contains(entry.Key))
                            {
                                ((Hashtable)objHashtables.HashFileUpload[entry.Key]).Clear();
                            }
                        }
                    }
                    objBLFD.HASHTABLES = objHashtables;
                }
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvFileUpload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    POPUPBUTTON_FOR_GRID col = dgv.Columns[e.ColumnIndex] as POPUPBUTTON_FOR_GRID;
                    if (col != null)
                    {
                        DataGridViewButtonCell b = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                        if (b != null)
                        {
                            OpenFileDialog objOpenDialog = new OpenFileDialog();
                            objOpenDialog.Title = "Upload File";
                            objOpenDialog.Filter = "*|*";
                            if (dgv.CurrentRow.Cells["file_path"].Value != null && dgv.CurrentRow.Cells["file_path"].Value.ToString() != "")
                            {
                                // objOpenDialog.InitialDirectory = dgv.CurrentRow.Cells["file_nm"].Value.ToString();
                                objOpenDialog.InitialDirectory = System.IO.Path.GetDirectoryName(dgv.CurrentRow.Cells["file_path"].Value.ToString());
                            }
                            else
                                objOpenDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            objOpenDialog.RestoreDirectory = true;
                            if (objOpenDialog.ShowDialog() == DialogResult.OK)
                            {
                                dgv.CurrentRow.Cells["file_path"].Value = objOpenDialog.FileName;
                                dgv.CurrentRow.Cells["file_nm"].Value = objOpenDialog.SafeFileName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception");
            }
        }

        private void dgvFileUpload_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null)
                {
                    POPUPBUTTON_FOR_GRID col = dgv.Columns[dgv.CurrentCell.ColumnIndex] as POPUPBUTTON_FOR_GRID;
                    if (col != null)
                    {
                        DataGridViewButtonCell b = dgv.CurrentCell as DataGridViewButtonCell;
                        if (b != null)
                        {
                            OpenFileDialog objOpenDialog = new OpenFileDialog();
                            objOpenDialog.Title = "Upload File";
                            objOpenDialog.Filter = "*|*";
                            if (dgv.CurrentRow.Cells["file_path"].Value != null && dgv.CurrentRow.Cells["file_path"].Value.ToString() != "")
                            {
                                objOpenDialog.InitialDirectory = dgv.CurrentRow.Cells["file_path"].Value.ToString();
                            }
                            else
                                objOpenDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            if (objOpenDialog.ShowDialog() == DialogResult.OK)
                            {
                                dgv.CurrentRow.Cells["file_path"].Value = objOpenDialog.FileName;
                                dgv.CurrentRow.Cells["file_nm"].Value = objOpenDialog.SafeFileName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception");
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dgvFileUpload.CurrentRow.Cells["si_no"].Value != null && dgvFileUpload.CurrentRow.Cells["si_no"].Value.ToString() != "")
            {
                if (_blnTypewise)
                {
                    key = "0," + dgvFileUpload.CurrentRow.Cells["si_no"].Value.ToString();
                }
                else
                {
                    key = _hashItemList["ptserial"].ToString() + "," + dgvFileUpload.CurrentRow.Cells["si_no"].Value.ToString();
                }                
                if (((Hashtable)_hashlocalFIleUpload[key]).Count != 0)
                {
                    if (System.IO.File.Exists(((Hashtable)_hashlocalFIleUpload[key])["file_path"].ToString()))
                    {
                        System.Diagnostics.Process.Start(((Hashtable)_hashlocalFIleUpload[key])["file_path"].ToString());
                    }
                }
            }
            else
            {
                foreach (DictionaryEntry entry in _hashlocalFIleUpload)
                {
                    if (((Hashtable)entry.Value).Count != 0)
                    {
                        if (System.IO.File.Exists(((Hashtable)entry.Value)["file_path"].ToString()))
                        {
                            System.Diagnostics.Process.Start(((Hashtable)entry.Value)["file_path"].ToString());
                        }
                    }
                }
            }
        }
    }
}
