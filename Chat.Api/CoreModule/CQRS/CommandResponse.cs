using Chat.Api.CoreModule.Models;

namespace Chat.Api.CoreModule.CQRS
{
    public class CommandResponse : Response
    {
        public string Name { get; set; } = string.Empty;
    }
}