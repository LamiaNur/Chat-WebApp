using Chat.Framework.Models;

namespace Chat.Api.ChatModule.Interfaces
{
    public interface IChatHubService
    {
        Task SendAsync<T>(string userId, T message, RequestContext requestContext);
    }
}