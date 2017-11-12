using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;


namespace CUSTOM_iMANTRA
{
    public partial class frmListOfMenus : CustomBaseForm
    {
        BL_BASEFIELD objBFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBFD
        {
            get { return objBFD; }
            set { objBFD = value; }
        }
        private bool _type = true;
        private string _validity_fld_nm;

        public string Validity_fld_nm
        {
            get { return _validity_fld_nm; }
            set { _validity_fld_nm = value; }
        }

        public bool Type
        {
            get { return _type; }
            set { _type = value; }
        }

        dblayer objDBAdaper = new dblayer();

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
            string strValidMast = "";
            foreach (DataGridViewRow row in dgvtranset.Rows)
            {
                if (row.Cells["Sel"].Value != null && row.Cells["Sel"].Value.ToString() != "" && bool.Parse(row.Cells["Sel"].Value.ToString()))
                {
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
            if (_type)
            {
                ds = objDBAdaper.dsquery("select distinct sel=0,code,tran_nm from tran_set where tran_type='Transaction' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
            }
            else
            {
                ds = objDBAdaper.dsquery("select distinct sel=0,code,tran_nm from tran_set where tran_type='Master' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('BM','EM','" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
                //ds = objDBAdaper.dsquery("select distinct sel=0,code,tran_nm from tran_set where tran_type='Transaction' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and code not in ('" + objBFD.HTMAIN["code"].ToString() + "') order by tran_nm");
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
            AddThemesToTitleBar((Form)this, ucToolBar1, objBFD, "CustomMaster");
            ucToolBar1.Titlebar = "List Of Transaction / Master";
        }
    }
}
