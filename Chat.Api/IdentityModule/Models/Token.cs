namespace Chat.Api.IdentityModule.Models
{
    public class Token
    {
        public string AccessToken {get; set;} = string.Empty;
        public string RefreshToken {get; set;} = string.Empty;
    }
}