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
using iMANTRA_DL;

namespace iMANTRA
{
    public partial class frmListOfUsers : BaseClass
    {
        BL_BASEFIELD objBFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBFD
        {
            get { return objBFD; }
            set { objBFD = value; }
        }

        private string _fld_nm;

        public string Fld_nm
        {
            get { return _fld_nm; }
            set { _fld_nm = value; }
        }
        Hashtable _htLocal = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HtLocal
        {
            get { return _htLocal; }
            set { _htLocal = value; }
        }

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        public frmListOfUsers(Hashtable _ht)
        {
            InitializeComponent();
            _htLocal = _ht;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            string strValidMast = "";
            bool _blnFlg = false;

            foreach (DataGridViewRow row in dgvtranset.Rows)
            {
                //if (row.Cells["Sel"].Value != null && row.Cells["Sel"].Value.ToString() != "" && bool.Parse(row.Cells["Sel"].Value.ToString()))
                //{
                _blnFlg = false;
                if (row.Cells["sel"].Value != null && row.Cells["sel"].Value.ToString() != "")
                {
                    if (row.Cells["sel"].Value.ToString() == "1")
                    {
                        _blnFlg = true;
                    }
                    else if (row.Cells["sel"].Value.ToString() == "0")
                    {
                        _blnFlg = false;
                    }
                }
                if (_blnFlg)
                {
                    if (strValidMast != "")
                    {
                        strValidMast += "," + row.Cells["user_nm"].Value.ToString();
                    }
                    else
                    {
                        strValidMast = row.Cells["user_nm"].Value.ToString();
                    }
                }
                //}
            }
            _htLocal[_fld_nm] = strValidMast;
            this.Close();
        }

        private void frmListOfUsers_Load(object sender, EventArgs e)
        {
            dgvtranset.AutoGenerateColumns = false;
            DataSet ds = new DataSet();
            ds = objDBAdaper.dsquery("select distinct sel=0,userid,user_nm from LOGIN_MAST where compid='" + objBFD.ObjCompany.Compid.ToString() + "' order by user_nm");
            dgvtranset.DataSource = ds.Tables[0];
            dgvtranset.Update();
            int i = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataGridViewColumn column in dgvtranset.Columns)
                {
                    if (ds.Tables[0].Columns.Contains(column.Name) && column.Name != "Sel")
                    {
                        dgvtranset.Rows[i].Cells[column.Name].Value = row[column.Name];
                        if (column.Name == "user_nm" && _htLocal[_fld_nm] != null && _htLocal[_fld_nm].ToString().Split(',').Contains(row[column.Name].ToString()))
                        {
                            dgvtranset.Rows[i].Cells["Sel"].Value = true;
                        }
                    }
                }
                i++;
            }
            //this.BackColor = objBFD.ObjControlSet.Back_color != null ? Color.FromName(objBFD.ObjControlSet.Back_color) : Color.White; this.ForeColor = objBFD.ObjControlSet.Font_color != null ? Color.FromName(objBFD.ObjControlSet.Font_color) : Color.Black; ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width; ucToolBar1.UCbackcolor = objBFD.ObjControlSet.Uc_color != null ? Color.FromName(objBFD.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(objBFD.ObjControlSet.Font_family != null ? objBFD.ObjControlSet.Font_family : "Courier New", float.Parse(objBFD.ObjControlSet.Font_size != null ? objBFD.ObjControlSet.Font_size : "9"));
            AddThemesToTitleBar((Form)this, ucToolBar1, objBFD, "CustomMaster");
            ucToolBar1.Titlebar = "List Of Users";

        }

        private void frmListOfUsers_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
