using System.Linq;
using Kontur.GameStats.Server.Models;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server.Info
{
    public class MatchInfo
    {
        public MatchInfo()
        {
        }

        public MatchInfo(Match match)
        {
            Map = match.Map.Name;
            GameMode = match.GameMode.Name;

            FragLimit = match.FragLimit;
            TimeLimit = match.TimeLimit;
            TimeElapsed = match.TimeElapsed;

            Scoreboard = match.Scoreboard
                .OrderBy(score => score.Position)
                .AsEnumerable()
                .Select(score => new ScoreInfo(score))
                .ToArray();
        }

        [JsonIgnore] public string Endpoint;
        [JsonIgnore] public string Timestamp;

        public string Map;
        public string GameMode;

        public int FragLimit;
        public int TimeLimit;
        public double TimeElapsed;

        public ScoreInfo[] Scoreboard;
    }
}