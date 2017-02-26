using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Info;
using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server.NancyModules
{
    public class PutServerInfoModule : NancyModule
    {
        public PutServerInfoModule()
        {
            Put["/servers/{endpoint}/info"] = parameters =>
            {
                var serverInfo = this.Bind<ServerInfo>();

                DatabaseHelper.AddOrUpdateServer(serverInfo); 

                return HttpStatusCode.OK;
            };
        }
    }
}
