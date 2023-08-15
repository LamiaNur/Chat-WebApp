using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public class Request : CommonData, IRequest
    {
        public RequestContext? RequestContext {get; set;}
        public RequestContext? GetRequestContext()
        {
            return RequestContext;
        }
        public void SetRequestContext(RequestContext context)
        {
            RequestContext = context;
        }
    }
}