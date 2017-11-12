using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public partial class frmstkstatus : CustomBaseForm
    {
        public frmstkstatus()
        {
            InitializeComponent();
        }
        private Hashtable _hashstkvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private dblayer objdblayer = new dblayer();
        private Keys KeyData;

        public Keys KeyData1
        {
            get { return KeyData; }
            set { KeyData = value; }
        }
        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public Hashtable Hashstkvalue
        {
            get { return _hashstkvalue; }
            set { _hashstkvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public void frmstkstatus_Load(object sender, EventArgs e)
        {
            if (objBLFD.ObjControlSet.neg_stk != null ? !bool.Parse(objBLFD.ObjControlSet.neg_stk) : true)
            {
                if (ACTIVE_BL.Code == "LI")
                {
                    string item_nm = _hashstkvalue["PROD_NM"].ToString();
                    decimal total_qty = 0, assigned_qty = 0, original_qty = 0;
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        original_qty = 0;
                        if (((Hashtable)entry.Value).Count != 0 && item_nm == ((Hashtable)entry.Value)["PROD_NM"].ToString())
                        {
                            if (((Hashtable)entry.Value)["QTY"] != null && ((Hashtable)entry.Value)["QTY"].ToString() != "")
                                total_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                            if (_hashstkvalue["PTSERIAL"].ToString() != entry.Key.ToString())
                            {
                                DataSet dsetQty = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashstkvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + entry.Key.ToString() + "'");
                                if (dsetQty != null && dsetQty.Tables[0].Rows.Count != 0)
                                {
                                    original_qty = decimal.Parse(dsetQty.Tables[0].Rows[0]["qty"].ToString());
                                }
                                if (original_qty != decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString()))
                                {
                                    assigned_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                                }
                            }
                        }
                    }
                    original_qty = 0;
                    DataSet dsetQty1 = objdblayer.dsquery("select qty from " + ACTIVE_BL.Item_tbl_nm + " where tran_id='" + ACTIVE_BL.Tran_id + "' and tran_cd='" + ACTIVE_BL.Code + "' and prod_nm='" + _hashstkvalue["PROD_NM"].ToString().Replace("'","''") + "' and ptserial='" + _hashstkvalue["PTSERIAL"].ToString() + "'");
                    if (dsetQty1 != null && dsetQty1.Tables[0].Rows.Count != 0)
                    {
                        original_qty = decimal.Parse(dsetQty1.Tables[0].Rows[0]["qty"].ToString());
                    }
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@Item", _hashstkvalue["PROD_NM"].ToString());
                    htparam.Add("@date", _hashstkvalue["TRAN_DT"].ToString());
                    htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                    htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());
                    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS", htparam);
                    if (dsetStock != null && dsetStock.Tables[0].Rows.Count != 0)
                    {
                        lblprod_nm.Text = dsetStock.Tables[0].Rows[0]["PROD_NM"].ToString();
                        lblstkqty.Text = (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) + original_qty - assigned_qty).ToString();

                    }
                    else
                    {
                        lblprod_nm.Text = _hashstkvalue["PROD_NM"].ToString();
                        lblstkqty.Text = "0.00";
                    }
                }
                else if (ACTIVE_BL.Code == "CE" || ACTIVE_BL.Code == "PP")
                {
                    string item_nm = _hashstkvalue["PROD_NM"].ToString();
                    decimal total_qty = 0;
                    foreach (DictionaryEntry entry in objBLFD.HTITEM)
                    {
                        if (((Hashtable)entry.Value).Count != 0 && item_nm == ((Hashtable)entry.Value)["PROD_NM"].ToString())
                        {
                            if (((Hashtable)entry.Value)["QTY"] != null && ((Hashtable)entry.Value)["QTY"].ToString() != "")
                                total_qty += decimal.Parse(((Hashtable)entry.Value)["QTY"].ToString());
                        }
                    }
                    Hashtable htparam = new Hashtable();
                    htparam.Add("@Item", _hashstkvalue["PROD_NM"].ToString());
                    htparam.Add("@date", _hashstkvalue["TRAN_DT"].ToString());
                    htparam.Add("@TRAN_ID", objBLFD.Tran_id);
                    htparam.Add("@RULE", objBLFD.IsRule ? objBLFD.HTMAIN["rule"].ToString() : "");
                    htparam.Add("@acompid", objBLFD.ObjCompany.Compid.ToString());

                    DataSet dsetStock = objdblayer.dsprocedure("ISP_STOCK_STATUS_OPENING", htparam);
                    if (dsetStock != null && dsetStock.Tables[0].Rows.Count != 0)
                    {
                        lblprod_nm.Text = dsetStock.Tables[0].Rows[0]["PROD_NM"].ToString().Replace("'","''") + "(" + dsetStock.Tables[0].Rows[0]["rule"].ToString() + ")";
                        lblstkqty.Text = (decimal.Parse(dsetStock.Tables[0].Rows[0]["STOCK"].ToString()) - total_qty + decimal.Parse(_hashstkvalue["qty"].ToString())).ToString();
                    }
                    else
                    {
                        lblprod_nm.Text = _hashstkvalue["PROD_NM"].ToString().Replace("'","''") + "(" + objBLFD.HTMAIN["rule"].ToString() + ")";
                        lblstkqty.Text = "0.00";
                    }
                }
            }
            AddThemesToTitleBar((Form)this, ucToolBar1, ACTIVE_BL, "CustomMaster");
            ucToolBar1.Titlebar = "Stock";
        }
        public void tmrstk_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
