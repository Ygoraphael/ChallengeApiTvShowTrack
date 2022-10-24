using System.Collections.Generic;

namespace TvShowTracker.EntityFrameworkPaginateCore
{
    public class Page<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }        
        public int RecordCount { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}