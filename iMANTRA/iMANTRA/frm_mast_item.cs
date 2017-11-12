using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
using iMANTRA_BL;
using CUSTOM_iMANTRA;
using iMANTRA_IL;

namespace iMANTRA
{
    public partial class frm_mast_item : BaseClass
    {
        /****************************************************************         
          * 1.0 Sharanamma Jekeen on 11.26.13 ==> Added new Class in Custom Layer
          * 
          * 
          * 
          * 
          * 
          * ****************************************************************/
        // private iInit objInit = new iInit();

        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();

        FL_MAST objFLMAST = new FL_MAST();
        FL_GEN_INVOICE objFLGENINV = new FL_GEN_INVOICE();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        FL_GRIDEVENTS objFL_GRIDEVENTS = new FL_GRIDEVENTS();
        FL_BASEFIELD objFLBASEFIELDS = new FL_BASEFIELD();

        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();
        SetFieldsValue objSetFieldsValue = new SetFieldsValue();

        iCustomInit objiCustominit = new iCustomInit();
        iMASTER objiMASTER = new iMASTER();
        AddTransactionAndMaster objADDTRANSANDMASTER = new AddTransactionAndMaster();
        EditTransactionAndMaster objEditTRANSANDMASTER = new EditTransactionAndMaster();
        btn_event objiButtonEvent = new btn_event();//Sharanamma on 04.24.13,rg: btn click events
        iDefaultControl objDefaultControl = new iDefaultControl();//1.0

        private int count = 1, ctrlhgt = 0, hgt = 0, ctrlwid = 0, wid = 0, _lblwid = 0, _ctrl_wid = 0;
        private string tran_cd, tran_mode, tran_id;

        public string Tran_id
        {
            get { return tran_id; }
            set { tran_id = value; }
        }
        public string Tran_mode
        {
            get { return tran_mode; }
            set { tran_mode = value; }
        }
        public string Tran_cd
        {
            get { return tran_cd; }
            set { tran_cd = value; }
        }

        private TabControl tabControl = new TabControl();

        public frm_mast_item(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            //   objBASEFILEDS.HTMAIN[_primary_key_nm] = this.tran_id;
            this.objBASEFILEDS = objBL;
        }
        private void frm_mast_item_Load(object sender, EventArgs e)
        {
            try
            {
                objFLMAST.objCompany = objBASEFILEDS.ObjCompany;
                objFLGENINV.objCompany = objBASEFILEDS.ObjCompany;
                objFLTransaction.objCompany = objBASEFILEDS.ObjCompany;

                this.Dock = DockStyle.Fill;
                ctrlhgt = this.ClientSize.Height * 5 / 100;
                hgt = 0;
                ctrlwid =  Screen.PrimaryScreen.WorkingArea.Width * 50 / 100;
                wid =  Screen.PrimaryScreen.WorkingArea.Width / 2;

                _lblwid = (ctrlwid / 2) * 60 / 100;
                _ctrl_wid = wid * 55 / 100;

                // this.BackColor = Color.LightGray;
                objBASEFILEDS.HTMAIN.Clear();
                objBASEFILEDS.dsBASEFIELDMAIN = objFLMAST.GETMASTBASEHEADERFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                foreach (DataRow row in objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Rows)
                {
                    if (!bool.Parse(row["ctrl_not_show"].ToString()))
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                    }
                    // Bind_Header_Page(row);
                }
                //header fields from custom table
                objBASEFILEDS.dsBASEADDIFIELD = new DataSet();
                objBASEFILEDS.dsBASEADDIFIELD = objFLBASEFIELDS.GETCUSTOMFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                DataRow[] drowCustomForHeader = objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Select("_top=1");
                foreach (DataRow row in drowCustomForHeader)
                {
                    if (!bool.Parse(row["ctrl_not_show"].ToString()))
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                    }
                    // Bind_Header_Page(row);
                }
                string sqlQuery = "";
                if (objBASEFILEDS.Code != "OM")
                {
                    sqlQuery = "select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,_pickup,_top,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from ibasefields where code='" + Tran_cd + "' and typewise=1 and _top=1 and data_ty!='TAB' union all select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,disp_pickup,disp_head,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from icustomfields where code='" + Tran_cd + "' and typewise=1 and disp_head=1 order by order_no,col_order_no";
                }
                else
                {
                    sqlQuery = "select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,_pickup,_top,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from ibasefields where code='" + Tran_cd + "' and typewise=1 and _top=1 and data_ty!='TAB' union all select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,disp_pickup,disp_head,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from icustomfields where code='" + Tran_cd + "' and typewise=1 and disp_head=1 order by order_no,col_order_no";
                }
                DataSet dsetOrderHead = objFLTransaction.GetDataSet(sqlQuery);

                if (dsetOrderHead != null && dsetOrderHead.Tables.Count != 0 && dsetOrderHead.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dsetOrderHead.Tables[0].Rows)
                    {
                        Bind_Header_Page(row);
                    }
                }
                BindTabPage();
                DisplayControlsonMode(tran_mode);
                Control ctrl = this.ActiveControl;
                if (objBASEFILEDS.HTMAIN.Contains("FIN_YR"))
                {
                    objBASEFILEDS.HTMAIN["FIN_YR"] = objBASEFILEDS.ObjCompany.Fin_yr;
                    //if (((frm_mainmenu)this.MdiParent).ObjBLComp.Fin_yr != null)
                    //{
                    //    objBASEFILEDS.HTMAIN["FIN_YR"] = ((frm_mainmenu)this.MdiParent).ObjBLComp.Fin_yr;
                    //}
                }
                if (objBASEFILEDS.HTMAIN.Contains("COMPID"))
                {
                    objBASEFILEDS.HTMAIN["COMPID"] = objBASEFILEDS.ObjCompany.Compid.ToString();
                    //if (((frm_mainmenu)this.MdiParent).ObjBLComp.Compid != null)
                    //{
                    //    objBASEFILEDS.HTMAIN["COMPID"] = objBASEFILEDS.ObjCompany.Compid//((frm_mainmenu)this.MdiParent).ObjBLComp.Compid;
                    //}
                }

                //this.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
                //this.ForeColor = objBASEFILEDS.ObjControlSet.Font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Font_color) : Color.Black; 
                //ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
                //ucToolBar1.UCbackcolor = objBASEFILEDS.ObjControlSet.Uc_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color) : Color.Maroon;
                //this.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "9"));
                //ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;
                AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "Master");
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception");
            }
        }
        private void Bind_Header_Page(DataRow row)
        {
            count = int.Parse(row["order_no"].ToString());
            if (count % 2 != 0 && (row["parent_ctrl"].ToString().Trim() == "" || row["data_ty"].ToString().Trim().ToLower() != "button"))//int.Parse(row["dec_no"].ToString().Trim()) % 2 != 0)
            {
                if (count != 1)
                    hgt += ctrlhgt;
                else
                    hgt = 20;
            }

            Label objlable = new Label();
            objlable.Name = "lbl" + row["head_nm"].ToString();
            objlable.Text = row["head_nm"].ToString();
            // objlable.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable.Visible = !bool.Parse(row["inter_val"].ToString());
            // objlable.TextAlign = ContentAlignment.TopRight;

            Label objlable1 = new Label();
            objlable1.Name = "lbl1" + row["head_nm"].ToString().Trim();
            objlable1.Text = bool.Parse(row["mandatory"].ToString().Trim()) ? "*" : "";
            objlable1.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable1.Visible = !bool.Parse(row["inter_val"].ToString().Trim());

            if ((row["parent_ctrl"].ToString().Trim() == "" || !bool.Parse(row["ctrl_not_show"].ToString().Trim())) && !bool.Parse(row["inter_val"].ToString().Trim()) && !bool.Parse(row["_mul"].ToString().Trim()))
            {
                if (count % 2 == 0)
                {
                    objlable.Bounds = new Rectangle(wid, hgt, _lblwid, ctrlhgt);
                    objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                }
                else
                {
                    objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, _lblwid, ctrlhgt);
                    objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                }
            }
            pnlform.Controls.Add(objlable);
            pnlform.Controls.Add(objlable1);
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                if (bool.Parse(row["_mul"].ToString()))
                {
                    ComboBox cmdseries = new ComboBox();
                    cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdseries.Name = row["fld_nm"].ToString().Trim();
                    cmdseries.Tag = row["data_ty"].ToString().Trim().ToLower();
                    DataSet dscmdseries = objFLGENINV.GET_TBL_VAL(row["tbl_nm"].ToString(), tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                    cmdseries.DataSource = dscmdseries.Tables[0];
                    cmdseries.DisplayMember = row["sel_item"].ToString();
                    cmdseries.ValueMember = row["sel_val"].ToString();
                    cmdseries.Update();
                    if (dscmdseries != null && dscmdseries.Tables[0].Rows.Count != 0)
                    {
                        if (!bool.Parse(row["inter_val"].ToString()))
                        {
                            if (count % 2 == 0)
                            {
                                objlable.Bounds = new Rectangle(wid, hgt, _lblwid, ctrlhgt);
                                objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(wid + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt);
                            }
                            else
                            {
                                objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, _lblwid, ctrlhgt);
                                objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid, ctrlhgt);
                            }
                        }
                        cmdseries.Visible = !bool.Parse(row["inter_val"].ToString());
                        cmdseries.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                        cmdseries.Validating += new CancelEventHandler(cmd_validate);

                        cmdseries.Enter -= new EventHandler(cmdseriesenter);
                        cmdseries.Enter += new EventHandler(cmdseriesenter);
                        cmdseries.Leave -= new EventHandler(cmdseriesleave);
                        cmdseries.Leave += new EventHandler(cmdseriesleave);
                        pnlform.Controls.Add(cmdseries);
                    }
                    else
                    {
                        objlable.Visible = false;
                    }
                }
                else
                {
                    //TextBox objtxt = new TextBox();
                    //objtxt.Name = row["fld_nm"].ToString().Trim();
                    //objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();

                    PopupTextBox objtxt = new PopupTextBox();
                    objtxt.Name = row["fld_nm"].ToString().Trim();
                    objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                    objtxt.frmName = row["frm_nm"].ToString().Trim();
                    objtxt.PTextName = row["parent_ctrl"].ToString().Trim();
                    objtxt.Tbl_nm = row["tbl_nm"].ToString();
                    objtxt.Primaryddl = row["_primddl"].ToString().Trim();
                    objtxt.Dispddlfields = row["_Dpopflds"].ToString().Trim();
                    objtxt.Reftbltran_cd = row["reftbltran_cd"].ToString().Trim();
                    objtxt.Query_con = row["_querycon"].ToString().Trim();
                    if (row["_isQcd"] != null && row["_isQcd"].ToString() != "" && bool.Parse(row["_isQcd"].ToString()))
                    {
                        objtxt.IsQcd = true;
                        objtxt.QcdCondition = row["QcdCondition"] != null ? row["QcdCondition"].ToString() : "";
                    }
                    if (objtxt.Name.Trim().ToLower() == "tran_cd")
                    {
                        objBASEFILEDS.HTMAIN[objtxt.Name] = Tran_cd;
                    }
                    if (objtxt.Name.Trim().ToLower() == "pwd" || objtxt.Name.Trim().ToLower() == "r_pwd")
                    {
                        objtxt.PasswordChar = '*';
                    }

                    if (!bool.Parse(row["inter_val"].ToString()))
                    {
                        if (count % 2 == 0)
                        {
                            objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt);
                        }
                        else
                        {
                            objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid, ctrlhgt);
                        }
                    }
                    objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                    objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                    objtxt.Validating += new CancelEventHandler(String_Validating);
                    objtxt.KeyDown += new KeyEventHandler(String_KeyDown);

                    objtxt.Enter -= new EventHandler(txtenter);
                    objtxt.Enter += new EventHandler(txtenter);
                    objtxt.Leave -= new EventHandler(txtleave);
                    objtxt.Leave += new EventHandler(txtleave);
                    //    objtxt.LostFocus += new EventHandler(String_LostFocus);                         
                    pnlform.Controls.Add(objtxt);
                }
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid, ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(int_Validating);
                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);

                objtxt.Enter -= new EventHandler(int_enter);
                objtxt.Enter += new EventHandler(int_enter);
                objtxt.Leave -= new EventHandler(int_leave);
                objtxt.Leave += new EventHandler(int_leave);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "text")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Multiline = true;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid, ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    hgt += ctrlhgt + ctrlhgt;
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Text_Validating);
                //  objtxt.LostFocus += new EventHandler(String_LostFocus);

                objtxt.Enter -= new EventHandler(Text_enter);
                objtxt.Enter += new EventHandler(Text_enter);
                objtxt.Leave -= new EventHandler(Text_leave);
                objtxt.Leave += new EventHandler(Text_leave);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "image")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png";
                PictureBox objtxt = new PictureBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                //objtxt.Multiline = true;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid, ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    hgt += ctrlhgt + ctrlhgt;
                }
                objtxt.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png");
                //  objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = objtxt.ImageLocation;
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png";
                objtxt.BackgroundImageLayout = ImageLayout.Zoom;
                objtxt.Click += new EventHandler(Image_Change);
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                //objtxt.Validating += new CancelEventHandler(Text_Validating);
                //  objtxt.LostFocus += new EventHandler(String_LostFocus);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0.00";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Decimal_Validating);

                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                //   objtxt.LostFocus += new EventHandler(Decimal_LostFocus);

                objtxt.Enter -= new EventHandler(Decimal_enter);
                objtxt.Enter += new EventHandler(Decimal_enter);
                objtxt.Leave -= new EventHandler(Decimal_leave);
                objtxt.Leave += new EventHandler(Decimal_leave);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "bit")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0";
                CheckBox objchk = new CheckBox();
                objchk.Name = row["fld_nm"].ToString().Trim();
                objchk.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objchk.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objchk.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                objchk.Visible = !bool.Parse(row["inter_val"].ToString());
                objchk.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                objchk.Validating += new CancelEventHandler(CheckBox_Validating);
                // objchk.LostFocus += new EventHandler(CheckBox_LostFocus);

                objchk.Enter -= new EventHandler(CheckBox_enter);
                objchk.Enter += new EventHandler(CheckBox_enter);
                objchk.Leave -= new EventHandler(CheckBox_leave);
                objchk.Leave += new EventHandler(CheckBox_leave);
                pnlform.Controls.Add(objchk);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = DateTime.Now.ToString("yyyy/MM/dd");

                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                //  dtp.CustomFormat = "dd-MMM-yyyy";
                dtp.CustomFormat = " ";
                dtp.Format = DateTimePickerFormat.Custom;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(_lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(_lblwid + objlable1.Width + (_ctrl_wid) * 5 / 100, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                dtp.Visible = !bool.Parse(row["inter_val"].ToString());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Validating += new CancelEventHandler(dateTimePicker1_Validating);
                //    dtp.LostFocus += new EventHandler(dateTimePicker_LostFocus);     

                dtp.Enter -= new EventHandler(dateTimePicker1_enter);
                dtp.Enter += new EventHandler(dateTimePicker1_enter);
                dtp.Leave -= new EventHandler(dateTimePicker1_leave);
                dtp.Leave += new EventHandler(dateTimePicker1_leave);
                pnlform.Controls.Add(dtp);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "time")
            {
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Format = DateTimePickerFormat.Time;
                dtp.CustomFormat = "HH:mm";
                dtp.Visible = !bool.Parse(row["inter_val"].ToString());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + 0, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    dtp.Validating += new CancelEventHandler(dateTimePicker1_Time_Validating);
                }

                dtp.Enter -= new EventHandler(dateTimePicker1_Time_enter);
                dtp.Enter += new EventHandler(dateTimePicker1_Time_enter);
                dtp.Leave -= new EventHandler(dateTimePicker1_Time_leave);
                dtp.Leave += new EventHandler(dateTimePicker1_Time_leave);
                pnlform.Controls.Add(dtp);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "button")
            {
                objlable.Text = "";
                PopupButton btnform = new PopupButton();
                btnform.Name = row["fld_nm"].ToString().Trim();
                //btnform.Text = row["head_nm"].ToString().Trim();
                btnform.frmName = row["frm_nm"].ToString().Trim();
                btnform.PTextName = row["parent_ctrl"].ToString().Trim();
                btnform.Tbl_nm = row["tbl_nm"].ToString();
                btnform.Primaryddl = row["_primddl"].ToString().Trim();
                btnform.Dispddlfields = row["_Dpopflds"].ToString().Trim();
                btnform.Reftbltran_cd = row["reftbltran_cd"].ToString().Trim();
                btnform.Query_con = row["_querycon"].ToString().Trim();
                btnform.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                if (row["_isQcd"] != null && row["_isQcd"].ToString() != "" && bool.Parse(row["_isQcd"].ToString()))
                {
                    btnform.IsQcd = true;
                    btnform.QcdCondition = row["QcdCondition"] != null ? row["QcdCondition"].ToString() : "";
                }
                if (row["parent_ctrl"].ToString().Trim() == "")
                {
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        btnform.Text = row["head_nm"].ToString().Trim();
                        if (count % 2 == 0)
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                        }
                        else
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                        }
                        btnform.Click += new System.EventHandler(btn_click);
                    }
                }
                else
                {
                    if (!bool.Parse(row["inter_val"].ToString()))
                    {
                        if (count % 2 == 0)
                        {
                            btnform.Bounds = new Rectangle(_lblwid + _ctrl_wid + wid + _ctrl_wid * 5 / 100, hgt, _ctrl_wid * 10 / 100, ctrlhgt * 9 / 10);
                        }
                        else
                        {
                            btnform.Bounds = new Rectangle(_lblwid + _ctrl_wid + _ctrl_wid * 10 / 100, hgt, _ctrl_wid * 10 / 100, ctrlhgt * 9 / 10);
                        }
                        btnform.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\search.jpg");
                        btnform.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                        btnform.BackgroundImageLayout = ImageLayout.Zoom;
                        btnform.Click += new System.EventHandler(btn_popup_click);
                    }
                }
                btnform.Enter -= new EventHandler(btn_enter);
                btnform.Enter += new EventHandler(btn_enter);
                btnform.Leave -= new EventHandler(btn_leave);
                btnform.Leave += new EventHandler(btn_leave);
                pnlform.Controls.Add(btnform);
            }
        }
        #region color
        private void btn_enter(object sender, EventArgs e)
        {
            Button txt = (Button)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;

        }
        private void int_enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            // txt.BackColor = Color.Yellow;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void cmdseriesenter(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            // txt.BackColor = Color.Yellow;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void txtenter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
            txt.SelectionStart = txt.Text.Length;
        }
        private void Text_enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void Decimal_enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void CheckBox_enter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            // chk.BackColor = Color.Yellow;
            chk.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void dateTimePicker1_enter(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            // dtp.CalendarTitleBackColor = Color.Yellow;
            dtp.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }

        private void dateTimePicker1_Time_enter(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            //dtp.CalendarTitleBackColor = Color.Yellow;
            dtp.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void btn_leave(object sender, EventArgs e)
        {
            Button txt = (Button)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void int_leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void cmdseriesleave(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void txtleave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
            txt.Text = txt.Text.Trim();
            txt.SelectionStart = 0;
        }
        private void Text_leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void Decimal_leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void CheckBox_leave(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        private void dateTimePicker1_leave(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            dtp.CalendarTitleBackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }

        private void dateTimePicker1_Time_leave(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            dtp.CalendarTitleBackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
        }
        #endregion
        private void Image_Change(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to change Logo?", "Add New Logo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                PictureBox objtxt = (PictureBox)sender;
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "Add Company Logo";
                open.InitialDirectory = Application.StartupPath;
                open.Filter = "*.jpg|*.png|*.gif|*.*";
                open.ShowDialog();
                objtxt.BackgroundImage = Image.FromFile(open.FileName);
                objBASEFILEDS.HTMAIN["LOGO"] = open.FileName;
            }
        }
        private void int_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void String_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void cmd_validate(object sender, CancelEventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.ToString();
            objBASEFILEDS.HTMAIN[txt.DisplayMember] = txt.Text.ToString();
            if (txt.SelectedValue != null)
            {
                objBASEFILEDS.HTMAIN[txt.ValueMember] = txt.SelectedValue.ToString();
            }
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void Text_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void Decimal_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void CheckBox_Validating(object sender, CancelEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            objBASEFILEDS.HTMAIN[chk.Name] = chk.Checked;
            e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString(), "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString(), "valid_con");
        }
        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
            e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "valid_con");
        }
        private void dateTimePicker1_Time_Validating(object sender, CancelEventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToLongTimeString();
            e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "valid_con");
        }
        private void tab_Int_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con", 1);
        }
        private void tab_String_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con", 1);
        }
        private void tab_cmd_validate(object sender, CancelEventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.ToString();
            objBASEFILEDS.HTMAIN[txt.DisplayMember] = txt.Text.ToString();
            objBASEFILEDS.HTMAIN[txt.ValueMember] = txt.SelectedValue.ToString();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con", 1);
        }
        private void tab_Text_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con", 1);
        }
        private void tab_Decimal_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con", 1);
        }
        private void tab_CheckBox_Validating(object sender, CancelEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            objBASEFILEDS.HTMAIN[chk.Name] = chk.Checked;
            e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString(), "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString(), "valid_con", 1);
        }
        private void tab_dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
            e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "valid_con", 1);
        }
        private void tab_dateTimePicker1_Time_Validating(object sender, CancelEventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToLongTimeString();
            e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "_mon_con", 1);
            if (!e.Cancel)
                e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "valid_con", 1);
        }
        private bool ValidateFields(string fld_nm, string fld_val, string con_nm, int flg = 0)
        {
            if (tran_mode == "add_mode" || tran_mode == "edit_mode")
            {
                DataRow[] rowdtp;
                string _strValue = "";
                string _strDataType = "varchar";
                if (flg == 0)
                {
                    rowdtp = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + fld_nm + "'");
                }
                else
                {
                    rowdtp = null;
                    foreach (DataRow row1 in objBASEFILEDS.dsTab.Tables[0].Rows)
                    {
                        objBASEFILEDS.dsTabDet = objFLMAST.GETMASTBASETABDETAILS(Tran_cd, row1["fld_nm"].ToString(), objBASEFILEDS.ObjCompany.Compid.ToString());
                        foreach (DataRow row in objBASEFILEDS.dsTabDet.Tables[0].Rows)
                        {
                            if (row["fld_nm"].ToString() == fld_nm)
                            {
                                rowdtp = objBASEFILEDS.dsTabDet.Tables[0].Select("fld_nm='" + fld_nm + "'");
                                break;
                            }
                        }
                    }
                }

                objVALIDATION.ObjBLFD = objBASEFILEDS;
                objFL_GRIDEVENTS.objBASEFILEDS = objBASEFILEDS;

                bool validflg = true;
                foreach (DataRow row in rowdtp)
                {
                    _strDataType = row["data_ty"].ToString().Trim().ToLower();
                    //string exp = row["valid_con"].ToString();
                    string exp = row[con_nm].ToString();
                    string[] ar = exp.Split(new Char[] { '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ar.Length >= 1)
                    {
                        if (ar.Length == 1 || ar.Length == 2)
                        {
                            string exp1 = ar[0];
                            string[] ar1 = exp1.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string str in ar1)
                            {
                                validflg = CallCustomMethod(str, row["tbl_nm"].ToString().Trim(), fld_nm, fld_val, row["head_nm"].ToString().Trim(), flg);
                                if (!validflg)
                                {
                                    break;
                                }
                            }
                            #region
                            //if (objVALIDATION.GetType().GetMethod(ar[0]) != null)
                            //{
                            //    objVALIDATION.ObjBLFD = objBASEFILEDS;
                            //    object[] args = new[] { fld_nm, row["tbl_nm"].ToString().Trim(), fld_val, "0" };
                            //    MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[0]);
                            //    validflg = bool.Parse(methodInfo.Invoke(objVALIDATION, args).ToString().Trim());
                            //    if (!validflg)
                            //    {
                            //        MessageBox.Show("Please enter Valid " + row["head_nm"].ToString().Trim());
                            //    }
                            //    else
                            //    {
                            //        BindControlsFromView();
                            //    }
                            //}
                            //else
                            //{                                
                            //    if (objiMASTER.GetType().GetMethod(ar[0]) != null)
                            //    {
                            //        objiMASTER.ACTIVE_MASTER = objBASEFILEDS;
                            //       // objFLMAST.ObjBLFD = objBASEFILEDS;
                            //        object[] args = new[] { fld_nm, row["tbl_nm"].ToString().Trim(), fld_val, "0" };
                            //        MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[0]);
                            //        validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
                            //        if (!validflg)
                            //        {
                            //            MessageBox.Show("Please enter Valid " + row["head_nm"].ToString().Trim());
                            //        }
                            //        else
                            //        {
                            //            objBASEFILEDS.HTMAIN = objiMASTER.ACTIVE_MASTER.HTMAIN;
                            //            objBASEFILEDS.HTITEM = objiMASTER.ACTIVE_MASTER.HTITEM;
                            //            BindControlsFromView();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        AutoClosingMessageBox.Show("Sorry!! Method is not defined!!","Validation",3000);
                            //        validflg = false;
                            //    }
                            //}
                            #endregion
                        }
                        else
                        {
                            if (ar[0].Contains("!EMPTY"))
                            {
                                bool blnreg = false;
                                if (_strDataType == "decimal" || _strDataType == "int")
                                {
                                    Regex reg = new Regex(@"^[0-9]+\.[0-9]+$");
                                    Regex reg1 = new Regex(@"^[0-9]+$");
                                    if (reg.IsMatch(fld_val) && _strDataType == "decimal")
                                    {
                                        blnreg = true;
                                    }
                                    else if (reg1.IsMatch(fld_val))
                                    {
                                        blnreg = true;
                                    }
                                }
                                else
                                {
                                    blnreg = true;
                                }
                                if (blnreg)
                                {
                                    string[] cond = ar[0].Split(new string[] { "!EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                                    string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                                    _strValue = cod1;
                                    if (cod1 == "")
                                    {
                                        AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should not be empty ", "Validation");
                                        validflg = false;
                                    }
                                    else
                                    {
                                        validflg = true;
                                    }
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString(), "Validation");
                                    validflg = false;
                                }
                            }
                            else if (ar[0].Contains("EMPTY"))
                            {
                                string[] cond = ar[0].Split(new string[] { "EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                                string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                                _strValue = cod1;
                                if (cod1 == "")
                                {
                                    validflg = true;
                                }
                                else
                                {
                                    validflg = false;
                                }
                            }
                            else
                            {
                                string valu = ar[0].Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$").Replace("$=", "=$");
                                string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                                if (cond.Length == 3)
                                {
                                    bool blnreg = false;
                                    if (_strDataType == "decimal" || _strDataType == "int")
                                    {
                                        Regex reg = new Regex(@"^[0-9]+\.[0-9]+$");
                                        Regex reg1 = new Regex(@"^[0-9]+$");
                                        if (reg.IsMatch(fld_val) && _strDataType == "decimal")
                                        {
                                            blnreg = true;
                                        }
                                        else if (reg1.IsMatch(fld_val))
                                        {
                                            blnreg = true;
                                        }
                                    }
                                    else
                                    {
                                        blnreg = true;
                                    }
                                    if (blnreg)
                                    {
                                        string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "decimal") : cond[0];
                                        string cod2 = cond[2].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[2], "decimal") : cond[2];
                                        switch (cond[1])
                                        {
                                            case "<": if (decimal.Parse(cod1) < decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be less than  " + cod2, "Validation"); } break;
                                            case ">": if (decimal.Parse(cod1) > decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be greater than  " + cod2, "Validation"); } break;
                                            case "<=": if (decimal.Parse(cod1) <= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be less than equal to " + cod2, "Validation"); } break;
                                            case ">=": if (decimal.Parse(cod1) >= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be greater than equal to  " + cod2, "Validation"); } break;
                                            case "==": if (decimal.Parse(cod1) == decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be equal to  " + cod2, "Validation"); } break;
                                            case "!=": if (decimal.Parse(cod1) != decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should be not equal to  " + cod2, "Validation"); } break;
                                            default: validflg = false; break;
                                        }
                                    }
                                    else
                                    {
                                        AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString(), "Validation");
                                        validflg = false;
                                    }
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show("Sytanx is wrong!!", "Validation");
                                }
                            }
                            if (validflg)
                            {
                                if (ar[1].ToString().Trim().ToUpper() != "TRUE")
                                {
                                    string exp1 = ar[1];
                                    string[] ar1 = exp1.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string str in ar1)
                                    {
                                        validflg = CallCustomMethod(str, row["tbl_nm"].ToString().Trim(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
                                        if (!validflg)
                                        {
                                            break;
                                        }
                                    }
                                    #region
                                    //if (objVALIDATION.GetType().GetMethod(ar[1]) != null)
                                    //{
                                    //    //object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id].ToString(),objBASEFILEDS.Tbl_catalog, "0" };
                                    //    object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "0" };
                                    //    MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[1]);
                                    //    validflg = bool.Parse(methodInfo.Invoke(objVALIDATION, args).ToString());
                                    //    if (!validflg)
                                    //    {
                                    //        MessageBox.Show(row["head_nm"].ToString() + " already existed");
                                    //    }
                                    //    else
                                    //    {
                                    //        if (ar.Length == 4)
                                    //        {
                                    //            if (ar[3].ToLower() == "multiple")
                                    //            {
                                    //                BindControlsFromView();
                                    //            }
                                    //            else
                                    //            {
                                    //                Control[] txts = this.Controls.Find(ar[3].ToString().ToUpper(), true);
                                    //                if (txts != null)
                                    //                {
                                    //                    txts[0].Text = objBASEFILEDS.HTMAIN[ar[3].ToString().ToUpper()].ToString();
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (objiMASTER.GetType().GetMethod(ar[1]) != null)
                                    //    {
                                    //        objiMASTER.ACTIVE_MASTER = objBASEFILEDS;
                                    //        // objFLMAST.ObjBLFD = objBASEFILEDS;
                                    //        object[] args = new[] { fld_nm, row["tbl_nm"].ToString().Trim(), fld_val, "0" };
                                    //        MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[1]);
                                    //        validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
                                    //        if (!validflg)
                                    //        {
                                    //            MessageBox.Show("Please enter Valid " + row["head_nm"].ToString().Trim());
                                    //        }
                                    //        else
                                    //        {
                                    //            objBASEFILEDS.HTMAIN = objiMASTER.ACTIVE_MASTER.HTMAIN;
                                    //            objBASEFILEDS.HTITEM = objiMASTER.ACTIVE_MASTER.HTITEM;                                               

                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        AutoClosingMessageBox.Show("Sorry!! Method is not defined!!","Validation",3000);
                                    //        validflg = false;
                                    //    }
                                    //}
                                    #endregion
                                }
                            }
                            if (!validflg)
                            {
                                if (ar[2].ToString().Trim().ToUpper() != "FALSE")
                                {
                                    string exp1 = ar[2];
                                    string[] ar1 = exp1.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string str in ar1)
                                    {
                                        validflg = CallCustomMethod(str, row["tbl_nm"].ToString().Trim(), fld_nm, fld_val, row["head_nm"].ToString().Trim(), flg);
                                        if (!validflg)
                                        {
                                            break;
                                        }
                                    }
                                    #region
                                    //if (objVALIDATION.GetType().GetMethod(ar[2]) != null)
                                    //{
                                    //    object[] args = new[] { fld_nm, row["tbl_nm"].ToString(), _strValue, "0" };
                                    //    MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(ar[2]);
                                    //    validflg = bool.Parse(methodInfo.Invoke(objVALIDATION, args).ToString());
                                    //}
                                    //else
                                    //{
                                    //    if (objiMASTER.GetType().GetMethod(ar[2]) != null)
                                    //    {
                                    //        objiMASTER.ACTIVE_MASTER = objBASEFILEDS;
                                    //        // objVALIDATION.ObjBLFD = objBASEFILEDS;
                                    //        object[] args = new[] { fld_nm, row["tbl_nm"].ToString().Trim(), fld_val, "0" };
                                    //        MethodInfo methodInfo = typeof(iMASTER).GetMethod(ar[2]);
                                    //        validflg = bool.Parse(methodInfo.Invoke(objiMASTER, args).ToString().Trim());
                                    //        if (!validflg)
                                    //        {
                                    //            MessageBox.Show("Please enter Valid " + row["head_nm"].ToString().Trim());
                                    //        }
                                    //        else
                                    //        {
                                    //            objBASEFILEDS.HTMAIN = objiMASTER.ACTIVE_MASTER.HTMAIN;
                                    //            objBASEFILEDS.HTITEM = objiMASTER.ACTIVE_MASTER.HTITEM;
                                    //            BindControlsFromView();
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        AutoClosingMessageBox.Show("Sorry!! Method is not defined!!","Validation",3000);
                                    //        validflg = false;
                                    //    }
                                    //}
                                    #endregion
                                }
                            }
                        }
                    }
                }
                return !validflg;
            }
            return false;
        }

        private bool CallCustomMethod(string _method_nm, string tbl_nm, string fld_nm, string fld_val, string header_nm, int flg)
        {
            bool validflg = true;

            if (objVALIDATION.GetType().GetMethod(_method_nm) != null)
            {
                objVALIDATION.ObjBLFD = objBASEFILEDS;
                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                objVALIDATION.objSetFieldsValue = objSetFieldsValue;
                //object[] args = new[] { fld_nm, row["tbl_nm"].ToString().Trim(), _strValue, "0" };
                MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(_method_nm);
                validflg = bool.Parse(methodInfo.Invoke(objVALIDATION, null).ToString().Trim());
                if (!validflg)
                {
                    AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                }
                else
                {
                    objBASEFILEDS.HTMAIN = objVALIDATION.ObjBLFD.HTMAIN;
                    BindControlsFromView();
                }
            }
            else
            {
                if (objiMASTER.GetType().GetMethod(_method_nm) != null)
                {
                    objiMASTER.ACTIVE_MASTER = objBASEFILEDS;
                    SetFieldsValue(tbl_nm, fld_nm, fld_val);
                    MethodInfo methodInfo = typeof(iMASTER).GetMethod(_method_nm);
                    validflg = bool.Parse(methodInfo.Invoke(objiMASTER, null).ToString().Trim());
                    if (!validflg)
                    {
                        if (objiMASTER.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objiMASTER.BL_FIELDS.Errormsg, "Master Validation");
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        }
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN = objiMASTER.ACTIVE_MASTER.HTMAIN;
                        BindControlsFromView();
                    }
                }
            }
            return validflg;
        }

        private void SetFieldsValue(string cur_tbl_nm, string fld_nm, string fld_val)
        {
            objSetFieldsValue.Fld_tbl_nm = cur_tbl_nm;
            objSetFieldsValue.Fld_nm = fld_nm;
            objSetFieldsValue.Fld_value = fld_val;
        }

        private void BindControlsFromView()
        {
            foreach (Control c in this.Controls[1].Controls)
            {
                if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                {
                    if (c is PictureBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                        {
                            ((PictureBox)c).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c.Name].ToString());
                        }
                    }
                    else if (c is CheckBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                        {
                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "0")
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = "false";
                            }
                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "1")
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = "true";
                            }
                            ((CheckBox)c).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                        }
                    }
                    else if (c is ComboBox)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                        {
                            ((ComboBox)c).Text = objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember].ToString();
                            ((ComboBox)c).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember].ToString();
                        }
                    }
                    else if (c is UserDT)
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                        {
                            ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                        }
                    }
                    else
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                        {
                            c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                        }
                    }
                    // c.Enabled = true;
                }
                if (c is TabControl)
                {
                    foreach (Control c1 in c.Controls)
                    {
                        if (c1 is TabPage)
                        {
                            foreach (Control c2 in c1.Controls)
                            {
                                if (c2 is Panel)
                                {
                                    foreach (Control c3 in c2.Controls)
                                    {
                                        if (objBASEFILEDS.HTMAIN.ContainsKey(c3.Name) && c3.Visible)
                                        {
                                            if (c3 is CheckBox)
                                            {
                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                {
                                                    if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "0")
                                                    {
                                                        objBASEFILEDS.HTMAIN[c3.Name] = "false";
                                                    }
                                                    if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "1")
                                                    {
                                                        objBASEFILEDS.HTMAIN[c3.Name] = "true";
                                                    }
                                                    ((CheckBox)c3).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                }
                                            }
                                            else if (c3 is PictureBox)
                                            {
                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                {
                                                    ((PictureBox)c3).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                }
                                            }
                                            else if (c3 is ComboBox)
                                            {
                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                {
                                                    ((ComboBox)c3).Text = objBASEFILEDS.HTMAIN[((ComboBox)c3).DisplayMember].ToString();
                                                    ((ComboBox)c3).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c3).ValueMember].ToString();
                                                }
                                            }
                                            else if (c3 is UserDT)
                                            {
                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                {
                                                    ((UserDT)c3).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                }
                                            }
                                            else
                                            {
                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                {
                                                    c3.Text = objBASEFILEDS.HTMAIN[c3.Name].ToString();
                                                }
                                            }
                                            // c3.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txt_Key_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (txt.Tag.ToString() == "decimal")
                {
                    if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
                    {
                        e.Handled = true;
                    }
                    string[] str = txt.Text.Split('.');
                    if (e.KeyChar == '.' && str.Length > 1)
                    {
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
            catch (Exception ex)
            {

            }
        }

        private void String_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F2)
                {
                    PopupTextBox txt = (PopupTextBox)sender;
                    if (txt.Primaryddl != "" && txt.Dispddlfields != "")
                    {
                        frmPopup frmpopup = new frmPopup(objBASEFILEDS.HTMAIN, txt.Tbl_nm, txt.Reftbltran_cd, txt.Primaryddl, txt.Dispddlfields, "Please select", txt.Query_con,txt.IsQcd,txt.QcdCondition, "0");
                        //frmpopup.objCompany = objBASEFILEDS.ObjCompany;
                        //objBASEFILEDS.ObjControlSet = objBASEFILEDS.ObjControlSet;
                        frmpopup.ObjBFD = objBASEFILEDS;
                        frmpopup.ShowDialog();
                        if (txt.PTextName != "")
                        {
                            Control[] txts = this.Controls.Find(txt.PTextName, true);
                            txts[0].Text = objBASEFILEDS.HTMAIN[txt.PTextName].ToString();
                        }
                        else
                        {
                            txt.Text = objBASEFILEDS.HTMAIN[txt.Name].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void btn_popup_click(object sender, EventArgs e)
        {
            try
            {
                PopupButton btn = (PopupButton)sender;
                if (btn != null && btn.frmName != null && btn.frmName != "")
                {
                    objiButtonEvent.ACTIVE_BL = objBASEFILEDS;
                    objiButtonEvent.BlnHeaderOrItem = true;
                    objiButtonEvent.Btn_nm = btn.Name;
                    bool validflg = objiButtonEvent.Button_Click_Event();
                    if (!validflg)
                    {
                        if (objiButtonEvent.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objiButtonEvent.BL_FIELDS.Errormsg, "Button Validation");
                        }
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN = objiButtonEvent.ACTIVE_BL.HTMAIN;
                        BindControlsFromView();
                    }
                }
                else
                {
                    //PopupButton btn = (PopupButton)sender;
                    frmPopup frmpopup = new frmPopup(objBASEFILEDS.HTMAIN, btn.Tbl_nm, btn.Reftbltran_cd, btn.Primaryddl, btn.Dispddlfields, "Please select", btn.Query_con,btn.IsQcd,btn.QcdCondition, "0");
                    //frmpopup.objCompany = objBASEFILEDS.ObjCompany;
                    //frmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                    frmpopup.ObjBFD = objBASEFILEDS;
                    frmpopup.ShowDialog();
                    Control[] txts = this.Controls.Find(btn.PTextName, true);
                    if (txts != null)
                    {
                        txts[0].Text = objBASEFILEDS.HTMAIN[btn.PTextName].ToString();
                        ValidateFields(txts[0].Name, txts[0].Text, "valid_con");
                    }
                    if (tran_mode == "view_mode")
                    {
                        objBASEFILEDS.Tran_id = objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id].ToString();
                        DisplayControlsonMode("view_mode");
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception");
            }
        }
        private void btn_click(object sender, EventArgs e)
        {
            PopupButton btn = (PopupButton)sender;
            if (btn != null && btn.frmName != null && btn.frmName != "")
            {
                if (objBASEFILEDS.Code == "OM")
                {
                    frmORGModule objfrmORGModule = new frmORGModule();
                    objfrmORGModule.ObjBSFD = objBASEFILEDS;
                    objfrmORGModule.ShowDialog();
                }
                else
                {
                    objiButtonEvent.ACTIVE_BL = objBASEFILEDS;
                    objiButtonEvent.BlnHeaderOrItem = true;
                    objiButtonEvent.Btn_nm = btn.Name;
                    bool validflg = objiButtonEvent.Button_Click_Event();
                    if (!validflg)
                    {
                        if (objiButtonEvent.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objiButtonEvent.BL_FIELDS.Errormsg, "Button Validation");
                        }
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN = objiButtonEvent.ACTIVE_BL.HTMAIN;
                        BindControlsFromView();
                    }
                }
            }
            else
            {
                frmAddl_Info frmadd = new frmAddl_Info(objBASEFILEDS.HTMAIN, 0, Tran_cd, Tran_mode, btn.Name, btn.Text);
                //frmadd.Text = btn.Text;
                frmadd.dset = objBASEFILEDS.dsBASEADDIFIELD;
                frmadd.ObjBSFD = objBASEFILEDS;
                frmadd.ShowDialog();

            }
        }
        private void ValidateDefaultCondition(DataRow row, Control c)
        {
            string valu = row["default_con"].ToString();//.Replace("&&", "$&&$").Replace("||", "$||$");
            string[] cond = valu.Split(new string[] { "&&", "||" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in cond)
            {
                string[] cond1 = str.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                if (cond1[0].ToString().Trim().ToLower() == "add_mode")
                {
                    if (this.tran_mode.ToLower() == "add_mode")
                    {
                        c.Enabled = bool.Parse(cond1[1].ToString());
                    }
                    else
                    {
                        c.Enabled = !bool.Parse(cond1[1].ToString());
                    }
                }
                else if (cond1[0].ToString().Trim().ToLower() == "edit_mode")
                {
                    if (this.tran_mode.ToLower() == "edit_mode")
                    {
                        c.Enabled = bool.Parse(cond1[1].ToString());
                    }
                    else
                    {
                        c.Enabled = !bool.Parse(cond1[1].ToString());
                    }
                }
                else if (cond1[0].ToString().Trim().ToLower() == "view_mode")
                {
                    if (this.tran_mode.ToLower() == "view_mode")
                    {
                        c.Enabled = bool.Parse(cond1[1].ToString());
                    }
                    else
                    {
                        c.Enabled = !bool.Parse(cond1[1].ToString());
                    }
                }

                string[] cond2 = str.Split(new string[] { "!=" }, StringSplitOptions.RemoveEmptyEntries);
                if (cond2[0].ToString().Trim().ToLower() == "add_mode")
                {
                    if (this.tran_mode.ToLower() == "add_mode")
                    {
                        c.Enabled = !bool.Parse(cond2[1].ToString());
                    }
                    else
                    {
                        c.Enabled = bool.Parse(cond2[1].ToString());
                    }
                }
                else if (cond2[0].ToString().Trim().ToLower() == "edit_mode")
                {
                    if (this.tran_mode.ToLower() == "edit_mode")
                    {
                        c.Enabled = !bool.Parse(cond2[1].ToString());
                    }
                    else
                    {
                        c.Enabled = bool.Parse(cond2[1].ToString());
                    }
                }
                else if (cond2[0].ToString().Trim().ToLower() == "view_mode")
                {
                    if (this.tran_mode.ToLower() == "view_mode")
                    {
                        c.Enabled = !bool.Parse(cond2[1].ToString());
                    }
                    else
                    {
                        c.Enabled = bool.Parse(cond2[1].ToString());
                    }
                }
            }
        }

        public void DisplayControlsonMode(string tran_mode)
        {
            try
            {
                objBASEFILEDS.Tran_mode = tran_mode;
                switch (tran_mode)
                {
                    case "add_mode":
                        foreach (Control c in this.Controls[1].Controls)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                            {
                                // DataRow[] rows = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0");
                                DataRow[] rows = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + c.Name + "'");
                                foreach (DataRow row in rows)
                                {
                                    if (row["data_ty"].ToString().Trim().ToLower() == "varchar" || row["data_ty"].ToString().Trim().ToLower() == "text")
                                        objBASEFILEDS.HTMAIN[c.Name] = "";
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
                                        objBASEFILEDS.HTMAIN[c.Name] = "0.00";
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "bit")
                                        objBASEFILEDS.HTMAIN[c.Name] = false;
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
                                    {
                                        //((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                        objBASEFILEDS.HTMAIN[c.Name] = DateTime.Now.ToString("yyyy/MM/dd");
                                    }
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "time")
                                    {
                                        objBASEFILEDS.HTMAIN[c.Name] = DateTime.Now.ToLongTimeString();
                                    }
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "int")
                                        objBASEFILEDS.HTMAIN[c.Name] = "0";
                                    else if (row["data_ty"].ToString().Trim().ToLower() == "image")
                                        objBASEFILEDS.HTMAIN[c.Name] = AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png";
                                    if (c is CheckBox)
                                    {
                                        ((CheckBox)c).Checked = false;
                                    }
                                    else if (c is ComboBox)
                                    {
                                        if (((ComboBox)c).SelectedIndex != -1)
                                        {
                                            ((ComboBox)c).SelectedIndex = 0;
                                            objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember] = ((ComboBox)c).SelectedText;
                                            objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember] = ((ComboBox)c).SelectedValue;
                                        }
                                    }
                                    else if (c is PictureBox)
                                    {
                                        ((PictureBox)c).BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png");
                                    }
                                    else
                                    {
                                        c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                                        if (c.Name.ToUpper() == "FIN_YR_STA")
                                        {
                                            if (DateTime.Now.Month < 4)
                                            {
                                                ((UserDT)c).DtValue = DateTime.Parse("01/04/" + DateTime.Now.AddYears(-1).Year.ToString());
                                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = ((UserDT)c).DtValue.ToString("yyyy/MM/dd");
                                            }
                                            else
                                            {
                                                ((UserDT)c).DtValue = DateTime.Parse("01/04/" + DateTime.Now.Year.ToString());
                                                objBASEFILEDS.HTMAIN[c.Name] = ((UserDT)c).DtValue.ToString("yyyy/MM/dd");
                                            }
                                        }
                                    }
                                    c.Enabled = true;
                                    if (row["order_no"].ToString() == "1")
                                    {
                                        c.Select();
                                    }
                                }
                            }
                            DataRow[] rowsdefault = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0 and default_con<>''");
                            foreach (DataRow row in rowsdefault)
                            {
                                ValidateDefaultCondition(row, c);
                            }
                            if (c is TabControl)
                            {
                                foreach (Control c1 in c.Controls)
                                {
                                    if (c1 is TabPage)
                                    {
                                        objBASEFILEDS.dsTabDet = objFLMAST.GETMASTBASETABDETAILS(Tran_cd, c1.Name, objBASEFILEDS.ObjCompany.Compid.ToString());
                                        foreach (Control c2 in c1.Controls)
                                        {
                                            if (c2 is Panel)
                                            {
                                                foreach (Control c3 in c2.Controls)
                                                {
                                                    if (objBASEFILEDS.HTMAIN.ContainsKey(c3.Name))
                                                    {
                                                        DataRow[] rows = objBASEFILEDS.dsTabDet.Tables[0].Select("fld_nm='" + c3.Name + "'");
                                                        foreach (DataRow rowtab in rows)
                                                        {
                                                            if (rowtab["data_ty"].ToString().Trim().ToLower() == "varchar" || rowtab["data_ty"].ToString().Trim().ToLower() == "text")
                                                                objBASEFILEDS.HTMAIN[c3.Name] = "";
                                                            else if (rowtab["data_ty"].ToString().Trim().ToLower() == "decimal")
                                                                objBASEFILEDS.HTMAIN[c3.Name] = "0.00";
                                                            else if (rowtab["data_ty"].ToString().Trim().ToLower() == "bit")
                                                                objBASEFILEDS.HTMAIN[c3.Name] = false;
                                                            else if (rowtab["data_ty"].ToString().Trim().ToLower() == "datetime")
                                                            {
                                                                // ((UserDT)c3).CustomFormat = "dd-MMM-yyyy";
                                                                objBASEFILEDS.HTMAIN[c3.Name] = DateTime.Now.ToString("yyyy/MM/dd");
                                                            }
                                                            else if (rowtab["data_ty"].ToString().Trim().ToLower() == "time")
                                                            {
                                                                objBASEFILEDS.HTMAIN[c3.Name] = DateTime.Now.ToLongTimeString();
                                                            }
                                                            else if (rowtab["data_ty"].ToString().Trim().ToLower() == "int")
                                                                objBASEFILEDS.HTMAIN[c3.Name] = "0";

                                                            if (c3 is CheckBox)
                                                            {
                                                                ((CheckBox)c3).Checked = false;
                                                            }
                                                            else if (c3 is ComboBox)
                                                            {
                                                                if (((ComboBox)c3).SelectedIndex != -1)
                                                                {
                                                                    ((ComboBox)c3).SelectedIndex = 0;
                                                                    objBASEFILEDS.HTMAIN[((ComboBox)c3).DisplayMember] = ((ComboBox)c3).SelectedText;
                                                                    objBASEFILEDS.HTMAIN[((ComboBox)c3).ValueMember] = ((ComboBox)c3).SelectedValue;
                                                                }
                                                            }
                                                            else if (c3 is PictureBox)
                                                            {
                                                                ((PictureBox)c3).BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png");
                                                            }
                                                            else if (c3 is UserDT)
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    ((UserDT)c3).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                                {
                                                                    c3.Text = objBASEFILEDS.HTMAIN[c3.Name].ToString();
                                                                }
                                                            }
                                                            c3.Enabled = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        objADDTRANSANDMASTER.ACTIVE_BL = objBASEFILEDS;
                        bool flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                        if (flgValid)
                        {
                            objBASEFILEDS.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            BindControlsFromView();
                        }
                        else
                        {
                            if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Add");
                            }
                        }
                        break;
                    case "edit_mode":
                        objBASEFILEDS.dsetview = new DataSet();
                        objBASEFILEDS.dsetview = objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);

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
                        foreach (Control c in this.Controls[1].Controls)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                            {
                                if (c is PictureBox)
                                {
                                    if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                    {
                                        ((PictureBox)c).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                    }
                                }
                                else if (c is CheckBox)
                                {
                                    if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "0")
                                        {
                                            objBASEFILEDS.HTMAIN[c.Name] = "false";
                                        }
                                        if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "1")
                                        {
                                            objBASEFILEDS.HTMAIN[c.Name] = "true";
                                        }
                                        ((CheckBox)c).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                    }
                                }
                                else if (c is UserDT)
                                {
                                    if (((UserDT)c).Tag.ToString() != "time")
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            // ((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                            ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());//.ToString("yyyy/MM/dd");
                                        }
                                    }
                                    else
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToLongTimeString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (objBASEFILEDS.HTMAIN[c.Name] != null)
                                    {
                                        c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                                    }
                                }
                                c.Enabled = true;
                            }

                            DataRow[] rowsdefault = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0 and default_con<>''");
                            foreach (DataRow row in rowsdefault)
                            {
                                ValidateDefaultCondition(row, c);
                            }

                            if (c is TabControl)
                            {
                                foreach (Control c1 in c.Controls)
                                {
                                    if (c1 is TabPage)
                                    {
                                        foreach (Control c2 in c1.Controls)
                                        {
                                            if (c2 is Panel)
                                            {
                                                foreach (Control c3 in c2.Controls)
                                                {
                                                    if (objBASEFILEDS.HTMAIN.ContainsKey(c3.Name))
                                                    {
                                                        if (c3 is CheckBox)
                                                        {
                                                            if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "0")
                                                                {
                                                                    objBASEFILEDS.HTMAIN[c3.Name] = "false";
                                                                }
                                                                if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "1")
                                                                {
                                                                    objBASEFILEDS.HTMAIN[c3.Name] = "true";
                                                                }
                                                                ((CheckBox)c3).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                            }
                                                        }
                                                        else if (c3 is UserDT)
                                                        {
                                                            if (((UserDT)c3).Tag.ToString() != "time")
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    // ((UserDT)c3).CustomFormat = "dd-MMM-yyyy";
                                                                    ((UserDT)c3).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());//.ToString("yyyy/MM/dd");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    c3.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString()).ToLongTimeString();
                                                                }
                                                            }
                                                        }
                                                        else if (c3 is PictureBox)
                                                        {
                                                            if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                            {
                                                                ((PictureBox)c3).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                            {
                                                                c3.Text = objBASEFILEDS.HTMAIN[c3.Name].ToString();
                                                            }
                                                        }
                                                        c3.Enabled = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        objEditTRANSANDMASTER.ACTIVE_BL = objBASEFILEDS;
                        flgValid = objEditTRANSANDMASTER.tsEditTransactionAndMaster();
                        if (flgValid)
                        {
                            objBASEFILEDS.HTMAIN = objEditTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                            BindControlsFromView();
                        }
                        else
                        {
                            if (objEditTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                            {
                                AutoClosingMessageBox.Show(objEditTRANSANDMASTER.BL_FIELDS.Errormsg, "Edit");
                            }
                        }
                        break;
                    case "view_mode":
                        objBASEFILEDS.dsetview = new DataSet();
                        objBASEFILEDS.dsetview = objFLTransaction.GET_MASTER_DATA(objBASEFILEDS);
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
                        foreach (Control c in this.Controls[1].Controls)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                            {
                                if (objBASEFILEDS.Tran_id.ToString() != "0")
                                {
                                    if (c is PictureBox)
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            ((PictureBox)c).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                        }
                                    }
                                    else if (c is CheckBox)
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "0")
                                            {
                                                objBASEFILEDS.HTMAIN[c.Name] = "false";
                                            }
                                            if (objBASEFILEDS.HTMAIN[c.Name].ToString() == "1")
                                            {
                                                objBASEFILEDS.HTMAIN[c.Name] = "true";
                                            }
                                            ((CheckBox)c).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                        }
                                    }
                                    else if (c is ComboBox)
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            ((ComboBox)c).SelectedItem = objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember].ToString();
                                            ((ComboBox)c).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember].ToString();
                                        }
                                    }
                                    else if (c is UserDT)
                                    {
                                        if (((UserDT)c).Tag.ToString() != "time")
                                        {
                                            if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                            {
                                                //((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                                ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());//.ToString("yyyy/MM/dd");
                                            }
                                        }
                                        else
                                        {
                                            if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                            {
                                                c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToLongTimeString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                                        {
                                            c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (c is PictureBox)
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                        {
                                            ((PictureBox)c).BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png");
                                        }
                                    }
                                    else if (c is ComboBox)
                                    {
                                        if (((ComboBox)c).SelectedIndex != -1)
                                        {
                                            ((ComboBox)c).SelectedIndex = 0;
                                        }
                                    }
                                    else if (c is UserDT)
                                    {
                                        if (((UserDT)c).Tag.ToString() != "time")
                                        {
                                            ((UserDT)c).CustomFormat = " ";
                                        }
                                    }
                                    else if (c is CheckBox)
                                    {
                                        ((CheckBox)c).Checked = false;
                                    }
                                    else
                                    {
                                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                                        {
                                            c.Text = "";
                                        }
                                    }
                                }
                                if (!(c is Label)) c.Enabled = false;
                            }
                            DataRow[] rowsdefault = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("fld_nm='" + c.Name + "' and data_ty='button' and inter_val=0 and default_con<>''");
                            foreach (DataRow row in rowsdefault)
                            {
                                ValidateDefaultCondition(row, c);
                            }
                            if (c is TabControl)
                            {
                                foreach (Control c1 in c.Controls)
                                {
                                    if (c1 is TabPage)
                                    {
                                        foreach (Control c2 in c1.Controls)
                                        {
                                            if (c2 is Panel)
                                            {
                                                foreach (Control c3 in c2.Controls)
                                                {
                                                    if (objBASEFILEDS.HTMAIN.ContainsKey(c3.Name))
                                                    {
                                                        if (objBASEFILEDS.Tran_id.ToString() != "0")
                                                        {
                                                            if (c3 is CheckBox)
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "0")
                                                                    {
                                                                        objBASEFILEDS.HTMAIN[c3.Name] = "false";
                                                                    }
                                                                    if (objBASEFILEDS.HTMAIN[c3.Name].ToString() == "1")
                                                                    {
                                                                        objBASEFILEDS.HTMAIN[c3.Name] = "true";
                                                                    }
                                                                    ((CheckBox)c3).Checked = bool.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                                }
                                                            }
                                                            else if (c3 is UserDT)
                                                            {
                                                                if (((UserDT)c3).Tag.ToString() != "time")
                                                                {
                                                                    if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                    {
                                                                        ((UserDT)c3).CustomFormat = "dd-MMM-yyyy";
                                                                        c3.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString()).ToString("yyyy/MM/dd");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                    {
                                                                        c3.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c3.Name].ToString()).ToLongTimeString();
                                                                    }
                                                                }
                                                            }
                                                            else if (c3 is PictureBox)
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    ((PictureBox)c3).BackgroundImage = Image.FromFile(objBASEFILEDS.HTMAIN[c3.Name].ToString());
                                                                }
                                                            }
                                                            else if (c3 is ComboBox)
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    ((ComboBox)c3).SelectedItem = objBASEFILEDS.HTMAIN[((ComboBox)c3).DisplayMember].ToString();
                                                                    ((ComboBox)c3).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c3).ValueMember].ToString();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                                {
                                                                    c3.Text = objBASEFILEDS.HTMAIN[c3.Name].ToString();
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (c3 is PictureBox)
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                                {
                                                                    ((PictureBox)c3).BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"\IMAGE\window_add.png");
                                                                }
                                                            }
                                                            else if (c3 is ComboBox)
                                                            {
                                                                if (((ComboBox)c3).SelectedIndex != -1)
                                                                {
                                                                    ((ComboBox)c3).SelectedIndex = 0;
                                                                }
                                                            }
                                                            else if (c3 is UserDT)
                                                            {
                                                                if (((UserDT)c3).Tag.ToString() != "time")
                                                                {
                                                                    ((UserDT)c3).CustomFormat = " ";
                                                                }
                                                            }
                                                            else if (c3 is CheckBox)
                                                            {
                                                                ((CheckBox)c3).Checked = false;
                                                            }
                                                            else
                                                            {
                                                                if (objBASEFILEDS.HTMAIN[c3.Name] != null)
                                                                {
                                                                    c3.Text = "";
                                                                }
                                                            }
                                                        }
                                                        c3.Enabled = false;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Button_visibility();
                        break;
                    default: break;
                }

                DefaultControlValidation();//1.0
                iInit.ActiveFrm = this;
                objiCustominit.ACTIVE_BL = objBASEFILEDS;
                objiCustominit.Load_Init_Details();
            }
            catch (Exception ex)
            {
            }
        }
        #region 1.0
        private void DefaultControlValidation()
        {
            bool validflg = true;
            if (objDefaultControl.GetType().GetMethod("DefaultControl") != null)
            {
                objDefaultControl.ACTIVE_BL = objBASEFILEDS;
                // SetFieldsValue(tbl_nm, fld_nm, fld_val);
                MethodInfo methodInfo = typeof(iDefaultControl).GetMethod("DefaultControl");
                validflg = bool.Parse(methodInfo.Invoke(objDefaultControl, null).ToString().Trim());
                if (!validflg)
                {
                    if (objDefaultControl.BL_FIELDS.Errormsg.Length != 0)
                    {
                        AutoClosingMessageBox.Show(objDefaultControl.BL_FIELDS.Errormsg, "Default");
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter Valid Details", "Valid Details");
                    }
                }
                else
                {
                    objBASEFILEDS.HTMAIN = objDefaultControl.ACTIVE_BL.HTMAIN;
                    objBASEFILEDS.HTITEM = objDefaultControl.ACTIVE_BL.HTITEM;
                    BindControlsFromView();
                }
            }
        }
        #endregion 1.0
        private void Button_visibility()
        {
            if (this.tran_mode == "view_mode")
            {
                DataRow[] rows = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("data_ty='BUTTON'");
                foreach (DataRow row in rows)
                {
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (c.Name == row["fld_nm"].ToString())
                            c.Enabled = true;
                    }
                }
            }
            else
            {
                //DataRow[] rows = objBASEFILEDS.dsBASEFIELDMAIN.Tables[0].Select("data_ty='BUTTON' and parent_ctrl<>''");
                //foreach (DataRow row in rows)
                //{
                //    foreach (Control c in this.Controls[1].Controls)
                //    {
                //        if (c.Name == row["fld_nm"].ToString())
                //            if (!(c is Label)) c.Enabled = false;
                //    }
                //}
            }
        }
        private void BindTabPage()
        {
            //tabControl.Bounds = new Rectangle(10, hgt + this.ClientSize.Height / 20,  Screen.PrimaryScreen.WorkingArea.Width - ( Screen.PrimaryScreen.WorkingArea.Width * 2 / 100), this.ClientSize.Height - (hgt + this.ClientSize.Height / 20));
            tabControl.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt + this.ClientSize.Height / 20, this.ClientSize.Width - (ctrlwid / 2) * 3 / 100, this.ClientSize.Height - (hgt + this.ClientSize.Height / 20));
            tabControl.Selected += new TabControlEventHandler(tab_selected);
            tabControl.KeyUp += new KeyEventHandler(tab_KeyUp);
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

            objBASEFILEDS.htitem_details.Clear();

            objBASEFILEDS.dsTab = objFLMAST.GETMASTBASETABFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
            foreach (DataRow row in objBASEFILEDS.dsTab.Tables[0].Rows)
            {
                TabPage tabPage = new TabPage();
                tabPage.Text = " " + row["head_nm"].ToString() + "    ";
                tabPage.Name = row["fld_nm"].ToString();
                tabPage.BorderStyle = BorderStyle.Fixed3D;
                // tabPage.Bounds = new Rectangle(0, 10, tabControl.Size.Width - (tabControl.Size.Width * 10 / 100), tabControl.Size.Height - (tabControl.Size.Height * 10 / 100));
                tabPage.Bounds = new Rectangle((tabControl.Size.Width * 2 / 100), 10, tabControl.Size.Width - (tabControl.Size.Width * 2 / 100), tabControl.Size.Height - (tabControl.Size.Height * 10 / 100));

                ctrlhgt = this.ClientSize.Height * 5 / 100; //tabPage.Size.Height * 15 / 100;
                hgt = 0;
                ctrlwid = tabPage.Size.Width * 50 / 100;
                wid = tabPage.Width / 2;

                _lblwid = (ctrlwid / 2) * 60 / 100;
                _ctrl_wid = wid * 55 / 100;

                Panel objpnl = new Panel();
                objpnl.BorderStyle = BorderStyle.FixedSingle;
                objpnl.Dock = DockStyle.Fill;
                //objpnl.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color);
                //objpnl.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color);

                objBASEFILEDS.dsTabDet = objFLMAST.GETMASTBASETABDETAILS(Tran_cd, row["fld_nm"].ToString(), objBASEFILEDS.ObjCompany.Compid.ToString());

                foreach (DataRow rowtab in objBASEFILEDS.dsTabDet.Tables[0].Rows)
                {
                    Bind_Tab_Page_Control(rowtab, objpnl);
                    objBASEFILEDS.htitem_details.Add(rowtab["fld_nm"].ToString().Trim().ToUpper(), "");
                }
                //tabPage.BackColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color);
                //tabPage.ForeColor = Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color);
                tabPage.Controls.Add(objpnl);
                tabControl.Controls.Add(tabPage);
                //  tabControl.Controls[tabPage.Name].Visible = false;
                if (bool.Parse(row["inter_val"].ToString()))
                {
                    tabControl.TabPages.Remove(tabPage);// tabPage.Hide();
                }
            }
            tabControl.DrawItem += new DrawItemEventHandler(tabcontrol_drawitem);
            tabControl.SelectedIndex = 0;
            pnlform.Controls.Add(tabControl);
        }

        private void tabcontrol_drawitem(object sender, DrawItemEventArgs e)
        {
            string tabName = "  " + tabControl.TabPages[e.Index].Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Far;
            tabControl.Font = new Font(objBASEFILEDS.ObjControlSet.Tab_family != null ? objBASEFILEDS.ObjControlSet.Tab_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Tab_size != null ? objBASEFILEDS.ObjControlSet.Tab_size : "9"));
            Font font = new Font(tabControl.Font, FontStyle.Bold);
            SolidBrush brushhead = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_head_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_head_color) : Color.Maroon);
            SolidBrush brush = new SolidBrush(objBASEFILEDS.ObjControlSet.Tab_font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color) : Color.Black);

            //Find if it is selected, this one will be hightlighted...
            if (e.Index == tabControl.SelectedIndex)
                e.Graphics.FillRectangle(brushhead, e.Bounds);

            e.Graphics.DrawString(tabName, font, brush, tabControl.GetTabRect(e.Index), stringFormat);
        }

        private void tab_selected(object sender, TabControlEventArgs e)
        {
        }
        private void Bind_Tab_Page_Control(DataRow row, Panel pnl)
        {
            count = int.Parse(row["order_no"].ToString());
            if (count % 2 != 0)
            {
                if (count != 1)
                    hgt += ctrlhgt;
                else
                    hgt += 5;
            }

            Label objlable = new Label();
            // objlable.AutoSize = true;
            objlable.Name = "lbl" + row["head_nm"].ToString();
            objlable.Text = row["head_nm"].ToString();
            // objlable.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable.Visible = !bool.Parse(row["inter_val"].ToString());
            // objlable.TextAlign = ContentAlignment.TopRight;

            Label objlable1 = new Label();
            // objlable1.AutoSize = true;
            objlable1.Name = "lbl1" + row["head_nm"].ToString().Trim();
            objlable1.Text = bool.Parse(row["mandatory"].ToString().Trim()) ? "*" : "";
            objlable1.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable1.Visible = !bool.Parse(row["inter_val"].ToString().Trim());

            if (!bool.Parse(row["inter_val"].ToString()))// && !bool.Parse(row["_mul"].ToString())
            {
                if (count % 2 == 0)
                {
                    objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + wid, hgt, _lblwid, ctrlhgt);
                    objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + wid + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                }
                else
                {
                    objlable.Bounds = new Rectangle(0, hgt, _lblwid, ctrlhgt);
                    objlable1.Bounds = new Rectangle(0 + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                }
            }
            pnl.Controls.Add(objlable);
            pnl.Controls.Add(objlable1);
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                if (bool.Parse(row["_mul"].ToString()))
                {
                    ComboBox cmdseries = new ComboBox();
                    cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdseries.Name = row["fld_nm"].ToString().Trim();
                    cmdseries.Tag = row["data_ty"].ToString().Trim().ToLower();
                    DataSet dscmdseries = objFLGENINV.GET_TBL_VAL(row["tbl_nm"].ToString(), this.tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                    cmdseries.DataSource = dscmdseries.Tables[0];
                    cmdseries.DisplayMember = row["sel_item"].ToString();
                    cmdseries.ValueMember = row["sel_val"].ToString();
                    cmdseries.Update();
                    objBASEFILEDS.HTMAIN[row["sel_item"].ToString().Trim()] = "";
                    objBASEFILEDS.HTMAIN[row["sel_val"].ToString().Trim()] = "0";
                    if (dscmdseries != null && dscmdseries.Tables[0].Rows.Count != 0)
                    {
                        if (!bool.Parse(row["inter_val"].ToString()))
                        {
                            if (count % 2 == 0)
                            {
                                objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + wid, hgt, _lblwid, ctrlhgt);
                                objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + wid + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + wid + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt);
                            }
                            else
                            {
                                objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, _lblwid, ctrlhgt);
                                objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + objlable.Width, hgt, _lblwid * 10 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt);
                            }
                        }
                        cmdseries.Visible = !bool.Parse(row["inter_val"].ToString());
                        cmdseries.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                        cmdseries.Validating += new CancelEventHandler(tab_cmd_validate);

                        cmdseries.Enter -= new EventHandler(cmdseriesenter);
                        cmdseries.Enter += new EventHandler(cmdseriesenter);
                        cmdseries.Leave -= new EventHandler(cmdseriesleave);
                        cmdseries.Leave += new EventHandler(cmdseriesleave);
                        pnl.Controls.Add(cmdseries);
                    }
                    else
                    {
                        objlable.Visible = false;
                    }
                }
                else
                {
                    objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";

                    TextBox objtxt = new TextBox();
                    objtxt.Name = row["fld_nm"].ToString().Trim();
                    objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                    if (objtxt.Name.Trim().ToLower() == "tran_cd")
                    {
                        objBASEFILEDS.HTMAIN[objtxt.Name] = Tran_cd;
                    }
                    if (!bool.Parse(row["inter_val"].ToString()))
                    {
                        if (count % 2 == 0)
                        {
                            objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt);
                        }
                        else
                        {
                            objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt);
                        }
                    }
                    objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                    objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                    objtxt.Validating += new CancelEventHandler(tab_String_Validating);
                    //  objtxt.LostFocus += new EventHandler(String_LostFocus);

                    objtxt.Enter -= new EventHandler(txtenter);
                    objtxt.Enter += new EventHandler(txtenter);
                    objtxt.Leave -= new EventHandler(txtleave);
                    objtxt.Leave += new EventHandler(txtleave);
                    pnl.Controls.Add(objtxt);
                }
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt + ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt + ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(tab_Int_Validating);
                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);

                objtxt.Enter -= new EventHandler(int_enter);
                objtxt.Enter += new EventHandler(int_enter);
                objtxt.Leave -= new EventHandler(int_leave);
                objtxt.Leave += new EventHandler(int_leave);
                pnl.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "text")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Multiline = true;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid, ctrlhgt + ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid, ctrlhgt + ctrlhgt);
                    }
                    hgt = ctrlhgt + ctrlhgt;
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(tab_Text_Validating);
                //  objtxt.LostFocus += new EventHandler(String_LostFocus);

                objtxt.Enter -= new EventHandler(Text_enter);
                objtxt.Enter += new EventHandler(Text_enter);
                objtxt.Leave -= new EventHandler(Text_leave);
                objtxt.Leave += new EventHandler(Text_leave);
                pnl.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0.00";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(tab_Decimal_Validating);
                //  objtxt.LostFocus += new EventHandler(Decimal_LostFocus);
                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);

                objtxt.Enter -= new EventHandler(Decimal_enter);
                objtxt.Enter += new EventHandler(Decimal_enter);
                objtxt.Leave -= new EventHandler(Decimal_leave);
                objtxt.Leave += new EventHandler(Decimal_leave);

                pnl.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "bit")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "false";
                CheckBox objchk = new CheckBox();
                objchk.Name = row["fld_nm"].ToString().Trim();
                objchk.Tag = row["data_ty"].ToString().Trim().ToLower();
                //objchk.TextAlign = ContentAlignment.TopLeft;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objchk.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objchk.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                objchk.Visible = !bool.Parse(row["inter_val"].ToString());
                objchk.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                objchk.Validating += new CancelEventHandler(tab_CheckBox_Validating);
                // objchk.LostFocus += new EventHandler(CheckBox_LostFocus);

                objchk.Enter -= new EventHandler(CheckBox_enter);
                objchk.Enter += new EventHandler(CheckBox_enter);
                objchk.Leave -= new EventHandler(CheckBox_leave);
                objchk.Leave += new EventHandler(CheckBox_leave);
                pnl.Controls.Add(objchk);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = DateTime.Now.ToString("yyyy/MM/dd");
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                dtp.CustomFormat = " ";
                dtp.Format = DateTimePickerFormat.Custom;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                }
                dtp.Visible = !bool.Parse(row["inter_val"].ToString());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Validating += new CancelEventHandler(tab_dateTimePicker1_Validating);
                // dtp.LostFocus += new EventHandler(dateTimePicker_LostFocus);

                dtp.Enter -= new EventHandler(dateTimePicker1_enter);
                dtp.Enter += new EventHandler(dateTimePicker1_enter);
                dtp.Leave -= new EventHandler(dateTimePicker1_leave);
                dtp.Leave += new EventHandler(dateTimePicker1_leave);
                pnl.Controls.Add(dtp);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "time")
            {
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Format = DateTimePickerFormat.Time;
                dtp.CustomFormat = "HH:mm";
                dtp.Visible = !bool.Parse(row["inter_val"].ToString());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width + wid, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + _lblwid + objlable1.Width, hgt, _ctrl_wid * 70 / 100, ctrlhgt);
                    }
                    dtp.Validating += new CancelEventHandler(tab_dateTimePicker1_Time_Validating);

                    dtp.Enter -= new EventHandler(dateTimePicker1_Time_enter);
                    dtp.Enter += new EventHandler(dateTimePicker1_Time_enter);
                    dtp.Leave -= new EventHandler(dateTimePicker1_Time_leave);
                    dtp.Leave += new EventHandler(dateTimePicker1_Time_leave);
                }
                pnl.Controls.Add(dtp);
            }
        }
        private void frm_mast_item_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                //  objInit.ActiveFrm = this;
                iInit.ActiveFrm = this;
                ((frm_mainmenu)this.MdiParent).MasterChildWindowActivate(this);// refreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void tab_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                //Control[] cntrl = this.Controls.Find("this", true);
                //cntrl[0].Select();
            }
        }
        private void frm_mast_item_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (e.KeyData == Keys.Down)
            //    {
            //        Control[] cntrl = this.Controls.Find("tabControl", true);
            //        if (cntrl != null && cntrl.Length == 1)
            //        {
            //            cntrl[0].Select();
            //        }
            //    }
        }
        public void toolbar_clicked()
        {
            foreach (Control c in this.Controls[1].Controls)
            {
                if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name) && c.Visible == true)
                {
                    if (c is PictureBox)
                    {
                        //objBASEFILEDS.HTMAIN[c.Name] = ((PictureBox)c).ImageLocation;
                    }
                    else if (c is CheckBox)
                    {
                        objBASEFILEDS.HTMAIN[c.Name] = ((CheckBox)c).Checked;
                    }
                    else if (c is ComboBox)
                    {
                        objBASEFILEDS.HTMAIN[((ComboBox)c).Name] = ((ComboBox)c).Text;
                        objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember] = ((ComboBox)c).Text;
                        objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember] = ((ComboBox)c).SelectedValue;
                    }
                    else if (c is UserDT)
                    {
                        objBASEFILEDS.HTMAIN[c.Name] = ((UserDT)c).DtValue.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN[c.Name] = c.Text;
                    }
                    // c.Enabled = true;
                }
                else
                {
                    if (c is CheckBox)
                    {
                        objBASEFILEDS.HTMAIN[c.Name] = false;
                    }
                    else if (c is UserDT)
                    {
                        objBASEFILEDS.HTMAIN[c.Name] = ((UserDT)c).DtValue.ToString("yyyy/MM/dd");
                    }
                }

                if (c is TabControl)
                {
                    foreach (Control c1 in c.Controls)
                    {
                        if (c1 is TabPage)
                        {
                            foreach (Control c2 in c1.Controls)
                            {
                                if (c2 is Panel)
                                {
                                    foreach (Control c3 in c2.Controls)
                                    {
                                        if (objBASEFILEDS.HTMAIN.ContainsKey(c3.Name))
                                        {
                                            if (c3.Visible == true)
                                            {
                                                if (c3 is CheckBox)
                                                {
                                                    if (objBASEFILEDS.HTMAIN[c3.Name] != null && objBASEFILEDS.HTMAIN[c3.Name].ToString() != "")
                                                    {
                                                        objBASEFILEDS.HTMAIN[c3.Name] = ((CheckBox)c3).Checked;
                                                    }
                                                }
                                                else if (c3 is ComboBox)
                                                {
                                                    objBASEFILEDS.HTMAIN[((ComboBox)c3).Name] = ((ComboBox)c3).Text;
                                                    objBASEFILEDS.HTMAIN[((ComboBox)c3).DisplayMember] = ((ComboBox)c3).Text;
                                                    objBASEFILEDS.HTMAIN[((ComboBox)c3).ValueMember] = ((ComboBox)c3).SelectedValue;
                                                }
                                                else if (c3 is PictureBox)
                                                {
                                                    // objBASEFILEDS.HTMAIN[c3.Name] = ((PictureBox)c3).ImageLocation;
                                                }
                                                else if (c3 is UserDT)
                                                {
                                                    objBASEFILEDS.HTMAIN[c3.Name] = ((UserDT)c3).DtValue.ToString("yyyy/MM/dd");
                                                }
                                                else
                                                {
                                                    objBASEFILEDS.HTMAIN[c3.Name] = c3.Text;
                                                }
                                            }
                                            else
                                            {
                                                if (c3 is CheckBox)
                                                {
                                                    objBASEFILEDS.HTMAIN[c3.Name] = false;
                                                }
                                                else if (c3 is UserDT)
                                                {
                                                    objBASEFILEDS.HTMAIN[c3.Name] = ((UserDT)c3).DtValue.ToString("yyyy/MM/dd");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void frm_mast_item_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                // objInit.ActiveFrm = this;
                iInit.ActiveFrm = this;
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseChildWindow(0);
            }
        }

        private void frm_mast_item_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
