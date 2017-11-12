using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using iMANTRA_IL;
using CUSTOM_iMANTRA;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using iMANTRA_BL;

namespace iMANTRA
{
    public partial class ifrm_Accounting : BaseClass
    {
        /***********************************************************************************************
         * Sharanamma Jekeen Inode Technologies Pvt. Ltd.
         * 1.0 on 11.22.13 Main Tax Calculation for add & edit also
         * 2.0 Sharanamma Jekeen on 11.26.13 ==> Added new Class in Custom Layer
         * 3.0 Sharanamm Jekeen on 12.04.13 ==> File Upload Button in Header/ItemWise
         * Expense Purchase	EX
            Bank Payment	BP
            Bank Receipt	BR
            Cash Payment	CA
            Cash receipt	CR
            Journal voucher	JV
            Debit note	DN
            Credit note	CN
            Service tax bill	SB
            Petty Cash	TP
            Bank Re-conciliation	BN
         * 
         * 
         * 
         * *********************************************************************************************/
        private iInit objInit = new iInit();

        private int _width = 0, _lblGridHeaderWidth = 0;

        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();

        private FL_BASEFIELD objFL_BASEFIELD = new FL_BASEFIELD();
        private FL_TRANSACTION objFL_Transaction = new FL_TRANSACTION();
        private FL_GRIDEVENTS objFL_GRIDEVENTS = new FL_GRIDEVENTS();
        private FL_GEN_INVOICE objFL_GEN_INV = new FL_GEN_INVOICE();
        private FL_MAST objFLValidate = new FL_MAST();
        private FL_PICKUP objPickup = new FL_PICKUP();
        private FL_GENERAL objFLGeneral = new FL_GENERAL();

        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();
        SetFieldsValue objSetFieldsValue = new SetFieldsValue();

        iCustomInit objiCustominit = new iCustomInit();
        iTRANSACTION objiTRANSACTION = new iTRANSACTION();
        iITEMVALID objiITEMVALID = new iITEMVALID();
        iQTYVALID objiQTYVALID = new iQTYVALID();
        iGRIDITEM objiGRIDITEM = new iGRIDITEM();
        btn_event objiButtonEvent = new btn_event();//Sharanamma on 04.24.13,rg: btn click events
        AddNewItem objiAddNewItem = new AddNewItem();
        DeleteItem objiDeleteItem = new DeleteItem();
        AddTransactionAndMaster objADDTRANSANDMASTER = new AddTransactionAndMaster();
        EditTransactionAndMaster objEditTRANSANDMASTER = new EditTransactionAndMaster();
        iDefaultControl objDefaultControl = new iDefaultControl();//2.0
        iCELL objCustomCell = new iCELL();//cell wise calculation

        //private MyDataGridView grid = new MyDataGridView();
        //private MyDataGridView gridTax = new MyDataGridView();
        private MyDataGridView gridAccount = new MyDataGridView();
        private TabControl tabControl = new TabControl();
        private Panel pnlTaxGrid = new Panel();
        private Panel pnlTax = new Panel();
        private Panel pnlAccountGrid = new Panel();
        private TextBox txtTaxBox;
        private Label lblTax;
        Label lbl = new Label();
        Label objlableGrid = new Label();

        ContextMenuStrip PopupMenu = new ContextMenuStrip();

        ContextMenuStrip PopupMenuAccount = new ContextMenuStrip();

        private string tran_mode = "view_mode", tran_cd, tran_id = "0", _strCMOrPROD;
        private bool allow_excise_calc = false; //sharanamma datetime 26.06.13

        string ac_nm = "";

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

        private int count = 1, itserial = 0, ctrlhgt = 0, hgt = 0, ctrlwid = 0, wid = 0;
        private decimal subtotal = 0, subtotal_tax = 0;

        string rows_no = "0";
        decimal tot_amt = 0;

        bool flgDt = true;
        bool flgAccount = false;

        public ifrm_Accounting(BL_BASEFIELD objBL)
        {
            InitializeComponent();
            this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            if (objBASEFILEDS.HTMAIN.Contains(objBL.Primary_id.ToString()))
            {
                objBASEFILEDS.HTMAIN[objBL.Primary_id.ToString()] = this.tran_id.ToString();
            }
            this.objBASEFILEDS = objBL;
        }

        private void ifrm_Accounting_Load(object sender, EventArgs e)
        {
            objlableGrid.Text = "Press F2 to get List";
            objlableGrid.ForeColor = Color.Red;
            objlableGrid.Visible = false;

            lbl.ForeColor = Color.Black;
            //objFL_BASEFIELD.objCompany = objBASEFILEDS.ObjCompany;
            //objFL_Transaction.objCompany = objBASEFILEDS.ObjCompany;
            //objFL_GEN_INV.objCompany = objBASEFILEDS.ObjCompany;
            //objFLValidate.objCompany = objBASEFILEDS.ObjCompany;
            //objPickup.objCompany = objBASEFILEDS.ObjCompany;

            this.Dock = DockStyle.Fill;
            ctrlhgt = this.ClientSize.Height * 5 / 100;//this.ClientSize.Height * 5 / 100;
            hgt = 0;
            ctrlwid = this.ClientSize.Width * 50 / 100;
            wid = this.ClientSize.Width / 2;

            // MessageBox.Show(objFL_GRIDEVENTS.InfixToPostfix("(5+6)*9+100"));
            objBASEFILEDS.HTMAIN.Clear();
            try
            {
                //  this.ClientSize = new Size(this.ParentForm.Width - 20, this.ParentForm.Height - 20);//new Size(this.MdiParent.Width - 4, this.MdiParent.Height - 4);
                objBASEFILEDS.dsHeader = objFL_BASEFIELD.GETBASEFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                foreach (DataRow row in objBASEFILEDS.dsHeader.Tables[0].Rows)
                {
                    if (!bool.Parse(row["ctrl_not_show"].ToString().Trim()))
                    {
                        objBASEFILEDS.HTMAIN.Add(row["fld_nm"].ToString().Trim().ToUpper(), "");
                    }
                }
                DataRow[] rowtops = objBASEFILEDS.dsHeader.Tables[0].Select("_top=1");
                string sqlQuery = "select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,_pickup,_top,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from ibasefields where code='" + Tran_cd + "' and typewise=1 and _top=1 union all select [type],typewise,code,tran_cd,head_nm,order_no,col_order_no,fld_nm,data_ty,fld_wid,fld_desc,when_con,valid_con,error_con,inter_val,mandatory,disp_pickup,disp_head,valid_mast,remarks,_mul,tbl_nm,sel_item,sel_val,_query,_querycon,_read,compid,parent_ctrl,ctrl_not_show,_primddl,_dpopflds,_copy,frm_nm,reftbltran_cd,_fld_width,_fld_pre,_isQcd,QcdCondition from icustomfields where code='" + Tran_cd + "' and typewise=1 and disp_head=1 order by order_no,col_order_no";
                DataSet dsetOrderHead = objFL_Transaction.GetDataSet(sqlQuery);

                if (dsetOrderHead != null && dsetOrderHead.Tables.Count != 0 && dsetOrderHead.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in dsetOrderHead.Tables[0].Rows)
                    {
                        Bind_Header_Form_Controls(row);
                    }
                }
                //foreach (DataRow row in rowtops)
                //{
                //    Bind_Header_Form_Controls(row);
                //}

                objBASEFILEDS.dsBASEADDIFIELD = new DataSet();
                objBASEFILEDS.dsBASEADDIFIELD = objFL_BASEFIELD.GETCUSTOMFIELD(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Rows)
                {
                    if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = String.Format("{0:F}", "0.00");
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "1900/01/01";
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                    }
                }
                objBASEFILEDS.htitem_details.Clear();
                objBASEFILEDS.dsBASEADDIFIELDITEM = new DataSet();
                objBASEFILEDS.dsBASEADDIFIELDITEM = objFL_BASEFIELD.GETCUSTOMFIELDFORGRID(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELDITEM.Tables[0].Rows)
                {
                    if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                    {
                        objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = String.Format("{0:F}", "0.00");
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                    {
                        objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                    {
                        objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "1900/01/01";
                    }
                    else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                    {
                        objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                    }
                    else
                    {
                        objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                    }
                }
                #region 3.0
                objBASEFILEDS.DsetFileUpload = objFLGeneral.GetFile_List(objBASEFILEDS);
                if (objBASEFILEDS.DsetFileUpload != null && objBASEFILEDS.DsetFileUpload.Tables.Count != 0 && objBASEFILEDS.DsetFileUpload.Tables[0].Rows.Count != 0)
                {
                    Upload_FileInHeader(objBASEFILEDS.DsetFileUpload);
                }
                #endregion 3.0
                #region newly added code for dispaly field in header i,e not in inside button

                //header fields from custom table
                //DataRow[] drowCustomForHeader = objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Select("disp_head=1");
                //foreach (DataRow row in drowCustomForHeader)
                //{
                //    Bind_Header_Form_Controls(row);
                //}
                //Sharanamma Jekeen 04-Jun-2014 showing label .
                objlableGrid.Bounds = new Rectangle((ctrlwid / 2), hgt + ctrlhgt, ctrlwid / 2, ctrlhgt * 60 / 100);
                hgt += ctrlhgt * 60 / 100;
                //bind tab pages
                Bind_TabPages();

                if (objBASEFILEDS.HTMAIN.Contains(objBASEFILEDS.Primary_id.ToString()))
                {
                    objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id.ToString()] = Tran_id.ToString();
                }

                this.pnlform.Controls.Add(objlableGrid);
                objBASEFILEDS.dsFooter = objFL_BASEFIELD.GETBASEFIELDFORGRID(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                //foreach (DataRow row in objBASEFILEDS.dsFooter.Tables[0].Rows)
                //{
                //    if (!bool.Parse(row["ctrl_not_show"].ToString().Trim()))
                //    {
                //        //objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                //        if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                //        {
                //            objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] =  String.Format("{0:F}","0.00");
                //        }
                //        else if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                //        {
                //            objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                //        }
                //        else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                //        {
                //            objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "1900/01/01";
                //        }
                //        else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                //        {
                //            objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                //        }
                //        else
                //        {
                //            objBASEFILEDS.htitem_details[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                //        }
                //    }
                //    Bind_Grid_Form_Controls(row);
                //}

                //footer data show
                DataRow[] rowfooters = objBASEFILEDS.dsHeader.Tables[0].Select("_top=0");
                foreach (DataRow row in rowfooters)
                {
                    Bind_Footer_Form_Controls(row);
                }

                Label objlable1 = new Label();
                objlable1.Name = "lblStkEffect";
                objlable1.ForeColor = Color.Maroon;
                objlable1.Text = "STOCK EFFECT : '" + objBASEFILEDS.Stk_effect + "'";
                if (objBASEFILEDS.Stk_effect != null && objBASEFILEDS.Stk_effect != "")
                {
                    if (count % 2 == 0)
                    {
                        objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt + ctrlhgt, ctrlwid / 2, ctrlhgt);
                    }
                    else
                    {
                        objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, ctrlwid / 2, ctrlhgt);
                    }
                    pnlform.Controls.Add(objlable1);
                }

                //display grid header fields from custom table
                //DataRow[] drowCustomForGrid = objBASEFILEDS.dsBASEADDIFIELDITEM.Tables[0].Select("disp_head=1");
                //foreach (DataRow row in drowCustomForGrid)
                //{
                //    Bind_Grid_Form_Controls(row);
                //}
                #endregion
                //objBASEFILEDS.dsDCITEMFIELDS = new DataSet();
                //objBASEFILEDS.dsDCITEMFIELDS = objFL_BASEFIELD.GETDCFIELDFORGRID(Tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString(), objBASEFILEDS.HTMAIN.Contains("tran_dt") && objBASEFILEDS.HTMAIN["tran_dt"] != null && objBASEFILEDS.HTMAIN["tran_dt"].ToString() != "" ? objBASEFILEDS.HTMAIN["tran_dt"].ToString() : "getdate()");
                //foreach (DataRow row in objBASEFILEDS.dsDCITEMFIELDS.Tables[0].Rows)
                //{
                //    objBASEFILEDS.htitem_details.Add(row["fld_nm"].ToString().Trim().ToUpper(), "0.00");
                //    Insert_Grid_Form_Controls(row);
                //}
                //#region 3.0
                //if (objBASEFILEDS.DsetFileUpload != null && objBASEFILEDS.DsetFileUpload.Tables.Count != 0 && objBASEFILEDS.DsetFileUpload.Tables[0].Rows.Count != 0)
                //{
                //    Upload_FileInGrid(objBASEFILEDS.DsetFileUpload);
                //}
                //#endregion 3.0
                //if (this.Width > _width)
                //{
                //    POPUPTEXTBOX_FOR_GRID txtcol = new POPUPTEXTBOX_FOR_GRID();
                //    txtcol.HeaderText = "";
                //    txtcol.Name = "col";
                //    grid.Columns.Add(txtcol);
                //    grid.Columns["col"].Width = this.ClientSize.Width - _width;
                //    grid.Columns["col"].ReadOnly = true;
                //}

                //Bind_Tax_GRID();
                //Tax_TextBox();

                Bind_GridAccount_Form_Controls();
                Bind_ContextMenustripForAccount();

                //string str = "select * from (select fld_nm,order_no,col_order_no from ibasefields where code='" + objBASEFILEDS.Code + "' and typewise=0 and order_no>0 union all select fld_nm,case when disp_pert=1 then corder+1 else corder end,col_order_no=0 from dc_mast where code='" + objBASEFILEDS.Code + "' union all select pert_name,corder,col_order_no=0 from dc_mast where code='" + objBASEFILEDS.Code + "' and disp_pert=1 union all select fld_nm,order_no,col_order_no from icustomfields where code='" + objBASEFILEDS.Code + "' and typewise=0 and order_no>0 and disp_head=1) vw order by order_no,col_order_no";
                //DataSet dsetOrder = objFL_Transaction.GetDataSet(str);

                //if (dsetOrder != null && dsetOrder.Tables.Count != 0 && dsetOrder.Tables[0].Rows.Count != 0)
                //{
                //    foreach (DataRow row in dsetOrder.Tables[0].Rows)
                //    {
                //        if (grid.Columns.Contains(row["fld_nm"].ToString().Trim()))
                //        {
                //            grid.Columns[row["fld_nm"].ToString().Trim()].DisplayIndex = int.Parse(row["order_no"].ToString());
                //        }
                //    }
                //}
                AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "Transaction");

                DisplayControlsonMode(Tran_mode, 1);
                //Bind_ContextMenustrip();
                //  objBASEFILEDS.HTMAINREF.Clear();
                if (objBASEFILEDS.HTMAIN.Contains("FIN_YR"))
                {
                    objBASEFILEDS.HTMAIN["FIN_YR"] = objBASEFILEDS.ObjCompany.Fin_yr;
                }
                if (objBASEFILEDS.HTMAIN.Contains("COMPID"))
                {
                    objBASEFILEDS.HTMAIN["COMPID"] = objBASEFILEDS.ObjCompany.Compid.ToString();//((frm_mainmenu)this.MdiParent).ObjBLComp.Compid;
                }
                if (objBASEFILEDS.HTMAIN.Contains("tran_cd"))
                {
                    objBASEFILEDS.HTMAIN["tran_cd"] = tran_cd;
                }
                objBASEFILEDS.BlnStopItemEnter = false;


                #region 2.0
                DefaultControlValidation();
                #endregion 2.0
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Error Msg : " + ex.Message, "Error");
            }
        }

        #region 3.0
        private void Upload_FileInHeader(DataSet dsetFileUpload)
        {
            DataRow[] rowFileHeader = dsetFileUpload.Tables[0].Select("typewise=1");
            if (rowFileHeader != null && rowFileHeader.Length != 0)
            {
                foreach (DataRow row in rowFileHeader)
                {
                    PopupButton btnform = new PopupButton();
                    btnform.Name = row["fld_nm"].ToString().Trim();
                    btnform.Tag = "button";
                    // btnform.Text = row["head_nm"].ToString().Trim();
                    btnform.frmName = "File_load";
                    btnform.PTextName = "";
                    btnform.Tbl_nm = "";
                    btnform.Primaryddl = "";
                    btnform.Dispddlfields = "";
                    btnform.Reftbltran_cd = "";
                    btnform.Query_con = "";
                    btnform.Visible = true;
                    btnform.Enabled = true;

                    if ((count + 1) % 2 == 0)
                    {
                        btnform.Bounds = new Rectangle(wid + (ctrlwid / 2) * 67 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 85 / 100);
                    }
                    else
                    {
                        btnform.Bounds = new Rectangle((ctrlwid / 2) * 70 / 100, hgt + ctrlhgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 85 / 100);
                        hgt += ctrlhgt;
                    }

                    btnform.Text = row["caption_nm"].ToString().Trim();
                    btnform.Click += new System.EventHandler(btn_click);

                    btnform.Enter -= new EventHandler(btn_enter);
                    btnform.Enter += new EventHandler(btn_enter);
                    btnform.Leave -= new EventHandler(btn_leave);
                    btnform.Leave += new EventHandler(btn_leave);

                    pnlform.Controls.Add(btnform);
                }
            }
        }
        #endregion 3.0

        public void DisplayControlsonMode(string tran_mode, int isaddgrid)
        {
            if (objBASEFILEDS.HTMAIN.Contains("INCL_EXC"))
            {
                if (objBASEFILEDS.HTMAIN["INCL_EXC"].ToString().ToLower() == "true" || objBASEFILEDS.HTMAIN["INCL_EXC"].ToString() == "1")
                {
                    allow_excise_calc = true;
                }
                else
                {
                    allow_excise_calc = false;
                }
            }
            else
            {
                allow_excise_calc = true;
            }
            switch (tran_mode)
            {
                case "add_mode":
                    #region
                    //objBASEFILEDS.DsDateTime = objFL_Transaction.GetDataSet("select isnull(max(isnull(tran_no,0)),0) tran_no,max(tran_dt) tran_dt from " + objBASEFILEDS.Main_tbl_nm + " where tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                    //ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;// + "      " + (objBASEFILEDS.Stk_effect != null && objBASEFILEDS.Stk_effect != "" ? "STOCK EFFECT : '" + objBASEFILEDS.Stk_effect + "'" : "");
                    //#region tran in add_mode
                    //if (isaddgrid == 1)
                    //{
                    //    objBASEFILEDS.BlnStopItemEnter = false;
                    //    foreach (Control c in this.Controls[1].Controls)
                    //    {
                    //        if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                    //        {
                    //            if (c is CheckBox)
                    //            {
                    //                ((CheckBox)c).Checked = false;
                    //                objBASEFILEDS.HTMAIN[c.Name] = false;
                    //            }
                    //            else if (c is ComboBox)
                    //            {
                    //                if (((ComboBox)c).SelectedIndex != -1)
                    //                {
                    //                    ((ComboBox)c).SelectedIndex = 0;
                    //                    objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember] = ((ComboBox)c).SelectedText;
                    //                    objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember] = ((ComboBox)c).SelectedValue;
                    //                }
                    //            }
                    //            else if (c.Tag.ToString().ToLower() == "decimal")
                    //            {
                    //                c.Text = String.Format("{0:F}", "0.00");
                    //            }
                    //            else if (c.Tag.ToString().ToLower() == "int")
                    //            {
                    //                c.Text = "0";
                    //            }
                    //            else if (c.Tag.ToString().ToLower() == "datetime")
                    //            {
                    //                if (objBASEFILEDS.Curr_date)
                    //                {
                    //                    if (c.Name == "TRAN_DT")
                    //                    {
                    //                        ((UserDT)c).bUpdateFlag = true;
                    //                        ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (objBASEFILEDS.DsDateTime != null && objBASEFILEDS.DsDateTime.Tables.Count != 0 && objBASEFILEDS.DsDateTime.Tables[0].Rows.Count != 0)
                    //                    {
                    //                        if (objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"] != null && objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString() != "")
                    //                        {
                    //                            ((UserDT)c).bUpdateFlag = true;
                    //                            ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString());
                    //                        }
                    //                    }
                    //                }
                    //                ((UserDT)c).bUpdateFlag = false;
                    //                ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                    //            }
                    //            else if (c.Tag.ToString().ToLower() == "time")
                    //            {
                    //                c.Text = DateTime.Now.ToLongTimeString();
                    //            }
                    //            else
                    //            {
                    //                c.Text = "";
                    //                //c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString().Trim();
                    //                objBASEFILEDS.HTMAIN[c.Name] = "";
                    //                if (c.Name.ToLower() == "ac_nm" && objBASEFILEDS.HTMAIN.Contains("ac_nm"))
                    //                {
                    //                    c.Text = objBASEFILEDS.Def_acc;
                    //                    objBASEFILEDS.HTMAIN["ac_nm"] = objBASEFILEDS.Def_acc;
                    //                    if (objBASEFILEDS.HTMAIN.Contains("ac_id"))
                    //                    {
                    //                        objBASEFILEDS.HTMAIN["ac_id"] = objBASEFILEDS.Def_acc_id;
                    //                    }
                    //                }
                    //                if (c.Name.ToLower() == "cons_nm" && objBASEFILEDS.HTMAIN.Contains("cons_nm"))
                    //                {
                    //                    if (objBASEFILEDS.Def_consignee != null && objBASEFILEDS.Def_consignee != "")
                    //                    {
                    //                        c.Text = objBASEFILEDS.Def_consignee;
                    //                        objBASEFILEDS.HTMAIN["cons_nm"] = objBASEFILEDS.Def_consignee;
                    //                        if (objBASEFILEDS.HTMAIN.Contains("cons_id"))
                    //                        {
                    //                            objBASEFILEDS.HTMAIN["cons_id"] = objBASEFILEDS.Def_consignee_id;
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        c.Text = objBASEFILEDS.Def_acc;
                    //                        objBASEFILEDS.HTMAIN["cons_nm"] = objBASEFILEDS.Def_acc;
                    //                        if (objBASEFILEDS.HTMAIN.Contains("cons_id"))
                    //                        {
                    //                            objBASEFILEDS.HTMAIN["cons_id"] = objBASEFILEDS.Def_acc_id;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            objBASEFILEDS.HTMAIN[c.Name] = c.Text;
                    //            c.Enabled = true;
                    //            if (c.Name == "TRAN_DT")
                    //            {
                    //                //this.ActiveControl = c;
                    //                c.Focus();
                    //            }
                    //        }
                    //        else if (c.Name == "TRAN_SR" && c.Visible == false)
                    //        {
                    //            objBASEFILEDS.HTMAIN[c.Name] = "";
                    //        }
                    //        DataRow[] rowsdefault = objBASEFILEDS.dsHeader.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0 and default_con<>''");
                    //        foreach (DataRow row in rowsdefault)
                    //        {
                    //            ValidateDefaultCondition(row, c);
                    //        }
                    //    }
                    //    foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Rows)
                    //    {
                    //        if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = String.Format("{0:F}", "0.00");
                    //        }
                    //        else if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                    //        }
                    //        else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "1900/01/01";
                    //        }
                    //        else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                    //        }
                    //        else if (row["data_ty"].ToString().Trim().ToUpper() == "BIT")
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = false;
                    //        }
                    //        else
                    //        {
                    //            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                    //        }
                    //    }
                    //    foreach (Control c in this.tabControl.Controls)
                    //    {
                    //        if (c is TabPage)
                    //        {
                    //            foreach (Control c1 in c.Controls[0].Controls)
                    //            {
                    //                if (c1.Name == "gridAccount")
                    //                {
                    //                    gridAccount.Enabled = true;
                    //                    foreach (DataGridViewColumn col in gridAccount.Columns)
                    //                    {
                    //                        if (col.Name != "col" && (col is DataGridViewTextBoxColumn || col is POPUPDATETIME_FOR_GRID) && col.Visible == true)
                    //                        {
                    //                            col.ReadOnly = false;
                    //                        }
                    //                    }
                    //                    Account_Posting();
                    //                    Account_Allocation();
                    //                }
                    //            }
                    //            foreach (Control c1 in c.Controls)
                    //            {
                    //                if (c1 is Panel)
                    //                {
                    //                    foreach (Control c3 in c1.Controls)
                    //                    {
                    //                        if (c3 is TextBox)
                    //                        {
                    //                            c3.Text = String.Format("{0:F}", "0.00");
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //#endregion tran in add_mode
                    //objADDTRANSANDMASTER.ACTIVE_BL = objBASEFILEDS;
                    //bool flgValid = objADDTRANSANDMASTER.tsAddTransactionAndMaster();
                    //if (flgValid)
                    //{
                    //    objBASEFILEDS.HTMAIN = objADDTRANSANDMASTER.ACTIVE_BL.HTMAIN;
                    //    BindControlsFromView();
                    //}
                    //else
                    //{
                    //    if (objADDTRANSANDMASTER.BL_FIELDS.Errormsg.Length != 0)
                    //    {
                    //        AutoClosingMessageBox.Show(objADDTRANSANDMASTER.BL_FIELDS.Errormsg, "Add");
                    //    }
                    //}
                    #endregion
                    //this.Text = objBASEFILEDS.Tran_nm;
                    objBASEFILEDS.DsDateTime = objFL_Transaction.GetDataSet("select isnull(max(isnull(tran_no,0)),0) tran_no,max(tran_dt) tran_dt from " + objBASEFILEDS.Main_tbl_nm + " where tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                    ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;// + "      " + (objBASEFILEDS.Stk_effect != null && objBASEFILEDS.Stk_effect != "" ? "STOCK EFFECT : '" + objBASEFILEDS.Stk_effect + "'" : "");
                    #region tran in add_mode
                    // _focus_move = false; //sharanamma datetime 26.06.13
                    if (isaddgrid == 1)
                    {
                        objBASEFILEDS.BlnStopItemEnter = false;
                        itserial = 0;
                        foreach (Control c in this.Controls[1].Controls)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                            {
                                if (c is CheckBox)
                                {
                                    ((CheckBox)c).Checked = false;
                                    objBASEFILEDS.HTMAIN[c.Name] = false;
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
                                else if (c.Tag.ToString().ToLower() == "decimal")
                                {
                                    c.Text = String.Format("{0:F}", "0.00");
                                }
                                else if (c.Tag.ToString().ToLower() == "int")
                                {
                                    c.Text = "0";
                                }
                                else if (c.Tag.ToString().ToLower() == "datetime")
                                {
                                    //SendKeys.Send("{RIGHT 0}");
                                    // ((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                    if (objBASEFILEDS.Curr_date)
                                    {
                                        //c.Text = DateTime.Now.ToString("yyyy/MM/dd"); 
                                        if (c.Name == "TRAN_DT")
                                        {
                                            //((UserDT)c).DtValue = DateTime.Now;
                                            ((UserDT)c).bUpdateFlag = true;
                                            ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                        }
                                    }
                                    else
                                    {
                                        if (objBASEFILEDS.DsDateTime != null && objBASEFILEDS.DsDateTime.Tables.Count != 0 && objBASEFILEDS.DsDateTime.Tables[0].Rows.Count != 0)
                                        {
                                            if (objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"] != null && objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString() != "")
                                            {
                                                //c.Text = DateTime.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString()).ToString("yyyy/MM/dd");
                                                ((UserDT)c).bUpdateFlag = true;
                                                ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString());
                                            }
                                            //else
                                            //{
                                            //    //c.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                            //    ((UserDT)c).DtValue = DateTime.Now;
                                            //}
                                        }
                                        //else
                                        //{
                                        //    c.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                        //}
                                    }
                                    ((UserDT)c).bUpdateFlag = false;
                                    ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                }
                                else if (c.Tag.ToString().ToLower() == "time")
                                {
                                    c.Text = DateTime.Now.ToLongTimeString();
                                }
                                else
                                {
                                    c.Text = "";
                                    //c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString().Trim();
                                    objBASEFILEDS.HTMAIN[c.Name] = "";
                                    if (c.Name.ToLower() == "ac_nm" && objBASEFILEDS.HTMAIN.Contains("ac_nm"))
                                    {
                                        c.Text = objBASEFILEDS.Def_acc;
                                        objBASEFILEDS.HTMAIN["ac_nm"] = objBASEFILEDS.Def_acc;
                                        if (objBASEFILEDS.HTMAIN.Contains("ac_id"))
                                        {
                                            objBASEFILEDS.HTMAIN["ac_id"] = objBASEFILEDS.Def_acc_id;
                                        }
                                    }
                                    if (c.Name.ToLower() == "cons_nm" && objBASEFILEDS.HTMAIN.Contains("cons_nm"))
                                    {
                                        if (objBASEFILEDS.Def_consignee != null && objBASEFILEDS.Def_consignee != "")
                                        {
                                            c.Text = objBASEFILEDS.Def_consignee;
                                            objBASEFILEDS.HTMAIN["cons_nm"] = objBASEFILEDS.Def_consignee;
                                            if (objBASEFILEDS.HTMAIN.Contains("cons_id"))
                                            {
                                                objBASEFILEDS.HTMAIN["cons_id"] = objBASEFILEDS.Def_consignee_id;
                                            }
                                        }
                                        else
                                        {
                                            c.Text = objBASEFILEDS.Def_acc;
                                            objBASEFILEDS.HTMAIN["cons_nm"] = objBASEFILEDS.Def_acc;
                                            if (objBASEFILEDS.HTMAIN.Contains("cons_id"))
                                            {
                                                objBASEFILEDS.HTMAIN["cons_id"] = objBASEFILEDS.Def_acc_id;
                                            }
                                        }
                                    }
                                }
                                objBASEFILEDS.HTMAIN[c.Name] = c.Text;
                                c.Enabled = true;
                                if (c.Name == "TRAN_DT")
                                {
                                    //this.ActiveControl = c;
                                    c.Focus();
                                }
                            }
                            else if (c.Name == "TRAN_SR" && c.Visible == false)
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = "";
                            }
                            DataRow[] rowsdefault = objBASEFILEDS.dsHeader.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0 and default_con<>''");
                            foreach (DataRow row in rowsdefault)
                            {
                                ValidateDefaultCondition(row, c);
                            }
                        }
                        foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Rows)
                        {
                            if (row["data_ty"].ToString().Trim().ToUpper() == "DECIMAL")
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = String.Format("{0:F}", "0.00");
                            }
                            else if (row["data_ty"].ToString().Trim().ToUpper() == "INT")
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "0";
                            }
                            else if (row["data_ty"].ToString().Trim().ToUpper() == "DATETIME")
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "1900/01/01";
                            }
                            else if (row["data_ty"].ToString().Trim().ToUpper() == "TIME")
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = DateTime.Now.ToLongTimeString();
                            }
                            else if (row["data_ty"].ToString().Trim().ToUpper() == "BIT")
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = false;
                            }
                            else
                            {
                                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()] = "";
                            }
                        }
                        if (objBASEFILEDS.Alloc_tbl_nm != "")
                        {
                            foreach (Control c in this.tabControl.Controls)
                            {
                                if (c is TabPage)
                                {
                                    foreach (Control c1 in c.Controls[0].Controls)
                                    {
                                        if (c1.Name == "gridAccount")
                                        {
                                            foreach (DataGridViewColumn col in gridAccount.Columns)
                                            {
                                                if (col.Name != "col" && (col is DataGridViewTextBoxColumn || col is POPUPDATETIME_FOR_GRID) && col.Visible == true)
                                                {
                                                    col.ReadOnly = false;
                                                }
                                            }
                                            Account_Posting();
                                            Account_Allocation();
                                        }
                                    }
                                    foreach (Control c1 in c.Controls)
                                    {
                                        if (c1 is Panel)
                                        {
                                            foreach (Control c3 in c1.Controls)
                                            {
                                                if (c3 is TextBox)
                                                {
                                                    c3.Text = "0.00";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            gridAccount.Rows.Clear();
                        }
                    }
                    #endregion tran in add_mode
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
                    #region edit_mode
                    objBASEFILEDS.dsetview = new DataSet();
                    objBASEFILEDS.dsetview = objFL_BASEFIELD.GET_HEADERDATA(objBASEFILEDS);
                    foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
                    {
                        foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                            {
                                objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString().Trim();
                            }
                        }
                    }
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                        {
                            if (c is CheckBox)
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
                            else if (c.Tag.ToString().ToLower() == "datetime")
                            {
                                //((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                {
                                    if (DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString() != "1900-01-01 12:00:00 AM" && DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000-00-01" && DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString("yyyy/mm/dd") != "1900/00/01" && DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000/00/01")
                                    {
                                        ((UserDT)c).bUpdateFlag = true;
                                        ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                    }
                                    else
                                    {
                                        ((UserDT)c).bUpdateFlag = false;
                                        ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                    }
                                }
                            }
                            else if (c.Tag.ToString().ToLower() == "time")
                            {
                                if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                {
                                    c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToLongTimeString();
                                }
                            }
                            else
                            {
                                c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString().Trim();
                            }
                            c.Enabled = true;
                            if (c.Name == "AC_NM" && c.Visible)
                            {
                                // this.ActiveControl = c;
                                c.Focus();
                            }
                            if (c.Name == "RULE")
                            {
                                if (c.Text == "NON-EXCISABLE" || c.Text == "CT-1" || c.Text == "CT-3")
                                    allow_excise_calc = false;
                                else
                                    allow_excise_calc = true;
                            }
                        }
                        DataRow[] rowsdefault = objBASEFILEDS.dsHeader.Tables[0].Select("fld_nm='" + c.Name + "' and inter_val=0 and default_con<>''");
                        foreach (DataRow row in rowsdefault)
                        {
                            ValidateDefaultCondition(row, c);
                        }
                    }

                    foreach (Control c in this.tabControl.Controls)
                    {
                        if (c is TabPage)
                        {
                            foreach (Control c1 in c.Controls[0].Controls)
                            {
                                if (c1.Name == "gridAccount")
                                {
                                    foreach (DataGridViewColumn col in gridAccount.Columns)
                                    {
                                        if (col.Name != "col" && (col is DataGridViewTextBoxColumn || col is POPUPDATETIME_FOR_GRID) && col.Visible == true)
                                        {
                                            col.ReadOnly = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    objFL_GRIDEVENTS.objBASEFILEDS = objBASEFILEDS;
                    objVALIDATION.ObjBLFD = objBASEFILEDS;
                    #endregion
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
                    Account_Posting();
                    Account_Allocation();
                    break;
                case "view_mode":
                    objBASEFILEDS.dsetview = new DataSet();
                    objBASEFILEDS.HTMAIN[objBASEFILEDS.Primary_id] = objBASEFILEDS.Tran_id;
                    objBASEFILEDS.dsetview = objFL_BASEFIELD.GET_HEADERDATA(objBASEFILEDS);
                    foreach (DataRow row in objBASEFILEDS.dsetview.Tables[0].Rows)
                    {
                        foreach (DataColumn column in objBASEFILEDS.dsetview.Tables[0].Columns)
                        {
                            if (objBASEFILEDS.HTMAIN.ContainsKey(column.ColumnName.Trim().ToUpper()))
                            {
                                objBASEFILEDS.HTMAIN[column.ColumnName.Trim().ToUpper()] = row[column.ColumnName].ToString().Trim();
                            }
                        }
                    }
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                        {
                            if (objBASEFILEDS.Tran_id.ToString() != "0")
                            {
                                if (c is CheckBox)
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
                                        ((ComboBox)c).SelectedText = objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember].ToString();
                                        ((ComboBox)c).SelectedValue = objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember].ToString();
                                    }
                                }
                                else if (c.Tag.ToString().ToLower() == "datetime")
                                {
                                    //((UserDT)c).CustomFormat = "dd-MMM-yyyy";
                                    if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                    {
                                        // c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString("yyyy/MM/dd");
                                        ((UserDT)c).bUpdateFlag = true;
                                        ((UserDT)c).DtValue = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString());
                                    }
                                }
                                else if (c.Tag.ToString().ToLower() == "time")
                                {
                                    if (objBASEFILEDS.HTMAIN[c.Name] != null && objBASEFILEDS.HTMAIN[c.Name].ToString() != "")
                                    {
                                        c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToLongTimeString();
                                    }
                                }
                                else
                                {
                                    c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString().Trim();
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
                                else if (c.Tag.ToString().ToLower() == "decimal")
                                {
                                    c.Text = String.Format("{0:F}", "0.00");
                                }
                                else if (c.Tag.ToString().ToLower() == "int")
                                {
                                    c.Text = "0";
                                }
                                else if (c.Tag.ToString().ToLower() == "datetime")
                                {
                                    // c.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                    ((UserDT)c).DtValue = DateTime.Parse("1900/01/01");
                                }
                                else if (c.Tag.ToString().ToLower() == "time")
                                {
                                    c.Text = DateTime.Now.ToLongTimeString();
                                }
                                else
                                {
                                    c.Text = "";
                                }
                            }
                            if (!(c is Label)) c.Enabled = false;
                        }
                    }
                    foreach (Control c in this.tabControl.Controls)
                    {
                        if (c is TabPage)
                        {
                            foreach (Control c1 in c.Controls[0].Controls)
                            {
                                if (c1.Name == "gridAccount")
                                {
                                    gridAccount.Enabled = true;
                                    //foreach (DataGridViewColumn col in gridAccount.Columns)
                                    //{
                                    //    if (col.Name != "col" && (col is DataGridViewTextBoxColumn || col is POPUPDATETIME_FOR_GRID) && col.Visible == true)
                                    //    {
                                    //        col.ReadOnly = true;
                                    //    }
                                    //}
                                }
                            }
                            if (objBASEFILEDS.Tran_id.ToString() == "0")
                            {
                                foreach (Control c1 in c.Controls)
                                {
                                    if (c1 is Panel)
                                    {
                                        foreach (Control c3 in c1.Controls)
                                        {
                                            if (c3 is TextBox)
                                            {
                                                c3.Text = String.Format("{0:F}", "0.00");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    objFL_GRIDEVENTS.objBASEFILEDS = objBASEFILEDS;
                    objVALIDATION.ObjBLFD = objBASEFILEDS;

                    Account_Posting();
                    Account_Allocation();
                    break;
                default: break;
            }
            DefaultControlValidation();
            iInit.ActiveFrm = this;
            objiCustominit.ACTIVE_BL = objBASEFILEDS;
            objiCustominit.Load_Init_Details();
        }
        #region 1.0
        private void DefaultControlValidation()
        {
            bool validflg = true;
            if (objBASEFILEDS.Tran_id.ToString() != "0" && objDefaultControl.GetType().GetMethod("DefaultControl") != null)
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
                    //objBASEFILEDS.HTITEM = objDefaultControl.ACTIVE_BL.HTITEM;
                    BindControlsFromView();
                }
            }
        }
        #endregion 1.0
        private void ValidateDefaultCondition(DataRow row, Control c)
        {
            string valu = row["default_con"].ToString().Replace("&&", "$&&$").Replace("||", "$||$");
            string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in cond)
            {
                string[] cond1 = str.Split(new string[] { "==" }, StringSplitOptions.RemoveEmptyEntries);
                if (cond1[0].ToString().ToLower() == "add_mode" && this.tran_mode.ToLower() == "add_mode")
                {
                    c.Enabled = bool.Parse(cond1[1].ToString());
                }
                else if (cond1[0].ToString().ToLower() == "edit_mode" && this.tran_mode.ToLower() == "edit_mode")
                {
                    c.Enabled = bool.Parse(cond1[1].ToString());
                }
                string[] cond2 = str.Split(new string[] { "!=" }, StringSplitOptions.RemoveEmptyEntries);
                if (cond2[0].ToString().ToLower() == "add_mode" && this.tran_mode.ToLower() == "add_mode")
                {
                    c.Enabled = !bool.Parse(cond2[1].ToString());
                }
                else if (cond2[0].ToString().ToLower() == "edit_mode" && this.tran_mode.ToLower() == "edit_mode")
                {
                    c.Enabled = !bool.Parse(cond2[1].ToString());
                }
                if (cond1.Length == 1 && cond1[0].ToLower() == "false")
                {
                    if (!(c is Label)) c.Enabled = false;
                }
                if (cond1.Length == 1 && cond1[0].ToLower() == "true")
                {
                    c.Enabled = true;
                }
            }
        }

        private void Account_Posting()
        {
            if (objBASEFILEDS.Ac_post)
            {
                gridAccount.Rows.Clear();

                objBASEFILEDS.HT_ACDET.Clear();
                objBASEFILEDS.HT_ALLOC.Clear();

                objBASEFILEDS.DsAccountPosting = objFL_Transaction.GetDataSet("select * from " + objBASEFILEDS.Ac_tbl_nm + " where tran_id='" + objBASEFILEDS.Tran_id + "' and tran_cd='" + objBASEFILEDS.Code + "'");
                if (objBASEFILEDS.Tran_mode != "add_mode")
                {
                    if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                    {
                        int j = 0;
                        foreach (DataRow row in objBASEFILEDS.DsAccountPosting.Tables[0].Rows)
                        {
                            gridAccount.Rows.Add();

                            if (!objBASEFILEDS.HT_ACDET.Contains(row["acserial"].ToString().Trim()))
                            {
                                objBASEFILEDS.HT_ACDET[row["acserial"].ToString().Trim()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry2 in objBASEFILEDS.HashacItem)
                                {
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[row["acserial"].ToString().Trim()])[entry2.Key] = entry2.Value;
                                }
                            }

                            foreach (DataColumn col in objBASEFILEDS.DsAccountPosting.Tables[0].Columns)
                            {
                                if (gridAccount.Columns.Contains(col.ColumnName))
                                {
                                    if (gridAccount.Columns[col.ColumnName].Tag != null && gridAccount.Columns[col.ColumnName].Tag.ToString() == "decimal")
                                    {
                                        gridAccount.Rows[j].Cells[col.ColumnName].Value = String.Format("{0:F}", row[col.ColumnName]);
                                    }
                                    else
                                    {
                                        gridAccount.Rows[j].Cells[col.ColumnName].Value = row[col.ColumnName];
                                    }
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[row["acserial"].ToString().Trim()])[col.ColumnName] = row[col.ColumnName].ToString();
                                }
                            }
                            j++;
                        }
                        gridAccount.Sort(gridAccount.Columns["acc_order_no"], ListSortDirection.Ascending);
                    }
                    foreach (DataGridViewRow row in gridAccount.Rows)
                    {
                        if (row.Cells["acc_amount"].Value != null && row.Cells["acc_amount"].Value.ToString() != "")
                        {
                            if (decimal.Parse(row.Cells["acc_amount"].Value.ToString()).ToString() != "0.00")
                            {
                                row.Visible = true;
                            }
                            else
                            {
                                row.Visible = false;
                            }
                        }
                    }
                    AccountNet_AmtBindGrid();//Load_AccountGrid();
                }
            }
        }

        private void Account_Allocation()
        {
            if (objBASEFILEDS.Ac_post)
            {
                //DataSet dsetAccountAlloc = objFL_Transaction.GetDataSet("select * from " + objBASEFILEDS.Alloc_tbl_nm + " where tran_id='" + objBASEFILEDS.Tran_id + "' and tran_cd='" + objBASEFILEDS.Code + "'");
                if (objBASEFILEDS.Tran_mode != "add_mode")
                {
                    Hashtable htparam = new Hashtable();

                    htparam.Add("@atran_id", objBASEFILEDS.Tran_id);
                    htparam.Add("@atran_cd", objBASEFILEDS.Code);
                    htparam.Add("@acompid", objBASEFILEDS.ObjCompany.Compid.ToString());

                    DataSet dsetAccountAlloc = objFLGeneral.GetDataSetByProcedure("ISP_ALLOCATION_LOADING", htparam);

                    if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] row1 = dsetAccountAlloc.Tables[0].Select("allocated_amt<>0");
                        string key;
                        foreach (DataRow r in row1)
                        {
                            key = r["acserial"].ToString() + "," + r["ref_tran_id"].ToString() + "," + r["ref_acserial"].ToString();

                            if (objBASEFILEDS.HT_ALLOC != null && !objBASEFILEDS.HT_ALLOC.Contains(key))
                            {
                                objBASEFILEDS.HT_ALLOC[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                            }
                            foreach (DataColumn column in dsetAccountAlloc.Tables[0].Columns)
                            {
                                ((Hashtable)objBASEFILEDS.HT_ALLOC[key])[column.ColumnName] = r[column.ColumnName];
                            }
                            foreach (DataGridViewRow row in gridAccount.Rows)
                            {
                                if (row.Cells["acserial"].Value.ToString() == r["acserial"].ToString())
                                {
                                    row.Cells["acc_allocation"].Style.BackColor = Color.Blue;
                                    row.Cells["acc_allocation"].Style.ForeColor = Color.Blue;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Header/Footer
        private void Bind_Header_Form_Controls(DataRow row)
        {
            count = int.Parse(row["order_no"].ToString().Trim());

            //if (!bool.Parse(row["inter_val"].ToString().Trim()))
            //{
            if (count % 2 != 0 && (row["parent_ctrl"].ToString().Trim() == "" || row["data_ty"].ToString().Trim().ToLower() != "button"))//int.Parse(row["dec_no"].ToString().Trim()) % 2 != 0)
            {
                if (count != 1)
                    hgt += ctrlhgt;
                else
                    hgt = ucToolBar1.Height + ctrlhgt * 10 / 100;
            }

            Label objlable = new Label();
            //  objlable.AutoSize = true;            
            objlable.Name = "lbl" + row["head_nm"].ToString().Trim();
            objlable.Text = row["head_nm"].ToString().Trim();
            //objlable.ForeColor= bool.Parse(row["mandatory"].ToString().Trim())?Color.Red:Color.Black;
            objlable.Visible = !bool.Parse(row["inter_val"].ToString().Trim());

            Label objlable1 = new Label();
            objlable1.AutoSize = true;
            objlable1.Name = "lbl1" + row["head_nm"].ToString().Trim();
            objlable1.Text = bool.Parse(row["mandatory"].ToString().Trim()) ? "*" : " ";
            objlable1.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            // objlable1.BackColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable1.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
            //   objlable1.TextAlign = ContentAlignment.TopLeft;
            // objlable.TextAlign = ContentAlignment.TopRight;
            if ((row["parent_ctrl"].ToString().Trim() == "" || !bool.Parse(row["ctrl_not_show"].ToString().Trim())) && !bool.Parse(row["inter_val"].ToString().Trim()) && !bool.Parse(row["_mul"].ToString().Trim()))
            {
                if (count % 2 == 0)
                {
                    objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                }
                else
                {
                    objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objlable1.Bounds = new Rectangle(objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                }
            }
            pnlform.Controls.Add(objlable);
            pnlform.Controls.Add(objlable1);
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";
                if (bool.Parse(row["_mul"].ToString().Trim()))
                {
                    ComboBox cmdseries = new ComboBox();
                    cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdseries.Name = row["fld_nm"].ToString().Trim();
                    cmdseries.Tag = row["data_ty"].ToString().Trim().ToLower();
                    DataSet dscmdseries = new DataSet();
                    if (row["_query"] != null && row["_query"].ToString() != "")
                    {
                        dscmdseries = GetDatasetForCombobox(row["_query"].ToString(), row["_querycon"].ToString());//objFL_GEN_INV.Execute_Procedure_Query(row["_query"].ToString(), "");
                    }
                    else
                    {
                        dscmdseries = objFL_GEN_INV.GET_TBL_VAL(row["tbl_nm"].ToString().Trim(), this.tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                    }
                    cmdseries.DataSource = dscmdseries.Tables[0];
                    cmdseries.DisplayMember = row["sel_item"].ToString().Trim();
                    cmdseries.ValueMember = row["sel_val"].ToString().Trim();
                    cmdseries.Update();
                    if (dscmdseries != null && dscmdseries.Tables[0].Rows.Count != 0)
                    {
                        // objBASEFILEDS.HTMAIN[cmdseries.Name] = dscmdseries.Tables[0].Rows[0]["sel_val"].ToString().Trim();
                        if (!bool.Parse(row["inter_val"].ToString().Trim()))
                        {
                            if (count % 2 == 0)
                            {
                                objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(wid + objlable.Width + objlable1.Width, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                            else
                            {
                                objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                        }
                        cmdseries.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                        cmdseries.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                        if (bool.Parse(row["inter_val"].ToString().Trim()))
                        {
                            objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";
                        }
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

                    PopupTextBox objtxt = new PopupTextBox();
                    objtxt.Name = row["fld_nm"].ToString().Trim();
                    objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                    objtxt.frmName = row["frm_nm"].ToString().Trim();
                    objtxt.PTextName = row["parent_ctrl"].ToString().Trim();
                    objtxt.Tbl_nm = row["tbl_nm"].ToString();
                    objtxt.Reftbltran_cd = row["reftbltran_cd"].ToString().Trim();

                    if (row["fld_nm"].ToString().Trim().ToLower() == "ac_nm")
                    {
                        _strCMOrPROD = "";
                        objtxt.Primaryddl = row["_primddl"].ToString().Trim();
                        objtxt.Dispddlfields = objBASEFILEDS.Ac_pop_sel;
                        if (objBASEFILEDS.Ac_grp.Split(',').Length != 0)
                        {
                            foreach (string str in objBASEFILEDS.Ac_grp.Split(','))
                            {
                                if (str != "")
                                {
                                    if (_strCMOrPROD == "") _strCMOrPROD = "'" + str + "'"; else _strCMOrPROD += "'" + str + "'";
                                }
                            }
                            objtxt.Query_con = "ac_grp_nm like " + (_strCMOrPROD == "" ? "'%'" : _strCMOrPROD).ToString();
                        }
                    }
                    else
                    {
                        objtxt.Primaryddl = row["_primddl"].ToString().Trim();
                        objtxt.Dispddlfields = row["_Dpopflds"].ToString().Trim();
                        objtxt.Query_con = row["_querycon"].ToString().Trim();
                        if (row["_isQcd"] != null && row["_isQcd"].ToString() != "" && bool.Parse(row["_isQcd"].ToString()))
                        {
                            objtxt.IsQcd = true;
                            objtxt.QcdCondition = row["QcdCondition"] != null ? row["QcdCondition"].ToString() : "";
                        }
                    }
                    if (objtxt.Name.Trim().ToLower() == "tran_cd")
                    {
                        objBASEFILEDS.HTMAIN[objtxt.Name] = Tran_cd;
                    }
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (count % 2 == 0)
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                        else
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                    }
                    objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                    objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                    objtxt.Validating += new CancelEventHandler(String_Validating);
                    if (objtxt.Name.Trim().ToLower() == "tran_no")
                    {
                        objtxt.Enter -= new EventHandler(txt_enter);
                        objtxt.Enter += new EventHandler(txt_enter);
                    }
                    if (row["parent_ctrl"].ToString().Trim() != "")
                    {
                        objtxt.KeyDown -= new KeyEventHandler(txt_popup_click);
                        objtxt.KeyDown += new KeyEventHandler(txt_popup_click);
                    }

                    objtxt.Enter -= new EventHandler(txtenter);
                    objtxt.Enter += new EventHandler(txtenter);
                    objtxt.Leave -= new EventHandler(txtleave);
                    objtxt.Leave += new EventHandler(txtleave);
                    pnlform.Controls.Add(objtxt);
                }
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                // objtxt.Multiline = true;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
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
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Text_Validating);

                objtxt.Enter -= new EventHandler(Text_enter);
                objtxt.Enter += new EventHandler(Text_enter);
                objtxt.Leave -= new EventHandler(Text_leave);
                objtxt.Leave += new EventHandler(Text_leave);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = String.Format("{0:F}", "0.00");
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Text = "0";
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Decimal_Validating);
                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);

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
                objchk.Tag = row["data_ty"].ToString().Trim().ToLower();
                objchk.Name = row["fld_nm"].ToString().Trim();
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                }
                objchk.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objchk.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                objchk.Validating += new CancelEventHandler(CheckBox_Validating);

                objchk.Enter -= new EventHandler(CheckBox_enter);
                objchk.Enter += new EventHandler(CheckBox_enter);
                objchk.Leave -= new EventHandler(CheckBox_leave);
                objchk.Leave += new EventHandler(CheckBox_leave);
                pnlform.Controls.Add(objchk);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                if (objBASEFILEDS.Curr_date)
                {
                    if (row["fld_nm"].ToString().Trim().ToUpper() == "TRAN_DT")
                    {
                        objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = DateTime.Now.ToString("yyyy/MM/dd");
                    }
                }
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                // dtp.CustomFormat = "dd-MMM-yyyy";
                dtp.CustomFormat = " ";
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                // dtp.DtValue = null;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 80 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 80 / 100, ctrlhgt);
                    }
                }
                dtp.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());

                //dtp.GotFocus -= new EventHandler(dateTimePicker1_GotFocus);
                //dtp.GotFocus += new EventHandler(dateTimePicker1_GotFocus);
                //dtp.KeyDown += new KeyEventHandler(dateTimePicker1_KeyDown);
                //dtp.KeyDown += new KeyEventHandler(dateTimePicker1_KeyDown);
                //dtp.DtValueChanged -= new EventHandler(dateTimePicker1_ValueChanged);
                //dtp.DtValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
                dtp.Validating += new CancelEventHandler(dateTimePicker1_Validating);

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
                btnform.Tag = row["data_ty"].ToString().Trim().ToLower();
                // btnform.Text = row["head_nm"].ToString().Trim();
                btnform.frmName = row["frm_nm"].ToString().Trim();
                btnform.PTextName = row["parent_ctrl"].ToString().Trim();
                btnform.Tbl_nm = row["tbl_nm"].ToString();
                btnform.Primaryddl = row["_primddl"].ToString().Trim();
                btnform.Dispddlfields = row["_Dpopflds"].ToString().Trim();
                btnform.Reftbltran_cd = row["reftbltran_cd"].ToString().Trim();
                btnform.Query_con = row["_querycon"].ToString().Trim();
                if (row["_isQcd"] != null && row["_isQcd"].ToString() != "" && bool.Parse(row["_isQcd"].ToString()))
                {
                    btnform.IsQcd = true;
                    btnform.QcdCondition = row["QcdCondition"] != null ? row["QcdCondition"].ToString() : "";
                }
                btnform.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                btnform.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                if (row["parent_ctrl"].ToString().Trim() == "")
                {
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (count % 2 == 0)
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 85 / 100);
                        }
                        else
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 85 / 100);
                        }
                        // btnform.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\add_fld.png");
                        btnform.Text = row["head_nm"].ToString().Trim();
                        //btnform.BackgroundImageLayout = ImageLayout.None;
                        btnform.Click += new System.EventHandler(btn_click);
                    }
                }
                else
                {
                    btnform.PTextName = row["parent_ctrl"].ToString().Trim();
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (btnform.Name == "PICK_UP_BTN")
                        {
                            //if (count % 2 == 0)
                            //{
                            //    btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid) * 15 / 100 + (ctrlwid) * 7 / 100 + wid, hgt, (ctrlwid / 2) * 10 / 100, ctrlhgt * 75 / 100);
                            //    //btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid/2) * 3 / 100 + wid, hgt, (ctrlwid / 2) * 15 / 100, ctrlhgt * 75 / 100);
                            //}
                            //else
                            //{
                            //    btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid) * 15 / 100 + (ctrlwid) * 7 / 100, hgt, (ctrlwid / 2) * 10 / 100, ctrlhgt * 75 / 100);
                            //    //btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2)+ (ctrlwid/2) * 3 / 100, hgt, (ctrlwid / 2) * 15 / 100, ctrlhgt * 75 / 100);
                            //}
                            //btnform.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\pickup.png");
                            //btnform.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                            //btnform.BackgroundImageLayout = ImageLayout.Zoom;
                            //btnform.Click += new System.EventHandler(btn_pickup_click);
                        }
                        else
                        {
                            if (count % 2 == 0)
                            {
                                btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid) * 15 / 100 + wid, hgt, (ctrlwid / 2) * 10 / 100, ctrlhgt * 75 / 100);
                                //btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid/2) * 10 / 100 + wid, hgt, (ctrlwid / 2) * 15 / 100, ctrlhgt * 75 / 100);
                            }
                            else
                            {
                                btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid) * 2 / 100 + (ctrlwid) * 15 / 100, hgt, (ctrlwid / 2) * 10 / 100, ctrlhgt * 75 / 100);
                                // btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid/2) * 10 / 100 , hgt, (ctrlwid / 2) * 15 / 100, ctrlhgt * 75 / 100);
                            }
                            btnform.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\search.jpg");
                            btnform.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                            btnform.BackgroundImageLayout = ImageLayout.Zoom;
                            btnform.Click += new System.EventHandler(btn_popup_click);
                        }
                    }
                }

                btnform.Enter -= new EventHandler(btn_enter);
                btnform.Enter += new EventHandler(btn_enter);
                btnform.Leave -= new EventHandler(btn_leave);
                btnform.Leave += new EventHandler(btn_leave);

                pnlform.Controls.Add(btnform);
            }
            //}
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
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void cmdseriesenter(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void txtenter(object sender, EventArgs e)
        {
            //  TextBox txt = (TextBox)sender;           
            PopupTextBox txt = (PopupTextBox)sender;
            if (txt != null)
            {
                txt.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
                if (txt.Name == "TRAN_NO")
                {
                    txt.ReadOnly = !objBASEFILEDS.Edit_tran_no;
                }
                if (txt.Primaryddl != "" && txt.Dispddlfields != "")
                {
                    objlableGrid.Visible = true;
                }
            }
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
            chk.BackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }
        private void dateTimePicker1_enter(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            dtp.CalendarTitleBackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
        }

        private void dateTimePicker1_Time_enter(object sender, EventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            dtp.CalendarTitleBackColor = objBASEFILEDS.ObjControlSet.On_focus != null ? Color.FromName(objBASEFILEDS.ObjControlSet.On_focus) : Color.White;
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
            if (objlableGrid.Visible)
                objlableGrid.Visible = false;
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

        private void Bind_Footer_Form_Controls(DataRow row)
        {
            count = int.Parse(row["order_no"].ToString().Trim());
            if (count % 2 != 0 && row["parent_ctrl"].ToString().Trim() == "")
            {
                if (count == 1 || count == 2)
                {
                    hgt = hgt + tabControl.Size.Height + ctrlhgt + 5;
                }
                else
                {
                    hgt += ctrlhgt;
                }
            }
            Label objlable = new Label();
            objlable.Name = "lbl" + row["head_nm"].ToString().Trim();
            objlable.Text = row["head_nm"].ToString().Trim();
            // objlable.BackColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
            //  objlable.TextAlign = ContentAlignment.TopRight;

            Label objlable1 = new Label();
            objlable1.Name = "lbl1" + row["head_nm"].ToString().Trim();
            objlable1.Text = bool.Parse(row["mandatory"].ToString().Trim()) ? " * " : "";
            objlable1.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable1.Visible = !bool.Parse(row["inter_val"].ToString().Trim());

            if (row["parent_ctrl"].ToString().Trim() == "" && !bool.Parse(row["inter_val"].ToString().Trim()) && !bool.Parse(row["_mul"].ToString().Trim()))
            {
                if (count % 2 == 0)
                {
                    objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                }
                else
                {
                    objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objlable1.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100 + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                }
            }
            pnlform.Controls.Add(objlable);
            pnlform.Controls.Add(objlable1);
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "";
                if (bool.Parse(row["_mul"].ToString().Trim()))
                {
                    ComboBox cmdseries = new ComboBox();
                    cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdseries.Name = row["fld_nm"].ToString().Trim();
                    cmdseries.Tag = row["data_ty"].ToString().Trim().ToLower();
                    DataSet dscmdseries = objFL_GEN_INV.GET_TBL_VAL(row["tbl_nm"].ToString().Trim(), this.tran_cd, objBASEFILEDS.ObjCompany.Compid.ToString());
                    cmdseries.DataSource = dscmdseries.Tables[0];
                    cmdseries.DisplayMember = row["sel_item"].ToString().Trim();
                    cmdseries.ValueMember = row["sel_val"].ToString().Trim();
                    cmdseries.Update();
                    if (dscmdseries != null && dscmdseries.Tables[0].Rows.Count != 0)
                    {
                        if (!bool.Parse(row["inter_val"].ToString().Trim()))
                        {
                            if (count % 2 == 0)
                            {
                                objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(wid + objlable.Width + objlable1.Width, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                            else
                            {
                                objlable.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                objlable1.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2) * 3 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                        }
                        cmdseries.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                        cmdseries.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                        cmdseries.Validating += new CancelEventHandler(cmd_validate);
                        cmdseries.Enter -= new EventHandler(cmdseries_enter);
                        cmdseries.Enter += new EventHandler(cmdseries_enter);

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
                    TextBox objtxt = new TextBox();
                    objtxt.Name = row["fld_nm"].ToString().Trim();
                    objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                    if (objtxt.Name.Trim().ToLower() == "tran_cd")
                    {
                        objBASEFILEDS.HTMAIN[objtxt.Name] = Tran_cd;
                    }
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (count % 2 == 0)
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                        else
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                    }
                    objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                    objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                    objtxt.Validating += new CancelEventHandler(String_Validating);
                    if (objtxt.Name.Trim().ToLower() == "tran_no")
                    {
                        objtxt.Enter -= new EventHandler(txt_enter);
                        objtxt.Enter += new EventHandler(txt_enter);
                    }

                    objtxt.Enter -= new EventHandler(txtenter);
                    objtxt.Enter += new EventHandler(txtenter);
                    objtxt.Leave -= new EventHandler(txtleave);
                    objtxt.Leave += new EventHandler(txtleave);
                    pnlform.Controls.Add(objtxt);
                }
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "0";
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Text = "0";
                objtxt.ReadOnly = true;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
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
                objtxt.ScrollBars = ScrollBars.Both;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    hgt += ctrlhgt + ctrlhgt;
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Text_Validating);

                objtxt.Enter -= new EventHandler(Text_enter);
                objtxt.Enter += new EventHandler(Text_enter);
                objtxt.Leave -= new EventHandler(Text_leave);
                objtxt.Leave += new EventHandler(Text_leave);
                pnlform.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = String.Format("{0:F}", "0.00");
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Text = String.Format("{0:F}", "0.00");
                objtxt.ReadOnly = true;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Validating += new CancelEventHandler(Decimal_Validating);
                objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                objtxt.TextAlign = HorizontalAlignment.Right;
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
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                objchk.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                objchk.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                objchk.Validating += new CancelEventHandler(CheckBox_Validating);

                objchk.Enter -= new EventHandler(CheckBox_enter);
                objchk.Enter += new EventHandler(CheckBox_enter);
                objchk.Leave -= new EventHandler(CheckBox_leave);
                objchk.Leave += new EventHandler(CheckBox_leave);
                pnlform.Controls.Add(objchk);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = "1900/01/01";
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                dtp.CustomFormat = "dd-MMM-yyyy";
                dtp.Format = DateTimePickerFormat.Custom;
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                dtp.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Validating += new CancelEventHandler(dateTimePicker1_Validating);

                dtp.Enter -= new EventHandler(dateTimePicker1_enter);
                dtp.Enter += new EventHandler(dateTimePicker1_enter);
                dtp.Leave -= new EventHandler(dateTimePicker1_leave);
                dtp.Leave += new EventHandler(dateTimePicker1_leave);
                pnlform.Controls.Add(dtp);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "time")
            {
                objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] = DateTime.Now.ToLongTimeString();
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                // dtp.CustomFormat = "dd-MMM-yyyy";
                dtp.Format = DateTimePickerFormat.Time;
                dtp.CustomFormat = "HH:mm";
                if (!bool.Parse(row["inter_val"].ToString().Trim()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) * 3 / 100, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                }
                dtp.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Validating += new CancelEventHandler(dateTimePicker1_Time_Validating);

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
                btnform.Tag = row["data_ty"].ToString().Trim().ToLower();
                btnform.Text = row["head_nm"].ToString().Trim();
                btnform.frmName = row["frm_nm"].ToString().Trim();
                btnform.Query_con = row["_querycon"].ToString().Trim();
                if (row["_isQcd"] != null && row["_isQcd"].ToString() != "" && bool.Parse(row["_isQcd"].ToString()))
                {
                    btnform.IsQcd = true;
                    btnform.QcdCondition = row["QcdCondition"] != null ? row["QcdCondition"].ToString() : "";
                }
                btnform.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                if (row["parent_ctrl"].ToString().Trim() == "")
                {
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
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
                    btnform.PTextName = row["parent_ctrl"].ToString().Trim();
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (count % 2 == 0)
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid / 2) + wid, hgt, (ctrlwid / 2) * 20 / 100, ctrlhgt);
                        }
                        else
                        {
                            btnform.Bounds = new Rectangle(objlable.Width + objlable1.Width + (ctrlwid / 2) + (ctrlwid / 2), hgt, (ctrlwid / 2) * 20 / 100, ctrlhgt);
                        }
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
        private void cmdseries_enter(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            DataRow[] rowtops = objBASEFILEDS.dsHeader.Tables[0].Select("_top=1 and fld_nm='" + txt.Name + "'");
            foreach (DataRow row in rowtops)
            {
                if (row["_query"] != null && row["_query"].ToString() != "")
                {
                    if (row["_querycon"] != null && row["_querycon"].ToString() != "")
                    {
                        DataSet dscmdseries = GetDatasetForCombobox(row["_query"].ToString(), row["_querycon"].ToString());

                        txt.DataSource = dscmdseries.Tables[0];
                        txt.DisplayMember = row["sel_item"].ToString().Trim();
                        txt.ValueMember = row["sel_val"].ToString().Trim();
                        txt.Update();
                    }
                }
            }
        }
        private DataSet GetDatasetForCombobox(string _sp_nm, string _querycon)
        {
            string sqlquerycon = "";
            string andorvalu = _querycon.Replace(" and ", "$ and $").Replace(" or ", "$ or $");
            string[] andorcond = andorvalu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < andorcond.Length; j++)
            {
                string valu = andorcond[j].Replace("=", "$=$").Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$");
                string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < cond.Length; i++)
                {
                    sqlquerycon += cond[i] + cond[i + 1] + "'" + objFL_GRIDEVENTS.InfixToPostfix(cond[i + 2], "string") + "'";
                    j++;
                    if (j < andorcond.Length)
                    {
                        sqlquerycon += andorcond[j];
                    }
                    i = i + 2;
                }
            }
            return objFL_GEN_INV.Execute_Procedure_Query(_sp_nm, sqlquerycon);
        }
        private void int_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {

                TextBox txt = (TextBox)sender;
                objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
                e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
            }
        }
        private void String_Validating(object sender, CancelEventArgs e)
        {
            flgDt = true;
            TextBox txt = (TextBox)sender;
            if (txt.Name.Trim().ToLower() == "tran_no")
            {
                // txt.Enter -= new EventHandler(txt_enter);
                txt.Enter += new EventHandler(txt_enter);
                txt.Leave += new EventHandler(txt_leave);
            }
            if (txt.Name.Trim().ToLower() == "tran_no" && txt.Text != "" && objBASEFILEDS.Bck_entry == "A-Allow Back End Date Entry" && objBASEFILEDS.Tran_mode == "add_mode")
            {
                if (objBASEFILEDS.HTMAIN.Contains("tran_dt") && objBASEFILEDS.HTMAIN["tran_dt"] != null && objBASEFILEDS.HTMAIN["tran_dt"].ToString() != "")
                {
                    if (objBASEFILEDS.DsDateTime != null && objBASEFILEDS.DsDateTime.Tables.Count != 0 && objBASEFILEDS.DsDateTime.Tables[0].Rows.Count != 0)
                    {
                        DataSet dsetDt = objFL_Transaction.GetDataSet("select isnull(max(tran_no),0) tran_no from " + objBASEFILEDS.Main_tbl_nm + " where tran_dt<='" + objBASEFILEDS.HTMAIN["tran_dt"].ToString() + "' and tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                        if (dsetDt != null && dsetDt.Tables.Count != 0 && dsetDt.Tables[0].Rows.Count != 0)//&& dsetDt.Tables[0].Rows[0]["tran_no"].ToString() != "0")
                        {
                            if (objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"] != null && objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString() != "")
                            {
                                if (DateTime.Parse(DateTime.Parse(objBASEFILEDS.HTMAIN["tran_dt"].ToString()).ToShortDateString()) < DateTime.Parse(DateTime.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString()).ToShortDateString()))
                                {
                                    if (dsetDt.Tables[0].Rows[0]["tran_no"].ToString() != "0")
                                    {
                                        if (int.Parse(txt.Text != "" ? txt.Text : "0") >= int.Parse(dsetDt.Tables[0].Rows[0]["tran_no"].ToString()))
                                        {
                                            AutoClosingMessageBox.Show("Sorry!! Transaction No. should less for Back End Date Entry", "Date");
                                            flgDt = false;
                                        }
                                    }
                                    else
                                    {
                                        if (int.Parse(txt.Text != "" ? txt.Text : "0") >= int.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_no"].ToString()))
                                        {
                                            AutoClosingMessageBox.Show("Sorry!! Transaction No. should less for Back End Date Entry", "Date");
                                            flgDt = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // flgDt = true;
            }
            //else if (txt.Name.Trim().ToLower() == "tran_no" && txt.Text != "" && objBASEFILEDS.Bck_entry == "C-Allow Greater Transaction No. For Back Date Entry" && objBASEFILEDS.Tran_mode == "add_mode")
            //{
            //    if (objBASEFILEDS.HTMAIN.Contains("tran_dt") && objBASEFILEDS.HTMAIN["tran_dt"] != null && objBASEFILEDS.HTMAIN["tran_dt"].ToString() != "")
            //    {
            //        DataSet dsetDt = objFL_Transaction.GetDataSet("select isnull(max(tran_no),0) tran_no from " + objBASEFILEDS.Main_tbl_nm + " where tran_dt<='" + objBASEFILEDS.HTMAIN["tran_dt"].ToString() + "' and tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            //        if (dsetDt != null && dsetDt.Tables.Count != 0 && dsetDt.Tables[0].Rows.Count != 0 && dsetDt.Tables[0].Rows[0]["tran_no"].ToString() != "0")
            //        {
            //            if (int.Parse(txt.Text != "" ? txt.Text : "0") < int.Parse(dsetDt.Tables[0].Rows[0]["tran_no"].ToString()))
            //            {
            //                MessageBox.Show("Sorry!! Transaction No. should greater for Back End Date Entry");
            //                flgDt = false;
            //            }
            //            else
            //            {
            //                flgDt = true;
            //            }
            //        }
            //    }
            //    //flgDt = true;
            //}
            else { flgDt = true; }

            if (flgDt)
            {
                objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
                e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void cmd_validate(object sender, CancelEventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            objBASEFILEDS.HTMAIN[txt.DisplayMember] = txt.Text.ToString().Trim();
            if (txt.SelectedValue != null)
            {
                objBASEFILEDS.HTMAIN[txt.Name] = txt.SelectedValue.ToString().Trim();
                objBASEFILEDS.HTMAIN[txt.ValueMember] = txt.SelectedValue.ToString().Trim();
            }
            e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
            if (!e.Cancel)
                e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
            if (txt.Name == "RULE")
            {
                if (txt.Text == "NON-EXCISABLE" || txt.Text == "CT-1" || txt.Text == "CT-3")
                    allow_excise_calc = false;
                else
                    allow_excise_calc = true;
            }

        }
        private void txt_enter(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Name == "TRAN_NO")
            {
                txt.ReadOnly = !objBASEFILEDS.Edit_tran_no;
            }
            Control[] tbxs = this.Controls.Find("TRAN_SR", true);
            if (tran_mode == "add_mode" && objBASEFILEDS.Auto_tran_no)
            {
                if (tbxs != null && tbxs.Length > 0)
                {
                    ComboBox objtxt = tbxs[0] as ComboBox;
                    if (objtxt != null && objtxt.Visible)
                    {
                        objBASEFILEDS.HTMAIN["tran_sr"] = objtxt.Text;
                        //txt.Text = objFL_GEN_INV.Gen_Number(Tran_cd, objtxt.Text, objBASEFILEDS.ObjCompany.Fin_yr, objBASEFILEDS.ObjCompany.Compid.ToString());
                        txt.Text = objFL_GEN_INV.Gen_Number(objBASEFILEDS, objtxt.Text);
                    }
                    else
                    { //txt.Text = objFL_GEN_INV.Gen_Number(Tran_cd, "", objBASEFILEDS.ObjCompany.Fin_yr, objBASEFILEDS.ObjCompany.Compid.ToString()); }
                        txt.Text = objFL_GEN_INV.Gen_Number(objBASEFILEDS, "");
                    }

                    objBASEFILEDS.HTMAIN[txt.Name] = txt.Text;
                }
                else
                {
                    // txt.Text = objFL_GEN_INV.Gen_Number(Tran_cd, "", objBASEFILEDS.ObjCompany.Fin_yr, objBASEFILEDS.ObjCompany.Compid.ToString());
                    txt.Text = objFL_GEN_INV.Gen_Number(objBASEFILEDS, "");
                    objBASEFILEDS.HTMAIN[txt.Name] = txt.Text;
                }
                txt.Enter -= new EventHandler(txt_enter);
            }
        }
        private void txt_leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            objBASEFILEDS.HTMAIN[txt.Name] = txt.Text;
            if (tran_mode == "add_mode")
            {
                if (objFL_GEN_INV.Find_Gen_Miss(objBASEFILEDS.HTMAIN) == 2)
                {
                    AutoClosingMessageBox.Show("Transaction Number already exist", "Error");
                    txt.Text = "";
                    // objBASEFILEDS.HTMAIN[txt.Name] = "";
                }
                txt.Leave -= new EventHandler(txt_leave);
            }

        }
        private void Text_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                TextBox txt = (TextBox)sender;
                objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
                e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
            }
        }
        private void Decimal_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                TextBox txt = (TextBox)sender;
                objBASEFILEDS.HTMAIN[txt.Name] = txt.Text.Trim();
                e.Cancel = ValidateFields(txt.Name, txt.Text, "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
                if (!e.Cancel)
                {
                    if (txt.Name == "net_amt" && txt.Text != "" && txt.Text != "0" && txt.Text != "0.00")
                    {
                        AccountNet_AmtBindGrid();//Load_AccountGrid
                    }
                }
            }
        }
        private void CheckBox_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                CheckBox chk = (CheckBox)sender;
                objBASEFILEDS.HTMAIN[chk.Name] = chk.Checked.ToString();
                e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString().Trim(), "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString().Trim(), "valid_con");
                if (chk.Name == "INCL_EXC")
                    allow_excise_calc = chk.Checked;
            }
        }
        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                UserDT dtp = (UserDT)sender;
                flgDt = true;
                //Sharanamma Jekeen added on 03.06.2014 --Finacial year removed  based on IISc requirement.
                if (dtp.Name.Trim().ToLower() == "tran_dt")
                {
                    if (dtp.DtValue != null)// && dtp.DtValue >= objBASEFILEDS.ObjCompany.Fin_yr_sta && dtp.DtValue <= objBASEFILEDS.ObjCompany.Fin_yr_end)
                    {
                        if (dtp.Name.Trim().ToLower() == "tran_dt" && objBASEFILEDS.Bck_entry == "A-Allow Back End Date Entry" && objBASEFILEDS.Tran_mode == "add_mode")
                        {
                            flgDt = true;
                        }
                        else if (dtp.Name.Trim().ToLower() == "tran_dt" && objBASEFILEDS.Bck_entry == "B-Do Not Allow Back End Date Entry" && objBASEFILEDS.Tran_mode == "add_mode")
                        {
                            // DataSet objBASEFILEDS.DsDateTime = objFL_Transaction.GetDataSet("select max(tran_dt) tran_dt from " + objBASEFILEDS.Main_tbl_nm + " where tran_cd='" + objBASEFILEDS.Code + "' and compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
                            if (objBASEFILEDS.DsDateTime != null && objBASEFILEDS.DsDateTime.Tables.Count != 0 && objBASEFILEDS.DsDateTime.Tables[0].Rows.Count != 0)
                            {
                                if (objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"] != null && objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString() != "")
                                {
                                    if (DateTime.Parse(dtp.DtValue.ToShortDateString()) >= DateTime.Parse(objBASEFILEDS.DsDateTime.Tables[0].Rows[0]["tran_dt"].ToString()))
                                    {
                                        flgDt = true;
                                    }
                                    else
                                    {
                                        flgDt = false;
                                        AutoClosingMessageBox.Show("Do Not Allow Back End Date Entry", "Date");
                                    }
                                }
                            }
                        }
                        else { flgDt = true; }
                    }
                    //else
                    //{
                    //    flgDt = false;
                    //    MessageBox.Show("Sorry!! Date is not in Financial Year Date");
                    //}
                }
                if (flgDt)
                {
                    objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
                    e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString().Trim(), "_mon_con");
                    if (!e.Cancel)
                        e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString().Trim(), "valid_con");
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        private void dateTimePicker1_Time_Validating(object sender, CancelEventArgs e)
        {
            if (objBASEFILEDS.Tran_mode != "view_mode")
            {
                UserDT dtp = (UserDT)sender;
                objBASEFILEDS.HTMAIN[dtp.Name] = dtp.DtValue.ToLongTimeString();
                e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToLongTimeString(), "_mon_con");
                if (!e.Cancel)
                    e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToLongTimeString(), "valid_con");
            }
        }
        private bool ValidateFields(string fld_nm, string fld_val, string con_nm, int flg = 0)
        {
            if (tran_mode == "add_mode" || tran_mode == "edit_mode")
            {
                DataRow[] rowdtp;

                if (flg == 0)//headerwise or itemwise
                {
                    rowdtp = objBASEFILEDS.dsHeader.Tables[0].Select("fld_nm='" + fld_nm + "'");
                }
                else
                {
                    rowdtp = objBASEFILEDS.dsFooter.Tables[0].Select("fld_nm='" + fld_nm + "'");
                }
                bool validflg = true;
                if (rowdtp.Length != 0)
                {
                    validflg = ValidateExpresstion(rowdtp, fld_nm, fld_val, con_nm, flg);
                }
                else
                {
                    if (flg == 0)//headerwise or itemwise
                    {
                        rowdtp = objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Select("fld_nm='" + fld_nm + "' and disp_head=1");
                    }
                    else
                    {
                        rowdtp = objBASEFILEDS.dsBASEADDIFIELDITEM.Tables[0].Select("fld_nm='" + fld_nm + "' and disp_head=1");
                    }
                    if (rowdtp.Length != 0)
                    {
                        validflg = ValidateExpresstion(rowdtp, fld_nm, fld_val, con_nm, flg);
                    }
                }
                return !validflg;
            }
            return false;
        }

        private bool ValidateExpresstion(DataRow[] rowdtp, string fld_nm, string fld_val, string con_nm, int flg = 0)
        {
            bool validflg = true;
            string _strValue = "";
            string _strDataType = "varchar";

            foreach (DataRow row in rowdtp)
            {
                _strDataType = row["data_ty"].ToString().Trim().ToLower();
                string exp = row[con_nm].ToString().Trim();
                string[] ar = exp.Split(new Char[] { '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (ar.Length >= 1)
                {
                    if (ar.Length == 1)
                    {
                        string exp1 = ar[0];
                        string[] ar1 = exp1.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string str in ar1)
                        {
                            validflg = CallCustomMethod(str, row["tbl_nm"].ToString(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
                            if (!validflg)
                            {
                                break;
                            }
                        }
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
                                if (_strDataType == "decimal" && reg.IsMatch(fld_val))
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
                                    AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should not be empty", "Valid");
                                    validflg = false;
                                }
                                else
                                {
                                    validflg = true;
                                }
                            }
                            else
                            {
                                AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString().Trim(), "Valid");
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
                            string valu = ar[0].Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$").Replace("=", "$=$");
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
                                        case "<": if (decimal.Parse(cod1) < decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be less than  " + cod2, "Validation"); } break;
                                        case ">": if (decimal.Parse(cod1) > decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be greater than  " + cod2, "Validation"); } break;
                                        case "<=": if (decimal.Parse(cod1) <= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be less than equal to " + cod2, "Validation"); } break;
                                        case ">=": if (decimal.Parse(cod1) >= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be greater than equal to  " + cod2, "Validation"); } break;
                                        case "==": if (decimal.Parse(cod1) == decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be equal to  " + cod2, "Validation"); } break;
                                        case "!=": if (decimal.Parse(cod1) != decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(row["head_nm"].ToString().Trim() + " should be not equal to  " + cod2, "Validation"); } break;
                                        default: validflg = false; break;
                                    }
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString().Trim(), "Valid");
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
                                    validflg = CallCustomMethod(str, row["tbl_nm"].ToString(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
                                    if (!validflg)
                                    {
                                        break;
                                    }
                                }
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
                                    validflg = CallCustomMethod(str, row["tbl_nm"].ToString(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
                                    if (!validflg)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                //MessageBox.Show(fld_nm +" SHOULD NOT BE EMPTY");
                            }
                        }
                    }
                }
            }
            return validflg;
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
                    objBASEFILEDS.HTITEM = objVALIDATION.ObjBLFD.HTITEM;
                    BindControlsFromView();
                }
            }
            else
            {
                if (objiTRANSACTION.GetType().GetMethod(_method_nm) != null)
                {
                    objiTRANSACTION.ACTIVE_TRANSACTION = objBASEFILEDS;
                    SetFieldsValue(tbl_nm, fld_nm, fld_val);
                    MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(_method_nm);
                    validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, null).ToString().Trim());
                    if (!validflg)
                    {
                        if (objiTRANSACTION.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objiTRANSACTION.BL_FIELDS.Errormsg, "Transaction Validation");
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        }
                    }
                    else
                    {
                        objBASEFILEDS.HTMAIN = objiTRANSACTION.ACTIVE_TRANSACTION.HTMAIN;
                        objBASEFILEDS.HTITEM = objiTRANSACTION.ACTIVE_TRANSACTION.HTITEM;
                        BindControlsFromView();
                    }
                }
                else
                {
                    if (flg == 1)
                    {
                        //if (fld_nm.ToLower() == "prod_nm")
                        //{
                        //    if (objiITEMVALID.GetType().GetMethod("iITEMValidate") != null)
                        //    {
                        //        objiITEMVALID.Hashitemvalue = ((Hashtable)objBASEFILEDS.HTITEM[grid.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                        //        objiITEMVALID.ACTIVE_BL = objBASEFILEDS;
                        //        SetFieldsValue(tbl_nm, fld_nm, fld_val);
                        //        MethodInfo methodInfo = typeof(iITEMVALID).GetMethod("iITEMValidate");
                        //        validflg = bool.Parse(methodInfo.Invoke(objiITEMVALID, null).ToString().Trim());
                        //        if (!validflg)
                        //        {
                        //            if (objiITEMVALID.BL_FIELDS.Errormsg.Length != 0)
                        //            {
                        //                AutoClosingMessageBox.Show(objiITEMVALID.BL_FIELDS.Errormsg, "Product Validation");
                        //            }
                        //            else
                        //            {
                        //                AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        //            }
                        //        }
                        //        else
                        //        {
                        //            Hashtable HTITEMVal = objBASEFILEDS.HTITEM;
                        //            Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        //            foreach (DictionaryEntry entry in HTITEMVal)
                        //            {
                        //                htcuritem.Clear();
                        //                foreach (DictionaryEntry entry1 in objiITEMVALID.Hashitemvalue)
                        //                {
                        //                    htcuritem.Add(entry1.Key, entry1.Value);
                        //                }
                        //                foreach (DictionaryEntry entry1 in htcuritem)
                        //                {
                        //                    if (grid.CurrentRow.Cells["PTSERIAL"].Value.ToString() == entry.Key.ToString())
                        //                    {
                        //                        if (((Hashtable)objBASEFILEDS.HTITEM[entry.Key]).Contains(entry1.Key))
                        //                        {
                        //                            ((Hashtable)objBASEFILEDS.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            BindControlsFromView();
                        //        }
                        //    }
                        //    else
                        //    {

                        //    }
                        //}
                        //else if (fld_nm.ToLower() == "qty")
                        //{
                        //    if (objiQTYVALID.GetType().GetMethod("iQTYValidate") != null)
                        //    {
                        //        SetFieldsValue(tbl_nm, fld_nm, fld_val);
                        //        objiQTYVALID.Hashqtyvalue = ((Hashtable)objBASEFILEDS.HTITEM[grid.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                        //        objiQTYVALID.ACTIVE_BL = objBASEFILEDS;
                        //        MethodInfo methodInfo = typeof(iQTYVALID).GetMethod("iQTYValidate");
                        //        validflg = bool.Parse(methodInfo.Invoke(objiQTYVALID, null).ToString().Trim());
                        //        if (!validflg)
                        //        {
                        //            if (objiQTYVALID.BL_FIELDS.Errormsg.Length != 0)
                        //            {
                        //                AutoClosingMessageBox.Show(objiQTYVALID.BL_FIELDS.Errormsg, "Quantity Validation");
                        //            }
                        //            else
                        //            {
                        //                AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        //            }
                        //        }
                        //        else
                        //        {
                        //            Hashtable HTITEMVal = objBASEFILEDS.HTITEM;
                        //            Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        //            foreach (DictionaryEntry entry in HTITEMVal)
                        //            {
                        //                htcuritem.Clear();
                        //                foreach (DictionaryEntry entry1 in objiQTYVALID.Hashqtyvalue)
                        //                {
                        //                    htcuritem.Add(entry1.Key, entry1.Value);
                        //                }
                        //                foreach (DictionaryEntry entry1 in htcuritem)
                        //                {
                        //                    if (grid.CurrentRow.Cells["PTSERIAL"].Value.ToString() == entry1.Key.ToString())
                        //                    {
                        //                        if (((Hashtable)objBASEFILEDS.HTITEM[entry.Key]).Contains(entry1.Key))
                        //                        {
                        //                            ((Hashtable)objBASEFILEDS.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            BindControlsFromView();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //if (objCustomCell.GetType().GetMethod("ValidateCell") != null)
                        //{
                        //    objCustomCell.HashgridItemvalue = ((Hashtable)objBASEFILEDS.HTITEM[grid.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                        //    objCustomCell.ACTIVE_BL = objBASEFILEDS;
                        //    //SetFieldsValue(tbl_nm, fld_nm, fld_val);
                        //    MethodInfo methodInfo = typeof(iCELL).GetMethod("ValidateCell");
                        //    validflg = bool.Parse(methodInfo.Invoke(objCustomCell, null).ToString().Trim());
                        //    if (!validflg)
                        //    {
                        //        if (objCustomCell.BL_FIELDS.Errormsg.Length != 0)
                        //        {
                        //            AutoClosingMessageBox.Show(objCustomCell.BL_FIELDS.Errormsg, "Cell Validation");
                        //        }
                        //        else
                        //        {
                        //            AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Hashtable HTITEMVal = objBASEFILEDS.HTITEM;
                        //        Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        //        foreach (DictionaryEntry entry in HTITEMVal)
                        //        {
                        //            htcuritem.Clear();
                        //            foreach (DictionaryEntry entry1 in objCustomCell.HashgridItemvalue)
                        //            {
                        //                htcuritem.Add(entry1.Key, entry1.Value);
                        //            }
                        //            foreach (DictionaryEntry entry1 in htcuritem)
                        //            {
                        //                if (grid.CurrentRow.Cells["PTSERIAL"].Value.ToString() == entry.Key.ToString())
                        //                {
                        //                    if (((Hashtable)objBASEFILEDS.HTITEM[entry.Key]).Contains(entry1.Key))
                        //                    {
                        //                        ((Hashtable)objBASEFILEDS.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        BindControlsFromView();
                        //    }
                        //}
                        //if (_method_nm != "ValidateCell")
                        //{
                        //    if (objiGRIDITEM.GetType().GetMethod(_method_nm) != null)
                        //    {
                        //        SetFieldsValue(tbl_nm, fld_nm, fld_val);
                        //        objiGRIDITEM.HashgridItemvalue = ((Hashtable)objBASEFILEDS.HTITEM[grid.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                        //        objiGRIDITEM.ACTIVE_BL = objBASEFILEDS;
                        //        MethodInfo methodInfo = typeof(iGRIDITEM).GetMethod(_method_nm);
                        //        validflg = bool.Parse(methodInfo.Invoke(objiGRIDITEM, null).ToString().Trim());
                        //        if (!validflg)
                        //        {
                        //            if (objiGRIDITEM.BL_FIELDS.Errormsg.Length != 0)
                        //            {
                        //                AutoClosingMessageBox.Show(objiGRIDITEM.BL_FIELDS.Errormsg, "Grid Row Validation");
                        //            }
                        //            else
                        //            {
                        //                AutoClosingMessageBox.Show("Please enter Valid " + header_nm, "Validation");
                        //            }
                        //        }
                        //        else
                        //        {
                        //            Hashtable HTITEMVal = objBASEFILEDS.HTITEM;
                        //            Hashtable htcuritem = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        //            foreach (DictionaryEntry entry in HTITEMVal)
                        //            {
                        //                htcuritem.Clear();
                        //                foreach (DictionaryEntry entry1 in objiGRIDITEM.HashgridItemvalue)
                        //                {
                        //                    htcuritem.Add(entry1.Key, entry1.Value);
                        //                }
                        //                foreach (DictionaryEntry entry1 in htcuritem)
                        //                {
                        //                    if (grid.CurrentRow.Cells["PTSERIAL"].Value.ToString() == entry1.Key.ToString())
                        //                    {
                        //                        if (((Hashtable)objBASEFILEDS.HTITEM[entry.Key]).Contains(entry1.Key))
                        //                        {
                        //                            ((Hashtable)objBASEFILEDS.HTITEM[entry.Key])[entry1.Key] = entry1.Value;
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //            BindControlsFromView();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        AutoClosingMessageBox.Show("Sorry!! Method is not defined!!", "Validation");
                        //        validflg = false;
                        //    }
                        //}
                        //}
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
                    else
                    {
                        if (objBASEFILEDS.HTMAIN[c.Name] != null)
                        {
                            c.Text = objBASEFILEDS.HTMAIN[c.Name].ToString();
                        }
                    }
                    //if (tran_mode != "view_mode")
                    //    c.Enabled = true;
                }
                #region comment
                //if (c is TabControl)
                //{
                //    foreach (Control c1 in c.Controls)
                //    {
                //        if (c1 is TabPage)
                //        {
                //            foreach (Control c2 in c1.Controls)
                //            {
                //                if (c2 is MyDataGridView)
                //                {
                //                    if (c2.Name == "PROD_GRID")
                //                    {
                //                        foreach (DataGridViewRow row in ((DataGridView)c2).Rows)
                //                        {
                //                            foreach (DataGridViewColumn column in ((DataGridView)c2).Columns)
                //                            {
                //                                if (objBASEFILEDS.HTITEM.Contains(row.Cells["PTSERIAL"].Value.ToString()))
                //                                {
                //                                    if (column is DataGridViewTextBoxColumn)
                //                                    {
                //                                        if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] != null)
                //                                        {
                //                                            if (column.Tag.ToString() == "decimal")
                //                                            {
                //                                                if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] != null && ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString() != "")
                //                                                {
                //                                                    row.Cells[column.Name].Value = decimal.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString());
                //                                                }
                //                                                else
                //                                                {
                //                                                    row.Cells[column.Name].Value = "0.00";
                //                                                }
                //                                            }
                //                                            else if (column.Tag.ToString() == "int")
                //                                            {
                //                                                if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] != null && ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString() != "")
                //                                                {
                //                                                    row.Cells[column.Name].Value = int.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString());
                //                                                }
                //                                                else
                //                                                {
                //                                                    row.Cells[column.Name].Value = "0";
                //                                                }
                //                                            }
                //                                            else if (column.Tag.ToString() == "datetime")
                //                                            {
                //                                                if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] != null && ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString() != "")
                //                                                {
                //                                                    if (((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString() != "1900-01-01 12:00:00 AM" && DateTime.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString()).ToString("yyyy/mm/dd") != "2000-00-01" && DateTime.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString()).ToString("yyyy/mm/dd") != "1900-00-01" && DateTime.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString()).ToString("yyyy/mm/dd") != "1900/00/01" && DateTime.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString()).ToString("yyyy/mm/dd") != "2000/00/01")
                //                                                    {
                //                                                        row.Cells[column.Name].Value = DateTime.Parse(((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString()).ToString("yyyy/mm/dd");
                //                                                    }
                //                                                    else
                //                                                    {
                //                                                        row.Cells[column.Name].Value = "";
                //                                                    }
                //                                                }
                //                                                else
                //                                                {
                //                                                    row.Cells[column.Name].Value = "";
                //                                                }
                //                                            }
                //                                            else
                //                                            {
                //                                                row.Cells[column.Name].Value = ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name].ToString();
                //                                            }
                //                                        }
                //                                    }
                //                                }

                //                            }
                //                        }
                //                    }
                //                    else
                //                    {

                //                    }
                //                }
                //                if (c2 is Panel)
                //                {
                //                    foreach (DataGridViewRow r in gridTax.Rows)
                //                    {
                //                        if (objBASEFILEDS.HTMAIN.ContainsKey("TAX_NM") && r.Cells["charge_type"].Value.ToString().Trim() == "")
                //                        {
                //                            gridTax[r.Cells["name"].ColumnIndex, r.Cells["fld_nm"].RowIndex].Value = objBASEFILEDS.HTMAIN["TAX_NM"].ToString().Trim();
                //                            DataRow[] rows = objBASEFILEDS.dsSTFIELDS.Tables[0].Select("tax_nm='" + objBASEFILEDS.HTMAIN["TAX_NM"].ToString().Trim() + "'");
                //                            foreach (DataRow row in rows)
                //                            {
                //                                r.Cells["pert"].Value = row["pert_val"] != null ? row["pert_val"].ToString().Trim() : "0";
                //                            }
                //                        }
                //                        else
                //                        {
                //                            if (objBASEFILEDS.HTMAIN.ContainsKey(r.Cells["fld_nm"].Value))
                //                            {
                //                                gridTax[r.Cells["amount"].ColumnIndex, r.Cells["fld_nm"].RowIndex].Value = objBASEFILEDS.HTMAIN[r.Cells["fld_nm"].Value] != null ? objBASEFILEDS.HTMAIN[r.Cells["fld_nm"].Value] : "0";
                //                            }
                //                            if (objBASEFILEDS.HTMAIN.ContainsKey(r.Cells["pert_nm"].Value))
                //                            {
                //                                gridTax[r.Cells["pert"].ColumnIndex, r.Cells["fld_nm"].RowIndex].Value = objBASEFILEDS.HTMAIN[r.Cells["pert_nm"].Value] != null ? objBASEFILEDS.HTMAIN[r.Cells["pert_nm"].Value] : "0";
                //                            }
                //                        }
                //                    }
                //                    GetExciseCalculation();
                //                    //Account_Posting();
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
        }
        private void btn_click(object sender, EventArgs e)
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
                    //objBASEFILEDS.HTITEM = objiButtonEvent.ACTIVE_BL.HTITEM;
                    //if (objiButtonEvent.BL_FIELDS.Bind_type != null && objiButtonEvent.BL_FIELDS.Bind_type == "GRID")
                    //{
                    //    BindGridFromPickup(objBASEFILEDS.Code);//change
                    //}
                    BindControlsFromView();
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
        private void btn_popup_click(object sender, EventArgs e)
        {
            try
            {
                if (tran_mode != "view_mode")
                {
                    PopupButton btn = (PopupButton)sender;
                    if (btn.Primaryddl != "" && btn.Dispddlfields != "")
                    {
                        frmPopup frmpopup = new frmPopup(objBASEFILEDS.HTMAIN, btn.Tbl_nm, btn.Reftbltran_cd, btn.Primaryddl, btn.Dispddlfields, "Please select", btn.Query_con, btn.IsQcd, btn.QcdCondition, "0");
                        //frmpopup.objCompany = objBASEFILEDS.ObjCompany;
                        //frmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
                        frmpopup.ObjBFD = objBASEFILEDS;
                        frmpopup.ShowDialog();
                        Control[] txts = this.Controls.Find(btn.PTextName, true);
                        if (txts != null)
                        {
                            txts[0].Text = objBASEFILEDS.HTMAIN[btn.PTextName].ToString();
                            ValidateFields(txts[0].Name, txts[0].Text, "_mon_con");
                            ValidateFields(txts[0].Name, txts[0].Text, "valid_con");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void txt_popup_click(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F2)
                {
                    PopupTextBox txt = (PopupTextBox)sender;
                    if (txt.Primaryddl != "" && txt.Dispddlfields != "")
                    {
                        frmPopup frmpopup = new frmPopup(objBASEFILEDS.HTMAIN, txt.Tbl_nm, txt.Reftbltran_cd, txt.Primaryddl, txt.Dispddlfields, "Please select", txt.Query_con, txt.IsQcd, txt.QcdCondition, "0");
                        //frmpopup.objCompany = objBASEFILEDS.ObjCompany;
                        //frmpopup.objControlSet = objBASEFILEDS.ObjControlSet;
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
        #endregion Header/Footer

        #region ItemGrid
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
        #endregion ItemGrid

        #region TaxGrid
        private void Bind_TabPages()
        {
            tabControl.Bounds = new Rectangle((ctrlwid / 2) * 3 / 100, hgt + ctrlhgt, this.ClientSize.Width - (ctrlwid / 2) * 3 / 100, (this.ClientSize.Height - (hgt + ctrlhgt)) * 75 / 100);
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

            tabControl.Enter -= new EventHandler(tab_enter);
            tabControl.Enter += new EventHandler(tab_enter);

            TabPage tabPage2 = new TabPage();
            tabPage2.Name = "tabPage2";
            tabPage2.Text = " ACCOUNT DETAILS      ";
            tabPage2.BorderStyle = BorderStyle.Fixed3D;
            pnlAccountGrid.BorderStyle = BorderStyle.Fixed3D;

            pnlAccountGrid.Bounds = new Rectangle(0, 0, tabControl.Size.Width, tabControl.Size.Height - (tabControl.Size.Height * 10 / 100));
            tabPage2.Controls.Add(pnlAccountGrid);
            tabControl.Controls.Add(tabPage2);

            tabControl.DrawItem += new DrawItemEventHandler(tabcontrol_drawitem);
            tabControl.SelectedIndex = 0;
            pnlform.Controls.Add(tabControl);
            pnlform.BorderStyle = BorderStyle.None;

            objBASEFILEDS.X_gridAccount = (ctrlwid / 2) * 15 / 100; objBASEFILEDS.Y_gridAccount = hgt + 4 * ctrlhgt;
            objBASEFILEDS.Width_gridAccount = pnlAccountGrid.Size.Width - 10; objBASEFILEDS.Hgt_gridAccount = pnlAccountGrid.Size.Height - ctrlhgt - 10;
        }

        private void tabcontrol_drawitem(object sender, DrawItemEventArgs e)
        {
            #region
            //string tabName = tabControl.TabPages[e.Index].Text;
            //StringFormat stringFormat = new StringFormat();
            //stringFormat.Alignment = StringAlignment.Center;
            //stringFormat.LineAlignment = StringAlignment.Center;
            ////Find if it is selected, this one will be hightlighted...
            //if (e.Index == tabControl.SelectedIndex)
            //    e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            //e.Graphics.DrawString(tabName, this.Font, Brushes.Black, tabControl.GetTabRect(e.Index), stringFormat);
            #endregion
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
        private void tab_enter(object sender, EventArgs e)
        {
            #region
            //Load_AccountGrid();
            bool flg = false;
            foreach (DataRow row in objBASEFILEDS.dsHeader.Tables[0].Rows)
            {
                if (objBASEFILEDS.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")//&& row["valid_con"].ToString() != "" 
                {
                    //flg = ValidateFields(row["fld_nm"].ToString(), objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString());
                    flg = ValidateFields(row["fld_nm"].ToString(), objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "_mon_con");
                    if (flg && row["valid_con"].ToString() != "")
                    {
                        flg = ValidateFields(row["fld_nm"].ToString(), objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "valid_con");
                    }
                    if (flg)
                    {
                        Control[] ctrl = this.Controls.Find(row["fld_nm"].ToString(), true);
                        if (ctrl != null)
                        {
                            ctrl[0].Focus();
                        }
                        break;
                    }
                }
            }
            if (!flg)
            {
                foreach (DataRow row in objBASEFILEDS.dsBASEADDIFIELD.Tables[0].Rows)
                {
                    if (objBASEFILEDS.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString().ToLower() != "false")//&& row["valid_con"].ToString() != ""
                    {
                        flg = ValidateFields(row["fld_nm"].ToString(), objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "_mon_con");
                        if (flg && row["valid_con"].ToString() != "")
                        {
                            flg = ValidateFields(row["fld_nm"].ToString(), objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "valid_con");
                        }
                        if (flg)
                        {
                            break;
                        }
                    }
                }
            }
            if (!flg)
                objBASEFILEDS.BlnStopItemEnter = true;
            #endregion
        }

        #endregion TaxGrid

        #region Accounts
        private void Bind_ContextMenustripForAccount()
        {
            PopupMenuAccount.Name = "PopupMenuAccount";
            PopupMenuAccount.Text = "Account File Menu";

            ToolStripMenuItem AddMenu = new ToolStripMenuItem("Add");
            AddMenu.Text = "ADD ACCOUNT(CTRL+I)";
            AddMenu.TextAlign = ContentAlignment.BottomRight;
            AddMenu.Click += new System.EventHandler(this.AddMenuAccountClick);
            PopupMenuAccount.Items.Add(AddMenu);

            ToolStripMenuItem RemoveMenu = new ToolStripMenuItem("Remove");
            RemoveMenu.Text = "REMOVE ACCOUNT(CTRL+R)";
            RemoveMenu.TextAlign = ContentAlignment.BottomRight;
            RemoveMenu.Click += new System.EventHandler(this.RemoveMenuAccountClick);
            PopupMenuAccount.Items.Add(RemoveMenu);

            PopupMenuAccount.GripStyle = ToolStripGripStyle.Visible;
        }

        private void AddMenuAccountClick(object sender, EventArgs e)
        {
            if (objBASEFILEDS.BlnStopItemEnter)
            {
                AddAccount();
            }
        }
        private void RemoveMenuAccountClick(object sender, EventArgs e)
        {
            if (objBASEFILEDS.BlnStopItemEnter)
            {
                RemoveAccount();
            }
        }

        private void Bind_GridAccount_Form_Controls()
        {
            try
            {
                if (objBASEFILEDS.Ac_post)
                {
                    Label objlable = new Label();
                    objlable.Name = "lblnet_amt";
                    objlable.Text = "NET AMOUNT";
                    objlable.AutoSize = true;
                    objlable.Bounds = new Rectangle(5, 5, (ctrlwid / 2) * 60 / 100, ctrlhgt);

                    PopupTextBox objtxt = new PopupTextBox();
                    objtxt.Name = "acc_net_amt";
                    objtxt.Text = String.Format("{0:F}", objBASEFILEDS.HTMAIN.Contains("NET_AMT") ? objBASEFILEDS.HTMAIN["NET_AMT"].ToString() : "0.00");
                    objtxt.Tag = "decimal";
                    objtxt.frmName = "";
                    objtxt.PTextName = "";
                    objtxt.Tbl_nm = "";
                    objtxt.Reftbltran_cd = "";
                    objtxt.Bounds = new Rectangle(objlable.Width, 5, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objtxt.ReadOnly = true;

                    pnlAccountGrid.Controls.Add(objlable);
                    pnlAccountGrid.Controls.Add(objtxt);

                    Label lblAccAmtDiff = new Label();
                    lblAccAmtDiff.Name = "lblAccAmtDiff";
                    lblAccAmtDiff.Text = " Amount Difference ";
                    lblAccAmtDiff.AutoSize = true;
                    lblAccAmtDiff.Visible = false;
                    lblAccAmtDiff.ForeColor = Color.Red;
                    lblAccAmtDiff.Bounds = new Rectangle(objlable.Width + objtxt.Width + (ctrlwid / 2) * 25 / 100, 5, (ctrlwid / 2), ctrlhgt);
                    pnlAccountGrid.Controls.Add(lblAccAmtDiff);

                    Label objlable1 = new Label();
                    objlable1.AutoSize = true;
                    objlable1.Name = "lbl1amt_paid";
                    objlable1.Text = "Amount to be paid";
                    objlable1.Bounds = new Rectangle(wid, 5, (ctrlwid / 2) * 50 / 100, ctrlhgt);

                    PopupTextBox objtxtamt_paid = new PopupTextBox();
                    objtxtamt_paid.Name = "acc_amount_paid";
                    objtxtamt_paid.Tag = "decimal";
                    objtxtamt_paid.frmName = "";
                    objtxtamt_paid.PTextName = "";
                    objtxtamt_paid.Tbl_nm = "";
                    objtxtamt_paid.Reftbltran_cd = "";
                    objtxtamt_paid.Bounds = new Rectangle(objlable1.Width + wid, 5, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                    objtxtamt_paid.ReadOnly = true;

                    pnlAccountGrid.Controls.Add(objlable1);
                    pnlAccountGrid.Controls.Add(objtxtamt_paid);


                    gridAccount.Name = "gridAccount";

                    gridAccount.Bounds = new Rectangle(0, ctrlhgt, pnlAccountGrid.Size.Width - 10, pnlAccountGrid.Size.Height - ctrlhgt - 10);
                    // gridAccount.Dock = DockStyle.Fill;                
                    gridAccount.AutoGenerateColumns = false;
                    gridAccount.RowHeadersVisible = false;
                    gridAccount.AllowUserToAddRows = false;
                    gridAccount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //DataGridViewAutoSizeColumnsMode.ColumnHeader;
                    gridAccount.AllowUserToResizeColumns = true;
                    gridAccount.ScrollBars = ScrollBars.Both;
                    gridAccount.AllowUserToOrderColumns = false;
                    gridAccount.EditMode = DataGridViewEditMode.EditOnEnter;
                    gridAccount.RowsDefaultCellStyle.SelectionBackColor = Color.Silver;
                    gridAccount.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    gridAccount.EnableHeadersVisualStyles = false;

                    gridAccount.CellClick += new DataGridViewCellEventHandler(Account_Cell_Click);
                    gridAccount.KeyPress += new KeyPressEventHandler(Account_grid_KeyPressEvent);
                    gridAccount.CellEnter += new DataGridViewCellEventHandler(Account_grid_CellEnter);
                    gridAccount.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Account_grid_EditingControlShowing);
                    gridAccount.CellLeave += new DataGridViewCellEventHandler(Account_grid_CellLeave);
                    gridAccount.CellValidating += new DataGridViewCellValidatingEventHandler(Account_Cell_Validating);
                    gridAccount.CellValidated += new DataGridViewCellEventHandler(Account_Cell_Validated);
                    gridAccount.MouseClick += new MouseEventHandler(gridAccount_MouseClick);
                    gridAccount.ForeColor = objBASEFILEDS.ObjControlSet.Font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Font_color) : Color.Black;
                    gridAccount.BackgroundColor = Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color);
                    gridAccount.BorderStyle = BorderStyle.None;
                    Bind_gridAccount();
                }
                else
                {
                    tabControl.TabPages.RemoveByKey("tabPage2");
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Bind_gridAccount()
        {
            POPUPTEXTBOX_FOR_GRID txtcolAccItemNo = new POPUPTEXTBOX_FOR_GRID();
            txtcolAccItemNo.HeaderText = "  Account No.  ";
            txtcolAccItemNo.Name = "acc_no";
            txtcolAccItemNo.Tag = "int";
            txtcolAccItemNo.Tbl_nm = "";
            txtcolAccItemNo.PTextName = "";
            txtcolAccItemNo.Dispddlfields = "";
            txtcolAccItemNo.Primaryddl = "";
            txtcolAccItemNo.Query_con = "";
            txtcolAccItemNo.IsQcd = false;
            txtcolAccItemNo.QcdCondition = "";
            txtcolAccItemNo.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcolAccItemNo);
            gridAccount.Columns["acc_no"].Visible = false;
            gridAccount.Columns["acc_no"].SortMode = DataGridViewColumnSortMode.NotSortable;
            objBASEFILEDS.HashacItem[txtcolAccItemNo.Name] = "0";

            POPUPTEXTBOX_FOR_GRID txtcolAccOrderNo = new POPUPTEXTBOX_FOR_GRID();
            txtcolAccOrderNo.HeaderText = "  Account Order No.  ";
            txtcolAccOrderNo.Name = "acc_order_no";
            txtcolAccOrderNo.Tag = "string";
            txtcolAccOrderNo.Tbl_nm = "";
            txtcolAccOrderNo.PTextName = "";
            txtcolAccOrderNo.Dispddlfields = "";
            txtcolAccOrderNo.Primaryddl = "";
            txtcolAccOrderNo.Query_con = "";
            txtcolAccOrderNo.IsQcd = false;
            txtcolAccOrderNo.QcdCondition = "";
            txtcolAccOrderNo.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcolAccOrderNo);
            gridAccount.Columns["acc_order_no"].SortMode = DataGridViewColumnSortMode.Automatic;
            gridAccount.Columns["acc_order_no"].Visible = false;
            objBASEFILEDS.HashacItem[txtcolAccOrderNo.Name] = "0";

            POPUPTEXTBOX_FOR_GRID txtacserial = new POPUPTEXTBOX_FOR_GRID();
            txtacserial.HeaderText = "  Serial No.  ";
            txtacserial.Name = "acserial";
            txtacserial.Tag = "string";
            txtacserial.Tbl_nm = "";
            txtacserial.PTextName = "";
            txtacserial.Dispddlfields = "";
            txtacserial.Primaryddl = "";
            txtacserial.Query_con = "";
            txtacserial.IsQcd = false;
            txtacserial.QcdCondition = "";
            txtacserial.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtacserial);
            gridAccount.Columns["acserial"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acserial"].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridAccount.Columns["acserial"].Visible = false;
            objBASEFILEDS.HashacItem[txtacserial.Name] = "0";

            POPUPTEXTBOX_FOR_GRID txtcolActualFld = new POPUPTEXTBOX_FOR_GRID();
            txtcolActualFld.HeaderText = " Actual Fields  ";
            txtcolActualFld.Name = "acc_actual_fld";
            txtcolActualFld.Tag = "string";
            txtcolActualFld.Tbl_nm = "";
            txtcolActualFld.PTextName = "";
            txtcolActualFld.Dispddlfields = "";
            txtcolActualFld.Primaryddl = "";
            txtcolActualFld.Query_con = "";
            txtcolActualFld.IsQcd = false;
            txtcolActualFld.QcdCondition = "";
            txtcolActualFld.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcolActualFld);
            gridAccount.Columns["acc_actual_fld"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_actual_fld"].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridAccount.Columns["acc_actual_fld"].Visible = false;
            //objBASEFILEDS.HashacItem[txtcolActualFld.Name] = "";

            POPUPTEXTBOX_FOR_GRID txtcolAccId = new POPUPTEXTBOX_FOR_GRID();
            txtcolAccId.HeaderText = "  Account Id  ";
            txtcolAccId.Name = "acc_ac_id";
            txtcolAccId.Tag = "int";
            txtcolAccId.Tbl_nm = "";
            txtcolAccId.PTextName = "";
            txtcolAccId.Dispddlfields = "";
            txtcolAccId.Primaryddl = "";
            txtcolAccId.Query_con = "";
            txtcolAccId.IsQcd = false;
            txtcolAccId.QcdCondition = "";
            txtcolAccId.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcolAccId);
            gridAccount.Columns["acc_ac_id"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_ac_id"].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridAccount.Columns["acc_ac_id"].Visible = false;
            objBASEFILEDS.HashacItem[txtcolAccId.Name] = "0";

            POPUPTEXTBOX_FOR_GRID txtcol = new POPUPTEXTBOX_FOR_GRID();
            txtcol.HeaderText = "  Account Name  ";
            txtcol.Name = "acc_ac_nm";
            txtcol.Tag = "string";
            txtcol.Tbl_nm = "IVW_ACCOUNTS";
            txtcol.PTextName = "";
            txtcol.Dispddlfields = "primary_nm;Account Name";
            txtcol.Primaryddl = "primary_id;acc_ac_id,primary_nm;acc_ac_nm";
            txtcol.Query_con = "";
            txtcol.IsQcd = false;
            txtcol.QcdCondition = "";
            txtcol.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcol);
            gridAccount.Columns["acc_ac_nm"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_ac_nm"].SortMode = DataGridViewColumnSortMode.NotSortable;
            objBASEFILEDS.HashacItem[txtcol.Name] = "";


            DataGridViewTextBoxColumn txtcoldec = new DataGridViewTextBoxColumn();
            txtcoldec.HeaderText = " Amount ";
            txtcoldec.Tag = "decimal";
            txtcoldec.Name = "acc_amount";
            gridAccount.Columns.Add(txtcoldec);
            gridAccount.Columns["acc_amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridAccount.Columns["acc_amount"].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridAccount.Columns["acc_amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //gridAccount.Columns["acc_amount"].DefaultCellStyle.Format = "N" + int.Parse(objBASEFILEDS.ObjControlSet.qty_dec != null && objBASEFILEDS.ObjControlSet.qty_dec.ToString() != "" ? objBASEFILEDS.ObjControlSet.qty_dec : "0").ToString();
            objBASEFILEDS.HashacItem[txtcoldec.Name] = String.Format("{0:F}", "0.00");

            POPUPTEXTBOX_FOR_GRID txtcoldrcr = new POPUPTEXTBOX_FOR_GRID();
            txtcoldrcr.HeaderText = "  Dr/Cr  ";
            txtcoldrcr.Name = "acc_account_type";
            txtcoldrcr.Tag = "string";
            txtcoldrcr.Tbl_nm = "DR_CR";
            txtcoldrcr.PTextName = "";
            txtcoldrcr.Dispddlfields = "dr_cr_nm;Account Name";
            //txtcoldrcr.Primaryddl = "dr_cr_id;acc_account_type_id,dr_cr_nm;acc_account_type";
            txtcoldrcr.Primaryddl = "dr_cr_nm;acc_account_type,dr_cr_nm;acc_account_type";
            txtcoldrcr.Query_con = "";
            txtcoldrcr.IsQcd = false;
            txtcoldrcr.QcdCondition = "";
            txtcoldrcr.Reftbltran_cd = "";
            gridAccount.Columns.Add(txtcoldrcr);
            gridAccount.Columns["acc_account_type"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_account_type"].SortMode = DataGridViewColumnSortMode.NotSortable;
            objBASEFILEDS.HashacItem[txtcoldrcr.Name] = "";

            POPUPBUTTON_FOR_GRID btncolNarration = new POPUPBUTTON_FOR_GRID();
            btncolNarration.HeaderText = " Narration ";
            btncolNarration.Text = " Narration ";
            btncolNarration.UseColumnTextForButtonValue = true;
            btncolNarration.Name = "acc_narr";
            btncolNarration.Tag = "button";
            btncolNarration.frmName = "frmNarration";
            btncolNarration.Query_con = "";
            gridAccount.Columns.Add(btncolNarration);
            gridAccount.Columns["acc_narr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_narr"].SortMode = DataGridViewColumnSortMode.NotSortable;

            //POPUPTEXTBOX_FOR_GRID txtcolNarr = new POPUPTEXTBOX_FOR_GRID();
            //POPUPBUTTON_FOR_GRID txtcolNarr = new POPUPBUTTON_FOR_GRID();
            //txtcolNarr.HeaderText = "  Account Narration  ";
            //txtcolNarr.Text = " Narration ";
            //txtcolNarr.Name = "acc_narr";
            //txtcolNarr.Tag = "string";
            //txtcolNarr.Tbl_nm = "";
            //txtcolNarr.PTextName = "";
            //txtcolNarr.Dispddlfields = "";
            //txtcolNarr.Primaryddl = "";
            //txtcolNarr.Query_con = "";
            ////txtcolNarr.IsQcd = false;
            ////txtcolNarr.QcdCondition = "";
            //txtcolNarr.Reftbltran_cd = "";
            //gridAccount.Columns.Add(txtcolNarr);
            //gridAccount.Columns["acc_narr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //gridAccount.Columns["acc_narr"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //objBASEFILEDS.HashacItem[txtcolNarr.Name] = "";

            POPUPBUTTON_FOR_GRID btncol = new POPUPBUTTON_FOR_GRID();
            btncol.HeaderText = " Allocation ";
            btncol.Text = " Allocation ";
            btncol.UseColumnTextForButtonValue = true;
            btncol.Name = "acc_allocation";
            btncol.Tag = "button";
            btncol.frmName = "frmAccountAlloc";
            btncol.Query_con = "";
            gridAccount.Columns.Add(btncol);
            gridAccount.Columns["acc_allocation"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridAccount.Columns["acc_allocation"].SortMode = DataGridViewColumnSortMode.NotSortable;

            pnlAccountGrid.Controls.Add(gridAccount);
        }
        private void gridAccount_MouseClick(object sender, MouseEventArgs e)
        {
            if (objBASEFILEDS.BlnStopItemEnter)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (tran_mode != "view_mode")
                    {
                        Point pt = gridAccount.PointToScreen(e.Location);
                        PopupMenuAccount.Show(pt);
                    }
                }
            }
        }
        private string GetAccount_Name(string ac_nm_valid)
        {
            if (ac_nm_valid.Contains("HTMAIN"))
            {
                ac_nm = "";
                if (Validate_Expression(ac_nm_valid))
                {

                }
            }
            else
            {
                ac_nm = ac_nm_valid;
            }
            return ac_nm;
        }
        private string GetAccount_Name_Row_Count(string ac_nm)
        {
            rows_no = "0";
            int i = 0;
            flgAccount = false;
            if (gridAccount.Rows.Count != 0)
            {
                foreach (DataGridViewRow r in gridAccount.Rows)
                {
                    if (r.Cells["acc_ac_nm"].Value != null && r.Cells["acc_ac_nm"].Value.ToString().Trim().ToLower() == ac_nm.Trim().ToLower())
                    {
                        rows_no = (int.Parse(r.Cells["acserial"].Value.ToString())).ToString() + "," + i.ToString();
                        i++;
                        flgAccount = true;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            if (!flgAccount)
            {
                gridAccount.Rows.Add();
                int acserial = 0;
                if (acserial == 0 && gridAccount.Rows.Count == 1)
                {
                    acserial = gridAccount.Rows.Count;
                }
                else
                {
                    foreach (DataGridViewRow r in gridAccount.Rows)
                    {
                        if (r.Cells["acserial"].Value != null && int.Parse(r.Cells["acserial"].Value.ToString()) > acserial)
                        {
                            acserial = int.Parse(r.Cells["acserial"].Value.ToString());
                        }
                    }
                    acserial++;
                }
                rows_no = (acserial).ToString() + "," + (gridAccount.Rows.Count - 1).ToString(); //gridAccount.Rows.Count - 1;
            }
            return rows_no;
        }

        private decimal GetAccount_Amount(string ac_nm, string acserial)
        {
            decimal amt = 0;
            if (gridAccount.Rows.Count != 0)
            {
                foreach (DataGridViewRow r in gridAccount.Rows)
                {
                    if (r.Cells["acserial"].Value.ToString().Trim().ToLower() != acserial && r.Cells["acc_ac_nm"].Value.ToString().Trim().ToLower() == ac_nm.Trim().ToLower())
                    {
                        amt += Decimal.Parse(r.Cells["acc_amount"].Value != null && r.Cells["acc_amount"].Value.ToString() != "" ? r.Cells["acc_amount"].Value.ToString() : "0.00");
                    }
                }
            }
            return amt;
        }
        private void AccountNet_AmtBindGrid()
        {
            if (gridAccount != null && objBASEFILEDS.HTMAIN.Contains("NET_AMT") && objBASEFILEDS.HTMAIN["NET_AMT"] != null && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "" && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "0" && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "0.00")
            {
                foreach (Control ctrl1 in tabControl.TabPages)
                {
                    foreach (Control ctrl2 in ctrl1.Controls)
                    {
                        foreach (Control ctrl in ctrl2.Controls)
                        {
                            if (ctrl is PopupTextBox)
                            {
                                PopupTextBox objtxt = (PopupTextBox)ctrl;
                                if (objtxt != null)
                                {
                                    if (objBASEFILEDS.HTMAIN.Contains("NET_AMT") && decimal.Parse(objtxt.Text != "" ? objtxt.Text : "0.00") != decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"] != null && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "" ? objBASEFILEDS.HTMAIN["NET_AMT"].ToString() : "0.00"))
                                    {
                                        objtxt.Text = String.Format("{0:F}", decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString()));
                                        Load_AccountGrid();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }
        private void Load_AccountGrid()
        {
            string account_name = "", acserial = "0", acc_id = "0";
            rows_no = "0";
            int dgv_no = 0;
            tot_amt = 0;
            decimal amt = 0;
            if (gridAccount != null && objBASEFILEDS.HTMAIN.Contains("NET_AMT") && objBASEFILEDS.HTMAIN["NET_AMT"] != null && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "" && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "0" && objBASEFILEDS.HTMAIN["NET_AMT"].ToString() != "0.00")
            {
                //    foreach (Control ctrl1 in tabControl.TabPages)
                //    {
                //        foreach (Control ctrl2 in ctrl1.Controls)
                //        {
                //            foreach (Control ctrl in ctrl2.Controls)
                //            {
                //                if (ctrl is PopupTextBox)
                //                {
                //                    PopupTextBox objtxt = (PopupTextBox)ctrl;
                //                    if (objtxt != null)
                //                    {
                //                        objtxt.Text = objBASEFILEDS.HTMAIN.Contains("NET_AMT") ? objBASEFILEDS.HTMAIN["NET_AMT"].ToString() : "0.00";
                //                        break;
                //                    }
                //                }
                //            }
                //        }
                //    }
                objBASEFILEDS.HtaccountAmountdet.Clear();
                #region tran_set
                if (objBASEFILEDS.Cr_ac_nm != "")
                {
                    //MessageBox.Show(objBASEFILEDS.Cr_ac_id);
                    account_name = GetAccount_Name(objBASEFILEDS.Cr_ac_nm);
                    DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + objBASEFILEDS.Cr_ac_id + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                    }
                    rows_no = GetAccount_Name_Row_Count(account_name);
                    acserial = "0"; dgv_no = 0;
                    if (rows_no.Split(',').Length == 2)
                    {
                        acserial = rows_no.Split(',')[0];
                        dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                    }

                    if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                    {
                        objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                        {
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                        }
                    }

                    objBASEFILEDS.Acc_alias_credit = account_name;
                    gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                    gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = "Cr_ac_nm";
                    gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                    gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                    gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");
                    gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "CR";
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "CR";// objBASEFILEDS.IsDefAccTranType ? "CR" : "DR";

                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                    if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                        if (rows != null && rows.Length != 0)
                        {
                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                        }
                    }

                    amt = 0;
                    if (!objBASEFILEDS.IsDefAccTranType)
                    {
                        gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString()));
                        amt = decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString());
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_amount"] = gridAccount.Rows[dgv_no].Cells["acc_amount"].Value;
                        gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "A";
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_ac_id"] = objBASEFILEDS.HTMAIN["ac_id"];
                    }
                    else
                    {
                        amt += Math.Round(decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString()), 2);
                        gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                        if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                        {
                            objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                        }
                        else
                        {
                            objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                        }
                        gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "B";
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                    }
                    if (amt != 0 && amt.ToString() != "0.00")
                    {
                        gridAccount.Rows[dgv_no].Visible = true;
                    }
                    else
                    {
                        gridAccount.Rows[dgv_no].Visible = false;
                    }
                }
                if (objBASEFILEDS.Dr_ac_nm != "")
                {
                    account_name = GetAccount_Name(objBASEFILEDS.Dr_ac_nm);
                    DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + objBASEFILEDS.Dr_ac_id + "'");
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                    }
                    rows_no = GetAccount_Name_Row_Count(account_name);
                    acserial = "0"; dgv_no = 0;
                    if (rows_no.Split(',').Length == 2)
                    {
                        acserial = rows_no.Split(',')[0];
                        dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                    }

                    if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                    {
                        objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                        {
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                        }
                    }

                    objBASEFILEDS.Acc_alias_debit = account_name;
                    gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                    gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = "dr_ac_nm";
                    gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                    gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                    gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");
                    gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "DR";

                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "DR";

                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                    if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                    {
                        DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                        if (rows != null && rows.Length != 0)
                        {
                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                        }
                    }
                    amt = 0;
                    if (!objBASEFILEDS.IsDefAccTranType)
                    {
                        amt += Math.Round(decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString()), 2);
                        gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                        if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                        {
                            objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                        }
                        else
                        {
                            objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                        }
                        gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "B";
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                    }
                    else
                    {
                        gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString()));
                        amt = decimal.Parse(objBASEFILEDS.HTMAIN["NET_AMT"].ToString());
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_amount"] = gridAccount.Rows[dgv_no].Cells["acc_amount"].Value;
                        gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "A";
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                        ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_ac_id"] = objBASEFILEDS.HTMAIN["ac_id"];
                    }
                    if (amt != 0 && amt.ToString() != "0.00")
                    {
                        gridAccount.Rows[dgv_no].Visible = true;
                    }
                    else
                    {
                        gridAccount.Rows[dgv_no].Visible = false;
                    }
                }
                #endregion tran_set
                #region dc Item
                if (objBASEFILEDS.dsDCITEMFIELDS != null && objBASEFILEDS.dsDCITEMFIELDS.Tables.Count != 0 && objBASEFILEDS.dsDCITEMFIELDS.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in objBASEFILEDS.dsDCITEMFIELDS.Tables[0].Rows)
                    {
                        if (row["dr_ac_nm"] != null && row["dr_ac_nm"].ToString() != "")
                        {
                            account_name = GetAccount_Name(row["dr_ac_nm"].ToString());
                            DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + row["dr_ac_id"] + "'");
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                            {
                                acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                            }
                            rows_no = GetAccount_Name_Row_Count(ac_nm);
                            acserial = "0"; dgv_no = 0;
                            if (rows_no.Split(',').Length == 2)
                            {
                                acserial = rows_no.Split(',')[0];
                                dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                            }
                            if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                                {
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                                }
                            }
                            objBASEFILEDS.Acc_alias_dc_debit = account_name;
                            gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                            gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = row["fld_nm"].ToString();
                            gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                            gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");
                            gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "DR";

                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "DR";

                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                            if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                            {
                                DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                                if (rows != null && rows.Length != 0)
                                {
                                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                                }
                            }
                            amt = 0;
                            foreach (DictionaryEntry entry in objBASEFILEDS.HTITEM)
                            {
                                if (((Hashtable)entry.Value).Count != 0)
                                {
                                    if (((Hashtable)entry.Value).Contains(row["fld_nm"].ToString().Trim()))
                                    {
                                        amt += decimal.Parse(((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()] != null && ((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()].ToString() != "" ? ((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()].ToString() : "0.00");
                                        tot_amt += amt;
                                    }
                                }
                            }
                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                            gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "CE";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                            if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                            }
                            else
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                            }

                            if (amt != 0 && amt.ToString() != "0.00")
                            {
                                gridAccount.Rows[dgv_no].Visible = true;
                            }
                            else
                            {
                                gridAccount.Rows[dgv_no].Visible = false;
                            }
                        }
                        if (row["cr_ac_nm"] != null && row["cr_ac_nm"].ToString() != "")
                        {
                            account_name = GetAccount_Name(row["cr_ac_nm"].ToString());
                            DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + row["cr_ac_id"] + "'");
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                            {
                                acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                            }
                            rows_no = GetAccount_Name_Row_Count(ac_nm);
                            acserial = "0"; dgv_no = 0;
                            if (rows_no.Split(',').Length == 2)
                            {
                                acserial = rows_no.Split(',')[0];
                                dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                            }
                            if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                                {
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                                }
                            }
                            objBASEFILEDS.Acc_alias_dc_credit = account_name;
                            gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                            gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = row["fld_nm"].ToString();
                            gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                            gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");
                            gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "CR";

                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "CR";

                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                            if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                            {
                                DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                                if (rows != null && rows.Length != 0)
                                {
                                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                                }
                            }
                            amt = 0; //amt = GetAccount_Amount(account_name, acserial.Trim().PadLeft(5, '0'));
                            foreach (DictionaryEntry entry in objBASEFILEDS.HTITEM)
                            {
                                if (((Hashtable)entry.Value).Count != 0)
                                {
                                    if (((Hashtable)entry.Value).Contains(row["fld_nm"].ToString().Trim()))
                                    {
                                        amt += decimal.Parse(((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()] != null && ((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()].ToString() != "" ? ((Hashtable)entry.Value)[row["fld_nm"].ToString().Trim()].ToString() : "0.00");
                                        tot_amt -= amt;
                                    }
                                }
                            }
                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                            gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "CE";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;
                            if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                            }
                            else
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                            }

                            if (amt != 0 && amt.ToString() != "0.00")
                            {
                                gridAccount.Rows[dgv_no].Visible = true;
                            }
                            else
                            {
                                gridAccount.Rows[dgv_no].Visible = false;
                            }
                        }
                    }
                }
                #endregion dc item
                #region dc head
                if (objBASEFILEDS.dsDCHEADRFIELDS != null && objBASEFILEDS.dsDCHEADRFIELDS.Tables.Count != 0 && objBASEFILEDS.dsDCHEADRFIELDS.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow row in objBASEFILEDS.dsDCHEADRFIELDS.Tables[0].Rows)
                    {
                        if (row["dr_ac_nm"] != null && row["dr_ac_nm"].ToString() != "")
                        {
                            account_name = GetAccount_Name(row["dr_ac_nm"].ToString());
                            DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + row["dr_ac_id"] + "'");
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                            {
                                acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                            }
                            rows_no = GetAccount_Name_Row_Count(ac_nm);
                            acserial = "0"; dgv_no = 0;
                            if (rows_no.Split(',').Length == 2)
                            {
                                acserial = rows_no.Split(',')[0];
                                dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                            }
                            if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                                {
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                                }
                            }

                            objBASEFILEDS.Acc_alias_dc_header_debit = account_name;
                            gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                            gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = row["fld_nm"].ToString();
                            gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                            gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                            gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "DR";

                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "DR";

                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                            if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                            {
                                DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                                if (rows != null && rows.Length != 0)
                                {
                                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                                }
                            }
                            amt = 0; //amt = GetAccount_Amount(account_name, acserial.Trim().PadLeft(5, '0'));

                            gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "CH";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;

                            if (objBASEFILEDS.HTMAIN.Contains(row["fld_nm"].ToString().Trim()))
                            {
                                amt += decimal.Parse(objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] != null && objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()].ToString() != "" ? objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()].ToString() : "0.00");
                                tot_amt += amt;
                            }

                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                            if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                            }
                            else
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                            }
                            // ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_amount"] = gridAccount.Rows[dgv_no].Cells["acc_amount"].Value;

                            //if (gridAccount.Rows[dgv_no].Cells["acc_amount"].Value != null && gridAccount.Rows[dgv_no].Cells["acc_amount"].Value.ToString() != "" && gridAccount.Rows[dgv_no].Cells["acc_amount"].Value.ToString() != "0" && gridAccount.Rows[dgv_no].Cells["acc_amount"].Value.ToString() != "0.00")
                            if (amt != 0 && amt.ToString() != "0.00")
                            {
                                gridAccount.Rows[dgv_no].Visible = true;
                            }
                            else
                            {
                                gridAccount.Rows[dgv_no].Visible = false;
                            }
                        }
                        if (row["cr_ac_nm"] != null && row["cr_ac_nm"].ToString() != "")
                        {
                            account_name = GetAccount_Name(row["dr_ac_nm"].ToString());
                            DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + row["cr_ac_id"] + "'");
                            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                            {
                                acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                            }
                            rows_no = GetAccount_Name_Row_Count(ac_nm);
                            acserial = "0"; dgv_no = 0;
                            if (rows_no.Split(',').Length == 2)
                            {
                                acserial = rows_no.Split(',')[0];
                                dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                            }
                            if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                                {
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                                }
                            }
                            objBASEFILEDS.Acc_alias_dc_header_credit = account_name;
                            gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                            gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = row["fld_nm"].ToString();
                            gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                            gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                            gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = "CR";

                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = "CR";

                            gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                            if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                            {
                                DataRow[] rows = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                                if (rows != null && rows.Length != 0)
                                {
                                    gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows[0]["acc_no"].ToString();
                                    ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows[0]["acc_no"].ToString();
                                }
                            }
                            amt = 0; //amt = GetAccount_Amount(account_name, acserial.Trim().PadLeft(5, '0'));

                            gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "CH";
                            ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;

                            if (objBASEFILEDS.HTMAIN.Contains(row["fld_nm"].ToString().Trim()))
                            {
                                amt += decimal.Parse(objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()] != null && objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()].ToString() != "" ? objBASEFILEDS.HTMAIN[row["fld_nm"].ToString().Trim()].ToString() : "0.00");
                                tot_amt -= amt;
                            }

                            gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                            if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                            }
                            else
                            {
                                objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                            }
                            // ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_amount"] = gridAccount.Rows[dgv_no].Cells["acc_amount"].Value;

                            if (amt != 0 && amt.ToString() != "0.00")
                            {
                                gridAccount.Rows[dgv_no].Visible = true;
                            }
                            else
                            {
                                gridAccount.Rows[dgv_no].Visible = false;
                            }
                        }
                    }
                }
                #endregion dc head
                #region st
                if (objBASEFILEDS.dsSTFIELDS != null && objBASEFILEDS.dsSTFIELDS.Tables.Count != 0 && objBASEFILEDS.dsSTFIELDS.Tables[0].Rows.Count != 0)
                {
                    DataRow[] rows = objBASEFILEDS.dsSTFIELDS.Tables[0].Select("tax_nm='" + objBASEFILEDS.HTMAIN["TAX_NM"].ToString().Trim() + "'");
                    if (rows != null && rows.Length != 0)
                    {
                        foreach (DataRow row in rows)
                        {
                            if (row["ac_nm"] != null && row["ac_nm"].ToString() != "")
                            {
                                account_name = GetAccount_Name(row["ac_nm"].ToString());
                                DataSet ds = objFL_GEN_INV.Execute_Query("select primary_id from IVW_ACCOUNTS where primary_nm='" + account_name + "' and primary_id='" + row["ac_id"] + "'");
                                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                                {
                                    acc_id = ds.Tables[0].Rows[0]["primary_id"] != null && ds.Tables[0].Rows[0]["primary_id"].ToString() != "" ? ds.Tables[0].Rows[0]["primary_id"].ToString() : "0";
                                }
                                rows_no = GetAccount_Name_Row_Count(ac_nm);
                                acserial = "0"; dgv_no = 0;
                                if (rows_no.Split(',').Length == 2)
                                {
                                    acserial = rows_no.Split(',')[0];
                                    dgv_no = int.Parse(rows_no.Split(',')[1] != null && rows_no.Split(',')[1].ToString() != "" ? rows_no.Split(',')[1].ToString() : "0");
                                }

                                if (!objBASEFILEDS.HT_ACDET.Contains(acserial.Trim().PadLeft(5, '0')))
                                {
                                    objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                                    foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                                    {
                                        ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                                    }
                                }

                                objBASEFILEDS.Acc_alias_st = account_name;
                                gridAccount.Rows[dgv_no].Cells["acc_ac_nm"].Value = account_name;
                                gridAccount.Rows[dgv_no].Cells["acc_ac_id"].Value = acc_id;
                                gridAccount.Rows[dgv_no].Cells["acserial"].Value = acserial.Trim().PadLeft(5, '0');
                                gridAccount.Rows[dgv_no].Cells["acc_actual_fld"].Value = "tax_nm";
                                gridAccount.Rows[dgv_no].Cells["acc_account_type"].Value = objBASEFILEDS.IsDefAccTranType ? "CR" : "DR";
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_nm"] = account_name;
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_ac_id"] = acc_id;
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acserial"] = acserial.Trim().PadLeft(5, '0');
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_account_type"] = objBASEFILEDS.IsDefAccTranType ? "CR" : "DR";

                                gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value = "ST";
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_order_no"] = gridAccount.Rows[dgv_no].Cells["acc_order_no"].Value;

                                gridAccount.Rows[dgv_no].Cells["acc_no"].Value = "0";
                                ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = "0";
                                if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                                {
                                    DataRow[] rows1 = objBASEFILEDS.DsAccountPosting.Tables[0].Select("acserial='" + acserial.Trim().PadLeft(5, '0') + "'");
                                    if (rows1 != null && rows1.Length != 0)
                                    {
                                        gridAccount.Rows[dgv_no].Cells["acc_no"].Value = rows1[0]["acc_no"].ToString();
                                        ((Hashtable)objBASEFILEDS.HT_ACDET[acserial.Trim().PadLeft(5, '0')])["acc_no"] = rows1[0]["acc_no"].ToString();
                                    }
                                }
                                amt = 0; //amt += GetAccount_Amount(account_name, acserial.Trim().PadLeft(5, '0'));
                                if (objBASEFILEDS.HTMAIN.Contains("TAX_AMT"))
                                {
                                    amt += decimal.Parse(objBASEFILEDS.HTMAIN["TAX_AMT"] != null && objBASEFILEDS.HTMAIN["TAX_AMT"].ToString().Trim() != "" ? objBASEFILEDS.HTMAIN["TAX_AMT"].ToString().Trim() : "0.00");
                                    tot_amt += amt;
                                }

                                gridAccount.Rows[dgv_no].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");//amt;
                                if (objBASEFILEDS.HtaccountAmountdet.Contains(acserial.Trim().PadLeft(5, '0')))
                                {
                                    objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt + decimal.Parse(objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] != null && objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() != "" ? objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')].ToString() : "0.00");
                                }
                                else
                                {
                                    objBASEFILEDS.HtaccountAmountdet[acserial.Trim().PadLeft(5, '0')] = amt;
                                }
                                // ((Hashtable)objBASEFILEDS.HT_ACDET[gridAccount.Rows[dgv_no].Cells["acserial"].Value.ToString().Trim().PadLeft(5, '0')])["acc_amount"] = gridAccount.Rows[dgv_no].Cells["acc_amount"].Value;

                                if (amt != 0 && amt.ToString() != "0.00")
                                {
                                    gridAccount.Rows[dgv_no].Visible = true;
                                }
                                else
                                {
                                    gridAccount.Rows[dgv_no].Visible = false;
                                }
                            }
                            else
                            {
                                dgv_no = 0;
                                flgAccount = false;
                                if (gridAccount.Rows.Count != 0)
                                {
                                    foreach (DataGridViewRow r in gridAccount.Rows)
                                    {
                                        if (r.Cells["acc_actual_fld"].Value != null && r.Cells["acc_actual_fld"].Value.ToString().Trim().ToLower() == "tax_nm")
                                        {
                                            dgv_no++;
                                            flgAccount = true;
                                            break;
                                        }
                                        else
                                        {
                                            dgv_no++;
                                        }
                                    }
                                    if (flgAccount)
                                    {
                                        objBASEFILEDS.HT_ACDET.Remove(gridAccount.Rows[dgv_no - 1].Cells["acserial"].Value);
                                        // objBASEFILEDS.HtaccountAmountdet.Remove(gridAccount.Rows[dgv_no - 1].Cells["acserial"].Value + "," + gridAccount.Rows[dgv_no - 1].Cells["acc_actual_fld"].Value);
                                        gridAccount.Rows.RemoveAt(dgv_no - 1);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion st
                #region extra Account Add
                //bool fldAddExtraAcc = false;
                //if (objBASEFILEDS.DsAccountPosting != null && objBASEFILEDS.DsAccountPosting.Tables.Count != 0 && objBASEFILEDS.DsAccountPosting.Tables[0].Rows.Count != 0)
                //{
                //    foreach (DataRow r in objBASEFILEDS.DsAccountPosting.Tables[0].Rows)
                //    {
                //        fldAddExtraAcc = false;
                //        if (!objBASEFILEDS.HT_ACDET.Contains(r["acserial"].ToString().Trim()))
                //        {
                //            fldAddExtraAcc = true;
                //        }
                //        if (fldAddExtraAcc)
                //        {
                //            gridAccount.Rows.Add();
                //            if (!objBASEFILEDS.HT_ACDET.Contains(r["acserial"].ToString().Trim()))
                //            {
                //                objBASEFILEDS.HT_ACDET[r["acserial"].ToString().Trim()] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                //                foreach (DictionaryEntry entry2 in objBASEFILEDS.HashacItem)
                //                {
                //                    ((Hashtable)objBASEFILEDS.HT_ACDET[r["acserial"].ToString().Trim()])[entry2.Key] = entry2.Value;
                //                }
                //            }
                //            //foreach (DictionaryEntry entry1 in ((Hashtable)entry.Value))
                //            //{
                //            foreach (DataGridViewColumn col in gridAccount.Columns)
                //            {
                //                if (objBASEFILEDS.DsAccountPosting.Tables[0].Columns.Contains(col.Name))
                //                {
                //                    gridAccount.Rows[gridAccount.Rows.Count - 1].Cells[col.Name].Value = r[col.Name].ToString();
                //                    ((Hashtable)objBASEFILEDS.HT_ACDET[r["acserial"].ToString().Trim()])[col.Name] = r[col.Name].ToString();
                //                }
                //            }
                //            //}
                //        }
                //    }
                //}
                #endregion  extra Account Add
                if (objBASEFILEDS.HtaccountAmountdet != null && objBASEFILEDS.HtaccountAmountdet.Count != 0)
                {
                    foreach (DictionaryEntry entry in objBASEFILEDS.HtaccountAmountdet)
                    {
                        foreach (DataGridViewRow r in gridAccount.Rows)
                        {
                            if (entry.Key.ToString() == r.Cells["acserial"].Value.ToString().Trim())
                            {
                                r.Cells["acc_amount"].Value = String.Format("{0:F}", decimal.Parse(r.Cells["acc_amount"].Value != null && r.Cells["acc_amount"].Value.ToString() != "" ? r.Cells["acc_amount"].Value.ToString() : "0.00") + decimal.Parse(entry.Value != null && entry.Value.ToString() != "" ? entry.Value.ToString() : "0.00"));
                                ((Hashtable)objBASEFILEDS.HT_ACDET[r.Cells["acserial"].Value.ToString().Trim()])["acc_amount"] = r.Cells["acc_amount"].Value;
                                break;
                            }
                        }
                    }
                }
                gridAccount.Sort(gridAccount.Columns["acc_order_no"], ListSortDirection.Ascending);
            }
        }

        private void AddAccount()
        {
            gridAccount.Rows.Add();
            int dgv_no = 0;
            foreach (DictionaryEntry entry in objBASEFILEDS.HT_ACDET)
            {
                if (dgv_no < int.Parse(entry.Key.ToString()))
                {
                    dgv_no = int.Parse(entry.Key.ToString());
                }
            }

            if (!objBASEFILEDS.HT_ACDET.Contains((dgv_no + 1).ToString().Trim().PadLeft(5, '0')))
            {
                objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                foreach (DictionaryEntry entry in objBASEFILEDS.HashacItem)
                {
                    ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])[entry.Key] = entry.Value;
                }
            }
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acserial"].Value = (dgv_no + 1).ToString().Trim().PadLeft(5, '0');
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acc_ac_nm"].Value = "";
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acc_no"].Value = "0";
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acc_order_no"].Value = "Z";
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acc_amount"].Value = String.Format("{0:F}", "0.00");
            gridAccount.Rows[gridAccount.Rows.Count - 1].Cells["acc_account_type"].Value = objBASEFILEDS.IsDefAccTranType ? "CR" : "DR";
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["acc_ac_nm"] = "";
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["acc_no"] = "0";
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["acc_order_no"] = "Z";
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["or_ac_nm"] = objBASEFILEDS.HTMAIN["ac_nm"].ToString();
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["acserial"] = (dgv_no + 1).ToString().Trim().PadLeft(5, '0');
            ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv_no + 1).ToString().Trim().PadLeft(5, '0')])["acc_account_type"] = objBASEFILEDS.IsDefAccTranType ? "CR" : "DR";
        }
        private void RemoveAccount()
        {
            if (objBASEFILEDS.Ac_post)
            {
                DataSet dsetAccountAlloc = objFLGeneral.GetDataSetByQuery("select * from IVW_AC_ALLOC where ref_tran_id='" + objBASEFILEDS.Tran_id + "' and ref_tran_cd='" + objBASEFILEDS.Code + "' and acc_ac_nm='" + gridAccount.CurrentRow.Cells["acc_ac_nm"].Value + "'");

                if (dsetAccountAlloc != null && dsetAccountAlloc.Tables.Count != 0 && dsetAccountAlloc.Tables[0].Rows.Count != 0)
                {
                    AutoClosingMessageBox.Show("Sorry! We can't delete because it is refered in the other Accounting transaction", "Delete");
                }
                else
                {
                    objBASEFILEDS.HT_ACDET.Remove(gridAccount.CurrentRow.Cells["acserial"].Value);
                    gridAccount.Rows.Remove(gridAccount.CurrentRow);
                    AccountNet_AmtBindGrid();//Load_AccountGrid;
                }
            }
            //objBASEFILEDS.HT_ACDET.Remove(gridAccount.CurrentRow.Cells["acserial"].Value);
            //gridAccount.Rows.Remove(gridAccount.CurrentRow);
            //AccountNet_AmtBindGrid();//Load_AccountGrid
        }

        private bool callMethod(string _method_nm)
        {
            bool validflg = false;
            if (objiTRANSACTION.GetType().GetMethod(_method_nm) != null)
            {
                objiTRANSACTION.ACTIVE_TRANSACTION = objBASEFILEDS;
                MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(_method_nm);
                validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, null).ToString().Trim());
                if (!validflg)
                {
                    if (objiTRANSACTION.BL_FIELDS.Errormsg.Length != 0)
                    {
                        AutoClosingMessageBox.Show(objiTRANSACTION.BL_FIELDS.Errormsg, "Transaction Validation");
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter Valid", "Validation");
                    }
                }
            }
            return validflg;
        }
        private bool Validate_Expression(string expression)
        {
            bool validflg = true;
            string _strValue = "";

            //if (objBASEFILEDS.Tran_mode != "view_mode")
            //{
            string exp = expression;
            string[] ar = exp.Split(new Char[] { '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (ar.Length >= 1)
            {
                if (ar.Length == 1)
                {
                    string exp1 = ar[0];
                    string[] ar1 = exp1.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in ar1)
                    {
                        if (str.Contains("HT"))
                        {
                            string[] cond = ar[0].Split(new string[] { "!EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                            string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                            _strValue = cod1;
                            if (cod1 == "")
                            {
                                AutoClosingMessageBox.Show("valid expression", "Valid");
                                validflg = false;
                            }
                            else
                            {
                                ac_nm = _strValue;
                                validflg = true;
                            }
                        }
                        else
                        {
                            validflg = callMethod(str);
                            if (!validflg)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (ar[0].Contains("!EMPTY"))
                    {
                        string[] cond = ar[0].Split(new string[] { "!EMPTY", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                        string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "string") : cond[0];
                        _strValue = cod1;
                        if (cod1 == "")
                        {
                            AutoClosingMessageBox.Show("valid expression", "Valid");
                            validflg = false;
                        }
                        else
                        {
                            ac_nm = _strValue;
                            validflg = true;
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
                        string valu = ar[0].Replace("<", "$<$").Replace(">", "$>$").Replace("<=", "$<=$").Replace(">=", "$>=$").Replace("==", "$==$").Replace("!=", "$!=$").Replace("=", "$=$");
                        string[] cond = valu.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                        if (cond.Length == 3)
                        {
                            string cod1 = cond[0].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[0], "decimal") : cond[0];
                            string cod2 = cond[2].Contains("HT") ? objFL_GRIDEVENTS.InfixToPostfix(cond[2], "decimal") : cond[2];
                            switch (cond[1])
                            {
                                case "<": if (decimal.Parse(cod1) < decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be less than  " + cod2, "Validation"); } break;
                                case ">": if (decimal.Parse(cod1) > decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be greater than  " + cod2, "Validation"); } break;
                                case "<=": if (decimal.Parse(cod1) <= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be less than equal to " + cod2, "Validation"); } break;
                                case ">=": if (decimal.Parse(cod1) >= decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be greater than equal to  " + cod2, "Validation"); } break;
                                case "==": if (decimal.Parse(cod1) == decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be equal to  " + cod2, "Validation"); } break;
                                case "!=": if (decimal.Parse(cod1) != decimal.Parse(cod2)) validflg = true; else { validflg = false; AutoClosingMessageBox.Show(cod1 + "should be not equal to  " + cod2, "Validation"); } break;
                                default: validflg = false; break;
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
                                validflg = callMethod(str);
                                if (!validflg)
                                {
                                    break;
                                }
                            }
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
                                validflg = callMethod(str);
                                if (!validflg)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //MessageBox.Show(fld_nm +" SHOULD NOT BE EMPTY");
                        }
                    }
                }
            }
            ac_nm = _strValue;
            //}
            return validflg;
        }

        private void Account_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    DataGridView dgv = sender as DataGridView;
                    if (dgv != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                    {
                        POPUPBUTTON_FOR_GRID col = dgv.Columns[e.ColumnIndex] as POPUPBUTTON_FOR_GRID;
                        if (col != null)
                        {
                            if (objBASEFILEDS.Ac_post && col.Name == "acc_allocation")
                            {
                                frmAccountAlloc objfrmAccountAlloc = new frmAccountAlloc();
                                objfrmAccountAlloc.ACTIVE_BL = objBASEFILEDS;
                                objfrmAccountAlloc.HashRowList = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]);
                                objfrmAccountAlloc.ShowDialog();
                            }
                            else if (objBASEFILEDS.Ac_post && col.Name == "acc_narr")
                            {
                                frmNarration objfrmNarration = new frmNarration();
                                objfrmNarration.objBASEFILEDS = objBASEFILEDS;
                                objfrmNarration.Fld_nm = "acc_narr";
                                objfrmNarration.Fld_value = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"] != null ? ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"].ToString() : "";
                                objfrmNarration.ShowDialog();
                                ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"] = objfrmNarration.Fld_value;
                            }
                            else if (col.frmName != null && col.frmName != "")
                            {
                                objiButtonEvent.ACTIVE_BL = objBASEFILEDS;
                                objiButtonEvent.HashRowItem = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]);
                                objiButtonEvent.BlnHeaderOrItem = false;
                                objiButtonEvent.Btn_nm = col.Name;
                                bool validflg = objiButtonEvent.Button_Click_Event();
                                if (!validflg)
                                {
                                    if (objiButtonEvent.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        AutoClosingMessageBox.Show(objiButtonEvent.BL_FIELDS.Errormsg, "Button Validation");
                                    }
                                }
                            }
                            else
                            {
                                DataGridViewButtonCell b = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                                if (b != null)
                                {
                                    frmAddl_Info objfrmAddl_Info = new frmAddl_Info(((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]), 1, objBASEFILEDS.Code, Tran_mode, b.OwningColumn.Name, b.OwningColumn.HeaderText);
                                    objfrmAddl_Info.dset = objBASEFILEDS.dsBASEADDIFIELDITEM;
                                    objfrmAddl_Info.ObjBSFD = objBASEFILEDS;
                                    objfrmAddl_Info.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message, "Exception");
            }
        }
        private void Account_grid_KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    DataGridView dgv = sender as DataGridView;
                    if (dgv != null)
                    {
                        POPUPBUTTON_FOR_GRID col = dgv.Columns[dgv.CurrentCell.ColumnIndex] as POPUPBUTTON_FOR_GRID;
                        if (col != null)
                        {
                            if (objBASEFILEDS.Ac_post && col.Name == "acc_allocation")
                            {
                                frmAccountAlloc objfrmAccountAlloc = new frmAccountAlloc();
                                objfrmAccountAlloc.ACTIVE_BL = objBASEFILEDS;
                                objfrmAccountAlloc.HashRowList = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]);
                                objfrmAccountAlloc.ShowDialog();
                            }
                            else if (objBASEFILEDS.Ac_post && col.Name == "acc_narr")
                            {
                                frmNarration objfrmNarration = new frmNarration();
                                objfrmNarration.objBASEFILEDS = objBASEFILEDS;
                                objfrmNarration.Fld_nm = "acc_narr";
                                objfrmNarration.Fld_value = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"] != null ? ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"].ToString() : "";
                                objfrmNarration.ShowDialog();
                                ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])["acc_narr"] = objfrmNarration.Fld_value;
                            }
                            else if (col.frmName != null && col.frmName != "")
                            {
                                objiButtonEvent.ACTIVE_BL = objBASEFILEDS;
                                objiButtonEvent.HashRowItem = ((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]);
                                objiButtonEvent.BlnHeaderOrItem = false;
                                objiButtonEvent.Btn_nm = col.Name;
                                bool validflg = objiButtonEvent.Button_Click_Event();
                                if (!validflg)
                                {
                                    if (objiButtonEvent.BL_FIELDS.Errormsg.Length != 0)
                                    {
                                        AutoClosingMessageBox.Show(objiButtonEvent.BL_FIELDS.Errormsg, "Button Validation");
                                    }
                                }
                            }
                            else
                            {
                                DataGridViewButtonCell b = dgv.CurrentCell as DataGridViewButtonCell;
                                if (b != null)
                                {
                                    frmAddl_Info objfrmAddl_Info = new frmAddl_Info(((Hashtable)objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]), 1, objBASEFILEDS.Code, Tran_mode, b.OwningColumn.Name, b.OwningColumn.HeaderText);
                                    objfrmAddl_Info.dset = objBASEFILEDS.dsBASEADDIFIELDITEM;
                                    objfrmAddl_Info.ObjBSFD = objBASEFILEDS;
                                    objfrmAddl_Info.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Account_Cell_Validating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    if (tran_mode == "add_mode" || tran_mode == "edit_mode")
                    {
                        decimal dramt = 0, cramt = 0;

                        if (gridAccount != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                        {
                            foreach (DataGridViewRow r in gridAccount.Rows)
                            {
                                if (r.Visible && r.Cells["acc_account_type"].Value != null && r.Cells["acc_account_type"].Value.ToString() == "CR")
                                {
                                    cramt += decimal.Parse(r.Cells["acc_amount"].Value != null && r.Cells["acc_amount"].Value.ToString() != "" ? r.Cells["acc_amount"].Value.ToString() : "0.00");
                                }
                                if (r.Visible && r.Cells["acc_account_type"].Value != null && r.Cells["acc_account_type"].Value.ToString() == "DR")
                                {
                                    dramt += decimal.Parse(r.Cells["acc_amount"].Value != null && r.Cells["acc_amount"].Value.ToString() != "" ? r.Cells["acc_amount"].Value.ToString() : "0.00");
                                }
                            }
                        }
                        foreach (Control ctrl1 in tabControl.TabPages)
                        {
                            foreach (Control ctrl2 in ctrl1.Controls)
                            {
                                foreach (Control ctrl in ctrl2.Controls)
                                {
                                    if (ctrl is Label)
                                    {
                                        Label objtxt = (Label)ctrl;
                                        if (objtxt != null && objtxt.Name == "lblAccAmtDiff")
                                        {
                                            if ((dramt - cramt) != 0)
                                            {
                                                objtxt.Visible = true;
                                                objtxt.Text = " Amount Difference : " + (dramt - cramt);
                                            }
                                            else
                                            {
                                                objtxt.Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //AutoClosingMessageBox.Show(ex.Message,"Exception",3000);
            }
        }
        private void Account_Cell_Validated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (tran_mode == "add_mode" || tran_mode == "edit_mode")
                {
                    DataGridView dgv = sender as DataGridView;
                    if (dgv != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                    {
                        if (((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)])).ContainsKey(dgv.CurrentCell.OwningColumn.Name))
                        {
                            if (gridAccount.CurrentCell.OwningColumn.Tag.ToString() == "decimal")
                            {
                                if (dgv.CurrentCell.Value != null && dgv.CurrentCell.Value.ToString() != "")
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = dgv.CurrentCell.Value;
                                }
                                else
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = String.Format("{0:F}", "0.00");
                                }
                            }
                            if (gridAccount.CurrentCell.OwningColumn.Tag.ToString() == "int")
                            {
                                if (dgv.CurrentCell.Value != null && dgv.CurrentCell.Value.ToString() != "")
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = int.Parse(dgv.CurrentCell.Value.ToString()).ToString();
                                }
                                else
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = "0";
                                }
                            }
                            else if (gridAccount.CurrentCell.OwningColumn.Tag.ToString() == "string")
                            {
                                if (dgv.CurrentCell.Value != null)
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = dgv.CurrentCell.Value;
                                }
                                else
                                {
                                    ((Hashtable)(objBASEFILEDS.HT_ACDET[(dgv.CurrentRow.Cells["ACSERIAL"].Value)]))[dgv.CurrentCell.OwningColumn.Name] = "";
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Account_grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView gridview = (DataGridView)sender;
            TextBox txt = e.Control as TextBox;
            if (txt != null)
            {
                txt.Name = gridview.CurrentCell.OwningColumn.Name.ToString().Trim();
                txt.Tag = gridview.CurrentCell.OwningColumn.Tag.ToString().Trim();
                if (txt.Tag.ToString().Trim() == "decimal" || txt.Tag.ToString().Trim() == "int")
                {
                    txt.KeyPress -= new KeyPressEventHandler(Account_txt_Key_Press);
                    txt.KeyPress += new KeyPressEventHandler(Account_txt_Key_Press);
                }
                else
                {
                    txt.KeyDown -= new KeyEventHandler(Account_txt_key_down);
                    txt.KeyDown += new KeyEventHandler(Account_txt_key_down);
                }
            }
        }
        private void Account_grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    DataGridView dgv = sender as DataGridView;
                    if (dgv != null & e.RowIndex != -1 && e.ColumnIndex != -1)
                    {
                        if (dgv.CurrentCell.OwningColumn is POPUPTEXTBOX_FOR_GRID)
                        {
                            POPUPTEXTBOX_FOR_GRID gridcolumncod = (POPUPTEXTBOX_FOR_GRID)gridAccount.Columns[gridAccount.CurrentCell.ColumnIndex];
                            if (gridcolumncod != null && gridcolumncod.Tbl_nm != "" && gridcolumncod.Primaryddl != "" && gridcolumncod.Dispddlfields != "")
                            {
                                objlableGrid.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Account_grid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && objBASEFILEDS.BlnStopItemEnter)
            {
                if (objlableGrid.Visible)
                    objlableGrid.Visible = false;
            }
        }
        private void Account_txt_key_down(object sender, KeyEventArgs e)
        {
            try
            {
                if (objBASEFILEDS.BlnStopItemEnter)
                {
                    bool flgItemEdit = false;
                    TextBox txt = (TextBox)sender;
                    if ((e.KeyData == Keys.F2 || e.KeyData == Keys.Enter))
                    {
                        POPUPTEXTBOX_FOR_GRID gridcolumncod = (POPUPTEXTBOX_FOR_GRID)gridAccount.Columns[gridAccount.CurrentCell.ColumnIndex];
                        if (gridcolumncod != null && !flgItemEdit && gridcolumncod.Tbl_nm != "" && gridcolumncod.Primaryddl != "" && gridcolumncod.Dispddlfields != "")
                        {
                            frmPopup objfrmPopup = new frmPopup(((Hashtable)objBASEFILEDS.HT_ACDET[(gridAccount.CurrentRow.Cells["ACSERIAL"].Value)]), gridcolumncod.Tbl_nm, gridcolumncod.Reftbltran_cd, gridcolumncod.Primaryddl, gridcolumncod.Dispddlfields, "Please select", gridcolumncod.Query_con, gridcolumncod.IsQcd, gridcolumncod.QcdCondition);
                            objfrmPopup.ObjBFD = objBASEFILEDS;
                            objfrmPopup.ShowDialog();
                            txt.Text = ((Hashtable)objBASEFILEDS.HT_ACDET[(gridAccount.CurrentRow.Cells["ACSERIAL"].Value)])[txt.Name].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Account_txt_Key_Press(object sender, KeyPressEventArgs e)
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

        #endregion

        private void ifrm_Accounting_Enter(object sender, EventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                iInit.ActiveFrm = this;
                ((frm_mainmenu)this.MdiParent).AccountChildWindowActivate(this);// efreshToolbar(this.tran_cd, this.tran_mode);
            }
        }

        private void ifrm_Accounting_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((frm_mainmenu)this.MdiParent).ActiveMdiChild != null)
            {
                iInit.ActiveFrm = this;
                if (this.Tran_cd == ((frm_mainmenu)this.MdiParent).Tran_cd)
                    ((frm_mainmenu)this.MdiParent).CloseChildWindow(0);
            }
        }

        public void toolbar_clicked()
        {
            foreach (Control c in this.Controls[1].Controls)
            {
                if (objBASEFILEDS.HTMAIN.ContainsKey(c.Name))
                {
                    if (c.Visible == true)
                    {
                        if (c is PictureBox)
                        {
                            //objBASEFILEDS.HTMAIN[c.Name] = ((PictureBox)c).ImageLocation;
                        }
                        else if (c is CheckBox)
                        {
                            if (((CheckBox)c).Checked.ToString() != "")
                            {
                                if (((CheckBox)c).Checked.ToString() == "0")
                                {
                                    objBASEFILEDS.HTMAIN[c.Name] = "false";
                                }
                                if (((CheckBox)c).Checked.ToString() == "1")
                                {
                                    objBASEFILEDS.HTMAIN[c.Name] = "true";
                                }
                                objBASEFILEDS.HTMAIN[c.Name] = ((CheckBox)c).Checked;
                            }
                        }
                        else if (c is ComboBox)
                        {
                            if (((ComboBox)c).SelectedValue != null && ((ComboBox)c).Text != null)
                            {
                                objBASEFILEDS.HTMAIN[((ComboBox)c).Name] = ((ComboBox)c).Text;
                                objBASEFILEDS.HTMAIN[((ComboBox)c).DisplayMember] = ((ComboBox)c).Text;
                                objBASEFILEDS.HTMAIN[((ComboBox)c).ValueMember] = ((ComboBox)c).SelectedValue;
                            }
                        }
                        else if (c is UserDT)
                        {
                            if (((UserDT)c).DtValue != null && ((UserDT)c).DtValue.ToString() != "")
                            {
                                objBASEFILEDS.HTMAIN[c.Name] = ((UserDT)c).DtValue.ToString("yyyy/MM/dd");
                            }
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
                }
                #region comment
                //if (c is TabControl)
                //{
                //    foreach (Control c1 in c.Controls)
                //    {
                //        if (c1 is TabPage)
                //        {
                //            foreach (Control c2 in c1.Controls)
                //            {
                //                if (c2 is DataGridView)
                //                {
                //                    foreach (DataGridViewRow row in ((DataGridView)c2).Rows)
                //                    {
                //                        foreach (DataGridViewColumn column in ((DataGridView)c2).Columns)
                //                        {
                //                            if (column.Visible && objBASEFILEDS.HTITEM.Contains(row.Cells["PTSERIAL"].Value.ToString()))
                //                            {
                //                                if (column is DataGridViewTextBoxColumn)
                //                                {
                //                                    ((Hashtable)objBASEFILEDS.HTITEM[row.Cells["PTSERIAL"].Value.ToString()])[column.Name] = row.Cells[column.Name].Value.ToString();
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
        }

        private void ifrm_Accounting_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}