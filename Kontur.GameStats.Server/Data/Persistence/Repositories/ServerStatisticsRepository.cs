using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class ServerStatisticsRepository : Repository<ServerStatistics>, IServerStatisticsRepository
    {
        public ServerStatisticsRepository(GameStatsDbContext db) : base(db)
        {
        }
    }
}