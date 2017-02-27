using Kontur.GameStats.Server.Models;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server.Info
{
    public class ScoreInfo
    {
        public ScoreInfo()
        {
        }

        public ScoreInfo(Score score)
        {
            Position = score.Position;
            Name = score.Player.NameLowerCase;
            Frags = score.Frags;
            Kills = score.Kills;
            Deaths = score.Deaths;
        }

        [JsonIgnore] public int Position;

        public string Name;

        public int Frags;
        public int Kills;
        public int Deaths;
    }
}