using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS
{
    public abstract class AQueryHandler<T> : IQueryHandler where T : IQuery
    {
        public readonly ICommandQueryService _commandQueryService = DIService.Instance.GetService<ICommandQueryService>();
        public async Task<QueryResponse> HandleAsync(IQuery query)
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