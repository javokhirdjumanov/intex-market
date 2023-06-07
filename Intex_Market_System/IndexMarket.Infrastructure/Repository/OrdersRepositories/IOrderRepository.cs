using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IOrderRepository : IBaseRepository<Order, Guid>
{
}
