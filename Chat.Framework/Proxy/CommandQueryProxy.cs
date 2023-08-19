using System.Composition;
using Chat.Framework.CQRS;
using Chat.Framework.Enums;
using Chat.Framework.Mediators;
using Chat.Framework.Models;
using Chat.Framework.Services;
using MongoDB.Driver;

namespace Chat.Framework.Proxy;

[Export(typeof(ICommandQueryProxy))]
[Shared]
public class CommandQueryProxy : ICommandQueryProxy
{
    private readonly IRequestMediator _requestMediator = DIService.Instance.GetService<IRequestMediator>();

    public async Task<CommandResponse> GetCommandResponseAsync<TCommand>(TCommand command,
        RequestContext? requestContext = null) where TCommand : ICommand
    {
        CommandResponse response;
        try
        {
            if (requestContext != null) command.SetRequestContext(requestContext);
            response = await _requestMediator.HandleAsync<TCommand, CommandResponse>(command);
            response = command.CreateResponse(response);
            response.Status = ResponseStatus.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = command.CreateResponse();
            response.Status = ResponseStatus.Error;
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<QueryResponse> GetQueryResponseAsync<TQuery>(TQuery query, RequestContext? requestContext = null) where TQuery : IQuery
    {
        QueryResponse response;
        try
        {
            if (requestContext != null) query.SetRequestContext(requestContext);
            response = await _requestMediator.HandleAsync<TQuery, QueryResponse>(query);
            response = query.CreateResponse(response);
            response.Status = ResponseStatus.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = query.CreateResponse();
            response.Status = ResponseStatus.Error;
            response.Message = e.Message;
        }
        return response;
    }
}