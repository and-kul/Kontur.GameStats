using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("ServerMapStats")]
    public class ServerMapStats
    {
        [Key, Column(Order = 0)]
        [Index("IX_ServerMapStats_ServerIdAndMatchesPlayed", 0)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Key, Column(Order = 1)]
        public int MapId { get; set; }
        public virtual Map Map { get; set; }

        [Index("IX_ServerMapStats_ServerIdAndMatchesPlayed", 1)]
        public int MatchesPlayed { get; set; }

    }
}