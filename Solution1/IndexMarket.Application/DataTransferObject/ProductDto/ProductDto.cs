using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.DataTransferObject;
public record ProductDto(
    string? PhotoLink,
    decimal? OldPrice,
    decimal NewPrice,
    int Amount,
    string Shape,
    double Height,
    double? Weight,
    int Depth,
    string Status,
    CategoryDto Category);
