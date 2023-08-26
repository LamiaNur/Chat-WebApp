using Microsoft.AspNetCore.Http;

namespace Chat.Framework.Extensions
{
    public static class HttpContextExtension
    {
        public static string? GetAccessToken(this HttpContext httpContext)
        {
            return httpContext?.Request?.Headers.Authorization;
        }
    }
}
