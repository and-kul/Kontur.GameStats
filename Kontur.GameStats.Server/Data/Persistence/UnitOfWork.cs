using System.Data.Entity;
using System.Linq;
using Kontur.GameStats.Server.Data.Core;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Data.Persistence.Repositories;
using Kontur.GameStats.Server.Helpers;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.Reports;
using Kontur.GameStats.Server.Stats;

namespace Kontur.GameStats.Server.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork() : this(new GameStatsDbContext())
        {
        }

        public UnitOfWork(GameStatsDbContext db)
        {
            _db = db;

            Players = new PlayerRepository(db);
            Servers = new ServerRepository(db);
            GameModes = new GameModeRepository(db);
            Maps = new MapRepository(db);
            Matches = new MatchRepository(db);

            PlayerStatistics = new PlayerStatisticsRepository(db);
            ServerStatistics = new ServerStatisticsRepository(db);

            PlayerServerStats = new PlayerServerStatsRepository(db);
            PlayerGameModeStats = new PlayerGameModeStatsRepository(db);
            ServerGameModeStats = new ServerGameModeStatsRepository(db);
            ServerMapStats = new ServerMapStatsRepository(db);

            DatePlayerStats = new DatePlayerStatsRepository(db);
            DateServerStats = new DateServerStatsRepository(db);

            BestPlayers = new BestPlayerRepository(db);
        }


        public Models.Server AddOrUpdateServer(ServerInfo serverInfo)
        {
            var server = Servers.FindServer(serverInfo.Endpoint);
            var gameModes = GameModes.FindOrAddGameModes(serverInfo.GameModes);

            if (server == null)
            {
                server = Servers.AddNewServer(serverInfo.Endpoint, serverInfo.Name, gameModes);
            }
            else
            {
                Servers.UpdateExistingServer(server, serverInfo.Name, gameModes);
            }

            return server;
        }


        public Match AddNewMatch(MatchInfo matchInfo, Models.Server server)
        {
            var match = new Match
            {
                Server = server,
                Timestamp = matchInfo.Timestamp,
                Map = Maps.FindOrAddMap(matchInfo.Map),
                GameMode = GameModes.FindOrAddGameMode(matchInfo.GameMode),
                FragLimit = matchInfo.FragLimit,
                TimeLimit = matchInfo.TimeLimit,
                TimeElapsed = matchInfo.TimeElapsed
            };

            foreach (var scoreInfo in matchInfo.Scoreboard)
            {
                var score = new Score
                {
                    Match = match,
                    Position = scoreInfo.Position,
                    Player = Players.FindOrAddPlayer(scoreInfo.Name),
                    Frags = scoreInfo.Frags,
                    Kills = scoreInfo.Kills,
                    Deaths = scoreInfo.Deaths
                };

                match.Scoreboard.Add(score);
            }

            match.IsProcessedForStatistics = false;

            Matches.Add(match);

            return match;
        }


        public ServerStats GetServerStats(Models.Server server)
        {
            var serverStats = new ServerStats();
            var statistics = server.Statistics;

            serverStats.TotalMatchesPlayed = statistics.TotalMatchesPlayed;

            // todo производительность
            var mostPopularDay = statistics.WorkingDays.OrderByDescending(day => day.MatchesPlayed).FirstOrDefault();
            if (mostPopularDay != null)
                serverStats.MaximumMatchesPerDay = mostPopularDay.MatchesPlayed;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(statistics.FirstMatchTimestamp,
                Matches.GetLastMatchTimestampAmongAllServers());

            serverStats.AverageMatchesPerDay = (double) serverStats.TotalMatchesPlayed / totalDays;

            serverStats.MaximumPopulation = statistics.MaximumPopulation;

            serverStats.AveragePopulation = (double) statistics.SumOfPopulations / totalDays;

            // todo производительность
            serverStats.Top5GameModes =
                statistics.GameModesStats
                    .OrderByDescending(gm => gm.MatchesPlayed)
                    .Take(ServerStats.TopGameModesCount)
                    .Select(gm => gm.GameMode.Name)
                    .ToArray();

            // todo производительность
            serverStats.Top5Maps =
                statistics.MapsStats
                    .OrderByDescending(map => map.MatchesPlayed)
                    .Take(ServerStats.TopMapsCount)
                    .Select(map => map.Map.Name)
                    .ToArray();

            return serverStats;
        }


        public PlayerStats GetPlayerStats(Player player)
        {
            var playerStats = new PlayerStats();
            var statistics = player.Statistics;

            playerStats.TotalMatchesPlayed = statistics.TotalMatchesPlayed;
            playerStats.TotalMatchesWon = statistics.TotalMatchesWon;

            // todo производительность
            playerStats.FavoriteServer =
                statistics.ServersStats
                    .OrderByDescending(server => server.MatchesPlayed)
                    .First().Server.Name;

            playerStats.UniqueServers = statistics.ServersStats.Count;

            // todo производительность
            playerStats.FavoriteGameMode = statistics.GameModesStats
                .OrderByDescending(gm => gm.MatchesPlayed)
                .First().GameMode.Name;

            playerStats.AverageScoreboardPercent = statistics.SumOfScoreboardPercents / playerStats.TotalMatchesPlayed;

            // todo производительность
            playerStats.MaximumMatchesPerDay = statistics.GameDays
                .OrderByDescending(day => day.MatchesPlayed)
                .First().MatchesPlayed;

            var totalDays = TimeHelper.GetUtcNumberOfDaysBetween(statistics.FirstMatchTimestamp,
                Matches.GetLastMatchTimestampAmongAllServers());


            playerStats.AverageMatchesPerDay = (double) playerStats.TotalMatchesPlayed / totalDays;

            playerStats.LastMatchPlayed = statistics.LastMatchTimestamp.ToUniversalTime();

            playerStats.KillToDeathRatio = statistics.KillToDeathRatio;

            return playerStats;
        }


        public PopularServer[] GetPopularServers(int count)
        {
            var lastMatchTimestamp = Matches.GetLastMatchTimestampAmongAllServers();

            var popularServers =
                Servers.All()
                    .Select(server => new PopularServer(server, lastMatchTimestamp))
                    .OrderByDescending(server => server.AverageMatchesPerDay)
                    .Take(count)
                    .ToArray();

            return popularServers;
        }
        
        
        public int Save()
        {
            return _db.SaveChanges();
        }


        public DbContextTransaction BeginTransaction()
        {
            return _db.Database.BeginTransaction();
        }



        public void Dispose()
        {
            _db.Dispose();
        }


        public IPlayerRepository Players { get; }
        public IServerRepository Servers { get; }
        public IGameModeRepository GameModes { get; }
        public IMapRepository Maps { get; }
        public IMatchRepository Matches { get; }
        public IPlayerStatisticsRepository PlayerStatistics { get; }
        public IServerStatisticsRepository ServerStatistics { get; }
        public IPlayerServerStatsRepository PlayerServerStats { get; }
        public IPlayerGameModeStatsRepository PlayerGameModeStats { get; }
        public IServerGameModeStatsRepository ServerGameModeStats { get; }
        public IServerMapStatsRepository ServerMapStats { get; }
        public IDatePlayerStatsRepository DatePlayerStats { get; }
        public IDateServerStatsRepository DateServerStats { get; }
        public IBestPlayerRepository BestPlayers { get; }

        private readonly GameStatsDbContext _db;
        
    }
}