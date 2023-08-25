using Chat.Framework.Enums;
using Chat.Framework.Mediators;

namespace Chat.Framework.CQRS
{
    public abstract class AQueryHandler<TQuery> : IRequestHandler<TQuery, QueryResponse> where TQuery : IQuery
    {
        protected abstract Task<QueryResponse> OnHandleAsync(TQuery query);

        public async Task<QueryResponse> HandleAsync(TQuery query)
        {
            Console.WriteLine($"OnHandleAsync of : {GetType().Name}\n");
            try
            {
                query.ValidateQuery();
                var response = await OnHandleAsync(query);
                return query.CreateResponse(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var response = query.CreateResponse();
                response.Message = e.Message;
                response.Status = ResponseStatus.Error;
                return response;
            }
        }
    }
}