using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public class UserRepository 
    : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
