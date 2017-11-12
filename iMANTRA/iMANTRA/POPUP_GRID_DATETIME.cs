using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iMANTRA
{
    public class POPUP_GRID_DATETIME : POPUPDATETIME_FOR_GRID
    {
        private DateTime _getDateTime;

        public DateTime GetDateTime
        {
            get
            {
                if (_getDateTime != null && _getDateTime.ToString() != "")
                {
                    return _getDateTime;
                }
                else
                {
                    return Convert.ToDateTime("01/01/1900");
                }
            }
            set
            {
                if (_getDateTime != null && _getDateTime.ToString() == "01/01/1900")
                {
                    _getDateTime = value;
                }
                //else
                //{
                //    _getDateTime = Convert.ToDateTime("01/01/1900");
                //}
            }
        }
    }
}
