using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(GameStatsDbContext db) : base(db)
        {
        }


        public Player FindOrAddPlayer(string playerName)
        {
            var playerNameLowerCase = playerName.ToLowerInvariant();

            var player = Db.Players.FirstOrDefault(p => p.NameLowerCase == playerNameLowerCase);

            if (player != null) return player;

            player = new Player
            {
                NameLowerCase = playerNameLowerCase,
                Statistics = new PlayerStatistics()
            };
            Db.Players.Add(player);
            
            return player;
        }


        public Player FindPlayer(string playerName)
        {
            var playerNameLowerCase = playerName.ToLowerInvariant();
            return Db.Players.FirstOrDefault(p => p.NameLowerCase == playerNameLowerCase);
        }

        
        
    }
}
