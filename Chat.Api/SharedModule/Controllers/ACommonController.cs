using Chat.Api.ChatModule.Hubs;
using Chat.Framework.Models;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.SharedModule.Controllers
{
    public abstract class ACommonController : ControllerBase
    {
        protected readonly ICommandQueryProxy CommandQueryProxy;
        protected readonly IHubContext HubContext;

        protected ACommonController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy)
        {
            HubContext = (IHubContext)hubContext;
            CommandQueryProxy = commandQueryProxy;
        }

        protected RequestContext GetRequestContext()
        {
            return new RequestContext
            {
                HubContext = HubContext,
                HttpContext = HttpContext
            };
        }
    }
}
