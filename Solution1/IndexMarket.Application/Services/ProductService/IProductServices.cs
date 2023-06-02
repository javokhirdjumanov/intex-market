using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IProductServices
{
    ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto);
    IQueryable<ProductDto> RetrieveProducts();
    ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId);
    ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto);
    ValueTask<ProductDto> RemoveProductAsync(Guid productId);
}
