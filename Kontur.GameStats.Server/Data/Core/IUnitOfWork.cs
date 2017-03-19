using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Models;
using Kontur.GameStats.Server.Reports;
using Kontur.GameStats.Server.Stats;

namespace Kontur.GameStats.Server.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPlayerRepository Players { get; }
        IServerRepository Servers { get; }

        IGameModeRepository GameModes { get; }
        IMapRepository Maps { get; }
        
        IMatchRepository Matches { get; }

        IPlayerStatisticsRepository PlayerStatistics { get; }
        IServerStatisticsRepository ServerStatistics { get; }

        IPlayerServerStatsRepository PlayerServerStats { get; }
        IPlayerGameModeStatsRepository PlayerGameModeStats { get; }

        IServerGameModeStatsRepository ServerGameModeStats { get; }
        IServerMapStatsRepository ServerMapStats { get; }

        IDatePlayerStatsRepository DatePlayerStats { get; }
        IDateServerStatsRepository DateServerStats { get; }

        IBestPlayerRepository BestPlayers { get; }

        Models.Server AddOrUpdateServer(ServerInfo serverInfo);
        Match AddNewMatch(MatchInfo matchInfo, Models.Server server);

        ServerStats GetServerStats(Models.Server server);
        PlayerStats GetPlayerStats(Player player);
        PopularServer[] GetPopularServers(int count);

        int Save();

        
        
    }
}