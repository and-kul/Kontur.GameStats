using Kontur.GameStats.Server.Data.Persistence;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetServerStatsModule : NancyModule
    {
        public GetServerStatsModule()
        {
            Get["/servers/{endpoint}/stats"] = parameters =>
            {
                var endpoint = parameters.endpoint;

                using (var unitOfWork = new UnitOfWork())
                {
                    var server = unitOfWork.Servers.FindServer(endpoint);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    return unitOfWork.GetServerStats(server);
                }
                
            };
        }
    }
}
