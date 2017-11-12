using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iMANTRA
{
    public partial class UcDGV : UserControl
    {
        Image image;
        public UcDGV()
        {
            InitializeComponent(); 
        }

        private void UcDGV_Load(object sender, EventArgs e)
        {
            image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\Line.png");
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            row.DefaultCellStyle.BackColor = Color.Transparent;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.Graphics.DrawImage(image, e.RowBounds);
        }
    }
}
