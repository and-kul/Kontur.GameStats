using System.Collections.Generic;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IServerRepository : IRepository<Models.Server>
    {
        Models.Server FindServer(string endpoint);

        Models.Server AddNewServer(string endpoint, string name, IEnumerable<GameMode> availableGameModes);
        void UpdateExistingServer(Models.Server server, string newName, IEnumerable<GameMode> newAvailableGameModes);
        
    }

}
