using Chat.Api.ChatModule.Commands;
using Chat.Api.ChatModule.Queries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.SharedModule.Controllers;
using Chat.Framework.Services;
using Chat.Framework.Models;
using Chat.Framework.Proxy;

namespace Chat.Api.ChatModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : AController
    {
        public ChatController(IHubContext<ChatHub> hubContext) : base(hubContext)
        {
            
        }

        [HttpPost, Route("send")]
        public async Task<IActionResult> SendMessageAsync(SendMessageCommand command)
        {
            return Ok(await CommandQueryProxy.GetCommandResponseAsync(command, RequestContext));
        }

        [HttpPost, Route("update-status")]
        public async Task<IActionResult> UpdateChatsStatusAsync(UpdateChatsStatusCommand command)
        {
            return Ok(await CommandQueryProxy.GetCommandResponseAsync(command, RequestContext));
        }

        [HttpPost, Route("list")]
        public async Task<IActionResult> GetChatListAsync(ChatListQuery query)
        {
            return Ok(await CommandQueryProxy.GetQueryResponseAsync(query, RequestContext));
        }

        [HttpPost, Route("get")]
        public async Task<IActionResult> GetChatsAsync(ChatQuery query)
        {
            return Ok(await CommandQueryProxy.GetQueryResponseAsync(query, RequestContext));
        }
        
    }
}