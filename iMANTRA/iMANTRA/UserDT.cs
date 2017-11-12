using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iMANTRA_BL;

namespace iMANTRA
{
    public partial class UserDT : UserControl
    {
        public UserDT()
        {
            bUpdateFlag = false;
            InitializeComponent();
        }

        public bool bUpdateFlag = false;
        private bool flg = false;
        public string CustomFormat
        {
            get { return dateTimePicker1.CustomFormat; }
            set { dateTimePicker1.CustomFormat = value; }
        }

        public DateTime DtValue
        {
            get
            {
                if (dateTimePicker1.Value != null && dateTimePicker1.Value.ToString() != "")
                {
                    if (dateTimePicker1.Value.ToString() != "1900-01-01 12:00:00 AM" && dateTimePicker1.Value.ToString("yyyy/mm/dd") != "2000-00-01" && dateTimePicker1.Value.ToString("yyyy/mm/dd") != "1900/00/01" && dateTimePicker1.Value.ToString("yyyy/mm/dd") != "2000/00/01")
                    {
                        if (textBox1.Text == "  -   -")
                            return Convert.ToDateTime("1900-01-01 12:00:00 AM");
                        else
                            return dateTimePicker1.Value;
                    }
                }
                return Convert.ToDateTime("1900-01-01 12:00:00 AM");
            }
            set
            {
                if (dateTimePicker1.Value != null && dateTimePicker1.Value.ToString() != "")
                {
                    if (value.ToString() != "1900-01-01 12:00:00 AM" && value.ToString("yyyy/mm/dd") != "2000-00-01" && value.ToString("yyyy/mm/dd") != "1900-00-01" && value.ToString("yyyy/mm/dd") != "1900/00/01" && value.ToString("yyyy/mm/dd") != "2000/00/01")
                    {
                        if(bUpdateFlag)
                            textBox1.Text = value.ToString("dd-MMM-yyyy");
                        else
                            textBox1.Text = "";
                        // textBox1.ReadOnly = false;
                        dateTimePicker1.Value = value;
                    }
                    else
                    {
                        textBox1.Text = "";
                        //  textBox1.ReadOnly = true;
                        dateTimePicker1.Value = value;
                        this.CustomFormat = " ";
                        this.Format = DateTimePickerFormat.Custom;
                    }
                }
                else
                {
                    textBox1.Text = "";
                    //   textBox1.ReadOnly = true;
                    dateTimePicker1.Value = value;
                    this.CustomFormat = " ";
                    this.Format = DateTimePickerFormat.Custom;
                }
            }
        }

        public DateTimePickerFormat Format
        {
            get { return DateTimePickerFormat.Custom; }
            set { dateTimePicker1.Format = value; }
        }

        public Color CalendarTitleBackColor
        {
            get { return dateTimePicker1.CalendarTitleBackColor; }
            set { dateTimePicker1.CalendarTitleBackColor = value; }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Show();
            if (textBox1.Text == "" || dateTimePicker1.Value == null || dateTimePicker1.Value.ToString() == "" || dateTimePicker1.Value.ToString() == "1900-01-01 12:00:00 AM")
            {
                // textBox1.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                dateTimePicker1.Show();
                textBox1.ReadOnly = false;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //if (dateTimePicker1.Value == null || dateTimePicker1.Value.ToString() == "" || dateTimePicker1.Value.ToString() == "1900-01-01 12:00:00 AM")
            //{
            //    dateTimePicker1.Value = Convert.ToDateTime("1900-01-01 12:00:00 AM");
            //    textBox1.Text = "";
            //    textBox1.ReadOnly = true;
            //}
            try
            {
                if (textBox1.Text != "  -   -")
                {
                    flg = true;
                    string[] array = textBox1.Text.Split('-');
                    if (array[0] == null || array[0].ToString() == "")
                    {
                        AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                        textBox1.Text = "";
                        flg = false;
                    }
                    else if (array[2] == null || array[2].ToString() == "")
                    {
                        AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                        textBox1.Text = "";
                        flg = false;
                    }
                    else
                    {
                        try
                        {
                            int.Parse(array[0]);
                            int.Parse(array[2]);
                        }
                        catch (Exception ex1)
                        {
                            AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                            textBox1.Text = "";
                            flg = false;
                        }

                        string Month = array[1].ToUpper();
                        if (Month == "JAN" || Month == "MAR" || Month == "MAY" || Month == "JUL" || Month == "AUG" || Month == "OCT" || Month == "DEC")
                        {
                            if (int.Parse(array[0]) >= 32)
                            {
                                AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                                textBox1.Text = "";
                                flg = false;
                            }
                        }
                        else if (Month == "APR" || Month == "JUN" || Month == "SEP" || Month == "NOV")
                        {
                            if (int.Parse(array[0]) >= 31)
                            {
                                AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                                textBox1.Text = "";
                                flg = false;
                            }
                        }
                        else if (Month == "FEB")
                        {
                            if (int.Parse(array[2]) % 4 == 0 && (int.Parse(array[2]) % 100 != 0 || int.Parse(array[2]) % 400 == 0))
                            {
                                if (int.Parse(array[0]) >= 30)
                                {
                                    AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                                    textBox1.Text = "";
                                    flg = false;
                                }
                            }
                            else
                            {
                                if (int.Parse(array[0]) >= 29)
                                {
                                    AutoClosingMessageBox.Show("Invalid Date","Date",3000);
                                    textBox1.Text = "";
                                    flg = false;
                                }
                            }
                        }
                    }
                    if (flg)
                    {
                        //DtValue = DateTime.Parse(textBox1.Text);
                        if (textBox1.Text.ToUpper() != "01-JAN-1900")
                        {
                            dateTimePicker1.Value = DateTime.Parse(textBox1.Text);
                        }
                        DtValue = DateTime.Parse(textBox1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Invalid Date","Date",3000);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //this.DtValue = dateTimePicker1.Value;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (textBox1.Text != "  -   -")
                {
                    if (!flg)
                    {
                        AutoClosingMessageBox.Show("Invalid Date", "Date", 3000);
                        textBox1.Text = "";
                        e.Cancel = true;
                    }
                }
                //else
                //{
                //    DtValue = DateTime.Parse("01/01/1900");
                //}
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("Invalid Date","Date",3000);
            }

        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (textBox1.Text != "  -   -" && flg)
            {
                //DtValue = DateTime.Parse(textBox1.Text);
                if (textBox1.Text.ToUpper() != "01-JAN-1900")
                {
                    dateTimePicker1.Value = DateTime.Parse(textBox1.Text);
                }
                DtValue = DateTime.Parse(textBox1.Text);
            }

        }

        private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            //if (dateTimePicker1.Value.Year == 1900)
            //{
            //    bUpdateFlag = false;
            //    dateTimePicker1.Value = DateTime.Now;//DateTime.Parse("01/01/1900");
            //}
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            bUpdateFlag = true;
            this.DtValue = dateTimePicker1.Value;
        }
    }
}
