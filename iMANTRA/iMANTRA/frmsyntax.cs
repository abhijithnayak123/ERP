using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;
using System.Collections;

namespace iMANTRA
{
    public partial class frmsyntax : BaseClass
    {
        string _con_nm;
        Hashtable _htLocal = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HtLocal
        {
            get { return _htLocal; }
            set { _htLocal = value; }
        }

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBLFD
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public frmsyntax(string con_nm,Hashtable _ht)
        {
            InitializeComponent();
            _con_nm = con_nm;
            _htLocal = _ht;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            HtLocal[_con_nm] = txtsyntax.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmsyntax_Load(object sender, EventArgs e)
        {
            txtsyntax.Text = HtLocal[_con_nm] != null ? HtLocal[_con_nm].ToString() : "";
            //this.BackColor = ObjBLFD.ObjControlSet.Back_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Back_color) : Color.White;
            //this.ForeColor = ObjBLFD.ObjControlSet.Font_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Font_color) : Color.Black; 
            //ucToolBar1.Width = this.Width;this.ucToolBar1.Maximize = false;
           ucToolBar1.Width1 = this.Width; ucToolBar1.UCbackcolor = ObjBLFD.ObjControlSet.Uc_color != null ? Color.FromName(ObjBLFD.ObjControlSet.Uc_color) : Color.Maroon;
            //this.Font = new Font(ObjBLFD.ObjControlSet.Font_family != null ? ObjBLFD.ObjControlSet.Font_family : "Courier New", float.Parse(ObjBLFD.ObjControlSet.Font_size != null ? ObjBLFD.ObjControlSet.Font_size : "9"));
            //ucToolBar1.Titlebar = "Syntax";
            AddThemesToTitleBar((Form)this, ucToolBar1, objBLFD, "CustomMaster");
            ucToolBar1.Titlebar = "Syntax";
        }

        private void frmsyntax_Resize(object sender, EventArgs e)
        {
           ShowTextInMinize((Form)this,ucToolBar1);
        }
    }
}
