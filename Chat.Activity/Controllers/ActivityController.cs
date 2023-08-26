using Chat.Activity.Api.Queries;
using Chat.Framework.Proxy;
using Chat.Shared.Contracts.Commands;
using Chat.Shared.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Activity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ACommonController
    {
        public ActivityController(ICommandQueryProxy commandQueryProxy) : base(commandQueryProxy)
        {
        }

        [HttpPost, Route("last-seen")]
        public async Task<IActionResult> GetLastSeenModelAsync(LastSeenQuery query)
        {
            return Ok(await GetQueryResponseAsync(query));
        }

        [HttpPost, Route("track")]
        public async Task<IActionResult> UpdateLastSeenAsync(UpdateLastSeenCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }
    }
}