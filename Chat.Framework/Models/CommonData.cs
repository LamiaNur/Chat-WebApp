using Chat.Framework.Extensions;
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
                try
                {
                    return (T)data;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    var serializedData = data.Serialize();
                    return serializedData.Deserialize<T>();
                }
            }
            return default;
        }
    }
}
