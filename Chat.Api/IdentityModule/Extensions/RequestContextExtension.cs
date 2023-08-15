using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Framework.Services;
using Microsoft.Net.Http.Headers;
using Chat.Framework.Models;

namespace Chat.Api.IdentityModule.Extensions;
public static class RequestContextExtension
{
    private static readonly ITokenService TokenService = DIService.Instance.GetService<ITokenService>();
    public static UserProfile GetCurrentUserProfile(this RequestContext context)
    {
        var accessToken = context.HttpContext?.Request.Headers[HeaderNames.Authorization].ToString();
        return TokenService.GetUserProfileFromAccessToken(accessToken);
    }
}