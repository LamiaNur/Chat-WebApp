using Microsoft.AspNetCore.SignalR;
using Chat.Api.ChatModule.Hubs;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.ChatModule.Interfaces
{
    public interface IChatHubService
    {
        Task SendAsync<T>(string userId, T message, RequestContext requestContext);
    }
}