using Chat.Api.ChatModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.ChatModule.Hubs
{
    [ServiceRegister(typeof(IChatHubService), ServiceLifetime.Singleton)]
    public class ChatHubService : IChatHubService
    {
        private readonly IHubConnectionService _hubConnectionService;
        public ChatHubService(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public async Task SendAsync<T>(string userId, T message, RequestContext requestContext)
        {
            var connectionId = _hubConnectionService.GetConnectionId(userId);
            Console.WriteLine("==============Sending");
            if (string.IsNullOrEmpty(connectionId))
            {
                Console.WriteLine("ConnectionId not found");
                return;
            }
            await requestContext.HubContext.Clients.Client(connectionId).SendAsync("ReceivedChat", message);
            Console.WriteLine("==============Sent message");
        }
    }
}