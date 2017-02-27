using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("PlayersStatistics")]
    public class PlayerStatistics
    {
        [Key, ForeignKey("Player")]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        
        public int TotalMatchesPlayed { get; set; }

        public int TotalMatchesWon { get; set; }
        
        public int SumOfScoreboardPercents { get; set; }

        //public int? LastMatchId { get; set; }

        //public virtual Match LastMatch { get; set; }

        public DateTime LastMatchTimestamp { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }
        
        [Index]
        public double KillToDeathRatio
        {
            get { return Deaths == 0 ? Double.PositiveInfinity : (double) Kills / Deaths; }
            protected set { }
        }

        public virtual ICollection<PlayerServer> ServersStats { get; set; } = new List<PlayerServer>();

        public virtual ICollection<PlayerGameMode> GameModesStats { get; set; } = new List<PlayerGameMode>();

    }
}