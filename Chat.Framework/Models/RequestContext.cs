using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Framework.Models
{
    public class RequestContext
    {
        public HttpContext? HttpContext { get; set; }
        public IHubContext? HubContext { get; set; }
    }
}