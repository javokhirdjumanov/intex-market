using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ConsultationController : ControllerBase
{
    private readonly IConsultationServices consultationServices;
    public ConsultationController(IConsultationServices consultationServices)
    {
        this.consultationServices = consultationServices;
    }

    [HttpGet("All")]
    public IActionResult GetAllConsultations()
    {
        var consultations = this.consultationServices.GetAllConsultations();

        return Ok(consultations);
    }

    [HttpGet("{consultationId:guid}")]
    public async ValueTask<ActionResult<ConsultationsDto>> GetConsultationByIdAsync(Guid consultationId)
    {
        var consultation = await this.consultationServices.GetConsultationsByIdAsync(consultationId);

        return Ok(consultation);
    }

    [HttpDelete("{consultationId:guid}")]
    public async ValueTask<ActionResult<ConsultationsDto>> DeleteConsultationAsync(Guid consultationId)
    {
        var removeConsultation = await this.consultationServices.DeleteConsultationAsync(consultationId);

        return Ok(removeConsultation);
    }
}
