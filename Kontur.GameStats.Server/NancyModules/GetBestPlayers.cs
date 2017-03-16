using System.Linq;
using Kontur.GameStats.Server.Database;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetBestPlayers : NancyModule
    {
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
                        db.BestPlayers
                            .OrderByDescending(best => best.KillToDeathRatio)
                            .Take(count)
                            .Select(best => new
                            {
                                Name = best.Player.NameLowerCase,
                                KillToDeathRatio = best.KillToDeathRatio
                            })
                            .ToArray();

                    return bestPlayers;
                }
            };
        }
    }
}