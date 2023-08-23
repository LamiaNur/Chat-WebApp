using Chat.Api.ActivityModule.Commands;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.SharedModule.Helpers;
using Chat.Framework.Attributes;
using Chat.Framework.Extensions;
using Chat.Framework.Models;
using Chat.Framework.Proxy;

namespace Chat.Api.ActivityModule.Middlewares
{
    [ServiceRegister(typeof(LastSeenMiddleware), ServiceLifetime.Transient)]
    public class LastSeenMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;
        private readonly ICommandQueryProxy _commandQueryProxy;

        public LastSeenMiddleware(ICommandQueryProxy commandQueryProxy, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _commandQueryProxy = commandQueryProxy;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Executing LastSeenMiddleware\n");
            var accessToken = context.GetAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("AccessToken not found at LastSeenMiddleware\n");
            } 
            else
            {
                Console.WriteLine($"Access Token found : {accessToken}\n");

                if (TokenHelper.IsTokenValid(accessToken, _tokenService.GetTokenValidationParameters()))
                {
                    var userProfile = _tokenService.GetUserProfileFromAccessToken(accessToken);
                    var updateLastSeenCommand = new UpdateLastSeenCommand()
                    {
                        UserId = userProfile.Id  
                    };
                    await _commandQueryProxy.GetCommandResponseAsync(updateLastSeenCommand, new RequestContext{
                        HttpContext = context
                    });
                    Console.WriteLine("Last seen activity saved at LastSeenMiddleware\n");
                } 
                
            } 

            await next(context);
            Console.WriteLine("Returning from LastSeenMiddleware\n");
        }
    }
}