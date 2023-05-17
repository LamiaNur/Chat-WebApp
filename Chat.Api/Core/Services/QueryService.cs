using System.Composition;
using Chat.Api.Core.Constants;
using Chat.Api.Core.Interfaces;
using Chat.Api.Core.Models;

namespace Chat.Api.Core.Services
{
    [Export(typeof(IQueryService))]
    [Shared]
    public class QueryService : IQueryService
    {
        public async Task<QueryResponse> HandleQueryAsync(IQuery query)
        {
            var queryName = query.GetType().Name;
            var handlerName = $"{queryName}Handler";
            try
            {
                Console.WriteLine($"Before handle query: {queryName}\n");
                Console.WriteLine($"Start Validating query: {queryName}\n");
                query.ValidateQuery();
                Console.WriteLine($"Success Validating query: {queryName}\n");
                Console.WriteLine($"Start Resolving QueryHandler: {handlerName}\n");
                var handler = DIService.Instance.GetService<IQueryHandler>(handlerName);
                if (handler == null)
                {
                    throw new Exception("Handler not found");
                }
                Console.WriteLine($"Success Resolving queryHandler: {handlerName}\n");
                var response = await handler.HandleAsync(query);
                if (string.IsNullOrEmpty(response.Status)) 
                {
                    response.Status = ResponseStatus.Success;
                }
                response.Name = queryName;
                Console.WriteLine($"After Successful handle query : {query}\n");
                return response;
            }
            catch (Exception ex)
            {
                var response = query.CreateResponse();
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
                Console.WriteLine($"After failed handle query : {queryName}\n");
                return response;
            }
        }
    }
}