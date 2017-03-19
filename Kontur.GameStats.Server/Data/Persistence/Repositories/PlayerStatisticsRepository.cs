using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class PlayerStatisticsRepository : Repository<PlayerStatistics>, IPlayerStatisticsRepository
    {
        public PlayerStatisticsRepository(GameStatsDbContext db) : base(db)
        {
        }
    }
}