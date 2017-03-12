using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Reports;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetPopularServers : NancyModule
    {
        public GetPopularServers()
        {
            Get["/reports/popular-servers/{count?5}"] = parameters =>
            {
                var count = (int)parameters.count;

                if (count < 0) count = 0;
                if (count > 50) count = 50;


                using (var db = new GameStatsDbContext())
                {
                    //db.Database.Log = s => Console.WriteLine(s);

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var popularServers =
                        db.Servers.AsEnumerable()
                            .Select(server => new PopularServer(server))
                            .OrderByDescending(server => server.AverageMatchesPerDay)
                            .Take(count)
                            .ToArray();
                        
                        return popularServers;
                    }
                    
                }

            };
        }
    }
}
