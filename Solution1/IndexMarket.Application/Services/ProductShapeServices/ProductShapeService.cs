using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Paginations;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace IndexMarket.Application.Services;
public class ProductShapeService : IProductShapeService
{
    private readonly IProductShapeRepository productShapeRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    public ProductShapeService(
        IProductShapeRepository productShapeRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.productShapeRepository = productShapeRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<ProductShapeDto> RetrieveProductsShapes(QueryParametrs queryParametrs)
    {
        var productShapes = this.productShapeRepository
            .SelectAll()
            .ToPagedList(
                httpContext: this.httpContextAccessor.HttpContext,
                pageSize: queryParametrs.Page.Size,
                pageIndex: queryParametrs.Page.Index);

        var dict = new Dictionary<string, Guid>();
        foreach (var item in productShapes.ToList())
        {
            if(!dict.ContainsKey(item.Name))
                dict.Add(item.Name, item.Id);
        }

        return
            dict.Select(kv => 
                new KeyValuePair<string, Guid>(kv.Key, kv.Value))
            .AsQueryable()
            .Select(x => 
                new ProductShapeDto(x.Value, x.Key));
    }
}
