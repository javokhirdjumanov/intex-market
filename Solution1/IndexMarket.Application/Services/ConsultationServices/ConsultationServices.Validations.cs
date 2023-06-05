using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;

namespace IndexMarket.Application.Services;
public partial class ConsultationServices
{
    public void ValidationConsultationId(Guid consultationId)
    {
        if(consultationId == default)
        {
            throw new ValidationException($"The given userId is invalid: {consultationId}");
        }
    }

    public void ValidationStorageConsultation(Consultation consultation, Guid consultationId)
    {
        if(consultation is null)
        {
            throw new NotFoundException($"Couldn't find user with given id: {consultationId}");
        }
    }
}
