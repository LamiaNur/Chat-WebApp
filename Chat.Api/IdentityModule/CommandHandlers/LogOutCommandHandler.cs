using System.Composition;
using Chat.Api.CoreModule.CQRS;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;
using Chat.Api.IdentityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;

namespace Chat.Api.IdentityModule.CommandHandlers
{
    [Export("LogOutCommandHandler", typeof(IRequestHandler))]
    [Shared]
    public class LogOutCommandHandler : ACommandHandler<LogOutCommand>
    {        
        private readonly ITokenService _tokenService;
        public LogOutCommandHandler()
        {
            _tokenService = DIService.Instance.GetService<ITokenService>();
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