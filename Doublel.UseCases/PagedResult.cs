using System;
using System.Collections.Generic;
namespace Doublel.UseCases
{
    public class PagedResult<T>
        where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int ItemsPerPage { get; set; }
    }
}
