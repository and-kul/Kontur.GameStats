using System;

namespace Kontur.GameStats.Server.Helpers
{
    public static class TimeHelper
    {
        public static void GetUtcYearAndDay(DateTime timestamp, out int year, out int dayOfYear)
        {
            year = timestamp.ToUniversalTime().Year;
            dayOfYear = timestamp.ToUniversalTime().DayOfYear;
        }


        public static int GetUtcNumberOfDaysBetween(DateTime start, DateTime end)
        {
            return (int)(end.ToUniversalTime().Date - start.ToUniversalTime().Date).TotalDays + 1;
        }



    }
}