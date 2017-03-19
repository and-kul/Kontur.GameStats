using System;
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
            Endpoint = match.Server.Endpoint;
            Timestamp = match.Timestamp.ToUniversalTime();

            Map = match.Map.Name;
            GameMode = match.GameMode.Name;

            FragLimit = match.FragLimit;
            TimeLimit = match.TimeLimit;
            TimeElapsed = match.TimeElapsed;

            Scoreboard = match.Scoreboard
                .OrderBy(score => score.Position)
                .Select(score => new ScoreInfo(score))
                .ToArray();
        }

        [JsonIgnore] public string Endpoint;
        [JsonIgnore] public DateTime Timestamp;

        public string Map;
        public string GameMode;

        public int FragLimit;
        public int TimeLimit;
        public double TimeElapsed;

        public ScoreInfo[] Scoreboard;
    }
}