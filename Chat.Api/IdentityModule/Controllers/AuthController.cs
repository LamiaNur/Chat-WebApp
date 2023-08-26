using Chat.Api.ChatModule.Hubs;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.SharedModule.Controllers;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.IdentityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : AChatController
    {
        public AuthController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy) 
            : base(hubContext, commandQueryProxy)
        {

        }

        [HttpPost]
        [Route("log-in")]
        public async Task<IActionResult> LoginUserAsync(LoginCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("log-out")]
        public async Task<IActionResult> LogOutUserAsync(LogOutCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }
    }
}