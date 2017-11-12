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
    public partial class frmThemeSetting : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_mode = "view_mode", tran_cd, tran_id = "0";

        DL_ADAPTER objDBAdaper = new DL_ADAPTER();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();

        DataSet dsetThemeSetting = new DataSet();

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

        public frmThemeSetting(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void frmAppearance_Load(object sender, EventArgs e)
        {
            BindThemeGrid();
            DisplayControlsonMode(objBASEFILEDS.Tran_mode);

            //this.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
            //this.ForeColor = objBASEFILEDS.ObjControlSet.Font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Font_color) : Color.Black;
            //this.ucToolBar1.Width = this.Width;
            //this.ucToolBar1.Maximize = false;
            //this.ucToolBar1.Width1 = this.Width;
            //this.ucToolBar1.UCbackcolor = objBASEFILEDS.ObjControlSet.Uc_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "9"));
            //this.ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                objBASEFILEDS.HTMAIN.Clear();
                objBASEFILEDS.HTITEM.Clear();
                switch (tran_mode)
                {
                    case "add_mode":
                        objBASEFILEDS.Tran_id = "0";
                        txtTheme_nm.Enabled = true;
                        txtTheme_nm.Text = "";
                        txtTheme_nm.Select();
                        dgvcolor.Enabled = true;
                        dgvFont.Enabled = true;
                        dgvThemes.Enabled = false;
                        Call_Grid();
                        break;
                    case "edit_mode":
                        txtTheme_nm.Enabled = false;
                        dgvcolor.Enabled = true;
                        dgvFont.Enabled = true;
                        dgvThemes.Enabled = false;
                        Call_Grid();
                        break;
                    case "view_mode":
                        txtTheme_nm.Enabled = false;
                        dgvcolor.Enabled = false;
                        dgvFont.Enabled = false;
                        dgvThemes.Enabled = true;
                        GetThemeData();
                        Call_Grid();
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
            if (txtTheme_nm.Text == "") { AutoClosingMessageBox.Show("Please Enter Theme Name","Validation",3000); flg = false; }
            else
            {
                DataSet dsetTheme = objDBAdaper.dsquery("select * from theme_set where theme_id!='" + objBASEFILEDS.Tran_id + "' and theme_nm='" + txtTheme_nm.Text + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                if (dsetTheme != null && dsetTheme.Tables.Count != 0 && dsetTheme.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("Theme Name Already exist","Validation",3000);
                    flg = false;
                }
            }
            objBASEFILEDS.HTMAIN["theme_nm"] = txtTheme_nm.Text;
            objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id] = objBASEFILEDS.Tran_id;
            foreach (DataGridViewRow row in dgvcolor.Rows)
            {
                foreach (DataGridViewColumn column in dgvcolor.Columns)
                {
                    if (!objBASEFILEDS.HTITEM.Contains(row.Cells["tran_type"].Value))
                    {
                        objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    if (column.Name == "control_setid" && objBASEFILEDS.Tran_mode == "add_mode")
                    {
                        ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])[column.Name] = "0";
                    }
                    else
                    {
                        ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
                ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])["theme_nm"] = txtTheme_nm.Text;
            }
            foreach (DataGridViewRow row in dgvFont.Rows)
            {
                foreach (DataGridViewColumn column in dgvFont.Columns)
                {
                    if (!objBASEFILEDS.HTITEM.Contains(row.Cells["tran_type"].Value))
                    {
                        objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                    }
                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])[column.Name] = row.Cells[column.Name].Value;
                }
            }
            return flg;
        }

        private void BindThemeGrid()
        {
            dgvThemes.AutoGenerateColumns = false;
            dgvcolor.AutoGenerateColumns = false;
            dgvFont.AutoGenerateColumns = false;

            GetThemeData();

            Bind_Color_Grid();
            Bind_Font_Grid();
            Call_Grid();
        }

        private void GetThemeData()
        {
            DataSet dsetThemes = objDBAdaper.dsquery("select distinct theme_id,theme_nm from " + objBASEFILEDS.Main_tbl_nm + " where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");

            if (dsetThemes != null && dsetThemes.Tables.Count != 0 && dsetThemes.Tables[0].Rows.Count != 0)
            {
                while (dgvThemes.Rows.Count > 0)
                {
                    if (!dgvThemes.Rows[0].IsNewRow)
                    {
                        dgvThemes.Rows.RemoveAt(0);
                    }
                }
                int i = 0;
                foreach (DataRow row in dsetThemes.Tables[0].Rows)
                {
                    dgvThemes.Rows.Add(1);
                    foreach (DataGridViewColumn column in dgvThemes.Columns)
                    {
                        if (dsetThemes.Tables[0].Columns.Contains(column.Name))
                        {
                            txtTheme_nm.Text = row["theme_nm"].ToString();
                            dgvThemes.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                lblRowsCount.Text = "Total Records : " + dgvThemes.Rows.Count;
            }
        }

        private void Call_Grid()
        {
            if (objBASEFILEDS.Tran_mode != "add_mode")
            {
                dsetThemeSetting = objDBAdaper.dsquery("select control_setid,back_color,font_color,uc_color,tab_head_color,on_hover_color,grid_color,tab_back_color,tab_font_color,on_focus,tran_type,font_family,font_size,tab_family,tab_size from control_set where theme_id='" + objBASEFILEDS.Tran_id + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            }
            else
            {
                dsetThemeSetting = objDBAdaper.dsquery("select control_setid,back_color,font_color,uc_color,tab_head_color,on_hover_color,grid_color,tab_back_color,tab_font_color,on_focus,tran_type,font_family,font_size,tab_family,tab_size from control_set where theme_id='" + objBASEFILEDS.Tran_id + "'");
            }

            objBASEFILEDS.HTMAIN["theme_nm"] = txtTheme_nm.Text;
            objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id] = objBASEFILEDS.Tran_id;

            Get_Color_Details();
            Get_Font_Details();
        }

        private void Bind_Color_Grid()
        {
            DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
            txtcol.HeaderText = "Transaction Type";
            txtcol.Tag = "string";
            txtcol.Name = "tran_type";
            dgvcolor.Columns.Add(txtcol);
            dgvcolor.Columns[txtcol.Name].Visible = true;
            dgvcolor.Columns[txtcol.Name].ReadOnly = true;
            dgvcolor.Columns[txtcol.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol.Name].DisplayIndex = 0;

            DataGridViewTextBoxColumn txtcol3 = new DataGridViewTextBoxColumn();
            txtcol3.HeaderText = "Title Color";
            txtcol3.Name = "uc_color";
            txtcol3.Tag = "string";
            dgvcolor.Columns.Add(txtcol3);
            dgvcolor.Columns[txtcol3.Name].Visible = true;
            dgvcolor.Columns[txtcol3.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol3.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol3.Name].DisplayIndex = 1;

            DataGridViewTextBoxColumn txtcol1 = new DataGridViewTextBoxColumn();
            txtcol1.HeaderText = "Background Color";
            txtcol1.Name = "back_color";
            txtcol1.Tag = "string";
            dgvcolor.Columns.Add(txtcol1);
            dgvcolor.Columns[txtcol1.Name].Visible = true;
            dgvcolor.Columns[txtcol1.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol1.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol1.Name].DisplayIndex = 2;

            DataGridViewTextBoxColumn txtcol2 = new DataGridViewTextBoxColumn();
            txtcol2.HeaderText = "Font Color";
            txtcol2.Name = "font_color";
            txtcol2.Tag = "string";
            dgvcolor.Columns.Add(txtcol2);
            dgvcolor.Columns[txtcol2.Name].Visible = true;
            dgvcolor.Columns[txtcol2.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol2.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol2.Name].DisplayIndex = 3;

            DataGridViewTextBoxColumn txtcol6 = new DataGridViewTextBoxColumn();
            txtcol6.HeaderText = "Grid Color";
            txtcol6.Name = "grid_color";
            txtcol6.Tag = "string";
            dgvcolor.Columns.Add(txtcol6);
            dgvcolor.Columns[txtcol6.Name].Visible = true;
            dgvcolor.Columns[txtcol6.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol6.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol6.Name].DisplayIndex = 4;

            DataGridViewTextBoxColumn txtcol5 = new DataGridViewTextBoxColumn();
            txtcol5.HeaderText = "On Hover Color";
            txtcol5.Name = "on_hover_color";
            txtcol5.Tag = "string";
            dgvcolor.Columns.Add(txtcol5);
            dgvcolor.Columns[txtcol5.Name].Visible = true;
            dgvcolor.Columns[txtcol5.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol5.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol5.Name].DisplayIndex = 5;

            DataGridViewTextBoxColumn txtcol8 = new DataGridViewTextBoxColumn();
            txtcol8.HeaderText = "On Focus Color";
            txtcol8.Name = "on_focus";
            txtcol8.Tag = "string";
            dgvcolor.Columns.Add(txtcol8);
            dgvcolor.Columns[txtcol8.Name].Visible = true;
            dgvcolor.Columns[txtcol8.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol8.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol8.Name].DisplayIndex = 6;

            DataGridViewTextBoxColumn txtcol4 = new DataGridViewTextBoxColumn();
            txtcol4.HeaderText = "Tab Title Color";
            txtcol4.Name = "tab_head_color";
            txtcol4.Tag = "string";
            dgvcolor.Columns.Add(txtcol4);
            dgvcolor.Columns[txtcol4.Name].Visible = true;
            dgvcolor.Columns[txtcol4.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol4.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol4.Name].DisplayIndex = 7;

            DataGridViewTextBoxColumn txtcol7 = new DataGridViewTextBoxColumn();
            txtcol7.HeaderText = "Tab Back Color";
            txtcol7.Name = "tab_back_color";
            txtcol7.Tag = "string";
            dgvcolor.Columns.Add(txtcol7);
            dgvcolor.Columns[txtcol7.Name].Visible = true;
            dgvcolor.Columns[txtcol7.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol7.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol7.Name].DisplayIndex = 8;

            DataGridViewTextBoxColumn txtcol10 = new DataGridViewTextBoxColumn();
            txtcol10.HeaderText = "Tab Font Color";
            txtcol10.Name = "tab_font_color";
            txtcol10.Tag = "string";
            dgvcolor.Columns.Add(txtcol10);
            dgvcolor.Columns[txtcol10.Name].Visible = true;
            dgvcolor.Columns[txtcol10.Name].ReadOnly = false;
            dgvcolor.Columns[txtcol10.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol10.Name].DisplayIndex = 9;

            DataGridViewTextBoxColumn txtcol9 = new DataGridViewTextBoxColumn();
            txtcol9.HeaderText = "primary id";
            txtcol9.Tag = "int";
            txtcol9.Name = "control_setid";
            dgvcolor.Columns.Add(txtcol9);
            dgvcolor.Columns[txtcol9.Name].Visible = false;
            dgvcolor.Columns[txtcol9.Name].ReadOnly = true;
            dgvcolor.Columns[txtcol9.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvcolor.Columns[txtcol9.Name].DisplayIndex = 0;
        }
        private void Get_Color_Details()
        {
            if (dsetThemeSetting != null && dsetThemeSetting.Tables.Count != 0 && dsetThemeSetting.Tables[0].Rows.Count != 0)
            {
                while (dgvcolor.Rows.Count > 0)
                {
                    if (!dgvcolor.Rows[0].IsNewRow)
                    {
                        dgvcolor.Rows.RemoveAt(0);
                    }
                }
                int i = 0;
                foreach (DataRow row in dsetThemeSetting.Tables[0].Rows)
                {
                    dgvcolor.Rows.Add(1);
                    foreach (DataGridViewColumn column in dgvcolor.Columns)
                    {
                        if (dsetThemeSetting.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvcolor.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                foreach (DataGridViewRow row in dgvcolor.Rows)
                {
                    foreach (DataGridViewColumn column in dgvcolor.Columns)
                    {
                        if (!objBASEFILEDS.HTITEM.Contains(row.Cells["tran_type"].Value))
                        {
                            objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
            }
        }

        private void Bind_Font_Grid()
        {
            DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
            txtcol.HeaderText = "Transaction Type";
            txtcol.Name = "tran_type";
            txtcol.Tag = "string";
            dgvFont.Columns.Add(txtcol);
            dgvFont.Columns[txtcol.Name].Visible = true;
            dgvFont.Columns[txtcol.Name].ReadOnly = true;
            dgvFont.Columns[txtcol.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol1 = new DataGridViewTextBoxColumn();
            txtcol1.HeaderText = "Font Family";
            txtcol1.Name = "font_family";
            txtcol1.Tag = "string";
            dgvFont.Columns.Add(txtcol1);
            dgvFont.Columns[txtcol1.Name].Visible = true;
            dgvFont.Columns[txtcol1.Name].ReadOnly = false;
            dgvFont.Columns[txtcol1.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol2 = new DataGridViewTextBoxColumn();
            txtcol2.HeaderText = "Font Size";
            txtcol2.Name = "font_size";
            txtcol2.Tag = "decimal";
            dgvFont.Columns.Add(txtcol2);
            dgvFont.Columns[txtcol2.Name].Visible = true;
            dgvFont.Columns[txtcol2.Name].ReadOnly = false;
            dgvFont.Columns[txtcol2.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol3 = new DataGridViewTextBoxColumn();
            txtcol3.HeaderText = "Tab Font Family";
            txtcol3.Name = "tab_family";
            txtcol3.Tag = "string";
            dgvFont.Columns.Add(txtcol3);
            dgvFont.Columns[txtcol3.Name].Visible = true;
            dgvFont.Columns[txtcol3.Name].ReadOnly = false;
            dgvFont.Columns[txtcol3.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DataGridViewTextBoxColumn txtcol4 = new DataGridViewTextBoxColumn();
            txtcol4.HeaderText = "Tab Font Size";
            txtcol4.Name = "tab_size";
            txtcol4.Tag = "decimal";
            dgvFont.Columns.Add(txtcol4);
            dgvFont.Columns[txtcol4.Name].Visible = true;
            dgvFont.Columns[txtcol4.Name].ReadOnly = false;
            dgvFont.Columns[txtcol4.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }
        private void Get_Font_Details()
        {
            if (dsetThemeSetting != null && dsetThemeSetting.Tables.Count != 0 && dsetThemeSetting.Tables[0].Rows.Count != 0)
            {
                while (dgvFont.Rows.Count > 0)
                {
                    if (!dgvFont.Rows[0].IsNewRow)
                    {
                        dgvFont.Rows.RemoveAt(0);
                    }
                }
                int i = 0;
                foreach (DataRow row in dsetThemeSetting.Tables[0].Rows)
                {
                    dgvFont.Rows.Add(1);
                    foreach (DataGridViewColumn column in dgvFont.Columns)
                    {
                        if (dsetThemeSetting.Tables[0].Columns.Contains(column.Name))
                        {
                            dgvFont.Rows[i].Cells[column.Name].Value = row[column.Name];
                        }
                    }
                    i++;
                }
                foreach (DataGridViewRow row in dgvFont.Rows)
                {
                    foreach (DataGridViewColumn column in dgvFont.Columns)
                    {
                        if (!objBASEFILEDS.HTITEM.Contains(row.Cells["tran_type"].Value))
                        {
                            objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        }
                        ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["tran_type"].Value])[column.Name] = row.Cells[column.Name].Value;
                    }
                }
            }
        }

        private void frmAppearance_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CustomChildWindowActivate(objBASEFILEDS);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void frmAppearance_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseCustomChildWindow(0, objBASEFILEDS);
            }
        }

        private void btnNewTheme_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void dgvFont_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                if (txt.Tag.ToString().Trim() == "decimal" || txt.Tag.ToString().Trim() == "int")
                {
                    txt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    txt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
                else
                {
                    txt.KeyDown -= new KeyEventHandler(txt_key_down_font);
                    txt.KeyDown += new KeyEventHandler(txt_key_down_font);
                }
            }
        }

        private void txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objfrmPopup = new frmPopup(((Hashtable)objBASEFILEDS.HTITEM[(dgvcolor.CurrentRow.Cells["tran_type"].Value)]), "COLOR", "", "color_nm;" + txt.Name + ",color_nm;" + txt.Name, "color_nm;Color Name", "Please select", "",false,"");
                    //objfrmPopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmPopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmPopup.ObjBFD = objBASEFILEDS;
                    objfrmPopup.ShowDialog();
                    txt.Text = ((Hashtable)objBASEFILEDS.HTITEM[(dgvcolor.CurrentRow.Cells["tran_type"].Value)])[txt.Name].ToString().Trim();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txt_key_down_font(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (e.KeyData == Keys.F2)
                {
                    frmPopup objfrmPopup = new frmPopup(((Hashtable)objBASEFILEDS.HTITEM[(dgvcolor.CurrentRow.Cells["tran_type"].Value)]), "FONT_FAMILY", "", "font_family_nm;" + txt.Name + ",font_family_nm;" + txt.Name, "font_family_nm;Font Family Name", "Please select", "",false,"");
                    //objfrmPopup.objCompany = objBASEFILEDS.ObjCompany;
                    //objfrmPopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    objfrmPopup.ObjBFD = objBASEFILEDS;
                    objfrmPopup.ShowDialog();
                    txt.Text = ((Hashtable)objBASEFILEDS.HTITEM[(dgvcolor.CurrentRow.Cells["tran_type"].Value)])[txt.Name].ToString().Trim();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void txt_Key_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    TextBox txt = (TextBox)sender;
                    if (txt.Tag.ToString().Trim() == "decimal")
                    {
                        if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                        {
                            e.Handled = true;
                        }
                        string[] str = txt.Text.Split('.');

                        if (e.KeyChar == '.' && str.Length > 1)
                        {
                            if (str[1] == "")
                                txt.Text = str[0] + ".00";
                            else
                                txt.Text = str[0] + "." + str[1];
                            e.Handled = true;
                        }
                    }
                    if (txt.Tag.ToString().Trim() == "int")
                    {
                        if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvcolor_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                if (txt.Tag.ToString().Trim() == "decimal" || txt.Tag.ToString().Trim() == "int")
                {
                    txt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    txt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
                else
                {
                    txt.KeyDown -= new KeyEventHandler(txt_key_down);
                    txt.KeyDown += new KeyEventHandler(txt_key_down);
                }
            }
        }

        private void txtTheme_nm_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                if (txtTheme_nm.Text != "")
                {
                    DataSet dsetTheme = objDBAdaper.dsquery("select * from theme_set where theme_id!='" + objBASEFILEDS.Tran_id + "' and theme_nm='" + txtTheme_nm.Text.Replace("'", "''") + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                    if (dsetTheme != null && dsetTheme.Tables.Count != 0 && dsetTheme.Tables[0].Rows.Count != 0)
                    {
                        AutoClosingMessageBox.Show("Theme Name Already exist","Validation",3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN["theme_nm"] = txtTheme_nm.Text;
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Enter Theme Name","Validation",3000);
                    e.Cancel = true;
                }
            }
        }

        private void dgvThemes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                objBASEFILEDS.Tran_id = dgvThemes.CurrentRow.Cells["theme_id"].Value.ToString();
                txtTheme_nm.Text = dgvThemes.CurrentRow.Cells["theme_nm"].Value.ToString();
                Call_Grid();
            }
        }

        private void frmThemeSetting_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
