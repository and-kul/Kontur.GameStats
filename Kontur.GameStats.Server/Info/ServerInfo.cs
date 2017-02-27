using System.Linq;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server.Info
{
    public class ServerInfo
    {
        public ServerInfo()
        {
        }

        public ServerInfo(Models.Server server)
        {
            Endpoint = server.Endpoint;
            Name = server.Name;
            GameModes = server.AvailableGameModes.Select(gm => gm.Name).ToArray();
        }

        [JsonIgnore] public string Endpoint;

        public string Name;
        public string[] GameModes;
    }
}