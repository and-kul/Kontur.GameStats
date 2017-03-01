using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("PlayersGameModes")]
    public class PlayerGameModeStats
    {
        [Key, Column(Order = 0)]
        [Index("IX_PlayersGameModes_PlayerIdAndMatchesPlayed", 0)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Key, Column(Order = 1)]
        public int GameModeId { get; set; }
        public virtual GameMode GameMode { get; set; }

        [Index("IX_PlayersGameModes_PlayerIdAndMatchesPlayed", 1)]
        public int MatchesPlayed { get; set; }

    }
}