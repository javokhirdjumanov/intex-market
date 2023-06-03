namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDtoRectangel(
    string? PhotoLink,
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    double Weight,
    int Depth,
    string category,
    string shape);
