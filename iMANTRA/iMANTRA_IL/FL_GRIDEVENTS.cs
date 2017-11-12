using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using CUSTOM_iMANTRA;
using iMANTRA_BL;

namespace iMANTRA_IL
{
    public class FL_GRIDEVENTS
    {
        public BL_BASEFIELD objBASEFILEDS = new BL_BASEFIELD();
        ieclscell_click objCL = new ieclscell_click();
        //public Hashtable HTMAIN = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        //public Hashtable HTITEM = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
        //public Hashtable HTITEM_VALUE = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

        public bool GridCell_Click(Hashtable HTDCFIELDS)
        {
            Regex re = new Regex(@"[a-zA-Z]*");
            bool flag = true;
            if (objBASEFILEDS.HTITEM_VALUE["QTY"].ToString() != "" && objBASEFILEDS.HTITEM_VALUE["RATE"].ToString() != "")
            {
                objBASEFILEDS.HTITEM_VALUE["ASSES_AMT"] = (decimal.Parse(objBASEFILEDS.HTITEM_VALUE["QTY"].ToString()) * decimal.Parse(objBASEFILEDS.HTITEM_VALUE["RATE"].ToString())).ToString();

                foreach (DictionaryEntry entry in HTDCFIELDS)
                {
                    if (objBASEFILEDS.HTITEM_VALUE[entry.Key.ToString()] == null || objBASEFILEDS.HTITEM_VALUE[entry.Key.ToString()].ToString() == "")
                        objBASEFILEDS.HTITEM_VALUE[entry.Key] = "0";
                    if (entry.Value.ToString() == "A")
                    {
                        objBASEFILEDS.HTITEM_VALUE["ASSES_AMT"] = Math.Round(decimal.Parse(objBASEFILEDS.HTITEM_VALUE["ASSES_AMT"].ToString()) + decimal.Parse(objBASEFILEDS.HTITEM_VALUE[entry.Key.ToString()].ToString()), 2);
                    }
                    else
                    {
                        objBASEFILEDS.HTITEM_VALUE["ASSES_AMT"] = Math.Round(decimal.Parse(objBASEFILEDS.HTITEM_VALUE["ASSES_AMT"].ToString()) - decimal.Parse(objBASEFILEDS.HTITEM_VALUE[entry.Key.ToString()].ToString()), 2);
                    }
                }
                return flag;
            }
            else
            {
                return false;
            }
        }
        public decimal Calculate_duty(string property_val, string amt_expr, bool pert)
        {
            try
            {
                if (pert)
                {
                    return decimal.Parse(Math.Round((decimal.Parse(property_val) / 100) * decimal.Parse(amt_expr), 2).ToString());

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }
        public bool Calculate_excise_duty(string fld_nm, string property_val, string amt_expr, bool pert, bool round_off)
        {
            try
            {
                if (pert)
                {
                    if (round_off)
                    {
                        objBASEFILEDS.HTITEM_VALUE[fld_nm] = Math.Round((decimal.Parse(property_val) / 100) * decimal.Parse(amt_expr), MidpointRounding.AwayFromZero).ToString();
                    }
                    else
                    {
                        objBASEFILEDS.HTITEM_VALUE[fld_nm] = Math.Round((decimal.Parse(property_val) / 100) * decimal.Parse(amt_expr), 2).ToString();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public string InfixToPostfix(string amt_expre1, string type)
        {
            int priority = 0;
            string postfixBuffer = "";

            Stack s1 = new Stack();
            amt_expre1 = amt_expre1.Replace("+", "$+$").Replace("-", "$-$").Replace("*", "$*$").Replace("/", "$/$").Replace("(", "$($").Replace(")", "$)$");
            string[] amt_expre = amt_expre1.Split(new Char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < amt_expre.Length; i++)
            {
                if (amt_expre[i] != "")
                {
                    string ch = amt_expre[i];
                    if (ch == "+" || ch == "-" || ch == "*" || ch == "/")
                    {
                        if (s1.Count <= 0) s1.Push(ch);
                        else
                        {
                            if ((string)s1.Peek() == "*" || (string)s1.Peek() == "/") priority = 1; else priority = 0;

                            if (priority == 1)
                            {
                                if (ch == "+" || ch == "-") { postfixBuffer += "," + s1.Pop(); }
                                else { postfixBuffer += "," + s1.Pop(); }
                            }
                            else
                            {
                                if (ch == "+" || ch == "-") { postfixBuffer += "," + s1.Pop(); s1.Push(ch); }
                                else s1.Push(ch);
                            }
                        }
                    }
                    else
                    {
                        if (ch == ")")
                        {
                            if (s1.Count > 0)
                                postfixBuffer += "," + s1.Pop();
                        }
                        else if (ch != "(" && ch != ")")
                        {
                            if (postfixBuffer == "")
                                postfixBuffer += ch;
                            else
                                postfixBuffer += "," + ch;
                        }
                    }
                }
            }
            for (int j = 0; j < s1.Count; j++)
                postfixBuffer += "," + s1.Pop();

            //postfix expression eval

            Stack s2 = new Stack();
            string amt_expre2 = postfixBuffer;
            postfixBuffer = "";
            decimal value = 0;
            string[] subs = amt_expre2.Split(',');
            for (int k = 0; k < subs.Length; k++)
            {
                if (subs[k].Equals("+") || subs[k].Equals("-") || subs[k].Equals("*") || subs[k].Equals("/"))
                {
                    string strOP1 = "0";
                    string strOP2 = "0";
                    strOP1 = (string)s2.Peek() == "" ? "0" : (string)s2.Pop();
                    strOP2 = (string)s2.Peek() == "" ? "0" : (string)s2.Pop();
                    decimal op1 = decimal.Parse(strOP1);
                    decimal op2 = decimal.Parse(strOP2);
                    if (subs[k].Equals("+"))
                    {
                        value = op1 + op2;
                    }
                    if (subs[k].Equals("-"))
                    {
                        value = op1 - op2;
                    }
                    if (subs[k].Equals("*"))
                    {
                        value = op1 * op2;
                    }
                    if (subs[k].Equals("/"))
                    {
                        value = op1 / op2;
                    }
                    s2.Push(value.ToString());
                }
                else
                {
                    //if (objBASEFILEDS.HTITEM_VALUE[subs[k]].ToString() == "")
                    //    objBASEFILEDS.HTITEM_VALUE[subs[k]] = "0";
                    string[] strhash = subs[k].Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] parts = strhash[1].Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v', '\\', '"' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strhash[0].ToString().Trim().ToUpper() == "HTITEM_VALUE")
                    {
                        s2.Push(objBASEFILEDS.HTITEM_VALUE[parts[0]].ToString());
                    }
                    else
                    {
                        s2.Push(objBASEFILEDS.HTMAIN[parts[0]].ToString());
                    }
                }
            }
            for (int j = 0; j < s2.Count; j++)
            {
                if (type == "decimal")
                    postfixBuffer = decimal.Parse(s2.Peek() != null && (string)s2.Peek() != "" ? (string)s2.Pop() : "0").ToString();//s2.Pop();                
                else if (type == "string")
                    postfixBuffer = s2.Peek() != null ? (string)s2.Pop() : "";//s2.Pop();}
                else if (type == "bit")
                    postfixBuffer = bool.Parse(s2.Peek() != null && (string)s2.Peek() != "" ? (string)s2.Pop() : "0").ToString();//s2.Pop();   
                else if (type == "int")
                    postfixBuffer = int.Parse(s2.Peek() != null && (string)s2.Peek() != "" ? (string)s2.Pop() : "0").ToString();//s2.Pop();   
            }

            return postfixBuffer;
        }
        public bool Calculate_gross_amt_grid()
        {
            return true;
        }
    }
}
