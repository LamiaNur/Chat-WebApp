using Chat.Framework.CQRS;

namespace Chat.Activity.Queries
{
    public class LastSeenQuery : AQuery
    {
        public List<string>? UserIds { get; set; }

        public override void ValidateQuery()
        {
            if (UserIds == null || !UserIds.Any())
            {
                throw new Exception("UserId not set");
            }
        }
    }
}