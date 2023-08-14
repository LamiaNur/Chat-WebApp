using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.CQRS
{
    public interface IQuery : IRequest
    {
        void ValidateQuery();
        QueryResponse CreateResponse();
    }
}