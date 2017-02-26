using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.Models;
using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server.NancyModules
{
    class ServerInfo
    {
        public string Endpoint;
        public string Name;
        public string[] GameModes;
    }



    public class PutServerInfoModule : NancyModule
    {
        
        public PutServerInfoModule()
        {
            Put["/servers/{endpoint}/info"] = parameters =>
            {
                var serverInfo = this.Bind<ServerInfo>();

                DatabaseHelper.AddOrUpdateServer(serverInfo); 

                return HttpStatusCode.OK;
            };
        }
    }
}
