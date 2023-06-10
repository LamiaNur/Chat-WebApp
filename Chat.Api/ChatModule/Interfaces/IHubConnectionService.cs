namespace Chat.Api.ChatModule.Interfaces
{
    public interface IHubConnectionService
    {
        void AddConnection(string connectionId, string accessToken);
        void RemoveConnection(string connectionId);
        string GetConnectionId(string userId);
        string GetUserId(string connectionId);
        bool IsUserConnectedWithHub(string userId);
    }
}