using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IProductServices
{
    ValueTask<ProductDto> CreateCircleProductAsync(ProductForCreationDto productCreationDto);
    ValueTask<ProductDto> CreateRectangelProductAsync(ProductForCreationDtoRectangel productForCreationDtoRectangel);
    IQueryable<ProductDto> RetrieveProducts();
    ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId);
    ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto);
    ValueTask<ProductDto> RemoveProductAsync(Guid productId);
}
