using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;

namespace IndexMarket.Infrastructure.Repository;
public sealed class ConsultationRepository 
    : BaseRepository<Consultation, Guid>, IConsultationRepository
{
    public ConsultationRepository(AppDbContext appDbContext) 
        : base(appDbContext)
    { }
}
