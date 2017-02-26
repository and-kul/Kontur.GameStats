using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    public class Server
    {
        public int Id { get; set; }

        [Required, Index(IsUnique = true)]
        public string Endpoint { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ServerGameMode> GameModes { get; set; } = new List<ServerGameMode>();

        public virtual ServerStatistics Statistics { get; set; }

        //public DateTime? FirstMatchDay { get; set; }

    }
}