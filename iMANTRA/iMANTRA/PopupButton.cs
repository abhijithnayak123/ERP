using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace iMANTRA
{
    public class PopupButton : Button
    {
       // Image imag;
        private Color gradientTop = System.Drawing.SystemColors.Control;
        private Color gradientBottom = Color.Gray;
        private Rectangle buttonRect;
        private int highlightAlphaTop = 255;
        private int highlightAlphaBottom;
        private Rectangle highlightRect;
        private Timer animateMouseOverTimer = new Timer();
        private Timer animateResumeNormalTimer = new Timer();
        private bool increasingAlpha;

        [Category("Appearance"),
    Description("The color to use for the top portion of the gradient fill of the component.")]
        public Color GradientTop
        {
            get
            {
                return gradientTop;
            }
            set
            {
                gradientTop = value;
                Invalidate();
            }
        }

        [Category("Appearance"),
    Description("The color to use for the bottom portion of the gradient fill of the component.")]
        public Color GradientBottom
        {
            get
            {
                return gradientBottom;
            }
            set
            {
                gradientBottom = value;
                Invalidate();
            }
        }


      //  Image imageVal;
        public TextBox pText = null;
        public string frmName = "";
        public string PTextName = "";
        private string _tbl_nm = "";
        private string _reftbltran_cd = "";
        private string _query_con = "";
        private bool _isQcd = false;
        private string _QcdCondition = "";

        public bool IsQcd
        {
            get { return _isQcd; }
            set { _isQcd = value; }
        }
        public string QcdCondition
        {
            get { return _QcdCondition; }
            set { _QcdCondition = value; }
        }
        public string Query_con
        {
            get { return _query_con; }
            set { _query_con = value; }
        }

        public string Tbl_nm
        {
            get { return _tbl_nm; }
            set { _tbl_nm = value; }
        }
        private string _primaryddl = "";

        public string Primaryddl
        {
            get { return _primaryddl; }
            set { _primaryddl = value; }
        }
        private string _dispddlfields = "";

        public string Dispddlfields
        {
            get { return _dispddlfields; }
            set { _dispddlfields = value; }
        }
        public string Reftbltran_cd
        {
            get { return _reftbltran_cd; }
            set { _reftbltran_cd = value; }
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    // //imageVal = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\hover1.png");
        //    // //this.BackgroundImage = imageVal;
        //    // this.BackColor = Color.Gray;
        //    //// this.BackgroundImageLayout = ImageLayout.Stretch;
        //    // this.FlatStyle = FlatStyle.Popup;
        //    base.OnMouseMove(e);
        //}

        //protected override void OnMouseLeave(EventArgs e)
        //{
        //    //  //imageVal = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\mouseleave.png");
        //    //  //this.BackgroundImage = imageVal;
        //    //  this.BackColor = Color.Transparent;
        //    ////  this.BackgroundImageLayout = ImageLayout.Stretch;
        //    //  this.FlatStyle = FlatStyle.Flat;
        //    base.OnMouseLeave(e);
        //}

        //protected override void OnPaint(PaintEventArgs pevent)
        //{
        //    gradientTop = System.Drawing.SystemColors.Control;
        //    gradientBottom = Color.Gray;

        //    Graphics g = pevent.Graphics;
        //    // Fill the background
        //    using (SolidBrush backgroundBrush = new SolidBrush(gradientTop))
        //    {
        //        g.FillRectangle(backgroundBrush, this.ClientRectangle);
        //    }
        //    // Paint the outer rounded rectangle
        //    g.SmoothingMode = SmoothingMode.AntiAlias;
        //    Rectangle outerRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
        //    using (GraphicsPath outerPath = RoundedRectangle(outerRect, 5, 0))
        //    {
        //        using (LinearGradientBrush outerBrush = new LinearGradientBrush(outerRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
        //        {
        //            g.FillPath(outerBrush, outerPath);
        //        }
        //        using (Pen outlinePen = new Pen(gradientTop))
        //        {
        //            g.DrawPath(outlinePen, outerPath);
        //        }
        //    }
        //    // Paint the highlight rounded rectangle
        //    Rectangle innerRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height / 2 - 1);
        //    using (GraphicsPath innerPath = RoundedRectangle(innerRect, 5, 2))
        //    {
        //        using (LinearGradientBrush innerBrush = new LinearGradientBrush(innerRect, Color.FromArgb(255, Color.White), Color.FromArgb(0, Color.White), LinearGradientMode.Vertical))
        //        {
        //            g.FillPath(innerBrush, innerPath);
        //        }
        //    }
        //    // Paint the text
        //    TextRenderer.DrawText(g, this.Text, this.Font, outerRect, this.ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

        //}

        //private GraphicsPath RoundedRectangle(Rectangle boundingRect, int cornerRadius, int margin)
        //{
        //    GraphicsPath roundedRect = new GraphicsPath();
        //    roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 180, 90);
        //    roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 270, 90);
        //    roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
        //    roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
        //    roundedRect.CloseFigure();
        //    return roundedRect;
        //}

        protected override void OnCreateControl()
        {
            SuspendLayout();
            base.OnCreateControl();
            buttonRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            highlightRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height / 2 - 1);
            animateMouseOverTimer.Interval = 20;
            animateMouseOverTimer.Tick += new EventHandler(animateMouseOverTimer_Tick);
            animateResumeNormalTimer.Interval = 5;
            animateResumeNormalTimer.Tick += new EventHandler(animateResumeNormalTimer_Tick);
            ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            // Fill the background
            ButtonRenderer.DrawParentBackground(g, ClientRectangle, this);
            // Paint the outer rounded rectangle
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath outerPath = RoundedRectangle(buttonRect, 5, 0))
            {
                using (LinearGradientBrush outerBrush = new LinearGradientBrush(buttonRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
                {
                    g.FillPath(outerBrush, outerPath);
                    // g.FillEllipse(outerBrush, 0, 0, ClientSize.Width, ClientSize.Height);
                }
                using (Pen outlinePen = new Pen(gradientTop))
                {
                    g.DrawPath(outlinePen, outerPath);
                    //  g.DrawEllipse(outlinePen, 0, 0, ClientSize.Width, ClientSize.Height);                   
                }
            }
            // Paint the highlight rounded rectangle
            using (GraphicsPath innerPath = RoundedRectangle(highlightRect, 5, 2))
            {
                using (LinearGradientBrush innerBrush = new LinearGradientBrush(highlightRect, Color.FromArgb(highlightAlphaTop, Color.White), Color.FromArgb(highlightAlphaBottom, Color.White), LinearGradientMode.Vertical))
                {
                    g.FillPath(innerBrush, innerPath);
                }
            }

            // Paint the text
            //if(this.Text!=""){
            Font objfont;
            if (this.Name == "btnUCClose" || this.Name == "btnUCMax" || this.Name == "btnUCMin")
            {
                objfont = new Font("Webdings", 8,FontStyle.Bold);
            }
            else
            {
                objfont = this.Font;
            }
            TextRenderer.DrawText(g, Text, objfont, buttonRect, ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            if (this.BackgroundImage != null)
            {
                g.DrawImage(this.BackgroundImage, buttonRect.X, buttonRect.Y, buttonRect.Width, buttonRect.Height);
            }
            //if (this.Image == null)
            //    this.Image = this.BackgroundImage;

            //Bitmap renderBmp = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            //g = Graphics.FromImage(renderBmp);
            // g.DrawImage(this.BackgroundImage, 0, 0, this.Width, this.Height);
            //this.Image = renderBmp;

        }

        private static GraphicsPath RoundedRectangle(Rectangle boundingRect, int cornerRadius, int margin)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            animateResumeNormalTimer.Stop();
            animateMouseOverTimer.Start();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            animateMouseOverTimer.Stop();
            animateResumeNormalTimer.Start();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            animateMouseOverTimer.Stop();
            animateResumeNormalTimer.Stop();
            highlightRect.Location = new Point(0, ClientRectangle.Height / 2);
            highlightAlphaTop = 0;
            highlightAlphaBottom = 200;
            Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            highlightRect.Location = new Point(0, 0);
            highlightAlphaTop = 255;
            highlightAlphaBottom = 0;
            if (DisplayRectangle.Contains(mevent.Location))
            {
                animateMouseOverTimer.Start();
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if ((mevent.Button & MouseButtons.Left) == MouseButtons.Left && !ClientRectangle.Contains(mevent.Location))
            {
                OnMouseUp(mevent);
            }
            base.OnMouseMove(mevent);
        }

        private void animateMouseOverTimer_Tick(object sender, EventArgs e)
        {
            if (increasingAlpha)
            {
                if (100 <= highlightAlphaBottom)
                {
                    increasingAlpha = false;
                }
                else
                {
                    highlightAlphaBottom += 5;
                }
            }
            else
            {
                if (0 >= highlightAlphaBottom)
                {
                    increasingAlpha = true;
                }
                else
                {
                    highlightAlphaBottom -= 5;
                }
            }
            Invalidate();
        }

        private void animateResumeNormalTimer_Tick(object sender, EventArgs e)
        {
            bool modified = false;
            if (highlightAlphaBottom > 0)
            {
                highlightAlphaBottom -= 5;
                modified = true;
            }
            if (highlightAlphaTop < 255)
            {
                highlightAlphaTop += 5;
                modified = true;
            }
            if (!modified)
            {
                animateResumeNormalTimer.Stop();
            }
            Invalidate();
        }
    }
}
