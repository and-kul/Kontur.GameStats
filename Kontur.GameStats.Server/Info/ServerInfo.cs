using System.Linq;

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
            GameModes = server.GameModes.Select(gm => gm.GameMode.Name).ToArray();
        }

        public string Endpoint;
        public string Name;
        public string[] GameModes;
    }
}