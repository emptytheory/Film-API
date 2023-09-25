using System.ComponentModel.DataAnnotations;

namespace Film_API.Data.Entities
{
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
    }
}
