using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public interface IProductFactory
{
    ProductDto MapToProductDto(Product product);

    Product MapToProduct(
        ProductForCreationDto productForCreationDto,
        Category maybeCategory,
        ProductShape productShape);

    Product MapToProduct(
        ProductForCreationDtoRectangel productForCreationDtoRectangel,
        Category category,
        ProductShape productShape);

    void MapToProduct(Product storageProduct, ProductForModificationDto productForModificationDto);
}
