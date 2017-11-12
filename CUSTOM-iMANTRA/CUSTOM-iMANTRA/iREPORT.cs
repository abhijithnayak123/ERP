using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMANTRA_BL;
using System.Data;
using System.Collections;


namespace CUSTOM_iMANTRA
{
    public class iREPORT
    {
        private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
        dblayer objdblayer = new dblayer();
        private Hashtable Hashreport = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public Hashtable HTREPORT
        {
            get { return Hashreport; }
            set { Hashreport = value; }
        }

        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }
        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_RELATED_FIELDS; }
            set { bL_RELATED_FIELDS = value; }
        }
        public bool ReportPreview()
        {
            DataSet ds = objdblayer.dsquery("select rep_nm,spl_cond from REP_MAST where rep_nm='" + HTREPORT["rep_nm"] + "'");
            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                if (HTREPORT["rep_nm"].ToString() == "ARE1_FORM" || HTREPORT["rep_nm"].ToString() == "ARE3_FORM")
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["spl_cond"]) == true)
                    {
                        frmCustomReportCond objCustomReportCond = new frmCustomReportCond();
                        objCustomReportCond.IHTFILTER = this.HTREPORT;
                        objCustomReportCond.ACTIVE_BL = objBLFD;
                        objCustomReportCond.ShowDialog();
                        this.HTREPORT = objCustomReportCond.IHTFILTER;
                    }
                }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["spl_cond"]) == false)
                {
                    this.HTREPORT["spl_cond"] = "";
                }
            }
            return true;
        }
    }
}
