using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(GameStatsDbContext db) : base(db)
        {
        }

        public Match FindById(int id)
        {
            return Db.Matches.Find(id);
        }

        public Match FindMatch(Models.Server server, DateTime timestamp)
        {
            return Db.Matches.FirstOrDefault(m => m.Timestamp == timestamp && m.ServerId == server.Id);
        }

        public IEnumerable<int> GetNotProcessedMatchesIds()
        {
            return
                Db
                .Matches
                .Where(m => !m.IsProcessedForStatistics)
                .OrderBy(m => m.Timestamp)
                .Select(m => m.Id)
                .ToList();
        }

        public DateTime GetLastMatchTimestampAmongAllServers()
        {
            return Db.Matches.OrderByDescending(match => match.Timestamp).First().Timestamp;
        }

        public IEnumerable<Match> GetRecentMatches(int count)
        {
            return Db.Matches.OrderByDescending(match => match.Timestamp).Take(count).ToList();
        }

        
    }
}
