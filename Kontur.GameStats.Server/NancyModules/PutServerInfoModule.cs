using Kontur.GameStats.Server.Data.Persistence;
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

                using (var unitOfWork = new UnitOfWork())
                {
                    unitOfWork.AddOrUpdateServer(serverInfo);
                    unitOfWork.Save();
                }
                
                return HttpStatusCode.OK;
            };
        }
    }
}
