using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        Match FindById(int id);
        Match FindMatch(Models.Server server, DateTime timestamp);

        IEnumerable<int> GetNotProcessedMatchesIds();

        DateTime GetLastMatchTimestampAmongAllServers();
        
        IEnumerable<Match> GetRecentMatches(int count);

    }
}
