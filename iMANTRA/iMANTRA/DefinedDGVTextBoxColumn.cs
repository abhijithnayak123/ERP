using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iMANTRA
{
    class DefinedDGVTextBoxColumn: DataGridViewTextBoxColumn
    {
        private bool _copyTextBoxtoParent;

        public bool CopyTextBoxtoParent
        {
            get { return _copyTextBoxtoParent; }
            set { _copyTextBoxtoParent = value; }
        }

    }
}
