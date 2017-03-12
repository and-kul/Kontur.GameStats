using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Reports;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetRecentMatches: NancyModule
    {
        public GetRecentMatches()
        {
            Get["/reports/recent-matches/{count?5}"] = parameters =>
            {
                var count = (int) parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;

                
                using (var db = new GameStatsDbContext())
                {
                    var recentMatches =
                        db.Matches.OrderByDescending(match => match.Timestamp)
                            .Take(count)
                            .AsEnumerable()
                            .Select(match => new RecentMatch(match)).ToArray();

                    return recentMatches;
                }

            };
        }


    }
}
