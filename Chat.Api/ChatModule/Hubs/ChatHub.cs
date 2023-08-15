using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Chat.Api.ChatModule.Interfaces;
using Chat.Framework.Services;

namespace Chat.Api.ChatModule.Hubs
{
    public class ChatHub : Hub
    {
        public readonly IHubConnectionService _hubConnectionService;
        public ChatHub()
        {
            _hubConnectionService = DIService.Instance.GetService<IHubConnectionService>();
        }
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var accessToken = Context?.GetHttpContext()?.Request?.Query["access_token"].ToString();
            Console.WriteLine($"Connected....Connection Id : {connectionId}");
            Console.WriteLine($"Connected....AccessToken : {accessToken}");
            if (accessToken != null) 
                _hubConnectionService.AddConnection(connectionId, accessToken);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            Console.WriteLine($"Disconnected...Connection Id : {connectionId}");
            _hubConnectionService.RemoveConnection(connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}