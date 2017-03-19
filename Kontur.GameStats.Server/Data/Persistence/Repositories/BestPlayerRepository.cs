using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class BestPlayerRepository : Repository<BestPlayer>, IBestPlayerRepository
    {
        public BestPlayerRepository(GameStatsDbContext db) : base(db)
        {
        }

        public BestPlayer FindOrAddBestPlayer(Player player)
        {
            var bestPlayer = Db.BestPlayers.FirstOrDefault(best => best.PlayerId == player.Id);

            if (bestPlayer != null) return bestPlayer;

            bestPlayer = new BestPlayer { Player = player };
            Db.BestPlayers.Add(bestPlayer);
            
            return bestPlayer;
        }

        public IEnumerable<BestPlayer> GetTop(int count)
        {
            return Db.BestPlayers.OrderByDescending(best => best.KillToDeathRatio).Take(count).ToList();
        }
    }
}
