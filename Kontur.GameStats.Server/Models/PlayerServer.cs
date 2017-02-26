using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("PlayersServers")]
    public class PlayerServer
    {
        [Key, Column(Order = 0)]
        [Index("IX_PlayersServers_PlayerIdAndMatchesPlayed", 0)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Key, Column(Order = 1)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Index("IX_PlayersServers_PlayerIdAndMatchesPlayed", 1)]
        public int MatchesPlayed { get; set; }

    }
}