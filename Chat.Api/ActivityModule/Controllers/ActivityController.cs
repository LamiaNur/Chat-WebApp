using Chat.Api.ActivityModule.Queries;
using Chat.Framework.Proxy;
using Chat.Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.ActivityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly ICommandQueryProxy _commandQueryService;
        public ActivityController()
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryProxy>();
        }

        [HttpPost, Route("last-seen")]
        public async Task<IActionResult> GetLastSeenModelAsync(LastSeenQuery query)
        {
            return Ok(await _commandQueryService.GetQueryResponseAsync(query));
        }
    }
}