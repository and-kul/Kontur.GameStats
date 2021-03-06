using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("ServersStatistics")]
    public class ServerStatistics
    {
        [Key, ForeignKey("Server")]
        public int ServerId { get; set; }

        public virtual Server Server { get; set; }

        public int TotalMatchesPlayed { get; set; }

        public int MaximumPopulation { get; set; }
 
        public int SumOfPopulations { get; set; }

        public DateTime FirstMatchTimestamp { get; set; }
        
        public virtual ICollection<ServerGameModeStats> GameModesStats { get; set; } = new List<ServerGameModeStats>();

        public virtual ICollection<ServerMapStats> MapsStats { get; set; } = new List<ServerMapStats>();

        public virtual ICollection<DateServerStats> WorkingDays { get; set; } = new List<DateServerStats>();
        
    }
}