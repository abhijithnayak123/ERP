using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_DL;
using iMANTRA_BL;
using iMANTRA_IL;
using System.Collections;
using System.Reflection;
using iMANTRA_iniL;

namespace iMANTRA
{
    public partial class frmControlCenter : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();

        Ini objIni = new Ini();

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

        public frmControlCenter(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmControlCenter_Load(object sender, EventArgs e)
        {
            BindControls();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");      

            rectangleShape3.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage4.Width - 1, tabPage4.Height - rectangleShape9.Height - 1);
            rectangleShape24.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage4.Width - 1, tabPage4.Height - rectangleShape9.Height - 1);
           
            rectangleShape28.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage4.Width - 1, tabPage4.Height - rectangleShape9.Height - 1);

            //rectangleShape3.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage1.Width - 1, tabPage1.Height - rectangleShape9.Height - 1);
            //rectangleShape24.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage4.Width - 1, tabPage4.Height - rectangleShape9.Height - 1);
            //rectangleShape26.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage3.Width - 1, tabPage3.Height - rectangleShape9.Height - 1);
            //rectangleShape28.Bounds = new Rectangle(rectangleShape9.Location.X, rectangleShape9.Height, this.tabPage7.Width - 1, tabPage7.Height - rectangleShape9.Height - 1);

            rectangleShape3.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape9.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape1.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape2.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape24.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape23.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            
            rectangleShape25.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape28.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;
            rectangleShape27.BorderColor = objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon;

            rectangleShape9.Width = this.tabPage1.Width / 2;
            rectangleShape1.Width = this.tabPage1.Width / 2;
            rectangleShape23.Width = this.tabPage1.Width / 2;
            rectangleShape25.Width = this.tabPage1.Width / 2;
            rectangleShape27.Width = this.tabPage1.Width / 2;
            //this.Height = this.tabControl1.Height;
            //this.Width = this.tabControl1.Width;            
                  
        }
        private void BindControls()
        {
            String[] myArray = { "2", "4", };
            cmbqtydecpt.DataSource = myArray.ToArray();

            cmbratedecpt.DataSource = myArray.ToArray();

            DataSet dsest = new DataSet();
            dsest = objDBAdaper.dsquery("select distinct theme_nm from theme_set where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
            if (dsest != null && dsest.Tables.Count != 0 && dsest.Tables[0].Rows.Count != 0)
            {
                cmbTheme.DataSource = dsest.Tables[0];
                cmbTheme.DisplayMember = "theme_nm";
                cmbTheme.ValueMember = "theme_nm";
                cmbTheme.Update();
            }
        }

        private void AddFieldToHashTable()
        {
            objBASEFILEDS.dsetview = new DataSet();
            objBASEFILEDS.dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "=" + objBASEFILEDS.Tran_id + " and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
            {
                if (!objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                {
                    if (column.DataType.Name.ToString().ToLower() == "int32")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "boolean")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = false;
                    }
                    if (column.DataType.Name.ToString().ToLower() == "string")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "";
                    }
                    if (column.DataType.Name.ToString().ToLower() == "decimal")
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = "0.00";
                    }
                }
            }

            foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
            {
                foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                {
                    if (objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                    {
                        objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString();
                    }
                }
            }
        }
        private void AddFieldToDataSet(string tran_id)
        {
            DataSet dsetview = new DataSet();
            dsetview = objDBAdaper.dsquery("select * from  " + objBASEFILEDS.Main_tbl_nm + " where " + objBASEFILEDS.Primary_id + "='" + objBASEFILEDS.Tran_id + "' and  compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "' order by " + objBASEFILEDS.Primary_id);//objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

            if (dsetview != null && dsetview.Tables.Count != 0 && dsetview.Tables[0].Rows.Count != 0)
            {
                InsertFieldValueToHashTable();
            }
        }
        private void InsertFieldValueToHashTable()
        {
            objBASEFILEDS.HTMAIN["neg_stk"] = chkNegStk.Checked;
            objBASEFILEDS.HTMAIN["man_bal_chk"] = chkManPay.Checked;
            objBASEFILEDS.HTMAIN["ol_bal_chk"] = chkOlBal.Checked;
            objBASEFILEDS.HTMAIN["qty_dec"] = cmbqtydecpt.SelectedValue;
            objBASEFILEDS.HTMAIN["rate_dec"] = cmbratedecpt.SelectedValue;
            objBASEFILEDS.HTMAIN["theme_nm"] = cmbTheme.SelectedValue;
            objBASEFILEDS.HTMAIN["app_path"] = objIni.GetKeyFieldValue("APP_PATH", "path");
            objBASEFILEDS.HTMAIN["bakup_path"] = objIni.GetKeyFieldValue("APP_PATH", "bakuppath");
            objBASEFILEDS.HTMAIN["db_server"] = objIni.GetKeyFieldValue("SQL", "data source");
        }
        private void GetFieldValueFromHashTable()
        {
            chkNegStk.Checked = false;
            chkNegStk.Checked = false;
            chkOlBal.Checked = false;
            if (objBASEFILEDS.HTMAIN["neg_stk"] != null && objBASEFILEDS.HTMAIN["neg_stk"].ToString() != "")
            {
                if (objBASEFILEDS.HTMAIN["neg_stk"].ToString() == "1" || objBASEFILEDS.HTMAIN["neg_stk"].ToString().ToLower()=="true")
                {
                    chkNegStk.Checked = true;
                }
            }
            if (objBASEFILEDS.HTMAIN["man_bal_chk"] != null && objBASEFILEDS.HTMAIN["man_bal_chk"].ToString() != "")
            {
                if (objBASEFILEDS.HTMAIN["man_bal_chk"].ToString() == "1" || objBASEFILEDS.HTMAIN["man_bal_chk"].ToString().ToLower() == "true")
                {
                    chkManPay.Checked = true;
                }
            }
            if (objBASEFILEDS.HTMAIN["ol_bal_chk"] != null && objBASEFILEDS.HTMAIN["ol_bal_chk"].ToString() != "")
            {
                if (objBASEFILEDS.HTMAIN["ol_bal_chk"].ToString() == "1" || objBASEFILEDS.HTMAIN["ol_bal_chk"].ToString().ToLower() == "true")
                {
                    chkOlBal.Checked = true;
                }
            }
            cmbqtydecpt.SelectedItem = objBASEFILEDS.HTMAIN["qty_dec"].ToString();
            cmbratedecpt.SelectedItem = objBASEFILEDS.HTMAIN["rate_dec"].ToString();
            cmbTheme.Text = objBASEFILEDS.HTMAIN["theme_nm"].ToString();
            txtAppPath.Text = objIni.GetKeyFieldValue("APP_PATH", "path");
            txtbakup.Text = objIni.GetKeyFieldValue("APP_PATH", "bakuppath");
            txtdbserver.Text = objIni.GetKeyFieldValue("SQL", "data source");
        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                switch (tran_mode)
                {
                    case "add_mode":
                        objBASEFILEDS.Tran_id = "0";
                        foreach (Control c in this.Controls)//form controls
                        {
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        if (c2 is CheckBox)
                                        {
                                            ((CheckBox)c2).Checked = false;
                                        }
                                        else if (c2 is ComboBox)
                                        {
                                            if (((ComboBox)c2).SelectedIndex != -1)
                                            {
                                                ((ComboBox)c2).SelectedIndex = 0;
                                            }
                                        }
                                        else if (c2 is TextBox)
                                        {
                                            ((TextBox)c2).Text = "";
                                        }
                                        c2.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                if (c is CheckBox)
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                                else if (c is ComboBox)
                                {
                                    if (((ComboBox)c).SelectedIndex != -1)
                                    {
                                        ((ComboBox)c).SelectedIndex = 0;
                                    }
                                }
                                else if (c is TextBox)
                                {
                                    ((TextBox)c).Text = "";
                                }
                                c.Enabled = true;
                            }
                            //}
                        }
                        // EnableApprovePage();
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        break;
                    case "edit_mode":
                        AddFieldToHashTable();
                        InsertFieldValueToHashTable();
                        GetFieldValueFromHashTable();
                        foreach (Control c in this.Controls)//form controls
                        {
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        c2.Enabled = true;
                                    }
                                }
                            }
                            else
                            {
                                c.Enabled = true;
                            }
                        }
                        break;
                    case "view_mode":
                        AddFieldToHashTable();
                        GetFieldValueFromHashTable();
                        foreach (Control c in this.Controls)//form controls
                        {
                            //foreach (Control c in con.Controls)//groupbox controls
                            //{
                            if (c is TabControl)//tabcontrol
                            {
                                foreach (Control c1 in c.Controls) //tab pages will be available here
                                {
                                    foreach (Control c2 in c1.Controls)//controls
                                    {
                                        if (!(c2 is Label)) c2.Enabled = false;
                                    }
                                }
                            }
                            if (c is TextBox || c is Button)
                            {
                                if (!(c is Label)) c.Enabled = false;
                            }
                            //}
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
            objBASEFILEDS = objBLFD;
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
            objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id] = objBASEFILEDS.Tran_id;
            if (cmbTheme.SelectedValue != null && cmbTheme.SelectedValue.ToString() == "") { AutoClosingMessageBox.Show("Please Enter Theme Name","Validation",3000); flg = false; }
            InsertFieldValueToHashTable();
            return flg;
        }

        private void frmControlCenter_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmControlCenter_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            string tabName = "  " + tabControl1.TabPages[e.Index].Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            //tabControl1.Font = new System.Drawing.Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objControlSet.Font_family : "Courier New",float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? ObjControlSet.Font_size : "9"));
            tabControl1.Font = new Font(objBASEFILEDS.ObjControlSet.Tab_family != null ? objBASEFILEDS.ObjControlSet.Tab_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Tab_size != null ? objBASEFILEDS.ObjControlSet.Tab_size : "9"));
            Font font = new Font(tabControl1.Font, FontStyle.Bold);
            SolidBrush brushhead = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon);
            SolidBrush brush = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color) : Color.Black);

            //Find if it is selected, this one will be hightlighted...
            if (e.Index == tabControl1.SelectedIndex)
                e.Graphics.FillRectangle(brushhead, e.Bounds);

            e.Graphics.DrawString(tabName, font, brush, tabControl1.GetTabRect(e.Index), stringFormat);
        }

        private void frmControlCenter_Resize(object sender, EventArgs e)
        {
           //ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
