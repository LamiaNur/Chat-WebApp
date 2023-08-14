namespace Chat.Api.CoreModule.CQRS;
public interface ICommandQueryService
{
    Task<CommandResponse> HandleAsync(ICommand command);
    Task<QueryResponse> HandleAsync(IQuery query);
}