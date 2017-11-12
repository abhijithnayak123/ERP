using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_DL;
using iMANTRA_BL;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace iMANTRA
{
    public partial class frmGenerateXML : BaseClass
    {
        BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        private BL_CONTROL_SET objControlSetMaster = new BL_CONTROL_SET();
        DL_ADAPTER objDBAdaper = new DL_ADAPTER();

        DataSet dsest = new DataSet();

        public frmGenerateXML()//BL_BASEFIELD objBSFD)
        {
            InitializeComponent();
            // objBASEFILEDS = objBSFD;
        }

        private void frmGenerateXML_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.ForeColor = Color.Black; ucToolBar1.Width = this.Width; this.ucToolBar1.Maximize = false;
            ucToolBar1.Width1 = this.Width;
            ucToolBar1.Titlebar = "License Generation";
            ucToolBar1.UCbackcolor = Color.Firebrick;
            objControlSetMaster.Back_color = "White";
            objControlSetMaster.Font_color = "Black";
            objControlSetMaster.Uc_color = "Firebrick";
            objControlSetMaster.Tab_back_color = "White";
            objControlSetMaster.Tab_font_color = "Black";

            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject share in searcher.Get())
            {
                txtMotherBoardId.Text = share.Properties["ProcessorId"].Value.ToString();
                txtMacId.Text = VALIDATIONLAYER.identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
                txtSystemNm.Text = share.Properties["SystemName"].Value.ToString();
                txtSystemIp.Text = localIP;
            }
            BindControls();
        }

        private void BindControls()
        {
            dsest = objDBAdaper.dsquery("SELECT DISTINCT * FROM ORG_MAST");// where compid='" + objBASEFILEDS.ObjCompany.Compid.ToString() + "'");
            if (dsest != null && dsest.Tables.Count != 0 && dsest.Tables[0].Rows.Count != 0)
            {
                cmbOrgNm.DataSource = dsest.Tables[0];
                cmbOrgNm.DisplayMember = "comp_nm";
                cmbOrgNm.ValueMember = "compid";
                cmbOrgNm.Update();

                txtModules_nm.Text = dsest.Tables[0].Rows[0]["module_cd"].ToString();
            }
            String[] myArray1 = { "MAIN-COMPANY", "SUB-COMPANY" };
            cmbOrgType.DataSource = myArray1.ToArray();

        }

        private void frmGenerateXML_Enter(object sender, EventArgs e)
        {

        }

        private void frmGenerateXML_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void cmbOrgNm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (dsest != null && dsest.Tables.Count != 0 && dsest.Tables[0].Rows.Count != 0)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(cmbOrgNm.Text.Replace('.', ' ') + "-" + txtMotherBoardId.Text + ".xml", settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Orgnization");

                    foreach (DataRow row in dsest.Tables[0].Rows)
                    {
                        // writer.WriteStartElement("Company");
                        writer.WriteElementString("comp_id", row["CompId"].ToString());
                        writer.WriteElementString("Comp_nm", row["Comp_nm"].ToString());
                        writer.WriteElementString("Add1", row["Add1"] != null ? row["Add1"].ToString() : "");
                        writer.WriteElementString("Add2", row["Add2"] != null ? row["Add2"].ToString() : "");
                        writer.WriteElementString("Add3", row["Add3"] != null ? row["Add3"].ToString() : "");
                        writer.WriteElementString("area_nm", row["area_nm"] != null ? row["area_nm"].ToString() : "");
                        writer.WriteElementString("zone_nm", row["zone_nm"] != null ? row["zone_nm"].ToString() : "");
                        writer.WriteElementString("city_nm", row["city_nm"] != null ? row["city_nm"].ToString() : "");
                        writer.WriteElementString("zip", row["zip"] != null ? row["zip"].ToString() : "");
                        writer.WriteElementString("state_id", row["state_id"] != null ? row["state_id"].ToString() : "100");
                        writer.WriteElementString("country_id", row["country_id"] != null ? row["country_id"].ToString() : "13");
                        writer.WriteElementString("state_nm", row["state_nm"] != null ? row["state_nm"].ToString() : "");
                        writer.WriteElementString("country_nm", row["country_nm"] != null ? row["country_nm"].ToString() : "");
                        writer.WriteElementString("Email", row["Email"] != null ? row["Email"].ToString() : "");
                        writer.WriteElementString("ecc_no", row["ecc_no"] != null ? row["ecc_no"].ToString() : "");
                        writer.WriteElementString("Fin_yr", row["Fin_yr"] != null ? row["Fin_yr"].ToString() : "");
                        writer.WriteElementString("module_cd", row["module_cd"] != null ? row["module_cd"].ToString() : "");
                        string module_nm = "";
                        string[] str = row["module_cd"].ToString().Split(new string[] { "+$IPTLM$+" }, StringSplitOptions.RemoveEmptyEntries);
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
                                if (module_nm == "")
                                {
                                    module_nm = st;
                                }
                                else
                                {
                                    module_nm = module_nm + "," + st;
                                }
                            }
                        }
                        writer.WriteElementString("module_nm", module_nm);
                        writer.WriteElementString("comp_type", cmbOrgType.Text != null ? cmbOrgType.Text : "MAIN-COMPANY");
                        writer.WriteElementString("noofusers", txtNoOfUsers.Text != "" ? txtNoOfUsers.Text : "1");
                        writer.WriteElementString("product", "iMANTRA");
                        writer.WriteElementString("prod_version", "5.0.0");
                        writer.WriteElementString("mac_id", txtMacId.Text != "" ? txtMacId.Text : "1");
                        writer.WriteElementString("server_ip", txtSystemIp.Text != "" ? txtSystemIp.Text : "");
                        writer.WriteElementString("server_nm", txtSystemNm.Text != "" ? txtSystemNm.Text : "");
                        writer.WriteElementString("motherboard_id", txtMotherBoardId.Text != "" ? txtMotherBoardId.Text : "");

                        //writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            AutoClosingMessageBox.Show("Generated License successfully", "Generate License");
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNoOfUsers_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back)))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
