using Chat.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chat.Api.Core.Services;
using Chat.Api.ContactService.Commands;
using Chat.Api.ContactService.Queries;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        public ContactController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddContactAsync(AddContactCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }
        [HttpPost, Route("accept-reject")]
        public async Task<IActionResult> AcceptOrRejectContactRequestAsync(AcceptOrRejectContactRequestCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }
        [HttpPost, Route("get")]
        public async Task<IActionResult> AddContactAsync(ContactQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
    }
}