using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IDatePlayerStatsRepository : IRepository<DatePlayerStats>
    {
        DatePlayerStats FindOrAddDatePlayerStats(int year, int dayOfYear, Player player);
    }
}