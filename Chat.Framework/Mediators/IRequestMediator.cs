namespace Chat.Framework.Mediators;

public interface IRequestMediator
{
    Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request);
}