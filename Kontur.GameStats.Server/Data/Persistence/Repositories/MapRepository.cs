using System.Linq;
using Kontur.GameStats.Server.Data.Core.Repositories;
using Kontur.GameStats.Server.Models;

namespace Kontur.GameStats.Server.Data.Persistence.Repositories
{
    public class MapRepository : Repository<Map>, IMapRepository
    {
        public MapRepository(GameStatsDbContext db) : base(db)
        {
        }

        public Map FindOrAddMap(string mapName)
        {
            var map = Db.Maps.FirstOrDefault(m => m.Name == mapName);

            if (map != null) return map;

            map = new Map { Name = mapName };
            Db.Maps.Add(map);

            return map;
        }
    }
}
