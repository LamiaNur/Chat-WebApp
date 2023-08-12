using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Mediators;

public interface IQueryMediator
{
    Task<QueryResponse> HandleAsync(IQuery command);
}