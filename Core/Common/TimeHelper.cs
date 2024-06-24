using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public static class TimeHelper
    {
        public static string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return $"{timeSpan.Seconds} giây trước";
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} phút trước" : "1 phút trước";
            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? $"{timeSpan.Hours} giờ trước" : "1 giờ trước";
            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 1 ? $"{timeSpan.Days} ngày trước" : "hôm qua";
            else if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? $"{timeSpan.Days / 30} tháng trước" : "1 tháng trước";
            else
                return timeSpan.Days > 365 ? $"{timeSpan.Days / 365} năm trước" : "1 năm trước";
        }
    }
}
