namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDtoRectangel(
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    double Weight,
    int Depth,
    Guid File_Id,
    Guid Category_Id,
    Guid Shape_Id);
