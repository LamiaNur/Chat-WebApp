using Chat.Framework.Attributes;
using Chat.Framework.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Framework.Mediators;

[ServiceRegister(typeof(IRequestMediator), ServiceLifetime.Singleton)]
public class RequestMediator : IRequestMediator
{
    private readonly IServiceProvider _serviceProvider;
    public RequestMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
    {
        var handlerName = request?.GetType().Name + GetHandlerNameSuffix();
        var handler = GetHandler<TRequest, TResponse>(handlerName) ?? throw new Exception($"{handlerName} not found");
        return await handler.HandleAsync(request);
    }

    protected virtual string GetHandlerNameSuffix()
    {
        return "Handler";
    }

    protected virtual IRequestHandler<TRequest, TResponse>? GetHandler<TRequest, TResponse>(string handlerName)
    {
        var handler = _serviceProvider.GetService<IRequestHandler>(handlerName);
        return (IRequestHandler<TRequest, TResponse>?)handler;
    }
}