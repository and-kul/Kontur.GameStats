using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IServerMapStatsRepository : IRepository<ServerMapStats>
    {
        ServerMapStats FindOrAddServerMapStats(Models.Server server, Map map);
    }

}