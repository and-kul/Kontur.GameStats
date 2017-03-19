using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class DateServerStatsRepository : Repository<DateServerStats>, IDateServerStatsRepository
    {
        public DateServerStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public DateServerStats FindOrAddDateServerStats(int year, int dayOfYear, Models.Server server)
        {
            var dateServer =
                Db.DateServerStats.FirstOrDefault(
                    ds => ds.Year == year && ds.DayOfYear == dayOfYear && ds.ServerId == server.Id);

            if (dateServer != null) return dateServer;

            dateServer = new DateServerStats
            {
                Year = year,
                DayOfYear = dayOfYear,
                Server = server,
                MatchesPlayed = 0
            };

            Db.DateServerStats.Add(dateServer);
            
            return dateServer;
        }

        public DateServerStats GetMostPopularDayForServer(Models.Server server)
        {
            return Db.DateServerStats
                .Where(ds => ds.ServerId == server.Id)
                .OrderByDescending(ds => ds.MatchesPlayed)
                .FirstOrDefault();

        }



    }
}