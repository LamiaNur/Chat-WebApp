using Chat.Api.Core.Models;

namespace Chat.Api.ActivityService.Queries
{
    public class LastSeenQuery : AQuery
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