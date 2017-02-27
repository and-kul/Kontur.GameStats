using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;
using Kontur.GameStats.Server.Info;
using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server.NancyModules
{
    public class PutMatchInfoModule : NancyModule
    {
        public PutMatchInfoModule()
        {
            Put["/servers/{endpoint}/matches/{timestamp:datetime}"] = parameters =>
            {
                var matchInfo = this.Bind<MatchInfo>();
                for (var i = 0; i < matchInfo.Scoreboard.Length; ++i)
                {
                    matchInfo.Scoreboard[i].Position = i;
                }


                using (var db = new GameStatsDbContext())
                {
                    var server = DatabaseHelper.FindServer(matchInfo.Endpoint, db);
                    if (server == null)
                    {
                        return HttpStatusCode.BadRequest;
                    }

                    DatabaseHelper.AddNewMatch(matchInfo, server, db);

                }

                
                return HttpStatusCode.OK;
            };
        }
    }
}
