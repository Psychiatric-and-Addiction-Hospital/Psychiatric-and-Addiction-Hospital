using Application.Common.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>
            (this IQueryable<T> query, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
        {
            pageNumber = Math.Max(pageNumber, 1);

            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalRecords = await query.CountAsync(ct);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var totalPages =
                (int)Math.Ceiling((double)totalRecords / pageSize);

            return new PagedResponse<T>
            {
                Items = items,

                PageNumber = pageNumber,

                PageSize = pageSize,

                TotalRecords = totalRecords,

                TotalPages = totalPages,

                HasNextPage = pageNumber < totalPages,

                HasPreviousPage = pageNumber > 1
            };
        }
    }
}
