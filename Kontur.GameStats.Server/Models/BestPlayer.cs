using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("BestPlayers")]
    public class BestPlayer
    {
        // todo изменить
        public const int NeedTotalMatches = 10;

        [Key, ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        
        [Index]
        public double KillToDeathRatio
        {
            get { return Player?.Statistics.KillToDeathRatio ?? double.NaN; }
            protected set { }
        }
    }
}
