using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.IdentityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandQueryService _commandQueryService;
        public AuthController()
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterCommand command)
        {
            return Ok(await _commandQueryService.HandleAsync(command));
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LoginUserAsync(LoginCommand command)
        {
            return Ok(await _commandQueryService.HandleAsync(command));
        }

        [HttpPost]
        [Route("log-out")]
        public async Task<IActionResult> LogOutUserAsync(LogOutCommand command)
        {
            return Ok(await _commandQueryService.HandleAsync(command));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return Ok(await _commandQueryService.HandleAsync(command));
        }

        [HttpPost]
        [Route("user-profile")]
        [Authorize]
        public async Task<IActionResult> UserProfileAsync(UserProfileQuery query)
        {
            return Ok(await _commandQueryService.HandleAsync(query));
        }
        
        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserProfileCommand command)
        {
            return Ok(await _commandQueryService.HandleAsync(command));
        }
    }
}