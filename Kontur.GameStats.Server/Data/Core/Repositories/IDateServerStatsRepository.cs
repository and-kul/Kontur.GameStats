using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IDateServerStatsRepository : IRepository<DateServerStats>
    {
        DateServerStats FindOrAddDateServerStats(int year, int dayOfYear, Models.Server server);
        DateServerStats GetMostPopularDayForServer(Models.Server server);

    }
}