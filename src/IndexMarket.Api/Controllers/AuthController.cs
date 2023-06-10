using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthentoicationServices authentoicationServices;
    public AuthenticationController(IAuthentoicationServices authentoicationServices)
    { 
         this.authentoicationServices = authentoicationServices;
    }

    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> LoginAsync(
        AuthentificationDto authentificationDto)
    {
        var tokenDto = await this.authentoicationServices.LoginAsync(authentificationDto);

        return Ok(tokenDto);
    }

    [HttpPost("refresh-token")]
    public async ValueTask<ActionResult<TokenDto>> RefreshTokenAsync(
        RefreshTokenDto refreshTokenDto)
    {
        var tokenDto = await this.authentoicationServices.RefreshTokenAsync(refreshTokenDto);

        return Ok(tokenDto);
    }
}
