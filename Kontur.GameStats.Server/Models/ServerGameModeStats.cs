using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("ServersGameModes")]
    public class ServerGameModeStats
    {
        [Key, Column(Order = 0)]
        [Index("IX_ServersGameModes_ServerIdAndMathesPlayed", 0)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Key, Column(Order = 1)]
        public int GameModeId { get; set; }
        public virtual GameMode GameMode { get; set; }

        [Index("IX_ServersGameModes_ServerIdAndMathesPlayed", 1)]
        public int MatchesPlayed { get; set; }
    }
}