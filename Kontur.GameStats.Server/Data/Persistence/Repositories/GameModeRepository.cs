using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class GameModeRepository : Repository<GameMode>, IGameModeRepository
    {
        public GameModeRepository(GameStatsDbContext db) : base(db)
        {
        }

        public GameMode FindOrAddGameMode(string gameModeName)
        {
            var gameMode = Db.GameModes.FirstOrDefault(gm => gm.Name == gameModeName);

            if (gameMode != null) return gameMode;

            gameMode = new GameMode { Name = gameModeName };
            Db.GameModes.Add(gameMode);
            
            return gameMode;
        }

        public IEnumerable<GameMode> FindOrAddGameModes(IEnumerable<string> gameModeNames)
        {
            List<GameMode> result = new List<GameMode>();

            foreach (var gameModeName in gameModeNames)
            {
                result.Add(FindOrAddGameMode(gameModeName));
            }

            return result;
        }

    }
}
