using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    public class Score
    {
        [Key, Column(Order = 0)]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }

        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int Position { get; set; }

        public int Frags { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }

    }
}