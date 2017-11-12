using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iBeforeReference
    {
        /****************************************************************
         *1.0 Sharanamma Jekeen on 11/25/13 Added Before Pick Up Trigger
         * 
         * 
         * 
         * 
         * 
         * 
         * ****************************************************************/

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private DataSet _dsReference = new DataSet();

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public DataSet DsReference
        {
            get { return _dsReference; }
            set { _dsReference = value; }
        }

        public bool iBeforeReferenceValidation()
        {
            if (objBLFD.Code == "SQ")
            {
                //if (DsReference != null && DsReference.Tables.Count != 0 && DsReference.Tables[0].Rows.Count != 0)
                //{                    
                //    DataRow[] rows = DsReference.Tables[0].Select("i_invalid=true");
                //    foreach (DataRow row in rows)
                //    {
                //        DsReference.Tables[0].Rows.Remove(row);
                //    }
                //}
            }
            return true;
        }
    }
}
