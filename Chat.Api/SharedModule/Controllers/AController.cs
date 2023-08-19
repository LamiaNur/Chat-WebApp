using Chat.Api.ChatModule.Hubs;
using Chat.Framework.Models;
using Chat.Framework.Proxy;
using Chat.Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.SharedModule.Controllers
{
    public abstract class AController : ControllerBase
    {
        protected readonly ICommandQueryProxy CommandQueryProxy = DIService.Instance.GetService<ICommandQueryProxy>();
        protected readonly IHubContext HubContext;
        protected RequestContext RequestContext;

        protected AController(IHubContext<ChatHub> hubContext)
        {
            HubContext = (IHubContext)hubContext;
            RequestContext = new RequestContext
            {
                HttpContext = HttpContext,
                HubContext = HubContext
            };
        }
    }
}
