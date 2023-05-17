using Chat.Api.Core.Models;

namespace Chat.Api.Core.Interfaces 
{
    public interface ICommand
    {
        void SetValue(string key, object value);
        T? GetValue<T>(string key);
        CommandResponse CreateResponse();
        void ValidateCommand();
    }
}