using System;
using Kontur.GameStats.Server.Data.Persistence;
using Kontur.GameStats.Server.Info;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetMatchInfoModule : NancyModule
    {
        public GetMatchInfoModule()
        {
            Get["/servers/{endpoint}/matches/{timestamp:datetime}"] = parameters =>
            {
                string endpoint = parameters.endpoint;
                DateTime timestamp = parameters.timestamp;

                using (var unitOfWork = new UnitOfWork())
                {
                    var server = unitOfWork.Servers.FindServer(endpoint);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    var match = unitOfWork.Matches.FindMatch(server, timestamp);

                    if (match == null)
                        return HttpStatusCode.NotFound;

                    return new MatchInfo(match);
                }
                
            };
        }
    }
}
