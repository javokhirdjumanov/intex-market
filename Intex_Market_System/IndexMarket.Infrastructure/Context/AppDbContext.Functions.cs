using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Infrastructure.Context;
public partial class AppDbContext
{
    [DbFunction("get_adresses", Schema = "public")]
    public IQueryable<get_address_model> GetAllAddress()
        => FromExpression(() => GetAllAddress());
}
