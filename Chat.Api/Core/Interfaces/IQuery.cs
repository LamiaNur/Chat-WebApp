using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces
{
    public interface IQuery
    {
        void ValidateQuery();
        QueryResponse CreateResponse();
        void SetValue(string key, object value);
        T? GetValue<T>(string key);
    }
}