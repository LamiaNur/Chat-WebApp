namespace Chat.Api.IdentityService.Models
{
    public class Token
    {
        public string AccessToken {get; set;} = string.Empty;
        public string RefreshToken {get; set;} = string.Empty;
    }
}