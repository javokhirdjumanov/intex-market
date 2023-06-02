using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Enums;
using IndexMarket.Infrastructure.Auth;

namespace IndexMarket.Application.Services;
public class UserFactory : IUserFactory
{
    private readonly IPasswordHasher passwordHasher;
    public UserFactory(IPasswordHasher passwordHasher)
        => this.passwordHasher = passwordHasher;

    public User MapToUser(UserForCreationDto userForCreationDto)
    {
        string randomSalt = Guid.NewGuid().ToString();

        return new User
        {
            FirstName = userForCreationDto.FirstName,
            LastName = userForCreationDto.LastName,
            PhoneNumber = userForCreationDto.PhoneNumber,
            Email = userForCreationDto.Email,

            Address = new Address
            {
                Country = userForCreationDto.Country,
                City = userForCreationDto.City,
                Region = userForCreationDto.Region,
                Street = userForCreationDto.Street,
                PostalCode = userForCreationDto.PostalCode,
            },

            Salt = randomSalt,

            PasswordHash = this.passwordHasher.Encrypt(
                password: userForCreationDto.Password,
                salt: randomSalt),

            Role = UserRoles.Admin,
        };
    }

    public void MapToUser(User storageUser, UserForModificationDto userForModificationDto)
    {
        storageUser.FirstName = userForModificationDto.firstName ?? storageUser.FirstName;
        storageUser.LastName = userForModificationDto.lastName;
        storageUser.PhoneNumber = userForModificationDto.phoneNumber ?? storageUser.PhoneNumber;
        storageUser.UpdatedAt = DateTime.UtcNow;

        storageUser.Address = storageUser.Address ?? new Address();
        storageUser.Address.Country = userForModificationDto.country ?? storageUser.Address.Country;
        storageUser.Address.City = userForModificationDto.city;
        storageUser.Address.Region = userForModificationDto.region;
        storageUser.Address.Street = userForModificationDto.street;
        storageUser.Address.PostalCode = userForModificationDto.postalCode;
    }

    public UserDto MapToUserDto(User user)
    {
        AddressDto? addressDto = default;

        if(user.Address is not null)
        {
            addressDto = new AddressDto(
                Country: user.Address.Country,
                City: user.Address.City,
                Region: user.Address.Region,
                Street: user.Address.Street,
                PostalCode: user.Address.PostalCode);
        }

        return new UserDto(
            id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            PhoneNumber: user.PhoneNumber,
            Email: user.Email,
            Role: user.Role,
            address: addressDto);
    }
}
