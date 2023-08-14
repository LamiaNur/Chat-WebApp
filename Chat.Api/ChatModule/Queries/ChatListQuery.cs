using Chat.Api.CoreModule.CQRS;

namespace Chat.Api.ChatModule.Queries
{
    public class ChatListQuery : AQuery
    {
        public string UserId {get; set;} = string.Empty;

        public override void ValidateQuery()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set");
            }
        }
    }
}