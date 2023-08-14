namespace Chat.Api.CoreModule.CQRS;

public interface IQueryMediator
{
    Task<QueryResponse> HandleAsync(IQuery command);
}