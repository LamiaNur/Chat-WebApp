using System.Composition;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.Mediators;

[Export(typeof(IRequestMediator))]
[Shared]
public class RequestMediator : IRequestMediator
{
    public async Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
    {
        var handlerName = request?.GetType().Name + GetHandlerNameSuffix();
        var handler = GetHandler<TRequest, TResponse>(handlerName) ?? throw new Exception("Handler not found");
        return await handler.HandleAsync(request);
    }

    protected virtual string GetHandlerNameSuffix()
    {
        return "Handler";
    }
    
    protected virtual IRequestHandler<TRequest, TResponse>? GetHandler<TRequest, TResponse>(string handlerName)
    {
        var handler = DIService.Instance.GetService<IRequestHandler>(handlerName);
        return (IRequestHandler<TRequest, TResponse>?)handler;
    }
}