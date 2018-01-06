using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace System.Web.Helpers
{
    public class GenericWebGridColumnCheckBox : GenericWebGridColumn
    {
        public bool showSelectAll { get; set; }

        public GenericWebGridColumnCheckBox()
            : base()
        {
        }

        public GenericWebGridColumnCheckBox(WebGridColumn baseColumn, bool ShowSelectAll = true)
        {
            ColumnName = baseColumn.ColumnName;
            Header = baseColumn.Header;
            Format = baseColumn.Format;
            Style = baseColumn.Style;
            CanSort = baseColumn.CanSort;
            showSelectAll = ShowSelectAll;
        }
    }
}