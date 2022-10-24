using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace TvShowTracker.EntityFrameworkPaginateCore
{
    public static class PaginateService
    {
        public static async Task<Page<T>> PaginateAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, int count)
        {
            Page<T> result = new Page<T>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                RecordCount = count,
                Results = await query.ToListAsync()
            };
            result.PageCount = (int)Math.Ceiling((double)result.RecordCount / pageSize);
            return result;
        }
        public static async Task<Page<T>> PaginateAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, int count, string sorts)
        {
            var result = query.ApplySort(sorts);
            return await result.PaginateAsync(pageNumber, pageSize, count);
        }
        private static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string sorts)
        {
            if (!query.Any())
                return query;
            if (string.IsNullOrWhiteSpace(sorts))
            {
                return query;
            }
            var orderParams = sorts.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new System.Text.StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(".")[0];
                var objectProperty = propertyInfos
                                  .FirstOrDefault(pi => pi.Name
                                     .Equals(propertyFromQueryName,
                                     StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(".desc") ? "descending" : "asc";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return query.OrderBy(orderQuery);
        }
    }
}