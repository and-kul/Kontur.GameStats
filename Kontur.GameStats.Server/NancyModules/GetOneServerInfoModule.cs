using Kontur.GameStats.Server.Data.Persistence;
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

                using (var unitOfWork = new UnitOfWork())
                {
                    var server = unitOfWork.Servers.FindServer(endpoint);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    return new ServerInfo(server);

                }
                
            };
        }
    }
}
