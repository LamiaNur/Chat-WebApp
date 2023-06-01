using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IQuery
    {
        void ValidateQuery();
        QueryResponse CreateResponse();
        void SetValue(string key, object value);
        T? GetValue<T>(string key);
    }
}