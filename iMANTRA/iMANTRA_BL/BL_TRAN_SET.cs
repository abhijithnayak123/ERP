using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace iMANTRA_BL
{
    public class BL_TRAN_SET
    {
        private Hashtable _HT_TRAN_TBLS = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HT_TRAN_TBLS
        {
            get { return _HT_TRAN_TBLS; }
            set { _HT_TRAN_TBLS = value; }
        }
    }
}
