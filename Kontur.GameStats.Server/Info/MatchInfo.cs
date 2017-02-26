using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server.Info
{
    public class MatchInfo
    {



        public string Endpoint;
        public string Timestamp;

        public string Map;
        public string GameMode;

        public int FragLimit;
        public int TimeLimit;
        public double TimeElapsed;

        public ScoreInfo[] Scoreboard;


    }
}
