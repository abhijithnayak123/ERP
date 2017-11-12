using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using iMANTRA_IL;
using iMANTRA_DL;
using iMANTRA_iniL;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;


namespace iMANTRA
{
    public partial class frmBakUp : BaseClass
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private string tran_cd, tran_mode, tran_id;
        Ini objIni = new Ini();
        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();

        DL_ADAPTER objDL_ADAPTER = new DL_ADAPTER();

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

        public frmBakUp(BL_BASEFIELD objBL)
        {
            InitializeComponent(); this.Tran_cd = objBL.Code;
            objBASEFILEDS.HTMAIN["TRAN_CD"] = this.tran_cd;
            this.objBASEFILEDS = objBL;
        }

        private void frmBakUp_Load(object sender, EventArgs e)
        {
            lblWait.Visible = false;
            AddThemesToTitleBar((Form)this, ucToolBar1, objBASEFILEDS, "CustomMaster");
            ucToolBar1.Titlebar = "APPLICATION BACKUP";
            lblOld.Text = "Current BackUp Path : " + objIni.GetKeyFieldValue("APP_PATH", "bakuppath");
        }

        private void btnHeaderNm_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog objOpenDia = new FolderBrowserDialog();
            objOpenDia.SelectedPath = objIni.GetKeyFieldValue("APP_PATH", "path");
            DialogResult res = objOpenDia.ShowDialog();
            if (res == DialogResult.OK)
            {
                txtPath.Text = objOpenDia.SelectedPath;
            }
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            
            //pictureBox1.Visible = true;
            //pictureBox1.Width = this.Width;
            //pictureBox1.BackColor = this.BackColor;
            //pictureBox1.Bounds = new Rectangle(this.Width * 25 / 100, this.Height * 25 / 100, this.Width / 2, this.Height * 1 / 20);
            try
            {
                lblWait.Visible = true;
                string path = "";
                if (txtPath.Text == "")
                {
                    path = objIni.GetKeyFieldValue("APP_PATH", "bakuppath");
                }
                else
                {
                    path = txtPath.Text;
                }
                if (path != null)
                {
                    bool flg = objVALIDATION.Create_BakUp(objBASEFILEDS.ObjCompany.Db_nm, path);
                    if (!flg)
                    {
                        AutoClosingMessageBox.Show("Not Successfully created bakup","Validation",3000);
                    }
                    else
                    {
                        string strDbServer_nm = objIni.GetKeyFieldValue("SQL", "initial catalog");
                        objIni.SetKeyFieldValue("SQL", "initial catalog", "InodeMFG");
                        string strUpdate = "Update ORG_MAST set frm_bakup='" + path + "',Bkup_dt='" + DateTime.Now.ToString("yyyy/MM/dd") + "' where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'";
                        objDL_ADAPTER.execUpdateQuery(strUpdate);

                        objIni.SetKeyFieldValue("APP_PATH", "bakuppath", path);
                        objIni.SetKeyFieldValue("SQL", "initial catalog", strDbServer_nm);

                        using (ZipFile zip = new ZipFile())
                        {
                            //foreach (var file in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory + objBASEFILEDS.ObjCompany.Db_nm))
                            //    zip.AddFile(file);
                            zip.AddDirectory(AppDomain.CurrentDomain.BaseDirectory + objBASEFILEDS.ObjCompany.Db_nm);
                            //zip.Save(path+objBASEFILEDS.ObjCompany.Db_nm + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");
                            zip.ParallelDeflateThreshold = -1;

                            zip.Save(Path.Combine(path, String.Format(objBASEFILEDS.ObjCompany.Db_nm + "{0}.zip", DateTime.Now.ToString("yyyyMMddHHmmss"))));
                            // zip.Save(path + @"\" + objBASEFILEDS.ObjCompany.Db_nm + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip");
                        }
                        lblWait.Visible = false;
                        AutoClosingMessageBox.Show("Successfully created bakup in " + path,"Bak Up",3000);
                    }
                }
                else
                {
                    AutoClosingMessageBox.Show("Sorry! Invalid Back Up Path ","Error",3000);
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show(ex.Message,"Exception",3000);
            }
        
            //pictureBox1.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBakUp_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this, ucToolBar1);
        }
    }
}
