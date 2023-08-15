using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class AQueryHandler<TQuery> : IRequestHandler<TQuery, QueryResponse> where TQuery : IQuery
    {
        protected readonly ICommandQueryService CommandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        
        protected abstract Task<QueryResponse> OnHandleAsync(TQuery query);
        
        public async Task<QueryResponse> HandleAsync(TQuery query)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            query.ValidateQuery();
            return await OnHandleAsync(query);
        }
    }
}