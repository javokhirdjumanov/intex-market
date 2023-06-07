using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IConsultationRepository 
    : IBaseRepository<Consultation, Guid>
{
}
