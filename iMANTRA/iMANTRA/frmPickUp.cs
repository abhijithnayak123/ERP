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
using CUSTOM_iMANTRA;
using iMANTRA_DL;

namespace iMANTRA
{
    public partial class frmPickUp : BaseClass
    {
        /****************************************************************
         *1.0 Sharanamma Jekeen on 11/25/13 Added Before Pick Up Trigger
         * 
         * 
         * 
         * 
         * 
         * 
         * ****************************************************************/

        BL_MAIN_FIELDS ObjBLMainFields = new BL_MAIN_FIELDS();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        DL_ADAPTER objiMANTRA_DL = new DL_ADAPTER();

        FL_PICKUP objFLPickUp = new FL_PICKUP();
        bool flgApproval = true;
        FL_BASEFIELD objFL_BASEFIELD = new FL_BASEFIELD();
        private Hashtable _htref = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _htrefitem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        string _pickupTran_nm = "";

        public string PickupTran_nm
        {
            get { return _pickupTran_nm; }
            set { _pickupTran_nm = value; }
        }

        iREFERENCE objiREFERENCE = new iREFERENCE();
        iBeforeReference objiBeforeReference = new iBeforeReference();//1.0

        DataSet dsetMainPickup, dsetMainGrid, dsetItemGrid, dsetRules = new DataSet();

        private string _tran_cd, _ac_nm = "", _rule = "", _pickupRef = "";

        public string Tran_cd
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }
        bool flgCheck = false;

        public frmPickUp(BL_BASEFIELD objFD)
        {
            this.Dock = DockStyle.Fill;
            InitializeComponent(); this.Tran_cd = objFD.Code;
            _tran_cd = objFD.Code;//= tran_cd;
            objBLFD = objFD;
        }

        private void frmPickUp_Load(object sender, EventArgs e)
        {
            try
            {
                objFLPickUp.objCompany = objBLFD.ObjCompany;
                objFL_BASEFIELD.objCompany = objBLFD.ObjCompany;

                this.Dock = DockStyle.Fill;

                foreach (string str in objBLFD.Ref_type.Split(','))
                {
                    _pickupRef = _pickupRef == "" ? "'" + str + "'" : _pickupRef + ",'" + str + "'";
                }
                BindControls();

                dsetMainGrid = objFLPickUp.Get_Ref_Main_Grid(objBLFD, cmbRef_Type.SelectedValue.ToString());

                //dataGridView1.CellClick -= new DataGridViewCellEventHandler(dataGridView1_CellClick);
                //dataGridView1.CellContentClick -= new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
                //dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
                //dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
                foreach (DataRow row in dsetMainGrid.Tables[0].Rows)
                {
                    Bind_Grid_Form_Controls(row, dataGridView1);
                }

                dsetItemGrid = objFLPickUp.Get_Ref_Item_Grid(objBLFD, cmbRef_Type.SelectedValue.ToString());

                //dataGridView2.CellClick -= new DataGridViewCellEventHandler(dataGridView2_CellClick);
                //dataGridView2.CellContentClick -= new DataGridViewCellEventHandler(dataGridView2_CellContentClick);
                //dataGridView2.CellValidating -= new DataGridViewCellValidatingEventHandler(dataGridView2_CellValidating);
                //dataGridView2.CellClick += new DataGridViewCellEventHandler(dataGridView2_CellClick);
                //dataGridView2.CellContentClick += new DataGridViewCellEventHandler(dataGridView2_CellContentClick);
                //dataGridView2.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView2_CellValidating);

                foreach (DataRow row in dsetItemGrid.Tables[0].Rows)
                {
                    Bind_Grid_Form_Controls(row, dataGridView2);
                }

                if (objBLFD.Hashitemref != null)
                {
                    if (_htrefitem != null)
                    {
                        _htrefitem.Clear();
                    }
                    foreach (DictionaryEntry entry in objBLFD.Hashitemref)
                    {
                        _htrefitem[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                        {
                            ((Hashtable)_htrefitem[entry.Key])[entry1.Key] = entry1.Value;
                        }
                    }
                }

                _ac_nm = "";
                _rule = "";

                if (objBLFD.HTMAINREF != null && objBLFD.HTMAINREF.Count != 0)
                {
                    _ac_nm = objBLFD.HTMAIN["ac_nm"].ToString();
                }
                else
                {
                    _ac_nm = objBLFD.Filter_req ? objBLFD.HTMAIN["ac_nm"].ToString() : "";
                }
                BindGrid(true);

                
                dsetRules = objiMANTRA_DL.dsquery("select distinct [rule] from [rules] where validity in (" + _pickupRef + ")");
                if (dsetRules != null && dsetRules.Tables.Count != 0 && dsetRules.Tables[0].Rows.Count != 0)
                {
                    DataRow[] rows = dsetRules.Tables[0].Select("rule='" + objBLFD.HTMAIN["rule"].ToString() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        _rule = objBLFD.HTMAIN["rule"].ToString();
                    }
                }

                CheckCheckboxSelection(_ac_nm, _rule);

                AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "Transaction");
                ucToolBar1.Titlebar = "PICK DETAILS";
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception");
            }
        }

        private void BindControls()
        {
            DataSet dssetTran = new DataSet();
            dssetTran = objiMANTRA_DL.dsquery("select tran_nm,code,behavier_cd from tran_set where code in (" + _pickupRef + ")");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);           
            if (dssetTran != null && dssetTran.Tables.Count != 0)
            {
                cmbRef_Type.DataSource = dssetTran.Tables[0];
                cmbRef_Type.ValueMember = "code";
                cmbRef_Type.DisplayMember = "tran_nm";
                cmbRef_Type.Update();

                if (objBLFD.HTMAINREF != null && objBLFD.HTMAINREF.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBLFD.HTMAINREF)
                    {
                        cmbRef_Type.SelectedValue = ((Hashtable)entry.Value)["REF_TRAN_CD"].ToString();
                        PickupTran_nm = ((Hashtable)entry.Value)["REF_TRAN_CD"].ToString();
                        break;
                    }
                }
                else
                {
                    cmbRef_Type.SelectedIndex = dssetTran.Tables[0].Rows.Count - 1;
                    PickupTran_nm = cmbRef_Type.SelectedValue.ToString();
                }
                cmbRef_Type.SelectedIndexChanged -= new EventHandler(cmbRef_Type_SelectedIndexChanged);
                cmbRef_Type.SelectedIndexChanged += new EventHandler(cmbRef_Type_SelectedIndexChanged);
            }

            DataSet dsest = new DataSet();
            dsest = objiMANTRA_DL.dsquery("select [rule] from [rules] where validity like '%" + objBLFD.Code + "%'");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);           

            cmbPickUpRule.DataSource = dsest.Tables[0];
            cmbPickUpRule.ValueMember = "rule";
            cmbPickUpRule.DisplayMember = "rule";
            cmbPickUpRule.Update();

            cmbPickUpRule.Text = objBLFD.HTMAIN["rule"].ToString();
            cmbPickUpRule.SelectedIndexChanged -= new EventHandler(cmbPickUpRule_SelectedIndexChanged);
            cmbPickUpRule.SelectedIndexChanged += new EventHandler(cmbPickUpRule_SelectedIndexChanged);
        }
        private void Bind_Grid_Form_Controls(DataRow row, DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.RowHeadersVisible = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeColumns = true;
            grid.ScrollBars = ScrollBars.Both;
            grid.AllowUserToOrderColumns = false;
            // grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EditMode = DataGridViewEditMode.EditOnEnter;
            //grid.DefaultCellStyle.BackColor = Color.SkyBlue;
            grid.RowsDefaultCellStyle.SelectionBackColor = Color.Aquamarine;
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                DefinedDGVTextBoxColumn txtcol = new DefinedDGVTextBoxColumn();
                txtcol.HeaderText = row["head_nm"].ToString().Trim();
                txtcol.Name = row["fld_nm"].ToString().Trim();
                txtcol.CopyTextBoxtoParent = (bool.Parse(row["_copy"].ToString().Trim()));
                txtcol.Tag = row["data_ty"].ToString().Trim().ToLower();
                grid.Columns.Add(txtcol);
                //  grid.Columns[row["fld_nm"].ToString().Trim()].
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim()) * int.Parse(row["FLD_DESC"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                // grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "button")
            {
                DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
                btncol.HeaderText = row["head_nm"].ToString().Trim();
                btncol.Text = row["head_nm"].ToString().Trim();
                btncol.UseColumnTextForButtonValue = true;
                btncol.Name = row["fld_nm"].ToString().Trim();
                btncol.Tag = row["data_ty"].ToString().Trim().ToLower();
                grid.Columns.Add(btncol);
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim()) * int.Parse(row["FLD_DESC"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                //  grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                DefinedDGVTextBoxColumn txtcol = new DefinedDGVTextBoxColumn();
                txtcol.HeaderText = row["head_nm"].ToString().Trim();
                txtcol.CopyTextBoxtoParent = (bool.Parse(row["_copy"].ToString().Trim()));
                txtcol.Tag = row["data_ty"].ToString().Trim().ToLower();
                txtcol.Name = row["fld_nm"].ToString().Trim();
                grid.Columns.Add(txtcol);
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim()) * int.Parse(row["FLD_DESC"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                // grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
                //   grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.Format = "N2";                
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                DefinedDGVTextBoxColumn txtcol = new DefinedDGVTextBoxColumn();
                txtcol.HeaderText = row["head_nm"].ToString().Trim();
                txtcol.Name = row["fld_nm"].ToString().Trim();
                txtcol.CopyTextBoxtoParent = (bool.Parse(row["_copy"].ToString().Trim()));
                txtcol.Tag = row["data_ty"].ToString().Trim().ToLower();
                grid.Columns.Add(txtcol);
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                // grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "bit")
            {
                DataGridViewCheckBoxColumn objchk = new DataGridViewCheckBoxColumn();
                objchk.Name = row["fld_nm"].ToString().Trim();
                objchk.Tag = row["data_ty"].ToString().Trim().ToLower();
                grid.Columns.Add(objchk);
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                // grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                DefinedDGVTextBoxColumn txtcol = new DefinedDGVTextBoxColumn();
                txtcol.HeaderText = row["head_nm"].ToString().Trim();
                txtcol.Name = row["fld_nm"].ToString().Trim();
                txtcol.CopyTextBoxtoParent = (bool.Parse(row["_copy"].ToString().Trim()));
                txtcol.Tag = row["data_ty"].ToString().Trim().ToLower();
                grid.Columns.Add(txtcol);
                grid.Columns[row["fld_nm"].ToString().Trim()].Width = int.Parse(row["FLD_WID"].ToString().Trim()) * int.Parse(row["FLD_DESC"].ToString().Trim());
                grid.Columns[row["fld_nm"].ToString().Trim()].Visible = (!bool.Parse(row["inter_val"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].ReadOnly = (bool.Parse(row["_read"].ToString().Trim()));
                grid.Columns[row["fld_nm"].ToString().Trim()].DefaultCellStyle.BackColor = bool.Parse(row["_read"].ToString().Trim()) ? Color.WhiteSmoke : Color.White;
                // grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString().Trim());
            }
        }
        private void Bind_Grid_Data_Controls(DataSet dset, DataGridView dataGrid, int flg)
        {
            if (dset != null && dset.Tables.Count != 0)
            {
                int i = 0;
                DataSet dsetTable = objiMANTRA_DL.dsquery("select code,Main_tbl_nm,Approve_tbl_nm from tran_set where isApprove=1 and tran_type='Transaction' and code='" + cmbRef_Type.SelectedValue + "' and compid=" + objBLFD.ObjCompany.Compid);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    flgApproval = true;
                    if (dataGrid == dataGridView1)
                    {
                        //DataSet dsetTable = objiMANTRA_DL.dsquery("select code,Main_tbl_nm,Approve_tbl_nm from tran_set where isApprove=1 and tran_type='Transaction' and code='" + objBLFD.Ref_type + "' and compid=" + objBLFD.ObjCompany.Compid);
                        if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
                        {
                            string strQuery = "select tran_id,tran_no,tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved=1 and tran_id='" + row["ref_tran_id"].ToString() + "' and tran_cd='" + dsetTable.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBLFD.ObjCompany.Compid;
                            DataSet dsetTransData = objiMANTRA_DL.dsquery(strQuery);
                            if (dsetTransData != null && dsetTransData.Tables.Count != 0 && dsetTransData.Tables[0].Rows.Count != 0)
                            {
                                flgApproval = true;
                            }
                            else
                            {
                                flgApproval = false;
                            }
                        }
                    }

                    if (flgApproval)
                    {
                        dataGrid.Rows.Add(1);
                        foreach (DataColumn column in dset.Tables[0].Columns)
                        {
                            if (dataGridView2 == dataGrid)
                            {
                                if (row["sel"].ToString().Trim().ToLower() == "true")
                                {
                                    if (!_htrefitem.ContainsKey(row["ref_ptserial"].ToString() + "," + row["ref_tran_id"].ToString()))
                                    {
                                        _htrefitem[row["ref_ptserial"].ToString() + "," + row["ref_tran_id"].ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                        foreach (DictionaryEntry entry in objBLFD.HTITEMREF)
                                        {
                                            ((Hashtable)_htrefitem[row["ref_ptserial"].ToString() + "," + row["ref_tran_id"].ToString()]).Add(entry.Key, entry.Value);
                                        }
                                    }
                                    ((Hashtable)_htrefitem[row["ref_ptserial"].ToString() + "," + row["ref_tran_id"].ToString()])[column.ColumnName] = row[column.ColumnName].ToString();
                                }
                            }
                            dataGrid.Rows[i].Cells[column.ColumnName].Value = row[column.ColumnName].ToString();
                        }
                        i++;
                    }
                }
                if (flg == 1)
                {
                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        foreach (DictionaryEntry entry in _htrefitem)
                        {
                            if (dataGrid == dataGridView1)
                            {
                                if (entry.Key.ToString().Split(',')[1] == row.Cells["ref_tran_id"].Value.ToString())
                                {
                                    row.Cells["sel"].Value = ((Hashtable)entry.Value)["sel"].ToString();                                   
                                    break;
                                }
                            }
                            else
                            {
                                if (entry.Key.ToString() == row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString())
                                {
                                    row.Cells["sel"].Value = ((Hashtable)entry.Value)["sel"].ToString();
                                    row.Cells["qty"].Value = ((Hashtable)entry.Value)["qty"].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region
            //string values = "";
            //bool flg = false;
            //foreach (DictionaryEntry entry in objBLFD.Hashitemref)
            //{
            //    flg = false;
            //    foreach (DictionaryEntry mainentry in objBLFD.HTMAINREF)
            //    {
            //        string key = ((Hashtable)mainentry.Value)["ref_ptserial"].ToString() + "," + ((Hashtable)mainentry.Value)["ref_tran_id"].ToString();
            //        if (entry.Key.ToString() == key)
            //        {
            //            flg = true;
            //            break;
            //        }
            //    }
            //    if (!flg)
            //    {
            //        values += entry.Key + ";";
            //    }
            //}
            //string[] strRemovekey = values.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string strKey in strRemovekey)
            //{
            //    objBLFD.Hashitemref.Remove(strKey);
            //}
            #endregion
            objBLFD.Flg_PickUp = false;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            objBLFD.Flg_PickUp = true;
            int i = 0;
            string itserial;
            objBLFD.HTITEM.Clear();
            objBLFD.HTMAINREF.Clear();
            _htref.Clear();

            _htref.Add("TRAN_NO", "");
            _htref.Add(objBLFD.Primary_id.ToString(), "");
            _htref.Add("TRAN_CD", "");
            _htref.Add("PTSERIAL", "");
            _htref.Add("REF_TRAN_NO", "");
            _htref.Add("REF_TRAN_ID", "");
            _htref.Add("REF_TRAN_CD", "");
            _htref.Add("REF_PTSERIAL", "");

            List<decimal> lst = new List<decimal>();
            List<string> lst2 = new List<string>();
            List<string> lst1 = new List<string>();
            foreach (DictionaryEntry entry in _htrefitem)
            {
                if (((Hashtable)entry.Value).Count != 0)
                {
                    if (entry.Key != null && entry.Key.ToString() != "")
                    {
                        lst.Add(decimal.Parse(entry.Key.ToString().Split(',')[0]));
                        lst1.Add(entry.Key.ToString());
                    }

                }
            }
            lst.Sort();

            decimal dt = new decimal();

            foreach (decimal key2 in lst)
            {
                foreach (string key1 in lst1)
                {
                    dt = Convert.ToDecimal(key1.Split(',')[0] != null && key1.Split(',')[0].ToString() != "" ? key1.Split(',')[0] : "0.00");
                    if (dt == key2)
                    {
                        lst2.Add(key1);
                        break;
                    }
                }
            }

            foreach (string key1 in lst2)
            {
            //foreach (DictionaryEntry entry in _htrefitem)
            //{
                i++;
                itserial = i.ToString();
                itserial = itserial.PadLeft(5, '0');
                objBLFD.HTITEM[itserial] = itserial;
                #region
                if (objBLFD.htitem_details == null || objBLFD.htitem_details.Count == 0)
                {
                    objBLFD.dsFooter = objFL_BASEFIELD.GETBASEFIELDFORGRID(Tran_cd, objBLFD.ObjCompany.Compid.ToString());
                    foreach (DataRow row in objBLFD.dsFooter.Tables[0].Rows)
                    {
                        if (!bool.Parse(row["ctrl_not_show"].ToString().Trim()))
                        {
                            objBLFD.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), "");
                        }
                    }
                    objBLFD.dsBASEADDIFIELDITEM = objFL_BASEFIELD.GETCUSTOMFIELDFORGRID(Tran_cd, objBLFD.ObjCompany.Compid.ToString());
                    foreach (DataRow row in objBLFD.dsBASEADDIFIELDITEM.Tables[0].Rows)
                    {
                        if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                        {
                            objBLFD.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), "0.00");
                        }
                        else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                        {
                            objBLFD.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), DateTime.Now.ToString("yyyy/MM/dd"));
                        }
                        else
                        {
                            objBLFD.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), "");
                        }
                    }
                    objBLFD.dsDCITEMFIELDS = objFL_BASEFIELD.GETDCFIELDFORGRID(Tran_cd, objBLFD.ObjCompany.Compid.ToString(), objBLFD.HTMAIN.Contains("tran_dt") && objBLFD.HTMAIN["tran_dt"] != null && objBLFD.HTMAIN["tran_dt"].ToString() != "" ? objBLFD.HTMAIN["tran_dt"].ToString() : "getdate()");
                    foreach (DataRow row in objBLFD.dsDCITEMFIELDS.Tables[0].Rows)
                    {
                        objBLFD.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), "0.00");
                    }
                }
                objBLFD.HTITEM[itserial] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry it_entry in objBLFD.htitem_details)
                {
                    ((Hashtable)objBLFD.HTITEM[itserial.ToString().Trim().PadLeft(5, '0')]).Add(it_entry.Key, it_entry.Value);
                }
                #endregion
                #region itref_tbl
                if (!objBLFD.HTMAINREF.ContainsKey(itserial))
                {
                    objBLFD.HTMAINREF[itserial] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("TRAN_NO", "0");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add(objBLFD.Primary_id.ToString(), "0");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("TRAN_CD", "");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("PTSERIAL", "");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("REF_TRAN_NO", "");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("REF_TRAN_ID", "");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("REF_TRAN_CD", "");
                    ((Hashtable)objBLFD.HTMAINREF[itserial]).Add("REF_PTSERIAL", "");
                }
                foreach (DictionaryEntry mainentry in objBLFD.HTMAINREF)
                {
                    if (mainentry.Key.ToString() == itserial)
                    {
                        ((Hashtable)mainentry.Value)["TRAN_NO"] = "0";
                        ((Hashtable)mainentry.Value)[objBLFD.Primary_id.ToString()] = "0";
                        ((Hashtable)mainentry.Value)["TRAN_CD"] = objBLFD.Code;
                        ((Hashtable)mainentry.Value)["PTSERIAL"] = itserial;
                        //((Hashtable)mainentry.Value)["REF_TRAN_NO"] = ((Hashtable)entry.Value)["ref_tran_no"];
                        //((Hashtable)mainentry.Value)["REF_TRAN_ID"] = ((Hashtable)entry.Value)["ref_tran_id"];
                        //((Hashtable)mainentry.Value)["REF_TRAN_CD"] = ((Hashtable)entry.Value)["ref_tran_cd"];
                        //((Hashtable)mainentry.Value)["REF_PTSERIAL"] = ((Hashtable)entry.Value)["ref_ptserial"];
                        ((Hashtable)mainentry.Value)["REF_TRAN_NO"] = ((Hashtable)_htrefitem[key1])["ref_tran_no"];
                        ((Hashtable)mainentry.Value)["REF_TRAN_ID"] = ((Hashtable)_htrefitem[key1])["ref_tran_id"];
                        ((Hashtable)mainentry.Value)["REF_TRAN_CD"] = ((Hashtable)_htrefitem[key1])["ref_tran_cd"];
                        ((Hashtable)mainentry.Value)["REF_PTSERIAL"] = ((Hashtable)_htrefitem[key1])["ref_ptserial"];
                    }
                }
                #endregion

                foreach (DictionaryEntry itementry in objBLFD.HTITEM)
                {
                    if (((Hashtable)itementry.Value).Count != 0 && itementry.Key.ToString() == itserial)
                    {
                        foreach (DataGridViewColumn col in dataGridView2.Columns)
                        {
                            if (col is DefinedDGVTextBoxColumn)
                            {
                                DefinedDGVTextBoxColumn txt = (DefinedDGVTextBoxColumn)col;
                                if (txt.CopyTextBoxtoParent && ((Hashtable)itementry.Value).Contains(col.Name))
                                {
                                   // ((Hashtable)itementry.Value)[col.Name] = ((Hashtable)entry.Value)[col.Name];
                                    ((Hashtable)itementry.Value)[col.Name] = ((Hashtable)_htrefitem[key1])[col.Name];
                                }
                            }
                        }
                        ((Hashtable)itementry.Value)["ptserial"] = itserial;
                        ((Hashtable)itementry.Value)["prod_no"] = i.ToString();
                    }
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["sel"].Value != null && bool.Parse(row.Cells["sel"].Value.ToString()))
                    {
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col is DefinedDGVTextBoxColumn)
                            {
                                DefinedDGVTextBoxColumn txt = (DefinedDGVTextBoxColumn)col;
                                if (txt.CopyTextBoxtoParent && objBLFD.HTMAIN.Contains(col.Name))
                                {
                                    objBLFD.HTMAIN[col.Name] = row.Cells[col.Name].Value;//((Hashtable)entry.Value)[col.Name];
                                }
                            }
                        }
                    }
                }
            }
            if (_htrefitem != null)
            {
                if (objBLFD.Hashitemref != null)
                {
                    objBLFD.Hashitemref.Clear();
                }
                foreach (DictionaryEntry entry in _htrefitem)
                {
                    objBLFD.Hashitemref[entry.Key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                    {
                        ((Hashtable)objBLFD.Hashitemref[entry.Key])[entry1.Key] = entry1.Value;
                    }
                }
            }
            objiREFERENCE.ACTIVE_REFERENCE = objBLFD;
            if (objBLFD.HTITEM.Count > 0 && objiREFERENCE.ValidateReference())
            {
                objBLFD.HTMAIN = objiREFERENCE.ACTIVE_REFERENCE.HTMAIN;
                objBLFD.HTITEM = objiREFERENCE.ACTIVE_REFERENCE.HTITEM;
                objBLFD.HTMAINREF = objiREFERENCE.ACTIVE_REFERENCE.HTMAINREF;
            }
            else
            {
                if (objiREFERENCE.BL_FIELDS.Errormsg.Length != 0)
                {
                    AutoClosingMessageBox.Show(objiREFERENCE.BL_FIELDS.Errormsg, "Reference");
                }
            }
            this.Close();
        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
        //private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
        //private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
        //private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{

        //}

        //private void GetApprovedTransaction()
        //{
        //    int i = 0; bool flg = true; string strTran_no = "";
        //    DataSet dsettblApprove = new DataSet();
        //    DataSet dsetTable = objiMANTRA_DL.dsquery("select code,Main_tbl_nm,Approve_tbl_nm from tran_set where isApprove=1 and tran_type='Transaction' and code='" + objBLFD.Code + "' and compid=" + objBLFD.ObjCompany.Compid);
        //    if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
        //    {
        //        string strQuery = "";
        //        string strApproveQuery = "";
        //        //dsetlevels = objiMANTRA_DL.dsquery("select level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where user_nm='" + ObjBLMainFields.CurUser + "' and code='" + cmbtran_nm.SelectedValue + "' and compid=" + objBLFD.ObjCompany.Compid);
        //        //DataSet dsetlevels = objiMANTRA_DL.dsquery("select levels.si_no,level_cnt,code,levels.user_nm,levels.level_nm,levels.condition,levels.isreqlvl,levels.main_cond_req,levels.compid,levels.fin_yr from levels where code='" + objBLFD.Code + "' and compid=" + objBLFD.ObjCompany.Compid + " order by cast(si_no as int)");
        //        DataSet dsetlevels = objiMANTRA_DL.dsquery("select levels.* from levels where code='" + objBLFD.Code + "' and compid=" + objBLFD.ObjCompany.Compid + " order by cast(si_no as int)");
        //        if (dsetlevels != null && dsetlevels.Tables.Count != 0 && dsetlevels.Tables[0].Rows.Count != 0)
        //        {
        //            foreach (DataRow levelsrow in dsetlevels.Tables[0].Rows)
        //            {
        //                strQuery = "select tran_id,tran_no,tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where i_approved=1 and tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBLFD.ObjCompany.Compid;
        //                DataSet dsetTransData = objiMANTRA_DL.dsquery(strQuery);

        //                if (dsetTransData != null && dsetTransData.Tables.Count != 0 && dsetTransData.Tables[0].Rows.Count != 0)
        //                {
        //                    foreach (DataRow row in dsetTransData.Tables[0].Rows)
        //                    {
        //                        dsettblApprove = objiMANTRA_DL.dsquery("select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + objBLFD.Code + "' and "+objBLFD.Primary_id+"='" + row["tran_id"].ToString() + "'");
        //                        if (dsettblApprove != null && dsettblApprove.Tables.Count != 0 && dsettblApprove.Tables[0].Rows.Count != 0)
        //                        {
        //                            foreach (DataRow row1 in dsettblApprove.Tables[0].Rows)
        //                            {
        //                                if (row["tran_id"].ToString() == row1["tran_id"].ToString())
        //                                {
        //                                    i = int.Parse(levelsrow["level_cnt"].ToString());
        //                                    if (int.Parse(levelsrow["si_no"].ToString()) != int.Parse(levelsrow["level_cnt"].ToString()))
        //                                    {
        //                                        i++;
        //                                        while (i <= int.Parse(levelsrow["level_cnt"].ToString()))
        //                                        {
        //                                            if (row1["level" + i + "_status"].ToString() != "APPROVE")
        //                                            {
        //                                                flg = false;
        //                                                break;
        //                                            }
        //                                            i++;
        //                                        }
        //                                    }
        //                                    if (flg)
        //                                    {
        //                                        if (strTran_no == "")
        //                                        {
        //                                            strTran_no = "'" + row["tran_id"].ToString() + "'";
        //                                        }
        //                                        else
        //                                        {
        //                                            strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
        //                                        }
        //                                    }

        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (ObjBLMainFields.CurUser.ToLower() == levelsrow["user_nm"].ToString())
        //                            {
        //                                i = int.Parse(levelsrow["si_no"].ToString());
        //                                if (int.Parse(levelsrow["si_no"].ToString()) == int.Parse(levelsrow["level_cnt"].ToString()))
        //                                {
        //                                    if (strTran_no == "")
        //                                    {
        //                                        strTran_no = "'" + row["tran_id"].ToString() + "'";
        //                                    }
        //                                    else
        //                                    {
        //                                        strTran_no = strTran_no + ",'" + row["tran_id"].ToString() + "'";
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (strTran_no == "")
        //            {
        //                strTran_no = "'0'";
        //            }
        //            strQuery = "select tran_id,tran_no,tran_dt,tran_cd,ac_nm from " + dsetTable.Tables[0].Rows[0]["Main_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBLFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
        //            strApproveQuery = "select * from " + dsetTable.Tables[0].Rows[0]["Approve_tbl_nm"].ToString() + " where tran_cd='" + dsetlevels.Tables[0].Rows[0]["code"].ToString() + "' and compid=" + objBLFD.ObjCompany.Compid + " and tran_id in (" + strTran_no + ")";
        //        }
        //    }
        //}

        private void frmPickUp_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            //if (txtPickupParty.Text == "")
            //{
            //    MessageBox.Show("Please Select Party");
            //    txtPickupParty.Select();
            //}
            //else if (cmbPickUpRule.Enabled && cmbPickUpRule.SelectedValue != null && cmbPickUpRule.SelectedValue.ToString() != "")
            //{
            //    MessageBox.Show("Please select Rule");
            //    cmbPickUpRule.Select();
            //}
        }

        private void BindGrid(bool flg)
        {
            dsetMainPickup = objFLPickUp.Get_Ref_main_Details(objBLFD, _ac_nm, _rule, cmbRef_Type.SelectedValue != null ? cmbRef_Type.SelectedValue.ToString() : objBLFD.Ref_type);
            if (dsetMainPickup != null && dsetMainPickup.Tables[0].Rows.Count != 0)
            {
                #region 1.0
                objiBeforeReference.DsReference = dsetMainPickup;
                objiBeforeReference.ACTIVE_BL = objBLFD;
                objiBeforeReference.iBeforeReferenceValidation();
                Bind_Grid_Data_Controls(objiBeforeReference.DsReference, dataGridView1, 1);
                #endregion 1.0
                if (dataGridView1.Rows.Count != 0)
                {
                    DataSet dsetItemPickup = objFLPickUp.Get_Ref_item_Details(dataGridView1.CurrentRow.Cells["ref_tran_id"].Value.ToString(), dataGridView1.CurrentRow.Cells["ref_tran_cd"].Value.ToString(), objBLFD.Tran_mode, objBLFD, _ac_nm, _rule);
                    if (objBLFD.HTITEMREF != null && objBLFD.HTITEMREF.Count == 0)
                    {
                        foreach (DataColumn column in dsetItemPickup.Tables[0].Columns)
                        {
                            objBLFD.HTITEMREF.Add(column.ColumnName, "");
                        }
                    }
                    Bind_Grid_Data_Controls(dsetItemPickup, dataGridView2, 1);
                }
                else
                {
                    AutoClosingMessageBox.Show("No Pick Up Details", "Pick Up");
                    //if (flg)
                    //{
                    //    this.Close();
                    //}
                }
            }
            else
            {
                AutoClosingMessageBox.Show("No Pick Up Details", "Pick Up");
                //if (flg)
                //{
                //    this.Close();
                //}
            }
        }

        private void cmbPickUpRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckCheckboxSelection(_ac_nm, cmbPickUpRule.Text);
            _rule = cmbPickUpRule.Text;
        }

        private void ClearGrid(DataGridView grid)
        {
            while (grid.Rows.Count > 0)
            {
                if (!grid.Rows[0].IsNewRow)
                {
                    grid.Rows.RemoveAt(0);
                }
            }
        }

        private void CheckCheckboxSelection(string ac_nm, string rule)
        {
            flgCheck = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (bool.Parse(row.Cells["sel"].Value.ToString()))
                {
                    flgCheck = true;
                    cmbPickUpRule.Enabled = false;
                    cmbPickUpRule.Text = rule;
                    cmbRef_Type.Enabled = false;
                    //  dataGridView1.CurrentCell = row.Cells["sel"];
                    break;
                }
            }
            if (!flgCheck)
            {
                //objBLFD.HTMAIN["ac_nm"] = "";
                //objBLFD.HTMAIN["rule"] = "";
                ac_nm = "";
                cmbPickUpRule.Enabled = true;
                cmbRef_Type.Enabled = true;
                if (cmbPickUpRule.Text == null || cmbPickUpRule.Text == "")
                {
                    rule = "";
                }
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ac_nm"].Value.ToString() != ac_nm)
                {
                    if (rule != "" && row.Cells["rule"].Value.ToString() != rule)
                    {
                        row.Visible = false;
                    }
                    else row.Visible = !flgCheck;
                }
                else
                {
                    if (rule != "" && row.Cells["rule"].Value.ToString() != rule)
                    {
                        row.Visible = false;
                    }
                    else
                        row.Visible = true;
                }
            }
        }

        private void cmbRef_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objBLFD.Tran_mode != "view_mode")
            {
                DialogResult result = MessageBox.Show("Changing Reference will be loose previous data?", "Reference Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    _htref.Clear();
                    _htrefitem.Clear();
                    dataGridView1.Columns.Clear();

                    dsetMainGrid = objFLPickUp.Get_Ref_Main_Grid(objBLFD, cmbRef_Type.SelectedValue.ToString());
                    PickupTran_nm = cmbRef_Type.SelectedValue.ToString();

                    foreach (DataRow row in dsetMainGrid.Tables[0].Rows)
                    {
                        Bind_Grid_Form_Controls(row, dataGridView1);
                    }

                    dataGridView2.Columns.Clear();

                    dsetItemGrid = objFLPickUp.Get_Ref_Item_Grid(objBLFD, cmbRef_Type.SelectedValue.ToString());

                    foreach (DataRow row in dsetItemGrid.Tables[0].Rows)
                    {
                        Bind_Grid_Form_Controls(row, dataGridView2);
                    }
                    _ac_nm = "";
                    _rule = "";

                    BindGrid(true);

                    _ac_nm = objBLFD.Filter_req ? objBLFD.HTMAIN["ac_nm"].ToString() : "";
                    //_rule = objBLFD.HTMAIN["rule"].ToString();
                    if (dsetRules != null && dsetRules.Tables.Count != 0 && dsetRules.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] rows = dsetRules.Tables[0].Select("rule='" + objBLFD.HTMAIN["rule"].ToString() + "'");
                        if (rows != null && rows.Length != 0)
                        {
                            _rule = objBLFD.HTMAIN["rule"].ToString();
                        }
                    }

                    CheckCheckboxSelection(_ac_nm, _rule);
                }
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            DataSet dsetItemPickup = objFLPickUp.Get_Ref_item_Details(gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString(), gridview.CurrentRow.Cells["ref_tran_cd"].Value.ToString(), objBLFD.Tran_mode, objBLFD, _ac_nm, _rule);
            dataGridView2.Rows.Clear();
            Bind_Grid_Data_Controls(dsetItemPickup, dataGridView2, 0);
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["sel"];
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    if (_htrefitem != null && _htrefitem.Count != 0)
                    {
                        if (_htrefitem.Contains(row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()))
                        {
                            chk.Value = (chk.Value != null) ? bool.Parse(((Hashtable)_htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()])["sel"].ToString()) : false;
                            row.Cells["qty"].Value = ((Hashtable)_htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()])["qty"].ToString();
                            row.Cells["sel"].Value = chk.Value;
                            break;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            flgApproval = true;
            DataGridView gridview = (DataGridView)sender;
            if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "sel")
                {
                    if (objBLFD.HTMAIN.Contains("ac_nm"))
                    {
                        _ac_nm = gridview.CurrentRow.Cells["ac_nm"].Value.ToString();
                        _rule = gridview.CurrentRow.Cells["rule"].Value.ToString();
                    }
                    CheckCheckboxSelection(gridview.CurrentRow.Cells["ac_nm"].Value.ToString(), gridview.CurrentRow.Cells["rule"].Value.ToString());

                    if (bool.Parse(gridview.CurrentRow.Cells["sel"].Value.ToString()))
                    {
                        DataSet dsetItemPickup = objFLPickUp.Get_Ref_item_Details(gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString(), gridview.CurrentRow.Cells["ref_tran_cd"].Value.ToString(), objBLFD.Tran_mode, objBLFD, _ac_nm, _rule);
                        if (objBLFD.HTITEMREF != null && objBLFD.HTITEMREF.Count == 0)
                        {
                            foreach (DataColumn column in dsetItemPickup.Tables[0].Columns)
                            {
                                objBLFD.HTITEMREF.Add(column.ColumnName, "");
                            }
                        }
                        dataGridView2.Rows.Clear();
                        Bind_Grid_Data_Controls(dsetItemPickup, dataGridView2, 0);
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["sel"];
                            chk.Value = true;
                            row.Cells["qty"].Value = row.Cells["bal_qty"].Value.ToString();
                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                if (!_htrefitem.ContainsKey(row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()))
                                {
                                    _htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in objBLFD.HTITEMREF)
                                    {
                                        ((Hashtable)_htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()]).Add(entry.Key, entry.Value);
                                    }
                                }
                                ((Hashtable)_htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()])[column.Name] = row.Cells[column.Name].Value.ToString();
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["sel"];
                            chk.Value = false;
                            row.Cells["qty"].Value = "0.00";
                            if (((Hashtable)_htrefitem).Contains(row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()))
                            {
                                if (((Hashtable)_htrefitem[row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString()])["ref_tran_id"].ToString() == gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString())
                                {
                                    _htrefitem.Remove(row.Cells["ref_ptserial"].Value.ToString() + "," + row.Cells["ref_tran_id"].Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           // bool _blnFlg = false;
            DataGridView gridview = (DataGridView)sender;
            gridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "sel" || gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "qty")
            {
                if (bool.Parse(gridview.CurrentRow.Cells["sel"].Value.ToString()))//gridview.CurrentCell.OwningColumn.Name == "sel" && 
                {
                    gridview.CurrentRow.Cells["qty"].Value = gridview.CurrentRow.Cells["bal_qty"].Value.ToString();
                    foreach (DataGridViewColumn column in dataGridView2.Columns)
                    {
                        if (!_htrefitem.ContainsKey(gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString()))
                        {
                            _htrefitem[gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            foreach (DictionaryEntry entry in objBLFD.HTITEMREF)
                            {
                                ((Hashtable)_htrefitem[gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString()]).Add(entry.Key, entry.Value);
                            }
                        }
                        ((Hashtable)_htrefitem[gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString()])[column.Name] = gridview.CurrentRow.Cells[column.Name].Value.ToString();
                    }
                }
                else
                {
                    gridview.CurrentRow.Cells["qty"].Value = "0.00";
                    _htrefitem.Remove(gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString());
                }
            }
        }

        private void dataGridView2_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridView gridview = (DataGridView)sender;
                if (gridview != null & e.RowIndex != -1 && e.ColumnIndex != -1 && _htrefitem != null && _htrefitem.Count != 0)
                {
                    if (gridview.CurrentCell.OwningColumn.Name.Trim().ToLower() == "qty")
                    {
                        if (decimal.Parse(gridview.CurrentRow.Cells["bal_qty"].Value.ToString()) >= decimal.Parse(e.FormattedValue.ToString()))
                        {
                            ((Hashtable)_htrefitem[gridview.CurrentRow.Cells["ref_ptserial"].Value.ToString() + "," + gridview.CurrentRow.Cells["ref_tran_id"].Value.ToString()])[gridview.CurrentCell.OwningColumn.Name] = e.FormattedValue.ToString().Trim();
                            e.Cancel = false;
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("quantity should be less than or equal to balance quantity", "Quantity Validation");
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
