using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Stats;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetServerStats : NancyModule
    {
        public GetServerStats()
        {
            Get["/servers/{endpoint}/stats"] = parameters =>
            {
                var endpoint = parameters.endpoint;

                using (var db = new GameStatsDbContext())
                {
                    var server = DatabaseHelper.FindServer(endpoint, db);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    return new ServerStats(server);
                }

            };
        }
    }
}
