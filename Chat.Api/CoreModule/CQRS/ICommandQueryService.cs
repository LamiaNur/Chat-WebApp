namespace Chat.Api.CoreModule.CQRS;
public interface ICommandQueryService
{
    Task<CommandResponse> HandleCommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
    Task<QueryResponse> HandleQueryAsync<TQuery>(TQuery query) where TQuery : IQuery;
}