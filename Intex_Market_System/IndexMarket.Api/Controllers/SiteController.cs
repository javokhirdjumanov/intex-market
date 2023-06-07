using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SiteController : ControllerBase
{
    private readonly ISiteServices siteServices;
    public SiteController(ISiteServices siteServices)
    {
        this.siteServices = siteServices;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetSites()
    {
        var site = this.siteServices.RetrieveSites();

        return Ok(site);
    }

    [AllowAnonymous]
    [HttpPut]
    public async ValueTask<ActionResult<SiteDto>> PutSiteAsync(SiteModificationDto siteModificationDto)
    {
        var modifySite = await this.siteServices
            .ModifySiteAsync(siteModificationDto);

        return Ok(modifySite);
    }
}
