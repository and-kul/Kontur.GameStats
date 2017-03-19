using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

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
                Db.ServersGameModes.FirstOrDefault(sgm => sgm.ServerId == server.Id && sgm.GameModeId == gameMode.Id);

            if (serverGameMode != null) return serverGameMode;

            serverGameMode = new ServerGameModeStats
            {
                Server = server,
                GameMode = gameMode,
                MatchesPlayed = 0
            };

            Db.ServersGameModes.Add(serverGameMode);
            
            return serverGameMode;
        }
    }
}
