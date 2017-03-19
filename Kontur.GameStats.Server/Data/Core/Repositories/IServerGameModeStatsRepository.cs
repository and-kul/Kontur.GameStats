using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IServerGameModeStatsRepository : IRepository<ServerGameModeStats>
    {
        ServerGameModeStats FindOrAddServerGameModeStats(Models.Server server, GameMode gameMode);
        IEnumerable<ServerGameModeStats> FindTopGameModesForServer(Models.Server server, int count);
    }
}
