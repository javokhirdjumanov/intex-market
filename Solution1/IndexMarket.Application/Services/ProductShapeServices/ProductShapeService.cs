using IndexMarket.Application.DataTransferObject;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public class ProductShapeService : IProductShapeService
{
    private readonly IProductShapeRepository productShapeRepository;
    public ProductShapeService(IProductShapeRepository productShapeRepository)
    {
        this.productShapeRepository = productShapeRepository;
    }

    public IQueryable<ProductShapeDto> RetrieveProductsShapes()
    {
        var productShapes = this.productShapeRepository.SelectAll();

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
