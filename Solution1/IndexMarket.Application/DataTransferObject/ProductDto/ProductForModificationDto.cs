namespace IndexMarket.Application.DataTransferObject;
public record ProductForModificationDto(
    Guid Product_Id,
    decimal? SalePrice,
    decimal? Price,
    int? Amount,
    double? Height,
    double? Weight,
    int? Depth);
