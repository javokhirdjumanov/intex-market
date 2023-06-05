namespace IndexMarket.Application.DataTransferObject;
public record OrderCreationDto(
    Guid Product_Id,
    Guid User_Id,
    Guid Address_Id);
