using System;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetAllServersInfoModule : NancyModule
    {
        public GetAllServersInfoModule()
        {
            Get["/servers/info"] = _ =>
            {
                using (var db = new GameStatsDbContext())
                {
                    //db.Database.Log = s => Console.WriteLine(s);

                    return db.Servers.Select(s => new
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