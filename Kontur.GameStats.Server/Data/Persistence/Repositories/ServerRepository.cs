using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

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

        
    }
}
