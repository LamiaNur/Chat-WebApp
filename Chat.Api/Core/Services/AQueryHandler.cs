using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    public abstract class AQueryHandler<T> : IQueryHandler where T : IQuery
    {
        public async Task<QueryResponse> HandleAsync(IQuery query)
        {
            return await OnHandleAsync((T)query);
        }
        public abstract Task<QueryResponse> OnHandleAsync(T query);
    }
}