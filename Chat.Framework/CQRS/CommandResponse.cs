using Chat.Framework.Models;

namespace Chat.Framework.CQRS
{
    public class CommandResponse : Response
    {
        public string Name { get; set; } = string.Empty;
    }
}