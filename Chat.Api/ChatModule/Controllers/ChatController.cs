using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Queries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.ChatModule.Hubs;
using Chat.Framework.CQRS;
using Chat.Framework.Services;
using Chat.Framework.Models;

namespace Chat.Api.ChatModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ICommandQueryService _commandQueryService;
        private readonly IHubContext _hubContext;
        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
            _hubContext = (IHubContext)hubContext;
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            var context = new RequestContext();
            context.HubContext = (IHubContext)_hubContext;
            context.HttpContext = HttpContext;
            command.SetRequestContext(context);
            return Ok(await _commandQueryService.HandleCommandAsync(command));
        }

        [HttpPost, Route("update-status")]
        public async Task<IActionResult> UpdateChatsStatusAsync(UpdateChatsStatusCommand command)
        {
            return Ok(await _commandQueryService.HandleCommandAsync(command));
        }

        [HttpPost, Route("list")]
        public async Task<IActionResult> GetChatListAsync(ChatListQuery query)
        {
            return Ok(await _commandQueryService.HandleQueryAsync(query));
        }

        [HttpPost, Route("get")]
        public async Task<IActionResult> GetChatsAsync(ChatQuery query)
        {
            return Ok(await _commandQueryService.HandleQueryAsync(query));
        }
        
    }
}