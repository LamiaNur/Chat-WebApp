using Chat.Api.Core.Interfaces;

namespace Chat.Api.Core.Models
{
    public abstract class ACommand : ICommand
    {
        public Dictionary<string, object> FieldValues;
        
        public ACommand()
        {
            FieldValues = new Dictionary<string, object>();
        }

        public CommandResponse CreateResponse()
        {
            return new CommandResponse
            {
                Name = this.GetType().Name
            };
        }
        
        public abstract void ValidateCommand();

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