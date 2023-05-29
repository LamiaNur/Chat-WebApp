using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IQueryService
    {
        Task<QueryResponse> HandleQueryAsync(IQuery query);
    }
}