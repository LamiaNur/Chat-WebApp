using Chat.Framework.CQRS;
using Chat.Framework.Models;

namespace Chat.Framework.Proxy;
public interface ICommandQueryProxy
{
    Task<CommandResponse> GetCommandResponseAsync<TCommand>(TCommand command, RequestContext? requestContext = null) where TCommand : ICommand;
    Task<QueryResponse> GetQueryResponseAsync<TQuery>(TQuery query, RequestContext? requestContext = null) where TQuery : IQuery;
}