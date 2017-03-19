using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.Stats;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class ServerGameModeStatsRepository : Repository<ServerGameModeStats>, IServerGameModeStatsRepository
    {
        public ServerGameModeStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public ServerGameModeStats FindOrAddServerGameModeStats(Models.Server server, GameMode gameMode)
        {
            var serverGameMode =
                Db.ServerGameModeStats.FirstOrDefault(sgm => sgm.ServerId == server.Id && sgm.GameModeId == gameMode.Id);

            if (serverGameMode != null) return serverGameMode;

            serverGameMode = new ServerGameModeStats
            {
                Server = server,
                GameMode = gameMode,
                MatchesPlayed = 0
            };

            Db.ServerGameModeStats.Add(serverGameMode);
            
            return serverGameMode;
        }

        public IEnumerable<ServerGameModeStats> FindTopGameModesForServer(Models.Server server, int count)
        {
            return Db.ServerGameModeStats
                .Where(gm => gm.ServerId == server.Id)
                .OrderByDescending(gm => gm.MatchesPlayed)
                .Take(ServerStats.TopGameModesCount)
                .ToList();
        }

    }
}
