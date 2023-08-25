using Chat.Framework.Attributes;
using Chat.Framework.CQRS;
using Chat.Framework.Enums;
using Chat.Framework.Mediators;
using Chat.Framework.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Proxy;

[ServiceRegister(typeof(ICommandQueryProxy), ServiceLifetime.Singleton)]
public class CommandQueryProxy : ICommandQueryProxy
{
    private readonly IRequestMediator _requestMediator;

    public CommandQueryProxy(IRequestMediator requestMediator)
    {
        _requestMediator = requestMediator;
    }

    public async Task<CommandResponse> GetCommandResponseAsync<TCommand>(TCommand command,
        RequestContext? requestContext = null) where TCommand : ICommand
    {
        CommandResponse response;
        try
        {
            if (requestContext != null) command.SetRequestContext(requestContext);
            if (command.GetData<bool>("FireAndForget"))
            {
                _ = Task.Factory.StartNew(async () => await _requestMediator.HandleAsync<TCommand, CommandResponse>(command));
                response = command.CreateResponse();
                response.Status = ResponseStatus.Pending;
            }
            else
            {
                response = await _requestMediator.HandleAsync<TCommand, CommandResponse>(command);
                response = command.CreateResponse(response);
                response.Status = ResponseStatus.Success;
            }
            
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