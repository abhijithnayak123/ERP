using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace iMANTRA
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

        public int height
        {
            get { return this.Height; }
            set { this.Height = value; }
        }

        public int width
        {
            get { return this.Width; }
            set { this.Width = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {           
            base.OnPaint(e);

            int rowHeight = this.RowTemplate.Height;

            int h = this.ColumnHeadersHeight + rowHeight * this.RowCount;
            int imgWidth = this.Width;
            Rectangle rFrame = new Rectangle(0, 0, imgWidth, rowHeight);
            Rectangle rFill = new Rectangle(1, 1, imgWidth, rowHeight);
            //Rectangle rowHeader = new Rectangle(1, 1, this.RowHeadersWidth, rowHeight);

            Pen pen = new Pen(this.GridColor, 1);

            Bitmap rowImg = new Bitmap(imgWidth, rowHeight);
            Graphics g = Graphics.FromImage(rowImg);
            g.DrawRectangle(pen, rFrame);
            g.FillRectangle(new SolidBrush(this.DefaultCellStyle.BackColor), rFill);
            //g.FillRectangle(new SolidBrush(this.RowHeadersDefaultCellStyle.BackColor), rowHeader);

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


        //protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        //{
        //    TextBox txt = e.Control as TextBox;
        //    if (txt != null)
        //    {
        //        txt.Name = this.CurrentCell.OwningColumn.Name.ToString().Trim();
        //        txt.Tag = this.CurrentCell.OwningColumn.Tag.ToString().Trim();
        //        if (txt.Tag.ToString().Trim() == "decimal" || txt.Tag.ToString().Trim() == "int")
        //        {
        //            txt.KeyPress -= new KeyPressEventHandler(txt_Key_Press);
        //            txt.KeyPress += new KeyPressEventHandler(txt_Key_Press);
        //        }
        //    }
        //    base.OnEditingControlShowing(e);
        //}

        //private void txt_Key_Press(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        TextBox txt = (TextBox)sender;
        //        if (txt.Tag.ToString().Trim() == "decimal")
        //        {
        //            if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
        //            {
        //                e.Handled = true;
        //            }
        //            string[] str = txt.Text.Split('.');

        //            if (e.KeyChar == '.' && str.Length > 1)
        //            {
        //                if (str[1] == "")
        //                    txt.Text = str[0] + ".00";
        //                else
        //                    txt.Text = str[0] + "." + str[1];
        //                e.Handled = true;
        //            }
        //        }
        //        if (txt.Tag.ToString().Trim() == "int")
        //        {
        //            if ((!(Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back || e.KeyChar == '.')))
        //            {
        //                e.Handled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    int h = this.RowTemplate.Height;
        //    int rh = this.ColumnHeadersHeight + 1;
        //    int lh = 100;//this.GetRowDisplayRectangle(0, false).Bottom;
        //    Rectangle r = e.ClipRectangle;//default(Rectangle);
        //    if (lh < Height)
        //    {
        //        for (int i = lh + rh; i <= this.Height; i += h)
        //        {
        //            e.Graphics.DrawRectangle(new Pen(this.DefaultCellStyle.BackColor), new Rectangle(0, i, this.Width, h));
        //        }
        //    }
        //    base.OnPaint(e);
        //}



        //protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        //{
        //    DataGridViewRow row = this.Rows[e.RowIndex];
        //    row.DefaultCellStyle.BackColor = Color.Transparent;
        //    base.OnCellFormatting(e);
        //}

        //protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(_backgroundPic, e.RowBounds);
        //    base.OnRowPrePaint(e);
        //}



        //protected override void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.RowIndex != -1 && e.ColumnIndex != -1)
        //    {
        //        DataGridViewButtonCell b = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
        //        if (b != null)
        //        {
        //            b.FlatStyle = FlatStyle.Popup;
        //            b.Style.BackColor = Color.Gray;
        //        }
        //    }
        //    base.OnCellMouseMove(e);
        //}

        //protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex != -1 && e.ColumnIndex != -1)
        //    {
        //        DataGridViewButtonCell b = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
        //        if (b != null)
        //        {
        //            b.FlatStyle = FlatStyle.Flat;
        //            b.Style.BackColor = Color.WhiteSmoke;
        //        }
        //    }
        //    base.OnCellMouseLeave(e);
        //}

        //protected override void PaintBackground(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle gridBounds)
        //{

        //    base.PaintBackground(graphics, clipBounds, gridBounds);

        //    if (((this.BackgroundImage != null)))
        //    {
        //        graphics.FillRectangle(Brushes.Black, gridBounds);
        //        graphics.DrawImage(this.BackgroundImage, gridBounds);
        //    }
        //}

        //public void SetCellsTransparent()
        //{
        //    this.EnableHeadersVisualStyles = false;

        //    this.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;

        //    this.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;

        //    foreach (DataGridViewColumn col in this.Columns)
        //    {
        //        col.DefaultCellStyle.BackColor = Color.Transparent;
        //    }
        //}

        internal void Sort(DataGridViewColumn dataGridViewColumn)
        {
            throw new NotImplementedException();
        }
    }
}
