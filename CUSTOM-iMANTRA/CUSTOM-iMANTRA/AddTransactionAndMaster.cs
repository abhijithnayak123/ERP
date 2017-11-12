using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMANTRA_BL;
using CUSTOM_iMANTRA_BL;
using System.IO;

namespace CUSTOM_iMANTRA
{
    public class AddTransactionAndMaster
    {
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        private BLHT objHashtables = new BLHT();
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        private iInit objinit = new iInit();

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
        public bool tsAddTransactionAndMaster()
        {
            if (ACTIVE_BL.IsSchedule)
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.IsDeSchedule)
            {
                if (objHashtables != null)
                {
                    objHashtables.HashDeallocateSchedule.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.IsFileAttach || objBLFD.Code == "EV")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashFileUpload.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "TS")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashGeneraltbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "WO")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    objHashtables.HashItemtbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "CE" || ACTIVE_BL.Code == "PP")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "PD")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashMaintbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (ACTIVE_BL.Code == "TS")
            {
                if (objHashtables != null)
                {
                    objHashtables.HashGeneraltbl.Clear();
                    objBLFD.HASHTABLES = objHashtables;
                }
            }
            if (objBLFD.Code == "SE")
            {
                if (objBLFD.HTMAIN.Contains("PREP_DT"))
                {
                    objBLFD.HTMAIN["PREP_DT"] = DateTime.Now;
                }
                if (objBLFD.HTMAIN.Contains("REMOV_DT"))
                {
                    objBLFD.HTMAIN["REMOV_DT"] = DateTime.Now;
                }
            }
            //if (ACTIVE_BL.Code == "PT")
            //{
            //    objinit.DisableField("ISDEACTIVE", 0);
            //}
            //if (ACTIVE_BL.Code == "OS" && ACTIVE_BL.HTMAIN.Contains("TRAN_DT"))
            //{
            //    ACTIVE_BL.HTMAIN["TRAN_DT"] = ACTIVE_BL.ObjCompany.Fin_yr_sta;
            //}
            return true;
        }
    }
}
