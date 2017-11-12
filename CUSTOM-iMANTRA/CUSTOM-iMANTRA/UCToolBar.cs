using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CUSTOM_iMANTRA
{
    public partial class UCToolBar : UserControl
    {
        public string Titlebar
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }
        private bool _close, _maximize, _minimize;
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
            get { return btnMin.Enabled; }
            set { btnMin.Enabled = value; }
        }

        public bool Maximize
        {
            get { return btnMax.Enabled; }
            set { btnMax.Enabled = value; }
        }

        public bool Close
        {
            get { return btnClose.Enabled; }
            set { btnClose.Enabled = value; }
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
            if (btnMax.Enabled)
            {
                if (this.ParentForm.WindowState == FormWindowState.Maximized)
                {
                    this.ParentForm.WindowState = FormWindowState.Normal;
                }
                else if (this.ParentForm.WindowState == FormWindowState.Normal)
                {
                    if (btnMax.Enabled)
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
            if (btnMin.Enabled)
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
                if (btnMax.Enabled)
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
                if (btnMax.Enabled)
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
            btnClose.BackColor = Color.Crimson;
        }

        private void btnMax_MouseHover(object sender, EventArgs e)
        {
            btnMax.BackColor = Color.Crimson;
        }

        private void btnMin_MouseHover(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Crimson;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnMax_MouseLeave(object sender, EventArgs e)
        {
            btnMax.BackColor = Color.Transparent;
        }

        private void btnMin_MouseLeave(object sender, EventArgs e)
        {
            btnMin.BackColor = Color.Red;
        }

        private void UCToolBar_Load(object sender, EventArgs e)
        {
            btnClose.Enabled = true;
            btnMax.Enabled = false;
            btnMin.Enabled = false;
            btnMax.Visible = false;
            btnMin.Visible = false;
        }
    }
}
