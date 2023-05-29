namespace Chat.Api.IdentityModule.Models
{
    public class TokenConfig
    {
        public string Issuer {get; set;} = string.Empty;
        public string Audience {get; set;} = string.Empty;
        public string SecretKey {get; set;} = string.Empty;
        public int ExpirationTimeInSec {get; set;}
        public int RefreshTokenExpirationTimeInSec {get; set;} 
    }
}