namespace IndexMarket.Application.DataTransferObject;
public record ConsultationsDto(
    Guid Id,
    string UserName,
    string PhoneNumber,
    DateTime Date);
