using Microsoft.AspNetCore.SignalR;
using Chat.Api.ChatModule.Hubs;

namespace Chat.Api.ChatModule.Interfaces
{
    public interface IChatHubService
    {
        IHubContext<ChatHub> _hubContext {get; set;}
        Task SendAsync<T>(string userId, T message);
    }
}