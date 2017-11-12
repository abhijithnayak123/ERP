using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using iMANTRA_DL;

namespace iMANTRA
{
    public partial class frmListOfMenus : BaseClass
    {
        BL_BASEFIELD objBFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBFD
        {
            get { return objBFD; }
            set { objBFD = value; }
        }
        private string _type;
        private string _validity_fld_nm, _condition = "";

        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public string Validity_fld_nm
        {
            get { return _validity_fld_nm; }
            set { _validity_fld_nm = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        public frmListOfMenus()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            bool _blnFlg = false;
            string strValidMast = "";
            foreach (DataGridViewRow row in dgvtranset.Rows)
            {
                _blnFlg = false;
                if (row.Cells["Sel"].Value != null && row.Cells["Sel"].Value.ToString() != "")
                {
                    if (row.Cells["sel"].Value.ToString() == "1" || row.Cells["sel"].Value.ToString().ToLower() == "true")
                    {
                        _blnFlg = true;
                    }
                    else if (row.Cells["sel"].Value.ToString() == "0" || row.Cells["sel"].Value.ToString().ToLower() == "false")
                    {
                        _blnFlg = false;
                    }
                }
                if (_blnFlg)
                {
                    //if (bool.Parse(row.Cells["Sel"].Value.ToString()))
                    //{
                    if (strValidMast != "")
                    {
                        strValidMast += "," + row.Cells["code"].Value.ToString();
                    }
                    else
                    {
                        strValidMast = row.Cells["code"].Value.ToString();
                    }
                }
            }
            ObjBFD.HTMAIN[_validity_fld_nm] = strValidMast;
            this.Close();
        }

        private void frmListOfMenus_Load(object sender, EventArgs e)
        {
            dgvtranset.AutoGenerateColumns = false;
            DataSet ds = new DataSet();
            if (_type == "1")
            {
                if (_condition == "")
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where tran_type='Transaction' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
                else
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where " + _condition + " and tran_type='Transaction' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
            }
            else if (_type == "0")
            {
                if (_condition == "")
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where tran_type='Master' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('BM','EM','" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
                else
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where " + _condition + " and tran_type='Master' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('BM','EM','" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
            }
            else
            {
                if (_condition == "")
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where tran_type='CustomMaster' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('BM','EM','" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
                else
                {
                    ds = objDBAdaper.dsquery("select distinct sel='0',code,tran_nm from tran_set where " + _condition + " and tran_type='CustomMaster' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('BM','EM','" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                }
            }
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
                        if (column.Name == "code" && objBFD.HTMAIN[_validity_fld_nm].ToString().Split(',').Contains(row[column.Name].ToString()))
                        {
                            dgvtranset.Rows[i].Cells["Sel"].Value = true;
                        }
                    }
                }
                i++;
            }
            //this.BackColor = objBFD.ObjControlSet.Back_color != null ? Color.FromName(objBFD.ObjControlSet.Back_color) : Color.White; this.ForeColor = objBFD.ObjControlSet.Font_color != null ? Color.FromName(objBFD.ObjControlSet.Font_color) : Color.Black; ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            //this.Font = new Font(objBFD.ObjControlSet.Font_family != null ? objBFD.ObjControlSet.Font_family : "Courier New", float.Parse(objBFD.ObjControlSet.Font_size != null ? objBFD.ObjControlSet.Font_size : "9"));
            ucToolBar1.Width1 = this.Width; ucToolBar1.UCbackcolor = objBFD.ObjControlSet.Uc_color != null ? Color.FromName(objBFD.ObjControlSet.Uc_color) : Color.Maroon;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBFD, "CustomMaster");
            ucToolBar1.Titlebar = "List Of Transaction / Master";
        }

        private void frmListOfMenus_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
