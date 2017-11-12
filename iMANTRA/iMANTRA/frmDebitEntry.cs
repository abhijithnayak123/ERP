using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using iMANTRA_DL;

namespace iMANTRA
{
    public partial class frmDebitEntry : BaseClass
    {
        private string _tran_cd, _tran_id, _tran_mode;

        public string Tran_mode
        {
            get { return _tran_mode; }
            set { _tran_mode = value; }
        }

        public string Tran_id
        {
            get { return _tran_id; }
            set { _tran_id = value; }
        }

        public string Tran_cd
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }

        private Hashtable _hashstkvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        private DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

        public Hashtable Hashstkvalue
        {
            get { return _hashstkvalue; }
            set { _hashstkvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmDebitEntry()
        {
            InitializeComponent();
        }

        private void frmDebitEntry_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            //this.BackColor = objBLFD.ObjControlSet.Back_color != null ? Color.FromName(objBLFD.ObjControlSet.Back_color) : Color.White;
            //this.ForeColor = objBLFD.ObjControlSet.Font_color != null ? Color.FromName(objBLFD.ObjControlSet.Font_color) : Color.Black; 
            //ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
           ucToolBar1.Width1 = this.Width;
            //ucToolBar1.UCbackcolor = objBLFD.ObjControlSet.Uc_color != null ? Color.FromName(objBLFD.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(objBLFD.ObjControlSet.Font_family != null ? objBLFD.ObjControlSet.Font_family : "Courier New", float.Parse(objBLFD.ObjControlSet.Font_size != null ? objBLFD.ObjControlSet.Font_size : "9"));
            AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "Transaction");
            ucToolBar1.Titlebar = "Debit Entry";
            ViewTool();
        }

        private void dgvcreditdetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            decimal decexcise = 0, deccess = 0, decshcess = 0, decaddl = 0;
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {

                decexcise = dgvcreditdetails.CurrentRow.Cells[5].Value != null && dgvcreditdetails.CurrentRow.Cells[5].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells[5].Value.ToString()) : decimal.Parse("0.00");
                deccess = dgvcreditdetails.CurrentRow.Cells[6].Value != null && dgvcreditdetails.CurrentRow.Cells[6].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells[6].Value.ToString()) : decimal.Parse("0.00");
                decshcess = dgvcreditdetails.CurrentRow.Cells[7].Value != null && dgvcreditdetails.CurrentRow.Cells[7].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells[7].Value.ToString()) : decimal.Parse("0.00");
                decaddl = dgvcreditdetails.CurrentRow.Cells[8].Value != null && dgvcreditdetails.CurrentRow.Cells[8].Value.ToString() != "" ? decimal.Parse(dgvcreditdetails.CurrentRow.Cells[8].Value.ToString()) : decimal.Parse("0.00");

                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells[1].Value.ToString()) < decexcise)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " excise duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " excise duty","Debit");
                    e.Cancel = true;
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells[2].Value.ToString()) < deccess)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cess duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Cess duty", "Debit");
                    e.Cancel = true;
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells[3].Value.ToString()) < decshcess)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " S&H Cess duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " S&H Cess duty", "Debit");
                    e.Cancel = true;
                }
                if (decimal.Parse(dgvcreditdetails.CurrentRow.Cells[4].Value.ToString()) < decaddl)
                {
                    AutoClosingMessageBox.Show(dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Addl. duty should be greater than or equal Adjusting " + dgvcreditdetails.CurrentRow.Cells[0].Value.ToString() + " Addl. duty", "Debit");
                    e.Cancel = true;
                }

                dgvcreditdetails.CurrentRow.Cells[9].Value = decexcise + deccess + decshcess + decaddl;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDebitEntry_Enter(object sender, EventArgs e)
        {
            //if (((frmMiddleLayer)this.MdiParent).ActiveMdiChild != null)
            //{
            //    iInit.ActiveFrm = this;
            //    ((frmMiddleLayer)this.MdiParent).ChildWindowActivate();// refreshToolbar(this.tran_cd, this.tran_mode);
            //}
        }
        private void Get_Entry_Details()
        {
            Hashtable htparam = new Hashtable();
            htparam.Add("@asdate", dtpDutyFrom.DtValue);
            htparam.Add("@aedate", dtpDutyTo.DtValue);
            htparam.Add("@atran_id", objBLFD.Tran_id);
            htparam.Add("@atran_cd", objBLFD.Code);
            DataSet dsetDebit = objDL_ADAPTER.dsprocedure("ISP_GET_DEBIT_ENTRY_DETAILS", htparam);
            dgvcreditdetails.Rows.Clear();
            if (dsetDebit != null && dsetDebit.Tables.Count != 0 && dsetDebit.Tables[0].Rows.Count != 0)
            {
                int j = 0;
                for (int i = 1; i <= 5; i++) // (DataRow row in dsetDebit.Tables[0].Rows)
                {
                    if (i == 1)
                    {
                        if (dsetDebit.Tables[0].Rows[j]["grp"].ToString() == i.ToString())
                        {
                            txtsalesexduty.Text = dsetDebit.Tables[0].Rows[j]["ex_duty"].ToString();
                            txtsalescessduty.Text = dsetDebit.Tables[0].Rows[j]["cess_duty"].ToString();
                            txtsaleshcessduty.Text = dsetDebit.Tables[0].Rows[j]["shcess_duty"].ToString();
                            txtsalesaddlduty.Text = dsetDebit.Tables[0].Rows[j]["addl_duty"].ToString();
                        }
                    }
                    else
                    {
                        if (i > 1)
                        {
                            dgvcreditdetails.Rows.Add();
                            if (j == 0)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "RG23A";
                            }
                            else if (j == 1)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "RG23C";
                            }
                            else if (j == 2)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "ST";
                            }
                            else if (j == 3)
                            {
                                dgvcreditdetails.Rows[j].Cells[0].Value = "PLA";
                            }
                            DataRow[] row = dsetDebit.Tables[0].Select("grp='" + (i) + "'");
                            if (row != null && row.Length != 0)
                            {
                                dgvcreditdetails.Rows[j].Cells[1].Value = row[0]["ex_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells[2].Value = row[0]["cess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells[3].Value = row[0]["shcess_duty"].ToString();
                                dgvcreditdetails.Rows[j].Cells[4].Value = row[0]["addl_duty"].ToString();
                            }
                            else
                            {
                                dgvcreditdetails.Rows[j].Cells[1].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells[2].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells[3].Value = "0.00";
                                dgvcreditdetails.Rows[j].Cells[4].Value = "0.00";
                            }
                            j++;
                        }
                    }
                }
            }
        }

        public void addTool()
        {
            foreach (Control c in this.Controls)
            {
                if (c is DataGrid)
                {
                    c.Enabled = true;
                }
                if (c is DateTimePicker)
                {
                    c.Enabled = true;
                }
                if (c is TextBox)
                {
                    c.Enabled = true;
                }
            }          
            txtentryno.Text = string.Empty;
            dtpDate.DtValue = DateTime.Now;
            dtpDutyFrom.DtValue = DateTime.Parse("01-" + DateTime.Now.Month + "-" + DateTime.Now.Year);
            dtpDutyTo.DtValue = dtpDutyFrom.DtValue.AddMonths(1);
            Get_Entry_Details();
        }
        public void EditTool()
        {
            dgvcreditdetails.Enabled = true;
            txtentryno.Enabled = false;
            dtpDutyFrom.Enabled = true;
            dtpDutyTo.Enabled = true;
            txtsalesaddlduty.Enabled = true;
            txtsalescessduty.Enabled = true;
            txtsalesexduty.Enabled = true;
            txtsaleshcessduty.Enabled = true;
        }
        public void DeleteTool()
        {
            MessageBox.Show("delte");
        }
        public void ViewTool()
        {
            Get_Entry_Details();
            foreach (Control c in this.Controls)
            {
                if (c is DataGrid)
                {
                    if (!(c is Label)) c.Enabled = false;
                }
                if (c is DateTimePicker)
                {
                    if (!(c is Label)) c.Enabled = false;
                }
                if (c is TextBox)
                {
                    if (!(c is Label)) c.Enabled = false;
                }
            }
        }

        private void frmDebitEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (((frmMiddleLayer)this.MdiParent).ActiveMdiChild != null)
            //{
            //    iInit.ActiveFrm = this;
            //    if (this.Tran_cd == ((frmMiddleLayer)this.MdiParent).Tran_cd)
            //        ((frmMiddleLayer)this.MdiParent).CloseChildWindow(0);
            //}
        }

        private void dtpDutyFrom_ValueChanged(object sender, EventArgs e)
        {
            Get_Entry_Details();
        }

        private void dtpDutyTo_ValueChanged(object sender, EventArgs e)
        {
            Get_Entry_Details();
        }

        private void frmDebitEntry_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }

    }
}
