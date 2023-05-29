using Chat.Api.ActivityModule.Queries;
using Chat.Api.CoreModule.Interfaces;
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
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        public ActivityController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
        }

        [HttpPost, Route("last-seen")]
        public async Task<IActionResult> GetLastSeenModelAsync(LastSeenQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}