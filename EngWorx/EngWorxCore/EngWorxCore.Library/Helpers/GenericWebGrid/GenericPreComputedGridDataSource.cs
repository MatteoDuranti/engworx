using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Web.Helpers {
    /// <summary>
    /// Source wrapper for data provided by the user that is already sorted and paged. The user provides the WebGrid the rows to bind and additionally the total number of rows that 
    /// are available.
    /// </summary>
    internal sealed class GenericPreComputedGridDataSource : IWebGridDataSource {
        private readonly int _totalRows;
        private readonly IList<GenericWebGridRow> _rows;

        public GenericPreComputedGridDataSource(GenericWebGrid grid, IEnumerable<dynamic> values, int totalRows) {
            Debug.Assert(grid != null);
            Debug.Assert(values != null);

            _totalRows = totalRows;
            _rows = values.Select((value, index) => new GenericWebGridRow(grid, value: value, rowIndex: index)).ToList();
        }

        public int TotalRowCount {
            get {
                return _totalRows;
            }
        }

        public IList<GenericWebGridRow> GetRows(SortInfo sortInfo, int pageIndex) {
            // Data is already sorted and paged. Ignore parameters.
            return _rows;
        }
    }
}
