using System.Composition;
using Chat.Api.CoreModule.Constants;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS;

[Export(typeof(ICommandQueryService))]
[Shared]
public class CommandQueryService : ICommandQueryService
{
    private readonly IRequestMediator _requestMediator = DIService.Instance.GetService<IRequestMediator>();

    public async Task<CommandResponse> HandleCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        CommandResponse response;
        try
        {
            response = await _requestMediator.HandleAsync<TCommand, CommandResponse>(command);
            response.Status = ResponseStatus.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = new()
            {
                Status = ResponseStatus.Error,
                Message = e.Message
            };
        }
        return response;
    }

    public async Task<QueryResponse> HandleQueryAsync<TQuery>(TQuery query) where TQuery : IQuery
    {
        QueryResponse response;
        try
        {
            response = await _requestMediator.HandleAsync<TQuery, QueryResponse>(query);
            response.Status = ResponseStatus.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = new()
            {
                Status = ResponseStatus.Error,
                Message = e.Message
            };
        }
        return response;
    }
}