using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iMANTRA
{
    public partial class frmAbout : BaseClass
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Gainsboro; this.ForeColor = Color.Black; ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            ucToolBar1.UCbackcolor = Color.SlateGray;
            ucToolBar1.Width1 = this.Width;
            ucToolBar1.Titlebar = "About";
        }

        private void frmAbout_Resize(object sender, EventArgs e)
        {
            ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
