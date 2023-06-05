using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface IConsultationServices
{
    ValueTask<ConsultationsDto> GetConsultationsByIdAsync(Guid consultationId);
    IEnumerable<ConsultationsDto> GetAllConsultations();
    ValueTask<ConsultationsDto> DeleteConsultationAsync(Guid consultationId);
}
