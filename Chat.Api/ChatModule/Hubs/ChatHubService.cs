using System.Composition;
using Chat.Api.ChatModule.Interfaces;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Models;
using Chat.Framework.Services;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.ChatModule.Hubs
{
    [Export(typeof(IChatHubService))]
    [Shared]
    public class ChatHubService : IChatHubService
    {
        private readonly ITokenService _tokenService;
        private readonly IHubConnectionService _hubConnectionService;
        public ChatHubService()
        {
            _hubConnectionService = DIService.Instance.GetService<IHubConnectionService>();
            _tokenService = DIService.Instance.GetService<ITokenService>();
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