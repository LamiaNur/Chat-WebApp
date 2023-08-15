using Chat.Api.CoreModule.Enums;
using Chat.Api.CoreModule.Interfaces;

namespace Chat.Api.CoreModule.Models
{
    public class Response : CommonData, IResponse
    {
        public string Message {get; set;} = string.Empty;
        public ResponseStatus Status {get; set;}
    }
}