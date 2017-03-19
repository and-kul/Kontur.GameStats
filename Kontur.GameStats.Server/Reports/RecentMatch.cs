using System;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Reports
{
    public class RecentMatch
    {
        public RecentMatch()
        {  
        }

        public RecentMatch(Match match)
        {
            var matchInfo = new MatchInfo(match);
            Server = matchInfo.Endpoint;
            Timestamp = matchInfo.Timestamp;
            Results = matchInfo;
        }
        

        public string Server;
        public DateTime Timestamp;
        public MatchInfo Results;
    }
}
