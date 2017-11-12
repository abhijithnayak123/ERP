using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using iMANTRA_iniL;
using iMANTRA_IL;
using iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmShortCut : BaseClass
    {
        SqlConnection conn;

        Ini objIni = new Ini();

        private BL_BASEFIELD objBSFD = new BL_BASEFIELD();


        FL_BASEFIELD objBaseField = new FL_BASEFIELD();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        FL_GEN_INVOICE objFLGenInvoice = new FL_GEN_INVOICE();
        FL_MAST objFLMAST = new FL_MAST();
        FL_REP_MAST objFLRep = new FL_REP_MAST();
        FL_Roles objRoles = new FL_Roles();

        public frmShortCut(BL_BASEFIELD objBL)
        {
            InitializeComponent();
           // objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            //if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            //{
            //    objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            //}
            this.objBSFD = objBL;
        }

        private void frmShortCut_Load(object sender, EventArgs e)
        {
            treeView1.Height = this.Height * 90 / 100;
            treeView1.Width = this.Width * 95 / 100;
            txtSearch.Width = this.Width * 95 / 100;

            conn = new SqlConnection("data source=" + objIni.GetKeyFieldValue("SQL", "data source") + ";initial catalog=InodeMFG;user=" + objIni.GetKeyFieldValue("SQL", "user") + ";pwd=" + objIni.GetKeyFieldValue("SQL", "pwd"));
            String Sequel = "select CMOD.module_id,module_name,code,alias_nm,[level],[type] from CMOD inner join Rights on CMOD.module_id=Rights.module_id where [level]=0 and view_access=1 and CMOD.compid=1 order by [level],CMOD.module_id";
            SqlDataAdapter da = new SqlDataAdapter(Sequel, conn);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["type"].ToString() == "Transaction")
                {
                    index = 0;
                }
                else if (dr["type"].ToString() == "Master" || dr["type"].ToString() == "CustomMaster")
                {
                    index = 1;
                }
                else
                {
                    index = 2;
                }
                PopulateTreeView(dr["type"].ToString(), Convert.ToInt32(dr["module_id"].ToString()), treeView1.Nodes.Add(dr["code"].ToString(), dr["module_name"].ToString(), index));
            }

            treeView1.ExpandAll();

        }

        private void PopulateTreeView(string type, int parentId, TreeNode parentNode)
        {
            String Seqchildc = "select CMOD.module_id,module_name,code,alias_nm,[level],[type] from CMOD inner join Rights on CMOD.module_id=Rights.module_id where [level]=" + parentId + "and view_access=1 and CMOD.compid=1 order by [level],CMOD.module_id";
            SqlDataAdapter dachildmnuc = new SqlDataAdapter(Seqchildc, conn);
            DataTable dtchildc = new DataTable();
            dachildmnuc.Fill(dtchildc);
            TreeNode childNode;
            foreach (DataRow dr in dtchildc.Rows)
            {
                if (parentNode == null)
                {
                    childNode = treeView1.Nodes.Add(dr["code"].ToString(), dr["module_name"].ToString());
                }
                else
                {
                    childNode = parentNode.Nodes.Add(dr["code"].ToString(), dr["module_name"].ToString());
                }

                if (type == "Transaction")
                {
                    childNode.ImageIndex = 0;
                    childNode.SelectedImageIndex = 0;
                }
                else if (type == "Master" || type == "CustomMaster")
                {
                    childNode.ImageIndex = 1;
                    childNode.SelectedImageIndex = 1;
                }
                else
                {
                    childNode.ImageIndex = 2;
                    childNode.SelectedImageIndex = 2;
                }
                PopulateTreeView(dr["type"].ToString(), Convert.ToInt32(dr["module_id"].ToString()), childNode);
            }
        }

        private void frmShortCut_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
            //if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            //{
            //    if (!((frm_mainmenu)this.MdiParent).ShowShortCut)
            //    {
            //        this.Show();
            //    }                    
            //}
        }

        private void ClearBackColor()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                ClearRecursive(n);
            }
        }

        // called by ClearBackColor function
        private void ClearRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                tn.BackColor = Color.White;
                ClearRecursive(tn);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ClearBackColor();
            FindByText();
        }

        private void FindByText()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode n in nodes)
            {
                FindRecursive(n);
            }
        }

        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                // if the text properties match, color the item
                if (tn.Text.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    tn.BackColor = Color.Yellow;
                }
                else
                {

                }
                FindRecursive(tn);
            }
        }

        //public bool BindTransactionSetting(BL_BASEFIELD objBASEFILEDS, string tran_cd)
        //{
        //    DataSet dset = objFLTransaction.GetTrans_Settings(tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
        //    Type t = typeof(BL_BASEFIELD);
        //    PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        //    if (dset != null && dset.Tables[0].Rows.Count != 0)
        //    {
        //        foreach (PropertyInfo field in publicFieldInfos)
        //        {
        //            foreach (DataColumn col in dset.Tables[0].Columns)
        //            {
        //                if (null != field && field.CanWrite && field.Name.ToLower() == col.ColumnName.ToLower())
        //                {
        //                    if (field.PropertyType.Name.ToLower() != "string")
        //                    {
        //                        if (dset.Tables[0].Rows[0][col.ColumnName] != null && dset.Tables[0].Rows[0][col.ColumnName].ToString() != "")
        //                        {
        //                            objBASEFILEDS.GetType().GetProperty(field.Name).SetValue(objBASEFILEDS, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
        //                        }
        //                    }
        //                    else
        //                        objBASEFILEDS.GetType().GetProperty(field.Name).SetValue(objBASEFILEDS, Convert.ChangeType(dset.Tables[0].Rows[0][col.ColumnName].ToString(), field.PropertyType), null);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                objIni.SetKeyFieldValue("SQL", "initial catalog", ((frm_mainmenu)this.MdiParent).ObjBLComp.Db_nm);
                BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
                objBASEFILEDS.ObjCompany = ((frm_mainmenu)this.MdiParent).ObjBLComp;
                if (((frm_mainmenu)this.MdiParent).BindTransactionSetting(objBASEFILEDS, treeView1.SelectedNode.Name))
                {
                    objRoles.ObjMainFields =((frm_mainmenu)this.MdiParent).ObjBLMainFields;
                    if (objRoles.CheckPermisson("view," + objBASEFILEDS.Tran_nm))
                    {
                        objBASEFILEDS.Tran_mode = "view_mode";
                        objBASEFILEDS.Primary_id = objFLTransaction.GetPrimaryKeyFldNm(objBASEFILEDS.Main_tbl_nm, objBASEFILEDS.Tbl_catalog).ToUpper();
                        objBASEFILEDS.dsNavigation = objFLTransaction.GET_ALL_NAVIGATION_DATA(objBASEFILEDS, "");
                        objBASEFILEDS.Curr_date_time = DateTime.Now.ToString();
                        ((frm_mainmenu)this.MdiParent).ActiveBLBF = objBASEFILEDS; ((frm_mainmenu)this.MdiParent).ActiveBLBF.ObjCompany = ((frm_mainmenu)this.MdiParent).ObjBLComp; 
                        ((frm_mainmenu)this.MdiParent).ActiveBLBF.ObjControlSet = ((frm_mainmenu)this.MdiParent).ObjControlSetTran;
                        ((frm_mainmenu)this.MdiParent).ActiveBLBF.ObjLoginUser = ((frm_mainmenu)this.MdiParent).ObjBLMainFields;
                        ifrm_transaction objtransaction = new ifrm_transaction(objBASEFILEDS);
                        objtransaction.Tran_cd = treeView1.SelectedNode.Name;
                        if (((frm_mainmenu)this.MdiParent).ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0)
                        {
                            objtransaction.Tran_id = ((frm_mainmenu)this.MdiParent).ActiveBLBF.dsNavigation.Tables[0].Rows.Count > 0 ? ((frm_mainmenu)this.MdiParent).ActiveBLBF.dsNavigation.Tables[0].Rows[((frm_mainmenu)this.MdiParent).ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1][((frm_mainmenu)this.MdiParent).ActiveBLBF.Primary_id].ToString() : "0";
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_id = objtransaction.Tran_id;
                            ((frm_mainmenu)this.MdiParent).ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ((frm_mainmenu)this.MdiParent).ObjBLMainFields.CurUser + ((frm_mainmenu)this.MdiParent).ActiveBLBF.Curr_date_time] = ((frm_mainmenu)this.MdiParent).ActiveBLBF.dsNavigation.Tables[0].Rows.Count - 1;//+objtransaction.objBASEFILEDS.GetHashCode().ToString()
                        }
                        else
                        {
                            objtransaction.Tran_id = "0";
                            ((frm_mainmenu)this.MdiParent).ActiveBLBF.Tran_id = objtransaction.Tran_id;
                            ((frm_mainmenu)this.MdiParent).ObjBLMainFields.HASHTOOL[objtransaction.Tran_cd + ((frm_mainmenu)this.MdiParent).ObjBLMainFields.CurUser + ((frm_mainmenu)this.MdiParent).ActiveBLBF.Curr_date_time] = 0;
                        }


                        objtransaction.Tran_mode = "view_mode";
                        objtransaction.Name = objBASEFILEDS.Tran_nm;
                        // objtransaction.Text = objBASEFILEDS.Tran_nm;
                        objtransaction.MdiParent = ((frm_mainmenu)this.MdiParent);
                        objtransaction.Show();
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Access Denied","Access Rights",3000);
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Transaction is not exist","Invalid Transaction",3000);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Activate();
            }
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
            //                AutoClosingMessageBox.Show("Access Denied","Access Rights",3000);
            //            }
            //        }
            //        else
            //        {
            //            AutoClosingMessageBox.Show("Transaction is not exist","Transaction",3000);
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
        }
    }
}
