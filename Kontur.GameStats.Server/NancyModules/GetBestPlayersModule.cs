using System.Linq;
using Kontur.GameStats.Server.Data.Persistence;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetBestPlayersModule : NancyModule
    {
        public GetBestPlayersModule()
        {
            Get["/reports/best-players/{count?5}"] = parameters =>
            {
                var count = (int) parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;


                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.BestPlayers.GetTop(count)
                        .Select(best => new
                        {
                            Name = best.Player.NameLowerCase,
                            KillToDeathRatio = best.KillToDeathRatio
                        })
                        .ToArray();
                }
            };
        }
    }
}