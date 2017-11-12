using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace iMANTRA
{
    public partial class UCToolBar : UserControl
    {
        public string Titlebar
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }
        private bool _close, _maximize, _minimize;
        private int _form_width, _form_height;

        public int Form_height
        {
            get { return _form_height; }
            set { _form_height = value; }
        }

        public int Form_width
        {
            get { return _form_width; }
            set { _form_width = value; }
        }

        private int _width, _height;      

        public Color UCforcolor
        {
            get { return this.panel1.ForeColor; }
            set { this.panel1.ForeColor = value; }
        }

        public Color UCbackcolor
        {
            get { return this.panel1.BackColor; }
            set { this.panel1.BackColor = value; }
        }


        public int Height1
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width1
        {
            get { return _width; }
            set { _width = value; }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public bool Minimize
        {
            get { return btnUCMin.Enabled; }
            set { btnUCMin.Enabled = value; }
        }

        public bool Maximize
        {
            get { return btnUCMax.Enabled; }
            set { btnUCMax.Enabled = value; }
        }

        public bool Close
        {
            get { return btnUCClose.Enabled; }
            set { btnUCClose.Enabled = value; }
        }

        public UCToolBar()
        {
            InitializeComponent(); 
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //Close = true;
            //Minimize = false;
            //Maximize = false;
            this.ParentForm.Close();
            // CloseForm(sender, e);
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            //Maximize = true;
            //Close = false;
            //Minimize = false;
            if (btnUCMax.Enabled)
            {
                if (this.ParentForm.WindowState == FormWindowState.Maximized)
                {
                    this.ParentForm.WindowState = FormWindowState.Normal;
                }
                else if (this.ParentForm.WindowState == FormWindowState.Normal)
                {
                    if (btnUCMax.Enabled)// && this.ParentForm.Name == "frm_mainmenu")
                    {
                        this.ParentForm.WindowState = FormWindowState.Maximized;                       
                        //this.ParentForm.Dock = DockStyle.Fill;
                    }
                }

                this.ParentForm.Show();
            }
            //Maxmise(sender, e);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            //Minimize = true;
            //Maximize = false;
            //Close = false;
            if (btnUCMin.Enabled)
            {
                this.ParentForm.WindowState = FormWindowState.Minimized;
                //this.ParentForm.Text = Titlebar;
                this.ParentForm.Show();
            }
            //Minimise(sender, e);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                    this.ParentForm.WindowState = FormWindowState.Normal;
                    this.ParentForm.Show();
            }
            else if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                //this.ParentForm.WindowState = FormWindowState.Maximized;
                if (btnUCMax.Enabled)// && this.ParentForm.Name == "frm_mainmenu")
                {
                    this.ParentForm.WindowState = FormWindowState.Maximized;
                    //this.ParentForm.Dock = DockStyle.Fill;
                    //this.ParentForm.Width = _width;
                    this.ParentForm.Show();
                }
            }
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((e.Clicks == 1) && (this.ParentForm.WindowState != FormWindowState.Maximized))
                {
                    ReleaseCapture();
                    SendMessage(this.ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        private void lblTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.ParentForm.WindowState == FormWindowState.Maximized)
            {
                this.ParentForm.WindowState = FormWindowState.Normal;
                this.ParentForm.Show();
            }
            else if (this.ParentForm.WindowState == FormWindowState.Normal)
            {
                if (btnUCMax.Enabled)// && this.ParentForm.Name == "frm_mainmenu")
                {
                    this.ParentForm.WindowState = FormWindowState.Maximized;
                    //this.ParentForm.Width = _width;
                    //this.ParentForm.Dock = DockStyle.Fill;
                    this.ParentForm.Show();
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((e.Clicks == 1) && (this.ParentForm.WindowState != FormWindowState.Maximized))
                {
                    ReleaseCapture();
                    SendMessage(this.ParentForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnUCClose.BackColor = Color.Crimson;
        }

        private void btnMax_MouseHover(object sender, EventArgs e)
        {
            btnUCMax.BackColor = Color.Crimson;
        }

        private void btnMin_MouseHover(object sender, EventArgs e)
        {
            btnUCMin.BackColor = Color.Crimson;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnUCClose.BackColor = Color.Transparent;
        }

        private void btnMax_MouseLeave(object sender, EventArgs e)
        {
            btnUCMax.BackColor = Color.Transparent;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnUCMin.BackColor = Color.Red;
        }

        private void UCToolBar_Load(object sender, EventArgs e)
        {
            btnUCClose.Enabled = true;
            btnUCMax.Enabled = true;
            btnUCMin.Enabled = true;
        }
    }
}
