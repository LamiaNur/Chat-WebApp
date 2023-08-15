using Chat.Api.CoreModule.Enums;

namespace Chat.Api.CoreModule.Interfaces
{
    public interface IResponse : ICommonData
    {
        string Message {get; set;}
        ResponseStatus Status {get; set;}
    }
}