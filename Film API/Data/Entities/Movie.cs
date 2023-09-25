using System.ComponentModel.DataAnnotations;

namespace Film_API.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(50)]
        public string Picture { get; set; }

        [StringLength(50)]
        public string Trailer { get; set; }
    }
}
