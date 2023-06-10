using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Paginations;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;

public interface IProductShapeService
{
    IQueryable<ProductShapeDto> RetrieveProductsShapes(QueryParametrs queryParametrs);
}
