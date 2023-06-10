using IndexMarket.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace IndexMarket.Application.Paginations;
public static class IQuerableEX
{
    private static int max_page_size = 100;
    private static string pagination_key = "X-Pagination";

    public static IQueryable<T> ToPagedList<T>(
        this IQueryable<T> source,
        HttpContext httpContext,
        int pageSize,
        int pageIndex)
    {
        if(pageIndex <= 0 || pageIndex <= 0)
        {
            throw new ValidationException("Page size or index should be greater than 0");
        }

        if(pageSize > max_page_size)
        {
            throw new ValidationException(
                $"Page size should be less than {max_page_size}");
        }

        var paginationMetadata = new PaginationMetadata(
            totalCount: source.Count(),
            currentPage: pageIndex,
            pageCount: pageSize);

        httpContext.Response.Headers[pagination_key] = JsonSerializer.Serialize(paginationMetadata);

        return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }
}
