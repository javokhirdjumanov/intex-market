using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using IndexMarket.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost]
        public async ValueTask<ActionResult<UserDto>> PostUserAsync(
            UserForCreationDto userForCreationDto)
        {
            var createUser = await this.userServices
                .CreateUserAsync(userForCreationDto);

            return Created("", createUser);
        }

        [AllowAnonymous]
        [HttpGet("All")]
        public IActionResult GetAllUsers()
        {
            var users = this.userServices.RetrieveUsers();

            return Ok(users);
        }

        [HttpGet("{userId:guid}")]
        [Authorize(Policy = "UserPolicy")]
        public async ValueTask<ActionResult<UserDto>> GetUserByIdAsync(
            Guid userId)
        {
            var user = await this.userServices
                .RetrieveUserByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async ValueTask<ActionResult<UserDto>> PutUserAsync(
            UserForModificationDto userForModificationDto)
        {
            var modifiedUser = await this.userServices
                .ModifyUserAsync(userForModificationDto);

            return Ok(modifiedUser);
        }

        [HttpDelete("{userId:guid}")]
        public async ValueTask<ActionResult<UserDto>> DeleteUserAsync(
            Guid userId)
        {
            var removed = await this.userServices
                .RemoveUserAsync(userId);

            return Ok(removed);
        }
    }
}
