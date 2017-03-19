using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class ServerMapStatsRepository : Repository<ServerMapStats>, IServerMapStatsRepository
    {
        public ServerMapStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public ServerMapStats FindOrAddServerMapStats(Models.Server server, Map map)
        {
            var serverMap = Db.ServersMaps.FirstOrDefault(sm => sm.ServerId == server.Id && sm.MapId == map.Id);

            if (serverMap != null) return serverMap;

            serverMap = new ServerMapStats
            {
                Server = server,
                Map = map,
                MatchesPlayed = 0
            };

            Db.ServersMaps.Add(serverMap);
            return serverMap;
        }
    }
}
