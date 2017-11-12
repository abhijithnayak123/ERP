using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace iMANTRA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new frmTransaction());
            //Application.Run(new frmMain());
            //Application.Run(new frmusercontrol());
            //Application.Run(new ifrm_transaction());
            //Application.Run(new frm_mainmenu());
            // Application.Run(new frmTab());
            // Application.Run(new frmParent());
            //  Application.Run(new frm_mast_item());
            // Application.Run(new frmLogin());
             Application.Run(new frmStartUpPage());
           // Application.Run(new frmGenerateXML());
            // Application.Run(new frmTestRibbon());
        }
    }
}
