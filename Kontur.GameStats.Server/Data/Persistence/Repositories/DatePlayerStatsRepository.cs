using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class DatePlayerStatsRepository : Repository<DatePlayerStats>, IDatePlayerStatsRepository
    {
        public DatePlayerStatsRepository(GameStatsDbContext db) : base(db)
        {
        }

        public DatePlayerStats FindOrAddDatePlayerStats(int year, int dayOfYear, Player player)
        {
            var datePlayerStats =
                Db.DatePlayerStats.FirstOrDefault(
                    dp => dp.Year == year && dp.DayOfYear == dayOfYear && dp.PlayerId == player.Id);

            if (datePlayerStats != null) return datePlayerStats;

            datePlayerStats = new DatePlayerStats
            {
                Year = year,
                DayOfYear = dayOfYear,
                Player = player,
                MatchesPlayed = 0
            };

            Db.DatePlayerStats.Add(datePlayerStats);
            
            return datePlayerStats;
        }


        public DatePlayerStats GetMostPopularDayForPlayer(Player player)
        {
            return Db.DatePlayerStats.Where(dp => dp.PlayerId == player.Id)
                .OrderByDescending(dp => dp.MatchesPlayed)
                .First();
        }


    }
}