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

namespace iMANTRA
{
    public partial class frmReportPreview : BaseClass
    {
        BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBLFD
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        FL_REP_MAST objFLRep = new FL_REP_MAST();
        private FL_GEN_INVOICE objFL_GEN_INV = new FL_GEN_INVOICE();

        public Hashtable HTFilter = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DataSet dsetRep = new DataSet();
        DataSet dscmdseries;
        int ctrlhgt = 0, hgt = 0, ctrlwid = 0, wid = 0, _lblwid = 0;
        private string rep_group = "";

        public string Rep_group
        {
            get { return rep_group; }
            set { rep_group = value; }
        }

        ComboBox cmdseries = new ComboBox();
        UserDT dtpfrm = new UserDT();
        UserDT dtpto = new UserDT();
        ComboBox cmdfrm_ac = new ComboBox();
        ComboBox cmdto_ac = new ComboBox();
        ComboBox cmdfrm_item = new ComboBox();
        ComboBox cmdto_item = new ComboBox();
        Label objlabledt = new Label();
        Label objlabledtto = new Label();
        Label objlable1 = new Label();
        Label objlable2 = new Label();
        Label objlable3 = new Label();
        Label objlable4 = new Label();

        public frmReportPreview()
        {
            InitializeComponent(); 
        }

        private void frmReportPreview_Load(object sender, EventArgs e)
        {
            objFL_GEN_INV.objCompany = objBLFD.ObjCompany;
            objFLRep.objCompany = objBLFD.ObjCompany;

            HTFilter.Clear();
            ctrlhgt = this.ClientSize.Height * 15 / 100;
            hgt = 0;
            _lblwid = this.ClientSize.Width * 20 / 100;
            ctrlwid = this.ClientSize.Width * 30 / 100;
            wid = this.ClientSize.Width / 2;

            hgt = ucToolBar1.Height + ctrlhgt / 2;

            Label objlable = new Label();
            objlable.Name = "lblName";
            objlable.Text = Rep_group;
            objlable.Bounds = new Rectangle(0, hgt, (wid), ctrlhgt);
            panel1.Controls.Add(objlable);

            cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdseries.Name = "rep_nm";
            dscmdseries = objFLRep.Get_Report_list(Rep_group, objBLFD.ObjCompany.Compid.ToString());
            if (dscmdseries != null)
            {
                if (dscmdseries.Tables[0].Rows.Count != 0)
                {
                    HTFilter.Add("sp_nm", dscmdseries.Tables[0].Rows[0]["sp_nm"].ToString());
                    HTFilter.Add("rep_nm", dscmdseries.Tables[0].Rows[0]["rep_nm"].ToString());
                    HTFilter.Add("rep_gr", dscmdseries.Tables[0].Rows[0]["desc"].ToString());
                }
                cmdseries.DataSource = dscmdseries.Tables[0];
                cmdseries.DisplayMember = "desc";
                cmdseries.ValueMember = "rep_nm";
                cmdseries.SelectedIndexChanged += new EventHandler(Series_Index_Changed);
                cmdseries.Update();
                cmdseries.Bounds = new Rectangle(wid, hgt, (wid), ctrlhgt);
                panel1.Controls.Add(cmdseries);
                // hgt = hgt + ctrlhgt;
                BindFilters();
                CopyDatatoHashTable();
                //this.Width = wid + ctrlwid + ctrlwid / 2;
                if (this.ClientSize.Height > hgt)
                    this.Height = hgt + ctrlhgt * 2;
                else
                    this.Height = this.ClientSize.Height;
            }
            else
            {
                AutoClosingMessageBox.Show("Procedure is not existed","Invalid Procedure",3000);
            }
            //this.BackColor = ObjBLFD.ObjControlSet.Back_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Back_color) : Color.White; this.ForeColor = ObjBLFD.ObjControlSet.Font_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Font_color) : Color.Black; ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width;
            ucToolBar1.UCbackcolor = ObjBLFD.ObjControlSet.Uc_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(ObjBLFD.ObjControlSet.Font_family != null ? ObjBLFD.ObjControlSet.Font_family : "Courier New", float.Parse(ObjBLFD.ObjControlSet.Font_size != null ? objBLFD.ObjControlSet.Font_size : "9"));
            AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "Report");
            ucToolBar1.Titlebar = "Report Preview";
        }

        private void Series_Index_Changed(object sender, EventArgs e)
        {
            //ctrlhgt = this.ClientSize.Height * 15 / 100;
            //hgt = 0;
            //_lblwid = this.ClientSize.Width * 20 / 100;
            //ctrlwid = this.ClientSize.Width * 30 / 100;
            //wid = this.ClientSize.Width / 2;
            //hgt = ucToolBar1.Height;

            ctrlhgt = this.ClientSize.Height * 20 / 100;
            hgt = 0;
            _lblwid = this.ClientSize.Width * 20 / 100;
            ctrlwid = this.ClientSize.Width * 30 / 100;
            wid = this.ClientSize.Width / 2;

            hgt = ucToolBar1.Height + ctrlhgt / 2;

            BindFilters();
            if (dscmdseries.Tables[0].Rows.Count != 0)
            {
                HTFilter["sp_nm"] = dscmdseries.Tables[0].Rows[cmdseries.SelectedIndex]["sp_nm"].ToString();
                HTFilter["rep_nm"] = dscmdseries.Tables[0].Rows[cmdseries.SelectedIndex]["rep_nm"].ToString();
                HTFilter["rep_gr"] = dscmdseries.Tables[0].Rows[cmdseries.SelectedIndex]["desc"].ToString();
            }

            //this.Width = wid + ctrlwid + ctrlwid / 2;
            //if (this.ClientSize.Height > hgt)
            //    this.Height = hgt + ctrlhgt * 2;
            //else
            //    this.Height = this.ClientSize.Height;
        }
        private void acfrm_Index_Changed(object sender, EventArgs e)
        {
            HTFilter["SAC"] = cmdfrm_ac.SelectedValue;
            objBLFD.ObjCompany.Start_ac = cmdfrm_ac.Text;
        }
        private void acto_Index_Changed(object sender, EventArgs e)
        {
            HTFilter["EAC"] = cmdto_ac.SelectedValue;
            objBLFD.ObjCompany.End_ac = cmdto_ac.Text;
        }
        private void frmitem_Index_Changed(object sender, EventArgs e)
        {
            HTFilter["SIT"] = cmdfrm_item.SelectedValue;
            objBLFD.ObjCompany.Start_it = cmdfrm_item.Text;
        }
        private void itemto_Index_Changed(object sender, EventArgs e)
        {
            HTFilter["EIT"] = cmdto_item.SelectedValue;
            objBLFD.ObjCompany.End_it = cmdto_item.Text;
        }
        private void dtpfrm_Validate(object sender, CancelEventArgs e)
        {
            if (dtpfrm.DtValue != null && dtpfrm.DtValue.ToString() != "")
            {
                if (dtpto.DtValue != null && dtpto.DtValue.ToString() != "")
                {
                    if (dtpfrm.DtValue > dtpto.DtValue)
                    {
                        AutoClosingMessageBox.Show("Start Date Should be Less than End Date","Validation",3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        HTFilter["SDATE"] = dtpfrm.DtValue.ToString("yyyy/MM/dd");
                        objBLFD.ObjCompany.Start_dt = dtpfrm.DtValue;
                    }
                }
                else
                {
                    HTFilter["SDATE"] = dtpfrm.DtValue.ToString("yyyy/MM/dd");
                    objBLFD.ObjCompany.Start_dt = dtpfrm.DtValue;
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Please Enter Valid Start Date","Validation",3000);
                e.Cancel = true;
            }
        }
        private void dtpto_Validate(object sender, CancelEventArgs e)
        {
            if (dtpto.DtValue != null && dtpto.DtValue.ToString() != "")
            {
                if (dtpfrm.DtValue != null && dtpfrm.DtValue.ToString() != "")
                {
                    if (dtpfrm.DtValue > dtpto.DtValue)
                    {
                        AutoClosingMessageBox.Show("End Date Should be Greater than Start Date","Validation",3000);
                        e.Cancel = true;
                    }
                    else
                    {
                        HTFilter["EDATE"] = dtpto.DtValue.ToString("yyyy/MM/dd");
                        objBLFD.ObjCompany.End_dt = dtpto.DtValue;
                    }
                }
                //else
                //{
                //    HTFilter["EDATE"] = dtpto.DtValue.ToString("yyyy/MM/dd");
                //    objBLFD.ObjCompany.End_dt = dtpto.DtValue.ToString("yyyy/MM/dd"); 
                //}
            }
            else
            {
                AutoClosingMessageBox.Show("Please Enter Valid End Date","Validation",3000);
                e.Cancel = true;
            }
        }

        private void BindFilters()
        {
            dsetRep = objFLRep.Get_Report_Details(cmdseries.SelectedValue.ToString(), objBLFD.ObjCompany.Compid.ToString());
            if (dsetRep != null && dsetRep.Tables[0].Rows.Count != 0)
            {
                #region add/remove controls
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_dt"].ToString()))
                {
                    if (this.panel1.Controls.Contains(dtpfrm))
                    {
                        panel1.Controls.Remove(objlabledt);
                        panel1.Controls.Remove(dtpfrm);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(dtpfrm))
                    {
                        panel1.Controls.Remove(dtpfrm);
                    }
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_dt"].ToString()))
                {
                    if (this.panel1.Controls.Contains(dtpto))
                    {
                        panel1.Controls.Remove(objlabledtto);
                        panel1.Controls.Remove(dtpto);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(dtpto))
                    {
                        panel1.Controls.Remove(dtpto);
                    }
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_ac"].ToString()))
                {
                    if (this.panel1.Controls.Contains(cmdfrm_ac))
                    {
                        panel1.Controls.Remove(objlable1);
                        panel1.Controls.Remove(cmdfrm_ac);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(cmdfrm_ac))
                    {
                        panel1.Controls.Remove(cmdfrm_ac);
                    }
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_ac"].ToString()))
                {
                    if (this.panel1.Controls.Contains(cmdto_ac))
                    {
                        panel1.Controls.Remove(objlable2);
                        panel1.Controls.Remove(cmdto_ac);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(cmdto_ac))
                    {
                        panel1.Controls.Remove(cmdto_ac);
                    }
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_item"].ToString()))
                {
                    if (this.panel1.Controls.Contains(cmdfrm_item))
                    {
                        panel1.Controls.Remove(objlable3);
                        panel1.Controls.Remove(cmdfrm_item);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(cmdfrm_item))
                    {
                        panel1.Controls.Remove(cmdfrm_item);
                    }
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_item"].ToString()))
                {
                    if (this.panel1.Controls.Contains(cmdto_item))
                    {
                        panel1.Controls.Remove(objlable4);
                        panel1.Controls.Remove(cmdto_item);
                    }
                }
                else
                {
                    if (this.panel1.Controls.Contains(cmdto_item))
                    {
                        panel1.Controls.Remove(cmdto_item);
                    }
                }
                #endregion

                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_dt"].ToString()))
                {
                    hgt += ctrlhgt + ctrlhgt * 10 / 100;
                    objlabledt.Name = "lbldtfrm";
                    objlabledt.Text = "Date From";
                    objlabledt.Bounds = new Rectangle(0, hgt, _lblwid, ctrlhgt);
                    panel1.Controls.Add(objlabledt);

                  //  dtpfrm.Text = objBLFD.ObjCompany.Fin_yr_sta.ToString("yyyy/MM/dd");//DateTime.Now.ToString("yyyy/MM/dd");  
                    dtpfrm.bUpdateFlag = true;
                    dtpfrm.DtValue = objBLFD.ObjCompany.Fin_yr_sta;
                  //  dtpfrm.CustomFormat = "dd-MM-yyyy";
                   // dtpfrm.Format = DateTimePickerFormat.Custom;
                    if (!HTFilter.Contains("SDATE"))
                    {
                        HTFilter.Add("SDATE", dtpfrm.DtValue.ToString("yyyy/MM/dd"));
                    }
                    dtpfrm.Bounds = new Rectangle(objlabledt.Width, hgt, ctrlwid, ctrlhgt);
                    dtpfrm.Validating += new CancelEventHandler(dtpfrm_Validate);
                    panel1.Controls.Add(dtpfrm);
                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_dt"].ToString()))
                {
                    objlabledtto.Name = "lbldtto";
                    objlabledtto.Text = "To";
                    objlabledtto.Bounds = new Rectangle(wid, hgt, _lblwid / 2, ctrlhgt);
                    panel1.Controls.Add(objlabledtto);

                    //dtpto.Text = objBLFD.ObjCompany.Fin_yr_end.ToString("yyyy/MM/dd");//DateTime.Now.AddYears(1).ToString();
                    dtpto.bUpdateFlag = true;
                    dtpto.DtValue = objBLFD.ObjCompany.Fin_yr_end;
                   // dtpto.CustomFormat = "dd-MM-yyyy";
                    //dtpto.Format = DateTimePickerFormat.Custom;
                    if (!HTFilter.Contains("EDATE"))
                    {
                        HTFilter.Add("EDATE", dtpto.DtValue.ToString("yyyy/MM/dd"));
                    }
                    dtpto.Bounds = new Rectangle(wid + objlabledtto.Width, hgt, ctrlwid, ctrlhgt);
                    dtpto.Validating += new CancelEventHandler(dtpto_Validate);
                    panel1.Controls.Add(dtpto);

                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_ac"].ToString()))
                {
                    hgt += ctrlhgt + ctrlhgt * 10 / 100;
                    objlable1.Name = "lblacfrm";
                    objlable1.Text = "Account From";
                    objlable1.Bounds = new Rectangle(0, hgt, _lblwid, ctrlhgt);
                    panel1.Controls.Add(objlable1);

                    cmdfrm_ac.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdfrm_ac.Name = "acfrm";
                    DataSet dscmdfrm_ac = objFL_GEN_INV.GET_TBL_VAL("CM_MAST", "", objBLFD.ObjCompany.Compid.ToString());
                    if (dscmdfrm_ac.Tables[0].Rows.Count != 0)
                    {
                        dscmdfrm_ac.Tables[0].DefaultView.Sort = "ac_nm";
                        cmdfrm_ac.DataSource = dscmdfrm_ac.Tables[0];
                        cmdfrm_ac.DisplayMember = "ac_nm";
                        cmdfrm_ac.ValueMember = "ac_nm";
                        cmdfrm_ac.SelectedIndexChanged += new EventHandler(acfrm_Index_Changed);
                        cmdfrm_ac.Update();
                        HTFilter["SAC"] = dscmdfrm_ac.Tables[0].Rows[0]["ac_nm"].ToString();
                    }
                    cmdfrm_ac.Bounds = new Rectangle(objlable1.Width, hgt, ctrlwid, ctrlhgt);
                    panel1.Controls.Add(cmdfrm_ac);

                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_ac"].ToString()))
                {
                    objlable2.Name = "lblacto";
                    objlable2.Text = "To";
                    objlable2.Bounds = new Rectangle(wid, hgt, _lblwid / 2, ctrlhgt);
                    panel1.Controls.Add(objlable2);
                    cmdto_ac.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdto_ac.Name = "acto";
                    DataSet dscmdto_ac = objFL_GEN_INV.GET_TBL_VAL("CM_MAST", "", objBLFD.ObjCompany.Compid.ToString());
                    if (dscmdto_ac.Tables[0].Rows.Count != 0)
                    {
                        dscmdto_ac.Tables[0].DefaultView.Sort = "ac_nm desc";
                        cmdto_ac.DataSource = dscmdto_ac.Tables[0];
                        cmdto_ac.DisplayMember = "ac_nm";
                        cmdto_ac.ValueMember = "ac_nm";
                        cmdto_ac.SelectedIndexChanged += new EventHandler(acto_Index_Changed);
                        cmdto_ac.Update();
                        HTFilter["EAC"] = dscmdto_ac.Tables[0].Rows[dscmdto_ac.Tables[0].Rows.Count - 1]["ac_nm"].ToString();
                    }
                    cmdto_ac.Bounds = new Rectangle(wid + objlable2.Width, hgt, ctrlwid, ctrlhgt);
                    panel1.Controls.Add(cmdto_ac);

                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["frm_item"].ToString()))
                {
                    hgt += ctrlhgt + ctrlhgt * 10 / 100;
                    objlable3.Name = "lblfrmitem";
                    objlable3.Text = "Item From";
                    objlable3.Bounds = new Rectangle(0, hgt, _lblwid, ctrlhgt);
                    panel1.Controls.Add(objlable3);

                    cmdfrm_item.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdfrm_item.Name = "frmitem";
                    DataSet dscmdfrm_item = objFL_GEN_INV.GET_TBL_VAL("PT_MAST", "", objBLFD.ObjCompany.Compid.ToString());
                    if (dscmdfrm_item.Tables[0].Rows.Count != 0)
                    {
                        dscmdfrm_item.Tables[0].DefaultView.Sort = "prod_nm";
                        cmdfrm_item.DataSource = dscmdfrm_item.Tables[0];
                        cmdfrm_item.DisplayMember = "prod_nm";
                        cmdfrm_item.ValueMember = "prod_nm";
                        cmdfrm_item.SelectedIndexChanged += new EventHandler(frmitem_Index_Changed);
                        cmdfrm_item.Update();
                        HTFilter["SIT"] = dscmdfrm_item.Tables[0].Rows[0]["prod_nm"].ToString();
                    }
                    cmdfrm_item.Bounds = new Rectangle(objlable3.Width, hgt, ctrlwid, ctrlhgt);
                    panel1.Controls.Add(cmdfrm_item);

                }
                if (bool.Parse(dsetRep.Tables[0].Rows[0]["to_item"].ToString()))
                {
                    objlable4.Name = "lblitemto";
                    objlable4.Text = "To";
                    objlable4.Bounds = new Rectangle(wid, hgt, _lblwid / 2, ctrlhgt);
                    panel1.Controls.Add(objlable4);

                    cmdto_item.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdto_item.Name = "itemto";
                    DataSet dscmdto_item = objFL_GEN_INV.GET_TBL_VAL("PT_MAST", "", objBLFD.ObjCompany.Compid.ToString());
                    if (dscmdto_item.Tables[0].Rows.Count != 0)
                    {
                        dscmdto_item.Tables[0].DefaultView.Sort = "prod_nm desc";
                        cmdto_item.DataSource = dscmdto_item.Tables[0];
                        cmdto_item.DisplayMember = "prod_nm";
                        cmdto_item.ValueMember = "prod_nm";
                        cmdto_item.SelectedIndexChanged += new EventHandler(itemto_Index_Changed);
                        cmdto_item.Update();
                        HTFilter["EIT"] = dscmdto_item.Tables[0].Rows[dscmdto_item.Tables[0].Rows.Count - 1]["prod_nm"].ToString();
                    }
                    cmdto_item.Bounds = new Rectangle(wid + objlable4.Width, hgt, ctrlwid, ctrlhgt);
                    panel1.Controls.Add(cmdto_item);

                }
            }
        }

        private void frmReportPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).CloseChildWindow(0);
            }
        }

        private void frmReportPreview_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                ((frm_mainmenu)this.MdiParent).ReportChildWindowActivate(this);// refreshToolbar(this.tran_cd, this.tran_mode);
            }
        }
        public void CopyDatatoHashTable()
        {
            HTFilter["SDATE"] = dtpfrm.DtValue.ToString("yyyy/MM/dd");
            HTFilter["EDATE"] = dtpto.DtValue.ToString("yyyy/MM/dd");
            HTFilter["SAC"] = cmdfrm_ac.Text;
            HTFilter["EAC"] = cmdto_ac.Text;
            HTFilter["SIT"] = cmdfrm_item.Text;
            HTFilter["EIT"] = cmdto_item.Text;
            objBLFD.ObjCompany.Start_ac = cmdfrm_ac.Text;
            objBLFD.ObjCompany.End_ac = cmdto_ac.Text;
            objBLFD.ObjCompany.Start_it = cmdfrm_item.Text;
            objBLFD.ObjCompany.End_it = cmdto_item.Text;
            objBLFD.ObjCompany.Start_dt = dtpfrm.DtValue;
            objBLFD.ObjCompany.End_dt = dtpto.DtValue;
        }

        private void frmReportPreview_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
