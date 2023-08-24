using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Queries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.SharedModule.Controllers;
using Chat.Framework.Proxy;

namespace Chat.Api.ChatModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : AChatController
    {
        public ChatController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy) 
            : base(hubContext, commandQueryProxy)
        {
            
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }

        [HttpPost, Route("update-status")]
        public async Task<IActionResult> UpdateChatsStatusAsync(UpdateChatsStatusCommand command)
        {
            return Ok(await GetCommandResponseAsync(command));
        }

        [HttpPost, Route("list")]
        public async Task<IActionResult> GetChatListAsync(ChatListQuery query)
        {
            return Ok(await GetQueryResponseAsync(query));
        }

        [HttpPost, Route("get")]
        public async Task<IActionResult> GetChatsAsync(ChatQuery query)
        {
            return Ok(await GetQueryResponseAsync(query));
        }
        
    }
}