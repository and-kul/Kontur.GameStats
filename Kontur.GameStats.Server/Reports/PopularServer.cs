using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;

namespace Kontur.GameStats.Server.Reports
{
    class PopularServer
    {
        public PopularServer()
        {
            
        }

        public PopularServer(Models.Server server)
        {
            Endpoint = server.Endpoint;
            Name = server.Name;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(server.Statistics.FirstMatchTimestamp,
                                       DatabaseHelper.GetLastMatchTimestampAmongAllServers());

            AverageMatchesPerDay = (double)server.Statistics.TotalMatchesPlayed / totalDays;
        }
    

        public string Endpoint;
        public string Name;
        public double AverageMatchesPerDay;
    }
}
