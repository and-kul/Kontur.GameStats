using System;

namespace Kontur.GameStats.Server.Stats
{
    public class PlayerStats
    {
        public PlayerStats()
        {
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