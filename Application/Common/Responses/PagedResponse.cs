using System.Collections.Generic;

namespace Application.Common.Responses
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public int FirstPage => 1;

        public int LastPage => TotalPages;
    }
}
