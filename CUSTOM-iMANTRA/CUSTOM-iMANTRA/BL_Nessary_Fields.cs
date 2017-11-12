using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUSTOM_iMANTRA
{
    public class BL_Nessary_Fields
    {
        private string _fld_tbl_nm, _fld_nm, _fld_value, _method_nm, _tran_cd, _errormsg = "", _bind_type;
        private bool _saveDetailsOrNOtFromDefault=true;

        public bool SaveDetailsOrNOtFromDefault
        {
            get { return _saveDetailsOrNOtFromDefault; }
            set { _saveDetailsOrNOtFromDefault = value; }
        }

        public string Bind_type
        {
            get { return _bind_type; }
            set { _bind_type = value; }
        }

        public string Errormsg
        {
            get { return _errormsg; }
            set { _errormsg = value; }
        }

        public string Tran_cd
        {
            get { return _tran_cd; }
            set { _tran_cd = value; }
        }

        public string Method_nm
        {
            get { return _method_nm; }
            set { _method_nm = value; }
        }

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
