using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace CUSTOM_iMANTRA
{
    public class ieclscell_click
    {
        public Hashtable HTITEM_VALUE = new Hashtable();

        public bool GridCell_Click(object sender, EventArgs e)
        {
            HTITEM_VALUE["ASSESS_VALUE"] = ((decimal.Parse(HTITEM_VALUE["RATE"].ToString()) * decimal.Parse(HTITEM_VALUE["QTY"].ToString())) + 10).ToString();
            return true;
        }
    }
}
