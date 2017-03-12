using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;

namespace Kontur.GameStats.Server.Stats
{
    public class ServerStats
    {
        private const int TopGameModesCount = 5;
        private const int TopMapsCount = 5;
        
        public ServerStats()
        {
        }

        public ServerStats(Models.Server server)
        {
            var statistics = server.Statistics;

            TotalMatchesPlayed = statistics.TotalMatchesPlayed;
            var dateServerStats = statistics.WorkingDays.OrderByDescending(day => day.MatchesPlayed).FirstOrDefault();
            if (dateServerStats != null)
                MaximumMatchesPerDay = dateServerStats.MatchesPlayed;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(statistics.FirstMatchTimestamp,
                                       DatabaseHelper.GetLastMatchTimestampAmongAllServers());

            AverageMatchesPerDay = (double) TotalMatchesPlayed / totalDays;
                                   
            MaximumPopulation = statistics.MaximumPopulation;

            AveragePopulation = (double) statistics.SumOfPopulations / totalDays;

            Top5GameModes =
                statistics.GameModesStats
                    .OrderByDescending(gm => gm.MatchesPlayed)
                    .Take(TopGameModesCount)
                    .Select(gm => gm.GameMode.Name)
                    .ToArray();

            Top5Maps =
                statistics.MapsStats
                    .OrderByDescending(map => map.MatchesPlayed)
                    .Take(TopMapsCount)
                    .Select(map => map.Map.Name)
                    .ToArray();

        }


        public int TotalMatchesPlayed;
        public int MaximumMatchesPerDay;
        public double AverageMatchesPerDay;
        public int MaximumPopulation;
        public double AveragePopulation;
        public string[] Top5GameModes;
        public string[] Top5Maps;
    }
}