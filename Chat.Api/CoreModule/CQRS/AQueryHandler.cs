using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class AQueryHandler<T> : IRequestHandler<T, QueryResponse> where T : IQuery
    {
        protected readonly ICommandQueryService CommandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        public async Task<QueryResponse> HandleAsync(T query)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            query.ValidateQuery();
            var response = await OnHandleAsync((T)query);
            Console.WriteLine($"Successfully returned OnHandleAsync of : {GetType().Name}\n");
            return response;
        }
        public abstract Task<QueryResponse> OnHandleAsync(T query);
    }
}