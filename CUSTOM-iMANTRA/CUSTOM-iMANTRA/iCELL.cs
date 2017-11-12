using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using iMANTRA_BL;
using System.Data;

namespace CUSTOM_iMANTRA
{
    public class iCELL
    {
        private Hashtable _hashgridItemvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        dblayer objdblayer = new dblayer();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }

        public Hashtable HashgridItemvalue
        {
            get { return _hashgridItemvalue; }
            set { _hashgridItemvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public bool ValidateCell()
        {
             if (objBLFD.Code == "PO")
            {
                if (objBLFD.HTITEM != null && objBLFD.HTITEM.Count != 0)
                {
                    if (objBLFD.HTMAIN.Contains("discount_per") && objBLFD.HTMAIN["discount_per"] != null && objBLFD.HTMAIN["discount_per"].ToString() != "" && objBLFD.HTMAIN["discount_per"].ToString() != "0" && objBLFD.HTMAIN["discount_per"].ToString() != "0.00")
                    {
                        if (objBLFD.HTMAIN.Contains("discount"))
                        {
                            objBLFD.HTMAIN["discount"] = Convert.ToDecimal(objBLFD.HTMAIN["discount_per"].ToString()) * Convert.ToDecimal(objBLFD.HTMAIN["tot_item"].ToString()) / 100;
                        }
                    }
                }
            }
            return true;
        }
    }
}
