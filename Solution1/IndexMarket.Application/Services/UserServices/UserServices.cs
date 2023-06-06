using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public partial class UserServices : IUserServices
{
    private readonly AppDbContext context;
    private readonly IUserRepository userRepository;
    private readonly IUserFactory userFactory;

    public UserServices(
        AppDbContext context,
        IUserRepository userRepository,
        IUserFactory userFactory)
    {
        this.context = context;
        this.userRepository = userRepository;
        this.userFactory = userFactory;
    }

    public async ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        ValidateCreationUserDto(userForCreationDto);

        var newUser = this.userFactory
            .MapToUser(userForCreationDto);

        var addeduser = await this.userRepository
            .InsertAsync(newUser);

        return this.userFactory
            .MapToUserDto(addeduser);
    }
    public async ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId)
    {
        userId.IsDefault();

        var storageUser = await this.userRepository
            .SelectByIdWithDetailsAsync
            (expression: user => user.Id == userId,
             includes: new string[]
             {
                 nameof(User.Address)
             });

        ValidationStorageObject
            .ValidationGeneric<User>(storageUser, userId);

        return this.userFactory
            .MapToUserDto(storageUser);
    }
    public IQueryable<UserDto> RetrieveUsers()
    {
        var allUsers = this.userRepository
            .SelectAll();

        return allUsers
            .Select(
            user => this.userFactory.MapToUserDto(user));
    }
    public async ValueTask<UserDto> ModifyUserAsync(UserForModificationDto userForModificationDto)
    {
        userForModificationDto.userId.IsDefault();

        ValidateUserForModificationDto(userForModificationDto);

        var storageUser = await this.userRepository
            .SelectByIdWithDetailsAsync(
                expression: user => user.Id == userForModificationDto.userId,
                includes:new string[]
                {
                    nameof(User.Address)
                });

        ValidationStorageObject
            .ValidationGeneric<User>(storageUser, userForModificationDto.userId);

        this.userFactory
            .MapToUser(storageUser, userForModificationDto);

        var modifyUser = await this.userRepository
            .UpdateAsync(storageUser);

        return this.userFactory
            .MapToUserDto(modifyUser);
    }
    public async ValueTask<UserDto> RemoveUserAsync(Guid userId)
    {
        userId.IsDefault();

        var storageUser = await this.userRepository
            .SelectByIdAsync(userId);

        ValidationStorageObject
            .ValidationGeneric<User>(storageUser, userId);

        var removedUser = await this.userRepository
            .DeleteAsync(storageUser);

        return this.userFactory
            .MapToUserDto(removedUser);
    }
}
