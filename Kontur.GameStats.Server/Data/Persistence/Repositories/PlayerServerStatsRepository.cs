using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class PlayerServerStatsRepository : Repository<PlayerServerStats>, IPlayerServerStatsRepository
    {
        public PlayerServerStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public PlayerServerStats FindOrAddPlayerServerStats(Player player, Models.Server server)
        {
            var playerServerStats =
                Db.PlayersServers.FirstOrDefault(ps => ps.PlayerId == player.Id && ps.ServerId == server.Id);

            if (playerServerStats != null) return playerServerStats;

            playerServerStats = new PlayerServerStats
            {
                Player = player,
                Server = server,
                MatchesPlayed = 0
            };

            Db.PlayersServers.Add(playerServerStats);
            
            return playerServerStats;
        }
    }
}