using System.Linq;
using Kontur.GameStats.Server.Database;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetBestPlayers : NancyModule
    {
        private const int NeedMatches = 10;


        public GetBestPlayers()
        {
            Get["/reports/best-players/{count?5}"] = parameters =>
            {
                var count = (int) parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;


                using (var db = new GameStatsDbContext())
                {
                    var inf = double.PositiveInfinity;
                    
                    // todo чекнуть на производительность
                    var bestPlayers =
                        db.PlayersStatistics
                            .Where(
                                ps =>
                                    ps.TotalMatchesPlayed >= NeedMatches &&
                                    ps.KillToDeathRatio < inf)
                            .OrderByDescending(ps => ps.KillToDeathRatio)
                            .Take(count)
                            .Select(ps => new
                            {
                                Name = ps.Player.NameLowerCase,
                                KillToDeathRatio = ps.KillToDeathRatio
                            })
                            .ToArray();

                    return bestPlayers;
                }
            };
        }
    }
}