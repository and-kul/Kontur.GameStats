using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class PlayerGameModeStatsRepository : Repository<PlayerGameModeStats>, IPlayerGameModeStatsRepository
    {
        public PlayerGameModeStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public PlayerGameModeStats FindOrAddPlayerGameModeStats(Player player, GameMode gameMode)
        {
            var playerGameModeStats =
                Db.PlayerGameModeStats.FirstOrDefault(pgm => pgm.PlayerId == player.Id && pgm.GameModeId == gameMode.Id);

            if (playerGameModeStats != null) return playerGameModeStats;

            playerGameModeStats = new PlayerGameModeStats
            {
                Player = player,
                GameMode = gameMode,
                MatchesPlayed = 0
            };

            Db.PlayerGameModeStats.Add(playerGameModeStats);
          
            return playerGameModeStats;
        }
        
        public GameMode GetFavoriteGameModeForPlayer(Player player)
        {
            return Db.PlayerGameModeStats
                .Where(gm => gm.PlayerId == player.Id)
                .OrderByDescending(gm => gm.MatchesPlayed)
                .First()
                .GameMode;
        }
    }
}