using Chat.Framework.Mediators;

namespace Chat.Framework.CQRS
{
    public abstract class AQueryHandler<TQuery> : IRequestHandler<TQuery, QueryResponse> where TQuery : IQuery
    {
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