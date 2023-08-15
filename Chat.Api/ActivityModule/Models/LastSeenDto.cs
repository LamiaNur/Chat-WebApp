namespace Chat.Api.ActivityModule.Models
{
    public class LastSeenDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId {get; set;} = string.Empty;
        public DateTime LastSeenAt {get; set;}
        public bool IsActive {get; set;}
        public string Status {get; set;} = string.Empty;
    }
}