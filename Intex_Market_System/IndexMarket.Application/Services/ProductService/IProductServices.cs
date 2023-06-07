using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Paginations;

namespace IndexMarket.Application.Services;
public interface IProductServices
{
    ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto);
    ValueTask<ProductDto> CreateRectangelProductAsync(ProductForCreationDtoRectangel productForCreationDtoRectangel);
    IQueryable<ProductDto> RetrieveProducts(QueryParametrs queryParametrs);
    ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId);
    ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto);
    ValueTask<ProductDto> RemoveProductAsync(Guid productId);
}
