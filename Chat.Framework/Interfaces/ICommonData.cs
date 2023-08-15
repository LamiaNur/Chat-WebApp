namespace Chat.Framework.Interfaces
{
    public interface ICommonData
    {
        Dictionary<string, object> MetaData { get; set; }
        void SetData(string key, object value);
        T? GetData<T>(string key);
    }
}
