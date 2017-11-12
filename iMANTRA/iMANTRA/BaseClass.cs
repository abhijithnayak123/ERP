using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using iMANTRA_BL;

namespace iMANTRA
{
    public class BaseClass : Form
    {
        public virtual bool SendMessageToClient(BL_BASEFIELD objBFL, string msg)
        {
            return true;
        }
        public void AddThemesToTitleBar(Form objform, UCToolBar ucToolBar1, BL_BASEFIELD objBASEFILEDS, string strTranType)
        {
            objform.BackColor = objBASEFILEDS.ObjControlSet.Back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Back_color) : Color.White;
            objform.ForeColor = objBASEFILEDS.ObjControlSet.Font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Font_color) : Color.Black;
            switch (strTranType)
            {
                case "Master": objform.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "12")); break;
                case "Transaction": objform.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "11.25")); break;
                case "CustomMaster": objform.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "9")); break;
                case "Report": objform.Font = new Font(objBASEFILEDS.ObjControlSet.Font_family != null ? objBASEFILEDS.ObjControlSet.Font_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Font_size != null ? objBASEFILEDS.ObjControlSet.Font_size : "9")); break;
            }

            ucToolBar1.Width = objform.Width;
            ucToolBar1.Width1 = objform.Width;
            ucToolBar1.Form_width = objform.Width;
            ucToolBar1.Form_height = objform.Height;
            ucToolBar1.Maximize = false;
            ucToolBar1.UCbackcolor = objBASEFILEDS.ObjControlSet.Uc_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Uc_color) : Color.Maroon;
            ucToolBar1.Titlebar = objBASEFILEDS.Tran_nm;// +"      " + (objBASEFILEDS.Stk_effect != null && objBASEFILEDS.Stk_effect != "" ? "STOCK EFFECT : '" + objBASEFILEDS.Stk_effect + "'" : "");

            foreach (Control c in objform.Controls)
            {
                if (c is TabControl)
                {
                    foreach (TabPage tp in c.Controls)
                    {
                        tp.ForeColor = objBASEFILEDS.ObjControlSet.Tab_font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color) : Color.Black;
                        tp.BackColor = objBASEFILEDS.ObjControlSet.Tab_back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color) : Color.White;
                        tp.Font = new Font(objBASEFILEDS.ObjControlSet.Tab_family != null ? objBASEFILEDS.ObjControlSet.Tab_family : "Courier New", float.Parse(objBASEFILEDS.ObjControlSet.Tab_size != null ? objBASEFILEDS.ObjControlSet.Tab_size : "9"));
                        foreach (Control c2 in tp.Controls)
                        {
                            if (c2 is DataGridView)
                            {
                                ((DataGridView)c2).BackgroundColor = objBASEFILEDS.ObjControlSet.Grid_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color) : Color.White;
                            }
                        }
                    }
                }
                else if (c is DataGridView)
                {
                    ((DataGridView)c).BackgroundColor = objBASEFILEDS.ObjControlSet.Grid_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color) : Color.White;
                }
                else if (c is Panel)
                {
                    foreach (Control c1 in c.Controls)
                    {
                        if (c1 is TabControl)
                        {
                            foreach (TabPage tp in c1.Controls)
                            {
                                tp.ForeColor = objBASEFILEDS.ObjControlSet.Tab_font_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_font_color) : Color.Black;
                                tp.BackColor = objBASEFILEDS.ObjControlSet.Tab_back_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Tab_back_color) : Color.White;
                                foreach (Control c2 in tp.Controls)
                                {
                                    if (c2 is DataGridView)
                                    {
                                        ((DataGridView)c2).BackgroundColor = objBASEFILEDS.ObjControlSet.Grid_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color) : Color.White;
                                    }
                                }
                            }
                        }
                        else if (c1 is DataGridView)
                        {
                            ((DataGridView)c1).BackgroundColor = objBASEFILEDS.ObjControlSet.Grid_color != null ? Color.FromName(objBASEFILEDS.ObjControlSet.Grid_color) : Color.White;
                        }
                    }
                }
            }
        }

        public void ShowTextInMinize(Form objform, UCToolBar ucToolBar1)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Text = ucToolBar1.Titlebar;
            }
            else
            {
                if (ucToolBar1.Form_width != 0 && ucToolBar1.Minimize)
                {
                    this.Text = "";
                    objform.Width = ucToolBar1.Form_width;
                    objform.Height = ucToolBar1.Form_height;
                    ucToolBar1.Maximize = false;
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseClass
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 265);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BaseClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
