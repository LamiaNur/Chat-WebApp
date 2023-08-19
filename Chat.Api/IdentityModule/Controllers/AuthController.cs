using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Queries;
using Chat.Framework.Proxy;
using Chat.Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.IdentityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandQueryProxy _commandQueryService;
        public AuthController()
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryProxy>();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LoginUserAsync(LoginCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("log-out")]
        public async Task<IActionResult> LogOutUserAsync(LogOutCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("user-profile")]
        [Authorize]
        public async Task<IActionResult> UserProfileAsync(UserProfileQuery query)
        {
            return Ok(await _commandQueryService.GetQueryResponseAsync(query));
        }
        
        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserProfileCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }
    }
}