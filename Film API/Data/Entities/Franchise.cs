using System.ComponentModel.DataAnnotations;

namespace Film_API.Data.Entities
{
    public class Franchise
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
