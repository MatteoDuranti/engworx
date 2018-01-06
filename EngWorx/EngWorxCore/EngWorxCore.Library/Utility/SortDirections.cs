using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;

namespace EngWorxCore.Library
{
    public partial class Utility
    {
        public static SortDirection getSortDirection(string sortDir)
        {
            return (SortDirection)(sortDir == "ASC" ? SortDirection.Ascending : SortDirection.Descending);
        }
    }
}
