using Chat.Framework.Models;

namespace Chat.Framework.Interfaces
{
    public interface IRequest : ICommonData
    {
        RequestContext? RequestContext { get; set; }
        void SetRequestContext(RequestContext context);
        RequestContext? GetRequestContext();
    }
}