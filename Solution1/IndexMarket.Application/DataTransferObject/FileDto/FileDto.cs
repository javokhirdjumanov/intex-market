namespace IndexMarket.Application.DataTransferObject;
public record FileDto(
    Guid Id,
    string Type,
    string FileName);
