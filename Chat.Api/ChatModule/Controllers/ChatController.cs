using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Queries;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.ChatModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _commandService = DIService.Instance.GetService<ICommandService>();
            _queryService = DIService.Instance.GetService<IQueryService>();
            _hubContext = hubContext;
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            var context = new RequestContext();
            context.HubContext = _hubContext;
            context.HttpContext = HttpContext;
            return Ok(await _commandService.HandleCommandAsync(command, context));
        }

        [HttpPost, Route("update-status")]
        public async Task<IActionResult> UpdateChatsStatusAsync(UpdateChatsStatusCommand command)
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