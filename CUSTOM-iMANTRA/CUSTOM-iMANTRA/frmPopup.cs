using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace CUSTOM_iMANTRA
{
    public partial class frmPopup : CustomBaseForm
    {
        dblayer objdblayer = new dblayer();

        //public Hashtable htpopup = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        DataSet ds = new DataSet();
        string strserachfield = "", strQueryUpdate = "", table = "", getfields = "", setfields = "", strstatus, _tran_cd, _con;
        string[] strdisp_value = new string[2];
        string[] strdisp_name;
        string[] fields;
        string strStore_Id, strStoring_nm;
        private string resultFieldValue = "";

        public string ResultFieldValue
        {
            get { return resultFieldValue; }
            set { resultFieldValue = value; }
        }

        public frmPopup(string ptable, string ref_tbl_tran_cd, string pdisp_v, string pdisp_n, string label, string con, string status = "0")
        {

            InitializeComponent();
            //  htpopup = ht;
            table = ptable;
            strdisp_value = pdisp_v.Split(',');
            strdisp_name = pdisp_n.Split(',');
            strStore_Id = strdisp_value[0].Split(';')[0];
            strStoring_nm = strdisp_value[1].Split(';')[0];
            strserachfield = strStoring_nm;
            _tran_cd = ref_tbl_tran_cd;
            _con = con;
            this.Text = label;
            strstatus = status;
            if (status == "0")
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                // this.Dock = DockStyle.Fill;
                this.WindowState = FormWindowState.Maximized;
                TextBox1.Text = "";
            }
        }
        private void frmPopup_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < strdisp_name.Length; i++)
            {
                fields = strdisp_name[i].Split(';');
                // if (fields[0].Trim().ToLower() != strdisp_value[0].Trim().ToLower() && fields[0].Trim().ToLower() != strdisp_value[1].Trim().ToLower())
                if (fields[0].Trim().ToLower() != strStore_Id.Trim().ToLower() && fields[0].Trim().ToLower() != strStoring_nm.Trim().ToLower())
                {
                    getfields += fields[0] + ",";
                    if (fields.Length > 1)
                    {
                        setfields += fields[1] + ",";
                    }
                }
            }
            if (_tran_cd == "")
            {
                if (_con != "")
                {
                    strQueryUpdate = "select " + getfields + strStore_Id.Trim().ToLower() + "," + strStoring_nm.Trim().ToLower() + " from " + table + " where " + _con;
                }
                else
                {
                    strQueryUpdate = "select " + getfields + strStore_Id.Trim().ToLower() + "," + strStoring_nm.Trim().ToLower() + " from " + table;
                }
            }
            else
            {
                if (_con != "")
                {
                    strQueryUpdate = "select " + getfields + strStore_Id.Trim().ToLower() + "," + strStoring_nm.Trim().ToLower() + " from " + table + " where tran_cd='" + _tran_cd + "' and " + _con;
                }
                else
                {
                    strQueryUpdate = "select " + getfields + strStore_Id.Trim().ToLower() + "," + strStoring_nm.Trim().ToLower() + " from " + table + " where tran_cd='" + _tran_cd + "'";
                }
            }
            ds = objdblayer.dsquery(strQueryUpdate);
            BIND_GRID();
            // TextBox1.Text = htpopup[strStoring_nm] != null ? htpopup[strStoring_nm].ToString() : "";
            TextBox1.Select();
            //if (TextBox1.CanFocus)
            //{
            // TextBox1.Focus();
            //}
        }
        private void BIND_GRID()
        {
            dgvpopup.DataSource = ds.Tables[0];
            dgvpopup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvpopup.Columns[strserachfield].DefaultCellStyle.BackColor = Color.Yellow;
            this.dgvpopup.ScrollBars = ScrollBars.Both;
            dgvpopup.DefaultCellStyle.SelectionBackColor = Color.LightPink;
            if (strstatus != "0")
            {
                dgvpopup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dgvpopup.Bounds = new Rectangle((this.ClientSize.Width / 100) * (3 / 2), (this.ClientSize.Height / 100) * 5, (this.ClientSize.Width / 100) * 97, (this.ClientSize.Height / 100) * 92);
            }
            else
            {
                int j = 0, k = 0;
                for (int i = 0; i < strdisp_name.Length; i++)
                {
                    fields = strdisp_name[i].Split(';');
                    if (fields.Length > 1)
                    {
                        dgvpopup.Columns[fields[0]].HeaderText = fields[1];
                    }
                    if (fields[0].Trim().ToLower() == strStore_Id.Trim().ToLower())
                    {
                        dgvpopup.Columns[strStore_Id].Visible = true;
                        j = 1;
                    }
                    if (fields[0].Trim().ToLower() == strStoring_nm.Trim().ToLower())
                    {
                        dgvpopup.Columns[strStoring_nm].Visible = true;
                        k = 1;
                    }
                }
                if (j == 0)
                    dgvpopup.Columns[strStore_Id].Visible = false;
                if (k == 0)
                    dgvpopup.Columns[strStore_Id].Visible = false;
            }
            dgvpopup.Update();
        }
        private void Sel_Value()
        {
            string strValue = strdisp_value[0], strName = strdisp_value[1];
            if (strdisp_value[0].Contains(";"))
            {
                strValue = strdisp_value[0].Split(';')[1];
            }
            if (strdisp_value[1].Contains(";"))
            {
                strName = strdisp_value[1].Split(';')[1];
            }
            resultFieldValue = dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStore_Id].Value.ToString() + "," + dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStoring_nm].Value.ToString();
            // htpopup[strValue] = dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStore_Id].Value.ToString();
            //  htpopup[strName] = dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStoring_nm].Value.ToString();

            this.Close();
        }
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Sel_Value();
            }
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ds.Clear();
            if (_tran_cd == "")
            {
                ds = objdblayer.dsquery(strQueryUpdate + " where " + strserachfield + " like '%" + TextBox1.Text + "%'");
            }
            else
            {
                ds = objdblayer.dsquery(strQueryUpdate + " and " + strserachfield + " like '%" + TextBox1.Text + "%'");
            }

            BIND_GRID();
        }
        private void dgvpopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Sel_Value();
            }
            else if (e.KeyData == Keys.Up && dgvpopup.CurrentRow.Index < 1)
            {
                TextBox1.Select();
            }
        }
        private void dgvpopup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                Sel_Value();
            }
            else
            {
                strserachfield = dgvpopup.Columns[e.ColumnIndex].Name;
            }
        }

        private void dgvpopup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //  TextBox1.Text = "";
            if (e.RowIndex == -1)
            {
                strserachfield = dgvpopup.Columns[e.ColumnIndex].Name;
                foreach (DataGridViewColumn row in dgvpopup.Columns)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                dgvpopup.Columns[strserachfield].DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgvpopup.Select();
            }
        }
    }
}
