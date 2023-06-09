namespace Chat.Api.CoreModule.Helpers
{
    public static class DisplayTimeHelper
    {
        public static string GetChatListDisplayTime(DateTime time, string activeStatus = "Just Now") 
        {
            var timeDifference = DateTime.UtcNow.Subtract(time);
            var days = (int)timeDifference.TotalDays;
            var hours = (int)timeDifference.TotalHours;
            var minutes = (int)timeDifference.TotalMinutes;
            if (days == 0 && hours == 0 && minutes == 0)
            {
                return activeStatus;
            }  
            if (days == 0 && hours == 0) 
            {
               return $"{minutes} minutes ago";
            } 
            if (days == 0) 
            {
               return $"{hours} hour ago";
            }
            return $"{days} days ago";
        }
        
        public static string GetChatDisplayTime(DateTime time)
        {
            var timeDifference = DateTime.UtcNow.Subtract(time);
            var days = (int)timeDifference.TotalDays;
            var hours = (int)timeDifference.TotalHours;
            var minutes = (int)timeDifference.TotalMinutes;
            if (days == 0 && hours == 0) 
            {
                return time.ToString("h:mm tt");
            }
            return time.ToString("dd/MM/yyyy");
        }
    }
}