namespace Chat.Api.CoreModule.Mediators;

public interface IRequestMediator
{
    Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request);
}