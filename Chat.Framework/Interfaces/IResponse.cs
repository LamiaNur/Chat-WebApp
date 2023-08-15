using Chat.Framework.Enums;

namespace Chat.Framework.Interfaces
{
    public interface IResponse : ICommonData
    {
        string Message { get; set; }
        ResponseStatus Status { get; set; }
    }
}