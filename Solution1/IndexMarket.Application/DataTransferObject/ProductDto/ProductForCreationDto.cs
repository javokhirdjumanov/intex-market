namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDto(
    decimal? SalePrice,
    decimal Price,
    int Amount,
    double Height,
    int Depth,
    Guid File_Id,
    Guid Category_Id,
    Guid Shape_Id);
