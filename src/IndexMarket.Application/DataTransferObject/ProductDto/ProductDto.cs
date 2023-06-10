using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.DataTransferObject;
public record ProductDto(
    Guid productId,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    ProductShapeDto? Shape,
    double Height,
    double? Weight,
    int Depth,
    FileDto? File,
    string Status,
    CategoryDto? Category);
