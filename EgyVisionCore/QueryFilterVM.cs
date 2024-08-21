using System;
using System.Collections.Generic;

namespace EgyVisionCore
{
    [Serializable]
    public class QueryFilterVM
    {
        public QueryFilterVM()
        {
            FilterFields = new List<FilterFieldsVM>();
        }
        public List<FilterFieldsVM> FilterFields { get; set; }
        public int PageIndex { get; set; }
        public int StartIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderByReversed { get; set; }

        public int TotalCount { get; set; }
    }

    public class FilterFieldsVM
    {
        public string FilterColumn { get; set; }
        public string FilterValue { get; set; }
    }
}
