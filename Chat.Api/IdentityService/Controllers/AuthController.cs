using System.Reflection;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICommandService _commandService;
        public AuthController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
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
    }
}