using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Helpers;
using Kontur.GameStats.Server.Info;
using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class GetMatchInfoModule : NancyModule
    {
        public GetMatchInfoModule()
        {
            Get["/servers/{endpoint}/matches/{timestamp}"] = parameters =>
            {
                var endpoint = parameters.endpoint;
                var timestampString = parameters.timestamp;
                if (!TimestampConverter.IsCorrectTimestamp(timestampString))
                    return HttpStatusCode.BadRequest;

                var timestamp = TimestampConverter.Parse(timestampString);

                using (var db = new GameStatsDbContext())
                {
                    var server = DatabaseHelper.FindServer(endpoint, db);

                    if (server == null)
                        return HttpStatusCode.NotFound;

                    var match = DatabaseHelper.FindMatch(server, timestamp, db);

                    if (match == null)
                        return HttpStatusCode.NotFound;
                    
                    return new MatchInfo(match);
                }

            };
        }
    }
}
