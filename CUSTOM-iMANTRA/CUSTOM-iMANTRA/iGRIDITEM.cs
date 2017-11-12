using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using iMANTRA_BL;
using System.Data;

namespace CUSTOM_iMANTRA
{
    public class iGRIDITEM
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
    }
}
