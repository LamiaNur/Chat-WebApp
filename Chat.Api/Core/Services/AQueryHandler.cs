using System.Composition;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    public abstract class AQueryHandler<T> : IQueryHandler where T : IQuery
    {
        [Import] public IQueryService _queryService {get; set;}
        public async Task<QueryResponse> HandleAsync(IQuery query)
        {
            Console.WriteLine($"OnHandleAsync of : {this.GetType().Name}\n");
            var response = await OnHandleAsync((T)query);
            Console.WriteLine($"Successfully returned OnHandleAsync of : {this.GetType().Name}\n");
            return response;
        }
        public abstract Task<QueryResponse> OnHandleAsync(T query);
    }
}