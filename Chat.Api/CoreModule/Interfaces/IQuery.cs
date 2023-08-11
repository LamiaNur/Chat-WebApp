using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IQuery : IRequest
    {
        void ValidateQuery();
        QueryResponse CreateResponse();
    }
}