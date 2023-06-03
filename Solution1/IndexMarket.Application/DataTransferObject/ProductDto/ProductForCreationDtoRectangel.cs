namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDtoRectangel(
    string? PhotoLink,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    double Weight,
    int Depth,
    Guid Category_Id,
    Guid Shape_Id);
