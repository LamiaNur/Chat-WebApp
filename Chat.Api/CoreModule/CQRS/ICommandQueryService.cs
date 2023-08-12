using Chat.Api.CoreModule.Models;
namespace Chat.Api.CoreModule.Interfaces;
public interface ICommandQueryService
{
    Task<CommandResponse> HandleAsync(ICommand command);
    Task<QueryResponse> HandleAsync(IQuery query);
}