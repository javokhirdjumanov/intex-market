using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public interface IProductFactory
{
    ProductDto MapToProductDto(Product product);
    Product MapToProduct(ProductForCreationDto productForCreationDto, Category? maybeCategory, ProductShape? Shape);
    void MapToProduct(Product storageProduct, ProductForModificationDto productForModificationDto);
}
