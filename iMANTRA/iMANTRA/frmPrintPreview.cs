using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer.ClientDoc;
using CrystalDecisions.ReportAppServer.Controllers;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using iMANTRA_BL;
using iMANTRA_IL;
using iMANTRA_DL;
using iMANTRA_iniL;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace iMANTRA
{
    public partial class frmPrintPreview : BaseClass
    {
        /************************************************************************************
         * 1.0 --> 31.01.2014 Sharanamma Jekeen : OutLook impliementation(send mail option.)
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * *****************************************************************************************/
        BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        public BL_BASEFIELD ObjBLFD
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        FL_REP_MAST objFLRep = new FL_REP_MAST();
        FL_GEN_INVOICE objFl_Gen_Inv = new FL_GEN_INVOICE();
        Ini objIni = new Ini();

        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

        DataSet dsetRep = new DataSet();
        public Hashtable HTFilter = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        DataGridView dgv = new DataGridView();
        DataGridView dgv1 = new DataGridView();

        Hashtable _hashSendMailAttachements = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        bool flgDocorReport = false, flgCopy = false, flgPreviewOrSendMail;
        int ctrlhgt = 0, hgt = 0, ctrlwid = 0, wid = 0;

        public frmPrintPreview(BL_BASEFIELD objbl, bool flgDocorRep, bool PreviewOrSendMail)
        {
            InitializeComponent();
            objBLFD = objbl;
            flgDocorReport = flgDocorRep;
            flgPreviewOrSendMail = PreviewOrSendMail;
            ctrlhgt = this.ClientSize.Height;
            hgt = 0;
            ctrlwid = this.ClientSize.Width;
            wid = this.ClientSize.Width / 2;
        }

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            objFLRep.objCompany = objBLFD.ObjCompany;
            objFl_Gen_Inv.objCompany = objBLFD.ObjCompany;

            if (flgDocorReport)
            {
                dsetRep = objFLRep.Get_Rep_Documents(ObjBLFD);
                if (dsetRep != null && dsetRep.Tables[0].Rows.Count != 0)
                {
                    if (dsetRep.Tables[0].Rows.Count == 1)
                    {
                        if (flgPreviewOrSendMail)
                        {
                            frmPrint objfrmPrint = new frmPrint(true);
                            objfrmPrint.ObjBLFD = ObjBLFD;
                            objfrmPrint.tran_cd = ObjBLFD.Code;
                            objfrmPrint.tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
                            objfrmPrint.sp_name = dsetRep.Tables[0].Rows[0]["sp_nm"].ToString().Trim();
                            objfrmPrint.rep_nm = dsetRep.Tables[0].Rows[0]["rep_nm"].ToString().Trim();
                            objfrmPrint.rep_nmshow = dsetRep.Tables[0].Rows[0]["desc"].ToString().Trim();
                            // objfrmPrint.objComp = objBLFD.ObjCompany;
                            objfrmPrint.Show();
                            this.Close();
                        }
                        else
                        {
                            SendMail();
                        }
                    }
                    else
                    {
                        GroupBox grpbxreports = new GroupBox();
                        grpbxreports.Text = "Reports Details";

                        dgv.Name = "dgvRep";
                        dgv.Dock = DockStyle.Fill;
                        dgv.RowHeadersVisible = false;
                        dgv.AllowUserToAddRows = false;

                        DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
                        chkcol.HeaderText = "Select";
                        chkcol.Name = "sel";
                        dgv.Columns.Add(chkcol);
                        dgv.Columns["sel"].Width = ctrlwid * 22 / 100;

                        DataGridViewTextBoxColumn txtcol = new DataGridViewTextBoxColumn();
                        txtcol.HeaderText = "Report Name";
                        txtcol.Name = "rep_desc";
                        dgv.Columns.Add(txtcol);
                        dgv.Columns["rep_desc"].Width = ctrlwid * 75 / 100;

                        DataGridViewTextBoxColumn txtcol1 = new DataGridViewTextBoxColumn();
                        txtcol1.HeaderText = "Procedure Name";
                        txtcol1.Name = "sp_nm";
                        dgv.Columns.Add(txtcol1);
                        dgv.Columns["sp_nm"].Visible = false;

                        DataGridViewTextBoxColumn txtcol2 = new DataGridViewTextBoxColumn();
                        txtcol2.HeaderText = "Report Name";
                        txtcol2.Name = "rep_nm";
                        dgv.Columns.Add(txtcol2);
                        dgv.Columns["rep_nm"].Visible = false;

                        dgv.Rows.Clear();
                        int i = 0;
                        foreach (DataRow row in dsetRep.Tables[0].Rows)
                        {
                            dgv.Rows.Add(1);
                            dgv.Rows[i].Cells[1].Value = row["desc"].ToString();
                            dgv.Rows[i].Cells[2].Value = row["sp_nm"].ToString();
                            dgv.Rows[i].Cells[3].Value = row["rep_nm"].ToString();
                            i++;
                        }
                        //dgv.Bounds = new Rectangle(5, hgt + 5, (ctrlwid), ctrlhgt * 75 / 100);
                        grpbxreports.Bounds = new Rectangle(5, hgt + 5, (ctrlwid) - 10, ctrlhgt / 2);
                        hgt += ctrlhgt / 2 + (ctrlhgt * 5 / 100);
                        grpbxreports.Controls.Add(dgv);
                        //    dgv.Rows.RemoveAt(dgv.Rows.Count-1);
                        panel1.Controls.Add(grpbxreports);


                        if (objBLFD.Copies_nm != "" && objBLFD.Copies_nm != null)
                        {
                            GroupBox grpbxreports1 = new GroupBox();
                            grpbxreports1.Text = "Copy Details";

                            //  DataGridView dgv1 = new DataGridView();
                            dgv1.Name = "dgvRepCopy";
                            dgv1.Dock = DockStyle.Fill;
                            //  dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            //dgv.Height = panel1.Size.Height * 70 / 100;
                            dgv1.RowHeadersVisible = false;
                            dgv1.AllowUserToAddRows = false;

                            DataGridViewCheckBoxColumn chkcol1 = new DataGridViewCheckBoxColumn();
                            chkcol1.HeaderText = "Select";
                            chkcol1.Name = "sel";
                            dgv1.Columns.Add(chkcol1);
                            dgv1.Columns["sel"].Width = ctrlwid * 22 / 100;

                            DataGridViewTextBoxColumn txtcolcopy = new DataGridViewTextBoxColumn();
                            txtcolcopy.HeaderText = "Copy Name";
                            txtcolcopy.Name = "copy_nm";
                            dgv1.Columns.Add(txtcolcopy);
                            dgv1.Columns["copy_nm"].Width = ctrlwid * 75 / 100;

                            dgv1.Rows.Clear();
                            //DataSet dsetcopy = objFl_Gen_Inv.GET_TBL_VAL("Copy_mast", "", ObjBLFD.ObjCompany.Compid.ToString());

                            String strCopy = objBLFD.Copies_nm;

                            int j = 0;

                            //foreach (DataRow row in dsetcopy.Tables[0].Rows)
                            //{
                            //    dgv1.Rows.Add(1);
                            //    dgv1.Rows[j].Cells[1].Value = row["copy_nm"].ToString();
                            //    j++;
                            //}

                            foreach (String str in strCopy.Split(','))
                            {
                                dgv1.Rows.Add(1);
                                dgv1.Rows[j].Cells[1].Value = str;
                                j++;
                            }

                            dgv1.CellContentClick += new DataGridViewCellEventHandler(dgv1_CellContent_click);
                            grpbxreports1.Bounds = new Rectangle(5, hgt, (ctrlwid) - 10, ctrlhgt / 2);
                            hgt += ctrlhgt / 2 + (ctrlhgt * 5 / 100);
                            grpbxreports1.Controls.Add(dgv1);
                            panel1.Controls.Add(grpbxreports1);
                        }

                        Button btn = new Button();
                        btn.Name = "Proceed";
                        btn.Text = "&Proceed";
                        btn.Click += new EventHandler(btnProceed_Click);
                        btn.Bounds = new Rectangle((ctrlwid / 2) * 15 / 100, hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 10 / 100);

                        //  btn.TextAlign = ContentAlignment.MiddleCenter;
                        this.panel1.Controls.Add(btn);
                        Button btn1 = new Button();
                        btn1.Name = "Cancel";
                        btn1.Text = "&Cancel";
                        btn1.Click += new EventHandler(btnCancel_Click);
                        btn1.Bounds = new Rectangle(((ctrlwid / 2) * 15 / 100) + (ctrlwid / 2), hgt, (ctrlwid / 2) * 70 / 100, ctrlhgt * 10 / 100);
                        hgt += ctrlhgt * 15 / 100;
                        //  btn1.TextAlign = ContentAlignment.MiddleCenter;
                        this.panel1.Controls.Add(btn1);

                        // this.panel1.Height = hgt; 
                        this.Width = ctrlwid;
                        this.Height = hgt;

                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("No documents exist", "Error", 3000);
                    this.Close();
                }
            }
            else
            {
                frmPrint objfrmPrint = new frmPrint(false);
                objfrmPrint.ObjBLFD = ObjBLFD;
                objfrmPrint.sp_name = HTFilter["sp_nm"].ToString();
                objfrmPrint.rep_nm = HTFilter["rep_nm"].ToString();
                objfrmPrint.rep_nmshow = HTFilter["rep_gr"].ToString();
                objfrmPrint.HTFilter = this.HTFilter;
                // objfrmPrint.objComp = objBLFD.ObjCompany;
                objfrmPrint.Show();
                this.Close();
            }
            ucToolBar1.Width1 = this.Width;
            this.Height = hgt;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "Report");
            ucToolBar1.Titlebar = "Print Preview";
        }
        private void dgv1_CellContent_click(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvCopy = (DataGridView)sender;
            if (dgvCopy != null)
            {
                foreach (DataGridViewRow r in dgvCopy.Rows)
                {
                    if (r.Cells["sel"].Value != null && bool.Parse(r.Cells["sel"].Value.ToString()))
                    {
                        flgCopy = true;
                        break;
                    }
                }
            }
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (flgPreviewOrSendMail)
            {
                foreach (Control c in panel1.Controls)
                {
                    if (c is GroupBox)
                    {
                        foreach (Control c1 in ((GroupBox)c).Controls)
                        {
                            if (c1 is DataGridView)
                            {
                                foreach (DataGridViewRow r in ((DataGridView)c1).Rows)
                                {
                                    if (((DataGridView)c1).Columns.Count != 2 && !flgCopy && r.Cells["sel"].Value != null && bool.Parse(r.Cells["sel"].Value.ToString()))
                                    {
                                        objBLFD.ObjCompany.Copy_nm = "Original For Buyer";
                                        frmPrint objfrmPrint = new frmPrint(true);
                                        objfrmPrint.ObjBLFD = ObjBLFD;
                                        objfrmPrint.tran_cd = ObjBLFD.Code;
                                        objfrmPrint.tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
                                        objfrmPrint.sp_name = r.Cells["sp_nm"].Value.ToString();
                                        objfrmPrint.rep_nm = r.Cells["rep_nm"].Value.ToString();
                                        objfrmPrint.rep_nmshow = r.Cells["rep_desc"].Value.ToString();
                                        // objfrmPrint.objComp = objBLFD.ObjCompany;
                                        objfrmPrint.Show();
                                    }
                                    else
                                    {
                                        if (((DataGridView)c1).Columns.Count == 2 && r.Cells["sel"].Value != null && bool.Parse(r.Cells["sel"].Value.ToString()))
                                        {
                                            foreach (DataGridViewRow row in dgv.Rows)
                                            {
                                                if (row.Cells["sel"].Value != null && bool.Parse(row.Cells["sel"].Value.ToString()))
                                                {
                                                    objBLFD.ObjCompany.Copy_nm = r.Cells["copy_nm"].Value.ToString();
                                                    frmPrint objfrmPrint = new frmPrint(true);
                                                    objfrmPrint.ObjBLFD = ObjBLFD;
                                                    objfrmPrint.tran_cd = ObjBLFD.Code;
                                                    objfrmPrint.tran_id = ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString();
                                                    objfrmPrint.sp_name = row.Cells["sp_nm"].Value.ToString();
                                                    objfrmPrint.rep_nm = row.Cells["rep_nm"].Value.ToString();
                                                    objfrmPrint.rep_nmshow = row.Cells["rep_desc"].Value.ToString();
                                                    // objfrmPrint.objComp = objBLFD.ObjCompany;
                                                    objfrmPrint.Show();
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
            else
            {
                SendMail();//1.0
            }
            this.Close();
        }

        #region 1.0
        private void SendMail()
        {
            bool _blnFlg = false;
            dsetRep = objFLRep.Get_Rep_Documents(ObjBLFD);
            if (dsetRep != null && dsetRep.Tables[0].Rows.Count != 0)
            {
                if (dsetRep.Tables[0].Rows.Count == 1)
                {
                    ConvertToPdf(ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString(), ObjBLFD.Code, dsetRep.Tables[0].Rows[0]["sp_nm"].ToString().Trim(), dsetRep.Tables[0].Rows[0]["rep_nm"].ToString().Trim(), dsetRep.Tables[0].Rows[0]["desc"].ToString().Trim());
                }
                else
                {
                    foreach (Control c in panel1.Controls)
                    {
                        if (c is GroupBox)
                        {
                            foreach (Control c1 in ((GroupBox)c).Controls)
                            {
                                if (c1 is DataGridView)
                                {
                                    foreach (DataGridViewRow r in ((DataGridView)c1).Rows)
                                    {
                                        if (((DataGridView)c1).Columns.Count != 2 && !flgCopy && r.Cells["sel"].Value != null && bool.Parse(r.Cells["sel"].Value.ToString()))
                                        {
                                            objBLFD.ObjCompany.Copy_nm = "Original For Buyer";
                                            ConvertToPdf(ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString(), ObjBLFD.Code, r.Cells["sp_nm"].Value.ToString(), r.Cells["rep_nm"].Value.ToString(), r.Cells["rep_desc"].Value.ToString());
                                        }
                                        else
                                        {
                                            if (((DataGridView)c1).Columns.Count == 2 && r.Cells["sel"].Value != null && bool.Parse(r.Cells["sel"].Value.ToString()))
                                            {
                                                foreach (DataGridViewRow row in dgv.Rows)
                                                {
                                                    //if (row.Cells["sel"].Value != null && bool.Parse(row.Cells["sel"].Value.ToString()))
                                                    //{
                                                    if (row.Cells["sel"].Value != null && row.Cells["sel"].Value.ToString() != "")
                                                    {
                                                        if (row.Cells["sel"].Value.ToString() == "1")
                                                        {
                                                            _blnFlg = true;
                                                        }
                                                        else if (row.Cells["sel"].Value.ToString() == "0")
                                                        {
                                                            _blnFlg = false;
                                                        }
                                                    }
                                                    if (_blnFlg)
                                                    {
                                                        objBLFD.ObjCompany.Copy_nm = r.Cells["copy_nm"].Value.ToString();
                                                        ConvertToPdf(ObjBLFD.HTMAIN[ObjBLFD.Primary_id].ToString(), ObjBLFD.Code, row.Cells["sp_nm"].Value.ToString(), row.Cells["rep_nm"].Value.ToString(), row.Cells["rep_desc"].Value.ToString());
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
                DataSet dsetCM = objDL_ADAPTER.dsquery("select * from CM_MAST where ac_id='" + objBLFD.HTMAIN["ac_id"].ToString() + "' and ac_nm='" + objBLFD.HTMAIN["ac_nm"].ToString() + "'");
                if (dsetCM != null && dsetCM.Tables.Count != 0 && dsetCM.Tables[0].Rows.Count != 0)
                {
                    if (dsetCM.Tables[0].Columns.Contains("email") && dsetCM.Tables[0].Rows[0]["email"] != null && dsetCM.Tables[0].Rows[0]["email"].ToString() != "")
                    {
                        Outlook.Application oApp = new Outlook.Application();

                        Outlook.Accounts accounts = oApp.Session.Accounts;

                        Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                        oMailItem.To = dsetCM.Tables[0].Rows[0]["email"].ToString();
                        //oMailItem.CC = "inode-pro@inodetechnologies.com";
                        oMailItem.Subject = objBLFD.Code + "-Attachment(s) ";
                        if (_hashSendMailAttachements != null && _hashSendMailAttachements.Count != 0)
                        {
                            foreach (DictionaryEntry entry in _hashSendMailAttachements)
                            {
                                oMailItem.Attachments.Add(entry.Value, Outlook.OlAttachmentType.olByValue, 1, entry.Value);
                            }
                        }
                        if (objBLFD.HASHTABLES != null && objBLFD.HASHTABLES.HashFileUpload!=null && objBLFD.HASHTABLES.HashFileUpload.Count!= 0)
                        {
                            foreach (DictionaryEntry entry in objBLFD.HASHTABLES.HashFileUpload)
                            {
                                if (((Hashtable)entry.Value).Count != 0)
                                {
                                    oMailItem.Attachments.Add(((Hashtable)entry.Value)["file_path"].ToString(), Outlook.OlAttachmentType.olByValue, 1, ((Hashtable)entry.Value)["file_path"].ToString());
                                }
                            }
                        }

                        string mailbody = "";
                        mailbody = mailbody + "<BR/><BR/>" + ReadSignature();
                        oMailItem.HTMLBody = mailbody;

                        oMailItem.Display(true);
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Please Provide Valid Email id");
                        this.Close();
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Please Provide Valid Customer");
                    this.Close();
                }
            }
            this.Close();
        }
        private void ConvertToPdf(string tran_id, string tran_cd, string sp_nm, string rep_nm, string rep_desc)
        {
            CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            DataSet ds = objFLRep.REPORT_TRANSACTION(tran_id, tran_cd, sp_nm);
            CrystalDecisions.CrystalReports.Engine.ReportDocument doc;
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                doc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + objIni.GetKeyFieldValue("SQL", "initial catalog") + @"\" + rep_nm + ".rpt");
                doc.SetDataSource(ds.Tables[0]);
                //if (objLicense.License_code == "sZLKa7BcYsepmYbILRVBlg==")
                if (ObjBLFD.ObjLoginUser != null && (ObjBLFD.ObjLoginUser.License_type == "SUPPORT LICENSE" || ObjBLFD.ObjLoginUser.License_type == "DEMO LICENSE"))
                {
                    Report_Modification(doc);
                }

                Type t = typeof(Company);
                PropertyInfo[] publicFieldInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                ParameterFieldDefinitions crParameterdef;
                crParameterdef = doc.DataDefinition.ParameterFields;
                foreach (PropertyInfo field in publicFieldInfos)
                {
                    foreach (ParameterFieldDefinition def in crParameterdef)
                    {
                        if (null != field)
                        {
                            if (def.Name.Equals("ORG." + field.Name.ToLower()))    // check if parameter exists in report
                            {
                                doc.SetParameterValue("ORG." + field.Name.ToLower(), field.GetValue(ObjBLFD.ObjCompany, null) == null ? string.Empty : field.GetValue(ObjBLFD.ObjCompany, null));    // set the parameter value in the report
                            }
                        }
                    }
                }
                string strApproval = string.Empty;
                //DataSet dsetTable = objDL_ADAPTER.dsquery("select i_approved from " + ObjBLFD.Main_tbl_nm + " main_tbl inner join tran_set on main_tbl.tran_cd=tran_set.code where tran_set.isApprove=1 and tran_set.tran_type='Transaction' and "+objBLFD.Primary_id+"='" + tran_id + "' and tran_set.code='" + ObjBLFD.Code + "' and tran_set.compid=" + ObjBLFD.ObjCompany.Compid);
                //if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
                //{
                //    if (dsetTable.Tables[0].Rows[0]["i_approved"] != null && dsetTable.Tables[0].Rows[0]["i_approved"].ToString() != "" && bool.Parse(dsetTable.Tables[0].Rows[0]["i_approved"].ToString()))
                //    {
                //        strApproval = "APPROVE";
                //    }
                //    else
                //    {
                //        strApproval = "PENDING";
                //    }
                //}
                 DataSet dsetTable1 = objDL_ADAPTER.dsquery("select code,Main_tbl_nm,Approve_tbl_nm,isReqAuthority from tran_set where isApprove=1 and tran_type='Transaction' and code='" + ObjBLFD.Code + "' and compid=" + ObjBLFD.ObjCompany.Compid);
                 if (dsetTable1 != null && dsetTable1.Tables.Count != 0 && dsetTable1.Tables[0].Rows.Count != 0)
                 {
                     DataSet dsetTable = new DataSet();
                     if (bool.Parse(dsetTable1.Tables[0].Rows[0]["isReqAuthority"] != null && dsetTable1.Tables[0].Rows[0]["isReqAuthority"].ToString() != "" ? dsetTable1.Tables[0].Rows[0]["isReqAuthority"].ToString() : "False"))
                     {
                         // dsetTable = objDL_ADAPTER.dsquery("select i_approved=case when(authorse_by='NOT APPLICABLE') then 1 else i_approved end from " + ObjBLFD.Main_tbl_nm + " main_tbl inner join tran_set on main_tbl.tran_cd=tran_set.code where tran_set.isApprove=1 and tran_set.tran_type='Transaction' and " + ObjBLFD.Primary_id + "='" + tran_id + "' and tran_set.code='" + ObjBLFD.Code + "' and tran_set.compid=" + ObjBLFD.ObjCompany.Compid);
                         dsetTable = objDL_ADAPTER.dsquery("select i_approved=case when(authorse_by='NOT APPLICABLE') then 'True' else i_approved end from " + ObjBLFD.Main_tbl_nm + " main_tbl where " + ObjBLFD.Primary_id + "='" + tran_id + "' and main_tbl.tran_cd='" + ObjBLFD.Code + "' and main_tbl.compid=" + ObjBLFD.ObjCompany.Compid);
                     }
                     else
                     {
                         //dsetTable = objDL_ADAPTER.dsquery("select i_approved from " + ObjBLFD.Main_tbl_nm + " main_tbl inner join tran_set on main_tbl.tran_cd=tran_set.code where tran_set.isApprove=1 and tran_set.tran_type='Transaction' and " + ObjBLFD.Primary_id + "='" + tran_id + "' and tran_set.code='" + ObjBLFD.Code + "' and tran_set.compid=" + ObjBLFD.ObjCompany.Compid);
                         dsetTable = objDL_ADAPTER.dsquery("select i_approved from " + ObjBLFD.Main_tbl_nm + " main_tbl where " + ObjBLFD.Primary_id + "='" + tran_id + "' and main_tbl.code='" + ObjBLFD.Code + "' and main_tbl.compid=" + ObjBLFD.ObjCompany.Compid);
                     }
                     if (dsetTable != null && dsetTable.Tables.Count != 0 && dsetTable.Tables[0].Rows.Count != 0)
                     {
                         if (dsetTable.Tables[0].Rows[0]["i_approved"] != null && dsetTable.Tables[0].Rows[0]["i_approved"].ToString() != "" && bool.Parse(dsetTable.Tables[0].Rows[0]["i_approved"].ToString()))
                         {
                             strApproval = "APPROVE";
                         }
                         else
                         {
                             strApproval = "PENDING";
                         }
                     }
                 }
                foreach (ParameterFieldDefinition def in crParameterdef)
                {
                    if (def.Name.Equals("approval"))    // check if parameter exists in report
                    {
                        doc.SetParameterValue("approval", strApproval);    // set the parameter value in the report
                    }
                }
                crystalReportViewer1.ReportSource = doc;
                crystalReportViewer1.Visible = false;

                CrystalDecisions.Shared.ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                CrExportOptions = doc.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;

                    CrDiskFileDestinationOptions.DiskFileName = AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + "\\FILES\\" + rep_desc.Replace(" ", "_") + tran_id + tran_cd + ".pdf";
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                doc.Export();

                if (_hashSendMailAttachements == null || !_hashSendMailAttachements.Contains(rep_nm.Replace(".rpt", "_") + tran_id + tran_cd))
                {
                    _hashSendMailAttachements[rep_nm.Replace(".rpt", "_") + tran_id + tran_cd] = AppDomain.CurrentDomain.BaseDirectory + objBLFD.ObjCompany.Db_nm + "\\FILES\\" + rep_desc.Replace(" ", "_") + tran_id + tran_cd + ".pdf";
                }
            }
            else
            {
                AutoClosingMessageBox.Show("error in report", "Error");
            }
        }
        private string ReadSignature()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Signatures";
            string signature = string.Empty;
            DirectoryInfo diInfo = new DirectoryInfo(appDataDir);
            if
            (diInfo.Exists)
            {
                FileInfo[] fiSignature = diInfo.GetFiles("*.htm");

                if (fiSignature.Length > 0)
                {
                    StreamReader sr = new StreamReader(fiSignature[0].FullName, Encoding.Default);
                    signature = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(signature))
                    {
                        string fileName = fiSignature[0].Name.Replace(fiSignature[0].Extension, string.Empty);
                        signature = signature.Replace(fileName + "_files/", appDataDir + "/" + fileName + "_files/");
                    }
                }

            }
            return signature;
        }
        #endregion 1.0

        private void Report_Modification(CrystalDecisions.CrystalReports.Engine.ReportDocument cryRpt)
        {
            CrystalDecisions.ReportAppServer.ClientDoc.ISCDReportClientDocument rptClientDoc1 = cryRpt.ReportClientDocument;
            ReportDefController2 reportDef = rptClientDoc1.ReportDefController;

            ReportSectionController reportSectionController = rptClientDoc1.ReportDefController.ReportSectionController;

            CrystalDecisions.ReportAppServer.ReportDefModel.Section newsec;

            int index = reportDef.ReportDefinition.PageHeaderArea.Sections.Count;
            //  CrystalDecisions.ReportAppServer.ReportDefModel.PictureObject objPictureBox = new CrystalDecisions.ReportAppServer.ReportDefModel.PictureObject();
            if (index > 0)
            {
                // reportDef.ReportDefinition.PageHeaderArea.Sections.Add(reportDef.ReportDefinition.PageHeaderArea.Sections[index - 1]);
                newsec = reportDef.ReportDefinition.PageHeaderArea.Sections[index - 1];//reportDef.ReportDefinition.PageHeaderArea.Sections[index - 1];
            }
            else
            {
                index = 0;
                newsec = reportDef.ReportDefinition.PageHeaderArea.Sections[index];
            }

            reportDef.ReportDefinition.PageHeaderArea.Sections.Insert(index, newsec);
            CrystalDecisions.ReportAppServer.ReportDefModel.Section sectionToAddTo = reportDef.ReportDefinition.PageHeaderArea.Sections[index];

            CrystalDecisions.ReportAppServer.ReportDefModel.SectionFormat newSectionFormat = sectionToAddTo.Format;
            newSectionFormat.EnableKeepTogether = false;
            newSectionFormat.EnableSuppress = false;
            newSectionFormat.EnableUnderlaySection = true;
            reportSectionController.SetProperty(sectionToAddTo, CrReportSectionPropertyEnum.crReportSectionPropertyFormat, newSectionFormat);
            reportDef.ReportObjectController.ImportPicture(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE/AccessReportWatermarks1.png", sectionToAddTo, 500, 700);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintPreview_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
