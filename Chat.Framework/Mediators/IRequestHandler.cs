namespace Chat.Framework.Mediators;

public interface IRequestHandler
{

}

public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
{
    Task<TResponse> HandleAsync(TRequest request);
}