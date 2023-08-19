using Chat.Framework.Mediators;
using Chat.Framework.Proxy;
using Chat.Framework.Services;

namespace Chat.Framework.CQRS
{
    public abstract class AQueryHandler<TQuery> : IRequestHandler<TQuery, QueryResponse> where TQuery : IQuery
    {
        protected readonly ICommandQueryProxy CommandQueryProxy = DIService.Instance.GetService<ICommandQueryProxy>();

        protected abstract Task<QueryResponse> OnHandleAsync(TQuery query);

        public async Task<QueryResponse> HandleAsync(TQuery query)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            query.ValidateQuery();
            var response = await OnHandleAsync(query);
            return query.CreateResponse(response);
        }
    }
}