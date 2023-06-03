using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.DataTransferObject;
public record ProductDto(
    Guid productId,
    string? PhotoLink,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    ProductShapeDto? Shape,
    double Height,
    double? Weight,
    int Depth,
    string Status,
    CategoryDto? Category);
