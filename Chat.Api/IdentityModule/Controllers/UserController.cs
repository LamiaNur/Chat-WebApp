using Chat.Api.ChatModule.Hubs;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Queries;
using Chat.Api.SharedModule.Controllers;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.IdentityModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : AChatController
    {
        public UserController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy) 
            : base(hubContext, commandQueryProxy)
        {

        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync(RegisterCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }

        [HttpPost]
        [Route("profiles")]
        public async Task<IActionResult> UserProfileAsync(UserProfileQuery query)
        {
            return Ok(await GetQueryResponseAsync(query));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserProfileCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }
    }
}
