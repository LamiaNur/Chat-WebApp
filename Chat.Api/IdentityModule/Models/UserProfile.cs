namespace Chat.Api.IdentityModule.Models
{
    public class UserProfile
    {
        public string Id { get; set;} = string.Empty;
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
        public string UserName {get; set;} = string.Empty;
        public DateTime BirthDay {get; set;}
        public string Email {get; set;} = string.Empty;
        public string About {get; set;} = string.Empty;
        public string ProfilePictureId {get; set;} = string.Empty;
    }
}