using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace CUSTOM_iMANTRA
{
    public class MyDataGridView : DataGridView
    {
        Image _backgroundPic;// = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\Line.png");
        [Browsable(true)]
        public override Image BackgroundImage
        {
            get { return _backgroundPic; }
            set { _backgroundPic = value; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {           
            base.OnPaint(e);

            int rowHeight = this.RowTemplate.Height;

            int h = this.ColumnHeadersHeight + rowHeight * this.RowCount;
            int imgWidth = this.Width;
            Rectangle rFrame = new Rectangle(0, 0, imgWidth, rowHeight);
            Rectangle rFill = new Rectangle(1, 1, imgWidth, rowHeight);
            Rectangle rowHeader = new Rectangle(1, 1, this.RowHeadersWidth, rowHeight);

            Pen pen = new Pen(this.GridColor, 1);

            Bitmap rowImg = new Bitmap(imgWidth, rowHeight);
            Graphics g = Graphics.FromImage(rowImg);
            g.DrawRectangle(pen, rFrame);
            g.FillRectangle(new SolidBrush(this.DefaultCellStyle.BackColor), rFill);
            g.FillRectangle(new SolidBrush(this.RowHeadersDefaultCellStyle.BackColor), rowHeader);

            //g.FillRectangle(new SolidBrush(this.DefaultCellStyle.SelectionBackColor), rowHeader);

            Bitmap rowImgAAlternative = rowImg.Clone() as Bitmap;
            Graphics g2 = Graphics.FromImage(rowImgAAlternative);
            rFill.X += this.RowHeadersWidth;
            g2.FillRectangle(new SolidBrush(this.AlternatingRowsDefaultCellStyle.BackColor), rFill);

            //int w = this.RowHeadersWidth - 1;
            //for (int j = 0; j < this.ColumnCount; j++)
            //{
            //    g.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
            //    g2.DrawLine(pen, new Point(w, 0), new Point(w, rowHeight));
            //    w += this.Columns[j].Width;
            //}

            int loop = (this.Height - h) / rowHeight;
            for (int j = 0; j < loop + 1; j++)
            {
                int index = this.RowCount + j;
                if (index % 2 == 0)
                {
                    e.Graphics.DrawImage(rowImg, 1, h + j * rowHeight);
                }
                else
                {
                    e.Graphics.DrawImage(rowImgAAlternative, 1, h + j * rowHeight);
                }
            }
        }        
    }
}
