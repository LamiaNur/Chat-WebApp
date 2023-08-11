using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IRequest
    {
        void SetValue(string key, object value);
        T? GetValue<T>(string key);
        void SetCurrentScope(RequestContext context);
        RequestContext GetCurrentScope();
    }
}