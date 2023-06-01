using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IQueryHandler
    {
        Task<QueryResponse> HandleAsync(IQuery query);
    }
}