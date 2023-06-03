using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;

public interface IProductShapeService
{
    IQueryable<ProductShapeDto> RetrieveProductsShapes();
}
