using System.Composition;
using Chat.Api.CoreModule.Enums;
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
            response.Name = command.GetType().Name;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = new()
            {
                Name = command.GetType().Name,
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
            response.Name = query.GetType().Name;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            response = new()
            {
                Name = query.GetType().Name,
                Status = ResponseStatus.Error,
                Message = e.Message
            };
        }
        return response;
    }
}