using Chat.Api.CoreModule.Mediators;

namespace Chat.Api.CoreModule.CQRS
{
    public interface IQueryHandler : IRequestHandler<IQuery, QueryResponse>
    {

    }
}