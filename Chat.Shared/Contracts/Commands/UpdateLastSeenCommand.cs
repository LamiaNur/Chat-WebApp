using Chat.Framework.CQRS;

namespace Chat.Shared.Contracts.Commands
{
    public class UpdateLastSeenCommand : ACommand
    {
        public string UserId { get; set; } = string.Empty;

        public override void ValidateCommand()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                throw new Exception("UserId not set!!");
            }
        }
    }
}