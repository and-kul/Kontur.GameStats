using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.StatisticsManagement
{
    class StatisticsProcessor
    {
        private static Thread statisticsThread;

        private static bool isStarted = false;

        private static readonly BlockingCollection<Match> MatchQueue = new BlockingCollection<Match>();

        public static void AddMatch(Match match)
        {
            MatchQueue.Add(match);
        }

        private static void LoadNotProcessedMatchesFromDatabase()
        {
            using (var db = new GameStatsDbContext())
            {
                foreach (var match in DatabaseHelper.GetNotProcessedMatches(db))
                {
                    MatchQueue.Add(match);
                }
            }
        }


        public static void Start()
        {
                
        }



    }
}
