using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("DateServerStats")]
    public class DateServerStats
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int DayOfYear { get; set; }

        [Key, Column(Order = 2)]
        [Index("IX_DateServerStats_ServerIdAndMatchesPlayed", 0)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Index("IX_DateServerStats_ServerIdAndMatchesPlayed", 1)]
        public int MatchesPlayed { get; set; }


    }
}