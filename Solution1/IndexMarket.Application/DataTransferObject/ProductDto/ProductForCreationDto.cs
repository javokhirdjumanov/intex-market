namespace IndexMarket.Application.DataTransferObject;
public record ProductForCreationDto(
    string? PhotoLink,
    decimal Price,
    int Amount,
    string Frame,
    double Height,
    double? Weight);
