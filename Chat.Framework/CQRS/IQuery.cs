using Chat.Framework.Interfaces;

namespace Chat.Framework.CQRS
{
    public interface IQuery : IRequest
    {
        void ValidateQuery();
        QueryResponse CreateResponse();
        QueryResponse CreateResponse(QueryResponse response);
    }
}