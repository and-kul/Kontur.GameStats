using System.Linq;
using Kontur.GameStats.Server.Data.Persistence;
using Kontur.GameStats.Server.Reports;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetRecentMatchesModule: NancyModule
    {
        public GetRecentMatchesModule()
        {
            Get["/reports/recent-matches/{count?5}"] = parameters =>
            {
                var count = (int) parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;

                using (var unitOfWork = new UnitOfWork())
                {
                    var recentMatches =
                        unitOfWork.Matches.GetRecentMatches(count)
                        .Select(match => new RecentMatch(match))
                        .ToArray();

                    return recentMatches;
                }
                
            };
        }


    }
}
