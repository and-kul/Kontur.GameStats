using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IPlayerServerStatsRepository : IRepository<PlayerServerStats>
    {
        PlayerServerStats FindOrAddPlayerServerStats(Player player, Models.Server server);
        Models.Server GetFavoriteServerForPlayer(Player player);
    }
}