using System.Composition;
using Chat.Framework.CQRS;
using Chat.Framework.Enums;
using Chat.Framework.Mediators;
using Chat.Framework.Services;

namespace Chat.Framework.Proxy;

[Export(typeof(ICommandQueryProxy))]
[Shared]
public class CommandQueryProxy : ICommandQueryProxy
{
    private readonly IRequestMediator _requestMediator = DIService.Instance.GetService<IRequestMediator>();

    public async Task<CommandResponse> GetCommandResponseAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        CommandResponse response;
        try
        {
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

    public async Task<QueryResponse> GetQueryResponseAsync<TQuery>(TQuery query) where TQuery : IQuery
    {
        QueryResponse response;
        try
        {
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