using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces
{
    public interface IQueryService
    {
        Task<QueryResponse> HandleQueryAsync(IQuery query);
    }
}