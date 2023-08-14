using Chat.Api.CoreModule.CQRS;

namespace Chat.Api.IdentityModule.Queries
{
    public class UserProfileQuery : AQuery
    {
        public List<string>? UserIds {get; set;}
        public List<string>? Emails {get; set;}
        public override void ValidateQuery()
        {
            if (UserIds == null && Emails == null) 
            {
                throw new Exception("Query Parameters not set");
            }
        }
    }
}