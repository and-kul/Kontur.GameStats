using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.Stats;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class ServerMapStatsRepository : Repository<ServerMapStats>, IServerMapStatsRepository
    {
        public ServerMapStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public ServerMapStats FindOrAddServerMapStats(Models.Server server, Map map)
        {
            var serverMap = Db.ServerMapStats.FirstOrDefault(sm => sm.ServerId == server.Id && sm.MapId == map.Id);

            if (serverMap != null) return serverMap;

            serverMap = new ServerMapStats
            {
                Server = server,
                Map = map,
                MatchesPlayed = 0
            };

            Db.ServerMapStats.Add(serverMap);
            return serverMap;
        }


        public IEnumerable<ServerMapStats> FindTopMapsForServer(Models.Server server, int count)
        {
            return Db.ServerMapStats
                .Where(sm => sm.ServerId == server.Id)
                .OrderByDescending(sm => sm.MatchesPlayed)
                .Take(ServerStats.TopMapsCount)
                .ToList();
        }

    }
}
