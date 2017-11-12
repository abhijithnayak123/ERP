using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class AddNewItem
    {
        private Hashtable _hashitemvalue = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        dblayer objdblayer = new dblayer();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public Hashtable Hashitemvalue
        {
            get { return _hashitemvalue; }
            set { _hashitemvalue = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public bool AddTransactionItem()
        {
            return true;
        }
    }
}
