using System.Collections.Generic;

namespace System.Web.Helpers {
    internal interface IWebGridDataSource {
        int TotalRowCount { get; }

        IList<GenericWebGridRow> GetRows(SortInfo sortInfo, int pageIndex);
    }
}
