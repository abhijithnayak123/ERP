using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using iMANTRA_BL;

namespace iMANTRA
{
    #region GridViewCheckBoxColumn

    [System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.DataGridViewCheckBoxColumn))]
    public class GridViewCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        #region Constructor

        bool flg = false;
        public GridViewCheckBoxColumn()
        {
            DatagridViewCheckBoxHeaderCell datagridViewCheckBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();

            this.HeaderCell = datagridViewCheckBoxHeaderCell;
            // this.Width = 50;

            //this.DataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grvList_CellFormatting);
            datagridViewCheckBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(datagridViewCheckBoxHeaderCell_OnCheckBoxClicked);

        }

        #endregion

        #region Methods

        void datagridViewCheckBoxHeaderCell_OnCheckBoxClicked(int columnIndex, bool state)
        {
            flg = false;
            DataGridView.RefreshEdit();

           // MessageBox.Show(this.DataGridView.Columns[columnIndex].Name);
            if (DataGridView.CurrentRow.Cells["view_access"].Value != null && DataGridView.CurrentRow.Cells["view_access"].Value.ToString() != "")
            {
                if (DataGridView.CurrentRow.Cells["view_access"].Value.ToString() == "1")
                {
                    DataGridView.CurrentRow.Cells["view_access"].Value = true;
                }
                else if (DataGridView.CurrentRow.Cells["view_access"].Value.ToString() == "0")
                {
                    DataGridView.CurrentRow.Cells["view_access"].Value = false;
                }
            }
            if (DataGridView.Columns[columnIndex].Name.ToLower() == "create_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "edit_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "delete_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "print_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "approve_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "plan_access" || DataGridView.Columns[columnIndex].Name.ToLower() == "exc_access")
            {
                if (DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value != null && DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value.ToString() != "")
                {
                    if (DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value.ToString() == "1")
                    {
                        DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value = true;
                    }
                    else if (DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value.ToString() == "0")
                    {
                        DataGridView.CurrentRow.Cells[DataGridView.Columns[columnIndex].Name.ToLower()].Value = false;
                    }
                }
                if (!bool.Parse(DataGridView.CurrentRow.Cells["view_access"].Value.ToString()))
                {
                    flg = true;
                    AutoClosingMessageBox.Show("Sorry!! Please Select View Access rights before selecting " + DataGridView.Columns[columnIndex].Name.ToLower(),"Access Rights",3000);
                }
            }
           
            if (!flg)
            {
                foreach (DataGridViewRow row in this.DataGridView.Rows)
                {
                    if (!row.Cells[columnIndex].ReadOnly)
                    {
                        row.Cells[columnIndex].Value = state;
                    }
                    if (DataGridView.Columns[columnIndex].Name.ToLower() == "view_access")
                    {
                        DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        if (!bool.Parse(row.Cells["view_access"].Value.ToString()))
                        {
                            foreach (DataGridViewColumn col in DataGridView.Columns)
                            {
                                if (col.Name.Trim().ToLower() == "create_access" || col.Name.Trim().ToLower() == "edit_access" || col.Name.Trim().ToLower() == "delete_access" || col.Name.Trim().ToLower() == "print_access" || col.Name.Trim().ToLower() == "approve_access" || col.Name.Trim().ToLower() == "exc_access" || col.Name.Trim().ToLower() == "plan_access")
                                {
                                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[col.Name];
                                    if (chk != null)
                                    {
                                        chk.Value = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            DataGridView.RefreshEdit();
        }
        #endregion
    }

    #endregion

    #region DatagridViewCheckBoxHeaderCell

    public delegate void CheckBoxClickedHandler(int columnIndex, bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(int columnIndex, bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }
    class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
        System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        public event CheckBoxClickedHandler OnCheckBoxClicked;

        public DatagridViewCheckBoxHeaderCell()
        {
        }

        protected override void Paint(System.Drawing.Graphics graphics,
        System.Drawing.Rectangle clipBounds,
        System.Drawing.Rectangle cellBounds,
        int rowIndex,
        DataGridViewElementStates dataGridViewElementState,
        object value,
        object formattedValue,
        string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
            dataGridViewElementState, value,
            formattedValue, errorText, cellStyle,
            advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X + (cellBounds.Width) - (s.Width);
            p.Y = cellBounds.Location.Y +
            (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.
                CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.
                CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, _cbState);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(e.ColumnIndex, _checked);
                    this.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }

    }

    #endregion

    #region ColumnSelection

    class DataGridViewColumnSelector
    {
        // the DataGridView to which the DataGridViewColumnSelector is attached
        private DataGridView mDataGridView = null;
        // a CheckedListBox containing the column header text and checkboxes
        private CheckedListBox mCheckedListBox;
        // a ToolStripDropDown object used to show the popup
        private ToolStripDropDown mPopup;

        /// <summary>
        /// The max height of the popup
        /// </summary>
        public int MaxHeight = 300;
        /// <summary>
        /// The width of the popup
        /// </summary>
        public int Width = 350;

        /// <summary>
        /// Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
        /// </summary>
        public DataGridView DataGridView
        {
            get { return mDataGridView; }
            set
            {
                // If any, remove handler from current DataGridView 
                if (mDataGridView != null) mDataGridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
                // Set the new DataGridView
                mDataGridView = value;
                // Attach CellMouseClick handler to DataGridView
                if (mDataGridView != null) mDataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
            }
        }

        // When user right-clicks the cell origin, it clears and fill the CheckedListBox with
        // columns header text. Then it shows the popup. 
        // In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
        // DataGridView columns (column additions or name changes and so on).
        void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                mCheckedListBox.Items.Clear();
                foreach (DataGridViewColumn c in mDataGridView.Columns)
                {
                    mCheckedListBox.Items.Add(c.HeaderText, c.Visible);
                }
                int PreferredHeight = (mCheckedListBox.Items.Count * 16) + 7;
                mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
                mCheckedListBox.Width = this.Width;
                mPopup.Show(mDataGridView.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        // The constructor creates an instance of CheckedListBox and ToolStripDropDown.
        // the CheckedListBox is hosted by ToolStripControlHost, which in turn is
        // added to ToolStripDropDown.
        public DataGridViewColumnSelector()
        {
            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            mControlHost.AutoSize = false;

            mPopup = new ToolStripDropDown();
            mPopup.Padding = Padding.Empty;
            mPopup.Items.Add(mControlHost);
        }

        public DataGridViewColumnSelector(DataGridView dgv)
            : this()
        {
            this.DataGridView = dgv;
        }

        // When user checks / unchecks a checkbox, the related column visibility is 
        // switched.
        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
        }
    }

    #endregion
}
