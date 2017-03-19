namespace Kontur.GameStats.Server.Stats
{
    public class ServerStats
    {
        public const int TopGameModesCount = 5;
        public const int TopMapsCount = 5;
        
        public ServerStats()
        {
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