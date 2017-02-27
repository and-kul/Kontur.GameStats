using System;
using System.Linq;
using Kontur.GameStats.Server.Helpers;
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

            map = new Map { Name = mapName };
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

            match.IsIncludedInStatistics = false;

            db.SaveChanges();

            return match;
        }


        




    }
}