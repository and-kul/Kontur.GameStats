using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("ServerGameModeStats")]
    public class ServerGameModeStats
    {
        [Key, Column(Order = 0)]
        [Index("IX_ServerGameModeStats_ServerIdAndMathesPlayed", 0)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Key, Column(Order = 1)]
        public int GameModeId { get; set; }
        public virtual GameMode GameMode { get; set; }

        [Index("IX_ServerGameModeStats_ServerIdAndMathesPlayed", 1)]
        public int MatchesPlayed { get; set; }
    }
}