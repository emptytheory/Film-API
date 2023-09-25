using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Film_API.Data.Entities
{
    [Table(nameof(Character))]

    public class Character
    {
        public int Id {  get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string? Alias { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public string? Picture { get; set; }

        // Navigation
        public HashSet<Movie> Movies { get; set; }
    }
}
