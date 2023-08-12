using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public class Response : IResponse
    {
        public string Message {get; set;} = string.Empty;
        public string Status {get; set;} = string.Empty;
        public Dictionary<string, object> MetaData {get; set;}

        public Response()
        {
            MetaData = new Dictionary<string, object>();
        }
        
        public void SetData(string key, object data)
        {
            if (MetaData.ContainsKey(key)) MetaData[key] = data;
            else MetaData.Add(key, data);
        }
        
        public T? GetData<T>(string key) 
        {
            if (MetaData.ContainsKey(key)) return (T)MetaData[key];
            return default;
        }
    }
}