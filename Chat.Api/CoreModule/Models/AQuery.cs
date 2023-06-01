using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public abstract class AQuery : IQuery
    {
        public int Offset {get; set;}
        public int Limit {get; set;}
        public Dictionary<string, object> FieldValues;
        
        public AQuery()
        {
            FieldValues = new Dictionary<string, object>();
        }
        
        public abstract void ValidateQuery();
        
        public QueryResponse CreateResponse()
        {
            return new QueryResponse
            {
                Name = this.GetType().Name,
                Offset = this.Offset,
                Limit = this.Limit
            };
        }

        public T? GetValue<T>(string key)
        {
            if (FieldValues.ContainsKey(key)) return (T)FieldValues[key];
            return default(T);
        }

        public void SetValue(string key, object value)
        {
            if (FieldValues.ContainsKey(key)) FieldValues[key] = value;
            else FieldValues.Add(key, value);
        }
    }
}