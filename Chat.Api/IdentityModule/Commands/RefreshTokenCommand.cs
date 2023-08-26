using Chat.Api.IdentityModule.Models;
using Chat.Framework.CQRS;

namespace Chat.Api.IdentityModule.Commands
{
    public class RefreshTokenCommand : ACommand
    {
        public Token Token {get; set;}
        public string AppId {get; set;} = string.Empty;
        public override void ValidateCommand()
        {
            if (Token == null) 
            {
                throw new Exception("Token not found!");
            }
            if (string.IsNullOrEmpty(Token.AccessToken))
            {
                throw new Exception("Access Token Not Set!");
            }
            if (string.IsNullOrEmpty(Token.RefreshToken))
            {
                throw new Exception("Refresh Token not set!");
            }
            if (string.IsNullOrEmpty(AppId))
            {
                throw new Exception("AppId not set");
            }
        }
    }
}