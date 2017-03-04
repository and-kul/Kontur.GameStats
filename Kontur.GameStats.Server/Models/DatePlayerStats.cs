using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("DatePlayerStats")]
    public class DatePlayerStats
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int DayOfYear { get; set; }

        [Key, Column(Order = 2)]
        [Index("IX_DatePlayerStats_PlayerIdAndMatchesPlayed", 0)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Index("IX_DatePlayerStats_PlayerIdAndMatchesPlayed", 1)]
        public int MatchesPlayed { get; set; }
        
    }
}
