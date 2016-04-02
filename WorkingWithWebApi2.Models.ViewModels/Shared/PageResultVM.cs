using System;
using System.Collections.Generic;

// Thanks to Jerrie Pelser for the following code block: http://www.jerriepelser.com/blog/webapi-angular-grid-part-3

namespace WorkingWithWebApi2.Models.ViewModels.Shared
{
    /// <summary>
    /// Helper Class used for Pagination
    /// </summary>
    /// <typeparam name="T">The Type of Object that will be sent to the client in a List</typeparam>
    public class PagedResultViewModel<T>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public long TotalRecordCount { get; set; }
        public List<T> Items { get; set; }

        public PagedResultViewModel(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Items = new List<T>(items);
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = (totalRecordCount > 0) ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;
            TotalRecordCount = totalRecordCount;
        }
    }
}
