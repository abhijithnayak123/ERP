using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iMANTRA
{
    public class POPUPTEXTBOX_FOR_GRID: DataGridViewTextBoxColumn
    {
        public TextBox pText = null;
        public string frmName = "";
        public string PTextName = "";
        private string _tbl_nm = "";
        private string _reftbltran_cd = "";
        private string _query_con = "";
        private bool _isQcd = false;        
        private string _QcdCondition="";

        public bool IsQcd
        {
            get { return _isQcd; }
            set { _isQcd = value; }
        }
        public string QcdCondition
        {
            get { return _QcdCondition; }
            set { _QcdCondition = value; }
        }


        public string Query_con
        {
            get { return _query_con; }
            set { _query_con = value; }
        }

        public string Tbl_nm
        {
            get { return _tbl_nm; }
            set { _tbl_nm = value; }
        }
        private string _primaryddl = "";

        public string Primaryddl
        {
            get { return _primaryddl; }
            set { _primaryddl = value; }
        }
        private string _dispddlfields = "";

        public string Dispddlfields
        {
            get { return _dispddlfields; }
            set { _dispddlfields = value; }
        }
        public string Reftbltran_cd
        {
            get { return _reftbltran_cd; }
            set { _reftbltran_cd = value; }
        }
    }
}
