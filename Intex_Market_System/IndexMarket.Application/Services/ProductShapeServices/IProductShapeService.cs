using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Paginations;

namespace IndexMarket.Application.Services;

public interface IProductShapeService
{
    IQueryable<ProductShapeDto> RetrieveProductsShapes(QueryParametrs queryParametrs);
}
