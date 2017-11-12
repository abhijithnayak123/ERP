using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUSTOM_iMANTRA
{
    public class iInit
    {
        private static Form _activeFrm;

        public static Form ActiveFrm
        {
            get { return _activeFrm; }
            set { _activeFrm = value; }
        }

        public void DisableField(string fld_nm, int level)
        {
            if (level == 1)
            {
                Control[] txts = _activeFrm.Controls.Find(fld_nm, true);
                if (txts != null)
                {
                    txts[0].Enabled = false;
                }
            }
            else
            {
                EnableOrDisable(fld_nm, false);
            }
        }


        public void EnableField(string fld_nm, int level)
        {
            if (level == 1)
            {
                Control[] txts = _activeFrm.Controls.Find(fld_nm, true);
                if (txts != null)
                {
                    txts[0].Enabled = true;
                }
            }
            else
            {
                EnableOrDisable(fld_nm, true);
            }
        }

        private void EnableOrDisable(string fld_nm, bool blnValue)
        {
            bool flg = true;
            //Transation & Master
            foreach (Control c in _activeFrm.Controls[1].Controls)
            {
                //if (c is Panel)
                //{
                    //foreach (Control cP in c.Controls)
                    //{
                        if (c is TabControl)
                        {
                            foreach (Control c1 in c.Controls)
                            {
                                if (c1 is TabPage)
                                {
                                    foreach (Control c2 in c1.Controls)
                                    {
                                        if (c2 is DataGridView)
                                        {
                                            if (((DataGridView)c2).Columns.Contains(fld_nm))
                                            {
                                                ((DataGridView)c2).Columns[fld_nm].ReadOnly = !blnValue;
                                                flg = true;
                                            }
                                            else
                                            {
                                                flg = false;
                                            }
                                        }
                                        else
                                        {
                                            if (c2.Name.Trim().ToLower() == fld_nm.ToLower())
                                            {
                                                c2.Enabled = blnValue;
                                                flg = true;
                                                break;
                                            }
                                            else
                                            {
                                                flg = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (c.Name.Trim().ToLower() == fld_nm.ToLower())
                            {
                                c.Enabled = blnValue;
                                flg = true;
                                break;
                            }
                            else
                            {
                                flg = false;
                            }
                        }
                    //}
                //}
                //else
                //{
                //    if (c.Name.Trim().ToLower() == fld_nm.ToLower())
                //    {
                //        c.Enabled = blnValue;
                //        flg = true;
                //        break;
                //    }
                //    else
                //    {
                //        flg = false;
                //    }
                //}
            }
            #region
            //foreach (Control c in _activeFrm.Controls[1].Controls)
            //{
            //    if (c is Panel)
            //    {
            //        foreach (Control c1 in c.Controls)
            //        {
            //            if (c1.Name.Trim().ToLower() == fld_nm.ToLower())
            //            {
            //                c1.Enabled = blnValue;
            //                flg = true;
            //                break;
            //            }
            //            else
            //            {
            //                flg = false;
            //            }
            //        }
            //    }
            //    if (c is TabControl)
            //    {
            //        foreach (Control c1 in c.Controls)
            //        {
            //            if (c1 is TabPage)
            //            {
            //                foreach (Control c2 in c1.Controls)
            //                {
            //                    if (c2 is DataGridView)
            //                    {
            //                        if (((DataGridView)c2).Columns.Contains(fld_nm))
            //                        {
            //                            ((DataGridView)c2).Columns[fld_nm].ReadOnly = !blnValue;
            //                            flg = true;
            //                        }
            //                        else
            //                        {
            //                            flg = false;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //foreach (Control c in _activeFrm.Controls[0].Controls)
            //{
            //    if (c is TabControl)
            //    {
            //        foreach (Control c1 in c.Controls)
            //        {
            //            if (c1 is TabPage)
            //            {
            //                foreach (Control c2 in c1.Controls)
            //                {
            //                    if (c2 is Panel)
            //                    {
            //                        foreach (Control c3 in c2.Controls)
            //                        {
            //                            if (c3.Name.Trim().ToLower() == fld_nm.ToLower())
            //                            {
            //                                c3.Enabled = blnValue;
            //                                flg = true;
            //                                break;
            //                            }
            //                            else
            //                            {
            //                                flg = false;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            ////if (!flg)
            ////{
            ////    MessageBox.Show(fld_nm + " Not found!!");
            ////}
            #endregion
        }

        public void ReadOnlyField(string fld_nm, int level, string type)
        {
            if (level == 1)
            {
                Control[] txts = _activeFrm.Controls.Find(fld_nm, true);
                if (txts != null)
                {
                    switch (type.ToLower())
                    {
                        case "textbox": ((TextBox)txts[0]).ReadOnly = true; break;
                        default: break;
                    }
                }
            }
        }
    }
}
