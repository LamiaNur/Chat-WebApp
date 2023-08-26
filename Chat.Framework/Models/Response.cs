using Chat.Framework.Enums;
using Chat.Framework.Interfaces;

namespace Chat.Framework.Models
{
    public class Response : CommonData, IResponse
    {
        public string Message { get; set; } = string.Empty;
        public ResponseStatus Status { get; set; }

        public void SetErrorMessage(string message)
        {
            Message = message;
            Status = ResponseStatus.Error;
        }
        public void SetSuccessMessage(string message)
        {
            Message = message;
            Status = ResponseStatus.Success;
        }
    }
}