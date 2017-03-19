using System;
using Kontur.GameStats.Server.Helpers;

namespace Kontur.GameStats.Server.Reports
{
    public class PopularServer
    {
        public PopularServer()
        {
            
        }

        public PopularServer(Models.Server server, DateTime lastMatchTimestamp)
        {
            Endpoint = server.Endpoint;
            Name = server.Name;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(server.Statistics.FirstMatchTimestamp,
                                       lastMatchTimestamp);

            AverageMatchesPerDay = (double)server.Statistics.TotalMatchesPlayed / totalDays;
        }


        public string Endpoint;
        public string Name;
        public double AverageMatchesPerDay;
    }
}
