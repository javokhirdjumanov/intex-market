using IndexMarket.Application.DataTransferObject;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public class AddressService : IAddressService
{
    private readonly IAddressRepository addressRepository;
    public AddressService(IAddressRepository addressRepository)
    {
        this.addressRepository = addressRepository;
    }

    public IQueryable<AddressDto> GetAddresses()
    {
        var address = this.addressRepository.GetAllAddress();

        return address.Select(ad => new AddressDto(ad.Id, ad.Country, ad.City, ad.Region, ad.Street, ad.PostalCode));
    }
}
