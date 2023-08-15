using Chat.Framework.Interfaces;

namespace Chat.Framework.Models
{
    public class CommonData : ICommonData
    {
        public Dictionary<string, object> MetaData { get; set; }

        public CommonData()
        {
            MetaData = new Dictionary<string, object>();
        }
        public void SetData(string key, object data)
        {
            MetaData[key] = data;
        }
        public T? GetData<T>(string key)
        {
            if (MetaData.TryGetValue(key, out var data))
            {
                return (T)data;
            }
            return default;
        }
    }
}
