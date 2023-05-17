using System.Composition;
using Chat.Api.Core.Helpers;
using Chat.Api.Core.Models;
using Chat.Api.Core.Services;
using Chat.Api.IdentityService.Interfaces;
using Chat.Api.Core.Interfaces;

namespace Chat.Api.IdentityService.Commands
{
    [Export(typeof(ICommandHandler))]
    [Export("LogOutCommandHandler", typeof(ICommandHandler))]
    [Shared]
    public class LogOutCommandHandler : ACommandHandler<LogOutCommand>
    {        
        private readonly ITokenService _tokenService;
        public LogOutCommandHandler()
        {
            _tokenService = DIService.Instance.GetService<ITokenService>();
        }
        public override async Task<CommandResponse> OnHandleAsync(LogOutCommand command)
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