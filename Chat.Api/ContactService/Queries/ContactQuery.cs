using Chat.Api.Core.Models;

namespace Chat.Api.ContactService.Queries
{
    public class ContactQuery : AQuery
    {
        public string UserId {get; set;} = string.Empty;
        public bool IsRequestContacts {get; set;}
        public bool IsPendingContacts {get; set;}

        public override void ValidateQuery()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("User id not set");
            }
        }
    }
}