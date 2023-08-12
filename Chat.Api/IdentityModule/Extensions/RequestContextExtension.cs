using Chat.Api.CoreModule.Models;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Api.CoreModule.Services;
using Microsoft.Net.Http.Headers;

namespace Chat.Api.IdentityModule.Extensions;
public static class RequestContextExtesion
{
    private static ITokenService _tokenService = DIService.Instance.GetService<ITokenService>();
    public static UserProfile GetCurrentUserProfile(this RequestContext context)
    {
        var accessToken = context.HttpContext?.Request.Headers[HeaderNames.Authorization].ToString();
        return _tokenService.GetUserProfileFromAccessToken(accessToken);
    }
}