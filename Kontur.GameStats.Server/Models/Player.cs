using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required, Index(IsUnique = true)]
        public string NameLowerCase { get; set; }

        public virtual PlayerStatistics Statistics { get; set; }
    }
}