using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.Stats;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class ServerRepository : Repository<Models.Server>, IServerRepository
    {
        public ServerRepository(GameStatsDbContext db) : base(db)
        {
        }

        public Models.Server FindServer(string endpoint)
        {
            return Db.Servers.FirstOrDefault(s => s.Endpoint == endpoint);
        }

        public Models.Server AddNewServer(string endpoint, string name, IEnumerable<GameMode> availableGameModes)
        {
            var server = new Models.Server
            {
                Endpoint = endpoint,
                Name = name,
                Statistics = new ServerStatistics()
            };
            
            foreach (var gameMode in availableGameModes)
            {
                server.AvailableGameModes.Add(gameMode);
            }

            Db.Servers.Add(server);
            return server;
        }

        public void UpdateExistingServer(Models.Server server, string newName, IEnumerable<GameMode> newAvailableGameModes)
        {
            server.Name = newName;
            server.AvailableGameModes.Clear();

            foreach (var gameMode in newAvailableGameModes)
            {
                server.AvailableGameModes.Add(gameMode);
            }

            Db.Entry(server).State = EntityState.Modified;
            
        }

        //public ServerInfo GetServerInfo(Models.Server server)
        //{
        //    if (server == null)
        //        return null;

        //    var serverInfo = new ServerInfo
        //    {
        //        Endpoint = server.Endpoint,
        //        Name = server.Name,
        //        GameModes = server.AvailableGameModes.Select(gm => gm.Name).ToArray()
        //    };

        //    return serverInfo;
        //}

        //public IEnumerable<ServerInfo> GetAllServerInfos()
        //{
        //    var result = new List<ServerInfo>();

        //    foreach (var server in Db.Servers)
        //    {
        //        result.Add(GetServerInfo(server));
        //    }

        //    return result;
        //}
        

    }
}
