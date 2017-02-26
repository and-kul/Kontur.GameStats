using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.NancyModules;
using Nancy;

namespace Kontur.GameStats.Server.Database
{
    static class DatabaseHelper
    {
        public static GameMode FindGameMode(string gameModeName, GameStatsDbContext db)
        {
            var gameMode = db.GameModes.FirstOrDefault(gm => gm.Name == gameModeName);

            if (gameMode != null) return gameMode;

            gameMode = new GameMode { Name = gameModeName };
            db.GameModes.Add(gameMode);
            db.SaveChanges();

            return gameMode;
        }

        public static Models.Server FindServer(string endpoint, GameStatsDbContext db)
        {
            return db.Servers.FirstOrDefault(s => s.Endpoint == endpoint);
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
                var gameMode = FindGameMode(gameModeName, db);

                var serverGameMode = new ServerGameMode
                {
                    Server = server,
                    GameMode = gameMode
                };

                db.ServersGameModes.Add(serverGameMode);
            }

            db.SaveChanges();
        }


        public static void RemoveServer(Models.Server server, GameStatsDbContext db)
        { 
            db.Servers.Remove(server);
            db.SaveChanges();
        }


        public static void UpdateExistingServer(Models.Server server, ServerInfo newServerInfo, GameStatsDbContext db)
        {
            RemoveServer(server, db);
            AddNewServer(newServerInfo, db);
        }


        public static void AddOrUpdateServer(ServerInfo serverInfo)
        {
            using (var db = new GameStatsDbContext())
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
        }





    }
}
