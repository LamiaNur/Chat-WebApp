namespace Chat.Api.CoreModule.Helpers
{
    public static class DisplayTimeHelper
    {
        public static string GetChatListDisplayTime(DateTime time) 
        {
            var timeDifference = DateTime.UtcNow - time;
            var days = ((int)timeDifference.TotalDays);
            var hours = ((int)timeDifference.TotalHours);
            var minutes =((int)timeDifference.TotalMinutes);
             var status = "";
            var isActive = false;
            if (days == 0 && hours == 0 && minutes == 0) {
                status = "Active Now";
                isActive = true;
            }  else if (days == 0 && hours == 0) {
                status = $"{minutes} minitues ago";
            } else if (days == 0) {
                status = $"{hours} hour ago";
            } else {
                status = $"{days} days ago";
            }
        }
    }
}