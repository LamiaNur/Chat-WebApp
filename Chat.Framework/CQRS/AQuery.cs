using Chat.Framework.Models;

namespace Chat.Framework.CQRS
{
    public abstract class AQuery : Request, IQuery
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public abstract void ValidateQuery();
        public QueryResponse CreateResponse()
        {
            return new QueryResponse
            {
                Name = GetType().Name,
                Offset = Offset,
                Limit = Limit
            };
        }

        public QueryResponse CreateResponse(QueryResponse response)
        {
            response.Name = GetType().Name;
            response.Offset = Offset;
            response.Limit = Limit;
            return response;
        }
    }
}