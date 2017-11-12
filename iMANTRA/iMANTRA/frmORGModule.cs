using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;

namespace iMANTRA
{
    public partial class frmORGModule : BaseClass
    {
        string strModuleCode = "";

        private BL_BASEFIELD objBSFD = new BL_BASEFIELD();

        public BL_BASEFIELD ObjBSFD
        {
            get { return objBSFD; }
            set { objBSFD = value; }
        }

        public frmORGModule()
        {
            InitializeComponent();
        }

        private void frmORGModule_Load(object sender, EventArgs e)
        {
            foreach (Control con1 in this.Controls)
            {
                if (!(con1 is UCToolBar))
                {
                    foreach (Control c in con1.Controls)
                    {
                        if (objBSFD.Tran_mode != "add_mode")
                        {
                            if (!(c is Label)) c.Enabled = false;
                        }
                        else
                        {
                            c.Enabled = true;
                        }
                    }
                }
            }
            if (objBSFD.HTMAIN.Contains("module_cd") && objBSFD.HTMAIN["module_cd"] != null && objBSFD.HTMAIN["module_cd"].ToString() != "")
            {
                string[] str = objBSFD.HTMAIN["module_cd"].ToString().Split(new string[] { "+$IPTLM$+" }, StringSplitOptions.RemoveEmptyEntries);
                string[] strDecrypt = new string[str.Length];
                int i = 0;
                if (str.Length != 0)
                {
                    foreach (string s in str)
                    {
                        strDecrypt[i] = VALIDATIONLAYER.Decrypt(s);
                        i++;
                    }
                    foreach (string st in strDecrypt)
                    {
                        if (st == "BSIC") { rbtnBasic.Checked = true; }
                        if (st == "BMFG") { rbtnExciseMfg.Checked = true; }
                        if (st == "BTRD") { rbtnExciseTrd.Checked = true; }
                        if (st == "MKTG") { chkMarketing.Checked = true; }
                        if (st == "PRCM") { chkProcurement.Checked = true; }
                        if (st == "INVT") { chkInventory.Checked = true; }
                        if (st == "PROD") { chkProduction.Checked = true; }
                        if (st == "PLAN") { chkPlanning.Checked = true; }
                        if (st == "QUAL") { chkQuality.Checked = true; }
                        if (st == "JOBW") { chkJobWork.Checked = true; }
                        if (st == "WRHS") { chkWarehouse.Checked = true; }
                        if (st == "GRAP") { chkGraphics.Checked = true; }
                    }
                }
            }
            else
            {
                rbtnBasic.Checked = true;
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, objBSFD, "CustomMaster");
            ucToolBar1.Titlebar = "Module Selection";
        }

        private void rbtnBasic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBasic.Checked)
            {
                chkInventory.Checked = false;
                chkProduction.Checked = false;
                //  chkQuality.Checked = false;
                chkPlanning.Checked = false;
                chkInventory.Enabled = false;
                chkProduction.Enabled = false;
                //  chkQuality.Enabled = false;
                chkPlanning.Enabled = false;
            }
            else
            {
                chkInventory.Enabled = true;
                chkProduction.Enabled = true;
                //  chkQuality.Enabled = true;
                chkPlanning.Enabled = true;
            }
        }

        private void rbtnExciseMfg_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExciseMfg.Checked)
            {
                chkJobWork.Checked = false;
                chkJobWork.Enabled = false;
            }
            else
            {
                chkJobWork.Enabled = true;
            }
        }

        private void rbtnExciseTrd_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnExciseTrd.Checked)
            {
                chkInventory.Checked = false;
                chkProduction.Checked = false;
                // chkQuality.Checked = false;
                chkPlanning.Checked = false;
                chkJobWork.Checked = false;
                chkInventory.Enabled = false;
                chkProduction.Enabled = false;
                // chkQuality.Enabled = false;
                chkPlanning.Enabled = false;
                chkJobWork.Enabled = false;
            }
            else
            {
                chkInventory.Enabled = true;
                chkProduction.Enabled = true;
                //   chkQuality.Enabled = true;
                chkPlanning.Enabled = true;
                chkJobWork.Enabled = true;
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            bool flg = true;
            if (rbtnBasic.Checked)
            {
                if (chkInventory.Checked || chkProduction.Checked || chkPlanning.Checked)
                {
                    MessageBox.Show("Sorry! checking Inventory,Production & Planning is not allowed.");
                    flg = false;
                }
            }
            else if (rbtnExciseMfg.Checked)
            {
                if (chkJobWork.Checked)
                {
                    MessageBox.Show("Sorry! checking Job Work is not allowed.");
                    flg = false;
                }
                else
                {
                    if (!((chkInventory.Checked && chkProduction.Checked) || (!chkInventory.Checked && !chkProduction.Checked)))
                    {
                        MessageBox.Show("Sorry! checking either Inventory or Production is not allowed.");
                        flg = false;
                    }
                    if (chkPlanning.Checked)
                    {
                        if (!chkProduction.Checked)
                        {
                            MessageBox.Show("Sorry! checking Planning is not possible untill Production is not checked.");
                            flg = false;
                        }
                    }
                }
            }
            else
            {
                if (chkInventory.Checked || chkProduction.Checked || chkPlanning.Checked || chkJobWork.Checked)
                {
                    MessageBox.Show("Sorry! checking Inventory,Production,Planning & Job Work is not allowed.");
                    flg = false;
                }
            }
            if (flg)
            {
                strModuleCode = "";
                if (rbtnBasic.Checked)
                {
                    strModuleCode = strModuleCode != "" ? strModuleCode + "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("BSIC") : VALIDATIONLAYER.Encrypt("BSIC");
                }
                else if (rbtnExciseMfg.Checked)
                {
                    strModuleCode = strModuleCode != "" ? strModuleCode + "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("BMFG") : VALIDATIONLAYER.Encrypt("BMFG");
                }
                else
                {
                    strModuleCode = strModuleCode != "" ? strModuleCode + "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("BTRD") : VALIDATIONLAYER.Encrypt("BTRD");
                }
                if (chkMarketing.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("MKTG");
                }
                if (chkProcurement.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("PRCM");
                }
                if (chkInventory.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("INVT");
                }
                if (chkProduction.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("PROD");
                }
                if (chkPlanning.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("PLAN");
                }
                if (chkQuality.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("QUAL");
                }
                if (chkJobWork.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("JOBW");
                }
                if (chkWarehouse.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("WRHS");
                }
                if (chkGraphics.Checked)
                {
                    strModuleCode += "+$IPTLM$+" + VALIDATIONLAYER.Encrypt("GRAP");
                }
                objBSFD.HTMAIN["module_cd"] = strModuleCode;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
