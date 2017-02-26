using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server.Info
{
    public class ScoreInfo
    {


        [JsonIgnore]
        public int Position;

        public string Name;

        public int Frags;
        public int Kills;
        public int Deaths;
    }
}
