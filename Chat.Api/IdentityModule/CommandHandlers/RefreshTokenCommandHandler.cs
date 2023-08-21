using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Helpers;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class RefreshTokenCommandHandler : ACommandHandler<RefreshTokenCommand>
    {
        private readonly ITokenService _tokenService;
        public RefreshTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected override async Task<CommandResponse> OnHandleAsync(RefreshTokenCommand command)
        {
            var response = command.CreateResponse();
            if (!TokenHelper.IsTokenValid(command.Token.AccessToken, _tokenService.GetTokenValidationParameters(true, true, false, true)))
            {
                throw new Exception("Invalid access token");
            }
            if (!TokenHelper.IsExpired(command.Token.AccessToken))
            {
                throw new Exception("AccessToken not expired yet!");
            }            
            var accessModel = await _tokenService.GetAccessModelByRefreshTokenAsync(command.Token.RefreshToken);
            if (accessModel == null || accessModel.AccessToken != command.Token.AccessToken)
            {
                throw new Exception("Refresh or AccessToken Error");
            }
            if (command.AppId != accessModel.AppId) 
            {
                throw new Exception("AppId Error");
            }
            if (accessModel.Expired)
            {
                await _tokenService.RevokeAllTokensByUserId(accessModel.UserId);
                throw new Exception("Suspicious Token refresh attempt");
            }
            accessModel.Expired = true;
            await _tokenService.SaveAccessModelAsync(accessModel);
            var userProfile = _tokenService.GetUserProfileFromAccessToken(command.Token.AccessToken);
            var token = await _tokenService.CreateTokenAsync(userProfile, command.AppId);
            if (token == null)
            {
                throw new Exception("Token Error");
            }
            response.SetData("Token", token);
            return response;
        }
    }
}