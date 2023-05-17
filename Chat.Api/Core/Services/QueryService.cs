using System.Composition;
using Chat.Api.Core.Constants;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    [Export(typeof(IQueryService))]
    [Export("QueryService", typeof(IQueryService))]
    [Shared]
    public class QueryService : IQueryService
    {
        public async Task<QueryResponse> HandleQueryAsync(IQuery query)
        {
            try
            {
                Console.WriteLine($"Before handle query: {query.GetType().Name}\n");

                query.ValidateQuery();
                
                var queryHandlerName = query.GetType().Name + "Handler";
                var handler = DIService.Instance.GetService<IQueryHandler>(queryHandlerName);
                if (handler == null)
                {
                    throw new Exception("Handler not set");
                }
                var response = await handler.HandleAsync(query);
                
                if (string.IsNullOrEmpty(response.Status)) 
                {
                    response.Status = ResponseStatus.Success;
                }
                
                response.Name = query.GetType().Name;
                
                Console.WriteLine($"After Successful handle query : {query.GetType().Name}\n");

                return response;
            }
            catch (Exception ex)
            {
                var response = query.CreateResponse();
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;

                Console.WriteLine($"After failed handle query : {query.GetType().Name}\n");

                return response;
            }
        }
    }
}