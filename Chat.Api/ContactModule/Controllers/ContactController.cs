using Chat.Api.ContactModule.Commands;
using Chat.Api.ContactModule.Queries;
using Chat.Framework.CQRS;
using Chat.Framework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.ContactModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly ICommandQueryService _commandQueryService;
        public ContactController()
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> AddContactAsync(AddContactCommand command)
        {
            return Ok(await _commandQueryService.HandleCommandAsync(command));
        }
        [HttpPost, Route("accept-reject")]
        public async Task<IActionResult> AcceptOrRejectContactRequestAsync(AcceptOrRejectContactRequestCommand command)
        {
            return Ok(await _commandQueryService.HandleCommandAsync(command));
        }
        [HttpPost, Route("get")]
        public async Task<IActionResult> AddContactAsync(ContactQuery query)
        {
            return Ok(await _commandQueryService.HandleQueryAsync(query));
        }
    }
}