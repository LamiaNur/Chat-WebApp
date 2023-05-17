using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces
{
    public interface IQueryHandler
    {
        Task<QueryResponse> HandleAsync(IQuery query);
    }
}