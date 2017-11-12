using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iMANTRA
{
    public class SetFieldsValue
    {
        private string _fld_tbl_nm, _fld_nm, _fld_value;

        public string Fld_tbl_nm
        {
            get { return _fld_tbl_nm; }
            set { _fld_tbl_nm = value; }
        }

        public string Fld_nm
        {
            get { return _fld_nm; }
            set { _fld_nm = value; }
        }

        public string Fld_value
        {
            get { return _fld_value; }
            set { _fld_value = value; }
        }
    }
}
