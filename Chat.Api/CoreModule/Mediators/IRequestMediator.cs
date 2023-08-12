using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Mediators;

public interface IRequestMediator<T, R> where T : IRequest where R : IResponse
{
    Task<R> HandleAsync(T request);
}
