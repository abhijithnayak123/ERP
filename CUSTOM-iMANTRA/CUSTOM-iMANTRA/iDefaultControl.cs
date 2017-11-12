using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iDefaultControl
    {
        /*  Created by Sharanamma Jekeen Inode Technologies Pvt. Ltd. on 11.26.13
        * This Class is used for Enable or Disable Fields,Fill details to Fields by Default/Loading.
         * 1.0 Sharanamma Jekeen on 11.29.13 ==> Default Enable or Disable Fields.
         * 
         * 
         * 
       * */
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();

        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        dblayer objdblayer = new dblayer();
        private iInit objinit = new iInit();

        iTRANSACTION objTransaction = new iTRANSACTION();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public bool DefaultControl()
        {
            #region 1.0
            if (objBLFD.IsAmendment && objBLFD.Tran_mode != "view_mode")
            {
                objTransaction.ACTIVE_TRANSACTION = ACTIVE_BL;
                objTransaction.ValidateAmedementDetails();
            }            
            #endregion 1.0
            if (ACTIVE_BL.IsFileAttach || objBLFD.Code == "EV")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashFileUpload.Clear();
                    EditFileUpload();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            return true;
        }
        private void EditFileUpload()
        {
            string key = "";
            DataSet dsetFileUpload = objdblayer.dsquery("select * from FILE_UPLOAD where sel=1 and tran_id='" + objBLFD.Tran_id.ToString() + "' and tran_cd='" + objBLFD.Code + "' and compid='" + objBLFD.ObjCompany.Compid.ToString() + "'");
            if (dsetFileUpload != null && dsetFileUpload.Tables.Count != 0 && dsetFileUpload.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow r in dsetFileUpload.Tables[0].Rows)
                {
                    key = r["PTSERIAL"].ToString() + "," + r["si_no"].ToString();
                    if (objHashtables != null && objHashtables.HashFileUpload != null && !objHashtables.HashFileUpload.Contains(key))
                    {
                        objHashtables.HashFileUpload[key] = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
                        foreach (DataColumn column in dsetFileUpload.Tables[0].Columns)
                        {
                            ((Hashtable)objHashtables.HashFileUpload[key])[column.ColumnName] = r[column.ColumnName];
                        }
                    }
                }
            }
        }
    }
}
