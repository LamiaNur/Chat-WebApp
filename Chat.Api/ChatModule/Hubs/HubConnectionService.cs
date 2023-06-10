using Chat.Api.ChatModule.Interfaces;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Interfaces;
using System.Composition;

namespace Chat.Api.ChatModule.Hubs
{
    [Export(typeof(IHubConnectionService))]
    [Shared]
    public class HubConnectionService : IHubConnectionService
    {
        private readonly Dictionary<string, string> connectionIdUserIdMapper;
        private readonly Dictionary<string, string> userIdConnectionIdMapper;
        private readonly ITokenService _tokenService;

        public HubConnectionService()
        {
             _tokenService = DIService.Instance.GetService<ITokenService>();
            connectionIdUserIdMapper = new Dictionary<string, string>();
            userIdConnectionIdMapper = new Dictionary<string, string>();
        }

        public void AddConnection(string connectionId, string accessToken)
        {
            var userProfile = _tokenService.GetUserProfileFromAccessToken(accessToken);
            var userId = userProfile.Id;
            if (userIdConnectionIdMapper.ContainsKey(userId))
            {
                var prevConnectionId = userIdConnectionIdMapper[userId];
                if (connectionIdUserIdMapper.ContainsKey(prevConnectionId))
                {
                    connectionIdUserIdMapper.Remove(prevConnectionId);
                }
            }
            userIdConnectionIdMapper[userId] = connectionId;
            connectionIdUserIdMapper[connectionId] = userId;
        }

        public string GetConnectionId(string userId)
        {
            return userIdConnectionIdMapper[userId];
        }

        public string GetUserId(string connectionId)
        {
            return connectionIdUserIdMapper[connectionId];
        }

        public void RemoveConnection(string connectionId)
        {
            var userId = GetUserId(connectionId);
            if (!string.IsNullOrEmpty(userId))
            {
                connectionIdUserIdMapper.Remove(userId);
            }
            if (connectionIdUserIdMapper.ContainsKey(connectionId))
            {
                connectionIdUserIdMapper.Remove(connectionId);
            }
        }

        public bool IsUserConnectedWithHub(string userId)
        {
            return userIdConnectionIdMapper.ContainsKey(userId);
        }
    }
}