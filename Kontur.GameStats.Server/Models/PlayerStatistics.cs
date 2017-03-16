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
        
        public double SumOfScoreboardPercents { get; set; }

        public DateTime FirstMatchTimestamp { get; set; }

        public DateTime LastMatchTimestamp { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        [NotMapped]
        public double KillToDeathRatio
        {
            get { return Deaths == 0 ? Double.PositiveInfinity : (double) Kills / Deaths; }
            protected set { }
        }

        public virtual ICollection<PlayerServerStats> ServersStats { get; set; } = new List<PlayerServerStats>();

        public virtual ICollection<PlayerGameModeStats> GameModesStats { get; set; } = new List<PlayerGameModeStats>();

        public virtual ICollection<DatePlayerStats> GameDays { get; set; } = new List<DatePlayerStats>();

    }
}