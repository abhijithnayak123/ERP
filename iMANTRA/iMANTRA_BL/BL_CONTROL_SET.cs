using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iMANTRA_BL
{
    public class BL_CONTROL_SET
    {
        private string _back_color, _font_color, _uc_color, _tab_head_color, _on_hover_color, _grid_color, _tab_back_color, _tab_font_color, _on_focus, _font_size, _font_family, _tab_family, _tab_size, _neg_stk, _qty_decimal_pt, _rate_decimal_pt;

        public string rate_dec
        {
            get { return _rate_decimal_pt; }
            set { _rate_decimal_pt = value; }
        }

        public string qty_dec
        {
            get { return _qty_decimal_pt; }
            set { _qty_decimal_pt = value; }
        }

        public string neg_stk
        {
            get { return _neg_stk; }
            set { _neg_stk = value; }
        }

        public string Tab_size
        {
            get { return _tab_size; }
            set { _tab_size = value; }
        }

        public string Tab_family
        {
            get { return _tab_family; }
            set { _tab_family = value; }
        }

        public string Font_family
        {
            get { return _font_family; }
            set { _font_family = value; }
        }

        public string Font_size
        {
            get { return _font_size; }
            set { _font_size = value; }
        }

        public string On_focus
        {
            get { return _on_focus; }
            set { _on_focus = value; }
        }

        public string Back_color
        {
            get { return _back_color; }
            set { _back_color = value; }
        }

        public string Font_color
        {
            get { return _font_color; }
            set { _font_color = value; }
        }

        public string Uc_color
        {
            get { return _uc_color; }
            set { _uc_color = value; }
        }

        public string Tab_head_color
        {
            get { return _tab_head_color; }
            set { _tab_head_color = value; }
        }

        public string On_hover_color
        {
            get { return _on_hover_color; }
            set { _on_hover_color = value; }
        }

        public string Grid_color
        {
            get { return _grid_color; }
            set { _grid_color = value; }
        }

        public string Tab_back_color
        {
            get { return _tab_back_color; }
            set { _tab_back_color = value; }
        }

        public string Tab_font_color
        {
            get { return _tab_font_color; }
            set { _tab_font_color = value; }
        }
    }
}
