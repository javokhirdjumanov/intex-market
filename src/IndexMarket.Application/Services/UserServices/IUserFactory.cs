using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public interface IUserFactory
{
    UserDto MapToUserDto(User user);
    User MapToUser(UserForCreationDto userForCreationDto);
    void MapToUser(User storageUser, UserForModificationDto userForCreationDto);
}
