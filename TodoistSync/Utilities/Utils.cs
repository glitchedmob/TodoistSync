using System;

namespace TodoistSync.Utilities
{
    public static class Utils
    {
        // Updates a time timezone on a DateTimeOffset without changing any underlying values
        public static DateTimeOffset? UpdateTimeZone(this DateTimeOffset? dateTime, TimeZoneInfo timeZone)
        {
            if (dateTime == null)
            {
                return null;
            }

            var modifiedDateTime = TimeZoneInfo.ConvertTime(dateTime.Value, timeZone);

            return dateTime.Value.Subtract(modifiedDateTime.Offset).ToOffset(modifiedDateTime.Offset);
        }
    }
}
