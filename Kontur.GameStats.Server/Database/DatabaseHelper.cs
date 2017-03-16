using System;
using System.Linq;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Database
{
    public static class DatabaseHelper
    {
        public static GameMode FindOrAddGameMode(string gameModeName, GameStatsDbContext db)
        {
            var gameMode = db.GameModes.FirstOrDefault(gm => gm.Name == gameModeName);

            if (gameMode != null) return gameMode;

            gameMode = new GameMode {Name = gameModeName};
            db.GameModes.Add(gameMode);
            db.SaveChanges();

            return gameMode;
        }


        public static Map FindOrAddMap(string mapName, GameStatsDbContext db)
        {
            var map = db.Maps.FirstOrDefault(m => m.Name == mapName);

            if (map != null) return map;

            map = new Map {Name = mapName};
            db.Maps.Add(map);
            db.SaveChanges();

            return map;
        }


        public static Player FindOrAddPlayer(string playerName, GameStatsDbContext db)
        {
            var playerNameLowerCase = playerName.ToLowerInvariant();

            var player = db.Players.FirstOrDefault(p => p.NameLowerCase == playerNameLowerCase);

            if (player != null) return player;

            player = new Player
            {
                NameLowerCase = playerNameLowerCase,
                Statistics = new PlayerStatistics()
            };
            db.Players.Add(player);
            db.SaveChanges();

            return player;
        }


        public static Models.Server FindServer(string endpoint, GameStatsDbContext db)
        {
            return db.Servers.FirstOrDefault(s => s.Endpoint == endpoint);
        }

        public static Match FindMatch(Models.Server server, DateTime timestamp, GameStatsDbContext db)
        {
            return db.Matches.FirstOrDefault(m => m.Timestamp == timestamp && m.ServerId == server.Id);
        }

        public static Player FindPlayer(string playerName, GameStatsDbContext db)
        {
            var playerNameLowerCase = playerName.ToLowerInvariant();
            return db.Players.FirstOrDefault(p => p.NameLowerCase == playerNameLowerCase);
        }

        
        public static void AddNewServer(ServerInfo serverInfo, GameStatsDbContext db)
        {
            var server = new Models.Server
            {
                Endpoint = serverInfo.Endpoint,
                Name = serverInfo.Name,
                Statistics = new ServerStatistics()
            };
            db.Servers.Add(server);

            foreach (var gameModeName in serverInfo.GameModes)
            {
                var gameMode = FindOrAddGameMode(gameModeName, db);
                server.AvailableGameModes.Add(gameMode);
            }

            db.SaveChanges();
        }


        public static void UpdateExistingServer(Models.Server server, ServerInfo newServerInfo, GameStatsDbContext db)
        {
            server.Name = newServerInfo.Name;
            server.AvailableGameModes.Clear();

            foreach (var gameModeName in newServerInfo.GameModes)
            {
                var gameMode = FindOrAddGameMode(gameModeName, db);
                server.AvailableGameModes.Add(gameMode);
            }

            db.SaveChanges();
        }


        public static void AddOrUpdateServer(ServerInfo serverInfo, GameStatsDbContext db)
        {
            var server = FindServer(serverInfo.Endpoint, db);

            if (server == null)
            {
                AddNewServer(serverInfo, db);
            }
            else
            {
                UpdateExistingServer(server, serverInfo, db);
            }
        }


        public static Match AddNewMatch(MatchInfo matchInfo, Models.Server server, GameStatsDbContext db)
        {
            var match = new Match
            {
                Server = server,
                Timestamp = matchInfo.Timestamp,
                Map = FindOrAddMap(matchInfo.Map, db),
                GameMode = FindOrAddGameMode(matchInfo.GameMode, db),
                FragLimit = matchInfo.FragLimit,
                TimeLimit = matchInfo.TimeLimit,
                TimeElapsed = matchInfo.TimeElapsed
            };

            db.Matches.Add(match);

            foreach (var scoreInfo in matchInfo.Scoreboard)
            {
                var score = new Score
                {
                    Match = match,
                    Position = scoreInfo.Position,
                    Player = FindOrAddPlayer(scoreInfo.Name, db),
                    Frags = scoreInfo.Frags,
                    Kills = scoreInfo.Kills,
                    Deaths = scoreInfo.Deaths
                };

                db.Scores.Add(score);
            }

            match.IsProcessedForStatistics = false;

            db.SaveChanges();

            return match;
        }


        public static int[] GetNotProcessedMatchesIds(GameStatsDbContext db)
        {
            return
                db
                .Matches
                .Where(m => !m.IsProcessedForStatistics)
                .OrderBy(m => m.Timestamp)
                .Select(m => m.Id)
                .ToArray();
        }


        public static ServerGameModeStats FindOrAddServerGameModeStats(Models.Server server, GameMode gameMode, GameStatsDbContext db)
        {
            var serverGameMode =
                db.ServersGameModes.FirstOrDefault(sgm => sgm.ServerId == server.Id && sgm.GameModeId == gameMode.Id);
            
            if (serverGameMode != null) return serverGameMode;

            serverGameMode = new ServerGameModeStats
            {
                Server = server,
                GameMode = gameMode,
                MatchesPlayed = 0
            };

            db.ServersGameModes.Add(serverGameMode);
            db.SaveChanges();
            return serverGameMode;
        }


        public static ServerMapStats FindOrAddServerMapStats(Models.Server server, Map map, GameStatsDbContext db)
        {
            var serverMap = db.ServersMaps.FirstOrDefault(sm => sm.ServerId == server.Id && sm.MapId == map.Id);

            if (serverMap != null) return serverMap;

            serverMap = new ServerMapStats
            {
                Server = server,
                Map = map,
                MatchesPlayed = 0
            };

            db.ServersMaps.Add(serverMap);
            db.SaveChanges();
            return serverMap;
        }


        public static DateServerStats FindOrAddDateServerStats(int year, int dayOfYear, Models.Server server,
            GameStatsDbContext db)
        {
            var dateServer =
                db.DateServerStats.FirstOrDefault(
                    ds => ds.Year == year && ds.DayOfYear == dayOfYear && ds.ServerId == server.Id);

            if (dateServer != null) return dateServer;

            dateServer = new DateServerStats
            {
                Year = year,
                DayOfYear = dayOfYear,
                Server = server,
                MatchesPlayed = 0
            };

            db.DateServerStats.Add(dateServer);
            db.SaveChanges();
            return dateServer;
            
        }

        ////


        public static PlayerServerStats FindOrAddPlayerServerStats(Player player, Models.Server server, GameStatsDbContext db)
        {
            var playerServerStats =
                db.PlayersServers.FirstOrDefault(ps => ps.PlayerId == player.Id && ps.ServerId == server.Id);

            if (playerServerStats != null) return playerServerStats;

            playerServerStats = new PlayerServerStats
            {
                Player = player,
                Server = server,
                MatchesPlayed = 0
            };

            db.PlayersServers.Add(playerServerStats);
            db.SaveChanges();
            return playerServerStats;
        }


        public static PlayerGameModeStats FindOrAddPlayerGameModeStats(Player player, GameMode gameMode,
            GameStatsDbContext db)
        {
            var playerGameModeStats =
                db.PlayersGameModes.FirstOrDefault(pgm => pgm.PlayerId == player.Id && pgm.GameModeId == gameMode.Id);

            if (playerGameModeStats != null) return playerGameModeStats;

            playerGameModeStats = new PlayerGameModeStats
            {
                Player = player,
                GameMode = gameMode,
                MatchesPlayed = 0
            };

            db.PlayersGameModes.Add(playerGameModeStats);
            db.SaveChanges();
            return playerGameModeStats;
        }


        public static DatePlayerStats FindOrAddDatePlayerStats(int year, int dayOfYear, Player player,
            GameStatsDbContext db)
        {
            var datePlayerStats =
                db.DatePlayerStats.FirstOrDefault(
                    dp => dp.Year == year && dp.DayOfYear == dayOfYear && dp.PlayerId == player.Id);

            if (datePlayerStats != null) return datePlayerStats;

            datePlayerStats = new DatePlayerStats
            {
                Year = year,
                DayOfYear = dayOfYear,
                Player = player,
                MatchesPlayed = 0
            };

            db.DatePlayerStats.Add(datePlayerStats);
            db.SaveChanges();
            return datePlayerStats;

        }

        public static DateTime GetLastMatchTimestampAmongAllServers()
        {
            using (var db = new GameStatsDbContext())
            {
                return db.Matches.OrderByDescending(match => match.Timestamp).First().Timestamp;
            }
            
        }


        public static BestPlayer FindOrAddBestPlayer(Player player, GameStatsDbContext db)
        {
            var bestPlayer = db.BestPlayers.FirstOrDefault(best => best.PlayerId == player.Id);

            if (bestPlayer != null) return bestPlayer;

            bestPlayer = new BestPlayer { Player = player };
            db.BestPlayers.Add(bestPlayer);
            db.SaveChanges();

            return bestPlayer;
        }

    }
}