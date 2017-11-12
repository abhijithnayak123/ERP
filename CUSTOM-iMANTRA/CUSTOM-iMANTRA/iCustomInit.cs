using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iMANTRA_BL;

namespace CUSTOM_iMANTRA
{
    public class iCustomInit
    {
        private iInit objInit = new iInit();
        private BL_Nessary_Fields bL_FIELDS = new BL_Nessary_Fields();

        private BL_BASEFIELD objBLFD = new BL_BASEFIELD();

        public BL_Nessary_Fields BL_FIELDS
        {
            get { return bL_FIELDS; }
            set { bL_FIELDS = value; }
        }
        public BL_BASEFIELD ACTIVE_BL
        {
            get { return objBLFD; }
            set { objBLFD = value; }
        }

        public void Load_Init_Details()
        {
            //SJK on 04.22.13 modified code to manufacturer
            #region
            //if (ACTIVE_BL.Code == "DC")
            //{
            //    objInit.DisableField("qty",0);
            //    objInit.DisableField("soqty", 0);
            //}
            //if (ACTIVE_BL.Code == "SR")
            //{
            //    objInit.DisableField("qty", 0);
            //    objInit.DisableField("dcqty", 0);
            //}
            #endregion
        }
    }
}
