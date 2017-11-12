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
   public class ViewTransactionAndMaster
   {
       private BL_BASEFIELD objBLFD = new BL_BASEFIELD();
       private BLHT objHashtables = new BLHT();
       private BL_Nessary_Fields bL_RELATED_FIELDS = new BL_Nessary_Fields();
       dblayer objdblayer = new dblayer();

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
       public bool tsViewTransactionAndMaster()
       {
           return true;
       }
    }
}
