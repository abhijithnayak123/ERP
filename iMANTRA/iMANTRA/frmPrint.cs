using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer.ClientDoc;
using CrystalDecisions.ReportAppServer.Controllers;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using System.Collections;
using System.Reflection;
using iMANTRA_BL;
using iMANTRA_IL;
using iMANTRA_DL;
using iMANTRA_iniL;
using CUSTOM_iMANTRA;
using License;

namespace iMANTRA
{
    public partial class frmPrint : BaseClass
    {
        /*  Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.12.13 07.00PM
            * 1.0 insert text box to report page header & show text in under details section.
            * 2.0 on 11.18.13 added report name in title bar
            * 
            * */

        BL_BASEFIELD _objBLFD = new BL_BASEFIELD();
        CrystalDecisions.CrystalReports.Engine.ReportDocument doc;

        public BL_BASEFIELD ObjBLFD
        {
            get { return _objBLFD; }
            set { _objBLFD = value; }
        }

        LI_License objLicense = new LI_License();

        // FL_TRANSACTION  = new FL_TRANSACTION();
        FL_REP_MAST objFL = new FL_REP_MAST();
        Ini objIni = new Ini();

        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

        public string tran_id;
        public string tran_cd;
        public string rep_nm;
        public string sp_name;
        public string rep_nmshow = "Report";
        bool flgDocorReport = false;
        public Hashtable HTFilter = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        public Hashtable HTREPORT = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        iREPORT objiREPORT = new iREPORT();

        public frmPrint(bool flgDocorRep)
        {
            InitializeComponent();
            flgDocorReport = flgDocorRep;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = true;
                pictureBox1.Width = this.Width;
                pictureBox1.BackColor = this.BackColor;
                pictureBox1.Bounds = new Rectangle(this.Width * 25 / 100, this.Height * 25 / 100, this.Width / 2, this.Height * 1 / 20);

                objFL.objCompany = ObjBLFD.ObjCompany;
                bool validflg = true;

                objiREPORT.HTREPORT = HTFilter;
                objiREPORT.ACTIVE_BL = ObjBLFD;
                MethodInfo methodInfo = typeof(iREPORT).GetMethod("ReportPreview");
                validflg = bool.Parse(methodInfo.Invoke(objiREPORT, null).ToString().Trim());
                if (validflg)
                {
                    HTFilter = objiREPORT.HTREPORT;
                }

                if (flgDocorReport)
                {
                    DataSet ds = objFL.REPORT_TRANSACTION(tran_id, tran_cd, sp_name);
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
                            foreach (ParameterFieldDefinition def in crParameterdef)
                            {
                                if (def.Name.Equals("approval"))    // check if parameter exists in report
                                {
                                    doc.SetParameterValue("approval", strApproval);    // set the parameter value in the report
                                }
                            }
                        }
                        else
                        {
                            foreach (ParameterFieldDefinition def in crParameterdef)
                            {
                                if (def.Name.Equals("approval"))    // check if parameter exists in report
                                {
                                    doc.SetParameterValue("approval", "");    // set the parameter value in the report
                                }
                            }
                        }
                        crystalReportViewer1.ReportSource = doc;
                        //crystalReportViewer1.RefreshReport();
                        //  this.TopMost = true;
                        crystalReportViewer1.Visible = true;
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("error in report", "Error");
                    }
                }
                else
                {
                    DataSet ds = objFL.REPORT_SHOW(this.HTFilter, ObjBLFD.ObjCompany.Compid.ToString());
                    if (ds != null)
                    {
                        CrystalDecisions.CrystalReports.Engine.ReportDocument doc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        doc.Load(AppDomain.CurrentDomain.BaseDirectory + objIni.GetKeyFieldValue("SQL", "initial catalog") + @"\" + rep_nm + ".rpt");
                        doc.SetDataSource(ds.Tables[0]);
                        //  if (objLicense.License_code == "sZLKa7BcYsepmYbILRVBlg==")
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
                        crystalReportViewer1.ReportSource = doc;


                        //crystalReportViewer1.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\setting.jpg");
                        //crystalReportViewer1.BackgroundImageLayout = ImageLayout.Stretch;
                        //crystalReportViewer1.DisplayBackgroundEdge = true;
                        crystalReportViewer1.Visible = true;
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("error in report", "Error");
                    }
                }
                ucToolBar1.Width1 = this.Width;
                AddThemesToTitleBar((Form)this, ucToolBar1, ObjBLFD, "Report");
                #region 2.0
                if (HTFilter != null && HTFilter.Count != 0)
                {
                    if (HTFilter.Contains("rep_gr"))
                    {
                        ucToolBar1.Titlebar = HTFilter["rep_gr"].ToString() + " (" + rep_nm + ".rpt)";
                    }
                    else
                    {
                        ucToolBar1.Titlebar = HTFilter["rep_nm"].ToString() + " (" + rep_nm + ".rpt)";
                    }
                }
                else
                {
                    ucToolBar1.Titlebar = rep_nmshow + " (" + rep_nm + ".rpt)";
                }
                #endregion 2.0
                this.TopMost = true;
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Fields not Exist", "Error");
                this.TopMost = false;
                pictureBox1.Visible = false;
            }
            pictureBox1.Visible = false;
            //this.TopMost = false;
        }

        #region 1.0
        private void Report_Modification(CrystalDecisions.CrystalReports.Engine.ReportDocument cryRpt)
        {
            CrystalDecisions.ReportAppServer.ClientDoc.ISCDReportClientDocument rptClientDoc1 = cryRpt.ReportClientDocument;

            //ISCRParagraphTextElement paragraphTextElement = new ParagraphTextElementClass();
            //paragraphTextElement.Text = "DEMO VERSION";
            //paragraphTextElement.Kind = CrParagraphElementKindEnum.crParagraphElementKindText;

            //ParagraphElements paragraphElements = new ParagraphElementsClass();
            //paragraphElements.Add(paragraphTextElement);

            //Paragraph paragraph = new ParagraphClass();
            //paragraph.Alignment = CrAlignmentEnum.crAlignmentHorizontalCenter;
            //paragraph.ParagraphElements = paragraphElements;

            //Paragraphs paragraphs = new ParagraphsClass();
            //paragraphs.Add(paragraph);

            //ISCRTextObject textObject = new TextObjectClass();
            //textObject.Paragraphs = paragraphs;
            //textObject.FontColor.Font.Bold = true;
            //textObject.FontColor.Font.Size = 48;
            //textObject.FontColor.Font.Italic = true;
            //textObject.FontColor.Color = uint.Parse(ColorTranslator.ToOle(Color.Gray).ToString());
            //textObject.FontColor.Font.Charset = 30;
            //textObject.Height = 8000;
            //textObject.Top = this.Height;
            //textObject.Left = 5000;
            //textObject.Width = 1500;

            //textObject.Format.TextRotationAngle = CrTextRotationAngleEnum.crRotationAngleRotate90;

            //ISCRPictureObject imgObject = new PictureObjectClass();
            //imgObject.Kind = CrReportObjectKindEnum.crReportObjectKindPicture;



            //textObject.Format.HorizontalAlignment = CrAlignmentEnum.crAlignmentDecimal;

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
            
            //newsec.Name = "Z";
            //ISCRArea ar = reportDef.ReportDefinition.PageHeaderArea;
            //ar.Name = "PH";
            //reportSectionController.Add(newsec, ar, index - 1);

            CrystalDecisions.ReportAppServer.ReportDefModel.Section sectionToAddTo = reportDef.ReportDefinition.PageHeaderArea.Sections[index];
            
            CrystalDecisions.ReportAppServer.ReportDefModel.SectionFormat newSectionFormat = sectionToAddTo.Format;
            newSectionFormat.EnableKeepTogether = false;
            newSectionFormat.EnableSuppress = false;
            newSectionFormat.EnableUnderlaySection = true;           
            reportSectionController.SetProperty(sectionToAddTo, CrReportSectionPropertyEnum.crReportSectionPropertyFormat, newSectionFormat);
            reportDef.ReportObjectController.ImportPicture(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE/AccessReportWatermarks1.png", sectionToAddTo, 500, 700);


            //reportDef.ReportObjectController.Add(textObject, sectionToAddTo, 0);
            //reportDef.ReportObjectController.Add(imgObject, sectionToAddTo, 0);

            //ReportObjectControllerClass objPictureBox = new ReportObjectControllerClass();
            //objPictureBox.ImportPicture(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE/AccessReportWatermarks1.png", sectionToAddTo, 20, 20);
            //rptClientDoc1.ReportDefController = reportDef;
            //cryRpt.ReportClientDocument = rptClientDoc1;
        }
        #endregion 1.0

        private void frmPrint_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
