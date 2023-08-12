using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public class Request : IRequest
    {
        private RequestContext? RequestContext {get; set;}
        public Dictionary<string, object> FieldValues = new();
        public RequestContext? GetCurrentScope()
        {
            return RequestContext;
        }
        public void SetCurrentScope(RequestContext context)
        {
            RequestContext = context;
        }
        public T? GetValue<T>(string key)
        {
            if (FieldValues.ContainsKey(key)) return (T)FieldValues[key];
            return default;
        }
        public void SetValue(string key, object value)
        {
            if (FieldValues.ContainsKey(key)) FieldValues[key] = value;
            else FieldValues.Add(key, value);
        }
    }
}