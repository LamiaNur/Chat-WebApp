using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Mediators;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [ServiceRegister(typeof(IRequestHandler), ServiceLifetime.Singleton)]
    public class LogOutCommandHandler : ACommandHandler<LogOutCommand>
    {        
        private readonly ITokenService _tokenService;
        public LogOutCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        protected override async Task<CommandResponse> OnHandleAsync(LogOutCommand command)
        {
            var response = command.CreateResponse();
            if (!await _tokenService.RevokeAllTokenByAppIdAsync(command.AppId))
            {
                throw new Exception("Log out error");
            }
            response.Message = "Logged out successfully!!";
            return response;
        }
    }
}