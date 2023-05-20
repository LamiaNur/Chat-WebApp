using Chat.Api.Core.Models;

namespace Chat.Api.IdentityService.Queries
{
    public class UserProfileQuery : AQuery
    {
        public string? UserId {get; set;} = string.Empty;
        public string? Email {get; set;} = string.Empty;
        public override void ValidateQuery()
        {
            if (string.IsNullOrEmpty(UserId) && string.IsNullOrEmpty(Email)) 
            {
                throw new Exception("Query Parameters not set");
            }
        }
    }
}