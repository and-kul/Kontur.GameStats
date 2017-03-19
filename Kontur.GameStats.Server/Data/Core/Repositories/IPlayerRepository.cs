using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player FindOrAddPlayer(string playerName);
        Player FindPlayer(string playerName);
    }
}
