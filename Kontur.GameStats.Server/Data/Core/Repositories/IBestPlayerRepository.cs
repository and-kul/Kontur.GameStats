using System.Collections.Generic;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IBestPlayerRepository : IRepository<BestPlayer>
    {
        BestPlayer FindOrAddBestPlayer(Player player);
        IEnumerable<BestPlayer> GetTop(int count);

    }
}
