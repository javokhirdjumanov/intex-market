using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileModelController : ControllerBase
{
    private readonly IFileServices fileServices;
    public FileModelController(IFileServices fileServices)
    {
        this.fileServices = fileServices;
    }

    [HttpPost]
    public async Task<ActionResult<string>> PostFile(IFormFile file)
    {
        var newFile = await this.fileServices.UploadFile(file);

        return Ok(newFile);
    }

    [HttpGet("{fileId:guid}")]
    public async Task<IActionResult> DownloadFile(Guid fileId)
    {
        var (streamModel, model) = await this.fileServices.DownloadFile(fileId);

        return File(streamModel, "application/octet-stream", model.FileName + model.Type);
    }
}
