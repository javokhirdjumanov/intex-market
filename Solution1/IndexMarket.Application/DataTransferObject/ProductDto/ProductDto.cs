using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.DataTransferObject;
public record ProductDto(
    string? PhotoLink,
    decimal Price,
    int Amount,
    string Frame,
    double Height,
    double? Weight,
    CategoryDto Category,
    string ProductType);
