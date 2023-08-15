using Chat.Framework.Enums;
using Chat.Framework.Interfaces;

namespace Chat.Framework.Models
{
    public class Response : CommonData, IResponse
    {
        public string Message { get; set; } = string.Empty;
        public ResponseStatus Status { get; set; }
    }
}