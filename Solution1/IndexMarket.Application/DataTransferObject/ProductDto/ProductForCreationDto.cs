namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDto(
    string? PhotoLink,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    int Depth,
    Guid Category_Id,
    string Shape);
