using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace System.Web.Helpers
{
    public class GenericWebGridColumn : WebGridColumn
    {
        public GenericWebGridColumn(WebGridColumn baseColumn)
        {
            ColumnName = baseColumn.ColumnName;
            Header = baseColumn.Header;
            Format = baseColumn.Format;
            Style = baseColumn.Style;
            CanSort = baseColumn.CanSort;
        }

        public GenericWebGridColumn()
        {
            // TODO: Complete member initialization
        }
    }
}