using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CUSTOM_iMANTRA_BL
{
    public class BLHT
    {
        private Hashtable _hashIssueAndReceipt = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashMaintbl = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashItemtbl = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashGeneraltbl = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashFileUpload = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashDeallocateSchedule = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        private Hashtable _hashAccountAlloc = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HashAccountAlloc
        {
            get { return _hashAccountAlloc; }
            set { _hashAccountAlloc = value; }
        }

        public Hashtable HashDeallocateSchedule
        {
            get { return _hashDeallocateSchedule; }
            set { _hashDeallocateSchedule = value; }
        }

        public Hashtable HashFileUpload
        {
            get { return _hashFileUpload; }
            set { _hashFileUpload = value; }
        }

        public Hashtable HashGeneraltbl
        {
            get { return _hashGeneraltbl; }
            set { _hashGeneraltbl = value; }
        }
        
        public Hashtable HashIssueAndReceipt
        {
            get { return _hashIssueAndReceipt; }
            set { _hashIssueAndReceipt = value; }
        }

        public Hashtable HashMaintbl
        {
            get { return _hashMaintbl; }
            set { _hashMaintbl = value; }
        }

        public Hashtable HashItemtbl
        {
            get { return _hashItemtbl; }
            set { _hashItemtbl = value; }
        }
    }
}
