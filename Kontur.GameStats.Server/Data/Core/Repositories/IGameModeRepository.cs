using System.Collections.Generic;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IGameModeRepository : IRepository<GameMode>
    {
        GameMode FindOrAddGameMode(string gameModeName);
        IEnumerable<GameMode> FindOrAddGameModes(IEnumerable<string> gameModeNames);
    }
}
