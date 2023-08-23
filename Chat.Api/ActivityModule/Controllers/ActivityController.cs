using Chat.Api.ActivityModule.Queries;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.SharedModule.Controllers;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.ActivityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ACommonController
    {
        public ActivityController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy) : base(hubContext, commandQueryProxy)
        {
        }

        [HttpPost, Route("last-seen")]
        public async Task<IActionResult> GetLastSeenModelAsync(LastSeenQuery query)
        {
            return Ok(await GetQueryResponseAsync(query));
        }
    }
}