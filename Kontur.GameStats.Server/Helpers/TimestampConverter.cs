using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server.Helpers
{
    public static class TimestampConverter
    {
        public static bool IsCorrectTimestamp(string str)
        {
            DateTime localDateTime;
            return DateTime.TryParse(str, out localDateTime);
        }


        public static DateTime Parse(string str)
        {
            return DateTime.Parse(str).ToUniversalTime();
        }


        public static string ToISOString(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}