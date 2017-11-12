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
    public partial class frmListOfFields : BaseClass
    {
        BL_BASEFIELD objBFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBFD
        {
            get { return objBFD; }
            set { objBFD = value; }
        }
        private bool _type;
        private string _validity_fld_nm, _filter_code;

        public string Filter_code
        {
            get { return _filter_code; }
            set { _filter_code = value; }
        }

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

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        public frmListOfFields()
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
                if (row.Cells["Sel"].Value != null && row.Cells["Sel"].Value.ToString() != "")// && bool.Parse(row.Cells["Sel"].Value.ToString())
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
                        strValidMast += "," + row.Cells["fld_nm"].Value.ToString() + ";" + row.Cells["head_nm"].Value.ToString();
                    }
                    else
                    {
                        strValidMast = row.Cells["fld_nm"].Value.ToString() + ";" + row.Cells["head_nm"].Value.ToString();
                    }
                }
            }
            ObjBFD.HTMAIN[_validity_fld_nm] = strValidMast;
            this.Close();
        }

        private void frmListOfFields_Load(object sender, EventArgs e)
        {
            dgvtranset.AutoGenerateColumns = false;
            DataSet ds = new DataSet();
            ds = objDBAdaper.dsquery("select distinct Sel=0,code,fld_nm,head_nm from ibasefields where code='" + Filter_code + "' and compid='" + objBFD.ObjCompany.Compid.ToString() + "' and data_ty!='button' and typewise=1");

            dgvtranset.DataSource = ds.Tables[0];
            dgvtranset.Update();
            int i = 0;
            string[] strArray;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataGridViewColumn column in dgvtranset.Columns)
                {
                    if (ds.Tables[0].Columns.Contains(column.Name) && column.Name != "Sel")
                    {
                        dgvtranset.Rows[i].Cells[column.Name].Value = row[column.Name];
                        strArray = objBFD.HTMAIN[_validity_fld_nm].ToString().Split(',');
                        foreach (string str in strArray)
                        {
                            if (str.Split(';')[0].Contains(row[column.Name].ToString()))
                            {
                                dgvtranset.Rows[i].Cells["Sel"].Value = true;
                                break;
                            }
                        }
                    }
                }
                i++;
            }
            //this.BackColor = objBFD.ObjControlSet.Back_color != null ? Color.FromName(objBFD.ObjControlSet.Back_color) : Color.White; 
            //this.ForeColor = objBFD.ObjControlSet.Font_color != null ? Color.FromName(objBFD.ObjControlSet.Font_color) : Color.Black;
            //this.Font = new Font(objBFD.ObjControlSet.Font_family != null ? objBFD.ObjControlSet.Font_family : "Courier New", float.Parse(objBFD.ObjControlSet.Font_size != null ? objBFD.ObjControlSet.Font_size : "9"));
            //ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
           ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = objBFD.ObjControlSet.Uc_color != null ? Color.FromName(objBFD.ObjControlSet.Uc_color) : Color.Maroon;           
            AddThemesToTitleBar((Form)this, ucToolBar1, objBFD, "CustomMaster");
            ucToolBar1.Titlebar = "List Of Fields";
        }

        private void frmListOfFields_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
