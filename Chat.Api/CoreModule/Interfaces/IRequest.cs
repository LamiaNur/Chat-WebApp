using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IRequest : ICommonData
    {
        RequestContext? RequestContext { get; set; }
        void SetRequestContext(RequestContext context);
        RequestContext? GetRequestContext();
    }
}