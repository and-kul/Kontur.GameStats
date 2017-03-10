using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    [Table("Matches")]
    public class Match
    {
        public int Id { get; set; }

        [Index("IX_Matches_TimestampAndServerId", 1)]
        public int ServerId { get; set; }
        public virtual Server Server { get; set; }

        [Index("IX_Matches_TimestampAndServerId", 0)]
        [Index("IX_Matches_IsProcessedForStatisticsAndTimestamp", 1)]
        public DateTime Timestamp { get; set; }

        [Index("IX_Matches_IsProcessedForStatisticsAndTimestamp", 0)]
        public bool IsProcessedForStatistics { get; set; }

        public int MapId { get; set; }
        public virtual Map Map { get; set; }

        public int GameModeId { get; set; }
        public virtual GameMode GameMode { get; set; }

        public int FragLimit { get; set; }
        public int TimeLimit { get; set; }
        public double TimeElapsed { get; set; }

        public virtual ICollection<Score> Scoreboard { get; set; } = new List<Score>();
    }
}