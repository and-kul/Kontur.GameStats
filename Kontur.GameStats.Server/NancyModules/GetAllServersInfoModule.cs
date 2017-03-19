using System.Linq;
using Kontur.GameStats.Server.Data.Persistence;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetAllServersInfoModule : NancyModule
    {
        public GetAllServersInfoModule()
        {
            Get["/servers/info"] = _ =>
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.Servers.All().Select(s => new
                    {
                        Endpoint = s.Endpoint,
                        Info = new
                        {
                            Name = s.Name,
                            GameModes = s.AvailableGameModes.Select(gm => gm.Name)
                        }
                    }).ToArray();
                }
            };
        }
    }
}