using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.Interfaces 
{
    public interface ICommand
    {
        void SetValue(string key, object value);
        T? GetValue<T>(string key);
        CommandResponse CreateResponse();
        void ValidateCommand();
    }
}