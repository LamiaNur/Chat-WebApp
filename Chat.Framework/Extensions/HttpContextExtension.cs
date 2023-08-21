using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Chat.Framework.Extensions
{
    public static class HttpContextExtension
    {
        public static string? GetAccessToken(this HttpContext httpContext)
        {
            return httpContext?.Request?.Headers[HeaderNames.Authorization].ToString();
        }
    }
}
