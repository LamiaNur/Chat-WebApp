using Chat.Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chat.Api.Core.Services;
using Chat.Api.ChatService.Commands;
using Chat.Api.ChatService.Queries;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        public ChatController()
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            return Ok(await _commandService.HandleCommandAsync(command));
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> GetChatListAsync(ChatListQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }

        [HttpPost, Route("get")]
        public async Task<IActionResult> GetChatsAsync(ChatQuery query)
        {
            return Ok(await _queryService.HandleQueryAsync(query));
        }
        
    }
}