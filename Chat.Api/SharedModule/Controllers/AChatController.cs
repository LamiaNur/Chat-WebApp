using Chat.Api.ChatModule.Hubs;
using Chat.Framework.CQRS;
using Chat.Framework.Models;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.SharedModule.Controllers
{
    public abstract class AChatController : ControllerBase
    {
        protected readonly ICommandQueryProxy CommandQueryProxy;
        protected readonly IHubContext HubContext;

        protected AChatController(IHubContext<ChatHub> hubContext, ICommandQueryProxy commandQueryProxy)
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

        protected async Task<CommandResponse> GetCommandResponseAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            return await CommandQueryProxy.GetCommandResponseAsync(command, GetRequestContext());
        }

        protected async Task<QueryResponse> GetQueryResponseAsync<TQuery>(TQuery query) where TQuery : IQuery
        {
            return await CommandQueryProxy.GetQueryResponseAsync(query, GetRequestContext());
        }
    }
}
