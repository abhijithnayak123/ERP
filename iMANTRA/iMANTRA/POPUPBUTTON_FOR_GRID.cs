using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace iMANTRA
{
    public class POPUPBUTTON_FOR_GRID : DataGridViewButtonColumn
    {
        public TextBox pText = null;
        public string frmName = "";
        public string PTextName = "";
        private string _tbl_nm = "";
        private string _reftbltran_cd = "";
        private string _query_con = "";

       

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
        //    //imageVal = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\hover1.png");
        //    //this.BackgroundImage = imageVal;
        //    this.DefaultCellStyle.BackColor = Color.Gray;
        //    // this.BackgroundImageLayout = ImageLayout.Stretch;
        //    this.FlatStyle = FlatStyle.Popup;
        //   // base.OnMouseMove(e);
        //}

        //protected override void OnCellMouseLeave(DataGridViewCellMouseEventArgs e)
        //{
        //    //imageVal = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"IMAGE\mouseleave.png");
        //    //this.BackgroundImage = imageVal;
        //    this.DefaultCellStyle.BackColor = Color.Transparent;
        //    //  this.BackgroundImageLayout = ImageLayout.Stretch;
        //    this.FlatStyle = FlatStyle.Flat;
        //   // base.OnMouseLeave(e);
        //}    
    }
}
