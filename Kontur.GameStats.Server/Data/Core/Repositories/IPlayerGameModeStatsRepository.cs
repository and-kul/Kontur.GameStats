using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IPlayerGameModeStatsRepository : IRepository<PlayerGameModeStats>
    {
        PlayerGameModeStats FindOrAddPlayerGameModeStats(Player player, GameMode gameMode);
    }
}