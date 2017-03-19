using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Core.Repositories
{
    public interface IMapRepository : IRepository<Map>
    {
        Map FindOrAddMap(string mapName);
    }
}
