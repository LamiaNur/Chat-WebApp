using Chat.Api.ActivityModule.Queries;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.ActivityModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly ICommandQueryService _commandQueryService;
        public ActivityController()
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        }

        [HttpPost, Route("last-seen")]
        public async Task<IActionResult> GetLastSeenModelAsync(LastSeenQuery query)
        {
            return Ok(await _commandQueryService.HandleQueryAsync(query));
        }
    }
}