using System.Composition;
using Chat.Api.CoreModule.Constants;
using Chat.Api.CoreModule.Interfaces;
using Chat.Api.CoreModule.Models;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.Mediators;

[Export(typeof(IRequestMediator<,>))]
[Shared]
public class RequestMediator<T, R> : IRequestMediator<T, R> where T : IRequest where R : IResponse
{
    public async Task<R> HandleAsync(T request)
    {
        try
        {
            var handlerName = request.GetType().Name + GetHandlerNameSuffix();
            var handler = GetHandler(handlerName) ?? throw new Exception("Handler not found");
            Console.WriteLine("Hanler got here at request mediator");
            var response = await handler.HandleAsync(request);
            if (string.IsNullOrEmpty(response.Status)) response.Status = ResponseStatus.Success;
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            IResponse response = new Response
            {
                Status = ResponseStatus.Error,
                Message = ex.Message
            };
            return (R)response;
        }
    }
    
    protected virtual string GetHandlerNameSuffix()
    {
        return "Handler";
    }
    
    protected virtual IRequestHandler<T, R>? GetHandler(string handlerName)
    {
        return (IRequestHandler<T, R>?)DIService.Instance.GetService<IRequestHandler>(handlerName);
    }
}