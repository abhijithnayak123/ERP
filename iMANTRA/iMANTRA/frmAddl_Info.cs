using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;
using iMANTRA_IL;
using iMANTRA_BL;
using CUSTOM_iMANTRA;

namespace iMANTRA
{
    public partial class frmAddl_Info : BaseClass
    {
        /****************************************************************         
         * 1.0 Sharanamma Jekeen on 11.26.13 ==> Added new Class in Custom Layer
         * 
         * 
         * 
         * 
         * 
         * ****************************************************************/
        private BL_BASEFIELD objBSFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBSFD
        {
            get { return objBSFD; }
            set { objBSFD = value; }
        }

        FL_BASEFIELD objFL_BASEFIELD = new FL_BASEFIELD();
        private FL_GRIDEVENTS objFL_GRIDEVENTS = new FL_GRIDEVENTS();
        private FL_GEN_INVOICE objFL_GEN_INV = new FL_GEN_INVOICE();
        private FL_MAST objFLValidate = new FL_MAST();

        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();
        iTRANSACTION objiTRANSACTION = new iTRANSACTION();
        iGRIDITEM objiGRIDITEM = new iGRIDITEM();
        iInit objInit = new iInit();
        iDefaultControl objDefaultControl = new iDefaultControl();//1.0

        SetFieldsValue objSetFieldsValue = new SetFieldsValue();

        ErrorProvider errorProvider = new ErrorProvider();
        public Hashtable HTAddl_Info = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        int ctrlhgt = 0, hgt = 0, _width = 0, ctrlwid = 0, wid = 0;

        //int height = 1;
        // int padding = 10;
        int count = 1;
        int intgridorheader = 0;
        string tran_cd, tran_mode = "view_mode", btn_nm, title_nm;
        public DataSet dset = new DataSet();

        public frmAddl_Info(Hashtable ht, int gridorheader, string ptran_cd, string pmode, string btn_name, string lbl_nm)
        {
            InitializeComponent();
            HTAddl_Info = ht;
            intgridorheader = gridorheader;
            tran_cd = ptran_cd;
            tran_mode = pmode;
            btn_nm = btn_name;
            title_nm = lbl_nm;
            iInit.ActiveFrm = this;
        }
        private void frmAddl_Info_Load(object sender, EventArgs e)
        {
            objFL_BASEFIELD.objCompany = objBSFD.ObjCompany;
            objFL_GEN_INV.objCompany = objBSFD.ObjCompany;
            objFLValidate.objCompany = objBSFD.ObjCompany;

            ctrlhgt = this.ClientSize.Height * 10 / 100;
            hgt = 0; _width = 0;
            ctrlwid = this.ClientSize.Width * 50 / 100;
            wid = this.ClientSize.Width * 50 / 100;
            ucToolBar1.Minimize = false;
            ucToolBar1.Maximize = false;

            Loadaddl_info();
            Load_Details();
            DisplayControlsonMode();
            this.Width = _width;
            this.Height = hgt;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBSFD, "Transaction");
            ucToolBar1.Titlebar = title_nm;//btn_nm+ " Details";
            DefaultControlValidation();//1.0
            this.CenterToScreen();

        }
        #region 1.0
        private void DefaultControlValidation()
        {
            bool validflg = true;
            if (objDefaultControl.GetType().GetMethod("DefaultControl") != null)
            {
                objDefaultControl.ACTIVE_BL = objBSFD;
                // SetFieldsValue(tbl_nm, fld_nm, fld_val);
                MethodInfo methodInfo = typeof(iDefaultControl).GetMethod("DefaultControl");
                validflg = bool.Parse(methodInfo.Invoke(objDefaultControl, null).ToString().Trim());
                if (!validflg)
                {
                    if (objDefaultControl.BL_FIELDS.Errormsg.Length != 0)
                    {
                        AutoClosingMessageBox.Show(objDefaultControl.BL_FIELDS.Errormsg, "Default", 3000);
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please enter Valid Details", "Valid Details", 3000);
                    }
                }
                else
                {
                    objBSFD.HTMAIN = objDefaultControl.ACTIVE_BL.HTMAIN;
                    objBSFD.HTITEM = objDefaultControl.ACTIVE_BL.HTITEM;
                    BindControlsFromView();
                }
            }
        }
        #endregion 1.0
        private void Loadaddl_info()
        {
            objVALIDATION.ObjBLFD = ObjBSFD;
            objFL_GRIDEVENTS.objBASEFILEDS = ObjBSFD;

            if (intgridorheader == 0)
            {
                DataRow[] rows = dset.Tables[0].Select("_btntype='" + btn_nm + "' and _top=0");
                foreach (DataRow row in rows)
                {
                    Bind_Header_CUSTOM_Controls(row);
                }
            }
            else
            {
                DataRow[] rows = dset.Tables[0].Select("_btntype='" + btn_nm + "' and _top=0");
                foreach (DataRow row in rows)
                {
                    Bind_Header_CUSTOM_Controls(row);
                }
            }

            if (_width < 0)
            {
                _width = (ctrlwid / 4);//+ btn.Width;
            }
            else if (_width == 0)
            {
                AutoClosingMessageBox.Show("No Extra Fields Exist", "Validation", 3000);
                this.Close();
            }

            PopupButton btn = new PopupButton();
            btn.Name = "Done";
            btn.Text = "&DONE";
            //btn.Font = FontStyle.Bold;
            btn.Click += new EventHandler(btnDone_Click);
            //btn.Bounds = new Rectangle(((_width / 2) - (ctrlwid / 2)) > 0 ? ((_width / 2) - (ctrlwid / 2)) : (ctrlwid / 2), hgt + ctrlhgt, (ctrlwid / 4), ctrlhgt * 75 / 100);
            btn.Bounds = new Rectangle((_width / 2) - (ctrlwid / 4) / 2, hgt + ctrlhgt, (ctrlwid / 4), ctrlhgt * 70 / 100);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            //btn.Anchor = AnchorStyles.None;
            pnlAddl_info.Controls.Add(btn);

            this.Width = _width;

            if (this.ClientSize.Height > hgt)
            {
                if (hgt == 0)
                {
                    hgt = ctrlhgt * 75 / 100;
                }
                this.Height = hgt + (ctrlhgt / 2) * 30 / 100 + 2 * ctrlhgt;
            }
            else
                this.Height = this.ClientSize.Height;

            hgt = this.Height;
        }
        private void Bind_Header_CUSTOM_Controls(DataRow row)
        {
            count = int.Parse(row["order_no"].ToString());

            if (count % 2 != 0)
            {
                //height += 25;
                if (count != 1)
                    hgt += ctrlhgt;
                else
                {
                    hgt = 25;
                }
            }
            if (count == 1)
            {
                _width = wid;//(ctrlwid);
            }
            else if (count == 2)
            {
                _width += (ctrlwid / 2) + (ctrlwid / 2) * 90 / 100;
            }

            Label objlable = new Label();
            objlable.Name = "lbl" + row["head_nm"].ToString();
            objlable.Text = row["head_nm"].ToString();
            //objlable.ForeColor = bool.Parse(row["mandatory"].ToString().Trim()) ? Color.Red : Color.Black;
            objlable.Visible = !bool.Parse(row["inter_val"].ToString());
            if (!bool.Parse(row["inter_val"].ToString()))
            {
                if (count % 2 == 0)
                {
                    objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 80 / 100, ctrlhgt);
                }
                else
                {
                    objlable.Bounds = new Rectangle(0, hgt, (ctrlwid / 2) * 80 / 100, ctrlhgt);
                }
            }
            pnlAddl_info.Controls.Add(objlable);
            if (row["data_ty"].ToString().Trim().ToLower() == "varchar")
            {
                if (bool.Parse(row["_mul"].ToString().Trim()))
                {
                    ComboBox cmdseries = new ComboBox();
                    cmdseries.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmdseries.Name = row["fld_nm"].ToString().Trim();
                    cmdseries.Tag = row["data_ty"].ToString().Trim().ToLower();
                    cmdseries.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                    cmdseries.Enabled = !bool.Parse(row["_read"].ToString().Trim());

                    DataSet dscmdseries = new DataSet();
                    if (row["_query"] != null && row["_query"].ToString() != "")
                    {
                        dscmdseries = GetDatasetForCombobox(row["_query"].ToString(), row["_querycon"].ToString());//objFL_GEN_INV.Execute_Procedure_Query(row["_query"].ToString(), "");
                    }
                    else
                    {
                        dscmdseries = objFL_GEN_INV.GET_TBL_VAL(row["tbl_nm"].ToString().Trim(), this.tran_cd, objBSFD.ObjCompany.Compid.ToString());
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
                                // objlable.Bounds = new Rectangle(wid, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(wid + objlable.Width, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                            else
                            {
                                //objlable.Bounds = new Rectangle((ctrlwid / 2) * 5 / 100, hgt, (ctrlwid / 2) * 60 / 100, ctrlhgt);
                                cmdseries.Bounds = new Rectangle(objlable.Width, hgt, (ctrlwid / 2), ctrlhgt);
                            }
                        }


                        cmdseries.Validating += new CancelEventHandler(cmd_validate);
                        cmdseries.Enter -= new EventHandler(cmdseries_enter);
                        cmdseries.Enter += new EventHandler(cmdseries_enter);
                        pnlAddl_info.Controls.Add(cmdseries);
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
                    objtxt.Visible = !bool.Parse(row["inter_val"].ToString().Trim());
                    objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                    if (!bool.Parse(row["inter_val"].ToString().Trim()))
                    {
                        if (count % 2 == 0)
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                        else
                        {
                            objtxt.Bounds = new Rectangle(objlable.Width, hgt, (ctrlwid / 2), ctrlhgt);
                        }
                    }
                    objtxt.Validating += new CancelEventHandler(String_Validating);
                    pnlAddl_info.Controls.Add(objtxt);
                }
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "int")
            {
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width, hgt, (ctrlwid / 2), ctrlhgt);
                    }
                    objtxt.Validating += new CancelEventHandler(int_Validating);
                    objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
                pnlAddl_info.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "text")
            {
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                objtxt.Multiline = true;
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2), ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + 0, hgt, (ctrlwid / 2), ctrlhgt + ctrlhgt * 7 / 10);
                    }
                    objtxt.Validating += new CancelEventHandler(Text_Validating);
                    hgt += ctrlhgt + ctrlhgt;
                }
                pnlAddl_info.Controls.Add(objtxt);

            }
            if (row["data_ty"].ToString().Trim().ToLower() == "decimal")
            {
                TextBox objtxt = new TextBox();
                objtxt.Name = row["fld_nm"].ToString().Trim();
                objtxt.Text = "0.00";
                objtxt.Tag = row["data_ty"].ToString().Trim().ToLower();
                objtxt.Visible = !bool.Parse(row["inter_val"].ToString());
                objtxt.ReadOnly = bool.Parse(row["_read"].ToString().Trim());
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objtxt.Bounds = new Rectangle(objlable.Width + 0, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    objtxt.Validating += new CancelEventHandler(Decimal_Validating);
                    objtxt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
                    objtxt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
                }
                pnlAddl_info.Controls.Add(objtxt);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "bit")
            {
                CheckBox objchk = new CheckBox();
                objchk.Name = row["fld_nm"].ToString().Trim();
                objchk.Visible = !bool.Parse(row["inter_val"].ToString());
                objchk.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                objchk.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        objchk.Bounds = new Rectangle(objlable.Width + 0, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    objchk.Validating += new CancelEventHandler(CheckBox_Validating);
                }
                pnlAddl_info.Controls.Add(objchk);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "datetime")
            {
                UserDT dtp = new UserDT();
                dtp.Name = row["fld_nm"].ToString().Trim();
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = " ";
                dtp.Visible = !bool.Parse(row["inter_val"].ToString());
                dtp.Enabled = !bool.Parse(row["_read"].ToString().Trim());
                dtp.Tag = row["data_ty"].ToString().Trim().ToLower();
                if (!bool.Parse(row["inter_val"].ToString()))
                {
                    if (count % 2 == 0)
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + 0, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    dtp.Validating += new CancelEventHandler(dateTimePicker1_Validating);
                }
                pnlAddl_info.Controls.Add(dtp);
            }
            if (row["data_ty"].ToString().Trim().ToLower() == "time")
            {
                DateTimePicker dtp = new DateTimePicker();
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
                        dtp.Bounds = new Rectangle(objlable.Width + wid, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    else
                    {
                        dtp.Bounds = new Rectangle(objlable.Width + 0, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt);
                    }
                    dtp.Validating += new CancelEventHandler(dateTimePicker1_Time_Validating);
                }
                pnlAddl_info.Controls.Add(dtp);
            }

        }
        private void int_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            HTAddl_Info[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void String_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            HTAddl_Info[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void cmdseries_enter(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            DataRow[] rowtops;
            if (intgridorheader == 0)
            {
                rowtops = dset.Tables[0].Select("_btntype='" + btn_nm + "' and _top=0 and fld_nm='" + txt.Name + "'");
            }
            else
            {
                rowtops = dset.Tables[0].Select("_btntype='" + btn_nm + "' and _top=0  and fld_nm='" + txt.Name + "'");
            }
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
        private void cmd_validate(object sender, CancelEventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            HTAddl_Info[txt.Name] = txt.Text.ToString();
            HTAddl_Info[txt.DisplayMember] = txt.Text.ToString();
            if (txt.SelectedValue != null)
            {
                HTAddl_Info[txt.Name] = txt.SelectedValue.ToString();
            }
            e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void Text_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            HTAddl_Info[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void Decimal_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            HTAddl_Info[txt.Name] = txt.Text.Trim();
            e.Cancel = ValidateFields(txt.Name, txt.Text, "valid_con");
        }
        private void CheckBox_Validating(object sender, CancelEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            HTAddl_Info[chk.Name] = chk.Checked;
            e.Cancel = ValidateFields(chk.Name, chk.Checked.ToString(), "valid_con");
        }
        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            UserDT dtp = (UserDT)sender;
            dtp.CustomFormat = "dd-MMM-yyyy";
            HTAddl_Info[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
            e.Cancel = ValidateFields(dtp.Name, dtp.DtValue.ToString(), "valid_con");
        }
        private void dateTimePicker1_Time_Validating(object sender, CancelEventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            HTAddl_Info[dtp.Name] = dtp.Value.ToLongTimeString();
            e.Cancel = ValidateFields(dtp.Name, dtp.Value.ToString(), "valid_con");
        }
        private void txt_Key_Press(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

            }
        }
        private bool ValidateFields(string fld_nm, string fld_val, string _con_nm, int flg = 0)
        {
            if (tran_mode == "add_mode" || tran_mode == "edit_mode")
            {
                DataRow[] rowdtp;
                string _strValue = "";
                string _strDataType = "varchar";
                if (flg == 0)
                {
                    rowdtp = dset.Tables[0].Select("fld_nm='" + fld_nm + "'");
                }
                else
                {
                    rowdtp = dset.Tables[0].Select("fld_nm='" + fld_nm + "'");
                }
                bool validflg = true;
                foreach (DataRow row in rowdtp)
                {
                    _strDataType = row["data_ty"].ToString().Trim().ToLower();
                    // string exp = row["valid_con"].ToString();
                    string exp = row[_con_nm].ToString();
                    string[] ar = exp.Split(new Char[] { '?', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ar.Length >= 1)
                    {
                        if (ar.Length == 1)
                        {
                            validflg = CallCustomMethod(ar[0], row["tbl_nm"].ToString().Trim(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
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
                                        AutoClosingMessageBox.Show(row["head_nm"].ToString() + " should not be empty ", "Validation", 3000);
                                        validflg = false;
                                    }
                                    else
                                    {
                                        validflg = true;
                                    }
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString(),"Validation",3000);
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
                                        AutoClosingMessageBox.Show(" Please enter valid " + row["head_nm"].ToString(),"Validation",3000);
                                        validflg = false;
                                    }
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show("Sytanx is wrong!!" ,"Validation",3000);
                                }
                            }

                            if (validflg)
                            {
                                if (ar[1].ToString().Trim().ToUpper() != "TRUE")
                                {
                                    validflg = CallCustomMethod(ar[1], row["tbl_nm"].ToString().Trim(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
                                }
                            }
                            if (!validflg)
                            {
                                if (ar[2].ToString().Trim().ToUpper() != "FALSE")
                                {
                                    validflg = CallCustomMethod(ar[0], row["tbl_nm"].ToString().Trim(), fld_nm, _strValue, row["head_nm"].ToString().Trim(), flg);
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
                objVALIDATION.ObjBLFD = objBSFD;
                SetFieldsValue(tbl_nm, fld_nm, fld_val);
                objVALIDATION.objSetFieldsValue = objSetFieldsValue;
                MethodInfo methodInfo = typeof(VALIDATIONLAYER).GetMethod(_method_nm);
                validflg = bool.Parse(methodInfo.Invoke(objVALIDATION, null).ToString().Trim());
                if (!validflg)
                {
                    AutoClosingMessageBox.Show("Please enter Valid " + header_nm ,"Validation",3000);
                }
                else
                {
                    foreach (DictionaryEntry entry in objVALIDATION.ObjBLFD.HTMAIN)
                    {
                        if (HTAddl_Info.Contains(entry.Key))
                        {
                            HTAddl_Info[entry.Key] = entry.Value;
                        }
                    }
                    foreach (DictionaryEntry entry in objVALIDATION.ObjBLFD.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Count != 0)
                        {
                            foreach (DictionaryEntry entry1 in (Hashtable)entry.Value)
                            {
                                if (HTAddl_Info.Contains(entry1.Key))
                                {
                                    HTAddl_Info[entry1.Key] = entry1.Value;
                                }
                            }
                        }
                    }
                    BindControlsFromView();
                }
            }
            else
            {
                if (objiTRANSACTION.GetType().GetMethod(_method_nm) != null)
                {
                    objiTRANSACTION.ACTIVE_TRANSACTION = objBSFD;
                    SetFieldsValue(tbl_nm, fld_nm, fld_val);
                    MethodInfo methodInfo = typeof(iTRANSACTION).GetMethod(_method_nm);
                    validflg = bool.Parse(methodInfo.Invoke(objiTRANSACTION, null).ToString().Trim());
                    if (!validflg)
                    {
                        if (objiTRANSACTION.BL_FIELDS.Errormsg.Length != 0)
                        {
                            AutoClosingMessageBox.Show(objiTRANSACTION.BL_FIELDS.Errormsg ,"Transaction Validation",3000);
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Please enter Valid " + header_nm ,"Validation",3000);
                        }
                    }
                    else
                    {
                        objBSFD.HTMAIN = objiTRANSACTION.ACTIVE_TRANSACTION.HTMAIN;
                        BindControlsFromView();
                    }
                }
                else
                {
                    if (flg == 1)
                    {
                        if (objiGRIDITEM.GetType().GetMethod(_method_nm) != null)
                        {
                            SetFieldsValue(tbl_nm, fld_nm, fld_val);
                            objiGRIDITEM.HashgridItemvalue = HTAddl_Info;//((Hashtable)objBSFD.HTITEM[grid.CurrentRow.Cells["PTSERIAL"].Value.ToString()]);
                            MethodInfo methodInfo = typeof(iGRIDITEM).GetMethod(_method_nm);
                            validflg = bool.Parse(methodInfo.Invoke(objiGRIDITEM, null).ToString().Trim());
                            if (!validflg)
                            {
                                if (objiGRIDITEM.BL_FIELDS.Errormsg.Length != 0)
                                {
                                    AutoClosingMessageBox.Show(objiGRIDITEM.BL_FIELDS.Errormsg,"Grid Row Validation",3000);
                                }
                                else
                                {
                                    AutoClosingMessageBox.Show("Please enter Valid " + header_nm ,"Validation",3000);
                                }
                            }
                            else
                            {
                                foreach (DictionaryEntry entry in objiGRIDITEM.HashgridItemvalue)
                                {
                                    if (HTAddl_Info.Contains(entry.Key))
                                    {
                                        HTAddl_Info[entry.Key] = entry.Value;
                                    }
                                }
                                BindControlsFromView();
                            }
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Sorry!! Method is not defined!!","Validation",3000);
                            validflg = false;
                        }
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
                if (HTAddl_Info.ContainsKey(c.Name))
                {
                    if (c is CheckBox)
                    {
                        if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                            ((CheckBox)c).Checked = bool.Parse(HTAddl_Info[c.Name].ToString());
                        else
                            ((CheckBox)c).Checked = false;
                    }
                    else if (c is DateTimePicker)
                    {
                        if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                        {
                            //((DateTimePicker)c).CustomFormat = "dd-MMM-yyyy";
                            ((DateTimePicker)c).Value = DateTime.Parse(HTAddl_Info[c.Name].ToString());
                        }
                    }
                    else if (c is UserDT)
                    {
                        if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                        {
                            if (DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString() != "1900-01-01 12:00:00 AM" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "1900-00-01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000-00-01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "1900/00/01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000/00/01")
                            {
                                ((UserDT)c).bUpdateFlag = true;
                                ((UserDT)c).DtValue = DateTime.Parse(HTAddl_Info[c.Name].ToString());
                            }
                            else
                            {
                                ((UserDT)c).bUpdateFlag = false;
                                ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                            }
                        }
                    }
                    else
                    {
                        c.Text = HTAddl_Info[c.Name].ToString();
                    }
                    // c.Enabled = true;
                }
            }
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            string strValue = "";
            objFL_GRIDEVENTS.objBASEFILEDS = ObjBSFD;
            objVALIDATION.ObjBLFD = ObjBSFD;
            if (intgridorheader == 0)
            {
                DataRow[] rows = dset.Tables[0].Select("_btntype='" + btn_nm + "'");
                foreach (DataRow row in rows)
                {
                    if (ObjBSFD.HTMAIN.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString() != "false")//&& row["valid_con"].ToString() != ""
                    {
                        bool bln = true;
                        bln = ValidateFields(row["fld_nm"].ToString().Trim().ToUpper(), objBSFD.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "_mon_con", 0);
                        if (!bln && row["valid_con"].ToString() != "")
                        {
                            bln = ValidateFields(row["fld_nm"].ToString().Trim().ToUpper(), objBSFD.HTMAIN[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "valid_con", 0);
                        }
                        if (bln)
                        {
                            strValue = row["head_nm"].ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    if (ObjBSFD.HTITEM_VALUE.ContainsKey(row["fld_nm"].ToString().Trim().ToUpper()) && row["mandatory"].ToString() != "false")//&& row["valid_con"].ToString() != "" 
                    {
                        //bool bln = ValidateFields(row["fld_nm"].ToString().Trim().ToUpper(), objBSFD.HTITEM_VALUE[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), 0);
                        bool bln = true;
                        bln = ValidateFields(row["fld_nm"].ToString().Trim().ToUpper(), objBSFD.HTITEM_VALUE[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "_mon_con", 0);
                        if (!bln && row["valid_con"].ToString() != "")
                        {
                            bln = ValidateFields(row["fld_nm"].ToString().Trim().ToUpper(), objBSFD.HTITEM_VALUE[row["fld_nm"].ToString().Trim().ToUpper()].ToString(), "valid_con", 0);
                        }
                        if (bln)
                        {
                            strValue = row["head_nm"].ToString();
                            break;
                        }
                    }
                }
            }
            if (strValue == "")
            {
                foreach (var control in this.Controls)
                {
                    if (control.GetType() == typeof(Panel))
                    {
                        var panel = control as Panel;
                        foreach (var pan in panel.Controls)
                        {
                            if (pan.GetType().Name == "TextBox")
                            {
                                TextBox txt = (TextBox)pan;
                                HTAddl_Info[txt.Name] = txt.Text;
                            }
                            if (pan.GetType().Name == "ComboBox")
                            {
                                ComboBox txt = (ComboBox)pan;
                                if (txt != null)
                                {
                                    HTAddl_Info[txt.DisplayMember] = txt.Text;
                                    HTAddl_Info[txt.ValueMember] = txt.SelectedValue;
                                }
                            }
                            if (pan.GetType().Name == "CheckBox")
                            {
                                CheckBox chk = (CheckBox)pan;
                                HTAddl_Info[chk.Name] = chk.Checked;
                            }
                            if (pan.GetType().Name == "DateTimePicker")
                            {
                                DateTimePicker dtp = (DateTimePicker)pan;
                                HTAddl_Info[dtp.Name] = dtp.Value.ToLongTimeString();
                            }
                            if (pan.GetType().Name == "UserDT")
                            {
                                UserDT dtp = (UserDT)pan;
                                HTAddl_Info[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
                                // HTAddl_Info[dtp.Name] = dtp.DtValue.ToString("yyyy/MM/dd");
                            }
                        }
                    }
                }
                this.Close();
            }
            else
            {
                AutoClosingMessageBox.Show("Please enter valid data in " + strValue, "Validation", 3000);
            }
        }
        private void Load_Details()
        {
            foreach (var control in this.Controls)
            {
                if (control.GetType() == typeof(Panel))
                {
                    var panel = control as Panel;
                    foreach (var pan in panel.Controls)
                    {
                        if (pan.GetType().Name == "TextBox" && HTAddl_Info.ContainsKey(((TextBox)pan).Name))
                        {
                            TextBox txt = (TextBox)pan;
                            txt.Text = HTAddl_Info[txt.Name].ToString();
                        }
                        if (pan.GetType().Name == "ComboBox" && HTAddl_Info.ContainsKey(((ComboBox)pan).Name))
                        {
                            ComboBox c = (ComboBox)pan;
                            if (c != null)
                            {
                                ((ComboBox)c).Text = HTAddl_Info[c.DisplayMember].ToString();
                                ((ComboBox)c).Text = HTAddl_Info[c.DisplayMember].ToString();
                                ((ComboBox)c).SelectedValue = HTAddl_Info[c.ValueMember].ToString();
                            }
                        }
                        if (pan.GetType().Name == "CheckBox" && HTAddl_Info.ContainsKey(((CheckBox)pan).Name))
                        {
                            CheckBox chk = (CheckBox)pan;
                            if (HTAddl_Info[chk.Name] != null && HTAddl_Info[chk.Name].ToString() != "")
                                chk.Checked = bool.Parse(HTAddl_Info[chk.Name].ToString());
                            else
                                chk.Checked = false;
                        }
                        if (pan.GetType().Name == "DateTimePicker" && HTAddl_Info.ContainsKey(((DateTimePicker)pan).Name))
                        {
                            DateTimePicker dtp = (DateTimePicker)pan;

                            if (HTAddl_Info[dtp.Name] != null && HTAddl_Info[dtp.Name].ToString() != "")
                            {
                                dtp.Value = DateTime.Parse(HTAddl_Info[dtp.Name].ToString());
                            }
                        }
                        if (pan.GetType().Name == "UserDT" && HTAddl_Info.ContainsKey(((UserDT)pan).Name))
                        {
                            UserDT dtp = (UserDT)pan;
                            if (HTAddl_Info[dtp.Name] != null && HTAddl_Info[dtp.Name].ToString() != "")
                            {
                                if (DateTime.Parse(HTAddl_Info[dtp.Name].ToString()).ToString() != "1900-01-01 12:00:00 AM" && DateTime.Parse(HTAddl_Info[dtp.Name].ToString()).ToString("yyyy/mm/dd") != "1900-00-01" && DateTime.Parse(HTAddl_Info[dtp.Name].ToString()).ToString("yyyy/mm/dd") != "2000-00-01" && DateTime.Parse(HTAddl_Info[dtp.Name].ToString()).ToString("yyyy/mm/dd") != "1900/00/01" && DateTime.Parse(HTAddl_Info[dtp.Name].ToString()).ToString("yyyy/mm/dd") != "2000/00/01")
                                {
                                    dtp.bUpdateFlag = true;
                                    dtp.DtValue = DateTime.Parse(HTAddl_Info[dtp.Name].ToString());
                                }
                                else
                                {
                                    dtp.bUpdateFlag = false;
                                    dtp.DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                }
                            }
                        }
                    }
                }
            }
        }
        private void DisplayControlsonMode()
        {
            switch (tran_mode)
            {
                case "add_mode":
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (HTAddl_Info.ContainsKey(c.Name))
                        {
                            if (c is UserDT)
                            {
                                if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                                {
                                    ((UserDT)c).bUpdateFlag = false;
                                    ((UserDT)c).DtValue = DateTime.Now;
                                }
                            }
                        }
                    }
                    break;
                case "edit_mode":
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (HTAddl_Info.ContainsKey(c.Name))
                        {
                            if (c is UserDT)
                            {
                                if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                                {
                                    if (DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString() != "1900-01-01 12:00:00 AM" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "1900-00-01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000-00-01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "1900/00/01" && DateTime.Parse(HTAddl_Info[c.Name].ToString()).ToString("yyyy/mm/dd") != "2000/00/01")
                                    {
                                        ((UserDT)c).bUpdateFlag = true;
                                        ((UserDT)c).DtValue = DateTime.Parse(HTAddl_Info[c.Name].ToString());
                                    }
                                    else
                                    {
                                        ((UserDT)c).bUpdateFlag = false;
                                        ((UserDT)c).DtValue = DateTime.Now;// DateTime.Parse("01/01/1900");
                                    }
                                }
                            }
                        }
                    } break;
                case "view_mode":
                    foreach (Control c in this.Controls[1].Controls)
                    {
                        if (HTAddl_Info.ContainsKey(c.Name))
                        {
                            if (c is CheckBox)
                            {
                                if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                                    ((CheckBox)c).Checked = bool.Parse(HTAddl_Info[c.Name].ToString());
                                else
                                    ((CheckBox)c).Checked = false;
                            }
                            else if (c is DateTimePicker)
                            {
                                if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                                {
                                    ((DateTimePicker)c).Value = DateTime.Parse(HTAddl_Info[c.Name].ToString());
                                }
                            }
                            else if (c is UserDT)
                            {
                                if (HTAddl_Info[c.Name] != null && HTAddl_Info[c.Name].ToString() != "")
                                {
                                    // c.Text = DateTime.Parse(objBASEFILEDS.HTMAIN[c.Name].ToString()).ToString("yyyy/MM/dd");
                                    ((UserDT)c).bUpdateFlag = true;
                                    ((UserDT)c).DtValue = DateTime.Parse(HTAddl_Info[c.Name].ToString());
                                }
                            }
                            else
                            {
                                c.Text = HTAddl_Info[c.Name].ToString();
                            }
                            if (!(c is Label)) c.Enabled = false;
                        }
                    }
                    break;
                default: break;
            }
        }
        private void frmAddl_Info_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
