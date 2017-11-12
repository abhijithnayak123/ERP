using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iMANTRA_IL;
using iMANTRA_BL;
using iMANTRA_iniL;

namespace iMANTRA
{
    public partial class frmStartUpPage : BaseClass
    {

        FL_MAST objFLMast = new FL_MAST();
        FL_TRANSACTION objFLTransaction = new FL_TRANSACTION();
        VALIDATIONLAYER objVALIDATION = new VALIDATIONLAYER();
        Ini objIni = new Ini();

        public frmStartUpPage()
        {
            InitializeComponent();
        }
        private void frmStartUpPage_Load(object sender, EventArgs e)
        {
            objIni.SetKeyFieldValue("APP_PATH", "path", Path.GetDirectoryName(Application.ExecutablePath));
            objIni.SetKeyFieldValue("APP_PATH", "bakuppath", Path.GetDirectoryName(Application.ExecutablePath) + "\\BAKUP");
            objIni.SetKeyFieldValue("SQL", "initial catalog", "master");
            if (!objVALIDATION.Find_DB_Existance())
            {
                this.Hide();
                if (objVALIDATION.Create_DataBase("InodeMFG", "InodeMFG"))
                {
                    frm_mainmenu objmain = new frm_mainmenu();
                    objmain.ObjBLMainFields.Login_type = "notlogin";
                    objmain.ObjBLMainFields.CurUser = "admin";
                    objmain.ObjBLMainFields.Fin_yr = "";
                    objmain.ObjBLMainFields.Comp_nm = "0";
                    objmain.ShowDialog();
                    this.Close();
                }
                else
                {
                    AutoClosingMessageBox.Show("Creating InodeMFG is not Successfull","Error",3000);
                }
            }
            else
            {
                this.Hide();
                frmLogin objLogin = new frmLogin();
                objLogin.ShowDialog();
                this.Close();
            }
            this.BackColor = Color.White;this.ForeColor = Color.Black;ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width;
            ucToolBar1.Titlebar = "Start Up Form";
        }

        private void frmStartUpPage_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
