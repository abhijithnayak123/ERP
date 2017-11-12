using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using iMANTRA_IL;

namespace iMANTRA
{
    public partial class frmWO_List : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        FL_GENERAL objFLGeneral = new FL_GENERAL();

        public frmWO_List(BL_BASEFIELD objBL)
        {
            InitializeComponent();
            objBASEFILEDS.HTMAIN["TRAN_CD"] = objBL.Code;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmWO_List_Load(object sender, EventArgs e)
        {
            DataSet dsetWOList = objFLGeneral.GetWorkOrderList(objBASEFILEDS.ObjCompany.Compid.ToString());
            dgvWOList.AutoGenerateColumns = false;
            if (dsetWOList != null && dsetWOList.Tables.Count != 0 && dsetWOList.Tables[0].Rows.Count != 0)
            {
                dgvWOList.DataSource = dsetWOList.Tables[0];
                dgvWOList.Update();
                int i = 0;
                foreach (DataRow row in dsetWOList.Tables[0].Rows)
                {
                    foreach (DataGridViewColumn column in dgvWOList.Columns)
                    {
                        if (dsetWOList.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvWOList.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                //  lblRowsCount.Text = "Total Records : " + dgvSearch.Rows.Count;
            }
            else
            {
                // lblRowsCount.Text = "Total Records : 0";
                CleardgvProcess(dgvWOList);
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
        }

        private void CleardgvProcess(DataGridView dgv)
        {
            if (dgv != null && dgv.Rows.Count != 0)
            {
                while (dgv.Rows.Count > 0)
                {
                    if (!dgv.Rows[0].IsNewRow)
                    {
                        dgv.Rows.RemoveAt(0);
                    }
                }
            }
        }

        private void dgvWOList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            objBASEFILEDS.HTMAIN["wo_id"] = dgvWOList.CurrentRow.Cells["tran_id"].Value;
            objBASEFILEDS.HTMAIN["wo_no"] = dgvWOList.CurrentRow.Cells["tran_no"].Value;
            objBASEFILEDS.HTMAIN["wo_cd"] = dgvWOList.CurrentRow.Cells["tran_cd"].Value;
            objBASEFILEDS.HTMAIN["wo_ptserial"] = dgvWOList.CurrentRow.Cells["ptserial"].Value;
            objBASEFILEDS.HTMAIN["prod_cd"] = dgvWOList.CurrentRow.Cells["prod_cd"].Value;
            objBASEFILEDS.HTMAIN["prod_nm"] = dgvWOList.CurrentRow.Cells["prod_nm"].Value;
            objBASEFILEDS.HTMAIN["wo_qty"] = dgvWOList.CurrentRow.Cells["qty"].Value;
            this.Close();
        }
    }
}
