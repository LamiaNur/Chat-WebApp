using System.Composition;
using Chat.Api.CoreModule.Mediators;
using Chat.Api.CoreModule.Services;

namespace Chat.Api.CoreModule.CQRS;

[Export(typeof(IQueryMediator))]
[Shared]
public class QueryMediator : RequestMediator<IQuery, QueryResponse>, IQueryMediator
{
    protected override IRequestHandler<IQuery, QueryResponse>? GetHandler(string handlerName)
    {
        Console.WriteLine("This is from QueryMediator Before Get handler\n");
        var handler = DIService.Instance.GetService<IQueryHandler>(handlerName);
        Console.WriteLine("This is from QueryMediator After Get handler\n");
        return handler;
    }
}