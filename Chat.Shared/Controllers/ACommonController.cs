using Chat.Framework.CQRS;
using Chat.Framework.Models;
using Chat.Framework.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Shared.Controllers
{
    public abstract class ACommonController : ControllerBase
    {
        protected readonly ICommandQueryProxy CommandQueryProxy;

        protected ACommonController(ICommandQueryProxy commandQueryProxy)
        {
            CommandQueryProxy = commandQueryProxy;
        }

        protected RequestContext GetRequestContext()
        {
            return new RequestContext
            {
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
