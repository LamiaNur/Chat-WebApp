using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public abstract class AQuery : Request, IQuery
    {
        public int Offset {get; set;}
        public int Limit {get; set;}
        public abstract void ValidateQuery();
        public QueryResponse CreateResponse()
        {
            return new QueryResponse
            {
                Name = this.GetType().Name,
                Offset = this.Offset,
                Limit = this.Limit
            };
        }
    }
}