using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Queries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.ChatModule.Hubs;
using Chat.Framework.Services;
using Chat.Framework.Models;
using Chat.Framework.Proxy;

namespace Chat.Api.ChatModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ICommandQueryProxy _commandQueryService;
        private readonly IHubContext _hubContext;
        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _commandQueryService = DIService.Instance.GetService<ICommandQueryProxy>();
            _hubContext = (IHubContext)hubContext;
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            var context = new RequestContext();
            context.HubContext = (IHubContext)_hubContext;
            context.HttpContext = HttpContext;
            command.SetRequestContext(context);
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost, Route("update-status")]
        public async Task<IActionResult> UpdateChatsStatusAsync(UpdateChatsStatusCommand command)
        {
            return Ok(await _commandQueryService.GetCommandResponseAsync(command));
        }

        [HttpPost, Route("list")]
        public async Task<IActionResult> GetChatListAsync(ChatListQuery query)
        {
            return Ok(await _commandQueryService.GetQueryResponseAsync(query));
        }

        [HttpPost, Route("get")]
        public async Task<IActionResult> GetChatsAsync(ChatQuery query)
        {
            return Ok(await _commandQueryService.GetQueryResponseAsync(query));
        }
        
    }
}