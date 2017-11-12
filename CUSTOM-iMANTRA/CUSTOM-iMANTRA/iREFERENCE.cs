using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iREFERENCE
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        private Hashtable _hashvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private dblayer objdblayer = new dblayer();
      //  private iInit objCustomInit = new iInit();

        public Hashtable Hashvalue
        {
            get { return _hashvalue; }
            set { _hashvalue = value; }
        }
        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }

        public BL_BASEFIELD ACTIVE_REFERENCE
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public bool ValidateReference()
        {
            if (ACTIVE_REFERENCE.HTMAIN.Contains("AC_NM") && ACTIVE_REFERENCE.HTMAIN.Contains("CONS_NM"))
            {
                ACTIVE_REFERENCE.HTMAIN["CONS_NM"] = ACTIVE_REFERENCE.HTMAIN["AC_NM"];
            }
            if (ACTIVE_REFERENCE.Code == "GR")
            {
                foreach (DictionaryEntry entry in ACTIVE_REFERENCE.Hashitemref)
                {
                    if (ACTIVE_REFERENCE.HTMAIN.Contains("PO_NO") && ACTIVE_REFERENCE.HTMAIN.Contains("PO_DT"))
                    {
                        ACTIVE_REFERENCE.HTMAIN["PO_NO"] = ((Hashtable)entry.Value)["REF_TRAN_NO"].ToString();
                        ACTIVE_REFERENCE.HTMAIN["PO_DT"] = DateTime.Parse(((Hashtable)entry.Value)["TRAN_DT"].ToString()).ToString("yyyy/MM/dd");
                    }
                }
            }
            if (ACTIVE_REFERENCE.Code == "PE")
            {
                Hashtable htparam = new Hashtable();
                DataSet ds = new DataSet();
                foreach (DictionaryEntry entry in ACTIVE_REFERENCE.HTMAINREF)
                {
                    ds = objdblayer.dsquery("select bill_no,bill_dt from pemain where tran_cd='" + ((Hashtable)entry.Value)["REF_TRAN_CD"].ToString() + "' and tran_id='" + ((Hashtable)entry.Value)["REF_TRAN_ID"].ToString() + "'");
                    break;
                }
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    ACTIVE_REFERENCE.HTMAIN["BILL_NO"] = (ds.Tables[0].Rows[0]["BILL_NO"].ToString());
                    ACTIVE_REFERENCE.HTMAIN["BILL_DT"] = (ds.Tables[0].Rows[0]["BILL_DT"].ToString());
                }
            }
            return true;
        }
    }
}
