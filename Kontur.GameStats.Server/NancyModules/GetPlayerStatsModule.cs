using System.Net;
using Kontur.GameStats.Server.Data.Persistence;
using Nancy;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetPlayerStatsModule : NancyModule
    {
        public GetPlayerStatsModule()
        {
            Get["/players/{name}/stats"] = parameters =>
            {
                var name = WebUtility.UrlDecode((string)parameters.name);

                using (var unitOfWork = new UnitOfWork())
                {
                    var player = unitOfWork.Players.FindPlayer(name);

                    if (player == null)
                        return HttpStatusCode.NotFound;

                    return unitOfWork.GetPlayerStats(player);
                }

            };
        }
    }
}