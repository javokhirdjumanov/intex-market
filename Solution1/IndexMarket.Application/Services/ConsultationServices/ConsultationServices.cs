using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Application.Services;
public class ConsultationServices : IConsultationServices
{
    private readonly IConsultationRepository consultationRepository;
    public ConsultationServices(IConsultationRepository consultationRepository)
    {
        this.consultationRepository = consultationRepository;
    }

    public IEnumerable<ConsultationsDto> GetAllConsultations()
    {
        var consultations = this.consultationRepository
            .SelectAll()
            .Include(c => c.Order)
            .Include(o => o.Order.User)
            .ToList();

        return consultations.Select(x => 
            new ConsultationsDto(
                x.Id,
                x.Order.User.FirstName,
                x.Order.User.PhoneNumber,
                x.Order.CreatedAt));
    }

    public async ValueTask<ConsultationsDto> GetConsultationsByIdAsync(Guid consultationId)
    {
        consultationId.IsDefault();

        var consultation = await this.consultationRepository
            .SelectByIdWithDetailsAsync(
            expression: con => con.Id == consultationId,
            includes: new string[]
            {
                $"{nameof(Consultation.Order)}.{nameof(Order.User)}"
            });

        ValidationStorageObject
            .ValidationGeneric<Consultation>(consultation, consultationId);

        return new ConsultationsDto(
            consultation.Id,
            consultation.Order.User.FirstName,
            consultation.Order.User.PhoneNumber,
            consultation.Order.CreatedAt);
    }

    public async ValueTask<ConsultationsDto> DeleteConsultationAsync(Guid consultationId)
    {
        consultationId.IsDefault();

        var consultation = await this.consultationRepository
            .SelectByIdWithDetailsAsync(
            expression: con => con.Id == consultationId,
            includes: new string[]
            {
                $"{nameof(Consultation.Order)}.{nameof(Order.User)}"
            });

        ValidationStorageObject
            .ValidationGeneric<Consultation>(consultation, consultationId);

        var removeConsultation = await this.consultationRepository
            .DeleteAsync(consultation);

        return new ConsultationsDto(consultation.Id,
            consultation.Order.User.FirstName,
            consultation.Order.User.PhoneNumber,
            consultation.Order.CreatedAt);
    }
}
