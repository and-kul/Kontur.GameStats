using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kontur.GameStats.Server.Models
{
    public class Map
    {
        public int Id { get; set; }

        [Required, Index(IsUnique = true)]
        public string Name { get; set; }
    }
}