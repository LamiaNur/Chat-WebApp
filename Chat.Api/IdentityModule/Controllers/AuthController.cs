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
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        public AuthController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LoginUserAsync(LoginCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }

        [HttpPost]
        [Route("log-out")]
        public async Task<IActionResult> LogOutUserAsync(LogOutCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }

        [HttpPost]
        [Route("user-profile")]
        [Authorize]
        public async Task<IActionResult> UserProfileAsync(UserProfileQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}