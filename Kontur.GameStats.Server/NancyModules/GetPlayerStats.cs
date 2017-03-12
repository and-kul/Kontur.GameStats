using System.Net;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Stats;
using Nancy;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetPlayerStats : NancyModule
    {
        public GetPlayerStats()
        {
            Get["/players/{name}/stats"] = parameters =>
            {
                var name = WebUtility.UrlDecode((string)parameters.name);

                using (var db = new GameStatsDbContext())
                {
                    var player = DatabaseHelper.FindPlayer(name, db);

                    if (player == null)
                        return HttpStatusCode.NotFound;

                    return new PlayerStats(player);
                }

            };
        }
    }
}