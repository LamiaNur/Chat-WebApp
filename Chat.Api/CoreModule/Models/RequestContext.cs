using Chat.Api.ChatModule.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.CoreModule.Models
{
    public class RequestContext
    {
        public HttpContext? HttpContext {get; set;}
        public IHubContext<ChatHub>? HubContext {get; set;}
    }
}