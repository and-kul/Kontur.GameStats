using Kontur.GameStats.Server.Data.Persistence;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetPopularServersModule : NancyModule
    {
        public GetPopularServersModule()
        {
            Get["/reports/popular-servers/{count?5}"] = parameters =>
            {
                var count = (int)parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;

                
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.GetPopularServers(count);

                }

            };
        }
    }
}
