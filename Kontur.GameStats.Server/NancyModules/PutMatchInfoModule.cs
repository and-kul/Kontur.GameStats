using Kontur.GameStats.Server.Data;
using Kontur.GameStats.Server.Data.Persistence;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.StatisticsManagement;
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

                using (var unitOfWork = new UnitOfWork())
                {
                    var server = unitOfWork.Servers.FindServer(matchInfo.Endpoint);
                    if (server == null)
                    {
                        return HttpStatusCode.BadRequest;
                    }

                    var match = unitOfWork.AddNewMatch(matchInfo, server);
                    unitOfWork.Save();

                    StatisticsProcessor.AddMatchId(match.Id);
                }
                
                return HttpStatusCode.OK;
            };
        }
    }
}
