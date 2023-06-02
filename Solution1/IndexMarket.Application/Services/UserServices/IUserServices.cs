using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;

public interface IUserServices
{
    ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    IQueryable<UserDto> RetrieveUsers();
    ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId);
    ValueTask<UserDto> ModifyUserAsync(UserForModificationDto userForModificationDto);
    ValueTask<UserDto> RemoveUserAsync(Guid userId);
}
