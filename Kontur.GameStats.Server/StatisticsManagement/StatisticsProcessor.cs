using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.StatisticsManagement
{
    class StatisticsProcessor
    {
        private static Thread statisticsThread;

        private static bool isStarted;

        private static readonly BlockingCollection<int> MatchQueue = new BlockingCollection<int>();

        public static void AddMatchId(int id)
        {
            MatchQueue.Add(id);
        }

        private static void LoadNotProcessedMatchesFromDatabase()
        {
            using (var db = new GameStatsDbContext())
            {
                foreach (var matchId in DatabaseHelper.GetNotProcessedMatchesIds(db))
                {
                    AddMatchId(matchId);
                }
            }
        }

        private static void UpdateServerStatistics(Match match, GameStatsDbContext db)
        {
            var server = match.Server;
            var serverStatistics = server.Statistics;

            serverStatistics.TotalMatchesPlayed++;
            serverStatistics.MaximumPopulation = Math.Max(serverStatistics.MaximumPopulation,
                match.Scoreboard.Count);

            serverStatistics.SumOfPopulations += match.Scoreboard.Count;

            if (serverStatistics.FirstMatchTimestamp == DateTime.MinValue)
                serverStatistics.FirstMatchTimestamp = match.Timestamp;

            //serverStatistics.LastMatchTimestamp = match.Timestamp;

            var serverGameModeStats = DatabaseHelper.FindOrAddServerGameModeStats(server, match.GameMode,
                db);
            serverGameModeStats.MatchesPlayed++;
            db.Entry(serverGameModeStats).State = EntityState.Modified;

            var serverMapStats = DatabaseHelper.FindOrAddServerMapStats(server, match.Map, db);
            serverMapStats.MatchesPlayed++;
            db.Entry(serverMapStats).State = EntityState.Modified;

            int year, dayOfYear;
            TimeHelper.GetUtcYearAndDay(match.Timestamp, out year, out dayOfYear);

            var dateServerStats = DatabaseHelper.FindOrAddDateServerStats(year, dayOfYear, server, db);
            dateServerStats.MatchesPlayed++;
            db.Entry(dateServerStats).State = EntityState.Modified;

            db.Entry(serverStatistics).State = EntityState.Modified;

            db.SaveChanges();
        }


        private static double ScoreboardPercent(int position, int totalPlayers)
        {
            if (totalPlayers == 1)
                return 100;

            return (double) (totalPlayers - position - 1) / (totalPlayers - 1) * 100;
        }


        private static void UpdatePlayersStatistics(Match match, GameStatsDbContext db)
        {
            var server = match.Server;
            var gameMode = match.GameMode;
            var scoreboard = match.Scoreboard.OrderBy(score => score.Position).ToArray();

            foreach (var score in scoreboard)
            {
                var player = score.Player;
                var playerStatistics = player.Statistics;

                playerStatistics.TotalMatchesPlayed++;
                if (score.Position == 0) playerStatistics.TotalMatchesWon++;

                playerStatistics.SumOfScoreboardPercents += ScoreboardPercent(score.Position, scoreboard.Length);

                if (playerStatistics.FirstMatchTimestamp == DateTime.MinValue)
                    playerStatistics.FirstMatchTimestamp = match.Timestamp;

                playerStatistics.LastMatchTimestamp = match.Timestamp;

                playerStatistics.Kills += score.Kills;
                playerStatistics.Deaths += score.Deaths;

                var playerServerStats = DatabaseHelper.FindOrAddPlayerServerStats(player, server, db);
                playerServerStats.MatchesPlayed++;
                db.Entry(playerServerStats).State = EntityState.Modified;

                var playerGameModeStats = DatabaseHelper.FindOrAddPlayerGameModeStats(player, gameMode, db);
                playerGameModeStats.MatchesPlayed++;
                db.Entry(playerGameModeStats).State = EntityState.Modified;

                int year, dayOfYear;
                TimeHelper.GetUtcYearAndDay(match.Timestamp, out year, out dayOfYear);

                var datePlayerStats = DatabaseHelper.FindOrAddDatePlayerStats(year, dayOfYear, player, db);
                datePlayerStats.MatchesPlayed++;
                db.Entry(datePlayerStats).State = EntityState.Modified;

                db.Entry(playerStatistics).State = EntityState.Modified;

                db.SaveChanges();
            }
        }


        private static void ProcessStatistics()
        {
            while (true)
            {
                var matchId = MatchQueue.Take();
                using (var db = new GameStatsDbContext())
                {
                    var match = db.Matches.Find(matchId);

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            UpdateServerStatistics(match, db);

                            UpdatePlayersStatistics(match, db);

                            match.IsProcessedForStatistics = true;
                            db.Entry(match).State = EntityState.Modified;

                            db.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }

        public static void Start()
        {
            if (isStarted) return;

            LoadNotProcessedMatchesFromDatabase();

            statisticsThread = new Thread(ProcessStatistics);
            statisticsThread.IsBackground = true;
            statisticsThread.Start();


            isStarted = true;
        }
    }
}