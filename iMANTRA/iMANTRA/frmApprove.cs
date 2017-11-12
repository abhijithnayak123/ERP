using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using iMANTRA_DL;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using System.Reflection;
using CUSTOM_iMANTRA_BL;
using iMANTRA_IL;

namespace iMANTRA
{
    public partial class frmApprove : BaseClass
    {
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        BL_BASEFIELD objBSFD = new BL_BASEFIELD();
        private BL_MAIN_FIELDS _objBLMainFields = new BL_MAIN_FIELDS();
        public BL_MAIN_FIELDS ObjBLMainFields
        {
            get { return _objBLMainFields; }
            set { _objBLMainFields = value; }
        }
        BLHT objHashtable = new BLHT();

        FL_APPROVE objFLApprove = new FL_APPROVE();
        FL_Roles objRoles = new FL_Roles();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        FL_GRIDEVENTS objFLExpression = new FL_GRIDEVENTS();

        DL_ADAPTER objiMANTRA_DL = new DL_ADAPTER();

        DataSet dsetlevels = new DataSet();
        DataSet dsetTransData = new DataSet();
        DataSet dsetApproveLevelsCount = new DataSet();
        DataSet dsettblApprove = new DataSet();


        public string Tran_id
        {
            get { return tran_id; }
            set { tran_id = value; }
        }

        public string Tran_cd
        {
            get { return tran_cd; }
            set { tran_cd = value; }
        }

        public string Tran_mode
        {
            get { return tran_mode; }
            set { tran_mode = value; }
        }

        public frmApprove(BL_BASEFIELD objBL)
        {
            InitializeComponent();
            objBSFD.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBSFD.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBSFD.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBSFD = objBL;
        }

        private void frmApprove_Load(object sender, EventArgs e)
        {
            //  this.Dock = DockStyle.Fill;
            //dgvApprove.Width = this.ClientSize.Width * 98 / 100;
            //dgvApprove.Height = this.ClientSize.Height * 85 / 100;
            dgvApprove.AutoGenerateColumns = false;
            objHashtable = new BLHT();
            objHashtable.HashMaintbl.Clear();

            //this.BackColor = objBSFD.ObjControlSet.Back_color != null ? Color.FromName(objBSFD.ObjControlSet.Back_color) : Color.White;
            //this.ForeColor = objBSFD.ObjControlSet.Font_color != null ? Color.FromName(objBSFD.ObjControlSet.Font_color) : Color.Black;
            //this.ucToolBar1.UCbackcolor =  objBSFD.ObjControlSet.Uc_color != null ? Color.FromName(objBSFD.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(objBSFD.ObjControlSet.Font_family != null ? objBSFD.ObjControlSet.Font_family : "Courier New", float.Parse(objBSFD.ObjControlSet.Font_size != null ? objBSFD.ObjControlSet.Font_size : "9"));
            //this.ucToolBar1.Titlebar = objBSFD.Tran_nm;           

            //DataSet dsetApprove = objiMANTRA_DL.dsquery("select distinct 'AL' code,'ALL' tran_nm from tran_set union all select code,tran_nm from tran_set where isApprove=1 and tran_type='Transaction' and compid=" + objBSFD.ObjCompany.Compid);
            DataSet dsetApprove = objiMANTRA_DL.dsquery("select tran_set.code,tran_nm from tran_set inner join levels on tran_set.code=levels.code where isApprove=1 and tran_type='Transaction' and user_nm='" + objBSFD.ObjLoginUser.CurUser + "' and tran_set.compid=" + objBSFD.ObjCompany.Compid);
            if (dsetApprove != null && dsetApprove.Tables.Count != 0 && dsetApprove.Tables[0].Rows.Count != 0)
            {
                cmbtran_nm.DataSource = dsetApprove.Tables[0];
                cmbtran_nm.DisplayMember = "tran_nm";
                cmbtran_nm.ValueMember = "code";
                cmbtran_nm.Update();
            }

            DataSet dsetStatus = objiMANTRA_DL.dsquery("SELECT appr_status_id,appr_status_nm FROM dbo.APPROVE_STATUS where compid=" + objBSFD.ObjCompany.Compid);
            if (dsetStatus != null && dsetStatus.Tables.Count != 0 && dsetStatus.Tables[0].Rows.Count != 0)
            {
                cmbstatus.DataSource = dsetStatus.Tables[0];
                cmbstatus.DisplayMember = "appr_status_nm";
                cmbstatus.ValueMember = "appr_status_id";
                cmbstatus.Update();
            }
            if (!(objHashtable != null && objHashtable.HashMaintbl != null && objHashtable.HashMaintbl.Count != 0))
            {
                objHashtable = new BLHT();
            }

            DisplayControlsonMode(tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBSFD, "CustomMaster");
            //this.ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            //this.ucToolBar1.Width1 = this.Width;           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            objBSFD.HTITEM.Clear();
            string strTran_no = "";
            int i = 0;
            bool flg = true;
            dgvApprove.DataSource = null;
            dgvApprove.Update();
            dgvApprove.Columns.Clear();

            DataSet dsetTable = objiMANTRA_DL.dsquery("select code,Main_tbl_nm,Approve_tbl_nm from tran_set where isApprove=1 and tran_type='Transaction' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid);
            if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
            {
                string strQuery = "";
                string strApproveQuery = "";
                //dsetlevels = objiMANTRA_DL.dsquery("select level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where user_nm='" + objBSFD.ObjLoginUser.CurUser + "' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid);
                dsetlevels = objiMANTRA_DL.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid + " order by cast(si_no as int)");
                if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
                {
                    if (bool.Parse(dsetlevels.Tables[0].Rows[0]["main_cond_req"].ToString()))
                    {
                        objFLExpression.objBASEFILEDS = objBSFD;
                        foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                        {
                            if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString())
                            {
                                strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved!=1 and tran_cd='" + levelsrow["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and (" + levelsrow["condition"].ToString() + ")";
                            }
                        }
                        //strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                    }
                    else
                    {
                        foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                        {
                            strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved!=1 and tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid;
                            dsetTransData = objiMANTRA_DL.dsquery(strQuery);

                            if (dsetTransData != null && dsetTransData.Tables.Count != 0 && dsetTransData.Tables[0].Rows.Count != 0)
                            {
                                foreach (DataRow row in dsetTransData.Tables[0].Rows)
                                {
                                    dsettblApprove = objiMANTRA_DL.dsquery("select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + cmbtran_nm.SelectedValue + "' and tran_id='" + row["tran_id"].ToString() + "'");
                                    if (dsettblApprove != null && dsettblApprove.Tables.Count != 0 && dsettblApprove.Tables[0].Rows.Count != 0)
                                    {
                                        foreach (DataRow row1 in dsettblApprove.Tables[0].Rows)
                                        {
                                            if (row["tran_id"].ToString() == row1["tran_id"].ToString())
                                            {
                                                if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString() && row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() != "APPROVE")
                                                {
                                                    i = int.Parse(levelsrow["si_no"].ToString());
                                                    if (int.Parse(levelsrow["si_no"].ToString()) != int.Parse(levelsrow["level_cnt"].ToString()))
                                                    {
                                                        i++;
                                                        while (i <= int.Parse(levelsrow["level_cnt"].ToString()))
                                                        {
                                                            if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                            {
                                                                flg = false;
                                                                break;
                                                            }
                                                            i++;
                                                        }
                                                    }
                                                    if (flg)
                                                    {
                                                        if (strTran_no == "")
                                                        {
                                                            strTran_no = "'" + row["tran_id"].ToString() + "'";
                                                        }
                                                        else
                                                        {
                                                            strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString())
                                        {
                                            i = int.Parse(levelsrow["si_no"].ToString());
                                            if (int.Parse(levelsrow["si_no"].ToString()) == int.Parse(levelsrow["level_cnt"].ToString()))
                                            {
                                                if (strTran_no == "")
                                                {
                                                    strTran_no = "'" + row["tran_id"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (strTran_no == "")
                        {
                            strTran_no = "'0'";
                        }
                        strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                        strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                    }
                }
                DataSet dsetApproveData = objiMANTRA_DL.dsquery(strQuery);
                DataSet dsetApproveTblData = new DataSet();
                if (strApproveQuery != "")
                {
                    dsetApproveTblData = objiMANTRA_DL.dsquery(strApproveQuery);
                }
                dsetApproveLevelsCount = objFLApprove.GetApproveLevelsCount(cmbtran_nm.SelectedValue.ToString(), objBSFD.ObjCompany.Compid.ToString());
                if (dsetApproveLevelsCount != null && dsetApproveLevelsCount.Tables.Count != 0 && dsetApproveLevelsCount.Tables[0].Rows.Count != 0)
                {
                    Bind_Grid_Form_Controls("SI. NO.", "itserial", false, true, "");
                    Bind_Grid_Form_Controls("Transaction Id", "tran_id", true, true, "");
                    Bind_Grid_Form_Controls("Transaction No.", "tran_no", false, true, "");
                    Bind_Grid_Form_Controls("Transaction Date", "tran_dt", false, true, "");
                    Bind_Grid_Form_Controls("Transaction Code", "tran_cd", false, true, "");
                    // Bind_Grid_Form_Controls("Transaction Name", "tran_nm", false, true, "");
                    Bind_Grid_Form_Controls("Customer Name", "ac_nm", false, true, "");
                    i = 0; bool _flgread = false;
                    foreach (DataRow row in dsetApproveLevelsCount.Tables[0].Rows)
                    {
                        i++;
                        if (objBSFD.ObjLoginUser.CurUser.ToString().ToLower() == row["user_nm"].ToString().ToLower())
                        {
                            _flgread = false;
                        }
                        else
                        {
                            _flgread = true;
                        }
                        Bind_Grid_Form_Controls(row["level_nm"].ToString(), "level" + i + "_status", false, _flgread, "keydown");
                        Bind_Grid_Form_Controls(row["level_nm"].ToString() + "_id", "level" + i + "_status_id", true, _flgread, "");
                        Bind_Grid_Form_Controls("User", "level" + i + "_app_by", true, _flgread, "");
                        Bind_Grid_Form_Controls("Status Date", "level" + i + "_app_dt", true, _flgread, "");
                        Bind_Grid_Form_Controls("Remarks Of " + row["level_nm"].ToString(), "level" + i + "_remarks", false, _flgread, "");
                    }

                    dgvApprove.DataSource = dsetApproveData.Tables[0];
                    dgvApprove.Update();
                    int itserial = 0;
                    foreach (DataRow row in dsetApproveData.Tables[0].Rows)
                    {
                        objBSFD.HTITEM[itserial + 1] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

                        foreach (DataGridViewColumn column in dgvApprove.Columns)
                        {
                            if (dsetApproveData.Tables[0].Columns.Contains(column.Name))
                            {
                                dgvApprove.Rows[itserial].Cells[column.Name].Value = row[column.Name];
                                ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = row[column.Name].ToString();
                            }
                            else if (column.Name == "itserial")
                            {
                                dgvApprove.Rows[itserial].Cells[column.Name].Value = itserial + 1;
                                ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = itserial + 1;
                            }
                            else
                            {
                                if (dsetApproveTblData != null && dsetApproveTblData.Tables.Count != 0 && dsetApproveTblData.Tables[0].Rows.Count != 0)
                                {
                                    foreach (DataRow rowApproveTbl in dsetApproveTblData.Tables[0].Rows)
                                    {
                                        if (row["tran_id"].ToString() == rowApproveTbl["tran_id"].ToString() && row["tran_cd"].ToString() == rowApproveTbl["tran_cd"].ToString())
                                        {
                                            dgvApprove.Rows[itserial].Cells[column.Name].Value = rowApproveTbl[column.Name];
                                            ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = rowApproveTbl[column.Name];
                                        }
                                    }
                                }
                                else
                                {
                                    ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = "";
                                }
                            }
                        }
                        itserial++;
                    }
                    if (!(objHashtable != null && objHashtable.HashMaintbl != null && objHashtable.HashMaintbl.Count != 0))
                    {
                        objHashtable = new BLHT();
                    }
                    foreach (DataGridViewRow row in dgvApprove.Rows)
                    {
                        if (objHashtable != null && objHashtable.HashMaintbl != null)
                        {
                            foreach (DictionaryEntry entry in objHashtable.HashMaintbl)
                            {
                                if (row.Cells["tran_id"].Value.ToString() + "," + row.Cells["tran_cd"].Value.ToString() == entry.Key.ToString())
                                {
                                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                                    {
                                        row.Cells[entry1.Key.ToString()].Value = entry1.Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cmbstatus.SelectedIndex != -1)
            {
                cmbstatus.SelectedIndex = 0;
            }
        }
        private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbstatus.SelectedValue != null && cmbstatus.SelectedValue.ToString() == "2")
            {
                Bind_Data_toGrid(1);
            }
            else
            {
                Bind_Data_toGrid(0);
            }
        }
        private void ClearGrid(DataGridView dgv)
        {
            if (dgv != null && dgv.Rows.Count != 0)
            {
                //dgvOrder.Rows.Clear();
                while (dgv.Rows.Count > 0)
                {
                    if (!dgv.Rows[0].IsNewRow)
                    {
                        dgv.Rows.RemoveAt(0);
                    }
                }
            }
        }

        private void Bind_Data_toGrid(int _status)
        {
            objBSFD.HTITEM.Clear();
            string strTran_no = "";
            int i = 0;
            bool flg = true;

            ClearGrid(dgvApprove);
            //dgvApprove.DataSource = null;
            //dgvApprove.Update();
            //dgvApprove.Columns.Clear();
            // dgvApprove.Rows.Clear();

            DataSet dsetTable = objiMANTRA_DL.dsquery("select code,Main_tbl_nm,Approve_tbl_nm from tran_set where isApprove=1 and tran_type='Transaction' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid);
            if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
            {
                string strQuery = "";
                string strApproveQuery = "";
                //dsetlevels = objiMANTRA_DL.dsquery("select level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where user_nm='" + objBSFD.ObjLoginUser.CurUser + "' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid);
                dsetlevels = objiMANTRA_DL.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid + " order by cast(si_no as int)");
                if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
                {
                    if (bool.Parse(dsetlevels.Tables[0].Rows[0]["main_cond_req"].ToString()))
                    {
                        objFLExpression.objBASEFILEDS = objBSFD;
                        foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                        {
                            if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString())
                            {
                                strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved=" + _status + " and tran_cd='" + levelsrow["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and (" + levelsrow["condition"].ToString() + ")";
                            }
                        }
                        //strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                    }
                    else
                    {
                        foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
                        {
                            // strQuery = "select tran_id,tran_no,tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved=" + _status + " and tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid;
                            strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid;
                            dsetTransData = objiMANTRA_DL.dsquery(strQuery);

                            if (dsetTransData != null && dsetTransData.Tables.Count != 0 && dsetTransData.Tables[0].Rows.Count != 0)
                            {
                                foreach (DataRow row in dsetTransData.Tables[0].Rows)
                                {
                                    flg = true;
                                    dsettblApprove = objiMANTRA_DL.dsquery("select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + cmbtran_nm.SelectedValue + "' and tran_id='" + row["tran_id"].ToString() + "'");
                                    if (dsettblApprove != null && dsettblApprove.Tables.Count != 0 && dsettblApprove.Tables[0].Rows.Count != 0)
                                    {
                                        foreach (DataRow row1 in dsettblApprove.Tables[0].Rows)
                                        {
                                            if (row["tran_id"].ToString() == row1["tran_id"].ToString())
                                            {
                                                //if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString() && row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() != "APPROVE")
                                                if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString())
                                                {
                                                    if (_status == 1)
                                                    {
                                                        if (row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() == "APPROVE")
                                                        {
                                                            #region
                                                            i = int.Parse(levelsrow["si_no"].ToString());
                                                            if (int.Parse(levelsrow["si_no"].ToString()) != int.Parse(levelsrow["level_cnt"].ToString()))
                                                            {
                                                                i--;
                                                                while (i > 0 && i <= int.Parse(levelsrow["level_cnt"].ToString()))
                                                                {
                                                                    // if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                                    if (row1["level" + i + "_status"].ToString() == "APPROVE")
                                                                    {
                                                                        flg = false;
                                                                        break;
                                                                    }
                                                                    i--;
                                                                }
                                                            }
                                                            if (int.Parse(levelsrow["si_no"].ToString()) == int.Parse(levelsrow["level_cnt"].ToString()))
                                                            {
                                                                i--;
                                                                while (i > 0 && i <= int.Parse(levelsrow["level_cnt"].ToString()))
                                                                {
                                                                    // if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                                    if (row1["level" + i + "_status"].ToString() == "APPROVE")
                                                                    {
                                                                        flg = false;
                                                                        break;
                                                                    }
                                                                    i--;
                                                                }
                                                            }
                                                            #endregion
                                                            if (flg)
                                                            {
                                                                if (strTran_no == "")
                                                                {
                                                                    strTran_no = "'" + row["tran_id"].ToString() + "'";
                                                                }
                                                                else
                                                                {
                                                                    strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (row1["level" + levelsrow["si_no"].ToString() + "_status"].ToString() != "APPROVE")
                                                        {
                                                            i = int.Parse(levelsrow["si_no"].ToString());
                                                            if (int.Parse(levelsrow["si_no"].ToString()) != int.Parse(levelsrow["level_cnt"].ToString()))
                                                            {
                                                                i++;
                                                                while (i <= int.Parse(levelsrow["level_cnt"].ToString()))
                                                                {
                                                                    if (row1["level" + i + "_status"].ToString() != "APPROVE")
                                                                    {
                                                                        flg = false;
                                                                        break;
                                                                    }
                                                                    i++;
                                                                }
                                                            }
                                                            if (flg)
                                                            {
                                                                if (strTran_no == "")
                                                                {
                                                                    strTran_no = "'" + row["tran_id"].ToString() + "'";
                                                                }
                                                                else
                                                                {
                                                                    strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (objBSFD.ObjLoginUser.CurUser.ToLower() == levelsrow["user_nm"].ToString())
                                        {
                                            i = int.Parse(levelsrow["si_no"].ToString());
                                            if (int.Parse(levelsrow["si_no"].ToString()) == int.Parse(levelsrow["level_cnt"].ToString()))
                                            {
                                                if (strTran_no == "")
                                                {
                                                    strTran_no = "'" + row["tran_id"].ToString() + "'";
                                                }
                                                else
                                                {
                                                    strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (strTran_no == "")
                        {
                            strTran_no = "'0'";
                        }
                        strQuery = "select tran_id,tran_no,replace(convert(varchar(12),tran_dt,106),' ','-') tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                        strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                    }

                    DataSet dsetApproveData = objiMANTRA_DL.dsquery(strQuery);
                    if (dsetApproveData != null && dsetApproveData.Tables.Count != 0 && dsetApproveData.Tables[0].Rows.Count != 0)
                    {
                        if (strApproveQuery == "" || strApproveQuery == "0")
                        {
                            strTran_no = "0";
                            foreach (DataRow row in dsetApproveData.Tables[0].Rows)
                            {
                                if (strTran_no == "")
                                {
                                    strTran_no = "'" + row["tran_id"].ToString() + "'";
                                }
                                else
                                {
                                    strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
                                }
                            }
                            strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBSFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
                        }
                        //}

                        DataSet dsetApproveTblData = new DataSet();
                        if (strApproveQuery != "")
                        {
                            dsetApproveTblData = objiMANTRA_DL.dsquery(strApproveQuery);
                        }
                        //if (dsetApproveData != null && dsetApproveData.Tables.Count != 0 && dsetApproveData.Tables[0].Rows.Count != 0)
                        //{
                        dgvApprove.DataSource = dsetApproveData.Tables[0];
                        dgvApprove.Update();

                        int itserial = 0;
                        foreach (DataRow row in dsetApproveData.Tables[0].Rows)
                        {
                            objBSFD.HTITEM[itserial + 1] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

                            foreach (DataGridViewColumn column in dgvApprove.Columns)
                            {
                                if (dsetApproveData.Tables[0].Columns.Contains(column.Name))
                                {
                                    dgvApprove.Rows[itserial].Cells[column.Name].Value = row[column.Name];
                                    ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = row[column.Name].ToString();
                                }
                                else if (column.Name == "itserial")
                                {
                                    dgvApprove.Rows[itserial].Cells[column.Name].Value = itserial + 1;
                                    ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = itserial + 1;
                                }
                                else
                                {
                                    if (dsetApproveTblData != null && dsetApproveTblData.Tables.Count != 0 && dsetApproveTblData.Tables[0].Rows.Count != 0)
                                    {
                                        foreach (DataRow rowApproveTbl in dsetApproveTblData.Tables[0].Rows)
                                        {
                                            if (row["tran_id"].ToString() == rowApproveTbl["tran_id"].ToString() && row["tran_cd"].ToString() == rowApproveTbl["tran_cd"].ToString())
                                            {
                                                dgvApprove.Rows[itserial].Cells[column.Name].Value = rowApproveTbl[column.Name];
                                                ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = rowApproveTbl[column.Name];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = "";
                                    }
                                }
                            }
                            itserial++;
                        }
                    }
                    if (!(objHashtable != null && objHashtable.HashMaintbl != null && objHashtable.HashMaintbl.Count != 0))
                    {
                        objHashtable = new BLHT();
                    }
                    foreach (DataGridViewRow row in dgvApprove.Rows)
                    {
                        if (objHashtable != null && objHashtable.HashMaintbl != null)
                        {
                            foreach (DictionaryEntry entry in objHashtable.HashMaintbl)
                            {
                                if (row.Cells["tran_id"].Value.ToString() + "," + row.Cells["tran_cd"].Value.ToString() == entry.Key.ToString())
                                {
                                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                                    {
                                        row.Cells[entry1.Key.ToString()].Value = entry1.Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Bind_Grid_Form_Controls(string strHeader, string strName, bool _internal, bool _read, string _type)
        {
            DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
            txtcol.HeaderText = strHeader;
            txtcol.Name = strName;
            if (_type == "")
            {
                txtcol.Tag = "";
            }
            else txtcol.Tag = "key";
            dgvApprove.Columns.Add(txtcol);
            dgvApprove.Columns[strName].Visible = (!_internal);
            dgvApprove.Columns[strName].ReadOnly = _read;
            if (_read && _type != "")
            {
                dgvApprove.Columns[strName].DefaultCellStyle.BackColor = Color.LightGray;
            }
            dgvApprove.Columns[strName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvApprove_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                DataGridView gridview = (DataGridView)sender;
                if (gridview != null)
                {
                    TextBox txt = e.Control as TextBox;
                    if (txt != null)
                    {
                        if (gridview.CurrentCell.OwningColumn.Tag.ToString().Trim() != "")
                        {
                            txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                            txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                            txt.KeyDown -= new KeyEventHandler(txt_key_down);
                            txt.KeyDown += new KeyEventHandler(txt_key_down);
                        }
                        else
                        {
                            txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                            txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2 && txt.Tag.ToString().Trim() != "")
                {
                    frmPopup objfrmPopup = new frmPopup(((Hashtable)objBSFD.HTITEM[(dgvApprove.CurrentRow.Cells["itserial"].Value)]), "APPROVE_STATUS", "", "appr_status_id;" + txt.Name + "_id" + ",appr_status_nm;" + txt.Name, "appr_status_nm;Status", "Please select", "", false, "");
                    //objfrmPopup.objCompany = objBSFD.ObjCompany;
                    //objfrmPopup.objControlSet = objBSFD.ObjControlSet;
                    objfrmPopup.ObjBFD = objBSFD;
                    objfrmPopup.ShowDialog();
                    txt.Text = ((Hashtable)objBSFD.HTITEM[(dgvApprove.CurrentRow.Cells["itserial"].Value)])[txt.Name].ToString().Trim();
                    if (txt.Text != "PENDING")
                    {
                        ((Hashtable)objBSFD.HTITEM[(dgvApprove.CurrentRow.Cells["itserial"].Value)])[txt.Name.Split('_')[0] + "_app_by"] = objBSFD.ObjLoginUser.CurUser;
                        ((Hashtable)objBSFD.HTITEM[(dgvApprove.CurrentRow.Cells["itserial"].Value)])[txt.Name.Split('_')[0] + "_app_dt"] = DateTime.Now.ToString("yyyy/MM/dd");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvApprove_DoubleClick(object sender, EventArgs e)
        {
            MyDataGridView grd = (MyDataGridView)sender;
            if (grd != null && grd.CurrentRow.Index > 0)
            {
                OpenTransaction();
            }
        }

        private void BindApproveGridBasedonUser()
        {

        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBSFD.Tran_mode = tran_mode;
                objBSFD.HTMAIN.Clear();
                switch (tran_mode)
                {
                    case "edit_mode":
                        foreach (Control con in this.Controls)//form controls
                        {
                            con.Enabled = true;
                        }
                        break;
                    case "view_mode":
                        foreach (Control con in this.Controls)//form controls
                        {
                            if (con is UCToolBar)
                            {
                            }
                            else
                            {
                                con.Enabled = false;
                            }
                        }
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public override bool SendMessageToClient(BL_BASEFIELD objBLFD, string msg)
        {
            objBSFD = objBLFD;
            if (msg != "SAVE")
            {
                DisplayControlsonMode(objBLFD.Tran_mode);
            }
            else
            {
                return Click_Save();
            }
            return true;
        }
        private bool Click_Save()
        {
            bool flg = true;
            objBSFD.HTMAIN["Tran_type"] = "Transaction";
            bool flgAddEntry = true;
            DialogResult result = MessageBox.Show("Are you sure to want Proceed?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                if (objHashtable != null && objHashtable.HashMaintbl != null)
                {
                    //int itserial = 0;
                    //foreach (DataGridViewRow row in dgvApprove.Rows)
                    //{
                    //    if (objBSFD.HTITEM.Contains(row.Cells["itserial"].Value.ToString()))
                    //    {
                    //        foreach (DataGridViewColumn column in dgvApprove.Columns)
                    //        {
                    //            if (((Hashtable)objBSFD.HTITEM[itserial + 1]).Contains(column.Name))
                    //            {
                    //                ((Hashtable)objBSFD.HTITEM[itserial + 1])[column.Name] = row.Cells[column.Name].Value;
                    //            }
                    //        }
                    //    }
                    //    itserial++;
                    //}
                    //dsetlevels = objiMANTRA_DL.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.compid,levels.fin_yr from levels where user_nm='"+objBSFD.ObjLoginUser.CurUser+"' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBSFD.ObjCompany.Compid);
                    //if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
                    //{
                    foreach (DictionaryEntry entry in objBSFD.HTITEM)
                    {
                        if (!objHashtable.HashMaintbl.Contains(((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()))
                        {
                            objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            flgAddEntry = true;
                            if (!(entry1.Key.ToString() == "tran_id" || entry1.Key.ToString() == "tran_cd" || entry1.Key.ToString() == "tran_dt" || entry1.Key.ToString() == "tran_no" || entry1.Key.ToString() == "ac_nm" || entry1.Key.ToString() == "itserial"))
                            {
                                flgAddEntry = !dgvApprove.Columns[entry1.Key.ToString()].ReadOnly;
                            }
                            if (flgAddEntry)
                            {
                                if (entry1.Key.ToString() == entry1.Key.ToString().Split('_')[0] + "_app_by")
                                {
                                    if (entry1.Value != null && entry1.Value.ToString() != "")
                                    {
                                        ((Hashtable)(objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()]))[entry1.Key] = objBSFD.ObjLoginUser.CurUser;
                                    }
                                    else
                                    {
                                        ((Hashtable)(objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()]))[entry1.Key] = "";
                                    }
                                }
                                else if (entry1.Key.ToString() == entry1.Key.ToString().Split('_')[0] + "_app_dt")
                                {
                                    if (entry1.Value != null && entry1.Value.ToString() != "")
                                    {
                                        ((Hashtable)(objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()]))[entry1.Key] = DateTime.Now.ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        ((Hashtable)(objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()]))[entry1.Key] = "1900/01/01";
                                    }
                                }
                                else
                                {
                                    ((Hashtable)(objHashtable.HashMaintbl[((Hashtable)entry.Value)["tran_id"].ToString() + "," + ((Hashtable)entry.Value)["tran_cd"].ToString()]))[entry1.Key] = entry1.Value;
                                }
                            }
                        }
                    }
                    //}
                }
                else
                {
                    objHashtable = new BLHT();
                }
                objBSFD.HASHTABLES = objHashtable;
            }
            return flg;
        }

        private void frmApprove_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                objBSFD.Tran_mode_type = "edit_only";
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBSFD);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmApprove_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBSFD);
            }
        }

        private void OpenTransaction()
        {
            #region
            //try
            //{
            //    // objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);
            //    BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
            //    objBASEFILEDS.ObjCompany = objBSFD.ObjCompany;
            //    if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            //    {
            //        if (((frm_mainmenu)this.MdiParent).BindTransactionSetting(objBASEFILEDS, cmbtran_nm.SelectedValue.ToString()))// efreshToolbar(this.tran_cd, this.tran_mode);)
            //        {
            //            objRoles.ObjMainFields = ((frm_mainmenu)this.MdiParent).ObjBLMainFields;

            //            if (objRoles.CheckPermisson("view," + objBASEFILEDS.Tran_nm))
            //            {
            //                objBASEFILEDS.Tran_mode = "view_mode";
            //                objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();
            //                objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();

            //                ((frm_mainmenu)this.MdiParent).ActiveBLBF = objBASEFILEDS;
            //                ((frm_mainmenu)this.MdiParent).ActiveBLBF.ObjCompany = objBSFD.ObjCompany;
            //                objBASEFILEDS.ObjLoginUser = ((frm_mainmenu)this.MdiParent).ObjBLMainFields;
            //                ifrm_transaction objtransaction = new ifrm_transaction(objBASEFILEDS);
            //                objtransaction.Tran_cd = cmbtran_nm.SelectedValue.ToString();

            //                objtransaction.Tran_id = dgvApprove.CurrentRow.Cells["tran_id"].Value.ToString();
            //                objBASEFILEDS.Tran_id = objtransaction.Tran_id;
            //                ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_id = objtransaction.Tran_id;
            //                ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_mode_type = "approve_only";
            //                objBASEFILEDS.Tran_mode_type = "approve_only";
            //                objtransaction.Tran_mode = "view_mode";
            //                objtransaction.Name = objBASEFILEDS.Tran_nm;
            //                objtransaction.Text = objBASEFILEDS.Tran_nm;
            //                objtransaction.MdiParent = this.ParentForm;
            //                objtransaction.MinimizeBox = false;
            //                objtransaction.Show();
            //                objtransaction.Activate();
            //            }
            //            else
            //            {
            //                AutoClosingMessageBox.Show("Access Denied", "Access Rights", 3000);
            //            }
            //        }
            //        else
            //        {
            //            AutoClosingMessageBox.Show("Transaction is not exist", "Invalid Transaction", 3000);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{

            //}
            #endregion
            try
            {
                // objIni.SetKeyFieldValue("SQL", "initial catalog", objBLComp.Db_nm);//setting database
                BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD(); objBASEFILEDS.ObjCompany = objBSFD.ObjCompany;
                if (((frm_mainmenu)this.MdiParent).BindTransactionSetting(objBASEFILEDS, cmbtran_nm.SelectedValue.ToString()))//assign all properties of transcation from transaction setting to business layer
                {
                    objRoles.ObjMainFields = objBSFD.ObjLoginUser;//logged users details
                    if (objRoles.CheckPermisson("view," + objBASEFILEDS.Tran_nm))//check authority/permission given to this transaction or not.
                    {
                        DataSet dsetfrm = objRoles.getFormName(objBASEFILEDS.Code);
                        if (dsetfrm != null && dsetfrm.Tables.Count != 0 && dsetfrm.Tables[0].Rows.Count != 0)
                        {
                            objBASEFILEDS.Tran_mode = "view_mode";//open transacation in view mode
                            objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();//find primary key
                            objBASEFILEDS.FormCondition = dsetfrm.Tables[0].Rows[0]["condition"].ToString();//set default conditions
                            objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, objBASEFILEDS.FormCondition);//get all data of the transactaion table

                            objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                            //assign company,selected modules list,theme,login users details to business layer.
                            // objBASEFILEDS = objBSFD;
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF = objBASEFILEDS;
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF.ObjCompany = objBSFD.ObjCompany;
                            objBASEFILEDS.ObjCompany = objBSFD.ObjCompany;
                            objBASEFILEDS.HtModuleList = objBSFD.HtModuleList;
                            objBASEFILEDS.ObjControlSet = objBSFD.ObjControlSet;
                            objBASEFILEDS.ObjLoginUser = objBSFD.ObjLoginUser;
                            //  frmVendorMast objVendorMast = new frmVendorMast(objBASEFILEDS);
                            Type t = Type.GetType("iMANTRA." + dsetfrm.Tables[0].Rows[0]["frm_nm"].ToString());//create form type by form name.

                            objBASEFILEDS.TRAN_CD = cmbtran_nm.SelectedValue.ToString();//assign transcation code to business layer.

                            objBASEFILEDS.Tran_id = dgvApprove.CurrentRow.Cells["tran_id"].Value.ToString();
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_id = objBASEFILEDS.Tran_id;
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_mode_type = "approve_only";
                            objBASEFILEDS.Tran_mode_type = "approve_only";
                            objBASEFILEDS.Tran_mode = "view_mode";
                            Form objForm = Activator.CreateInstance(t, new object[] { objBASEFILEDS }) as Form;//create instance of the transcation from form type.
                            objForm.Name = objBASEFILEDS.Tran_nm;
                            objForm.MdiParent = this.ParentForm;
                            objForm.Show();//call transaction template.
                            objForm.Activate();
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Form Missing", "Form Missing");//permission is denied.
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Access Denied", "Access Rights");//permission is denied.
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Transaction is not exist", "Invalid Transaction");//transaction is not existed.
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.Activate();
            }
        }

        private void btn_approve_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to want cancel changes in approve?", "Approve Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                DisplayControlsonMode("view_mode");
            }
        }

        private void frmApprove_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void dgvApprove_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvApprove.Columns)
            {
                if (dgvApprove.Columns.Contains("level1_status"))
                {
                    if (dgvApprove.CurrentCell.OwningColumn.Name == "level1_status")
                    {
                        lblF2.Visible = true;
                    }
                }
            }
        }

        private void dgvApprove_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewColumn column in dgvApprove.Columns)
            {
                if (dgvApprove.Columns.Contains("level1_status"))
                {
                    if (dgvApprove.CurrentCell.OwningColumn.Name == "level1_status")
                    {
                        lblF2.Visible = false;
                    }
                }
            }

        }

        private void dgvApprove_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                if (objBSFD.HTITEM!=null && objBSFD.HTITEM.Contains(dgvApprove.CurrentRow.Cells["itserial"].Value))
                {
                    if (((Hashtable)objBSFD.HTITEM[dgvApprove.CurrentRow.Cells["itserial"].Value])!=null && ((Hashtable)objBSFD.HTITEM[dgvApprove.CurrentRow.Cells["itserial"].Value]).Count != 0)
                    {
                        if (((Hashtable)objBSFD.HTITEM[dgvApprove.CurrentRow.Cells["itserial"].Value]).Contains(dgvApprove.CurrentCell.OwningColumn.Name))
                        {
                            ((Hashtable)objBSFD.HTITEM[dgvApprove.CurrentRow.Cells["itserial"].Value])[dgvApprove.CurrentCell.OwningColumn.Name] = e.FormattedValue;
                        }
                    }
                }
            }
        }
    }
}
