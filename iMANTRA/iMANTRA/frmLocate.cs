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
using iMANTRA_DL;
using iMANTRA_iniL;

namespace iMANTRA
{
    public partial class frmLocate : BaseClass
    {
        BL_BASEFIELD _objBFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBFD
        {
            get { return _objBFD; }
            set { _objBFD = value; }
        }

        FL_BASEFIELD objFL_BASEFIELD = new FL_BASEFIELD();

        DL_ADAPTER objdlAdapter = new DL_ADAPTER();
        Ini objIni = new Ini();//initialise/show or hide controls in the page

        public Hashtable htpopup = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string strserachfield = "", strQueryUpdate = "", strQueryExistance = "", table = "", getfields = "", setfields = "", strstatus, _tran_cd, _con;
        string[] strdisp_value = new string[2];
        string[] strdisp_name;
        string[] fields;
        string strStore_Id, strStoring_nm, alias_id, alias_nm, searchcond = "", fin_yr = "";
        int selected_value = 0;
        bool blnFin_Yr = false, blnCompid = false;

        public frmLocate(Hashtable ht, string ptable, string ref_tbl_tran_cd, string pdisp_v, string pdisp_n, string label, string con, string status = "0")
        {
            InitializeComponent();
            htpopup = ht;
            table = ptable;
            strdisp_value = pdisp_v.Split(',');
            strdisp_name = pdisp_n.Split(',');
            //strStore_Id = strdisp_value[0].Split(';')[0];
            //strStoring_nm = strdisp_value[1].Split(';')[0];
            alias_id = strdisp_value[0].Split(';')[0];
            alias_nm = strdisp_value[1].Split(';')[0];
            strStore_Id = strdisp_value[0].Split(';').Length > 1 ? strdisp_value[0].Split(';')[1] : strdisp_value[0].Split(';')[0];
            strStoring_nm = strdisp_value[1].Split(';').Length > 1 ? strdisp_value[1].Split(';')[1] : strdisp_value[1].Split(';')[0];
            strserachfield = alias_nm;
            _tran_cd = ref_tbl_tran_cd;
            _con = con;
            // this.Text = label;

            ucToolBar1.Titlebar = label;
            strstatus = status;
            if (status == "0")
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                //this.Dock = DockStyle.Fill;              
                this.Width = this.ClientSize.Width;// *90 / 100;
                this.Height = this.ClientSize.Height;// *90 / 100;               
                TextBox1.Text = "";
            }
        }

        private void frmLocate_Load(object sender, EventArgs e)
        {
            fin_yr = ObjBFD.ObjCompany.Fin_yr;

            string str_nm = (alias_nm == strStoring_nm ? strStoring_nm.Trim().ToLower() : alias_nm + " as " + strStoring_nm.Trim().ToLower());
            string str_id = (alias_id == strStore_Id ? strStore_Id.Trim().ToLower() : alias_id + " as " + strStore_Id.Trim().ToLower());

            DataSet dsDDL1 = new DataSet();
            DataSet dsDDL2 = new DataSet();

            dsDDL1 = objdlAdapter.dsquery("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + table + "' AND COLUMN_NAME = 'compid'");
            dsDDL2 = objdlAdapter.dsquery("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + table + "' AND COLUMN_NAME = 'fin_yr'");
            if (dsDDL1 != null && dsDDL1.Tables.Count != 0 && dsDDL1.Tables[0].Rows.Count != 0)
            {
                blnCompid = true;
            }
            if (strstatus != "0" && dsDDL2 != null && dsDDL2.Tables.Count != 0 && dsDDL2.Tables[0].Rows.Count != 0)
            {
                blnFin_Yr = true;
            }
            objFL_BASEFIELD.objCompany = ObjBFD.ObjCompany;//objCompany;

            for (int i = 0; i < strdisp_name.Length; i++)
            {
                fields = strdisp_name[i].Split(';');
                if (fields[0].Trim().ToLower() != alias_id.Trim().ToLower() && fields[0].Trim().ToLower() != alias_nm.Trim().ToLower())
                {
                    getfields += fields[0] + ",";
                    if (fields.Length > 1)
                    {
                        setfields += fields[1] + ",";
                    }
                }
            }
            #region 1.0
            if (str_nm == "tran_cd")
                str_nm = "tran_cd";
            if (str_id == "tran_id")
                str_id = "tran_id";
            if (blnFin_Yr)
            {
                strQueryExistance = "select " + str_nm + "," + getfields + str_id + ",fin_yr from " + table + " where 1=1 ";
            }
            else
            {
                strQueryExistance = "select " + str_nm + "," + getfields + str_id + " from " + table + " where 1=1 ";
            }
            if (_tran_cd == "")
            {
                if (_con != "")
                {
                    if (blnCompid)
                    {
                        strQueryExistance += " and compid='" + ObjBFD.ObjCompany.Compid + "'";
                    }
                    //if (blnFin_Yr)
                    //{
                    //    strQueryExistance += " and fin_yr='" + ObjBFD.ObjCompany.Fin_yr + "'";
                    //}
                    strQueryExistance += " and " + _con;
                }
                else
                {
                    if (blnCompid)
                    {
                        strQueryExistance += " and compid='" + ObjBFD.ObjCompany.Compid + "'";
                    }
                    //if (blnFin_Yr)
                    //{
                    //    strQueryExistance += " and fin_yr='" + ObjBFD.ObjCompany.Fin_yr + "'";
                    //}
                }
            }
            else
            {
                if (_con != "")
                {
                    if (blnCompid)
                    {
                        strQueryExistance += " and compid='" + ObjBFD.ObjCompany.Compid + "'";
                    }
                    //if (blnFin_Yr)
                    //{
                    //    strQueryExistance += " and fin_yr='" + ObjBFD.ObjCompany.Fin_yr + "'";
                    //}
                    strQueryExistance += " and tran_cd='" + _tran_cd + "' and " + _con;
                }
                else
                {
                    if (blnCompid)
                    {
                        strQueryExistance += " and compid='" + ObjBFD.ObjCompany.Compid + "'";
                    }
                    //if (blnFin_Yr)
                    //{
                    //    strQueryExistance += " and fin_yr='" + ObjBFD.ObjCompany.Fin_yr + "'";
                    //}
                    strQueryExistance += " and tran_cd='" + _tran_cd + "'";
                }
            }

            string strTran_dt = ObjBFD.HTMAIN.Contains("tran_dt") && ObjBFD.HTMAIN["tran_dt"] != null && ObjBFD.HTMAIN["tran_dt"].ToString() != "" ? ObjBFD.HTMAIN["tran_dt"].ToString() : "getdate()";

            DataSet dsIsDeactivate = objdlAdapter.dsquery("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + table + "' AND COLUMN_NAME = 'isdeactive'");
            if (strstatus == "0" && (dsIsDeactivate != null && dsIsDeactivate.Tables.Count != 0 && dsIsDeactivate.Tables[0].Rows.Count != 0))
            {
                DataSet dsetDeactivatefrm = objdlAdapter.dsquery("SELECT * FROM information_schema.COLUMNS WHERE  TABLE_NAME = '" + table + "' AND COLUMN_NAME = 'deactfrm'");
                if (dsetDeactivatefrm != null && dsetDeactivatefrm.Tables.Count != 0 && dsetDeactivatefrm.Tables[0].Rows.Count != 0)
                {
                    strQueryUpdate = strQueryExistance + " and isdeactive=case when(isdeactive=1) then case when(datediff(day,'" + strTran_dt + "',case when(deactfrm='1900-01-01 00:00:00.000' or deactfrm='2000-01-01 00:00:00.000') then '" + strTran_dt + "' else deactfrm end)>=0) then 1 else 0 end else 0 end";
                }
                else
                {
                    strQueryUpdate = strQueryExistance + " and isdeactive=0";
                }
            }
            else
            {
                strQueryUpdate = strQueryExistance;
            }
            #endregion 1.0


            ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate);
            TextBox1.Text = htpopup[strStoring_nm] != null ? htpopup[strStoring_nm].ToString() : "";
            TextBox1.Select();
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                BIND_GRID();
            }
            objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");//setting database
            DataSet dsetFY = objdlAdapter.dsquery("SELECT * FROM COMP_FIN_YR_MAPPING where compid='" + ObjBFD.ObjCompany.Compid + "'");
            objIni.SetKeyFieldValue("SQL", "initial catalog", ObjBFD.ObjCompany.Db_nm);//setting database
            if (dsetFY != null && dsetFY.Tables.Count != 0 && dsetFY.Tables[0].Rows.Count != 0)
            {
                comboBox1.DataSource = dsetFY.Tables[0];
                comboBox1.DisplayMember = "fin_yr";
                comboBox1.ValueMember = "fin_yr_id";
                comboBox1.Update();
            }
            comboBox1.Text = ObjBFD.ObjCompany.Fin_yr;
            if (comboBox1.SelectedValue != null && comboBox1.SelectedValue.ToString() != "")
            {
                selected_value = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            }
            this.BackColor = ObjBFD.ObjControlSet.Back_color != null ? Color.FromName(ObjBFD.ObjControlSet.Back_color) : Color.White;
            this.ForeColor = ObjBFD.ObjControlSet.Font_color != null ? Color.FromName(ObjBFD.ObjControlSet.Font_color) : Color.Black;
            ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width;
            ucToolBar1.UCbackcolor = ObjBFD.ObjControlSet.Uc_color != null ? Color.FromName(ObjBFD.ObjControlSet.Uc_color) : Color.Maroon;
            this.Font = new Font(ObjBFD.ObjControlSet.Font_family != null ? ObjBFD.ObjControlSet.Font_family : "Courier New", float.Parse(ObjBFD.ObjControlSet.Font_size != null ? ObjBFD.ObjControlSet.Font_size : "9"));
            this.CenterToScreen();
        }

        private void BIND_GRID()
        {
            dgvpopup.DataSource = ds.Tables[0];
            if (blnFin_Yr)
            {
                (dgvpopup.DataSource as DataTable).DefaultView.RowFilter = String.Format("fin_yr = '" + fin_yr + "'");
            }
            dgvpopup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvpopup.Columns[strserachfield].DefaultCellStyle.BackColor = Color.Yellow;
            this.dgvpopup.ScrollBars = ScrollBars.Both;
            dgvpopup.DefaultCellStyle.SelectionBackColor = Color.LightPink;
            if (strstatus != "0")
            {
                dgvpopup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                for (int i = 0; i < strdisp_name.Length; i++)
                {
                    fields = strdisp_name[i].Split(';');
                    if (fields.Length > 1)
                    {
                        //dgvpopup.Columns[fields[0].ToString().Trim()].HeaderText = fields[1];

                        if (dgvpopup.Columns.Contains(fields[0].ToString().Trim()))
                        {
                            dgvpopup.Columns[fields[0].ToString().Trim()].HeaderText = fields[1].Replace('[', ' ').Replace(']', ' ');
                            dgvpopup.Columns[fields[0].ToString().Trim()].Visible = true;
                        }
                        else if (dgvpopup.Columns.Contains(strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm))
                        {
                            dgvpopup.Columns[strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm].HeaderText = fields[1].Replace('[', ' ').Replace(']', ' ');
                            dgvpopup.Columns[strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm].Visible = true;
                        }
                    }
                }
                  if (dgvpopup.Columns.Contains("fin_yr"))
                {
                    dgvpopup.Columns["fin_yr"].HeaderText = "Fiscal Year";
                }
                if (dgvpopup.Columns.Contains("tran_cd"))
                {
                    dgvpopup.Columns["tran_cd"].HeaderText = "Transaction Code";
                }
                if (dgvpopup.Columns.Contains(ObjBFD.Primary_id.ToLower()))
                {
                    dgvpopup.Columns[ObjBFD.Primary_id.ToLower()].Visible = false;
                }
                //this.dgvpopup.Bounds = new Rectangle((this.ClientSize.Width / 100) * (3 / 2), TextBox1.Height + 2, (this.ClientSize.Width / 100) * 90, ((this.ClientSize.Height / 100) * 90) - TextBox1.Height - 2);
            }
            else
            {
                for (int i = 0; i < strdisp_name.Length; i++)
                {
                    fields = strdisp_name[i].Split(';');
                    if (fields.Length > 1)
                    {
                        //dgvpopup.Columns[fields[0].ToString().Trim()].HeaderText = fields[1];

                        if (dgvpopup.Columns.Contains(fields[0].ToString().Trim()))
                        {
                            dgvpopup.Columns[fields[0].ToString().Trim()].HeaderText = fields[1];
                            dgvpopup.Columns[fields[0].ToString().Trim()].Visible = true;
                        }
                        else if (dgvpopup.Columns.Contains(strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm))
                        {
                            dgvpopup.Columns[strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm].HeaderText = fields[1];
                            dgvpopup.Columns[strStoring_nm == fields[0] ? fields[0].ToString().Trim() : strStoring_nm].Visible = true;
                        }
                    }
                }
                //int j = 0, k = 0;
                //for (int i = 0; i < strdisp_name.Length; i++)
                //{
                //    fields = strdisp_name[i].Split(';');
                //    if (fields.Length > 1)
                //    {
                //        dgvpopup.Columns[(strStoring_nm == fields[0] ? strStoring_nm : fields[0])].HeaderText = fields[1];
                //    }
                //    if (fields[0].Trim().ToLower() == strStore_Id.Trim().ToLower())
                //    {
                //        dgvpopup.Columns[strStore_Id].Visible = true;
                //        j = 1;
                //    }
                //    if (fields[0].Trim().ToLower() == strStoring_nm.Trim().ToLower())
                //    {
                //        dgvpopup.Columns[strStoring_nm].Visible = true;
                //        k = 1;
                //    }
                //}
                //if (j == 0)
                //    dgvpopup.Columns[strStore_Id].Visible = false;
                //if (k == 0)
                //    dgvpopup.Columns[strStore_Id].Visible = false;
            }
            foreach (DataGridViewRow row in dgvpopup.Rows)
            {
                if (comboBox1.SelectedValue != null && comboBox1.SelectedValue.ToString() != "")
                {
                    if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) > selected_value)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    else if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) < selected_value)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                    }
                }
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
            if (dgvpopup.SelectedCells.Count != 0)
            {
                htpopup[strValue] = dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStore_Id].Value.ToString();
                htpopup[strName] = dgvpopup.Rows[dgvpopup.SelectedCells[0].RowIndex].Cells[strStoring_nm].Value.ToString();
            }
            if (ObjBFD.ObjCompany.Fin_yr != comboBox1.Text)
            {
                ObjBFD.Locate_fin_yr = true;
            }
            else
            {
                ObjBFD.Locate_fin_yr = false;
            }
            this.Close();
        }

        private void frmLocate_Enter(object sender, EventArgs e)
        {

        }

        private void frmLocate_FormClosed(object sender, FormClosedEventArgs e)
        {

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
            searchcond = (strserachfield == strStoring_nm ? alias_nm : strserachfield == strStore_Id ? alias_id : strserachfield);
            if (_tran_cd == "")
            {
                if (!strQueryUpdate.Contains(" where "))
                {
                    ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " where " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
                }
                else
                {
                    ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " and " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
                }
            }
            else
            {
                ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " and " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
            }

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                BIND_GRID();
            }
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

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                dgvpopup.Select();
            }
        }

        private void dgvpopup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                Sel_Value();
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

        private void frmLocate_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            fin_yr = comboBox1.Text.ToString();
            ds.Clear();
            searchcond = (strserachfield == strStoring_nm ? alias_nm : strserachfield == strStore_Id ? alias_id : strserachfield);
            if (_tran_cd == "")
            {
                if (!strQueryUpdate.Contains(" where "))
                {
                    ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " where " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
                }
                else
                {
                    ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " and " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
                }
            }
            else
            {
                ds = objFL_BASEFIELD.GETPOPUPDETAILS(strQueryUpdate + " and " + searchcond + " like '%" + TextBox1.Text.Replace("'", "''") + "%'");
            }
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                BIND_GRID();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
