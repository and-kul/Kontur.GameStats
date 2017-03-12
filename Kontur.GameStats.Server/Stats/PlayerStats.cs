using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Stats
{
    public class PlayerStats
    {
        public PlayerStats()
        {
        }

        public PlayerStats(Player player)
        {
            var statistics = player.Statistics;

            TotalMatchesPlayed = statistics.TotalMatchesPlayed;
            TotalMatchesWon = statistics.TotalMatchesWon;
            FavoriteServer =
                statistics.ServersStats
                    .OrderByDescending(server => server.MatchesPlayed)
                    .First().Server.Name;

            UniqueServers = statistics.ServersStats
                .OrderByDescending(server => server.MatchesPlayed)
                .Count();

            FavoriteGameMode = statistics.GameModesStats
                .OrderByDescending(gm => gm.MatchesPlayed)
                .First().GameMode.Name;

            AverageScoreboardPercent = statistics.SumOfScoreboardPercents / TotalMatchesPlayed;

            MaximumMatchesPerDay = statistics.GameDays
                .OrderByDescending(day => day.MatchesPlayed)
                .First().MatchesPlayed;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(statistics.FirstMatchTimestamp,
                                       DatabaseHelper.GetLastMatchTimestampAmongAllServers());

            AverageMatchesPerDay = (double) TotalMatchesPlayed / totalDays;

            LastMatchPlayed = statistics.LastMatchTimestamp.ToUniversalTime();

            KillToDeathRatio = statistics.KillToDeathRatio;
            
        }


        public int TotalMatchesPlayed;
        public int TotalMatchesWon;
        public string FavoriteServer;
        public int UniqueServers;
        public string FavoriteGameMode;
        public double AverageScoreboardPercent;
        public int MaximumMatchesPerDay;
        public double AverageMatchesPerDay;
        public DateTime LastMatchPlayed;
        public double KillToDeathRatio;
    }
}