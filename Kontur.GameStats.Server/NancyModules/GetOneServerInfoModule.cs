using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Info;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetOneServerInfoModule : NancyModule
    {
        public GetOneServerInfoModule()
        {
            Get["/servers/{endpoint}/info"] = parameters =>
            {
                var endpoint = parameters.endpoint;

                using (GameStatsDbContext db = new GameStatsDbContext())
                {
                    var server = DatabaseHelper.FindServer(endpoint, db);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    var serverInfo = new ServerInfo(server);

                    var result = new
                    {
                        Name = serverInfo.Name,
                        GameModes = serverInfo.GameModes
                    };

                    return result;
                }
                
            };
        }
    }
}
