using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IAddressService
{
    IQueryable<AddressDto> GetAddresses();
}
